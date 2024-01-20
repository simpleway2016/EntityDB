using EJ;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Way.EntityDB;
using Way.EntityDB.Design;
using Way.EntityDB.Design.Services;
using Way.Lib;
using Way.Lib.ScriptRemoting;

namespace Way.EJServer
{
    public class ImportCSFileHandler : Way.Lib.ScriptRemoting.ICustomHttpHandler
    {
        public void Handle(string originalUrl, HttpConnectInformation connectInfo, ref bool handled)
        {
            if (originalUrl.Contains("/ImportCSFileHandler.aspx") == false)
                return;
            handled = true;

            if (connectInfo.Session["user"] == null || ((EJ.User)connectInfo.Session["user"]).Role.GetValueOrDefault().HasFlag(EJ.User_RoleEnum.数据库设计师) == false)
                return;
            try
            {
                int projectid = Convert.ToInt32(connectInfo.Request.Query["projectid"]);
          
                byte[] bs = new byte[connectInfo.Request.ContentLength];
                connectInfo.Request.Body.Read(bs, 0, bs.Length);
                ImportDesign(projectid, bs, connectInfo);

                connectInfo.Response.Write("ok\r\n");
            }
            catch(Exception ex)
            {
                connectInfo.Response.Write($"{ex.Message}\r\n");
            }
            
        }

        public bool ImportDesign(int projectid, byte[] csFileContent, HttpConnectInformation connectInfo)
        {
            var user = ((EJ.User)connectInfo.Session["user"]);
            using (MemoryStream ms = new MemoryStream(csFileContent))
            using (StreamReader sr = new StreamReader(ms, Encoding.UTF8))
            {
                StringBuilder content = new StringBuilder();
                bool bufferStarted = false;
                while (true)
                {
                    var line = sr.ReadLine();
                    if (line == null)
                        break;
                    if (line.StartsWith("<design>*/"))
                        break;
                    if (bufferStarted)
                    {
                        content.Append(line);
                    }
                    if (line.StartsWith("/*<design>"))
                    {
                        bufferStarted = true;
                    }
                }
                //result.Append(\"\\r\\n\");

                StringBuilder actioncontent = new StringBuilder();
                bufferStarted = false;
                ms.Position = 0;
                while (true)
                {
                    var line = sr.ReadLine().Trim();
                    if (line == null)
                        break;
                    if (line.StartsWith("return result.ToString()"))
                        break;
                    if (bufferStarted)
                    {
                        var index = line.IndexOf("result.Append(");
                        line = line.Substring(index + "result.Append(".Length + 1);
                        line = line.Substring(0, line.LastIndexOf("\""));
                        actioncontent.Append(line);
                    }
                    if (line.StartsWith("result.Append(\"\\r\\n\");"))
                    {
                        bufferStarted = true;
                    }
                }

                var bs = Convert.FromBase64String(content.ToString());
                bs = CodeBuilder.UnGzip(bs);
                var designData = Encoding.UTF8.GetString(bs).FromJson<DesignData>();

                bs = Convert.FromBase64String(actioncontent.ToString());
                bs = CodeBuilder.UnGzip(bs);
                var dset = Encoding.UTF8.GetString(bs).FromJson<WayDataSet>();

                int oldDatabaseid;
                using (EJDB_Check db = new EJDB_Check())
                {
                    if (db.Databases.Any(m => m.Name == designData.Database.Name))
                        throw new Exception("数据库名称有重复");

                    try
                    {
                        db.BeginTransaction();

                        oldDatabaseid = designData.Database.id.Value;
                        designData.Database.ProjectID = projectid;
                        designData.Database.conStr = "";
                        db.Insert(designData.Database);

                        
                        

                        #region Modules
                        foreach (var module in designData.Modules)
                        {
                            module.BackupChangedProperties.Add("id", new DataValueChangedItem() { OriginalValue = module.id.Value });
                            module.DatabaseID = designData.Database.id;
                            db.Insert(module);
                        }

                        foreach (var module in designData.Modules)
                        {
                            if (module.parentID != null && module.parentID != 0)
                            {
                                var parentModule = designData.Modules.FirstOrDefault(m => (int)m.BackupChangedProperties["id"].OriginalValue == module.parentID.Value);
                                if (parentModule != null)
                                {
                                    module.parentID = parentModule.id;
                                    db.Update(module);
                                }
                            }
                        }
                        #endregion

                        #region Tables
                        foreach (var table in designData.Tables)
                        {
                            table.BackupChangedProperties.Add("id", new DataValueChangedItem() { OriginalValue = table.id.Value });
                            table.DatabaseID = designData.Database.id;
                            db.Insert(table);
                        }
                        #endregion

                        #region Columns
                        foreach (var column in designData.DBColumns)
                        {
                            column.BackupChangedProperties.Add("id", new DataValueChangedItem() { OriginalValue = column.id.Value });
                            column.TableID = designData.Tables.FirstOrDefault(m => (int)m.BackupChangedProperties["id"].OriginalValue == column.TableID.Value).id;
                            db.Insert(column);
                        }
                        #endregion

                        #region IDXIndexes
                        foreach (var idxindex in designData.IDXIndexes)
                        {
                            idxindex.BackupChangedProperties.Add("id", new DataValueChangedItem() { OriginalValue = idxindex.id.Value });
                            idxindex.TableID = designData.Tables.FirstOrDefault(m => (int)m.BackupChangedProperties["id"].OriginalValue == idxindex.TableID.Value).id;
                            db.Insert(idxindex);
                        }
                        #endregion

                        #region classproperties
                        foreach (var classpro in designData.classproperties)
                        {
                            classpro.BackupChangedProperties.Add("id", new DataValueChangedItem() { OriginalValue = classpro.id.Value });
                            if(classpro.tableid != null)
                                classpro.tableid = designData.Tables.FirstOrDefault(m => (int)m.BackupChangedProperties["id"].OriginalValue == classpro.tableid.Value).id;
                            if (classpro.foreignkey_tableid != null)
                                classpro.foreignkey_tableid = designData.Tables.FirstOrDefault(m => (int)m.BackupChangedProperties["id"].OriginalValue == classpro.foreignkey_tableid.Value).id;
                            db.Insert(classpro);
                        }
                        #endregion

                        #region DBDeleteConfigs
                        foreach (var delconfig in designData.DBDeleteConfigs)
                        {
                            delconfig.BackupChangedProperties.Add("id", new DataValueChangedItem() { OriginalValue = delconfig.id.Value });
                            delconfig.TableID = designData.Tables.FirstOrDefault(m => (int)m.BackupChangedProperties["id"].OriginalValue == delconfig.TableID.Value).id;
                            delconfig.RelaColumID = designData.DBColumns.FirstOrDefault(m => (int)m.BackupChangedProperties["id"].OriginalValue == delconfig.RelaColumID.Value).id;
                            delconfig.RelaTableID = designData.Tables.FirstOrDefault(m => (int)m.BackupChangedProperties["id"].OriginalValue == delconfig.RelaTableID.Value).id;
                            db.Insert(delconfig);
                        }
                        #endregion

                        #region TableInModules
                        foreach (var tableInModule in designData.TableInModules)
                        {
                            tableInModule.BackupChangedProperties.Add("id", new DataValueChangedItem() { OriginalValue = tableInModule.id.Value });
                            tableInModule.TableID = designData.Tables.FirstOrDefault(m => (int)m.BackupChangedProperties["id"].OriginalValue == tableInModule.TableID.Value).id;
                            tableInModule.ModuleID = designData.Modules.FirstOrDefault(m => (int)m.BackupChangedProperties["id"].OriginalValue == tableInModule.ModuleID.Value).id;
                            db.Insert(tableInModule);
                        }
                        #endregion


                        var assembly = typeof(Way.EntityDB.Design.Actions.CreateTableAction).GetTypeInfo().Assembly;
                        var dtable = dset.Tables[0];
                        var rows = dtable.Rows.OrderBy(m => (long)m["id"]).ToList();
                        foreach (var datarow in rows)
                        {

                            EJ.DesignHistory action = new EJ.DesignHistory()
                            {
                                ActionId = Convert.ToInt32(datarow["id"]),
                                Content = datarow["content"].ToString(),
                                DatabaseId = designData.Database.id,
                                Type = datarow["type"].ToString()
                            };
                            db.Insert(action);
                        }


                        db.Insert(new EJ.SysLog()
                        {
                            UserId = ((EJ.User)connectInfo.Session["user"]).id,
                            Type = "从cs导入数据库设计模型",
                            DatabaseId = designData.Database.id,
                            Time = DateTime.Now,

                        });

                        //try
                        //{
                        //    //变更数据库类型
                        //    IDatabaseDesignService dbservice = Way.EntityDB.Design.DBHelper.CreateDatabaseDesignService((Way.EntityDB.DatabaseType)(int)designData.Database.dbType);
                        //    dbservice.Create(designData.Database);
                        //    db.Update(designData.Database);

                        //    //更新到现在的数据结构                         

                        //    var invokeDB = Way.EntityDB.Design.DBHelper.CreateInvokeDatabase(designData.Database);
                        //    var designDataStr = CodeBuilder.GetDesignData(db, designData.Database);
                        //    DBUpgrade.Upgrade(invokeDB.DBContext, "\r\n" + designDataStr);
                        //}
                        //catch
                        //{

                        //}

                        db.Insert(new DBPower { 
                            DatabaseID = designData.Database.id,
                            UserID = user.id,
                            Role = DBPower_RoleEnum.Owner
                        });

                        db.CommitTransaction();

                    }
                    catch (Exception)
                    {
                        db.RollbackTransaction();
                        throw;
                    }

                    return true;


                }
            }
        }
    }
}
