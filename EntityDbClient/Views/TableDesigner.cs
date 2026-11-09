using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Avalonia;
using Avalonia.Controls;
using EntityDbClient.Models;
using EntityDbClient.Services;

namespace EntityDbClient.Views;

public sealed class IndexDesign : INotifyPropertyChanged
{
    private string keys = "";
    public string Keys { get => keys; set { if (keys == value) return; keys = value; PropertyChanged?.Invoke(this, new(nameof(Keys))); } }
    [JsonIgnore] public string[] ColumnNames => (Keys ?? "").Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    public bool IsUnique { get; set; }
    public bool IsClustered { get; set; }
    public event PropertyChangedEventHandler? PropertyChanged;
}

public sealed class TableDesigner : UserControl, IUnsavedChanges
{
    private readonly ClientWorkspace workspace;
    private readonly string connectionId;
    private DBTable table;
    private bool create;
    private string? schemaHash;
    private readonly Func<DBTable, Task>? onCreated;
    private readonly TextBox name;
    private readonly TextBox caption;
    private readonly ObservableCollection<DBColumn> columns = [];
    private readonly ObservableCollection<IndexDesign> indexes = [];
    private readonly ObservableCollection<ClassProperty> properties = [];
    private readonly TextBlock status = Ui.Label("编辑完成后点击保存，将修改服务器数据库。");
    private readonly TabControl sections = new() { Classes = { "editor-tabs" } };
    private readonly ColumnEditor columnEditor;
    private string savedState = "";
    private string CurrentState() => JsonSerializer.Serialize(new { Name = name.Text ?? "", Caption = caption.Text ?? "", Columns = columns, Indexes = indexes, Properties = properties });
    public bool HasUnsavedChanges => savedState != CurrentState() || columnEditor.HasPendingChanges;
    public TableDesigner(ClientWorkspace workspace, string connectionId, DBTable table, bool create, Func<DBTable, Task>? onCreated = null)
    {
        this.workspace = workspace; this.connectionId = connectionId; this.table = Ui.Clone(table); this.create = create;
        this.onCreated = onCreated;
        name = Ui.Text(table.Name); caption = Ui.Text(table.caption);
        var dock = new DockPanel { Margin = new Thickness(18, 14) };
        var commands = new Grid { ColumnDefinitions = new ColumnDefinitions("*,Auto"), Margin = new Thickness(0, 0, 0, 16) };
        commands.Children.Add(new TextBlock { Text = create ? "新建数据表" : "表结构设计", FontSize = 18, FontWeight = Avalonia.Media.FontWeight.SemiBold, VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center });
        var buttons = Ui.Row(Ui.Button("重新加载", async () =>
        {
            if (!HasUnsavedChanges || await Ui.Confirm(Ui.Owner(this), "重新加载会丢弃此编辑页的未保存修改，是否继续？")) await LoadAsync();
        }), Ui.Button("DTO 代码", ShowDtoAsync), Ui.PrimaryButton("保存表结构", SaveAsync));
        buttons.Margin = new Thickness(0); Grid.SetColumn(buttons, 1); commands.Children.Add(buttons);
        var fields = new Grid { ColumnDefinitions = new ColumnDefinitions("*,*"), ColumnSpacing = 20, Margin = new Thickness(0, 0, 0, 18) };
        fields.Children.Add(Ui.Field("表名", name)); var captionField = Ui.Field("说明", caption); Grid.SetColumn(captionField, 1); fields.Children.Add(captionField);
        var header = new StackPanel { Children = { commands, fields } };
        DockPanel.SetDock(header, Dock.Top); dock.Children.Add(header);
        status.FontSize = 12; status.Foreground = Avalonia.Media.Brush.Parse("#68758A"); status.Margin = new Thickness(0, 10, 0, 0);
        DockPanel.SetDock(status, Dock.Bottom); dock.Children.Add(status);
        dock.Children.Add(sections); Content = dock;
        columnEditor = new ColumnEditor(columns, indexes);
        sections.ItemsSource = new[]
        {
            new TabItem { Header = "字段", Content = columnEditor },
            new TabItem { Header = "索引", Content = new IndexEditor(indexes, () => columns.Select(x => x.Name).ToArray()) },
            new TabItem { Header = "导航属性", Content = Ui.ListEditor(properties, () => new ClassProperty(), [("name","属性名"),("desc","说明"),("foreignkey_tableid","关联表 ID"),("foreignkey_columnid","外键字段 ID"),("iscollection","集合属性")]) }
        };
        if (create) columns.Add(new DBColumn { Name = "id", dbType = "int", length = "", IsPKID = true, IsAutoIncrement = true, CanNull = false });
        savedState = CurrentState();
    }

    public async Task LoadAsync(bool refreshTable = true)
    {
        if (create) return;
        var info = await workspace.ReadTableAsync(connectionId, table.id!.Value);
        var loadedColumns = info["Columns"]!.Deserialize<DBColumn[]>(ServerConnection.Json)!;
        var loadedIndexes = info["IdxIndexes"]!.Deserialize<IndexDesign[]>(ServerConnection.Json)!;
        var loadedProperties = info["ClassProperties"]!.Deserialize<ClassProperty[]>(ServerConnection.Json)!;
        if (refreshTable)
        {
            var tables = await workspace.Get(connectionId).InvokeAsync<DBTable[]>("GetTableList", table.DatabaseID);
            table = tables.First(x => x.id == table.id);
        }
        schemaHash = info["schemaHash"]!.ToString();
        columns.Clear(); indexes.Clear(); properties.Clear();
        foreach (var column in loadedColumns) columns.Add(column);
        foreach (var index in loadedIndexes) indexes.Add(index);
        foreach (var property in loadedProperties) properties.Add(property);
        // Refresh name/caption too, since an agent may have renamed the table.
        name.Text = table.Name; caption.Text = table.caption;
        savedState = CurrentState();
        status.Text = "已加载最新表结构。";
    }

    private async Task SaveAsync()
    {
        columnEditor.ValidatePending();
        var editedTable = Ui.Clone(table);
        editedTable.Name = name.Text?.Trim() ?? "";
        editedTable.caption = caption.Text ?? "";
        var design = new JsonObject
        {
            ["table"] = JsonSerializer.SerializeToNode(editedTable), ["columns"] = JsonSerializer.SerializeToNode(columns),
            ["indexes"] = JsonSerializer.SerializeToNode(indexes.Where(x => x.ColumnNames.Length > 0).Select(x => new { x.IsUnique, x.IsClustered, x.ColumnNames })),
            ["classProperties"] = JsonSerializer.SerializeToNode(properties)
        };
        if (!create) design["expectedSchemaHash"] = schemaHash;
        if (!await Ui.Confirm(Ui.Owner(this), $"保存 {editedTable.Name} 的结构到 {workspace.Get(connectionId).Url}？删除字段会影响其中的数据。")) return;
        var result = await workspace.SaveTableAsync(connectionId, design, create);
        var wasCreate = create;
        table = create ? result!.Deserialize<DBTable>(ServerConnection.Json)! : editedTable;
        create = false;
        savedState = CurrentState();
        if (wasCreate && onCreated != null) await onCreated(table);
        await LoadAsync(refreshTable: false);
        status.Text = "表结构已保存。";
    }

    private async Task ShowDtoAsync()
    {
        if (create) throw new InvalidOperationException("请先保存数据表。");
        var code = await workspace.Get(connectionId).InvokeAsync<string>("GetDtoCode", table);
        var window = Ui.Dialog("DTO 代码", 900, 700);
        window.Content = new TextBox { Text = code, IsReadOnly = true, AcceptsReturn = true, FontFamily = new Avalonia.Media.FontFamily("Consolas"), Margin = new Thickness(12) };
        await window.ShowDialog(Ui.Owner(this));
    }
}
