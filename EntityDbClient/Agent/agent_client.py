"""EntityDbClient local Agent API client. Python 3.10+, standard library only.

Examples:
    python agent_client.py connections
    python agent_client.py connect https://localhost:6060 sa --password 1
    python agent_client.py rpc CONNECTION_ID GetTableList --params '[1]'
    python agent_client.py schema CONNECTION_ID 10
    python agent_client.py create-table CONNECTION_ID design.json
    python agent_client.py update-table CONNECTION_ID 10 design.json
"""
from __future__ import annotations

import argparse
import json
import os
import pathlib
import sys
import urllib.error
import urllib.parse
import urllib.request


class EntityDbClient:
    def __init__(self, discovery_path: str | pathlib.Path | None = None):
        if discovery_path is None:
            if os.name == "nt":
                root = pathlib.Path(os.environ["LOCALAPPDATA"])
            elif sys.platform == "darwin":
                root = pathlib.Path.home() / "Library/Application Support"
            else:
                root = pathlib.Path(os.environ.get("XDG_DATA_HOME", pathlib.Path.home() / ".local/share"))
            discovery_path = root / "EntityDbClient/agent.json"
        info = json.loads(pathlib.Path(discovery_path).read_text(encoding="utf-8-sig"))
        self.base_url = info["baseUrl"]
        address = urllib.parse.urlparse(self.base_url)
        if address.scheme != "http" or address.hostname != "127.0.0.1" or address.username:
            raise ValueError("Discovery must refer to the local EntityDbClient loopback API")
        self.token = info["token"]
        # Never forward local Agent credentials through a machine-wide HTTP proxy.
        self.opener = urllib.request.build_opener(urllib.request.ProxyHandler({}))

    def request(self, method: str, path: str, body=None, *, binary=False):
        data = body if isinstance(body, bytes) else None if body is None else json.dumps(body, ensure_ascii=False).encode("utf-8")
        request = urllib.request.Request(self.base_url + path, data=data, method=method,
            headers={"Authorization": "Bearer " + self.token,
                     "Content-Type": "application/octet-stream" if isinstance(body, bytes) else "application/json"})
        try:
            with self.opener.open(request, timeout=120) as response:
                raw = response.read()
                return raw if binary else json.loads(raw) if raw else None
        except urllib.error.HTTPError as error:
            detail = error.read().decode("utf-8", errors="replace")
            raise RuntimeError(f"EntityDbClient HTTP {error.code}: {detail}") from None

    def connections(self):
        return self.request("GET", "/api/connections")

    def connect(self, url: str, user_name: str, password: str | None = None):
        body = {"url": url, "userName": user_name}
        if password is not None:
            body["password"] = password
        return self.request("POST", "/api/connections", body)

    def rpc(self, connection_id: str, method: str, *parameters):
        return self.request("POST", f"/api/connections/{urllib.parse.quote(connection_id, safe='')}/rpc/{urllib.parse.quote(method, safe='')}", {"parameters": parameters})

    def schema(self, connection_id: str, table_id: int):
        return self.request("GET", f"/api/connections/{connection_id}/tables/{table_id}/schema")

    def create_table(self, connection_id: str, design: dict):
        return self.request("POST", f"/api/connections/{connection_id}/tables", design)

    def update_table(self, connection_id: str, table_id: int, design: dict):
        return self.request("PUT", f"/api/connections/{connection_id}/tables/{table_id}", design)


def main():
    parser = argparse.ArgumentParser(description=__doc__, formatter_class=argparse.RawDescriptionHelpFormatter)
    parser.add_argument("--discovery", help="Path to agent.json or agent-PID.json")
    commands = parser.add_subparsers(dest="command", required=True)
    for name in ("connections", "profiles", "capabilities", "state", "openapi"):
        commands.add_parser(name)
    connect = commands.add_parser("connect")
    connect.add_argument("url")
    connect.add_argument("user_name")
    connect.add_argument("--password", help="Omit to use the saved credential")
    disconnect = commands.add_parser("disconnect")
    disconnect.add_argument("connection_id")
    rpc = commands.add_parser("rpc")
    rpc.add_argument("connection_id")
    rpc.add_argument("method")
    rpc.add_argument("--params", default="[]", help="JSON positional parameter array; use @file.json to read from a file")
    schema = commands.add_parser("schema")
    schema.add_argument("connection_id")
    schema.add_argument("table_id", type=int)
    for name in ("create-table", "update-table"):
        command = commands.add_parser(name)
        command.add_argument("connection_id")
        if name == "update-table": command.add_argument("table_id", type=int)
        command.add_argument("design", help="JSON file, or - for stdin")
    args = parser.parse_args()
    try:
        client = EntityDbClient(args.discovery)
        if args.command == "connect":
            result = client.connect(args.url, args.user_name, args.password)
        elif args.command == "disconnect":
            result = client.request("DELETE", f"/api/connections/{args.connection_id}")
        elif args.command == "rpc":
            text = pathlib.Path(args.params[1:]).read_text(encoding="utf-8-sig") if args.params.startswith("@") else args.params
            parameters = json.loads(text)
            if not isinstance(parameters, list): raise ValueError("--params must be a JSON array")
            result = client.rpc(args.connection_id, args.method, *parameters)
        elif args.command == "schema":
            result = client.schema(args.connection_id, args.table_id)
        elif args.command in ("create-table", "update-table"):
            text = sys.stdin.read() if args.design == "-" else pathlib.Path(args.design).read_text(encoding="utf-8-sig")
            design = json.loads(text)
            result = client.create_table(args.connection_id, design) if args.command == "create-table" else client.update_table(args.connection_id, args.table_id, design)
        else:
            result = client.request("GET", "/openapi.json" if args.command == "openapi" else "/api/" + args.command)
        print(json.dumps(result, ensure_ascii=False, indent=2))
    except (OSError, ValueError, RuntimeError) as error:
        print(str(error), file=sys.stderr)
        return 1
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
