using Way.EntityDB.Design.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.IO.Compression;

namespace Way.EntityDB.Design
{
    public class ColumnType
    {
        static List<string> _supportTypes = new List<string>(new string[] {
                                            "varchar",
                                            "int",
                                            "image",
                                            "text",
                                            "smallint",
                                            "smalldatetime",
                                            "real",
                                            "datetime",
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
                                            "timestamp", });
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
                        int? lastid = Convert.ToInt32(query.Last()["id"]);
                        var assembly = typeof(Way.EntityDB.Design.Actions.CreateTableAction).GetTypeInfo().Assembly;
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
                                    List<EJ.DBColumn> allcolumns = new List<EJ.DBColumn>();

                                    //往上逆推，查找字段信息
                                    var datarows = dtable.Rows.Where(m => (long)m["id"] < currentRowId).OrderByDescending(m => (long)m["id"]).ToList();
                                    var curTableName = changeAction.OldTableName;
                                    List<int> deletedColumnids = new List<int>();

                                    foreach (var preRow in datarows )
                                    {
                                        var preAction = dataRowToAction(assembly, preRow);
                                        if(preAction is EntityDB.Design.Actions.CreateTableAction)
                                        {
                                            var tableAction = preAction as EntityDB.Design.Actions.CreateTableAction;
                                            if (tableAction.Table.Name != curTableName)
                                                continue;
                                            else
                                            {
                                                foreach( var c in tableAction.Columns )
                                                {
                                                    if(allcolumns.Any(m=>m.id == c.id) == false)
                                                    {
                                                        allcolumns.Add(c);
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                        else if (preAction is EntityDB.Design.Actions.ChangeTableAction)
                                        {
                                            var preChangeAction = preAction as EntityDB.Design.Actions.ChangeTableAction;
                                            if (preChangeAction.NewTableName != curTableName)
                                                continue;
                                            else
                                            {
                                                curTableName = preChangeAction.OldTableName;
                                                foreach (var c in preChangeAction.newColumns)
                                                {
                                                    if (deletedColumnids.Contains(c.id.Value) == false && allcolumns.Any(m => m.id == c.id) == false)
                                                    {
                                                        allcolumns.Add(c);
                                                    }
                                                }
                                                foreach (var c in preChangeAction.changedColumns)
                                                {
                                                    if (deletedColumnids.Contains(c.id.Value) == false && allcolumns.Any(m => m.id == c.id) == false)
                                                    {
                                                        allcolumns.Add(c);
                                                    }
                                                }
                                                foreach (var c in preChangeAction.deletedColumns)
                                                {
                                                    deletedColumnids.Add(c.id.Value);
                                                }
                                            }
                                        }
                                    }
                                    return allcolumns;
                                };
                            }
                            else if(actionItem is EntityDB.Design.Actions.CreateTableAction)
                            {
                                //往下查找表的变更
                                var createAction = (EntityDB.Design.Actions.CreateTableAction)actionItem;
                                var curTableName = createAction.Table.Name;

                                for(int j = i + 1; j < query.Count; j ++)
                                {
                                    var nextAction = dataRowToAction(assembly, query[j]);
                                    if(nextAction is EntityDB.Design.Actions.ChangeTableAction)
                                    {
                                        var changeAction = (EntityDB.Design.Actions.ChangeTableAction)nextAction;
                                        if(changeAction.OldTableName == curTableName)
                                        {
                                            curTableName = changeAction.NewTableName;
                                            createAction.Columns = (from m in createAction.Columns
                                                                    where changeAction.deletedColumns.Any(n => n.id == m.id) == false
                                                                    && changeAction.changedColumns.Any(n => n.id == m.id) == false
                                                                    select m).ToArray();

                                            createAction.Columns = createAction.Columns.Concat(changeAction.changedColumns).ToArray();
                                            createAction.Columns = createAction.Columns.Concat(changeAction.newColumns).ToArray();
                                            createAction.IDXConfigs = changeAction.IDXConfigs;
                                            query.RemoveAt(j);
                                            j--;
                                            continue;
                                        }
                                    }
                                    else if (nextAction is EntityDB.Design.Actions.DeleteTableAction)
                                    {
                                        var deleteAction = (EntityDB.Design.Actions.DeleteTableAction)nextAction;
                                        if(deleteAction.TableName == curTableName)
                                        {
                                            query.RemoveAt(j);
                                            curTableName = null;
                                            break;
                                        }
                                    }
                                }

                                if(curTableName == null)
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