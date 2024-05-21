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
using System.Xml;
using FllowOrderSystem.DBModels;
using Npgsql;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Dfd.Common.DBModels.DB;
using TradeSystem.DBModels;
using TestDB;
using System.Text.Json;
using TestDB.DB;

namespace Way.EntityDB.Test
{

    [TestClass]
    public class Test
    {
        [TestMethod]
        public void JsonTest()
        {
            var option = new System.Text.Json.JsonSerializerOptions();
            option.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;

            var dataitem = new TradeUser
            {
                TradeUserId = 2,
                Status = TradeUser_StatusEnum.Approved,
                NickName = "bac"
            };
            dataitem.ChangedProperties.Clear();

            var str0 = System.Text.Json.JsonSerializer.Serialize(new Dictionary<string, TradeUser> { { "a", dataitem } }, option);

            var str = System.Text.Json.JsonSerializer.Serialize(new[] { dataitem }, option);

            var item = System.Text.Json.JsonSerializer.Deserialize<TradeUser[]>(str);

            str = Newtonsoft.Json.JsonConvert.SerializeObject(dataitem, new Newtonsoft.Json.JsonSerializerSettings() { 
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
            });

            str = Newtonsoft.Json.JsonConvert.SerializeObject(dataitem);
        }



        [TestMethod]
        public void MemorySqliteTest()
        {
            try
            {
                using (var db = new TradeSystem.DBModels.DB.TradeSystemDB("data source='file:memdb2?mode=memory&cache=shared'", DatabaseType.Sqlite))
                {
                       var cddd = DBContext.InvokeCountAsync(db.MoneyAccount).GetAwaiter().GetResult();
                    int c = 2;
                    var user = new TradeSystem.DBModels.MoneyAccount();
                    user.SetValue(m => m.Balance == m.Balance + (decimal)c);
                    db.Update(user, m => m.Balance < -m.Id);
                }
            }
            catch (Exception ex)
            {
                var str = ex.ToString();
                throw;
            }
           
        }
    }
    public class ExtObj
    {
        public string Name { get; set; }
    }
    class TestItem:DataItem
    {

        private string _Name;
        public string Name
        {
            get => _Name;
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                }
            }
        }
    }

}
