using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Way.EntityDB.Design.Services;
using Way.Lib.ScriptRemoting;

namespace Way.EJServer
{
    class DownLoadSimpleCodeHandler : Way.Lib.ScriptRemoting.ICustomHttpHandler
    {
        public void Handle(string originalUrl, HttpConnectInformation connectInfo, ref bool handled)
        {
            if (originalUrl.Contains("/DownLoadSimpleCodeHandler.aspx") == false)
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
                    if(database.dllPath == null || database.dllPath.StartsWith("{") == false)
                    {
                        database.dllPath = Newtonsoft.Json.JsonConvert.SerializeObject(new {
                            simple = connectInfo.Request.Query["filepath"],
                            db = "",
                        });
                    }
                    else
                    {
                        var json = (Newtonsoft.Json.Linq.JToken)Newtonsoft.Json.JsonConvert.DeserializeObject(database.dllPath);
                        json["simple"] = connectInfo.Request.Query["filepath"];
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

                    NamespaceCode codeNamespace = new NamespaceCode(database.NameSpace + ".Dtos");
                    codeNamespace.AddBeforeCode("//此代码由工具自动生成，请不要随意修改");
                    codeNamespace.AddUsing("System");
                    codeNamespace.AddUsing("System.Collections.Generic");
                    codeNamespace.AddUsing("System.Linq");
                    codeNamespace.AddUsing("System.Text");
                    codeNamespace.AddBeforeCode("");

                    foreach (var table in tables)
                    {
                        codeBuilder.BuildSimpleTable(db, codeNamespace, table);
                    }

                    bw.Write("code.cs");
                    byte[] bs = System.Text.Encoding.UTF8.GetBytes(codeNamespace.Build());
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
