using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Way.EntityDB.Design.Database.SqlServer;

namespace Way.EntityDB.Design.Impls.SqlServer.Handles
{
    class ChangeColumnHandler
    {
        // 辅助：返回带方括号的限定名，例如 "[schema].[table]" 或 "[table]"
        private static string GetQualifiedName(string schema, string name)
        {
            if (string.IsNullOrWhiteSpace(schema))
                return $"[{name}]";
            return $"[{schema}].[{name}]";
        }

        // 辅助：返回用于 OBJECT_ID 等的 schema 前缀，例如 "schema." 或 ""
        private static string GetSchemaDotPrefix(string schema)
        {
            if (string.IsNullOrWhiteSpace(schema))
                return string.Empty;
            return $"{schema}.";
        }

        // 辅助：返回用于 sp_rename 的对象字符串，包含 schema/table/column 的方括号表示
        private static string GetSpRenameColumnObject(string schema, string table, string column)
        {
            if (string.IsNullOrWhiteSpace(schema))
                return $"[{table}].[{column}]";
            return $"[{schema}].[{table}].[{column}]";
        }

        static Action<IDatabaseService, string, string, EJ.DBColumn> CancelPrimaryKey = (database, schema, tablename, column) => {
            // 去除主键（按 schema + table 查找）
            string sql = $@"
DECLARE @NAME SYSNAME
SELECT TOP 1 @NAME = NAME FROM SYS.OBJECTS WITH(NOLOCK)
WHERE TYPE_DESC = 'PRIMARY_KEY_CONSTRAINT'
AND PARENT_OBJECT_ID = OBJECT_ID('{GetSchemaDotPrefix(schema)}{tablename}')
SELECT @NAME
";
            var constraintName = database.ExecSqlString(sql).ToSafeString();
            if (constraintName.IsNullOrEmpty() == false)
            {
                // 如果constraintName没有值，那么就是变更自增长字段的时候，原来字段被删除了
                database.ExecSqlString($"ALTER TABLE {GetQualifiedName(schema, tablename)} DROP CONSTRAINT {constraintName}");
            }

        };

        static Action<IDatabaseService, string, string, EJ.DBColumn> BePrimaryKey = (database, schema, tablename, column) => {
            // 设为主键（带 schema 限定）
            database.ExecSqlString($"alter table {GetQualifiedName(schema, tablename)} add constraint PK_{tablename}_{column.Name.ToLower()} primary key ([{column.Name.ToLower()}])");

        };

        static Action<IDatabaseService, string, string, EJ.DBColumn> Rename = (database, schema, tablename, column) => {
            var changeitem = column.BackupChangedProperties["Name"];
            var oldColumn = changeitem.OriginalValue.ToString();
            var oldObject = GetSpRenameColumnObject(schema, tablename, oldColumn);
            database.ExecSqlString($"EXEC sp_rename '{oldObject}', '{column.Name.ToLower()}', 'COLUMN'");

            column.BackupChangedProperties.Remove("Name");
        };

        static Action<IDatabaseService, string, string, EJ.DBColumn> Delete = (database, schema, tablename, column) => {
            var columnName = column.Name.ToLower();
            if (column.BackupChangedProperties["Name"] != null)
                columnName = column.BackupChangedProperties["Name"].OriginalValue.ToString().ToLower();


            #region delete
            // 使用 sp_help 'schema.table' 形式，兼容 schema 情况
            using (var sp_helpResult = database.SelectDataSet($"sp_help '{GetSchemaDotPrefix(schema)}{tablename}'"))
            {
                foreach (var dtable in sp_helpResult.Tables)
                {
                    if (dtable.Columns.Any(m => m.ColumnName == "constraint_name"))
                    {
                        var query = dtable.Rows.Where(m => m["constraint_keys"].ToSafeString().ToLower() == columnName);
                        if (query.Count() > 0)
                        {
                            database.ExecSqlString($"alter table {GetQualifiedName(schema, tablename)} drop constraint " + query.First()["constraint_name"]);
                        }
                        break;
                    }
                }

                // 删除默认值
                foreach (var dtable in sp_helpResult.Tables)
                {
                    if (dtable.Columns.Any(m => m.ColumnName == "constraint_name"))
                    {
                        var query = from m in dtable.Rows
                                    where ((string)m["constraint_type"]).ToLower().EndsWith(" " + columnName) && ((string)m["constraint_type"]).ToLower().StartsWith("default on ")
                                    select m;

                        if (query.Count() > 0)
                        {
                            database.ExecSqlString($"alter table {GetQualifiedName(schema, tablename)} drop constraint " + query.First()["constraint_name"]);
                        }
                        break;
                    }
                }
            }

            #endregion

            database.ExecSqlString($"ALTER TABLE {GetQualifiedName(schema, tablename)} DROP COLUMN [{columnName}]");
        };

        static Action<IDatabaseService, string, string, EJ.DBColumn> CancelAutoIncrement = (database, schema, tablename, column) => {
            var flagColumnName = "_tempcolumn";
            while (Convert.ToInt32(database.ExecSqlString($"Select count(*) from syscolumns Where  ID=OBJECT_ID('{GetSchemaDotPrefix(schema)}{tablename}') and name='{flagColumnName}'")) > 0)
            {
                flagColumnName += "1";
            }

            var dbtype = SqlServerTableService.GetSqlServerType(column);

            // 去掉自增长
            database.ExecSqlString($"alter table {GetQualifiedName(schema, tablename)} add {flagColumnName.ToLower()} {dbtype}{(column.IsPKID == true ? " not null" : "")}");
            database.ExecSqlString($"update {GetQualifiedName(schema, tablename)} set {flagColumnName.ToLower()}=[{column.Name.ToLower()}]");
            Delete(database, schema, tablename, column);
            var oldObject = GetSpRenameColumnObject(schema, tablename, flagColumnName.ToLower());
            database.ExecSqlString($"EXEC sp_rename '{oldObject}', '{column.Name.ToLower()}', 'COLUMN'");

            // 去掉自增长后，由于原来的列删除了，所以如果原来是主键，必须重新设置为主键
            if (column.IsPKID == true)
            {
                BePrimaryKey(database, schema, tablename, column);
            }
        };

        static Action<IDatabaseService, string, string, EJ.DBColumn> BeAutoIncrement = (database, schema, tablename, column) => {
            var flagColumnName = "_tempcolumn";
            while (Convert.ToInt32(database.ExecSqlString($"Select count(*) from syscolumns Where  ID=OBJECT_ID('{GetSchemaDotPrefix(schema)}{tablename}') and name='{flagColumnName}'")) > 0)
            {
                flagColumnName += "1";
            }

            var dbtype = SqlServerTableService.GetSqlServerType(column);

            // 设为自增长
            database.ExecSqlString($"alter table {GetQualifiedName(schema, tablename)} add {flagColumnName.ToLower()} {dbtype} IDENTITY (1, 1)");
            Delete(database, schema, tablename, column);
            var oldObject = GetSpRenameColumnObject(schema, tablename, flagColumnName.ToLower());
            database.ExecSqlString($"EXEC sp_rename '{oldObject}', '{column.Name.ToLower()}', 'COLUMN'");

            // 去掉自增长后，由于原来的列删除了，所以如果原来是主键，必须重新设置为主键
            if (column.IsPKID == true)
            {
                if( column.BackupChangedProperties.TryGetValue("IsPKID" , out DataValueChangedItem pair) && (bool)pair.OriginalValue == false)
                {
                    column.BackupChangedProperties.Remove("IsPKID");//防止后面再次设置为主键
                }
                // 主键不允许为空
                database.ExecSqlString($"alter table {GetQualifiedName(schema, tablename)} alter column [{column.Name.ToLower()}] [{dbtype}] not null");
                BePrimaryKey(database, schema, tablename, column);
            }
        };

        static Action<IDatabaseService, string, string, EJ.DBColumn> CancelDefaultValue = (database, schema, tablename, column) => {
            // 获取默认值的id（OBJECT_ID 支持 schema.table）
            var defaultSettingID = database.ExecSqlString($"Select cdefault from syscolumns Where  ID=OBJECT_ID('{GetSchemaDotPrefix(schema)}{tablename}') and name='{column.Name.ToLower()}'");
            if (defaultSettingID != null && Convert.ToInt32(defaultSettingID) != 0)
            {
                var defaultKeyName = database.ExecSqlString($"select name from sysObjects where type='D' and id={defaultSettingID}");
                if (defaultKeyName != null)
                {
                    // 如果进到这里，那么表示原来有默认值
                    database.ExecSqlString($"alter table {GetQualifiedName(schema, tablename)} drop constraint {defaultKeyName}");
                }
            }
        };

        static Action<IDatabaseService, string, string, EJ.DBColumn> SetDefaultValue = (database, schema, tablename, column) => {
            string sql = "";
            string defaultValue = column.defaultValue.Trim();
            sql += "alter  table  " + GetQualifiedName(schema, tablename) + "  add  constraint DF_" + tablename + "_" + column.Name.ToLower() + " default '" + defaultValue.Replace("'", "''") + "' for [" + column.Name.ToLower() + "]";


            if (sql.Length > 0)
                database.ExecSqlString(sql);


            database.ExecSqlString($"update {GetQualifiedName(schema, tablename)} set [{column.Name.ToLower()}]='" + defaultValue.Replace("'", "''") + "' where [" + column.Name.ToLower() + "] is null");
        };

        static Action<IDatabaseService, string, string, EJ.DBColumn> Modify = (database, schema, tablename, column) => {
            var dbtype = SqlServerTableService.GetSqlServerType(column);

            string sql = "alter table " + GetQualifiedName(schema, tablename) + " alter column [" + column.Name.ToLower() + "] [" + dbtype + "]";

            if (dbtype.IndexOf("char") >= 0)
            {
                if (!string.IsNullOrEmpty(column.length))
                    sql += " (" + column.length + ")";
                else
                {
                    sql += " (50)";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(column.length))
                {
                    sql += " (" + column.length + ")";
                }
            }

            // 先改变字段类型，下面再设置默认值，和非null
            if (column.CanNull == false || column.IsPKID == true || column.IsAutoIncrement == true)
            {
                database.ExecSqlString(sql + " NOT NULL");
            }
            else
            {
                database.ExecSqlString(sql + " NULL");
            }
            if (column.CanNull == false && !string.IsNullOrEmpty(column.defaultValue))
            {
                string defaultValue = column.defaultValue.Trim();

                database.ExecSqlString($"update {GetQualifiedName(schema, tablename)} set [{column.Name.ToLower()}]='" + defaultValue.Replace("'", "''") + "' where [" + column.Name.ToLower() + "] is null");
            }

        };

        public static void HandleDelete(IDatabaseService database, string schema, string tablename, EJ.DBColumn originalColumn)
        {
            tablename = tablename.ToLower();

            var column = (EJ.DBColumn)originalColumn.Clone();
            column.BackupChangedProperties.ImportData(originalColumn.BackupChangedProperties);
            if (column.BackupChangedProperties["Name"] != null)
            {
                column.Name = column.BackupChangedProperties["Name"].OriginalValue.ToString();
                column.BackupChangedProperties.Remove("Name");
            }

            if (column.BackupChangedProperties["IsPKID"] != null && (bool)column.BackupChangedProperties["IsPKID"].OriginalValue == true)
                CancelPrimaryKey(database, schema, tablename, column);
            else if (column.IsPKID == true)
                CancelPrimaryKey(database, schema, tablename, column);

            Delete(database, schema, tablename, column);
        }

        public static void HandleChange(IDatabaseService database, string schema, string tablename, EJ.DBColumn originalColumn)
        {
            tablename = tablename.ToLower();

            var column = (EJ.DBColumn)originalColumn.Clone();
            column.BackupChangedProperties.ImportData(originalColumn.BackupChangedProperties);

            if (column.BackupChangedProperties.ContainsKey("Name"))
                Rename(database, schema, tablename, column);

            if (column.BackupChangedProperties.ContainsKey("IsAutoIncrement"))
            {
                if (column.IsAutoIncrement == false)
                {
                    // 去掉自增长
                    CancelAutoIncrement(database, schema, tablename, column);
                }
                else
                {
                    // 设为自增长
                    BeAutoIncrement(database, schema, tablename, column);
                }
            }

            if (column.BackupChangedProperties.ContainsKey("IsPKID") && column.IsPKID == false)
            {
                CancelPrimaryKey(database, schema, tablename, column);
            }

            bool tosetDefaultValue = false;
            if (column.BackupChangedProperties.ContainsKey("defaultValue") || column.BackupChangedProperties.ContainsKey("dbType"))
            {
                tosetDefaultValue = true;
                CancelDefaultValue(database, schema, tablename, column);
            }

            if (column.BackupChangedProperties.ContainsKey("dbType") || column.BackupChangedProperties.ContainsKey("length") || column.BackupChangedProperties.ContainsKey("CanNull"))
            {
                Modify(database, schema, tablename, column);
            }

            if (tosetDefaultValue && !string.IsNullOrEmpty(column.defaultValue))
            {
                SetDefaultValue(database, schema, tablename, column);
            }

            if (column.BackupChangedProperties.ContainsKey("IsPKID") && column.IsPKID == true)
            {
                BePrimaryKey(database, schema, tablename, column);
            }
        }

        public static void HandleNewColumn(IDatabaseService database, string schema, string tablename, EJ.DBColumn originalColumn)
        {
            tablename = tablename.ToLower();

            var column = (EJ.DBColumn)originalColumn.Clone();
            column.BackupChangedProperties.ImportData(originalColumn.BackupChangedProperties);

            var dbtype = SqlServerTableService.GetSqlServerType(column);

            string sql = "alter table " + GetQualifiedName(schema, tablename) + " add [" + column.Name.ToLower() + "] [" + dbtype + "]";

            if (dbtype.IndexOf("char") >= 0)
            {
                if (!string.IsNullOrEmpty(column.length))
                    sql += " (" + column.length + ")";
                else
                {
                    sql += " (50)";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(column.length))
                {
                    sql += " (" + column.length + ")";
                }
            }

            if (column.IsAutoIncrement == true)
                sql += " IDENTITY (1, 1)";

            if (column.CanNull == false || column.IsPKID == true || column.IsAutoIncrement == true)
            {
                sql += " NOT";
            }
            sql += " NULL  ";
            if (!string.IsNullOrEmpty(column.defaultValue))
            {
                string defaultValue = column.defaultValue.Trim();
                sql += " default '" + defaultValue.Replace("'", "''") + "' with values";

            }
            database.ExecSqlString(sql);

            if (column.IsPKID == true)
                database.ExecSqlString($"alter table {GetQualifiedName(schema, tablename)} add constraint pk_{tablename}_{column.Name.ToLower()} primary key ([{column.Name.ToLower()}])");

        }
    }
}