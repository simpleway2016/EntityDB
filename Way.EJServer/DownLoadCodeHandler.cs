using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Way.EntityDB.Design.Services;
using Way.Lib.ScriptRemoting;

namespace Way.EJServer
{
    class DownLoadCodeHandler : Way.Lib.ScriptRemoting.ICustomHttpHandler
    {
        public void Handle(string originalUrl, HttpConnectInformation connectInfo, ref bool handled)
        {
            if (originalUrl.Contains("/DownloadDatabaseCode.aspx") == false)
                return;
            handled = true;
            try
            {
                if (connectInfo.Session["user"] == null )
                    throw new Exception("not arrow");

                int databaseid = Convert.ToInt32(connectInfo.Request.Query["databaseid"]);
                using (EJDB db = new EJDB())
                {

                    var database = db.Databases.FirstOrDefault(m => m.id == databaseid);
                    if (database.dllPath == null || database.dllPath.StartsWith("{") == false)
                    {
                        database.dllPath = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            db = connectInfo.Request.Query["filepath"],
                            simple = "",
                        });
                    }
                    else
                    {
                        var json = (Newtonsoft.Json.Linq.JToken)Newtonsoft.Json.JsonConvert.DeserializeObject(database.dllPath);
                        json["db"] = connectInfo.Request.Query["filepath"];
                        database.dllPath = json.ToString();
                    }
                    db.Update(database);

                    var tables = db.DBTable.Where(m => m.DatabaseID == databaseid).ToList();
                    System.IO.BinaryWriter bw = new System.IO.BinaryWriter(connectInfo.Response);
                    bw.Write("start");

                    var invokingDB = Way.EntityDB.Design.DBHelper.CreateInvokeDatabase(database);
                    IDatabaseDesignService dbservice = Way.EntityDB.Design.DBHelper.CreateDatabaseDesignService((Way.EntityDB.DatabaseType)(int)database.dbType);


                    bw.Write(1);
                    ICodeBuilder codeBuilder = new CodeBuilder();

                    NamespaceCode namespaceCode = new NamespaceCode(database.NameSpace);
                    NamespaceCode namespaceCode2 = new NamespaceCode(database.NameSpace + ".DB");
                    namespaceCode.AddUsing("System");
                    namespaceCode.AddUsing("Microsoft.EntityFrameworkCore");
                    namespaceCode.AddUsing("System.Collections.Generic");
                    namespaceCode.AddUsing("System.ComponentModel");
                    namespaceCode.AddUsing("System.Data");
                    namespaceCode.AddUsing("System.Linq");
                    namespaceCode.AddUsing("System.Text");
                    namespaceCode.AddUsing("System.ComponentModel.DataAnnotations");
                    namespaceCode.AddUsing("System.ComponentModel.DataAnnotations.Schema");
                    namespaceCode.AddUsing("Way.EntityDB.Attributes");
                    namespaceCode.AddUsing("System.Diagnostics.CodeAnalysis");
                    namespaceCode.AddBeforeCode("");
                    codeBuilder.BuilderDB(db, database, namespaceCode2, tables);

                    List<string> foreignKeys = new List<string>();
                    foreach (var table in tables)
                    {
                        codeBuilder.BuildTable(db, namespaceCode, table, foreignKeys);
                    }

                    bw.Write("code.cs");
                    string code = namespaceCode.Build() + "\r\n" + namespaceCode2.Build();
                    byte[] bs = System.Text.Encoding.UTF8.GetBytes(code);
                    bw.Write(bs.Length);
                    bw.Write(bs);

                    bw.Write(":end");
                }
            }
            catch(Exception ex)
            {
                new System.IO.BinaryWriter(connectInfo.Response).Write(ex.Message);
            }
        }
    }
}
