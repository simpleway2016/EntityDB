using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;
using EntityDbClient.Agent;
using EntityDbClient.Services;

namespace EntityDbClient.Views;

public sealed class MainWindow : Window
{
    public ClientWorkspace Workspace { get; }
    private readonly TabControl tabs = new() { Name = "ConnectionTabs", Classes = { "connections" } };
    private readonly ObservableCollection<TabItem> tabItems = [];
    private readonly TextBlock status = Ui.Label("就绪");
    private readonly TextBlock connectionCount = Ui.Label("未连接服务器");
    private readonly TextBlock agentStatus = Ui.Label("Agent API 启动中");
    private readonly Control welcome;
    private readonly MenuItem tools;
    private readonly MenuItem disconnect;
    private AgentApi? api;
    private bool closed;
    private ServerView? ActiveServer => (tabs.SelectedItem as TabItem)?.Content as ServerView;

    public MainWindow(ClientWorkspace? workspace = null, bool startAgentApi = true)
    {
        Workspace = workspace ?? new ClientWorkspace();
        Title = "EntityDbClient"; Width = 1440; Height = 920; MinWidth = 1000; MinHeight = 640;
        Background = Ui.ShellBackground;
        tabs.ItemsSource = tabItems;
        var connect = Ui.MenuItem("新建连接…", ShowConnectionDialogAsync);
        connect.HotKey = new KeyGesture(Key.N, KeyModifiers.Control);
        disconnect = Ui.MenuItem("断开当前连接…", DisconnectCurrentAsync);
        tools = new MenuItem { Header = "工具", ItemsSource = new[]
        {
            Ui.MenuItem("用户管理…", async () => { if (ActiveServer is { } server) await server.ShowUsersAsync(); }),
            Ui.MenuItem("修改密码…", async () => { if (ActiveServer is { } server) await server.ShowChangePasswordAsync(); }),
            Ui.MenuItem("高级操作", () => { ActiveServer?.ShowOperations(); return Task.CompletedTask; })
        }};
        var menu = new Menu { Background = Brushes.Transparent, ItemsSource = new MenuItem[]
        {
            new() { Header = "连接", ItemsSource = new Control[] { connect, disconnect, new Separator(), Ui.MenuItem("退出", () => { Close(); return Task.CompletedTask; }) } },
            tools,
            new() { Header = "帮助", ItemsSource = new[]
            {
                Ui.MenuItem("Agent 接口…", ShowAgentInfoAsync),
                Ui.MenuItem("关于 EntityDbClient", () => Ui.Message(this, "EntityDbClient\nEJServer 数据库工作台 · Avalonia 12.1.4"))
            }}
        }};
        var header = new Grid { ColumnDefinitions = new ColumnDefinitions("Auto,Auto,*,Auto"), ColumnSpacing = 22, Margin = new Thickness(18, 0) };
        var brand = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 10, VerticalAlignment = VerticalAlignment.Center, Children =
        {
            new Border { Width = 28, Height = 28, CornerRadius = new CornerRadius(6), Background = Ui.Accent,
                Child = new TextBlock { Text = "E", FontWeight = FontWeight.Bold, Foreground = Brushes.White, FontSize = 18, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center } },
            new TextBlock { Text = "EntityDbClient", FontSize = 15, FontWeight = FontWeight.SemiBold, VerticalAlignment = VerticalAlignment.Center }
        }};
        header.Children.Add(brand); Grid.SetColumn(menu, 1); header.Children.Add(menu);
        connectionCount.Classes.Add("muted"); Grid.SetColumn(connectionCount, 3); header.Children.Add(connectionCount);
        welcome = Ui.EmptyState("数据库工作台", "连接 EJServer，开始管理项目、数据库和数据表。\n每个服务器在独立的 Tab 中打开。", Ui.PrimaryButton("新建连接…", ShowConnectionDialogAsync));
        var body = new Grid(); body.Children.Add(tabs); body.Children.Add(welcome);
        var footer = new Grid { ColumnDefinitions = new ColumnDefinitions("*,Auto"), Margin = new Thickness(14, 0) };
        status.Classes.Add("status"); agentStatus.Classes.Add("status");
        footer.Children.Add(status); Grid.SetColumn(agentStatus, 1); footer.Children.Add(agentStatus);
        ToolTip.SetTip(agentStatus, "通过“帮助 → Agent 接口”查看本机接口地址和发现文件。");
        var root = new Grid { RowDefinitions = new RowDefinitions("50,*,28") };
        root.Children.Add(Ui.Panel(header, bottomBorder: true));
        Grid.SetRow(body, 1); root.Children.Add(body);
        var footerBorder = Ui.Panel(footer, topBorder: true); Grid.SetRow(footerBorder, 2); root.Children.Add(footerBorder);
        Content = root;
        tabs.SelectionChanged += (_, _) => UpdateShell();
        UpdateShell();
        Workspace.Changed += OnWorkspaceChanged;
        Opened += async (_, _) =>
        {
            if (!startAgentApi) return;
            try
            {
                var started = await AgentApi.StartAsync(Workspace);
                if (closed) { await started.DisposeAsync(); return; }
                api = started; agentStatus.Text = "Agent API · 已就绪";
            }
            catch (Exception ex) { agentStatus.Text = "Agent API · 启动失败"; ToolTip.SetTip(agentStatus, ex.Message); }
        };
        Closed += async (_, _) =>
        {
            closed = true; Workspace.Changed -= OnWorkspaceChanged;
            foreach (var tab in tabItems) (tab.Content as ServerView)?.Dispose();
            Workspace.Dispose();
            if (api != null) await api.DisposeAsync();
        };
    }

    private async Task ShowConnectionDialogAsync()
    {
        var dialog = new ConnectionDialog(Workspace);
        var connected = await dialog.ShowDialog<ConnectionInfo?>(this);
        if (connected != null) status.Text = $"已连接 {connected.Url}";
    }
    private async Task DisconnectCurrentAsync()
    {
        if (tabs.SelectedItem is TabItem tab) await DisconnectTabAsync(tab);
    }
    private async Task DisconnectTabAsync(TabItem tab)
    {
        if (tab.Content is IUnsavedChanges { HasUnsavedChanges: true } &&
            !await Ui.Confirm(this, "此连接中有未保存的修改，断开后将丢失。确定断开？")) return;
        Workspace.Disconnect((string)tab.Tag!);
    }
    private Task ShowAgentInfoAsync() => Ui.Message(this, api == null ? "Agent API 尚未就绪。" :
        $"本机接口\n{api.BaseUrl}\n\n发现文件\n{api.DiscoveryPath}\n\nAgent 可读取发现文件获取认证信息，通过 OpenAPI 操作当前连接。");

    private void UpdateShell()
    {
        welcome.IsVisible = tabItems.Count == 0; tabs.IsVisible = tabItems.Count > 0;
        tools.IsEnabled = disconnect.IsEnabled = tabs.SelectedItem != null;
        connectionCount.Text = tabItems.Count == 0 ? "未连接服务器" : $"{tabItems.Count} 个活动连接";
    }
    private void OnWorkspaceChanged(WorkspaceChange change) => Dispatcher.UIThread.Post(() =>
    {
        if (closed) return;
        if (change.Kind == "connected")
        {
            var info = Workspace.Connections.FirstOrDefault(x => x.Id == change.ConnectionId);
            if (info == null) return;
            var view = new ServerView(Workspace, change.ConnectionId);
            var tab = new TabItem { Tag = change.ConnectionId, Content = view };
            var title = $"{new Uri(info.Url).Authority}  /  {info.UserName}";
            tab.Header = Ui.TabHeader(title, () => DisconnectTabAsync(tab));
            ToolTip.SetTip(tab, info.Url + "\n" + info.UserName);
            tabItems.Add(tab); tabs.SelectedItem = tab; view.Initialize();
        }
        else if (change.Kind == "disconnected")
        {
            var tab = tabItems.FirstOrDefault(x => (string?)x.Tag == change.ConnectionId);
            if (tab?.Content is ServerView view) view.Dispose();
            if (tab != null) tabItems.Remove(tab);
            if (tabs.SelectedItem == null && tabItems.Count > 0) tabs.SelectedItem = tabItems[0];
            status.Text = "服务器连接已断开";
        }
        else if (tabItems.FirstOrDefault(x => (string?)x.Tag == change.ConnectionId)?.Content is ServerView view) view.ApplyChange(change);
        UpdateShell();
    });
}
