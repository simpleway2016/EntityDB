using System.Collections.Concurrent;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using EntityDbClient.Services;

namespace EntityDbClient.Agent;

public sealed class AgentApi : IAsyncDisposable
{
    private readonly WebApplication app;
    private readonly ClientWorkspace workspace;
    private readonly string instanceId = Guid.NewGuid().ToString("N");
    private readonly ConcurrentQueue<object> events = new();
    private long revision;
    public string BaseUrl { get; private set; } = "";
    public string DiscoveryPath { get; private set; } = "";
    private string InstancePath = "";
    private AgentApi(WebApplication app, ClientWorkspace workspace) { this.app = app; this.workspace = workspace; }

    public static async Task<AgentApi> StartAsync(ClientWorkspace workspace)
    {
        var builder = WebApplication.CreateSlimBuilder(new WebApplicationOptions { Args = [], ApplicationName = typeof(AgentApi).Assembly.FullName });
        builder.Logging.ClearProviders();
        builder.WebHost.ConfigureKestrel(options => options.Listen(IPAddress.Loopback, 0));
        var app = builder.Build();
        var api = new AgentApi(app, workspace);
        var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
        var expected = Encoding.ASCII.GetBytes("Bearer " + token);
        app.Use(async (context, next) =>
        {
            var actual = Encoding.ASCII.GetBytes(context.Request.Headers.Authorization.ToString());
            // No browser CORS access, even if a website manages to learn the discovery port.
            if (context.Request.Headers.ContainsKey("Origin") || !CryptographicOperations.FixedTimeEquals(actual, expected))
            { context.Response.StatusCode = 401; await context.Response.WriteAsJsonAsync(new { error = "需要本机 Agent Bearer Token。" }); return; }
            try { await next(context); }
            catch (Exception ex)
            {
                context.Response.StatusCode = ex switch { KeyNotFoundException => 404, ArgumentException or JsonException => 400, OperationCanceledException or SchemaConflictException => 409, _ => 502 };
                await context.Response.WriteAsJsonAsync(new { error = ex.Message, type = ex.GetType().Name });
            }
        });
        app.MapGet("/api/state", () => new { version = 1, api.instanceId, revision = Interlocked.Read(ref api.revision), connections = workspace.Connections, activities = workspace.Activities, events = api.events.ToArray() });
        app.MapGet("/api/capabilities", () => new
        {
            protocol = "EntityDbClient.Agent/1", operations = RpcCatalog.Methods,
            tableOperations = new[] { "GET /api/connections/{id}/tables/{tableId}/schema", "POST /api/connections/{id}/tables", "PUT /api/connections/{id}/tables/{tableId}" },
            entitySchemas = EntitySchemas(), openapi = "/openapi.json"
        });
        app.MapGet("/api/profiles", () => workspace.Store.Load().Select(x => new { x.Url, x.UserName, x.LastConnected }));
        app.MapDelete("/api/profiles", async (string url, string userName) => { await workspace.ForgetAsync(new() { Url = url, UserName = userName }); return Results.NoContent(); });
        app.MapGet("/api/connections", () => workspace.Connections);
        app.MapPost("/api/connections", async (ConnectRequest request) =>
        {
            string password;
            if (request.Password != null) password = request.Password;
            else
            {
                var normalized = ServerConnection.NormalizeUrl(request.Url);
                var profile = workspace.Store.Load().FirstOrDefault(x => x.Url == normalized && x.UserName == request.UserName)
                    ?? throw new ArgumentException("没有此服务器和用户名的保存记录，请提供 password。");
                password = workspace.Store.GetPassword(profile);
            }
            return Results.Json(await workspace.ConnectAsync(request.Url, request.UserName, password));
        });
        app.MapDelete("/api/connections/{id}", (string id) => { workspace.Get(id); workspace.Disconnect(id); return Results.NoContent(); });
        app.MapPost("/api/connections/{id}/rpc/{method}", async (string id, string method, RpcRequest request) =>
        {
            if (!RpcCatalog.Methods.TryGetValue(method, out var operation)) throw new ArgumentException("不支持的操作，请读取 /api/capabilities。建表和修改表请使用 tables API。");
            var parameters = request.Parameters ?? [];
            if (parameters.Length != operation["parameters"]!.AsArray().Count) throw new ArgumentException("参数个数与接口定义不一致，请读取 /api/capabilities。");
            var result = await workspace.InvokeAsync(id, method, parameters.Cast<object?>().ToArray());
            if (method == "GetTableInfo" && result is JsonObject table) table.Remove("DBDeleteConfigs");
            return Results.Json(result);
        });
        app.MapGet("/api/connections/{id}/tables/{tableId:int}/schema", async (string id, int tableId) => Results.Json(await workspace.ReadTableAsync(id, tableId)));
        app.MapPost("/api/connections/{id}/tables", async (string id, JsonObject design) => Results.Json(await workspace.SaveTableAsync(id, design, true)));
        app.MapPut("/api/connections/{id}/tables/{tableId:int}", async (string id, int tableId, JsonObject design) =>
        {
            if (design["table"] is not JsonObject table) throw new ArgumentException("缺少 table。");
            table["id"] = tableId;
            return Results.Json(await workspace.SaveTableAsync(id, design, false));
        });
        app.MapGet("/api/connections/{id}/code", async (string id, int databaseId, string? kind, string? nameSpace) =>
            Results.File(await FileTransfers.GenerateCodeAsync(workspace.Get(id), databaseId, kind ?? "model", "agent.cs", nameSpace), "text/plain; charset=utf-8", "model.cs"));
        app.MapGet("/api/connections/{id}/data/export", async (string id, string tableIds) =>
        {
            var ids = tableIds.Split(',').Select(int.Parse).ToArray();
            return Results.File(await FileTransfers.ExportDataAsync(workspace.Get(id), ids), "application/octet-stream", "data.ejdata");
        });
        app.MapPost("/api/connections/{id}/data/import", async (string id, int databaseId, bool? clearDataFirst, HttpRequest request) =>
        {
            using var buffer = new MemoryStream(); await request.Body.CopyToAsync(buffer);
            await FileTransfers.ImportDataAsync(workspace.Get(id), databaseId, buffer.ToArray(), clearDataFirst ?? false);
            api.OnChanged(new("updated", id)); return Results.Ok(new { success = true });
        });
        app.MapPost("/api/connections/{id}/design/import", async (string id, int projectId, HttpRequest request) =>
        {
            using var buffer = new MemoryStream(); await request.Body.CopyToAsync(buffer);
            await FileTransfers.ImportDesignAsync(workspace.Get(id), projectId, buffer.ToArray());
            api.OnChanged(new("updated", id)); return Results.Ok(new { success = true });
        });
        app.MapGet("/openapi.json", () => Results.Json(api.OpenApi()));
        workspace.Changed += api.OnChanged;
        try
        {
            await app.StartAsync();
            api.BaseUrl = app.Services.GetRequiredService<IServer>().Features.Get<IServerAddressesFeature>()!.Addresses.Single().TrimEnd('/');
            Directory.CreateDirectory(workspace.Store.DirectoryPath);
            api.DiscoveryPath = Path.Combine(workspace.Store.DirectoryPath, "agent.json");
            api.InstancePath = Path.Combine(workspace.Store.DirectoryPath, $"agent-{Environment.ProcessId}.json");
            var discovery = JsonSerializer.Serialize(new { protocol = "EntityDbClient.Agent/1", api.instanceId, processId = Environment.ProcessId,
                baseUrl = api.BaseUrl, token, openapi = api.BaseUrl + "/openapi.json", capabilities = api.BaseUrl + "/api/capabilities" }, new JsonSerializerOptions { WriteIndented = true });
            foreach (var path in new[] { api.InstancePath, api.DiscoveryPath })
            {
                File.WriteAllText(path + ".tmp", discovery); File.Move(path + ".tmp", path, true);
                if (!OperatingSystem.IsWindows()) File.SetUnixFileMode(path, UnixFileMode.UserRead | UnixFileMode.UserWrite);
            }
            return api;
        }
        catch { workspace.Changed -= api.OnChanged; await app.DisposeAsync(); throw; }
    }

    private void OnChanged(WorkspaceChange change)
    {
        var sequence = Interlocked.Increment(ref revision);
        events.Enqueue(new { revision = sequence, kind = change.Kind, connectionId = change.ConnectionId, at = DateTimeOffset.UtcNow });
        while (events.Count > 100) events.TryDequeue(out _);
    }

    private static object EntitySchemas() => typeof(Models.Project).Assembly.GetTypes()
        .Where(t => t.Namespace == "EntityDbClient.Models" && t.IsClass).ToDictionary(t => t.Name,
            t => t.GetProperties().ToDictionary(p => p.Name, p => new { type = (Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType).Name,
                nullable = Nullable.GetUnderlyingType(p.PropertyType) != null,
                values = (Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType).IsEnum ? Enum.GetNames(Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType) : null,
                enumValues = (Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType).IsEnum ? Enum.GetValues(Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType).Cast<object>().ToDictionary(v => v.ToString()!, v => Convert.ToInt32(v)) : null }));

    private object OpenApi()
    {
        var paths = new Dictionary<string, object>();
        void Add(string path, string verb, string summary, object? body = null, object[]? parameters = null)
        {
            if (!paths.TryGetValue(path, out var entry)) paths[path] = entry = new Dictionary<string, object>();
            var operation = new Dictionary<string, object> { ["summary"] = summary, ["responses"] = new Dictionary<string, object> { ["200"] = new { description = "成功；RPC 返回 EJServer 的 result 值。" }, ["400"] = new { description = "参数错误" }, ["401"] = new { description = "Token 无效" }, ["502"] = new { description = "EJServer 操作失败，JSON error 字段包含原因" } } };
            if (body != null) operation["requestBody"] = new { required = true, content = new Dictionary<string, object> { ["application/json"] = new { schema = body } } };
            if (parameters != null) operation["parameters"] = parameters;
            ((Dictionary<string, object>)entry)[verb] = operation;
        }
        object Param(string name, string location = "path", string type = "string", bool required = true) => new { name, @in = location, required, schema = new { type } };
        object arbitrary = new { type = "object", additionalProperties = true };
        object design = new { type = "object", required = new[] { "table", "columns" }, properties = new { table = arbitrary, columns = new { type = "array", items = arbitrary }, indexes = new { type = "array", items = arbitrary }, classProperties = new { type = "array", items = arbitrary }, expectedSchemaHash = new { type = "string", description = "修改表时可填 schema 读取返回的 schemaHash，发生并发修改时返回 409" } } };
        Add("/api/state", "get", "读取当前连接、修订号和最近 100 个变更事件");
        Add("/api/capabilities", "get", "发现所有 RPC 名称、位置参数、返回类型和实体字段");
        Add("/api/profiles", "get", "读取保存的服务器和用户名，不返回密码");
        Add("/api/profiles", "delete", "忘记保存的连接", parameters: [Param("url", "query"), Param("userName", "query")]);
        Add("/api/connections", "get", "列出活跃服务器 Tab");
        Add("/api/connections", "post", "连接服务器并打开 Tab；省略 password 时使用保存的密码", new { type = "object", required = new[] { "url", "userName" }, properties = new { url = new { type = "string" }, userName = new { type = "string" }, password = new { type = "string" } } });
        Add("/api/connections/{id}", "delete", "关闭连接及 Tab，丢弃该 Tab 未保存的界面编辑", parameters: [Param("id")]);
        Add("/api/connections/{id}/tables/{tableId}/schema", "get", "读取字段、索引和导航属性", parameters: [Param("id"), Param("tableId", type: "integer")]);
        Add("/api/connections/{id}/tables", "post", "创建表（实际修改数据库）", design, [Param("id")]);
        Add("/api/connections/{id}/tables/{tableId}", "put", "修改表；columns 是完整字段列表，遗漏原字段会删除该字段；省略 indexes/classProperties 时保留现有配置", design, [Param("id"), Param("tableId", type: "integer")]);
        Add("/api/connections/{id}/code", "get", "下载代码；kind=model/simple/minimal", parameters: [Param("id"), Param("databaseId", "query", "integer"), Param("kind", "query", required: false), Param("nameSpace", "query", required: false)]);
        Add("/api/connections/{id}/data/export", "get", "导出 EJClient 二进制数据备份", parameters: [Param("id"), Param("tableIds", "query")]);
        Add("/api/connections/{id}/data/import", "post", "请求体为 EJClient 二进制备份；clearDataFirst 默认为 false", parameters: [Param("id"), Param("databaseId", "query", "integer"), Param("clearDataFirst", "query", "boolean", false)]);
        Add("/api/connections/{id}/design/import", "post", "请求体为包含 design 的模型 C# 文件", parameters: [Param("id"), Param("projectId", "query", "integer")]);
        foreach (var (method, descriptor) in RpcCatalog.Methods)
            Add("/api/connections/{id}/rpc/" + method, "post", method + " " + descriptor.ToJsonString(), new { type = "object", required = new[] { "parameters" }, properties = new { parameters = new { type = "array", items = new { }, minItems = descriptor["parameters"]!.AsArray().Count, maxItems = descriptor["parameters"]!.AsArray().Count } } }, [Param("id")]);
        return new { openapi = "3.1.0", info = new { title = "EntityDbClient Agent API", version = "1.0.0" }, servers = new[] { new { url = BaseUrl } },
            security = new[] { new Dictionary<string, string[]> { ["bearerAuth"] = [] } }, components = new { securitySchemes = new { bearerAuth = new { type = "http", scheme = "bearer" } } }, paths };
    }

    public async ValueTask DisposeAsync()
    {
        workspace.Changed -= OnChanged;
        await app.StopAsync(); await app.DisposeAsync();
        foreach (var path in new[] { InstancePath, DiscoveryPath })
        {
            try { if (File.Exists(path) && JsonNode.Parse(File.ReadAllText(path))?["instanceId"]?.ToString() == instanceId) File.Delete(path); }
            catch (IOException) { }
        }
    }
    public sealed record ConnectRequest(string Url, string UserName, string? Password);
    public sealed record RpcRequest(JsonElement[]? Parameters);
}
