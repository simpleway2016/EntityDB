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
        static Action<IDatabaseService,string, EJ.DBColumn> CancelPrimaryKey = (database, tablename, column) => {
            //去除主键
            string sql = @"
DECLARE @NAME SYSNAME
DECLARE @TB_NAME SYSNAME
SET @TB_NAME = '" + tablename + @"'
SELECT TOP 1 @NAME = NAME FROM SYS.OBJECTS WITH(NOLOCK)
WHERE TYPE_DESC ='PRIMARY_KEY_CONSTRAINT'

AND PARENT_OBJECT_ID = (SELECT OBJECT_ID

FROM SYS.OBJECTS WITH(NOLOCK)

WHERE NAME = @TB_NAME )
SELECT @NAME
";
            var constraintName = database.ExecSqlString(sql).ToSafeString();
            if (constraintName.IsNullOrEmpty() == false)
            {
                //如果constraintName没有值，那么就是变更自增长字段的时候，原来字段被删除了
                database.ExecSqlString($"ALTER TABLE {tablename} DROP CONSTRAINT {constraintName}");
            }

        };

        static Action<IDatabaseService, string, EJ.DBColumn> BePrimaryKey = (database, tablename, column) => {
            //设为主键
            database.ExecSqlString("alter table [" + tablename + "] add constraint PK_" + tablename + "_" + column.Name.ToLower() + " primary key ([" + column.Name.ToLower() + "])");

        };

        static Action<IDatabaseService, string, EJ.DBColumn> Rename = (database, tablename, column) => {
            var changeitem = column.BackupChangedProperties["Name"];
            database.ExecSqlString($"EXEC sp_rename '[{tablename}].[{changeitem.OriginalValue}]', '{column.Name.ToLower()}', 'COLUMN'");

            column.BackupChangedProperties.Remove("Name");
        };

        static Action<IDatabaseService, string, EJ.DBColumn> Delete = (database, tablename, column) => {
            var columnName = column.Name.ToLower();
            if (column.BackupChangedProperties["Name"] != null)
                columnName = column.BackupChangedProperties["Name"].OriginalValue.ToString().ToLower();


            #region delete
            using (var sp_helpResult = database.SelectDataSet("sp_help [" + tablename + "]"))
            {
                foreach (var dtable in sp_helpResult.Tables)
                {
                    if (dtable.Columns.Any(m => m.ColumnName == "constraint_name"))
                    {
                        var query = dtable.Rows.Where(m => m["constraint_keys"].ToSafeString().ToLower() == columnName);
                        if (query.Count() > 0)
                        {
                            database.ExecSqlString("alter  table  [" + tablename + "]  drop  constraint " + query.First()["constraint_name"]);
                        }
                        break;
                    }
                }

                //删除默认值
                foreach (var dtable in sp_helpResult.Tables)
                {
                    if (dtable.Columns.Any(m => m.ColumnName == "constraint_name"))
                    {
                        var query = from m in dtable.Rows
                                    where ((string)m["constraint_type"]).ToLower().EndsWith(" " + columnName) && ((string)m["constraint_type"]).ToLower().StartsWith("default on ")
                                    select m;

                        if (query.Count() > 0)
                        {
                            database.ExecSqlString("alter  table  [" + tablename + "]  drop  constraint " + query.First()["constraint_name"]);
                        }
                        break;
                    }
                }
            }

            #endregion

            database.ExecSqlString("ALTER TABLE [" + tablename + "] DROP COLUMN [" + columnName + "]");
        };

        static Action<IDatabaseService, string, EJ.DBColumn> CancelAutoIncrement = (database, tablename, column) => {
            var flagColumnName = "_tempcolumn";
            while (Convert.ToInt32(database.ExecSqlString($"Select count(*) from syscolumns Where  ID=OBJECT_ID('{tablename}') and name='{flagColumnName}'")) > 0)
            {
                flagColumnName += "1";
            }

            var dbtype = SqlServerTableService.GetSqlServerType(column);

            //去掉自增长
            database.ExecSqlString($"alter table [{tablename}] add {flagColumnName.ToLower()} {dbtype}{(column.IsPKID == true? " not null" : "")}");
            database.ExecSqlString($"update [{tablename}] set {flagColumnName.ToLower()}=[{column.Name.ToLower()}]");
            Delete(database, tablename, column);
            database.ExecSqlString($"EXEC sp_rename '[{tablename}].{flagColumnName.ToLower()}', '{column.Name.ToLower()}', 'COLUMN'");

            //去掉自增长后，由于原来的列删除了，所以如果原来是主键，必须重新设置为主键
            if (column.IsPKID == true)
            {
                BePrimaryKey(database, tablename, column);
            }
        };

        static Action<IDatabaseService, string, EJ.DBColumn> BeAutoIncrement = (database, tablename, column) => {
            var flagColumnName = "_tempcolumn";
            while (Convert.ToInt32(database.ExecSqlString($"Select count(*) from syscolumns Where  ID=OBJECT_ID('{tablename}') and name='{flagColumnName}'")) > 0)
            {
                flagColumnName += "1";
            }

            var dbtype = SqlServerTableService.GetSqlServerType(column);

            //设为自增长
            database.ExecSqlString($"alter table [{tablename}] add {flagColumnName.ToLower()} {dbtype} IDENTITY (1, 1)");
            Delete(database, tablename, column );
            database.ExecSqlString($"EXEC sp_rename '[{tablename}].{flagColumnName.ToLower()}', '{column.Name.ToLower()}', 'COLUMN'");

            //去掉自增长后，由于原来的列删除了，所以如果原来是主键，必须重新设置为主键
            if (column.IsPKID == true)
            {
                //主键不允许为空
                database.ExecSqlString($"alter table [{tablename}] alter column [{column.Name.ToLower()}] [{dbtype}] not null");
                BePrimaryKey(database, tablename, column);
            }
        };

        static Action<IDatabaseService, string, EJ.DBColumn> CancelDefaultValue = (database, tablename, column) => {
            //获取默认值的id
            var defaultSettingID = database.ExecSqlString($"Select cdefault from syscolumns Where  ID=OBJECT_ID('{tablename}') and name='{column.Name.ToLower()}'");
            if (defaultSettingID != null && Convert.ToInt32(defaultSettingID) != 0)
            {
                var defaultKeyName = database.ExecSqlString($"select name from sysObjects where type='D' and id={defaultSettingID}");
                if (defaultKeyName != null)
                {
                    //如果进到这里，那么表示原来有默认值
                    database.ExecSqlString($"alter  table  [{tablename}]  drop  constraint {defaultKeyName}");
                }
            }
        };

        static Action<IDatabaseService, string, EJ.DBColumn> SetDefaultValue = (database, tablename, column) => {
            string sql = "";
            string defaultValue = column.defaultValue.Trim();
            sql += "alter  table  [" + tablename + "]  add  constraint DF_" + tablename + "_" + column.Name.ToLower() + " default '" + defaultValue.Replace("'", "''") + "' for [" + column.Name.ToLower() + "]";


            if (sql.Length > 0)
                database.ExecSqlString(sql);


            database.ExecSqlString("update [" + tablename + "] set [" + column.Name.ToLower() + "]='" + defaultValue.Replace("'", "''") + "' where [" + column.Name.ToLower() + "] is null");
        };

        static Action<IDatabaseService, string, EJ.DBColumn> Modify = (database, tablename, column) => {
            var dbtype = SqlServerTableService.GetSqlServerType(column);

            string sql = "alter table [" + tablename + "] alter column [" + column.Name.ToLower() + "] [" + dbtype + "]";

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

            //先改变字段类型，下面再设置默认值，和非null
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

                database.ExecSqlString("update [" + tablename + "] set [" + column.Name.ToLower() + "]='" + defaultValue.Replace("'", "''") + "' where [" + column.Name.ToLower() + "] is null");
            }

        };

        public static void HandleDelete(IDatabaseService database, string tablename, EJ.DBColumn originalColumn)
        {
            tablename = tablename.ToLower();

            var column = (EJ.DBColumn)originalColumn.Clone();
            column.BackupChangedProperties.ImportData(originalColumn.BackupChangedProperties);
            if(column.BackupChangedProperties["Name"] != null)
            {
                column.Name = column.BackupChangedProperties["Name"].OriginalValue.ToString();
                column.BackupChangedProperties.Remove("Name");
            }

            if (column.BackupChangedProperties["IsPKID"] != null && (bool)column.BackupChangedProperties["IsPKID"].OriginalValue == true)
                CancelPrimaryKey(database, tablename, column);
            else if (column.IsPKID == true)
                CancelPrimaryKey(database, tablename, column);

            Delete(database, tablename, column);
        }

        public static void HandleChange(IDatabaseService database,string tablename, EJ.DBColumn originalColumn)
        {
            tablename = tablename.ToLower();

            var column = (EJ.DBColumn)originalColumn.Clone();
            column.BackupChangedProperties.ImportData(originalColumn.BackupChangedProperties);

            if (column.BackupChangedProperties.ContainsKey("Name"))
                Rename(database, tablename, column);

            if(column.BackupChangedProperties.ContainsKey("IsAutoIncrement"))
            {
                if (column.IsAutoIncrement == false)
                {
                    //去掉自增长
                    CancelAutoIncrement(database, tablename, column);
                }
                else
                {
                    //设为自增长
                    BeAutoIncrement(database, tablename, column);
                }
            }

            if( column.BackupChangedProperties.ContainsKey("IsPKID") && column.IsPKID == false )
            {
                CancelPrimaryKey(database, tablename, column);
            }

            bool tosetDefaultValue = false;
            if(column.BackupChangedProperties.ContainsKey("defaultValue") || column.BackupChangedProperties.ContainsKey("dbType"))
            {
                tosetDefaultValue = true;
                CancelDefaultValue(database, tablename, column);
            }

            if(column.BackupChangedProperties.ContainsKey("dbType") || column.BackupChangedProperties.ContainsKey("length") || column.BackupChangedProperties.ContainsKey("CanNull"))
            {
                Modify(database, tablename, column);
            }

            if (tosetDefaultValue&&!string.IsNullOrEmpty(column.defaultValue))
            {
                SetDefaultValue(database, tablename, column);
            }

            if (column.BackupChangedProperties.ContainsKey("IsPKID") && column.IsPKID == true)
            {
                BePrimaryKey(database, tablename, column);
            }
        }

        public static void HandleNewColumn(IDatabaseService database, string tablename, EJ.DBColumn originalColumn)
        {
            tablename = tablename.ToLower();

            var column = (EJ.DBColumn)originalColumn.Clone();
            column.BackupChangedProperties.ImportData(originalColumn.BackupChangedProperties);

            var dbtype = SqlServerTableService.GetSqlServerType(column);

            string sql = "alter table [" + tablename + "] add [" + column.Name.ToLower() + "] [" + dbtype + "]";

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
                database.ExecSqlString("alter table [" + tablename + "] add constraint pk_" + tablename + "_" + column.Name.ToLower() + " primary key ([" + column.Name.ToLower() + "])");

        }
    }
}
