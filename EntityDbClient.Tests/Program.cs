using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Headless;
using Avalonia.Threading;
using Avalonia.VisualTree;
using Avalonia.Interactivity;
using System.Diagnostics;
using EntityDbClient;
using EntityDbClient.Agent;
using EntityDbClient.Models;
using EntityDbClient.Services;
using EntityDbClient.Views;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EntityDbClient.Tests;

internal static class Program
{
    private static int assertions;
    private static void Assert(bool condition, string message)
    {
        if (!condition) throw new Exception("FAIL: " + message);
        assertions++; Console.WriteLine("PASS: " + message);
    }
    public static async Task Main()
    {
        var directory = Path.Combine(AppContext.BaseDirectory, "EntityDbClientTests-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directory);
        try
        {
            await IntegrationAsync(directory);
            UiSmoke(directory);
            Console.WriteLine($"All {assertions} assertions passed.");
        }
        finally { Directory.Delete(directory, true); }
    }

    private static async Task IntegrationAsync(string directory)
    {
        var downloads = new CodeDownloadStore(directory);
        downloads.Remember("https://example.com/", 1, "model", "Shop.Core", @"D:\Core\Models.cs");
        downloads.Remember("https://example.com", 1, "model", "Shop.Admin", @"D:\Admin\Models.cs");
        Assert(new CodeDownloadStore(directory).Find("https://example.com", 1, "model", "Shop.Core") == @"D:\Core\Models.cs" &&
            downloads.Find("https://example.com", 1, "model", "Shop.Admin") == @"D:\Admin\Models.cs",
            "Download paths persist independently for each selected namespace");
        Assert(downloads.Find("https://other.example.com", 1, "model", "Shop.Core") == null &&
            downloads.Find("https://example.com", 2, "model", "Shop.Core") == null &&
            downloads.Find("https://example.com", 1, "simple", "Shop.Core") == null,
            "Download paths are isolated by server, database and code kind");
        Assert(CodeDownloadStore.Namespaces(" Shop.Core，Shop.Admin, $not lower$, ,Shop.Core ").SequenceEqual(new[] { "Shop.Core", "Shop.Admin" }),
            "Namespace choices handle legacy separators and exclude generation flags");
        var store = new ConnectionStore(directory);
        store.Remember("https://example.com:6060/", "designer", "凭据-Secret-123");
        Assert(store.Load().Single().Url == "https://example.com:6060", "Normalize saved URL");
        Assert(!File.ReadAllText(Path.Combine(directory, "connections.json")).Contains("Secret-123"), "Password is encrypted at rest");
        Assert(new ConnectionStore(directory).GetPassword(store.Load().Single()) == "凭据-Secret-123", "Password survives store restart");
        store.Remember("https://example.com:6060", "designer", "replacement");
        Assert(store.Load().Count == 1 && store.GetPassword(store.Load()[0]) == "replacement", "Reconnect updates existing credentials");
        store.Forget(store.Load()[0]);
        Assert(store.Load().Count == 0, "Forget removes credentials");
        await using var serverA = await FakeServer.StartAsync("A");
        await using var serverB = await FakeServer.StartAsync("B");
        using var workspace = new ClientWorkspace(store);
        await using var api = await AgentApi.StartAsync(workspace);
        var discovery = JsonNode.Parse(File.ReadAllText(api.DiscoveryPath))!;
        using var http = new HttpClient(new HttpClientHandler { UseProxy = false }) { BaseAddress = new Uri(api.BaseUrl) };
        Assert((await http.GetAsync("/api/state")).StatusCode == HttpStatusCode.Unauthorized, "Agent rejects unauthenticated requests");
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", discovery["token"]!.ToString());
        using (var origin = new HttpRequestMessage(HttpMethod.Get, "/api/state"))
        {
            origin.Headers.Add("Origin", "https://example.com");
            Assert((await http.SendAsync(origin)).StatusCode == HttpStatusCode.Unauthorized, "Agent rejects browser-origin requests");
        }
        var caps = await Get(http, "/api/capabilities");
        Assert(caps["operations"]?["GetTableList"] != null && caps["entitySchemas"]?["DBColumn"] != null, "Agent discovers operations and model fields");
        Assert(caps["operations"]!.AsObject().All(x => !x.Key.Contains("Interface")) && caps["entitySchemas"]?["InterfaceModule"] == null, "Interface module removed from Agent operations and schemas");
        Assert(caps["operations"]?["Search"] == null, "Search removed from Agent operations");
        Assert(caps["operations"]?["ModifyTable"] == null && caps["operations"]?["GetTableDeleteConfigList"] == null, "Agent table writes use shared validated workflow");
        var openapi = await Get(http, "/openapi.json");
        Assert(openapi["openapi"]!.ToString() == "3.1.0" && openapi["paths"]?["/api/connections/{id}/rpc/SaveDataTable"] != null, "OpenAPI contains all RPC operations");
        var a = await Post(http, "/api/connections", new { url = serverA.Url, userName = "sa", password = "1" });
        var b = await Post(http, "/api/connections", new { url = serverB.Url, userName = "sa", password = "1" });
        string aId = a["id"]!.ToString(), bId = b["id"]!.ToString();
        Assert(workspace.Connections.Length == 2, "Agent opens two independent workspace connections");
        Assert((await http.PostAsJsonAsync("/api/connections", new { url = serverA.Url, userName = "invalid", password = "wrong" })).StatusCode == HttpStatusCode.BadGateway,
            "Failed login returns server error");
        Assert(workspace.Connections.Length == 2 && store.Load().All(x => x.UserName != "invalid"), "Failed login is neither kept nor saved");
        await Task.WhenAll(Post(http, $"/api/connections/{aId}/rpc/GetTableList", new { parameters = new[] { 1 } }), Post(http, $"/api/connections/{bId}/rpc/GetTableList", new { parameters = new[] { 1 } }));
        Assert(serverA.WrongSessionCount == 0 && serverB.WrongSessionCount == 0, "Parallel requests never cross server sessions");
        var schema = await Get(http, $"/api/connections/{aId}/tables/10/schema");
        Assert(schema["Columns"]!.AsArray().Count == 1 && schema["DBDeleteConfigs"] == null, "Read table schema without cascade UI configuration");
        Assert(schema["schemaHash"]!.ToString().Length == 64, "Schema exposes concurrency hash");
        serverA.ExpireNext = true;
        await Post(http, $"/api/connections/{aId}/rpc/GetTableList", new { parameters = new[] { 1 } });
        Assert(serverA.LoginCount == 2 && serverB.LoginCount == 1, "Expired session relogs only its own connection");
        var design = JsonNode.Parse("""
        {"table":{"DatabaseID":1,"Name":"Accounts","caption":"账户"},"columns":[{"Name":"id","dbType":"int","IsPKID":true,"CanNull":false,"IsAutoIncrement":true}]}
        """)!.AsObject();
        var created = await Post(http, $"/api/connections/{aId}/tables", design);
        Assert(created["Name"]!.ToString() == "Accounts", "Agent creates table through shared service");
        Assert(serverA.LastParameters![0]!["ChangedProperties"]?["Name"] != null, "Legacy DataItem update tracking is present on wire");
        Assert(serverA.LastParameters[2]!.AsArray().Count == 0, "New table has no cascade configuration");
        using (var response = await http.PutAsJsonAsync($"/api/connections/{aId}/tables/10", design)) await Ensure(response);
        Assert(serverA.LastParameters![2]!.AsArray().Count == 1, "Editing preserves existing hidden cascade configuration");
        Assert(serverA.LastParameters[3]!.AsArray().Count == 1 && serverA.LastParameters[4]!.AsArray().Count == 1, "Omitted indexes and navigation properties are preserved");
        var stale = (JsonObject)design.DeepClone(); stale["expectedSchemaHash"] = "outdated";
        Assert((await http.PutAsJsonAsync($"/api/connections/{aId}/tables/10", stale)).StatusCode == HttpStatusCode.Conflict, "Stale schema write returns 409 before mutation");
        var duplicate = (JsonObject)design.DeepClone(); duplicate["columns"]!.AsArray().Add(duplicate["columns"]![0]!.DeepClone());
        Assert((await http.PostAsJsonAsync($"/api/connections/{aId}/tables", duplicate)).StatusCode == HttpStatusCode.BadRequest, "Duplicate columns rejected before server mutation");
        Assert((await http.PostAsJsonAsync($"/api/connections/{aId}/rpc/DeleteTable", new { parameters = Array.Empty<object>() })).StatusCode == HttpStatusCode.BadRequest, "RPC arity validated");
        Assert((await http.PostAsJsonAsync($"/api/connections/{aId}/rpc/Unknown", new { parameters = Array.Empty<object>() })).StatusCode == HttpStatusCode.BadRequest, "Unknown methods rejected");
        foreach (var kind in new[] { "model", "simple", "minimal" })
        {
            var code = await http.GetStringAsync($"/api/connections/{aId}/code?databaseId=1&kind={kind}");
            Assert(code == "// generated", "Binary code download decoded: " + kind);
        }
        var downloadActivities = new System.Collections.Concurrent.ConcurrentQueue<ConnectionActivity>();
        void TrackDownload(ConnectionActivity activity) => downloadActivities.Enqueue(activity);
        workspace.Get(aId).ActivityChanged += TrackDownload;
        var streamedCode = await FileTransfers.GenerateCodeAsync(workspace.Get(aId), 1, "model", "streaming.cs").WaitAsync(TimeSpan.FromSeconds(3));
        workspace.Get(aId).ActivityChanged -= TrackDownload;
        Assert(Encoding.UTF8.GetString(streamedCode) == "// generated", "Code download completes at end marker while HTTP response remains open");
        Assert(downloadActivities.Any(x => x.Progress is > 0 and < 100 && x.Description.Contains("字节")) &&
            downloadActivities.Any(x => x.Progress == 100) && !workspace.Get(aId).Activity.IsBusy,
            "Code download reports actual received bytes, intermediate progress, completion and clears busy state");
        var backup = await http.GetByteArrayAsync($"/api/connections/{aId}/data/export?tableIds=10");
        using (var response = await http.PostAsync($"/api/connections/{aId}/data/import?databaseId=1", new ByteArrayContent(backup))) await Ensure(response);
        Assert(serverA.ImportRows == 1, "Backup export and import preserve legacy binary format");
        Assert(serverA.LastCookie == "WayScriptRemoting=" + workspace.Get(aId).SessionId, "File transfers use owning connection cookie");
        await Post(http, $"/api/connections/{aId}/rpc/ChangePassword", new { parameters = new[] { "1", "new-password" } });
        var saved = store.Load().Single(x => x.Url == serverA.Url);
        Assert(store.GetPassword(saved) == "new-password", "Agent password change updates saved credential");
        var state = await Get(http, "/api/state");
        Assert(state["revision"]!.GetValue<long>() > 2 && state["events"]!.AsArray().Count > 0, "Agent receives structured workspace changes");
        Assert(state["activities"]!.AsArray().All(x => x!["activity"]!["isBusy"]!.GetValue<bool>() == false), "Agent state exposes completed network activity");
        using (var response = await http.DeleteAsync($"/api/connections/{aId}")) Assert(response.IsSuccessStatusCode, "Agent closes only selected connection");
        Assert(workspace.Connections.Single().Id == bId, "Other server stays connected");
        var restored = await Post(http, "/api/connections", new { url = serverA.Url, userName = "sa" });
        Assert(restored["id"] != null && serverA.LastLoginPassword == "new-password", "Agent reconnects using saved password without reading it");
        Assert(DataView.ToValue("123.456", "System.Decimal")!.GetValue<decimal>() == 123.456m && DataView.ToValue("NULL", "System.String") == null, "Data edit values preserve numeric and NULL types");
        var row = new EditableDataRow(["NULL", "1"], 2, [JsonValue.Create("NULL"), JsonValue.Create(1)]);
        row[1] = "2";
        Assert(row.GetWireValue(0, "System.String")!.GetValue<string>() == "NULL", "Editing another cell preserves literal NULL string");
        Assert(row.GetWireValue(1, "System.Int32")!.GetValue<int>() == 2 && row.State == 16, "Edited cells preserve wire type and row state");
    }
    private static void UiSmoke(string directory)
    {
        AppBuilder.Configure<App>().UseSkia().UseHeadless(new AvaloniaHeadlessPlatformOptions { UseHeadlessDrawing = false }).SetupWithoutStarting();
        using var workspace = new ClientWorkspace(new ConnectionStore(Path.Combine(directory, "ui")));
        var window = new MainWindow(workspace, startAgentApi: false);
        window.Show(); window.UpdateLayout(); Dispatcher.UIThread.RunJobs();
        Assert(window.Title == "EntityDbClient" && window.Content is Grid, "Professional shell constructs and lays out");
        Assert(!window.GetVisualDescendants().OfType<TextBox>().Any(), "Main workspace does not contain inline login fields");
        var menus = window.GetVisualDescendants().OfType<Menu>().First();
        Assert(menus.Items.OfType<MenuItem>().Any(x => x.Header?.ToString() == "连接"), "Connection management lives in top menu");
        Snapshot(window, "welcome.png");
        var fakeA = Task.Run(() => FakeServer.StartAsync("UI-A")).GetAwaiter().GetResult();
        var fakeB = Task.Run(() => FakeServer.StartAsync("UI-B")).GetAwaiter().GetResult();
        try
        {
            var connectedA = Task.Run(() => workspace.ConnectAsync(fakeA.Url, "sa", "1")).GetAwaiter().GetResult();
            var connectedB = Task.Run(() => workspace.ConnectAsync(fakeB.Url, "sa", "1")).GetAwaiter().GetResult();
            Dispatcher.UIThread.RunJobs(); window.UpdateLayout();
            var connectionTabs = window.GetVisualDescendants().OfType<TabControl>().Single(x => x.Name == "ConnectionTabs");
            Assert(connectionTabs.Items.Count == 2, "Agent/shared workspace connections appear as two UI tabs");
            var server = (ServerView)((TabItem)connectionTabs.SelectedItem!).Content!;
            var tree = server.GetVisualDescendants().OfType<TreeView>().Single();
            PumpUntil(window, () => tree.Items.Count == 1);
            var project = (ExplorerNode)tree.Items[0]!;
            tree.SelectedItem = project;
            Assert(!project.Loaded, "Selection does not trigger lazy loading");
            tree.SelectedItem = null;
            var projectItem = (TreeViewItem)tree.ContainerFromItem(project)!;
            projectItem.IsExpanded = true;
            PumpUntil(window, () => project.Loaded);
            Assert(tree.SelectedItem == null && project.Children.Count == 1, "Expanding unselected project loads children and omits interface module");
            var databases = project.Children.Single();
            var databaseGroupItem = (TreeViewItem)projectItem.ContainerFromItem(databases)!;
            databaseGroupItem.IsExpanded = true;
            PumpUntil(window, () => databases.Loaded);
            Assert(tree.SelectedItem == null && databases.Children.Count == 1, "Expanding unselected database group fetches server children");
            Assert(!server.GetVisualDescendants().OfType<TextBox>().Any(x => x.PlaceholderText?.Contains("搜索") == true), "Search removed from object explorer");
            var database = databases.Children[0];
            var databaseItem = (TreeViewItem)databaseGroupItem.ContainerFromItem(database)!;
            databaseItem.IsExpanded = true; PumpUntil(window, () => database.Loaded);
            var tableGroup = database.Children.First(x => x.Kind == "tables");
            var tableGroupItem = (TreeViewItem)databaseItem.ContainerFromItem(tableGroup)!;
            tableGroupItem.IsExpanded = true; PumpUntil(window, () => tableGroup.Loaded);
            Assert(tree.SelectedItem == null && tableGroup.Children.Single().Kind == "table", "Expanding table group loads table without selection");
            Assert(fakeB.DatabaseListCalls == 1 && fakeB.TableListCalls == 1, "Each expanded branch loads exactly once");
            databaseGroupItem.IsExpanded = false; databaseGroupItem.IsExpanded = true; window.UpdateLayout();
            Assert(fakeB.DatabaseListCalls == 1, "Repeated expansion reuses loaded children");
            var refresh = server.RefreshAsync(); PumpUntil(window, () => refresh.IsCompleted); refresh.GetAwaiter().GetResult();
            project = (ExplorerNode)tree.Items[0]!;
            databases = project.Children.Single(); database = databases.Children.Single(); tableGroup = database.Children.First(x => x.Kind == "tables");
            Assert(project.IsExpanded && databases.IsExpanded && database.IsExpanded && tableGroup.IsExpanded, "Refresh preserves expanded branches even without selected node");
            var untouchedModules = database.Children.Single(x => x.Kind == "modules");
            tree.SelectedItem = tableGroup;
            var beforeBranchRefresh = fakeB.ReadCalls;
            var beforeTableList = fakeB.TableListCalls;
            tree.ContextMenu!.Items.OfType<MenuItem>().Single(x => x.Header?.ToString() == "刷新").RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
            PumpUntil(window, () => !ReferenceEquals(tree.SelectedItem, tableGroup));
            tableGroup = (ExplorerNode)tree.SelectedItem!;
            Assert(fakeB.ReadCalls == beforeBranchRefresh + 1 && fakeB.TableListCalls == beforeTableList + 1 &&
                tableGroup.IsExpanded && ReferenceEquals(database.Children.Single(x => x.Kind == "modules"), untouchedModules) &&
                ReferenceEquals(tree.Items[0], project), "Tree refresh menu reloads only the selected table branch and preserves selection and expansion");
            tree.SelectedItem = database; window.UpdateLayout();
            Assert(tree.ContextMenu!.Items.OfType<MenuItem>().Any(x => x.Header?.ToString() == "刷新"), "Legacy database refresh is available from right click menu");
            Assert(tree.ContextMenu.Items.Select(x => x is MenuItem m ? m.Header?.ToString() : "|").Take(19).SequenceEqual(new[]
            {
                "生成模型代码", "生成简单模型", "生成最简模型", "|", "导出数据", "导入数据", "|", "导入其他数据库结构", "|",
                "重置变更历史", "|", "移除数据库", "|", "刷新", "|", "数据库属性", "|", "新建表", "变更历史"
            }), "Database context menu preserves legacy ordering and separators before new commands");
            tree.ContextMenu.Items.OfType<MenuItem>().Single(x => x.Header?.ToString() == "数据库属性").RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
            PumpUntil(window, () => window.OwnedWindows.Any());
            var databaseDialog = window.OwnedWindows.Single(); databaseDialog.UpdateLayout();
            var inputs = databaseDialog.GetVisualDescendants().OfType<TextBox>().ToArray();
            Assert(inputs.All(x => x.Text != ((Databases)database.Value!).Guid) &&
                !databaseDialog.GetVisualDescendants().OfType<TextBlock>().Any(x => x.Text?.Contains("GUID") == true),
                "Database configuration does not display GUID field or value");
            Assert(inputs.All(x => x.PasswordChar == default), "Database connection string is displayed in plain text");
            Respond(window, "取消");
            Assert(server.GetVisualDescendants().OfType<Menu>().Any(m => m.Items.OfType<MenuItem>().Any(x => x.Header?.ToString() == "更多")), "Database actions use overflow menu instead of multiple button rows");
            Snapshot(window, "workspace.png");
            window.Width = 1000; window.Height = 680; window.UpdateLayout(); Snapshot(window, "workspace-compact.png");
            Assert(tree.Bounds.Width >= 220 && tree.Bounds.Width <= 500, "Object explorer keeps usable width at minimum window size");
            var busy = (BusyPresenter)server.Content!;
            var connection = workspace.Get(connectedB.Id);
            var release = fakeB.Hold("GetTableList");
            var request = connection.InvokeAsync<DBTable[]>("GetTableList", 1);
            PumpUntil(window, () => fakeB.HeldRequestStarted.Task.IsCompleted);
            var queued = connection.InvokeAsync<JsonObject>("GetTableInfo", 10);
            Assert(busy.IsBusy && !tree.IsEffectivelyEnabled && busy.Description.Length > 0, "Waiting network requests show loading overlay and disable duplicate UI operations");
            Assert(connection.Activity.ActiveRequests == 2, "Concurrent and queued requests keep loading active");
            Assert(!workspace.Get(connectedA.Id).Activity.IsBusy, "Waiting on one server does not mark another server busy");
            Snapshot(window, "network-loading.png");
            release.TrySetResult(); PumpUntil(window, () => request.IsCompleted && queued.IsCompleted && !busy.IsBusy);
            request.GetAwaiter().GetResult(); queued.GetAwaiter().GetResult();
            Assert(tree.IsEffectivelyEnabled && !connection.Activity.IsBusy, "All successful requests restore the interface");
            fakeB.FailNextMethod = "GetTableList";
            var failed = connection.InvokeAsync<DBTable[]>("GetTableList", 1);
            PumpUntil(window, () => failed.IsCompleted && !busy.IsBusy);
            Assert(failed.Exception?.GetBaseException().Message == "模拟请求失败" && tree.IsEffectivelyEnabled, "Server failures clear loading and restore the interface");
            var failedTransfer = connection.TransferAsync("missing-file");
            PumpUntil(window, () => failedTransfer.IsCompleted && !busy.IsBusy);
            Assert(failedTransfer.Exception != null && !connection.Activity.IsBusy, "File transfer failures also clear loading");
            window.Width = 1440; window.Height = 920; window.UpdateLayout();
            tree.SelectedItem = tableGroup.Children.Single(); window.UpdateLayout();
            var design = server.GetVisualDescendants().OfType<Button>().First(x => x.Content?.ToString() == "设计表");
            design.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            PumpUntil(window, () => server.GetVisualDescendants().OfType<TableDesigner>().Any());
            Snapshot(window, "table-designer.png");
            var fieldGrid = server.GetVisualDescendants().OfType<DataGrid>().Single();
            Assert(fieldGrid.ContextMenu!.Items.OfType<MenuItem>().Any(x => x.Header?.ToString() == "复制") &&
                fieldGrid.ContextMenu.Items.OfType<MenuItem>().Any(x => x.Header?.ToString() == "粘贴"), "Field editor exposes copy and paste in unified right click menu");
            var designer = server.GetVisualDescendants().OfType<TableDesigner>().Single();
            Assert(!designer.HasUnsavedChanges, "Opening existing table does not mark it modified");
            var columnEditor = designer.GetVisualDescendants().OfType<ColumnEditor>().Single();
            Assert(columnEditor.GetVisualDescendants().OfType<TextBlock>().Any(x => x.Text == "A字段属性") &&
                columnEditor.GetVisualDescendants().OfType<TextBlock>().Any(x => x.Text == "B继承设置"), "Column properties preserve legacy grouping");
            Assert(columnEditor.GetVisualDescendants().OfType<Control>().Any(x => x.Name == "Property_ClassFullName") &&
                !columnEditor.GetVisualDescendants().OfType<Control>().Any(x => x.Name == "Property_Type"), "Column properties match legacy jsonb and inheritance fields without exposing CLR Type");
            var nullable = columnEditor.GetVisualDescendants().OfType<ComboBox>().Single(x => x.Name == "Property_CanNull");
            Assert(nullable.Items.Cast<string>().SequenceEqual(new[] { "YES", "NO" }), "Legacy boolean properties use YES / NO choices");
            nullable.SelectedItem = "YES";
            Assert(((ColumnItem)fieldGrid.SelectedItem!).Column.CanNull == false, "Primary key cannot be made nullable through property panel");
            var editable = fieldGrid.ItemsSource.Cast<ColumnItem>().Single(x => x.Column.Name == "Name"); fieldGrid.SelectedItem = editable;
            columnEditor.GetVisualDescendants().OfType<TextBox>().Single(x => x.Name == "Property_Name").Text = "NickName";
            Dispatcher.UIThread.RunJobs(); window.UpdateLayout();
            var captionEditor = columnEditor.GetVisualDescendants().OfType<Grid>().Single(x => x.Name == "Property_caption");
            captionEditor.GetVisualDescendants().OfType<TextBox>().Single().Text = "昵称\n完整中文备注";
            Dispatcher.UIThread.RunJobs(); window.UpdateLayout();
            Assert(editable.Column.id == 2 && editable.Caption == "昵称" && editable.Column.caption.Contains("\n"), "Property panel edits retain field identity and multiline Chinese comments");
            columnEditor.GetVisualDescendants().OfType<ComboBox>().Single(x => x.Name == "Property_dbType").SelectedItem = "decimal";
            Assert(editable.Column.length == "18,4", "Database type selection preserves legacy decimal length default");
            var enumEditor = columnEditor.GetVisualDescendants().OfType<Grid>().Single(x => x.Name == "Property_EnumDefine");
            enumEditor.GetVisualDescendants().OfType<TextBox>().Single().Text = "Guest=0,\nMember=1";
            Dispatcher.UIThread.RunJobs(); window.UpdateLayout();
            Assert(editable.Column.dbType == "int" && editable.Column.length == "", "Enum definition automatically switches field to int");
            columnEditor.GetVisualDescendants().OfType<ComboBox>().Single(x => x.Name == "Property_IsDiscriminator").SelectedItem = "YES";
            Assert(editable.Column.IsDiscriminator == true && editable.Column.CanNull == false, "Legacy discriminator setting enforces non-null enum field");
            var other = fieldGrid.ItemsSource.Cast<ColumnItem>().Single(x => x.Column.Name == "Email"); fieldGrid.SelectedItem = other;
            var derived = columnEditor.GetVisualDescendants().OfType<ComboBox>().Single(x => x.Name == "Property_ClassName");
            Assert(derived.Items.Cast<string>().Contains("Member"), "Derived class choices come from discriminator enum");
            derived.SelectedItem = "Member";
            Assert(other.DisplayName == "Member.Email", "Derived class prefix is reflected in field list");
            var editorTabs = designer.GetVisualDescendants().OfType<TabControl>().Single();
            editorTabs.SelectedItem = editorTabs.Items.OfType<TabItem>().Single(x => x.Header?.ToString() == "索引"); window.UpdateLayout();
            designer.GetVisualDescendants().OfType<Button>().Single(x => x.Content?.ToString() == "添加索引").RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            window.UpdateLayout();
            var indexRow = designer.GetVisualDescendants().OfType<IndexRow>().Single();
            indexRow.GetVisualDescendants().OfType<ComboBox>().Single(x => x.Name == "IndexUniqueness").SelectedIndex = 0;
            indexRow.GetVisualDescendants().OfType<ComboBox>().Single(x => x.Name == "IndexClustering").SelectedIndex = 1;
            indexRow.GetVisualDescendants().OfType<ComboBox>().Single(x => x.Name == "IndexColumn_0").SelectedItem = "NickName";
            window.UpdateLayout();
            indexRow.GetVisualDescendants().OfType<ComboBox>().Single(x => x.Name == "IndexColumn_1").SelectedItem = "Email";
            window.UpdateLayout();
            var indexList = designer.GetVisualDescendants().OfType<ListBox>().Single(x => x.Name == "IndexList");
            var selectedIndex = (IndexDesign)indexList.SelectedItem!;
            Assert(selectedIndex.IsUnique && selectedIndex.IsClustered && selectedIndex.ColumnNames.SequenceEqual(new[] { "NickName", "Email" }), "Legacy index row builds ordered composite index with dropdowns");
            Assert(indexRow.GetVisualDescendants().OfType<ComboBox>().Any(x => x.Name == "IndexColumn_2"), "Index selector appends an empty field slot automatically");
            Snapshot(window, "index-editor.png");
            editorTabs.SelectedItem = editorTabs.Items.OfType<TabItem>().Single(x => x.Header?.ToString() == "字段"); window.UpdateLayout();
            fieldGrid.SelectedItem = editable;
            columnEditor.GetVisualDescendants().OfType<TextBox>().Single(x => x.Name == "Property_Name").Text = "DisplayName";
            Dispatcher.UIThread.RunJobs(); window.UpdateLayout();
            Assert(selectedIndex.ColumnNames.SequenceEqual(new[] { "DisplayName", "Email" }), "Renaming a field updates existing index field choices");
            Assert(designer.HasUnsavedChanges, "Table field and index edits are tracked as unsaved");
            Snapshot(window, "field-properties.png");
            window.Width = 1000; window.Height = 680; window.UpdateLayout(); Snapshot(window, "table-designer-compact.png");
            window.Width = 1440; window.Height = 920; window.UpdateLayout();
            projectItem = (TreeViewItem)tree.ContainerFromItem(project)!;
            databaseGroupItem = (TreeViewItem)projectItem.ContainerFromItem(databases)!;
            databaseItem = (TreeViewItem)databaseGroupItem.ContainerFromItem(database)!;
            var modules = database.Children.Single(x => x.Kind == "modules");
            var modulesItem = (TreeViewItem)databaseItem.ContainerFromItem(modules)!;
            modulesItem.IsExpanded = true; PumpUntil(window, () => modules.Loaded);
            var moduleNode = modules.Children.Single();
            var moduleItem = (TreeViewItem)modulesItem.ContainerFromItem(moduleNode)!;
            moduleItem.BringIntoView(); window.UpdateLayout();
            var modulePoint = moduleItem.TranslatePoint(new Point(moduleItem.Bounds.Width - 40, 15), window)!.Value;
            int doubleTaps = 0; tree.DoubleTapped += (_, _) => doubleTaps++;
            window.MouseDown(modulePoint, Avalonia.Input.MouseButton.Left); window.MouseUp(modulePoint, Avalonia.Input.MouseButton.Left);
            window.MouseDown(modulePoint, Avalonia.Input.MouseButton.Left); window.MouseUp(modulePoint, Avalonia.Input.MouseButton.Left);
            PumpUntil(window, () => server.GetVisualDescendants().OfType<ModuleView>().Any());
            Assert(doubleTaps > 0, "Real double click on design module opens document");
            var moduleView = server.GetVisualDescendants().OfType<ModuleView>().Single();
            Assert(!moduleView.HasUnsavedChanges, "Opening module does not mark layout modified");
            Assert(moduleView.GetVisualDescendants().OfType<Canvas>().Single().Children.Count == 1, "Module renders saved table card without crashing");
            var canvas = moduleView.GetVisualDescendants().OfType<Canvas>().Single();
            var card = (Border)canvas.Children.Single();
            var captions = card.GetVisualDescendants().OfType<TextBlock>().Where(x => x.Name == "ColumnCaption").ToArray();
            Assert(captions.Any(x => x.Text == "昵称") && Grid.GetColumn(captions[0]) == 0, "Module table view displays aligned Chinese comments on left side");
            Assert(!card.GetVisualDescendants().OfType<Button>().Any(), "Module table cards contain no inline remove button");
            Assert(card.ContextMenu!.Items.OfType<MenuItem>().Any(x => x.Header?.ToString() == "查看数据…") &&
                card.ContextMenu.Items.OfType<MenuItem>().Any(x => x.Header?.ToString() == "从模块移除…"), "Legacy table card view and remove actions live in right click menu");
            Assert(card.ContextMenu.Items.Select(x => x is MenuItem m ? m.Header?.ToString() : "|")
                .SequenceEqual(new[] { "查看数据…", "|", "从模块移除…", "|", "设计数据表…" }),
                "Table card menu preserves original view/remove ordering before new designer command");
            Assert(canvas.ContextMenu!.Items.OfType<MenuItem>().Any(x => x.Header?.ToString() == "新建数据表…"), "Legacy module canvas create table menu is restored");
            var tablePicker = moduleView.GetVisualDescendants().OfType<ComboBox>().Single();
            Assert(tablePicker.SelectedItem == null && tablePicker.ItemTemplate!.Build(null) == null, "Empty table selection renders safely");
            Assert(tablePicker.ItemTemplate!.Build(new DBTable { Name = "Accounts" }) is TextBlock label && label.Text!.Contains("Accounts"), "Table picker renders table names for nonempty selections");
            tablePicker.SelectedIndex = 0; window.UpdateLayout();
            tablePicker.SelectedIndex = -1; window.UpdateLayout();
            Thread.Sleep(550);
            window.MouseDown(modulePoint, Avalonia.Input.MouseButton.Left); window.MouseUp(modulePoint, Avalonia.Input.MouseButton.Left);
            window.MouseDown(modulePoint, Avalonia.Input.MouseButton.Left); window.MouseUp(modulePoint, Avalonia.Input.MouseButton.Left);
            PumpUntil(window, () => doubleTaps >= 2);
            Assert(ReferenceEquals(moduleView, server.GetVisualDescendants().OfType<ModuleView>().Single()), "Repeated double click reuses existing module document");
            Snapshot(window, "design-module.png");
            int readCalls = fakeB.ReadCalls, layoutWrites = fakeB.LayoutWrites;
            var header = (Border)((StackPanel)card.Child!).Children[0];
            var dragStart = header.TranslatePoint(new Point(50, 15), window)!.Value;
            window.MouseDown(dragStart, Avalonia.Input.MouseButton.Left);
            window.MouseMove(dragStart + new Vector(70, 40)); window.MouseUp(dragStart + new Vector(70, 40), Avalonia.Input.MouseButton.Left);
            Assert(Canvas.GetLeft(card) == 120 && Canvas.GetTop(card) == 90, "Dragging module table updates local position");
            Assert(moduleView.HasUnsavedChanges, "Moving a table marks module layout unsaved");
            var documents = server.GetVisualDescendants().OfType<TabControl>().Single(x => x.Classes.Contains("documents"));
            var moduleTab = documents.Items.OfType<TabItem>().Single(x => ReferenceEquals(x.Content, moduleView));
            ClosePage(moduleTab); PumpUntil(window, () => window.OwnedWindows.Any());
            Assert(documents.Items.Contains(moduleTab), "Closing dirty module prompts before discarding layout");
            Respond(window, "取消");
            Assert(documents.Items.Contains(moduleTab) && moduleView.HasUnsavedChanges, "Canceling close keeps dirty module and edits");
            var saveLayout = moduleView.GetVisualDescendants().OfType<Button>().Single(x => x.Content?.ToString() == "保存布局");
            saveLayout.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            PumpUntil(window, () => fakeB.LayoutWrites == layoutWrites + 1 && saveLayout.IsEnabled && !busy.IsBusy);
            var settle = Stopwatch.StartNew();
            while (settle.ElapsedMilliseconds < 900) { Dispatcher.UIThread.RunJobs(); window.UpdateLayout(); Thread.Sleep(10); }
            Assert(fakeB.ReadCalls == readCalls && ReferenceEquals(card, canvas.Children.Single()), "Save layout only writes coordinates without reloading tree or module");
            saveLayout.RaiseEvent(new RoutedEventArgs(Button.ClickEvent)); window.UpdateLayout();
            Assert(fakeB.LayoutWrites == layoutWrites + 1, "Save unchanged layout makes no network calls");
            Assert(!moduleView.HasUnsavedChanges, "Successful layout save clears unsaved state without reload");
            int readsBeforeDoubleClick = fakeB.ReadCalls;
            var tableDocument = documents.Items.OfType<TabItem>().Single(x => ReferenceEquals(x.Content, designer));
            var bodyPoint = card.TranslatePoint(new Point(180, 60), window)!.Value;
            Thread.Sleep(550);
            window.MouseDown(bodyPoint, Avalonia.Input.MouseButton.Left); window.MouseUp(bodyPoint, Avalonia.Input.MouseButton.Left);
            window.MouseDown(bodyPoint, Avalonia.Input.MouseButton.Left); window.MouseUp(bodyPoint, Avalonia.Input.MouseButton.Left);
            PumpUntil(window, () => ReferenceEquals(documents.SelectedItem, tableDocument));
            Assert(fakeB.ReadCalls == readsBeforeDoubleClick && designer.HasUnsavedChanges, "Double clicking table body opens existing designer and preserves edits without reloading");
            documents.SelectedItem = moduleTab; window.UpdateLayout();
            var titlePoint = header.TranslatePoint(new Point(70, 15), window)!.Value;
            Thread.Sleep(550);
            window.MouseDown(titlePoint, Avalonia.Input.MouseButton.Left); window.MouseUp(titlePoint, Avalonia.Input.MouseButton.Left);
            window.MouseDown(titlePoint, Avalonia.Input.MouseButton.Left); window.MouseUp(titlePoint, Avalonia.Input.MouseButton.Left);
            PumpUntil(window, () => ReferenceEquals(documents.SelectedItem, tableDocument));
            Assert(!moduleView.HasUnsavedChanges, "Double clicking table header opens designer without changing saved layout");
            documents.SelectedItem = moduleTab; window.UpdateLayout();
            var cardPoint = card.TranslatePoint(new Point(30, 35), window)!.Value;
            window.MouseDown(cardPoint, Avalonia.Input.MouseButton.Right); window.MouseUp(cardPoint, Avalonia.Input.MouseButton.Right);
            Assert(card.ContextMenu.IsOpen, "Real right click opens the table card context menu");
            Snapshot(window, "module-context-menu.png");
            var viewData = card.ContextMenu.Items.OfType<MenuItem>().Single(x => x.Header?.ToString() == "查看数据…");
            viewData.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent)); card.ContextMenu.Close();
            PumpUntil(window, () => server.GetVisualDescendants().OfType<DataView>().Any() && !busy.IsBusy);
            var dataGrid = server.GetVisualDescendants().OfType<DataGrid>().Single();
            Assert(dataGrid.ContextMenu!.Items.OfType<MenuItem>().Any(x => x.Header?.ToString() == "删除所选行…"), "Table card view action opens editable data page with row context menu");
            ClosePage(moduleTab); Dispatcher.UIThread.RunJobs();
            Assert(!documents.Items.Contains(moduleTab) && !window.OwnedWindows.Any(), "Closing saved module does not prompt");
            Thread.Sleep(550);
            window.MouseDown(modulePoint, Avalonia.Input.MouseButton.Left); window.MouseUp(modulePoint, Avalonia.Input.MouseButton.Left);
            window.MouseDown(modulePoint, Avalonia.Input.MouseButton.Left); window.MouseUp(modulePoint, Avalonia.Input.MouseButton.Left);
            PumpUntil(window, () => server.GetVisualDescendants().OfType<ModuleView>().Any());
            var unchangedModule = server.GetVisualDescendants().OfType<ModuleView>().Single();
            var unchangedTab = documents.Items.OfType<TabItem>().Single(x => ReferenceEquals(x.Content, unchangedModule));
            var reopenedCanvas = unchangedModule.GetVisualDescendants().OfType<Canvas>().Single();
            var reopenedCard = (Border)reopenedCanvas.Children.Single();
            var moduleReads = fakeB.ReadCalls;
            reopenedCard.ContextMenu!.Items.OfType<MenuItem>().Single(x => x.Header?.ToString() == "从模块移除…")
                .RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
            PumpUntil(window, () => window.OwnedWindows.Any()); Respond(window, "确认");
            PumpUntil(window, () => reopenedCanvas.Children.Count == 0);
            unchangedModule.GetVisualDescendants().OfType<ComboBox>().Single().SelectedIndex = 0;
            unchangedModule.GetVisualDescendants().OfType<Button>().Single(x => x.Content?.ToString() == "添加到模块")
                .RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            PumpUntil(window, () => reopenedCanvas.Children.Count == 1 && !busy.IsBusy);
            Assert(fakeB.ReadCalls == moduleReads + 1 && !unchangedModule.HasUnsavedChanges,
                "Removing and adding a table changes only its card and reads only the added table's columns");
            ClosePage(unchangedTab); Dispatcher.UIThread.RunJobs();
            Assert(!documents.Items.Contains(unchangedTab) && !window.OwnedWindows.Any(), "Closing untouched module does not prompt");
            var dataTab = documents.Items.OfType<TabItem>().Single(x => x.Content is DataView);
            documents.SelectedItem = dataTab; window.UpdateLayout();
            var dataView = (DataView)dataTab.Content!;
            dataGrid = dataView.GetVisualDescendants().OfType<DataGrid>().Single();
            Assert(!dataView.HasUnsavedChanges, "Opening data page does not mark rows modified");
            var dataRow = dataGrid.ItemsSource.Cast<EditableDataRow>().First();
            dataRow[1] = "Bob"; Assert(dataView.HasUnsavedChanges, "Data edits are tracked as unsaved");
            dataRow[1] = "Alice"; Assert(!dataView.HasUnsavedChanges, "Reverting a data edit clears unsaved state");
            dataRow[1] = "Bob";
            dataView.GetVisualDescendants().OfType<Button>().Single(x => x.Content?.ToString() == "保存数据").RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            PumpUntil(window, () => window.OwnedWindows.Any()); Respond(window, "确认");
            PumpUntil(window, () => !busy.IsBusy && !dataView.HasUnsavedChanges);
            ClosePage(dataTab); Dispatcher.UIThread.RunJobs();
            Assert(!documents.Items.Contains(dataTab) && !window.OwnedWindows.Any(), "Closing saved data page does not prompt");
            var tableTab = documents.Items.OfType<TabItem>().Single(x => ReferenceEquals(x.Content, designer));
            documents.SelectedItem = tableTab; window.UpdateLayout();
            designer.GetVisualDescendants().OfType<TextBox>().First(x => x.Text == "Accounts").Text = "AccountsRenamed";
            var beforeSchemaSave = fakeB.ReadCalls;
            var saveTable = designer.GetVisualDescendants().OfType<Button>().Single(x => x.Content?.ToString() == "保存表结构");
            saveTable.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            PumpUntil(window, () => window.OwnedWindows.Any()); Respond(window, "确认");
            PumpUntil(window, () => saveTable.IsEnabled && !busy.IsBusy && !designer.HasUnsavedChanges);
            var rename = workspace.InvokeAsync(connectedB.Id, "UpdateProject", 1, "已重命名项目");
            PumpUntil(window, () => rename.IsCompleted && project.Title == "已重命名项目");
            rename.GetAwaiter().GetResult();
            settle.Restart();
            while (settle.ElapsedMilliseconds < 900) { Dispatcher.UIThread.RunJobs(); window.UpdateLayout(); Thread.Sleep(10); }
            Assert(fakeB.ReadCalls == beforeSchemaSave + 2 && ReferenceEquals(tree.Items[0], project) &&
                tableGroup.Children.Single().Title.StartsWith("AccountsRenamed", StringComparison.Ordinal),
                "Saving schema and renaming a project update tree in place and only read the saved table's schema");
            ClosePage(tableTab); Dispatcher.UIThread.RunJobs();
            Assert(!documents.Items.Contains(tableTab) && !window.OwnedWindows.Any(), "Closing saved table designer does not prompt");
            server.ShowOperations(); window.UpdateLayout();
            var operationTab = documents.Items.OfType<TabItem>().Single(x => x.Content is OperationsView);
            ClosePage(operationTab); Dispatcher.UIThread.RunJobs();
            Assert(!documents.Items.Contains(operationTab) && !window.OwnedWindows.Any(), "Closing non-editing page does not prompt");
            var dialog = new ConnectionDialog(workspace); dialog.Show(window); dialog.UpdateLayout(); Snapshot(dialog, "new-connection.png");
            Assert(dialog.GetVisualDescendants().OfType<TextBox>().Count(x => x.Name is "ServerAddress" or "ServerUserName" or "ServerPassword") == 3, "Separate connection window contains address, username and password");
            dialog.Close();
            var canceledConnection = workspace.Get(connectedA.Id);
            var cancelRelease = fakeA.Hold("GetTableList");
            var canceled = canceledConnection.InvokeAsync<DBTable[]>("GetTableList", 1);
            PumpUntil(window, () => fakeA.HeldRequestStarted.Task.IsCompleted);
            var cleanConnectionTab = connectionTabs.Items.OfType<TabItem>().Single(x => (string?)x.Tag == connectedA.Id);
            ClosePage(cleanConnectionTab); PumpUntil(window, () => canceled.IsCompleted);
            cancelRelease.TrySetResult();
            Assert(canceled.IsCanceled && !canceledConnection.Activity.IsBusy, "Disconnect cancels pending network request and clears loading");
            Dispatcher.UIThread.RunJobs();
            Assert(connectionTabs.Items.Count == 1, "Disconnect updates UI tab without closing other server");
            workspace.Dispose(); Dispatcher.UIThread.RunJobs();
        }
        finally
        {
            Task.Run(async () => { await fakeA.DisposeAsync(); await fakeB.DisposeAsync(); }).GetAwaiter().GetResult();
            window.Close(); Dispatcher.UIThread.RunJobs();
        }
    }
    private static void ClosePage(TabItem tab) => ((Control)tab.Header!).GetVisualDescendants().OfType<Button>().Single().RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
    private static void Respond(Window owner, string button)
    {
        owner.OwnedWindows.Single().GetVisualDescendants().OfType<Button>().Single(x => x.Content?.ToString() == button).RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        PumpUntil(owner, () => !owner.OwnedWindows.Any());
    }
    private static void PumpUntil(Window window, Func<bool> condition)
    {
        var timer = Stopwatch.StartNew();
        while (!condition())
        {
            if (timer.Elapsed > TimeSpan.FromSeconds(8)) throw new TimeoutException("UI operation did not complete");
            Dispatcher.UIThread.RunJobs(); window.UpdateLayout(); Thread.Sleep(10);
        }
        Dispatcher.UIThread.RunJobs(); window.UpdateLayout();
    }
    private static void Snapshot(Window window, string name)
    {
        window.UpdateLayout(); Dispatcher.UIThread.RunJobs(); AvaloniaHeadlessPlatform.ForceRenderTimerTick();
        using var bitmap = window.CaptureRenderedFrame() ?? throw new Exception("Headless render failed");
        var path = Path.Combine(AppContext.BaseDirectory, "previews"); Directory.CreateDirectory(path);
        bitmap.Save(Path.Combine(path, name), Avalonia.Media.Imaging.PngBitmapEncoderOptions.Default);
    }
    private static async Task Ensure(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode) throw new Exception($"HTTP {(int)response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
    }
    private static async Task<JsonNode> Get(HttpClient http, string path)
    { using var response = await http.GetAsync(path); await Ensure(response); return JsonNode.Parse(await response.Content.ReadAsStringAsync())!; }
    private static async Task<JsonNode> Post(HttpClient http, string path, object body)
    { using var response = await http.PostAsJsonAsync(path, body); await Ensure(response); return JsonNode.Parse(await response.Content.ReadAsStringAsync())!; }
}

internal sealed class FakeServer : IAsyncDisposable
{
    private readonly WebApplication app;
    private readonly string name;
    private string session = "";
    public string Url { get; private set; } = "";
    public int LoginCount, WrongSessionCount, ImportRows, DatabaseListCalls, TableListCalls, ReadCalls, LayoutWrites;
    public bool ExpireNext;
    public string? HoldMethod, FailNextMethod;
    public TaskCompletionSource HeldRequestStarted = new(TaskCreationOptions.RunContinuationsAsynchronously);
    private TaskCompletionSource? heldResponse;
    public TaskCompletionSource Hold(string method)
    {
        HeldRequestStarted = new(TaskCreationOptions.RunContinuationsAsynchronously);
        heldResponse = new(TaskCreationOptions.RunContinuationsAsynchronously); HoldMethod = method; return heldResponse;
    }
    public string? LastCookie, LastLoginPassword;
    public JsonArray? LastParameters;
    private FakeServer(WebApplication app, string name) { this.app = app; this.name = name; }
    public static async Task<FakeServer> StartAsync(string name)
    {
        var builder = WebApplication.CreateSlimBuilder(); builder.Logging.ClearProviders(); builder.WebHost.ConfigureKestrel(x => x.Listen(IPAddress.Loopback, 0));
        var app = builder.Build(); var server = new FakeServer(app, name);
        app.MapPost("/wayscriptremoting_invoke", async (HttpRequest request) =>
        {
            var form = await request.ReadFormAsync(); var message = JsonNode.Parse(form["m"].ToString())!;
            string method = message["MethodName"]!.ToString();
            var parameters = JsonNode.Parse(message["ParameterJson"]!.ToString())!.AsArray(); server.LastParameters = parameters;
            if (method == "Login")
            {
                if (parameters[1]?.ToString() == "wrong") return Results.Json(new { type = 4, result = "用户名密码错误" });
                server.LoginCount++; server.session = name + "-" + server.LoginCount;
                server.LastLoginPassword = parameters[1]!.ToString();
                return Results.Json(new { type = 1, sessionid = server.session, result = new[] { 13, 1 } });
            }
            if (message["SessionID"]?.ToString() != server.session) server.WrongSessionCount++;
            if (server.ExpireNext) { server.ExpireNext = false; return Results.Json(new { type = 4, result = "请先登录" }); }
            if (server.HoldMethod == method)
            {
                server.HoldMethod = null; var response = server.heldResponse!;
                server.HeldRequestStarted.TrySetResult(); await response.Task.WaitAsync(request.HttpContext.RequestAborted);
            }
            if (server.FailNextMethod == method) { server.FailNextMethod = null; return Results.Json(new { type = 4, result = "模拟请求失败" }); }
            if (method == "GetDatabaseList") server.DatabaseListCalls++;
            if (method == "GetTableList") server.TableListCalls++;
            if (method.StartsWith("Get", StringComparison.Ordinal)) Interlocked.Increment(ref server.ReadCalls);
            if (method == "UpdateTableInMoudle") Interlocked.Increment(ref server.LayoutWrites);
            JsonNode? result = method switch
            {
                "GetTableInfo" when name.StartsWith("UI-") => JsonNode.Parse("""{"Columns":[{"id":1,"Name":"id","caption":"主键","dbType":"int","IsPKID":true,"IsAutoIncrement":true,"CanNull":false,"length":"","defaultValue":null,"EnumDefine":null,"ClassFullName":null,"ClassName":null},{"id":2,"Name":"Name","caption":"账户名称","dbType":"varchar","length":"100","CanNull":false},{"id":3,"Name":"Email","caption":"电子邮箱","dbType":"varchar","length":"200","CanNull":true},{"id":4,"Name":"Balance","caption":"账户余额","dbType":"decimal","length":"18,2","defaultValue":"0","CanNull":false},{"id":5,"Name":"CreatedAt","caption":"创建时间","dbType":"datetime","length":"","CanNull":false}],"IdxIndexes":[],"ClassProperties":[],"DBDeleteConfigs":[]}"""),
                "GetTableInfo" => JsonNode.Parse("""{"Columns":[{"id":1,"Name":"id","dbType":"int"}],"IdxIndexes":[{"Keys":"id","IsUnique":true,"IsClustered":false}],"ClassProperties":[{"name":"owner"}],"DBDeleteConfigs":[{"RelaTable_Desc":"Detail","RelaColumn_Desc":"ParentId"}]}"""),
                "GetTableList" => JsonNode.Parse("""[{"id":10,"DatabaseID":1,"Name":"Accounts"}]"""),
                "CreateTable" => parameters[0]!.DeepClone(),
                "GetCurrentUserProjectList" => JsonNode.Parse("""[{"id":1,"Name":"商城服务"}]"""),
                "GetDatabaseList" => JsonNode.Parse("""[{"id":1,"ProjectID":1,"Name":"ShopDb","dbType":2,"NameSpace":"Shop.Models","Guid":"56d3cc49-aa2f-4baf-a7b8-f124eaac07d8","conStr":"Server=localhost;Database=ShopDb;Password=test"}]"""),
                "GetDBModuleList" => JsonNode.Parse("""[{"id":21,"DatabaseID":1,"Name":"订单管理","IsFolder":false,"parentID":0}]"""),
                "GetTablesInModule" => JsonNode.Parse("""[{"id":31,"ModuleID":21,"TableID":10,"x":50,"y":50}]"""),
                "GetColumns" => JsonNode.Parse("""[{"id":1,"Name":"id","caption":"主键","dbType":"int","IsPKID":true},{"id":2,"Name":"Name","caption":"昵称","dbType":"varchar","CanNull":true,"length":"50"}]"""),
                "UpdateTableInMoudle" => JsonValue.Create(31),
                "GetColumnList" => JsonNode.Parse("""[{"id":1,"Name":"id","dbType":"int","IsPKID":true,"IsAutoIncrement":true},{"id":2,"Name":"Name","dbType":"varchar"}]"""),
                "GetObjectFormat" => JsonValue.Create("[{0}]"),
                "GetDataTable" => JsonNode.Parse("""{"TableName":"1","Columns":[{"ColumnName":"id","DataType":"System.Int32"},{"ColumnName":"Name","DataType":"System.String"}],"Rows":[{"RowState":2,"Items":[{"Name":"id","Value":1},{"Name":"Name","Value":"Alice"}]}]}"""),
                "ChangePassword" => JsonValue.Create(1),
                _ => null
            };
            return Results.Json(new { type = 1, sessionid = server.session, result });
        });
        app.MapGet("/{page}", async (string page, HttpRequest request) =>
        {
            server.LastCookie = request.Headers.Cookie.ToString();
            using var buffer = new MemoryStream(); using var writer = new BinaryWriter(buffer, Encoding.UTF8, true);
            if (page == "DownloadTableData.aspx")
            {
                writer.Write("[\"Accounts\"]"); writer.Write("[1]"); writer.Write("Accounts"); writer.Write("{\"id\":1}"); writer.Write(":end");
            }
            else if (page is "DownloadDatabaseCode.aspx" or "DownLoadSimpleCodeHandler.aspx" or "DownLoadVerySimpleCodeHandler.aspx")
            {
                writer.Write("start"); writer.Write(1); writer.Write("code.cs"); var bytes = Encoding.UTF8.GetBytes("// generated"); writer.Write(bytes.Length); writer.Write(bytes); writer.Write(":end");
            }
            else return Results.NotFound();
            writer.Flush();
            if (request.Query["filepath"] == "streaming.cs")
            {
                var response = request.HttpContext.Response;
                var payload = buffer.ToArray();
                int split = payload.Length - 11;
                await response.Body.WriteAsync(payload.AsMemory(0, split));
                await response.Body.FlushAsync();
                await Task.Delay(100);
                await response.Body.WriteAsync(payload.AsMemory(split));
                await response.Body.FlushAsync();
                try { await Task.Delay(TimeSpan.FromSeconds(10), request.HttpContext.RequestAborted); }
                catch (OperationCanceledException) { }
                return Results.Empty;
            }
            return Results.Bytes(buffer.ToArray());
        });
        app.MapPost("/ImportTableData.aspx", async (HttpRequest request) =>
        {
            using var buffer = new MemoryStream(); await request.Body.CopyToAsync(buffer); buffer.Position = 0;
            using var reader = new BinaryReader(buffer); reader.ReadString();
            while (reader.ReadString() != ":end") { reader.ReadString(); server.ImportRows++; }
            return Results.Text("ok\r\n");
        });
        await app.StartAsync(); server.Url = app.Services.GetRequiredService<IServer>().Features.Get<IServerAddressesFeature>()!.Addresses.Single(); return server;
    }
    public async ValueTask DisposeAsync() { await app.StopAsync(); await app.DisposeAsync(); }
}
