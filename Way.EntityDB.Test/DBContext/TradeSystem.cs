using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Way.EntityDB.Attributes;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;
using System.Collections;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace TradeSystem.DBModels
{
    public partial class MoneyAccount
    {
        [NotMapped]
        public bool TestName { get; set; }
    }
    /// <summary>用户资金表</summary>
    [TableConfig]
    [Table("moneyaccount")]
    [Way.EntityDB.DataItemJsonConverter]
    public partial class MoneyAccount : Way.EntityDB.DataItem
    {
        Int64 _UserId;
        /// <summary>用户id，0表示系统账户</summary>
        [DisallowNull]
        [Display(Name = "用户id，0表示系统账户")]
        [Column("userid")]
        public virtual Int64 UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                if ((_UserId != value))
                {
                    SendPropertyChanging("UserId", _UserId, value);
                    _UserId = value;
                    SendPropertyChanged("UserId");
                }
            }
        }
        Decimal _Balance = 0m;
        /// <summary>可用余额</summary>
        [DisallowNull]
        [Display(Name = "可用余额")]
        [Column("balance")]
        public virtual Decimal Balance
        {
            get
            {
                return _Balance;
            }
            set
            {
                if ((_Balance != value))
                {
                    SendPropertyChanging("Balance", _Balance, value);
                    _Balance = value;
                    SendPropertyChanged("Balance");
                }
            }
        }
        string? _Coin = "USDT";
        /// <summary>币种</summary>
        [MaxLength(50)]
        [Display(Name = "币种")]
        [Column("coin")]
        public virtual string? Coin
        {
            get
            {
                return _Coin;
            }
            set
            {
                if ((_Coin != value))
                {
                    SendPropertyChanging("Coin", _Coin, value);
                    _Coin = value;
                    SendPropertyChanged("Coin");
                }
            }
        }
        System.Nullable<Int64> _Id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int64> Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if ((_Id != value))
                {
                    SendPropertyChanging("Id", _Id, value);
                    _Id = value;
                    SendPropertyChanged("Id");
                }
            }
        }
        Decimal _Frozen = 0m;
        /// <summary>冻结金额</summary>
        [DisallowNull]
        [Display(Name = "冻结金额")]
        [Column("frozen")]
        public virtual Decimal Frozen
        {
            get
            {
                return _Frozen;
            }
            set
            {
                if ((_Frozen != value))
                {
                    SendPropertyChanging("Frozen", _Frozen, value);
                    _Frozen = value;
                    SendPropertyChanged("Frozen");
                }
            }
        }
        MoneyAccount_PositionTypeEnum _PositionType = (MoneyAccount_PositionTypeEnum)(1);
        /// <summary>持仓模式</summary>
        [DisallowNull]
        [Display(Name = "持仓模式")]
        [Column("positiontype")]
        public virtual MoneyAccount_PositionTypeEnum PositionType
        {
            get
            {
                return _PositionType;
            }
            set
            {
                if ((_PositionType != value))
                {
                    SendPropertyChanging("PositionType", _PositionType, value);
                    _PositionType = value;
                    SendPropertyChanged("PositionType");
                }
            }
        }
        System.Nullable<DateTime> _ExpireTime;
        /// <summary>过期时间</summary>
        [Display(Name = "过期时间")]
        [Column("expiretime")]
        public virtual System.Nullable<DateTime> ExpireTime
        {
            get
            {
                return _ExpireTime;
            }
            set
            {
                if ((_ExpireTime != value))
                {
                    SendPropertyChanging("ExpireTime", _ExpireTime, value);
                    _ExpireTime = value;
                    SendPropertyChanged("ExpireTime");
                }
            }
        }
        MoneyAccount_AccountTypeEnum _AccountType = (MoneyAccount_AccountTypeEnum)(1);
        /// <summary>账户类型</summary>
        [DisallowNull]
        [Display(Name = "账户类型")]
        [Column("accounttype")]
        public virtual MoneyAccount_AccountTypeEnum AccountType
        {
            get
            {
                return _AccountType;
            }
            set
            {
                if ((_AccountType != value))
                {
                    SendPropertyChanging("AccountType", _AccountType, value);
                    _AccountType = value;
                    SendPropertyChanged("AccountType");
                }
            }
        }
        Int32 _Priority = 0;
        /// <summary>
        /// 优先级
        /// 数值越大，优先级越高，如赠金，应该把优先级设高一点
        /// </summary>
        [DisallowNull]
        [Display(Name = "优先级  数值越大，优先级越高，如赠金，应该把优先级设高一点")]
        [Column("priority")]
        public virtual Int32 Priority
        {
            get
            {
                return _Priority;
            }
            set
            {
                if ((_Priority != value))
                {
                    SendPropertyChanging("Priority", _Priority, value);
                    _Priority = value;
                    SendPropertyChanged("Priority");
                }
            }
        }
        Boolean _IsRobot = false;
        /// <summary>机器人账号</summary>
        [DisallowNull]
        [Display(Name = "机器人账号")]
        [Column("isrobot")]
        public virtual Boolean IsRobot
        {
            get
            {
                return _IsRobot;
            }
            set
            {
                if ((_IsRobot != value))
                {
                    SendPropertyChanging("IsRobot", _IsRobot, value);
                    _IsRobot = value;
                    SendPropertyChanged("IsRobot");
                }
            }
        }
        MoneyAccount_FuncEnum _Func = (MoneyAccount_FuncEnum)(0);
        /// <summary>账户功能</summary>
        [DisallowNull]
        [Display(Name = "账户功能")]
        [Column("func")]
        public virtual MoneyAccount_FuncEnum Func
        {
            get
            {
                return _Func;
            }
            set
            {
                if ((_Func != value))
                {
                    SendPropertyChanging("Func", _Func, value);
                    _Func = value;
                    SendPropertyChanged("Func");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
        public virtual void SetValue(System.Linq.Expressions.Expression<Func<MoneyAccount, bool>> exp)
        {
            base.SetValue<MoneyAccount>(exp);
        }
    }
    public enum MoneyAccount_PositionTypeEnum : int
    {
        /// <summary>逐仓模式</summary>
        SingleMode = 1,
        /// <summary>全仓模式</summary>
        FullMode = 2
    }
    public enum MoneyAccount_AccountTypeEnum : int
    {
        /// <summary>资金（钱包）账户</summary>
        Asset = 1 << 1,
        /// <summary>币币账户</summary>
        Exchange = 1 << 2,
        /// <summary>信用账户</summary>
        Credit = 1 << 3,
        /// <summary>云挖矿账户</summary>
        Mining = 1 << 4,
        /// <summary>赠金</summary>
        Bonus = 1 << 5,
        /// <summary>合约交易账户</summary>
        Trade = 1 << 10 | 1,
    }
    public enum MoneyAccount_FuncEnum : int
    {
        None = 0,
        /// <summary>支付手续费</summary>
        PayFee = 1 << 1,
        /// <summary>交易</summary>
        Trade = 1 << 2,
        /// <summary>提现</summary>
        Withdraw = 1 << 3,
        /// <summary>划转</summary>
        Transfer = 1 << 4
    }
    [TableConfig]
    [Table("symbolinfo")]
    [Way.EntityDB.DataItemJsonConverter]
    public class SymbolInfo : Way.EntityDB.DataItem
    {
        System.Nullable<Int64> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int64> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id", _id, value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        string? _Name;
        /// <summary>全称</summary>
        [MaxLength(50)]
        [Display(Name = "全称")]
        [Column("name")]
        public virtual string? Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if ((_Name != value))
                {
                    SendPropertyChanging("Name", _Name, value);
                    _Name = value;
                    SendPropertyChanged("Name");
                }
            }
        }
        System.Nullable<Int32> _Sort = 0;
        /// <summary>排序，降序</summary>
        [Display(Name = "排序，降序")]
        [Column("sort")]
        public virtual System.Nullable<Int32> Sort
        {
            get
            {
                return _Sort;
            }
            set
            {
                if ((_Sort != value))
                {
                    SendPropertyChanging("Sort", _Sort, value);
                    _Sort = value;
                    SendPropertyChanged("Sort");
                }
            }
        }
        Boolean _Enable = true;
        /// <summary>是否可用</summary>
        [DisallowNull]
        [Display(Name = "是否可用")]
        [Column("enable")]
        public virtual Boolean Enable
        {
            get
            {
                return _Enable;
            }
            set
            {
                if ((_Enable != value))
                {
                    SendPropertyChanging("Enable", _Enable, value);
                    _Enable = value;
                    SendPropertyChanged("Enable");
                }
            }
        }
        string? _Commodity;
        /// <summary>
        /// 物品名称
        /// 如：BTC/USDT，物品名称就是BTC
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "物品名称 如：BTC/USDT，物品名称就是BTC")]
        [Column("commodity")]
        public virtual string? Commodity
        {
            get
            {
                return _Commodity;
            }
            set
            {
                if ((_Commodity != value))
                {
                    SendPropertyChanging("Commodity", _Commodity, value);
                    _Commodity = value;
                    SendPropertyChanged("Commodity");
                }
            }
        }
        string? _TradeCoin;
        /// <summary>
        /// 交易币种
        /// 如：BTC/USDT，交易币种就是USDT
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "交易币种 如：BTC/USDT，交易币种就是USDT")]
        [Column("tradecoin")]
        public virtual string? TradeCoin
        {
            get
            {
                return _TradeCoin;
            }
            set
            {
                if ((_TradeCoin != value))
                {
                    SendPropertyChanging("TradeCoin", _TradeCoin, value);
                    _TradeCoin = value;
                    SendPropertyChanged("TradeCoin");
                }
            }
        }
        string? _ShortName;
        /// <summary>简称</summary>
        [MaxLength(50)]
        [Display(Name = "简称")]
        [Column("shortname")]
        public virtual string? ShortName
        {
            get
            {
                return _ShortName;
            }
            set
            {
                if ((_ShortName != value))
                {
                    SendPropertyChanging("ShortName", _ShortName, value);
                    _ShortName = value;
                    SendPropertyChanged("ShortName");
                }
            }
        }
        SymbolInfo_ShowFlagEnum _ShowFlag = (SymbolInfo_ShowFlagEnum)(0);
        /// <summary>
        /// 显示标签
        /// 在哪些交易中，显示这个币对
        /// </summary>
        [DisallowNull]
        [Display(Name = "显示标签 在哪些交易中，显示这个币对")]
        [Column("showflag")]
        public virtual SymbolInfo_ShowFlagEnum ShowFlag
        {
            get
            {
                return _ShowFlag;
            }
            set
            {
                if ((_ShowFlag != value))
                {
                    SendPropertyChanging("ShowFlag", _ShowFlag, value);
                    _ShowFlag = value;
                    SendPropertyChanged("ShowFlag");
                }
            }
        }
        Int32 _QuantityDecimal;
        /// <summary>数量显示小数位</summary>
        [DisallowNull]
        [Display(Name = "数量显示小数位")]
        [Column("quantitydecimal")]
        public virtual Int32 QuantityDecimal
        {
            get
            {
                return _QuantityDecimal;
            }
            set
            {
                if ((_QuantityDecimal != value))
                {
                    SendPropertyChanging("QuantityDecimal", _QuantityDecimal, value);
                    _QuantityDecimal = value;
                    SendPropertyChanged("QuantityDecimal");
                }
            }
        }
        Int32 _PriceDecimal;
        /// <summary>价格显示小数位</summary>
        [DisallowNull]
        [Display(Name = "价格显示小数位")]
        [Column("pricedecimal")]
        public virtual Int32 PriceDecimal
        {
            get
            {
                return _PriceDecimal;
            }
            set
            {
                if ((_PriceDecimal != value))
                {
                    SendPropertyChanging("PriceDecimal", _PriceDecimal, value);
                    _PriceDecimal = value;
                    SendPropertyChanged("PriceDecimal");
                }
            }
        }
        Decimal _MinDealAmount;
        /// <summary>单笔最小交易额</summary>
        [DisallowNull]
        [Display(Name = "单笔最小交易额")]
        [Column("mindealamount")]
        public virtual Decimal MinDealAmount
        {
            get
            {
                return _MinDealAmount;
            }
            set
            {
                if ((_MinDealAmount != value))
                {
                    SendPropertyChanging("MinDealAmount", _MinDealAmount, value);
                    _MinDealAmount = value;
                    SendPropertyChanged("MinDealAmount");
                }
            }
        }
        System.Nullable<double> _CPriceScale;
        /// <summary>
        /// 限价单允许和当前市价的误差比
        /// 如：0.1,表示限价单可以在当前市价10%左右浮动
        /// </summary>
        [Display(Name = "限价单允许和当前市价的误差比 如：0.1,表示限价单可以在当前市价10%左右浮动")]
        [Column("cpricescale")]
        public virtual System.Nullable<double> CPriceScale
        {
            get
            {
                return _CPriceScale;
            }
            set
            {
                if ((_CPriceScale != value))
                {
                    SendPropertyChanging("CPriceScale", _CPriceScale, value);
                    _CPriceScale = value;
                    SendPropertyChanged("CPriceScale");
                }
            }
        }
        System.Nullable<Int64> _CreatedUserId;
        [Column("createduserid")]
        public virtual System.Nullable<Int64> CreatedUserId
        {
            get
            {
                return _CreatedUserId;
            }
            set
            {
                if ((_CreatedUserId != value))
                {
                    SendPropertyChanging("CreatedUserId", _CreatedUserId, value);
                    _CreatedUserId = value;
                    SendPropertyChanged("CreatedUserId");
                }
            }
        }
        string? _CreatedUserName;
        [MaxLength(50)]
        [Column("createdusername")]
        public virtual string? CreatedUserName
        {
            get
            {
                return _CreatedUserName;
            }
            set
            {
                if ((_CreatedUserName != value))
                {
                    SendPropertyChanging("CreatedUserName", _CreatedUserName, value);
                    _CreatedUserName = value;
                    SendPropertyChanged("CreatedUserName");
                }
            }
        }
        System.Nullable<DateTime> _CreatedTime;
        [Column("createdtime")]
        public virtual System.Nullable<DateTime> CreatedTime
        {
            get
            {
                return _CreatedTime;
            }
            set
            {
                if ((_CreatedTime != value))
                {
                    SendPropertyChanging("CreatedTime", _CreatedTime, value);
                    _CreatedTime = value;
                    SendPropertyChanged("CreatedTime");
                }
            }
        }
        System.Nullable<double> _FeeRate;
        /// <summary>手续费率</summary>
        [Display(Name = "手续费率")]
        [Column("feerate")]
        public virtual System.Nullable<double> FeeRate
        {
            get
            {
                return _FeeRate;
            }
            set
            {
                if ((_FeeRate != value))
                {
                    SendPropertyChanging("FeeRate", _FeeRate, value);
                    _FeeRate = value;
                    SendPropertyChanged("FeeRate");
                }
            }
        }
        double _MPriceScale = 0;
        /// <summary>允许市价波动率</summary>
        [DisallowNull]
        [Display(Name = "允许市价波动率")]
        [Column("mpricescale")]
        public virtual double MPriceScale
        {
            get
            {
                return _MPriceScale;
            }
            set
            {
                if ((_MPriceScale != value))
                {
                    SendPropertyChanging("MPriceScale", _MPriceScale, value);
                    _MPriceScale = value;
                    SendPropertyChanged("MPriceScale");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
        public virtual void SetValue(System.Linq.Expressions.Expression<Func<SymbolInfo, bool>> exp)
        {
            base.SetValue<SymbolInfo>(exp);
        }
    }
    public enum SymbolInfo_ShowFlagEnum : int
    {
        None = 0,
        /// <summary>币币交易</summary>
        Exchange = 1 << 0,
        /// <summary>合约交易</summary>
        Contract = 1 << 1,
    }
    /// <summary>设置止盈止损的历史</summary>
    [TableConfig]
    [Table("setstopplhistory")]
    [Way.EntityDB.DataItemJsonConverter]
    public class SetStopPLHistory : Way.EntityDB.DataItem
    {
        System.Nullable<Int64> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int64> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id", _id, value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        System.Nullable<Decimal> _StopProfit;
        /// <summary>止盈价格</summary>
        [Display(Name = "止盈价格")]
        [Column("stopprofit")]
        public virtual System.Nullable<Decimal> StopProfit
        {
            get
            {
                return _StopProfit;
            }
            set
            {
                if ((_StopProfit != value))
                {
                    SendPropertyChanging("StopProfit", _StopProfit, value);
                    _StopProfit = value;
                    SendPropertyChanged("StopProfit");
                }
            }
        }
        System.Nullable<Decimal> _StopLoss;
        /// <summary>止损价格</summary>
        [Display(Name = "止损价格")]
        [Column("stoploss")]
        public virtual System.Nullable<Decimal> StopLoss
        {
            get
            {
                return _StopLoss;
            }
            set
            {
                if ((_StopLoss != value))
                {
                    SendPropertyChanging("StopLoss", _StopLoss, value);
                    _StopLoss = value;
                    SendPropertyChanged("StopLoss");
                }
            }
        }
        System.Nullable<Decimal> _StopProfitAmount;
        /// <summary>止盈金额</summary>
        [Display(Name = "止盈金额")]
        [Column("stopprofitamount")]
        public virtual System.Nullable<Decimal> StopProfitAmount
        {
            get
            {
                return _StopProfitAmount;
            }
            set
            {
                if ((_StopProfitAmount != value))
                {
                    SendPropertyChanging("StopProfitAmount", _StopProfitAmount, value);
                    _StopProfitAmount = value;
                    SendPropertyChanged("StopProfitAmount");
                }
            }
        }
        System.Nullable<Decimal> _StopLossAmount;
        /// <summary>止损金额</summary>
        [Display(Name = "止损金额")]
        [Column("stoplossamount")]
        public virtual System.Nullable<Decimal> StopLossAmount
        {
            get
            {
                return _StopLossAmount;
            }
            set
            {
                if ((_StopLossAmount != value))
                {
                    SendPropertyChanging("StopLossAmount", _StopLossAmount, value);
                    _StopLossAmount = value;
                    SendPropertyChanged("StopLossAmount");
                }
            }
        }
        System.Nullable<Decimal> _PriceOnSetPL;
        /// <summary>设置止盈止损时的市价</summary>
        [Display(Name = "设置止盈止损时的市价")]
        [Column("priceonsetpl")]
        public virtual System.Nullable<Decimal> PriceOnSetPL
        {
            get
            {
                return _PriceOnSetPL;
            }
            set
            {
                if ((_PriceOnSetPL != value))
                {
                    SendPropertyChanging("PriceOnSetPL", _PriceOnSetPL, value);
                    _PriceOnSetPL = value;
                    SendPropertyChanged("PriceOnSetPL");
                }
            }
        }
        DateTime _CreateTime;
        [DisallowNull]
        [Column("createtime")]
        public virtual DateTime CreateTime
        {
            get
            {
                return _CreateTime;
            }
            set
            {
                if ((_CreateTime != value))
                {
                    SendPropertyChanging("CreateTime", _CreateTime, value);
                    _CreateTime = value;
                    SendPropertyChanged("CreateTime");
                }
            }
        }
        System.Nullable<Decimal> _MovingStopProfit;
        /// <summary>移动止盈</summary>
        [Display(Name = "移动止盈")]
        [Column("movingstopprofit")]
        public virtual System.Nullable<Decimal> MovingStopProfit
        {
            get
            {
                return _MovingStopProfit;
            }
            set
            {
                if ((_MovingStopProfit != value))
                {
                    SendPropertyChanging("MovingStopProfit", _MovingStopProfit, value);
                    _MovingStopProfit = value;
                    SendPropertyChanged("MovingStopProfit");
                }
            }
        }
        System.Nullable<Int64> _PositionId;
        [Column("positionid")]
        public virtual System.Nullable<Int64> PositionId
        {
            get
            {
                return _PositionId;
            }
            set
            {
                if ((_PositionId != value))
                {
                    SendPropertyChanging("PositionId", _PositionId, value);
                    _PositionId = value;
                    SendPropertyChanged("PositionId");
                }
            }
        }
        System.Nullable<Decimal> _StopProfitLossQuantity;
        /// <summary>止盈止损手数</summary>
        [Display(Name = "止盈止损手数")]
        [Column("stopprofitlossquantity")]
        public virtual System.Nullable<Decimal> StopProfitLossQuantity
        {
            get
            {
                return _StopProfitLossQuantity;
            }
            set
            {
                if ((_StopProfitLossQuantity != value))
                {
                    SendPropertyChanging("StopProfitLossQuantity", _StopProfitLossQuantity, value);
                    _StopProfitLossQuantity = value;
                    SendPropertyChanged("StopProfitLossQuantity");
                }
            }
        }
        System.Nullable<Decimal> _MovingStopProfitQuantity;
        /// <summary>移动止盈手数</summary>
        [Display(Name = "移动止盈手数")]
        [Column("movingstopprofitquantity")]
        public virtual System.Nullable<Decimal> MovingStopProfitQuantity
        {
            get
            {
                return _MovingStopProfitQuantity;
            }
            set
            {
                if ((_MovingStopProfitQuantity != value))
                {
                    SendPropertyChanging("MovingStopProfitQuantity", _MovingStopProfitQuantity, value);
                    _MovingStopProfitQuantity = value;
                    SendPropertyChanged("MovingStopProfitQuantity");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
        public virtual void SetValue(System.Linq.Expressions.Expression<Func<SetStopPLHistory, bool>> exp)
        {
            base.SetValue<SetStopPLHistory>(exp);
        }
    }
    /// <summary>仓位</summary>
    [TableConfig]
    [Table("position")]
    [Way.EntityDB.DataItemJsonConverter]
    public class Position : Way.EntityDB.DataItem
    {
        System.Nullable<Int64> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int64> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id", _id, value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        string? _Symbol;
        /// <summary>
        /// 交易对
        /// 如：BTCUSDT
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "交易对  如：BTCUSDT")]
        [Column("symbol")]
        public virtual string? Symbol
        {
            get
            {
                return _Symbol;
            }
            set
            {
                if ((_Symbol != value))
                {
                    SendPropertyChanging("Symbol", _Symbol, value);
                    _Symbol = value;
                    SendPropertyChanged("Symbol");
                }
            }
        }
        string? _Commodity;
        /// <summary>
        /// 物品名称
        /// 如：BTCUSDT，物品名称就是BTC
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "物品名称 如：BTCUSDT，物品名称就是BTC")]
        [Column("commodity")]
        public virtual string? Commodity
        {
            get
            {
                return _Commodity;
            }
            set
            {
                if ((_Commodity != value))
                {
                    SendPropertyChanging("Commodity", _Commodity, value);
                    _Commodity = value;
                    SendPropertyChanged("Commodity");
                }
            }
        }
        string? _TradeCoin;
        /// <summary>
        /// 交易币种
        /// 如：BTCUSDT，交易币种就是USDT
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "交易币种 如：BTCUSDT，交易币种就是USDT")]
        [Column("tradecoin")]
        public virtual string? TradeCoin
        {
            get
            {
                return _TradeCoin;
            }
            set
            {
                if ((_TradeCoin != value))
                {
                    SendPropertyChanging("TradeCoin", _TradeCoin, value);
                    _TradeCoin = value;
                    SendPropertyChanged("TradeCoin");
                }
            }
        }
        Decimal _TradeCoinAmount = 0m;
        /// <summary>
        /// 交易币现存金额
        /// 负数表示是问平台借的
        /// </summary>
        [DisallowNull]
        [Display(Name = "交易币现存金额  负数表示是问平台借的")]
        [Column("tradecoinamount")]
        public virtual Decimal TradeCoinAmount
        {
            get
            {
                return _TradeCoinAmount;
            }
            set
            {
                if ((_TradeCoinAmount != value))
                {
                    SendPropertyChanging("TradeCoinAmount", _TradeCoinAmount, value);
                    _TradeCoinAmount = value;
                    SendPropertyChanged("TradeCoinAmount");
                }
            }
        }
        MarketOrder_TypeEnum _Type = (MarketOrder_TypeEnum)(1);
        [DisallowNull]
        [Column("type")]
        public virtual MarketOrder_TypeEnum Type
        {
            get
            {
                return _Type;
            }
            set
            {
                if ((_Type != value))
                {
                    SendPropertyChanging("Type", _Type, value);
                    _Type = value;
                    SendPropertyChanged("Type");
                }
            }
        }
        Decimal _Margin = 0m;
        /// <summary>保证金、投入的金额</summary>
        [DisallowNull]
        [Display(Name = "保证金、投入的金额")]
        [Column("margin")]
        public virtual Decimal Margin
        {
            get
            {
                return _Margin;
            }
            set
            {
                if ((_Margin != value))
                {
                    SendPropertyChanging("Margin", _Margin, value);
                    _Margin = value;
                    SendPropertyChanged("Margin");
                }
            }
        }
        Int32 _Leverage = 1;
        /// <summary>杠杆</summary>
        [DisallowNull]
        [Display(Name = "杠杆")]
        [Column("leverage")]
        public virtual Int32 Leverage
        {
            get
            {
                return _Leverage;
            }
            set
            {
                if ((_Leverage != value))
                {
                    SendPropertyChanging("Leverage", _Leverage, value);
                    _Leverage = value;
                    SendPropertyChanged("Leverage");
                }
            }
        }
        System.Nullable<Int64> _UserId;
        /// <summary>所属用户</summary>
        [Display(Name = "所属用户")]
        [Column("userid")]
        public virtual System.Nullable<Int64> UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                if ((_UserId != value))
                {
                    SendPropertyChanging("UserId", _UserId, value);
                    _UserId = value;
                    SendPropertyChanged("UserId");
                }
            }
        }
        System.Nullable<Int64> _MoneyAccountId;
        /// <summary>用户结算账户id</summary>
        [Display(Name = "用户结算账户id")]
        [Column("moneyaccountid")]
        public virtual System.Nullable<Int64> MoneyAccountId
        {
            get
            {
                return _MoneyAccountId;
            }
            set
            {
                if ((_MoneyAccountId != value))
                {
                    SendPropertyChanging("MoneyAccountId", _MoneyAccountId, value);
                    _MoneyAccountId = value;
                    SendPropertyChanged("MoneyAccountId");
                }
            }
        }
        System.Nullable<DateTime> _CreateTime;
        /// <summary>创建时间</summary>
        [Display(Name = "创建时间")]
        [Column("createtime")]
        public virtual System.Nullable<DateTime> CreateTime
        {
            get
            {
                return _CreateTime;
            }
            set
            {
                if ((_CreateTime != value))
                {
                    SendPropertyChanging("CreateTime", _CreateTime, value);
                    _CreateTime = value;
                    SendPropertyChanged("CreateTime");
                }
            }
        }
        Position_StatusEnum _Status = (Position_StatusEnum)(1);
        /// <summary>状态</summary>
        [DisallowNull]
        [Display(Name = "状态")]
        [Column("status")]
        public virtual Position_StatusEnum Status
        {
            get
            {
                return _Status;
            }
            set
            {
                if ((_Status != value))
                {
                    SendPropertyChanging("Status", _Status, value);
                    _Status = value;
                    SendPropertyChanged("Status");
                }
            }
        }
        Decimal _CommodityQuantity = 0m;
        /// <summary>
        /// 现存物品数量
        /// 负数表示是问平台借的
        /// </summary>
        [DisallowNull]
        [Display(Name = "现存物品数量  负数表示是问平台借的")]
        [Column("commodityquantity")]
        public virtual Decimal CommodityQuantity
        {
            get
            {
                return _CommodityQuantity;
            }
            set
            {
                if ((_CommodityQuantity != value))
                {
                    SendPropertyChanging("CommodityQuantity", _CommodityQuantity, value);
                    _CommodityQuantity = value;
                    SendPropertyChanged("CommodityQuantity");
                }
            }
        }
        Position_DirectionEnum _Direction = (Position_DirectionEnum)(1);
        /// <summary>方向</summary>
        [DisallowNull]
        [Display(Name = "方向")]
        [Column("direction")]
        public virtual Position_DirectionEnum Direction
        {
            get
            {
                return _Direction;
            }
            set
            {
                if ((_Direction != value))
                {
                    SendPropertyChanging("Direction", _Direction, value);
                    _Direction = value;
                    SendPropertyChanged("Direction");
                }
            }
        }
        Decimal _AdditionalMargin = 0m;
        /// <summary>追加的保证金</summary>
        [DisallowNull]
        [Display(Name = "追加的保证金")]
        [Column("additionalmargin")]
        public virtual Decimal AdditionalMargin
        {
            get
            {
                return _AdditionalMargin;
            }
            set
            {
                if ((_AdditionalMargin != value))
                {
                    SendPropertyChanging("AdditionalMargin", _AdditionalMargin, value);
                    _AdditionalMargin = value;
                    SendPropertyChanged("AdditionalMargin");
                }
            }
        }
        System.Nullable<Decimal> _StopProfit;
        /// <summary>止盈价格</summary>
        [Display(Name = "止盈价格")]
        [Column("stopprofit")]
        public virtual System.Nullable<Decimal> StopProfit
        {
            get
            {
                return _StopProfit;
            }
            set
            {
                if ((_StopProfit != value))
                {
                    SendPropertyChanging("StopProfit", _StopProfit, value);
                    _StopProfit = value;
                    SendPropertyChanged("StopProfit");
                }
            }
        }
        System.Nullable<Decimal> _StopLoss;
        /// <summary>止损价格</summary>
        [Display(Name = "止损价格")]
        [Column("stoploss")]
        public virtual System.Nullable<Decimal> StopLoss
        {
            get
            {
                return _StopLoss;
            }
            set
            {
                if ((_StopLoss != value))
                {
                    SendPropertyChanging("StopLoss", _StopLoss, value);
                    _StopLoss = value;
                    SendPropertyChanged("StopLoss");
                }
            }
        }
        System.Nullable<Decimal> _StopProfitAmount;
        /// <summary>止盈金额</summary>
        [Display(Name = "止盈金额")]
        [Column("stopprofitamount")]
        public virtual System.Nullable<Decimal> StopProfitAmount
        {
            get
            {
                return _StopProfitAmount;
            }
            set
            {
                if ((_StopProfitAmount != value))
                {
                    SendPropertyChanging("StopProfitAmount", _StopProfitAmount, value);
                    _StopProfitAmount = value;
                    SendPropertyChanged("StopProfitAmount");
                }
            }
        }
        System.Nullable<Decimal> _StopLossAmount;
        /// <summary>止损金额</summary>
        [Display(Name = "止损金额")]
        [Column("stoplossamount")]
        public virtual System.Nullable<Decimal> StopLossAmount
        {
            get
            {
                return _StopLossAmount;
            }
            set
            {
                if ((_StopLossAmount != value))
                {
                    SendPropertyChanging("StopLossAmount", _StopLossAmount, value);
                    _StopLossAmount = value;
                    SendPropertyChanged("StopLossAmount");
                }
            }
        }
        System.Nullable<Decimal> _PriceOnSetPL;
        /// <summary>设置止盈止损时的市价</summary>
        [Display(Name = "设置止盈止损时的市价")]
        [Column("priceonsetpl")]
        public virtual System.Nullable<Decimal> PriceOnSetPL
        {
            get
            {
                return _PriceOnSetPL;
            }
            set
            {
                if ((_PriceOnSetPL != value))
                {
                    SendPropertyChanging("PriceOnSetPL", _PriceOnSetPL, value);
                    _PriceOnSetPL = value;
                    SendPropertyChanged("PriceOnSetPL");
                }
            }
        }
        System.Nullable<Decimal> _MovingStopProfit;
        /// <summary>
        /// 移动止盈
        /// 保存的是小数，表示百分比
        /// </summary>
        [Display(Name = "移动止盈  保存的是小数，表示百分比")]
        [Column("movingstopprofit")]
        public virtual System.Nullable<Decimal> MovingStopProfit
        {
            get
            {
                return _MovingStopProfit;
            }
            set
            {
                if ((_MovingStopProfit != value))
                {
                    SendPropertyChanging("MovingStopProfit", _MovingStopProfit, value);
                    _MovingStopProfit = value;
                    SendPropertyChanged("MovingStopProfit");
                }
            }
        }
        System.Nullable<Decimal> _Profit = 0m;
        /// <summary>
        /// 盈亏
        /// 临时记录，此值无参考意义
        /// </summary>
        [Display(Name = "盈亏 临时记录，此值无参考意义")]
        [Column("profit")]
        public virtual System.Nullable<Decimal> Profit
        {
            get
            {
                return _Profit;
            }
            set
            {
                if ((_Profit != value))
                {
                    SendPropertyChanging("Profit", _Profit, value);
                    _Profit = value;
                    SendPropertyChanged("Profit");
                }
            }
        }
        System.Nullable<Decimal> _ClosePrice;
        /// <summary>此字段程序使用</summary>
        [Display(Name = "此字段程序使用")]
        [Column("closeprice")]
        public virtual System.Nullable<Decimal> ClosePrice
        {
            get
            {
                return _ClosePrice;
            }
            set
            {
                if ((_ClosePrice != value))
                {
                    SendPropertyChanging("ClosePrice", _ClosePrice, value);
                    _ClosePrice = value;
                    SendPropertyChanged("ClosePrice");
                }
            }
        }
        System.Nullable<DateTime> _CloseTime;
        /// <summary>平仓时间</summary>
        [Display(Name = "平仓时间")]
        [Column("closetime")]
        public virtual System.Nullable<DateTime> CloseTime
        {
            get
            {
                return _CloseTime;
            }
            set
            {
                if ((_CloseTime != value))
                {
                    SendPropertyChanging("CloseTime", _CloseTime, value);
                    _CloseTime = value;
                    SendPropertyChanged("CloseTime");
                }
            }
        }
        System.Nullable<Decimal> _OpenPrice;
        /// <summary>开仓价格</summary>
        [Display(Name = "开仓价格")]
        [Column("openprice")]
        public virtual System.Nullable<Decimal> OpenPrice
        {
            get
            {
                return _OpenPrice;
            }
            set
            {
                if ((_OpenPrice != value))
                {
                    SendPropertyChanging("OpenPrice", _OpenPrice, value);
                    _OpenPrice = value;
                    SendPropertyChanged("OpenPrice");
                }
            }
        }
        System.Nullable<Decimal> _MovingMaxPrice;
        /// <summary>
        /// 移动的历史最高价格
        /// 数据库不存此值
        /// </summary>
        [Display(Name = "移动的历史最高价格  数据库不存此值")]
        [Column("movingmaxprice")]
        public virtual System.Nullable<Decimal> MovingMaxPrice
        {
            get
            {
                return _MovingMaxPrice;
            }
            set
            {
                if ((_MovingMaxPrice != value))
                {
                    SendPropertyChanging("MovingMaxPrice", _MovingMaxPrice, value);
                    _MovingMaxPrice = value;
                    SendPropertyChanged("MovingMaxPrice");
                }
            }
        }
        Position_CloseTypeEnum? _CloseType = (Position_CloseTypeEnum?)(1);
        /// <summary>平仓类型</summary>
        [Display(Name = "平仓类型")]
        [Column("closetype")]
        public virtual Position_CloseTypeEnum? CloseType
        {
            get
            {
                return _CloseType;
            }
            set
            {
                if ((_CloseType != value))
                {
                    SendPropertyChanging("CloseType", _CloseType, value);
                    _CloseType = value;
                    SendPropertyChanged("CloseType");
                }
            }
        }
        Boolean _IsLocked = false;
        /// <summary>仓位是否已被锁</summary>
        [DisallowNull]
        [Display(Name = "仓位是否已被锁")]
        [Column("islocked")]
        public virtual Boolean IsLocked
        {
            get
            {
                return _IsLocked;
            }
            set
            {
                if ((_IsLocked != value))
                {
                    SendPropertyChanging("IsLocked", _IsLocked, value);
                    _IsLocked = value;
                    SendPropertyChanged("IsLocked");
                }
            }
        }
        Position_ProfitTypeEnum _ProfitType = (Position_ProfitTypeEnum)(1);
        /// <summary>收益计算方式</summary>
        [DisallowNull]
        [Display(Name = "收益计算方式")]
        [Column("profittype")]
        public virtual Position_ProfitTypeEnum ProfitType
        {
            get
            {
                return _ProfitType;
            }
            set
            {
                if ((_ProfitType != value))
                {
                    SendPropertyChanging("ProfitType", _ProfitType, value);
                    _ProfitType = value;
                    SendPropertyChanged("ProfitType");
                }
            }
        }
        System.Nullable<Decimal> _CurrentPrice;
        /// <summary>此字段程序使用</summary>
        [Display(Name = "此字段程序使用")]
        [Column("currentprice")]
        public virtual System.Nullable<Decimal> CurrentPrice
        {
            get
            {
                return _CurrentPrice;
            }
            set
            {
                if ((_CurrentPrice != value))
                {
                    SendPropertyChanging("CurrentPrice", _CurrentPrice, value);
                    _CurrentPrice = value;
                    SendPropertyChanged("CurrentPrice");
                }
            }
        }
        Decimal _FrozenCommodityQuantity = 0m;
        /// <summary>
        /// 冻结的物品数量
        /// 此字段数据库不存，只在前端有用
        /// </summary>
        [DisallowNull]
        [Display(Name = "冻结的物品数量 此字段数据库不存，只在前端有用")]
        [Column("frozencommodityquantity")]
        public virtual Decimal FrozenCommodityQuantity
        {
            get
            {
                return _FrozenCommodityQuantity;
            }
            set
            {
                if ((_FrozenCommodityQuantity != value))
                {
                    SendPropertyChanging("FrozenCommodityQuantity", _FrozenCommodityQuantity, value);
                    _FrozenCommodityQuantity = value;
                    SendPropertyChanged("FrozenCommodityQuantity");
                }
            }
        }
        System.Nullable<Decimal> _StopProfitLossQuantity;
        /// <summary>止盈止损手数</summary>
        [Display(Name = "止盈止损手数")]
        [Column("stopprofitlossquantity")]
        public virtual System.Nullable<Decimal> StopProfitLossQuantity
        {
            get
            {
                return _StopProfitLossQuantity;
            }
            set
            {
                if ((_StopProfitLossQuantity != value))
                {
                    SendPropertyChanging("StopProfitLossQuantity", _StopProfitLossQuantity, value);
                    _StopProfitLossQuantity = value;
                    SendPropertyChanged("StopProfitLossQuantity");
                }
            }
        }
        System.Nullable<Decimal> _MovingStopProfitQuantity;
        /// <summary>移动止盈手数</summary>
        [Display(Name = "移动止盈手数")]
        [Column("movingstopprofitquantity")]
        public virtual System.Nullable<Decimal> MovingStopProfitQuantity
        {
            get
            {
                return _MovingStopProfitQuantity;
            }
            set
            {
                if ((_MovingStopProfitQuantity != value))
                {
                    SendPropertyChanging("MovingStopProfitQuantity", _MovingStopProfitQuantity, value);
                    _MovingStopProfitQuantity = value;
                    SendPropertyChanged("MovingStopProfitQuantity");
                }
            }
        }
        Boolean _IsRobot = false;
        /// <summary>是否是机器人的仓位</summary>
        [DisallowNull]
        [Display(Name = "是否是机器人的仓位")]
        [Column("isrobot")]
        public virtual Boolean IsRobot
        {
            get
            {
                return _IsRobot;
            }
            set
            {
                if ((_IsRobot != value))
                {
                    SendPropertyChanging("IsRobot", _IsRobot, value);
                    _IsRobot = value;
                    SendPropertyChanged("IsRobot");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
        public virtual void SetValue(System.Linq.Expressions.Expression<Func<Position, bool>> exp)
        {
            base.SetValue<Position>(exp);
        }
    }
    public enum Position_StatusEnum : int
    {
        /// <summary>开仓中</summary>
        Open = 1,
        /// <summary>平仓中</summary>
        Closing = 100,
        /// <summary>已平仓</summary>
        Closed = 300
    }
    public enum Position_DirectionEnum : int
    {
        /// <summary>做多</summary>
        Buy = 1,
        /// <summary>做空</summary>
        Sell = ~Buy
    }
    public enum Position_CloseTypeEnum : int
    {
        /// <summary>手动平仓</summary>
        Manual = 1,
        /// <summary>系统强平</summary>
        SysClose = 2,
        /// <summary>止盈止损</summary>
        StopProfitLoss = 3,
        /// <summary>移动止盈</summary>
        MovingStop = 4
    }
    public enum Position_ProfitTypeEnum : int
    {
        /// <summary>最后结算金额</summary>
        SettlementAmount = 1,
        /// <summary>涨跌百分比</summary>
        Percent = 2
    }
    /// <summary>市场挂单</summary>
    [TableConfig]
    [Table("marketorder")]
    [Way.EntityDB.DataItemJsonConverter]
    public class MarketOrder : Way.EntityDB.DataItem
    {
        System.Nullable<Int64> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int64> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id", _id, value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        System.Nullable<Int64> _PositionId;
        /// <summary>平仓挂反向单时的仓位id</summary>
        [Display(Name = "平仓挂反向单时的仓位id")]
        [Column("positionid")]
        public virtual System.Nullable<Int64> PositionId
        {
            get
            {
                return _PositionId;
            }
            set
            {
                if ((_PositionId != value))
                {
                    SendPropertyChanging("PositionId", _PositionId, value);
                    _PositionId = value;
                    SendPropertyChanged("PositionId");
                }
            }
        }
        Position_DirectionEnum _Direction = (Position_DirectionEnum)(1);
        /// <summary>方向</summary>
        [DisallowNull]
        [Display(Name = "方向")]
        [Column("direction")]
        public virtual Position_DirectionEnum Direction
        {
            get
            {
                return _Direction;
            }
            set
            {
                if ((_Direction != value))
                {
                    SendPropertyChanging("Direction", _Direction, value);
                    _Direction = value;
                    SendPropertyChanged("Direction");
                }
            }
        }
        Decimal _Quantity;
        /// <summary>数量</summary>
        [DisallowNull]
        [Display(Name = "数量")]
        [Column("quantity")]
        public virtual Decimal Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                if ((_Quantity != value))
                {
                    SendPropertyChanging("Quantity", _Quantity, value);
                    _Quantity = value;
                    SendPropertyChanged("Quantity");
                }
            }
        }
        System.Nullable<Decimal> _Price;
        /// <summary>
        /// 挂单单价
        /// null表示任意单价
        /// </summary>
        [Display(Name = "挂单单价 null表示任意单价")]
        [Column("price")]
        public virtual System.Nullable<Decimal> Price
        {
            get
            {
                return _Price;
            }
            set
            {
                if ((_Price != value))
                {
                    SendPropertyChanging("Price", _Price, value);
                    _Price = value;
                    SendPropertyChanged("Price");
                }
            }
        }
        Decimal _DealQuantity = 0m;
        /// <summary>已成交数量</summary>
        [DisallowNull]
        [Display(Name = "已成交数量")]
        [Column("dealquantity")]
        public virtual Decimal DealQuantity
        {
            get
            {
                return _DealQuantity;
            }
            set
            {
                if ((_DealQuantity != value))
                {
                    SendPropertyChanging("DealQuantity", _DealQuantity, value);
                    _DealQuantity = value;
                    SendPropertyChanged("DealQuantity");
                }
            }
        }
        MarketOrder_StatusEnum _Status = (MarketOrder_StatusEnum)(1);
        /// <summary>状态</summary>
        [DisallowNull]
        [Display(Name = "状态")]
        [Column("status")]
        public virtual MarketOrder_StatusEnum Status
        {
            get
            {
                return _Status;
            }
            set
            {
                if ((_Status != value))
                {
                    SendPropertyChanging("Status", _Status, value);
                    _Status = value;
                    SendPropertyChanged("Status");
                }
            }
        }
        System.Nullable<DateTime> _CreateTime;
        /// <summary>挂单时间</summary>
        [Display(Name = "挂单时间")]
        [Column("createtime")]
        public virtual System.Nullable<DateTime> CreateTime
        {
            get
            {
                return _CreateTime;
            }
            set
            {
                if ((_CreateTime != value))
                {
                    SendPropertyChanging("CreateTime", _CreateTime, value);
                    _CreateTime = value;
                    SendPropertyChanged("CreateTime");
                }
            }
        }
        System.Nullable<DateTime> _ValidTime;
        /// <summary>
        /// 生效时间
        /// 没到生效时间，不能进入撮合队列，null表示立即生效
        /// </summary>
        [Display(Name = "生效时间  没到生效时间，不能进入撮合队列，null表示立即生效")]
        [Column("validtime")]
        public virtual System.Nullable<DateTime> ValidTime
        {
            get
            {
                return _ValidTime;
            }
            set
            {
                if ((_ValidTime != value))
                {
                    SendPropertyChanging("ValidTime", _ValidTime, value);
                    _ValidTime = value;
                    SendPropertyChanged("ValidTime");
                }
            }
        }
        string? _Symbol;
        /// <summary>
        /// 交易对
        /// 如：BTC/USDT
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "交易对  如：BTC/USDT")]
        [Column("symbol")]
        public virtual string? Symbol
        {
            get
            {
                return _Symbol;
            }
            set
            {
                if ((_Symbol != value))
                {
                    SendPropertyChanging("Symbol", _Symbol, value);
                    _Symbol = value;
                    SendPropertyChanged("Symbol");
                }
            }
        }
        System.Nullable<DateTime> _UpdateTime;
        /// <summary>更新状态时间</summary>
        [Display(Name = "更新状态时间")]
        [Column("updatetime")]
        public virtual System.Nullable<DateTime> UpdateTime
        {
            get
            {
                return _UpdateTime;
            }
            set
            {
                if ((_UpdateTime != value))
                {
                    SendPropertyChanging("UpdateTime", _UpdateTime, value);
                    _UpdateTime = value;
                    SendPropertyChanged("UpdateTime");
                }
            }
        }
        Int64 _UserId;
        [DisallowNull]
        [Column("userid")]
        public virtual Int64 UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                if ((_UserId != value))
                {
                    SendPropertyChanging("UserId", _UserId, value);
                    _UserId = value;
                    SendPropertyChanged("UserId");
                }
            }
        }
        System.Nullable<Decimal> _Margin;
        /// <summary>保证金</summary>
        [Display(Name = "保证金")]
        [Column("margin")]
        public virtual System.Nullable<Decimal> Margin
        {
            get
            {
                return _Margin;
            }
            set
            {
                if ((_Margin != value))
                {
                    SendPropertyChanging("Margin", _Margin, value);
                    _Margin = value;
                    SendPropertyChanged("Margin");
                }
            }
        }
        System.Nullable<Int32> _Leverage;
        /// <summary>杠杆</summary>
        [Display(Name = "杠杆")]
        [Column("leverage")]
        public virtual System.Nullable<Int32> Leverage
        {
            get
            {
                return _Leverage;
            }
            set
            {
                if ((_Leverage != value))
                {
                    SendPropertyChanging("Leverage", _Leverage, value);
                    _Leverage = value;
                    SendPropertyChanged("Leverage");
                }
            }
        }
        MarketOrder_TypeEnum _Type = (MarketOrder_TypeEnum)(0);
        /// <summary>类型</summary>
        [DisallowNull]
        [Display(Name = "类型")]
        [Column("type")]
        public virtual MarketOrder_TypeEnum Type
        {
            get
            {
                return _Type;
            }
            set
            {
                if ((_Type != value))
                {
                    SendPropertyChanging("Type", _Type, value);
                    _Type = value;
                    SendPropertyChanged("Type");
                }
            }
        }
        Int32 _PriceType = 2;
        /// <summary>2:市价 1:限价</summary>
        [DisallowNull]
        [Display(Name = "2:市价 1:限价")]
        [Column("pricetype")]
        public virtual Int32 PriceType
        {
            get
            {
                return _PriceType;
            }
            set
            {
                if ((_PriceType != value))
                {
                    SendPropertyChanging("PriceType", _PriceType, value);
                    _PriceType = value;
                    SendPropertyChanged("PriceType");
                }
            }
        }
        System.Nullable<Decimal> _Fee;
        /// <summary>手续费</summary>
        [Display(Name = "手续费")]
        [Column("fee")]
        public virtual System.Nullable<Decimal> Fee
        {
            get
            {
                return _Fee;
            }
            set
            {
                if ((_Fee != value))
                {
                    SendPropertyChanging("Fee", _Fee, value);
                    _Fee = value;
                    SendPropertyChanged("Fee");
                }
            }
        }
        System.Nullable<Int64> _MoneyAccountId;
        /// <summary>用户结算账户id</summary>
        [Display(Name = "用户结算账户id")]
        [Column("moneyaccountid")]
        public virtual System.Nullable<Int64> MoneyAccountId
        {
            get
            {
                return _MoneyAccountId;
            }
            set
            {
                if ((_MoneyAccountId != value))
                {
                    SendPropertyChanging("MoneyAccountId", _MoneyAccountId, value);
                    _MoneyAccountId = value;
                    SendPropertyChanged("MoneyAccountId");
                }
            }
        }
        System.Nullable<Int64> _TargetMoneyAccountId;
        /// <summary>兑换到哪个资金账户</summary>
        [Display(Name = "兑换到哪个资金账户")]
        [Column("targetmoneyaccountid")]
        public virtual System.Nullable<Int64> TargetMoneyAccountId
        {
            get
            {
                return _TargetMoneyAccountId;
            }
            set
            {
                if ((_TargetMoneyAccountId != value))
                {
                    SendPropertyChanging("TargetMoneyAccountId", _TargetMoneyAccountId, value);
                    _TargetMoneyAccountId = value;
                    SendPropertyChanged("TargetMoneyAccountId");
                }
            }
        }
        string? _FailReason;
        /// <summary>失败原因</summary>
        [MaxLength(255)]
        [Display(Name = "失败原因")]
        [Column("failreason")]
        public virtual string? FailReason
        {
            get
            {
                return _FailReason;
            }
            set
            {
                if ((_FailReason != value))
                {
                    SendPropertyChanging("FailReason", _FailReason, value);
                    _FailReason = value;
                    SendPropertyChanged("FailReason");
                }
            }
        }
        System.Nullable<Decimal> _FixedPrice;
        /// <summary>固定成交价格，用于系统强平单</summary>
        [Display(Name = "固定成交价格，用于系统强平单")]
        [Column("fixedprice")]
        public virtual System.Nullable<Decimal> FixedPrice
        {
            get
            {
                return _FixedPrice;
            }
            set
            {
                if ((_FixedPrice != value))
                {
                    SendPropertyChanging("FixedPrice", _FixedPrice, value);
                    _FixedPrice = value;
                    SendPropertyChanged("FixedPrice");
                }
            }
        }
        MarketOrder_SubTypeEnum _SubType = (MarketOrder_SubTypeEnum)(0);
        /// <summary>子类型</summary>
        [DisallowNull]
        [Display(Name = "子类型")]
        [Column("subtype")]
        public virtual MarketOrder_SubTypeEnum SubType
        {
            get
            {
                return _SubType;
            }
            set
            {
                if ((_SubType != value))
                {
                    SendPropertyChanging("SubType", _SubType, value);
                    _SubType = value;
                    SendPropertyChanged("SubType");
                }
            }
        }
        string? _Commodity;
        /// <summary>物品名称</summary>
        [MaxLength(50)]
        [Display(Name = "物品名称")]
        [Column("commodity")]
        public virtual string? Commodity
        {
            get
            {
                return _Commodity;
            }
            set
            {
                if ((_Commodity != value))
                {
                    SendPropertyChanging("Commodity", _Commodity, value);
                    _Commodity = value;
                    SendPropertyChanged("Commodity");
                }
            }
        }
        string? _TradeCoin;
        /// <summary>交易货币名称</summary>
        [MaxLength(50)]
        [Display(Name = "交易货币名称")]
        [Column("tradecoin")]
        public virtual string? TradeCoin
        {
            get
            {
                return _TradeCoin;
            }
            set
            {
                if ((_TradeCoin != value))
                {
                    SendPropertyChanging("TradeCoin", _TradeCoin, value);
                    _TradeCoin = value;
                    SendPropertyChanged("TradeCoin");
                }
            }
        }
        Boolean _IsRobot = false;
        /// <summary>是否是机器人下的单</summary>
        [DisallowNull]
        [Display(Name = "是否是机器人下的单")]
        [Column("isrobot")]
        public virtual Boolean IsRobot
        {
            get
            {
                return _IsRobot;
            }
            set
            {
                if ((_IsRobot != value))
                {
                    SendPropertyChanging("IsRobot", _IsRobot, value);
                    _IsRobot = value;
                    SendPropertyChanged("IsRobot");
                }
            }
        }
        System.Nullable<DateTime> _AutoCancelTime;
        /// <summary>自动取消时间</summary>
        [Display(Name = "自动取消时间")]
        [Column("autocanceltime")]
        public virtual System.Nullable<DateTime> AutoCancelTime
        {
            get
            {
                return _AutoCancelTime;
            }
            set
            {
                if ((_AutoCancelTime != value))
                {
                    SendPropertyChanging("AutoCancelTime", _AutoCancelTime, value);
                    _AutoCancelTime = value;
                    SendPropertyChanged("AutoCancelTime");
                }
            }
        }
        System.Nullable<Decimal> _PriceMovingRate;
        /// <summary>用户设置的滑点</summary>
        [Display(Name = "用户设置的滑点")]
        [Column("pricemovingrate")]
        public virtual System.Nullable<Decimal> PriceMovingRate
        {
            get
            {
                return _PriceMovingRate;
            }
            set
            {
                if ((_PriceMovingRate != value))
                {
                    SendPropertyChanging("PriceMovingRate", _PriceMovingRate, value);
                    _PriceMovingRate = value;
                    SendPropertyChanged("PriceMovingRate");
                }
            }
        }
        string? _Guid;
        /// <summary>字符串唯一编码</summary>
        [MaxLength(50)]
        [Display(Name = "字符串唯一编码")]
        [Column("guid")]
        public virtual string? Guid
        {
            get
            {
                return _Guid;
            }
            set
            {
                if ((_Guid != value))
                {
                    SendPropertyChanging("Guid", _Guid, value);
                    _Guid = value;
                    SendPropertyChanged("Guid");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
        public virtual void SetValue(System.Linq.Expressions.Expression<Func<MarketOrder, bool>> exp)
        {
            base.SetValue<MarketOrder>(exp);
        }
    }
    public enum MarketOrder_StatusEnum : int
    {
        /// <summary>挂单中（等待成交）</summary>
        Open = 1,
        /// <summary>正在处理（状态未知）</summary>
        Trading = 50,
        /// <summary>已经撤销</summary>
        Canceled = 100,
        /// <summary>关闭(交易成功)</summary>
        Closed = 200,
        /// <summary>交易失败</summary>
        Fail = 300,
        /// <summary>超时取消</summary>
        TimeoutCancel = 800,
        /// <summary>后台管理员手动撤销</summary>
        ManualCancel = 900,
        /// <summary>后台管理员手动设置交易成功</summary>
        ManualSuccess = 901
    }
    public enum MarketOrder_TypeEnum : int
    {
        /// <summary>持仓类订单</summary>
        PositionOrder = 1 << 10,
        ExchangeOrder = 1 << 11,
        /// <summary>合约交易里的市场挂单</summary>
        ContractMarketOrder = PositionOrder | 1 << 20,
        /// <summary>固定现货合约的市场挂单</summary>
        SpotMarketOrder = ContractMarketOrder | 1 << 21,
        /// <summary>永续合约的市场挂单</summary>
        PerpetualMarketOrder = ContractMarketOrder | 1 << 22
    }
    public enum MarketOrder_SubTypeEnum : int
    {
        /// <summary>用户挂单</summary>
        None = 0,
        /// <summary>止盈单</summary>
        StopProfit = 1,
        /// <summary>止损单</summary>
        StopLoss = 2,
        /// <summary>系统强平</summary>
        SysClose = 3,
    }
    /// <summary>市场成交历史</summary>
    [TableConfig]
    [Table("marketdealhistory")]
    [Way.EntityDB.DataItemJsonConverter]
    public class MarketDealHistory : Way.EntityDB.DataItem
    {
        System.Nullable<Int64> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int64> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id", _id, value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        System.Nullable<Int64> _MarketOrderId;
        /// <summary>挂单id</summary>
        [Display(Name = "挂单id")]
        [Column("marketorderid")]
        public virtual System.Nullable<Int64> MarketOrderId
        {
            get
            {
                return _MarketOrderId;
            }
            set
            {
                if ((_MarketOrderId != value))
                {
                    SendPropertyChanging("MarketOrderId", _MarketOrderId, value);
                    _MarketOrderId = value;
                    SendPropertyChanged("MarketOrderId");
                }
            }
        }
        System.Nullable<Decimal> _Price;
        /// <summary>给用户看的成交价</summary>
        [Display(Name = "给用户看的成交价")]
        [Column("price")]
        public virtual System.Nullable<Decimal> Price
        {
            get
            {
                return _Price;
            }
            set
            {
                if ((_Price != value))
                {
                    SendPropertyChanging("Price", _Price, value);
                    _Price = value;
                    SendPropertyChanged("Price");
                }
            }
        }
        System.Nullable<Int64> _TargetOrderId;
        /// <summary>对方挂单id</summary>
        [Display(Name = "对方挂单id")]
        [Column("targetorderid")]
        public virtual System.Nullable<Int64> TargetOrderId
        {
            get
            {
                return _TargetOrderId;
            }
            set
            {
                if ((_TargetOrderId != value))
                {
                    SendPropertyChanging("TargetOrderId", _TargetOrderId, value);
                    _TargetOrderId = value;
                    SendPropertyChanged("TargetOrderId");
                }
            }
        }
        System.Nullable<DateTime> _DealTime;
        /// <summary>成交时间</summary>
        [Display(Name = "成交时间")]
        [Column("dealtime")]
        public virtual System.Nullable<DateTime> DealTime
        {
            get
            {
                return _DealTime;
            }
            set
            {
                if ((_DealTime != value))
                {
                    SendPropertyChanging("DealTime", _DealTime, value);
                    _DealTime = value;
                    SendPropertyChanged("DealTime");
                }
            }
        }
        System.Nullable<Decimal> _Quantity;
        /// <summary>成交数量</summary>
        [Display(Name = "成交数量")]
        [Column("quantity")]
        public virtual System.Nullable<Decimal> Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                if ((_Quantity != value))
                {
                    SendPropertyChanging("Quantity", _Quantity, value);
                    _Quantity = value;
                    SendPropertyChanged("Quantity");
                }
            }
        }
        System.Nullable<Int64> _PositionId;
        /// <summary>仓位id</summary>
        [Display(Name = "仓位id")]
        [Column("positionid")]
        public virtual System.Nullable<Int64> PositionId
        {
            get
            {
                return _PositionId;
            }
            set
            {
                if ((_PositionId != value))
                {
                    SendPropertyChanging("PositionId", _PositionId, value);
                    _PositionId = value;
                    SendPropertyChanged("PositionId");
                }
            }
        }
        Position_DirectionEnum _Direction = (Position_DirectionEnum)(1);
        /// <summary>方向</summary>
        [DisallowNull]
        [Display(Name = "方向")]
        [Column("direction")]
        public virtual Position_DirectionEnum Direction
        {
            get
            {
                return _Direction;
            }
            set
            {
                if ((_Direction != value))
                {
                    SendPropertyChanging("Direction", _Direction, value);
                    _Direction = value;
                    SendPropertyChanged("Direction");
                }
            }
        }
        System.Nullable<DateTime> _CreateTime;
        /// <summary>数据生成时间</summary>
        [Display(Name = "数据生成时间")]
        [Column("createtime")]
        public virtual System.Nullable<DateTime> CreateTime
        {
            get
            {
                return _CreateTime;
            }
            set
            {
                if ((_CreateTime != value))
                {
                    SendPropertyChanging("CreateTime", _CreateTime, value);
                    _CreateTime = value;
                    SendPropertyChanged("CreateTime");
                }
            }
        }
        Boolean _IsHandled = false;
        /// <summary>是否已处理</summary>
        [DisallowNull]
        [Display(Name = "是否已处理")]
        [Column("ishandled")]
        public virtual Boolean IsHandled
        {
            get
            {
                return _IsHandled;
            }
            set
            {
                if ((_IsHandled != value))
                {
                    SendPropertyChanging("IsHandled", _IsHandled, value);
                    _IsHandled = value;
                    SendPropertyChanged("IsHandled");
                }
            }
        }
        System.Nullable<Decimal> _TradeFee;
        /// <summary>交易手续费</summary>
        [Display(Name = "交易手续费")]
        [Column("tradefee")]
        public virtual System.Nullable<Decimal> TradeFee
        {
            get
            {
                return _TradeFee;
            }
            set
            {
                if ((_TradeFee != value))
                {
                    SendPropertyChanging("TradeFee", _TradeFee, value);
                    _TradeFee = value;
                    SendPropertyChanged("TradeFee");
                }
            }
        }
        System.Nullable<Decimal> _RealPrice;
        /// <summary>实际成交价格，前端不展示</summary>
        [Display(Name = "实际成交价格，前端不展示")]
        [Column("realprice")]
        public virtual System.Nullable<Decimal> RealPrice
        {
            get
            {
                return _RealPrice;
            }
            set
            {
                if ((_RealPrice != value))
                {
                    SendPropertyChanging("RealPrice", _RealPrice, value);
                    _RealPrice = value;
                    SendPropertyChanged("RealPrice");
                }
            }
        }
        System.Nullable<Decimal> _OpenPrice;
        /// <summary>
        /// 成本开仓价
        /// 平仓才有值
        /// </summary>
        [Display(Name = "成本开仓价 平仓才有值")]
        [Column("openprice")]
        public virtual System.Nullable<Decimal> OpenPrice
        {
            get
            {
                return _OpenPrice;
            }
            set
            {
                if ((_OpenPrice != value))
                {
                    SendPropertyChanging("OpenPrice", _OpenPrice, value);
                    _OpenPrice = value;
                    SendPropertyChanged("OpenPrice");
                }
            }
        }
        System.Nullable<Decimal> _Profit;
        /// <summary>盈亏</summary>
        [Display(Name = "盈亏")]
        [Column("profit")]
        public virtual System.Nullable<Decimal> Profit
        {
            get
            {
                return _Profit;
            }
            set
            {
                if ((_Profit != value))
                {
                    SendPropertyChanging("Profit", _Profit, value);
                    _Profit = value;
                    SendPropertyChanged("Profit");
                }
            }
        }
        System.Nullable<Decimal> _Margin;
        /// <summary>保证金</summary>
        [Display(Name = "保证金")]
        [Column("margin")]
        public virtual System.Nullable<Decimal> Margin
        {
            get
            {
                return _Margin;
            }
            set
            {
                if ((_Margin != value))
                {
                    SendPropertyChanging("Margin", _Margin, value);
                    _Margin = value;
                    SendPropertyChanged("Margin");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
        public virtual void SetValue(System.Linq.Expressions.Expression<Func<MarketDealHistory, bool>> exp)
        {
            base.SetValue<MarketDealHistory>(exp);
        }
    }
    /// <summary>资金明细</summary>
    [TableConfig]
    [Table("moneydetail")]
    [Way.EntityDB.DataItemJsonConverter]
    public class MoneyDetail : Way.EntityDB.DataItem
    {
        System.Nullable<Int64> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int64> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id", _id, value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        Int64 _UserId;
        [DisallowNull]
        [Column("userid")]
        public virtual Int64 UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                if ((_UserId != value))
                {
                    SendPropertyChanging("UserId", _UserId, value);
                    _UserId = value;
                    SendPropertyChanged("UserId");
                }
            }
        }
        Int64 _MoneyAccountId;
        /// <summary>资金账户id</summary>
        [DisallowNull]
        [Display(Name = "资金账户id")]
        [Column("moneyaccountid")]
        public virtual Int64 MoneyAccountId
        {
            get
            {
                return _MoneyAccountId;
            }
            set
            {
                if ((_MoneyAccountId != value))
                {
                    SendPropertyChanging("MoneyAccountId", _MoneyAccountId, value);
                    _MoneyAccountId = value;
                    SendPropertyChanged("MoneyAccountId");
                }
            }
        }
        Decimal _Amount;
        /// <summary>金额</summary>
        [DisallowNull]
        [Display(Name = "金额")]
        [Column("amount")]
        public virtual Decimal Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                if ((_Amount != value))
                {
                    SendPropertyChanging("Amount", _Amount, value);
                    _Amount = value;
                    SendPropertyChanged("Amount");
                }
            }
        }
        Decimal _Balance;
        /// <summary>变化后余额</summary>
        [DisallowNull]
        [Display(Name = "变化后余额")]
        [Column("balance")]
        public virtual Decimal Balance
        {
            get
            {
                return _Balance;
            }
            set
            {
                if ((_Balance != value))
                {
                    SendPropertyChanging("Balance", _Balance, value);
                    _Balance = value;
                    SendPropertyChanged("Balance");
                }
            }
        }
        System.Nullable<DateTime> _CreateTime;
        [Column("createtime")]
        public virtual System.Nullable<DateTime> CreateTime
        {
            get
            {
                return _CreateTime;
            }
            set
            {
                if ((_CreateTime != value))
                {
                    SendPropertyChanging("CreateTime", _CreateTime, value);
                    _CreateTime = value;
                    SendPropertyChanged("CreateTime");
                }
            }
        }
        MoneyDetail_TypeEnum _Type = (MoneyDetail_TypeEnum)(1);
        [DisallowNull]
        [Column("type")]
        public virtual MoneyDetail_TypeEnum Type
        {
            get
            {
                return _Type;
            }
            set
            {
                if ((_Type != value))
                {
                    SendPropertyChanging("Type", _Type, value);
                    _Type = value;
                    SendPropertyChanged("Type");
                }
            }
        }
        System.Nullable<Int64> _PositionId;
        /// <summary>关联的仓位id</summary>
        [Display(Name = "关联的仓位id")]
        [Column("positionid")]
        public virtual System.Nullable<Int64> PositionId
        {
            get
            {
                return _PositionId;
            }
            set
            {
                if ((_PositionId != value))
                {
                    SendPropertyChanging("PositionId", _PositionId, value);
                    _PositionId = value;
                    SendPropertyChanged("PositionId");
                }
            }
        }
        System.Nullable<Int64> _MarketOrderId;
        [Column("marketorderid")]
        public virtual System.Nullable<Int64> MarketOrderId
        {
            get
            {
                return _MarketOrderId;
            }
            set
            {
                if ((_MarketOrderId != value))
                {
                    SendPropertyChanging("MarketOrderId", _MarketOrderId, value);
                    _MarketOrderId = value;
                    SendPropertyChanged("MarketOrderId");
                }
            }
        }
        Decimal _Frozen;
        /// <summary>变化后的冻结总额</summary>
        [DisallowNull]
        [Display(Name = "变化后的冻结总额")]
        [Column("frozen")]
        public virtual Decimal Frozen
        {
            get
            {
                return _Frozen;
            }
            set
            {
                if ((_Frozen != value))
                {
                    SendPropertyChanging("Frozen", _Frozen, value);
                    _Frozen = value;
                    SendPropertyChanged("Frozen");
                }
            }
        }
        System.Nullable<Int64> _WithdrawId;
        /// <summary>关联提现表id</summary>
        [Display(Name = "关联提现表id")]
        [Column("withdrawid")]
        public virtual System.Nullable<Int64> WithdrawId
        {
            get
            {
                return _WithdrawId;
            }
            set
            {
                if ((_WithdrawId != value))
                {
                    SendPropertyChanging("WithdrawId", _WithdrawId, value);
                    _WithdrawId = value;
                    SendPropertyChanged("WithdrawId");
                }
            }
        }
        MoneyAccount_AccountTypeEnum? _RefAccountType;
        /// <summary>
        /// 与之关联的账户类型
        /// 例如转出资金，这里就表示转出到哪个账户上了
        /// 转入资金，这里就表示从哪里转过来的
        /// </summary>
        [Display(Name = "与之关联的账户类型 例如转出资金，这里就表示转出到哪个账户上了 转入资金，这里就表示从哪里转过来的")]
        [Column("refaccounttype")]
        public virtual MoneyAccount_AccountTypeEnum? RefAccountType
        {
            get
            {
                return _RefAccountType;
            }
            set
            {
                if ((_RefAccountType != value))
                {
                    SendPropertyChanging("RefAccountType", _RefAccountType, value);
                    _RefAccountType = value;
                    SendPropertyChanged("RefAccountType");
                }
            }
        }
        string? _TxId;
        /// <summary>充值的txid</summary>
        [MaxLength(255)]
        [Display(Name = "充值的txid")]
        [Column("txid")]
        public virtual string? TxId
        {
            get
            {
                return _TxId;
            }
            set
            {
                if ((_TxId != value))
                {
                    SendPropertyChanging("TxId", _TxId, value);
                    _TxId = value;
                    SendPropertyChanged("TxId");
                }
            }
        }
        Decimal _ReturnAmount = 0m;
        /// <summary>已退还金额</summary>
        [DisallowNull]
        [Display(Name = "已退还金额")]
        [Column("returnamount")]
        public virtual Decimal ReturnAmount
        {
            get
            {
                return _ReturnAmount;
            }
            set
            {
                if ((_ReturnAmount != value))
                {
                    SendPropertyChanging("ReturnAmount", _ReturnAmount, value);
                    _ReturnAmount = value;
                    SendPropertyChanged("ReturnAmount");
                }
            }
        }
        System.Nullable<Int64> _TransferId;
        /// <summary>转账id</summary>
        [Display(Name = "转账id")]
        [Column("transferid")]
        public virtual System.Nullable<Int64> TransferId
        {
            get
            {
                return _TransferId;
            }
            set
            {
                if ((_TransferId != value))
                {
                    SendPropertyChanging("TransferId", _TransferId, value);
                    _TransferId = value;
                    SendPropertyChanged("TransferId");
                }
            }
        }
        string? _SenderAddress;
        /// <summary>充值转出地址</summary>
        [MaxLength(255)]
        [Display(Name = "充值转出地址")]
        [Column("senderaddress")]
        public virtual string? SenderAddress
        {
            get
            {
                return _SenderAddress;
            }
            set
            {
                if ((_SenderAddress != value))
                {
                    SendPropertyChanging("SenderAddress", _SenderAddress, value);
                    _SenderAddress = value;
                    SendPropertyChanged("SenderAddress");
                }
            }
        }
        string? _ReceiveAddress;
        /// <summary>充值转入地址</summary>
        [MaxLength(255)]
        [Display(Name = "充值转入地址")]
        [Column("receiveaddress")]
        public virtual string? ReceiveAddress
        {
            get
            {
                return _ReceiveAddress;
            }
            set
            {
                if ((_ReceiveAddress != value))
                {
                    SendPropertyChanging("ReceiveAddress", _ReceiveAddress, value);
                    _ReceiveAddress = value;
                    SendPropertyChanged("ReceiveAddress");
                }
            }
        }
        string? _LinkName;
        /// <summary>充值-公链</summary>
        [MaxLength(50)]
        [Display(Name = "充值-公链")]
        [Column("linkname")]
        public virtual string? LinkName
        {
            get
            {
                return _LinkName;
            }
            set
            {
                if ((_LinkName != value))
                {
                    SendPropertyChanging("LinkName", _LinkName, value);
                    _LinkName = value;
                    SendPropertyChanged("LinkName");
                }
            }
        }
        string? _Coin;
        /// <summary>币种</summary>
        [MaxLength(50)]
        [Display(Name = "币种")]
        [Column("coin")]
        public virtual string? Coin
        {
            get
            {
                return _Coin;
            }
            set
            {
                if ((_Coin != value))
                {
                    SendPropertyChanging("Coin", _Coin, value);
                    _Coin = value;
                    SendPropertyChanged("Coin");
                }
            }
        }
        string? _Remarks;
        /// <summary>备注</summary>
        [MaxLength(255)]
        [Display(Name = "备注")]
        [Column("remarks")]
        public virtual string? Remarks
        {
            get
            {
                return _Remarks;
            }
            set
            {
                if ((_Remarks != value))
                {
                    SendPropertyChanging("Remarks", _Remarks, value);
                    _Remarks = value;
                    SendPropertyChanged("Remarks");
                }
            }
        }
        System.Nullable<Decimal> _GiveDeductAmount;
        /// <summary>赠金</summary>
        [Display(Name = "赠金")]
        [Column("givedeductamount")]
        public virtual System.Nullable<Decimal> GiveDeductAmount
        {
            get
            {
                return _GiveDeductAmount;
            }
            set
            {
                if ((_GiveDeductAmount != value))
                {
                    SendPropertyChanging("GiveDeductAmount", _GiveDeductAmount, value);
                    _GiveDeductAmount = value;
                    SendPropertyChanged("GiveDeductAmount");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
        public virtual void SetValue(System.Linq.Expressions.Expression<Func<MoneyDetail, bool>> exp)
        {
            base.SetValue<MoneyDetail>(exp);
        }
    }
    public enum MoneyDetail_TypeEnum : int
    {
        /// <summary>充值</summary>
        Recharge = 1,
        /// <summary>提现</summary>
        Withdraw = 2,
        /// <summary>冻结资金</summary>
        BalanceToFrozen = 3,
        /// <summary>解冻资金</summary>
        FrozenToBalance = 4,
        /// <summary>交易明细</summary>
        Trade = 5,
        /// <summary>扣除订单保证金</summary>
        AddPositionMargin = 6,
        /// <summary>归还订单保证金</summary>
        CancelPositionMargin = 7,
        /// <summary>扣除手续费</summary>
        AddTradeFee = 8,
        /// <summary>归还手续费</summary>
        CancelTradeFee = 9,
        /// <summary>订单盈亏</summary>
        PositionProfit = 10,
        /// <summary>资金过期被回收</summary>
        MoneyExpire = 11,
        /// <summary>隔夜息</summary>
        DailyInterest = 12,
        /// <summary>转入</summary>
        TransferIn = 13,
        /// <summary>转出</summary>
        TransferOut = 14,
        /// <summary>解冻保证金</summary>
        UnFrozenMargin = 15,
        /// <summary>兑换实际消耗</summary>
        ExchangeOut = 17,
        /// <summary>兑换实际获得</summary>
        ExchangeIn = 18,
        /// <summary>提现取消，资金返还</summary>
        CancelWithdraw = 19,
        /// <summary>转账手续费</summary>
        TransferFee = 20,
        /// <summary>支出</summary>
        PayOut = 21,
        /// <summary>收入</summary>
        InCome = 22,
    }
    /// <summary>用户配置信息</summary>
    [TableConfig]
    [Table("usersetting")]
    [Way.EntityDB.DataItemJsonConverter]
    public class UserSetting : Way.EntityDB.DataItem
    {
        Int64 _UserId;
        [Key]
        [DisallowNull]
        [Column("userid")]
        public virtual Int64 UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                if ((_UserId != value))
                {
                    SendPropertyChanging("UserId", _UserId, value);
                    _UserId = value;
                    SendPropertyChanged("UserId");
                }
            }
        }
        Boolean _IsPositionMerge = false;
        /// <summary>是否合并仓位</summary>
        [DisallowNull]
        [Display(Name = "是否合并仓位")]
        [Column("ispositionmerge")]
        public virtual Boolean IsPositionMerge
        {
            get
            {
                return _IsPositionMerge;
            }
            set
            {
                if ((_IsPositionMerge != value))
                {
                    SendPropertyChanging("IsPositionMerge", _IsPositionMerge, value);
                    _IsPositionMerge = value;
                    SendPropertyChanged("IsPositionMerge");
                }
            }
        }
        Boolean _IsPositionLocked = true;
        /// <summary>是否锁仓</summary>
        [DisallowNull]
        [Display(Name = "是否锁仓")]
        [Column("ispositionlocked")]
        public virtual Boolean IsPositionLocked
        {
            get
            {
                return _IsPositionLocked;
            }
            set
            {
                if ((_IsPositionLocked != value))
                {
                    SendPropertyChanging("IsPositionLocked", _IsPositionLocked, value);
                    _IsPositionLocked = value;
                    SendPropertyChanged("IsPositionLocked");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
        public virtual void SetValue(System.Linq.Expressions.Expression<Func<UserSetting, bool>> exp)
        {
            base.SetValue<UserSetting>(exp);
        }
    }
    /// <summary>币种</summary>
    [TableConfig]
    [Table("currency")]
    [Way.EntityDB.DataItemJsonConverter]
    public class Currency : Way.EntityDB.DataItem
    {
        System.Nullable<Int64> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int64> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id", _id, value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        string _Name;
        /// <summary>全称</summary>
        [MaxLength(50)]
        [DisallowNull]
        [Display(Name = "全称")]
        [Column("name")]
        public virtual string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if ((_Name != value))
                {
                    SendPropertyChanging("Name", _Name, value);
                    _Name = value;
                    SendPropertyChanged("Name");
                }
            }
        }
        string? _ShortName;
        /// <summary>简称</summary>
        [MaxLength(50)]
        [Display(Name = "简称")]
        [Column("shortname")]
        public virtual string? ShortName
        {
            get
            {
                return _ShortName;
            }
            set
            {
                if ((_ShortName != value))
                {
                    SendPropertyChanging("ShortName", _ShortName, value);
                    _ShortName = value;
                    SendPropertyChanged("ShortName");
                }
            }
        }
        string? _Unit;
        /// <summary>单位</summary>
        [MaxLength(10)]
        [Display(Name = "单位")]
        [Column("unit")]
        public virtual string? Unit
        {
            get
            {
                return _Unit;
            }
            set
            {
                if ((_Unit != value))
                {
                    SendPropertyChanging("Unit", _Unit, value);
                    _Unit = value;
                    SendPropertyChanged("Unit");
                }
            }
        }
        Int32 _Precision = 2;
        /// <summary>精度</summary>
        [DisallowNull]
        [Display(Name = "精度")]
        [Column("precision")]
        public virtual Int32 Precision
        {
            get
            {
                return _Precision;
            }
            set
            {
                if ((_Precision != value))
                {
                    SendPropertyChanging("Precision", _Precision, value);
                    _Precision = value;
                    SendPropertyChanged("Precision");
                }
            }
        }
        string? _Desc;
        /// <summary>简介</summary>
        [MaxLength(50)]
        [Display(Name = "简介")]
        [Column("desc")]
        public virtual string? Desc
        {
            get
            {
                return _Desc;
            }
            set
            {
                if ((_Desc != value))
                {
                    SendPropertyChanging("Desc", _Desc, value);
                    _Desc = value;
                    SendPropertyChanged("Desc");
                }
            }
        }
        Boolean _IsEnable = true;
        /// <summary>是否启用</summary>
        [DisallowNull]
        [Display(Name = "是否启用")]
        [Column("isenable")]
        public virtual Boolean IsEnable
        {
            get
            {
                return _IsEnable;
            }
            set
            {
                if ((_IsEnable != value))
                {
                    SendPropertyChanging("IsEnable", _IsEnable, value);
                    _IsEnable = value;
                    SendPropertyChanged("IsEnable");
                }
            }
        }
        MoneyAccount_AccountTypeEnum _ShowFlag = (MoneyAccount_AccountTypeEnum)(0);
        /// <summary>在哪些账户中显示</summary>
        [DisallowNull]
        [Display(Name = "在哪些账户中显示")]
        [Column("showflag")]
        public virtual MoneyAccount_AccountTypeEnum ShowFlag
        {
            get
            {
                return _ShowFlag;
            }
            set
            {
                if ((_ShowFlag != value))
                {
                    SendPropertyChanging("ShowFlag", _ShowFlag, value);
                    _ShowFlag = value;
                    SendPropertyChanged("ShowFlag");
                }
            }
        }
        string? _WithdrawFee;
        /// <summary>
        /// 提现手续费公式
        /// 如：{0}*0.03
        /// {0}表示提现金额
        /// </summary>
        [MaxLength(100)]
        [Display(Name = "提现手续费公式 如：{0}*0.03 {0}表示提现金额")]
        [Column("withdrawfee")]
        public virtual string? WithdrawFee
        {
            get
            {
                return _WithdrawFee;
            }
            set
            {
                if ((_WithdrawFee != value))
                {
                    SendPropertyChanging("WithdrawFee", _WithdrawFee, value);
                    _WithdrawFee = value;
                    SendPropertyChanged("WithdrawFee");
                }
            }
        }
        Int32 _Sort = 0;
        /// <summary>排序</summary>
        [DisallowNull]
        [Display(Name = "排序")]
        [Column("sort")]
        public virtual Int32 Sort
        {
            get
            {
                return _Sort;
            }
            set
            {
                if ((_Sort != value))
                {
                    SendPropertyChanging("Sort", _Sort, value);
                    _Sort = value;
                    SendPropertyChanged("Sort");
                }
            }
        }
        double _MinWithdraw = 0;
        /// <summary>最小提现金额</summary>
        [DisallowNull]
        [Display(Name = "最小提现金额")]
        [Column("minwithdraw")]
        public virtual double MinWithdraw
        {
            get
            {
                return _MinWithdraw;
            }
            set
            {
                if ((_MinWithdraw != value))
                {
                    SendPropertyChanging("MinWithdraw", _MinWithdraw, value);
                    _MinWithdraw = value;
                    SendPropertyChanged("MinWithdraw");
                }
            }
        }
        System.Nullable<Decimal> _MaxWithdraw;
        /// <summary>单次最大提现</summary>
        [Display(Name = "单次最大提现")]
        [Column("maxwithdraw")]
        public virtual System.Nullable<Decimal> MaxWithdraw
        {
            get
            {
                return _MaxWithdraw;
            }
            set
            {
                if ((_MaxWithdraw != value))
                {
                    SendPropertyChanging("MaxWithdraw", _MaxWithdraw, value);
                    _MaxWithdraw = value;
                    SendPropertyChanged("MaxWithdraw");
                }
            }
        }
        System.Nullable<Decimal> _DayWithdraw;
        /// <summary>单日最大提现</summary>
        [Display(Name = "单日最大提现")]
        [Column("daywithdraw")]
        public virtual System.Nullable<Decimal> DayWithdraw
        {
            get
            {
                return _DayWithdraw;
            }
            set
            {
                if ((_DayWithdraw != value))
                {
                    SendPropertyChanging("DayWithdraw", _DayWithdraw, value);
                    _DayWithdraw = value;
                    SendPropertyChanged("DayWithdraw");
                }
            }
        }
        System.Nullable<Decimal> _MinRecharge;
        /// <summary>最新充值金额</summary>
        [Display(Name = "最新充值金额")]
        [Column("minrecharge")]
        public virtual System.Nullable<Decimal> MinRecharge
        {
            get
            {
                return _MinRecharge;
            }
            set
            {
                if ((_MinRecharge != value))
                {
                    SendPropertyChanging("MinRecharge", _MinRecharge, value);
                    _MinRecharge = value;
                    SendPropertyChanged("MinRecharge");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
        public virtual void SetValue(System.Linq.Expressions.Expression<Func<Currency, bool>> exp)
        {
            base.SetValue<Currency>(exp);
        }
    }
    /// <summary>用户私钥</summary>
    [TableConfig]
    [Table("accountkeys")]
    [Way.EntityDB.DataItemJsonConverter]
    public class AccountKeys : Way.EntityDB.DataItem
    {
        System.Nullable<Int64> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int64> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id", _id, value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        string? _AddressPrivateKey;
        /// <summary>地址私钥</summary>
        [MaxLength(4096)]
        [Display(Name = "地址私钥")]
        [Column("addressprivatekey")]
        public virtual string? AddressPrivateKey
        {
            get
            {
                return _AddressPrivateKey;
            }
            set
            {
                if ((_AddressPrivateKey != value))
                {
                    SendPropertyChanging("AddressPrivateKey", _AddressPrivateKey, value);
                    _AddressPrivateKey = value;
                    SendPropertyChanged("AddressPrivateKey");
                }
            }
        }
        Int64 _UserId;
        [DisallowNull]
        [Column("userid")]
        public virtual Int64 UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                if ((_UserId != value))
                {
                    SendPropertyChanging("UserId", _UserId, value);
                    _UserId = value;
                    SendPropertyChanged("UserId");
                }
            }
        }
        string? _Address;
        /// <summary>充币地址</summary>
        [MaxLength(50)]
        [Display(Name = "充币地址")]
        [Column("address")]
        public virtual string? Address
        {
            get
            {
                return _Address;
            }
            set
            {
                if ((_Address != value))
                {
                    SendPropertyChanging("Address", _Address, value);
                    _Address = value;
                    SendPropertyChanged("Address");
                }
            }
        }
        string _Link;
        /// <summary>属于哪条链</summary>
        [MaxLength(50)]
        [DisallowNull]
        [Display(Name = "属于哪条链")]
        [Column("link")]
        public virtual string Link
        {
            get
            {
                return _Link;
            }
            set
            {
                if ((_Link != value))
                {
                    SendPropertyChanging("Link", _Link, value);
                    _Link = value;
                    SendPropertyChanged("Link");
                }
            }
        }
        System.Nullable<Int64> _MoneyAccountId;
        [Column("moneyaccountid")]
        public virtual System.Nullable<Int64> MoneyAccountId
        {
            get
            {
                return _MoneyAccountId;
            }
            set
            {
                if ((_MoneyAccountId != value))
                {
                    SendPropertyChanging("MoneyAccountId", _MoneyAccountId, value);
                    _MoneyAccountId = value;
                    SendPropertyChanged("MoneyAccountId");
                }
            }
        }
        System.Nullable<Decimal> _LinkBalance;
        /// <summary>链上余额</summary>
        [Display(Name = "链上余额")]
        [Column("linkbalance")]
        public virtual System.Nullable<Decimal> LinkBalance
        {
            get
            {
                return _LinkBalance;
            }
            set
            {
                if ((_LinkBalance != value))
                {
                    SendPropertyChanging("LinkBalance", _LinkBalance, value);
                    _LinkBalance = value;
                    SendPropertyChanged("LinkBalance");
                }
            }
        }
        System.Nullable<DateTime> _UpdateLinkBalanceTime;
        /// <summary>更新链上余额的时间</summary>
        [Display(Name = "更新链上余额的时间")]
        [Column("updatelinkbalancetime")]
        public virtual System.Nullable<DateTime> UpdateLinkBalanceTime
        {
            get
            {
                return _UpdateLinkBalanceTime;
            }
            set
            {
                if ((_UpdateLinkBalanceTime != value))
                {
                    SendPropertyChanging("UpdateLinkBalanceTime", _UpdateLinkBalanceTime, value);
                    _UpdateLinkBalanceTime = value;
                    SendPropertyChanged("UpdateLinkBalanceTime");
                }
            }
        }
        System.Nullable<DateTime> _CreateTime;
        [Column("createtime")]
        public virtual System.Nullable<DateTime> CreateTime
        {
            get
            {
                return _CreateTime;
            }
            set
            {
                if ((_CreateTime != value))
                {
                    SendPropertyChanging("CreateTime", _CreateTime, value);
                    _CreateTime = value;
                    SendPropertyChanged("CreateTime");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
        public virtual void SetValue(System.Linq.Expressions.Expression<Func<AccountKeys, bool>> exp)
        {
            base.SetValue<AccountKeys>(exp);
        }
    }
    /// <summary>提现表</summary>
    [TableConfig]
    [Table("withdraw")]
    [Way.EntityDB.DataItemJsonConverter]
    public class Withdraw : Way.EntityDB.DataItem
    {
        System.Nullable<Int64> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int64> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id", _id, value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        Int64 _MoneyAccountId;
        /// <summary>资金表id</summary>
        [DisallowNull]
        [Display(Name = "资金表id")]
        [Column("moneyaccountid")]
        public virtual Int64 MoneyAccountId
        {
            get
            {
                return _MoneyAccountId;
            }
            set
            {
                if ((_MoneyAccountId != value))
                {
                    SendPropertyChanging("MoneyAccountId", _MoneyAccountId, value);
                    _MoneyAccountId = value;
                    SendPropertyChanged("MoneyAccountId");
                }
            }
        }
        Decimal _Amount;
        /// <summary>提现金额</summary>
        [DisallowNull]
        [Display(Name = "提现金额")]
        [Column("amount")]
        public virtual Decimal Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                if ((_Amount != value))
                {
                    SendPropertyChanging("Amount", _Amount, value);
                    _Amount = value;
                    SendPropertyChanged("Amount");
                }
            }
        }
        DateTime _CreateTime;
        /// <summary>提现时间</summary>
        [DisallowNull]
        [Display(Name = "提现时间")]
        [Column("createtime")]
        public virtual DateTime CreateTime
        {
            get
            {
                return _CreateTime;
            }
            set
            {
                if ((_CreateTime != value))
                {
                    SendPropertyChanging("CreateTime", _CreateTime, value);
                    _CreateTime = value;
                    SendPropertyChanged("CreateTime");
                }
            }
        }
        Withdraw_StatusEnum _Status = (Withdraw_StatusEnum)(1);
        /// <summary>状态</summary>
        [DisallowNull]
        [Display(Name = "状态")]
        [Column("status")]
        public virtual Withdraw_StatusEnum Status
        {
            get
            {
                return _Status;
            }
            set
            {
                if ((_Status != value))
                {
                    SendPropertyChanging("Status", _Status, value);
                    _Status = value;
                    SendPropertyChanged("Status");
                }
            }
        }
        DateTime _UpdateTime;
        /// <summary>最后更新时间</summary>
        [DisallowNull]
        [Display(Name = "最后更新时间")]
        [Column("updatetime")]
        public virtual DateTime UpdateTime
        {
            get
            {
                return _UpdateTime;
            }
            set
            {
                if ((_UpdateTime != value))
                {
                    SendPropertyChanging("UpdateTime", _UpdateTime, value);
                    _UpdateTime = value;
                    SendPropertyChanged("UpdateTime");
                }
            }
        }
        System.Nullable<Int64> _ExamUserId;
        /// <summary>审核人id</summary>
        [Display(Name = "审核人id")]
        [Column("examuserid")]
        public virtual System.Nullable<Int64> ExamUserId
        {
            get
            {
                return _ExamUserId;
            }
            set
            {
                if ((_ExamUserId != value))
                {
                    SendPropertyChanging("ExamUserId", _ExamUserId, value);
                    _ExamUserId = value;
                    SendPropertyChanged("ExamUserId");
                }
            }
        }
        System.Nullable<DateTime> _ExamTime;
        /// <summary>审核时间</summary>
        [Display(Name = "审核时间")]
        [Column("examtime")]
        public virtual System.Nullable<DateTime> ExamTime
        {
            get
            {
                return _ExamTime;
            }
            set
            {
                if ((_ExamTime != value))
                {
                    SendPropertyChanging("ExamTime", _ExamTime, value);
                    _ExamTime = value;
                    SendPropertyChanged("ExamTime");
                }
            }
        }
        string? _FailReason;
        /// <summary>用户看到的失败原因</summary>
        [MaxLength(100)]
        [Display(Name = "用户看到的失败原因")]
        [Column("failreason")]
        public virtual string? FailReason
        {
            get
            {
                return _FailReason;
            }
            set
            {
                if ((_FailReason != value))
                {
                    SendPropertyChanging("FailReason", _FailReason, value);
                    _FailReason = value;
                    SendPropertyChanged("FailReason");
                }
            }
        }
        string? _Address;
        /// <summary>提现到什么地址</summary>
        [MaxLength(50)]
        [Display(Name = "提现到什么地址")]
        [Column("address")]
        public virtual string? Address
        {
            get
            {
                return _Address;
            }
            set
            {
                if ((_Address != value))
                {
                    SendPropertyChanging("Address", _Address, value);
                    _Address = value;
                    SendPropertyChanged("Address");
                }
            }
        }
        string? _Coin;
        /// <summary>币种</summary>
        [MaxLength(50)]
        [Display(Name = "币种")]
        [Column("coin")]
        public virtual string? Coin
        {
            get
            {
                return _Coin;
            }
            set
            {
                if ((_Coin != value))
                {
                    SendPropertyChanging("Coin", _Coin, value);
                    _Coin = value;
                    SendPropertyChanged("Coin");
                }
            }
        }
        string? _Link;
        /// <summary>地址在哪条链</summary>
        [MaxLength(50)]
        [Display(Name = "地址在哪条链")]
        [Column("link")]
        public virtual string? Link
        {
            get
            {
                return _Link;
            }
            set
            {
                if ((_Link != value))
                {
                    SendPropertyChanging("Link", _Link, value);
                    _Link = value;
                    SendPropertyChanged("Link");
                }
            }
        }
        Int64 _UserId;
        [DisallowNull]
        [Column("userid")]
        public virtual Int64 UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                if ((_UserId != value))
                {
                    SendPropertyChanging("UserId", _UserId, value);
                    _UserId = value;
                    SendPropertyChanged("UserId");
                }
            }
        }
        string? _TxId;
        /// <summary>链上转账的交易id</summary>
        [MaxLength(100)]
        [Display(Name = "链上转账的交易id")]
        [Column("txid")]
        public virtual string? TxId
        {
            get
            {
                return _TxId;
            }
            set
            {
                if ((_TxId != value))
                {
                    SendPropertyChanging("TxId", _TxId, value);
                    _TxId = value;
                    SendPropertyChanged("TxId");
                }
            }
        }
        System.Nullable<Decimal> _Fee;
        /// <summary>手续费</summary>
        [Display(Name = "手续费")]
        [Column("fee")]
        public virtual System.Nullable<Decimal> Fee
        {
            get
            {
                return _Fee;
            }
            set
            {
                if ((_Fee != value))
                {
                    SendPropertyChanging("Fee", _Fee, value);
                    _Fee = value;
                    SendPropertyChanged("Fee");
                }
            }
        }
        string? _TransferFailReason;
        /// <summary>转账失败原因</summary>
        [MaxLength(200)]
        [Display(Name = "转账失败原因")]
        [Column("transferfailreason")]
        public virtual string? TransferFailReason
        {
            get
            {
                return _TransferFailReason;
            }
            set
            {
                if ((_TransferFailReason != value))
                {
                    SendPropertyChanging("TransferFailReason", _TransferFailReason, value);
                    _TransferFailReason = value;
                    SendPropertyChanged("TransferFailReason");
                }
            }
        }
        string? _UserRealName;
        /// <summary>用户姓名</summary>
        [MaxLength(50)]
        [Display(Name = "用户姓名")]
        [Column("userrealname")]
        public virtual string? UserRealName
        {
            get
            {
                return _UserRealName;
            }
            set
            {
                if ((_UserRealName != value))
                {
                    SendPropertyChanging("UserRealName", _UserRealName, value);
                    _UserRealName = value;
                    SendPropertyChanged("UserRealName");
                }
            }
        }
        string? _ExamUserName;
        /// <summary>审核人姓名</summary>
        [MaxLength(50)]
        [Display(Name = "审核人姓名")]
        [Column("examusername")]
        public virtual string? ExamUserName
        {
            get
            {
                return _ExamUserName;
            }
            set
            {
                if ((_ExamUserName != value))
                {
                    SendPropertyChanging("ExamUserName", _ExamUserName, value);
                    _ExamUserName = value;
                    SendPropertyChanged("ExamUserName");
                }
            }
        }
        System.Nullable<Decimal> _AdditionalFee;
        /// <summary>追加手续费</summary>
        [Display(Name = "追加手续费")]
        [Column("additionalfee")]
        public virtual System.Nullable<Decimal> AdditionalFee
        {
            get
            {
                return _AdditionalFee;
            }
            set
            {
                if ((_AdditionalFee != value))
                {
                    SendPropertyChanging("AdditionalFee", _AdditionalFee, value);
                    _AdditionalFee = value;
                    SendPropertyChanged("AdditionalFee");
                }
            }
        }
        string? _Remarks;
        /// <summary>备注</summary>
        [MaxLength(255)]
        [Display(Name = "备注")]
        [Column("remarks")]
        public virtual string? Remarks
        {
            get
            {
                return _Remarks;
            }
            set
            {
                if ((_Remarks != value))
                {
                    SendPropertyChanging("Remarks", _Remarks, value);
                    _Remarks = value;
                    SendPropertyChanged("Remarks");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
        public virtual void SetValue(System.Linq.Expressions.Expression<Func<Withdraw, bool>> exp)
        {
            base.SetValue<Withdraw>(exp);
        }
    }
    public enum Withdraw_StatusEnum : int
    {
        /// <summary>待审核</summary>
        Waiting = 1,
        /// <summary>追加手续费，待用户确认</summary>
        WaitingUser = 101,
        /// <summary>询问用户是否变更提现地址</summary>
        WaitingUserChangeAddress = 102,
        /// <summary>用户确认不变更地址</summary>
        UserDontChangeAddress = 103,
        /// <summary>提醒用户限制提币</summary>
        WithdrawWarnning = 104,
        /// <summary>审核通过</summary>
        Pass = 2,
        /// <summary>转账中</summary>
        Transfering = 3,
        /// <summary>转账成功</summary>
        TransferSuccess = 4,
        /// <summary>转账失败</summary>
        TransferFail = 8,
        /// <summary>审核失败</summary>
        NoPass = 9,
        /// <summary>人工设置失败</summary>
        TransferFailByManual = 10,
        /// <summary>人工设置转账成功</summary>
        SuccessByManual = 11,
        /// <summary>取消提现</summary>
        Cancel = 12
    }
    /// <summary>币种充值地址表</summary>
    [TableConfig]
    [Table("coinaddress")]
    [Way.EntityDB.DataItemJsonConverter]
    public class CoinAddress : Way.EntityDB.DataItem
    {
        System.Nullable<Int64> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int64> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id", _id, value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        System.Nullable<Int64> _UserId;
        /// <summary>用户id，如果是null，表示没有用户使用</summary>
        [Display(Name = "用户id，如果是null，表示没有用户使用")]
        [Column("userid")]
        public virtual System.Nullable<Int64> UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                if ((_UserId != value))
                {
                    SendPropertyChanging("UserId", _UserId, value);
                    _UserId = value;
                    SendPropertyChanged("UserId");
                }
            }
        }
        string _LinkName;
        /// <summary>哪条链</summary>
        [MaxLength(50)]
        [DisallowNull]
        [Display(Name = "哪条链")]
        [Column("linkname")]
        public virtual string LinkName
        {
            get
            {
                return _LinkName;
            }
            set
            {
                if ((_LinkName != value))
                {
                    SendPropertyChanging("LinkName", _LinkName, value);
                    _LinkName = value;
                    SendPropertyChanged("LinkName");
                }
            }
        }
        string _Address;
        /// <summary>地址</summary>
        [MaxLength(50)]
        [DisallowNull]
        [Display(Name = "地址")]
        [Column("address")]
        public virtual string Address
        {
            get
            {
                return _Address;
            }
            set
            {
                if ((_Address != value))
                {
                    SendPropertyChanging("Address", _Address, value);
                    _Address = value;
                    SendPropertyChanged("Address");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
        public virtual void SetValue(System.Linq.Expressions.Expression<Func<CoinAddress, bool>> exp)
        {
            base.SetValue<CoinAddress>(exp);
        }
    }
    /// <summary>会员规则</summary>
    [TableConfig]
    [Table("memberrule")]
    [Way.EntityDB.DataItemJsonConverter]
    public class MemberRule : Way.EntityDB.DataItem
    {
        System.Nullable<Int64> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int64> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id", _id, value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        string? _RuleName;
        /// <summary>规则名称</summary>
        [MaxLength(255)]
        [Display(Name = "规则名称")]
        [Column("rulename")]
        public virtual string? RuleName
        {
            get
            {
                return _RuleName;
            }
            set
            {
                if ((_RuleName != value))
                {
                    SendPropertyChanging("RuleName", _RuleName, value);
                    _RuleName = value;
                    SendPropertyChanged("RuleName");
                }
            }
        }
        string? _RuleValue;
        /// <summary>规则内容</summary>
        [MaxLength(255)]
        [Display(Name = "规则内容")]
        [Column("rulevalue")]
        public virtual string? RuleValue
        {
            get
            {
                return _RuleValue;
            }
            set
            {
                if ((_RuleValue != value))
                {
                    SendPropertyChanging("RuleValue", _RuleValue, value);
                    _RuleValue = value;
                    SendPropertyChanged("RuleValue");
                }
            }
        }
        string? _CREATED_BY;
        /// <summary>创建人</summary>
        [MaxLength(50)]
        [Display(Name = "创建人")]
        [Column("created_by")]
        public virtual string? CREATED_BY
        {
            get
            {
                return _CREATED_BY;
            }
            set
            {
                if ((_CREATED_BY != value))
                {
                    SendPropertyChanging("CREATED_BY", _CREATED_BY, value);
                    _CREATED_BY = value;
                    SendPropertyChanged("CREATED_BY");
                }
            }
        }
        System.Nullable<DateTime> _CREATED_TIME;
        /// <summary>创建时间</summary>
        [Display(Name = "创建时间")]
        [Column("created_time")]
        public virtual System.Nullable<DateTime> CREATED_TIME
        {
            get
            {
                return _CREATED_TIME;
            }
            set
            {
                if ((_CREATED_TIME != value))
                {
                    SendPropertyChanging("CREATED_TIME", _CREATED_TIME, value);
                    _CREATED_TIME = value;
                    SendPropertyChanged("CREATED_TIME");
                }
            }
        }
        string? _UPDATED_BY;
        /// <summary>更新人</summary>
        [MaxLength(50)]
        [Display(Name = "更新人")]
        [Column("updated_by")]
        public virtual string? UPDATED_BY
        {
            get
            {
                return _UPDATED_BY;
            }
            set
            {
                if ((_UPDATED_BY != value))
                {
                    SendPropertyChanging("UPDATED_BY", _UPDATED_BY, value);
                    _UPDATED_BY = value;
                    SendPropertyChanged("UPDATED_BY");
                }
            }
        }
        System.Nullable<DateTime> _UPDATED_TIME;
        /// <summary>更新时间</summary>
        [Display(Name = "更新时间")]
        [Column("updated_time")]
        public virtual System.Nullable<DateTime> UPDATED_TIME
        {
            get
            {
                return _UPDATED_TIME;
            }
            set
            {
                if ((_UPDATED_TIME != value))
                {
                    SendPropertyChanging("UPDATED_TIME", _UPDATED_TIME, value);
                    _UPDATED_TIME = value;
                    SendPropertyChanged("UPDATED_TIME");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
        public virtual void SetValue(System.Linq.Expressions.Expression<Func<MemberRule, bool>> exp)
        {
            base.SetValue<MemberRule>(exp);
        }
    }
    /// <summary>积分明细</summary>
    [TableConfig]
    [Table("integraldetail")]
    [Way.EntityDB.DataItemJsonConverter]
    public class IntegralDetail : Way.EntityDB.DataItem
    {
        System.Nullable<Int64> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int64> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id", _id, value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        Decimal _Score = 0m;
        /// <summary>积分</summary>
        [DisallowNull]
        [Display(Name = "积分")]
        [Column("score")]
        public virtual Decimal Score
        {
            get
            {
                return _Score;
            }
            set
            {
                if ((_Score != value))
                {
                    SendPropertyChanging("Score", _Score, value);
                    _Score = value;
                    SendPropertyChanged("Score");
                }
            }
        }
        Int32 _Type;
        /// <summary>
        /// 类型
        /// 1=充值
        /// 2=交易
        /// 3=盈利
        /// 4=亏损
        /// 5=邀请好友
        /// 6=手续费
        /// 7=邀请好友开仓
        /// </summary>
        [DisallowNull]
        [Display(Name = "类型 1=充值 2=交易 3=盈利 4=亏损 5=邀请好友 6=手续费 7=邀请好友开仓")]
        [Column("type")]
        public virtual Int32 Type
        {
            get
            {
                return _Type;
            }
            set
            {
                if ((_Type != value))
                {
                    SendPropertyChanging("Type", _Type, value);
                    _Type = value;
                    SendPropertyChanged("Type");
                }
            }
        }
        DateTime _CreateTime;
        /// <summary>创建时间</summary>
        [DisallowNull]
        [Display(Name = "创建时间")]
        [Column("createtime")]
        public virtual DateTime CreateTime
        {
            get
            {
                return _CreateTime;
            }
            set
            {
                if ((_CreateTime != value))
                {
                    SendPropertyChanging("CreateTime", _CreateTime, value);
                    _CreateTime = value;
                    SendPropertyChanged("CreateTime");
                }
            }
        }
        Int64 _UserId;
        /// <summary>用户id</summary>
        [DisallowNull]
        [Display(Name = "用户id")]
        [Column("userid")]
        public virtual Int64 UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                if ((_UserId != value))
                {
                    SendPropertyChanging("UserId", _UserId, value);
                    _UserId = value;
                    SendPropertyChanged("UserId");
                }
            }
        }
        Decimal _TotalSocre;
        /// <summary>总积分</summary>
        [DisallowNull]
        [Display(Name = "总积分")]
        [Column("totalsocre")]
        public virtual Decimal TotalSocre
        {
            get
            {
                return _TotalSocre;
            }
            set
            {
                if ((_TotalSocre != value))
                {
                    SendPropertyChanging("TotalSocre", _TotalSocre, value);
                    _TotalSocre = value;
                    SendPropertyChanged("TotalSocre");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
        public virtual void SetValue(System.Linq.Expressions.Expression<Func<IntegralDetail, bool>> exp)
        {
            base.SetValue<IntegralDetail>(exp);
        }
    }
    /// <summary>会员等级配置表</summary>
    [TableConfig]
    [Table("memberlevel")]
    [Way.EntityDB.DataItemJsonConverter]
    public class MemberLevel : Way.EntityDB.DataItem
    {
        System.Nullable<Int64> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int64> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id", _id, value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        Int32 _Level;
        /// <summary>等级</summary>
        [DisallowNull]
        [Display(Name = "等级")]
        [Column("level")]
        public virtual Int32 Level
        {
            get
            {
                return _Level;
            }
            set
            {
                if ((_Level != value))
                {
                    SendPropertyChanging("Level", _Level, value);
                    _Level = value;
                    SendPropertyChanged("Level");
                }
            }
        }
        Decimal _MaxScore;
        /// <summary>最大分数</summary>
        [DisallowNull]
        [Display(Name = "最大分数")]
        [Column("maxscore")]
        public virtual Decimal MaxScore
        {
            get
            {
                return _MaxScore;
            }
            set
            {
                if ((_MaxScore != value))
                {
                    SendPropertyChanging("MaxScore", _MaxScore, value);
                    _MaxScore = value;
                    SendPropertyChanged("MaxScore");
                }
            }
        }
        Decimal _MinScore;
        /// <summary>最小分数</summary>
        [DisallowNull]
        [Display(Name = "最小分数")]
        [Column("minscore")]
        public virtual Decimal MinScore
        {
            get
            {
                return _MinScore;
            }
            set
            {
                if ((_MinScore != value))
                {
                    SendPropertyChanging("MinScore", _MinScore, value);
                    _MinScore = value;
                    SendPropertyChanged("MinScore");
                }
            }
        }
        DateTime _CreateTime;
        /// <summary>创建时间</summary>
        [DisallowNull]
        [Display(Name = "创建时间")]
        [Column("createtime")]
        public virtual DateTime CreateTime
        {
            get
            {
                return _CreateTime;
            }
            set
            {
                if ((_CreateTime != value))
                {
                    SendPropertyChanging("CreateTime", _CreateTime, value);
                    _CreateTime = value;
                    SendPropertyChanged("CreateTime");
                }
            }
        }
        System.Nullable<DateTime> _UpdateTime;
        /// <summary>更新时间</summary>
        [Display(Name = "更新时间")]
        [Column("updatetime")]
        public virtual System.Nullable<DateTime> UpdateTime
        {
            get
            {
                return _UpdateTime;
            }
            set
            {
                if ((_UpdateTime != value))
                {
                    SendPropertyChanging("UpdateTime", _UpdateTime, value);
                    _UpdateTime = value;
                    SendPropertyChanged("UpdateTime");
                }
            }
        }
        string? _Statements;
        /// <summary>拥有哪些权益</summary>
        [MaxLength(255)]
        [Display(Name = "拥有哪些权益")]
        [Column("statements")]
        public virtual string? Statements
        {
            get
            {
                return _Statements;
            }
            set
            {
                if ((_Statements != value))
                {
                    SendPropertyChanging("Statements", _Statements, value);
                    _Statements = value;
                    SendPropertyChanged("Statements");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
        public virtual void SetValue(System.Linq.Expressions.Expression<Func<MemberLevel, bool>> exp)
        {
            base.SetValue<MemberLevel>(exp);
        }
    }
    /// <summary>用户等级表</summary>
    [TableConfig]
    [Table("userintegral")]
    [Way.EntityDB.DataItemJsonConverter]
    public class UserIntegral : Way.EntityDB.DataItem
    {
        System.Nullable<Int64> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int64> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id", _id, value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        Int64 _UserId;
        /// <summary>用户id</summary>
        [DisallowNull]
        [Display(Name = "用户id")]
        [Column("userid")]
        public virtual Int64 UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                if ((_UserId != value))
                {
                    SendPropertyChanging("UserId", _UserId, value);
                    _UserId = value;
                    SendPropertyChanged("UserId");
                }
            }
        }
        Int32 _Level;
        /// <summary>当前等级</summary>
        [DisallowNull]
        [Display(Name = "当前等级")]
        [Column("level")]
        public virtual Int32 Level
        {
            get
            {
                return _Level;
            }
            set
            {
                if ((_Level != value))
                {
                    SendPropertyChanging("Level", _Level, value);
                    _Level = value;
                    SendPropertyChanged("Level");
                }
            }
        }
        Decimal _TotalSocre = 0m;
        /// <summary>总积分</summary>
        [DisallowNull]
        [Display(Name = "总积分")]
        [Column("totalsocre")]
        public virtual Decimal TotalSocre
        {
            get
            {
                return _TotalSocre;
            }
            set
            {
                if ((_TotalSocre != value))
                {
                    SendPropertyChanging("TotalSocre", _TotalSocre, value);
                    _TotalSocre = value;
                    SendPropertyChanged("TotalSocre");
                }
            }
        }
        string? _AvatarframeId;
        /// <summary>头像框编号</summary>
        [MaxLength(255)]
        [Display(Name = "头像框编号")]
        [Column("avatarframeid")]
        public virtual string? AvatarframeId
        {
            get
            {
                return _AvatarframeId;
            }
            set
            {
                if ((_AvatarframeId != value))
                {
                    SendPropertyChanging("AvatarframeId", _AvatarframeId, value);
                    _AvatarframeId = value;
                    SendPropertyChanged("AvatarframeId");
                }
            }
        }
        DateTime _CreateTime;
        /// <summary>创建时间</summary>
        [DisallowNull]
        [Display(Name = "创建时间")]
        [Column("createtime")]
        public virtual DateTime CreateTime
        {
            get
            {
                return _CreateTime;
            }
            set
            {
                if ((_CreateTime != value))
                {
                    SendPropertyChanging("CreateTime", _CreateTime, value);
                    _CreateTime = value;
                    SendPropertyChanged("CreateTime");
                }
            }
        }
        System.Nullable<DateTime> _UpdateTime;
        /// <summary>更新时间</summary>
        [Display(Name = "更新时间")]
        [Column("updatetime")]
        public virtual System.Nullable<DateTime> UpdateTime
        {
            get
            {
                return _UpdateTime;
            }
            set
            {
                if ((_UpdateTime != value))
                {
                    SendPropertyChanging("UpdateTime", _UpdateTime, value);
                    _UpdateTime = value;
                    SendPropertyChanged("UpdateTime");
                }
            }
        }
        System.Nullable<Int64> _Pid;
        /// <summary>上级id</summary>
        [Display(Name = "上级id")]
        [Column("pid")]
        public virtual System.Nullable<Int64> Pid
        {
            get
            {
                return _Pid;
            }
            set
            {
                if ((_Pid != value))
                {
                    SendPropertyChanging("Pid", _Pid, value);
                    _Pid = value;
                    SendPropertyChanged("Pid");
                }
            }
        }
        string? _Remark;
        /// <summary>备注信息</summary>
        [MaxLength(255)]
        [Display(Name = "备注信息")]
        [Column("remark")]
        public virtual string? Remark
        {
            get
            {
                return _Remark;
            }
            set
            {
                if ((_Remark != value))
                {
                    SendPropertyChanging("Remark", _Remark, value);
                    _Remark = value;
                    SendPropertyChanged("Remark");
                }
            }
        }
        System.Nullable<DateTime> _LastTradeTime;
        /// <summary>最后一次交易时间</summary>
        [Display(Name = "最后一次交易时间")]
        [Column("lasttradetime")]
        public virtual System.Nullable<DateTime> LastTradeTime
        {
            get
            {
                return _LastTradeTime;
            }
            set
            {
                if ((_LastTradeTime != value))
                {
                    SendPropertyChanging("LastTradeTime", _LastTradeTime, value);
                    _LastTradeTime = value;
                    SendPropertyChanged("LastTradeTime");
                }
            }
        }
        Int32 _ContinueTradeDays = 0;
        /// <summary>连续交易天数</summary>
        [DisallowNull]
        [Display(Name = "连续交易天数")]
        [Column("continuetradedays")]
        public virtual Int32 ContinueTradeDays
        {
            get
            {
                return _ContinueTradeDays;
            }
            set
            {
                if ((_ContinueTradeDays != value))
                {
                    SendPropertyChanging("ContinueTradeDays", _ContinueTradeDays, value);
                    _ContinueTradeDays = value;
                    SendPropertyChanged("ContinueTradeDays");
                }
            }
        }
        Int32 _TotalProfitCount = 0;
        /// <summary>累计盈利次数</summary>
        [DisallowNull]
        [Display(Name = "累计盈利次数")]
        [Column("totalprofitcount")]
        public virtual Int32 TotalProfitCount
        {
            get
            {
                return _TotalProfitCount;
            }
            set
            {
                if ((_TotalProfitCount != value))
                {
                    SendPropertyChanging("TotalProfitCount", _TotalProfitCount, value);
                    _TotalProfitCount = value;
                    SendPropertyChanged("TotalProfitCount");
                }
            }
        }
        Decimal _TotalProfit = 0m;
        /// <summary>累计盈利金额</summary>
        [DisallowNull]
        [Display(Name = "累计盈利金额")]
        [Column("totalprofit")]
        public virtual Decimal TotalProfit
        {
            get
            {
                return _TotalProfit;
            }
            set
            {
                if ((_TotalProfit != value))
                {
                    SendPropertyChanging("TotalProfit", _TotalProfit, value);
                    _TotalProfit = value;
                    SendPropertyChanged("TotalProfit");
                }
            }
        }
        Decimal _TotalRecharge = 0m;
        /// <summary>累计充值金额</summary>
        [DisallowNull]
        [Display(Name = "累计充值金额")]
        [Column("totalrecharge")]
        public virtual Decimal TotalRecharge
        {
            get
            {
                return _TotalRecharge;
            }
            set
            {
                if ((_TotalRecharge != value))
                {
                    SendPropertyChanging("TotalRecharge", _TotalRecharge, value);
                    _TotalRecharge = value;
                    SendPropertyChanged("TotalRecharge");
                }
            }
        }
        Decimal _TotalMargin = 0m;
        /// <summary>累计保证金</summary>
        [DisallowNull]
        [Display(Name = "累计保证金")]
        [Column("totalmargin")]
        public virtual Decimal TotalMargin
        {
            get
            {
                return _TotalMargin;
            }
            set
            {
                if ((_TotalMargin != value))
                {
                    SendPropertyChanging("TotalMargin", _TotalMargin, value);
                    _TotalMargin = value;
                    SendPropertyChanged("TotalMargin");
                }
            }
        }
        Int32 _ContinueProfits = 0;
        /// <summary>连续盈利次数</summary>
        [DisallowNull]
        [Display(Name = "连续盈利次数")]
        [Column("continueprofits")]
        public virtual Int32 ContinueProfits
        {
            get
            {
                return _ContinueProfits;
            }
            set
            {
                if ((_ContinueProfits != value))
                {
                    SendPropertyChanging("ContinueProfits", _ContinueProfits, value);
                    _ContinueProfits = value;
                    SendPropertyChanged("ContinueProfits");
                }
            }
        }
        Int32 _DayTradingCount = 0;
        /// <summary>当天开仓笔数</summary>
        [DisallowNull]
        [Display(Name = "当天开仓笔数")]
        [Column("daytradingcount")]
        public virtual Int32 DayTradingCount
        {
            get
            {
                return _DayTradingCount;
            }
            set
            {
                if ((_DayTradingCount != value))
                {
                    SendPropertyChanging("DayTradingCount", _DayTradingCount, value);
                    _DayTradingCount = value;
                    SendPropertyChanged("DayTradingCount");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
        public virtual void SetValue(System.Linq.Expressions.Expression<Func<UserIntegral, bool>> exp)
        {
            base.SetValue<UserIntegral>(exp);
        }
    }
    /// <summary>会员勋章</summary>
    [TableConfig]
    [Table("membermedal")]
    [Way.EntityDB.DataItemJsonConverter]
    public class MemberMedal : Way.EntityDB.DataItem
    {
        System.Nullable<Int64> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int64> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id", _id, value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        Int64 _UserId;
        /// <summary>用户id</summary>
        [DisallowNull]
        [Display(Name = "用户id")]
        [Column("userid")]
        public virtual Int64 UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                if ((_UserId != value))
                {
                    SendPropertyChanging("UserId", _UserId, value);
                    _UserId = value;
                    SendPropertyChanged("UserId");
                }
            }
        }
        Int32 _MedalNo;
        /// <summary>勋章编号</summary>
        [DisallowNull]
        [Display(Name = "勋章编号")]
        [Column("medalno")]
        public virtual Int32 MedalNo
        {
            get
            {
                return _MedalNo;
            }
            set
            {
                if ((_MedalNo != value))
                {
                    SendPropertyChanging("MedalNo", _MedalNo, value);
                    _MedalNo = value;
                    SendPropertyChanged("MedalNo");
                }
            }
        }
        Int32 _MedalLevel = 1;
        /// <summary>勋章等级</summary>
        [DisallowNull]
        [Display(Name = "勋章等级")]
        [Column("medallevel")]
        public virtual Int32 MedalLevel
        {
            get
            {
                return _MedalLevel;
            }
            set
            {
                if ((_MedalLevel != value))
                {
                    SendPropertyChanging("MedalLevel", _MedalLevel, value);
                    _MedalLevel = value;
                    SendPropertyChanged("MedalLevel");
                }
            }
        }
        System.Nullable<DateTime> _CreateTime;
        /// <summary>创建时间</summary>
        [Display(Name = "创建时间")]
        [Column("createtime")]
        public virtual System.Nullable<DateTime> CreateTime
        {
            get
            {
                return _CreateTime;
            }
            set
            {
                if ((_CreateTime != value))
                {
                    SendPropertyChanging("CreateTime", _CreateTime, value);
                    _CreateTime = value;
                    SendPropertyChanged("CreateTime");
                }
            }
        }
        System.Nullable<DateTime> _UpdateTime;
        /// <summary>更新时间</summary>
        [Display(Name = "更新时间")]
        [Column("updatetime")]
        public virtual System.Nullable<DateTime> UpdateTime
        {
            get
            {
                return _UpdateTime;
            }
            set
            {
                if ((_UpdateTime != value))
                {
                    SendPropertyChanging("UpdateTime", _UpdateTime, value);
                    _UpdateTime = value;
                    SendPropertyChanged("UpdateTime");
                }
            }
        }
        Boolean _IsShow = true;
        /// <summary>是否在首页显示</summary>
        [DisallowNull]
        [Display(Name = "是否在首页显示")]
        [Column("isshow")]
        public virtual Boolean IsShow
        {
            get
            {
                return _IsShow;
            }
            set
            {
                if ((_IsShow != value))
                {
                    SendPropertyChanging("IsShow", _IsShow, value);
                    _IsShow = value;
                    SendPropertyChanged("IsShow");
                }
            }
        }
        Boolean _HasNotification = false;
        /// <summary>是否已通知前端</summary>
        [DisallowNull]
        [Display(Name = "是否已通知前端")]
        [Column("hasnotification")]
        public virtual Boolean HasNotification
        {
            get
            {
                return _HasNotification;
            }
            set
            {
                if ((_HasNotification != value))
                {
                    SendPropertyChanging("HasNotification", _HasNotification, value);
                    _HasNotification = value;
                    SendPropertyChanged("HasNotification");
                }
            }
        }
        System.Nullable<Int32> _Num;
        /// <summary>用户是第几个人获取的这个勋章</summary>
        [Display(Name = "用户是第几个人获取的这个勋章")]
        [Column("num")]
        public virtual System.Nullable<Int32> Num
        {
            get
            {
                return _Num;
            }
            set
            {
                if ((_Num != value))
                {
                    SendPropertyChanging("Num", _Num, value);
                    _Num = value;
                    SendPropertyChanged("Num");
                }
            }
        }
        string? _MedalOrderId;
        /// <summary>勋章流水号</summary>
        [MaxLength(50)]
        [Display(Name = "勋章流水号")]
        [Column("medalorderid")]
        public virtual string? MedalOrderId
        {
            get
            {
                return _MedalOrderId;
            }
            set
            {
                if ((_MedalOrderId != value))
                {
                    SendPropertyChanging("MedalOrderId", _MedalOrderId, value);
                    _MedalOrderId = value;
                    SendPropertyChanged("MedalOrderId");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
        public virtual void SetValue(System.Linq.Expressions.Expression<Func<MemberMedal, bool>> exp)
        {
            base.SetValue<MemberMedal>(exp);
        }
    }
    /// <summary>黑名单</summary>
    [TableConfig]
    [Table("blackuser")]
    [Way.EntityDB.DataItemJsonConverter]
    public class BlackUser : Way.EntityDB.DataItem
    {
        System.Nullable<Int64> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int64> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id", _id, value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        Int64 _UserId;
        /// <summary>黑名单用户</summary>
        [DisallowNull]
        [Display(Name = "黑名单用户")]
        [Column("userid")]
        public virtual Int64 UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                if ((_UserId != value))
                {
                    SendPropertyChanging("UserId", _UserId, value);
                    _UserId = value;
                    SendPropertyChanged("UserId");
                }
            }
        }
        Boolean _IsAgent = false;
        /// <summary>是否是代理</summary>
        [DisallowNull]
        [Display(Name = "是否是代理")]
        [Column("isagent")]
        public virtual Boolean IsAgent
        {
            get
            {
                return _IsAgent;
            }
            set
            {
                if ((_IsAgent != value))
                {
                    SendPropertyChanging("IsAgent", _IsAgent, value);
                    _IsAgent = value;
                    SendPropertyChanged("IsAgent");
                }
            }
        }
        BlackUser_FuncFlagEnum _FuncFlag = (BlackUser_FuncFlagEnum)(0);
        /// <summary>功能项</summary>
        [DisallowNull]
        [Display(Name = "功能项")]
        [Column("funcflag")]
        public virtual BlackUser_FuncFlagEnum FuncFlag
        {
            get
            {
                return _FuncFlag;
            }
            set
            {
                if ((_FuncFlag != value))
                {
                    SendPropertyChanging("FuncFlag", _FuncFlag, value);
                    _FuncFlag = value;
                    SendPropertyChanged("FuncFlag");
                }
            }
        }
        System.Nullable<Int64> _CreatedUserId;
        [Column("createduserid")]
        public virtual System.Nullable<Int64> CreatedUserId
        {
            get
            {
                return _CreatedUserId;
            }
            set
            {
                if ((_CreatedUserId != value))
                {
                    SendPropertyChanging("CreatedUserId", _CreatedUserId, value);
                    _CreatedUserId = value;
                    SendPropertyChanged("CreatedUserId");
                }
            }
        }
        System.Nullable<DateTime> _CreatedTime;
        [Column("createdtime")]
        public virtual System.Nullable<DateTime> CreatedTime
        {
            get
            {
                return _CreatedTime;
            }
            set
            {
                if ((_CreatedTime != value))
                {
                    SendPropertyChanging("CreatedTime", _CreatedTime, value);
                    _CreatedTime = value;
                    SendPropertyChanged("CreatedTime");
                }
            }
        }
        System.Nullable<Int64> _ModifiedUserId;
        [Column("modifieduserid")]
        public virtual System.Nullable<Int64> ModifiedUserId
        {
            get
            {
                return _ModifiedUserId;
            }
            set
            {
                if ((_ModifiedUserId != value))
                {
                    SendPropertyChanging("ModifiedUserId", _ModifiedUserId, value);
                    _ModifiedUserId = value;
                    SendPropertyChanged("ModifiedUserId");
                }
            }
        }
        System.Nullable<DateTime> _ModifiedTime;
        [Column("modifiedtime")]
        public virtual System.Nullable<DateTime> ModifiedTime
        {
            get
            {
                return _ModifiedTime;
            }
            set
            {
                if ((_ModifiedTime != value))
                {
                    SendPropertyChanging("ModifiedTime", _ModifiedTime, value);
                    _ModifiedTime = value;
                    SendPropertyChanged("ModifiedTime");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
        public virtual void SetValue(System.Linq.Expressions.Expression<Func<BlackUser, bool>> exp)
        {
            base.SetValue<BlackUser>(exp);
        }
    }
    public enum BlackUser_FuncFlagEnum : int
    {
        /// <summary>划转</summary>
        Transfer = 1 << 1,
        /// <summary>提现</summary>
        Withdraw = 1 << 2,
    }
    /// <summary>策略</summary>
    [TableConfig]
    [Table("strategy")]
    [Way.EntityDB.DataItemJsonConverter]
    public class Strategy : Way.EntityDB.DataItem
    {
        System.Nullable<Int64> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int64> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id", _id, value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        System.Nullable<Int64> _MarketOrderId;
        /// <summary>适用于哪个订单</summary>
        [Display(Name = "适用于哪个订单")]
        [Column("marketorderid")]
        public virtual System.Nullable<Int64> MarketOrderId
        {
            get
            {
                return _MarketOrderId;
            }
            set
            {
                if ((_MarketOrderId != value))
                {
                    SendPropertyChanging("MarketOrderId", _MarketOrderId, value);
                    _MarketOrderId = value;
                    SendPropertyChanged("MarketOrderId");
                }
            }
        }
        Strategy_TypeEnum _Type;
        /// <summary>策略类型</summary>
        [DisallowNull]
        [Display(Name = "策略类型")]
        [Column("type")]
        public virtual Strategy_TypeEnum Type
        {
            get
            {
                return _Type;
            }
            set
            {
                if ((_Type != value))
                {
                    SendPropertyChanging("Type", _Type, value);
                    _Type = value;
                    SendPropertyChanged("Type");
                }
            }
        }
        string? _Content;
        /// <summary>策略内容</summary>
        [MaxLength(255)]
        [Display(Name = "策略内容")]
        [Column("content")]
        public virtual string? Content
        {
            get
            {
                return _Content;
            }
            set
            {
                if ((_Content != value))
                {
                    SendPropertyChanging("Content", _Content, value);
                    _Content = value;
                    SendPropertyChanged("Content");
                }
            }
        }
        Strategy_SceneEnum _Scene;
        /// <summary>什么场景下触发</summary>
        [DisallowNull]
        [Display(Name = "什么场景下触发")]
        [Column("scene")]
        public virtual Strategy_SceneEnum Scene
        {
            get
            {
                return _Scene;
            }
            set
            {
                if ((_Scene != value))
                {
                    SendPropertyChanging("Scene", _Scene, value);
                    _Scene = value;
                    SendPropertyChanged("Scene");
                }
            }
        }
        Int64 _Enable = 1;
        /// <summary>是否可用</summary>
        [DisallowNull]
        [Display(Name = "是否可用")]
        [Column("enable")]
        public virtual Int64 Enable
        {
            get
            {
                return _Enable;
            }
            set
            {
                if ((_Enable != value))
                {
                    SendPropertyChanging("Enable", _Enable, value);
                    _Enable = value;
                    SendPropertyChanged("Enable");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
        public virtual void SetValue(System.Linq.Expressions.Expression<Func<Strategy, bool>> exp)
        {
            base.SetValue<Strategy>(exp);
        }
    }
    public enum Strategy_TypeEnum : int
    {
        /// <summary>设置止盈止损</summary>
        SetStopProfitLoss = 1
    }
    public enum Strategy_SceneEnum : int
    {
        /// <summary>下单时</summary>
        NewOrder = 1 << 0,
        /// <summary>成交时</summary>
        Deal = 1 << 1,
        /// <summary>新建仓位时</summary>
        OnNewPosition = 1 << 2,
        /// <summary>合并到仓位时</summary>
        OnMergePosition = 1 << 3
    }
    /// <summary>转账表</summary>
    [TableConfig]
    [Table("transfer")]
    [Way.EntityDB.DataItemJsonConverter]
    public class Transfer : Way.EntityDB.DataItem
    {
        System.Nullable<Int64> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int64> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id", _id, value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        Int64 _UserId;
        [DisallowNull]
        [Column("userid")]
        public virtual Int64 UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                if ((_UserId != value))
                {
                    SendPropertyChanging("UserId", _UserId, value);
                    _UserId = value;
                    SendPropertyChanged("UserId");
                }
            }
        }
        Decimal _Amount;
        /// <summary>金额</summary>
        [DisallowNull]
        [Display(Name = "金额")]
        [Column("amount")]
        public virtual Decimal Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                if ((_Amount != value))
                {
                    SendPropertyChanging("Amount", _Amount, value);
                    _Amount = value;
                    SendPropertyChanged("Amount");
                }
            }
        }
        Decimal _Fee = 0m;
        /// <summary>手续费</summary>
        [DisallowNull]
        [Display(Name = "手续费")]
        [Column("fee")]
        public virtual Decimal Fee
        {
            get
            {
                return _Fee;
            }
            set
            {
                if ((_Fee != value))
                {
                    SendPropertyChanging("Fee", _Fee, value);
                    _Fee = value;
                    SendPropertyChanged("Fee");
                }
            }
        }
        MoneyAccount_AccountTypeEnum _FromAccountType;
        /// <summary>从哪个账户转出</summary>
        [DisallowNull]
        [Display(Name = "从哪个账户转出")]
        [Column("fromaccounttype")]
        public virtual MoneyAccount_AccountTypeEnum FromAccountType
        {
            get
            {
                return _FromAccountType;
            }
            set
            {
                if ((_FromAccountType != value))
                {
                    SendPropertyChanging("FromAccountType", _FromAccountType, value);
                    _FromAccountType = value;
                    SendPropertyChanged("FromAccountType");
                }
            }
        }
        MoneyAccount_AccountTypeEnum _ToAccountType;
        /// <summary>转到哪个账户</summary>
        [DisallowNull]
        [Display(Name = "转到哪个账户")]
        [Column("toaccounttype")]
        public virtual MoneyAccount_AccountTypeEnum ToAccountType
        {
            get
            {
                return _ToAccountType;
            }
            set
            {
                if ((_ToAccountType != value))
                {
                    SendPropertyChanging("ToAccountType", _ToAccountType, value);
                    _ToAccountType = value;
                    SendPropertyChanged("ToAccountType");
                }
            }
        }
        DateTime _CreateTime;
        /// <summary>申请时间</summary>
        [DisallowNull]
        [Display(Name = "申请时间")]
        [Column("createtime")]
        public virtual DateTime CreateTime
        {
            get
            {
                return _CreateTime;
            }
            set
            {
                if ((_CreateTime != value))
                {
                    SendPropertyChanging("CreateTime", _CreateTime, value);
                    _CreateTime = value;
                    SendPropertyChanged("CreateTime");
                }
            }
        }
        System.Nullable<DateTime> _ExamTime;
        /// <summary>审核时间</summary>
        [Display(Name = "审核时间")]
        [Column("examtime")]
        public virtual System.Nullable<DateTime> ExamTime
        {
            get
            {
                return _ExamTime;
            }
            set
            {
                if ((_ExamTime != value))
                {
                    SendPropertyChanging("ExamTime", _ExamTime, value);
                    _ExamTime = value;
                    SendPropertyChanged("ExamTime");
                }
            }
        }
        System.Nullable<Int64> _ExamUserId;
        /// <summary>审核人id</summary>
        [Display(Name = "审核人id")]
        [Column("examuserid")]
        public virtual System.Nullable<Int64> ExamUserId
        {
            get
            {
                return _ExamUserId;
            }
            set
            {
                if ((_ExamUserId != value))
                {
                    SendPropertyChanging("ExamUserId", _ExamUserId, value);
                    _ExamUserId = value;
                    SendPropertyChanged("ExamUserId");
                }
            }
        }
        DateTime _UpdateTime;
        /// <summary>最后更新时间</summary>
        [DisallowNull]
        [Display(Name = "最后更新时间")]
        [Column("updatetime")]
        public virtual DateTime UpdateTime
        {
            get
            {
                return _UpdateTime;
            }
            set
            {
                if ((_UpdateTime != value))
                {
                    SendPropertyChanging("UpdateTime", _UpdateTime, value);
                    _UpdateTime = value;
                    SendPropertyChanged("UpdateTime");
                }
            }
        }
        Transfer_StatusEnum _Status = (Transfer_StatusEnum)(1);
        /// <summary>状态</summary>
        [DisallowNull]
        [Display(Name = "状态")]
        [Column("status")]
        public virtual Transfer_StatusEnum Status
        {
            get
            {
                return _Status;
            }
            set
            {
                if ((_Status != value))
                {
                    SendPropertyChanging("Status", _Status, value);
                    _Status = value;
                    SendPropertyChanged("Status");
                }
            }
        }
        string? _RejectReason;
        /// <summary>拒绝转账的原因</summary>
        [MaxLength(255)]
        [Display(Name = "拒绝转账的原因")]
        [Column("rejectreason")]
        public virtual string? RejectReason
        {
            get
            {
                return _RejectReason;
            }
            set
            {
                if ((_RejectReason != value))
                {
                    SendPropertyChanging("RejectReason", _RejectReason, value);
                    _RejectReason = value;
                    SendPropertyChanged("RejectReason");
                }
            }
        }
        string? _Coin;
        /// <summary>币种</summary>
        [MaxLength(50)]
        [Display(Name = "币种")]
        [Column("coin")]
        public virtual string? Coin
        {
            get
            {
                return _Coin;
            }
            set
            {
                if ((_Coin != value))
                {
                    SendPropertyChanging("Coin", _Coin, value);
                    _Coin = value;
                    SendPropertyChanged("Coin");
                }
            }
        }
        string? _TransferFailReason;
        /// <summary>转账失败原因</summary>
        [MaxLength(255)]
        [Display(Name = "转账失败原因")]
        [Column("transferfailreason")]
        public virtual string? TransferFailReason
        {
            get
            {
                return _TransferFailReason;
            }
            set
            {
                if ((_TransferFailReason != value))
                {
                    SendPropertyChanging("TransferFailReason", _TransferFailReason, value);
                    _TransferFailReason = value;
                    SendPropertyChanged("TransferFailReason");
                }
            }
        }
        System.Nullable<DateTime> _FExamTime;
        /// <summary>财务审核时间</summary>
        [Display(Name = "财务审核时间")]
        [Column("fexamtime")]
        public virtual System.Nullable<DateTime> FExamTime
        {
            get
            {
                return _FExamTime;
            }
            set
            {
                if ((_FExamTime != value))
                {
                    SendPropertyChanging("FExamTime", _FExamTime, value);
                    _FExamTime = value;
                    SendPropertyChanged("FExamTime");
                }
            }
        }
        System.Nullable<Int64> _FeUserId;
        /// <summary>财务审核人id</summary>
        [Display(Name = "财务审核人id")]
        [Column("feuserid")]
        public virtual System.Nullable<Int64> FeUserId
        {
            get
            {
                return _FeUserId;
            }
            set
            {
                if ((_FeUserId != value))
                {
                    SendPropertyChanging("FeUserId", _FeUserId, value);
                    _FeUserId = value;
                    SendPropertyChanged("FeUserId");
                }
            }
        }
        string? _UserRealName;
        /// <summary>用户姓名</summary>
        [MaxLength(50)]
        [Display(Name = "用户姓名")]
        [Column("userrealname")]
        public virtual string? UserRealName
        {
            get
            {
                return _UserRealName;
            }
            set
            {
                if ((_UserRealName != value))
                {
                    SendPropertyChanging("UserRealName", _UserRealName, value);
                    _UserRealName = value;
                    SendPropertyChanged("UserRealName");
                }
            }
        }
        string? _FeUserName;
        /// <summary>审核人姓名</summary>
        [MaxLength(50)]
        [Display(Name = "审核人姓名")]
        [Column("feusername")]
        public virtual string? FeUserName
        {
            get
            {
                return _FeUserName;
            }
            set
            {
                if ((_FeUserName != value))
                {
                    SendPropertyChanging("FeUserName", _FeUserName, value);
                    _FeUserName = value;
                    SendPropertyChanged("FeUserName");
                }
            }
        }
        string? _CancelReason;
        /// <summary>取消的原因</summary>
        [MaxLength(255)]
        [Display(Name = "取消的原因")]
        [Column("cancelreason")]
        public virtual string? CancelReason
        {
            get
            {
                return _CancelReason;
            }
            set
            {
                if ((_CancelReason != value))
                {
                    SendPropertyChanging("CancelReason", _CancelReason, value);
                    _CancelReason = value;
                    SendPropertyChanged("CancelReason");
                }
            }
        }
        Boolean _IsReview = true;
        /// <summary>是否已经复查</summary>
        [DisallowNull]
        [Display(Name = "是否已经复查")]
        [Column("isreview")]
        public virtual Boolean IsReview
        {
            get
            {
                return _IsReview;
            }
            set
            {
                if ((_IsReview != value))
                {
                    SendPropertyChanging("IsReview", _IsReview, value);
                    _IsReview = value;
                    SendPropertyChanged("IsReview");
                }
            }
        }
        System.Nullable<Decimal> _GiveDeductAmount;
        /// <summary>赠金</summary>
        [Display(Name = "赠金")]
        [Column("givedeductamount")]
        public virtual System.Nullable<Decimal> GiveDeductAmount
        {
            get
            {
                return _GiveDeductAmount;
            }
            set
            {
                if ((_GiveDeductAmount != value))
                {
                    SendPropertyChanging("GiveDeductAmount", _GiveDeductAmount, value);
                    _GiveDeductAmount = value;
                    SendPropertyChanged("GiveDeductAmount");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
        public virtual void SetValue(System.Linq.Expressions.Expression<Func<Transfer, bool>> exp)
        {
            base.SetValue<Transfer>(exp);
        }
    }
    public enum Transfer_StatusEnum : int
    {
        /// <summary>待审核</summary>
        Waiting = 1,
        /// <summary>审核通过</summary>
        Pass = 15,
        /// <summary>需要用户确认外加的手续费</summary>
        ConfirmFee = 19,
        /// <summary>财务审核通过</summary>
        FPass = 20,
        /// <summary>转账完成</summary>
        Completed = 30,
        /// <summary>转账失败</summary>
        TransferFail = 90,
        /// <summary>订单取消</summary>
        TransferCancel = 91,
        /// <summary>拒绝转账</summary>
        Reject = 99
    }
    /// <summary>转账免审核名单</summary>
    [TableConfig]
    [Table("transferwhitelist")]
    [Way.EntityDB.DataItemJsonConverter]
    public class TransferWhiteList : Way.EntityDB.DataItem
    {
        System.Nullable<Int64> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int64> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id", _id, value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        Int64 _UserId;
        [DisallowNull]
        [Column("userid")]
        public virtual Int64 UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                if ((_UserId != value))
                {
                    SendPropertyChanging("UserId", _UserId, value);
                    _UserId = value;
                    SendPropertyChanged("UserId");
                }
            }
        }
        DateTime _CreateTime;
        /// <summary>添加时间</summary>
        [DisallowNull]
        [Display(Name = "添加时间")]
        [Column("createtime")]
        public virtual DateTime CreateTime
        {
            get
            {
                return _CreateTime;
            }
            set
            {
                if ((_CreateTime != value))
                {
                    SendPropertyChanging("CreateTime", _CreateTime, value);
                    _CreateTime = value;
                    SendPropertyChanged("CreateTime");
                }
            }
        }
        System.Nullable<Int64> _OpUserId;
        /// <summary>操作人id</summary>
        [Display(Name = "操作人id")]
        [Column("opuserid")]
        public virtual System.Nullable<Int64> OpUserId
        {
            get
            {
                return _OpUserId;
            }
            set
            {
                if ((_OpUserId != value))
                {
                    SendPropertyChanging("OpUserId", _OpUserId, value);
                    _OpUserId = value;
                    SendPropertyChanged("OpUserId");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
        public virtual void SetValue(System.Linq.Expressions.Expression<Func<TransferWhiteList, bool>> exp)
        {
            base.SetValue<TransferWhiteList>(exp);
        }
    }
    /// <summary>资金审核</summary>
    [TableConfig]
    [Table("moneyexam")]
    [Way.EntityDB.DataItemJsonConverter]
    public class MoneyExam : Way.EntityDB.DataItem
    {
        System.Nullable<Int64> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int64> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id", _id, value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        Int64 _UserId;
        /// <summary>用户id</summary>
        [DisallowNull]
        [Display(Name = "用户id")]
        [Column("userid")]
        public virtual Int64 UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                if ((_UserId != value))
                {
                    SendPropertyChanging("UserId", _UserId, value);
                    _UserId = value;
                    SendPropertyChanged("UserId");
                }
            }
        }
        string? _Coin = "USDT";
        /// <summary>币种</summary>
        [MaxLength(50)]
        [Display(Name = "币种")]
        [Column("coin")]
        public virtual string? Coin
        {
            get
            {
                return _Coin;
            }
            set
            {
                if ((_Coin != value))
                {
                    SendPropertyChanging("Coin", _Coin, value);
                    _Coin = value;
                    SendPropertyChanged("Coin");
                }
            }
        }
        MoneyAccount_AccountTypeEnum _AccountType = (MoneyAccount_AccountTypeEnum)(1);
        /// <summary>账户类型</summary>
        [DisallowNull]
        [Display(Name = "账户类型")]
        [Column("accounttype")]
        public virtual MoneyAccount_AccountTypeEnum AccountType
        {
            get
            {
                return _AccountType;
            }
            set
            {
                if ((_AccountType != value))
                {
                    SendPropertyChanging("AccountType", _AccountType, value);
                    _AccountType = value;
                    SendPropertyChanged("AccountType");
                }
            }
        }
        System.Nullable<Decimal> _Amount;
        /// <summary>金额</summary>
        [Display(Name = "金额")]
        [Column("amount")]
        public virtual System.Nullable<Decimal> Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                if ((_Amount != value))
                {
                    SendPropertyChanging("Amount", _Amount, value);
                    _Amount = value;
                    SendPropertyChanged("Amount");
                }
            }
        }
        Int32 _ActionType;
        /// <summary>
        /// 操作类型
        /// 1：增加 2：减少
        /// </summary>
        [DisallowNull]
        [Display(Name = "操作类型 1：增加 2：减少")]
        [Column("actiontype")]
        public virtual Int32 ActionType
        {
            get
            {
                return _ActionType;
            }
            set
            {
                if ((_ActionType != value))
                {
                    SendPropertyChanging("ActionType", _ActionType, value);
                    _ActionType = value;
                    SendPropertyChanged("ActionType");
                }
            }
        }
        Int64 _CreateUserId;
        /// <summary>创建人id</summary>
        [DisallowNull]
        [Display(Name = "创建人id")]
        [Column("createuserid")]
        public virtual Int64 CreateUserId
        {
            get
            {
                return _CreateUserId;
            }
            set
            {
                if ((_CreateUserId != value))
                {
                    SendPropertyChanging("CreateUserId", _CreateUserId, value);
                    _CreateUserId = value;
                    SendPropertyChanged("CreateUserId");
                }
            }
        }
        DateTime _CreateTime;
        /// <summary>创建时间</summary>
        [DisallowNull]
        [Display(Name = "创建时间")]
        [Column("createtime")]
        public virtual DateTime CreateTime
        {
            get
            {
                return _CreateTime;
            }
            set
            {
                if ((_CreateTime != value))
                {
                    SendPropertyChanging("CreateTime", _CreateTime, value);
                    _CreateTime = value;
                    SendPropertyChanged("CreateTime");
                }
            }
        }
        System.Nullable<Int64> _ExamUserId;
        /// <summary>审核人id</summary>
        [Display(Name = "审核人id")]
        [Column("examuserid")]
        public virtual System.Nullable<Int64> ExamUserId
        {
            get
            {
                return _ExamUserId;
            }
            set
            {
                if ((_ExamUserId != value))
                {
                    SendPropertyChanging("ExamUserId", _ExamUserId, value);
                    _ExamUserId = value;
                    SendPropertyChanged("ExamUserId");
                }
            }
        }
        System.Nullable<DateTime> _ExamTime;
        /// <summary>审核时间</summary>
        [Display(Name = "审核时间")]
        [Column("examtime")]
        public virtual System.Nullable<DateTime> ExamTime
        {
            get
            {
                return _ExamTime;
            }
            set
            {
                if ((_ExamTime != value))
                {
                    SendPropertyChanging("ExamTime", _ExamTime, value);
                    _ExamTime = value;
                    SendPropertyChanged("ExamTime");
                }
            }
        }
        string? _Comment;
        /// <summary>备注</summary>
        [MaxLength(255)]
        [Display(Name = "备注")]
        [Column("comment")]
        public virtual string? Comment
        {
            get
            {
                return _Comment;
            }
            set
            {
                if ((_Comment != value))
                {
                    SendPropertyChanging("Comment", _Comment, value);
                    _Comment = value;
                    SendPropertyChanged("Comment");
                }
            }
        }
        Int32 _Status = 0;
        /// <summary>状态，0：待审核 10:同意 20:拒绝</summary>
        [DisallowNull]
        [Display(Name = "状态，0：待审核 10:同意 20:拒绝")]
        [Column("status")]
        public virtual Int32 Status
        {
            get
            {
                return _Status;
            }
            set
            {
                if ((_Status != value))
                {
                    SendPropertyChanging("Status", _Status, value);
                    _Status = value;
                    SendPropertyChanged("Status");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
        public virtual void SetValue(System.Linq.Expressions.Expression<Func<MoneyExam, bool>> exp)
        {
            base.SetValue<MoneyExam>(exp);
        }
    }
    /// <summary>币币兑换汇率设置</summary>
    [TableConfig]
    [Table("expriceratesetting")]
    [Way.EntityDB.DataItemJsonConverter]
    public class ExPriceRateSetting : Way.EntityDB.DataItem
    {
        System.Nullable<Int64> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int64> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id", _id, value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        Decimal _MinTradeVolume;
        /// <summary>最小交易量</summary>
        [DisallowNull]
        [Display(Name = "最小交易量")]
        [Column("mintradevolume")]
        public virtual Decimal MinTradeVolume
        {
            get
            {
                return _MinTradeVolume;
            }
            set
            {
                if ((_MinTradeVolume != value))
                {
                    SendPropertyChanging("MinTradeVolume", _MinTradeVolume, value);
                    _MinTradeVolume = value;
                    SendPropertyChanged("MinTradeVolume");
                }
            }
        }
        Decimal _MaxTradeVolume;
        /// <summary>最大交易量</summary>
        [DisallowNull]
        [Display(Name = "最大交易量")]
        [Column("maxtradevolume")]
        public virtual Decimal MaxTradeVolume
        {
            get
            {
                return _MaxTradeVolume;
            }
            set
            {
                if ((_MaxTradeVolume != value))
                {
                    SendPropertyChanging("MaxTradeVolume", _MaxTradeVolume, value);
                    _MaxTradeVolume = value;
                    SendPropertyChanged("MaxTradeVolume");
                }
            }
        }
        double _Rate;
        /// <summary>汇率</summary>
        [DisallowNull]
        [Display(Name = "汇率")]
        [Column("rate")]
        public virtual double Rate
        {
            get
            {
                return _Rate;
            }
            set
            {
                if ((_Rate != value))
                {
                    SendPropertyChanging("Rate", _Rate, value);
                    _Rate = value;
                    SendPropertyChanged("Rate");
                }
            }
        }
        DateTime _CreateTime;
        [DisallowNull]
        [Column("createtime")]
        public virtual DateTime CreateTime
        {
            get
            {
                return _CreateTime;
            }
            set
            {
                if ((_CreateTime != value))
                {
                    SendPropertyChanging("CreateTime", _CreateTime, value);
                    _CreateTime = value;
                    SendPropertyChanged("CreateTime");
                }
            }
        }
        Int64 _CreateUserId;
        [DisallowNull]
        [Column("createuserid")]
        public virtual Int64 CreateUserId
        {
            get
            {
                return _CreateUserId;
            }
            set
            {
                if ((_CreateUserId != value))
                {
                    SendPropertyChanging("CreateUserId", _CreateUserId, value);
                    _CreateUserId = value;
                    SendPropertyChanged("CreateUserId");
                }
            }
        }
        System.Nullable<DateTime> _UpdateTime;
        [Column("updatetime")]
        public virtual System.Nullable<DateTime> UpdateTime
        {
            get
            {
                return _UpdateTime;
            }
            set
            {
                if ((_UpdateTime != value))
                {
                    SendPropertyChanging("UpdateTime", _UpdateTime, value);
                    _UpdateTime = value;
                    SendPropertyChanged("UpdateTime");
                }
            }
        }
        System.Nullable<Int64> _UpdateUserId;
        [Column("updateuserid")]
        public virtual System.Nullable<Int64> UpdateUserId
        {
            get
            {
                return _UpdateUserId;
            }
            set
            {
                if ((_UpdateUserId != value))
                {
                    SendPropertyChanging("UpdateUserId", _UpdateUserId, value);
                    _UpdateUserId = value;
                    SendPropertyChanged("UpdateUserId");
                }
            }
        }
        string? _Symbol;
        /// <summary>交易对</summary>
        [MaxLength(50)]
        [Display(Name = "交易对")]
        [Column("symbol")]
        public virtual string? Symbol
        {
            get
            {
                return _Symbol;
            }
            set
            {
                if ((_Symbol != value))
                {
                    SendPropertyChanging("Symbol", _Symbol, value);
                    _Symbol = value;
                    SendPropertyChanged("Symbol");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
        public virtual void SetValue(System.Linq.Expressions.Expression<Func<ExPriceRateSetting, bool>> exp)
        {
            base.SetValue<ExPriceRateSetting>(exp);
        }
    }
}

namespace TradeSystem.DBModels.DB
{
    public class TradeSystemDB : Way.EntityDB.DBContext
    {
        public TradeSystemDB(string connection, Way.EntityDB.DatabaseType dbType, bool upgradeDatabase = true) : base(connection, dbType, upgradeDatabase)
        {
            if (!setEvented)
            {
                lock (lockObj)
                {
                    if (!setEvented)
                    {
                        setEvented = true;
                        Way.EntityDB.DBContext.BeforeDelete += Database_BeforeDelete;
                    }
                }
            }
        }
        static object lockObj = new object();
        static bool setEvented = false;
        static void Database_BeforeDelete(object sender, Way.EntityDB.DatabaseModifyEventArg e)
        {
            var db = sender as TradeSystemDB;
            if (db == null) return;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MoneyAccount>().HasKey(m => m.Id);
            modelBuilder.Entity<SymbolInfo>().HasKey(m => m.id);
            modelBuilder.Entity<SetStopPLHistory>().HasKey(m => m.id);
            modelBuilder.Entity<Position>().HasKey(m => m.id);
            modelBuilder.Entity<MarketOrder>().HasKey(m => m.id);
            modelBuilder.Entity<MarketDealHistory>().HasKey(m => m.id);
            modelBuilder.Entity<MoneyDetail>().HasKey(m => m.id);
            modelBuilder.Entity<UserSetting>().HasKey(m => m.UserId);
            modelBuilder.Entity<Currency>().HasKey(m => m.id);
            modelBuilder.Entity<AccountKeys>().HasKey(m => m.id);
            modelBuilder.Entity<Withdraw>().HasKey(m => m.id);
            modelBuilder.Entity<CoinAddress>().HasKey(m => m.id);
            modelBuilder.Entity<MemberRule>().HasKey(m => m.id);
            modelBuilder.Entity<IntegralDetail>().HasKey(m => m.id);
            modelBuilder.Entity<MemberLevel>().HasKey(m => m.id);
            modelBuilder.Entity<UserIntegral>().HasKey(m => m.id);
            modelBuilder.Entity<MemberMedal>().HasKey(m => m.id);
            modelBuilder.Entity<BlackUser>().HasKey(m => m.id);
            modelBuilder.Entity<Strategy>().HasKey(m => m.id);
            modelBuilder.Entity<Transfer>().HasKey(m => m.id);
            modelBuilder.Entity<TransferWhiteList>().HasKey(m => m.id);
            modelBuilder.Entity<MoneyExam>().HasKey(m => m.id);
            modelBuilder.Entity<ExPriceRateSetting>().HasKey(m => m.id);

        }

        System.Linq.IQueryable<MoneyAccount> _MoneyAccount;
        /// <summary>用户资金表</summary>
        public virtual System.Linq.IQueryable<MoneyAccount> MoneyAccount
        {
            get
            {
                if (_MoneyAccount == null)
                {
                    _MoneyAccount = this.Set<MoneyAccount>();
                }
                return _MoneyAccount;
            }
        }
        System.Linq.IQueryable<SymbolInfo> _SymbolInfo;
        public virtual System.Linq.IQueryable<SymbolInfo> SymbolInfo
        {
            get
            {
                if (_SymbolInfo == null)
                {
                    _SymbolInfo = this.Set<SymbolInfo>();
                }
                return _SymbolInfo;
            }
        }
        System.Linq.IQueryable<SetStopPLHistory> _SetStopPLHistory;
        /// <summary>设置止盈止损的历史</summary>
        public virtual System.Linq.IQueryable<SetStopPLHistory> SetStopPLHistory
        {
            get
            {
                if (_SetStopPLHistory == null)
                {
                    _SetStopPLHistory = this.Set<SetStopPLHistory>();
                }
                return _SetStopPLHistory;
            }
        }
        System.Linq.IQueryable<Position> _Position;
        /// <summary>仓位</summary>
        public virtual System.Linq.IQueryable<Position> Position
        {
            get
            {
                if (_Position == null)
                {
                    _Position = this.Set<Position>();
                }
                return _Position;
            }
        }
        System.Linq.IQueryable<MarketOrder> _MarketOrder;
        /// <summary>市场挂单</summary>
        public virtual System.Linq.IQueryable<MarketOrder> MarketOrder
        {
            get
            {
                if (_MarketOrder == null)
                {
                    _MarketOrder = this.Set<MarketOrder>();
                }
                return _MarketOrder;
            }
        }
        System.Linq.IQueryable<MarketDealHistory> _MarketDealHistory;
        /// <summary>市场成交历史</summary>
        public virtual System.Linq.IQueryable<MarketDealHistory> MarketDealHistory
        {
            get
            {
                if (_MarketDealHistory == null)
                {
                    _MarketDealHistory = this.Set<MarketDealHistory>();
                }
                return _MarketDealHistory;
            }
        }
        System.Linq.IQueryable<MoneyDetail> _MoneyDetail;
        /// <summary>资金明细</summary>
        public virtual System.Linq.IQueryable<MoneyDetail> MoneyDetail
        {
            get
            {
                if (_MoneyDetail == null)
                {
                    _MoneyDetail = this.Set<MoneyDetail>();
                }
                return _MoneyDetail;
            }
        }
        System.Linq.IQueryable<UserSetting> _UserSetting;
        /// <summary>用户配置信息</summary>
        public virtual System.Linq.IQueryable<UserSetting> UserSetting
        {
            get
            {
                if (_UserSetting == null)
                {
                    _UserSetting = this.Set<UserSetting>();
                }
                return _UserSetting;
            }
        }
        System.Linq.IQueryable<Currency> _Currency;
        /// <summary>币种</summary>
        public virtual System.Linq.IQueryable<Currency> Currency
        {
            get
            {
                if (_Currency == null)
                {
                    _Currency = this.Set<Currency>();
                }
                return _Currency;
            }
        }
        System.Linq.IQueryable<AccountKeys> _AccountKeys;
        /// <summary>用户私钥</summary>
        public virtual System.Linq.IQueryable<AccountKeys> AccountKeys
        {
            get
            {
                if (_AccountKeys == null)
                {
                    _AccountKeys = this.Set<AccountKeys>();
                }
                return _AccountKeys;
            }
        }
        System.Linq.IQueryable<Withdraw> _Withdraw;
        /// <summary>提现表</summary>
        public virtual System.Linq.IQueryable<Withdraw> Withdraw
        {
            get
            {
                if (_Withdraw == null)
                {
                    _Withdraw = this.Set<Withdraw>();
                }
                return _Withdraw;
            }
        }
        System.Linq.IQueryable<CoinAddress> _CoinAddress;
        /// <summary>币种充值地址表</summary>
        public virtual System.Linq.IQueryable<CoinAddress> CoinAddress
        {
            get
            {
                if (_CoinAddress == null)
                {
                    _CoinAddress = this.Set<CoinAddress>();
                }
                return _CoinAddress;
            }
        }
        System.Linq.IQueryable<MemberRule> _MemberRule;
        /// <summary>会员规则</summary>
        public virtual System.Linq.IQueryable<MemberRule> MemberRule
        {
            get
            {
                if (_MemberRule == null)
                {
                    _MemberRule = this.Set<MemberRule>();
                }
                return _MemberRule;
            }
        }
        System.Linq.IQueryable<IntegralDetail> _IntegralDetail;
        /// <summary>积分明细</summary>
        public virtual System.Linq.IQueryable<IntegralDetail> IntegralDetail
        {
            get
            {
                if (_IntegralDetail == null)
                {
                    _IntegralDetail = this.Set<IntegralDetail>();
                }
                return _IntegralDetail;
            }
        }
        System.Linq.IQueryable<MemberLevel> _MemberLevel;
        /// <summary>会员等级配置表</summary>
        public virtual System.Linq.IQueryable<MemberLevel> MemberLevel
        {
            get
            {
                if (_MemberLevel == null)
                {
                    _MemberLevel = this.Set<MemberLevel>();
                }
                return _MemberLevel;
            }
        }
        System.Linq.IQueryable<UserIntegral> _UserIntegral;
        /// <summary>用户等级表</summary>
        public virtual System.Linq.IQueryable<UserIntegral> UserIntegral
        {
            get
            {
                if (_UserIntegral == null)
                {
                    _UserIntegral = this.Set<UserIntegral>();
                }
                return _UserIntegral;
            }
        }
        System.Linq.IQueryable<MemberMedal> _MemberMedal;
        /// <summary>会员勋章</summary>
        public virtual System.Linq.IQueryable<MemberMedal> MemberMedal
        {
            get
            {
                if (_MemberMedal == null)
                {
                    _MemberMedal = this.Set<MemberMedal>();
                }
                return _MemberMedal;
            }
        }
        System.Linq.IQueryable<BlackUser> _BlackUser;
        /// <summary>黑名单</summary>
        public virtual System.Linq.IQueryable<BlackUser> BlackUser
        {
            get
            {
                if (_BlackUser == null)
                {
                    _BlackUser = this.Set<BlackUser>();
                }
                return _BlackUser;
            }
        }
        System.Linq.IQueryable<Strategy> _Strategy;
        /// <summary>策略</summary>
        public virtual System.Linq.IQueryable<Strategy> Strategy
        {
            get
            {
                if (_Strategy == null)
                {
                    _Strategy = this.Set<Strategy>();
                }
                return _Strategy;
            }
        }
        System.Linq.IQueryable<Transfer> _Transfer;
        /// <summary>转账表</summary>
        public virtual System.Linq.IQueryable<Transfer> Transfer
        {
            get
            {
                if (_Transfer == null)
                {
                    _Transfer = this.Set<Transfer>();
                }
                return _Transfer;
            }
        }
        System.Linq.IQueryable<TransferWhiteList> _TransferWhiteList;
        /// <summary>转账免审核名单</summary>
        public virtual System.Linq.IQueryable<TransferWhiteList> TransferWhiteList
        {
            get
            {
                if (_TransferWhiteList == null)
                {
                    _TransferWhiteList = this.Set<TransferWhiteList>();
                }
                return _TransferWhiteList;
            }
        }
        System.Linq.IQueryable<MoneyExam> _MoneyExam;
        /// <summary>资金审核</summary>
        public virtual System.Linq.IQueryable<MoneyExam> MoneyExam
        {
            get
            {
                if (_MoneyExam == null)
                {
                    _MoneyExam = this.Set<MoneyExam>();
                }
                return _MoneyExam;
            }
        }
        System.Linq.IQueryable<ExPriceRateSetting> _ExPriceRateSetting;
        /// <summary>币币兑换汇率设置</summary>
        public virtual System.Linq.IQueryable<ExPriceRateSetting> ExPriceRateSetting
        {
            get
            {
                if (_ExPriceRateSetting == null)
                {
                    _ExPriceRateSetting = this.Set<ExPriceRateSetting>();
                }
                return _ExPriceRateSetting;
            }
        }
        protected override string GetDesignString()
        {
            var result = new StringBuilder();
            result.Append("\r\n");
            result.Append("H4sIAAAAAAAAA+1963MTV7bvv+JyzflyjyfobTuVfCAYzlADCQeTk3vrempKWI2jG1viSHICk+GWncHYGPwAm6fNcwx4ErBNIGAkG/8z2t3yp/wLZ3fvltS9d0ut3r13a7fcVakZI7Vev15r7fX4rbV+7D6TPDsq5bs//b8/oj+/TI5J3Z92n8rmCyM5afC/R7t7uk9n");
            result.Append("f0BXHC9IY+gv/bJ0Cj79X8nRcfiPaCJ6qaf2TOHiean+XPeRnJQsSNonHB4upLOZbsO1w9lMQcoUDJf/OIS+zVD3p/DPdAr+f7hnqHs4eV59LfzXULeyvC7PvK/8dnl/+kblyfpQN3xefT/tyZPZjHTx8PBwdjxT0J4ZSBaSZ5N56fiA/lbpE9nh7+DfoUvwH0eyo+Nj");
            result.Append("mfyQ+tMsPy4zPjpqfP+v81LueEp75+P5w+OF7PHMcE4ak9RP+/RccjQvqe+azHwJX2d4JHX2DIRFe4ez6ZG0/t30x/TPOJoZHxuQzqUzhsdGpcxI4dv6v1PSueT4aEFDq/6ohljtBx7Pn/qz9o/qh2dzKSlX+23H8wPp/HAuPZbOJAvZnPFrjybzef2Xqu8Mb5SOSQS7");
            result.Append("BWBhE96F8u69/acPzPh/kRxNZoYlNwClpOH0WHLUKUJD3ZG+nj7tVRhKQ90h9GatwhShhKkXh2l7UnkxZwboSDadaRmdQm4cA+f7ZG7422TOOTjxkDU0Xw8OnHGGTowSnb6mitVQqXQMOOjUULcVJM11Sv82dTRClGj047JypaSUlqBJI1TqWC77N6l1mRFSo6KUKIVx");
            result.Append("lZKvT5ZLS/L6E7CzYIYJHlxp9TL0A+nBakGQhroPHdqfWKx/kaHMYDozMiqdzKakrs+7IBhDmUOHwNS68ZJj8J30CyLaJ9gI4lB32BnIvbQg92EgV/am5dVH8p13+3femkE+euF8OiedSY+1DjFpw1LQIShU34KzvpIo9dEa9xCO0tvn0A9RXpfAw2tmlHT/wyNJlGd/");
            result.Append("k6/+U756TSm9qrz9oEqaZJDCcnFNvruEvix84EwuiWT0s8/Coa6/d8FrdWld2JQXFpX5rdql36QL36ZyyR/0q03v9++1y6vvabgafcbfu2oP9XCR9zjtQYS7M+Wdu2BqRim+gL+lS761BSZ2Ku9mwdqL33eu156Dj+z/chc+Ap7/VPntMbTU6t/F5crmM3l2tn7Zxkd4");
            result.Append("WXl7QvnpA2agculsLl24yFkkWtMaxxY84Qzsv6iXD/zvI9nMufSI5l+jR9QT85IxBEjp/rkxoAjDl6uxx2ABWgrNU7eNQGI8IxCzvOCey+DFsbPZ0eOZc1m6gCPa9N3T3vtFLcYaES5+UQz3i6bWlRdbZk1Cf1CfP4x96ObAsAvC4rgrNH8TFDEnaDCbK7hAhqN5sQGF");
            result.Append("1kFM4KDc3QSLz1F8irkuGaTV9Mb3bJoZOvhJZ4MObciVwD075eq/wNIkWJxTdWooA4+y33fuf3HmiBoFwuPM+DTYeg3BhM/h8evYWDbl5BgTTgEdHmV1NPGYDblCepRPoml8GqFZC7ZrcGquklg5AadwUocdIUI6NyYIWz/4LTRp/jb4tOmkcAgXOPnuR2WtKD+eVl59");
            result.Append("JGD64dhocoR7vPFlNqOGDyE9aNiehP8hQYcPHL0Acc6M6PFF9ZpF6Bc/r10DXcJCLjlc0KOKntZCA4fHCW1oEA7jUR4MB/anFxDwYGsB/rO8i6X0/nM8mSlAizhgSLO02793KqO0UXE4jKfny6X38uOdpoDBSGhY8jNa/dRoEZn0uVvKy2V5dQJChVSEyP+dTGcGpOTo");
            result.Append("4bFaTcPrNCD81pZpQMeeL21QEA5HMdz27y1CQYPoganJysY2uHkd7C6Bq3Ng+yf4uHL/cmVzE7zfkDeXawdz6JNwT+XJOhTL+osXNsulZ2B13fjicOjfwHvoQr6Rf9sAs5gXeUST3cHhpDtXMpUdrzqjngtv2GEA4nUoH+cZyuNyVNn4qOxuyK+eKisz8H/luUdQdsD8");
            result.Append("FbDwK3bASoXBQvb8qRN/SuchYBcpi4vm48U/wX7UKFPMgn3i8EA3Ah0hGP4q+LnsubSb+JbeAvb1xFrSPRNO7GJ/4txAstoIqBPZfN7PMFF7zLh6I3myqqrV5cnhwSogXNTVNTzZhqSqEVyqVPkfLNpcShjPv5FHh3znnXp6aF6Ehcf7VQYeIqdO+Bk86rjKnKfDzz39");
            result.Append("XHdSVrRyqvjWFW2goU2D9BJZkBcl6HYiucLJTd+nMyOdcRLSJuF6+5tKUpUF4ICjRYLDlU5ig4vDQNxr5zzB0znHj6NyaYnMHeg3mNL/bi494vrfsR4e/ncEz3bpKetNlUXQZcpnY+6AVvMUOCVrwoudxxQhyKgHoqBigyatVxAhiBgHo6BiAyetJxHBo586XvNb4NVd");
            result.Append("3a+Hml15+0i+tYWSYRDF/Tsb4MMbsLAFJtT0RwNA25iAbJ2HaAMtbZo7Yj6a8HPDI6ZXg0pK9UhEnC+qcooNbA1ShtVTsTFseMxU3ntQ2ZyEgog5tsnciAONZZrbZiVa4daTFqrRN9+6oW4DaASn4cFj+cEVM2InpO+lXHKEt9TR8hnssGo9CLfBiuAHX50Arx+iFhEz");
            result.Append("Yg47NzyOCuwAo7RbMevmGaW0pGzcQURP3ee17KDxLV60CcQYUZ+bWQGlohUVmiJn4TkV2gYl2jJmDA/UlNl38sQknjFMFsZbz0K7OBV3JtRAcfsVPBK/Oi9lDAT8D29qzxwZheYjM6I+GdKJCfL1n8DCHFi8AeZuqRR+KZOSUvp1X6kgqRfHqyyG97+it9PfS0rBZ6Oh");
            result.Append("UGvnrVP7SM2exA9c5PmhMARRGZz6f7X4pEpz8LcHSJt3iuFpOvn2Byg6ZqwG0jlpuJad4Cv25Q9bYOoZvJtfjF80iPzcbTBd1KR5dBQ+/P/hs1xElDaNHsdPpMreLph9DMWugW94OJXSjv3kqEMvkW3SszUChJ1et553aO73JCzqgaIWTpkgF6G1iAmLkqCYlVM2ONF6");
            result.Append("igmLWqDYpVM2eNF6igmCji927ZQNWtQ0fTym9VnxlA161MRzPMo1lQihPwePTtXLu39ZbX/QOJe/71xH7p1y7yOYuaLy4EQsJbLBldYX6SWiY3iE4i014uHjPDVFXWwlwmEUUVkcnlpMpGmqSFA5dtCou4NxNwMhZZk4UJHydd4gTJs46CW6+1DsbiFPaiTvf3GiJoVY");
            result.Append("W/waN1VenVC7jBFuqGlZntsAxaXy9hw8CeRXa2Bix8rin0xe8D2qEerGb7zcjZTUqoUfKamTsg5VN6XWv3/1Gry1teTSyWRmPDlaj+iVNyWl9AjsFOEValx/Ma99N3WKhJ7GMjhR6gW1M131OdUklf4+Zp+h7gDAS2J8slit3yf1Ew2zUcieEpUIovd2vv+18vSX/WUs");
            result.Append("3Xg8r1I/pNYT1pYMDm7tr3Yi7dBQkLSfH9XXf51J/7f2BaovPA6FZTxfkHIqMPW304gx6vtpL63XRv7SY8G0+Wv1We0G0X8IXliw/jD8qkt8+Uy9PPlMeIgIgxuwWlTTznO3iALod1JBSzrTEZsizacriUtsihsVgx2xiWjV1SwIfE49LGcWy8U1sDgvX58GizfU1KP2");
            result.Append("LIyZVNKJ5pLAO1TZu1fe/jmd0md83N1UP0Kd+bHws7x6df8f62pcVX0r+Lh8dQ4+rp678E00a157k8pPm2Djvv5st5Xci1tpM90gN5W2+o/V5bx+u6JE12Vbk+t/qH7RT8yf6fiAtMGONiyLEm0zqLJjgqut5ZqWXT0bhGj95yiRH9eMLvxPzS4NZdSrUX6kXCrJlxf0");
            result.Append("J8i0k9B+sg14tLFslIhl3/+KDJ2VnKm9qULKGumE2eDlwF/FABOqHo5E3bIe/v5XpbQg31zbX55Qy9jqpM5RrZBdq4mDqTf7d14Za9yRUAj+k4v9o85LxYiijvab/UrTsIOJms2CZ92V5UfyrRkdJtUT+vUJmNkyPqqybLfnKv/YreytgKln8s0NsDizf/cRmLkDn6qb");
            result.Append("TeWXa2DuDXqlGXD4K9Mpf+NNW+WI4Vkbazr9IR/y6W0go7WcccLx04yTpSJr1srfgkVdqGjePteuodSMUKEEpY+guzMhFwvoyDmgg5kTaX1EUZ8FlZjj3DjHyLQuPHVCjSnZYgCL4M5Z5IU9YvqjMcfwC1Q2niJ2oil2rs5v7THMXDI+ESYnL+1PX0fl9XoGqt5BYEAE");
            result.Append("voP5o/6uvqNGv2nFA3TqclsHeLaNBX24ZxP5FDEHusKfoukuFrGcB/eu1fRwxCFMtKFcH8mPr44JNuFzTPJ1pEtbj+wjeq87hB5vgxetg9tPZAambshzT2EMAZZ+VvOcaCkHGiBttpvwEJYKzMBrs7vrpH3FZJ9r9QxmpRtjAtlUUTHY9L8aL3JZxbEsFRk/q1Yt4lu4");
            result.Append("6eNZuMFPF/3YRMl+i+FQ6PermTBX06GiZg/EP0WchFFdmBVxokQjnOa3EBa5LnziGmQTQm6qKBhExPmuyagVoUb8dLYNRrTBcxQ/5MHmB/n2B2thQueUv4WJOo1NjJtFSX+LZIxq6sROxdhgRJ15JhJWDQsjjosiAuocdSa0Yf3dZ9VvG3xoc1e9RPdAZxS8beCiDmMt");
            result.Append("Ct7y3IZafphZ9GvVxwar9jLCbMIKg6vLMrgwSJ/Nhxqu5Bxo9PMMNHD3DQXQ8t15pYRlabXQeUAqJNOjdMFFvPkWG3GDi16jnjALLuLN1+4IXlIxYcLuWIrj9QJjQodRJkwwqGgjrzh+glt1/h0eywo2MckpPLQOYJxIoSzcBddvg8V5v2yNdYoUdUO8+QzADZEfPBkb");
            result.Append("ZKjJAc05zR6V4srb8yh4UVa2wdQbrclSO57rNTnD2WzYu7g9j1IM5OsMGSvzS6sdHVf/uX9vDZX+6kXtoczhVKr2kVpRu/6ZoXDXv3dZv69eC9y9Wdm7S74nolU0eNuI7duir2pcPgm/pDYy7pgk1d4n2uLXM76PzvfA3ypm+1boN+qtnPWbhHpiau8T197H4g7qb4IO");
            result.Append("Pm0RauXpL2Dlobz8TmubgReiLai1t0qE9Fu+f38ZrK3Kk5vwXwPwuYvHM6rLm69/am+zT6ULuWyUzzrRYVtgjRP7iafeVCaXa1x5/0XyNji1Hm7Vf6zJJzdEq82HzPoiX22DVuuBvOn3mgHzeHZxb4hnJGddSd6fmlN2N8p7T1SjYFIYNcAYlAqFdGaELp5LNF8l0q4A");
            result.Append("psWgrq+HR1CXIPJEqGVwcQZ8eGc1TRp+haouSzl30xQ5Ng+awGIX7fUTuWwNrf1llXvUCCehuizxs9AGKIexntf2KdzEPmkUAif26avRlHY5RjGurSn+Uvqh6fMZ6QeD0VF/OKIxpLBHU9KoVCAe9RK2SAAbDWxRrrDhDa0EbvgFZuDqC6KIiRFrrytvn4H5R2DlMcZh");
            result.Append("g17FaSmZd1AvYUgsisTjTAitTvxQ3YeilzAeJYLaGcOUY2QwyLwUotkK+bYrRHCfmd3nZvsF23xe1M1ecyYYCldSYo+0tltDSb3kraVlUho46DHvDwM2e6Op+9DC4VZyyoI36dmB4zDfLrIr1mypkqAnj3CM8HbS6V3QwXvU4tfwd+Pn0Y1Oncplz0u5Qlo7In80fFP1");
            result.Append("zubU35gcrce/qp25ZMTB8qqaGbt06VLgMVDoZ7MhQQz0E2N2kPqJXeBEP1stU4u22YdvndpO6/RLLbVJr+k70CW+wtmsEcK9cOJpakI48QsaObQECVtr1ytvX0NzG9Aad628pFLX0ep1ZX4ao5yIt319qDv0SQhv57PJQzr060R2XZqx49puGuvSR1Qzq2wUtSf2Skkp");
            result.Append("LckTJcIuHstl/ya1Zy0VK7vokHshsKj1NSvfiRK3Ews1a4wGwpYdk6TT8Le7CcAYmjGn4VfnmLA+gcsudbnCC5vovESHqPzmqX8OS+ykZJwCEVnO+NapXDpqToALnDpGFtFL6eNc7jOkcZp4asYrfFMp7WtWGHLNGwoTM8nR9mNzO9N4LidlhinbysMRv7Z+6IOPWNOE");
            result.Append("wsSmZDC1ri7wNoHuqHhggQzv6oEZHHa0oHCEGGe5MUGgM/htNlcQvL5ihxB1BYrYa6ySqnFu2dcZV2tjqMEJswGHuvhE7C9Wfv0Iis8xvnAORsZ5/r2trZ73+GFvhw5tt0eY2FgMlatcwqaMDUj5YR/rFW3DR9hmiv3x/NFM8qw7N5Ej+9AOFtqZA2Fimj1YXVfLa8UV");
            result.Append("VFgrb7+S735U1oqEif7h2GhyhHuTzJfQt+v6vCtUa50oF1/oJb+hzOF8XioYZhBvT8L/as9Wy2H64Dq9dUabWFe7RusBQRfoDTLlvSfK8nrtAuiFpdIFdEV128nGHTC7Il+/rTzaq113Mp3RVwB/9lmLK0/weNXuHrvu2ubryvLlvpjcVCICMD3rH++fc3G+kwMnvnXT");
            result.Append("+l5cK9hMzwrDpWtUv+a2WYd/dbyPbwGSS3VcDPUQunYmMHD9fCtBHQwc31JHBwPHN3fv2gmoRQrEIiUwNaV63Ktb4MEERvpJpXKSq2Xn3MNMplGmyOIleHK+Ll5EGVITLOXF5P7NZ5bidSqX/h7C8GfJzRg8akGLhfoTLETNYeQusqjxLWYQC7XRPG0r+UDSBgUjT1nV");
            result.Append("iPq2qmGSNrdVDa8FiG8KARcLwlThFzSyVESFp1MslY2pclj1EdlU8c28MJM0c7UMN0OCD5KzEyeKlnlRxckDFr9XibzAIRP7jOSbEnNpuQRMf/K9Gx7k2Tozgx8L8c20dWC9KBbinGPj2czpYBvj1+dTwo+lZLuNscdFz5hxd2XQhEmnV74b81NPiuCxKNpQp+4jRyz+");
            result.Append("m9fB7hK4Oofo08r9y5XNTfB+Q95cHhrKoLWxoU/CPWjzbv3FC5vl0jOVCmJ4cTj0b+D9c7DwBpGtMRopBdNarLYTh0Mj7HS2/jMt1FZ7U0HaPGMhwVPELJ1KXeQcWqOemt9bGwx8JJvOeG2mOE8R6tQSWyzEN6/YpvA8KMe1HpJ3kBHwSeaSYLH7WTptxJOazk5sIQSv");
            result.Append("H5aL82DpZ/nBk/2lj2agTqQz3wndNGIDk6dTlXwYhfBNIjPT7Obzv/wxdImxRh84Ue1gXqaru2HcqOLtHelgwie3O+LDVFW4g/mpPrvP/uXVx8LNCgnumVpm8tRQt7ywqMxvVZ5gKcNv0oVvU7nkD7QcreYNfQJztEyJSHad58SOZX1A5pN1n67XC9tlbGmRIjctaxIq");
            result.Append("2PzCcF9PjAlO1L3oMWLniYYTo624VljxrdrZAUUdyMdwk6fMvpMnJrGeWWifx1vPc9B2zB46BD5OgY0n8uPtoaHMN8l0QW9M1ZtktWf2J+5X9qbVFWbwN9W3w1V2X1bePi9vv0KtsZn8OYiM9uqo8dVoUQJ84Mus/voY/ZIxu5tC3Q4fw2tj8uoEWJyXV97Kty1lmKLw");
            result.Append("LJ4MUzfIx4h1GNq9LheL+PFx9EJyTOz58HYgUbfLx4i8pgaSlTSpKIlNYrBDyWHDuQElIq+JRGl7DtkddTKmcFtWwqEW05w2qDlcRO9xLSzMl+tg8umJ6NH0bIP8YgwfYoJ8DjCzVS5NlD/M+LSEcFBmXsbCfNkEDASMnCxoMRVNq+wF4tR+ceJLemAgTuQAHdVAoTE6");
            result.Append("rst44olWx7TGxsJ8aSHuRSsu5hpWRt5lx7T/xMJ8ORkMBAnP40CjVN6eRdG+OkNXG4qFh3lnLrgK8NruiFPMyxJVwPhSA6gFTGTI+Jao2wSZq5JTNf/obamJb2G6Q1foxiKCdkfVjxTrIl9tJwWYegl2Fmo9Bj+GLv2v0CehKHwA/om6DRoXXar6c0xykz3jfwDZDGyk");
            result.Append("yASJKo58O88YeDhErr8qiFiK0ZVEtb+U5x8OZVtOm0iz7KZ7YgMx+EPLHKnU5okdFPYTJAc1kWRMTzrnOcSbD4EQmOcQNcoyO54DsXcQTQJKp37fuQ5PG/nhqnx3U30N/Ld+0vz6RF69ii4r7+7BP7DCn9jFLBOODGkQcaJM0zhjhf7RIkDtIJ/bgETNgYjj9QgWBQgB");
            result.Append("ARJ6x0wsInpVIY5XFVByBTElrIqdVXpFe4ueEUa5lrCnaWEfOiaCljEO3H3woBWUwVLROFFUmnpTmVyuEWnxVG31DovrxNjsEvU0VA6I5p6pmwcbDhioWwLnA5e358sfriGlU2dmaFs+lNcl8BDbpnNaOoc3BdMpX0vUzj8Yb+Yn+AcHKhmoZAsq6cGswqCDqOOlyIdj");
            result.Append("CDmNVRWsdVngKUb+a7eM8K1e+knMPacPiSPnqoZ3tJhHOXcV8xgs6cnd6LBBh1G+ZdzOGx7s7h7XqgLe3mRBOzwOWqoxKmjJQsD74E7NEE/V23vLN53vnp6VIDq352+C4gLWkJzNuepsZ7gj2+ku3H5H3p/I1Kqo0BWJIB/DLB/j0shpOuatkeObvGdg5Jpv6TqZzjCY");
            result.Append("3sFwIvWBNnN8086M1z04EDGTp3UAhKzHxa4Cg0aKMvw82iyR7Z4xis/UKO/cBzfuVl5cBjP3zM7YSWnsrJQ7Pa7LgXOeaCLaVFQF5onGjPLJjieawMFHsIPFOeXFFlbGHa9aixaVmCUFKx5vLSFqgokhDTSB82l1nK5MgY0PJE76F/QvUNRU0ATBl51ZAaViuVjEON+n");
            result.Append("jx4+c3Tgr1/8H5H7vG1Qop6FlSAIsxpKlkPDdKDOHD951E1bBucxOTZIUQ+oSuCcUTSaipCnr08N+F+eqOdSJXAuXJMBXjpQ/pYnoRcQxqLNSqLuHSait+PFJpi5It+dV0pXzDf7OHy7kVxy1JC7cO409TYfsSCw02TaQMbOaeol1mxrNwBLHA5nc627AFYBj4tGur4W");
            result.Append("ox67FW3UCBGzHhEvcCgT/hy1gsE/I5+jmQrwz+jnysoMmPkX/DMGH12Q5x7BP+Of7/80Udl8D57tggX1xYnP6y2LQ5le09NgZ6JcWsL6B9zxDbkfBzbwUzthvUQTXmP3whczSW2AovbDevHwp9om56r3zXMaiA08FA2xHh6WsWbECveHJR6PoDusvLqqFF8QnagowXBC");
            result.Append("+l6iPizNh7OPDssEp8OSuAEa9Fj3ZA1xbsxwWt0ywcLyiCRmOK5OgLUXqiN3C8u8nExecOZNsO3Kt3QmHANFf5gRESAEamvBEqh0xv9A0R9mREOUz099G6Cocwp9xGh7luOuvY+VbXByvRmQ7/HPd88GbmWbHv+an6eHzJTnf585NvTR+W9qdWJ3/vcRoaAvXWybRjBq");
            result.Append("N6APD9Xcu0c8WUiOgaE+9vvw0EyeKFnlWc5kC8nRweyw+MkWG6Soz/0+Yt7+2lvwjwX5yRVl5zZYeG/G6/D30KblzuXgv9szurLlEpUNXvTHv3WJyr9+kg1Q1FWFPiJE8befZIMT9cKLPjxCKW/PQhuOH3GnGh7+7W+Ys4GGestFHxGTrE3Lb9bLe0/kyU2sbi6NJXNt");
            result.Append("mfrNyiKJvdUixrfnAU+dkSRa7ALfzBmN8W1SIIIOAjniCv9Ax7cHgIvM8cxceuuaM01d9njYHcpXJjk3E/CVSY5ZYy/CIs5pYxZC6k3mWUiwnYWgnWMR+DZxWJBwCKNgcY0zu+BDwoW3hyFTroWd6Ne+nSXFX6WHXDL+cMurauHMpR4/6RLfHhYmulTTmn6iEOSzHKND");
            result.Append("PlfnrBOK8Z2t5NKJqwsY0WJ87Zm8ehUs/VwursgP/qGszGKsQRUBVajyQidC2Nb6RZYzvsONXMf+dUkjdo1AX/XZbnl7Qn75BB37VnnbE8l84UwumfJ56pY6xd2Pl98qew+hv6MD9vIJ4ehDwSukM+OSBtoRt52zPKcDsM12C6yjcb6TmTo5PxfnO+yIE3SMtHcgebH1");
            result.Append("U9bPymsXreiXWkYhVvZOlFgjzneTDVHV0/qSwbVryi/YngLkDZ6UUtS0oX7fLrDpM0oqO9pQf6MFNv6iDZnQYUgb6ic4DJpYWhE+NLH8MiuEpXMMEDV9qJ/osEUAWZRwNIDEqePAeB8/DWxAomYO9RNr3lkyYbyPEmxwomYM9Ttor/UDEcYGJ7EJw3G+5Xj8LG+Q+qlf");
            result.Append("YB2QR0JEbvHuJlh8DlbX95/f3n/ym3z3o7JWNAvP8fzgt1lXU3LOpttmhWhNdSREJMkQUu9/3Z+4rzx6Bq7OKb9gZKE/JfNfZgvpc+nhJHqZEJARbjxbwy1yIMmX6MEsTxYJEYWyt5uVjSeoGmYVMmq5fxgxnUsX/JzuoWXwRUIEQd0AmNViYgNgglVLnGLWObuJ43w5");
            result.Append("LwzVE2f9g90laP/l65Pl0pLycplUz1wylc6M+Fk1O2eAYZwvkYKhmBHZBc2oIaZEQ6N2WlLLaiOiFYGdyluYNikTCRGMSA228t6DyuYkhM0Cs5MQsLQr/0wExBzmaURWUc6rrdxFTwKuN+B7N/jSGBgaTDydhiotyAsEa/9qWGlBjqCPSy0OHWeRVV9wLkPzYp9bETwY");
            result.Append("8tfDoNRnVFlRSn0JwWv8dSklUuowgIHC2TCAgTh3QAzD00M60MGhU2fda730JYFEDJPGl72Al6wqG1tg95a88lEpLYC1y8rilcretHL/cuECXlNXdwX9KZ2HgnaRjs0QCZk/3Edshn6jNjJjM0TCzUeoou1MLdqndvB9TbC4pjF4rWh8C4a4uhAmCr9AmJC3DUvBEnzr");
            result.Append("RALdC11NBb4VfGsCLm9F3XQ2H6glONPCxnB62ijhN/nkW0xwufStLp94QRkVERp6ViKf83axSBvF1eHRFizGs1+M1+M3g9CkdDGgCZTjgKnhYcTvR/DN+FeXcVubNNOzwrih7dEovqLKN9VOfZerp1aUmIsqL79TVmZVYs/GHfn2B7CD7cFFSWEP+vuHug8dUnsXF+eV");
            result.Append("0hL8Mno1figzKBUKo9qnof14n3dBz3AoA69+t155f12591EdzLG5DB87JeWG4XVdn3dFtI9zzHW0WyPmaVEoUC6zcvXyrQUwNqE1R7F5h49HmgUWZ5Ti89oYELXkk0sOF6q/qktTKi4qY50jqmb8HJaxsG9jWc4KOapeBUqGKRnfxD6j6CtKNOlprPGJyt5dKxbXaakw");
            result.Append("nmOx8JiWkhRpmZLEdvhtEIkFkRhL48B5lGgy951U+EoV9gbGAbtAmFCmeo6yF5y20NN6fdFqFSVqnahXWL67qbx8CaYfl7d/LheLlfn3YOG2cv9yZe8efMSqj/vL8TEX6bn2dcl62qjgQzHmW3QybbcnZNj0rKPpFjGig3BhUZnfsnJsBHRpSCGONBditsMnm0/ggwoV");
            result.Append("08KDeo9vHO9zqk8+NEF9THJVb2o7zM48R0Ywe2JbVIMwnmfusbXBveJbmT0ynstJmeEGZVnTsw0O3AhumcDcLbV3UptFjKyUWWlOJi+Y7KCwyhNqrjxOaY49dcjwJmcVsjvPGkM2kLzYGZB1zjDA3mYVadeMvSieytgv3QCLc1BOzILxxSg0yKofRUfOi0bMBWv/kPOi");
            result.Append("fEYNRSP4yV8DHsURZvgFnzgUZTtxyGsN49udFlRHxcgtcx7eq2WHBqTkaFP+mfVljTweYhtddUYJ4lKbbcTx/J+SmdSo5NJMcGs6sBnf62lDuqD5toF0ThpGd9tb3eDLHAhSq4LkpPr4FrGD+yzKfeZbRw08GiE8mr6gIHYwtJlzQSy4z4LcZ74Vo8Bqi2G1+ebZg7ss");
            result.Append("xl3m20cU3GUx7nKQOTwId9mLzGHgf7Xf/xK9fyaCD5AEC3Ng8YZ8/Se1bHT/snxra38aa6E5LX0v5fKSJj3/OZ7MFNKFi22h7CBaRk8LiWIbUn/QBtNGW9jvqzYY67sc6JNbfQp0pKmOCN3FEnRlMJOioCsDCrvQSei6qSdWV60UwcZ9eWaxXFwrl97Lj3d+37m+P3Ef");
            result.Append("bG8ry+vl4rzypqSUHoGdIvjwhqBDHUtfUGmp6WGpHTS5RjOmCV6OzWbmoOTepkijP8joH4z7zDejbyZlEnfZ/HQjw0jMZNA4RvB/y6V/WnCMDo9I7no+ODKM7GiInUMK7udbRAgMiCgGhPM4MlbeFdEQ/2pReV0CD6+Zzcfg+FlPRk58CR33rs+7QvqolldP1Xnt0Icb");
            result.Append("ygwWsufRVJmu+iiXV0/luUf1509k83l1igt61ugHqhdchOKSzatvrzK4acyUjVcWoRi+HGgbE23jvKYksKqC3GdfjUOrWtk43sxV2dsFs4+V+5cbrEI6nEppn+STfUh2s3xab9XVDbHLYT5Iuo24WFykRvXBwB96TeRbcjuSTWegFuSkfL5B3yl2gSiTmWvfycu7EQ91");
            result.Append("QHEn0DnbuxyQww/CXfZJXp6YonP1X2BpEizOKS+IzVpjY9mUk3Irw2HW8RCLhHvCWWRX7xaO4rMP0DDFytsXYNsSLHUJkaSebv4Fyz88j84Kj+KhZtUJ9xMHCI1/dVu59QxLFBVy8GNGKFcBRaPN1zcIPG2AzyqgaBTfF7A/8RMqc4Kln8vbP1c2nhI1ToMFd7VAgO/k");
            result.Append("AbZLggyIERvoNTG1ymp6NEW3svFR2d1AyUw0mdoqmVm7Sp57VL+qmtJsJWHpGGOHZRUDxnh/O8IYXJkCGx/ww18zLe04zVrdY2EHk7NRY0aYcJpauTRR/jADVovyvc3y9rXKi+dgAcv9DA5LGS+EEn68NlPoHZQ16ANq1kIVxs8+qybfNYYFukAde4Ce1IUVLM6AD+/A");
            result.Append("zFa5tFTenUNXfZUxDob+7DM+Qksxy5ahx0DaVv2GeXvQ8y1Pmw5xIkAwPdsgOojirB19AMbCJjy8zAJ/NIP8DHqJZ3lOkZPK7eQx7htvVxDZbVYBd+2kElMwK7svK2+fV55gUgfDrEz+HPVUrKiZfOEfPzVmmr/G0E/tawqI4GOwYnZT6ahhwWMmv45jtUOI2pUkx9a6");
            result.Append("H6TqBUIWZTAbiKjdyBhePyyXqtHf2+fyzHto4sB0EYMrlx3TM5ieRDl/MOZMP8E/mbU+OvQBDVDi4bSK3cyWEU3snMh2LooOPRcDikSIvfymsvkeRgH7d95i4Z+hc8CF9vLdu2kHFHX2N0asoN94Ij/etgLq6IXkmNjbSe1Qcpj2NaBEdHNpKJWLRd2PMoHk0JXwOK1l");
            result.Append("B5LD2fsGkIiIStt6Jq+8lW9vWQnU1+dT/tc8h40OBrhwdrQy+06emMST1WgEOLUotbpG6+MUEumhocw3yXQhnRmp5/3QM/sT9yt702reb3x4WEI5v2oi5tpNpfQAxTHwgdPS/5OG1bxhfz+XUyFMHQPEcDKp8Zsr9y+D+UdgBduhgX7NaSmZz7al4tVqjtAWNaEn5cZD");
            result.Append("fOnIpmiaXN1tfLZBwiiGB0pge1J5MYfnkoUuizIOAMTtYoiH+PJtqcWpOQ8zOBQaZzkZ27ceNiTO1vmZfMWdL+3YT+JOX3ByJ+9I0sHGdXlmUdtEOnZeEwq1m6MzVIIlW5mvNvCl/rr3JeLWNQCw9rry9pmVE1p902PJ9GgHuKKd42eE+fKaGRteMSDjSxKmLg0f+Lpn");
            result.Append("mC+vN7gx1DeG70yH4MZQ3xi+LBcGjgbR2Pf2KZh90jjJf8z/WX769GCcqL0Z0LJI9h+T/J3qp0kJiupUeDG1gskKpo5sJmiyXcjlG0Msrae68RUnoYdjdKQItaUfJezFWAa2m9t47HVm3JXf4vBQm61tTs/xjtSKjjOs/luVGMcpMqhBsoHaad2Rbdqp3kj1CMewjZoX");
            result.Append("2DGP+0EC29SybRJ0SH8wvYDlXY4IOomkbhhxsqH8ag28uiNv/KasXwPFhfLuHtGidGQ8l4PfTfyxwTaTmHw0oK4TVUPQ8S111SBGbV8pKaUl5f5lNNvDaqvCsVz2b1KmNuGjrYsVHLRqNNcT/wyw6EQ14Tz/xt0Shrqy4OQaRDLAk9jVMoK4aexeG/+aot862FIRbKlg");
            result.Append("ZQ34FmcZHJoE6V2b84HmeMCgGx6aOOGuOvtDne3h+MBk6FY2Pi+dHZcR6s6fODFS6EUJzK7rEFqAdzL7fTozUodQRPgce+X+2ebRie4G51Ea+rCfE00zppZXNTA4CaJhuvMMTrS5xlB3hSVwwkZH2hsb9Lyt/PBV3mAl90Ew0ZzpAobf1iQkNF7RyDTjDCe0gqJy7758");
            result.Append("7ZHKQF/Apgsczw9ehOCPuUmXcFzLE2ZqhlkqhbO4xcAjw2cqoJZCb8VZ6GUVdWEmKojV/VLyahHcWy8Xi+Xta2pbLz5/8nj+dPZsVtRdUzbDa/2T7ugwslCEM3fBrZW3abajsP0iawlT099j0/qmX2rR92Y8Iqm3dfj6sBC0al6Xe5y/Qx4T6qIbbUyonxTAJmnjaY9d");
            result.Append("EBCYlSLKl2TwTbrwbSqX/MFaKUzPNlIKnGSgLK/LM+/BiyWwiGmBenvQP1pUA+/nWoQjnaIL1WZ3xpUW78siUb5cAmoNsHOSnOnFaSk5eqB0w42jVDckXq016xxt4ks5cN9omMCZOb7XG8bzZAXOUUf5VrAZOCs4k6XWkWklX9URjILLl51d9hGtpXPMrOj93AliG3Yz");
            result.Append("TUD9yYLrwQEaFhPl3KQsjZ2VciellF67JfOH2AWNhIyYdHjtmvLLY/m3SXnrLZEl1N7O/Son7ua2r7mY+WdJRlty31G+FU7tN8ELR3KNZJe4ooHw9uLkEyvD6AP3M2zDeqVeZ9CLM0xUVslqkdDrU99mHax5Eg8g6mUGveY6if7BdfdyrMq79icunrqVfG0S3zK1a5tE");
            result.Append("1RYb2K/AfgX2i+d2Pb42yQOOAH1vUND6wqx8GLS+QGHnW/oPpiQJEvzF+Fazg/ssyn3mW7MN7rMo95lzAzOHIUy9ONcXbDzcvzeFVkCXS+/lxzu/71wHV+eUXzbL23Pg9S1lrUgGTeIPyLAZJuSfNGUwd6hldeRbf2VFt+/FeZSV6Z/B7DpYuC2/m7GaJawqGFSmYWlU");
            result.Append("7IHCNnz7sMONeB2pc+05p/iWYwN/xKtguhFBgK/0eDESmoFZtWYcVjY+Krsbyv3LcumG8hM2E1FzY1Dn72kIh9AOjZ119Q83N9C6VrQumJwdSA+99HDuPGXWSN2L8133lz6Wt2fLu/f2nz4wG+sT6cx3XyRHVUe4HYa65aEMNg11bbTTfm6Pi/myadSuSB1Iv7sZzr6W");
            result.Append("aL5VH/0X/lm6mLcWaPyCRhYaJ413gIzaCGnCN640H5flcCqVk/Je+yxxvtUxZvpAjAFceSvf3jJqhRptWuTxvj6vJuAMSiJ2Os9OSw767Nw2aQnn2mJAjAmIMcIQY+K+KbB2ZEKk0wqEcS8KhBzF6UdfrlJiUq93uGKzx6a9vvYNf7RqsEcri7xqrg/0t2X9Fb2ttJfo");
            result.Append("+NOK+urovPlHYOWxWS9RTf+0lMzrg5c8JpJH4nE2naWexst8JYxzrTPwrgPvWhzvmm+Jkdqcimwf+NbVOhIyvoUbxgOsPDGmHTPnI865cYXRBqc+ohd0agpM7FR2X4LpIljdAg8mzJ7ZoJSBzk0twSeua8Z4pVO9+bEP7w6tQzb1zAqy09KwlP5e6gDMHFLDDZjhg4ER");
            result.Append("Zn8EUy/3lz6SpTL0D++BarGB1A4n/1TJAue185zXBOdeOlZHD9Fvsz2pvMDGECDygW8NgX8KgYEh6EBD4KsBuUF84eje8q3/saKE9JnLMboBrZt3g9WhNvJtJnv4p3erk8geCX90NPbhrTfg1R3l5fPy9q9gebO8PaHs3FYeT5p9nv8YT6cE9nnsum18ow5Bu0Qresa3");
            result.Append("sHhkPJeTMsMN+ACmZxtpGE4Ul1cn5NtbKMGwP32DoOKeTGdOS6qGjLg6dFyU/WOtnTsHZ2ptgm9t0f148D5i0f3eLph93IBVAk9EzbAkR9tELWmd7m0zIdw/iaz2OPY9dVsPxZ6DYvCtQwYRnDA32i9diX04x7/y9rm6hWP2UeUfu2YzeGw8M9yy9as+YjB/6YztYjP4");
            result.Append("afB7d33eBW/QUObQIRk61aW7dbM8lDmVvAiNMLwi/NlnYXQRYgTCPzX2H3oqor9+YVGZ34J/VmUfPRtFz4KZm5Xdl+iFWrkZPYuOc9bbBdtoej1rHeOuVR3UGXlwbpoHlfPgpjG+ab18a05tZ/9wx49vqr7z8ePcDhOwU4O6npiRQy/nRDiban8sTBDN1qblN+s4U2os");
            result.Append("mfvOzwwpxzvBtYeOwb8FyaME9qLT7QXniYc8TkoaK+JroiUTM9LjYpFu1RJ71ekXmJ1ONzuClniCTDbrGy1oyaJ+hhCUDH+eIbbbfX3vi3aogvAt9QSWUJgb3az6gPwdBzdauxa5a6g+hLfUVHZfVt4+B1NzaGEzWJwDc7eIcRdaou+bb9PqtK08Sm8P6L+2VpVKn8gO");
            result.Append("fzek/cYqrCYTGsdaoHBWZ0P6mm4lm1b9zqquqG3hDzeb+r9tR2mYbKb+deomM8TEYvbUcWq+J8+Qz6cskdKB1SLxNWF3wLBGC28Ikd+XVJKNxbw4Ct6wBXqcicN2+DnbT9kCfvjoG3lprry7Wi4WdYWswffVeYeiR7o3bZU8Z8y3Zp5N2+oVfIucrue1xAnisnx3Eyw+");
            result.Append("B+9/VUoLYG1OfvTMLFPH86el79PSD+7MGSOJItkWjCe18PGWvZO/Pm/qtWZHo6Egmi8Txi2130jD/Tb5qizcMcYj3Bbj0WOTr8W+pmXeNuQoZeuhIPOtz38xCoFTf4C1JJufbnTi4Y0EYHUdLP1cLq4gimN5+xUoLivLWKZo8NvsD8dGkyPcmY5/MOacP8EJQI4PxGif");
            result.Append("Vz6WL85DvlV0F/JpZ2rppFbl5gZSS2mImxXO6uZATDvMt/rLxA6bGebWLcMpsQNYOzF1NnyokXE1hP79rWAmdp+1HWbOhg/ZY9YfaorZyWwqfS7NQNB49+Pa4cYmsDXg1jz5W8XN38LmbHCL770fvsV5ft6PVzrc1sPCk+Cy+dhv1RRdMkJieVXN0onp+jQjJrgvx+HF");
            result.Append("i8pvl/enb6BaHNaArnrERy8kxyirb/3RpkIvcPUtahR07tW3frwegtbe4sUQ0atwJtB4VeHaqpge9L7W1M2aj1p7ukFM0k9whoSfVQdv2uDAGSx8thMm1iXJfjw7gZISyusSeHjNjB6eGuCZlDh0CFnn33dm9m++Btenft+5ir7Z0FDmcD4vFUwNw/Bmw/9qFxy9gDwS");
            result.Append("U+dwee8JNC+1a+CBkkoXTN3D5eIN+fpt5dFe7aKT6Uw6M6I3EesftTijFJ+jDuXadYY+5XCo6+9dqsrbWxIyjW139w9Yys+D5mC3ZodYMGkxz+bwWK1RVdgNNjaCxzod0k8sItSYEMjs9HSFf9+5D54+BLOPuyLqn9MLYOsGbo3U13pgjDh5BcyTJcT6lpkVUCpacEuQ");
            result.Append("K+1zp4pxziQRIvpWNPj8S22ywY9N+sSAHzFzXYurLMRPNalih/t22DmbJ9kCdsTsdQ07K9lTwRM7b2cHHhvOuwE8Yki1RbPAkewYQogaNe7NArZRJJvg2ze+nwczRtz5fokQnr1AaquUluSHqzAoCH8KFq/Llxfgn5HQp/K1m0rpAanNp6U8FAsXktnGxMbBEsl+D6b2");
            result.Append("04mkbxD0YP8zSwQZqnq1gebAqnmPC8qG0U4KWbno92C4TYebBg+GpHhjGpTZd/LE5O8710Nq5uLjFDIVXeGQbiO62FgInskMktUlho2w59ei4MCIk8VFqnEV04xwnvzhbmR+G3n+nTcInrsocV4GHIjSwRElvvMiGM0vS4SISuZvj/ensarBf6S/lwak1PhwoX1FmZZn");
            result.Append("vwejzATTxWCmkFPTIejK6brRINZG+N1o2HXCRQ5Y0o7riI9eomqoEVHA1A157qn8elqZn65sfFR2N/C876lcelg6DT99UCoU0pkROqJhIt6cXSsu0bDXJKW8iYaJOF6dlFcnwNYCYvHsTy9g9M80IvT8l4q4uyIv9x1EvXaNr6yRxGuVKpJrLxohmbzQMUgyZt4l4sQw");
            result.Append("Fc1emAFUTYQr2LLjmsVyihoTxNiw1QyIMV9V6T0vww4zxkSrRDzeAmaCc4HsMGNMpUrEE00x+/p8yv9yxpo/Fe9tATOfyxm7lrO2ecN8WRUN/FoiMmtwXYMYLY5zRZGzATaxpX6DF8fOZkfF4fe3yVfzSWSWCPGlUzCVRZsaIPsDw3Panie2r8emvte0aqd91qVLnM8c");
            result.Append("j6mmdrAzoZoygV1AA+IBmyhYQ8b6pnFmymhlxwEpOfqndB4qS4O11daXNfA/+vEknzyzKK++BDsT5dJSuaT2f4EPb+Df8tU5efUqmNgx+yVfnZcy2hkjdgeQF/FCR9Z9B9I5aRgJB+M3hvJpXRzzUF8587J46Cue7FVWZspFLDMJz8BzacF78jzwyAKN9J9GekESY6yR");
            result.Append("eNFAufovsDQJFueUF1tkp042lS5cFDiOt1FMEZq0A8Vsg2J6QbljrJh4DYrJABav2zUDdQzU0UIdOdMWeagj3ktQ3ntQ2ZwkCEnwTUdcTbJpu/MqROtgoJVt0ErOo6R4aCU+Wsqv9ScbnWRdRidGS+m4WQzm0khCYk3nChJkgUFryaBxHlLFzqA5ka0DGrqzZl8FR0dw");
            result.Append("dHh5dATHQduPA85za9p9HAQJo0AdxVFH+E9dSjWFrH+FqlqqLTWqAMF/D16EX3nsk+OZQiKmfZDpal1VyesHC7l0ZoR8QV1fW3+N6Tc1/Gp/UX+W+uygVNBf2H82Ai3LudAf+6K94T/G4slzf+yLD8f/KPWmQom+4bPhs71nuy/9D3Cpll6ATwMA");
            return result.ToString();
        }
    }
}

/*<design>
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
<design>*/

