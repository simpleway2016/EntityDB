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

namespace TradeSystem.DBModels
{
    /// <summary>
    /// 用户资金表
    /// </summary>
    [TableConfig]
    [Table("moneyaccount")]
    [Way.EntityDB.DataItemJsonConverter]
    public class MoneyAccount : Way.EntityDB.DataItem
    {
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
        double _Balance = 0;
        /// <summary>
        /// 可用余额
        /// </summary>
        [DisallowNull]
        [Display(Name = "可用余额")]
        [Column("balance")]
        public virtual double Balance
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
        double _OrderMargin = 0;
        /// <summary>
        /// 持仓保证金
        /// </summary>
        [DisallowNull]
        [Display(Name = "持仓保证金")]
        [Column("ordermargin")]
        public virtual double OrderMargin
        {
            get
            {
                return _OrderMargin;
            }
            set
            {
                if ((_OrderMargin != value))
                {
                    SendPropertyChanging("OrderMargin", _OrderMargin, value);
                    _OrderMargin = value;
                    SendPropertyChanged("OrderMargin");
                }
            }
        }
        String _Coin = "USDT";
        /// <summary>
        /// 币种
        /// </summary>
        [MaxLength(10)]
        [Display(Name = "币种")]
        [Column("coin")]
        public virtual String Coin
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
        double _WithdrawFrozen = 0;
        /// <summary>
        /// 提现冻结金额
        /// </summary>
        [DisallowNull]
        [Display(Name = "提现冻结金额")]
        [Column("withdrawfrozen")]
        public virtual double WithdrawFrozen
        {
            get
            {
                return _WithdrawFrozen;
            }
            set
            {
                if ((_WithdrawFrozen != value))
                {
                    SendPropertyChanging("WithdrawFrozen", _WithdrawFrozen, value);
                    _WithdrawFrozen = value;
                    SendPropertyChanged("WithdrawFrozen");
                }
            }
        }
        MoneyAccount_PositionTypeEnum _PositionType = (MoneyAccount_PositionTypeEnum)(1);
        /// <summary>
        /// 持仓模式
        /// </summary>
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
        /// <summary>
        /// 过期时间
        /// </summary>
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
        /// <summary>
        /// 账户类型
        /// </summary>
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
    }
    public enum MoneyAccount_PositionTypeEnum : int
    {
        /// <summary>
        /// 逐仓模式
        /// </summary>
        SingleMode = 1,
        /// <summary>
        /// 全仓模式
        /// </summary>
        FullMode = 2
    }
    public enum MoneyAccount_AccountTypeEnum : int
    {
        /// <summary>
        /// 余额账户
        /// </summary>
        BalanceMoney = 1,
        /// <summary>
        /// 抵扣手续费
        /// </summary>
        Fee = 1 << 1,
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
        String _Name;
        [MaxLength(50)]
        [Column("name")]
        public virtual String Name
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
        [DisallowNull]
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
        String _Commodity;
        /// <summary>
        /// 物品名称
        /// 如：BTCUSDT，物品名称就是BTC
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "物品名称 如：BTCUSDT，物品名称就是BTC")]
        [Column("commodity")]
        public virtual String Commodity
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
        String _TradeCoin;
        /// <summary>
        /// 交易币种
        /// 如：BTCUSDT，交易币种就是USDT
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "交易币种 如：BTCUSDT，交易币种就是USDT")]
        [Column("tradecoin")]
        public virtual String TradeCoin
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
    }
    /// <summary>
    /// 资金明细(废弃)
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = (MoneyHistory_TypeEnum)0)]
    [Table("moneyhistory")]
    [Way.EntityDB.DataItemJsonConverter]
    public class MoneyHistory : Way.EntityDB.DataItem
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
        System.Nullable<Decimal> _Amount;
        /// <summary>
        /// 金额
        /// </summary>
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
        System.Nullable<Decimal> _Balance;
        /// <summary>
        /// 当时余额
        /// </summary>
        [Display(Name = "当时余额")]
        [Column("balance")]
        public virtual System.Nullable<Decimal> Balance
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
        MoneyHistory_TypeEnum _Type;
        /// <summary>
        /// 明细类型
        /// </summary>
        [DisallowNull]
        [Display(Name = "明细类型")]
        [Column("type")]
        public virtual MoneyHistory_TypeEnum Type
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
        Int64 _MoneyAccountId;
        /// <summary>
        /// 资金账户Id
        /// </summary>
        [DisallowNull]
        [Display(Name = "资金账户Id")]
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
    }
    /// <summary>
    /// 与订单相关的记录
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = MoneyHistory_TypeEnum.OrderHistory)]
    public class OrderHistory : MoneyHistory
    {
        System.Nullable<Int64> _OrderId;
        /// <summary>
        /// 关联的订单id
        /// </summary>
        [Display(Name = "关联的订单id")]
        [Column("orderid")]
        public virtual System.Nullable<Int64> OrderId
        {
            get
            {
                return _OrderId;
            }
            set
            {
                if ((_OrderId != value))
                {
                    SendPropertyChanging("OrderId", _OrderId, value);
                    _OrderId = value;
                    SendPropertyChanged("OrderId");
                }
            }
        }
    }
    /// <summary>
    /// 扣除订单保证金
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = MoneyHistory_TypeEnum.AddOrderMarginHistory)]
    public class AddOrderMarginHistory : OrderHistory
    {
    }
    /// <summary>
    /// 归还订单保证金
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = MoneyHistory_TypeEnum.CancelOrderMarginHistory)]
    public class CancelOrderMarginHistory : OrderHistory
    {
    }
    /// <summary>
    /// 扣除手续费
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = MoneyHistory_TypeEnum.AddTradeFeeHistory)]
    public class AddTradeFeeHistory : OrderHistory
    {
    }
    /// <summary>
    /// 归还手续费
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = MoneyHistory_TypeEnum.CancelTradeFeeHistory)]
    public class CancelTradeFeeHistory : OrderHistory
    {
    }
    /// <summary>
    /// 订单盈亏
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = MoneyHistory_TypeEnum.OrderProfitHistory)]
    public class OrderProfitHistory : OrderHistory
    {
    }
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = MoneyHistory_TypeEnum.MoneyExpireHistory)]
    public class MoneyExpireHistory : MoneyHistory
    {
    }
    public enum MoneyHistory_TypeEnum : int
    {
        /// <summary>
        /// 与订单相关的记录
        /// </summary>
        OrderHistory = 1,
        /// <summary>
        /// 扣除订单保证金
        /// </summary>
        AddOrderMarginHistory = 101 + OrderHistory,
        /// <summary>
        /// 归还订单保证金
        /// </summary>
        CancelOrderMarginHistory = 201 + OrderHistory,
        /// <summary>
        /// 扣除手续费
        /// </summary>
        AddTradeFeeHistory = 301 + OrderHistory,
        /// <summary>
        /// 归还手续费
        /// </summary>
        CancelTradeFeeHistory = 401 + OrderHistory,
        /// <summary>
        /// 订单盈亏
        /// </summary>
        OrderProfitHistory = 501 + OrderHistory,
        MoneyExpireHistory = 601
    }
    /// <summary>
    /// 设置止盈止损的历史
    /// </summary>
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
        /// <summary>
        /// 止盈价格
        /// </summary>
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
        /// <summary>
        /// 止损价格
        /// </summary>
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
        /// <summary>
        /// 止盈金额
        /// </summary>
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
        /// <summary>
        /// 止损金额
        /// </summary>
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
        /// <summary>
        /// 设置止盈止损时的市价
        /// </summary>
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
        System.Nullable<double> _MovingStopProfit;
        /// <summary>
        /// 移动止盈
        /// </summary>
        [Display(Name = "移动止盈")]
        [Column("movingstopprofit")]
        public virtual System.Nullable<double> MovingStopProfit
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
    }
    /// <summary>
    /// 仓位
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = (Position_TypeEnum)0)]
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
        String _Symbol;
        /// <summary>
        /// 交易对
        /// 如：BTCUSDT
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "交易对  如：BTCUSDT")]
        [Column("symbol")]
        public virtual String Symbol
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
        String _Commodity;
        /// <summary>
        /// 物品名称
        /// 如：BTCUSDT，物品名称就是BTC
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "物品名称 如：BTCUSDT，物品名称就是BTC")]
        [Column("commodity")]
        public virtual String Commodity
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
        String _TradeCoin;
        /// <summary>
        /// 交易币种
        /// 如：BTCUSDT，交易币种就是USDT
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "交易币种 如：BTCUSDT，交易币种就是USDT")]
        [Column("tradecoin")]
        public virtual String TradeCoin
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
        double _TradeCoinAmount = 0;
        /// <summary>
        /// 交易币现存金额
        /// 负数表示是问平台借的
        /// </summary>
        [DisallowNull]
        [Display(Name = "交易币现存金额  负数表示是问平台借的")]
        [Column("tradecoinamount")]
        public virtual double TradeCoinAmount
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
        Position_TypeEnum _Type = (Position_TypeEnum)(0);
        [DisallowNull]
        [Column("type")]
        public virtual Position_TypeEnum Type
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
        System.Nullable<Int64> _UserId;
        /// <summary>
        /// 所属用户
        /// </summary>
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
        /// <summary>
        /// 用户结算账户id
        /// </summary>
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
        /// <summary>
        /// 创建时间
        /// </summary>
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
        /// <summary>
        /// 状态
        /// 当状态要变为closing时，所有挂单应该也要设为canceling
        /// </summary>
        [DisallowNull]
        [Display(Name = "状态  当状态要变为closing时，所有挂单应该也要设为canceling")]
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
        double _CommodityQuantity = 0;
        /// <summary>
        /// 现存物品数量
        /// 负数表示是问平台借的
        /// </summary>
        [DisallowNull]
        [Display(Name = "现存物品数量  负数表示是问平台借的")]
        [Column("commodityquantity")]
        public virtual double CommodityQuantity
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
        /// <summary>
        /// 方向
        /// </summary>
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
        System.Nullable<double> _Fee;
        /// <summary>
        /// 手续费
        /// </summary>
        [Display(Name = "手续费")]
        [Column("fee")]
        public virtual System.Nullable<double> Fee
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
        System.Nullable<double> _StopProfit;
        /// <summary>
        /// 止盈价格
        /// </summary>
        [Display(Name = "止盈价格")]
        [Column("stopprofit")]
        public virtual System.Nullable<double> StopProfit
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
        System.Nullable<double> _StopLoss;
        /// <summary>
        /// 止损价格
        /// </summary>
        [Display(Name = "止损价格")]
        [Column("stoploss")]
        public virtual System.Nullable<double> StopLoss
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
        System.Nullable<double> _StopProfitAmount;
        /// <summary>
        /// 止盈金额
        /// </summary>
        [Display(Name = "止盈金额")]
        [Column("stopprofitamount")]
        public virtual System.Nullable<double> StopProfitAmount
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
        System.Nullable<double> _StopLossAmount;
        /// <summary>
        /// 止损金额
        /// </summary>
        [Display(Name = "止损金额")]
        [Column("stoplossamount")]
        public virtual System.Nullable<double> StopLossAmount
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
        System.Nullable<double> _PriceOnSetPL;
        /// <summary>
        /// 设置止盈止损时的市价
        /// </summary>
        [Display(Name = "设置止盈止损时的市价")]
        [Column("priceonsetpl")]
        public virtual System.Nullable<double> PriceOnSetPL
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
        System.Nullable<double> _MovingStopProfit;
        /// <summary>
        /// 移动止盈
        /// 保存的是小数，表示百分比
        /// </summary>
        [Display(Name = "移动止盈  保存的是小数，表示百分比")]
        [Column("movingstopprofit")]
        public virtual System.Nullable<double> MovingStopProfit
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
        System.Nullable<double> _Profit = 0;
        /// <summary>
        /// 盈亏
        /// </summary>
        [Display(Name = "盈亏")]
        [Column("profit")]
        public virtual System.Nullable<double> Profit
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
        System.Nullable<double> _ClosePrice;
        /// <summary>
        /// 平仓价格
        /// </summary>
        [Display(Name = "平仓价格")]
        [Column("closeprice")]
        public virtual System.Nullable<double> ClosePrice
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
        /// <summary>
        /// 平仓时间
        /// </summary>
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
        System.Nullable<double> _OpenPrice;
        /// <summary>
        /// 开仓价格
        /// </summary>
        [Display(Name = "开仓价格")]
        [Column("openprice")]
        public virtual System.Nullable<double> OpenPrice
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
        double _PlanQuantity;
        /// <summary>
        /// 计划交易数量
        /// 也就是交易手数
        /// </summary>
        [DisallowNull]
        [Display(Name = "计划交易数量  也就是交易手数")]
        [Column("planquantity")]
        public virtual double PlanQuantity
        {
            get
            {
                return _PlanQuantity;
            }
            set
            {
                if ((_PlanQuantity != value))
                {
                    SendPropertyChanging("PlanQuantity", _PlanQuantity, value);
                    _PlanQuantity = value;
                    SendPropertyChanged("PlanQuantity");
                }
            }
        }
        System.Nullable<double> _MovingMaxPrice;
        /// <summary>
        /// 移动的历史最高价格
        /// 数据库不存此值
        /// </summary>
        [Display(Name = "移动的历史最高价格  数据库不存此值")]
        [Column("movingmaxprice")]
        public virtual System.Nullable<double> MovingMaxPrice
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
    }
    /// <summary>
    /// 合约交易
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = Position_TypeEnum.ContractPosition)]
    public class ContractPosition : Position
    {
        double _Margin = 0;
        /// <summary>
        /// 保证金
        /// </summary>
        [DisallowNull]
        [Display(Name = "保证金")]
        [Column("margin")]
        public virtual double Margin
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
        /// <summary>
        /// 杠杆
        /// </summary>
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
        System.Nullable<double> _AdditionalMargin;
        /// <summary>
        /// 追加的保证金
        /// </summary>
        [Display(Name = "追加的保证金")]
        [Column("additionalmargin")]
        public virtual System.Nullable<double> AdditionalMargin
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
    }
    public enum Position_TypeEnum : int
    {
        /// <summary>
        /// 合约交易
        /// </summary>
        ContractPosition = 1,
    }
    public enum Position_StatusEnum : int
    {
        /// <summary>
        /// 开仓中
        /// </summary>
        Open = 1,
        /// <summary>
        /// 平仓中
        /// </summary>
        Closing = 100,
        /// <summary>
        /// 挂反向单
        /// </summary>
        SendedClosingOrder = 150,
        /// <summary>
        /// 变更中，例如正在进行仓位合并
        /// </summary>
        Updating = 200,
        /// <summary>
        /// 已平仓
        /// </summary>
        Closed = 300
    }
    public enum Position_DirectionEnum : int
    {
        /// <summary>
        /// 买入
        /// </summary>
        Buy = 1,
        /// <summary>
        /// 卖出
        /// </summary>
        Sell = 2
    }
    /// <summary>
    /// 市场挂单
    /// </summary>
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
        /// <summary>
        /// 仓位id
        /// </summary>
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
        MarketOrder_DirectionEnum _Direction = (MarketOrder_DirectionEnum)(1);
        /// <summary>
        /// 方向
        /// </summary>
        [DisallowNull]
        [Display(Name = "方向")]
        [Column("direction")]
        public virtual MarketOrder_DirectionEnum Direction
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
        double _Quantity;
        /// <summary>
        /// 数量
        /// </summary>
        [DisallowNull]
        [Display(Name = "数量")]
        [Column("quantity")]
        public virtual double Quantity
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
        System.Nullable<double> _Price;
        /// <summary>
        /// 挂单单价
        /// null表示任意单价
        /// </summary>
        [Display(Name = "挂单单价 null表示任意单价")]
        [Column("price")]
        public virtual System.Nullable<double> Price
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
        double _DealQuantity = 0;
        /// <summary>
        /// 已成交数量
        /// </summary>
        [DisallowNull]
        [Display(Name = "已成交数量")]
        [Column("dealquantity")]
        public virtual double DealQuantity
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
        /// <summary>
        /// 状态
        /// </summary>
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
        /// <summary>
        /// 挂单时间
        /// </summary>
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
        String _Symbol;
        /// <summary>
        /// 交易对
        /// 如：BTCUSDT
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "交易对  如：BTCUSDT")]
        [Column("symbol")]
        public virtual String Symbol
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
        System.Nullable<DateTime> _CancelTime;
        /// <summary>
        /// 撤销时间
        /// </summary>
        [Display(Name = "撤销时间")]
        [Column("canceltime")]
        public virtual System.Nullable<DateTime> CancelTime
        {
            get
            {
                return _CancelTime;
            }
            set
            {
                if ((_CancelTime != value))
                {
                    SendPropertyChanging("CancelTime", _CancelTime, value);
                    _CancelTime = value;
                    SendPropertyChanged("CancelTime");
                }
            }
        }
        System.Nullable<double> _FixPrice;
        /// <summary>
        /// 固定成交价格
        /// </summary>
        [Display(Name = "固定成交价格")]
        [Column("fixprice")]
        public virtual System.Nullable<double> FixPrice
        {
            get
            {
                return _FixPrice;
            }
            set
            {
                if ((_FixPrice != value))
                {
                    SendPropertyChanging("FixPrice", _FixPrice, value);
                    _FixPrice = value;
                    SendPropertyChanged("FixPrice");
                }
            }
        }
        System.Nullable<DateTime> _FixTime;
        /// <summary>
        /// 固定成交时间
        /// </summary>
        [Display(Name = "固定成交时间")]
        [Column("fixtime")]
        public virtual System.Nullable<DateTime> FixTime
        {
            get
            {
                return _FixTime;
            }
            set
            {
                if ((_FixTime != value))
                {
                    SendPropertyChanging("FixTime", _FixTime, value);
                    _FixTime = value;
                    SendPropertyChanged("FixTime");
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
    }
    public enum MarketOrder_DirectionEnum : int
    {
        /// <summary>
        /// 买入
        /// </summary>
        Buy = 1,
        /// <summary>
        /// 卖出
        /// </summary>
        Sell = 2
    }
    public enum MarketOrder_StatusEnum : int
    {
        /// <summary>
        /// 挂单中
        /// </summary>
        Open = 1,
        /// <summary>
        /// 撤销中
        /// </summary>
        Canceling = 100,
        /// <summary>
        /// 已经撤销
        /// </summary>
        Canceled = 200,
        /// <summary>
        /// 关闭
        /// </summary>
        Closed = 300
    }
    /// <summary>
    /// 市场成交历史
    /// </summary>
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
        /// <summary>
        /// 挂单id
        /// </summary>
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
        System.Nullable<double> _Price;
        /// <summary>
        /// 成交价格
        /// </summary>
        [Display(Name = "成交价格")]
        [Column("price")]
        public virtual System.Nullable<double> Price
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
        /// <summary>
        /// 对方挂单id
        /// </summary>
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
        /// <summary>
        /// 成交时间
        /// </summary>
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
        System.Nullable<double> _Quantity;
        /// <summary>
        /// 成交数量
        /// </summary>
        [Display(Name = "成交数量")]
        [Column("quantity")]
        public virtual System.Nullable<double> Quantity
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
        /// <summary>
        /// 仓位id
        /// </summary>
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
        MarketDealHistory_DirectionEnum _Direction = (MarketDealHistory_DirectionEnum)(1);
        /// <summary>
        /// 方向
        /// </summary>
        [DisallowNull]
        [Display(Name = "方向")]
        [Column("direction")]
        public virtual MarketDealHistory_DirectionEnum Direction
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
    }
    public enum MarketDealHistory_DirectionEnum : int
    {
        /// <summary>
        /// 买入
        /// </summary>
        Buy = 1,
        /// <summary>
        /// 卖出
        /// </summary>
        Sell = 2
    }
    /// <summary>
    /// 资金明细
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = (MoneyDetail_TypeEnum)0)]
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
        /// <summary>
        /// 资金账户id
        /// </summary>
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
        double _Amont;
        /// <summary>
        /// 金额
        /// </summary>
        [DisallowNull]
        [Display(Name = "金额")]
        [Column("amont")]
        public virtual double Amont
        {
            get
            {
                return _Amont;
            }
            set
            {
                if ((_Amont != value))
                {
                    SendPropertyChanging("Amont", _Amont, value);
                    _Amont = value;
                    SendPropertyChanged("Amont");
                }
            }
        }
        double _Balance;
        /// <summary>
        /// 变化后余额
        /// </summary>
        [DisallowNull]
        [Display(Name = "变化后余额")]
        [Column("balance")]
        public virtual double Balance
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
    }
    /// <summary>
    /// 与仓位相关的明细
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = MoneyDetail_TypeEnum.PositionMoneyDetail)]
    public class PositionMoneyDetail : MoneyDetail
    {
        System.Nullable<Int64> _PositionId;
        /// <summary>
        /// 关联的仓位id
        /// </summary>
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
    }
    /// <summary>
    /// 扣除订单保证金
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = MoneyDetail_TypeEnum.AddPositionMarginDetail)]
    public class AddPositionMarginDetail : PositionMoneyDetail
    {
    }
    /// <summary>
    /// 归还订单保证金
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = MoneyDetail_TypeEnum.CancelPositionMarginDetail)]
    public class CancelPositionMarginDetail : PositionMoneyDetail
    {
    }
    /// <summary>
    /// 扣除手续费
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = MoneyDetail_TypeEnum.AddTradeFeeDetail)]
    public class AddTradeFeeDetail : PositionMoneyDetail
    {
    }
    /// <summary>
    /// 归还手续费
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = MoneyDetail_TypeEnum.CancelTradeFeeDetail)]
    public class CancelTradeFeeDetail : PositionMoneyDetail
    {
    }
    /// <summary>
    /// 订单盈亏
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = MoneyDetail_TypeEnum.PositionProfitDetail)]
    public class PositionProfitDetail : PositionMoneyDetail
    {
    }
    /// <summary>
    /// 资金过期被回收
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = MoneyDetail_TypeEnum.MoneyExpireDetail)]
    public class MoneyExpireDetail : MoneyDetail
    {
    }
    /// <summary>
    /// 隔夜息
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = MoneyDetail_TypeEnum.DailyInterestDetail)]
    public class DailyInterestDetail : PositionMoneyDetail
    {
    }
    public enum MoneyDetail_TypeEnum : int
    {
        /// <summary>
        /// 与仓位相关的明细
        /// </summary>
        PositionMoneyDetail = 1,
        /// <summary>
        /// 扣除订单保证金
        /// </summary>
        AddPositionMarginDetail = 101 + PositionMoneyDetail,
        /// <summary>
        /// 归还订单保证金
        /// </summary>
        CancelPositionMarginDetail = 201 + PositionMoneyDetail,
        /// <summary>
        /// 扣除手续费
        /// </summary>
        AddTradeFeeDetail = 301 + PositionMoneyDetail,
        /// <summary>
        /// 归还手续费
        /// </summary>
        CancelTradeFeeDetail = 401 + PositionMoneyDetail,
        /// <summary>
        /// 订单盈亏
        /// </summary>
        PositionProfitDetail = 501 + PositionMoneyDetail,
        /// <summary>
        /// 资金过期被回收
        /// </summary>
        MoneyExpireDetail = 601,
        /// <summary>
        /// 隔夜息
        /// </summary>
        DailyInterestDetail = 701 + PositionMoneyDetail,
    }
    /// <summary>
    /// 用户配置信息
    /// </summary>
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
        /// <summary>
        /// 是否合并仓位
        /// </summary>
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
            var db = sender as TradeSystem.DBModels.DB.TradeSystemDB;
            if (db == null) return;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoneyAccount>().HasKey(m => m.Id);
            modelBuilder.Entity<SymbolInfo>().HasKey(m => m.id);
            modelBuilder.Entity<MoneyHistory>().HasKey(m => m.id);
            modelBuilder.Entity<MoneyHistory>().HasDiscriminator<MoneyHistory_TypeEnum>("Type")
            .HasValue<MoneyHistory>((MoneyHistory_TypeEnum)0)
            .HasValue<OrderHistory>(MoneyHistory_TypeEnum.OrderHistory)
            .HasValue<AddOrderMarginHistory>(MoneyHistory_TypeEnum.AddOrderMarginHistory)
            .HasValue<CancelOrderMarginHistory>(MoneyHistory_TypeEnum.CancelOrderMarginHistory)
            .HasValue<AddTradeFeeHistory>(MoneyHistory_TypeEnum.AddTradeFeeHistory)
            .HasValue<CancelTradeFeeHistory>(MoneyHistory_TypeEnum.CancelTradeFeeHistory)
            .HasValue<OrderProfitHistory>(MoneyHistory_TypeEnum.OrderProfitHistory)
            .HasValue<MoneyExpireHistory>(MoneyHistory_TypeEnum.MoneyExpireHistory)
            ;
            modelBuilder.Entity<SetStopPLHistory>().HasKey(m => m.id);
            modelBuilder.Entity<Position>().HasKey(m => m.id);
            modelBuilder.Entity<Position>().HasDiscriminator<Position_TypeEnum>("Type")
            .HasValue<Position>((Position_TypeEnum)0)
            .HasValue<ContractPosition>(Position_TypeEnum.ContractPosition)
            ;
            modelBuilder.Entity<MarketOrder>().HasKey(m => m.id);
            modelBuilder.Entity<MarketDealHistory>().HasKey(m => m.id);
            modelBuilder.Entity<MoneyDetail>().HasKey(m => m.id);
            modelBuilder.Entity<MoneyDetail>().HasDiscriminator<MoneyDetail_TypeEnum>("Type")
            .HasValue<MoneyDetail>((MoneyDetail_TypeEnum)0)
            .HasValue<PositionMoneyDetail>(MoneyDetail_TypeEnum.PositionMoneyDetail)
            .HasValue<AddPositionMarginDetail>(MoneyDetail_TypeEnum.AddPositionMarginDetail)
            .HasValue<CancelPositionMarginDetail>(MoneyDetail_TypeEnum.CancelPositionMarginDetail)
            .HasValue<AddTradeFeeDetail>(MoneyDetail_TypeEnum.AddTradeFeeDetail)
            .HasValue<CancelTradeFeeDetail>(MoneyDetail_TypeEnum.CancelTradeFeeDetail)
            .HasValue<PositionProfitDetail>(MoneyDetail_TypeEnum.PositionProfitDetail)
            .HasValue<MoneyExpireDetail>(MoneyDetail_TypeEnum.MoneyExpireDetail)
            .HasValue<DailyInterestDetail>(MoneyDetail_TypeEnum.DailyInterestDetail)
            ;
            modelBuilder.Entity<UserSetting>().HasKey(m => m.UserId);
        }
        System.Linq.IQueryable<MoneyAccount> _MoneyAccount;
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
        System.Linq.IQueryable<MoneyHistory> _MoneyHistory;
        public virtual System.Linq.IQueryable<MoneyHistory> MoneyHistory
        {
            get
            {
                if (_MoneyHistory == null)
                {
                    _MoneyHistory = this.Set<MoneyHistory>();
                }
                return _MoneyHistory;
            }
        }
        System.Linq.IQueryable<OrderHistory> _OrderHistory;
        public virtual System.Linq.IQueryable<OrderHistory> OrderHistory
        {
            get
            {
                if (_OrderHistory == null)
                {
                    _OrderHistory = this.Set<OrderHistory>();
                }
                return _OrderHistory;
            }
        }
        System.Linq.IQueryable<AddOrderMarginHistory> _AddOrderMarginHistory;
        public virtual System.Linq.IQueryable<AddOrderMarginHistory> AddOrderMarginHistory
        {
            get
            {
                if (_AddOrderMarginHistory == null)
                {
                    _AddOrderMarginHistory = this.Set<AddOrderMarginHistory>();
                }
                return _AddOrderMarginHistory;
            }
        }
        System.Linq.IQueryable<CancelOrderMarginHistory> _CancelOrderMarginHistory;
        public virtual System.Linq.IQueryable<CancelOrderMarginHistory> CancelOrderMarginHistory
        {
            get
            {
                if (_CancelOrderMarginHistory == null)
                {
                    _CancelOrderMarginHistory = this.Set<CancelOrderMarginHistory>();
                }
                return _CancelOrderMarginHistory;
            }
        }
        System.Linq.IQueryable<AddTradeFeeHistory> _AddTradeFeeHistory;
        public virtual System.Linq.IQueryable<AddTradeFeeHistory> AddTradeFeeHistory
        {
            get
            {
                if (_AddTradeFeeHistory == null)
                {
                    _AddTradeFeeHistory = this.Set<AddTradeFeeHistory>();
                }
                return _AddTradeFeeHistory;
            }
        }
        System.Linq.IQueryable<CancelTradeFeeHistory> _CancelTradeFeeHistory;
        public virtual System.Linq.IQueryable<CancelTradeFeeHistory> CancelTradeFeeHistory
        {
            get
            {
                if (_CancelTradeFeeHistory == null)
                {
                    _CancelTradeFeeHistory = this.Set<CancelTradeFeeHistory>();
                }
                return _CancelTradeFeeHistory;
            }
        }
        System.Linq.IQueryable<OrderProfitHistory> _OrderProfitHistory;
        public virtual System.Linq.IQueryable<OrderProfitHistory> OrderProfitHistory
        {
            get
            {
                if (_OrderProfitHistory == null)
                {
                    _OrderProfitHistory = this.Set<OrderProfitHistory>();
                }
                return _OrderProfitHistory;
            }
        }
        System.Linq.IQueryable<MoneyExpireHistory> _MoneyExpireHistory;
        public virtual System.Linq.IQueryable<MoneyExpireHistory> MoneyExpireHistory
        {
            get
            {
                if (_MoneyExpireHistory == null)
                {
                    _MoneyExpireHistory = this.Set<MoneyExpireHistory>();
                }
                return _MoneyExpireHistory;
            }
        }
        System.Linq.IQueryable<SetStopPLHistory> _SetStopPLHistory;
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
        System.Linq.IQueryable<ContractPosition> _ContractPosition;
        public virtual System.Linq.IQueryable<ContractPosition> ContractPosition
        {
            get
            {
                if (_ContractPosition == null)
                {
                    _ContractPosition = this.Set<ContractPosition>();
                }
                return _ContractPosition;
            }
        }
        System.Linq.IQueryable<MarketOrder> _MarketOrder;
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
        System.Linq.IQueryable<PositionMoneyDetail> _PositionMoneyDetail;
        public virtual System.Linq.IQueryable<PositionMoneyDetail> PositionMoneyDetail
        {
            get
            {
                if (_PositionMoneyDetail == null)
                {
                    _PositionMoneyDetail = this.Set<PositionMoneyDetail>();
                }
                return _PositionMoneyDetail;
            }
        }
        System.Linq.IQueryable<AddPositionMarginDetail> _AddPositionMarginDetail;
        public virtual System.Linq.IQueryable<AddPositionMarginDetail> AddPositionMarginDetail
        {
            get
            {
                if (_AddPositionMarginDetail == null)
                {
                    _AddPositionMarginDetail = this.Set<AddPositionMarginDetail>();
                }
                return _AddPositionMarginDetail;
            }
        }
        System.Linq.IQueryable<CancelPositionMarginDetail> _CancelPositionMarginDetail;
        public virtual System.Linq.IQueryable<CancelPositionMarginDetail> CancelPositionMarginDetail
        {
            get
            {
                if (_CancelPositionMarginDetail == null)
                {
                    _CancelPositionMarginDetail = this.Set<CancelPositionMarginDetail>();
                }
                return _CancelPositionMarginDetail;
            }
        }
        System.Linq.IQueryable<AddTradeFeeDetail> _AddTradeFeeDetail;
        public virtual System.Linq.IQueryable<AddTradeFeeDetail> AddTradeFeeDetail
        {
            get
            {
                if (_AddTradeFeeDetail == null)
                {
                    _AddTradeFeeDetail = this.Set<AddTradeFeeDetail>();
                }
                return _AddTradeFeeDetail;
            }
        }
        System.Linq.IQueryable<CancelTradeFeeDetail> _CancelTradeFeeDetail;
        public virtual System.Linq.IQueryable<CancelTradeFeeDetail> CancelTradeFeeDetail
        {
            get
            {
                if (_CancelTradeFeeDetail == null)
                {
                    _CancelTradeFeeDetail = this.Set<CancelTradeFeeDetail>();
                }
                return _CancelTradeFeeDetail;
            }
        }
        System.Linq.IQueryable<PositionProfitDetail> _PositionProfitDetail;
        public virtual System.Linq.IQueryable<PositionProfitDetail> PositionProfitDetail
        {
            get
            {
                if (_PositionProfitDetail == null)
                {
                    _PositionProfitDetail = this.Set<PositionProfitDetail>();
                }
                return _PositionProfitDetail;
            }
        }
        System.Linq.IQueryable<MoneyExpireDetail> _MoneyExpireDetail;
        public virtual System.Linq.IQueryable<MoneyExpireDetail> MoneyExpireDetail
        {
            get
            {
                if (_MoneyExpireDetail == null)
                {
                    _MoneyExpireDetail = this.Set<MoneyExpireDetail>();
                }
                return _MoneyExpireDetail;
            }
        }
        System.Linq.IQueryable<DailyInterestDetail> _DailyInterestDetail;
        public virtual System.Linq.IQueryable<DailyInterestDetail> DailyInterestDetail
        {
            get
            {
                if (_DailyInterestDetail == null)
                {
                    _DailyInterestDetail = this.Set<DailyInterestDetail>();
                }
                return _DailyInterestDetail;
            }
        }
        System.Linq.IQueryable<UserSetting> _UserSetting;
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
        protected override string GetDesignString()
        {
            var result = new StringBuilder();
            result.Append("\r\n");
            result.Append("H4sIAAAAAAAACu1dW3PTWLb+K5RfDz1tS7Zsdw0PQJqa1IGGMzCXqjFFKbYSVO3IGVtphumiKukmkAC5DJcmzaWBngzN1AyBBhpCQoY/E8nO0/yFsy+yJUvbdrytLe/t+AVia0uWPq219rqvr2Nn1LGiVol99qev8Z9fqJNa7LPY6T8XT2vlr7Ry7GDst6ULeMGoqU3i");
            result.Append("v5xVegEc/r1anAYfEunEpYONI+bFKc09Fjta1lRTQz9wOG/qJSPmWZsvGaZmmJ7lX+fwzeRin4E/9QL4X0oczMXy6hQ8GXw0potF8AW8AviUi50sF7RyLga+GlFNdUytaKMj4EAafKEfL+W/BH/HL4EPR0vF6UmjAj7+qXFlpf2l4SLwebRyeNosjRr5sjYJ7hYcMsvT");
            result.Append("GrykanwBTgFfjKvFCvymMHYGPD46eUyf0OFi8K3znXP5z43pyRFtXDc83xU1Y8I8734uaOPqdNFEqLjfImTQ0yFMRiun/hd9cm6nBJFADxZHR0f0Sr6sT+qGapbKnps8WlQrFech4ZXBC2kAIjUBkotZP922F1arP29ZP1xHz9IAZ0QrahPw1aKHI8LU+MEGTs6demDa");
            result.Append("A0a52Kef2nde7l5dxneTyxn/N60apm5ePHDoAEAiZ3z66c7WO/vxh8aCU2U9r4GjErp4A95cDH3uDt76Y7j4ZqjxlX34Vq+9tWdmm5E9barmdIU9phisnY3nAK/623QBta5esdau2vMrO5tr4Ivfamrh4pnSySnNgLA6a969sj7M7GzdAp+cI4l4/fz3r8EBfPWjxVJF");
            result.Append("NybgieDwAXR899tn1vwVvAq+MLVs6moRrtQKYKHcuM7cM7i0vu5w0V2TbKx598q+ubZ7ewb+lmrkwcPA4ynPcWtl3nr/Fnw6oZUn4GOm4vWHQE9grf1ce/MP5zmOqTq+ghJP7IWAcjG8bO8UlKWmoKSfQ5ce4RdpLd6xlt+RmRQJydFCDzRFJ85C4rdEkhqulA+u2vqP");
            result.Append("ACh74TqQKM1YHS+Ze2c6gsAPDZ5cLN4lNSUUanwUPzkBVkG8RoIIc57IQKWpgUoTd0b77tvdu29a7Iw6/IKa4wrgEmb9En3gOfpNLkOECu/QZKjQdt0LVlpen1SL3UMFnjITDlz0Ej3rhwttSACuZqzgxiQ+ThK1bpqO+3CyH8xYK0s7GzP2v5842oYfMySwBgA0iRq0");
            result.Append("hF/hvP3Mnn/nWDYNnH5XEVk9oBbqab+5g7VIklDHaqXgIl2mBspvt+x8fFh7Mbt79W/NKJ1Qy4AOelEOQmc2CiUhQQ1TQDnfmK3+tOgz7y5OjpWKPVDRV2o5f14tdw9RKh4OGVHD41fG7YeP7YdXfGq4BnxP6oTGhYbZNTb0HOZXxO3nP1bvz5OUptNmaepUuTSum2Jv");
            result.Append("atRGXTodxMpefNQKq+OlSkVspFLUSGU66kzEzQ6qTWLvdRI1J2bSRE4Ee93ujw9bceLhydK0I3SEpTJqJ0ImQ+THVohBfhwEvKjVzozf4rNntqCs31xuBqt/Ql4KTZmSqO3irN90sb97b634FM4jla7iD3S6AnKWz96z1u4B//CRaU/UAXxb/ecm+PO0ViwyCzdQb5TZ");
            result.Append("QDhn+5a1sFi9d9na+CZgLAPl/UvNFN9appZjWb+VU1v/T3V7Hct/LNPAbtkCPYTbSUAI5qnjQsMHeJ8WvyTRk0XSMKAzS3AFg9oMymYJStn8Q8ehTHC7nyxD74paFNnxTqvCynGS22/+4c7GdRjHqbOj9WAz6DB1YBsUuUYPoX8n9YY8Yfzw/uMgd8IYKAj6VtA51Kjt");
            result.Append("cXetvZ2zrm5iAw64luxra7Ub31r339Qj+UATMrW8CTdeJjtsgtYrLaMzm/aMXy4Dxbf25ikIxvrdrCdKhnbxcB6qvl14W0NLLglL8tFihdzYTV7EzTV7FUYaq1vPa2/eN4N1TNNEdyR2idRZuHzkj0dLxrg+Uc9UGq38ztD/jO6gfuJo5WhxumJqZa3gvRxKcILXQ6e6");
            result.Append("Tv2zgXSpc/VD6OXQ/4KPngk/5FvhPCJYEL/kTQQrOKlb3rSyNFgMM9BgOoyGkrg65qFJTPPQpLbJYshngh76N3oFvOiLlDlp6ayoOWkSm5y0TLxz/h/HsSuJkTM9E9jUxQ6BdsCJOgKaCdjALdNbutKvqXQdRkRE7+v0W7xi+4QlVn4Uv5InrLdOCtXECFlhaYjys203");
            result.Append("1nONdYzVCZmpOiETMzMc8+HJM6LlgBQpSsUik2y7j3aZAhK5diGz2kf9QWlr+QV4FTvb3weCF0fUIswC6YtV0gWPy6x20kCI+sYs1DjIqSCISfuYDxIeYLS7qxzP7iEd5GipC4BCTAZJEJNBgBw4PXKmS4CoHVMo06a1SBqN3tbZq6YhM7F1ZJSh1cRiyyvVpZfWla3q");
            result.Append("1i1SPPUPunm+UFYvHCuX/qqJzmi0+pmMUkmCksl+9sT64NfSShUdLosobLg7s+LeCAgT6sZEUTtRKngLbeaeeZccA1dyFkiUtSdyqPGxTkqeQ/1dOqUO1iXf2VZqjuOkOoeXMVb1kkxVvfaaF04IHDXGS5SaXTYlqssoycZllFXaAoL/4Df/MslI1c2m29NhqdyL+coy");
            result.Append("uphkpNJmM20R+dzAnEy/TYzpoYGS6BIUuVcxz1bipphK3EDZHzKr7dWl6pYv3xhtOT257GVYSSqm/E2xUWPjMpeuhr0q9yk28leO+9VUkkbfv8zIVop81wDtXfLAN+IFKOCL2Ya+6DB8MTziRG3vxAOeGCTYSG0aIrJzdjaWcGV19f6GNfcaJOXU1l9a23dgQb3Hdeta");
            result.Append("PfbC33e/X8PnuB6knHG4UPB4jjynxRMH/ueA91qO9bR9s/ZxNXgdXK9FvJTU4lL4ltxsBHQzZ8pqQQOpCO7pcts78Z7u1IwFrpBscYU6gii24ACHowruuanAuXQJMSkaG7G+YbWhzPYqprOzdxNFIrkqGIeRUqzcXVKrXCHg+Hc2RKIpLPBmSZ8sFOhMMPe6NnsbSRbI");
            result.Append("Jv7cKt7zADrgtPdiAudRG9wfSdqQV0sOOXuInD7U9IP+lYydQQpT00TpmGQOU1qXrljLr5opHKSWo4Kj471ZK6iZipDWisLI6Z4auNJLhZEFgxrNDFLlpcJo90oMahGhwihWmhjMGkKFkQWI2s0MfqmSwkgTR11oRDdTFDZlcPJeGm7ybJF0wCXNYY6bX7WLLMUt3UbV");
            result.Append("Pa8aE12puieLhUYnWXLW2hfahQ4rDO0CQWlFnRyb5d3Hq/aDR6RE1c//MqWXec9UlUOn0Tx6XQUPePDbglbUzMC3UYXzwe+cm4RvWHVC+nmw5tw0OEcvnGNO2xm+aXv40kgvLcv0pbnNogNvyz20l9fUU/3f/ioABF8dUfNfTk/h91cAts6UVjZ1JEIAjs7Sr92CXTfm");
            result.Append("7S8fu3SJTqiF4QKDwgxh4BFgzBxgHtWDkXjMoDVD8SjUnpZJsN/TvN5F8p7mXcFuTwuLZ9HLOu/o1RGwLuFXvfQSBZVIomj1gYAYCoXhOPZ/P9ywV19YK0/Bv7VfHgcKDJzrdxXgpm5QUH3wyFr/Ad8e7oZfRA/pBrTxLTZW/F4vm9OeRfE4N8m8Q0vBzy8y3/zC0Us7");
            result.Append("6OO7aFWWdlnaw/e0x/dEUGGc/yGk0aozTJNAU34fOSxW214k14PQBVMV1CxLxGAqAif8YKqC+v11qnXgOPm+GZfwQqeKHKghX/indWvWWlms/vQSKAzW02/+++HekTNHYQEe0H28h62XPwMlCBxrpt2jpcnJUkE3LwqMJ22EVZEDne0RYHhQVAuc6mOjGCdE0pY5dMKK");
            result.Append("NriqyOTGS045apD2vIcx7TWqQt3EUpjW2K/i1ZCIjzb8qsiBfvgNxJZeWs9XSWHrBmBdxq1JkcXSdL0upQ9kSBt1VeT2mUcRpSrb36/vztzDLwzQ/helMghi19UAYKrFnXTelfnq5tPGMqBQm2U1b3oWQgHEAl/K3FtFTkU2pKGv9NdNqqT/tXnLDRRZEW0eQdA/0Ams");
            result.Append("TG9gRVwVllGYWnhNyn7ACm86GqYFzhayNJ+QuVwWSIVbmLF+/gG3u2nmNc4Lw0K2DXgmqoxIfBgBqUVcLRAupXWKwLqPSQjCoot2EXhlS5dZlg4jxW+i48bS9o1vrMU7AR3qS83sYfa0IovqOUIoMfAcyWR3nT+Hoi4d+GXeZoBCdCEl43uYiDECkt8Q+UdgSu28f2nN");
            result.Append("/SMwFGPxO+tqt0MxgpptJxipPUfJwGARgs+IK1dR1yRG7SlKBiYiIuEHi2tBFnnOgMtrT55V1zZ3trbsy8vOgWB6eS+5lyFal10jR+0SSgZaqb57haenk8hrBASQuSKxoBneCSlqN1DS3waheu2tPTPrL/JQzelKFO4gROB4Tn19hr07NX7u9e7d+gR7rZc4fic0e27K");
            result.Append("xVTvysaZ6l0Jst6FuIdUCYm1L8hDPZVCKklRG2chxBjoYMlgi0/IHYE8Vlf75VgNYzTGVEkGrExEqKQiSL63QkZzPpRkYAD8i/dAVSXT0hngjRaeluj1rcAsKKwyEOpaoLjju6qlE0rUulUqYPi0VKy6Vqqi7OPfCaAU30oA4+Rj4qYezJUjLhPGXZ+VOHfXpwJ5Fch7");
            result.Append("Wt26VV2/i9NbQypr4cynSjGsiVcak3mnscBMlvn71tYmcexI96XYke974cVguacsxhm4Pq96C+HfqlTPpS95X+baMfOYppL7M38sZGchz4ydEoOxA62akE0p6sbRib7Sg0NfnCb2uJSl8OSUxgN6iU5pPOVvo+6U1o0J7JZu+KyrW8t4Ud1tXUA9Tyn91p10m+zgkCin");
            result.Append("iVQ8QyZomlDAxEVJ200FBDnjQO3NI/ABRzuBCrN7dx3wlbX80pqBHRpbqIc8+Z9Yp3YPTl5RNis+7w/JO+zKhWiJEGzRvOtIaY7kJndlCRTExee2LsUTYhiD/jBj9TYgvHnHGASEaL96Ys2/9H4LfRMbi7Vvt2sf74PMNfvmurUyv7v6yJq/20yX4P71gtjG48B4HaW4");
            result.Append("xDM9Uu3RQw8lkWaHHsr+eyijZGxOA1U8y8LkIEIWduevRvxCzOZ8Upxrd3wf37W3+iPaN6KIoRMHimhwKO4FnEd1oClO53Nt896YJeTgJn80jH7j3CR6z7jP6JSzkH0nJik+kM5vNg0l668lumaSjV+MtpGkFGfcQnvQtqH+svAA+LCHLBw2VSR4dyor+6qoNeSOVEPe");
            result.Append("6uf2mODaTz7cHptfFqeJ90MW7isLD6QHcmj39EgVQyfr0BoOUoWATtnom54I1L2jU/5a46mIs4mQORHVRCIeHQ4JRex60D6VFwtU0x8tg7Al1rTIwrsvvYXYptOFm740FOW9cUdGFFHe4Il0ZlDaBYbbp4bj3JUEpxEKl6YC3VgWrle3ntfevG8mqmOaxqsCwFWW8tAP");
            result.Append("0RvDSHEhkk4yAf355tru7RliVa5q5DXe2xjxlFg9cFajlBCDpgM1gmgopa+HQSSzJ2FAEkQfF+/gmKQTgHTikvbzH6v35/HB02ZpCpho47p5SGoctRcfuUePlyqVQzI+Vn29Vd16ZH3YhNU5eMXFCioYPpSkC252bHzcZV/CIec0cw7nY13FGFPJz/hDSZIF9kmELyB7");
            result.Append("S9nYjxKypwJsyGL72X0tcRrkq3NYOjCeqvZx27r2uHrvcovBXocLBXRRtdjliC/ObNREsvehVUOjtV8pFRLjMCl8mvZuRP8K/lMr0Cs6j285urTypl+NmkoUph3l/Y682i+Xgbi0V5eqW775fgilEc1U9SJdF/m0b+qUQF3k2cyATncYbcT5lLdmVMLrG5+WyUQZak9d");
            result.Append("3sCiLehOB2YdkUbqHgZCi5tBul1jQ9s0Ph3I5bGWV60b31krSzvb3wdAOqIWodNVWJhou8anfbOM/GJIiAaRjEYxp30ZBX5sorLbN5Zw1LZ6f8Oaew0sG2eHzhl1dd6zPUOTntLsZjNxOR2YZAQeojZ7G1poogajQ5y9THiFkU8UliTGeT8+7ZFsjHgWCNNoQmKcEjK4");
            result.Append("wLHNchhc4GSuo93DYFTzy0qwtN/TceIQkt25xer2+s7HJ/bsi+adFdqUpzXT1I0JSiu+wyy4fhmtezTlEWAMTPngQLjVF9bKU2tl3nr/Fis5zS8C3EN929fKEz0pkWM6sz64zXCFknMXpaAcvH54+zya6Kz4acu69gw3b8crTpS+AhINXukQFE+8JGUMeGRQ5rQo1JXK");
            result.Append("gQmUiCpJEyhdGhU2GqhQb17+FHHMnq1ggswqLEhpapCyRFoiuVtdWgKO13oOkIhgZWjBClaKIYpqBRakKMGhylJD5d/Ra+v/Aco7pi4MG0gQBq4ya+Mb8gj5kwbQ6E8dFxU6SZxsx2GrM0nmPRsoMDMRa4uYn/yxw7rWKPbOP+QgkZoFymzzfoatVMSkCrZNAnDb2lFj");
            result.Append("vESWrL7jrWTr/pwXKiUZRd7367zQToAOzogYOS1iLUqdQKWMf46otfwChBgESJ8J+rIlOcqWH43bI7b8kDIHMzBvtVNrkIKW1ydVmA5wyRUbUiYQbLgxC2MM5Gx35H/uMtGdu5cji/Jy5IR/j7SXV+D8ryvAq32L5AD5g26eL5TVC8fKpb9qAr+iJOevaFhg13qXiiCH");
            result.Append("ZVgxGWHFpJwdnBdal6ypuN+zgzPCSRHQpqJWajV4r3FQpA3hm4GhUKwIIQQ88dBrv9gLf3f7pOSMY5oGD//619ABTBOx7CSQu4wHRawZJ+N8k6hrpKWYkl0P8XdO6W5wBiomE4O8L7KFrk3uzQi6ua7z3potGsxgzG5fZnf7KJcFPYO3+JLdo3AfsfGbszBFY9PXrLHL");
            result.Append("CE3k1lHY2RnDKGc/vfFJoWI0TDmJbayTOSP11PoDXTSq3h/D5AIpqfC+VfkTCnESKilTDu3yKD9n/+XKBSbOY5iIjQ4hTHwXl4adJjfc2vu6tXM6wdNlHn+iqfVhpoWMOTmlGftKxAxZp6+sI1z38/T+mprYqUV6pNzGlhQZBziGNbAhvqwU1wXLrqwItIu5v2mt38MD");
            result.Append("REjb7zH9L1zvvmGXtLkKviK1gYqk5gOo+FbyO2IV6XiJgSsVTAnRzTytyEJ3ZAu/WntIwi4JczoY1yVefzZxbf2JNX8TJw07A7ByxoGd949w7rBzYOE6ONYsrY+XzAq3fdCGXXEH2cZMyUJGXsLjvVMgi4O3qXRR82CnoI1zKjHlE8uuYdAmOo7lNL9g6CvsqxznNILu");
            result.Append("SuwUsU4YFtovXbGWX9kPZnb/teqY/UB0A0FtL65bm7d2Nhat56v28zVr5gOpnviEyrdXoGM18TBTRSR9iXHdqGai+vjjbV3yxFWt+E4ZuPp8h2PCSsT2uNnad8znv01uJ2QYRs4bBVlSoi2IyCfEsVOnE4QUMZ0I4zQpTqPtQ+WQ4aYFPjpQxyBduJtpnTpg11HIKeAz");
            result.Append("6O0HCOhXo4apJNEPNa12KCa4/rRZBltD8ASXbPZ+TtMztby1s/Cx4FGw2TknZsckJZ4dj3+SkdOJT5IpdfyTTCqf+kRLF+JKJj+WGEuPxS79PwdNv7JCSAEA");
            return result.ToString();
        }
    }
}

/*<design>
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
<design>*/

