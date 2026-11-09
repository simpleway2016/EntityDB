using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Nodes;
using Avalonia;
using Avalonia.Controls;
using EntityDbClient.Models;
using EntityDbClient.Services;

namespace EntityDbClient.Views;

public sealed class UsersView : UserControl
{
    private readonly ClientWorkspace workspace;
    private readonly string id;
    private readonly ObservableCollection<User> users = [];
    private readonly DataGrid grid;
    public UsersView(ClientWorkspace workspace, string id)
    {
        this.workspace = workspace; this.id = id;
        grid = Ui.Grid(users, ("id", "ID"), ("Name", "用户名"), ("Role", "角色"));
        var dock = new DockPanel { Margin = new Thickness(12) };
        var toolbar = Ui.Row(Ui.Button("新增用户", () => EditAsync(new User { Role = User_RoleEnum.数据库设计师 })),
            Ui.Button("编辑", async () => { if (grid.SelectedItem is User user) await EditAsync(Ui.Clone(user)); }),
            Ui.Button("权限设置", PermissionsAsync), Ui.Button("删除", async () =>
            {
                if (grid.SelectedItem is User user && await Ui.Confirm(Ui.Owner(this), "删除用户 " + user.Name + "？"))
                { await workspace.InvokeAsync(id, "DeleteUsers", new int[] { user.id!.Value }); await LoadAsync(); }
            }), Ui.Button("刷新", LoadAsync));
        DockPanel.SetDock(toolbar, Dock.Top); dock.Children.Add(toolbar); dock.Children.Add(grid); Content = dock;
        grid.ContextMenu = Ui.ContextMenu(this,
            ("新增用户…", () => EditAsync(new User { Role = User_RoleEnum.数据库设计师 })),
            ("编辑用户…", async () => { if (grid.SelectedItem is User user) await EditAsync(Ui.Clone(user)); }),
            ("权限设置…", PermissionsAsync),
            ("删除用户…", async () =>
            {
                if (grid.SelectedItem is User user && await Ui.Confirm(Ui.Owner(this), "删除用户 " + user.Name + "？"))
                { await workspace.InvokeAsync(id, "DeleteUsers", new int[] { user.id!.Value }); await LoadAsync(); }
            }), ("刷新", LoadAsync));
    }
    public async Task LoadAsync() { var result = await workspace.Get(id).InvokeAsync<User[]>("GetUsers"); users.Clear(); foreach (var user in result) users.Add(user); }
    private async Task EditAsync(User user)
    {
        if (!await Ui.Edit(Ui.Owner(this), "用户（编辑时密码留空则不修改）", user, ("Name", "用户名"), ("Role", "角色"), ("Password", "密码"))) return;
        if (string.IsNullOrWhiteSpace(user.Name)) throw new ArgumentException("用户名不能为空。");
        var payload = JsonSerializer.SerializeToNode(user)!.AsObject();
        if (user.id != null && user.Password.Length == 0) payload.Remove("Password");
        if (user.id == null && user.Password.Length == 0) throw new ArgumentException("新用户密码不能为空。");
        await workspace.InvokeAsync(id, "UpdateUser", payload); await LoadAsync();
    }
    private async Task PermissionsAsync()
    {
        if (grid.SelectedItem is not User user) return;
        var dialog = Ui.Dialog("权限设置 · " + user.Name, 900, 700);
        var operations = new ComboBox { ItemsSource = new[] { "SetProjectPower", "SetTablePower" }, SelectedIndex = 0 };
        var input = Ui.Text("", "输入项目 ID / 表 ID");
        var databaseId = Ui.Text("", "表权限需填写数据库 ID");
        var allowed = new CheckBox { Content = "授权（取消勾选则撤销）", IsChecked = true };
        var projects = await workspace.Get(id).InvokeAsync<JsonNode>("GetCurrentUserProjectToSetPowerList", user.id);
        var output = new TextBox { Text = projects?.ToJsonString(new JsonSerializerOptions { WriteIndented = true }), IsReadOnly = true, AcceptsReturn = true, Height = 300 };
        dialog.Content = new StackPanel { Margin = new Thickness(20), Spacing = 10, Children =
        {
            Ui.Label("项目及当前授权记录；可用高级操作读取项目下的表权限。"), output, operations, input, databaseId, allowed,
            Ui.Button("应用权限", async () =>
            {
                int target = int.Parse(input.Text ?? "");
                if ((string)operations.SelectedItem! == "SetTablePower") await workspace.InvokeAsync(id, "SetTablePower", int.Parse(databaseId.Text ?? ""), target, user.id, allowed.IsChecked == true);
                else await workspace.InvokeAsync(id, (string)operations.SelectedItem!, target, user.id, allowed.IsChecked == true);
                await Ui.Message(dialog, "权限已更新。");
            })
        }};
        using var busy = new BusyPresenter((Control)dialog.Content!, workspace.Get(id)); dialog.Content = busy;
        await dialog.ShowDialog(Ui.Owner(this));
    }
}
