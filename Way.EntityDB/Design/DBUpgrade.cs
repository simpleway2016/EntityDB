using Way.EntityDB.Design.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.IO.Compression;
using EJ;

namespace Way.EntityDB.Design
{
    public class ColumnType
    {
        static List<string> _supportTypes = new List<string>(new string[] {
                                            "varchar",
                                            "int",
                                            "image",
                                            "text",
                                            "mediumtext",
                                            "longtext",
                                            "smallint",
                                            "smalldatetime",
                                            "real",
                                            "datetime",
                                            "datetimezone",
                                             "date",
                                              "time",
                                            "float",
                                            "double",
                                            "bit",
                                            "decimal",
                                            "numeric",
                                            "bigint",
                                            "varbinary",
                                            "char",
                                            "timestamp",
                                            "jsonb",
        });
        /// <summary>
        /// 目前支持的数据库字段类型
        /// </summary>
        public static List<string> SupportTypes
        {
            get
            {
                return _supportTypes;
            }
        }
    }
    public class DBUpgrade
    {
        static byte[] UnGzip(byte[] zippedData)
        {
            MemoryStream ms = new MemoryStream(zippedData);
            GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Decompress);
            MemoryStream outBuffer = new MemoryStream();
            byte[] block = new byte[1024];
            while (true)
            {
                int bytesRead = compressedzipStream.Read(block, 0, block.Length);
                if (bytesRead <= 0)
                    break;
                else
                    outBuffer.Write(block, 0, bytesRead);
            }
            compressedzipStream.Close();
            return outBuffer.ToArray();
        }

        public static List<EntityDB.Design.Actions.Action> GetDatabaseActions(string designData)
        {
            var list = new List<EntityDB.Design.Actions.Action>();
            if (designData.IsNullOrEmpty())
                return list;

            byte[] bs;
            if (designData.StartsWith("\r\n"))
            {
                bs = System.Convert.FromBase64String(designData.Substring(2));
                bs = UnGzip(bs);
            }
            else
            {
                bs = System.Convert.FromBase64String(designData);
            }

            using (var dset = Newtonsoft.Json.JsonConvert.DeserializeObject<WayDataSet>(System.Text.Encoding.UTF8.GetString(bs)))
            {
                var assembly = typeof(Way.EntityDB.Design.Actions.CreateTableAction).GetTypeInfo().Assembly;
                var dtable = dset.Tables[0];
                var rows = dtable.Rows.OrderBy(m => (long)m["id"]).ToList();
                foreach( var datarow in rows)
                {
                    string actionType = datarow["type"].ToString();

                    string json = datarow["content"].ToString();

                    Type type = assembly.GetType($"Way.EntityDB.Design.Actions.{actionType}");
                    var actionItem = (EntityDB.Design.Actions.Action)Newtonsoft.Json.JsonConvert.DeserializeObject(json, type);
                    actionItem.ID = Convert.ToInt32(datarow["id"]);
                    list.Add(actionItem);
                }
            }
            return list;
        }

        static void ResetColumnId(List<WayDataRow> query, string tablename,int startRowIndex)
        {
            var assembly = typeof(Way.EntityDB.Design.Actions.CreateTableAction).GetTypeInfo().Assembly;
            string tblName = tablename;
            //记录最新的列名，对应的column对象
            Dictionary<string, DBColumn> columniDDict = new Dictionary<string, DBColumn>();

            for (int i = startRowIndex; i < query.Count; i++)
            {
                var datarow = query[i];
                var actionItem = dataRowToAction(assembly, datarow);

                if (actionItem is EntityDB.Design.Actions.ChangeTableAction changeAction)
                {
                    if(changeAction.OldTableName == tblName)
                    {
                        tblName = changeAction.NewTableName;
                    }
                    else
                    {
                        continue;
                    }

                    foreach (var column in changeAction.deletedColumns)
                    {
                        var originalColumn = columniDDict[column.Name];
                        column.m_notSendPropertyChanged = true;
                        column.id = originalColumn.id;
                        column.m_notSendPropertyChanged = false;
                        columniDDict.Remove(column.Name);
                    }

                    foreach (var column in changeAction.changedColumns)
                    {
                        var oldname = column.Name;
                        if (column.BackupChangedProperties.Any(m => m.Key == "Name"))
                            oldname = column.BackupChangedProperties["Name"].OriginalValue.ToString();

                        var originalColumn = columniDDict[oldname];
                        column.m_notSendPropertyChanged = true;
                        column.id = originalColumn.id;
                        column.m_notSendPropertyChanged = false;

                        columniDDict.Remove(oldname);
                        columniDDict[column.Name] = column;
                    }

                    foreach (var column in changeAction.newColumns)
                    {
                        columniDDict[column.Name] = column;
                    }
                }
                else if (actionItem is EntityDB.Design.Actions.CreateTableAction createAction)
                {
                    if (createAction.Table.Name == tblName)
                    {
                        foreach (var column in createAction.Columns)
                        {
                            columniDDict[column.Name] = column;
                        }
                    }
                }
                else if (actionItem is EntityDB.Design.Actions.DeleteTableAction delTblAction)
                {
                    if (delTblAction.TableName == tblName)
                        return;
                }

                datarow["content"] = actionItem.ToJsonString();
            }
        }

        static DBColumn[] FindAllColumnsForChangeTable(Assembly assembly, EntityDB.Design.Actions.ChangeTableAction changeAction, int changeActionRowId,List<WayDataRow> allRows)
        {
            List<EJ.DBColumn> allcolumns = new List<EJ.DBColumn>();

            //往上逆推，查找字段信息
            var datarows = allRows.Where(m => (long)m["id"] < changeActionRowId).OrderBy(m => (long)m["id"]).ToList();

            //先从后逆推出最早的CreateTableAction;
            var curTableName = changeAction.OldTableName;
            EntityDB.Design.Actions.CreateTableAction createTableAction = null;
            int startIndex = 0;
            for (int i = datarows.Count - 1; i>=0; i--)
            {
                var preAction = dataRowToAction(assembly, datarows[i]);
                if (preAction is EntityDB.Design.Actions.ChangeTableAction action)
                {
                    if(action.NewTableName == curTableName)
                    {
                        curTableName = action.OldTableName;
                    }
                }
                else if (preAction is EntityDB.Design.Actions.CreateTableAction caction)
                {
                    if(caction.Table.Name == curTableName)
                    {
                        curTableName = caction.Table.Name;
                        createTableAction = caction;
                        startIndex = i + 1;
                        break;
                    }
                }
            }

            if (createTableAction == null)
                throw new Exception($"表“{changeAction.NewTableName}”无法找到当初的建表行为");

            if (FindCreateTableActionAllColumns(assembly, createTableAction, datarows, startIndex) == false)
                throw new Exception($"表“{changeAction.NewTableName}”在查找表所有字段时，发现表被删除了");

            return createTableAction.Columns;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="createAction"></param>
        /// <param name="startIndex"></param>
        /// <returns>返回false，表示table后来删除了</returns>
        static bool FindCreateTableActionAllColumns(Assembly assembly, EntityDB.Design.Actions.CreateTableAction createAction,List<WayDataRow> query ,int startIndex)
        {
            for (int j = startIndex; j < query.Count; j++)
            {
                var nextAction = dataRowToAction(assembly, query[j]);
                if (nextAction is EntityDB.Design.Actions.ChangeTableAction)
                {
                    var changeAction = (EntityDB.Design.Actions.ChangeTableAction)nextAction;
                    if (changeAction.OldTableName == createAction.Table.Name)
                    {
                        createAction.Table.Name = changeAction.NewTableName;                        
                        createAction.IDXConfigs = changeAction.IDXConfigs;

                        List<DBColumn> nowColumns = new List<DBColumn>(createAction.Columns);

                        foreach( var delColumn in changeAction.deletedColumns )
                        {
                            var columnName = delColumn.Name;
                            if (delColumn.BackupChangedProperties.Any(m => m.Key == "Name"))
                                columnName = delColumn.BackupChangedProperties.FirstOrDefault(m => m.Key == "Name").Value.OriginalValue.ToString();

                            var index = nowColumns.FindIndex(m => m.Name == columnName);
                            if(index >= 0)
                            {
                                nowColumns.RemoveAt(index);
                            }
                        }

                        foreach (var column in changeAction.changedColumns)
                        {
                            var columnName = column.Name;
                            if (column.BackupChangedProperties.Any(m => m.Key == "Name"))
                                columnName = column.BackupChangedProperties.FirstOrDefault(m => m.Key == "Name").Value.OriginalValue.ToString();

                            var index = nowColumns.FindIndex(m => m.Name == columnName);
                            if (index >= 0)
                            {
                                nowColumns.RemoveAt(index);
                            }
                            nowColumns.Add(column);
                        }

                        foreach (var column in changeAction.newColumns)
                        {
                            nowColumns.Add(column);
                        }

                        createAction.Columns = nowColumns.ToArray();

                        query.RemoveAt(j);
                        j--;
                    }
                }
                else if (nextAction is EntityDB.Design.Actions.DeleteTableAction)
                {
                    var deleteAction = (EntityDB.Design.Actions.DeleteTableAction)nextAction;
                    if (deleteAction.TableName == createAction.Table.Name)
                    {
                        query.RemoveAt(j);
                        createAction.Table.Name = null;
                        break;
                    }
                }
            }

            if (createAction.Table.Name == null)
            {
                //表后来删除了
                return false;
            }
            return true;
        }

        public static void Upgrade(EntityDB.DBContext dbContext, string designData)
        {
            if (designData.IsNullOrEmpty())
                return;

            byte[] bs;
            if (designData.StartsWith("\r\n"))
            {
                bs = System.Convert.FromBase64String(designData.Substring(2));
                bs = UnGzip(bs);
            }
            else
            {
                bs = System.Convert.FromBase64String(designData);
            }
            using (var dset = Newtonsoft.Json.JsonConvert.DeserializeObject<WayDataSet>(System.Text.Encoding.UTF8.GetString(bs)))
            {
                if (dbContext == null)
                    return;

                bs = null;
                EntityDB.IDatabaseService db = dbContext.Database;
                EntityDB.DatabaseType dbType = dbContext.DatabaseType;

                IDatabaseDesignService dbservice = EntityDB.Design.DBHelper.CreateDatabaseDesignService(dbType);
                dbservice.CreateEasyJobTable(db);

                var dbconfig = db.ExecSqlString("select contentConfig from __wayeasyjob").ToString().ToJsonObject<DataBaseConfig>();
                if (string.IsNullOrEmpty(dbconfig.DatabaseGuid) == false && dbconfig.DatabaseGuid != dset.DataSetName)
                    throw new Exception("此结构脚本并不是对应此数据库");


                var dtable = dset.Tables[0];
                int currentRowId = 0;
                try
                {
                    var query = dtable.Rows.Where(m => (long)m["id"] > dbconfig.LastUpdatedID).OrderBy(m => (long)m["id"]).ToList();

                    if (query.Count > 0)
                    {
                        var allquery = dtable.Rows.OrderBy(m => (long)m["id"]).ToList();
                        var assembly = typeof(Way.EntityDB.Design.Actions.CreateTableAction).GetTypeInfo().Assembly;
                        //因为通过cs导入结构后，column的id发生变化，所以需要重新统一一下column的id
                        for (int i = 0; i < allquery.Count; i++)
                        {
                            var datarow = allquery[i];
                            currentRowId = Convert.ToInt32(datarow["id"]);
                            var actionItem = dataRowToAction(assembly, datarow);

                            if (actionItem is EntityDB.Design.Actions.CreateTableAction createAction)
                            {
                                ResetColumnId(allquery, createAction.Table.Name, i);
                            }
                        }

                        int? lastid = Convert.ToInt32(query.Last()["id"]);                       
                        db.DBContext.BeginTransaction();
                        for(int i = 0; i < query.Count; i ++)
                        {
                            var datarow = query[i];
                            currentRowId = Convert.ToInt32(datarow["id"]);
                            var actionItem = dataRowToAction(assembly, datarow);

                            if (actionItem is EntityDB.Design.Actions.ChangeTableAction)
                            {
                                var changeAction = (EntityDB.Design.Actions.ChangeTableAction)actionItem;
                                
                                changeAction._getColumnsFunc = () => {
                                    return FindAllColumnsForChangeTable(assembly, changeAction, currentRowId, dtable.Rows).ToList();
                                };
                            }
                            else if(actionItem is EntityDB.Design.Actions.CreateTableAction createAction)
                            {
                                //往下查找表的变更
                                var ret = FindCreateTableActionAllColumns(assembly , createAction , query , i+1);

                                if(ret == false)
                                {
                                    //表后来删除了
                                    query.RemoveAt(i);
                                    i--;
                                    continue;
                                }
                            }

                            actionItem.Invoke(db);

                        }

                        SetLastUpdateID(lastid.Value, dset.DataSetName, db);
                        db.DBContext.CommitTransaction();
                    }
                }
                catch(Exception ex)
                {
                    db.DBContext.RollbackTransaction();
                    throw new Exception("发生错误，最后执行的id：" + currentRowId , ex);
                }
            }
        }

        static EntityDB.Design.Actions.Action dataRowToAction(Assembly assembly, WayDataRow datarow)
        {
            string actionType = datarow["type"].ToString();
            string json = datarow["content"].ToString();

            Type type = assembly.GetType($"Way.EntityDB.Design.Actions.{actionType}");
            var actionItem = (EntityDB.Design.Actions.Action)Newtonsoft.Json.JsonConvert.DeserializeObject(json, type);
            return actionItem;
        }

        public static void SetLastUpdateID(object actionid, string databaseGuid, EntityDB.IDatabaseService db)
        {
            if (string.IsNullOrEmpty(databaseGuid))
                throw new Exception("Database Guid can not be empty");
            var dbconfig = db.ExecSqlString("select contentConfig from __wayeasyjob").ToString().ToJsonObject<DataBaseConfig>();
            dbconfig.LastUpdatedID = Convert.ToInt32(actionid);
            dbconfig.DatabaseGuid = databaseGuid;

            var data = new EntityDB.CustomDataItem("__wayeasyjob", null, null);
            data.SetValue("contentConfig", dbconfig.ToJsonString());
            db.Update(data,null);
        }
    }
}