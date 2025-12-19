using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Way.EntityDB.Design;
using Way.EntityDB.Design.Actions;
using System;
using Way.EntityDB.Design.Services;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.Data.SqlClient;

namespace Way.EntityDB.Test
{ 
    [TestClass]
    public class TestAllDatabaseType
    {
        const string SqlServerConstr = "Data Source=8.219.191.213;User ID=sa;Password=NAM121R7E2Y2198UMCVKX6Iq;Initial Catalog=TestDB";
        const string PostgreSqlConStr = "Server=47.250.182.178;Port=15432;UserId=postgres;Password=gis;Database=TestDB;";
        const string MySqlConStr = "server=192.168.136.137;User Id=user1;password=User.123456;Database=TestDB";

        [TestMethod]
        public void ConnectionString_Check()
        {
            SqlConnectionStringBuilder conStrBuilder = new SqlConnectionStringBuilder("server=ETHAN-20171016H;uid=sa;pwd=123;Database=TestDB");
            conStrBuilder.InitialCatalog = "master";
            var constr = conStrBuilder.ToString();

            //Server=;Port=5432;UserId=;Password=;Database=;
            Npgsql.NpgsqlConnectionStringBuilder conStrBuilder2 = new Npgsql.NpgsqlConnectionStringBuilder("Server=localhost;Port=5432;UserId=sa;Password=1;Database=testDB;");
            conStrBuilder2.Database = null;
            constr = conStrBuilder2.ToString();

            Pomelo.Data.MySql.MySqlConnectionStringBuilder conStrBuilder3 = new Pomelo.Data.MySql.MySqlConnectionStringBuilder("server=locahost;User Id=sa;password=12;Database=testDB");
            conStrBuilder3.Database = null;
            constr = conStrBuilder3.ToString();

            //server=;User Id=;password=;Database=
        }

       

        [TestMethod]
        public void TestAll()
        {
            IDatabaseDesignService dbservice;
            IDatabaseService db;

            var dbfile = $"{AppDomain.CurrentDomain.BaseDirectory}test.sqlite";
            if (File.Exists(dbfile))
                File.Delete(dbfile);

            Test(new EJ.Databases()
            {
                conStr = $"data source=\"{dbfile}\"",
                Name = "TestingDb",
                dbType = EJ.Databases_dbTypeEnum.Sqlite,
            });
            dbservice = EntityDB.Design.DBHelper.CreateDatabaseDesignService(DatabaseType.Sqlite);
            db = EntityDB.DBContext.CreateDatabaseService($"data source=\"{dbfile}\"", EntityDB.DatabaseType.Sqlite);
            dbservice.GetCurrentTableNames(db);
            dbservice.GetCurrentColumns(db, "test3");
            dbservice.GetCurrentIndexes(db, "test3");

            Test(new EJ.Databases()
            {
                conStr = new SqlConnectionStringBuilder(SqlServerConstr) { InitialCatalog = "TestingDb" }.ToString(),
                Name = "testingdb",
                dbType = EJ.Databases_dbTypeEnum.SqlServer,
            });
            dbservice = EntityDB.Design.DBHelper.CreateDatabaseDesignService(DatabaseType.SqlServer);
            db = EntityDB.DBContext.CreateDatabaseService(new SqlConnectionStringBuilder(SqlServerConstr) { InitialCatalog = "TestingDb" }.ToString(), EntityDB.DatabaseType.SqlServer);
            dbservice.GetCurrentTableNames(db);
            dbservice.GetCurrentColumns(db, "test3");
            dbservice.GetCurrentIndexes(db, "test3");

            Test(new EJ.Databases()
            {
                conStr = new Npgsql.NpgsqlConnectionStringBuilder(PostgreSqlConStr) { Database = "TestingDb" }.ToString(),
                Name = "testingdb",
                dbType = EJ.Databases_dbTypeEnum.PostgreSql,
            });
            dbservice = EntityDB.Design.DBHelper.CreateDatabaseDesignService(DatabaseType.PostgreSql);
            db = EntityDB.DBContext.CreateDatabaseService(new Npgsql.NpgsqlConnectionStringBuilder(PostgreSqlConStr) { Database = "TestingDb" }.ToString(), EntityDB.DatabaseType.PostgreSql);
            object result = dbservice.GetCurrentTableNames(db);
            result = dbservice.GetCurrentColumns(db, "test3");
            result = dbservice.GetCurrentIndexes(db, "test3");

           
        }

        [TestMethod]
        public void TestSqlServer()
        {

            Test(new EJ.Databases()
            {
                conStr = new SqlConnectionStringBuilder(SqlServerConstr) { InitialCatalog = "TestingDb" }.ToString(),
                Name = "testingdb",
                dbType = EJ.Databases_dbTypeEnum.SqlServer,
            } , "a1");

            Test(new EJ.Databases()
            {
                conStr = new SqlConnectionStringBuilder(SqlServerConstr) { InitialCatalog = "TestingDb" }.ToString(),
                Name = "testingdb",
                dbType = EJ.Databases_dbTypeEnum.SqlServer,
            }, "a2");
        }

        [TestMethod]
        public void TestPostgreSql()
        {

            Test(new EJ.Databases()
            {
                conStr = new Npgsql.NpgsqlConnectionStringBuilder(PostgreSqlConStr) { Database = "TestingDb" }.ToString(),
                Name = "testingdb",
                dbType = EJ.Databases_dbTypeEnum.PostgreSql,
            }, "a1");

            Test(new EJ.Databases()
            {
                conStr = new Npgsql.NpgsqlConnectionStringBuilder(PostgreSqlConStr) { Database = "TestingDb" }.ToString(),
                Name = "testingdb",
                dbType = EJ.Databases_dbTypeEnum.PostgreSql,
            }, "a2");
        }

        static void Test(EJ.Databases dbconfig,string schema = null)
        { 
            IDatabaseDesignService dbservice = EntityDB.Design.DBHelper.CreateDatabaseDesignService((EntityDB.DatabaseType)(int)dbconfig.dbType);
            EntityDB.IDatabaseService db = null;
          
            if(schema == "a1")
                dbservice.Drop(dbconfig);
            try
            {
                dbservice.Create(dbconfig , schema);
                db = EntityDB.DBContext.CreateDatabaseService(dbconfig.conStr, schema, (EntityDB.DatabaseType)(int)dbconfig.dbType);
                dbservice.CreateEasyJobTable(db);

                List<EJ.DBColumn> allColumns = new List<EJ.DBColumn>();
                List<EntityDB.Design.IndexInfo> allindexes = new List<EntityDB.Design.IndexInfo>();

                #region CreateTable
                if (true)
                {
                    EJ.DBTable tableUser = new EJ.DBTable();
                    tableUser.Name = "User";

                    allColumns.Add(new EJ.DBColumn()
                    {
                        IsPKID = true,
                        CanNull = false,
                        Name = "Id",
                        dbType = "int",
                        IsAutoIncrement = true,
                    });
                    allColumns.Add(new EJ.DBColumn()
                    {
                        Name = "Name",
                        dbType = "varchar",
                        length = "50",
                        defaultValue = "a'b,c"
                    });

                    CreateTableAction _CreateTableAction = new CreateTableAction(0, tableUser, allColumns.ToArray(), allindexes.ToArray());
                    _CreateTableAction.Invoke(db);

                    DeleteTableAction _delaction = new DeleteTableAction(0,tableUser.Name);
                    _delaction.Invoke(db);

                    //再次创建
                    _CreateTableAction.Invoke(db);
                }
                #endregion

                allColumns.Clear();
                allindexes.Clear();
                allColumns.Add(new EJ.DBColumn()
                {
                    IsPKID = true,
                    CanNull = false,
                    Name = "Id",
                    dbType = "int",
                    IsAutoIncrement = true,
                });
                allColumns.Add(new EJ.DBColumn()
                {
                    Name = "C1",
                    dbType = "varchar",
                    length = "50",
                    defaultValue = "a'b,c"
                });
                allColumns.Add(new EJ.DBColumn()
                {
                    Name = "C2",
                    dbType = "varchar",
                    length = "50",
                    defaultValue = "abc"
                });
                allColumns.Add(new EJ.DBColumn()
                {
                    Name = "C3",
                    dbType = "int",
                    defaultValue = "8"
                });
                allColumns.Add(new EJ.DBColumn()
                {
                    Name = "Text1",
                    dbType = "text",
                });
                //索引
                allindexes.Add(new EntityDB.Design.IndexInfo()
                {
                    ColumnNames = new string[] { "C1" },
                    IsUnique = true,
                });
                allindexes.Add(new EntityDB.Design.IndexInfo()
                {
                    ColumnNames = new string[] { "C2", "C3" },
                });


                EJ.DBTable table = new EJ.DBTable();
                table.Name = "Test";

                #region CreateTable
                if (true)
                {
                    CreateTableAction _CreateTableAction = new CreateTableAction(0,table, allColumns.ToArray(), allindexes.ToArray());
                    _CreateTableAction.Invoke(db);

                    foreach (var c in allColumns)
                    {
                        c.ChangedProperties.Clear();
                        c.BackupChangedProperties.Clear();
                    }

                   // checkColumns(dbservice, db, table.Name, allColumns, allindexes);
                }
                #endregion


                #region ChangeTable1
                if (true)
                {
                    EJ.DBColumn[] newcolumns = new EJ.DBColumn[2];
                    newcolumns[0] = (new EJ.DBColumn()
                    {
                        Name = "N0",
                        dbType = "varchar",
                        length = "30",
                        defaultValue = "t'b"
                    });
                    newcolumns[1] = (new EJ.DBColumn()
                    {
                        Name = "N1",
                        dbType = "int",
                        defaultValue = "18"
                    });


                    EJ.DBColumn[] changedColumns = new EJ.DBColumn[2];
                    changedColumns[0] = allColumns.FirstOrDefault(m => m.Name == "C3");
                    changedColumns[0].Name = "C3_changed";
                    changedColumns[0].dbType = "varchar";
                    changedColumns[0].defaultValue = "1'a";
                    changedColumns[0].CanNull = false;
                    changedColumns[0].length = "88";


                    changedColumns[1] = allColumns.FirstOrDefault(m => m.Name == "Id");
                    changedColumns[1].IsAutoIncrement = false;
                    changedColumns[1].IsPKID = false;
                    changedColumns[1].CanNull = true;



                    EJ.DBColumn[] deletecolumns = new EJ.DBColumn[1];
                    deletecolumns[0] = allColumns.FirstOrDefault(m => m.Name == "C2");

                    var tableColumns = allColumns.ToList();

                    allColumns.Remove(deletecolumns[0]);

                    allindexes.Clear();
                    allindexes.Add(new EntityDB.Design.IndexInfo()
                    {
                        ColumnNames = new string[] { "N0", "C3_changed" },
                        IsUnique = true,
                        IsClustered = true
                    });

                    new ChangeTableAction(0,table.Name, "Test2", newcolumns, changedColumns, deletecolumns, ()=> tableColumns, allindexes.ToArray())
                    .Invoke(db);
                    table.Name = "Test2";
                    allColumns.AddRange(newcolumns);

                    foreach (var c in allColumns)
                    {
                        c.ChangedProperties.Clear();
                        c.BackupChangedProperties.Clear();
                    }
                    //checkColumns(dbservice, db, table.Name, allColumns, allindexes);
                }
                #endregion

                #region ChangeTable2
                if (true)
                {
                    EJ.DBColumn[] newcolumns = new EJ.DBColumn[0];
                    EJ.DBColumn[] changedColumns = new EJ.DBColumn[1];
                    changedColumns[0] = allColumns.FirstOrDefault(m => m.Name == "Id");
                    changedColumns[0].IsAutoIncrement = true;
                    changedColumns[0].IsPKID = true;
                    changedColumns[0].CanNull = false;


                    EJ.DBColumn[] deletecolumns = new EJ.DBColumn[0];

                    var tableColumns = allColumns.ToList();

                    new ChangeTableAction(0,table.Name, "Test3", newcolumns, changedColumns, deletecolumns, ()=>tableColumns, allindexes.ToArray())
                    .Invoke(db);
                    table.Name = "Test3";
                    allColumns.AddRange(newcolumns);

                    foreach (var c in allColumns)
                    {
                        c.ChangedProperties.Clear();
                        c.BackupChangedProperties.Clear();
                    }

                    //checkColumns(dbservice, db, table.Name, allColumns, allindexes);
                }
                #endregion
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (db != null)
                {
                    db.DBContext.Dispose();
                }

            }
        }

        static void checkColumns(IDatabaseDesignService design, IDatabaseService db, string table, List<EJ.DBColumn> allcolumns, List<EntityDB.Design.IndexInfo> allindex)
        {
            var columns = design.GetCurrentColumns(db, table.ToLower());
            var indexes = design.GetCurrentIndexes(db, table.ToLower());

            if (allcolumns.Count != columns.Count)
            {
                throw new Exception("column 数量不一致");
            }
            foreach (var column in allcolumns)
            {
                if (column.defaultValue == null)
                    column.defaultValue = "";

                var compareColumn = columns.FirstOrDefault(m => m.Name.ToLower() == column.Name.ToLower());
                if (compareColumn == null)
                    throw new Exception("找不到字段" + column.Name);

                if (compareColumn.defaultValue == null)
                    compareColumn.defaultValue = "";

                if (column.CanNull != compareColumn.CanNull)
                    throw new Exception($"column:{column.Name} CanNull 不一致");
                if (column.dbType != compareColumn.dbType)
                    throw new Exception($"column:{column.Name} dbType 不一致");
                if (column.defaultValue != compareColumn.defaultValue)
                    throw new Exception($"column:{column.Name} defaultValue 不一致 {column.defaultValue}  vs  {compareColumn.defaultValue}");
                if (column.IsAutoIncrement != compareColumn.IsAutoIncrement)
                    throw new Exception($"column:{column.Name} IsAutoIncrement 不一致");
                if (column.IsPKID != compareColumn.IsPKID)
                    throw new Exception($"column:{column.Name} IsPKID 不一致");
                if (column.dbType.Contains("char"))
                {
                    if (column.length != compareColumn.length)
                        throw new Exception($"column:{column.Name} length 不一致 {column.length}  vs  {compareColumn.length}");
                }
            }

            if (allindex.Count != indexes.Count)
            {
                throw new Exception("index 数量不一致");
            }
            foreach (var index in allindex)
            {
                if (indexes.Any(m => m.ColumnNames.OrderBy(n => n).ToArray().ToSplitString(",").ToLower() == index.ColumnNames.OrderBy(n => n).ToArray().ToSplitString(",").ToLower()) == false)
                    throw new Exception($"index:{index.Name} ColumnNames 不一致");
                if (indexes.Any(m => m.ColumnNames.OrderBy(n => n).ToArray().ToSplitString(",").ToLower() == index.ColumnNames.OrderBy(n => n).ToArray().ToSplitString(",").ToLower() && m.IsUnique == index.IsUnique) == false)
                    throw new Exception($"index:{index.Name} IsUnique 不一致");
            }
        }

    }
    static class MyExtensions
    {

        /// <summary>
        /// 功能和ToString一样，null会返回""
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToSafeString(this object obj)
        {
            if (obj == null)
                return "";
            return obj.ToString();
        }
        /// <summary>
        /// 功能和ToString一样，null或者空字符，会返回noneString
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="noneString"></param>
        /// <returns></returns>
        public static string ToSafeString(this object obj, string noneString)
        {
            if (obj == null)
                return noneString;
            string result = obj.ToString();
            if (result.Length == 0)
                return noneString;
            return result;
        }
      

        /// <summary>
        /// 每个对象逗号隔开
        /// </summary>
        /// <param name="arrs"></param>
        /// <returns></returns>
        public static string ToSplitString(this Array arrs)
        {
            return arrs.ToSplitString(",");
        }
        /// <summary>
        /// 用制定字符串联数组
        /// </summary>
        /// <param name="arrs"></param>
        /// <param name="splitchar">间隔字符</param>
        /// <returns></returns>
        public static string ToSplitString(this Array arrs, string splitchar)
        {
            StringBuilder result = new StringBuilder();
            foreach (object str in arrs)
            {
                if (result.Length > 0)
                    result.Append(splitchar);
                result.Append(str.ToString().Trim());
            }
            return result.ToString();
        }
        public static string ToSplitString(this Array arrs, string splitchar, string itemFormat)
        {
            StringBuilder result = new StringBuilder();
            foreach (object str in arrs)
            {
                if (result.Length > 0)
                    result.Append(splitchar);
                result.Append(string.Format(itemFormat, str));
            }
            return result.ToString();
        }
    }
    class MySqlServerEmptyDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("test");
            base.OnConfiguring(optionsBuilder);
        }
    }
    class MySqliteEmptyDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("test");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
