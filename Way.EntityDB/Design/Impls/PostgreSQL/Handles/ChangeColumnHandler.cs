using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using Way.EntityDB.Design.Database.PostgreSQL;
using Way.EntityDB.Design.Database.SqlServer;

namespace Way.EntityDB.Design.Impls.PostgreSQL.Handles
{
    class ChangeColumnHandler
    {
        static Action<IDatabaseService,string, EJ.DBColumn> CancelPrimaryKey = (database, tablename, column) => {
            var pkeyIndexName = database.ExecSqlString(@"
SELECT
    pg_constraint.conname AS pk_name
FROM
    pg_constraint
INNER JOIN pg_class ON pg_constraint.conrelid = pg_class.oid
WHERE
    pg_class.relname = '" + tablename + @"'
AND pg_constraint.contype = 'p';
").ToSafeString();
            //if (pkeyIndexName.Length > 0)
            //{
            //    database.ExecSqlString($"ALTER TABLE {newTableName} DROP CONSTRAINT IF EXISTS {oldTableName}_pkey");
            //    database.ExecSqlString($"DROP INDEX IF EXISTS {oldTableName}_pkey");
            //}

            database.ExecSqlString($"ALTER TABLE \"{tablename}\" DROP CONSTRAINT IF EXISTS {pkeyIndexName}");
            database.ExecSqlString($"DROP INDEX IF EXISTS {pkeyIndexName}");
        };

        static Action<IDatabaseService, string, EJ.DBColumn> BePrimaryKey = (database, tablename, column) => {
            //设为主键;
            database.ExecSqlString($"ALTER TABLE \"{tablename}\" ADD PRIMARY KEY (\"{column.Name.ToLower()}\")");
        };

        static Action<IDatabaseService, string, EJ.DBColumn> Rename = (database, tablename, column) => {
            var changeitem = column.BackupChangedProperties["Name"];
            database.ExecSqlString($"alter table \"{tablename}\" rename \"{changeitem.OriginalValue.ToString().ToLower()}\" to \"{column.Name.ToLower()}\"");

            column.BackupChangedProperties.Remove("Name");
        };

        static Action<IDatabaseService, string, EJ.DBColumn> Delete = (database, tablename, column) => {
            var columnName = column.Name.ToLower();
            if (column.BackupChangedProperties["Name"] != null)
                columnName = column.BackupChangedProperties["Name"].OriginalValue.ToString().ToLower();

            database.ExecSqlString(string.Format("alter table \"{0}\" drop column \"{1}\"", tablename, columnName));
        };

        static Action<IDatabaseService, string, EJ.DBColumn> CancelAutoIncrement = (db, tablename, column) => {
            //查找是哪个sequence
            //select column_name,data_type,column_default,is_nullable,character_maximum_length,character_octet_length from information_schema.columns where table_name = 'tbl_role';
            var seqName = db.ExecSqlString($"select column_default from information_schema.columns where table_name = '{tablename}' and column_name='{column.Name.ToLower()}'").ToSafeString();
            Match m = Regex.Match(seqName, @"nextval\(\'(?<n>(\w)+)\'");
            if (m != null && m.Length > 0)
            {
                seqName = m.Groups["n"].Value;
                db.ExecSqlString($"alter table \"{tablename}\" ALTER COLUMN \"{column.Name.ToLower()}\" DROP DEFAULT");
                db.ExecSqlString($"DROP SEQUENCE IF EXISTS  {seqName}");
            }
        };

        static Action<IDatabaseService, string, EJ.DBColumn> BeAutoIncrement = (db, table, column) => {
            /*
           先创建一张表，再创建一个序列，然后将表主键ID的默认值设置成这个序列的NEXT值
           SELECT c.relname FROM pg_class c WHERE c.relkind = 'S';可以查看所有SEQUENCE
           */
            db.ExecSqlString($"DROP SEQUENCE IF EXISTS {table}_{column.Name.ToLower()}_seq");

            db.ExecSqlString($@"
CREATE SEQUENCE {table}_{column.Name.ToLower()}_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
alter table ""{table}"" alter column ""{column.Name.ToLower()}"" set default nextval('{table}_{column.Name.ToLower()}_seq');
");
        };

        static Action<IDatabaseService, string, EJ.DBColumn> CancelDefaultValue = (database, tablename, column) => {
            //删除默认值
            database.ExecSqlString($"alter table \"{tablename}\" ALTER COLUMN \"{column.Name.ToLower()}\" DROP DEFAULT");
        };

        static Action<IDatabaseService, string, EJ.DBColumn> SetDefaultValue = (database, tablename, column) => {
            string sql = "";
            string defaultValue = column.defaultValue.Trim();
            sql += $"alter table \"{tablename}\" ALTER COLUMN \"{column.Name.ToLower()}\" SET DEFAULT '{defaultValue.Replace("'", "''")}'";


            database.ExecSqlString(sql);

            database.ExecSqlString($"update \"{tablename}\" set \"{column.Name.ToLower()}\"='{defaultValue.Replace("'", "''")}' where \"{column.Name.ToLower()}\" is null");
        };

        static Action<IDatabaseService, string, EJ.DBColumn> CanNullChange = (database, tablename, column) => {
            if (column.CanNull == false && !string.IsNullOrEmpty(column.defaultValue))
            {
                string defaultValue = column.defaultValue.Trim();
                database.ExecSqlString($"update \"{tablename}\" set \"{column.Name.ToLower()}\"='{defaultValue.Replace("'", "''")}' where \"{column.Name.ToLower()}\" is null");
            }


            string sql = $"alter table \"{tablename}\" ALTER COLUMN \"{column.Name.ToLower()}\" {(column.CanNull.Value ? "drop not null" : "set not null")}";
            database.ExecSqlString(sql);
        };

        static Action<IDatabaseService, string, EJ.DBColumn> Modify = (database, tablename, column) => {
            var sqltype = PostgreSQLTableService.GetSqlType(column);
            
            string sql = $"alter table \"{tablename}\" ALTER COLUMN \"{column.Name.ToLower()}\" TYPE {(sqltype + (column.length.IsNullOrEmpty() ? "" : $"({column.length})"))} using \"{column.Name.ToLower()}\"::{sqltype}";
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

            if (column.BackupChangedProperties["IsAutoIncrement"] != null && (bool)column.BackupChangedProperties["IsAutoIncrement"].OriginalValue == true)
                CancelAutoIncrement(database,  tablename, column);
            else if (column.IsAutoIncrement == true)
                CancelAutoIncrement(database, tablename, column);

            if (column.BackupChangedProperties["IsPKID"] != null && (bool)column.BackupChangedProperties["IsPKID"].OriginalValue == true)
                CancelPrimaryKey(database, tablename,column);
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

            if(column.BackupChangedProperties.ContainsKey("dbType") || column.BackupChangedProperties.ContainsKey("length"))
            {
                Modify(database, tablename, column);
            }

            if (column.BackupChangedProperties.ContainsKey("CanNull"))
            {
                CanNullChange(database, tablename, column);
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

            string sqltype = PostgreSQLTableService.GetSqlType(column);
            if (column.length.IsNullOrEmpty() == false)
            {
                if (sqltype.Contains("("))
                    sqltype = sqltype.Substring(0, sqltype.IndexOf("("));
                sqltype += "(" + column.length + ")";
            }

            string sql = "alter table \"" + tablename + "\" add COLUMN \"" + column.Name.ToLower() + "\" " + sqltype;


            if (column.CanNull == false || column.IsPKID == true || column.IsAutoIncrement == true)
            {
                sql += " NOT";
            }
            sql += " NULL  ";
            if (!string.IsNullOrEmpty(column.defaultValue))
            {
                string defaultValue = column.defaultValue.Trim();
                sql += " DEFAULT '" + defaultValue.Replace("'", "''") + "'";
            }
            database.ExecSqlString(sql);

            if (column.IsPKID == true)
            {
                database.ExecSqlString($"ALTER TABLE \"{tablename}\" ADD CONSTRAINT {tablename}_pkey PRIMARY KEY(\"{column.Name.ToLower()}\")");
            }

        }
    }
}
