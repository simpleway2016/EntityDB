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
using System.Data.Common;
using Way.EntityDB.ExpressionParsers;

namespace Way.EntityDB.Test
{

    [TestClass]
    public class ExpressionParserTest
    {
        [TestMethod]
        public void TestExpression1()
        {
            Expression<Func<TradeSystem.DBModels.Position, bool>> condition = m => m.id == null;
            using (var db = new TradeSystem.DBModels.DB.TradeSystemDB("data source='file:memdb2?mode=memory&cache=shared'", DatabaseType.Sqlite))
            {
                var cmd = db.Database.CreateCommand("select 1");

                ExpressionParserRoute expressionParserRoute = new ExpressionParserRoute(s => $"[{s}]");
                var parser = expressionParserRoute.GetExpressionParser(condition.Body);
                var ret = parser.Parse(condition.Body, cmd, null,false);
                if (ret != "[id] is null")
                    throw new Exception("结果错误");
            }
        }

        [TestMethod]
        public void TestExpression2()
        {
            Expression<Func<TradeSystem.DBModels.Position, bool>> condition = m => m.id == m.id + 1 && m.StopLoss > 3;
            using (var db = new TradeSystem.DBModels.DB.TradeSystemDB("data source='file:memdb2?mode=memory&cache=shared'", DatabaseType.Sqlite))
            {
                var cmd = db.Database.CreateCommand("select 1");

                ExpressionParserRoute expressionParserRoute = new ExpressionParserRoute(s => $"[{s}]");
                var parser = expressionParserRoute.GetExpressionParser(condition.Body);
                var ret = parser.Parse(condition.Body, cmd, null, false);
                if (ret != "([id]=[id]+1 and [stoploss]>3)")
                    throw new Exception("结果错误");
            }
        }

        [TestMethod]
        public void TestExpression3()
        {
            var p = 1;
            var p2 = 3;
            Expression<Func<TradeSystem.DBModels.Position, bool>> condition = m => m.id == m.id + p && m.StopLoss > p2;
            using (var db = new TradeSystem.DBModels.DB.TradeSystemDB("data source='file:memdb2?mode=memory&cache=shared'", DatabaseType.Sqlite))
            {
                var cmd = db.Database.CreateCommand("select 1");

                ExpressionParserRoute expressionParserRoute = new ExpressionParserRoute(s => $"[{s}]");
                var parser = expressionParserRoute.GetExpressionParser(condition.Body);
                var ret = parser.Parse(condition.Body, cmd, null, false);
                if (ret != "([id]=[id]+1 and [stoploss]>3)")
                    throw new Exception("结果错误");
            }
        }

        [TestMethod]
        public void TestExpression4()
        {
            var p = new Position();
            p.id = 1;
            var p2 = 3;
            Expression<Func<TradeSystem.DBModels.Position, bool>> condition = m => m.id == m.id + p.id.GetValueOrDefault() && m.StopLoss <= p2;
            using (var db = new TradeSystem.DBModels.DB.TradeSystemDB("data source='file:memdb2?mode=memory&cache=shared'", DatabaseType.Sqlite))
            {
                var cmd = db.Database.CreateCommand("select 1");

                ExpressionParserRoute expressionParserRoute = new ExpressionParserRoute(s => $"[{s}]");
                var parser = expressionParserRoute.GetExpressionParser(condition.Body);
                var ret = parser.Parse(condition.Body, cmd, null, false);
                if (ret != "([id]=[id]+@w_0 and [stoploss]<=3)")
                    throw new Exception("结果错误");
            }
        }

        [TestMethod]
        public void TestExpression5()
        {
            var p = new Position();
            p.id = 1;
            var p2 = 3;
            Expression<Func<TradeSystem.DBModels.Position, bool>> condition = m => m.Symbol.Contains("abc");
            using (var db = new TradeSystem.DBModels.DB.TradeSystemDB("data source='file:memdb2?mode=memory&cache=shared'", DatabaseType.Sqlite))
            {
                var cmd = db.Database.CreateCommand("select 1");

                ExpressionParserRoute expressionParserRoute = new ExpressionParserRoute(s => $"[{s}]");
                var parser = expressionParserRoute.GetExpressionParser(condition.Body);
                var ret = parser.Parse(condition.Body, cmd, null, false);
                if (ret != "[symbol] like @w_0")
                    throw new Exception("结果错误");
            }
        }

        [TestMethod]
        public void TestExpression6()
        {
            Expression<Func<TradeSystem.DBModels.Position, bool>> condition = m => m.id + 100 > 200;
            using (var db = new TradeSystem.DBModels.DB.TradeSystemDB("data source='file:memdb2?mode=memory&cache=shared'", DatabaseType.Sqlite))
            {
                var cmd = db.Database.CreateCommand("select 1");

                ExpressionParserRoute expressionParserRoute = new ExpressionParserRoute(s => $"[{s}]");
                var parser = expressionParserRoute.GetExpressionParser(condition.Body);
                var ret = parser.Parse(condition.Body, cmd, null, false);
                if (ret != "[id]+100>200")
                    throw new Exception("结果错误");
            }
        }

        [TestMethod]
        public void TestExpression7()
        {
            double p = 2.0;
            double p2 = 3.0;
            Expression<Func<TradeSystem.DBModels.Position, bool>> condition = m => m.id + (int)p > (int)p2;
            using (var db = new TradeSystem.DBModels.DB.TradeSystemDB("data source='file:memdb2?mode=memory&cache=shared'", DatabaseType.Sqlite))
            {
                var cmd = db.Database.CreateCommand("select 1");

                ExpressionParserRoute expressionParserRoute = new ExpressionParserRoute(s => $"[{s}]");
                var parser = expressionParserRoute.GetExpressionParser(condition.Body);
                var ret = parser.Parse(condition.Body, cmd, null, false);
                if (ret != "[id]+2>3")
                    throw new Exception("结果错误");
            }
        }

        [TestMethod]
        public void TestExpression8()
        {
            int p = 2;
            Expression<Func<TradeSystem.DBModels.Position, bool>> condition = m => m.id == p && m.MoneyAccountId == null;
            using (var db = new TradeSystem.DBModels.DB.TradeSystemDB("data source='file:memdb2?mode=memory&cache=shared'", DatabaseType.Sqlite))
            {
                var cmd = db.Database.CreateCommand("select 1");

                ExpressionParserRoute expressionParserRoute = new ExpressionParserRoute(s => $"[{s}]");
                var parser = expressionParserRoute.GetExpressionParser(condition.Body);
                var ret = parser.Parse(condition.Body, cmd, null, false);
                if (ret != "([id]=2 and [moneyaccountid] is null)")
                    throw new Exception("结果错误");
            }
        }

        [TestMethod]
        public void TestExpression9()
        {
            Expression<Func<TradeSystem.DBModels.Position, bool>> condition = m => m.id == 1 && m.Type == MarketOrder_TypeEnum.ContractMarketOrder;
            using (var db = new TradeSystem.DBModels.DB.TradeSystemDB("data source='file:memdb2?mode=memory&cache=shared'", DatabaseType.Sqlite))
            {
                var cmd = db.Database.CreateCommand("select 1");

                ExpressionParserRoute expressionParserRoute = new ExpressionParserRoute(s => $"[{s}]");
                var parser = expressionParserRoute.GetExpressionParser(condition.Body);
                var ret = parser.Parse(condition.Body, cmd, null, false);
                if (ret != "([id]=1 and [type]=1049600)")
                    throw new Exception("结果错误");
            }
        }

        [TestMethod]
        public void TestExpression10()
        {
            Expression<Func<TradeSystem.DBModels.Position, bool>> condition = m => m.id == 1 && (int)m.Type == (int)MarketOrder_TypeEnum.ContractMarketOrder;
            using (var db = new TradeSystem.DBModels.DB.TradeSystemDB("data source='file:memdb2?mode=memory&cache=shared'", DatabaseType.Sqlite))
            {
                var cmd = db.Database.CreateCommand("select 1");

                ExpressionParserRoute expressionParserRoute = new ExpressionParserRoute(s => $"[{s}]");
                var parser = expressionParserRoute.GetExpressionParser(condition.Body);
                var ret = parser.Parse(condition.Body, cmd, null, false);
                if (ret != "([id]=1 and [type]=1049600)")
                    throw new Exception("结果错误");
            }
        }

        [TestMethod]
        public void TestExpression11()
        {
            Expression<Func<TradeSystem.DBModels.Position, bool>> condition = m => m.id == 1 && (int)m.Type != (int)MarketOrder_TypeEnum.ContractMarketOrder;
            using (var db = new TradeSystem.DBModels.DB.TradeSystemDB("data source='file:memdb2?mode=memory&cache=shared'", DatabaseType.Sqlite))
            {
                var cmd = db.Database.CreateCommand("select 1");

                ExpressionParserRoute expressionParserRoute = new ExpressionParserRoute(s => $"[{s}]");
                var parser = expressionParserRoute.GetExpressionParser(condition.Body);
                var ret = parser.Parse(condition.Body, cmd, null, false);
                if (ret != "([id]=1 and [type]<>1049600)")
                    throw new Exception("结果错误");
            }
        }

        [TestMethod]
        public void TestExpression12()
        {
            Expression<Func<TradeSystem.DBModels.Position, bool>> condition = m => m.id > 1 && m.id >= 2 && (m.id < 3 || m.id <= 5);
            using (var db = new TradeSystem.DBModels.DB.TradeSystemDB("data source='file:memdb2?mode=memory&cache=shared'", DatabaseType.Sqlite))
            {
                var cmd = db.Database.CreateCommand("select 1");

                ExpressionParserRoute expressionParserRoute = new ExpressionParserRoute(s => $"[{s}]");
                var parser = expressionParserRoute.GetExpressionParser(condition.Body);
                var ret = parser.Parse(condition.Body, cmd, null, false);
                if (ret != "(([id]>1 and [id]>=2) and ([id]<3 or [id]<=5))")
                    throw new Exception("结果错误");
            }
        }

        [TestMethod]
        public void TestExpression13()
        {
            Expression<Func<TradeSystem.DBModels.Position, bool>> condition = m => m.id == m.id * 2 && (m.id == m.id / 3 || m.TradeCoinAmount == m.TradeCoinAmount - 8);
            using (var db = new TradeSystem.DBModels.DB.TradeSystemDB("data source='file:memdb2?mode=memory&cache=shared'", DatabaseType.Sqlite))
            {
                var cmd = db.Database.CreateCommand("select 1");

                ExpressionParserRoute expressionParserRoute = new ExpressionParserRoute(s => $"[{s}]");
                var parser = expressionParserRoute.GetExpressionParser(condition.Body);
                var ret = parser.Parse(condition.Body, cmd, null, false);
                if (ret != "([id]=[id]*2 and ([id]=[id]/3 or [tradecoinamount]=[tradecoinamount]-8))")
                    throw new Exception("结果错误");
            }
        }


        [TestMethod]
        public void TestExpression14()
        {
            Expression<Func<TradeSystem.DBModels.Position, bool>> condition = m => m.IsLocked == false || m.IsLocked == true;
            using (var db = new TradeSystem.DBModels.DB.TradeSystemDB("data source='file:memdb2?mode=memory&cache=shared'", DatabaseType.Sqlite))
            {
                var cmd = db.Database.CreateCommand("select 1");

                ExpressionParserRoute expressionParserRoute = new ExpressionParserRoute(s => $"[{s}]");
                var parser = expressionParserRoute.GetExpressionParser(condition.Body);
                var ret = parser.Parse(condition.Body, cmd, null, false);
                if (ret != "([islocked]=@w_0 or [islocked]=@w_1)")
                    throw new Exception("结果错误");
            }
        }
        [TestMethod]
        public void TestExpression15()
        {
            decimal p = 3;
            Expression<Func<TradeSystem.DBModels.Position, bool>> condition = m => m.CommodityQuantity <= -p;
            using (var db = new TradeSystem.DBModels.DB.TradeSystemDB("data source='file:memdb2?mode=memory&cache=shared'", DatabaseType.Sqlite))
            {
                var cmd = db.Database.CreateCommand("select 1");

                ExpressionParserRoute expressionParserRoute = new ExpressionParserRoute(s => $"[{s}]");
                var parser = expressionParserRoute.GetExpressionParser(condition.Body);
                var ret = parser.Parse(condition.Body, cmd, null, false);
                if (ret != "[commodityquantity]<=-3")
                    throw new Exception("结果错误");
            }
        }

        [TestMethod]
        public void TestExpression16()
        {
            decimal p = 3;
            Expression<Func<TradeSystem.DBModels.Position, bool>> condition = m => m.CommodityQuantity <= -m.TradeCoinAmount;
            using (var db = new TradeSystem.DBModels.DB.TradeSystemDB("data source='file:memdb2?mode=memory&cache=shared'", DatabaseType.Sqlite))
            {
                var cmd = db.Database.CreateCommand("select 1");

                ExpressionParserRoute expressionParserRoute = new ExpressionParserRoute(s => $"[{s}]");
                var parser = expressionParserRoute.GetExpressionParser(condition.Body);
                var ret = parser.Parse(condition.Body, cmd, null, false);
                if (ret != "[commodityquantity]<=-[tradecoinamount]")
                    throw new Exception("结果错误");
            }
        }

        [TestMethod]
        public void TestExpression17()
        {
            decimal p = 3;
            Expression<Func<TradeSystem.DBModels.Position, bool>> condition = m => m.CloseTime <= DateTime.Now.AddMonths(-6);
            using (var db = new TradeSystem.DBModels.DB.TradeSystemDB("data source='file:memdb2?mode=memory&cache=shared'", DatabaseType.Sqlite))
            {
                var cmd = db.Database.CreateCommand("select 1");

                ExpressionParserRoute expressionParserRoute = new ExpressionParserRoute(s => $"[{s}]");
                var parser = expressionParserRoute.GetExpressionParser(condition.Body);
                var ret = parser.Parse(condition.Body, cmd, null, false);
                if (ret != "[closetime]<=@w_0")
                    throw new Exception("结果错误");
            }
        }

        [TestMethod]
        public void TestExpression18()
        {
            decimal p = 3;
            Expression<Func<TradeSystem.DBModels.Position, bool>> condition = m => m.CloseType == Position_CloseTypeEnum.StopProfitLoss || m.Commodity.Contains("BCC");
            using (var db = new TradeSystem.DBModels.DB.TradeSystemDB("data source='file:memdb2?mode=memory&cache=shared'", DatabaseType.Sqlite))
            {
                var cmd = db.Database.CreateCommand("select 1");

                ExpressionParserRoute expressionParserRoute = new ExpressionParserRoute(s => $"[{s}]");
                var parser = expressionParserRoute.GetExpressionParser(condition.Body);
                var ret = parser.Parse(condition.Body, cmd, null, false);
                if (ret != "([closetype]=3 or [commodity] like @w_0)")
                    throw new Exception("结果错误");
            }
        }

        [TestMethod]
        public void TestExpression19()
        {
            decimal p = 3;
            Expression<Func<TradeSystem.DBModels.Position, bool>> condition = m => m.CloseType == (m.CloseType | Position_CloseTypeEnum.StopProfitLoss) || m.CloseType == Position_CloseTypeEnum.StopProfitLoss;
            using (var db = new TradeSystem.DBModels.DB.TradeSystemDB("data source='file:memdb2?mode=memory&cache=shared'", DatabaseType.Sqlite))
            {
                var cmd = db.Database.CreateCommand("select 1");

                ExpressionParserRoute expressionParserRoute = new ExpressionParserRoute(s => $"[{s}]");
                var parser = expressionParserRoute.GetExpressionParser(condition.Body);
                var ret = parser.Parse(condition.Body, cmd, null, false);
                if (ret != "([closetype]=[closetype] | 3 or [closetype]=3)")
                    throw new Exception("结果错误");
            }
        }

        [TestMethod]
        public void TestExpression20()
        {
            decimal p = 3;
            Expression<Func<TradeSystem.DBModels.Position, bool>> condition = m => m.CloseType == (m.CloseType & Position_CloseTypeEnum.StopProfitLoss) && m.CloseType == Position_CloseTypeEnum.StopProfitLoss;
            using (var db = new TradeSystem.DBModels.DB.TradeSystemDB("data source='file:memdb2?mode=memory&cache=shared'", DatabaseType.Sqlite))
            {
                var cmd = db.Database.CreateCommand("select 1");

                ExpressionParserRoute expressionParserRoute = new ExpressionParserRoute(s => $"[{s}]");
                var parser = expressionParserRoute.GetExpressionParser(condition.Body);
                var ret = parser.Parse(condition.Body, cmd, null, false);
                if (ret != "([closetype]=[closetype] & 3 and [closetype]=3)")
                    throw new Exception("结果错误");
            }
        }

        [TestMethod]
        public void TestExpression21()
        {
            Expression<Func<TradeSystem.DBModels.Position, bool>> condition = m => m.CloseType == ~m.CloseType;
            using (var db = new TradeSystem.DBModels.DB.TradeSystemDB("data source='file:memdb2?mode=memory&cache=shared'", DatabaseType.Sqlite))
            {
                var cmd = db.Database.CreateCommand("select 1");

                ExpressionParserRoute expressionParserRoute = new ExpressionParserRoute(s => $"[{s}]");
                var parser = expressionParserRoute.GetExpressionParser(condition.Body);
                var ret = parser.Parse(condition.Body, cmd, null, false);
                if (ret != "[closetype]=~[closetype]")
                    throw new Exception("结果错误");
            }
        }

        [TestMethod]
        public void TestExpression22()
        {
            decimal p = 3;
            Expression<Func<TradeSystem.DBModels.Position, bool>> condition = m => m.MoneyAccountId == null;
            using (var db = new TradeSystem.DBModels.DB.TradeSystemDB("data source='file:memdb2?mode=memory&cache=shared'", DatabaseType.Sqlite))
            {
                var cmd = db.Database.CreateCommand("select 1");

                ExpressionParserRoute expressionParserRoute = new ExpressionParserRoute(s => $"[{s}]");
                var parser = expressionParserRoute.GetExpressionParser(condition.Body);
                var ret = parser.Parse(condition.Body, cmd, null, true);
                if (ret != "[moneyaccountid]=null")
                    throw new Exception("结果错误");
            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            DateTime? emptyTime = null;
            using (var db = new TradeSystem.DBModels.DB.TradeSystemDB("data source='file:memdb2?mode=memory&cache=shared'", DatabaseType.Sqlite))
            {
                var data = new Position();
                data.IsLocked = true;
                data.MoneyAccountId = 1;
                data.CreateTime = DateTime.Now;
                data.TradeCoin = "USDT";
                data.CloseType = Position_CloseTypeEnum.StopProfitLoss;
                db.Insert(data);

                data.CommodityQuantity = 3;
                data.TradeCoinAmount = 999;
                data.SetValue<Position>(m => m.CommodityQuantity == m.CommodityQuantity + 100 && 
                m.MoneyAccountId == null && m.CreateTime == emptyTime
                && m.TradeCoinAmount == m.TradeCoinAmount + 200 && m.CloseType == (m.CloseType | Position_CloseTypeEnum.SysClose));
                var ret = db.Update(data, m => m.id == data.id && data.CommodityQuantity > -1 && data.CommodityQuantity + 100 >= 0 && m.IsLocked == true && m.TradeCoin.Contains("SD") &&
                m.TradeCoin.StartsWith("US") && m.TradeCoin.EndsWith("DT"));

                data = db.Position.FirstOrDefault(m => m.id == data.id);
                if (data.CommodityQuantity != 100 || data.CreateTime != null || data.MoneyAccountId != null || data.TradeCoinAmount != 200 || data.CloseType != (Position_CloseTypeEnum.StopProfitLoss| Position_CloseTypeEnum.SysClose))
                {
                    throw new Exception("结果错误");
                }
            }
        }
    }

}
