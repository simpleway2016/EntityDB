using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Way.EntityDB.Design.Database.MySql;
using Way.EntityDB.Design.Database.SqlServer;

namespace Way.EntityDB.Design.Impls.MySql.Handles
{
    class ChangeColumnHandler
    {
        static Action<IDatabaseService,string, EJ.DBColumn> CancelPrimaryKey = (database, tablename, column) => {

            database.ExecSqlString(string.Format("Alter table `{0}` drop primary key", tablename));
        };

        static Action<IDatabaseService, string, EJ.DBColumn> BePrimaryKey = (database, tablename, column) => {
            //设为主键;
            database.ExecSqlString(string.Format("Alter table `{0}` add primary key(`{1}`)", tablename, column.Name.ToLower()));
        };

        static Action<IDatabaseService, string, EJ.DBColumn> Rename = (database, tablename, column) => {
            var changeitem = column.BackupChangedProperties["Name"];

            string sqltype = MySqlTableService.GetSqlType(column.dbType);
            database.ExecSqlString(string.Format("alter table `{0}` change `{1}` `{2}` {3}", tablename, changeitem.OriginalValue.ToString().ToLower(), column.Name.ToLower(), sqltype));

            column.BackupChangedProperties.Remove("Name");
        };

        static Action<IDatabaseService, string, EJ.DBColumn> Delete = (database, tablename, column) => {
            database.ExecSqlString(string.Format("alter table `{0}` drop column `{1}`", tablename, column.Name.ToLower()));
        };

        static Action<IDatabaseService, string, EJ.DBColumn> CancelAutoIncrement = (database, tablename, column) => {
            string sqltype = MySqlTableService.GetSqlType(column.dbType);
            //去掉自增长
            database.ExecSqlString(string.Format("Alter table `{0}` change `{1}` `{1}` {2}", tablename, column.Name.ToLower(), sqltype));
        };

        static Action<IDatabaseService, string, EJ.DBColumn> BeAutoIncrement = (database, tablename, column) => {
            string sqltype = MySqlTableService.GetSqlType(column.dbType);
            if (column.IsPKID == true && column.BackupChangedProperties["IsPKID"] != null && (bool)column.BackupChangedProperties["IsPKID"].OriginalValue == false)
            {
                //设为自增长之前，此字段必须是主键
                //设为主键;
                database.ExecSqlString(string.Format("Alter table `{0}` add primary key(`{1}`)", tablename, column.Name.ToLower()));
                column.BackupChangedProperties.Remove("IsPKID");
            }
            //设为自增长
            database.ExecSqlString(string.Format("Alter table `{0}` change `{1}` `{1}` {2} not null auto_increment", tablename, column.Name.ToLower(), sqltype));
        };

        static Action<IDatabaseService, string, EJ.DBColumn> CancelDefaultValue = (database, tablename, column) => {
            string sqltype = MySqlTableService.GetSqlType(column.dbType);
            //删除默认值
            database.ExecSqlString($"alter table `{tablename}` MODIFY `{column.Name.ToLower()}` {sqltype} default null");
        };

        static Action<IDatabaseService, string, EJ.DBColumn> SetDefaultValue = (database, tablename, column) => {
            string sqltype = MySqlTableService.GetSqlType(column.dbType);
            string sql = "";
            string defaultValue = column.defaultValue.Trim();
            string typestr = sqltype;
            if (column.CanNull == false || column.IsPKID == true || column.IsAutoIncrement == true)
                typestr += " NOT NULL";

            if (column.dbType == "bit")
            {
                sql += $"alter table `{tablename}` MODIFY `{column.Name.ToLower()}` {typestr} default {defaultValue}";
            }
            else
            {
                sql += $"alter table `{tablename}` MODIFY `{column.Name.ToLower()}` {typestr} default '{defaultValue.Replace("'", "''")}'";
            }

            database.ExecSqlString(sql);

            database.ExecSqlString("update `" + tablename + "` set `" + column.Name.ToLower() + "`='" + defaultValue.Replace("'", "''") + "' where `" + column.Name.ToLower() + "` is null");
        };

        static Action<IDatabaseService, string, EJ.DBColumn> Modify = (database, tablename, column) => {
            if (column.CanNull == false && !string.IsNullOrEmpty(column.defaultValue))
            {
                string defaultValue = column.defaultValue.Trim();

                database.ExecSqlString("update `" + tablename + "` set `" + column.Name.ToLower() + "`='" + defaultValue.Replace("'", "''") + "' where `" + column.Name.ToLower() + "` is null");
            }
            string sqltype = MySqlTableService.GetSqlType(column.dbType);
            if (column.length.IsNullOrEmpty() == false)
            {
                if (sqltype.Contains("("))
                    sqltype = sqltype.Substring(0, sqltype.IndexOf("("));
                sqltype += "(" + column.length + ")";
            }

            string sql = "alter table `" + tablename + "` MODIFY `" + column.Name.ToLower() + "` " + sqltype;            

            if (column.CanNull == false || column.IsPKID == true || column.IsAutoIncrement == true)
                sql += " NOT";
            sql += " NULL ";
            if (column.IsAutoIncrement == true)
            {
                sql += " auto_increment ";
            }
            database.ExecSqlString(sql);

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

            if(column.BackupChangedProperties.ContainsKey("defaultValue"))
            {
                CancelDefaultValue(database, tablename, column);
            }

            if(column.BackupChangedProperties.ContainsKey("dbType") || column.BackupChangedProperties.ContainsKey("length") || column.BackupChangedProperties.ContainsKey("CanNull"))
            {
                Modify(database, tablename, column);
            }

            if (column.BackupChangedProperties.ContainsKey("defaultValue"))
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

            string sqltype = MySqlTableService.GetSqlType(column.dbType);
            if (column.length.IsNullOrEmpty() == false)
            {
                if (sqltype.Contains("("))
                    sqltype = sqltype.Substring(0, sqltype.IndexOf("("));
                sqltype += "(" + column.length + ")";
            }

            string sql = "alter table `" + tablename + "` add `" + column.Name.ToLower() + "` " + sqltype;

            if (column.IsAutoIncrement == true)
                sql += " AUTOINCREMENT";

            if (column.CanNull == false || column.IsPKID == true || column.IsAutoIncrement == true)
            {
                sql += " NOT";
            }
            sql += " NULL  ";
            if (!string.IsNullOrEmpty(column.defaultValue))
            {
                string defaultValue = column.defaultValue.Trim();
                if (column.dbType == "bit")
                {
                    sql += " default " + defaultValue;
                }
                else
                {
                    sql += " default '" + defaultValue.Replace("'", "''") + "'";
                }

            }
            database.ExecSqlString(sql);

            if (column.IsPKID == true)
            {
                database.ExecSqlString(string.Format("Alter table `{0}` add primary key(`{1}`)", tablename, column.Name.ToLower()));
            }

        }
    }
}
