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
        public void test()
        {
            using (var db = new FllowOrderSystem.DBModels.DB.FllowOrders("Server=47.52.0.186;Port=5432;UserId=orders;Password=moniff61384e;Database=fllowordersystem;", DatabaseType.PostgreSql))
            {

            }
        }
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
