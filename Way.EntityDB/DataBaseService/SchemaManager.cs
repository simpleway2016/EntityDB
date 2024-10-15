using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;
using Way.EntityDB.Attributes;

namespace Way.EntityDB.DataBaseService
{
    class SchemaManager
    {
        static System.Collections.Concurrent.ConcurrentDictionary<Type, SchemaTable> Dict = new System.Collections.Concurrent.ConcurrentDictionary<Type, SchemaTable>();
         public static SchemaTable GetSchemaTable(Type tableType)
        {
            SchemaTable ret;
            if (Dict.TryGetValue(tableType , out ret ))
            {
                return ret;
            }
            else
            {
                ret = new SchemaTable();
                var wayAttr = tableType.GetCustomAttribute<Way.EntityDB.Attributes.TableConfigAttribute>();
                if (wayAttr == null)
                    return null;

                var tableAttr = tableType.GetCustomAttribute<TableAttribute>();
                if (tableAttr == null)
                    return null;
                ret.TableName = tableAttr.Name;
                ret.AutoSetPropertyNameOnInsert = wayAttr.AutoSetPropertyNameOnInsert;
                ret.AutoSetPropertyValueOnInsert = wayAttr.AutoSetPropertyValueOnInsert;

                var proInfos = tableType.GetProperties();
                foreach( var pro in proInfos )
                {
                    var columnAttr = pro.GetCustomAttribute<ColumnAttribute>();
                    var design_columnAttr = pro.GetCustomAttribute<DesignColumnAttribute>();
                    if (columnAttr != null)
                    {
                        var column = new SchemaColumn();
                        column.Name = columnAttr.Name;
                        column.TypeName = design_columnAttr?.TypeName;
                        column.IsKey = pro.GetCustomAttribute<KeyAttribute>() != null;
                        if (column.IsKey && ret.KeyColumn == null)
                            ret.KeyColumn = column;

                        var attr = pro.GetCustomAttribute<DatabaseGeneratedAttribute>();
                        if (attr != null && attr.DatabaseGeneratedOption != DatabaseGeneratedOption.None)
                            column.IsDatabaseGenerated = true;

                        var displayAttr = pro.GetCustomAttribute<DisplayAttribute>();
                        if (displayAttr != null)
                            column.Display = displayAttr.Name;
                        column.PropertyName = pro.Name;
                        column.PropertyInfo = pro;
                        ret.Columns.Add(column);
                    }
                }

                Dict.TryAdd(tableType, ret);
                return Dict[tableType];
            }
        }
    }

    class SchemaTable
    {
        public string TableName;
        public string AutoSetPropertyNameOnInsert;
        public object AutoSetPropertyValueOnInsert;
        public SchemaColumn KeyColumn;
        public List<SchemaColumn> Columns = new List<SchemaColumn>();
    }
    class SchemaColumn
    {
        public PropertyInfo PropertyInfo;
        public string PropertyName;
        public string Name;
        public string Display;
        public string TypeName;
        public bool IsKey;
        /// <summary>
        /// 自增长
        /// </summary>
        public bool IsDatabaseGenerated;
    }
}
