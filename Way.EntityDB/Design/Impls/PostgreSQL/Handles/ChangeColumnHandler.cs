
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
        // 辅助：根据 schema 生成带双引号的限定表名或序列名
        static string QuoteIdentifier(string name) => $"\"{name}\"";
        static string QualifiedObject(string schema, string name)
        {
            if (string.IsNullOrEmpty(schema))
                return QuoteIdentifier(name);
            return $"{QuoteIdentifier(schema)}.{QuoteIdentifier(name)}";
        }

        static Action<IDatabaseService, string, string, EJ.DBColumn> CancelPrimaryKey = (database, schema, tablename, column) => {
            // 查找主键约束名，按 schema 限定（若给出）
            var sql = new StringBuilder(@"
SELECT
    pg_constraint.conname AS pk_name
FROM
    pg_constraint
INNER JOIN pg_class ON pg_constraint.conrelid = pg_class.oid
INNER JOIN pg_namespace ns ON pg_class.relnamespace = ns.oid
WHERE
    pg_class.relname = '" + tablename + @"'
AND pg_constraint.contype = 'p'");

            if (!string.IsNullOrEmpty(schema))
            {
                sql.Append(" AND ns.nspname = '" + schema + "'");
            }

            var pkeyIndexName = database.ExecSqlString(sql.ToString()).ToSafeString();
            if (string.IsNullOrEmpty(pkeyIndexName))
                return;

            var fullTable = QualifiedObject(schema, tablename);
            database.ExecSqlString($"ALTER TABLE {fullTable} DROP CONSTRAINT IF EXISTS \"{pkeyIndexName}\"");
            database.ExecSqlString($"DROP INDEX IF EXISTS \"{pkeyIndexName}\"");
        };

        static Action<IDatabaseService, string, string, EJ.DBColumn> BePrimaryKey = (database, schema, tablename, column) => {
            // 设为主键;
            var fullTable = QualifiedObject(schema, tablename);
            database.ExecSqlString($"ALTER TABLE {fullTable} ADD PRIMARY KEY (\"{column.Name.ToLower()}\")");
        };

        static Action<IDatabaseService, string, string, EJ.DBColumn> Rename = (database, schema, tablename, column) => {
            var changeitem = column.BackupChangedProperties["Name"];
            var fullTable = QualifiedObject(schema, tablename);
            database.ExecSqlString($"alter table {fullTable} rename \"{changeitem.OriginalValue.ToString().ToLower()}\" to \"{column.Name.ToLower()}\"");

            column.BackupChangedProperties.Remove("Name");
        };

        static Action<IDatabaseService, string, string, EJ.DBColumn> Delete = (database, schema, tablename, column) => {
            var columnName = column.Name.ToLower();
            if (column.BackupChangedProperties["Name"] != null)
                columnName = column.BackupChangedProperties["Name"].OriginalValue.ToString().ToLower();

            var fullTable = QualifiedObject(schema, tablename);
            database.ExecSqlString(string.Format("alter table {0} drop column \"{1}\"", fullTable, columnName));
        };

        static Action<IDatabaseService, string, string, EJ.DBColumn> CancelAutoIncrement = (db, schema, tablename, column) => {
            // 查找是哪个 sequence（按 schema 限定 information_schema）
            var infoSql = $"select column_default from information_schema.columns where table_name = '{tablename}' and column_name='{column.Name.ToLower()}'";
            if (!string.IsNullOrEmpty(schema))
                infoSql += $" and table_schema = '{schema}'";
            var seqDef = db.ExecSqlString(infoSql).ToSafeString();

            // 支持 schema-qualified sequence 名称，抓取 nextval('...') 中的内容
            Match m = Regex.Match(seqDef, @"nextval\('(?<n>[^']+)'\s*::?"); // 捕获 nextval('schema.seq'::regclass) 或 nextval('seq')
            if (m != null && m.Success)
            {
                var seqName = m.Groups["n"].Value; // 可能包含 schema 前缀，例如 schema.seq
                // 生成 DROP 时的限定名称：若已带 schema 则分别加双引号，否则使用提供的 schema（若有）
                string dropSeq;
                if (seqName.Contains("."))
                {
                    var parts = seqName.Split('.');
                    dropSeq = $"{QuoteIdentifier(parts[0])}.{QuoteIdentifier(parts[1])}";
                }
                else if (!string.IsNullOrEmpty(schema))
                {
                    dropSeq = $"{QuoteIdentifier(schema)}.{QuoteIdentifier(seqName)}";
                }
                else
                {
                    dropSeq = QuoteIdentifier(seqName);
                }

                var fullTable = QualifiedObject(schema, tablename);
                db.ExecSqlString($"alter table {fullTable} ALTER COLUMN \"{column.Name.ToLower()}\" DROP DEFAULT");
                db.ExecSqlString($"DROP SEQUENCE IF EXISTS {dropSeq}");
            }
        };

        static Action<IDatabaseService, string, string, EJ.DBColumn> BeAutoIncrement = (db, schema, table, column) => {
            /*
           先创建一个 schema 下的序列（若指定），然后将表主键 ID 的默认值设置成这个序列的 NEXT 值
           */
            var seqBaseName = $"{table}_{column.Name.ToLower()}_seq";
            string createSeqName = !string.IsNullOrEmpty(schema) ? $"{QuoteIdentifier(schema)}.{QuoteIdentifier(seqBaseName)}" : QuoteIdentifier(seqBaseName);
            string nextvalArg = !string.IsNullOrEmpty(schema) ? $"{schema}.{seqBaseName}" : seqBaseName; // 用于 nextval('schema.seq')

            db.ExecSqlString($"DROP SEQUENCE IF EXISTS {createSeqName}");

            db.ExecSqlString($@"
CREATE SEQUENCE {createSeqName}
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
alter table {QualifiedObject(schema, table)} alter column ""{column.Name.ToLower()}"" set default nextval('{nextvalArg}');
");
        };

        static Action<IDatabaseService, string, string, EJ.DBColumn> CancelDefaultValue = (database, schema, tablename, column) => {
            // 删除默认值
            var fullTable = QualifiedObject(schema, tablename);
            database.ExecSqlString($"alter table {fullTable} ALTER COLUMN \"{column.Name.ToLower()}\" DROP DEFAULT");
        };

        static Action<IDatabaseService, string, string, EJ.DBColumn> SetDefaultValue = (database, schema, tablename, column) => {
            string defaultValue = column.defaultValue.Trim();
            var fullTable = QualifiedObject(schema, tablename);
            var sql = $"alter table {fullTable} ALTER COLUMN \"{column.Name.ToLower()}\" SET DEFAULT '{defaultValue.Replace("'", "''")}'";

            database.ExecSqlString(sql);

            database.ExecSqlString($"update {fullTable} set \"{column.Name.ToLower()}\"='{defaultValue.Replace("'", "''")}' where \"{column.Name.ToLower()}\" is null");
        };

        static Action<IDatabaseService, string, string, EJ.DBColumn> CanNullChange = (database, schema, tablename, column) => {
            var fullTable = QualifiedObject(schema, tablename);

            if (column.CanNull == false && !string.IsNullOrEmpty(column.defaultValue))
            {
                string defaultValue = column.defaultValue.Trim();
                database.ExecSqlString($"update {fullTable} set \"{column.Name.ToLower()}\"='{defaultValue.Replace("'", "''")}' where \"{column.Name.ToLower()}\" is null");
            }

            string sql = $"alter table {fullTable} ALTER COLUMN \"{column.Name.ToLower()}\" {(column.CanNull.Value ? "drop not null" : "set not null")}";
            database.ExecSqlString(sql);
        };

        static Action<IDatabaseService, string, string, EJ.DBColumn> Modify = (database, schema, tablename, column) => {
            var sqltype = PostgreSQLTableService.GetSqlType(column);
            var fullTable = QualifiedObject(schema, tablename);

            string sql = $"alter table {fullTable} ALTER COLUMN \"{column.Name.ToLower()}\" TYPE {(sqltype + (column.length.IsNullOrEmpty() ? "" : $"({column.length})"))} using \"{column.Name.ToLower()}\"::{sqltype}";
            database.ExecSqlString(sql);
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

            if (column.BackupChangedProperties["IsAutoIncrement"] != null && (bool)column.BackupChangedProperties["IsAutoIncrement"].OriginalValue == true)
                CancelAutoIncrement(database, schema, tablename, column);
            else if (column.IsAutoIncrement == true)
                CancelAutoIncrement(database, schema, tablename, column);

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

            if (column.BackupChangedProperties.ContainsKey("defaultValue"))
            {
                CancelDefaultValue(database, schema, tablename, column);
            }

            if (column.BackupChangedProperties.ContainsKey("dbType") || column.BackupChangedProperties.ContainsKey("length"))
            {
                Modify(database, schema, tablename, column);
            }

            if (column.BackupChangedProperties.ContainsKey("CanNull"))
            {
                CanNullChange(database, schema, tablename, column);
            }

            if (column.BackupChangedProperties.ContainsKey("defaultValue"))
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

            string sqltype = PostgreSQLTableService.GetSqlType(column);
            if (column.length.IsNullOrEmpty() == false)
            {
                if (sqltype.Contains("("))
                    sqltype = sqltype.Substring(0, sqltype.IndexOf("("));
                sqltype += "(" + column.length + ")";
            }

            var fullTable = QualifiedObject(schema, tablename);
            string sql = $"alter table {fullTable} add COLUMN \"{column.Name.ToLower()}\" {sqltype}";

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
                database.ExecSqlString($"ALTER TABLE {fullTable} ADD CONSTRAINT \"{tablename}_pkey\" PRIMARY KEY(\"{column.Name.ToLower()}\")");
            }

        }
    }
}