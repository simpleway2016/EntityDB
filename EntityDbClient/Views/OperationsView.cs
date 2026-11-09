using System.Text.Json;
using System.Text.Json.Nodes;
using Avalonia;
using Avalonia.Controls;
using EntityDbClient.Services;

namespace EntityDbClient.Views;

// Every exposed RPC is available both here and through the Agent API, using identical parameter order.
public sealed class OperationsView : UserControl
{
    public OperationsView(ClientWorkspace workspace, string connectionId)
    {
        var methods = new ComboBox { ItemsSource = RpcCatalog.Methods.Keys.Order().ToArray(), Width = 420 };
        var description = new TextBox { IsReadOnly = true, AcceptsReturn = true, Height = 150 };
        var parameters = new TextBox { Text = "[]", AcceptsReturn = true, Height = 130, PlaceholderText = "JSON 位置参数数组" };
        var result = new TextBox { IsReadOnly = true, AcceptsReturn = true, TextWrapping = Avalonia.Media.TextWrapping.Wrap };
        methods.SelectionChanged += (_, _) =>
        {
            if (methods.SelectedItem is string method) description.Text = RpcCatalog.Methods[method].ToJsonString(new JsonSerializerOptions { WriteIndented = true });
        };
        var dock = new DockPanel { Margin = new Thickness(16) };
        var header = new StackPanel { Spacing = 8, Children =
        {
            Ui.Label("高级操作 · 项目、权限、问题记录、结构导入与变更历史。参数顺序与 Agent API 一致。"),
            methods, description, Ui.Label("参数（JSON 数组；对象的字段可从 Agent /api/capabilities 查询）"), parameters,
            Ui.Button("执行", async () =>
            {
                if (methods.SelectedItem is not string method) return;
                var args = JsonSerializer.Deserialize<JsonElement[]>(parameters.Text ?? "[]")!;
                if (args.Length != RpcCatalog.Methods[method]["parameters"]!.AsArray().Count) throw new ArgumentException("参数数量不一致。");
                if (!method.StartsWith("Get") && !await Ui.Confirm(Ui.Owner(this), $"在当前连接执行 {method}？")) return;
                var response = await workspace.InvokeAsync(connectionId, method, args.Cast<object?>().ToArray());
                result.Text = response?.ToJsonString(new JsonSerializerOptions { WriteIndented = true }) ?? "操作成功（无返回值）";
            })
        }};
        DockPanel.SetDock(header, Dock.Top); dock.Children.Add(header); dock.Children.Add(result); Content = dock;
        methods.SelectedIndex = 0;
    }
}
