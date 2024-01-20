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
using System.Linq.Expressions;
using TestDB;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Cryptography;
using FllowOrderSystem.DBModels;

namespace Way.EntityDB.Test
{ 
    [TestClass]
    public class NormalTest
    {
        string ConStr = "server=.\\sqlexpress;uid=sa;pwd=123456;Database=test";

        [TestMethod]
        public void CopyValue()
        {
            var data1 = new TestDB.User() { 
            Birthday = DateTime.Now,
             IntColumn1 = 3,
              Name = "test"
            };

            var data2 = new TestDB.User();
            data1.CopyValueTo(data2,false);

            if (data1.Birthday != data2.Birthday || data1.IntColumn1 != data2.IntColumn1 || data1.Name != data2.Name)
                throw new Exception("error");
        }

        [TestMethod]
        public void InsertUpdateDelete()
        {
            using ( var db = new TestDB.DB.Test(ConStr , DatabaseType.SqlServer) )
            using ( var tran = db.BeginTransaction() )
            {
                for (int i = 0; i < 10; i++)
                {
                    var user = new User();
                    user.Name = "test_user" + i;
                    user.Birthday = DateTime.Now;
                    user.IntColumn1 = i;
                    db.Insert(user);
                }

                //var ar = (from m in db.User
                //          select new User
                //          {
                //              Name = m.Name
                //          }).ToArray();

                var list = db.User.Where(m => m.Name.StartsWith("test_user")).ToArray();
                if (list.Length < 10)
                    throw new Exception("数量不对");

                for (int i = 0; i < 10; i++)
                {
                    if (list.Any(m => m.Name == "test_user" + i && m.Birthday != null && m.IntColumn1 == i) == false)
                        throw new Exception("数据错误");
                }

                for (int i = 0; i < 10; i++)
                {
                    var user = list.FirstOrDefault(m => m.Name == "test_user" + i && m.Birthday != null && m.IntColumn1 == i);
                    user.Name = "test_user" + (i+10);
                    user.IntColumn1 = i + 10;
                    db.Update(user);
                }

                list = db.User.Where(m => m.Name.StartsWith("test_user")).ToArray();
                for (int i = 0; i < 10; i++)
                {
                    if (list.Any(m => m.Name == "test_user" + (i+10) && m.Birthday != null && m.IntColumn1 == (i+10)) == false)
                        throw new Exception("数据错误");
                }

                for (int i = 0; i < 10; i++)
                {
                    var user = list[i];
                    user.SetValue<User>(m => m.IntColumn1 == m.IntColumn1 + 2);
                    db.Update(user);
                }

                list = db.User.Where(m => m.Name.StartsWith("test_user")).ToArray();
                for (int i = 0; i < 10; i++)
                {
                    if (list.Any(m => m.Name == "test_user" + (i + 10) && m.Birthday != null && m.IntColumn1 == (i + 12)) == false)
                        throw new Exception("数据错误");
                }

                var syslog = new TestDB.SysLog();
                syslog.UserId = list[0].id;
                syslog.Time = DateTime.Now;
                syslog.SystemPath = "my sys path";
                syslog.SysId = "sys id";
                db.Insert(syslog);

                var usrlog = new TestDB.UserLog();
                usrlog.UserId = list[1].id;
                usrlog.Time = DateTime.Now;
                usrlog.PeopleName = "my People Name";
                db.Insert(usrlog);

                var admlog = new TestDB.AdminLog();
                admlog.UserId = list[2].id;
                admlog.Time = DateTime.Now;
                admlog.AdminId = "my ad id";
                db.Insert(admlog);

                var logs = db.Log.ToArray();
                if(logs.Any(m=>m is TestDB.SysLog) == false)
                    throw new Exception("数据错误");
                if (logs.Any(m => m is TestDB.UserLog) == false)
                    throw new Exception("数据错误");
                if (logs.Any(m => m is TestDB.AdminLog) == false)
                    throw new Exception("数据错误");

                logs = db.Log.Include(m => m.User).ToArray();
                if(logs[0].User == null)
                    throw new Exception("数据错误");

                var syslogs = db.SysLog.Include(m => m.User).ToArray();
                if (syslogs[0].User == null)
                    throw new Exception("数据错误");

                //测试删除

                db.Delete(db.User);

                if (db.Log.Count() > 0)
                    throw new Exception("没有级联删除");
            }
        }

        [TestMethod]
        public void 乐观锁()
        {
            using (var db = new TestDB.DB.Test(ConStr, DatabaseType.SqlServer))
            {
                db.Delete(db.User.Where(m=>m.Name.StartsWith("lock_test")));

                for(int i = 0; i < 50; i ++)
                {
                    var user = new TestDB.User { 
                        Name = "lock_test",
                        IntColumn1 = 0
                    };
                    db.Insert(user);
                }

                var count = db.User.Where(m => m.Name.StartsWith("lock_test") && m.IntColumn1 == 0).Count();
                if(count < 50)
                    throw new Exception("数量不对");
            }

            

            int unSuccessCount = 0;
            int SuccessCount = 0;
            //并发
            Parallel.For(0, 300, (index) => {
                using (var db = new TestDB.DB.Test(ConStr, DatabaseType.SqlServer))
                {
                    while (true)
                    {
                        var user = db.User.FirstOrDefault(m => m.Name.StartsWith("lock_test") && m.IntColumn1 == 0);
                        if (user == null)
                        {
                            Interlocked.Increment(ref unSuccessCount);
                            return;
                        }

                        user.IntColumn1 = index +1;
                        if( db.Update(user, m => m.IntColumn1 == 0 && m.id == user.id) > 0 )
                        {
                            Interlocked.Increment(ref SuccessCount);
                            return;
                        }
                    }
                }
            });

            using (var db = new TestDB.DB.Test(ConStr, DatabaseType.SqlServer))
            {
                var count = db.User.Where(m => m.Name.StartsWith("lock_test") && m.IntColumn1 > 0).Count();
                if (count < 50)
                    throw new Exception("数量不对");

                count = (from m in db.User
                             group m by m.IntColumn1 into g
                             select g.Key).Count();

                if (count < 50)
                    throw new Exception("数量不对");
            }

            if (SuccessCount != 50)
                throw new Exception("成功数量不对");

            if (unSuccessCount != 250)
                throw new Exception("失败数量不对");
        }
    }


}
