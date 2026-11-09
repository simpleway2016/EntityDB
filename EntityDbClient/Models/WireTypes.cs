namespace EntityDbClient.Models;

// Additional payload shapes advertised to Agents by /api/capabilities.
public sealed class IndexInfo
{
    public bool IsUnique { get; set; }
    public bool IsClustered { get; set; }
    public string[] ColumnNames { get; set; } = [];
    public string? Name { get; set; }
}
public sealed class TableInfo
{
    public string TableName { get; set; } = "";
    public DBColumn[] Columns { get; set; } = [];
    public IndexInfo[] Indexes { get; set; } = [];
}
public sealed class WayDataTable
{
    public string TableName { get; set; } = "";
    public WayDataColumn[] Columns { get; set; } = [];
    public WayDataRow[] Rows { get; set; } = [];
}
public sealed class WayDataColumn
{
    public string ColumnName { get; set; } = "";
    public string DataType { get; set; } = "System.String";
}
public sealed class WayDataRow
{
    public int RowState { get; set; } = 2;
    public ItemPair[] Items { get; set; } = [];
}
public sealed class ItemPair
{
    public string Name { get; set; } = "";
    public object? Value { get; set; }
}
