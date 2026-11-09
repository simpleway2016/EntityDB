using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input.Platform;
using Avalonia.Layout;
using Avalonia.Media;
using EntityDbClient.Models;
using EntityDbClient.Services;

namespace EntityDbClient.Views;

public sealed class ColumnItem(DBColumn column) : INotifyPropertyChanged
{
    public DBColumn Column { get; } = column;
    public string DisplayName => (string.IsNullOrEmpty(Column.ClassName) ? "" : Column.ClassName + ".") + Column.Name;
    public string Caption => (Column.caption ?? "").Split('\n')[0].TrimEnd('\r');
    public bool IsChecked { get; set; }
    public event PropertyChangedEventHandler? PropertyChanged;
    public void Refresh() { PropertyChanged?.Invoke(this, new(nameof(DisplayName))); PropertyChanged?.Invoke(this, new(nameof(Caption))); }
}

public sealed class ColumnEditor : UserControl
{
    private readonly ObservableCollection<DBColumn> columns;
    private readonly ObservableCollection<IndexDesign> indexes;
    private readonly ObservableCollection<ColumnItem> items = [];
    private readonly DataGrid list;
    private readonly StackPanel properties = new() { Spacing = 0 };
    private readonly TextBlock error = new() { Foreground = Brushes.Firebrick, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(8) };
    private readonly TextBlock help = Ui.Muted("选择左侧字段，编辑右侧属性。", true);
    private readonly Dictionary<string, Control> editors = [];
    private bool updating;
    private bool synchronizing;

    public ColumnEditor(ObservableCollection<DBColumn> columns, ObservableCollection<IndexDesign> indexes)
    {
        this.columns = columns; this.indexes = indexes;
        list = Ui.Grid(items, ("DisplayName", "字段名"), ("Caption", "中文注释")); list.Name = "ColumnList";
        list.SelectionMode = DataGridSelectionMode.Extended; list.IsReadOnly = false;
        foreach (var column in list.Columns) column.IsReadOnly = true;
        list.Columns.Insert(0, new DataGridCheckBoxColumn { Header = "", Binding = new Binding("IsChecked") { Mode = BindingMode.TwoWay }, Width = new DataGridLength(30), MinWidth = 30 });
        list.SelectionChanged += (_, _) => { if (!synchronizing) BuildProperties(); };
        (string Title, Func<Task> Run)[] commands = [("添加", Add), ("移除", Remove), ("上移", () => Move(-1)), ("下移", () => Move(1)), ("复制", Copy), ("粘贴", Paste)];
        var toolbar = new StackPanel { Spacing = 6, Margin = new Thickness(4, 0) };
        var symbols = new[] { "＋", "－", "↑", "↓", "复制", "粘贴" };
        for (int i = 0; i < commands.Length; i++)
        {
            var button = Ui.Button(symbols[i], commands[i].Run); button.Padding = new Thickness(2, 5); button.HorizontalAlignment = HorizontalAlignment.Stretch;
            ToolTip.SetTip(button, commands[i].Title); Avalonia.Automation.AutomationProperties.SetName(button, commands[i].Title); toolbar.Children.Add(button);
        }
        list.ContextMenu = Ui.ContextMenu(this, commands);
        var right = new DockPanel { Margin = new Thickness(10, 0, 0, 0) };
        var feedback = new StackPanel { Children = { error, help } }; DockPanel.SetDock(feedback, Dock.Bottom); right.Children.Add(feedback);
        right.Children.Add(new ScrollViewer { Content = properties });
        var root = new Grid { ColumnDefinitions = new ColumnDefinitions("*,42,4,*") };
        root.ColumnDefinitions[0].MinWidth = 220; root.ColumnDefinitions[3].MinWidth = 280;
        root.Children.Add(list); Grid.SetColumn(toolbar, 1); root.Children.Add(toolbar);
        var splitter = new GridSplitter { Width = 4, Background = Ui.Line, HorizontalAlignment = HorizontalAlignment.Stretch };
        Grid.SetColumn(splitter, 2); root.Children.Add(splitter); Grid.SetColumn(right, 3); root.Children.Add(right); Content = root;
        columns.CollectionChanged += (_, _) => Synchronize(); Synchronize();
    }
    private void Synchronize()
    {
        var selected = (list.SelectedItem as ColumnItem)?.Column;
        synchronizing = true;
        var previous = items.ToArray(); items.Clear();
        foreach (var column in columns) items.Add(previous.FirstOrDefault(x => ReferenceEquals(x.Column, column)) ?? new ColumnItem(column));
        list.SelectedItem = items.FirstOrDefault(x => ReferenceEquals(x.Column, selected)) ?? items.FirstOrDefault();
        synchronizing = false;
        if (!ReferenceEquals(selected, Selected) || properties.Children.Count == 0) BuildProperties();
    }
    private DBColumn? Selected => (list.SelectedItem as ColumnItem)?.Column;
    public bool HasPendingChanges => Selected is { } column && editors.Any(pair =>
    {
        var input = pair.Value as TextBox ?? (pair.Value as Grid)?.Children.OfType<TextBox>().FirstOrDefault();
        if (input == null) return false;
        var value = input.Text ?? "";
        var original = typeof(DBColumn).GetProperty(pair.Key)!.GetValue(column)?.ToString() ?? "";
        if (pair.Key is "Name" or "caption") { value = value.Trim(); original = original.Trim(); }
        return value != original;
    });
    public void ValidatePending()
    {
        // TextChanged is queued by Avalonia; commit the visible values before sending a save request.
        if (Selected is { } column)
            foreach (var (property, editor) in editors)
            {
                var input = editor as TextBox ?? (editor as Grid)?.Children.OfType<TextBox>().FirstOrDefault();
                if (input == null) continue;
                var value = input.Text ?? "";
                if ((typeof(DBColumn).GetProperty(property)!.GetValue(column)?.ToString() ?? "") != value) Apply(column, property, value);
            }
        ColumnRules.Validate(columns);
        error.Text = "";
    }
    private void Apply(DBColumn column, string property, object? value)
    {
        var oldName = column.Name; ColumnRules.Set(column, property, value, columns);
        if (property == "Name" && oldName != column.Name)
            foreach (var index in indexes) index.Keys = string.Join(',', index.ColumnNames.Select(x => x == oldName ? column.Name : x));
        foreach (var item in items) item.Refresh(); RefreshDependentEditors();
    }
    private void Set(string property, object? value, DBColumn? target = null)
    {
        var column = target ?? Selected;
        if (updating || column == null || !columns.Contains(column)) return;
        try
        {
            var existing = typeof(DBColumn).GetProperty(property)!.GetValue(column);
            if (Equals(existing, value) || existing is null && value is string { Length: 0 }) return;
            Apply(column, property, value); error.Text = "";
        }
        catch (ArgumentException ex) { error.Text = ex.Message; RefreshDependentEditors(); }
    }
    private void RefreshDependentEditors()
    {
        if (Selected is not { } column) return;
        updating = true;
        try
        {
            foreach (var property in new[] { "CanNull", "IsPKID", "IsAutoIncrement", "IsDiscriminator" })
                if (editors.TryGetValue(property, out var editor)) ((ComboBox)editor).SelectedItem = (bool?)typeof(DBColumn).GetProperty(property)!.GetValue(column) == true ? "YES" : "NO";
            if (editors.TryGetValue("dbType", out var type)) ((ComboBox)type).SelectedItem = column.dbType;
            if (editors.TryGetValue("length", out var length)) ((TextBox)length).Text = column.length;
            if (editors.TryGetValue("ClassName", out var className))
            {
                var choices = new[] { "" }.Concat(ColumnRules.EnumNames(columns.FirstOrDefault(x => x.IsDiscriminator == true)?.EnumDefine)).Append(column.ClassName ?? "").Distinct().ToArray();
                ((ComboBox)className).ItemsSource = choices; ((ComboBox)className).SelectedItem = column.ClassName ?? "";
            }
        }
        finally { updating = false; }
    }
    private void BuildProperties()
    {
        updating = true; properties.Children.Clear(); editors.Clear(); error.Text = "";
        if (Selected is not { } column) { updating = false; return; }
        Group("A字段属性");
        Text("Name", "1:Name", column.Name);
        Multiline("caption", "2:中文注释", column.caption, "中文注释，换行后可以备注更多内容");
        Boolean("CanNull", "3:CanNull", column.CanNull);
        Boolean("IsPKID", "3:主键", column.IsPKID);
        Boolean("IsAutoIncrement", "3:自增长", column.IsAutoIncrement);
        Text("ClassFullName", "4:jsonb类型", column.ClassFullName, "jsonb 对应的类型 FullName");
        var type = new ComboBox { ItemsSource = ColumnRules.DatabaseTypes.Append(column.dbType).Distinct().Order().ToArray(), SelectedItem = column.dbType, HorizontalAlignment = HorizontalAlignment.Stretch };
        type.SelectionChanged += (_, _) => { if (type.SelectedItem is string value) Set("dbType", value); };
        Row("dbType", "4:数据库中的类型", type);
        Text("defaultValue", "5:默认值", column.defaultValue);
        Multiline("EnumDefine", "6:Enum", column.EnumDefine, "枚举定义；设置后数据库类型自动设为 int");
        Text("length", "7:Length", column.length);
        Group("B继承设置");
        Boolean("IsDiscriminator", "1:IsDiscriminator", column.IsDiscriminator);
        var className = new ComboBox { HorizontalAlignment = HorizontalAlignment.Stretch };
        className.SelectionChanged += (_, _) => { if (className.SelectedItem is string value) Set("ClassName", value); };
        Row("ClassName", "2:所属派生类", className);
        updating = false; RefreshDependentEditors();
    }
    private void Group(string title) => properties.Children.Add(new Border { Background = Ui.ShellBackground, Padding = new Thickness(8, 7), Child = new TextBlock { Text = title, FontWeight = FontWeight.SemiBold } });
    private void Row(string property, string title, Control editor, string? description = null)
    {
        editor.Name = "Property_" + property; editors[property] = editor;
        var row = new Grid { ColumnDefinitions = new ColumnDefinitions("155,*"), MinHeight = 34 };
        var label = new TextBlock { Text = title, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(8, 0), TextTrimming = TextTrimming.CharacterEllipsis };
        row.Children.Add(label); Grid.SetColumn(editor, 1); row.Children.Add(editor);
        row.GotFocus += (_, _) => help.Text = description ?? title;
        properties.Children.Add(Ui.Panel(row, bottomBorder: true));
    }
    private void Text(string property, string title, string? value, string? description = null)
    {
        var column = Selected;
        var text = Ui.Text(value ?? ""); text.TextChanged += (_, _) => Set(property, text.Text ?? "", column); Row(property, title, text, description);
    }
    private void Boolean(string property, string title, bool? value)
    {
        var combo = new ComboBox { ItemsSource = new[] { "YES", "NO" }, SelectedItem = value == true ? "YES" : "NO", HorizontalAlignment = HorizontalAlignment.Stretch };
        combo.SelectionChanged += (_, _) => Set(property, combo.SelectedItem?.ToString() == "YES"); Row(property, title, combo);
    }
    private void Multiline(string property, string title, string? value, string description)
    {
        var target = Selected;
        var text = Ui.Text(value ?? ""); text.TextChanged += (_, _) => Set(property, text.Text ?? "", target);
        var edit = Ui.Button("…", async () =>
        {
            if (Selected is not { } column) return;
            var dialog = Ui.Dialog(title, 650, 450);
            var input = new TextBox { Text = typeof(DBColumn).GetProperty(property)!.GetValue(column)?.ToString(), AcceptsReturn = true, TextWrapping = TextWrapping.Wrap };
            var body = new DockPanel { Margin = new Thickness(16) };
            var buttons = Ui.Row(Ui.Button("取消", () => { dialog.Close(); return Task.CompletedTask; }), Ui.PrimaryButton("确定", () =>
            { Set(property, input.Text ?? ""); if (string.IsNullOrEmpty(error.Text)) { text.Text = input.Text; dialog.Close(); } return Task.CompletedTask; }));
            DockPanel.SetDock(buttons, Dock.Bottom); body.Children.Add(buttons); body.Children.Add(input); dialog.Content = body; await dialog.ShowDialog(Ui.Owner(this));
        });
        var line = new Grid { ColumnDefinitions = new ColumnDefinitions("*,32") }; line.Children.Add(text); Grid.SetColumn(edit, 1); line.Children.Add(edit);
        Row(property, title, line, description);
    }
    private Task Add()
    {
        int count = 1; while (columns.Any(x => string.Equals(x.Name, "column" + count, StringComparison.OrdinalIgnoreCase))) count++;
        var column = new DBColumn { Name = "column" + count }; var index = Selected is { } current ? columns.IndexOf(current) + 1 : columns.Count;
        columns.Insert(index, column); list.SelectedItem = items.First(x => ReferenceEquals(x.Column, column)); return Task.CompletedTask;
    }
    private Task Remove()
    {
        var selected = items.Where(x => x.IsChecked).ToArray(); if (selected.Length == 0) selected = list.SelectedItems.Cast<ColumnItem>().ToArray();
        foreach (var item in selected)
        {
            columns.Remove(item.Column);
            foreach (var index in indexes) index.Keys = string.Join(',', index.ColumnNames.Where(x => x != item.Column.Name));
        }
        return Task.CompletedTask;
    }
    private Task Move(int delta)
    {
        if (Selected is { } column && columns.IndexOf(column) + delta >= 0 && columns.IndexOf(column) + delta < columns.Count)
            columns.Move(columns.IndexOf(column), columns.IndexOf(column) + delta);
        return Task.CompletedTask;
    }
    private async Task Copy()
    {
        list.CommitEdit(DataGridEditingUnit.Cell, true); list.CommitEdit(DataGridEditingUnit.Row, true);
        var selected = items.Where(x => x.IsChecked).Select(x => x.Column).ToArray();
        if (selected.Length == 0) selected = list.SelectedItems.Cast<ColumnItem>().Select(x => x.Column).ToArray();
        if (TopLevel.GetTopLevel(this)?.Clipboard is { } clipboard) await clipboard.SetTextAsync(JsonSerializer.Serialize(selected));
    }
    private async Task Paste()
    {
        if (TopLevel.GetTopLevel(this)?.Clipboard is not { } clipboard || await clipboard.TryGetTextAsync() is not { } text) return;
        foreach (var column in JsonSerializer.Deserialize<DBColumn[]>(text, ServerConnection.Json) ?? [])
        {
            if (columns.Any(x => string.Equals(x.Name, column.Name, StringComparison.OrdinalIgnoreCase))) continue;
            column.id = null; column.TableID = null; columns.Add(column);
        }
    }
}
