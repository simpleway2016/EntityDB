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

namespace FllowOrderSystem.DBModels
{
    [TableConfig]
    [Table("tradeuser")]
    [Way.EntityDB.DataItemJsonConverter]
    public class TradeUser :Way.EntityDB.DataItem
    {
        Int64 _TradeUserId;
        [Key]
        [DisallowNull]
        [Column("tradeuserid")]
        public virtual Int64 TradeUserId
        {
            get
            {
                return _TradeUserId;
            }
            set
            {
                if ((_TradeUserId != value))
                {
                    SendPropertyChanging("TradeUserId",_TradeUserId,value);
                    _TradeUserId = value;
                    SendPropertyChanged("TradeUserId");
                }
            }
        }
        TradeUser_StyleEnum? _Style;
        /// <summary>
        /// 风格
        /// </summary>
        [Display(Name = "风格")]
        [Column("style")]
        public virtual TradeUser_StyleEnum? Style
        {
            get
            {
                return _Style;
            }
            set
            {
                if ((_Style != value))
                {
                    SendPropertyChanging("Style",_Style,value);
                    _Style = value;
                    SendPropertyChanged("Style");
                }
            }
        }
        String _Description;
        /// <summary>
        /// 简介
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "简介")]
        [Column("description")]
        public virtual String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                if ((_Description != value))
                {
                    SendPropertyChanging("Description",_Description,value);
                    _Description = value;
                    SendPropertyChanged("Description");
                }
            }
        }
        System.Nullable<Boolean> _EnableSubscribe=false;
        /// <summary>
        /// 开启订阅
        /// </summary>
        [Display(Name = "开启订阅")]
        [Column("enablesubscribe")]
        public virtual System.Nullable<Boolean> EnableSubscribe
        {
            get
            {
                return _EnableSubscribe;
            }
            set
            {
                if ((_EnableSubscribe != value))
                {
                    SendPropertyChanging("EnableSubscribe",_EnableSubscribe,value);
                    _EnableSubscribe = value;
                    SendPropertyChanged("EnableSubscribe");
                }
            }
        }
        System.Nullable<Boolean> _IsVisible=false;
        /// <summary>
        /// 开启展示
        /// </summary>
        [Display(Name = "开启展示")]
        [Column("isvisible")]
        public virtual System.Nullable<Boolean> IsVisible
        {
            get
            {
                return _IsVisible;
            }
            set
            {
                if ((_IsVisible != value))
                {
                    SendPropertyChanging("IsVisible",_IsVisible,value);
                    _IsVisible = value;
                    SendPropertyChanged("IsVisible");
                }
            }
        }
        System.Nullable<double> _SubscribePrice;
        /// <summary>
        /// 订阅价格
        /// </summary>
        [Display(Name = "订阅价格")]
        [Column("subscribeprice")]
        public virtual System.Nullable<double> SubscribePrice
        {
            get
            {
                return _SubscribePrice;
            }
            set
            {
                if ((_SubscribePrice != value))
                {
                    SendPropertyChanging("SubscribePrice",_SubscribePrice,value);
                    _SubscribePrice = value;
                    SendPropertyChanged("SubscribePrice");
                }
            }
        }
        System.Nullable<double> _SpecialPrice;
        /// <summary>
        /// 优惠价
        /// </summary>
        [Display(Name = "优惠价")]
        [Column("specialprice")]
        public virtual System.Nullable<double> SpecialPrice
        {
            get
            {
                return _SpecialPrice;
            }
            set
            {
                if ((_SpecialPrice != value))
                {
                    SendPropertyChanging("SpecialPrice",_SpecialPrice,value);
                    _SpecialPrice = value;
                    SendPropertyChanged("SpecialPrice");
                }
            }
        }
        System.Nullable<Int32> _SubscribeDays;
        /// <summary>
        /// 最长订阅几天
        /// </summary>
        [Display(Name = "最长订阅几天")]
        [Column("subscribedays")]
        public virtual System.Nullable<Int32> SubscribeDays
        {
            get
            {
                return _SubscribeDays;
            }
            set
            {
                if ((_SubscribeDays != value))
                {
                    SendPropertyChanging("SubscribeDays",_SubscribeDays,value);
                    _SubscribeDays = value;
                    SendPropertyChanged("SubscribeDays");
                }
            }
        }
        TradeUser_StatusEnum? _Status=(TradeUser_StatusEnum?)(1);
        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        [Column("status")]
        public virtual TradeUser_StatusEnum? Status
        {
            get
            {
                return _Status;
            }
            set
            {
                if ((_Status != value))
                {
                    SendPropertyChanging("Status",_Status,value);
                    _Status = value;
                    SendPropertyChanged("Status");
                }
            }
        }
        System.Nullable<double> _TotalProfit=0;
        /// <summary>
        /// 累计收益
        /// </summary>
        [Display(Name = "累计收益")]
        [Column("totalprofit")]
        public virtual System.Nullable<double> TotalProfit
        {
            get
            {
                return _TotalProfit;
            }
            set
            {
                if ((_TotalProfit != value))
                {
                    SendPropertyChanging("TotalProfit",_TotalProfit,value);
                    _TotalProfit = value;
                    SendPropertyChanged("TotalProfit");
                }
            }
        }
        System.Nullable<double> _ProfitRate=0;
        /// <summary>
        /// 收益率
        /// </summary>
        [Display(Name = "收益率")]
        [Column("profitrate")]
        public virtual System.Nullable<double> ProfitRate
        {
            get
            {
                return _ProfitRate;
            }
            set
            {
                if ((_ProfitRate != value))
                {
                    SendPropertyChanging("ProfitRate",_ProfitRate,value);
                    _ProfitRate = value;
                    SendPropertyChanged("ProfitRate");
                }
            }
        }
        System.Nullable<double> _Retracement=0;
        /// <summary>
        /// 近3周最大回撤
        /// </summary>
        [Display(Name = "近3周最大回撤")]
        [Column("retracement")]
        public virtual System.Nullable<double> Retracement
        {
            get
            {
                return _Retracement;
            }
            set
            {
                if ((_Retracement != value))
                {
                    SendPropertyChanging("Retracement",_Retracement,value);
                    _Retracement = value;
                    SendPropertyChanged("Retracement");
                }
            }
        }
        System.Nullable<Int32> _TradeDays=0;
        /// <summary>
        /// 交易天数
        /// </summary>
        [Display(Name = "交易天数")]
        [Column("tradedays")]
        public virtual System.Nullable<Int32> TradeDays
        {
            get
            {
                return _TradeDays;
            }
            set
            {
                if ((_TradeDays != value))
                {
                    SendPropertyChanging("TradeDays",_TradeDays,value);
                    _TradeDays = value;
                    SendPropertyChanged("TradeDays");
                }
            }
        }
        System.Nullable<double> _PositionDisRate=0;
        /// <summary>
        /// 持仓分布的数值
        /// </summary>
        [Display(Name = "持仓分布的数值")]
        [Column("positiondisrate")]
        public virtual System.Nullable<double> PositionDisRate
        {
            get
            {
                return _PositionDisRate;
            }
            set
            {
                if ((_PositionDisRate != value))
                {
                    SendPropertyChanging("PositionDisRate",_PositionDisRate,value);
                    _PositionDisRate = value;
                    SendPropertyChanged("PositionDisRate");
                }
            }
        }
        System.Nullable<Int32> _Subscribers=0;
        /// <summary>
        /// 累计订阅人数
        /// </summary>
        [Display(Name = "累计订阅人数")]
        [Column("subscribers")]
        public virtual System.Nullable<Int32> Subscribers
        {
            get
            {
                return _Subscribers;
            }
            set
            {
                if ((_Subscribers != value))
                {
                    SendPropertyChanging("Subscribers",_Subscribers,value);
                    _Subscribers = value;
                    SendPropertyChanged("Subscribers");
                }
            }
        }
        System.Nullable<double> _WinRate=0;
        /// <summary>
        /// 胜率
        /// </summary>
        [Display(Name = "胜率")]
        [Column("winrate")]
        public virtual System.Nullable<double> WinRate
        {
            get
            {
                return _WinRate;
            }
            set
            {
                if ((_WinRate != value))
                {
                    SendPropertyChanging("WinRate",_WinRate,value);
                    _WinRate = value;
                    SendPropertyChanged("WinRate");
                }
            }
        }
        System.Nullable<double> _TranFrequency=0;
        /// <summary>
        /// 交易频率
        /// </summary>
        [Display(Name = "交易频率")]
        [Column("tranfrequency")]
        public virtual System.Nullable<double> TranFrequency
        {
            get
            {
                return _TranFrequency;
            }
            set
            {
                if ((_TranFrequency != value))
                {
                    SendPropertyChanging("TranFrequency",_TranFrequency,value);
                    _TranFrequency = value;
                    SendPropertyChanged("TranFrequency");
                }
            }
        }
        String _HeadIconUrl="https://filename.yjfzww.com/headsculpture/mo.png";
        /// <summary>
        /// 头像url
        /// </summary>
        [MaxLength(100)]
        [Display(Name = "头像url")]
        [Column("headiconurl")]
        public virtual String HeadIconUrl
        {
            get
            {
                return _HeadIconUrl;
            }
            set
            {
                if ((_HeadIconUrl != value))
                {
                    SendPropertyChanging("HeadIconUrl",_HeadIconUrl,value);
                    _HeadIconUrl = value;
                    SendPropertyChanged("HeadIconUrl");
                }
            }
        }
        String _NickName;
        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "昵称")]
        [Column("nickname")]
        public virtual String NickName
        {
            get
            {
                return _NickName;
            }
            set
            {
                if ((_NickName != value))
                {
                    SendPropertyChanging("NickName",_NickName,value);
                    _NickName = value;
                    SendPropertyChanged("NickName");
                }
            }
        }
        System.Nullable<Int32> _TradeCount=0;
        /// <summary>
        /// 累计交易订单数
        /// </summary>
        [Display(Name = "累计交易订单数")]
        [Column("tradecount")]
        public virtual System.Nullable<Int32> TradeCount
        {
            get
            {
                return _TradeCount;
            }
            set
            {
                if ((_TradeCount != value))
                {
                    SendPropertyChanging("TradeCount",_TradeCount,value);
                    _TradeCount = value;
                    SendPropertyChanged("TradeCount");
                }
            }
        }
        System.Nullable<Int32> _TradeProfitCount=0;
        /// <summary>
        /// 盈利订单数
        /// </summary>
        [Display(Name = "盈利订单数")]
        [Column("tradeprofitcount")]
        public virtual System.Nullable<Int32> TradeProfitCount
        {
            get
            {
                return _TradeProfitCount;
            }
            set
            {
                if ((_TradeProfitCount != value))
                {
                    SendPropertyChanging("TradeProfitCount",_TradeProfitCount,value);
                    _TradeProfitCount = value;
                    SendPropertyChanged("TradeProfitCount");
                }
            }
        }
        System.Nullable<Int32> _TradeLossCount=0;
        /// <summary>
        /// 亏损订单数
        /// </summary>
        [Display(Name = "亏损订单数")]
        [Column("tradelosscount")]
        public virtual System.Nullable<Int32> TradeLossCount
        {
            get
            {
                return _TradeLossCount;
            }
            set
            {
                if ((_TradeLossCount != value))
                {
                    SendPropertyChanging("TradeLossCount",_TradeLossCount,value);
                    _TradeLossCount = value;
                    SendPropertyChanged("TradeLossCount");
                }
            }
        }
        System.Nullable<double> _TradeLots=0;
        /// <summary>
        /// 交易手数
        /// </summary>
        [Display(Name = "交易手数")]
        [Column("tradelots")]
        public virtual System.Nullable<double> TradeLots
        {
            get
            {
                return _TradeLots;
            }
            set
            {
                if ((_TradeLots != value))
                {
                    SendPropertyChanging("TradeLots",_TradeLots,value);
                    _TradeLots = value;
                    SendPropertyChanged("TradeLots");
                }
            }
        }
        System.Nullable<double> _DayTradeLots=0;
        /// <summary>
        /// 日均交易笔数
        /// </summary>
        [Display(Name = "日均交易笔数")]
        [Column("daytradelots")]
        public virtual System.Nullable<double> DayTradeLots
        {
            get
            {
                return _DayTradeLots;
            }
            set
            {
                if ((_DayTradeLots != value))
                {
                    SendPropertyChanging("DayTradeLots",_DayTradeLots,value);
                    _DayTradeLots = value;
                    SendPropertyChanged("DayTradeLots");
                }
            }
        }
        System.Nullable<double> _MaxProfit=0;
        /// <summary>
        /// 最大盈利
        /// </summary>
        [Display(Name = "最大盈利")]
        [Column("maxprofit")]
        public virtual System.Nullable<double> MaxProfit
        {
            get
            {
                return _MaxProfit;
            }
            set
            {
                if ((_MaxProfit != value))
                {
                    SendPropertyChanging("MaxProfit",_MaxProfit,value);
                    _MaxProfit = value;
                    SendPropertyChanged("MaxProfit");
                }
            }
        }
        System.Nullable<double> _MaxLoss=0;
        /// <summary>
        /// 最大亏损
        /// </summary>
        [Display(Name = "最大亏损")]
        [Column("maxloss")]
        public virtual System.Nullable<double> MaxLoss
        {
            get
            {
                return _MaxLoss;
            }
            set
            {
                if ((_MaxLoss != value))
                {
                    SendPropertyChanging("MaxLoss",_MaxLoss,value);
                    _MaxLoss = value;
                    SendPropertyChanged("MaxLoss");
                }
            }
        }
        System.Nullable<Int32> _PositionCount=0;
        /// <summary>
        /// 当前持仓数量
        /// </summary>
        [Display(Name = "当前持仓数量")]
        [Column("positioncount")]
        public virtual System.Nullable<Int32> PositionCount
        {
            get
            {
                return _PositionCount;
            }
            set
            {
                if ((_PositionCount != value))
                {
                    SendPropertyChanging("PositionCount",_PositionCount,value);
                    _PositionCount = value;
                    SendPropertyChanged("PositionCount");
                }
            }
        }
        String _PositionDisStr="0m-5m";
        /// <summary>
        /// 持仓分布描述
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "持仓分布描述")]
        [Column("positiondisstr")]
        public virtual String PositionDisStr
        {
            get
            {
                return _PositionDisStr;
            }
            set
            {
                if ((_PositionDisStr != value))
                {
                    SendPropertyChanging("PositionDisStr",_PositionDisStr,value);
                    _PositionDisStr = value;
                    SendPropertyChanged("PositionDisStr");
                }
            }
        }
        String _FailReason;
        /// <summary>
        /// 审核失败原因
        /// </summary>
        [MaxLength(100)]
        [Display(Name = "审核失败原因")]
        [Column("failreason")]
        public virtual String FailReason
        {
            get
            {
                return _FailReason;
            }
            set
            {
                if ((_FailReason != value))
                {
                    SendPropertyChanging("FailReason",_FailReason,value);
                    _FailReason = value;
                    SendPropertyChanged("FailReason");
                }
            }
        }
        TradeUser_TypeEnum? _Type=(TradeUser_TypeEnum?)(1);
        /// <summary>
        /// 类型
        /// </summary>
        [Display(Name = "类型")]
        [Column("type")]
        public virtual TradeUser_TypeEnum? Type
        {
            get
            {
                return _Type;
            }
            set
            {
                if ((_Type != value))
                {
                    SendPropertyChanging("Type",_Type,value);
                    _Type = value;
                    SendPropertyChanged("Type");
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
                    SendPropertyChanging("CreateTime",_CreateTime,value);
                    _CreateTime = value;
                    SendPropertyChanged("CreateTime");
                }
            }
        }
        System.Nullable<DateTime> _ExamTime;
        /// <summary>
        /// 审批时间
        /// </summary>
        [Display(Name = "审批时间")]
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
                    SendPropertyChanging("ExamTime",_ExamTime,value);
                    _ExamTime = value;
                    SendPropertyChanged("ExamTime");
                }
            }
        }
        System.Nullable<double> _TotalProfitRate=0;
        /// <summary>
        /// 累计收益率
        /// </summary>
        [Display(Name = "累计收益率")]
        [Column("totalprofitrate")]
        public virtual System.Nullable<double> TotalProfitRate
        {
            get
            {
                return _TotalProfitRate;
            }
            set
            {
                if ((_TotalProfitRate != value))
                {
                    SendPropertyChanging("TotalProfitRate",_TotalProfitRate,value);
                    _TotalProfitRate = value;
                    SendPropertyChanged("TotalProfitRate");
                }
            }
        }
        System.Nullable<double> _AnalysisWinRate=0;
        /// <summary>
        /// 订单分析交易胜率
        /// </summary>
        [Display(Name = "订单分析交易胜率")]
        [Column("analysiswinrate")]
        public virtual System.Nullable<double> AnalysisWinRate
        {
            get
            {
                return _AnalysisWinRate;
            }
            set
            {
                if ((_AnalysisWinRate != value))
                {
                    SendPropertyChanging("AnalysisWinRate",_AnalysisWinRate,value);
                    _AnalysisWinRate = value;
                    SendPropertyChanged("AnalysisWinRate");
                }
            }
        }
    }
    public enum TradeUser_StyleEnum:int
    {
        /// <summary>
        /// 短线
        /// </summary>
        Short = 1<<0,
        /// <summary>
        /// 长线
        /// </summary>
        Long = 1<<1,
        /// <summary>
        /// 重仓
        /// </summary>
        High = 1<<2,
        /// <summary>
        /// 高频
        /// </summary>
        HighFrequency = 1<<3,
        /// <summary>
        /// 激进
        /// </summary>
        Radical = 1<<5,
        /// <summary>
        /// 中线
        /// </summary>
        CenterLine = 1<<6,
        /// <summary>
        /// 低频
        /// </summary>
        LowFrequency=1<<7,
        /// <summary>
        /// 轻仓
        /// </summary>
        LightWarehouse=1<<8,
        /// <summary>
        /// 稳健
        /// </summary>
        Steady = 1<<11
    }
    public enum TradeUser_StatusEnum:int
    {
        None = 0,
        /// <summary>
        /// 等待审核
        /// </summary>
        Reviewing = 1,
        /// <summary>
        /// 审核通过
        /// </summary>
        Approved = 2,
        /// <summary>
        /// 审核失败
        /// </summary>
        Fail = 3
    }
    public enum TradeUser_TypeEnum:int
    {
        /// <summary>
        /// 用户自己申请成为的交易员
        /// </summary>
        User = 1,
        /// <summary>
        /// 系统后台配置的交易员
        /// </summary>
        System = 2
    }
    /// <summary>
    /// 跟单设置
    /// </summary>
    [TableConfig( AutoSetPropertyNameOnInsert = "Type" , AutoSetPropertyValueOnInsert=(FllowOrderSetting_TypeEnum)0)]
    [Table("fllowordersetting")]
    [Way.EntityDB.DataItemJsonConverter]
    public class FllowOrderSetting :Way.EntityDB.DataItem
    {
        System.Nullable<Int32> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int32> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id",_id,value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        Int64 _UserId;
        /// <summary>
        /// 跟单人id
        /// </summary>
        [DisallowNull]
        [Display(Name = "跟单人id")]
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
                    SendPropertyChanging("UserId",_UserId,value);
                    _UserId = value;
                    SendPropertyChanged("UserId");
                }
            }
        }
        Int64 _TradeUserId;
        /// <summary>
        /// 被跟单人id
        /// </summary>
        [DisallowNull]
        [Display(Name = "被跟单人id")]
        [Column("tradeuserid")]
        public virtual Int64 TradeUserId
        {
            get
            {
                return _TradeUserId;
            }
            set
            {
                if ((_TradeUserId != value))
                {
                    SendPropertyChanging("TradeUserId",_TradeUserId,value);
                    _TradeUserId = value;
                    SendPropertyChanged("TradeUserId");
                }
            }
        }
        System.Nullable<Boolean> _IsEnable=true;
        /// <summary>
        /// 是否跟单
        /// </summary>
        [Display(Name = "是否跟单")]
        [Column("isenable")]
        public virtual System.Nullable<Boolean> IsEnable
        {
            get
            {
                return _IsEnable;
            }
            set
            {
                if ((_IsEnable != value))
                {
                    SendPropertyChanging("IsEnable",_IsEnable,value);
                    _IsEnable = value;
                    SendPropertyChanged("IsEnable");
                }
            }
        }
        System.Nullable<DateTime> _StartTime;
        /// <summary>
        /// 有效期
        /// </summary>
        [Display(Name = "有效期")]
        [Column("starttime")]
        public virtual System.Nullable<DateTime> StartTime
        {
            get
            {
                return _StartTime;
            }
            set
            {
                if ((_StartTime != value))
                {
                    SendPropertyChanging("StartTime",_StartTime,value);
                    _StartTime = value;
                    SendPropertyChanged("StartTime");
                }
            }
        }
        System.Nullable<DateTime> _EndTime;
        /// <summary>
        /// 有效期
        /// </summary>
        [Display(Name = "有效期")]
        [Column("endtime")]
        public virtual System.Nullable<DateTime> EndTime
        {
            get
            {
                return _EndTime;
            }
            set
            {
                if ((_EndTime != value))
                {
                    SendPropertyChanging("EndTime",_EndTime,value);
                    _EndTime = value;
                    SendPropertyChanged("EndTime");
                }
            }
        }
        System.Nullable<Int32> _Lots;
        /// <summary>
        /// 固定跟几手
        /// null表示不固定，Lots和Multiple只能有一个有值
        /// </summary>
        [Display(Name = "固定跟几手  null表示不固定，Lots和Multiple只能有一个有值")]
        [Column("lots")]
        public virtual System.Nullable<Int32> Lots
        {
            get
            {
                return _Lots;
            }
            set
            {
                if ((_Lots != value))
                {
                    SendPropertyChanging("Lots",_Lots,value);
                    _Lots = value;
                    SendPropertyChanged("Lots");
                }
            }
        }
        System.Nullable<Int32> _Multiple;
        /// <summary>
        /// 跟几倍
        /// null表示不固定，Lots和Multiple只能有一个有值
        /// </summary>
        [Display(Name = "跟几倍  null表示不固定，Lots和Multiple只能有一个有值")]
        [Column("multiple")]
        public virtual System.Nullable<Int32> Multiple
        {
            get
            {
                return _Multiple;
            }
            set
            {
                if ((_Multiple != value))
                {
                    SendPropertyChanging("Multiple",_Multiple,value);
                    _Multiple = value;
                    SendPropertyChanged("Multiple");
                }
            }
        }
        FllowOrderSetting_DirectionEnum? _Direction=(FllowOrderSetting_DirectionEnum?)(1);
        /// <summary>
        /// 跟单方向
        /// </summary>
        [Display(Name = "跟单方向")]
        [Column("direction")]
        public virtual FllowOrderSetting_DirectionEnum? Direction
        {
            get
            {
                return _Direction;
            }
            set
            {
                if ((_Direction != value))
                {
                    SendPropertyChanging("Direction",_Direction,value);
                    _Direction = value;
                    SendPropertyChanged("Direction");
                }
            }
        }
        System.Nullable<double> _DayTotalAmount;
        /// <summary>
        /// 单日跟单总额
        /// </summary>
        [Display(Name = "单日跟单总额")]
        [Column("daytotalamount")]
        public virtual System.Nullable<double> DayTotalAmount
        {
            get
            {
                return _DayTotalAmount;
            }
            set
            {
                if ((_DayTotalAmount != value))
                {
                    SendPropertyChanging("DayTotalAmount",_DayTotalAmount,value);
                    _DayTotalAmount = value;
                    SendPropertyChanged("DayTotalAmount");
                }
            }
        }
        System.Nullable<double> _TotalAmount;
        /// <summary>
        /// 跟单总金额
        /// </summary>
        [Display(Name = "跟单总金额")]
        [Column("totalamount")]
        public virtual System.Nullable<double> TotalAmount
        {
            get
            {
                return _TotalAmount;
            }
            set
            {
                if ((_TotalAmount != value))
                {
                    SendPropertyChanging("TotalAmount",_TotalAmount,value);
                    _TotalAmount = value;
                    SendPropertyChanged("TotalAmount");
                }
            }
        }
        System.Nullable<double> _PositionRate;
        /// <summary>
        /// 最大持仓比例
        /// 即今日跟单持仓金额占跟单金额的比例
        /// </summary>
        [Display(Name = "最大持仓比例  即今日跟单持仓金额占跟单金额的比例")]
        [Column("positionrate")]
        public virtual System.Nullable<double> PositionRate
        {
            get
            {
                return _PositionRate;
            }
            set
            {
                if ((_PositionRate != value))
                {
                    SendPropertyChanging("PositionRate",_PositionRate,value);
                    _PositionRate = value;
                    SendPropertyChanged("PositionRate");
                }
            }
        }
        System.Nullable<Boolean> _IsFllowAll=true;
        /// <summary>
        /// 是否跟随所有产品
        /// </summary>
        [Display(Name = "是否跟随所有产品")]
        [Column("isfllowall")]
        public virtual System.Nullable<Boolean> IsFllowAll
        {
            get
            {
                return _IsFllowAll;
            }
            set
            {
                if ((_IsFllowAll != value))
                {
                    SendPropertyChanging("IsFllowAll",_IsFllowAll,value);
                    _IsFllowAll = value;
                    SendPropertyChanged("IsFllowAll");
                }
            }
        }
        FllowOrderSetting_TypeEnum? _Type;
        [Column("type")]
        public virtual FllowOrderSetting_TypeEnum? Type
        {
            get
            {
                return _Type;
            }
            set
            {
                if ((_Type != value))
                {
                    SendPropertyChanging("Type",_Type,value);
                    _Type = value;
                    SendPropertyChanged("Type");
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
                    SendPropertyChanging("CreateTime",_CreateTime,value);
                    _CreateTime = value;
                    SendPropertyChanged("CreateTime");
                }
            }
        }
        [ForeignKey("TradeUserId")]
        public virtual TradeUser TradeUser { get; set; }
        [ForeignKey("UserId")]
        public virtual UserInfo NickNameInfo { get; set; }
    }
    /// <summary>
    /// 系统后台配置的跟单
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type" , AutoSetPropertyValueOnInsert=FllowOrderSetting_TypeEnum.SystemFllowSetting)]
    public class SystemFllowSetting :FllowOrderSetting
    {
        System.Nullable<Int32> _DelayStart;
        /// <summary>
        /// 延迟范围
        /// </summary>
        [Display(Name = "延迟范围")]
        [Column("delaystart")]
        public virtual System.Nullable<Int32> DelayStart
        {
            get
            {
                return _DelayStart;
            }
            set
            {
                if ((_DelayStart != value))
                {
                    SendPropertyChanging("DelayStart",_DelayStart,value);
                    _DelayStart = value;
                    SendPropertyChanged("DelayStart");
                }
            }
        }
        System.Nullable<double> _DelayRate;
        /// <summary>
        /// 延迟比例
        /// </summary>
        [Display(Name = "延迟比例")]
        [Column("delayrate")]
        public virtual System.Nullable<double> DelayRate
        {
            get
            {
                return _DelayRate;
            }
            set
            {
                if ((_DelayRate != value))
                {
                    SendPropertyChanging("DelayRate",_DelayRate,value);
                    _DelayRate = value;
                    SendPropertyChanged("DelayRate");
                }
            }
        }
        System.Nullable<double> _FllowRate;
        /// <summary>
        /// 订单比例
        /// </summary>
        [Display(Name = "订单比例")]
        [Column("fllowrate")]
        public virtual System.Nullable<double> FllowRate
        {
            get
            {
                return _FllowRate;
            }
            set
            {
                if ((_FllowRate != value))
                {
                    SendPropertyChanging("FllowRate",_FllowRate,value);
                    _FllowRate = value;
                    SendPropertyChanged("FllowRate");
                }
            }
        }
        System.Nullable<Int32> _LotStart;
        /// <summary>
        /// 保证金范围
        /// </summary>
        [Display(Name = "保证金范围")]
        [Column("lotstart")]
        public virtual System.Nullable<Int32> LotStart
        {
            get
            {
                return _LotStart;
            }
            set
            {
                if ((_LotStart != value))
                {
                    SendPropertyChanging("LotStart",_LotStart,value);
                    _LotStart = value;
                    SendPropertyChanged("LotStart");
                }
            }
        }
        System.Nullable<Int32> _LotEnd;
        /// <summary>
        /// 保证金范围
        /// </summary>
        [Display(Name = "保证金范围")]
        [Column("lotend")]
        public virtual System.Nullable<Int32> LotEnd
        {
            get
            {
                return _LotEnd;
            }
            set
            {
                if ((_LotEnd != value))
                {
                    SendPropertyChanging("LotEnd",_LotEnd,value);
                    _LotEnd = value;
                    SendPropertyChanged("LotEnd");
                }
            }
        }
        System.Nullable<Int32> _DelayEnd;
        /// <summary>
        /// 延迟范围
        /// </summary>
        [Display(Name = "延迟范围")]
        [Column("delayend")]
        public virtual System.Nullable<Int32> DelayEnd
        {
            get
            {
                return _DelayEnd;
            }
            set
            {
                if ((_DelayEnd != value))
                {
                    SendPropertyChanging("DelayEnd",_DelayEnd,value);
                    _DelayEnd = value;
                    SendPropertyChanged("DelayEnd");
                }
            }
        }
        System.Nullable<double> _StoplossStart;
        /// <summary>
        /// 止损范围
        /// </summary>
        [Display(Name = "止损范围")]
        [Column("stoplossstart")]
        public virtual System.Nullable<double> StoplossStart
        {
            get
            {
                return _StoplossStart;
            }
            set
            {
                if ((_StoplossStart != value))
                {
                    SendPropertyChanging("StoplossStart",_StoplossStart,value);
                    _StoplossStart = value;
                    SendPropertyChanged("StoplossStart");
                }
            }
        }
        System.Nullable<double> _StoplossEnd;
        /// <summary>
        /// 止损范围
        /// </summary>
        [Display(Name = "止损范围")]
        [Column("stoplossend")]
        public virtual System.Nullable<double> StoplossEnd
        {
            get
            {
                return _StoplossEnd;
            }
            set
            {
                if ((_StoplossEnd != value))
                {
                    SendPropertyChanging("StoplossEnd",_StoplossEnd,value);
                    _StoplossEnd = value;
                    SendPropertyChanged("StoplossEnd");
                }
            }
        }
        System.Nullable<double> _StopProfitStart;
        /// <summary>
        /// 止盈范围
        /// </summary>
        [Display(Name = "止盈范围")]
        [Column("stopprofitstart")]
        public virtual System.Nullable<double> StopProfitStart
        {
            get
            {
                return _StopProfitStart;
            }
            set
            {
                if ((_StopProfitStart != value))
                {
                    SendPropertyChanging("StopProfitStart",_StopProfitStart,value);
                    _StopProfitStart = value;
                    SendPropertyChanged("StopProfitStart");
                }
            }
        }
        System.Nullable<double> _StopProfitEnd;
        /// <summary>
        /// 止盈范围
        /// </summary>
        [Display(Name = "止盈范围")]
        [Column("stopprofitend")]
        public virtual System.Nullable<double> StopProfitEnd
        {
            get
            {
                return _StopProfitEnd;
            }
            set
            {
                if ((_StopProfitEnd != value))
                {
                    SendPropertyChanging("StopProfitEnd",_StopProfitEnd,value);
                    _StopProfitEnd = value;
                    SendPropertyChanged("StopProfitEnd");
                }
            }
        }
        System.Nullable<double> _StopProfitStartOnMargin;
        /// <summary>
        /// 追加减少保证金时止盈范围
        /// </summary>
        [Display(Name = "追加减少保证金时止盈范围")]
        [Column("stopprofitstartonmargin")]
        public virtual System.Nullable<double> StopProfitStartOnMargin
        {
            get
            {
                return _StopProfitStartOnMargin;
            }
            set
            {
                if ((_StopProfitStartOnMargin != value))
                {
                    SendPropertyChanging("StopProfitStartOnMargin",_StopProfitStartOnMargin,value);
                    _StopProfitStartOnMargin = value;
                    SendPropertyChanged("StopProfitStartOnMargin");
                }
            }
        }
        System.Nullable<double> _StopProfitEndOnMargin;
        /// <summary>
        /// 追加减少保证金时止盈范围
        /// </summary>
        [Display(Name = "追加减少保证金时止盈范围")]
        [Column("stopprofitendonmargin")]
        public virtual System.Nullable<double> StopProfitEndOnMargin
        {
            get
            {
                return _StopProfitEndOnMargin;
            }
            set
            {
                if ((_StopProfitEndOnMargin != value))
                {
                    SendPropertyChanging("StopProfitEndOnMargin",_StopProfitEndOnMargin,value);
                    _StopProfitEndOnMargin = value;
                    SendPropertyChanged("StopProfitEndOnMargin");
                }
            }
        }
    }
    /// <summary>
    /// 用户跟单
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type" , AutoSetPropertyValueOnInsert=FllowOrderSetting_TypeEnum.UserFllowSetting)]
    public class UserFllowSetting :FllowOrderSetting
    {
        System.Nullable<double> _OrderPriceRate=0.002;
        /// <summary>
        /// 成交价格保护比例
        /// </summary>
        [Display(Name = "成交价格保护比例")]
        [Column("orderpricerate")]
        public virtual System.Nullable<double> OrderPriceRate
        {
            get
            {
                return _OrderPriceRate;
            }
            set
            {
                if ((_OrderPriceRate != value))
                {
                    SendPropertyChanging("OrderPriceRate",_OrderPriceRate,value);
                    _OrderPriceRate = value;
                    SendPropertyChanged("OrderPriceRate");
                }
            }
        }
        String _Language;
        /// <summary>
        /// 订阅时的语言
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "订阅时的语言")]
        [Column("language")]
        public virtual String Language
        {
            get
            {
                return _Language;
            }
            set
            {
                if ((_Language != value))
                {
                    SendPropertyChanging("Language",_Language,value);
                    _Language = value;
                    SendPropertyChanged("Language");
                }
            }
        }
        Boolean _IsSendMessage=true;
        /// <summary>
        /// 是否发送推送信息
        /// </summary>
        [DisallowNull]
        [Display(Name = "是否发送推送信息")]
        [Column("issendmessage")]
        public virtual Boolean IsSendMessage
        {
            get
            {
                return _IsSendMessage;
            }
            set
            {
                if ((_IsSendMessage != value))
                {
                    SendPropertyChanging("IsSendMessage",_IsSendMessage,value);
                    _IsSendMessage = value;
                    SendPropertyChanged("IsSendMessage");
                }
            }
        }
        FllowOrderSetting_PushedMsgFlagEnum _PushedMsgFlag=(FllowOrderSetting_PushedMsgFlagEnum)(0);
        /// <summary>
        /// 已经推送了哪些消息
        /// </summary>
        [DisallowNull]
        [Display(Name = "已经推送了哪些消息")]
        [Column("pushedmsgflag")]
        public virtual FllowOrderSetting_PushedMsgFlagEnum PushedMsgFlag
        {
            get
            {
                return _PushedMsgFlag;
            }
            set
            {
                if ((_PushedMsgFlag != value))
                {
                    SendPropertyChanging("PushedMsgFlag",_PushedMsgFlag,value);
                    _PushedMsgFlag = value;
                    SendPropertyChanged("PushedMsgFlag");
                }
            }
        }
    }
    public enum FllowOrderSetting_DirectionEnum:int
    {
        /// <summary>
        /// 正向
        /// </summary>
        Forward = 1,
        /// <summary>
        /// 反向
        /// </summary>
        backward = 2
    }
    public enum FllowOrderSetting_TypeEnum:int
    {
        /// <summary>
        /// 系统后台配置的跟单
        /// </summary>
        SystemFllowSetting = 1,
        /// <summary>
        /// 用户跟单
        /// </summary>
        UserFllowSetting = 2
    }
    public enum FllowOrderSetting_PushedMsgFlagEnum:int
    {
        /// <summary>
        /// 订阅后24h，未开启跟单
        /// </summary>
        Sub24 = 1,
        /// <summary>
        /// 订阅后72h，未开启跟单
        /// </summary>
        Sub72 = 1<<1,
        /// <summary>
        /// 订阅还有3天到期
        /// </summary>
        SubOvr3 = 1<<2,
        /// <summary>
        /// 订阅还有1天到期
        /// </summary>
        SubOvr1 = 1<<3,
        /// <summary>
        /// 订阅过期后
        /// </summary>
        SubOvrTime = 1<<4,
        /// <summary>
        /// 订阅过期3天
        /// </summary>
        SubOvrDay3 = 1<<5
    }
    /// <summary>
    /// 设置跟随什么产品
    /// </summary>
    [TableConfig]
    [Table("fllowproduct")]
    [Way.EntityDB.DataItemJsonConverter]
    public class FllowProduct :Way.EntityDB.DataItem
    {
        System.Nullable<Int32> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int32> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id",_id,value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        Int32 _FllowSettingId;
        [DisallowNull]
        [Column("fllowsettingid")]
        public virtual Int32 FllowSettingId
        {
            get
            {
                return _FllowSettingId;
            }
            set
            {
                if ((_FllowSettingId != value))
                {
                    SendPropertyChanging("FllowSettingId",_FllowSettingId,value);
                    _FllowSettingId = value;
                    SendPropertyChanged("FllowSettingId");
                }
            }
        }
        String _Symbol;
        [MaxLength(50)]
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
                    SendPropertyChanging("Symbol",_Symbol,value);
                    _Symbol = value;
                    SendPropertyChanged("Symbol");
                }
            }
        }
        System.Nullable<Int32> _Leverage;
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
                    SendPropertyChanging("Leverage",_Leverage,value);
                    _Leverage = value;
                    SendPropertyChanged("Leverage");
                }
            }
        }
    }
    /// <summary>
    /// 跟单记录
    /// </summary>
    [TableConfig( AutoSetPropertyNameOnInsert = "Type" , AutoSetPropertyValueOnInsert=(FllowHistory_TypeEnum)0)]
    [Table("fllowhistory")]
    [Way.EntityDB.DataItemJsonConverter]
    public class FllowHistory :Way.EntityDB.DataItem
    {
        System.Nullable<Int32> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int32> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id",_id,value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        Int64 _TradeUserId;
        /// <summary>
        /// 交易员
        /// </summary>
        [DisallowNull]
        [Display(Name = "交易员")]
        [Column("tradeuserid")]
        public virtual Int64 TradeUserId
        {
            get
            {
                return _TradeUserId;
            }
            set
            {
                if ((_TradeUserId != value))
                {
                    SendPropertyChanging("TradeUserId",_TradeUserId,value);
                    _TradeUserId = value;
                    SendPropertyChanged("TradeUserId");
                }
            }
        }
        Int64 _UserId;
        /// <summary>
        /// 跟单人
        /// </summary>
        [DisallowNull]
        [Display(Name = "跟单人")]
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
                    SendPropertyChanging("UserId",_UserId,value);
                    _UserId = value;
                    SendPropertyChanged("UserId");
                }
            }
        }
        System.Nullable<Int64> _OriginalOrderId;
        /// <summary>
        /// 交易员的订单号
        /// </summary>
        [Display(Name = "交易员的订单号")]
        [Column("originalorderid")]
        public virtual System.Nullable<Int64> OriginalOrderId
        {
            get
            {
                return _OriginalOrderId;
            }
            set
            {
                if ((_OriginalOrderId != value))
                {
                    SendPropertyChanging("OriginalOrderId",_OriginalOrderId,value);
                    _OriginalOrderId = value;
                    SendPropertyChanged("OriginalOrderId");
                }
            }
        }
        FllowHistory_TypeEnum? _Type;
        [Column("type")]
        public virtual FllowHistory_TypeEnum? Type
        {
            get
            {
                return _Type;
            }
            set
            {
                if ((_Type != value))
                {
                    SendPropertyChanging("Type",_Type,value);
                    _Type = value;
                    SendPropertyChanged("Type");
                }
            }
        }
        String _TradeUserName;
        /// <summary>
        /// 交易员名称
        /// 程序自用
        /// </summary>
        [MaxLength(1)]
        [Display(Name = "交易员名称  程序自用")]
        [Column("tradeusername")]
        public virtual String TradeUserName
        {
            get
            {
                return _TradeUserName;
            }
            set
            {
                if ((_TradeUserName != value))
                {
                    SendPropertyChanging("TradeUserName",_TradeUserName,value);
                    _TradeUserName = value;
                    SendPropertyChanged("TradeUserName");
                }
            }
        }
        System.Nullable<DateTime> _Time;
        [Column("time")]
        public virtual System.Nullable<DateTime> Time
        {
            get
            {
                return _Time;
            }
            set
            {
                if ((_Time != value))
                {
                    SendPropertyChanging("Time",_Time,value);
                    _Time = value;
                    SendPropertyChanged("Time");
                }
            }
        }
    }
    [TableConfig(AutoSetPropertyNameOnInsert = "Type" , AutoSetPropertyValueOnInsert=FllowHistory_TypeEnum.FailFllowHistory)]
    public class FailFllowHistory :FllowHistory
    {
        FllowHistory_OpenFailReasonEnum? _OpenFailReason;
        /// <summary>
        /// 开仓失败原因
        /// </summary>
        [Display(Name = "开仓失败原因")]
        [Column("openfailreason")]
        public virtual FllowHistory_OpenFailReasonEnum? OpenFailReason
        {
            get
            {
                return _OpenFailReason;
            }
            set
            {
                if ((_OpenFailReason != value))
                {
                    SendPropertyChanging("OpenFailReason",_OpenFailReason,value);
                    _OpenFailReason = value;
                    SendPropertyChanged("OpenFailReason");
                }
            }
        }
    }
    /// <summary>
    /// 用户成功的跟单
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type" , AutoSetPropertyValueOnInsert=FllowHistory_TypeEnum.SuccessFllowHistory)]
    public class SuccessFllowHistory :FllowHistory
    {
        System.Nullable<Int64> _OrderId;
        /// <summary>
        /// 跟单的订单号
        /// </summary>
        [Display(Name = "跟单的订单号")]
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
                    SendPropertyChanging("OrderId",_OrderId,value);
                    _OrderId = value;
                    SendPropertyChanged("OrderId");
                }
            }
        }
        System.Nullable<double> _Profit;
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
                    SendPropertyChanging("Profit",_Profit,value);
                    _Profit = value;
                    SendPropertyChanged("Profit");
                }
            }
        }
        FllowHistory_CloseTypeEnum? _CloseType;
        /// <summary>
        /// 平仓类型
        /// </summary>
        [Display(Name = "平仓类型")]
        [Column("closetype")]
        public virtual FllowHistory_CloseTypeEnum? CloseType
        {
            get
            {
                return _CloseType;
            }
            set
            {
                if ((_CloseType != value))
                {
                    SendPropertyChanging("CloseType",_CloseType,value);
                    _CloseType = value;
                    SendPropertyChanged("CloseType");
                }
            }
        }
        System.Nullable<double> _OrderAmount;
        /// <summary>
        /// 下单金额
        /// </summary>
        [Display(Name = "下单金额")]
        [Column("orderamount")]
        public virtual System.Nullable<double> OrderAmount
        {
            get
            {
                return _OrderAmount;
            }
            set
            {
                if ((_OrderAmount != value))
                {
                    SendPropertyChanging("OrderAmount",_OrderAmount,value);
                    _OrderAmount = value;
                    SendPropertyChanged("OrderAmount");
                }
            }
        }
        System.Nullable<double> _ProfitRate;
        /// <summary>
        /// 收益率
        /// </summary>
        [Display(Name = "收益率")]
        [Column("profitrate")]
        public virtual System.Nullable<double> ProfitRate
        {
            get
            {
                return _ProfitRate;
            }
            set
            {
                if ((_ProfitRate != value))
                {
                    SendPropertyChanging("ProfitRate",_ProfitRate,value);
                    _ProfitRate = value;
                    SendPropertyChanged("ProfitRate");
                }
            }
        }
        System.Nullable<double> _OpenPrice;
        /// <summary>
        /// 开仓价
        /// </summary>
        [Display(Name = "开仓价")]
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
                    SendPropertyChanging("OpenPrice",_OpenPrice,value);
                    _OpenPrice = value;
                    SendPropertyChanged("OpenPrice");
                }
            }
        }
        System.Nullable<double> _ClosePrice;
        /// <summary>
        /// 平仓价
        /// </summary>
        [Display(Name = "平仓价")]
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
                    SendPropertyChanging("ClosePrice",_ClosePrice,value);
                    _ClosePrice = value;
                    SendPropertyChanged("ClosePrice");
                }
            }
        }
        System.Nullable<Boolean> _IsFllowDisable=false;
        /// <summary>
        /// 是否已经停止自动跟单
        /// </summary>
        [Display(Name = "是否已经停止自动跟单")]
        [Column("isfllowdisable")]
        public virtual System.Nullable<Boolean> IsFllowDisable
        {
            get
            {
                return _IsFllowDisable;
            }
            set
            {
                if ((_IsFllowDisable != value))
                {
                    SendPropertyChanging("IsFllowDisable",_IsFllowDisable,value);
                    _IsFllowDisable = value;
                    SendPropertyChanged("IsFllowDisable");
                }
            }
        }
        System.Nullable<Int32> _BsType;
        /// <summary>
        /// 下单方向
        /// </summary>
        [Display(Name = "下单方向")]
        [Column("bstype")]
        public virtual System.Nullable<Int32> BsType
        {
            get
            {
                return _BsType;
            }
            set
            {
                if ((_BsType != value))
                {
                    SendPropertyChanging("BsType",_BsType,value);
                    _BsType = value;
                    SendPropertyChanged("BsType");
                }
            }
        }
        System.Nullable<Int32> _Leverage;
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
                    SendPropertyChanging("Leverage",_Leverage,value);
                    _Leverage = value;
                    SendPropertyChanged("Leverage");
                }
            }
        }
        String _Symbol;
        [MaxLength(50)]
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
                    SendPropertyChanging("Symbol",_Symbol,value);
                    _Symbol = value;
                    SendPropertyChanged("Symbol");
                }
            }
        }
        System.Nullable<Int32> _Lots;
        /// <summary>
        /// 手数
        /// </summary>
        [Display(Name = "手数")]
        [Column("lots")]
        public virtual System.Nullable<Int32> Lots
        {
            get
            {
                return _Lots;
            }
            set
            {
                if ((_Lots != value))
                {
                    SendPropertyChanging("Lots",_Lots,value);
                    _Lots = value;
                    SendPropertyChanged("Lots");
                }
            }
        }
        System.Nullable<DateTime> _OpenTime;
        /// <summary>
        /// 开仓时间
        /// </summary>
        [Display(Name = "开仓时间")]
        [Column("opentime")]
        public virtual System.Nullable<DateTime> OpenTime
        {
            get
            {
                return _OpenTime;
            }
            set
            {
                if ((_OpenTime != value))
                {
                    SendPropertyChanging("OpenTime",_OpenTime,value);
                    _OpenTime = value;
                    SendPropertyChanged("OpenTime");
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
                    SendPropertyChanging("CloseTime",_CloseTime,value);
                    _CloseTime = value;
                    SendPropertyChanged("CloseTime");
                }
            }
        }
    }
    /// <summary>
    /// 系统的跟单
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type" , AutoSetPropertyValueOnInsert=FllowHistory_TypeEnum.SysFllowHistory)]
    public class SysFllowHistory :SuccessFllowHistory
    {
        System.Nullable<Boolean> _IsSetedProfitLoss;
        /// <summary>
        /// 是否已经设置了止盈止损
        /// </summary>
        [Display(Name = "是否已经设置了止盈止损")]
        [Column("issetedprofitloss")]
        public virtual System.Nullable<Boolean> IsSetedProfitLoss
        {
            get
            {
                return _IsSetedProfitLoss;
            }
            set
            {
                if ((_IsSetedProfitLoss != value))
                {
                    SendPropertyChanging("IsSetedProfitLoss",_IsSetedProfitLoss,value);
                    _IsSetedProfitLoss = value;
                    SendPropertyChanged("IsSetedProfitLoss");
                }
            }
        }
    }
    /// <summary>
    /// 用户的跟单
    /// </summary>
    [TableConfig(AutoSetPropertyNameOnInsert = "Type" , AutoSetPropertyValueOnInsert=FllowHistory_TypeEnum.UserFllowHistory)]
    public class UserFllowHistory :SuccessFllowHistory
    {
    }
    public enum FllowHistory_CloseTypeEnum:int
    {
        /// <summary>
        /// 跟单平仓
        /// </summary>
        FllowTradeUser = 1,
        /// <summary>
        /// 系统强平
        /// </summary>
        SystemClose = 2,
        /// <summary>
        /// 手动平仓
        /// </summary>
        Manual = 3
    }
    public enum FllowHistory_OpenFailReasonEnum:int
    {
        /// <summary>
        /// 账户资金不足
        /// </summary>
        NotEnoughMoney = 1,
        /// <summary>
        /// 跟单本金限制
        /// </summary>
        TotalAmountLimit = 2,
        /// <summary>
        /// 单日跟单本金限制
        /// </summary>
        DayAmountLimit = 3,
        /// <summary>
        /// 最大持仓比例限制
        /// </summary>
        PositionRateLimit = 4,
        /// <summary>
        /// 成交价格保护限制
        /// </summary>
        PriceProtect = 5,
        /// <summary>
        /// 跟单风控限制
        /// </summary>
        RiskControl = 6,
        /// <summary>
        /// 网络异常
        /// </summary>
        NetworkError =7,
        /// <summary>
        /// 订单撮合时间超时
        /// </summary>
        Timeout = 8
    }
    public enum FllowHistory_TypeEnum:int
    {
        FailFllowHistory = 1,
        /// <summary>
        /// 用户成功的跟单
        /// </summary>
        SuccessFllowHistory = 1<<1,
        /// <summary>
        /// 系统的跟单
        /// </summary>
        SysFllowHistory = SuccessFllowHistory | 1<<2,
        /// <summary>
        /// 用户的跟单
        /// </summary>
        UserFllowHistory = SuccessFllowHistory | 1<<3
    }
    /// <summary>
    /// 消费记录
    /// </summary>
    [TableConfig]
    [Table("consumerecord")]
    [Way.EntityDB.DataItemJsonConverter]
    public class ConsumeRecord :Way.EntityDB.DataItem
    {
        System.Nullable<Int32> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int32> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id",_id,value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        Int64 _TradeUserId;
        /// <summary>
        /// 收款人
        /// </summary>
        [DisallowNull]
        [Display(Name = "收款人")]
        [Column("tradeuserid")]
        public virtual Int64 TradeUserId
        {
            get
            {
                return _TradeUserId;
            }
            set
            {
                if ((_TradeUserId != value))
                {
                    SendPropertyChanging("TradeUserId",_TradeUserId,value);
                    _TradeUserId = value;
                    SendPropertyChanged("TradeUserId");
                }
            }
        }
        Int64 _UserId;
        /// <summary>
        /// 付款人
        /// </summary>
        [DisallowNull]
        [Display(Name = "付款人")]
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
                    SendPropertyChanging("UserId",_UserId,value);
                    _UserId = value;
                    SendPropertyChanged("UserId");
                }
            }
        }
        System.Nullable<DateTime> _PayTime;
        /// <summary>
        /// 时间
        /// </summary>
        [Display(Name = "时间")]
        [Column("paytime")]
        public virtual System.Nullable<DateTime> PayTime
        {
            get
            {
                return _PayTime;
            }
            set
            {
                if ((_PayTime != value))
                {
                    SendPropertyChanging("PayTime",_PayTime,value);
                    _PayTime = value;
                    SendPropertyChanged("PayTime");
                }
            }
        }
        System.Nullable<Boolean> _IsConfirm=false;
        /// <summary>
        /// 资金是否已经打给交易员
        /// </summary>
        [Display(Name = "资金是否已经打给交易员")]
        [Column("isconfirm")]
        public virtual System.Nullable<Boolean> IsConfirm
        {
            get
            {
                return _IsConfirm;
            }
            set
            {
                if ((_IsConfirm != value))
                {
                    SendPropertyChanging("IsConfirm",_IsConfirm,value);
                    _IsConfirm = value;
                    SendPropertyChanged("IsConfirm");
                }
            }
        }
        System.Nullable<double> _Amount;
        /// <summary>
        /// 金额
        /// </summary>
        [Display(Name = "金额")]
        [Column("amount")]
        public virtual System.Nullable<double> Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                if ((_Amount != value))
                {
                    SendPropertyChanging("Amount",_Amount,value);
                    _Amount = value;
                    SendPropertyChanged("Amount");
                }
            }
        }
        System.Nullable<DateTime> _ToBalanceTime;
        /// <summary>
        /// 转入余额时间
        /// 预计转入余额的时间，没转入之前，这个字段也是有值的
        /// </summary>
        [Display(Name = "转入余额时间  预计转入余额的时间，没转入之前，这个字段也是有值的")]
        [Column("tobalancetime")]
        public virtual System.Nullable<DateTime> ToBalanceTime
        {
            get
            {
                return _ToBalanceTime;
            }
            set
            {
                if ((_ToBalanceTime != value))
                {
                    SendPropertyChanging("ToBalanceTime",_ToBalanceTime,value);
                    _ToBalanceTime = value;
                    SendPropertyChanged("ToBalanceTime");
                }
            }
        }
        String _TransferVoucher;
        /// <summary>
        /// 转账凭证
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "转账凭证")]
        [Column("transfervoucher")]
        public virtual String TransferVoucher
        {
            get
            {
                return _TransferVoucher;
            }
            set
            {
                if ((_TransferVoucher != value))
                {
                    SendPropertyChanging("TransferVoucher",_TransferVoucher,value);
                    _TransferVoucher = value;
                    SendPropertyChanged("TransferVoucher");
                }
            }
        }
    }
    /// <summary>
    /// 关注列表
    /// </summary>
    [TableConfig]
    [Table("concern")]
    [Way.EntityDB.DataItemJsonConverter]
    public class Concern :Way.EntityDB.DataItem
    {
        System.Nullable<Int32> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int32> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id",_id,value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        Int64 _TradeUserId;
        /// <summary>
        /// 交易员
        /// </summary>
        [DisallowNull]
        [Display(Name = "交易员")]
        [Column("tradeuserid")]
        public virtual Int64 TradeUserId
        {
            get
            {
                return _TradeUserId;
            }
            set
            {
                if ((_TradeUserId != value))
                {
                    SendPropertyChanging("TradeUserId",_TradeUserId,value);
                    _TradeUserId = value;
                    SendPropertyChanged("TradeUserId");
                }
            }
        }
        Int64 _UserId;
        /// <summary>
        /// 用户
        /// </summary>
        [DisallowNull]
        [Display(Name = "用户")]
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
                    SendPropertyChanging("UserId",_UserId,value);
                    _UserId = value;
                    SendPropertyChanged("UserId");
                }
            }
        }
    }
    /// <summary>
    /// 用户的交易对胜率
    /// </summary>
    [TableConfig]
    [Table("symbolwinrate")]
    [Way.EntityDB.DataItemJsonConverter]
    public class SymbolWinRate :Way.EntityDB.DataItem
    {
        System.Nullable<Int32> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int32> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id",_id,value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        Int64 _TradeUserId;
        /// <summary>
        /// 用户id
        /// </summary>
        [DisallowNull]
        [Display(Name = "用户id")]
        [Column("tradeuserid")]
        public virtual Int64 TradeUserId
        {
            get
            {
                return _TradeUserId;
            }
            set
            {
                if ((_TradeUserId != value))
                {
                    SendPropertyChanging("TradeUserId",_TradeUserId,value);
                    _TradeUserId = value;
                    SendPropertyChanged("TradeUserId");
                }
            }
        }
        String _Symbol;
        /// <summary>
        /// 交易对
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "交易对")]
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
                    SendPropertyChanging("Symbol",_Symbol,value);
                    _Symbol = value;
                    SendPropertyChanged("Symbol");
                }
            }
        }
        System.Nullable<double> _WinRate;
        /// <summary>
        /// 胜率
        /// </summary>
        [Display(Name = "胜率")]
        [Column("winrate")]
        public virtual System.Nullable<double> WinRate
        {
            get
            {
                return _WinRate;
            }
            set
            {
                if ((_WinRate != value))
                {
                    SendPropertyChanging("WinRate",_WinRate,value);
                    _WinRate = value;
                    SendPropertyChanged("WinRate");
                }
            }
        }
        System.Nullable<double> _ProfitRate;
        /// <summary>
        /// 累计收益率
        /// </summary>
        [Display(Name = "累计收益率")]
        [Column("profitrate")]
        public virtual System.Nullable<double> ProfitRate
        {
            get
            {
                return _ProfitRate;
            }
            set
            {
                if ((_ProfitRate != value))
                {
                    SendPropertyChanging("ProfitRate",_ProfitRate,value);
                    _ProfitRate = value;
                    SendPropertyChanged("ProfitRate");
                }
            }
        }
    }
    /// <summary>
    /// 用户收益表
    /// </summary>
    [TableConfig]
    [Table("userprofit")]
    [Way.EntityDB.DataItemJsonConverter]
    public class UserProfit :Way.EntityDB.DataItem
    {
        System.Nullable<Int32> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int32> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id",_id,value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        Int64 _TradeUserId;
        /// <summary>
        /// 用户id
        /// </summary>
        [DisallowNull]
        [Display(Name = "用户id")]
        [Column("tradeuserid")]
        public virtual Int64 TradeUserId
        {
            get
            {
                return _TradeUserId;
            }
            set
            {
                if ((_TradeUserId != value))
                {
                    SendPropertyChanging("TradeUserId",_TradeUserId,value);
                    _TradeUserId = value;
                    SendPropertyChanged("TradeUserId");
                }
            }
        }
        UserProfit_TypeEnum? _Type;
        /// <summary>
        /// 日期类型
        /// </summary>
        [Display(Name = "日期类型")]
        [Column("type")]
        public virtual UserProfit_TypeEnum? Type
        {
            get
            {
                return _Type;
            }
            set
            {
                if ((_Type != value))
                {
                    SendPropertyChanging("Type",_Type,value);
                    _Type = value;
                    SendPropertyChanged("Type");
                }
            }
        }
        System.Nullable<double> _Loss;
        /// <summary>
        /// 亏损
        /// </summary>
        [Display(Name = "亏损")]
        [Column("loss")]
        public virtual System.Nullable<double> Loss
        {
            get
            {
                return _Loss;
            }
            set
            {
                if ((_Loss != value))
                {
                    SendPropertyChanging("Loss",_Loss,value);
                    _Loss = value;
                    SendPropertyChanged("Loss");
                }
            }
        }
        System.Nullable<double> _Profit;
        /// <summary>
        /// 收益
        /// </summary>
        [Display(Name = "收益")]
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
                    SendPropertyChanging("Profit",_Profit,value);
                    _Profit = value;
                    SendPropertyChanged("Profit");
                }
            }
        }
        System.Nullable<double> _TotalProfit;
        /// <summary>
        /// 总盈亏
        /// </summary>
        [Display(Name = "总盈亏")]
        [Column("totalprofit")]
        public virtual System.Nullable<double> TotalProfit
        {
            get
            {
                return _TotalProfit;
            }
            set
            {
                if ((_TotalProfit != value))
                {
                    SendPropertyChanging("TotalProfit",_TotalProfit,value);
                    _TotalProfit = value;
                    SendPropertyChanged("TotalProfit");
                }
            }
        }
        String _DateText;
        /// <summary>
        /// 日期
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "日期")]
        [Column("datetext")]
        public virtual String DateText
        {
            get
            {
                return _DateText;
            }
            set
            {
                if ((_DateText != value))
                {
                    SendPropertyChanging("DateText",_DateText,value);
                    _DateText = value;
                    SendPropertyChanged("DateText");
                }
            }
        }
        System.Nullable<Int32> _Index;
        /// <summary>
        /// 排序索引
        /// </summary>
        [Display(Name = "排序索引")]
        [Column("index")]
        public virtual System.Nullable<Int32> Index
        {
            get
            {
                return _Index;
            }
            set
            {
                if ((_Index != value))
                {
                    SendPropertyChanging("Index",_Index,value);
                    _Index = value;
                    SendPropertyChanged("Index");
                }
            }
        }
        System.Nullable<Int32> _Year;
        [Column("year")]
        public virtual System.Nullable<Int32> Year
        {
            get
            {
                return _Year;
            }
            set
            {
                if ((_Year != value))
                {
                    SendPropertyChanging("Year",_Year,value);
                    _Year = value;
                    SendPropertyChanged("Year");
                }
            }
        }
        System.Nullable<double> _ProfitRate;
        /// <summary>
        /// 收益率
        /// </summary>
        [Display(Name = "收益率")]
        [Column("profitrate")]
        public virtual System.Nullable<double> ProfitRate
        {
            get
            {
                return _ProfitRate;
            }
            set
            {
                if ((_ProfitRate != value))
                {
                    SendPropertyChanging("ProfitRate",_ProfitRate,value);
                    _ProfitRate = value;
                    SendPropertyChanged("ProfitRate");
                }
            }
        }
    }
    public enum UserProfit_TypeEnum:int
    {
        /// <summary>
        /// 日
        /// </summary>
        Day = 1,
        /// <summary>
        /// 周
        /// </summary>
        Week=2,
        /// <summary>
        /// 月
        /// </summary>
        Month=3
    }
    /// <summary>
    /// 用户信息
    /// </summary>
    [TableConfig]
    [Table("userinfo")]
    [Way.EntityDB.DataItemJsonConverter]
    public class UserInfo :Way.EntityDB.DataItem
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
                    SendPropertyChanging("UserId",_UserId,value);
                    _UserId = value;
                    SendPropertyChanged("UserId");
                }
            }
        }
        String _NickName;
        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "昵称")]
        [Column("nickname")]
        public virtual String NickName
        {
            get
            {
                return _NickName;
            }
            set
            {
                if ((_NickName != value))
                {
                    SendPropertyChanging("NickName",_NickName,value);
                    _NickName = value;
                    SendPropertyChanged("NickName");
                }
            }
        }
        String _HeadIconUrl="https://filename.yjfzww.com/headsculpture/mo.png";
        /// <summary>
        /// 头像
        /// </summary>
        [MaxLength(100)]
        [Display(Name = "头像")]
        [Column("headiconurl")]
        public virtual String HeadIconUrl
        {
            get
            {
                return _HeadIconUrl;
            }
            set
            {
                if ((_HeadIconUrl != value))
                {
                    SendPropertyChanging("HeadIconUrl",_HeadIconUrl,value);
                    _HeadIconUrl = value;
                    SendPropertyChanged("HeadIconUrl");
                }
            }
        }
        System.Nullable<Boolean> _IsCanFllow=false;
        /// <summary>
        /// 是否允许自动跟单
        /// </summary>
        [Display(Name = "是否允许自动跟单")]
        [Column("iscanfllow")]
        public virtual System.Nullable<Boolean> IsCanFllow
        {
            get
            {
                return _IsCanFllow;
            }
            set
            {
                if ((_IsCanFllow != value))
                {
                    SendPropertyChanging("IsCanFllow",_IsCanFllow,value);
                    _IsCanFllow = value;
                    SendPropertyChanged("IsCanFllow");
                }
            }
        }
    }
    /// <summary>
    /// 记录一些交易员的订单
    /// </summary>
    [TableConfig]
    [Table("traderorder")]
    [Way.EntityDB.DataItemJsonConverter]
    public class TraderOrder :Way.EntityDB.DataItem
    {
        String _Symbol;
        /// <summary>
        /// 品种
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "品种")]
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
                    SendPropertyChanging("Symbol",_Symbol,value);
                    _Symbol = value;
                    SendPropertyChanged("Symbol");
                }
            }
        }
        System.Nullable<double> _OrderPrice;
        /// <summary>
        /// 开仓价
        /// </summary>
        [Display(Name = "开仓价")]
        [Column("orderprice")]
        public virtual System.Nullable<double> OrderPrice
        {
            get
            {
                return _OrderPrice;
            }
            set
            {
                if ((_OrderPrice != value))
                {
                    SendPropertyChanging("OrderPrice",_OrderPrice,value);
                    _OrderPrice = value;
                    SendPropertyChanged("OrderPrice");
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
                    SendPropertyChanging("Leverage",_Leverage,value);
                    _Leverage = value;
                    SendPropertyChanged("Leverage");
                }
            }
        }
        System.Nullable<Int32> _Bstype;
        /// <summary>
        /// 1-做多 2-做空
        /// </summary>
        [Display(Name = "1-做多 2-做空")]
        [Column("bstype")]
        public virtual System.Nullable<Int32> Bstype
        {
            get
            {
                return _Bstype;
            }
            set
            {
                if ((_Bstype != value))
                {
                    SendPropertyChanging("Bstype",_Bstype,value);
                    _Bstype = value;
                    SendPropertyChanged("Bstype");
                }
            }
        }
        System.Nullable<Int32> _Lots;
        /// <summary>
        /// 手数
        /// </summary>
        [Display(Name = "手数")]
        [Column("lots")]
        public virtual System.Nullable<Int32> Lots
        {
            get
            {
                return _Lots;
            }
            set
            {
                if ((_Lots != value))
                {
                    SendPropertyChanging("Lots",_Lots,value);
                    _Lots = value;
                    SendPropertyChanged("Lots");
                }
            }
        }
        System.Nullable<Int64> _TradeUserId;
        /// <summary>
        /// 交易员id
        /// </summary>
        [Display(Name = "交易员id")]
        [Column("tradeuserid")]
        public virtual System.Nullable<Int64> TradeUserId
        {
            get
            {
                return _TradeUserId;
            }
            set
            {
                if ((_TradeUserId != value))
                {
                    SendPropertyChanging("TradeUserId",_TradeUserId,value);
                    _TradeUserId = value;
                    SendPropertyChanged("TradeUserId");
                }
            }
        }
        Int64 _OrderId;
        /// <summary>
        /// 订单号
        /// </summary>
        [Key]
        [DisallowNull]
        [Display(Name = "订单号")]
        [Column("orderid")]
        public virtual Int64 OrderId
        {
            get
            {
                return _OrderId;
            }
            set
            {
                if ((_OrderId != value))
                {
                    SendPropertyChanging("OrderId",_OrderId,value);
                    _OrderId = value;
                    SendPropertyChanged("OrderId");
                }
            }
        }
        System.Nullable<double> _Profit;
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
                    SendPropertyChanging("Profit",_Profit,value);
                    _Profit = value;
                    SendPropertyChanged("Profit");
                }
            }
        }
        System.Nullable<double> _ProfitRate;
        /// <summary>
        /// 收益率
        /// </summary>
        [Display(Name = "收益率")]
        [Column("profitrate")]
        public virtual System.Nullable<double> ProfitRate
        {
            get
            {
                return _ProfitRate;
            }
            set
            {
                if ((_ProfitRate != value))
                {
                    SendPropertyChanging("ProfitRate",_ProfitRate,value);
                    _ProfitRate = value;
                    SendPropertyChanged("ProfitRate");
                }
            }
        }
        System.Nullable<double> _ClosePrice;
        /// <summary>
        /// 平仓价
        /// </summary>
        [Display(Name = "平仓价")]
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
                    SendPropertyChanging("ClosePrice",_ClosePrice,value);
                    _ClosePrice = value;
                    SendPropertyChanged("ClosePrice");
                }
            }
        }
        System.Nullable<DateTime> _OpenTime;
        /// <summary>
        /// 开仓时间
        /// </summary>
        [Display(Name = "开仓时间")]
        [Column("opentime")]
        public virtual System.Nullable<DateTime> OpenTime
        {
            get
            {
                return _OpenTime;
            }
            set
            {
                if ((_OpenTime != value))
                {
                    SendPropertyChanging("OpenTime",_OpenTime,value);
                    _OpenTime = value;
                    SendPropertyChanged("OpenTime");
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
                    SendPropertyChanging("CloseTime",_CloseTime,value);
                    _CloseTime = value;
                    SendPropertyChanged("CloseTime");
                }
            }
        }
        Int32 _AppendLots=0;
        /// <summary>
        /// 追加保证金手数
        /// </summary>
        [DisallowNull]
        [Display(Name = "追加保证金手数")]
        [Column("appendlots")]
        public virtual Int32 AppendLots
        {
            get
            {
                return _AppendLots;
            }
            set
            {
                if ((_AppendLots != value))
                {
                    SendPropertyChanging("AppendLots",_AppendLots,value);
                    _AppendLots = value;
                    SendPropertyChanged("AppendLots");
                }
            }
        }
        Int32 _TradeMode=0;
        /// <summary>
        /// 0=逐仓,1=全仓
        /// </summary>
        [DisallowNull]
        [Display(Name = "0=逐仓,1=全仓")]
        [Column("trademode")]
        public virtual Int32 TradeMode
        {
            get
            {
                return _TradeMode;
            }
            set
            {
                if ((_TradeMode != value))
                {
                    SendPropertyChanging("TradeMode",_TradeMode,value);
                    _TradeMode = value;
                    SendPropertyChanged("TradeMode");
                }
            }
        }
    }
    /// <summary>
    /// 总收益率记录
    /// </summary>
    [TableConfig]
    [Table("totalprofitratehistory")]
    [Way.EntityDB.DataItemJsonConverter]
    public class TotalProfitRateHistory :Way.EntityDB.DataItem
    {
        System.Nullable<Int32> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Column("id")]
        public virtual System.Nullable<Int32> id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((_id != value))
                {
                    SendPropertyChanging("id",_id,value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        System.Nullable<Int64> _TradeUserId;
        /// <summary>
        /// 交易员id
        /// </summary>
        [Display(Name = "交易员id")]
        [Column("tradeuserid")]
        public virtual System.Nullable<Int64> TradeUserId
        {
            get
            {
                return _TradeUserId;
            }
            set
            {
                if ((_TradeUserId != value))
                {
                    SendPropertyChanging("TradeUserId",_TradeUserId,value);
                    _TradeUserId = value;
                    SendPropertyChanged("TradeUserId");
                }
            }
        }
        System.Nullable<double> _Value;
        [Column("value")]
        public virtual System.Nullable<double> Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if ((_Value != value))
                {
                    SendPropertyChanging("Value",_Value,value);
                    _Value = value;
                    SendPropertyChanged("Value");
                }
            }
        }
        System.Nullable<DateTime> _Date;
        [Column("date")]
        public virtual System.Nullable<DateTime> Date
        {
            get
            {
                return _Date;
            }
            set
            {
                if ((_Date != value))
                {
                    SendPropertyChanging("Date",_Date,value);
                    _Date = value;
                    SendPropertyChanged("Date");
                }
            }
        }
    }
}

namespace FllowOrderSystem.DBModels.DB
{
    public class FllowOrders : Way.EntityDB.DBContext
    {
         public FllowOrders(string connection, Way.EntityDB.DatabaseType dbType , bool upgradeDatabase = true): base(connection, dbType , upgradeDatabase)
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
             var db =  sender as FllowOrderSystem.DBModels.DB.FllowOrders;
            if (db == null) return;
            if (e.DataItem is FllowOrderSetting)
            {
                var deletingItem = (FllowOrderSetting)e.DataItem;
                var items0 = (from m in db.FllowProduct where m.FllowSettingId == deletingItem.id
                select new FllowProduct
                {
                    id = m.id
                }
                );
                while(true)
                {
                    var data2del = items0.Take(100).ToList();
                    if(data2del.Count() ==0)
                        break;
                    foreach (var t in data2del)
                    {
                        t.ChangedProperties.Clear();
                        db.Delete(t);
                    }
                }
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TradeUser>().HasKey(m => m.TradeUserId);
            modelBuilder.Entity<FllowOrderSetting>().HasKey(m => m.id);
            modelBuilder.Entity<FllowOrderSetting>().HasDiscriminator<FllowOrderSetting_TypeEnum?>("Type")
            .HasValue<FllowOrderSetting>((FllowOrderSetting_TypeEnum)0)
            .HasValue<SystemFllowSetting>(FllowOrderSetting_TypeEnum.SystemFllowSetting)
            .HasValue<UserFllowSetting>(FllowOrderSetting_TypeEnum.UserFllowSetting)
            ;
            modelBuilder.Entity<FllowProduct>().HasKey(m => m.id);
            modelBuilder.Entity<FllowHistory>().HasKey(m => m.id);
            modelBuilder.Entity<FllowHistory>().HasDiscriminator<FllowHistory_TypeEnum?>("Type")
            .HasValue<FllowHistory>((FllowHistory_TypeEnum)0)
            .HasValue<FailFllowHistory>(FllowHistory_TypeEnum.FailFllowHistory)
            .HasValue<SuccessFllowHistory>(FllowHistory_TypeEnum.SuccessFllowHistory)
            .HasValue<SysFllowHistory>(FllowHistory_TypeEnum.SysFllowHistory)
            .HasValue<UserFllowHistory>(FllowHistory_TypeEnum.UserFllowHistory)
            ;
            modelBuilder.Entity<ConsumeRecord>().HasKey(m => m.id);
            modelBuilder.Entity<Concern>().HasKey(m => m.id);
            modelBuilder.Entity<SymbolWinRate>().HasKey(m => m.id);
            modelBuilder.Entity<UserProfit>().HasKey(m => m.id);
            modelBuilder.Entity<UserInfo>().HasKey(m => m.UserId);
            modelBuilder.Entity<TraderOrder>().HasKey(m => m.OrderId);
            modelBuilder.Entity<TotalProfitRateHistory>().HasKey(m => m.id);
        }
        System.Linq.IQueryable<TradeUser> _TradeUser;
        public virtual System.Linq.IQueryable<TradeUser> TradeUser
        {
            get
            {
                if (_TradeUser == null)
                {
                    _TradeUser = this.Set<TradeUser>();
                }
                return _TradeUser;
            }
        }
        System.Linq.IQueryable<FllowOrderSetting> _FllowOrderSetting;
        public virtual System.Linq.IQueryable<FllowOrderSetting> FllowOrderSetting
        {
            get
            {
                if (_FllowOrderSetting == null)
                {
                    _FllowOrderSetting = this.Set<FllowOrderSetting>();
                }
                return _FllowOrderSetting;
            }
        }
        System.Linq.IQueryable<SystemFllowSetting> _SystemFllowSetting;
        public virtual System.Linq.IQueryable<SystemFllowSetting> SystemFllowSetting
        {
            get
            {
                if (_SystemFllowSetting == null)
                {
                    _SystemFllowSetting = this.Set<SystemFllowSetting>();
                }
                return _SystemFllowSetting;
            }
        }
        System.Linq.IQueryable<UserFllowSetting> _UserFllowSetting;
        public virtual System.Linq.IQueryable<UserFllowSetting> UserFllowSetting
        {
            get
            {
                if (_UserFllowSetting == null)
                {
                    _UserFllowSetting = this.Set<UserFllowSetting>();
                }
                return _UserFllowSetting;
            }
        }
        System.Linq.IQueryable<FllowProduct> _FllowProduct;
        public virtual System.Linq.IQueryable<FllowProduct> FllowProduct
        {
            get
            {
                if (_FllowProduct == null)
                {
                    _FllowProduct = this.Set<FllowProduct>();
                }
                return _FllowProduct;
            }
        }
        System.Linq.IQueryable<FllowHistory> _FllowHistory;
        public virtual System.Linq.IQueryable<FllowHistory> FllowHistory
        {
            get
            {
                if (_FllowHistory == null)
                {
                    _FllowHistory = this.Set<FllowHistory>();
                }
                return _FllowHistory;
            }
        }
        System.Linq.IQueryable<FailFllowHistory> _FailFllowHistory;
        public virtual System.Linq.IQueryable<FailFllowHistory> FailFllowHistory
        {
            get
            {
                if (_FailFllowHistory == null)
                {
                    _FailFllowHistory = this.Set<FailFllowHistory>();
                }
                return _FailFllowHistory;
            }
        }
        System.Linq.IQueryable<SuccessFllowHistory> _SuccessFllowHistory;
        public virtual System.Linq.IQueryable<SuccessFllowHistory> SuccessFllowHistory
        {
            get
            {
                if (_SuccessFllowHistory == null)
                {
                    _SuccessFllowHistory = this.Set<SuccessFllowHistory>();
                }
                return _SuccessFllowHistory;
            }
        }
        System.Linq.IQueryable<SysFllowHistory> _SysFllowHistory;
        public virtual System.Linq.IQueryable<SysFllowHistory> SysFllowHistory
        {
            get
            {
                if (_SysFllowHistory == null)
                {
                    _SysFllowHistory = this.Set<SysFllowHistory>();
                }
                return _SysFllowHistory;
            }
        }
        System.Linq.IQueryable<UserFllowHistory> _UserFllowHistory;
        public virtual System.Linq.IQueryable<UserFllowHistory> UserFllowHistory
        {
            get
            {
                if (_UserFllowHistory == null)
                {
                    _UserFllowHistory = this.Set<UserFllowHistory>();
                }
                return _UserFllowHistory;
            }
        }
        System.Linq.IQueryable<ConsumeRecord> _ConsumeRecord;
        public virtual System.Linq.IQueryable<ConsumeRecord> ConsumeRecord
        {
            get
            {
                if (_ConsumeRecord == null)
                {
                    _ConsumeRecord = this.Set<ConsumeRecord>();
                }
                return _ConsumeRecord;
            }
        }
        System.Linq.IQueryable<Concern> _Concern;
        public virtual System.Linq.IQueryable<Concern> Concern
        {
            get
            {
                if (_Concern == null)
                {
                    _Concern = this.Set<Concern>();
                }
                return _Concern;
            }
        }
        System.Linq.IQueryable<SymbolWinRate> _SymbolWinRate;
        public virtual System.Linq.IQueryable<SymbolWinRate> SymbolWinRate
        {
            get
            {
                if (_SymbolWinRate == null)
                {
                    _SymbolWinRate = this.Set<SymbolWinRate>();
                }
                return _SymbolWinRate;
            }
        }
        System.Linq.IQueryable<UserProfit> _UserProfit;
        public virtual System.Linq.IQueryable<UserProfit> UserProfit
        {
            get
            {
                if (_UserProfit == null)
                {
                    _UserProfit = this.Set<UserProfit>();
                }
                return _UserProfit;
            }
        }
        System.Linq.IQueryable<UserInfo> _UserInfo;
        public virtual System.Linq.IQueryable<UserInfo> UserInfo
        {
            get
            {
                if (_UserInfo == null)
                {
                    _UserInfo = this.Set<UserInfo>();
                }
                return _UserInfo;
            }
        }
        System.Linq.IQueryable<TraderOrder> _TraderOrder;
        public virtual System.Linq.IQueryable<TraderOrder> TraderOrder
        {
            get
            {
                if (_TraderOrder == null)
                {
                    _TraderOrder = this.Set<TraderOrder>();
                }
                return _TraderOrder;
            }
        }
        System.Linq.IQueryable<TotalProfitRateHistory> _TotalProfitRateHistory;
        public virtual System.Linq.IQueryable<TotalProfitRateHistory> TotalProfitRateHistory
        {
            get
            {
                if (_TotalProfitRateHistory == null)
                {
                    _TotalProfitRateHistory = this.Set<TotalProfitRateHistory>();
                }
                return _TotalProfitRateHistory;
            }
        }
        protected override string GetDesignString()
        {
            var result = new StringBuilder();
            result.Append("\r\n");
            result.Append("H4sIAAAAAAAACu09a3PT1rZ/pZPPbbFsWbY75QPlMc1cHh1C23Pm0GEUW0l8caQcSQZyOswkl0OTAIkTHpfQpOXRUFJuSWihISSE/BlLdj6dv3D3Q7Yl7W1Lth6WQ76AI21Je6299nqvtb/vO8cPFgSl77N/fI9/nuZHhb7P+gb+WRgQ5EuC3Pdx31npMh7Qrwqj+Jcx");
            result.Append("Kp8Dt7/hC0XwR5q7+nH9hjo+JjRu9R2VBV4V0PuPZNW8JPaZxmYlURVE1TT8+/N4Luf7PgM/8znwP/PxeTQe/AQ3ZT4nfK0I8vk+cPkYr/KDvCL0HzPG5U9K2Yvgd+wq+OOoVCiOigr48x+t39UPboEb/cqRoir1i1lZGAWzAmOG+IIiwDfx4ulioWC6khs8B+BErxnM");
            result.Append("D+fhaHC1IIjD6gi6iv5GoNQn16989V/oD1UuwndIck6Q0bRi6O6xvJKV86N5kVclufYtgC1j7kkwKMuPQRyiL+z9Mqc/eoe+UwdpQB2HuHMJjDEPEyw1QI6LxdFjwlBexJcPHao8fFHZ2j1/XhwYkWT1o8MfMZ9/DmZ9Xjx0aO/eLr51UhKH8R3GuDM1W96+A35+mR8e");
            result.Append("wXfixp3/W9x7smDcOSEL/ywKYnYcD0ngIeXNbf2vSfDzFJ8XB1RARqP4Povv67sT1d0l8PMsn8tn+QK+maw9bEz3KIBdkE8CQPB9zri/M4e/f1K6XP/8YXA/he9Xd7bxzE+C+anf8rIwIhUVAY5IG59/8USffQgRokpjJyVFwe/PNOD7ZW7vwW8GiGfzykUDNQbWKquv");
            result.Append("tMmn6HmBzxmgM0w7ZFRb0AYdJdzQEWejo8raRHn7ppWOjgnwFcaYjqnpEi9nR3jZDlM8FmsPLNYNWCkbWNq7CW1+vbr2ZG/xuhW44yL88EBxEL5v0Mt2GczT931OGOKLBRUxNXS1TYCTbgBOUwHW/rhXWdmyAtyvfJNX8oirRg5Uzg2oGRuoeFXL229IBlhb1a/kfNYL");
            result.Append("vDmpWENYx9sx5QY2yBAswJXfLerXHgHgbJCNCdk8X4gCXGlXcDE2uPTlCSAq8NJpU4+0ld+aLN0xflzxQYZ1DF7GDXisXRxXbmzoE5N2ccyrRcV/eXxaQvKsJkpezGjvr2trj/VHm1AeCpfywuU8lsV4BL63N/FjdXcKXDgyNiZLl4QcGBB3t6mZ9jCIaNoZhYQkeg34");
            result.Append("9WP97kZl6YYVkeckFZK+NGRwoe7RPiJrZ9Ds0ggDVZmbssKFQToLNORugxV3BZZd5lR3FxLawirY2trKM23pZ/32ihXAs4Iq81kDmu5C6EqosnZRU95a0RfvAGal33tpo0loPnSbVzGu5GfSLmP0W5NAv9Wmf9A2r1V+/DeATZuwSdKvJCUPx4NXR4E+XcnSJEPlKIa6");
            result.Append("sLVFrGFd5shBrKJX3YhxJYmSdklUvbZMMJpv82IUVtGVgZLkqHsQGGwEWGAPinX7rdvAuTJTkoSdsvJau1YqygUrZF8Cq7Af+Ee+Nm74aoEx7VpgroRe0i4d9MW/Ks9sO+50PnsR/+E3UMk2YXIl8ZIZKkfBFAn4ijZ7jy4YjkpFTyLPNU9pE2hXmhlnlxeVpWlt+rdW");
            result.Append("8GJNJiio3cPnilI5u5wob5WAN6cVfNDF033oXNEsF6fyT33mZjPQVKXLvDPuznOVoG5GTJxNabLrsLmSCxxrZ573n2o/TeGlq/x+l1g6oHn6sXreadKVXs3ZlRRsMtCW7hR/JRKWXtyVas1xVMAwQyEAg1yk22C50qQzdg6p7dzRZmax3QBocW+qRDcXQpN77Up7d06r");
            result.Append("NNPCUtJLperuy6ZmEghSdF+rYVrB+R28d+xvRyVxKD+MAmP4CvgVu2oOzOWMwJo5yseAx2FAEDq1BBRicwoLpoIMC9qFXPXNQyC8q2vvKztr1iU6UShIl89ADA0IqgocY53FDuPmd8Ir1KU2FrZlxLBG441FjXsPDybM0wspqhn3wWpguxWSdZi8K1ULuc2sJs+6Nv+r");
            result.Append("vjyj35u2B2Fw3CmMGAzTHqiulC9ku5s927J6Lu/JjgNMRlBrr+h4nVxpV8g2NwUAc5GYuivNCfktLZJ4aUtb+xGyu6lHQKcH/vyPRDDP6uNVEPgrb87i+/95dwtqhtrtW6cAaeTHCoJWel69tgNIs7w5Ud58Dn4QPr/AdUkHhLjSuJDL0875QTxpYtZPVNSGdhMd7uKH");
            result.Append("Kaog1P/3rTa/YDMZ8rKAhK3/ESmYEPEL/OB58YQkX+blnCn6VJrFdwb57EXjVodxJweEudTx7BsKYOv+UwNtE9t7T34iLS0Yfzoy6lG3daW+O4DoyiWMHMcUmgDATS0Q8EUHOHdxQ+TDIk0uwzZZv1t+j5iiNvuqvH2jsbLoNoZfm32EL+I/YQQEP0ZV7cNwnDshxpUq");
            result.Append("hUxxUhsBoO79WNJnJiDD23qm3bFFqfsVpCEfgfOPnG7iLjBJmm3T88B1gjNEyrs/6TdWaCuMjAKUURHcGpOG66exWLxNJLRQcuDMCryiWHR/tKB1a8dk1qepFALkIfxRWtibmNQ3pvXJdTuFnBEL46cEReGHu5JE5ISfpE/4YdKchTOiifsvKyuvtivbDzELgimA4wqw");
            result.Append("o81zagjPyt1VffpNfah98k1FqXvc0bV/CI0ZL0TYanujugv1rsqz2zZxKRR4L7E472Yn1wY1kLi30gOR8YBiyfr9DSA0qusvqqsTNhWaF4eL3naJW0+QExpS3jYF6SwCWOlXvhbzINzamHi/crRQBDiUhZz57ch/Al+PnrR4JCxG/nctPTUX8LALlkeMmQXktEoH6bSy");
            result.Append("BzCwuwoL6PL2RPntNE1AI7QAn3iumFU79F0xATqvEt6dVyiwZYXXoABvjiBnXpLwwYUVty+rdfkGxkcHpUIIDMEBFleaFIpWNbiZcEmQvXEzz0vQyjvlxKNqb3PJpOyE912zXXjBPjJYnpQJkiexTRzpL7WdexQ+9GVeASsw3iEf4gLkQ6wPfChFT/9bWKSEl0PyU7N+");
            result.Append("cKg0dZFBSpwVrmiA5MofHW+SqbmwCDUzlMmhlWw59GdkODu+gFSMNuCkWTLewXTluU7EqCvXCsYowJZuRwUvZrPAtEQTNyvfCSKbcwmoR/a4cyipAmzHTjn30NpDSdpb4L+6U/ljW/vZ5rY4WpAUISjbFNMX/jj05kKeX2d3JrsUmbDauy0wsm7CoolBk9SokZu5qd1Y");
            result.Append("rb/qFC8WUZ1ewiPHY/xAN0uWT8FA/8of1ddPtbmH2tIj274aE8QTfL5wVuCVYDzo1de/QlP/r38DryQIXlQ3IGJPS+pxUSoOj5ySRAGVCRoVitipufw7dGE+mNemN8Blkyf3JMCM2lgKi4/b+hBwb1sfMQowSa9q/RGzZ7T2VK0sk3C84ac+go9BTxvYsCqIQoAnjFpN");
            result.Append("wxP7y5w+96z+BVgwCbQ6VZYgwRhVm5Wdhcr2svbuf7RNWOhyWlAvS/LF47IsAcKsVW7iHL7ba9r8NLDS9+6/rm5Acx1iBwTspCL8ctobAbZjWEOSIYmPUDM2b9a90RR2Ho5z3mnbxX3YdmmLoRcMD6ujvLZbzJNx4TBzwAPnwl+WTkSt/sdpcWN+LK6dp1ZWb2pbperU");
            result.Append("c+DFbKJAB5MLzrQHvbsYQ9ICQCgJBKwPCQSZBFXSEQWmUMCFUl3qRIoJH0gxQ4h3pIUQQCONJRJQsz5AzaRj9PjOmz8r2yVtcll/8QTsRqCUGQEFWhwQTCGsPKVYmzhKdo4jnz1FDj5sw0li81t/7OmTzl8LxUGeiQXpjCKyqTemq6/f0pxRYCWV4qhwVsgCAunMG5VI");
            result.Append("B+iN4nxI6cyQWoT++3vCaxOmN4rzI+WTaHmwvUiDKxoguauMJhIQkO1h0/ZAIlEoagPng3+NJRKskWFqFij6zJ3K9oMmLlLA1SCvlUe7IUg4HzxvrF1vopln4VhmnA95gizh59/5Xbv+tLzzAABlUCvImtp7AhyLj833YHYUug3yKPU/a7fe3tRmZsGV6u4DkEGpvbiv");
            result.Append("r/1VfvsQkAfOpgRP2ZiU9AVf4MWsEIkt4E5zJrbAzu/ASaNNvaiuT5KFyMqQIH8jFbMjRneyYIN9XOeptKHpQhYZHbwyZP1cONoQE6Q2RCTdXH+lv1rVpu+DDGdCG8oKstiZHpQMsrQl5UPnu0T0onIpH/SgJOEvQcleXVGCUp0rQf4mCtkXsdlOh+RuZikh7fd4kPs9");
            result.Append("TaUHIEoNgl9/S+vtgfNLzB0+2uYAXJCWUNo7B+AyVMwYE+0KA0j7wABShCFUW+cuZRClfTCFUkx0+tGkfTCEUvEWbcq66NFPd27jhKYAWlhTiFmcmUSQbJrOjDBBEJoZhNeUpNA2a04lAmTNGe+sOcVGjjVn/GDNRC3N/af6sq2jA1jMEEzZjB9MmaOCQ8suCSqxBHwR");
            result.Append("ZxuYKgMXVsH/3wrCxcO1lJHlaZgpAnbhyOGEN7y4Y+5kwh/ZuCOMrh0ZP1ogE9U9lFaa4SRMZTp3v4QmnBqcOUzJxAbaK5+hMuPy7mOipgtBKw5JnUmljCUYHlYTfR+66Ge4iDWp86PzXobeVLC7HQU9tN8Lt09NJhnoliTq81EwE3Ye2Foic5YpWpNsxLA72acMovfw");
            result.Append("DUmj+Mxj1w5E15Rq3aCll9Ps3TXuQMW24ZbSOE3cXUeJTMY88S8UuCe6Om1XfR3isZhVaQu6L17Kh04N8ZjnI2k6TOB3mr67IoxYwkvtQceKgA9BhXjMUukWUstBp7CBy6knu9RE3mn67oqRYlx3UvCcpu+q/VA8RjlpBqbIUxI+YEplOOFux5VxB1qamjVJAw3XoEQC");
            result.Append("tghpkq0OQhvhxeG2NMkzhVz9XDardMBLIVxudVsULpv0QQh1Fs0gZ7uaEwqCarm6j9vthkwOqd4ih273Ig56+cGdL/jsxeIYRn4OEOyYIKt55Cr63jSV7xuloo0cM6SWXL16lb5pQqasdFQpqyFL7FESbe4HrfSnPrFNK+M0UpgjwkJamUntcNKQiSKzn6TPviag8IgC");
            result.Append("OOYCpQpbeJMgC9v9JsyCOASvySqHeA5Yxlt+ZecsIqBoB/jIhTEc7VDhoCIYlM9dCDjcwcSYXiA/xmWw95xwRQ3BexoS7e2vWPe+30nxXthJREP0udvaVqny+on2zl5uKOaEK4F6aB2IKTgtb58RXqIXCM/iiP67YHDZLpGW0yEcB6RlkBYbKGnRT/4gKIw+rBmhsdRO");
            result.Append("qLBKanmLSAdFzVCj0EA67bUnqp9kG0DrUMgbh+CMIVNUjO6hZi4ZFrNMBk/R5hZ8dGI2j+jcFA9VsKHFGzEq2W3iLYBaMeKTYdFHsCGCA/rodfoINmZwQB+9Th/Bev6JVtN0+WIe0UxPSn5A59I4tDB2Ckl2SSC3bHdcJ/Axo+Ux+sPQrMIg9Ey0Cb2jmMbBpuhKgVqk");
            result.Append("CZ2J9YoNfGACultQJvLBeeIMPW3tsf5os3n3W18637qvHXBwZjA9GZ9nmGA9+Ac7PWxnDxOsZ/xgQUNf0J7xR7fO4Gtyzlvl18nK3Sa9P0M+4Y0J9YQ3x8w/6/So+X+xtrL/PhiHNxOCw/tAfQ1zQbnei8k1OQOZEpBD741CQC7j2yGFceJEY3jc6/KWPnMTZGpX/3qj");
            result.Append("PbtJO+obneEeaJTcAQfoIF+/cJBpgYPK9h39p4c0HBwXc13FQFtHmRyEZTtnaqmeY2rkMdcvnuizD2nnGA+o0lhBUpQI8DV0kqNPexrtDhsGQFZwMwyEVwfphIO2TjA42NWd7+rol8Gg/WApogux5ZJxePTUc+3NH5W7r6rrb+DxTJtbjY6SC4vGodLEyV7zc1rp5d71");
            result.Append("2crOmm04ptjOg2WMt95AUXXuhRCvOrCDQrSD4gdxmf3lqYgzvacDJqjJptVb17Sl15RM0y6bM96zTE3KH0tVf2mg19RfrwZtNHQ/EwqSbaPAGwH4ggDWRwRwVAOgGQKwARAJKuB8REKqAyREgA5SwRhBjbPOUlSPD+aQ2tSjyrPbFA7ZdacX4xUvTrEM42FqDAOh4CCO");
            result.Append("QdUOeiY43+AMxKnuuzvajUfaVEl7uVDe/am6PglPbLq/4ZptnhFP8TKgmZ7nHSb2mfERSYCtRgVFfpWY9bq/sa0zIfepv9GX4q4PNH4dP8jh2WcLyka7Aq/ObhKtDuutrr0Hntfy1g8GD0K82J6zAzYw0v8AG/LYo75Z3o77s3nbtHisSIpuUfQHUpYYT/bKprHL6PLm");
            result.Append("zSZlRV8oPsVVOt4UmfbPqz7YF9HaF1yv7It46M3GnSRCW9k9B9QfRakQ2QamDbonIiTTS9r2FrVlMT7LIZyexQ57w1139QRLKdOZeUsD7vgVfjQaoAVX7Bosrad7hdMnu3KiR+CqjonsibAPyuIkkjeVroo3xk+Io9hT3gkBaT8REMnO86yf+csHGk4UNZwQsrYO2jX1");
            result.Append("rl8kEWwSmP18L7oObBrgZ0F+sIgLoQh+fyIu3puHRiTpZxAWjdMGwz2GkMhBHlHVMeWzQ4eG8mA0mMqn4/899K/Llz/NSqOHRsCMlGyxMKYWZeHQqPTpmBF2d2/ztNQDPFaeigAHETl2gkkE3wq2fiYptRFs/W5b1dCROR7Td7rs/JDNfUaYbG9yTZajnrFEO7g58LMP");
            result.Append("COJsty1LMgRqq5uMLJEWiXBGFPwGnaXhGWtcqFgjU8YWEtrCqr48oa0805Z+1m+vWPF3VgBRz6yBrEgiMBMqAu3pZEZl1MpvhIcKcYVj/HgQbiqvSHNwXfiMtCSR6XBrEnh3tOkftM1rsHP2vZdEcfZXkpKH48EUo7t5Heq3/UYj0VLz2jLB777Ni9HFl0Pys9/44qh7");
            result.Append("de/JAoE1sFfFE7IAEjnE7HhEcZcOFXcZqmKCMYiPKqczvKNS0ZOkCIzjeTkY0EV7IJRVX0cfZ+d4+NTMVojDikpk0ReqcscRWT5bJVj51AJ9MAEussgLVcfj7CXneNPSgmgBn9rpGW+hiosIHnvqGYGhygyOqFdEVgXtwOBT/JUom7KoaCQ8vHFUvGGeR+DNY6ZvgFhD");
            result.Append("Cc2hYY1JMy2sCr1Uqu6+bGpSDKhyYDkTJBpHP0mOtkmAH47nLrkfTq0NG2lcNP3wjXyODL224fpkdW0TNqS50aQNKdh2KGgeRg9Sgr058bfezGxLRD+Lk020cIOTxnrDEx5hVwfXo9SS7rk6aJalM5vSwt7EpD63Cv4t7z7WJ9ftzGZAEHPt9jyuXYl+0+OOyoBbd442");
            result.Append("M+7z4kf1rmC4COc/725ppefwAYz5jWmA872JHysPn4Jb5c3ZVox/X/ef7mqBLYKiiwWZiUzvcRS7JYuLMg1esvWDdud5eWsJE7jNxigqI0LulDJ8osAPe2EqTZv6ASfY3uJ1bX4uzo6AbaUvPwfJwtr8en1bDhQH42yjn199fCredHwqDsd//rnlkeruor48kwChLW36");
            result.Append("JTwPHA09c0lO4MFxcjBDDmbw4IR18BQYAqZUHwXzefFAlhwIp1AfCZwuxveTncltP/uzeGS6HzJXYGO9xxW4bpUTOdFsWAfM7WuCjP6xTUTOCw6IAI+X/vO8EZqjhISPANfPuJJXoh0azvSmvcS2ytfFLKINskFjwUzrS05kTkxs163i6tpLbedeS9vYXHtwzIC3vhr5");
            result.Append("k1L2IkJLffda6Q0lHNRfDa9RKcegE1fqjImnxcwEYbyjQQ8tfMmmuqEmIX5gkhizJTeYwZH6O7Qvhl10gIt1nC5sBi1lnr6xr4LYue4nHnc38bQ1SKaGISBj3lx3YTKM4HOoTaE1qvfWdL+JpEHpH6HlUDbWMdOt3uABtpCB+ssYwqC9gUywdMZGl84O1sq2VskoV0vV");
            result.Append("uQKR1YR7KTa6KFJSTI6MjQEva1spEi3Uh/a1zlTnXUoirXZyvUAxHNG8OHZ4b2IeROk/Zg5r11fBD4qWdkrKefLH13VNfAFGu+2ONOOa/+Tkvv03CsJ3j8LAn8a7+yCNNWRajdKguQAxCP7GPSM/7RdVjkUfsow2qI8cD5IsgC1PPtAgQffPWGBqOrXvIFjwLvDmGA+y");
            result.Append("zGBcyLGxT3LCIPsJyyWzn6S53NAnMTYTSw7GuOQQH+u7+v/6ml3SDgsBAA==");
            return result.ToString();
        }
    }
}

/*<design>
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
<design>*/

