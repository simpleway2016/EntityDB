using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using EntityDbClient.Models;
using EntityDbClient.Services;

namespace EntityDbClient.Views;

public sealed class EditableDataRow(string[] values, int state = 2, JsonNode?[]? originals = null) : INotifyPropertyChanged
{
    private readonly string[] values = values;
    private readonly string[] originalText = (string[])values.Clone();
    private readonly JsonNode?[] originals = originals ?? new JsonNode?[values.Length];
    private readonly HashSet<int> edited = [];
    public int State { get; private set; } = state;
    public string this[int index]
    {
        get => values[index];
        set
        {
            if (values[index] == value) return;
            values[index] = value;
            if (value == originalText[index]) edited.Remove(index); else edited.Add(index);
            if (State != 4) State = edited.Count == 0 ? 2 : 16;
            PropertyChanged?.Invoke(this, new("Item[]"));
        }
    }
    public void AcceptChanges(Func<int, JsonNode?> getValue)
    {
        for (int i = 0; i < values.Length; i++) { originals[i] = getValue(i); originalText[i] = values[i]; }
        edited.Clear(); State = 2;
    }
    public JsonNode? GetWireValue(int index, string type) => edited.Contains(index) ? DataView.ToValue(values[index], type) : originals[index]?.DeepClone();
    public event PropertyChangedEventHandler? PropertyChanged;
}

public sealed class DataView : UserControl, IUnsavedChanges
{
    private readonly ClientWorkspace workspace;
    private readonly string id;
    private readonly DBTable table;
    private readonly ObservableCollection<EditableDataRow> rows = [];
    private readonly List<string> deleted = [];
    private readonly DataGrid grid;
    private readonly TextBox sql = new() { AcceptsReturn = true, Height = 74 };
    private readonly TextBlock status = Ui.Label("");
    private readonly NumericUpDown pageSize = new() { Minimum = 1, Maximum = 10000, Value = 100, Width = 110 };
    private JsonArray wireColumns = [];
    private DBColumn[] schema = [];
    private int page;
    private int total;
    private int pkIndex = -1;
    private bool canEdit;
    public bool HasUnsavedChanges { get { Commit(); return rows.Any(x => x.State != 2) || deleted.Count > 0; } }
    public DataView(ClientWorkspace workspace, string id, DBTable table)
    {
        this.workspace = workspace; this.id = id; this.table = table;
        grid = Ui.Grid(rows); grid.IsReadOnly = false;
        var dock = new DockPanel { Margin = new Thickness(18, 14) };
        var commands = new Grid { ColumnDefinitions = new ColumnDefinitions("*,Auto"), Margin = new Thickness(0, 0, 0, 12) };
        commands.Children.Add(new TextBlock { Text = "数据查询", FontSize = 18, FontWeight = Avalonia.Media.FontWeight.SemiBold, VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center });
        var writes = Ui.Row(Ui.Button("添加行", AddAsync), Ui.Button("删除行", DeleteAsync), Ui.PrimaryButton("保存数据", SaveAsync));
        writes.Margin = new Thickness(0); Grid.SetColumn(writes, 1); commands.Children.Add(writes);
        sql.FontFamily = new Avalonia.Media.FontFamily("Consolas, monospace");
        var query = new Grid { ColumnDefinitions = new ColumnDefinitions("*,Auto"), ColumnSpacing = 10 };
        query.Children.Add(sql);
        var execute = Ui.Button("执行查询", async () => { if (await CanReload()) { page = 0; await LoadAsync(); } });
        Grid.SetColumn(execute, 1); query.Children.Add(execute);
        var header = new StackPanel { Spacing = 8, Margin = new Thickness(0, 0, 0, 12), Children =
        {
            commands, query, Ui.Muted("双击单元格编辑 · 输入 NULL 表示空值 · 修改后点击保存数据")
        }};
        DockPanel.SetDock(header, Dock.Top); dock.Children.Add(header);
        var footer = new Grid { ColumnDefinitions = new ColumnDefinitions("*,Auto"), Margin = new Thickness(0, 10, 0, 0) };
        status.FontSize = 12; status.TextTrimming = Avalonia.Media.TextTrimming.CharacterEllipsis; footer.Children.Add(status);
        pageSize.Width = 84;
        var paging = Ui.Row(Ui.Button("上一页", async () => { if (page > 0 && await CanReload()) { page--; await LoadAsync(); } }),
            Ui.Button("下一页", async () => { if ((page + 1) * (int)pageSize.Value! < total && await CanReload()) { page++; await LoadAsync(); } }), Ui.Muted("每页"), pageSize);
        paging.Margin = new Thickness(0); Grid.SetColumn(paging, 1); footer.Children.Add(paging);
        DockPanel.SetDock(footer, Dock.Bottom); dock.Children.Add(footer); dock.Children.Add(grid); Content = dock;
        grid.ContextMenu = Ui.ContextMenu(this,
            ("添加行", AddAsync), ("删除所选行…", DeleteAsync), ("保存数据…", SaveAsync),
            ("重新查询", async () => { if (await CanReload()) await LoadAsync(); }));
    }
    private void Commit() { grid.CommitEdit(DataGridEditingUnit.Cell, true); grid.CommitEdit(DataGridEditingUnit.Row, true); }
    private async Task<bool> CanReload()
    {
        return !HasUnsavedChanges || await Ui.Confirm(Ui.Owner(this), "丢弃当前未保存的数据编辑并重新查询？");
    }
    public async Task LoadAsync()
    {
        schema = await workspace.Get(id).InvokeAsync<DBColumn[]>("GetColumnList", table.id);
        if (string.IsNullOrWhiteSpace(sql.Text))
        {
            var format = await workspace.Get(id).InvokeAsync<string>("GetObjectFormat", table.id);
            sql.Text = "select * from " + string.Format(format, table.Name);
        }
        var result = await workspace.Get(id).InvokeAsync<JsonObject>("GetDataTable", sql.Text, table.id, page, (int)pageSize.Value!);
        wireColumns = result["Columns"]!.AsArray();
        total = int.TryParse(result["TableName"]?.ToString(), out var count) ? count : 0;
        rows.Clear(); deleted.Clear(); grid.Columns.Clear(); pkIndex = -1;
        var pk = schema.FirstOrDefault(x => x.IsPKID == true)?.Name;
        for (int i = 0; i < wireColumns.Count; i++)
        {
            var name = wireColumns[i]!["ColumnName"]!.ToString();
            if (string.Equals(name, pk, StringComparison.OrdinalIgnoreCase)) pkIndex = i;
            var definition = schema.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
            grid.Columns.Add(new DataGridTextColumn { Header = name, Binding = new Binding($"[{i}]") { Mode = BindingMode.TwoWay },
                IsReadOnly = definition == null || definition.IsAutoIncrement == true || definition.IsPKID == true, Width = new DataGridLength(150) });
        }
        foreach (var wireRow in result["Rows"]!.AsArray())
        {
            var items = wireRow!["Items"]!.AsArray();
            var originalValues = wireColumns.Select(c => items.FirstOrDefault(x => string.Equals(x?["Name"]?.ToString(), c!["ColumnName"]!.ToString(), StringComparison.OrdinalIgnoreCase))?["Value"]?.DeepClone()).ToArray();
            rows.Add(new EditableDataRow(originalValues.Select(x => x?.ToString() ?? "NULL").ToArray(), 2, originalValues));
        }
        canEdit = pkIndex >= 0 && schema.All(column => wireColumns.Any(c => string.Equals(c?["ColumnName"]?.ToString(), column.Name, StringComparison.OrdinalIgnoreCase)));
        grid.IsReadOnly = !canEdit;
        status.Text = $"第 {page + 1} 页 · 共 {total} 行 · 当前 {rows.Count} 行";
        if (!canEdit) status.Text += " · 只读结果（编辑需要包含表的全部字段和主键）";
    }
    private async Task AddAsync()
    {
        RequireEditable();
        var values = Enumerable.Repeat("NULL", wireColumns.Count).ToArray();
        var row = new EditableDataRow(values, 4);
        if (schema.Any(x => x.IsPKID == true && x.IsAutoIncrement != true))
        {
            var key = await Ui.Prompt(Ui.Owner(this), "新行主键值");
            if (key == null) return;
            row[pkIndex] = key;
        }
        rows.Add(row); grid.SelectedItem = row;
    }
    private void RequireEditable() { if (!canEdit) throw new InvalidOperationException("请先查询包含此表全部字段及主键的结果，再编辑数据。"); }
    private async Task DeleteAsync()
    {
        if (grid.SelectedItem is not EditableDataRow row) return;
        RequireEditable();
        if (row.State != 4 && pkIndex < 0) throw new InvalidOperationException("查询结果缺少主键，无法删除。");
        if (!await Ui.Confirm(Ui.Owner(this), "删除所选行？点击保存后才会提交。")) return;
        if (row.State != 4) deleted.Add(row[pkIndex]); rows.Remove(row);
    }
    private async Task SaveAsync()
    {
        Commit();
        RequireEditable();
        var changed = rows.Where(x => x.State != 2).ToArray();
        if (changed.Length == 0 && deleted.Count == 0) return;
        if (pkIndex < 0) throw new InvalidOperationException("查询结果缺少主键，无法保存。");
        var wireRows = new JsonArray();
        foreach (var row in changed)
        {
            var items = new JsonArray();
            for (int i = 0; i < wireColumns.Count; i++)
                items.Add(new JsonObject { ["Name"] = wireColumns[i]!["ColumnName"]!.ToString(), ["Value"] = row.GetWireValue(i, wireColumns[i]!["DataType"]!.ToString()) });
            wireRows.Add(new JsonObject { ["RowState"] = row.State, ["Items"] = items });
        }
        if (!await Ui.Confirm(Ui.Owner(this), $"向 {workspace.Get(id).Url} 保存 {changed.Length} 行修改，删除 {deleted.Count} 行？")) return;
        await workspace.InvokeAsync(id, "SaveDataTable", new JsonObject { ["TableName"] = table.Name, ["Columns"] = wireColumns.DeepClone(), ["Rows"] = wireRows }, table.id, deleted);
        foreach (var row in changed) row.AcceptChanges(i => row.GetWireValue(i, wireColumns[i]!["DataType"]!.ToString()));
        deleted.Clear();
        await LoadAsync(); status.Text += " · 已保存";
    }
    public static JsonNode? ToValue(string text, string type)
    {
        if (text == "NULL") return null;
        object value = type switch
        {
            "System.Int16" => short.Parse(text, CultureInfo.InvariantCulture), "System.Int32" => int.Parse(text, CultureInfo.InvariantCulture),
            "System.Int64" => long.Parse(text, CultureInfo.InvariantCulture), "System.Byte" => byte.Parse(text, CultureInfo.InvariantCulture),
            "System.Decimal" => decimal.Parse(text, CultureInfo.InvariantCulture), "System.Double" => double.Parse(text, CultureInfo.InvariantCulture),
            "System.Single" => float.Parse(text, CultureInfo.InvariantCulture), "System.Boolean" => bool.Parse(text),
            "System.DateTime" => DateTime.Parse(text, CultureInfo.InvariantCulture), "System.Byte[]" => Convert.FromBase64String(text),
            _ => text
        };
        return JsonSerializer.SerializeToNode(value);
    }
}
