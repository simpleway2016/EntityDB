using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Input;
using Avalonia.Media;
using System.Collections.ObjectModel;
using EntityDbClient.Models;
using EntityDbClient.Services;

namespace EntityDbClient.Views;

public sealed class ModuleView : UserControl, IUnsavedChanges
{
    private readonly ClientWorkspace workspace;
    private readonly string id;
    private readonly DBModule module;
    private readonly Func<DBTable, bool, Func<DBTable, Task>?, Task> openDesigner;
    private readonly Func<DBTable, Task> openData;
    private Point menuLocation = new(40, 40);
    private readonly Canvas canvas = new() { Width = 4000, Height = 3000, Background = Brushes.WhiteSmoke };
    private readonly ComboBox tables = new()
    {
        Width = 300, PlaceholderText = "选择要添加的数据表",
        // Avalonia also builds this template for the empty selection during layout.
        ItemTemplate = new FuncDataTemplate<DBTable>((table, _) => table is null ? null : Ui.Label(table.Name + " " + table.caption))
    };
    private readonly List<TableInModule> positions = [];
    private readonly ObservableCollection<DBTable> availableTables = [];
    private readonly Dictionary<TableInModule, (Border Card, TextBlock Title, StackPanel Panel)> cards = [];
    private readonly Dictionary<int, (int? X, int? Y)> savedPositions = [];
    private readonly TextBlock layoutStatus = Ui.Muted("");
    public bool HasUnsavedChanges => positions.Any(p => p.id is int itemId &&
        (!savedPositions.TryGetValue(itemId, out var saved) || saved != (p.x, p.y)));
    public ModuleView(ClientWorkspace workspace, string id, DBModule module,
        Func<DBTable, bool, Func<DBTable, Task>?, Task> openDesigner, Func<DBTable, Task> openData)
    {
        this.workspace = workspace; this.id = id; this.module = module;
        this.openDesigner = openDesigner; this.openData = openData;
        tables.ItemsSource = availableTables;
        var dock = new DockPanel { Margin = new Thickness(12) };
        var toolbar = Ui.Row(tables, Ui.Button("添加到模块", AddAsync), Ui.Button("保存布局", SaveAsync), Ui.Button("刷新", LoadAsync), layoutStatus);
        DockPanel.SetDock(toolbar, Dock.Top); dock.Children.Add(toolbar);
        dock.Children.Add(new ScrollViewer { Content = canvas, HorizontalScrollBarVisibility = Avalonia.Controls.Primitives.ScrollBarVisibility.Auto }); Content = dock;
        canvas.PointerPressed += (_, args) =>
        {
            if (args.GetCurrentPoint(canvas).Properties.IsRightButtonPressed) menuLocation = args.GetPosition(canvas);
        };
    }
    public async Task LoadAsync()
    {
        var all = await workspace.Get(id).InvokeAsync<DBTable[]>("GetTableList", module.DatabaseID);
        var items = await workspace.Get(id).InvokeAsync<TableInModule[]>("GetTablesInModule", module.id);
        var loadedCards = new List<(TableInModule Position, DBTable Table, DBColumn[] Columns)>();
        foreach (var position in items)
        {
            var table = all.FirstOrDefault(x => x.id == position.TableID);
            if (table == null) continue;
            loadedCards.Add((position, table, await workspace.Get(id).InvokeAsync<DBColumn[]>("GetColumns", table.id)));
        }
        availableTables.Clear();
        foreach (var table in all) availableTables.Add(table);
        BuildCanvasMenu();
        positions.Clear(); canvas.Children.Clear(); savedPositions.Clear(); cards.Clear();
        foreach (var (position, table, columns) in loadedCards) AddCard(position, table, columns);
    }
    private void BuildCanvasMenu()
    {
        canvas.ContextMenu = new ContextMenu { ItemsSource = new Control[]
        {
            Ui.MenuItem("新建数据表…", NewTableAsync, this),
            new MenuItem { Header = "添加已有数据表", ItemsSource = availableTables.Select(table => Ui.MenuItem(table.Name, () => AddAtAsync(table, menuLocation), this)).ToArray() },
            new Separator(), Ui.MenuItem("保存布局", SaveAsync, this), Ui.MenuItem("刷新", LoadAsync, this)
        }};
    }
    private void AddCard(TableInModule position, DBTable table, DBColumn[] columns)
    {
        var title = new TextBlock { Text = table.Name + " " + table.caption, Foreground = Brushes.White };
        var header = new Border { Background = Brushes.SteelBlue, Padding = new Thickness(10), Child = title };
        var panel = new StackPanel { Spacing = 4, Children = { header } };
        panel.Children.Add(BuildColumns(columns));
        var card = new Border { Child = panel, MinWidth = 400, MaxWidth = 1100, Background = Brushes.White, BorderBrush = Brushes.LightGray, BorderThickness = new Thickness(1) };
        card.DoubleTapped += async (_, args) =>
        {
            args.Handled = true;
            try { await openDesigner(table, false, null); }
            catch (OperationCanceledException) { }
            catch (Exception ex) { await Ui.Message(Ui.Owner(this), ex.Message); }
        };
        card.ContextMenu = new ContextMenu { ItemsSource = new Control[]
        {
            Ui.MenuItem("查看数据…", () => openData(table), this), new Separator(),
            Ui.MenuItem("从模块移除…", async () =>
            {
                if (!await Ui.Confirm(Ui.Owner(this), "仅从当前模块移除此表？数据库中的表和数据将保留。")) return;
                await workspace.InvokeAsync(id, "RemoveTableFromModule", position.id, table.id);
                RemovePosition(position);
            }, this),
            new Separator(), Ui.MenuItem("设计数据表…", () => openDesigner(table, false, null), this)
        }};
        Canvas.SetLeft(card, position.x ?? 30); Canvas.SetTop(card, position.y ?? 30); canvas.Children.Add(card);
        positions.Add(position);
        cards[position] = (card, title, panel);
        if (position.id is int itemId) savedPositions[itemId] = (position.x, position.y);
        Point? origin = null; double left = 0, top = 0;
        header.PointerPressed += (_, e) => { if (!e.GetCurrentPoint(header).Properties.IsLeftButtonPressed) return; origin = e.GetPosition(canvas); left = Canvas.GetLeft(card); top = Canvas.GetTop(card); e.Pointer.Capture(header); e.Handled = true; };
        header.PointerMoved += (_, e) =>
        {
            if (origin == null) return;
            var offset = e.GetPosition(canvas) - origin.Value;
            position.x = (int)Math.Max(0, left + offset.X); position.y = (int)Math.Max(0, top + offset.Y);
            Canvas.SetLeft(card, position.x.Value); Canvas.SetTop(card, position.y.Value);
        };
        header.PointerReleased += (_, e) => { origin = null; e.Pointer.Capture(null); };
        header.PointerCaptureLost += (_, _) => origin = null;
    }
    private void RemovePosition(TableInModule position)
    {
        if (cards.Remove(position, out var card)) canvas.Children.Remove(card.Card);
        positions.Remove(position);
        if (position.id is int itemId) savedPositions.Remove(itemId);
    }
    public void UpdateTable(DBTable table, DBColumn[] columns)
    {
        if (table.DatabaseID != module.DatabaseID) return;
        var existing = availableTables.FirstOrDefault(x => x.id == table.id);
        if (existing == null) availableTables.Add(table);
        else
        {
            existing.Name = table.Name;
            existing.caption = table.caption;
            existing.iLock = table.iLock;
            // Refresh the combo box template while keeping the object used by existing card actions.
            availableTables[availableTables.IndexOf(existing)] = existing;
        }
        foreach (var position in positions.Where(x => x.TableID == table.id))
        {
            var card = cards[position];
            card.Title.Text = table.Name + " " + table.caption;
            card.Panel.Children[1] = BuildColumns(columns);
        }
        BuildCanvasMenu();
    }
    public void RemoveTable(int databaseId, string? name)
    {
        if (databaseId != module.DatabaseID || availableTables.FirstOrDefault(x => x.Name == name) is not { } table) return;
        foreach (var position in positions.Where(x => x.TableID == table.id).ToArray()) RemovePosition(position);
        availableTables.Remove(table);
        BuildCanvasMenu();
    }
    private static Control BuildColumns(DBColumn[] columns)
    {
        var grid = new Grid { Name = "ModuleTableColumns", ColumnDefinitions = new ColumnDefinitions("Auto,Auto,Auto,Auto"), Margin = new Thickness(0, 2, 0, 6) };
        grid.ColumnDefinitions[0].MinWidth = 150; grid.ColumnDefinitions[1].MinWidth = 120;
        grid.ColumnDefinitions[2].MinWidth = 80; grid.ColumnDefinitions[3].MinWidth = 48;
        for (int row = 0; row < columns.Length; row++)
        {
            var column = columns[row]; grid.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
            var fullCaption = column.caption ?? "";
            var firstLine = fullCaption.Split('\n')[0].TrimEnd('\r');
            var enumText = column.EnumDefine ?? "";
            var cells = new[]
            {
                new TextBlock { Name = "ColumnCaption", Text = firstLine + (fullCaption.Contains('\n') || enumText.Length > 0 ? "…" : ""), MaxWidth = 230 },
                new TextBlock { Name = "ColumnName", Text = (column.IsPKID == true ? "◆ " : "") + (string.IsNullOrEmpty(column.ClassName) ? "" : column.ClassName + ".") + column.Name, MaxWidth = 280 },
                new TextBlock { Text = column.dbType + (column.CanNull == true ? "?" : ""), MaxWidth = 150 },
                new TextBlock { Text = column.length, MaxWidth = 90 }
            };
            ToolTip.SetTip(cells[0], fullCaption + (enumText.Length == 0 ? "" : "\n\n" + enumText));
            for (int cell = 0; cell < cells.Length; cell++)
            {
                cells[cell].Margin = new Thickness(10, 3); cells[cell].TextTrimming = TextTrimming.CharacterEllipsis;
                Grid.SetRow(cells[cell], row); Grid.SetColumn(cells[cell], cell); grid.Children.Add(cells[cell]);
            }
        }
        return grid;
    }
    private async Task AddAsync()
    {
        if (tables.SelectedItem is not DBTable table) return;
        await AddAtAsync(table, new Point(40 + positions.Count * 30, 40 + positions.Count * 30));
    }
    private async Task AddAtAsync(DBTable table, Point location)
    {
        var columns = await workspace.Get(id).InvokeAsync<DBColumn[]>("GetColumns", table.id);
        var position = new TableInModule { ModuleID = module.id, TableID = table.id, x = (int)location.X, y = (int)location.Y };
        var result = await workspace.InvokeAsync(id, "UpdateTableInMoudle", position);
        if (result?.GetValue<int>() is not > 0) throw new InvalidOperationException("服务器未将数据表添加到模块。");
        position.id = result.GetValue<int>();
        UpdateTable(table, columns);
        AddCard(position, availableTables.First(x => x.id == table.id), columns);
    }
    private Task NewTableAsync()
    {
        var location = menuLocation;
        return openDesigner(new DBTable { DatabaseID = module.DatabaseID }, true, table => AddAtAsync(table, location));
    }
    private async Task SaveAsync()
    {
        var changed = positions.Where(p => p.id is int itemId && (!savedPositions.TryGetValue(itemId, out var saved) || saved != (p.x, p.y))).ToArray();
        foreach (var position in changed)
        {
            var result = await workspace.InvokeAsync(id, "UpdateTableInMoudle", position);
            if (result?.GetValue<int>() is not > 0) throw new InvalidOperationException("服务器未保存此布局，请确认当前用户有修改权限。");
            savedPositions[position.id!.Value] = (position.x, position.y);
        }
        layoutStatus.Text = changed.Length == 0 ? "布局没有变化" : $"布局已保存 · {changed.Length} 张表";
    }
}
