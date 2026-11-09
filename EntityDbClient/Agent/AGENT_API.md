# EntityDbClient Agent API v1

运行 EntityDbClient 后，Agent 通过本机 HTTP API 操作客户端，无须截图、鼠标或 computer use。
接口模块和搜索功能已移除，不在客户端和 Agent 能力目录中提供。
UI 和 API 共用 `ClientWorkspace`：相同连接、相同登录会话、相同表结构验证、相同 EJServer 权限。
API 的数据库结构写入会触发对应 Tab 的目录刷新；模块位置变更仅发出 layout-updated 状态，不触发目录或模块重载。已经打开的编辑页保留未保存修改，使用“重新加载”获取最新内容。

## 发现和认证

Windows 发现文件：`%LOCALAPPDATA%\EntityDbClient\agent.json`。
文件包含 `baseUrl`、`token`、`instanceId`、`processId`、OpenAPI 和能力发现 URL。
服务只监听 `127.0.0.1` 的随机端口。每次启动生成新 Token，所有路由均要求：

```http
Authorization: Bearer <token>
```

禁止带 `Origin` 的浏览器请求，不启用 CORS。不要将发现文件或 Token 提交到仓库。
多个客户端进程各有 `agent-<PID>.json`，`agent.json` 指向最近启动的实例。
退出后删除本实例发现文件。进程异常退出时可能残留；重新启动应用即可更新。
调用本机 API 时绕过 HTTP 代理。Python SDK 已自动处理。

## 快速调用（Python 标准库）

```python
from agent_client import EntityDbClient

client = EntityDbClient()
print(client.request("GET", "/api/capabilities"))
print(client.connections())

# 也可直接复用 UI 中已有的 connection ID。
# password 省略时，使用已成功连接过的相同地址和用户名的保存记录。
connection = client.connect("https://localhost:6060", "sa", "1")
cid = connection["id"]
projects = client.rpc(cid, "GetCurrentUserProjectList")
databases = client.rpc(cid, "GetDatabaseList", projects[0]["id"])
tables = client.rpc(cid, "GetTableList", databases[0]["id"])
schema = client.schema(cid, tables[0]["id"])
```

也可使用命令行：

```powershell
python EntityDbClient/Agent/agent_client.py connections
python EntityDbClient/Agent/agent_client.py connect https://localhost:6060 sa
python EntityDbClient/Agent/agent_client.py rpc CONNECTION_ID GetDatabaseList --params '[1]'
python EntityDbClient/Agent/agent_client.py schema CONNECTION_ID 10
python EntityDbClient/Agent/agent_client.py create-table CONNECTION_ID design.json
python EntityDbClient/Agent/agent_client.py update-table CONNECTION_ID 10 design.json
```

可通过 `--discovery <path>` 选择具体客户端进程。
对大参数或避免 shell 引号问题，使用 `--params @parameters.json`，或通过 stdin 传入设计文件：`design` 参数填 `-`。

## 创建表和修改表

`POST /api/connections/{id}/tables` 请求示例：

```json
{
  "table": { "DatabaseID": 1, "Name": "Accounts", "caption": "账户" },
  "columns": [
    { "Name": "id", "caption": "ID", "dbType": "int", "IsPKID": true, "IsAutoIncrement": true, "CanNull": false, "length": "" },
    { "Name": "Name", "caption": "名称", "dbType": "varchar", "length": "100", "CanNull": false, "IsPKID": false, "IsAutoIncrement": false }
  ],
  "indexes": [ { "ColumnNames": ["Name"], "IsUnique": true, "IsClustered": false } ],
  "classProperties": []
}
```

返回新建的 `DBTable`，含 `id`。这会实际创建数据库表。
`GET /api/connections/{id}/tables/{tableId}/schema` 返回 `Columns`、`IdxIndexes`、`ClassProperties` 和 `schemaHash`。
表名、说明、数据库 ID 从 `GetTableList` 获得。

`PUT /api/connections/{id}/tables/{tableId}` 使用同样的设计格式：

- `table` 必须包含 `DatabaseID`、`Name`；路径中的 tableId 会作为 `table.id`。
- `columns` 为**完整的最终字段列表**。修改已有字段时保留字段 `id`，新增字段省略 `id`；原字段未包含在列表中时会被删除。
- `orderid` 根据数组顺序自动计算。CLR 属性、枚举、jsonb 等字段名称可从能力接口的 `entitySchemas.DBColumn` 发现。
- `indexes` 使用 `ColumnNames` 数组；读取返回的 `IdxIndexes` 使用 `Keys` 逗号分隔字符串。
- 省略 `indexes` / `classProperties` 时保留现有值；传 `[]` 表示清空。
- 可携带 `expectedSchemaHash`，值为上次读取返回的 `schemaHash`。期间结构被修改时返回 HTTP 409，重新读取并合并后再提交。UI 编辑页也使用此检查。此检查覆盖经过本客户端的修改；EJServer 没有跨客户端事务式版本检查。
- 不提供级联删除功能。编辑旧表时自动保留服务器已有的该配置。
- 成功的修改返回 JSON `null`（EJServer 的 ModifyTable 无返回值）。随后重新读取结构验证结果。

保留现有字段并增加一列：

```python
table = next(t for t in client.rpc(cid, "GetTableList", 1) if t["id"] == 10)
schema = client.schema(cid, 10)
columns = schema["Columns"]
columns.append({"Name": "Email", "dbType": "varchar", "length": "200",
                "CanNull": True, "IsPKID": False, "IsAutoIncrement": False})
client.update_table(cid, 10, {"table": table, "columns": columns,
                            "expectedSchemaHash": schema["schemaHash"]})
```

## 全功能操作发现

`GET /api/capabilities` 返回操作名称、每个参数的名称/类型/顺序、返回类型和实体字段。
`GET /openapi.json` 返回 OpenAPI 3.1，包含专用路由和所有支持的 RPC。
`POST /api/connections/{id}/rpc/{method}` 请求体为 `{"parameters": [...]}`，返回 EJServer 的 result 值。
参数数组严格按能力描述的顺序传递。EJServer 的用户权限检查仍然生效。
建表和修改表必须走 tables 路由，其余客户端业务都可通过 RPC 或专用文件路由完成。

| 操作 | RPC / 路由 |
| --- | --- |
| 活跃连接、连接/断开 | GET/POST `/api/connections`，DELETE `/api/connections/{id}` |
| 已保存连接、忘记记录 | GET/DELETE `/api/profiles`；DELETE 使用 `url`、`userName` 查询参数 |
| 项目管理 | GetCurrentUserProjectList / CreateProject / UpdateProject / DeleteProject |
| 数据库管理 | GetDatabaseList / GetDatabase / UpdateDatabase / DeleteDatabase |
| 表结构 | GetTableList / GetColumnList / GetTableInfo / GetTableIDXIndexList / GetClassPropertyList；tables 路由负责写入 |
| 查看、编辑数据 | GetDataTable / SaveDataTable / DeleteTable |
| 数据库模块与布局 | GetDBModuleList / UpdateDBModule / DeleteModule / ChangeModuleParent / GetTablesInModule / UpdateTableInMoudle / RemoveTableFromModule |
| 代码生成 | GET `/api/connections/{id}/code?databaseId=1&kind=model`；kind 为 model/simple/minimal，可选 nameSpace |
| DTO | GetDtoCode |
| 导出数据 | GET `/api/connections/{id}/data/export?tableIds=10,11`，返回 EJClient 二进制备份 |
| 导入数据 | POST `/api/connections/{id}/data/import?databaseId=1&clearDataFirst=false`，请求体为二进制备份，Content-Type 为 application/octet-stream |
| C# 还原模型 | POST `/api/connections/{id}/design/import?projectId=1`，请求体为含 design 的模型 .cs 文件 |
| 其他数据库结构导入 | GetDatabaseCurrentTableNames / GetDatabaseCurrentColumns / GetDatabaseCurrentIndexes / CreateTables |
| 变更历史 | GetActions / RebuildDatabaseActions |
| 用户、密码、权限 | GetUsers / UpdateUser / DeleteUsers / ChangePassword / GetCurrentUserProjectToSetPowerList / GetCurrentUserDBTableToSetPowerList / SetProjectPower / SetTablePower 等 |
| 问题记录 | GetMyBugs / GetBugHistories / GetBugPicture / SubmitBug / SubmitHistory / BugFinish |
| 结构化状态 | GET `/api/state` 返回 connection ID、revision、各连接的 activities（isBusy / activeRequests / description）和最近 100 个变更事件 |

`UpdateTableInMoudle` 的拼写来自旧服务器协议。所有方法的权威参数定义以 `/api/capabilities` 为准。
`byte[]` 参数（问题内容、附件等）在 JSON 中使用 Base64。
`CreateTables` 参数为 `[databaseid, [{"TableName":"...","Columns":[...],"Indexes":[...]}]]`。

### 编辑数据示例

```python
rows = client.rpc(cid, "GetDataTable", "select * from Accounts", 10, 0, 100)
# 返回 WayDataTable：Columns=[{ColumnName, DataType}], Rows=[{RowState, Items:[{Name, Value}]}]。
# TableName 在查询结果里承载总行数。
row = rows["Rows"][0]
next(item for item in row["Items"] if item["Name"] == "Name")["Value"] = "新名称"
row["RowState"] = 16  # 4=新增，16=修改，2=未修改。
client.rpc(cid, "SaveDataTable", {"TableName":"Accounts", "Columns":rows["Columns"], "Rows":[row]}, 10, [])
# 最后一个参数是需要删除的主键字符串数组。更改和删除都需要表的主键。
```

写入不经过 UI 弹窗。Agent 按调用者授权直接执行；操作会修改所选服务器的数据库。
HTTP 400 为请求验证失败，401 为 Token 无效，404 为连接不存在，409 为结构冲突/操作取消，502 为 EJServer 或传输失败。
错误体为 `{"error":"具体原因","type":"异常类型"}`。不要盲目重试超时写入；先读取状态确认是否已执行。

连接信息不会从 API 返回密码。Windows 保存密码使用当前用户 DPAPI；修改密码后同步更新保存的凭据。
客户端进程必须保持运行；这是本机客户端控制接口，不是 EJServer 上新部署的远程服务。
