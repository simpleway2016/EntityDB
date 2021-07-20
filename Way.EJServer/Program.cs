using EJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Way.EntityDB;
using Way.EntityDB.Design.Actions;
using Way.EntityDB.Design.Services;
using Way.Lib.ScriptRemoting;

namespace Way.EJServer
{
    public class Program
    {
      
        public static void Main(string[] args)
        {
            HttpServer server = null;
            try
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                int port = 6069;
                if (args != null && args.Length > 0)
                {
                    port = Convert.ToInt32(args[0]);
                }
                
                Console.WriteLine($"server starting at port:{port}...");
                var webroot = $"{AppDomain.CurrentDomain.BaseDirectory}Port{port}";

                if (!System.IO.Directory.Exists(webroot))
                {
                    System.IO.Directory.CreateDirectory(webroot);
                }

                if (System.IO.File.Exists($"{webroot}/main.html") == false)
                {
                    System.IO.File.WriteAllText($"{webroot}/main.html", "<html><body controller=\"Way.EJServer.MainController\"></body></html>");
                }

                server = new HttpServer(new int[] { port }, webroot);
                Console.WriteLine($"Root:{server.Root}");

                server.RegisterHandler(new DownLoadCodeHandler());
                server.RegisterHandler(new DownLoadSimpleCodeHandler());
                server.RegisterHandler(new DownLoadVerySimpleCodeHandler());
                server.RegisterHandler(new DownloadTableDataHandler());
                server.RegisterHandler(new ImportDataHandler());
                server.RegisterHandler(new ImportCSFileHandler());

                server.UseHttps(new X509Certificate2(Way.Lib.PlatformHelper.GetAppDirectory() + "EJServerCert.pfx", "123456") , System.Security.Authentication.SslProtocols.None , true);
                Console.WriteLine($"use ssl EJServerCert.pfx");

                server.SessionTimeout = 60 * 24;


                //copy action table data
                //复制action表
                using (var db = new EJ.DB.easyjob($"Data Source=\"{webroot}/EasyJob.db\"" , DatabaseType.Sqlite))
                {
                    //判断是否有__action

                    bool isOldVersion = false;
                    try
                    {
                        db.Database.ExecSqlString("select count(*) from __action");
                        isOldVersion = true;
                    }
                    catch(Exception)
                    {

                    }

                    if (isOldVersion && db.DesignHistory.Count() == 0)
                    {
                        db.BeginTransaction();
                        try
                        {
                            db.Database.ExecuteReader((reader) =>
                            {
                                db.Insert(new DesignHistory() { 
                                    ActionId = Convert.ToInt32(reader["id"]),
                                    Type = (string)reader["type"],
                                    Content = (string)reader["content"],
                                    DatabaseId = Convert.ToInt32(reader["databaseid"]),
                                });
                                return true;
                            }, "select * from __action", null);
                            db.CommitTransaction();
                        }
                        catch (Exception)
                        {
                            db.RollbackTransaction();
                            throw;
                        }
                       
                    }

                }

                server.Start();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            while (true)
            {
                Console.Write("Web>");
                var line = Console.ReadLine();
                if(line == null)
                {
                    //是在后台运行的
                    while(true)
                    {
                        System.Threading.Thread.Sleep(10000000);
                    }
                }
                else if (line == "exit")
                    break;
            }
            server?.Stop();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

      
    }
}
