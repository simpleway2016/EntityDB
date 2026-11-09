using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace EntityDbClient.Services;

// Each tab owns one instance: HTTP cookies, session ID, credentials and lifetime never cross tabs.
public sealed class ServerConnection : IDisposable
{
    public static readonly JsonSerializerOptions Json = new() { PropertyNameCaseInsensitive = true };
    private readonly HttpClient http;
    private readonly SemaphoreSlim gate = new(1, 1);
    private readonly CancellationTokenSource lifetime = new();
    private readonly object activityLock = new();
    private readonly SortedDictionary<long, (string Description, double? Progress)> activities = [];
    private long activitySequence, activityVersion;
    public event Action<ConnectionActivity>? ActivityChanged;
    public ConnectionActivity Activity { get { lock (activityLock) return SnapshotActivity(); } }
    public string Url { get; }
    public string UserName { get; }
    private string password;
    public string SessionId { get; private set; } = "";
    public int Role { get; private set; }
    public int UserId { get; private set; }
    public CancellationToken Cancellation => lifetime.Token;

    public ServerConnection(string url, string userName, string password, HttpMessageHandler? handler = null)
    {
        Url = NormalizeUrl(url);
        UserName = userName.Trim();
        this.password = password;
        handler ??= new HttpClientHandler
        {
            AllowAutoRedirect = false,
            UseProxy = !new Uri(Url).IsLoopback,
            ServerCertificateCustomValidationCallback = (_, certificate, _, errors) =>
                errors == SslPolicyErrors.None || (certificate != null && certificate.RawData.AsSpan().SequenceEqual(PinnedCertificate))
        };
        http = new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(90) };
        http.DefaultRequestHeaders.Referrer = new Uri(Url + "/main.html");
    }

    private static readonly byte[] PinnedCertificate = ReadCertificate();
    private static byte[] ReadCertificate()
    {
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("EntityDbClient.certificate")!;
        using var buffer = new MemoryStream();
        stream.CopyTo(buffer);
        return buffer.ToArray();
    }

    public static string NormalizeUrl(string value)
    {
        if (!Uri.TryCreate(value.Trim(), UriKind.Absolute, out var uri) ||
            (uri.Scheme != "http" && uri.Scheme != "https") || !string.IsNullOrEmpty(uri.UserInfo) ||
            !string.IsNullOrEmpty(uri.Query) || !string.IsNullOrEmpty(uri.Fragment))
            throw new ArgumentException("服务器地址应为 http:// 或 https:// 开头的地址，例如 https://localhost:6060。");
        return uri.AbsoluteUri.TrimEnd('/');
    }

    public async Task LoginAsync()
    {
        var result = await InvokeAsync<int[]>("Login", UserName, password);
        if (result.Length < 2) throw new InvalidDataException("服务器返回的登录信息不完整。");
        Role = result[0];
        UserId = result[1];
    }

    public async Task<T> InvokeAsync<T>(string method, params object?[] parameters)
    {
        using var activity = BeginActivity(DescribeOperation(method));
        await gate.WaitAsync(Cancellation);
        try
        {
            var response = await InvokeCoreAsync(method, parameters);
            if (method != "Login" && response["type"]?.GetValue<int>() == 4 && response["result"]?.ToString() == "请先登录")
            {
                // Retry a rejected call once, and only after an explicit expired-session response.
                var login = await InvokeCoreAsync("Login", [UserName, password]);
                EnsureSuccess(login);
                response = await InvokeCoreAsync(method, parameters);
            }
            EnsureSuccess(response);
            var value = response["result"];
            return value is null ? default! : value.Deserialize<T>(Json)!;
        }
        finally { gate.Release(); }
    }

    private static void EnsureSuccess(JsonObject response)
    {
        if (response["type"]?.GetValue<int>() != 1)
            throw new InvalidOperationException(response["result"]?.ToString() ?? "EJServer 返回了未知响应。");
    }

    private async Task<JsonObject> InvokeCoreAsync(string method, object?[] parameters)
    {
        var wireParameters = PrepareParameters(method, parameters);
        using var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["m"] = JsonSerializer.Serialize(new
            {
                ClassFullName = "Way.EJServer.MainController", MethodName = method,
                ParameterJson = wireParameters.ToJsonString(Json), SessionID = SessionId
            }, Json)
        });
        using var response = await http.PostAsync(Url + "/wayscriptremoting_invoke?a=1", content, Cancellation);
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync(Cancellation);
        var result = JsonNode.Parse(body) as JsonObject ?? throw new InvalidDataException("服务器响应不是有效的 EJServer 消息。");
        if (result["sessionid"] is JsonValue session && !string.IsNullOrEmpty(session.ToString())) SessionId = session.ToString();
        return result;
    }

    private static JsonArray PrepareParameters(string method, object?[] parameters)
    {
        var array = JsonSerializer.SerializeToNode(parameters, Json)!.AsArray();
        string[] types = method switch
        {
            "CreateTable" or "ModifyTable" => ["EJ.DBTable", "EJ.DBColumn[]", "EJ.DBDeleteConfig[]", "IndexInfo[]", "EJ.classproperty[]"],
            _ => RpcCatalog.Methods.TryGetValue(method, out var descriptor)
                ? descriptor["parameters"]!.AsArray().Select(x => x!["type"]!.ToString()).ToArray() : []
        };
        for (int i = 0; i < Math.Min(types.Length, array.Count); i++)
        {
            if (!types[i].StartsWith("EJ.", StringComparison.Ordinal)) continue;
            if (array[i] is JsonObject entity) AddChangeTracking(entity);
            else if (array[i] is JsonArray entities) foreach (var item in entities.OfType<JsonObject>()) AddChangeTracking(item);
        }
        return array;
    }
    private static void AddChangeTracking(JsonObject entity)
    {
        // EJServer's Newtonsoft DataItem converter suppresses setter tracking during deserialization.
        // Supply the legacy wire metadata so db.Update actually writes every submitted field.
        var changes = new JsonObject();
        foreach (var property in entity.ToArray())
            if (property.Key is not ("id" or "ChangedProperties" or "BackupChangedProperties"))
                changes[property.Key] = new JsonObject { ["OriginalValue"] = null };
        entity.Remove("BackupChangedProperties");
        entity["ChangedProperties"] = changes;
    }

    public async Task<byte[]> TransferAsync(string path, byte[]? data = null,
        Func<Stream, CancellationToken, Action<string, double?>, Task<byte[]>>? readResponse = null)
    {
        using var activity = BeginActivity(data != null ? "正在上传并导入数据…" : path.Contains("TableData", StringComparison.Ordinal) ? "正在导出表数据…" : "正在生成并下载文件…");
        using var request = new HttpRequestMessage(data is null ? HttpMethod.Get : HttpMethod.Post, Url + "/" + path.TrimStart('/'));
        request.Headers.Add("Cookie", "WayScriptRemoting=" + SessionId);
        request.Headers.ConnectionClose = true;
        if (data != null) request.Content = new ByteArrayContent(data);
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(Cancellation);
        timeout.CancelAfter(TimeSpan.FromSeconds(90));
        using var response = await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, timeout.Token);
        response.EnsureSuccessStatusCode();
        if (readResponse == null) return await response.Content.ReadAsByteArrayAsync(timeout.Token);
        await using var stream = await response.Content.ReadAsStreamAsync(timeout.Token);
        return await readResponse(stream, timeout.Token, activity.Report);
    }

    public void SetPassword(string value) => password = value;
    private ConnectionActivity SnapshotActivity() => new(activityVersion, activities.Count,
        activities.Count == 0 ? "" : activities.Last().Value.Description,
        activities.Count == 0 ? null : activities.Last().Value.Progress);
    private ActivityScope BeginActivity(string description)
    {
        long key; ConnectionActivity snapshot;
        lock (activityLock) { key = ++activitySequence; activities.Add(key, (description, null)); activityVersion++; snapshot = SnapshotActivity(); }
        ActivityChanged?.Invoke(snapshot);
        return new ActivityScope(() =>
        {
            lock (activityLock) { activities.Remove(key); activityVersion++; snapshot = SnapshotActivity(); }
            ActivityChanged?.Invoke(snapshot);
        }, (message, progress) =>
        {
            lock (activityLock)
            {
                if (!activities.ContainsKey(key)) return;
                activities[key] = (message, progress); activityVersion++; snapshot = SnapshotActivity();
            }
            ActivityChanged?.Invoke(snapshot);
        });
    }
    private static string DescribeOperation(string method) => method switch
    {
        "Login" => "正在连接服务器…", "GetCurrentUserProjectList" => "正在加载项目…",
        "GetDatabaseList" => "正在加载数据库…", "GetTableList" => "正在加载数据表…",
        "GetTableInfo" or "GetColumns" or "GetColumnList" => "正在加载表结构…",
        "GetDBModuleList" or "GetTablesInModule" => "正在加载设计模块…",
        "GetDataTable" => "正在执行查询…", "SaveDataTable" => "正在保存数据…",
        "CreateTable" => "正在创建数据表…", "ModifyTable" => "正在保存表结构…",
        "GetUsers" => "正在加载用户…",
        "GetDtoCode" => "正在生成代码…", "RemoveTableFromModule" => "正在从模块移除数据表…",
        "UpdateTableInMoudle" => "正在保存模块布局…", "CreateTables" => "正在导入表结构…",
        _ when method.StartsWith("Get", StringComparison.Ordinal) => "正在读取服务器数据…",
        _ when method.StartsWith("Delete", StringComparison.Ordinal) => "正在删除…",
        _ => "正在保存更改…"
    };
    public void Dispose()
    {
        lifetime.Cancel();
        http.Dispose();
        password = "";
        // Do not dispose gate while an in-flight call may still release it.
    }
}
