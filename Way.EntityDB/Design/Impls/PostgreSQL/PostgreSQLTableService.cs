using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Way.EntityDB.Design.Impls.PostgreSQL.Handles;

namespace Way.EntityDB.Design.Database.PostgreSQL
{
    //在mysql 5.7.18 版本测试
    [EntityDB.Attributes.DatabaseTypeAttribute(DatabaseType.PostgreSql)]
    class PostgreSQLTableService : Services.ITableDesignService
    {
        internal static List<string> ColumnType = new List<string>(new string[] {
                                            "character varying",
                                            "integer",
                                            "bytea",//image
                                            "text",
                                             "text",//MediumText
                                              "text",//longtext
                                            "smallint",
                                            "date",//smalldatetime
                                            "real",
                                            "timestamp without time zone",//datetime
                                             "timestamp with time zone",//datetime with time zone
                                             "date",//date
                                              "time without time zone",//time
                                            "float4",
                                            "float8",
                                            "boolean",
                                            "decimal",//decimal
                                            "numeric",
                                            "int8",
                                            "bytea",//varbinary
                                            "character",
                                            "timestamp without time zone",
                                            "jsonb",});
        public static string GetSqlType(EJ.DBColumn column)
        {
            //serial不是一种自增长类型，只是一个宏，所以不要用serial
            string dbtype = column.dbType.ToLower();
            int index = Design.ColumnType.SupportTypes.IndexOf(dbtype);
            if (index < 0 || ColumnType[index] == null)
                throw new Exception($"不支持字段类型{dbtype}");
            return ColumnType[index];
        }



        public void CreateTable(EntityDB.IDatabaseService db, EJ.DBTable table, EJ.DBColumn[] columns, IndexInfo[] indexInfos)
        {

            //db.ExecSqlString("drop table if exists " + table.Name.ToLower() + "");

            string sqlstr;
            sqlstr = @"
CREATE TABLE """ + table.Name.ToLower() + @""" (
";

            for (int i = 0; i < columns.Length; i++)
            {
                var column = columns[i];

                if (column.dbType == "jsonb")
                    column.length = null;

                if (i > 0)
                    sqlstr += ",\r\n";
                string sqltype = GetSqlType(column);
                if (string.IsNullOrEmpty(column.length) == false)
                {
                    if (sqltype.Contains("("))
                        sqltype = sqltype.Substring(0, sqltype.IndexOf("("));
                    sqltype += "(" + column.length + ")";
                }
                sqlstr += "\"" + column.Name.ToLower() + "\" " + sqltype;

                if (column.CanNull == false || column.IsPKID == true || column.IsAutoIncrement == true)
                    sqlstr += " NOT";
                sqlstr += " NULL ";

                if (column.IsPKID == true)
                {
                    sqlstr += " PRIMARY KEY ";
                }

                if (!string.IsNullOrEmpty(column.defaultValue))
                {
                    string defaultValue = column.defaultValue.Trim();
                    sqlstr += " DEFAULT '" + defaultValue.Replace("'", "''") + "'";
                }


            }



            sqlstr += ")";

            db.ExecSqlString(sqlstr);


            foreach (var column in columns)
            {
                if (column.IsAutoIncrement == true)
                {
                    setColumn_IsAutoIncrement(db, column, table.Name.ToLower(), true);
                }
            }

            if (indexInfos != null && indexInfos.Length > 0)
            {
                foreach (var config in indexInfos)
                {
                    createIndex(db, table.Name.ToLower(), config);
                }
            }

        }

        void setColumn_IsAutoIncrement(IDatabaseService db, EJ.DBColumn column, string table, bool isAutoIncrement)
        {
            table = table.ToLower();
            /*
             先创建一张表，再创建一个序列，然后将表主键ID的默认值设置成这个序列的NEXT值
             SELECT c.relname FROM pg_class c WHERE c.relkind = 'S';可以查看所有SEQUENCE
             */
            if (isAutoIncrement)
            {
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
            }
            else
            {
                //查找是哪个sequence
                //select column_name,data_type,column_default,is_nullable,character_maximum_length,character_octet_length from information_schema.columns where table_name = 'tbl_role';
                var seqName = db.ExecSqlString($"select column_default from information_schema.columns where table_name = '{table.ToLower()}' and column_name='{column.Name.ToLower()}'").ToSafeString();
                Match m = Regex.Match(seqName, @"nextval\(\'(?<n>(\w)+)\'");
                if (m != null && m.Length > 0)
                {
                    seqName = m.Groups["n"].Value;
                    db.ExecSqlString($"alter table \"{table}\" ALTER COLUMN \"{column.Name.ToLower()}\" DROP DEFAULT");
                    db.ExecSqlString($"DROP SEQUENCE IF EXISTS  {seqName}");
                }
            }
        }

        public void DeleteTable(EntityDB.IDatabaseService database, string tableName)
        {
            database.ExecSqlString(string.Format("DROP TABLE IF EXISTS \"{0}\"", tableName.ToLower()));
        }



        List<string> checkIfIdxChanged(EntityDB.IDatabaseService database, string tablename, List<IndexInfo> indexInfos)
        {
            tablename = tablename.ToLower();
            var tableindexes = new Impls.PostgreSQL.PostgreSQLDatabaseService().GetCurrentIndexes(database, tablename.ToLower());
            List<string> needToDels = new List<string>();
            foreach (var dbindex in tableindexes)
            {
                string longname = dbindex.ColumnNames.ToArray().ToSplitString(",");
                if (indexInfos.Any(m => m.ColumnNames.OrderBy(n => n).ToArray().ToSplitString(",").ToLower() == longname.ToLower()) == false)
                {
                    needToDels.Add(dbindex.Name.ToLower());
                }
                else
                {
                    var existIndexes = indexInfos.Where(m => m.IsUnique == dbindex.IsUnique && m.IsClustered == dbindex.IsClustered && m.ColumnNames.OrderBy(n => n).ToArray().ToSplitString(",").ToLower() == longname.ToLower()).ToArray();
                    if (existIndexes.Length == 0)
                    {
                        needToDels.Add(dbindex.Name.ToLower());
                    }
                    else
                    {
                        //现有的索引是一样的，所以不用创建了
                        foreach (var indexinfo in existIndexes)
                            indexInfos.Remove(indexinfo);
                    }
                }
            }

            return needToDels;
        }
        void deletecolumn(EntityDB.IDatabaseService database, string table, string column)
        {
            table = table.ToLower();
            column = column.ToLower();

            database.ExecSqlString(string.Format("alter table \"{0}\" drop column \"{1}\"", table, column));
        }
        void dropTableIndex(EntityDB.IDatabaseService database, string table, string indexName)
        {
            table = table.ToLower();
            database.ExecSqlString("ALTER TABLE \"" + table.ToLower() + "\" DROP CONSTRAINT IF EXISTS " + indexName.ToLower() + "");
            database.ExecSqlString("DROP INDEX IF EXISTS " + indexName.ToLower() + "");
        }

        void deletePkColumn(IDatabaseService database, string tablename)
        {
            tablename = tablename.ToLower();
            //去除主键;//删除主建
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
        }
        public void ChangeTable(EntityDB.IDatabaseService database, string oldTableName, string newTableName, EJ.DBColumn[] addColumns, EJ.DBColumn[] changed_columns, EJ.DBColumn[] deletedColumns, Func<List<EJ.DBColumn>> getColumnsFunc, IndexInfo[] _indexInfos)
        {
            List<EJ.DBColumn> changedColumns = new List<EJ.DBColumn>(changed_columns);
            oldTableName = oldTableName.ToLower();
            newTableName = newTableName.ToLower();
            List<IndexInfo> indexInfos = new List<IndexInfo>(_indexInfos);

            //先判断表明是否更改
            if (oldTableName != newTableName)
            {
                //更改表名
                database.ExecSqlString($"alter table \"{oldTableName}\" RENAME TO \"{newTableName}\"");
            }

            var needToDels = checkIfIdxChanged(database, newTableName, indexInfos);

            foreach (var column in deletedColumns)
            {
                ChangeColumnHandler.HandleDelete(database, newTableName, column);
            }

            foreach (string delIndexName in needToDels)
                dropTableIndex(database, newTableName, delIndexName);

            //将取消主键的列放前面处理
            if (true)
            {
                var column = changedColumns.FirstOrDefault(m => m.BackupChangedProperties["IsPKID"] != null && (bool)m.BackupChangedProperties["IsPKID"].OriginalValue == true);
                if (column != null && column.IsPKID == false)
                {
                    changedColumns.Remove(column);
                    changedColumns.Insert(0, column);
                }
            }

            foreach (var column in changedColumns)
            {
                if (column.dbType == "jsonb")
                    column.length = null;
                ChangeColumnHandler.HandleChange(database, newTableName, column);
            }

            foreach (var column in addColumns)
            {
                if (column.dbType == "jsonb")
                    column.length = null;
                ChangeColumnHandler.HandleNewColumn(database, newTableName, column);
            }

            foreach (var config in indexInfos)
            {
                createIndex(database, newTableName, config);
            }
        }

        void createIndex(EntityDB.IDatabaseService database, string table, IndexInfo indexinfo)
        {
            table = table.ToLower();
            //alter table table_name add unique key new_uk_name (col1,col2);
            var columns = indexinfo.ColumnNames.OrderBy(m => m).Select(m => m.ToLower()).ToArray();
            string name = table + "_ej_" + columns.ToSplitString("_");

            string sql = "CREATE ";
            if (indexinfo.IsUnique)
            {
                sql += "UNIQUE ";
            }
            if (indexinfo.IsClustered)
            {
                sql += $"INDEX ON \"{table}\" ({columns.ToSplitString(" ASC NULLS FIRST,")} ASC NULLS FIRST)";
            }
            else
            {
                sql += $"INDEX ON \"{table}\" ({columns.ToSplitString(",")})";
            }
            database.ExecSqlString(sql);
        }
    }
}