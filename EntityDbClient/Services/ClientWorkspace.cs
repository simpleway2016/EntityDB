using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Security.Cryptography;
using System.Text;

namespace EntityDbClient.Services;

public sealed record ConnectionInfo(string Id, string Url, string UserName, int Role, int UserId);
public sealed record ConnectionActivityInfo(string ConnectionId, ConnectionActivity Activity);
public sealed record WorkspaceChange(string Kind, string ConnectionId, string? Method = null,
    object?[]? Parameters = null, JsonNode? Result = null);
public sealed class SchemaConflictException(string message) : Exception(message);

// UI and the local Agent API use this same owner of all server sessions.
public sealed class ClientWorkspace : IDisposable
{
    private readonly ConcurrentDictionary<string, ServerConnection> connections = new();
    private readonly SemaphoreSlim profileGate = new(1, 1);
    private readonly ConcurrentDictionary<string, SemaphoreSlim> tableGates = new();
    public ConnectionStore Store { get; }
    public event Action<WorkspaceChange>? Changed;
    public ClientWorkspace(ConnectionStore? store = null) => Store = store ?? new ConnectionStore();
    public ConnectionInfo[] Connections => connections.Select(x => Describe(x.Key, x.Value)).ToArray();
    public ConnectionActivityInfo[] Activities => connections.Select(x => new ConnectionActivityInfo(x.Key, x.Value.Activity)).ToArray();
    private static ConnectionInfo Describe(string id, ServerConnection connection) => new(id, connection.Url, connection.UserName, connection.Role, connection.UserId);
    public ServerConnection Get(string id) => connections.TryGetValue(id, out var connection) ? connection : throw new KeyNotFoundException("连接不存在或已关闭。");

    public async Task<ConnectionInfo> ConnectAsync(string url, string userName, string password)
    {
        var connection = new ServerConnection(url, userName, password);
        try { await connection.LoginAsync(); }
        catch { connection.Dispose(); throw; }
        var id = Guid.NewGuid().ToString("N");
        connections[id] = connection;
        Changed?.Invoke(new("connected", id));
        // Persist only authenticated credentials; failure to save must not silently disconnect a live tab.
        try { await RememberAsync(connection.Url, connection.UserName, password); }
        catch (Exception ex) { throw new IOException($"连接已建立（{id}），但保存连接信息失败：{ex.Message}", ex); }
        return Describe(id, connection);
    }

    public void Disconnect(string id)
    {
        if (connections.TryRemove(id, out var connection))
        {
            connection.Dispose();
            Changed?.Invoke(new("disconnected", id));
        }
    }

    public async Task RememberAsync(string url, string userName, string password)
    {
        await profileGate.WaitAsync();
        try { Store.Remember(url, userName, password); }
        finally { profileGate.Release(); }
    }

    public async Task ForgetAsync(SavedConnection profile)
    {
        await profileGate.WaitAsync();
        try { Store.Forget(profile); }
        finally { profileGate.Release(); }
    }

    public async Task<JsonNode?> InvokeAsync(string id, string method, params object?[] parameters)
    {
        if (!RpcCatalog.Methods.ContainsKey(method) && method is not ("CreateTable" or "ModifyTable")) throw new ArgumentException("不支持的操作：" + method);
        var connection = Get(id);
        var result = await connection.InvokeAsync<JsonNode>(method, parameters);
        if (method == "ChangePassword" && parameters.Length == 2)
        {
            var password = parameters[1]?.ToString() ?? "";
            connection.SetPassword(password);
            await RememberAsync(connection.Url, connection.UserName, password);
        }
        if (!method.StartsWith("Get", StringComparison.Ordinal))
            Changed?.Invoke(new(method is "UpdateTableInMoudle" or "RemoveTableFromModule" ? "layout-updated" : "updated",
                id, method, parameters, result));
        return result;
    }

    public async Task<JsonObject> ReadTableAsync(string id, int tableId)
    {
        var result = await Get(id).InvokeAsync<JsonObject>("GetTableInfo", tableId);
        result.Remove("DBDeleteConfigs");
        result["schemaHash"] = SchemaHash(result);
        return result;
    }

    public static string SchemaHash(JsonObject info)
    {
        var schema = new JsonObject
        {
            ["Columns"] = info["Columns"]?.DeepClone(), ["IdxIndexes"] = info["IdxIndexes"]?.DeepClone(),
            ["ClassProperties"] = info["ClassProperties"]?.DeepClone()
        };
        // Canonicalize property ordering; ignore serializer-specific legacy change tracking metadata.
        JsonNode? Canonical(JsonNode? node) => node switch
        {
            JsonObject obj => new JsonObject(obj.Where(p => p.Key is not ("ChangedProperties" or "BackupChangedProperties"))
                .OrderBy(p => p.Key, StringComparer.Ordinal).Select(p => new KeyValuePair<string, JsonNode?>(p.Key, Canonical(p.Value)))),
            JsonArray array => new JsonArray(array.Select(Canonical).ToArray()),
            _ => node?.DeepClone()
        };
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(Canonical(schema)!.ToJsonString())));
    }

    public async Task<JsonNode?> SaveTableAsync(string id, JsonObject design, bool create)
    {
        var gate = tableGates.GetOrAdd(id, _ => new SemaphoreSlim(1, 1));
        await gate.WaitAsync(Get(id).Cancellation);
        try { return await SaveTableCoreAsync(id, design, create); }
        finally { gate.Release(); }
    }

    private async Task<JsonNode?> SaveTableCoreAsync(string id, JsonObject design, bool create)
    {
        var table = design["table"] as JsonObject ?? throw new ArgumentException("缺少 table 对象。");
        var columns = design["columns"] as JsonArray ?? throw new ArgumentException("缺少 columns 数组。");
        if (string.IsNullOrWhiteSpace(table["Name"]?.ToString())) throw new ArgumentException("表名不能为空。");
        if (table["DatabaseID"] == null) throw new ArgumentException("缺少 table.DatabaseID。");
        if (columns.Count == 0) throw new ArgumentException("至少需要一个字段。");
        if (create && !columns.Any(x => x?["IsPKID"]?.GetValue<bool>() == true)) throw new ArgumentException("新建数据表必须设置主键。");
        ColumnRules.Validate(columns.Deserialize<Models.DBColumn[]>(ServerConnection.Json)!);
        var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        for (int i = 0; i < columns.Count; i++)
        {
            var column = columns[i] as JsonObject ?? throw new ArgumentException("字段必须是对象。");
            if (string.IsNullOrWhiteSpace(column["Name"]?.ToString()) || !names.Add(column["Name"]!.ToString()))
                throw new ArgumentException("字段名称不能为空或重复。");
            if (!string.IsNullOrWhiteSpace(column["EnumDefine"]?.ToString()) && column["dbType"]?.ToString() != "int")
                throw new ArgumentException("枚举字段必须使用 int 类型。");
            if (column["dbType"]?.ToString() == "jsonb" && string.IsNullOrWhiteSpace(column["ClassFullName"]?.ToString()))
                throw new ArgumentException("jsonb 字段必须设置 ClassFullName。");
            column["orderid"] = i;
        }
        foreach (var index in design["indexes"]?.AsArray() ?? [])
            foreach (var name in index?["ColumnNames"]?.AsArray() ?? [])
                if (!names.Contains(name?.ToString() ?? "")) throw new ArgumentException("索引包含不存在的字段：" + name);

        var connection = Get(id);
        JsonArray legacyConfigurations = [];
        JsonArray indexes;
        JsonArray properties;
        if (!create)
        {
            var tableId = table["id"]?.GetValue<int>() ?? throw new ArgumentException("修改表需要 table.id。");
            var previous = await connection.InvokeAsync<JsonObject>("GetTableInfo", tableId);
            if (design["expectedSchemaHash"] is JsonValue expected && expected.ToString() != SchemaHash(previous))
                throw new SchemaConflictException("表结构已被其他操作修改，请重新读取结构后再提交。");
            // The legacy server replaces these arrays. Preserve hidden configuration instead of clearing it.
            legacyConfigurations = (JsonArray?)previous["DBDeleteConfigs"]?.DeepClone() ?? [];
            indexes = (JsonArray?)design["indexes"]?.DeepClone() ?? ConvertIndexes(previous["IdxIndexes"] as JsonArray ?? []);
            properties = (JsonArray?)design["classProperties"]?.DeepClone() ?? (JsonArray?)previous["ClassProperties"]?.DeepClone() ?? [];
        }
        else
        {
            indexes = (JsonArray?)design["indexes"]?.DeepClone() ?? [];
            properties = (JsonArray?)design["classProperties"]?.DeepClone() ?? [];
        }
        foreach (var index in indexes)
            foreach (var name in index?["ColumnNames"]?.AsArray() ?? [])
                if (!names.Contains(name?.ToString() ?? "")) throw new ArgumentException("索引包含不存在的字段：" + name);
        return await InvokeAsync(id, create ? "CreateTable" : "ModifyTable", table, columns, legacyConfigurations, indexes, properties);
    }

    public static JsonArray ConvertIndexes(JsonArray source) => new(source.Select(x => (JsonNode)new JsonObject
    {
        ["IsUnique"] = x?["IsUnique"]?.DeepClone(), ["IsClustered"] = x?["IsClustered"]?.DeepClone(),
        ["ColumnNames"] = new JsonArray((x?["Keys"]?.ToString() ?? "").Split(',', StringSplitOptions.RemoveEmptyEntries).Select(n => (JsonNode?)JsonValue.Create(n)).ToArray())
    }).ToArray());

    public void Dispose() { foreach (var id in connections.Keys) Disconnect(id); }
}

public static class RpcCatalog
{
    public static readonly Dictionary<string, JsonObject> Methods = Load();
    private static Dictionary<string, JsonObject> Load()
    {
        using var stream = typeof(RpcCatalog).Assembly.GetManifestResourceStream("EntityDbClient.rpc-catalog.json")!;
        return JsonSerializer.Deserialize<Dictionary<string, JsonObject>>(stream)!;
    }
}
