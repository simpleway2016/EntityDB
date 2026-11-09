using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Layout;

namespace EntityDbClient.Views;

public sealed class IndexEditor : UserControl
{
    public IndexEditor(ObservableCollection<IndexDesign> indexes, Func<string[]> columnNames)
    {
        var list = new ListBox { Name = "IndexList", ItemsSource = indexes };
        list.ItemTemplate = new FuncDataTemplate<IndexDesign>((index, _) => index is null ? null : new IndexRow(index, columnNames));
        Task Add() { var item = new IndexDesign(); indexes.Add(item); list.SelectedItem = item; return Task.CompletedTask; }
        Task Remove() { if (list.SelectedItem is IndexDesign item) indexes.Remove(item); return Task.CompletedTask; }
        var toolbar = Ui.Row(Ui.Button("添加索引", Add), Ui.Button("移除索引", Remove));
        var dock = new DockPanel(); DockPanel.SetDock(toolbar, Dock.Top); dock.Children.Add(toolbar);
        var note = Ui.Muted("按索引顺序选择字段；末尾自动出现下一格。选择空项可移除该字段。", true); note.Margin = new Thickness(0, 0, 0, 10);
        DockPanel.SetDock(note, Dock.Top); dock.Children.Add(note); dock.Children.Add(list); Content = dock;
        list.ContextMenu = Ui.ContextMenu(this, ("添加索引", Add), ("移除索引", Remove));
    }
}

public sealed class IndexRow : UserControl
{
    private readonly IndexDesign index;
    private readonly Func<string[]> columnNames;
    private readonly WrapPanel fields = new();
    private bool rebuilding;
    public IndexRow(IndexDesign index, Func<string[]> columnNames)
    {
        this.index = index; this.columnNames = columnNames;
        var unique = new ComboBox { Name = "IndexUniqueness", ItemsSource = new[] { "唯一值", "非唯一值" }, SelectedIndex = index.IsUnique ? 0 : 1, Width = 112 };
        var clustered = new ComboBox { Name = "IndexClustering", ItemsSource = new[] { "非聚集", "聚集" }, SelectedIndex = index.IsClustered ? 1 : 0, Width = 105 };
        unique.SelectionChanged += (_, _) => index.IsUnique = unique.SelectedIndex == 0;
        clustered.SelectionChanged += (_, _) => index.IsClustered = clustered.SelectedIndex == 1;
        var root = new StackPanel { Spacing = 8, Margin = new Thickness(8) };
        root.Children.Add(Ui.Row(unique, clustered)); root.Children.Add(fields); Content = root;
        AttachedToVisualTree += (_, _) => { index.PropertyChanged += OnChanged; RebuildFields(); };
        DetachedFromVisualTree += (_, _) => index.PropertyChanged -= OnChanged;
        RebuildFields();
    }
    private void OnChanged(object? sender, PropertyChangedEventArgs args) { if (args.PropertyName == nameof(IndexDesign.Keys)) RebuildFields(); }
    private void RebuildFields()
    {
        rebuilding = true; fields.Children.Clear();
        var names = index.ColumnNames;
        for (int slot = 0; slot <= names.Length; slot++)
        {
            int position = slot;
            var current = slot < names.Length ? names[slot] : "";
            var combo = new ComboBox { Name = "IndexColumn_" + slot, Width = 170, Margin = new Thickness(0, 0, 8, 8), PlaceholderText = "选择字段…" };
            void Fill()
            {
                var selected = combo.SelectedItem?.ToString() ?? current;
                rebuilding = true;
                combo.ItemsSource = new[] { "" }.Concat(columnNames().Where(x => x == selected || !index.ColumnNames.Contains(x))).Append(selected).Distinct(StringComparer.Ordinal).ToArray();
                combo.SelectedItem = selected; rebuilding = false;
            }
            Fill();
            combo.DropDownOpened += (_, _) => Fill();
            combo.SelectionChanged += (_, _) =>
            {
                if (rebuilding || combo.SelectedItem is not string selected) return;
                var ordered = index.ColumnNames.ToList();
                if (position == ordered.Count) { if (selected.Length == 0) return; ordered.Add(selected); }
                else if (selected.Length == 0) ordered.RemoveAt(position);
                else ordered[position] = selected;
                index.Keys = string.Join(',', ordered.Distinct(StringComparer.Ordinal));
            };
            fields.Children.Add(combo);
        }
        rebuilding = false;
    }
}
