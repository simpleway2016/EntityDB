using System.Collections;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text.Json;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Input;
using Avalonia.Input.Platform;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using System.Text.Json.Nodes;
using Avalonia.Media;
using EntityDbClient.Services;

namespace EntityDbClient.Views;

public static class Ui
{
    public static readonly IBrush Accent = Brush.Parse("#2563EB");
    public static readonly IBrush Line = Brush.Parse("#E2E7EF");
    public static readonly IBrush ShellBackground = Brush.Parse("#F5F7FA");
    public static TextBlock Muted(string text, bool wrap = false) => new() { Text = text, Foreground = Brush.Parse("#68758A"), FontSize = 12,
        VerticalAlignment = VerticalAlignment.Center, TextWrapping = wrap ? TextWrapping.Wrap : TextWrapping.NoWrap };
    public static Border Panel(Control child, bool bottomBorder = false, bool topBorder = false) => new()
    { Background = Brushes.White, BorderBrush = Line, BorderThickness = new Thickness(0, topBorder ? 1 : 0, 0, bottomBorder ? 1 : 0), Child = child };
    public static Control Field(string label, Control input) => new StackPanel { Spacing = 6, Children = { new TextBlock { Text = label, FontWeight = FontWeight.Medium }, input } };
    public static Button PrimaryButton(string title, Func<Task> action)
    { var button = Button(title, action); button.Classes.Add("accent"); return button; }
    public static MenuItem MenuItem(string title, Func<Task> action, Control? ownerControl = null)
    {
        var item = new MenuItem { Header = title };
        item.Click += async (_, _) =>
        {
            if (!item.IsEnabled) return;
            item.IsEnabled = false;
            try { await action(); }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                var owner = (ownerControl is null ? null : TopLevel.GetTopLevel(ownerControl) as Window) ?? TopLevel.GetTopLevel(item) as Window ??
                    (Application.Current?.ApplicationLifetime as Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime)?.MainWindow;
                if (owner != null) await Message(owner, ex.Message);
            }
            finally { item.IsEnabled = true; }
        };
        return item;
    }
    public static ContextMenu ContextMenu(Control owner, params (string Title, Func<Task> Run)[] actions) => new()
    { ItemsSource = actions.Select(action => MenuItem(action.Title, action.Run, owner)).ToArray() };
    public static Control EmptyState(string title, string text, Control? action = null)
    {
        var panel = new StackPanel { Spacing = 14, MaxWidth = 500, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(40) };
        panel.Children.Add(new TextBlock { Text = title, FontSize = 23, FontWeight = FontWeight.SemiBold, Foreground = Brush.Parse("#24344B") });
        var description = Muted(text, true); description.FontSize = 14; description.LineHeight = 23; panel.Children.Add(description);
        if (action != null) { action.Margin = new Thickness(0, 8, 0, 0); panel.Children.Add(action); }
        return panel;
    }
    public static Control TabHeader(string title, Func<Task> close)
    {
        var text = new TextBlock { Text = title, MaxWidth = 260, TextTrimming = TextTrimming.CharacterEllipsis, VerticalAlignment = VerticalAlignment.Center };
        var button = Button("×", close); button.Classes.Add("tab-close"); ToolTip.SetTip(button, "关闭");
        var row = Row(text, button); row.Margin = new Thickness(0); row.Spacing = 12; return row;
    }
    public static Button Button(string title, Func<Task> action)
    {
        var button = new Button { Content = title, VerticalAlignment = VerticalAlignment.Center };
        button.Click += async (_, _) =>
        {
            if (!button.IsEnabled) return;
            button.IsEnabled = false;
            try { await action(); }
            catch (OperationCanceledException) { }
            catch (Exception ex) { if (TopLevel.GetTopLevel(button) is Window owner) await Message(owner, ex.Message); }
            finally { button.IsEnabled = true; }
        };
        return button;
    }
    public static StackPanel Row(params Control[] controls)
    {
        var row = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8, Margin = new Thickness(0, 0, 0, 8) };
        foreach (var control in controls) row.Children.Add(control);
        return row;
    }
    public static TextBlock Label(string text) => new() { Text = text, VerticalAlignment = VerticalAlignment.Center, TextWrapping = TextWrapping.Wrap };
    public static TextBox Text(string value = "", string? placeholder = null) => new() { Text = value, PlaceholderText = placeholder };
    public static Window Owner(Control control) => (Window)TopLevel.GetTopLevel(control)!;
    public static async Task Message(Window owner, string text)
    {
        var window = Dialog("EntityDbClient", 560, 260);
        window.Content = new StackPanel { Margin = new Thickness(24), Spacing = 16, Children =
        {
            new ScrollViewer { MaxHeight = 150, Content = Label(text) }, Button("确定", () => { window.Close(); return Task.CompletedTask; })
        }};
        await window.ShowDialog(owner);
    }
    public static Window Dialog(string title, double width = 640, double height = 520) => new()
    {
        Title = title, Width = width, Height = height, MinWidth = 420, MinHeight = 220,
        WindowStartupLocation = WindowStartupLocation.CenterOwner
    };
    public static async Task<bool> Confirm(Window owner, string text)
    {
        var dialog = Dialog("确认操作", 520, 230);
        dialog.Content = new StackPanel { Margin = new Thickness(24), Spacing = 18, Children =
        {
            Label(text), Row(Button("确认", () => { dialog.Close(true); return Task.CompletedTask; }),
                Button("取消", () => { dialog.Close(false); return Task.CompletedTask; }))
        }};
        return await dialog.ShowDialog<bool>(owner);
    }
    public static async Task<string?> Prompt(Window owner, string title, string initial = "", bool password = false)
    {
        var dialog = Dialog(title, 520, 230);
        var input = Text(initial);
        if (password) input.PasswordChar = '●';
        dialog.Content = new StackPanel { Margin = new Thickness(24), Spacing = 16, Children =
        {
            Label(title), input, Row(Button("确定", () => { dialog.Close(input.Text ?? ""); return Task.CompletedTask; }),
                Button("取消", () => { dialog.Close(); return Task.CompletedTask; }))
        }};
        return await dialog.ShowDialog<string?>(owner);
    }
    public static T Clone<T>(T value) => JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(value), ServerConnection.Json)!;

    public static async Task<bool> Edit<T>(Window owner, string title, T target, params (string Property, string Label)[] fields) where T : class
    {
        var dialog = Dialog(title);
        var form = new StackPanel { Spacing = 12, Margin = new Thickness(24) };
        var readers = new List<Action>();
        foreach (var (name, label) in fields)
        {
            var property = typeof(T).GetProperty(name)!;
            var type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
            Control editor;
            if (type == typeof(bool))
            {
                var box = new CheckBox { Content = label, IsChecked = (bool?)property.GetValue(target) == true };
                readers.Add(() => property.SetValue(target, box.IsChecked == true));
                editor = box;
            }
            else if (type.IsEnum)
            {
                var combo = new ComboBox { ItemsSource = Enum.GetValues(type), SelectedItem = property.GetValue(target), HorizontalAlignment = HorizontalAlignment.Stretch };
                readers.Add(() => property.SetValue(target, combo.SelectedItem));
                editor = new StackPanel { Spacing = 4, Children = { Label(label), combo } };
            }
            else
            {
                var input = Text(property.GetValue(target)?.ToString() ?? "");
                if (name.Contains("Password", StringComparison.OrdinalIgnoreCase)) input.PasswordChar = '●';
                readers.Add(() => property.SetValue(target, type == typeof(string) ? input.Text ?? "" :
                    string.IsNullOrWhiteSpace(input.Text) ? null : Convert.ChangeType(input.Text, type)));
                editor = new StackPanel { Spacing = 4, Children = { Label(label), input } };
            }
            form.Children.Add(editor);
        }
        form.Children.Add(Row(Button("确定", () => { foreach (var read in readers) read(); dialog.Close(true); return Task.CompletedTask; }),
            Button("取消", () => { dialog.Close(false); return Task.CompletedTask; })));
        dialog.Content = new ScrollViewer { Content = form };
        return await dialog.ShowDialog<bool>(owner);
    }

    public static DataGrid Grid(IEnumerable source, params (string Property, string Label)[] fields)
    {
        var grid = new DataGrid { ItemsSource = source, AutoGenerateColumns = false, GridLinesVisibility = DataGridGridLinesVisibility.All,
            CanUserResizeColumns = true, CanUserSortColumns = false, IsReadOnly = true, SelectionMode = DataGridSelectionMode.Single };
        var itemType = source.GetType().GetElementType() ?? source.GetType().GetGenericArguments().FirstOrDefault();
        foreach (var (property, label) in fields)
        {
            var type = itemType?.GetProperty(property)?.PropertyType;
            var boolean = type == typeof(bool) || type == typeof(bool?);
            DataGridBoundColumn column = boolean ? new DataGridCheckBoxColumn() : new DataGridTextColumn();
            column.Header = label; column.Binding = new Binding(property);
            column.Width = boolean ? new DataGridLength(72) : new DataGridLength(1, DataGridLengthUnitType.Star);
            column.MinWidth = boolean ? 72 : 90;
            grid.Columns.Add(column);
        }
        grid.AddHandler(InputElement.PointerPressedEvent, (_, args) =>
        {
            if (!args.GetCurrentPoint(grid).Properties.IsRightButtonPressed || args.Source is not Visual source) return;
            var row = source as DataGridRow ?? source.GetVisualAncestors().OfType<DataGridRow>().FirstOrDefault();
            if (row?.DataContext != null) grid.SelectedItem = row.DataContext;
        }, RoutingStrategies.Tunnel);
        return grid;
    }

    public static Control ListEditor<T>(ObservableCollection<T> items, Func<T> create, (string, string)[] fields, (string, string)[]? visibleFields = null) where T : class
    {
        var grid = Grid(items, visibleFields ?? fields);
        var dock = new DockPanel();
        async Task Add()
        {
            var item = create();
            if (await Edit(Owner(grid), "添加", item, fields)) items.Add(item);
        }
        async Task EditSelected()
        {
            if (grid.SelectedItem is not T selected) return;
            var copy = Clone(selected);
            if (await Edit(Owner(grid), "编辑", copy, fields)) items[items.IndexOf(selected)] = copy;
        }
        Task Remove() { if (grid.SelectedItem is T selected) items.Remove(selected); return Task.CompletedTask; }
        Task Move(int delta)
        {
            if (grid.SelectedItem is T selected && items.IndexOf(selected) + delta >= 0 && items.IndexOf(selected) + delta < items.Count)
                items.Move(items.IndexOf(selected), items.IndexOf(selected) + delta);
            return Task.CompletedTask;
        }
        async Task Copy()
        {
            if (grid.SelectedItem is not T selected || TopLevel.GetTopLevel(grid)?.Clipboard is not { } clipboard) return;
            await clipboard.SetTextAsync(new JsonObject { ["type"] = typeof(T).FullName, ["item"] = JsonSerializer.SerializeToNode(selected) }.ToJsonString());
        }
        async Task Paste()
        {
            var clipboard = TopLevel.GetTopLevel(grid)?.Clipboard;
            var text = clipboard == null ? null : await clipboard.TryGetTextAsync();
            if (string.IsNullOrWhiteSpace(text)) return;
            var source = JsonNode.Parse(text);
            if (source?["type"]?.ToString() != typeof(T).FullName) throw new ArgumentException("剪贴板中的内容与当前列表类型不一致。");
            var item = source!["item"]!.Deserialize<T>(ServerConnection.Json)!;
            foreach (var property in new[] { "id", "TableID", "tableid" }) typeof(T).GetProperty(property)?.SetValue(item, null);
            if (await Edit(Owner(grid), "粘贴", item, fields)) items.Add(item);
        }
        (string Title, Func<Task> Run)[] commands = [("添加", Add), ("编辑", EditSelected), ("移除", Remove),
            ("上移", () => Move(-1)), ("下移", () => Move(1)), ("复制", Copy), ("粘贴", Paste)];
        var toolbar = Row(commands.Take(5).Select(x => Button(x.Title, x.Run)).ToArray());
        grid.ContextMenu = ContextMenu(grid, commands);
        DockPanel.SetDock(toolbar, Dock.Top);
        dock.Children.Add(toolbar); dock.Children.Add(grid);
        return dock;
    }
}
