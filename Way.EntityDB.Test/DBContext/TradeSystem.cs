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
        Decimal _Balance = 0m;
        /// <summary>
        /// 可用余额
        /// </summary>
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
        Decimal _WithdrawFrozen = 0m;
        /// <summary>
        /// 提现冻结金额
        /// </summary>
        [DisallowNull]
        [Display(Name = "提现冻结金额")]
        [Column("withdrawfrozen")]
        public virtual Decimal WithdrawFrozen
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
        /// 抵扣手续费
        /// </summary>
        Fee = 1,
        /// <summary>
        /// 交易账户
        /// </summary>
        Trade = 1 << 10 | Fee,
        /// <summary>
        /// 可提现账户
        /// </summary>
        Withdraw = 1 << 11,
        /// <summary>
        /// 交易+可提现
        /// </summary>
        TradeWithdraw = Trade | Withdraw,
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
        /// </summary>
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
        Position_CloseTypeEnum? _CloseType = (Position_CloseTypeEnum?)(1);
        /// <summary>
        /// 平仓类型
        /// </summary>
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
        /// <summary>
        /// 仓位是否已被锁
        /// </summary>
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
    }
    /// <summary>
    /// 合约交易
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = Position_TypeEnum.ContractPosition)]
    public class ContractPosition : Position
    {
        Decimal _Margin = 0m;
        /// <summary>
        /// 保证金
        /// </summary>
        [DisallowNull]
        [Display(Name = "保证金")]
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
        System.Nullable<Decimal> _AdditionalMargin;
        /// <summary>
        /// 追加的保证金
        /// </summary>
        [Display(Name = "追加的保证金")]
        [Column("additionalmargin")]
        public virtual System.Nullable<Decimal> AdditionalMargin
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
        Sell = ~Buy
    }
    public enum Position_CloseTypeEnum : int
    {
        /// <summary>
        /// 手动平仓
        /// </summary>
        Manual = 1,
        /// <summary>
        /// 系统强平
        /// </summary>
        SysClose = 2,
        /// <summary>
        /// 止盈止损
        /// </summary>
        StopProfitLoss = 3,
        /// <summary>
        /// 移动止盈
        /// </summary>
        MovingStop = 4
    }
    /// <summary>
    /// 市场挂单
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = (MarketOrder_TypeEnum)0)]
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
        /// 成交后指向的仓位，
        /// 开仓单这个id应该是null，只有部分成交后，才有值
        /// 平仓单这个肯定有值
        /// </summary>
        [Display(Name = "仓位id  成交后指向的仓位， 开仓单这个id应该是null，只有部分成交后，才有值 平仓单这个肯定有值")]
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
        Decimal _Quantity;
        /// <summary>
        /// 数量
        /// </summary>
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
        Decimal _DealQuantity = 0m;
        /// <summary>
        /// 已成交数量
        /// </summary>
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
        MarketOrder_TypeEnum _Type = (MarketOrder_TypeEnum)(0);
        /// <summary>
        /// 类型
        /// </summary>
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
        /// <summary>
        /// 2:市价 1:限价
        /// </summary>
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
        /// <summary>
        /// 手续费
        /// </summary>
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
    }
    /// <summary>
    /// 合约交易里的市场挂单
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = MarketOrder_TypeEnum.ContractMarketOrder)]
    public class ContractMarketOrder : MarketOrder
    {
        System.Nullable<Decimal> _Margin;
        /// <summary>
        /// 保证金
        /// </summary>
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
        /// <summary>
        /// 杠杆
        /// </summary>
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
    }
    public enum MarketOrder_StatusEnum : int
    {
        /// <summary>
        /// 挂单中
        /// </summary>
        Open = 1,
        /// <summary>
        /// 已经撤销
        /// </summary>
        Canceled = 100,
        /// <summary>
        /// 关闭
        /// </summary>
        Closed = 200
    }
    public enum MarketOrder_TypeEnum : int
    {
        /// <summary>
        /// 合约交易里的市场挂单
        /// </summary>
        ContractMarketOrder = 1
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
        System.Nullable<DateTime> _CreateTime;
        /// <summary>
        /// 数据生成时间
        /// </summary>
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
    /// 与挂单相关的明细
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = MoneyDetail_TypeEnum.MarketOrderMoneyDetail)]
    public class MarketOrderMoneyDetail : MoneyDetail
    {
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
    }
    /// <summary>
    /// 扣除订单保证金
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = MoneyDetail_TypeEnum.AddPositionMarginDetail)]
    public class AddPositionMarginDetail : MarketOrderMoneyDetail
    {
    }
    /// <summary>
    /// 归还订单保证金
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = MoneyDetail_TypeEnum.CancelPositionMarginDetail)]
    public class CancelPositionMarginDetail : MarketOrderMoneyDetail
    {
    }
    /// <summary>
    /// 扣除手续费
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = MoneyDetail_TypeEnum.AddTradeFeeDetail)]
    public class AddTradeFeeDetail : MarketOrderMoneyDetail
    {
    }
    /// <summary>
    /// 归还手续费
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type", AutoSetPropertyValueOnInsert = MoneyDetail_TypeEnum.CancelTradeFeeDetail)]
    public class CancelTradeFeeDetail : MarketOrderMoneyDetail
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
        /// 与挂单相关的明细
        /// </summary>
        MarketOrderMoneyDetail = 2,
        /// <summary>
        /// 扣除订单保证金
        /// </summary>
        AddPositionMarginDetail = 101 + MarketOrderMoneyDetail,
        /// <summary>
        /// 归还订单保证金
        /// </summary>
        CancelPositionMarginDetail = 201 + MarketOrderMoneyDetail,
        /// <summary>
        /// 扣除手续费
        /// </summary>
        AddTradeFeeDetail = 301 + MarketOrderMoneyDetail,
        /// <summary>
        /// 归还手续费
        /// </summary>
        CancelTradeFeeDetail = 401 + MarketOrderMoneyDetail,
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
        Boolean _IsPositionLocked = true;
        /// <summary>
        /// 是否锁仓
        /// </summary>
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
            modelBuilder.Entity<MarketOrder>().HasDiscriminator<MarketOrder_TypeEnum>("Type")
            .HasValue<MarketOrder>((MarketOrder_TypeEnum)0)
            .HasValue<ContractMarketOrder>(MarketOrder_TypeEnum.ContractMarketOrder)
            ;
            modelBuilder.Entity<MarketDealHistory>().HasKey(m => m.id);
            modelBuilder.Entity<MoneyDetail>().HasKey(m => m.id);
            modelBuilder.Entity<MoneyDetail>().HasDiscriminator<MoneyDetail_TypeEnum>("Type")
            .HasValue<MoneyDetail>((MoneyDetail_TypeEnum)0)
            .HasValue<PositionMoneyDetail>(MoneyDetail_TypeEnum.PositionMoneyDetail)
            .HasValue<MarketOrderMoneyDetail>(MoneyDetail_TypeEnum.MarketOrderMoneyDetail)
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
        System.Linq.IQueryable<ContractMarketOrder> _ContractMarketOrder;
        public virtual System.Linq.IQueryable<ContractMarketOrder> ContractMarketOrder
        {
            get
            {
                if (_ContractMarketOrder == null)
                {
                    _ContractMarketOrder = this.Set<ContractMarketOrder>();
                }
                return _ContractMarketOrder;
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
        System.Linq.IQueryable<MarketOrderMoneyDetail> _MarketOrderMoneyDetail;
        public virtual System.Linq.IQueryable<MarketOrderMoneyDetail> MarketOrderMoneyDetail
        {
            get
            {
                if (_MarketOrderMoneyDetail == null)
                {
                    _MarketOrderMoneyDetail = this.Set<MarketOrderMoneyDetail>();
                }
                return _MarketOrderMoneyDetail;
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
            result.Append("H4sIAAAAAAAACu1dbXPTSLb+K1S+XuZiS7ZkT+18gGSom7q87cLu3KrNFKXEIrjGsVlbGZaZoiqZGSAB8nIHZghvE5jNAlNzSRhgICRk+TOR7Hzav3C7W7IlS23LaqvlbkVfILbasnX6nNPn5TnnfD10RhkvqbWhj//6tfnnCWVKHfp46PTfSqfV6pdqdejg0J8qF80F");
            result.Append("o5o6Zf5lrSoWwOW/KKVp8CItpy8fbF3RLl1Q7WtDw1VV0VT0BYcntGKlPORYO1Epa2pZcyz/esz8MWNDH4M/iwXwv5A+ODY0oVyAHwYvy9OlEngD3gG8Ghs6WS2o1bEh8NaIoinjSk0dHQEXZPBG8Vhl4gvwd+oyeDFcKU1PlWvg5V9bd5a63xouAq9Ha4entcpoeaKq");
            result.Append("ToFfCy5p1WkV3lIpnwAfAW+cU0o1+E5h/Ax4fPTh8eJkES4G71rvWbf/tDw9NaKeK5Yd75XU8qR23n5dUM8p0yUNUcV+F1EGPR2iyWjt1H+jV9bPqUBKoAdLoasjxdpEtThVLCtaper4kcMlpVazHhLeGWxIiyBCG0HGhvSnt435lfpv2/pPN9CztIgzopbUSbi16OGw");
            result.Append("ZGp9YYtO1i91kKkHGo0NHTpk/PBi79qS+WvGxsp/nFbKWlG7dOCTA4ASY+VDh3a33xqP3rcWnKoWJ1RwVUA3b5F3bAi9Dkbe5mPY9M0R01d00bd+/Y0xM9tO2dOaok3X6NPUJNbu5nNAr+Zu2gTVr13V164Zc8u7W2vgjT+pSuHSmcrJC2oZktVa8/al/n5md/sWeGVd");
            result.Append("Saean3/3Clww7z5cqtSK5Un4QXD5ALq+9+0zfe6quQpumFLVikoJrlQLYKHYus+VZ3Bpc93hkr0m01rz9qXx/dre7Rn4XUp5AjwMvJ51XNeX5/R3b8Cr42p1Ej5mNtV8CPQE+tpvjdf/tJ7jqFI07yCl0r0w0NiQuax3DsoTc1DGLaGLq+ZG6gs/6Etv8UKKlORooQ+e");
            result.Append("IlNnIclbOkNMrqyLXI31nwGhjPkbQKO00+pYRetd6DAKPzTyjA2lAnJTWiKmj+RmJyAqSNZwJDIlj2dCycSEkrEno3Hnzd6d1x1OxiJ8g1jiCuAWWvMWA5A58kMuhyWVeULjSYWO635opU4Up5RScFKBp8yFQy5yjZ53kwsdSIBc7bSCBxP/dBKIbVM55aKT8WBGX17c");
            result.Append("3Zwx/u+xZW24aYYUVgyIJhATLe02OG8/M+beWp5Ni05/rvFsHhArddnt7phWJE6pm2Yl5ypdJCaU22/Z/fCwsTG7d+1/26l0XKkCPujHOAhd2AiMhDQxmTzG+eZs/emCy727NDVeKfXBRV8q1YnzSjU4ibKpcNiImDxuY9x4+Mh4eNVlhqsg9qRMqkxYmIFpQy5hbkPc");
            result.Append("eP5z/f4czmg6rVUunKpWzhU1vg81YqdOlr20MhZWO9HqWKVW45tSWWJK5XxtJuxhB80mvs86gVgSczJWEsFZt/fzw06SeHiqMm0pHW65jDiIkMth5bETxaA8xoFexGZnzu3xGTPbUNdvLbUTa3BKXgjNmBKI/eK823UxfnynL7sMziO1QPkHMlsBBctn7+lr90B8+Mi0");
            result.Append("I+sA3q3/sgX+PK2WStTSDcQHZd6Tztm5pc8v1O99p29+43GWgfH+harx7y0T67G828tprP+rvrNu6n9Tp4HTsgP1EN1OAkbQTh3jmnxA9knpl8FGsnAWBgxmcW5gELtB+TzGKJt7aAWUMWH3k1UYXVFKPAfeSU1YMYUL+8093N28AfM4TXHUH2x5A6YW2eKi18hJ6D5J");
            result.Append("nSlPmD+8/8grnTAHCpK+NfQZYqr1eLo23lzRr22ZDhwILRnX1xo3v9Xvv25m8oElpKkTGjx4qZywadKotIg+2XZm/P4dMHwbr5+AZKw7zHq8UlYvHZ6Apm+AaGto4JKwNB8prVAYuy2KuLVmrMBMY337eeP1u3ZiHVVV3gOJASn1OVw+8j/DlfK54mQTqTRa+3O5+Df0");
            result.Append("C5ofHK0Nl6ZrmlpVC87bIYATvB/6qB3U/9wDlzrbvIQ2h/wbXPyM+SLXCusRwYLUZScQrGBBt5ywMhkshgg0CIdREYjLF4cmUMWhCV3BYihmgh76v4o1sNGXCDFpcp5XTJpAB5OWS/nj/xjOXQmUguk5z6HOdwrUh07EGdCcxwfuCG8JZF8T2TqUmIg81un2ePmOCQu0");
            result.Append("4ihuI4/baJ0QqosRssHSUuWfdz1Yz7bWUTYnRKrmhIhFZljuw+NnWM8BGVKEhkUu0/UcDQgBidy6EGmdo+6ktL60AbZid+euJ3lxRClBFMhAvJIAMi7SOkk9Keqbs9DiwENBkJAOEA8SHsFIT1cxle8BDjJcCUCgEMEgaSwYBOiB0yNnAhKIODCFkDadVdJo9L5Or5aG");
            result.Append("SMXXERFCq03Elpbriy/0q9v17Vu4fOpnRe18oapcPFqtfKXyLmik9pmIoCRezWQ8e6y/d1tplVoRLosobbg3s2z/EJAmLJYnS+rxSsFZaHPlmXPJUXAna4FAWHsihpof8zPyLO4PGJQ62NR8n3cyc6wg1VlzGWVTL0PV1OtueZmAwNHyuQqhZZfP8hoyytAJGeWlrgQx");
            result.Append("/2AXf5mhZOrm5e58WKn2477SzC5mKJm0+VxXinxaNiWZ/JgYL4ZGlHRAooj9qnm6GjdLVeN6yv6QW22sLNa3XXhjdOT0FbIXYSUpn/o3S8eMTYlMhhp6Ne6zdPSvmHKbqTiLfnDIyE6GfGAC9a554I44CeSJxezAWHQYsRgW6UTs76Q8kRik2HBtGiLyc3Y3F83K6vr9");
            result.Append("Tf3KKwDKaay/0Hd+gAX1jtCt7fUY8//Yu7tmfsaOII2VDxcKjsiR42Op9IH/OOC8l+U97Xzf+LDivY9Zr4W9ldDhVuZPstEI6MecqSoFFUAR7I+LXX+J8+NWzZjnDpkOd2hSEOUWLMKZWQX7s1nPZ8kAMVkSH7F5YHXhzO4mpnWyB8ki4UIVlNNIWVrhLqETVggE/q0D");
            result.Append("EesKc3xYkoOFPJ0JrrxqzN5GmgWKiRtbxToOwIdOvRcTWI/akv5IYENOKzlk9BAePtT2he6VlINBElXXRPIFmUNI6+JVfellO4cDaDkqODrWn7eCmqlw6a1IlILu2diVXkqUPBjUaCZOlZcSpdMrHdciQolSrjQdzxpCiZIHiNrNxL9USaJkiaMuNLy7KRKdMjixl4ab");
            result.Append("LHskPnSRGcS4uU27yCBuchdT97xSngxk6p4sFVqdZPGotRPqRZ8VZfUixmhFnRzb9d2Ha8aDVRxQ9dO/XyhWWUeqiqHz6ATaroKDePDdglpSNc+7UaXzwfecnYI7rFgp/Qmw5uw0+EyxcJY6b+fY5u1k03Cblqe6aXazaM9u2Zd62aa+6v/2VwEgeOuIMvHF9AVz/wrA");
            result.Append("17mgVrUiUiGAjtbSr+2CXTvn7S4fu3yZTKmFEQKDygzRwKHAqAXAHKYHJfWYQ2sS9cjVmZZL0z/TnNFF/JnmXEHvTAtLZtFmnbfs6ghEF/OtTn6JgksEXqx6T0IMpcLMPPa/3980Vjb05Sfg38bvjzwFBtb9AyW4iRsU1B+s6us/mT/P7IZfQg9pJ7TNn9ha8ZdiVZt2");
            result.Append("LEqlmAHzJp6CW15EtuWFoU076JK7aE2WbijtZJ963CeMCWP9D0karTlDFQSadcfIYbHazgK+HoQsmSqhZlk8JlMRccJPpkqo359frQPD4Pt2uoSXOpVETw35/C/6rVl9eaH+9AUwGPQn3/z7/b0jZ4ZhAR6wfZyX9Re/ASMIXGvn3eHK1FSlUNQucUxP0gyrJHo62yOC");
            result.Append("mYOiOtCpOTaKMiCStMzBj1akyVVJxDdesspRvbznvGzyXqsq1AaWQljjoIpXQ2I+0vSrJHr64bcotvhCf76CS1u3CBYwb43LLFamm3UpA2BD0qyrJHZHHkUEVTburu/N3DM3DPD+iUoVJLGbZgBw1VIWnHd5rr71pLUMGNRaVZnQHAuhAqJBX0LsrSRmIxvSMFD+CwKV");
            result.Append("dG+bs9xAEiXe5hF44wN+xMr1R6yIq8JyElUPr83Y93jhbVfD9MDpkkxmk2S2lHmgcPMz+m8/me1u2mWN8cKwkH0Dlpkqx5McRsBqEVcLhMtpfhlY+zExSVh00wCJV7p8macZMJLcLrrZWNq4+Y2+8IPHhvpC1fqYPS2JvEaOEJUoRI5EfLjOjaFoagd2hbedQCGGkDKp");
            result.Append("HiZijADwG2L/CFyp3Xcv9Cv/9AzFWPhRvxZ0KIbXsvUjI3HkKOMZLIKJGTEVKgrMYsSRooxnIiJSfrC4FqDIx8pweePxs/ra1u72tvHdknXBCy/vB3sZoncZmHLEIaGMp5Xq25fm9HQce42ABDJTLOZ1w/0oRRwGyrjbINSvvzFmZt1FHoo2XYsiHIQY3JxT35xhb0+N");
            result.Append("v/Jq705zgr3aTx7fj5p9N+WianflU1TtrjTe7kLSg6uENK0vKEN9lUJKGV4bZyGKUbDBMt4Wn1A6PDhW2/pl2AyjNMZUyni8TMSouCJIto9CSnM+pIxnAPzGO2Cq4nnpDIhGc89L5PaWZxaUaTJg6lqgumO7qsWPSsS2Vdbj+HQ0rAIbVVH28fcjUJZtI4Ay+Bh7qHux");
            result.Append("cthl3ITr8wLj4fqsB1eBoqf17Vv19TsmvDWkshbGYqoEw5pY5TGRdR7zzGSZu69vb2HHjgQvxY783AsvB8s8Z1FG4Lqi6h2Uf6dSPZu/xH2JtaMWMc1m9id+LORgIcuCneVDsD2tmpBPyevB4cdfcnz4i1Fgj81ZEktBaXNALzYobU7522wGpYvlSTMs3YpZ17eXzEXN");
            result.Append("sHUB9TwljFv72Tb5+LAoo0AqlknGKUzI4+Ii0HZbAcFY+UDj9Sp4YWY7gQmzd2cdyJW+9EKfgR0aO5iHLMWfaEO744Mryuf5l/2EvcOuXIiWCcERzbqNJDOkN5krSyBgLjaPdSGV5sMZdKcZ67cB481ZziBgROPlY33uhfNdGJvYXGh8u9P4cB8g14zv1/Xlub2VVX3u");
            result.Append("Tjtfgt9fLPDtPMYm6iikBJb5keiMTiKUWJ5NIpSDj1BGKdiMJqpY1oWZOJIs7M5frfwFn835hBTT4fgB7rWz+iPaHZH4sIk9RTRmKm4DzqM60Janc4W2WW/MEnJykz0eRt9xdgrts9ln9IK1kH4nJiEVy+A3nYaSzW2Jrplk6xujbSQppCi30I7bMTRYEY5BDDsR4bC5");
            result.Append("Is16UFnaV0WtIXekSmRrkMdjmuk4eXI8tm8Wo8D7RIQHKsKxjEAmfk+fXJEEWRNv2MsVHAZlo296wlH3Dj/8WuupsLOJkDsR1UQiFgMOaYnvetABlRdzVNMfrYDQZVaZZ+U9kN5CdOF04cKXElXen3TkeFHlLZmQc3FpFxhunxqGsStpRjMUNk95urHM36hvP2+8ftfO");
            result.Append("VEdVlVUDgCmUchKH6E9ghBQXoJOcx37+fm3v9gy2KlcpT6istzFiCVgdO69RSPPB054aQTSU0tXDIJLZkzAhCbKPCz+YOUkrAWnlJY3nP9fvz5kXT2uVC8BFO1fUPhFaV42FVfvqsUqt9oloXqu/2q5vr+rvt2B1jrniUg0VDH+SIUtu+jY+DtiXMJGcdslhfKwrH2Mq");
            result.Append("2Rl/KAgixzGJ8BVkf5CN/agh+yrAhiK2n8PXAqNJvqaEyZ7xVI0PO/r1R/V733UY7HW4UEA3VUoBR3wx5qOmM/0PrUqc1kFBKgTKaVL4NN3DiO4V7EMr0BadN39ydLDytm+Nmkskqh3l3YG8xu/fAXVprCzWt13z/RCVRlRNKZbIusjLrqlTHHWRpzMDWvYZbcT4lLd2");
            result.Append("qoTXN14W8UwZak9d1ohFWtAte2Yd4UbqHgZKi5lBuoFpQ9o0XvZgefSlFf3mj/ry4u7OXQ+RjiglGHTllkykXeNl1ywjtxriokEkpVHMsgtR4KZNVH775qKZta3f39SvvAKejXVCj5Wb5rzjeIYuPaHbTWfisuyZZAQeojF7G3povCajQ5y9jNnCyCcKCwJl3I/LesQ7");
            result.Append("I44F3DSaEChDQuJLOLooh/gSTmQ6250ko9o3K03Tf5dT2CEke1cW6jvrux8eG7Mb7Scr9ClPq5pWLE8SevE+s+AG5bT26MojglFw5b0D4VY29OUn+vKc/u6NaeS0bwT4Dc1jX61O9mVEjhep9cFtJ1comLsoFWX8+uHt82yiteLptn79mdm83VxxvPIl0GjwTp9A9cQK");
            result.Append("KCPmmUGR0aJQWyt7JlAirsRNoLR5lNtsoER8eLkh4qZ4diITFFZuiSQTEymP5SVcuNXmJRB4bWKAeCRWjpRY3koxxFGdiAU5inNS5YlJ5T7RG+v/Asa7yV0m2QBAGITK9M1v8CPkT5aBRX/qGK+kE/hBOyatzgSRdTSQZ2aiaS2a8uTOHTatRr5P/kSCeGoWKNLF/SSt");
            result.Append("VPjkCrpNAsy2taPlcxW8ZnVd76Rb9+e8UCFDKfO+X+eF+hE0PiNiRJnHWpQmgwo59xxRfWkDpBg4gM94Y9mCGGXLj9bPw7b8EHIHcxC36tcapKBOFKcUCAe4bKsNIedJNtychTkGPNodxZ8DAt2Z2xyRl80R0+4z0lhahvO/roKo9i1cAOSzona+UFUuHq1WvlI53qIM");
            result.Append("41uUFNh1PqUiwLAkFZMRVkyK+fhsaFOzZlPuyI6JCMdlQNuKWonN4F7zoMgaMn8MTIWahhCigCMfev13Y/4fdp+UsfJRVYWX//AHGAAmyVj6KeSA+aCILeNMim0WtZ20LFW26yP/zijfxWegYiYd53ORLum6YG9G0I8LjHtr92hMAaP280V6Px9hWdAzOIsv6T0K8xkb");
            result.Append("tzsLIRpbrmaNATM0kXtHYaMzkiznIKPxGa5yNFQliW6uk7og9dX6A900qt4fCbhAyEisH1VuQKEJQsUh5dApj/A5+w8r55k4b5IJ2+gQkont4tKwYXLJ0T7Qo53RCZ628LiBpvr7mQ465uQFtbyvVEwiOgMVHe66n8v7a2qiX4v0SKWNLitSTnAkNbAhblaW6YJlW1d4");
            result.Append("2sXc39LX75kDRHDH79Hi35k+fcMuabMNfEnoQiqcmQ9IxbaR70urSMdLxK5UMMtFN3NZErnuyBZ+tXbCwjYLMzoY12ZeN5q4sf5Yn/veBA1bA7DGygd2362a2GHrwvwNcK1dWx+raDVm+6AlXXHj7GNmRS4zL+HJ3imA4mBtKl3UMuiXtLE+ioV8mrorSdpEJ7GM4guS");
            result.Append("WOFA9TijGXRbY2exdcKw0H7xqr700ngws/friuX2A9UNFLWxsK5v3drdXNCfrxjP1/SZ97h64uMK21EB32riBKnCk71EuW5U1VB9/LGuIXnsqk5yJ8WuPt+SmLCA2I4wW/eO+ey3yfWjDMXMeasgS0h3JSKKCTEc1PEjIUFOJ8I8TZbRbHtiHA700Mqxbhy6AUx7d5c9");
            result.Append("zZeGQZylMrVf8BfJBCn2bD/G54TLnvFCwsdmF7MD6Y9xEoVkKYLqqV6xySYMI0ECxjPULKUYFx9Pd28TRourPDRhtJGUu4Kgdav7Lmy9q5SnFUfjX2f7XkfvXghqcnYGNjsbtjUHhl0gwbJmG2CnW+js8AuWEE4VTYC8sRJfupCCxLXgkysYzdInDudAuYLDCeL7Fism");
            result.Append("ZXiBmTMMkJYoTxfebxx/MOzaTkc0lhYLSPtDjmiwQysR04ffa0tA+Dd3lKxEy1OU+1nGmaecKqYAfntzoniYigbPF52+udBcTf9M5690THaXg5gIkPrtVWNuGVvKzMOgXJYqxBIhC1nIGM0HJL7vIH1fOcV3C+pAzY5bQxWTfsc94qXb2kMHgE3T5dkYNd5LOJlhfJIs");
            result.Append("8AGf7K4kxXQ2RoMO/RBvwcoY+2s0nu6vF7wUm8GKfpsi8LMpcuwGOfptjsjP5uRiNjjSb2sy/GxNPuaDKv22Ksv4VrFhuTOdabWDewIrc9RPVKpgA/0Gp7fBpzyj03tATVnz1zNmuynccPUsYc8pv6YTmTiFFCNJDXq/MKKxIXImLknjRNC5EPREeEMU3tggPvZxR7aA");
            result.Append("LZr2dUe2YCGHRN+EbSxIcdE3yUkUMmdEARJKOINHByPWgybpko7xkY62x+UegLv7fkW/Mlffemq10dFn3jfeXNfXnsKJ5s1r4J29X1c8QcNKtc82aCFWgwYdiBtpHx2qvJfjEkLCBulihGSImnR0c/OxJh3lKHycSUc5Bhpn0lGOQMWZdJSd6TiTjrK3GWfSJe4YMeli");
            result.Append("gLVP2l0RtLtKJn6GqYHyfIwGkt3Ypw7lBwHLDjA5lSaeJ2jIwgQMhZBWiVUFWLzhGnmmh+p0h6xLGXeYUH/70spWmuMS2gQL1myGMhWhD/nK9BYWDDnH2x+OsCcMoZXxjmpmQiK3XPUoakms6AnsoyEm+uZsffGF/nzFAmmDIH/j9SoQ4sbjZ/W1LWNlY+/OOkQhLYHI");
            result.Append("/yrAB7tAllWloA5XiuWAOO4whVvocHgSTKdPsSvcLbCIJGapmDOD0bcDnkVDaYs8k+ZNGavP/6LfmrUHCgWRtOHK1FSlAA5QTg5Sv40NZqkOZh9l0V0C1viwo19/BDang8wdLoAtAouV0kCdiUw4/aWz/MheJLZPR/eh1R0g9m1383xUiOQ8k8MfPjIeXm0X1mPql2pV");
            result.Append("mewHRTm4caA5AtlsI55DzeXSrNTTHDqkLwNAyRPTON27dtOsSNMfbBk3vzGR8JiHgfU3vcHiA7t7ePPDYoQknsJSPCWZZJecV16uYLquwtbAQrxmLfjq1WDluEmgepCKVYpTsjQZahJlnU3iMQn5GAxRSyZl9++EJQgEGmdTjg/zzp18MeZv1LefN16/c5V1qirTEcME");
            result.Append("fhAf+AFXELhWZsWD5eFUkvxi71G2K0pC7SyG2sUUH+i6XMYdPL79zJh7W9++VV+/03j9BPwNFzqp6s5uEMsp1dnNfoddsL58SQhjcIedmIqgjm9E1ZRiqUuRgWNBJ1HKdh127ppJwqjUhOgyOR64jYIMV9uKKS7rHu02125wkb60ART67s5dTwvPI0pJKQfo1jMYzJdf");
            result.Append("nbfAAQ5FTHv8x6VlCCm6ug2OWVx71c+K2vlCVbl4tFr5Si1zvkUi/7YwXZUj8owkxwzq8QDImcS8BTYYJf75OIGO9yWoMYAoJJGYJIEWcRyEaQhHUkI3QLc+aWKYFGnhOYNu0h0mck6rmlYsT+J1hntBp4CPZ8TMyoa+/GTvNhyS1m4CA7vSOsCPVSa+UPuL/4SG+3K3");
            result.Append("mZZ9TGCCrq+sxnlyLHNYd6crEr7DuGDcMJ6f72U/JcaxggLHTGiA0SyvzYnuSjk4HXJnweRH/e3Lxs+/Aq50syRLKjBo4ZvAT4+GBL0nplM8wiSiESu6Gp6yXEWp4hMh7O8US9NN3yajsVksRQUvLaEagnxh/64md4yAu0A9A16D6TiAgf5ztKxJGfRFbastjvGuP61V");
            result.Append("gfns/YDNNr1/pu2ZOv60z+FjwavAdLc+mB8XpFT+XOqjnCinP8pklXMf5bIT2Y9UuZCSchPj6XF5fOjy/wP8xlZryuABAA==");
            return result.ToString();
        }
    }
}

/*<design>
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
<design>*/

