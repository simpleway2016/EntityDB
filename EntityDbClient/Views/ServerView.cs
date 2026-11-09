using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia.Interactivity;
using Avalonia.Styling;
using Avalonia.Media;
using Avalonia.VisualTree;
using System.Text.Json;
using System.Text.Json.Nodes;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Platform.Storage;
using EntityDbClient.Models;
using EntityDbClient.Services;

namespace EntityDbClient.Views;

public sealed class ExplorerNode(string title, object? value = null, string kind = "", ExplorerNode? parent = null) : INotifyPropertyChanged
{
    private string currentTitle = title;
    public string Title
    {
        get => currentTitle;
        set { if (currentTitle == value) return; currentTitle = value; PropertyChanged?.Invoke(this, new(nameof(Title))); }
    }
    public object? Value { get; set; } = value;
    public string Kind { get; } = kind;
    public ExplorerNode? Parent { get; set; } = parent;
    public ObservableCollection<ExplorerNode> Children { get; } = [];
    public bool Loaded { get; set; }
    public Task? LoadingTask { get; set; }
    private bool expanded;
    public bool IsExpanded
    {
        get => expanded;
        set { if (expanded == value) return; expanded = value; PropertyChanged?.Invoke(this, new(nameof(IsExpanded))); }
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    public override string ToString() => Title;
}

public sealed class ServerView : UserControl, IDisposable, IUnsavedChanges
{
    private readonly ClientWorkspace workspace;
    private readonly string id;
    private ServerConnection Connection => workspace.Get(id);
    private readonly TreeView tree = new() { Name = "ObjectExplorer", Background = Brushes.Transparent, Margin = new Thickness(6) };
    private readonly ObservableCollection<ExplorerNode> nodes = [];
    private readonly TabControl documents = new() { Classes = { "documents" } };
    private readonly ObservableCollection<TabItem> documentItems = [];
    public bool HasUnsavedChanges => documentItems.Any(tab => tab.Content is IUnsavedChanges { HasUnsavedChanges: true });
    private readonly StackPanel actions = new() { Orientation = Orientation.Horizontal, Spacing = 6 };
    private readonly List<(string Title, Func<Task> Run)> nodeActions = [];
    private readonly TextBlock breadcrumb = new() { Text = "工作空间", FontWeight = FontWeight.Medium, TextTrimming = TextTrimming.CharacterEllipsis, VerticalAlignment = VerticalAlignment.Center };
    private readonly TextBlock status = Ui.Muted("正在加载项目…");
    private readonly TextBlock objectCount = Ui.Muted("");
    private readonly Control empty;
    private readonly SemaphoreSlim refreshGate = new(1, 1);
    private bool disposed;
    private bool refreshing;
    private readonly BusyPresenter busy;

    public ServerView(ClientWorkspace workspace, string id)
    {
        this.workspace = workspace; this.id = id;
        tree.ItemsSource = nodes;
        tree.ItemTemplate = new FuncTreeDataTemplate<ExplorerNode>((node, _) => NodeLabel(node), node => node.Children);
        tree.Styles.Add(new Style(selector => selector.OfType<TreeViewItem>())
        {
            Setters = { new Setter(TreeViewItem.IsExpandedProperty, new Binding(nameof(ExplorerNode.IsExpanded)) { Mode = BindingMode.TwoWay }) }
        });
        tree.AddHandler(TreeViewItem.ExpandedEvent, OnExpanded, RoutingStrategies.Bubble);
        tree.AddHandler(TreeViewItem.CollapsedEvent, (_, args) =>
        {
            if (args.Source is TreeViewItem { DataContext: ExplorerNode node }) node.IsExpanded = false;
        }, RoutingStrategies.Bubble);
        tree.SelectionChanged += (_, _) =>
        {
            if (!refreshing) UpdateActions();
        };
        tree.AddHandler(Avalonia.Input.InputElement.PointerPressedEvent, (_, args) =>
        {
            if (!args.GetCurrentPoint(tree).Properties.IsRightButtonPressed || args.Source is not Visual source) return;
            var item = source as TreeViewItem ?? source.GetVisualAncestors().OfType<TreeViewItem>().FirstOrDefault();
            if (item?.DataContext is ExplorerNode node) { tree.SelectedItem = node; BuildActions(node); }
        }, RoutingStrategies.Tunnel);
        tree.DoubleTapped += async (_, _) =>
        {
            try { if (tree.SelectedItem is ExplorerNode node) await OpenAsync(node); }
            catch (OperationCanceledException) { }
            catch (Exception ex) { await Ui.Message(Ui.Owner(this), ex.Message); }
        };
        documents.ItemsSource = documentItems;
        empty = Ui.EmptyState("选择一个数据库对象", "从左侧展开项目和数据库。\n双击数据表打开设计器，或右键查看可用操作。");
        var documentArea = new Grid(); documentArea.Children.Add(documents); documentArea.Children.Add(empty);
        documentItems.CollectionChanged += (_, _) => { empty.IsVisible = documentItems.Count == 0; documents.IsVisible = documentItems.Count > 0; };
        documents.IsVisible = false;
        var navigation = new Grid { RowDefinitions = new RowDefinitions("44,*,28"), Background = Brushes.White };
        var navigationHeader = new Grid { ColumnDefinitions = new ColumnDefinitions("*,Auto,Auto"), ColumnSpacing = 4, Margin = new Thickness(14, 0, 8, 0) };
        navigationHeader.Children.Add(new TextBlock { Text = "对象资源管理器", FontWeight = FontWeight.SemiBold, FontSize = 12, VerticalAlignment = VerticalAlignment.Center });
        var add = Ui.Button("＋", NewProjectAsync); add.Classes.Add("quiet"); ToolTip.SetTip(add, "新建项目"); Grid.SetColumn(add, 1); navigationHeader.Children.Add(add);
        var refresh = Ui.Button("刷新", RefreshAsync); refresh.Classes.Add("quiet"); Grid.SetColumn(refresh, 2); navigationHeader.Children.Add(refresh);
        navigation.Children.Add(Ui.Panel(navigationHeader, bottomBorder: true));
        Grid.SetRow(tree, 1); navigation.Children.Add(tree);
        objectCount.Margin = new Thickness(14, 0); Grid.SetRow(objectCount, 2); navigation.Children.Add(objectCount);
        var editor = new Grid { RowDefinitions = new RowDefinitions("44,*"), Background = Brushes.White };
        var commandBar = new Grid { ColumnDefinitions = new ColumnDefinitions("*,Auto"), ColumnSpacing = 16, Margin = new Thickness(16, 0, 12, 0) };
        commandBar.Children.Add(breadcrumb); Grid.SetColumn(actions, 1); commandBar.Children.Add(actions);
        editor.Children.Add(Ui.Panel(commandBar, bottomBorder: true));
        Grid.SetRow(documentArea, 1); editor.Children.Add(documentArea);
        var split = new Grid { ColumnDefinitions = new ColumnDefinitions("270,4,*") };
        split.ColumnDefinitions[0].MinWidth = 220; split.ColumnDefinitions[0].MaxWidth = 500;
        split.Children.Add(navigation);
        var splitter = new GridSplitter { Width = 4, HorizontalAlignment = HorizontalAlignment.Stretch, Background = Ui.Line }; Grid.SetColumn(splitter, 1); split.Children.Add(splitter);
        Grid.SetColumn(editor, 2); split.Children.Add(editor);
        var root = new Grid { RowDefinitions = new RowDefinitions("*,26") }; root.Children.Add(split);
        status.Margin = new Thickness(12, 0); status.TextTrimming = TextTrimming.CharacterEllipsis;
        var footer = Ui.Panel(status, topBorder: true); Grid.SetRow(footer, 1); root.Children.Add(footer);
        busy = new BusyPresenter(root, Connection); Content = busy;
    }

    private static Control NodeLabel(ExplorerNode node)
    {
        if (node.Kind.Length == 0) return Ui.Muted(node.Title);
        var badge = new TextBlock { Text = node.Kind switch { "project" => "P", "database" => "D", "table" => "T", "module" => "M", _ => "≡" }, FontSize = 10,
            Foreground = Ui.Accent, FontWeight = FontWeight.SemiBold, VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center };
        return new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8, Children =
        {
            new Border { Width = 18, Height = 18, CornerRadius = new CornerRadius(3), Background = Brush.Parse("#EDF3FF"), Child = badge },
            new TextBlock { [!TextBlock.TextProperty] = new Binding(nameof(ExplorerNode.Title)) { Source = node },
                VerticalAlignment = VerticalAlignment.Center, TextTrimming = TextTrimming.CharacterEllipsis }
        }};
    }
    private async void OnExpanded(object? sender, RoutedEventArgs args)
    {
        if (disposed || args.Source is not TreeViewItem { DataContext: ExplorerNode node }) return;
        node.IsExpanded = true;
        try { await LoadChildrenAsync(node); }
        catch (OperationCanceledException) { }
        catch (Exception ex) { status.Text = "加载失败：" + ex.Message; }
    }
    public Task ShowUsersAsync() => UsersAsync();
    public Task ShowChangePasswordAsync() => ChangePasswordAsync();
    public void ShowOperations() => OpenDocument("高级操作", "operations", new OperationsView(workspace, id));
    public async void Initialize()
    {
        try { await RefreshAsync(); }
        catch (OperationCanceledException) { }
        catch (Exception ex) { status.Text = "加载项目失败：" + ex.Message; }
    }
    public void ApplyChange(WorkspaceChange change)
    {
        if (disposed || change.Parameters is not { } args) return;
        if (change.Kind == "layout-updated")
        {
            status.Text = "模块布局已更新";
            return;
        }

        T? Read<T>(int index) => args[index] is T value ? value :
            JsonSerializer.SerializeToNode(args[index]).Deserialize<T>(ServerConnection.Json);
        ExplorerNode? Find(string kind, int? entityId) => AllNodes(nodes).FirstOrDefault(n => n.Kind == kind && EntityId(n.Value) == entityId);

        switch (change.Method)
        {
            case "CreateProject" when change.Result?.Deserialize<Project>(ServerConnection.Json) is { id: not null } project:
                UpsertNode(null, "project", project, project.Name);
                break;
            case "UpdateProject" when Find("project", Read<int>(0)) is { Value: Project updated } projectNode:
                updated.Name = Read<string>(1)!;
                projectNode.Title = updated.Name;
                break;
            case "DeleteProject": RemoveNode(Find("project", Read<int>(0))); break;
            case "UpdateDatabase" when change.Result?.GetValue<int>() is > 0:
                var database = Ui.Clone(Read<Databases>(0)!);
                database.id = change.Result.GetValue<int>();
                var group = Find("project", database.ProjectID)?.Children.FirstOrDefault(n => n.Kind == "databases");
                if (group is { Loaded: true }) UpsertNode(group, "database", database, database.Name);
                break;
            case "DeleteDatabase": RemoveNode(Find("database", Read<int>(0))); break;
            case "CreateTable":
            case "ModifyTable":
                var table = change.Method == "CreateTable" ? change.Result?.Deserialize<DBTable>(ServerConnection.Json) : Read<DBTable>(0);
                if (table?.id == null) break;
                var tables = Find("database", table.DatabaseID)?.Children.FirstOrDefault(n => n.Kind == "tables");
                if (tables is { Loaded: true }) UpsertNode(tables, "table", table, table.Name + "  " + table.caption, false);
                var columns = Read<DBColumn[]>(1)!;
                foreach (var view in documentItems.Select(x => x.Content).OfType<ModuleView>()) view.UpdateTable(table, columns);
                break;
            case "DeleteTable":
                var databaseId = Read<int>(0);
                var tableName = Read<string>(1);
                RemoveNode(AllNodes(nodes).FirstOrDefault(n => n.Value is DBTable item && item.DatabaseID == databaseId && item.Name == tableName));
                foreach (var view in documentItems.Select(x => x.Content).OfType<ModuleView>()) view.RemoveTable(databaseId, tableName);
                break;
            case "UpdateDBModule" when change.Result?.GetValue<int>() is > 0:
                var module = Ui.Clone(Read<DBModule>(0)!);
                module.id = change.Result.GetValue<int>();
                var destination = module.parentID.GetValueOrDefault() == 0
                    ? Find("database", module.DatabaseID)?.Children.FirstOrDefault(n => n.Kind == "modules")
                    : Find("module", module.parentID);
                var existing = Find("module", module.id);
                if (destination is { Loaded: true } && destination != existing &&
                    (existing == null || !AllNodes(existing.Children).Contains(destination)))
                    UpsertNode(destination, "module", module, module.Name, module.IsFolder == true, existing);
                else RemoveNode(existing);
                break;
            case "DeleteModule": RemoveNode(Find("module", Read<DBModule>(0)?.id)); break;
            case "CreateTables":
                status.Text = "表结构已导入，可用右键「刷新」查看导入结果。";
                return;
            default: return;
        }
        objectCount.Text = $"{nodes.Count} 个项目";
        UpdateActions();
    }

    private static IEnumerable<ExplorerNode> AllNodes(IEnumerable<ExplorerNode> level)
    {
        foreach (var node in level)
        {
            yield return node;
            foreach (var child in AllNodes(node.Children)) yield return child;
        }
    }

    private void UpsertNode(ExplorerNode? parent, string kind, object value, string title, bool loadable = true, ExplorerNode? existing = null)
    {
        var siblings = parent?.Children ?? nodes;
        existing ??= siblings.FirstOrDefault(n => n.Kind == kind && EntityId(n.Value) == EntityId(value));
        if (existing == null)
        {
            siblings.Add(NewNode(title, value, kind, parent, loadable));
            return;
        }
        if (existing.Parent != parent)
        {
            RemoveNode(existing);
            existing.Parent = parent;
            siblings.Add(existing);
        }
        foreach (var child in existing.Children)
            if (ReferenceEquals(child.Value, existing.Value)) child.Value = value;
        existing.Value = value;
        existing.Title = title;
    }

    private void RemoveNode(ExplorerNode? node)
    {
        if (node != null) (node.Parent?.Children ?? nodes).Remove(node);
    }

    private void UpdateActions()
    {
        if (tree.SelectedItem is ExplorerNode { Kind.Length: > 0 } selected) { BuildActions(selected); return; }
        actions.Children.Clear();
        nodeActions.Clear();
        tree.ContextMenu = null;
        breadcrumb.Text = "工作空间";
        ToolTip.SetTip(breadcrumb, null);
    }
    private ExplorerNode NewNode(string title, object value, string kind, ExplorerNode? parent, bool loadable = true)
    {
        var node = new ExplorerNode(title, value, kind, parent) { Loaded = !loadable };
        if (loadable) node.Children.Add(new ExplorerNode("展开以加载…"));
        return node;
    }
    private static string NodePath(ExplorerNode node) => (node.Parent == null ? "" : NodePath(node.Parent) + "/") + node.Kind + ":" + EntityId(node.Value);
    public Task RefreshAsync() => RefreshAsync(null);

    private async Task RefreshAsync(ExplorerNode? branch)
    {
        await refreshGate.WaitAsync();
        try
        {
            if (disposed) return;
            var siblings = branch?.Parent?.Children ?? nodes;
            if (branch != null && !AllNodes(nodes).Contains(branch)) return;
            var previous = branch == null ? nodes.AsEnumerable() : new[] { branch };
            var selection = tree.SelectedItem as ExplorerNode;
            var refreshSelection = branch == null || AllNodes(previous).Contains(selection);
            var selectedPath = refreshSelection && selection != null ? NodePath(selection) : null;
            var expanded = new HashSet<string>();
            var loaded = new HashSet<string>();
            foreach (var node in AllNodes(previous))
            {
                if (node.IsExpanded) expanded.Add(NodePath(node));
                if (branch != null && node.Loaded) loaded.Add(NodePath(node));
            }
            ExplorerNode[] replacements;
            if (branch == null)
            {
                var projects = await Connection.InvokeAsync<Project[]>("GetCurrentUserProjectList");
                replacements = projects.Select(p => NewNode(p.Name, p, "project", null)).ToArray();
            }
            else
            {
                var replacement = await ReadBranchAsync(branch);
                replacements = replacement == null ? [] : [replacement];
            }
            ExplorerNode? restored = null;
            async Task Restore(ExplorerNode node, bool force = false)
            {
                var path = NodePath(node);
                if (path == selectedPath) restored = node;
                var wasExpanded = expanded.Contains(path);
                if (!force && !wasExpanded && !loaded.Contains(path)) return;
                await LoadChildrenAsync(node);
                node.IsExpanded = wasExpanded;
                foreach (var child in node.Children)
                    await Restore(child, force && node.Kind is ("project" or "database"));
            }
            foreach (var node in replacements) await Restore(node, force: branch != null);
            if (disposed) return;
            refreshing = true;
            if (branch == null)
            {
                nodes.Clear();
                foreach (var project in replacements) nodes.Add(project);
                tree.SelectedItem = restored;
                status.Text = $"已连接 · {Connection.UserName} · {Connection.Url}";
            }
            else
            {
                var index = siblings.IndexOf(branch);
                if (index < 0) return;
                if (replacements.Length == 0) siblings.RemoveAt(index);
                else siblings[index] = replacements[0];
                if (refreshSelection) tree.SelectedItem = restored ?? replacements.FirstOrDefault() ?? branch.Parent;
                status.Text = $"已刷新 · {branch.Title}";
            }
            UpdateActions();
            objectCount.Text = $"{nodes.Count} 个项目";
        }
        finally { refreshing = false; refreshGate.Release(); }
    }
    private async Task<ExplorerNode?> ReadBranchAsync(ExplorerNode branch)
    {
        switch (branch.Value)
        {
            case Project project when branch.Kind == "project":
                var projects = await Connection.InvokeAsync<Project[]>("GetCurrentUserProjectList");
                var updatedProject = projects.FirstOrDefault(x => x.id == project.id);
                return updatedProject == null ? null : NewNode(updatedProject.Name, updatedProject, branch.Kind, branch.Parent);
            case Databases database when branch.Kind == "database":
                var updatedDatabase = await Connection.InvokeAsync<Databases?>("GetDatabase", database.id);
                return updatedDatabase == null ? null : NewNode(updatedDatabase.Name, updatedDatabase, branch.Kind, branch.Parent);
            case DBModule module:
                var modules = await Connection.InvokeAsync<DBModule[]>("GetDBModuleList", module.DatabaseID, module.parentID.GetValueOrDefault());
                var updatedModule = modules.FirstOrDefault(x => x.id == module.id);
                return updatedModule == null ? null : NewNode(updatedModule.Name, updatedModule, branch.Kind, branch.Parent, updatedModule.IsFolder == true);
            default:
                return NewNode(branch.Title, branch.Value!, branch.Kind, branch.Parent);
        }
    }
    private static int? EntityId(object? value) => value?.GetType().GetProperty("id")?.GetValue(value) as int?;

    private async Task LoadChildrenAsync(ExplorerNode node)
    {
        if (node.Loaded) return;
        var pending = node.LoadingTask ??= LoadChildrenCoreAsync(node);
        try { await pending; }
        finally { if (ReferenceEquals(node.LoadingTask, pending)) node.LoadingTask = null; }
    }
    private async Task LoadChildrenCoreAsync(ExplorerNode node)
    {
        var children = new List<ExplorerNode>();
        switch (node.Kind)
        {
            case "project":
                children.Add(NewNode("数据库", node.Value!, "databases", node));
                break;
            case "databases":
                foreach (var db in await Connection.InvokeAsync<Databases[]>("GetDatabaseList", ((Project)node.Value!).id))
                    children.Add(NewNode(db.Name, db, "database", node));
                break;
            case "database":
                children.Add(NewNode("数据表", node.Value!, "tables", node));
                children.Add(NewNode("设计模块", node.Value!, "modules", node));
                break;
            case "tables":
                foreach (var table in await Connection.InvokeAsync<DBTable[]>("GetTableList", ((Databases)node.Value!).id))
                    children.Add(NewNode(table.Name + "  " + table.caption, table, "table", node, false));
                break;
            case "modules":
            case "module":
                var dbId = node.Value is Databases dbValue ? dbValue.id : ((DBModule)node.Value!).DatabaseID;
                var parentId = node.Value is DBModule module ? module.id : 0;
                foreach (var moduleValue in await Connection.InvokeAsync<DBModule[]>("GetDBModuleList", dbId, parentId))
                    children.Add(NewNode(moduleValue.Name, moduleValue, "module", node, moduleValue.IsFolder == true));
                break;

        }
        node.Children.Clear(); foreach (var child in children) node.Children.Add(child); node.Loaded = true;
    }

    private void Action(string title, Func<Task> action)
    {
        nodeActions.Add((title, action));
    }
    private void BuildActions(ExplorerNode node)
    {
        nodeActions.Clear();
        actions.Children.Clear();
        switch (node.Kind)
        {
            case "project":
                Action("新建数据库", () => EditDatabaseAsync((Project)node.Value!, null));
                Action("重命名项目", async () =>
                {
                    var project = (Project)node.Value!;
                    var name = await Ui.Prompt(Ui.Owner(this), "项目名称", project.Name);
                    if (!string.IsNullOrWhiteSpace(name)) await workspace.InvokeAsync(id, "UpdateProject", project.id, name);
                });
                Action("删除项目", () => DeleteAsync("项目", "DeleteProject", ((Project)node.Value!).id));
                Action("从 C# 还原模型", () => ImportDesignAsync(((Project)node.Value!).id!.Value));
                break;
            case "databases":
                Action("新建数据库", () => EditDatabaseAsync((Project)node.Value!, null));
                Action("从 C# 还原模型", () => ImportDesignAsync(((Project)node.Value!).id!.Value));
                break;
            case "database":
                var database = (Databases)node.Value!;
                Action("数据库属性", () => EditDatabaseAsync(null, database));
                Action("新建表", () => OpenTableAsync(new DBTable { DatabaseID = database.id }, true));
                foreach (var kind in new[] { ("model", "生成模型代码"), ("simple", "生成简单模型"), ("minimal", "生成最简模型") })
                    Action(kind.Item2, () => ExportCodeAsync(database, kind.Item1));
                Action("导出数据", () => ExportDataAsync(database));
                Action("导入数据", () => ImportDataAsync(database));
                Action("导入其他数据库结构", () => ImportSchemaAsync(database));
                Action("变更历史", async () => OpenJson("变更历史", await Connection.InvokeAsync<JsonNode>("GetActions", database.id)));
                Action("重置变更历史", () => DeleteAsync("表结构变更历史（重建）", "RebuildDatabaseActions", database.id));
                Action("移除数据库", () => DeleteAsync("数据库配置", "DeleteDatabase", database.id));
                break;
            case "tables":
                Action("新建表", () => OpenTableAsync(new DBTable { DatabaseID = ((Databases)node.Value!).id }, true)); break;
            case "table":
                var table = (DBTable)node.Value!;
                Action("设计表", () => OpenTableAsync(table, false));
                Action("查看 / 编辑数据", () => OpenDataAsync(table));
                Action("删除表", () => DeleteAsync("表 " + table.Name + " 及其数据", "DeleteTable", table.DatabaseID, table.Name));
                break;
            case "module":
            case "modules":
                Action("新建目录", () => NewModuleAsync(node, true));
                Action("新建模块", () => NewModuleAsync(node, false));
                if (node.Value is DBModule module)
                {
                    if (module.IsFolder != true) Action("打开模块", () => OpenAsync(node));
                    Action("重命名", async () =>
                    {
                        var name = await Ui.Prompt(Ui.Owner(this), "模块名称", module.Name);
                        if (string.IsNullOrWhiteSpace(name)) return;
                        var copy = Ui.Clone(module); copy.Name = name.Trim();
                        await workspace.InvokeAsync(id, "UpdateDBModule", copy);
                    });
                    Action("模块属性 / 移动", async () =>
                    {
                        var copy = Ui.Clone(module);
                        if (await Ui.Edit(Ui.Owner(this), "模块属性", copy, ("Name", "名称"), ("parentID", "父目录 ID（根目录为 0）"))) await workspace.InvokeAsync(id, "UpdateDBModule", copy);
                    });
                    Action("删除模块", () => DeleteAsync("模块", "DeleteModule", module));
                }
                break;
        }
        if (node.Kind is "project" or "databases" or "database" or "tables" or "modules" or "module") Action("刷新", () => RefreshAsync(node));
        breadcrumb.Text = Breadcrumb(node);
        ToolTip.SetTip(breadcrumb, breadcrumb.Text);
        foreach (var action in nodeActions.Take(2)) actions.Children.Add(Ui.Button(action.Title, action.Run));
        if (nodeActions.Count > 2)
            actions.Children.Add(new Menu { Background = Brushes.Transparent, ItemsSource = new[]
            {
                new MenuItem { Header = "更多", ItemsSource = nodeActions.Skip(2).Select(x => Ui.MenuItem(x.Title, x.Run, this)).ToArray() }
            }});
        tree.ContextMenu = BuildContextMenu(node.Kind);
    }
    private ContextMenu BuildContextMenu(string kind)
    {
        // Keep legacy commands and separators in their original order. New commands follow them.
        string?[] order = kind switch
        {
            "project" => ["删除项目", "重命名项目"],
            "databases" => ["新建数据库", null, "从 C# 还原模型", "刷新"],
            "database" => ["生成模型代码", "生成简单模型", "生成最简模型", null,
                "导出数据", "导入数据", null, "导入其他数据库结构", null, "重置变更历史", null,
                "移除数据库", null, "刷新", null, "数据库属性"],
            "tables" => ["新建表", "刷新"],
            "table" => ["设计表", "查看 / 编辑数据", null, "删除表"],
            "module" or "modules" => ["新建目录", "新建模块", "删除模块", "重命名"],
            _ => []
        };
        var items = new List<Control>();
        var included = new HashSet<string>();
        foreach (var title in order)
        {
            if (title == null) { if (items.LastOrDefault() is MenuItem) items.Add(new Separator()); continue; }
            var action = nodeActions.FirstOrDefault(x => x.Title == title);
            if (action.Run == null) continue;
            items.Add(Ui.MenuItem(action.Title, action.Run, this)); included.Add(title);
        }
        var extra = nodeActions.Where(x => !included.Contains(x.Title)).ToArray();
        if (extra.Length > 0 && items.LastOrDefault() is MenuItem) items.Add(new Separator());
        items.AddRange(extra.Select(x => Ui.MenuItem(x.Title, x.Run, this)));
        return new ContextMenu { ItemsSource = items };
    }
    private static string Breadcrumb(ExplorerNode node) => node.Parent == null ? node.Title : Breadcrumb(node.Parent) + "  /  " + node.Title;

    private async Task OpenAsync(ExplorerNode node)
    {
        if (node.Value is DBTable table) await OpenTableAsync(table, false);
        else if (node.Value is DBModule module && module.IsFolder != true)
        {
            if (SelectDocument("module:" + module.id)) return;
            var view = new ModuleView(workspace, id, module, OpenTableAsync, OpenDataAsync);
            await view.LoadAsync(); OpenDocument(module.Name, "module:" + module.id, view);
        }
    }
    private Task OpenTableAsync(DBTable table, bool create, Func<DBTable, Task>? onCreated = null)
    {
        return OpenTableCoreAsync();
        async Task OpenTableCoreAsync()
        {
            var key = create ? "new:" + Guid.NewGuid() : "table:" + table.id;
            if (SelectDocument(key)) return;
            var editor = new TableDesigner(workspace, id, table, create, onCreated);
            if (!create) await editor.LoadAsync(refreshTable: false);
            OpenDocument(create ? "新建表" : table.Name, key, editor);
        }
    }
    private async Task OpenDataAsync(DBTable table)
    {
        var key = "data:" + table.id;
        if (SelectDocument(key)) return;
        var viewer = new DataView(workspace, id, table); await viewer.LoadAsync(); OpenDocument(table.Name + " · 数据", key, viewer);
    }
    private bool SelectDocument(string key)
    {
        var existing = documentItems.FirstOrDefault(x => (string?)x.Tag == key);
        if (existing == null) return false;
        documents.SelectedItem = existing; return true;
    }
    private void OpenDocument(string title, string key, Control content)
    {
        if (SelectDocument(key)) return;
        var tab = new TabItem { Tag = key, Content = content };
        tab.Header = Ui.TabHeader(title, async () =>
        {
            if (content is IUnsavedChanges { HasUnsavedChanges: true } &&
                !await Ui.Confirm(Ui.Owner(this), "此页面有未保存的修改，关闭后将丢失。确定关闭？")) return;
            documentItems.Remove(tab);
        });
        documentItems.Add(tab); documents.SelectedItem = tab;
    }
    private async Task NewProjectAsync()
    {
        var name = await Ui.Prompt(Ui.Owner(this), "新建项目");
        if (!string.IsNullOrWhiteSpace(name)) await workspace.InvokeAsync(id, "CreateProject", name.Trim());
    }
    private async Task DeleteAsync(string description, string method, params object?[] args)
    {
        if (await Ui.Confirm(Ui.Owner(this), "确定删除 / 移除 " + description + "？")) await workspace.InvokeAsync(id, method, args);
    }
    private async Task EditDatabaseAsync(Project? project, Databases? original)
    {
        var db = original == null ? new Databases { ProjectID = project!.id, Guid = Guid.NewGuid().ToString(), dbType = Databases_dbTypeEnum.SqlServer } : Ui.Clone(original);
        if (await Ui.Edit(Ui.Owner(this), "数据库配置", db, ("Name", "名称"), ("dbType", "数据库类型"), ("conStr", "连接字符串"), ("NameSpace", "命名空间")))
        {
            if (string.IsNullOrWhiteSpace(db.Name) || string.IsNullOrWhiteSpace(db.conStr)) throw new ArgumentException("名称和连接字符串不能为空。");
            await workspace.InvokeAsync(id, "UpdateDatabase", db);
        }
    }
    private async Task NewModuleAsync(ExplorerNode node, bool folder)
    {
        var name = await Ui.Prompt(Ui.Owner(this), folder ? "新建目录" : "新建模块");
        if (string.IsNullOrWhiteSpace(name)) return;
        await workspace.InvokeAsync(id, "UpdateDBModule", new DBModule { Name = name, IsFolder = folder,
            DatabaseID = node.Value is Databases db ? db.id : ((DBModule)node.Value!).DatabaseID,
            parentID = node.Value is DBModule module ? (module.IsFolder == true ? module.id : module.parentID) : 0 });
    }
    private async Task ChangePasswordAsync()
    {
        var old = await Ui.Prompt(Ui.Owner(this), "旧密码", password: true); if (old == null) return;
        var next = await Ui.Prompt(Ui.Owner(this), "新密码", password: true); if (string.IsNullOrEmpty(next)) return;
        await workspace.InvokeAsync(id, "ChangePassword", old, next);
        await Ui.Message(Ui.Owner(this), "密码已修改，并更新本地保存记录。");
    }
    private async Task UsersAsync()
    {
        var view = new UsersView(workspace, id); await view.LoadAsync(); OpenDocument("用户管理", "users", view);
    }
    private void OpenJson(string title, JsonNode? value) => OpenDocument(title, "result:" + Guid.NewGuid(),
        new TextBox { Text = value?.ToJsonString(new JsonSerializerOptions { WriteIndented = true }), AcceptsReturn = true, IsReadOnly = true, TextWrapping = Avalonia.Media.TextWrapping.Wrap, Margin = new Thickness(12) });

    private async Task<IStorageFile?> PickSaveAsync(string fileName, string? previousPath = null)
    {
        var storage = Ui.Owner(this).StorageProvider;
        IStorageFolder? folder = null;
        if (!string.IsNullOrEmpty(previousPath))
        {
            var parent = Path.GetDirectoryName(previousPath);
            if (!string.IsNullOrEmpty(parent) && Directory.Exists(parent))
                folder = await storage.TryGetFolderFromPathAsync(parent);
            fileName = Path.GetFileName(previousPath);
        }
        return await storage.SaveFilePickerAsync(new FilePickerSaveOptions
        { SuggestedFileName = fileName, SuggestedStartLocation = folder, ShowOverwritePrompt = true });
    }
    private async Task<byte[]?> PickReadAsync(string title)
    {
        var files = await Ui.Owner(this).StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions { Title = title, AllowMultiple = false });
        if (files.Count == 0) return null;
        await using var file = await files[0].OpenReadAsync(); using var buffer = new MemoryStream(); await file.CopyToAsync(buffer); return buffer.ToArray();
    }
    private async Task ExportCodeAsync(Databases db, string kind)
    {
        string? nameSpace = null;
        if (kind == "model")
        {
            var choices = CodeDownloadStore.Namespaces(db.NameSpace);
            if (choices.Length == 1) nameSpace = choices[0];
            else if (choices.Length > 1)
            {
                var dialog = Ui.Dialog("选择模型命名空间");
                var selection = new ComboBox { ItemsSource = choices, SelectedIndex = 0, MinWidth = 360 };
                dialog.Content = new StackPanel { Margin = new Thickness(24), Spacing = 16, Children =
                {
                    Ui.Field("命名空间", selection),
                    Ui.Row(Ui.Button("继续…", () => { dialog.Close(true); return Task.CompletedTask; }),
                        Ui.Button("取消", () => { dialog.Close(false); return Task.CompletedTask; }))
                }};
                if (!await dialog.ShowDialog<bool>(Ui.Owner(this))) return;
                nameSpace = (string)selection.SelectedItem!;
            }
        }
        var paths = new CodeDownloadStore();
        var previous = paths.Find(Connection.Url, db.id!.Value, kind, nameSpace);
        var file = await PickSaveAsync(db.Name + ".cs", previous); if (file == null) return;
        var bytes = await FileTransfers.GenerateCodeAsync(Connection, db.id.Value, kind, file.TryGetLocalPath() ?? file.Name, nameSpace);
        await using (var stream = await file.OpenWriteAsync()) { stream.SetLength(0); await stream.WriteAsync(bytes); }
        if (file.TryGetLocalPath() is { } path) paths.Remember(Connection.Url, db.id.Value, kind, nameSpace, path);
        status.Text = "代码已生成。";
    }
    private async Task ExportDataAsync(Databases db)
    {
        var tables = await Connection.InvokeAsync<DBTable[]>("GetTableList", db.id);
        var dialog = Ui.Dialog("选择要导出的表");
        var list = new ListBox { ItemsSource = tables, SelectionMode = SelectionMode.Multiple,
            ItemTemplate = new FuncDataTemplate<DBTable>((x, _) => x is null ? null : Ui.Label(x.Name + " " + x.caption)) };
        foreach (var table in tables) list.SelectedItems!.Add(table);
        var dock = new DockPanel { Margin = new Thickness(16) };
        var accept = Ui.Button("导出所选表", () => { dialog.Close(true); return Task.CompletedTask; }); DockPanel.SetDock(accept, Dock.Bottom); dock.Children.Add(accept); dock.Children.Add(list); dialog.Content = dock;
        if (!await dialog.ShowDialog<bool>(Ui.Owner(this))) return;
        var ids = list.SelectedItems!.Cast<DBTable>().Select(x => x.id!.Value).ToArray();
        var file = await PickSaveAsync(db.Name + ".ejdata"); if (file == null) return;
        var data = await FileTransfers.ExportDataAsync(Connection, ids);
        await using var output = await file.OpenWriteAsync(); output.SetLength(0); await output.WriteAsync(data); status.Text = "数据已导出。";
    }
    private async Task ImportDataAsync(Databases db)
    {
        var bytes = await PickReadAsync("选择 EJClient 数据备份（.ejdata 或旧 .xml）"); if (bytes == null) return;
        if (!await Ui.Confirm(Ui.Owner(this), "将备份数据合并到 " + db.Name + "，主键相同的数据会被更新，是否继续？")) return;
        await FileTransfers.ImportDataAsync(Connection, db.id!.Value, bytes, false); status.Text = "数据导入完成。";
    }
    private async Task ImportDesignAsync(int projectId)
    {
        var bytes = await PickReadAsync("选择包含设计模型的 .cs 文件"); if (bytes == null) return;
        if (!await Ui.Confirm(Ui.Owner(this), "将此 C# 文件中的模型还原到当前项目？")) return;
        await FileTransfers.ImportDesignAsync(Connection, projectId, bytes);
        status.Text = "模型已还原，可用右键「刷新」查看导入结果。";
    }
    private async Task ImportSchemaAsync(Databases target)
    {
        var source = new Databases { dbType = Databases_dbTypeEnum.SqlServer };
        if (!await Ui.Edit(Ui.Owner(this), "来源数据库", source, ("dbType", "类型"), ("conStr", "连接字符串"))) return;
        var names = await Connection.InvokeAsync<string[]>("GetDatabaseCurrentTableNames", source);
        var dialog = Ui.Dialog("选择导入的表");
        var list = new ListBox { ItemsSource = names, SelectionMode = SelectionMode.Multiple };
        var panel = new DockPanel { Margin = new Thickness(16) };
        var ok = Ui.Button("导入所选结构", () => { dialog.Close(true); return Task.CompletedTask; }); DockPanel.SetDock(ok, Dock.Bottom); panel.Children.Add(ok); panel.Children.Add(list); dialog.Content = panel;
        if (!await dialog.ShowDialog<bool>(Ui.Owner(this))) return;
        var infos = new List<object>();
        foreach (string name in list.SelectedItems!) infos.Add(new { TableName = name,
            Columns = await Connection.InvokeAsync<JsonArray>("GetDatabaseCurrentColumns", source, name),
            Indexes = await Connection.InvokeAsync<JsonArray>("GetDatabaseCurrentIndexes", source, name) });
        if (infos.Count > 0) await workspace.InvokeAsync(id, "CreateTables", target.id, infos);
    }
    public void Dispose() { disposed = true; busy.Dispose(); }
}
