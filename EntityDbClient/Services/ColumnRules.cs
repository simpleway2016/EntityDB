using System.Text.RegularExpressions;
using EntityDbClient.Models;

namespace EntityDbClient.Services;

public static class ColumnRules
{
    public static readonly string[] DatabaseTypes = ["varchar", "int", "image", "text", "mediumtext", "longtext", "smallint", "smalldatetime", "real", "datetime", "datetimezone", "date", "time", "float", "double", "bit", "decimal", "numeric", "bigint", "varbinary", "char", "timestamp", "jsonb"];

    public static string[] EnumNames(string? definition) => string.IsNullOrWhiteSpace(definition) ? [] :
        Regex.Matches(definition, @"\b(\w+)\s*=").Select(m => m.Groups[1].Value).Distinct(StringComparer.Ordinal).Order(StringComparer.Ordinal).ToArray();

    public static void Set(DBColumn column, string property, object? value, IEnumerable<DBColumn> columns)
    {
        switch (property)
        {
            case "Name":
                var name = value?.ToString()?.Trim() ?? "";
                if (name.Length == 0) throw new ArgumentException("字段名称不能为空。");
                if (columns.Any(c => !ReferenceEquals(c, column) && string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase))) throw new ArgumentException("列名与其它列冲突！");
                column.Name = name; return;
            case "caption": column.caption = value?.ToString()?.Trim() ?? ""; return;
            case "IsPKID":
                if (value is true && columns.Any(c => !ReferenceEquals(c, column) && c.IsPKID == true)) throw new ArgumentException("一个表只能有一个主键，您可以另外设置唯一值索引。");
                column.IsPKID = value is true; if (column.IsPKID == true) column.CanNull = false; return;
            case "CanNull":
                if (value is true && column.IsPKID == true) throw new ArgumentException("主键不允许为空！");
                if (value is true && column.IsDiscriminator == true) throw new ArgumentException("鉴别器字段不允许为空。");
                column.CanNull = value is true; return;
            case "dbType":
                var type = value?.ToString() ?? "";
                if (column.dbType == type) return;
                column.dbType = type;
                column.length = type.Contains("char", StringComparison.Ordinal) ? "50" : type.Contains("decimal", StringComparison.Ordinal) ? "18,4" : "";
                return;
            case "EnumDefine":
                var definition = value?.ToString() ?? "";
                if (column.IsDiscriminator == true && definition.Length == 0) throw new ArgumentException("鉴别器字段必须定义 Enum 内容。");
                column.EnumDefine = definition;
                if (definition.Length > 0) { column.dbType = "int"; column.length = ""; }
                return;
            case "IsDiscriminator":
                if (value is true)
                {
                    if (string.IsNullOrWhiteSpace(column.EnumDefine)) throw new ArgumentException("请先定义当前列的 Enum 内容。");
                    if (columns.Any(c => !ReferenceEquals(c, column) && c.IsDiscriminator == true)) throw new ArgumentException("不能设置多个 IsDiscriminator 字段。");
                    column.CanNull = false;
                }
                column.IsDiscriminator = value is true; return;
            default: typeof(DBColumn).GetProperty(property)!.SetValue(column, value); return;
        }
    }

    public static void Validate(IReadOnlyCollection<DBColumn> columns)
    {
        if (columns.Count(c => c.IsPKID == true) > 1) throw new ArgumentException("一个表只能有一个主键，您可以另外设置唯一值索引。");
        if (columns.Any(c => c.IsPKID == true && c.CanNull == true)) throw new ArgumentException("主键不允许为空！");
        if (columns.Count(c => c.IsDiscriminator == true) > 1) throw new ArgumentException("不能设置多个 IsDiscriminator 字段。");
        if (columns.Any(c => c.IsDiscriminator == true && (string.IsNullOrWhiteSpace(c.EnumDefine) || c.CanNull == true))) throw new ArgumentException("鉴别器字段必须定义 Enum 且不允许为空。");
    }
}
