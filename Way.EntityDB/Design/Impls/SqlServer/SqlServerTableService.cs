using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Way.EntityDB.Design.Impls.SqlServer.Handles;

namespace Way.EntityDB.Design.Database.SqlServer
{
    [EntityDB.Attributes.DatabaseTypeAttribute(DatabaseType.SqlServer)]
    class SqlServerTableService : Services.ITableDesignService
    {

        internal static List<string> ColumnType = new List<string>(new string[] {
                                            "varchar",
                                            "int",
                                            "image",
                                            "text",
                                            "text",//MediumText
                                            "text",//longtext
                                            "smallint",
                                            "smalldatetime",
                                            "real",
                                            "datetime",
                                             "date",
                                              "time",
                                            "float",
                                            "float",//double
                                            "bit",
                                            "decimal",
                                            "numeric",
                                            "bigint",
                                            "varbinary",
                                            "char",
                                            "timestamp", 
                                            "text",//jsonb
        });

        public static string GetSqlServerType(EJ.DBColumn column)
        {
            string dbtype = column.dbType.ToLower();
            int index = Design.ColumnType.SupportTypes.IndexOf(dbtype);
            if (index < 0 || ColumnType[index] == null)
                throw new Exception($"不支持字段类型{dbtype}");
            return ColumnType[index];
        }

        public void CreateTable(EntityDB.IDatabaseService db, EJ.DBTable table, EJ.DBColumn[] columns
            , IndexInfo[] IDXConfigs)
        {

                string sqlstr;
                sqlstr = @"
CREATE TABLE [" + table.Name.ToLower() + @"] (
";

            
                foreach (EJ.DBColumn column in columns)
                {
                    var dbtype = GetSqlServerType(column);
                    sqlstr += "[" + column.Name.ToLower() + "] [" + dbtype + "]";
                    if (dbtype.IndexOf("char") >= 0)
                    {
                        if (!string.IsNullOrEmpty(column.length))
                            sqlstr += " (" + column.length + ")";
                        else
                        {
                            sqlstr += " (50)";
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(column.length))
                        {
                            sqlstr += " (" + column.length + ")";
                        }
                    }

                    if (column.IsAutoIncrement == true)
                    {
                        sqlstr += " IDENTITY (1, 1)";
                    }
                    if (column.CanNull == false || column.IsPKID == true || column.IsAutoIncrement == true)
                        sqlstr += " NOT";
                    sqlstr += " NULL ";

                    
                    if (!string.IsNullOrEmpty( column.defaultValue) )
                    {
                        string defaultValue = column.defaultValue.Trim();
                        sqlstr += " CONSTRAINT [DF_" + table.Name.ToLower() + "_" + column.Name.ToLower() + "] DEFAULT ('" + defaultValue.Replace("'","''") + "')";
                        
                    }

                    sqlstr += ",";
                }
            if (sqlstr.EndsWith(","))
            {
                sqlstr = sqlstr.Remove(sqlstr.Length - 1);
            }
                sqlstr += ")";


                db.ExecSqlString(sqlstr);

            foreach (var column in columns)
            {
                if (column.IsPKID == true)
                {
                    //设为主键
                    db.ExecSqlString("alter table [" + table.Name.ToLower() + "] add constraint PK_" + table.Name.ToLower() + "_" + column.Name.ToLower() + " primary key ([" + column.Name.ToLower() + "])");
                    
                }
            }

            if (IDXConfigs != null && IDXConfigs.Length > 0)
                {
                    foreach (var c in IDXConfigs)
                    {
                        createIndex(db, table.Name.ToLower(), c);
                    }
                }
            
        }
        /// <summary>
        /// 根据列创建索引
        /// </summary>
        /// <param name="database"></param>
        /// <param name="table"></param>
        /// <param name="indexInfo"></param>
        void createIndex(EntityDB.IDatabaseService database, string table, IndexInfo indexInfo)
        {
            table = table.ToLower();
            var columns = indexInfo.ColumnNames.OrderBy(m => m).Select(m=>m.ToLower()).ToArray();
            string columnsStr = "";
            string name = table + "_";
            for (int i = 0; i < columns.Length; i++)
            {
                name += columns[i] + "_";
                columnsStr += "[" + columns[i] + "]";
                if (i < columns.Length - 1)
                    columnsStr += ",";
            }

            try
            {
                string type = "";
                if (indexInfo.IsUnique)
                    type = "unique ";

                if (indexInfo.IsClustered)
                {
                    type += "CLUSTERED ";
                }
                else
                {
                    type += "NONCLUSTERED ";
                }
                database.ExecSqlString(string.Format("CREATE {3} index IDX_{2} on [{0}] ({1})", table, columnsStr, name,type));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 删除table的所有包含字段的索引
        /// </summary>
        /// <param name="database"></param>
        /// <param name="table"></param>
        /// <param name="columnNames">包含的字段</param>
        void dropTableAllUniqueIndexWithColumns(EntityDB.IDatabaseService database, string table, List<string> columnNames)
        {
            table = table.ToLower();
            List<string> toDelIndexes = new List<string>();
            using (var sp_helpResult = database.SelectDataSet("sp_help [" + table + "]"))
            {
                foreach (var dtable in sp_helpResult.Tables)
                {
                    if (dtable.Columns.Any(m=>m.ColumnName == "index_keys"))
                    {
                        foreach (var drow in dtable.Rows)
                        {
                            string existColumnString = drow["index_keys"].ToString();
                             string indexName = drow["index_name"].ToString();
                             if (indexName.StartsWith("IDX_"))
                             {
                                 foreach (string column in columnNames)
                                 {
                                     if (Regex.IsMatch(existColumnString, @"\b(" + column + @")\b" , RegexOptions.IgnoreCase))
                                     {
                                         toDelIndexes.Add(indexName);
                                         break;
                                     }
                                 }
                             }
                        }
                        break;
                    }
                }
            }

            foreach (string indexName in toDelIndexes)
            {
                database.ExecSqlString("drop   index "+ indexName +" on ["+table+"]");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <param name="table"></param>
        void dropTableIndex(EntityDB.IDatabaseService database, string table,IndexInfo[] dontDelIndexes)
        {
            table = table.ToLower();
            List<string> toDelIndexes = new List<string>();
            using (var sp_helpResult = database.SelectDataSet("sp_help [" + table + "]"))
            {
                foreach (var dtable in sp_helpResult.Tables)
                {
                    if (dtable.Columns.Any(m=>m.ColumnName == "index_keys"))
                    {
                        foreach (WayDataRow drow in dtable.Rows)
                        {
                            string existColumnString = drow["index_keys"].ToString();
                            string indexName = drow["index_name"].ToString();
                            string index_description = drow["index_description"].ToString();

                            if (index_description.Contains("primary key") == false  )
                            {
                                if (dontDelIndexes.Count(m => string.Equals( m.Name , indexName , StringComparison.CurrentCultureIgnoreCase)) == 0)
                                {
                                    toDelIndexes.Add(indexName);
                                }
                            }
                        }
                        break;
                    }
                }
            }

            foreach (string indexName in toDelIndexes)
            {
                database.ExecSqlString("drop   index " + indexName + " on [" + table + "]");
            }
        }

        /// <summary>
        /// 返回没有变化的索引
        /// </summary>
        /// <param name="database"></param>
        /// <param name="tablename"></param>
        /// <param name="idxConfigs"></param>
        /// <returns></returns>
        List<IndexInfo> checkIfIdxChanged(EntityDB.IDatabaseService database, string tablename, IndexInfo[] idxConfigs)
        {
            tablename = tablename.ToLower();
            List<IndexInfo> result = new List<IndexInfo>();
            List<IndexInfo> existKeys = new List<IndexInfo>();
            using (var sp_helpResult = database.SelectDataSet("sp_help [" + tablename + "]"))
            {
                foreach (var dtable in sp_helpResult.Tables)
                {
                    if (dtable.Columns.Any(m=>m.ColumnName=="index_keys"))
                    {
                        foreach (WayDataRow drow in dtable.Rows)
                        {
                            string existColumnString = drow["index_keys"].ToString();
                            string indexName = drow["index_name"].ToString();
                            string index_description = drow["index_description"].ToString();
                            if (index_description.Contains("primary key") == false)
                            {
                                //去除空格
                                string flag = existColumnString.Split(',').ToSplitString();
                                string dbname = flag.Split(',').OrderBy(m => m).ToArray().ToSplitString().ToLower();
                                //再排序，不要在去除空格之前排序
                                existKeys.Add(new IndexInfo
                                {
                                    Name = indexName,
                                    IsUnique = index_description.Contains("unique"),
                                    IsClustered = index_description.Contains("clustered") && !index_description.Contains("nonclustered"),
                                    ColumnNames = new string[] { dbname },
                                });
                            }
                            else
                            {
                                if (idxConfigs.Count(m => m.IsClustered) > 0)
                                {
                                    //去除主键
                                    database.ExecSqlString("alter  table  [" + tablename + "]  drop  constraint " + indexName);
                                    //设为主键
                                    string flag = existColumnString.Split(',').ToSplitString();
                                    string ppname = flag.Split(',').OrderBy(m => m).ToArray().ToSplitString("," , "[{0}]").ToLower();
                                    database.ExecSqlString("alter table [" + tablename + "] add constraint " + indexName + " primary key NONCLUSTERED (" + ppname + ")");
                                }
                            }
                            
                        }
                        break;
                    }
                }
            }
           

            foreach (var nowConfigItem in idxConfigs)
            {
                string myname = nowConfigItem.ColumnNames.OrderBy(m => m).ToArray().ToSplitString().ToLower();
                var fined = existKeys.FirstOrDefault(m => m.IsUnique == nowConfigItem.IsUnique && m.IsClustered == nowConfigItem.IsClustered && m.ColumnNames[0].ToLower() == myname);
                if (fined != null)
                {
                    nowConfigItem.Name = fined.Name;
                    result.Add(nowConfigItem);
                }
                
            }
            return result;
        }


        public void ChangeTable(EntityDB.IDatabaseService database, string oldTableName, string newTableName, 
            EJ.DBColumn[] addColumns, EJ.DBColumn[] changed_columns, EJ.DBColumn[] deletedColumns, Func<List<EJ.DBColumn>> getColumnsFunc
            , IndexInfo[] indexInfos)
        {
            List<EJ.DBColumn> changedColumns = new List<EJ.DBColumn>(changed_columns);

            oldTableName = oldTableName.ToLower();
            newTableName = newTableName.ToLower();

            //先判断表明是否更改
            if (oldTableName != newTableName)
            {
                //更改表名
                database.ExecSqlString("EXEC sp_rename '" + oldTableName + "', '" + newTableName + "'");
            }


            //获取那个索引存在了
            var existIndexed = checkIfIdxChanged( database , newTableName , indexInfos);
            
            dropTableIndex(database, newTableName, existIndexed.ToArray());

            foreach (var column in deletedColumns)
            {
                ChangeColumnHandler.HandleDelete(database, newTableName, column);
            }

            //将取消主键的列放前面处理
            if (true)
            {
                var column = changedColumns.FirstOrDefault(m =>m.BackupChangedProperties["IsPKID"] != null && (bool)m.BackupChangedProperties["IsPKID"].OriginalValue == true);
                if(column != null && column.IsPKID == false)
                {
                    changedColumns.Remove(column);
                    changedColumns.Insert(0, column);
                }
            }

            foreach (var column in changedColumns)
            {
                ChangeColumnHandler.HandleChange(database, newTableName, column);
            }

            foreach (var column in addColumns)
            {
                ChangeColumnHandler.HandleNewColumn(database, newTableName, column);
            }

            if (  indexInfos != null && indexInfos.Length > 0)
            {
                foreach (var c in indexInfos)
                {
                    if (existIndexed.Contains(c))
                        continue;

                    createIndex(database, newTableName, c);
                }
            }
        }


        public void DeleteTable(EntityDB.IDatabaseService database, string tableName)
        {
            tableName = tableName.ToLower();
            string sql = "if exists(select id from sysobjects where name='" + tableName + "' and xtype='U') drop table [" + tableName + "]";
            database.ExecSqlString(sql);
        }
    }
}