using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using EntityDbClient.Services;

namespace EntityDbClient.Views;

public sealed class ConnectionDialog : Window
{
    private readonly ClientWorkspace workspace;
    private readonly ComboBox history = new() { HorizontalAlignment = HorizontalAlignment.Stretch, PlaceholderText = "选择已保存的连接" };
    private readonly TextBox url = Ui.Text("https://localhost:6060", "https://server:6060");
    private readonly TextBox user = Ui.Text("sa", "用户名");
    private readonly TextBox password = new() { PasswordChar = '●', PlaceholderText = "服务器密码" };
    private readonly TextBlock error = new() { Foreground = Brushes.Firebrick, TextWrapping = TextWrapping.Wrap };
    private readonly Grid form;
    private readonly ProgressBar progress = new() { IsIndeterminate = true, Height = 4, IsVisible = false };
    public ConnectionDialog(ClientWorkspace workspace)
    {
        this.workspace = workspace;
        Title = "新建连接"; Width = 560; Height = 480; MinWidth = 480; MinHeight = 480;
        url.Name = "ServerAddress"; user.Name = "ServerUserName"; password.Name = "ServerPassword";
        CanResize = false; WindowStartupLocation = WindowStartupLocation.CenterOwner;
        var root = new Grid { RowDefinitions = new RowDefinitions("Auto,*,Auto"), Background = Brushes.White };
        var heading = new StackPanel { Margin = new Thickness(28, 24, 28, 20), Spacing = 6, Children =
        {
            new TextBlock { Text = "连接 EJServer", FontSize = 21, FontWeight = FontWeight.SemiBold },
            Ui.Muted("创建独立的服务器工作区，连接信息将自动保存。")
        }};
        root.Children.Add(heading);
        form = new Grid { RowDefinitions = new RowDefinitions("Auto,Auto,Auto,Auto,Auto"), RowSpacing = 16, Margin = new Thickness(28, 0, 28, 12) };
        var saved = new Grid { ColumnDefinitions = new ColumnDefinitions("*,Auto"), ColumnSpacing = 8 };
        saved.Children.Add(history);
        var forget = Ui.Button("移除记录", async () =>
        {
            if (history.SelectedItem is not SavedConnection selected) return;
            await workspace.ForgetAsync(selected); ReloadHistory(); password.Text = "";
        });
        Grid.SetColumn(forget, 1); saved.Children.Add(forget);
        form.Children.Add(Ui.Field("最近连接", saved));
        var address = Ui.Field("服务器地址", url); Grid.SetRow(address, 1); form.Children.Add(address);
        var credentials = new Grid { ColumnDefinitions = new ColumnDefinitions("*,*"), ColumnSpacing = 16 };
        credentials.Children.Add(Ui.Field("用户名", user));
        var passwordField = Ui.Field("密码", password); Grid.SetColumn(passwordField, 1); credentials.Children.Add(passwordField);
        Grid.SetRow(credentials, 2); form.Children.Add(credentials);
        var note = Ui.Muted("密码在本机加密保存。下次可直接选择最近连接。", wrap: true); Grid.SetRow(note, 3); form.Children.Add(note);
        var errorArea = new StackPanel { Spacing = 8, Children = { progress, new ScrollViewer { MaxHeight = 44, Content = error } } }; Grid.SetRow(errorArea, 4); form.Children.Add(errorArea);
        Grid.SetRow(form, 1); root.Children.Add(form);
        var buttons = Ui.Row(Ui.Button("取消", () => { Close(); return Task.CompletedTask; }), Ui.PrimaryButton("连接", ConnectAsync));
        buttons.HorizontalAlignment = HorizontalAlignment.Right; buttons.Margin = new Thickness(28, 16);
        var footer = Ui.Panel(buttons, topBorder: true); Grid.SetRow(footer, 2); root.Children.Add(footer);
        Content = root;
        history.SelectionChanged += (_, _) =>
        {
            if (history.SelectedItem is not SavedConnection entry) return;
            url.Text = entry.Url; user.Text = entry.UserName;
            try { password.Text = workspace.Store.GetPassword(entry); error.Text = ""; }
            catch { password.Text = ""; error.Text = "无法解密此记录的密码，请重新输入。"; }
        };
        try { ReloadHistory(); if (history.ItemCount > 0) history.SelectedIndex = 0; }
        catch (Exception ex) { error.Text = "读取连接记录失败：" + ex.Message; }
        KeyDown += async (_, args) =>
        {
            if (args.Key == Avalonia.Input.Key.Escape) Close();
            else if (args.Key == Avalonia.Input.Key.Enter && form.IsEnabled) { args.Handled = true; await ConnectAsync(); }
        };
    }
    private void ReloadHistory() => history.ItemsSource = workspace.Store.Load();
    private async Task ConnectAsync()
    {
        if (!form.IsEnabled) return;
        error.Text = "正在连接…"; form.IsEnabled = false; progress.IsVisible = true;
        try
        {
            if (string.IsNullOrWhiteSpace(user.Text)) throw new ArgumentException("请输入用户名。");
            var result = await workspace.ConnectAsync(url.Text ?? "", user.Text.Trim(), password.Text ?? "");
            Close(result);
        }
        catch (Exception ex) { error.Text = ex.Message; }
        finally { form.IsEnabled = true; progress.IsVisible = false; }
    }
}
