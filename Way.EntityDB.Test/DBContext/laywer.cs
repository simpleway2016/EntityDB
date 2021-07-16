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

namespace Dfd.Common.DBModels
{
    /// <summary>律师案件关联表</summary>
    [TableConfig]
    [Table("fa_case_lawyer")]
    [Way.EntityDB.DataItemJsonConverter]
    public class fa_case_lawyer :Way.EntityDB.DataItem
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
        System.Nullable<Int32> _lawyer_id;
        /// <summary>律师id</summary>
        [Display(Name = "律师id")]
        [Column("lawyer_id")]
        public virtual System.Nullable<Int32> lawyer_id
        {
            get
            {
                return _lawyer_id;
            }
            set
            {
                if ((_lawyer_id != value))
                {
                    SendPropertyChanging("lawyer_id",_lawyer_id,value);
                    _lawyer_id = value;
                    SendPropertyChanged("lawyer_id");
                }
            }
        }
        System.Nullable<Int32> _case_id;
        /// <summary>案件id</summary>
        [Display(Name = "案件id")]
        [Column("case_id")]
        public virtual System.Nullable<Int32> case_id
        {
            get
            {
                return _case_id;
            }
            set
            {
                if ((_case_id != value))
                {
                    SendPropertyChanging("case_id",_case_id,value);
                    _case_id = value;
                    SendPropertyChanged("case_id");
                }
            }
        }
        fa_case_lawyer_statusEnum? _status;
        /// <summary>状态</summary>
        [Display(Name = "状态")]
        [Column("status")]
        public virtual fa_case_lawyer_statusEnum? status
        {
            get
            {
                return _status;
            }
            set
            {
                if ((_status != value))
                {
                    SendPropertyChanging("status",_status,value);
                    _status = value;
                    SendPropertyChanged("status");
                }
            }
        }
        String _invitation_addr;
        /// <summary>邀约地址</summary>
        [MaxLength(255)]
        [Display(Name = "邀约地址")]
        [Column("invitation_addr")]
        public virtual String invitation_addr
        {
            get
            {
                return _invitation_addr;
            }
            set
            {
                if ((_invitation_addr != value))
                {
                    SendPropertyChanging("invitation_addr",_invitation_addr,value);
                    _invitation_addr = value;
                    SendPropertyChanged("invitation_addr");
                }
            }
        }
        System.Nullable<DateTime> _invitation_time;
        /// <summary>邀约时间</summary>
        [Display(Name = "邀约时间")]
        [Column("invitation_time")]
        public virtual System.Nullable<DateTime> invitation_time
        {
            get
            {
                return _invitation_time;
            }
            set
            {
                if ((_invitation_time != value))
                {
                    SendPropertyChanging("invitation_time",_invitation_time,value);
                    _invitation_time = value;
                    SendPropertyChanged("invitation_time");
                }
            }
        }
        System.Nullable<Boolean> _is_agree;
        /// <summary>律师是否同意邀约</summary>
        [Display(Name = "律师是否同意邀约")]
        [Column("is_agree")]
        public virtual System.Nullable<Boolean> is_agree
        {
            get
            {
                return _is_agree;
            }
            set
            {
                if ((_is_agree != value))
                {
                    SendPropertyChanging("is_agree",_is_agree,value);
                    _is_agree = value;
                    SendPropertyChanged("is_agree");
                }
            }
        }
        String _contract_path;
        /// <summary>合同文件路径</summary>
        [MaxLength(1024)]
        [Display(Name = "合同文件路径")]
        [Column("contract_path")]
        public virtual String contract_path
        {
            get
            {
                return _contract_path;
            }
            set
            {
                if ((_contract_path != value))
                {
                    SendPropertyChanging("contract_path",_contract_path,value);
                    _contract_path = value;
                    SendPropertyChanged("contract_path");
                }
            }
        }
        System.Nullable<Boolean> _can_refund=true;
        /// <summary>是否可退款</summary>
        [Display(Name = "是否可退款")]
        [Column("can_refund")]
        public virtual System.Nullable<Boolean> can_refund
        {
            get
            {
                return _can_refund;
            }
            set
            {
                if ((_can_refund != value))
                {
                    SendPropertyChanging("can_refund",_can_refund,value);
                    _can_refund = value;
                    SendPropertyChanged("can_refund");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<fa_case_lawyer, bool>> exp)
        {
            base.SetValue<fa_case_lawyer>(exp);
        }
    }
    public enum fa_case_lawyer_statusEnum:int
    {
        进行中=1,
        待付款=20,
        已付款=30,
        已发起退款=35,
        已完成=40,
        已取消=-1,
        已退款=-2
    }
    /// <summary>案件付款表</summary>
    [TableConfig]
    [Table("fa_case_pay")]
    [Way.EntityDB.DataItemJsonConverter]
    public class fa_case_pay :Way.EntityDB.DataItem
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
        String _order_id;
        /// <summary>支付订单号</summary>
        [MaxLength(50)]
        [Display(Name = "支付订单号")]
        [Column("order_id")]
        public virtual String order_id
        {
            get
            {
                return _order_id;
            }
            set
            {
                if ((_order_id != value))
                {
                    SendPropertyChanging("order_id",_order_id,value);
                    _order_id = value;
                    SendPropertyChanged("order_id");
                }
            }
        }
        System.Nullable<Decimal> _amount;
        /// <summary>金额</summary>
        [Display(Name = "金额")]
        [Column("amount")]
        public virtual System.Nullable<Decimal> amount
        {
            get
            {
                return _amount;
            }
            set
            {
                if ((_amount != value))
                {
                    SendPropertyChanging("amount",_amount,value);
                    _amount = value;
                    SendPropertyChanged("amount");
                }
            }
        }
        String _third_order_id;
        /// <summary>第三方支付订单号</summary>
        [MaxLength(50)]
        [Display(Name = "第三方支付订单号")]
        [Column("third_order_id")]
        public virtual String third_order_id
        {
            get
            {
                return _third_order_id;
            }
            set
            {
                if ((_third_order_id != value))
                {
                    SendPropertyChanging("third_order_id",_third_order_id,value);
                    _third_order_id = value;
                    SendPropertyChanged("third_order_id");
                }
            }
        }
        Int32 _case_lawyer_id;
        /// <summary>律师案件关联id</summary>
        [DisallowNull]
        [Display(Name = "律师案件关联id")]
        [Column("case_lawyer_id")]
        public virtual Int32 case_lawyer_id
        {
            get
            {
                return _case_lawyer_id;
            }
            set
            {
                if ((_case_lawyer_id != value))
                {
                    SendPropertyChanging("case_lawyer_id",_case_lawyer_id,value);
                    _case_lawyer_id = value;
                    SendPropertyChanged("case_lawyer_id");
                }
            }
        }
        fa_case_pay_statusEnum? _status;
        /// <summary>状态</summary>
        [Display(Name = "状态")]
        [Column("status")]
        public virtual fa_case_pay_statusEnum? status
        {
            get
            {
                return _status;
            }
            set
            {
                if ((_status != value))
                {
                    SendPropertyChanging("status",_status,value);
                    _status = value;
                    SendPropertyChanged("status");
                }
            }
        }
        System.Nullable<DateTime> _create_time;
        /// <summary>生成时间</summary>
        [Display(Name = "生成时间")]
        [Column("create_time")]
        public virtual System.Nullable<DateTime> create_time
        {
            get
            {
                return _create_time;
            }
            set
            {
                if ((_create_time != value))
                {
                    SendPropertyChanging("create_time",_create_time,value);
                    _create_time = value;
                    SendPropertyChanged("create_time");
                }
            }
        }
        System.Nullable<DateTime> _pay_time;
        /// <summary>付款时间</summary>
        [Display(Name = "付款时间")]
        [Column("pay_time")]
        public virtual System.Nullable<DateTime> pay_time
        {
            get
            {
                return _pay_time;
            }
            set
            {
                if ((_pay_time != value))
                {
                    SendPropertyChanging("pay_time",_pay_time,value);
                    _pay_time = value;
                    SendPropertyChanged("pay_time");
                }
            }
        }
        String _pay_channel;
        /// <summary>付款方式</summary>
        [MaxLength(50)]
        [Display(Name = "付款方式")]
        [Column("pay_channel")]
        public virtual String pay_channel
        {
            get
            {
                return _pay_channel;
            }
            set
            {
                if ((_pay_channel != value))
                {
                    SendPropertyChanging("pay_channel",_pay_channel,value);
                    _pay_channel = value;
                    SendPropertyChanged("pay_channel");
                }
            }
        }
        Int32 _user_id;
        /// <summary>付款人id</summary>
        [DisallowNull]
        [Display(Name = "付款人id")]
        [Column("user_id")]
        public virtual Int32 user_id
        {
            get
            {
                return _user_id;
            }
            set
            {
                if ((_user_id != value))
                {
                    SendPropertyChanging("user_id",_user_id,value);
                    _user_id = value;
                    SendPropertyChanged("user_id");
                }
            }
        }
        Int32 _lawyer_id;
        /// <summary>收款律师id</summary>
        [DisallowNull]
        [Display(Name = "收款律师id")]
        [Column("lawyer_id")]
        public virtual Int32 lawyer_id
        {
            get
            {
                return _lawyer_id;
            }
            set
            {
                if ((_lawyer_id != value))
                {
                    SendPropertyChanging("lawyer_id",_lawyer_id,value);
                    _lawyer_id = value;
                    SendPropertyChanged("lawyer_id");
                }
            }
        }
        System.Nullable<Int32> _invite_coupon_id;
        /// <summary>优惠卷id</summary>
        [Display(Name = "优惠卷id")]
        [Column("invite_coupon_id")]
        public virtual System.Nullable<Int32> invite_coupon_id
        {
            get
            {
                return _invite_coupon_id;
            }
            set
            {
                if ((_invite_coupon_id != value))
                {
                    SendPropertyChanging("invite_coupon_id",_invite_coupon_id,value);
                    _invite_coupon_id = value;
                    SendPropertyChanged("invite_coupon_id");
                }
            }
        }
        String _comment;
        /// <summary>收款描述</summary>
        [MaxLength(255)]
        [Display(Name = "收款描述")]
        [Column("comment")]
        public virtual String comment
        {
            get
            {
                return _comment;
            }
            set
            {
                if ((_comment != value))
                {
                    SendPropertyChanging("comment",_comment,value);
                    _comment = value;
                    SendPropertyChanged("comment");
                }
            }
        }
        System.Nullable<Decimal> _pay_amount;
        /// <summary>实际付款金额</summary>
        [Display(Name = "实际付款金额")]
        [Column("pay_amount")]
        public virtual System.Nullable<Decimal> pay_amount
        {
            get
            {
                return _pay_amount;
            }
            set
            {
                if ((_pay_amount != value))
                {
                    SendPropertyChanging("pay_amount",_pay_amount,value);
                    _pay_amount = value;
                    SendPropertyChanged("pay_amount");
                }
            }
        }
        Int32 _case_id;
        /// <summary>案件id</summary>
        [DisallowNull]
        [Display(Name = "案件id")]
        [Column("case_id")]
        public virtual Int32 case_id
        {
            get
            {
                return _case_id;
            }
            set
            {
                if ((_case_id != value))
                {
                    SendPropertyChanging("case_id",_case_id,value);
                    _case_id = value;
                    SendPropertyChanged("case_id");
                }
            }
        }
        System.Nullable<DateTime> _withdraw_time;
        /// <summary>可提现时间</summary>
        [Display(Name = "可提现时间")]
        [Column("withdraw_time")]
        public virtual System.Nullable<DateTime> withdraw_time
        {
            get
            {
                return _withdraw_time;
            }
            set
            {
                if ((_withdraw_time != value))
                {
                    SendPropertyChanging("withdraw_time",_withdraw_time,value);
                    _withdraw_time = value;
                    SendPropertyChanged("withdraw_time");
                }
            }
        }
        Boolean _to_balance=false;
        /// <summary>是否已经累计到律师余额</summary>
        [DisallowNull]
        [Display(Name = "是否已经累计到律师余额")]
        [Column("to_balance")]
        public virtual Boolean to_balance
        {
            get
            {
                return _to_balance;
            }
            set
            {
                if ((_to_balance != value))
                {
                    SendPropertyChanging("to_balance",_to_balance,value);
                    _to_balance = value;
                    SendPropertyChanged("to_balance");
                }
            }
        }
        System.Nullable<Decimal> _commission;
        /// <summary>平台抽佣金额</summary>
        [Display(Name = "平台抽佣金额")]
        [Column("commission")]
        public virtual System.Nullable<Decimal> commission
        {
            get
            {
                return _commission;
            }
            set
            {
                if ((_commission != value))
                {
                    SendPropertyChanging("commission",_commission,value);
                    _commission = value;
                    SendPropertyChanged("commission");
                }
            }
        }
        System.Nullable<Boolean> _is_fromBalance=false;
        /// <summary>是否是余额支付</summary>
        [Display(Name = "是否是余额支付")]
        [Column("is_frombalance")]
        public virtual System.Nullable<Boolean> is_fromBalance
        {
            get
            {
                return _is_fromBalance;
            }
            set
            {
                if ((_is_fromBalance != value))
                {
                    SendPropertyChanging("is_fromBalance",_is_fromBalance,value);
                    _is_fromBalance = value;
                    SendPropertyChanged("is_fromBalance");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<fa_case_pay, bool>> exp)
        {
            base.SetValue<fa_case_pay>(exp);
        }
    }
    public enum fa_case_pay_statusEnum:int
    {
        待付款=0,
        已付款=1,
        已发起退款=2
    }
    /// <summary>用户银行卡</summary>
    [TableConfig]
    [Table("fa_bank_card")]
    [Way.EntityDB.DataItemJsonConverter]
    public class fa_bank_card :Way.EntityDB.DataItem
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
                    SendPropertyChanging("id",_id,value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        System.Nullable<Int32> _user_id;
        /// <summary>用户id</summary>
        [Display(Name = "用户id")]
        [Column("user_id")]
        public virtual System.Nullable<Int32> user_id
        {
            get
            {
                return _user_id;
            }
            set
            {
                if ((_user_id != value))
                {
                    SendPropertyChanging("user_id",_user_id,value);
                    _user_id = value;
                    SendPropertyChanged("user_id");
                }
            }
        }
        String _bank_name;
        /// <summary>银行名称</summary>
        [MaxLength(100)]
        [Display(Name = "银行名称")]
        [Column("bank_name")]
        public virtual String bank_name
        {
            get
            {
                return _bank_name;
            }
            set
            {
                if ((_bank_name != value))
                {
                    SendPropertyChanging("bank_name",_bank_name,value);
                    _bank_name = value;
                    SendPropertyChanged("bank_name");
                }
            }
        }
        String _create_account_bank;
        /// <summary>开户行</summary>
        [MaxLength(100)]
        [Display(Name = "开户行")]
        [Column("create_account_bank")]
        public virtual String create_account_bank
        {
            get
            {
                return _create_account_bank;
            }
            set
            {
                if ((_create_account_bank != value))
                {
                    SendPropertyChanging("create_account_bank",_create_account_bank,value);
                    _create_account_bank = value;
                    SendPropertyChanged("create_account_bank");
                }
            }
        }
        String _number;
        /// <summary>卡号</summary>
        [MaxLength(50)]
        [Display(Name = "卡号")]
        [Column("number")]
        public virtual String number
        {
            get
            {
                return _number;
            }
            set
            {
                if ((_number != value))
                {
                    SendPropertyChanging("number",_number,value);
                    _number = value;
                    SendPropertyChanged("number");
                }
            }
        }
        String _name;
        /// <summary>姓名</summary>
        [MaxLength(50)]
        [Display(Name = "姓名")]
        [Column("name")]
        public virtual String name
        {
            get
            {
                return _name;
            }
            set
            {
                if ((_name != value))
                {
                    SendPropertyChanging("name",_name,value);
                    _name = value;
                    SendPropertyChanged("name");
                }
            }
        }
        String _id_number;
        /// <summary>身份证号</summary>
        [MaxLength(50)]
        [Display(Name = "身份证号")]
        [Column("id_number")]
        public virtual String id_number
        {
            get
            {
                return _id_number;
            }
            set
            {
                if ((_id_number != value))
                {
                    SendPropertyChanging("id_number",_id_number,value);
                    _id_number = value;
                    SendPropertyChanged("id_number");
                }
            }
        }
        String _area;
        /// <summary>地区</summary>
        [MaxLength(50)]
        [Display(Name = "地区")]
        [Column("area")]
        public virtual String area
        {
            get
            {
                return _area;
            }
            set
            {
                if ((_area != value))
                {
                    SendPropertyChanging("area",_area,value);
                    _area = value;
                    SendPropertyChanged("area");
                }
            }
        }
        String _address;
        /// <summary>地址</summary>
        [MaxLength(100)]
        [Display(Name = "地址")]
        [Column("address")]
        public virtual String address
        {
            get
            {
                return _address;
            }
            set
            {
                if ((_address != value))
                {
                    SendPropertyChanging("address",_address,value);
                    _address = value;
                    SendPropertyChanged("address");
                }
            }
        }
        String _phone;
        /// <summary>电话</summary>
        [MaxLength(50)]
        [Display(Name = "电话")]
        [Column("phone")]
        public virtual String phone
        {
            get
            {
                return _phone;
            }
            set
            {
                if ((_phone != value))
                {
                    SendPropertyChanging("phone",_phone,value);
                    _phone = value;
                    SendPropertyChanged("phone");
                }
            }
        }
        String _email;
        [MaxLength(50)]
        [Column("email")]
        public virtual String email
        {
            get
            {
                return _email;
            }
            set
            {
                if ((_email != value))
                {
                    SendPropertyChanging("email",_email,value);
                    _email = value;
                    SendPropertyChanged("email");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<fa_bank_card, bool>> exp)
        {
            base.SetValue<fa_bank_card>(exp);
        }
    }
    /// <summary>提现表</summary>
    [TableConfig]
    [Table("fa_withdraw")]
    [Way.EntityDB.DataItemJsonConverter]
    public class fa_withdraw :Way.EntityDB.DataItem
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
                    SendPropertyChanging("id",_id,value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        System.Nullable<Int32> _user_id;
        /// <summary>用户id</summary>
        [Display(Name = "用户id")]
        [Column("user_id")]
        public virtual System.Nullable<Int32> user_id
        {
            get
            {
                return _user_id;
            }
            set
            {
                if ((_user_id != value))
                {
                    SendPropertyChanging("user_id",_user_id,value);
                    _user_id = value;
                    SendPropertyChanged("user_id");
                }
            }
        }
        String _user_type;
        /// <summary>用户类型 - 'user' 'lawyer'</summary>
        [MaxLength(50)]
        [Display(Name = "用户类型 - 'user' 'lawyer'")]
        [Column("user_type")]
        public virtual String user_type
        {
            get
            {
                return _user_type;
            }
            set
            {
                if ((_user_type != value))
                {
                    SendPropertyChanging("user_type",_user_type,value);
                    _user_type = value;
                    SendPropertyChanged("user_type");
                }
            }
        }
        System.Nullable<Decimal> _amount;
        /// <summary>金额</summary>
        [Display(Name = "金额")]
        [Column("amount")]
        public virtual System.Nullable<Decimal> amount
        {
            get
            {
                return _amount;
            }
            set
            {
                if ((_amount != value))
                {
                    SendPropertyChanging("amount",_amount,value);
                    _amount = value;
                    SendPropertyChanged("amount");
                }
            }
        }
        fa_withdraw_statusEnum? _status;
        /// <summary>状态</summary>
        [Display(Name = "状态")]
        [Column("status")]
        public virtual fa_withdraw_statusEnum? status
        {
            get
            {
                return _status;
            }
            set
            {
                if ((_status != value))
                {
                    SendPropertyChanging("status",_status,value);
                    _status = value;
                    SendPropertyChanged("status");
                }
            }
        }
        String _bank_name;
        /// <summary>银行名称</summary>
        [MaxLength(50)]
        [Display(Name = "银行名称")]
        [Column("bank_name")]
        public virtual String bank_name
        {
            get
            {
                return _bank_name;
            }
            set
            {
                if ((_bank_name != value))
                {
                    SendPropertyChanging("bank_name",_bank_name,value);
                    _bank_name = value;
                    SendPropertyChanged("bank_name");
                }
            }
        }
        String _create_account_bank;
        /// <summary>开户行</summary>
        [MaxLength(50)]
        [Display(Name = "开户行")]
        [Column("create_account_bank")]
        public virtual String create_account_bank
        {
            get
            {
                return _create_account_bank;
            }
            set
            {
                if ((_create_account_bank != value))
                {
                    SendPropertyChanging("create_account_bank",_create_account_bank,value);
                    _create_account_bank = value;
                    SendPropertyChanged("create_account_bank");
                }
            }
        }
        String _name;
        /// <summary>姓名</summary>
        [MaxLength(50)]
        [Display(Name = "姓名")]
        [Column("name")]
        public virtual String name
        {
            get
            {
                return _name;
            }
            set
            {
                if ((_name != value))
                {
                    SendPropertyChanging("name",_name,value);
                    _name = value;
                    SendPropertyChanged("name");
                }
            }
        }
        String _card_number;
        /// <summary>卡号</summary>
        [MaxLength(50)]
        [Display(Name = "卡号")]
        [Column("card_number")]
        public virtual String card_number
        {
            get
            {
                return _card_number;
            }
            set
            {
                if ((_card_number != value))
                {
                    SendPropertyChanging("card_number",_card_number,value);
                    _card_number = value;
                    SendPropertyChanged("card_number");
                }
            }
        }
        System.Nullable<DateTime> _create_time;
        /// <summary>创建时间</summary>
        [Display(Name = "创建时间")]
        [Column("create_time")]
        public virtual System.Nullable<DateTime> create_time
        {
            get
            {
                return _create_time;
            }
            set
            {
                if ((_create_time != value))
                {
                    SendPropertyChanging("create_time",_create_time,value);
                    _create_time = value;
                    SendPropertyChanged("create_time");
                }
            }
        }
        System.Nullable<DateTime> _pay_time;
        /// <summary>付款时间</summary>
        [Display(Name = "付款时间")]
        [Column("pay_time")]
        public virtual System.Nullable<DateTime> pay_time
        {
            get
            {
                return _pay_time;
            }
            set
            {
                if ((_pay_time != value))
                {
                    SendPropertyChanging("pay_time",_pay_time,value);
                    _pay_time = value;
                    SendPropertyChanged("pay_time");
                }
            }
        }
        System.Nullable<Decimal> _fee;
        /// <summary>手续费</summary>
        [Display(Name = "手续费")]
        [Column("fee")]
        public virtual System.Nullable<Decimal> fee
        {
            get
            {
                return _fee;
            }
            set
            {
                if ((_fee != value))
                {
                    SendPropertyChanging("fee",_fee,value);
                    _fee = value;
                    SendPropertyChanged("fee");
                }
            }
        }
        System.Nullable<Decimal> _arrvial_amount;
        /// <summary>实际到账金额</summary>
        [Display(Name = "实际到账金额")]
        [Column("arrvial_amount")]
        public virtual System.Nullable<Decimal> arrvial_amount
        {
            get
            {
                return _arrvial_amount;
            }
            set
            {
                if ((_arrvial_amount != value))
                {
                    SendPropertyChanging("arrvial_amount",_arrvial_amount,value);
                    _arrvial_amount = value;
                    SendPropertyChanged("arrvial_amount");
                }
            }
        }
        fa_withdraw_withdraw_typeEnum? _withdraw_type;
        /// <summary>提现类型</summary>
        [Display(Name = "提现类型")]
        [Column("withdraw_type")]
        public virtual fa_withdraw_withdraw_typeEnum? withdraw_type
        {
            get
            {
                return _withdraw_type;
            }
            set
            {
                if ((_withdraw_type != value))
                {
                    SendPropertyChanging("withdraw_type",_withdraw_type,value);
                    _withdraw_type = value;
                    SendPropertyChanged("withdraw_type");
                }
            }
        }
        String _id_number;
        /// <summary>身份证号</summary>
        [MaxLength(50)]
        [Display(Name = "身份证号")]
        [Column("id_number")]
        public virtual String id_number
        {
            get
            {
                return _id_number;
            }
            set
            {
                if ((_id_number != value))
                {
                    SendPropertyChanging("id_number",_id_number,value);
                    _id_number = value;
                    SendPropertyChanged("id_number");
                }
            }
        }
        System.Nullable<Int32> _fa_order_id;
        [Column("fa_order_id")]
        public virtual System.Nullable<Int32> fa_order_id
        {
            get
            {
                return _fa_order_id;
            }
            set
            {
                if ((_fa_order_id != value))
                {
                    SendPropertyChanging("fa_order_id",_fa_order_id,value);
                    _fa_order_id = value;
                    SendPropertyChanged("fa_order_id");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<fa_withdraw, bool>> exp)
        {
            base.SetValue<fa_withdraw>(exp);
        }
    }
    public enum fa_withdraw_statusEnum:int
    {
        待审批=0,
        已付款=1,
        已否决=2
    }
    public enum fa_withdraw_withdraw_typeEnum:int
    {
        余额提现=1,
        保证金退款=2
    }
    /// <summary>发票信息</summary>
    [TableConfig]
    [Table("fa_invoice_info")]
    [Way.EntityDB.DataItemJsonConverter]
    public class fa_invoice_info :Way.EntityDB.DataItem
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
                    SendPropertyChanging("id",_id,value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        System.Nullable<Int32> _case_pay_id;
        /// <summary>付款表id</summary>
        [Display(Name = "付款表id")]
        [Column("case_pay_id")]
        public virtual System.Nullable<Int32> case_pay_id
        {
            get
            {
                return _case_pay_id;
            }
            set
            {
                if ((_case_pay_id != value))
                {
                    SendPropertyChanging("case_pay_id",_case_pay_id,value);
                    _case_pay_id = value;
                    SendPropertyChanged("case_pay_id");
                }
            }
        }
        fa_invoice_info_title_typeEnum? _title_type;
        /// <summary>发票抬头类型</summary>
        [Display(Name = "发票抬头类型")]
        [Column("title_type")]
        public virtual fa_invoice_info_title_typeEnum? title_type
        {
            get
            {
                return _title_type;
            }
            set
            {
                if ((_title_type != value))
                {
                    SendPropertyChanging("title_type",_title_type,value);
                    _title_type = value;
                    SendPropertyChanged("title_type");
                }
            }
        }
        fa_invoice_info_typeEnum? _type;
        /// <summary>发票类型</summary>
        [Display(Name = "发票类型")]
        [Column("type")]
        public virtual fa_invoice_info_typeEnum? type
        {
            get
            {
                return _type;
            }
            set
            {
                if ((_type != value))
                {
                    SendPropertyChanging("type",_type,value);
                    _type = value;
                    SendPropertyChanged("type");
                }
            }
        }
        String _title;
        /// <summary>发票抬头</summary>
        [MaxLength(255)]
        [Display(Name = "发票抬头")]
        [Column("title")]
        public virtual String title
        {
            get
            {
                return _title;
            }
            set
            {
                if ((_title != value))
                {
                    SendPropertyChanging("title",_title,value);
                    _title = value;
                    SendPropertyChanged("title");
                }
            }
        }
        String _tax_id;
        /// <summary>税号</summary>
        [MaxLength(50)]
        [Display(Name = "税号")]
        [Column("tax_id")]
        public virtual String tax_id
        {
            get
            {
                return _tax_id;
            }
            set
            {
                if ((_tax_id != value))
                {
                    SendPropertyChanging("tax_id",_tax_id,value);
                    _tax_id = value;
                    SendPropertyChanged("tax_id");
                }
            }
        }
        String _phone_number;
        /// <summary>电话号码</summary>
        [MaxLength(50)]
        [Display(Name = "电话号码")]
        [Column("phone_number")]
        public virtual String phone_number
        {
            get
            {
                return _phone_number;
            }
            set
            {
                if ((_phone_number != value))
                {
                    SendPropertyChanging("phone_number",_phone_number,value);
                    _phone_number = value;
                    SendPropertyChanged("phone_number");
                }
            }
        }
        String _company_addr;
        /// <summary>单位地址</summary>
        [MaxLength(255)]
        [Display(Name = "单位地址")]
        [Column("company_addr")]
        public virtual String company_addr
        {
            get
            {
                return _company_addr;
            }
            set
            {
                if ((_company_addr != value))
                {
                    SendPropertyChanging("company_addr",_company_addr,value);
                    _company_addr = value;
                    SendPropertyChanged("company_addr");
                }
            }
        }
        String _create_account_bank;
        /// <summary>开户银行</summary>
        [MaxLength(50)]
        [Display(Name = "开户银行")]
        [Column("create_account_bank")]
        public virtual String create_account_bank
        {
            get
            {
                return _create_account_bank;
            }
            set
            {
                if ((_create_account_bank != value))
                {
                    SendPropertyChanging("create_account_bank",_create_account_bank,value);
                    _create_account_bank = value;
                    SendPropertyChanged("create_account_bank");
                }
            }
        }
        String _bank_card_number;
        /// <summary>银行账号</summary>
        [MaxLength(50)]
        [Display(Name = "银行账号")]
        [Column("bank_card_number")]
        public virtual String bank_card_number
        {
            get
            {
                return _bank_card_number;
            }
            set
            {
                if ((_bank_card_number != value))
                {
                    SendPropertyChanging("bank_card_number",_bank_card_number,value);
                    _bank_card_number = value;
                    SendPropertyChanged("bank_card_number");
                }
            }
        }
        String _contact_name;
        /// <summary>联系人</summary>
        [MaxLength(50)]
        [Display(Name = "联系人")]
        [Column("contact_name")]
        public virtual String contact_name
        {
            get
            {
                return _contact_name;
            }
            set
            {
                if ((_contact_name != value))
                {
                    SendPropertyChanging("contact_name",_contact_name,value);
                    _contact_name = value;
                    SendPropertyChanged("contact_name");
                }
            }
        }
        String _deliver_address;
        /// <summary>收件地址</summary>
        [MaxLength(255)]
        [Display(Name = "收件地址")]
        [Column("deliver_address")]
        public virtual String deliver_address
        {
            get
            {
                return _deliver_address;
            }
            set
            {
                if ((_deliver_address != value))
                {
                    SendPropertyChanging("deliver_address",_deliver_address,value);
                    _deliver_address = value;
                    SendPropertyChanged("deliver_address");
                }
            }
        }
        String _id_number;
        /// <summary>证件号</summary>
        [MaxLength(50)]
        [Display(Name = "证件号")]
        [Column("id_number")]
        public virtual String id_number
        {
            get
            {
                return _id_number;
            }
            set
            {
                if ((_id_number != value))
                {
                    SendPropertyChanging("id_number",_id_number,value);
                    _id_number = value;
                    SendPropertyChanged("id_number");
                }
            }
        }
        System.Nullable<Int32> _withdraw_id;
        /// <summary>提现表id</summary>
        [Display(Name = "提现表id")]
        [Column("withdraw_id")]
        public virtual System.Nullable<Int32> withdraw_id
        {
            get
            {
                return _withdraw_id;
            }
            set
            {
                if ((_withdraw_id != value))
                {
                    SendPropertyChanging("withdraw_id",_withdraw_id,value);
                    _withdraw_id = value;
                    SendPropertyChanged("withdraw_id");
                }
            }
        }
        System.Nullable<Int32> _createtime;
        /// <summary>创建时间</summary>
        [Display(Name = "创建时间")]
        [Column("createtime")]
        public virtual System.Nullable<Int32> createtime
        {
            get
            {
                return _createtime;
            }
            set
            {
                if ((_createtime != value))
                {
                    SendPropertyChanging("createtime",_createtime,value);
                    _createtime = value;
                    SendPropertyChanged("createtime");
                }
            }
        }
        System.Nullable<Int32> _updatetime;
        /// <summary>修改时间</summary>
        [Display(Name = "修改时间")]
        [Column("updatetime")]
        public virtual System.Nullable<Int32> updatetime
        {
            get
            {
                return _updatetime;
            }
            set
            {
                if ((_updatetime != value))
                {
                    SendPropertyChanging("updatetime",_updatetime,value);
                    _updatetime = value;
                    SendPropertyChanged("updatetime");
                }
            }
        }
        fa_invoice_info_statusEnum? _status;
        /// <summary>状态</summary>
        [Display(Name = "状态")]
        [Column("status")]
        public virtual fa_invoice_info_statusEnum? status
        {
            get
            {
                return _status;
            }
            set
            {
                if ((_status != value))
                {
                    SendPropertyChanging("status",_status,value);
                    _status = value;
                    SendPropertyChanged("status");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<fa_invoice_info, bool>> exp)
        {
            base.SetValue<fa_invoice_info>(exp);
        }
    }
    public enum fa_invoice_info_title_typeEnum:int
    {
        单位=1,
        个人=2
    }
    public enum fa_invoice_info_typeEnum:int
    {
        普通发票=1,
        增值税发票=2
    }
    public enum fa_invoice_info_statusEnum:int
    {
        已开具=1,
        未开具=2
    }
    /// <summary>待客服审核案件</summary>
    [TableConfig]
    [Table("fa_case_exam")]
    [Way.EntityDB.DataItemJsonConverter]
    public class fa_case_exam :Way.EntityDB.DataItem
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
                    SendPropertyChanging("id",_id,value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        System.Nullable<Int32> _case_id;
        /// <summary>案件id</summary>
        [Display(Name = "案件id")]
        [Column("case_id")]
        public virtual System.Nullable<Int32> case_id
        {
            get
            {
                return _case_id;
            }
            set
            {
                if ((_case_id != value))
                {
                    SendPropertyChanging("case_id",_case_id,value);
                    _case_id = value;
                    SendPropertyChanged("case_id");
                }
            }
        }
        System.Nullable<DateTime> _exam_time;
        /// <summary>审核时间</summary>
        [Display(Name = "审核时间")]
        [Column("exam_time")]
        public virtual System.Nullable<DateTime> exam_time
        {
            get
            {
                return _exam_time;
            }
            set
            {
                if ((_exam_time != value))
                {
                    SendPropertyChanging("exam_time",_exam_time,value);
                    _exam_time = value;
                    SendPropertyChanged("exam_time");
                }
            }
        }
        System.Nullable<Int32> _lawyer_id;
        /// <summary>委派律师id</summary>
        [Display(Name = "委派律师id")]
        [Column("lawyer_id")]
        public virtual System.Nullable<Int32> lawyer_id
        {
            get
            {
                return _lawyer_id;
            }
            set
            {
                if ((_lawyer_id != value))
                {
                    SendPropertyChanging("lawyer_id",_lawyer_id,value);
                    _lawyer_id = value;
                    SendPropertyChanged("lawyer_id");
                }
            }
        }
        System.Nullable<Int32> _type;
        /// <summary>类型 - 新案件1 更换律师2</summary>
        [Display(Name = "类型 - 新案件1 更换律师2")]
        [Column("type")]
        public virtual System.Nullable<Int32> type
        {
            get
            {
                return _type;
            }
            set
            {
                if ((_type != value))
                {
                    SendPropertyChanging("type",_type,value);
                    _type = value;
                    SendPropertyChanged("type");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<fa_case_exam, bool>> exp)
        {
            base.SetValue<fa_case_exam>(exp);
        }
    }
    /// <summary>退款表</summary>
    [TableConfig]
    [Table("fa_pay_back")]
    [Way.EntityDB.DataItemJsonConverter]
    public class fa_pay_back :Way.EntityDB.DataItem
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
                    SendPropertyChanging("id",_id,value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        System.Nullable<Int32> _case_id;
        /// <summary>案件id</summary>
        [Display(Name = "案件id")]
        [Column("case_id")]
        public virtual System.Nullable<Int32> case_id
        {
            get
            {
                return _case_id;
            }
            set
            {
                if ((_case_id != value))
                {
                    SendPropertyChanging("case_id",_case_id,value);
                    _case_id = value;
                    SendPropertyChanged("case_id");
                }
            }
        }
        System.Nullable<Int32> _fa_case_pay_id;
        /// <summary>付款id</summary>
        [Display(Name = "付款id")]
        [Column("fa_case_pay_id")]
        public virtual System.Nullable<Int32> fa_case_pay_id
        {
            get
            {
                return _fa_case_pay_id;
            }
            set
            {
                if ((_fa_case_pay_id != value))
                {
                    SendPropertyChanging("fa_case_pay_id",_fa_case_pay_id,value);
                    _fa_case_pay_id = value;
                    SendPropertyChanged("fa_case_pay_id");
                }
            }
        }
        String _reason;
        /// <summary>退款原因</summary>
        [MaxLength(255)]
        [Display(Name = "退款原因")]
        [Column("reason")]
        public virtual String reason
        {
            get
            {
                return _reason;
            }
            set
            {
                if ((_reason != value))
                {
                    SendPropertyChanging("reason",_reason,value);
                    _reason = value;
                    SendPropertyChanged("reason");
                }
            }
        }
        System.Nullable<DateTime> _create_time;
        /// <summary>退款时间</summary>
        [Display(Name = "退款时间")]
        [Column("create_time")]
        public virtual System.Nullable<DateTime> create_time
        {
            get
            {
                return _create_time;
            }
            set
            {
                if ((_create_time != value))
                {
                    SendPropertyChanging("create_time",_create_time,value);
                    _create_time = value;
                    SendPropertyChanged("create_time");
                }
            }
        }
        String _exam;
        /// <summary>退款审核人</summary>
        [MaxLength(50)]
        [Display(Name = "退款审核人")]
        [Column("exam")]
        public virtual String exam
        {
            get
            {
                return _exam;
            }
            set
            {
                if ((_exam != value))
                {
                    SendPropertyChanging("exam",_exam,value);
                    _exam = value;
                    SendPropertyChanged("exam");
                }
            }
        }
        System.Nullable<DateTime> _exam_time;
        /// <summary>退款审核时间</summary>
        [Display(Name = "退款审核时间")]
        [Column("exam_time")]
        public virtual System.Nullable<DateTime> exam_time
        {
            get
            {
                return _exam_time;
            }
            set
            {
                if ((_exam_time != value))
                {
                    SendPropertyChanging("exam_time",_exam_time,value);
                    _exam_time = value;
                    SendPropertyChanged("exam_time");
                }
            }
        }
        System.Nullable<DateTime> _arrival_time;
        /// <summary>到账时间</summary>
        [Display(Name = "到账时间")]
        [Column("arrival_time")]
        public virtual System.Nullable<DateTime> arrival_time
        {
            get
            {
                return _arrival_time;
            }
            set
            {
                if ((_arrival_time != value))
                {
                    SendPropertyChanging("arrival_time",_arrival_time,value);
                    _arrival_time = value;
                    SendPropertyChanged("arrival_time");
                }
            }
        }
        fa_pay_back_statusEnum? _status;
        /// <summary>状态</summary>
        [Display(Name = "状态")]
        [Column("status")]
        public virtual fa_pay_back_statusEnum? status
        {
            get
            {
                return _status;
            }
            set
            {
                if ((_status != value))
                {
                    SendPropertyChanging("status",_status,value);
                    _status = value;
                    SendPropertyChanged("status");
                }
            }
        }
        String _refund_no;
        /// <summary>退款单号</summary>
        [MaxLength(50)]
        [Display(Name = "退款单号")]
        [Column("refund_no")]
        public virtual String refund_no
        {
            get
            {
                return _refund_no;
            }
            set
            {
                if ((_refund_no != value))
                {
                    SendPropertyChanging("refund_no",_refund_no,value);
                    _refund_no = value;
                    SendPropertyChanged("refund_no");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<fa_pay_back, bool>> exp)
        {
            base.SetValue<fa_pay_back>(exp);
        }
    }
    public enum fa_pay_back_statusEnum:int
    {
        已拒绝=-1,
        审核中=1,
        已通过=2,
        退款中=3,
        已退款成功=4,
        退款失败=-2
    }
    /// <summary>律师信息</summary>
    [TableConfig]
    [Table("fa_lawyerinfo")]
    [Way.EntityDB.DataItemJsonConverter]
    public class fa_lawyerinfo :Way.EntityDB.DataItem
    {
        System.Nullable<Int32> _id;
        /// <summary>ID</summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Display(Name = "ID")]
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
        System.Nullable<Int32> _user_id;
        /// <summary>申请用户ID</summary>
        [Display(Name = "申请用户ID")]
        [Column("user_id")]
        public virtual System.Nullable<Int32> user_id
        {
            get
            {
                return _user_id;
            }
            set
            {
                if ((_user_id != value))
                {
                    SendPropertyChanging("user_id",_user_id,value);
                    _user_id = value;
                    SendPropertyChanged("user_id");
                }
            }
        }
        System.Nullable<Int32> _is_type=1;
        /// <summary>类型:0=律所,1=律师</summary>
        [Display(Name = "类型:0=律所,1=律师")]
        [Column("is_type")]
        public virtual System.Nullable<Int32> is_type
        {
            get
            {
                return _is_type;
            }
            set
            {
                if ((_is_type != value))
                {
                    SendPropertyChanging("is_type",_is_type,value);
                    _is_type = value;
                    SendPropertyChanged("is_type");
                }
            }
        }
        String _user_type="lawyer";
        [MaxLength(20)]
        [Column("user_type")]
        public virtual String user_type
        {
            get
            {
                return _user_type;
            }
            set
            {
                if ((_user_type != value))
                {
                    SendPropertyChanging("user_type",_user_type,value);
                    _user_type = value;
                    SendPropertyChanged("user_type");
                }
            }
        }
        System.Nullable<Int32> _agent_id;
        /// <summary>代理商ID</summary>
        [Display(Name = "代理商ID")]
        [Column("agent_id")]
        public virtual System.Nullable<Int32> agent_id
        {
            get
            {
                return _agent_id;
            }
            set
            {
                if ((_agent_id != value))
                {
                    SendPropertyChanging("agent_id",_agent_id,value);
                    _agent_id = value;
                    SendPropertyChanged("agent_id");
                }
            }
        }
        System.Nullable<Int32> _lawfirm_id;
        /// <summary>归属律所</summary>
        [Display(Name = "归属律所")]
        [Column("lawfirm_id")]
        public virtual System.Nullable<Int32> lawfirm_id
        {
            get
            {
                return _lawfirm_id;
            }
            set
            {
                if ((_lawfirm_id != value))
                {
                    SendPropertyChanging("lawfirm_id",_lawfirm_id,value);
                    _lawfirm_id = value;
                    SendPropertyChanged("lawfirm_id");
                }
            }
        }
        String _lawfirm_name;
        /// <summary>律所名称</summary>
        [MaxLength(255)]
        [Display(Name = "律所名称")]
        [Column("lawfirm_name")]
        public virtual String lawfirm_name
        {
            get
            {
                return _lawfirm_name;
            }
            set
            {
                if ((_lawfirm_name != value))
                {
                    SendPropertyChanging("lawfirm_name",_lawfirm_name,value);
                    _lawfirm_name = value;
                    SendPropertyChanged("lawfirm_name");
                }
            }
        }
        String _name;
        /// <summary>律师名称</summary>
        [MaxLength(20)]
        [Display(Name = "律师名称")]
        [Column("name")]
        public virtual String name
        {
            get
            {
                return _name;
            }
            set
            {
                if ((_name != value))
                {
                    SendPropertyChanging("name",_name,value);
                    _name = value;
                    SendPropertyChanged("name");
                }
            }
        }
        String _login_name;
        /// <summary>登录名称</summary>
        [MaxLength(20)]
        [Display(Name = "登录名称")]
        [Column("login_name")]
        public virtual String login_name
        {
            get
            {
                return _login_name;
            }
            set
            {
                if ((_login_name != value))
                {
                    SendPropertyChanging("login_name",_login_name,value);
                    _login_name = value;
                    SendPropertyChanged("login_name");
                }
            }
        }
        String _login_pwd;
        /// <summary>登录密码</summary>
        [MaxLength(250)]
        [Display(Name = "登录密码")]
        [Column("login_pwd")]
        public virtual String login_pwd
        {
            get
            {
                return _login_pwd;
            }
            set
            {
                if ((_login_pwd != value))
                {
                    SendPropertyChanging("login_pwd",_login_pwd,value);
                    _login_pwd = value;
                    SendPropertyChanged("login_pwd");
                }
            }
        }
        System.Nullable<Int32> _login_audit=0;
        /// <summary>登录状态:0=登录,1=未登录</summary>
        [Display(Name = "登录状态:0=登录,1=未登录")]
        [Column("login_audit")]
        public virtual System.Nullable<Int32> login_audit
        {
            get
            {
                return _login_audit;
            }
            set
            {
                if ((_login_audit != value))
                {
                    SendPropertyChanging("login_audit",_login_audit,value);
                    _login_audit = value;
                    SendPropertyChanged("login_audit");
                }
            }
        }
        String _law_pic;
        /// <summary>个人照片</summary>
        [MaxLength(1000)]
        [Display(Name = "个人照片")]
        [Column("law_pic")]
        public virtual String law_pic
        {
            get
            {
                return _law_pic;
            }
            set
            {
                if ((_law_pic != value))
                {
                    SendPropertyChanging("law_pic",_law_pic,value);
                    _law_pic = value;
                    SendPropertyChanged("law_pic");
                }
            }
        }
        String _license_number;
        /// <summary>执业证号</summary>
        [MaxLength(50)]
        [Display(Name = "执业证号")]
        [Column("license_number")]
        public virtual String license_number
        {
            get
            {
                return _license_number;
            }
            set
            {
                if ((_license_number != value))
                {
                    SendPropertyChanging("license_number",_license_number,value);
                    _license_number = value;
                    SendPropertyChanged("license_number");
                }
            }
        }
        String _duties;
        /// <summary>职务</summary>
        [MaxLength(20)]
        [Display(Name = "职务")]
        [Column("duties")]
        public virtual String duties
        {
            get
            {
                return _duties;
            }
            set
            {
                if ((_duties != value))
                {
                    SendPropertyChanging("duties",_duties,value);
                    _duties = value;
                    SendPropertyChanged("duties");
                }
            }
        }
        String _year;
        /// <summary>职业年份</summary>
        [MaxLength(20)]
        [Display(Name = "职业年份")]
        [Column("year")]
        public virtual String year
        {
            get
            {
                return _year;
            }
            set
            {
                if ((_year != value))
                {
                    SendPropertyChanging("year",_year,value);
                    _year = value;
                    SendPropertyChanged("year");
                }
            }
        }
        String _content;
        /// <summary>个人简介</summary>
        [Display(Name = "个人简介")]
        [Column("content")]
        public virtual String content
        {
            get
            {
                return _content;
            }
            set
            {
                if ((_content != value))
                {
                    SendPropertyChanging("content",_content,value);
                    _content = value;
                    SendPropertyChanged("content");
                }
            }
        }
        String _law_video;
        /// <summary>视频</summary>
        [Display(Name = "视频")]
        [Column("law_video")]
        public virtual String law_video
        {
            get
            {
                return _law_video;
            }
            set
            {
                if ((_law_video != value))
                {
                    SendPropertyChanging("law_video",_law_video,value);
                    _law_video = value;
                    SendPropertyChanged("law_video");
                }
            }
        }
        String _tel;
        /// <summary>联系方式</summary>
        [MaxLength(20)]
        [Display(Name = "联系方式")]
        [Column("tel")]
        public virtual String tel
        {
            get
            {
                return _tel;
            }
            set
            {
                if ((_tel != value))
                {
                    SendPropertyChanging("tel",_tel,value);
                    _tel = value;
                    SendPropertyChanged("tel");
                }
            }
        }
        String _field;
        /// <summary>擅长领域</summary>
        [MaxLength(100)]
        [Display(Name = "擅长领域")]
        [Column("field")]
        public virtual String field
        {
            get
            {
                return _field;
            }
            set
            {
                if ((_field != value))
                {
                    SendPropertyChanging("field",_field,value);
                    _field = value;
                    SendPropertyChanged("field");
                }
            }
        }
        String _tags;
        /// <summary>个人标签</summary>
        [MaxLength(255)]
        [Display(Name = "个人标签")]
        [Column("tags")]
        public virtual String tags
        {
            get
            {
                return _tags;
            }
            set
            {
                if ((_tags != value))
                {
                    SendPropertyChanging("tags",_tags,value);
                    _tags = value;
                    SendPropertyChanged("tags");
                }
            }
        }
        String _server_company;
        /// <summary>服务过的企业</summary>
        [Display(Name = "服务过的企业")]
        [Column("server_company")]
        public virtual String server_company
        {
            get
            {
                return _server_company;
            }
            set
            {
                if ((_server_company != value))
                {
                    SendPropertyChanging("server_company",_server_company,value);
                    _server_company = value;
                    SendPropertyChanged("server_company");
                }
            }
        }
        String _skills;
        /// <summary>专业技能</summary>
        [Display(Name = "专业技能")]
        [Column("skills")]
        public virtual String skills
        {
            get
            {
                return _skills;
            }
            set
            {
                if ((_skills != value))
                {
                    SendPropertyChanging("skills",_skills,value);
                    _skills = value;
                    SendPropertyChanged("skills");
                }
            }
        }
        String _industry;
        /// <summary>熟悉行业</summary>
        [Display(Name = "熟悉行业")]
        [Column("industry")]
        public virtual String industry
        {
            get
            {
                return _industry;
            }
            set
            {
                if ((_industry != value))
                {
                    SendPropertyChanging("industry",_industry,value);
                    _industry = value;
                    SendPropertyChanged("industry");
                }
            }
        }
        String _language;
        /// <summary>语言能力</summary>
        [Display(Name = "语言能力")]
        [Column("language")]
        public virtual String language
        {
            get
            {
                return _language;
            }
            set
            {
                if ((_language != value))
                {
                    SendPropertyChanging("language",_language,value);
                    _language = value;
                    SendPropertyChanged("language");
                }
            }
        }
        String _court;
        /// <summary>常去法院</summary>
        [Display(Name = "常去法院")]
        [Column("court")]
        public virtual String court
        {
            get
            {
                return _court;
            }
            set
            {
                if ((_court != value))
                {
                    SendPropertyChanging("court",_court,value);
                    _court = value;
                    SendPropertyChanged("court");
                }
            }
        }
        String _procuratorate;
        /// <summary>常去检察院</summary>
        [Display(Name = "常去检察院")]
        [Column("procuratorate")]
        public virtual String procuratorate
        {
            get
            {
                return _procuratorate;
            }
            set
            {
                if ((_procuratorate != value))
                {
                    SendPropertyChanging("procuratorate",_procuratorate,value);
                    _procuratorate = value;
                    SendPropertyChanged("procuratorate");
                }
            }
        }
        String _serverCase;
        /// <summary>服务案件</summary>
        [Display(Name = "服务案件")]
        [Column("servercase")]
        public virtual String serverCase
        {
            get
            {
                return _serverCase;
            }
            set
            {
                if ((_serverCase != value))
                {
                    SendPropertyChanging("serverCase",_serverCase,value);
                    _serverCase = value;
                    SendPropertyChanged("serverCase");
                }
            }
        }
        System.Nullable<Int32> _audit=0;
        /// <summary>审核状态:0=待审核,2=审核通过,3=驳回</summary>
        [Display(Name = "审核状态:0=待审核,2=审核通过,3=驳回")]
        [Column("audit")]
        public virtual System.Nullable<Int32> audit
        {
            get
            {
                return _audit;
            }
            set
            {
                if ((_audit != value))
                {
                    SendPropertyChanging("audit",_audit,value);
                    _audit = value;
                    SendPropertyChanged("audit");
                }
            }
        }
        System.Nullable<DateTime> _input_time;
        /// <summary>添加时间</summary>
        [Display(Name = "添加时间")]
        [Column("input_time")]
        public virtual System.Nullable<DateTime> input_time
        {
            get
            {
                return _input_time;
            }
            set
            {
                if ((_input_time != value))
                {
                    SendPropertyChanging("input_time",_input_time,value);
                    _input_time = value;
                    SendPropertyChanged("input_time");
                }
            }
        }
        System.Nullable<Int32> _server_number=0;
        /// <summary>服务人数</summary>
        [Display(Name = "服务人数")]
        [Column("server_number")]
        public virtual System.Nullable<Int32> server_number
        {
            get
            {
                return _server_number;
            }
            set
            {
                if ((_server_number != value))
                {
                    SendPropertyChanging("server_number",_server_number,value);
                    _server_number = value;
                    SendPropertyChanged("server_number");
                }
            }
        }
        System.Nullable<Int32> _score=0;
        /// <summary>总评分</summary>
        [Display(Name = "总评分")]
        [Column("score")]
        public virtual System.Nullable<Int32> score
        {
            get
            {
                return _score;
            }
            set
            {
                if ((_score != value))
                {
                    SendPropertyChanging("score",_score,value);
                    _score = value;
                    SendPropertyChanged("score");
                }
            }
        }
        System.Nullable<Int32> _score_num;
        /// <summary>评分人数</summary>
        [Display(Name = "评分人数")]
        [Column("score_num")]
        public virtual System.Nullable<Int32> score_num
        {
            get
            {
                return _score_num;
            }
            set
            {
                if ((_score_num != value))
                {
                    SendPropertyChanging("score_num",_score_num,value);
                    _score_num = value;
                    SendPropertyChanged("score_num");
                }
            }
        }
        System.Nullable<Int32> _collect=0;
        /// <summary>收藏数</summary>
        [Display(Name = "收藏数")]
        [Column("collect")]
        public virtual System.Nullable<Int32> collect
        {
            get
            {
                return _collect;
            }
            set
            {
                if ((_collect != value))
                {
                    SendPropertyChanging("collect",_collect,value);
                    _collect = value;
                    SendPropertyChanged("collect");
                }
            }
        }
        System.Nullable<Int32> _active;
        /// <summary>活跃度</summary>
        [Display(Name = "活跃度")]
        [Column("active")]
        public virtual System.Nullable<Int32> active
        {
            get
            {
                return _active;
            }
            set
            {
                if ((_active != value))
                {
                    SendPropertyChanging("active",_active,value);
                    _active = value;
                    SendPropertyChanged("active");
                }
            }
        }
        String _province;
        /// <summary>省份</summary>
        [MaxLength(100)]
        [Display(Name = "省份")]
        [Column("province")]
        public virtual String province
        {
            get
            {
                return _province;
            }
            set
            {
                if ((_province != value))
                {
                    SendPropertyChanging("province",_province,value);
                    _province = value;
                    SendPropertyChanged("province");
                }
            }
        }
        String _city_num;
        /// <summary>城市ID</summary>
        [MaxLength(255)]
        [Display(Name = "城市ID")]
        [Column("city_num")]
        public virtual String city_num
        {
            get
            {
                return _city_num;
            }
            set
            {
                if ((_city_num != value))
                {
                    SendPropertyChanging("city_num",_city_num,value);
                    _city_num = value;
                    SendPropertyChanged("city_num");
                }
            }
        }
        String _city;
        /// <summary>城市</summary>
        [MaxLength(255)]
        [Display(Name = "城市")]
        [Column("city")]
        public virtual String city
        {
            get
            {
                return _city;
            }
            set
            {
                if ((_city != value))
                {
                    SendPropertyChanging("city",_city,value);
                    _city = value;
                    SendPropertyChanged("city");
                }
            }
        }
        System.Nullable<Int32> _recommend=0;
        /// <summary>是否推荐:0=否,1=推荐</summary>
        [Display(Name = "是否推荐:0=否,1=推荐")]
        [Column("recommend")]
        public virtual System.Nullable<Int32> recommend
        {
            get
            {
                return _recommend;
            }
            set
            {
                if ((_recommend != value))
                {
                    SendPropertyChanging("recommend",_recommend,value);
                    _recommend = value;
                    SendPropertyChanged("recommend");
                }
            }
        }
        System.Nullable<DateTime> _login_time;
        /// <summary>登录时间</summary>
        [Display(Name = "登录时间")]
        [Column("login_time")]
        public virtual System.Nullable<DateTime> login_time
        {
            get
            {
                return _login_time;
            }
            set
            {
                if ((_login_time != value))
                {
                    SendPropertyChanging("login_time",_login_time,value);
                    _login_time = value;
                    SendPropertyChanged("login_time");
                }
            }
        }
        System.Nullable<Int32> _practising=0;
        /// <summary>职业认证:0=未认证,1=认证</summary>
        [Display(Name = "职业认证:0=未认证,1=认证")]
        [Column("practising")]
        public virtual System.Nullable<Int32> practising
        {
            get
            {
                return _practising;
            }
            set
            {
                if ((_practising != value))
                {
                    SendPropertyChanging("practising",_practising,value);
                    _practising = value;
                    SendPropertyChanged("practising");
                }
            }
        }
        System.Nullable<Int32> _platform=0;
        /// <summary>平台认证:0=未认证,1=认证</summary>
        [Display(Name = "平台认证:0=未认证,1=认证")]
        [Column("platform")]
        public virtual System.Nullable<Int32> platform
        {
            get
            {
                return _platform;
            }
            set
            {
                if ((_platform != value))
                {
                    SendPropertyChanging("platform",_platform,value);
                    _platform = value;
                    SendPropertyChanged("platform");
                }
            }
        }
        System.Nullable<Int32> _real_name=0;
        /// <summary>实名认证:0=未认证,1=认证</summary>
        [Display(Name = "实名认证:0=未认证,1=认证")]
        [Column("real_name")]
        public virtual System.Nullable<Int32> real_name
        {
            get
            {
                return _real_name;
            }
            set
            {
                if ((_real_name != value))
                {
                    SendPropertyChanging("real_name",_real_name,value);
                    _real_name = value;
                    SendPropertyChanged("real_name");
                }
            }
        }
        String _address;
        /// <summary>工作地址</summary>
        [MaxLength(255)]
        [Display(Name = "工作地址")]
        [Column("address")]
        public virtual String address
        {
            get
            {
                return _address;
            }
            set
            {
                if ((_address != value))
                {
                    SendPropertyChanging("address",_address,value);
                    _address = value;
                    SendPropertyChanged("address");
                }
            }
        }
        String _law_location;
        /// <summary>工作位置坐标</summary>
        [MaxLength(255)]
        [Display(Name = "工作位置坐标")]
        [Column("law_location")]
        public virtual String law_location
        {
            get
            {
                return _law_location;
            }
            set
            {
                if ((_law_location != value))
                {
                    SendPropertyChanging("law_location",_law_location,value);
                    _law_location = value;
                    SendPropertyChanged("law_location");
                }
            }
        }
        System.Nullable<Int32> _isdel=0;
        /// <summary>是否删除:0=否,1=删除</summary>
        [Display(Name = "是否删除:0=否,1=删除")]
        [Column("isdel")]
        public virtual System.Nullable<Int32> isdel
        {
            get
            {
                return _isdel;
            }
            set
            {
                if ((_isdel != value))
                {
                    SendPropertyChanging("isdel",_isdel,value);
                    _isdel = value;
                    SendPropertyChanged("isdel");
                }
            }
        }
        System.Nullable<Int32> _sort;
        /// <summary>排序</summary>
        [Display(Name = "排序")]
        [Column("sort")]
        public virtual System.Nullable<Int32> sort
        {
            get
            {
                return _sort;
            }
            set
            {
                if ((_sort != value))
                {
                    SendPropertyChanging("sort",_sort,value);
                    _sort = value;
                    SendPropertyChanged("sort");
                }
            }
        }
        String _card_just;
        /// <summary>身份证正面</summary>
        [MaxLength(255)]
        [Display(Name = "身份证正面")]
        [Column("card_just")]
        public virtual String card_just
        {
            get
            {
                return _card_just;
            }
            set
            {
                if ((_card_just != value))
                {
                    SendPropertyChanging("card_just",_card_just,value);
                    _card_just = value;
                    SendPropertyChanged("card_just");
                }
            }
        }
        String _card_back;
        /// <summary>身份证背面</summary>
        [MaxLength(255)]
        [Display(Name = "身份证背面")]
        [Column("card_back")]
        public virtual String card_back
        {
            get
            {
                return _card_back;
            }
            set
            {
                if ((_card_back != value))
                {
                    SendPropertyChanging("card_back",_card_back,value);
                    _card_back = value;
                    SendPropertyChanged("card_back");
                }
            }
        }
        String _license_pic;
        /// <summary>职业照片</summary>
        [MaxLength(255)]
        [Display(Name = "职业照片")]
        [Column("license_pic")]
        public virtual String license_pic
        {
            get
            {
                return _license_pic;
            }
            set
            {
                if ((_license_pic != value))
                {
                    SendPropertyChanging("license_pic",_license_pic,value);
                    _license_pic = value;
                    SendPropertyChanged("license_pic");
                }
            }
        }
        System.Nullable<Decimal> _account;
        /// <summary>账户</summary>
        [Display(Name = "账户")]
        [Column("account")]
        public virtual System.Nullable<Decimal> account
        {
            get
            {
                return _account;
            }
            set
            {
                if ((_account != value))
                {
                    SendPropertyChanging("account",_account,value);
                    _account = value;
                    SendPropertyChanged("account");
                }
            }
        }
        String _school;
        /// <summary>毕业学校</summary>
        [MaxLength(255)]
        [Display(Name = "毕业学校")]
        [Column("school")]
        public virtual String school
        {
            get
            {
                return _school;
            }
            set
            {
                if ((_school != value))
                {
                    SendPropertyChanging("school",_school,value);
                    _school = value;
                    SendPropertyChanged("school");
                }
            }
        }
        String _law_tel;
        /// <summary>律所电话</summary>
        [MaxLength(255)]
        [Display(Name = "律所电话")]
        [Column("law_tel")]
        public virtual String law_tel
        {
            get
            {
                return _law_tel;
            }
            set
            {
                if ((_law_tel != value))
                {
                    SendPropertyChanging("law_tel",_law_tel,value);
                    _law_tel = value;
                    SendPropertyChanged("law_tel");
                }
            }
        }
        String _remarks;
        /// <summary>备注</summary>
        [MaxLength(255)]
        [Display(Name = "备注")]
        [Column("remarks")]
        public virtual String remarks
        {
            get
            {
                return _remarks;
            }
            set
            {
                if ((_remarks != value))
                {
                    SendPropertyChanging("remarks",_remarks,value);
                    _remarks = value;
                    SendPropertyChanged("remarks");
                }
            }
        }
        System.Nullable<Decimal> _money;
        /// <summary>余额</summary>
        [Display(Name = "余额")]
        [Column("money")]
        public virtual System.Nullable<Decimal> money
        {
            get
            {
                return _money;
            }
            set
            {
                if ((_money != value))
                {
                    SendPropertyChanging("money",_money,value);
                    _money = value;
                    SendPropertyChanged("money");
                }
            }
        }
        System.Nullable<Int32> _sign_score;
        /// <summary>签到积分</summary>
        [Display(Name = "签到积分")]
        [Column("sign_score")]
        public virtual System.Nullable<Int32> sign_score
        {
            get
            {
                return _sign_score;
            }
            set
            {
                if ((_sign_score != value))
                {
                    SendPropertyChanging("sign_score",_sign_score,value);
                    _sign_score = value;
                    SendPropertyChanged("sign_score");
                }
            }
        }
        System.Nullable<Int64> _viptime;
        /// <summary>VIP到期时间</summary>
        [Display(Name = "VIP到期时间")]
        [Column("viptime")]
        public virtual System.Nullable<Int64> viptime
        {
            get
            {
                return _viptime;
            }
            set
            {
                if ((_viptime != value))
                {
                    SendPropertyChanging("viptime",_viptime,value);
                    _viptime = value;
                    SendPropertyChanged("viptime");
                }
            }
        }
        String _invite_code;
        /// <summary>邀请码</summary>
        [MaxLength(255)]
        [Display(Name = "邀请码")]
        [Column("invite_code")]
        public virtual String invite_code
        {
            get
            {
                return _invite_code;
            }
            set
            {
                if ((_invite_code != value))
                {
                    SendPropertyChanging("invite_code",_invite_code,value);
                    _invite_code = value;
                    SendPropertyChanged("invite_code");
                }
            }
        }
        System.Nullable<Decimal> _margin=0m;
        /// <summary>保证金</summary>
        [Display(Name = "保证金")]
        [Column("margin")]
        public virtual System.Nullable<Decimal> margin
        {
            get
            {
                return _margin;
            }
            set
            {
                if ((_margin != value))
                {
                    SendPropertyChanging("margin",_margin,value);
                    _margin = value;
                    SendPropertyChanged("margin");
                }
            }
        }
        fa_lawyerinfo_statusEnum? _status;
        /// <summary>状态</summary>
        [Display(Name = "状态")]
        [Column("status")]
        public virtual fa_lawyerinfo_statusEnum? status
        {
            get
            {
                return _status;
            }
            set
            {
                if ((_status != value))
                {
                    SendPropertyChanging("status",_status,value);
                    _status = value;
                    SendPropertyChanged("status");
                }
            }
        }
        String _pay_pwd;
        /// <summary>支付密码</summary>
        [MaxLength(50)]
        [Display(Name = "支付密码")]
        [Column("pay_pwd")]
        public virtual String pay_pwd
        {
            get
            {
                return _pay_pwd;
            }
            set
            {
                if ((_pay_pwd != value))
                {
                    SendPropertyChanging("pay_pwd",_pay_pwd,value);
                    _pay_pwd = value;
                    SendPropertyChanged("pay_pwd");
                }
            }
        }
        System.Nullable<Boolean> _is_online=false;
        /// <summary>是否在线</summary>
        [Display(Name = "是否在线")]
        [Column("is_online")]
        public virtual System.Nullable<Boolean> is_online
        {
            get
            {
                return _is_online;
            }
            set
            {
                if ((_is_online != value))
                {
                    SendPropertyChanging("is_online",_is_online,value);
                    _is_online = value;
                    SendPropertyChanged("is_online");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<fa_lawyerinfo, bool>> exp)
        {
            base.SetValue<fa_lawyerinfo>(exp);
        }
    }
    public enum fa_lawyerinfo_statusEnum:int
    {
        /// <summary>在线</summary>
        online = 1,
        /// <summary>离线</summary>
        offline = 2
    }
    /// <summary>案件表</summary>
    [TableConfig]
    [Table("fa_case_source")]
    [Way.EntityDB.DataItemJsonConverter]
    public class fa_case_source :Way.EntityDB.DataItem
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
        System.Nullable<Int32> _agent_id;
        /// <summary>代理人</summary>
        [Display(Name = "代理人")]
        [Column("agent_id")]
        public virtual System.Nullable<Int32> agent_id
        {
            get
            {
                return _agent_id;
            }
            set
            {
                if ((_agent_id != value))
                {
                    SendPropertyChanging("agent_id",_agent_id,value);
                    _agent_id = value;
                    SendPropertyChanged("agent_id");
                }
            }
        }
        String _author;
        /// <summary>发布人</summary>
        [MaxLength(50)]
        [Display(Name = "发布人")]
        [Column("author")]
        public virtual String author
        {
            get
            {
                return _author;
            }
            set
            {
                if ((_author != value))
                {
                    SendPropertyChanging("author",_author,value);
                    _author = value;
                    SendPropertyChanged("author");
                }
            }
        }
        String _title;
        /// <summary>标题</summary>
        [MaxLength(255)]
        [Display(Name = "标题")]
        [Column("title")]
        public virtual String title
        {
            get
            {
                return _title;
            }
            set
            {
                if ((_title != value))
                {
                    SendPropertyChanging("title",_title,value);
                    _title = value;
                    SendPropertyChanged("title");
                }
            }
        }
        String _name;
        /// <summary>姓名</summary>
        [MaxLength(50)]
        [Display(Name = "姓名")]
        [Column("name")]
        public virtual String name
        {
            get
            {
                return _name;
            }
            set
            {
                if ((_name != value))
                {
                    SendPropertyChanging("name",_name,value);
                    _name = value;
                    SendPropertyChanged("name");
                }
            }
        }
        String _tel;
        /// <summary>联系方式</summary>
        [MaxLength(50)]
        [Display(Name = "联系方式")]
        [Column("tel")]
        public virtual String tel
        {
            get
            {
                return _tel;
            }
            set
            {
                if ((_tel != value))
                {
                    SendPropertyChanging("tel",_tel,value);
                    _tel = value;
                    SendPropertyChanged("tel");
                }
            }
        }
        System.Nullable<Int32> _classa;
        /// <summary>一级分类</summary>
        [Display(Name = "一级分类")]
        [Column("classa")]
        public virtual System.Nullable<Int32> classa
        {
            get
            {
                return _classa;
            }
            set
            {
                if ((_classa != value))
                {
                    SendPropertyChanging("classa",_classa,value);
                    _classa = value;
                    SendPropertyChanged("classa");
                }
            }
        }
        System.Nullable<Int32> _classb;
        /// <summary>二级分类</summary>
        [Display(Name = "二级分类")]
        [Column("classb")]
        public virtual System.Nullable<Int32> classb
        {
            get
            {
                return _classb;
            }
            set
            {
                if ((_classb != value))
                {
                    SendPropertyChanging("classb",_classb,value);
                    _classb = value;
                    SendPropertyChanged("classb");
                }
            }
        }
        String _city;
        /// <summary>城市名</summary>
        [MaxLength(255)]
        [Display(Name = "城市名")]
        [Column("city")]
        public virtual String city
        {
            get
            {
                return _city;
            }
            set
            {
                if ((_city != value))
                {
                    SendPropertyChanging("city",_city,value);
                    _city = value;
                    SendPropertyChanged("city");
                }
            }
        }
        System.Nullable<Int32> _city_code;
        /// <summary>城市邮编</summary>
        [Display(Name = "城市邮编")]
        [Column("city_code")]
        public virtual System.Nullable<Int32> city_code
        {
            get
            {
                return _city_code;
            }
            set
            {
                if ((_city_code != value))
                {
                    SendPropertyChanging("city_code",_city_code,value);
                    _city_code = value;
                    SendPropertyChanged("city_code");
                }
            }
        }
        String _content;
        /// <summary>详情</summary>
        [Display(Name = "详情")]
        [Column("content")]
        public virtual String content
        {
            get
            {
                return _content;
            }
            set
            {
                if ((_content != value))
                {
                    SendPropertyChanging("content",_content,value);
                    _content = value;
                    SendPropertyChanged("content");
                }
            }
        }
        String _images;
        /// <summary>图片</summary>
        [Display(Name = "图片")]
        [Column("images")]
        public virtual String images
        {
            get
            {
                return _images;
            }
            set
            {
                if ((_images != value))
                {
                    SendPropertyChanging("images",_images,value);
                    _images = value;
                    SendPropertyChanged("images");
                }
            }
        }
        System.Nullable<Int32> _about_money;
        /// <summary>涉案金额</summary>
        [Display(Name = "涉案金额")]
        [Column("about_money")]
        public virtual System.Nullable<Int32> about_money
        {
            get
            {
                return _about_money;
            }
            set
            {
                if ((_about_money != value))
                {
                    SendPropertyChanging("about_money",_about_money,value);
                    _about_money = value;
                    SendPropertyChanged("about_money");
                }
            }
        }
        System.Nullable<double> _price;
        /// <summary>价格</summary>
        [Display(Name = "价格")]
        [Column("price")]
        public virtual System.Nullable<double> price
        {
            get
            {
                return _price;
            }
            set
            {
                if ((_price != value))
                {
                    SendPropertyChanging("price",_price,value);
                    _price = value;
                    SendPropertyChanged("price");
                }
            }
        }
        System.Nullable<Boolean> _is_send_msg;
        /// <summary>发送短信</summary>
        [Display(Name = "发送短信")]
        [Column("is_send_msg")]
        public virtual System.Nullable<Boolean> is_send_msg
        {
            get
            {
                return _is_send_msg;
            }
            set
            {
                if ((_is_send_msg != value))
                {
                    SendPropertyChanging("is_send_msg",_is_send_msg,value);
                    _is_send_msg = value;
                    SendPropertyChanged("is_send_msg");
                }
            }
        }
        System.Nullable<Int32> _num;
        /// <summary>数量</summary>
        [Display(Name = "数量")]
        [Column("num")]
        public virtual System.Nullable<Int32> num
        {
            get
            {
                return _num;
            }
            set
            {
                if ((_num != value))
                {
                    SendPropertyChanging("num",_num,value);
                    _num = value;
                    SendPropertyChanged("num");
                }
            }
        }
        System.Nullable<Int32> _audit;
        /// <summary>审核</summary>
        [Display(Name = "审核")]
        [Column("audit")]
        public virtual System.Nullable<Int32> audit
        {
            get
            {
                return _audit;
            }
            set
            {
                if ((_audit != value))
                {
                    SendPropertyChanging("audit",_audit,value);
                    _audit = value;
                    SendPropertyChanged("audit");
                }
            }
        }
        System.Nullable<Boolean> _is_del;
        /// <summary>删除</summary>
        [Display(Name = "删除")]
        [Column("is_del")]
        public virtual System.Nullable<Boolean> is_del
        {
            get
            {
                return _is_del;
            }
            set
            {
                if ((_is_del != value))
                {
                    SendPropertyChanging("is_del",_is_del,value);
                    _is_del = value;
                    SendPropertyChanged("is_del");
                }
            }
        }
        System.Nullable<Int32> _createtime;
        /// <summary>创建时间</summary>
        [Display(Name = "创建时间")]
        [Column("createtime")]
        public virtual System.Nullable<Int32> createtime
        {
            get
            {
                return _createtime;
            }
            set
            {
                if ((_createtime != value))
                {
                    SendPropertyChanging("createtime",_createtime,value);
                    _createtime = value;
                    SendPropertyChanged("createtime");
                }
            }
        }
        System.Nullable<Int32> _updatetime;
        /// <summary>更新时间</summary>
        [Display(Name = "更新时间")]
        [Column("updatetime")]
        public virtual System.Nullable<Int32> updatetime
        {
            get
            {
                return _updatetime;
            }
            set
            {
                if ((_updatetime != value))
                {
                    SendPropertyChanging("updatetime",_updatetime,value);
                    _updatetime = value;
                    SendPropertyChanged("updatetime");
                }
            }
        }
        String _status;
        /// <summary>状态</summary>
        [MaxLength(255)]
        [Display(Name = "状态")]
        [Column("status")]
        public virtual String status
        {
            get
            {
                return _status;
            }
            set
            {
                if ((_status != value))
                {
                    SendPropertyChanging("status",_status,value);
                    _status = value;
                    SendPropertyChanged("status");
                }
            }
        }
        System.Nullable<Int32> _user_id;
        /// <summary>发布人id</summary>
        [Display(Name = "发布人id")]
        [Column("user_id")]
        public virtual System.Nullable<Int32> user_id
        {
            get
            {
                return _user_id;
            }
            set
            {
                if ((_user_id != value))
                {
                    SendPropertyChanging("user_id",_user_id,value);
                    _user_id = value;
                    SendPropertyChanged("user_id");
                }
            }
        }
        fa_case_source_case_statusEnum? _case_status=(fa_case_source_case_statusEnum?)(1);
        /// <summary>案件状态</summary>
        [Display(Name = "案件状态")]
        [Column("case_status")]
        public virtual fa_case_source_case_statusEnum? case_status
        {
            get
            {
                return _case_status;
            }
            set
            {
                if ((_case_status != value))
                {
                    SendPropertyChanging("case_status",_case_status,value);
                    _case_status = value;
                    SendPropertyChanged("case_status");
                }
            }
        }
        System.Nullable<DateTime> _exam_time;
        /// <summary>审核时间</summary>
        [Display(Name = "审核时间")]
        [Column("exam_time")]
        public virtual System.Nullable<DateTime> exam_time
        {
            get
            {
                return _exam_time;
            }
            set
            {
                if ((_exam_time != value))
                {
                    SendPropertyChanging("exam_time",_exam_time,value);
                    _exam_time = value;
                    SendPropertyChanged("exam_time");
                }
            }
        }
        fa_case_source_exam_typeEnum? _exam_type=(fa_case_source_exam_typeEnum?)(1);
        /// <summary>审核类型</summary>
        [Display(Name = "审核类型")]
        [Column("exam_type")]
        public virtual fa_case_source_exam_typeEnum? exam_type
        {
            get
            {
                return _exam_type;
            }
            set
            {
                if ((_exam_type != value))
                {
                    SendPropertyChanging("exam_type",_exam_type,value);
                    _exam_type = value;
                    SendPropertyChanged("exam_type");
                }
            }
        }
        String _reject_reason;
        /// <summary>拒绝原因</summary>
        [MaxLength(255)]
        [Display(Name = "拒绝原因")]
        [Column("reject_reason")]
        public virtual String reject_reason
        {
            get
            {
                return _reject_reason;
            }
            set
            {
                if ((_reject_reason != value))
                {
                    SendPropertyChanging("reject_reason",_reject_reason,value);
                    _reject_reason = value;
                    SendPropertyChanged("reject_reason");
                }
            }
        }
        System.Nullable<Int32> _lawyer_id;
        /// <summary>律师id</summary>
        [Display(Name = "律师id")]
        [Column("lawyer_id")]
        public virtual System.Nullable<Int32> lawyer_id
        {
            get
            {
                return _lawyer_id;
            }
            set
            {
                if ((_lawyer_id != value))
                {
                    SendPropertyChanging("lawyer_id",_lawyer_id,value);
                    _lawyer_id = value;
                    SendPropertyChanged("lawyer_id");
                }
            }
        }
        fa_case_source_operationEnum? _operation;
        /// <summary>律师指派操作</summary>
        [Display(Name = "律师指派操作")]
        [Column("operation")]
        public virtual fa_case_source_operationEnum? operation
        {
            get
            {
                return _operation;
            }
            set
            {
                if ((_operation != value))
                {
                    SendPropertyChanging("operation",_operation,value);
                    _operation = value;
                    SendPropertyChanged("operation");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<fa_case_source, bool>> exp)
        {
            base.SetValue<fa_case_source>(exp);
        }
    }
    public enum fa_case_source_case_statusEnum:int
    {
        待审核=1,
        审核通过=20,
        待抢案=30,
        已绑定律师=40,
        审批拒绝=99,
        取消=-1
    }
    public enum fa_case_source_exam_typeEnum:int
    {
        新案件=1,
        更换律师=2
    }
    public enum fa_case_source_operationEnum:int
    {
        标签匹配=1,
        手动分配=2
    }
    /// <summary>案件类别</summary>
    [TableConfig]
    [Table("fa_case_class")]
    [Way.EntityDB.DataItemJsonConverter]
    public class fa_case_class :Way.EntityDB.DataItem
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
        Int32 _pid=0;
        /// <summary>父ID</summary>
        [DisallowNull]
        [Display(Name = "父ID")]
        [Column("pid")]
        public virtual Int32 pid
        {
            get
            {
                return _pid;
            }
            set
            {
                if ((_pid != value))
                {
                    SendPropertyChanging("pid",_pid,value);
                    _pid = value;
                    SendPropertyChanged("pid");
                }
            }
        }
        String _name;
        /// <summary>文章类别名称</summary>
        [MaxLength(100)]
        [Display(Name = "文章类别名称")]
        [Column("name")]
        public virtual String name
        {
            get
            {
                return _name;
            }
            set
            {
                if ((_name != value))
                {
                    SendPropertyChanging("name",_name,value);
                    _name = value;
                    SendPropertyChanged("name");
                }
            }
        }
        String _images;
        /// <summary>类别图标</summary>
        [MaxLength(255)]
        [Display(Name = "类别图标")]
        [Column("images")]
        public virtual String images
        {
            get
            {
                return _images;
            }
            set
            {
                if ((_images != value))
                {
                    SendPropertyChanging("images",_images,value);
                    _images = value;
                    SendPropertyChanged("images");
                }
            }
        }
        String _remark;
        /// <summary>备注</summary>
        [MaxLength(255)]
        [Display(Name = "备注")]
        [Column("remark")]
        public virtual String remark
        {
            get
            {
                return _remark;
            }
            set
            {
                if ((_remark != value))
                {
                    SendPropertyChanging("remark",_remark,value);
                    _remark = value;
                    SendPropertyChanged("remark");
                }
            }
        }
        Int32 _weigh=0;
        /// <summary>权重</summary>
        [DisallowNull]
        [Display(Name = "权重")]
        [Column("weigh")]
        public virtual Int32 weigh
        {
            get
            {
                return _weigh;
            }
            set
            {
                if ((_weigh != value))
                {
                    SendPropertyChanging("weigh",_weigh,value);
                    _weigh = value;
                    SendPropertyChanged("weigh");
                }
            }
        }
        String _status;
        /// <summary>状态</summary>
        [MaxLength(30)]
        [Display(Name = "状态")]
        [Column("status")]
        public virtual String status
        {
            get
            {
                return _status;
            }
            set
            {
                if ((_status != value))
                {
                    SendPropertyChanging("status",_status,value);
                    _status = value;
                    SendPropertyChanged("status");
                }
            }
        }
        System.Nullable<Int32> _createtime;
        /// <summary>创建时间</summary>
        [Display(Name = "创建时间")]
        [Column("createtime")]
        public virtual System.Nullable<Int32> createtime
        {
            get
            {
                return _createtime;
            }
            set
            {
                if ((_createtime != value))
                {
                    SendPropertyChanging("createtime",_createtime,value);
                    _createtime = value;
                    SendPropertyChanged("createtime");
                }
            }
        }
        System.Nullable<Int32> _updatetime;
        /// <summary>更新时间</summary>
        [Display(Name = "更新时间")]
        [Column("updatetime")]
        public virtual System.Nullable<Int32> updatetime
        {
            get
            {
                return _updatetime;
            }
            set
            {
                if ((_updatetime != value))
                {
                    SendPropertyChanging("updatetime",_updatetime,value);
                    _updatetime = value;
                    SendPropertyChanged("updatetime");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<fa_case_class, bool>> exp)
        {
            base.SetValue<fa_case_class>(exp);
        }
    }
    [TableConfig]
    [Table("fa_user_token")]
    [Way.EntityDB.DataItemJsonConverter]
    public class fa_user_token :Way.EntityDB.DataItem
    {
        String _token;
        /// <summary>Token</summary>
        [MaxLength(50)]
        [Key]
        [DisallowNull]
        [Display(Name = "Token")]
        [Column("token")]
        public virtual String token
        {
            get
            {
                return _token;
            }
            set
            {
                if ((_token != value))
                {
                    SendPropertyChanging("token",_token,value);
                    _token = value;
                    SendPropertyChanged("token");
                }
            }
        }
        Int32 _user_id=0;
        /// <summary>会员ID</summary>
        [DisallowNull]
        [Display(Name = "会员ID")]
        [Column("user_id")]
        public virtual Int32 user_id
        {
            get
            {
                return _user_id;
            }
            set
            {
                if ((_user_id != value))
                {
                    SendPropertyChanging("user_id",_user_id,value);
                    _user_id = value;
                    SendPropertyChanged("user_id");
                }
            }
        }
        String _user_type="user";
        /// <summary>用户类型</summary>
        [MaxLength(20)]
        [Display(Name = "用户类型")]
        [Column("user_type")]
        public virtual String user_type
        {
            get
            {
                return _user_type;
            }
            set
            {
                if ((_user_type != value))
                {
                    SendPropertyChanging("user_type",_user_type,value);
                    _user_type = value;
                    SendPropertyChanged("user_type");
                }
            }
        }
        System.Nullable<Int32> _createtime;
        /// <summary>创建时间</summary>
        [Display(Name = "创建时间")]
        [Column("createtime")]
        public virtual System.Nullable<Int32> createtime
        {
            get
            {
                return _createtime;
            }
            set
            {
                if ((_createtime != value))
                {
                    SendPropertyChanging("createtime",_createtime,value);
                    _createtime = value;
                    SendPropertyChanged("createtime");
                }
            }
        }
        System.Nullable<Int32> _expiretime;
        /// <summary>过期时间</summary>
        [Display(Name = "过期时间")]
        [Column("expiretime")]
        public virtual System.Nullable<Int32> expiretime
        {
            get
            {
                return _expiretime;
            }
            set
            {
                if ((_expiretime != value))
                {
                    SendPropertyChanging("expiretime",_expiretime,value);
                    _expiretime = value;
                    SendPropertyChanged("expiretime");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<fa_user_token, bool>> exp)
        {
            base.SetValue<fa_user_token>(exp);
        }
    }
    [TableConfig]
    [Table("fa_area")]
    [Way.EntityDB.DataItemJsonConverter]
    public class fa_area :Way.EntityDB.DataItem
    {
        System.Nullable<Int32> _id;
        /// <summary>ID</summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Display(Name = "ID")]
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
        System.Nullable<Int32> _pid;
        /// <summary>父id</summary>
        [Display(Name = "父id")]
        [Column("pid")]
        public virtual System.Nullable<Int32> pid
        {
            get
            {
                return _pid;
            }
            set
            {
                if ((_pid != value))
                {
                    SendPropertyChanging("pid",_pid,value);
                    _pid = value;
                    SendPropertyChanged("pid");
                }
            }
        }
        String _shortname;
        /// <summary>简称</summary>
        [MaxLength(100)]
        [Display(Name = "简称")]
        [Column("shortname")]
        public virtual String shortname
        {
            get
            {
                return _shortname;
            }
            set
            {
                if ((_shortname != value))
                {
                    SendPropertyChanging("shortname",_shortname,value);
                    _shortname = value;
                    SendPropertyChanged("shortname");
                }
            }
        }
        String _name;
        /// <summary>名称</summary>
        [MaxLength(100)]
        [Display(Name = "名称")]
        [Column("name")]
        public virtual String name
        {
            get
            {
                return _name;
            }
            set
            {
                if ((_name != value))
                {
                    SendPropertyChanging("name",_name,value);
                    _name = value;
                    SendPropertyChanged("name");
                }
            }
        }
        String _mergename;
        /// <summary>全称</summary>
        [MaxLength(255)]
        [Display(Name = "全称")]
        [Column("mergename")]
        public virtual String mergename
        {
            get
            {
                return _mergename;
            }
            set
            {
                if ((_mergename != value))
                {
                    SendPropertyChanging("mergename",_mergename,value);
                    _mergename = value;
                    SendPropertyChanged("mergename");
                }
            }
        }
        System.Nullable<Int32> _level;
        /// <summary>层级 0 1 2 省市区县</summary>
        [Display(Name = "层级 0 1 2 省市区县")]
        [Column("level")]
        public virtual System.Nullable<Int32> level
        {
            get
            {
                return _level;
            }
            set
            {
                if ((_level != value))
                {
                    SendPropertyChanging("level",_level,value);
                    _level = value;
                    SendPropertyChanged("level");
                }
            }
        }
        String _pinyin;
        /// <summary>拼音</summary>
        [MaxLength(100)]
        [Display(Name = "拼音")]
        [Column("pinyin")]
        public virtual String pinyin
        {
            get
            {
                return _pinyin;
            }
            set
            {
                if ((_pinyin != value))
                {
                    SendPropertyChanging("pinyin",_pinyin,value);
                    _pinyin = value;
                    SendPropertyChanged("pinyin");
                }
            }
        }
        String _code;
        /// <summary>长途区号</summary>
        [MaxLength(100)]
        [Display(Name = "长途区号")]
        [Column("code")]
        public virtual String code
        {
            get
            {
                return _code;
            }
            set
            {
                if ((_code != value))
                {
                    SendPropertyChanging("code",_code,value);
                    _code = value;
                    SendPropertyChanged("code");
                }
            }
        }
        String _zip;
        /// <summary>邮编</summary>
        [MaxLength(100)]
        [Display(Name = "邮编")]
        [Column("zip")]
        public virtual String zip
        {
            get
            {
                return _zip;
            }
            set
            {
                if ((_zip != value))
                {
                    SendPropertyChanging("zip",_zip,value);
                    _zip = value;
                    SendPropertyChanged("zip");
                }
            }
        }
        String _first;
        /// <summary>首字母</summary>
        [MaxLength(50)]
        [Display(Name = "首字母")]
        [Column("first")]
        public virtual String first
        {
            get
            {
                return _first;
            }
            set
            {
                if ((_first != value))
                {
                    SendPropertyChanging("first",_first,value);
                    _first = value;
                    SendPropertyChanged("first");
                }
            }
        }
        String _lng;
        /// <summary>经度</summary>
        [MaxLength(100)]
        [Display(Name = "经度")]
        [Column("lng")]
        public virtual String lng
        {
            get
            {
                return _lng;
            }
            set
            {
                if ((_lng != value))
                {
                    SendPropertyChanging("lng",_lng,value);
                    _lng = value;
                    SendPropertyChanged("lng");
                }
            }
        }
        String _lat;
        /// <summary>纬度</summary>
        [MaxLength(100)]
        [Display(Name = "纬度")]
        [Column("lat")]
        public virtual String lat
        {
            get
            {
                return _lat;
            }
            set
            {
                if ((_lat != value))
                {
                    SendPropertyChanging("lat",_lat,value);
                    _lat = value;
                    SendPropertyChanged("lat");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<fa_area, bool>> exp)
        {
            base.SetValue<fa_area>(exp);
        }
    }
    /// <summary>律师可以抢哪些案件</summary>
    [TableConfig]
    [Table("fa_case_lawyer_power")]
    [Way.EntityDB.DataItemJsonConverter]
    public class fa_case_lawyer_power :Way.EntityDB.DataItem
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
                    SendPropertyChanging("id",_id,value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        System.Nullable<Int32> _lawyer_id;
        /// <summary>律师id</summary>
        [Display(Name = "律师id")]
        [Column("lawyer_id")]
        public virtual System.Nullable<Int32> lawyer_id
        {
            get
            {
                return _lawyer_id;
            }
            set
            {
                if ((_lawyer_id != value))
                {
                    SendPropertyChanging("lawyer_id",_lawyer_id,value);
                    _lawyer_id = value;
                    SendPropertyChanged("lawyer_id");
                }
            }
        }
        System.Nullable<Int32> _case_id;
        /// <summary>案件id</summary>
        [Display(Name = "案件id")]
        [Column("case_id")]
        public virtual System.Nullable<Int32> case_id
        {
            get
            {
                return _case_id;
            }
            set
            {
                if ((_case_id != value))
                {
                    SendPropertyChanging("case_id",_case_id,value);
                    _case_id = value;
                    SendPropertyChanged("case_id");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<fa_case_lawyer_power, bool>> exp)
        {
            base.SetValue<fa_case_lawyer_power>(exp);
        }
    }
    [TableConfig]
    [Table("fa_user")]
    [Way.EntityDB.DataItemJsonConverter]
    public class fa_user :Way.EntityDB.DataItem
    {
        System.Nullable<Int32> _id;
        /// <summary>ID</summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Display(Name = "ID")]
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
        System.Nullable<Int32> _agent_id;
        /// <summary>代理商id</summary>
        [Display(Name = "代理商id")]
        [Column("agent_id")]
        public virtual System.Nullable<Int32> agent_id
        {
            get
            {
                return _agent_id;
            }
            set
            {
                if ((_agent_id != value))
                {
                    SendPropertyChanging("agent_id",_agent_id,value);
                    _agent_id = value;
                    SendPropertyChanged("agent_id");
                }
            }
        }
        Int32 _group_id=0;
        /// <summary>组别ID</summary>
        [DisallowNull]
        [Display(Name = "组别ID")]
        [Column("group_id")]
        public virtual Int32 group_id
        {
            get
            {
                return _group_id;
            }
            set
            {
                if ((_group_id != value))
                {
                    SendPropertyChanging("group_id",_group_id,value);
                    _group_id = value;
                    SendPropertyChanged("group_id");
                }
            }
        }
        String _username;
        /// <summary>用户名</summary>
        [MaxLength(32)]
        [Display(Name = "用户名")]
        [Column("username")]
        public virtual String username
        {
            get
            {
                return _username;
            }
            set
            {
                if ((_username != value))
                {
                    SendPropertyChanging("username",_username,value);
                    _username = value;
                    SendPropertyChanged("username");
                }
            }
        }
        String _nickname;
        /// <summary>昵称</summary>
        [MaxLength(255)]
        [Display(Name = "昵称")]
        [Column("nickname")]
        public virtual String nickname
        {
            get
            {
                return _nickname;
            }
            set
            {
                if ((_nickname != value))
                {
                    SendPropertyChanging("nickname",_nickname,value);
                    _nickname = value;
                    SendPropertyChanged("nickname");
                }
            }
        }
        String _password;
        /// <summary>密码</summary>
        [MaxLength(32)]
        [Display(Name = "密码")]
        [Column("password")]
        public virtual String password
        {
            get
            {
                return _password;
            }
            set
            {
                if ((_password != value))
                {
                    SendPropertyChanging("password",_password,value);
                    _password = value;
                    SendPropertyChanged("password");
                }
            }
        }
        String _salt;
        /// <summary>密码盐</summary>
        [MaxLength(30)]
        [Display(Name = "密码盐")]
        [Column("salt")]
        public virtual String salt
        {
            get
            {
                return _salt;
            }
            set
            {
                if ((_salt != value))
                {
                    SendPropertyChanging("salt",_salt,value);
                    _salt = value;
                    SendPropertyChanged("salt");
                }
            }
        }
        String _user_type="user";
        [MaxLength(255)]
        [DisallowNull]
        [Column("user_type")]
        public virtual String user_type
        {
            get
            {
                return _user_type;
            }
            set
            {
                if ((_user_type != value))
                {
                    SendPropertyChanging("user_type",_user_type,value);
                    _user_type = value;
                    SendPropertyChanged("user_type");
                }
            }
        }
        String _email;
        /// <summary>电子邮箱</summary>
        [MaxLength(100)]
        [Display(Name = "电子邮箱")]
        [Column("email")]
        public virtual String email
        {
            get
            {
                return _email;
            }
            set
            {
                if ((_email != value))
                {
                    SendPropertyChanging("email",_email,value);
                    _email = value;
                    SendPropertyChanged("email");
                }
            }
        }
        String _mobile;
        /// <summary>手机号</summary>
        [MaxLength(11)]
        [Display(Name = "手机号")]
        [Column("mobile")]
        public virtual String mobile
        {
            get
            {
                return _mobile;
            }
            set
            {
                if ((_mobile != value))
                {
                    SendPropertyChanging("mobile",_mobile,value);
                    _mobile = value;
                    SendPropertyChanged("mobile");
                }
            }
        }
        String _avatar;
        /// <summary>头像</summary>
        [MaxLength(255)]
        [Display(Name = "头像")]
        [Column("avatar")]
        public virtual String avatar
        {
            get
            {
                return _avatar;
            }
            set
            {
                if ((_avatar != value))
                {
                    SendPropertyChanging("avatar",_avatar,value);
                    _avatar = value;
                    SendPropertyChanged("avatar");
                }
            }
        }
        Int32 _level=0;
        /// <summary>等级</summary>
        [DisallowNull]
        [Display(Name = "等级")]
        [Column("level")]
        public virtual Int32 level
        {
            get
            {
                return _level;
            }
            set
            {
                if ((_level != value))
                {
                    SendPropertyChanging("level",_level,value);
                    _level = value;
                    SendPropertyChanged("level");
                }
            }
        }
        System.Nullable<Int32> _gender=0;
        /// <summary>性别</summary>
        [Display(Name = "性别")]
        [Column("gender")]
        public virtual System.Nullable<Int32> gender
        {
            get
            {
                return _gender;
            }
            set
            {
                if ((_gender != value))
                {
                    SendPropertyChanging("gender",_gender,value);
                    _gender = value;
                    SendPropertyChanged("gender");
                }
            }
        }
        System.Nullable<DateTime> _birthday;
        /// <summary>生日</summary>
        [Display(Name = "生日")]
        [Column("birthday")]
        public virtual System.Nullable<DateTime> birthday
        {
            get
            {
                return _birthday;
            }
            set
            {
                if ((_birthday != value))
                {
                    SendPropertyChanging("birthday",_birthday,value);
                    _birthday = value;
                    SendPropertyChanged("birthday");
                }
            }
        }
        String _bio;
        /// <summary>格言</summary>
        [MaxLength(100)]
        [Display(Name = "格言")]
        [Column("bio")]
        public virtual String bio
        {
            get
            {
                return _bio;
            }
            set
            {
                if ((_bio != value))
                {
                    SendPropertyChanging("bio",_bio,value);
                    _bio = value;
                    SendPropertyChanged("bio");
                }
            }
        }
        Decimal _money=0.00m;
        /// <summary>余额</summary>
        [DisallowNull]
        [Display(Name = "余额")]
        [Column("money")]
        public virtual Decimal money
        {
            get
            {
                return _money;
            }
            set
            {
                if ((_money != value))
                {
                    SendPropertyChanging("money",_money,value);
                    _money = value;
                    SendPropertyChanged("money");
                }
            }
        }
        Int32 _score=0;
        /// <summary>积分</summary>
        [DisallowNull]
        [Display(Name = "积分")]
        [Column("score")]
        public virtual Int32 score
        {
            get
            {
                return _score;
            }
            set
            {
                if ((_score != value))
                {
                    SendPropertyChanging("score",_score,value);
                    _score = value;
                    SendPropertyChanged("score");
                }
            }
        }
        System.Nullable<Int64> _viptime;
        /// <summary>vip有效日期</summary>
        [Display(Name = "vip有效日期")]
        [Column("viptime")]
        public virtual System.Nullable<Int64> viptime
        {
            get
            {
                return _viptime;
            }
            set
            {
                if ((_viptime != value))
                {
                    SendPropertyChanging("viptime",_viptime,value);
                    _viptime = value;
                    SendPropertyChanged("viptime");
                }
            }
        }
        Int32 _successions=1;
        /// <summary>连续登录天数</summary>
        [DisallowNull]
        [Display(Name = "连续登录天数")]
        [Column("successions")]
        public virtual Int32 successions
        {
            get
            {
                return _successions;
            }
            set
            {
                if ((_successions != value))
                {
                    SendPropertyChanging("successions",_successions,value);
                    _successions = value;
                    SendPropertyChanged("successions");
                }
            }
        }
        Int32 _maxsuccessions=1;
        /// <summary>最大连续登录天数</summary>
        [DisallowNull]
        [Display(Name = "最大连续登录天数")]
        [Column("maxsuccessions")]
        public virtual Int32 maxsuccessions
        {
            get
            {
                return _maxsuccessions;
            }
            set
            {
                if ((_maxsuccessions != value))
                {
                    SendPropertyChanging("maxsuccessions",_maxsuccessions,value);
                    _maxsuccessions = value;
                    SendPropertyChanged("maxsuccessions");
                }
            }
        }
        System.Nullable<Int32> _prevtime;
        /// <summary>上次登录时间</summary>
        [Display(Name = "上次登录时间")]
        [Column("prevtime")]
        public virtual System.Nullable<Int32> prevtime
        {
            get
            {
                return _prevtime;
            }
            set
            {
                if ((_prevtime != value))
                {
                    SendPropertyChanging("prevtime",_prevtime,value);
                    _prevtime = value;
                    SendPropertyChanged("prevtime");
                }
            }
        }
        System.Nullable<Int32> _logintime;
        /// <summary>登录时间</summary>
        [Display(Name = "登录时间")]
        [Column("logintime")]
        public virtual System.Nullable<Int32> logintime
        {
            get
            {
                return _logintime;
            }
            set
            {
                if ((_logintime != value))
                {
                    SendPropertyChanging("logintime",_logintime,value);
                    _logintime = value;
                    SendPropertyChanged("logintime");
                }
            }
        }
        String _loginip;
        /// <summary>登录IP</summary>
        [MaxLength(50)]
        [Display(Name = "登录IP")]
        [Column("loginip")]
        public virtual String loginip
        {
            get
            {
                return _loginip;
            }
            set
            {
                if ((_loginip != value))
                {
                    SendPropertyChanging("loginip",_loginip,value);
                    _loginip = value;
                    SendPropertyChanged("loginip");
                }
            }
        }
        Int32 _loginfailure=0;
        /// <summary>失败次数</summary>
        [DisallowNull]
        [Display(Name = "失败次数")]
        [Column("loginfailure")]
        public virtual Int32 loginfailure
        {
            get
            {
                return _loginfailure;
            }
            set
            {
                if ((_loginfailure != value))
                {
                    SendPropertyChanging("loginfailure",_loginfailure,value);
                    _loginfailure = value;
                    SendPropertyChanged("loginfailure");
                }
            }
        }
        String _joinip;
        /// <summary>加入IP</summary>
        [MaxLength(50)]
        [Display(Name = "加入IP")]
        [Column("joinip")]
        public virtual String joinip
        {
            get
            {
                return _joinip;
            }
            set
            {
                if ((_joinip != value))
                {
                    SendPropertyChanging("joinip",_joinip,value);
                    _joinip = value;
                    SendPropertyChanged("joinip");
                }
            }
        }
        System.Nullable<Int32> _jointime;
        /// <summary>加入时间</summary>
        [Display(Name = "加入时间")]
        [Column("jointime")]
        public virtual System.Nullable<Int32> jointime
        {
            get
            {
                return _jointime;
            }
            set
            {
                if ((_jointime != value))
                {
                    SendPropertyChanging("jointime",_jointime,value);
                    _jointime = value;
                    SendPropertyChanged("jointime");
                }
            }
        }
        System.Nullable<Int32> _createtime;
        /// <summary>创建时间</summary>
        [Display(Name = "创建时间")]
        [Column("createtime")]
        public virtual System.Nullable<Int32> createtime
        {
            get
            {
                return _createtime;
            }
            set
            {
                if ((_createtime != value))
                {
                    SendPropertyChanging("createtime",_createtime,value);
                    _createtime = value;
                    SendPropertyChanged("createtime");
                }
            }
        }
        System.Nullable<Int32> _updatetime;
        /// <summary>更新时间</summary>
        [Display(Name = "更新时间")]
        [Column("updatetime")]
        public virtual System.Nullable<Int32> updatetime
        {
            get
            {
                return _updatetime;
            }
            set
            {
                if ((_updatetime != value))
                {
                    SendPropertyChanging("updatetime",_updatetime,value);
                    _updatetime = value;
                    SendPropertyChanged("updatetime");
                }
            }
        }
        String _token;
        /// <summary>Token</summary>
        [MaxLength(50)]
        [Display(Name = "Token")]
        [Column("token")]
        public virtual String token
        {
            get
            {
                return _token;
            }
            set
            {
                if ((_token != value))
                {
                    SendPropertyChanging("token",_token,value);
                    _token = value;
                    SendPropertyChanged("token");
                }
            }
        }
        String _status;
        /// <summary>状态</summary>
        [MaxLength(30)]
        [Display(Name = "状态")]
        [Column("status")]
        public virtual String status
        {
            get
            {
                return _status;
            }
            set
            {
                if ((_status != value))
                {
                    SendPropertyChanging("status",_status,value);
                    _status = value;
                    SendPropertyChanged("status");
                }
            }
        }
        String _verification;
        /// <summary>验证</summary>
        [MaxLength(255)]
        [Display(Name = "验证")]
        [Column("verification")]
        public virtual String verification
        {
            get
            {
                return _verification;
            }
            set
            {
                if ((_verification != value))
                {
                    SendPropertyChanging("verification",_verification,value);
                    _verification = value;
                    SendPropertyChanged("verification");
                }
            }
        }
        String _region;
        /// <summary>地区</summary>
        [MaxLength(255)]
        [Display(Name = "地区")]
        [Column("region")]
        public virtual String region
        {
            get
            {
                return _region;
            }
            set
            {
                if ((_region != value))
                {
                    SendPropertyChanging("region",_region,value);
                    _region = value;
                    SendPropertyChanged("region");
                }
            }
        }
        String _region_num;
        /// <summary>地区编号</summary>
        [MaxLength(255)]
        [Display(Name = "地区编号")]
        [Column("region_num")]
        public virtual String region_num
        {
            get
            {
                return _region_num;
            }
            set
            {
                if ((_region_num != value))
                {
                    SendPropertyChanging("region_num",_region_num,value);
                    _region_num = value;
                    SendPropertyChanged("region_num");
                }
            }
        }
        System.Nullable<Int32> _age;
        /// <summary>年龄</summary>
        [Display(Name = "年龄")]
        [Column("age")]
        public virtual System.Nullable<Int32> age
        {
            get
            {
                return _age;
            }
            set
            {
                if ((_age != value))
                {
                    SendPropertyChanging("age",_age,value);
                    _age = value;
                    SendPropertyChanged("age");
                }
            }
        }
        String _openid;
        /// <summary>微信ID</summary>
        [MaxLength(255)]
        [Display(Name = "微信ID")]
        [Column("openid")]
        public virtual String openid
        {
            get
            {
                return _openid;
            }
            set
            {
                if ((_openid != value))
                {
                    SendPropertyChanging("openid",_openid,value);
                    _openid = value;
                    SendPropertyChanged("openid");
                }
            }
        }
        String _unionid;
        [MaxLength(255)]
        [Column("unionid")]
        public virtual String unionid
        {
            get
            {
                return _unionid;
            }
            set
            {
                if ((_unionid != value))
                {
                    SendPropertyChanging("unionid",_unionid,value);
                    _unionid = value;
                    SendPropertyChanged("unionid");
                }
            }
        }
        String _mini_openid;
        [MaxLength(255)]
        [Column("mini_openid")]
        public virtual String mini_openid
        {
            get
            {
                return _mini_openid;
            }
            set
            {
                if ((_mini_openid != value))
                {
                    SendPropertyChanging("mini_openid",_mini_openid,value);
                    _mini_openid = value;
                    SendPropertyChanged("mini_openid");
                }
            }
        }
        System.Nullable<Int32> _bsviptime;
        /// <summary>企业服务到期时间</summary>
        [Display(Name = "企业服务到期时间")]
        [Column("bsviptime")]
        public virtual System.Nullable<Int32> bsviptime
        {
            get
            {
                return _bsviptime;
            }
            set
            {
                if ((_bsviptime != value))
                {
                    SendPropertyChanging("bsviptime",_bsviptime,value);
                    _bsviptime = value;
                    SendPropertyChanged("bsviptime");
                }
            }
        }
        String _invite_code;
        /// <summary>邀请码</summary>
        [MaxLength(255)]
        [Display(Name = "邀请码")]
        [Column("invite_code")]
        public virtual String invite_code
        {
            get
            {
                return _invite_code;
            }
            set
            {
                if ((_invite_code != value))
                {
                    SendPropertyChanging("invite_code",_invite_code,value);
                    _invite_code = value;
                    SendPropertyChanged("invite_code");
                }
            }
        }
        String _pay_pwd;
        /// <summary>支付密码</summary>
        [MaxLength(50)]
        [Display(Name = "支付密码")]
        [Column("pay_pwd")]
        public virtual String pay_pwd
        {
            get
            {
                return _pay_pwd;
            }
            set
            {
                if ((_pay_pwd != value))
                {
                    SendPropertyChanging("pay_pwd",_pay_pwd,value);
                    _pay_pwd = value;
                    SendPropertyChanged("pay_pwd");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<fa_user, bool>> exp)
        {
            base.SetValue<fa_user>(exp);
        }
    }
    /// <summary>案件返佣/提现卷</summary>
    [TableConfig]
    [Table("fa_commission")]
    [Way.EntityDB.DataItemJsonConverter]
    public class fa_commission :Way.EntityDB.DataItem
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
                    SendPropertyChanging("id",_id,value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        System.Nullable<Int32> _user_id;
        /// <summary>佣金给谁</summary>
        [Display(Name = "佣金给谁")]
        [Column("user_id")]
        public virtual System.Nullable<Int32> user_id
        {
            get
            {
                return _user_id;
            }
            set
            {
                if ((_user_id != value))
                {
                    SendPropertyChanging("user_id",_user_id,value);
                    _user_id = value;
                    SendPropertyChanged("user_id");
                }
            }
        }
        System.Nullable<Int32> _case_id;
        /// <summary>来源案件id</summary>
        [Display(Name = "来源案件id")]
        [Column("case_id")]
        public virtual System.Nullable<Int32> case_id
        {
            get
            {
                return _case_id;
            }
            set
            {
                if ((_case_id != value))
                {
                    SendPropertyChanging("case_id",_case_id,value);
                    _case_id = value;
                    SendPropertyChanged("case_id");
                }
            }
        }
        System.Nullable<Int32> _pay_user_id;
        /// <summary>付款人id</summary>
        [Display(Name = "付款人id")]
        [Column("pay_user_id")]
        public virtual System.Nullable<Int32> pay_user_id
        {
            get
            {
                return _pay_user_id;
            }
            set
            {
                if ((_pay_user_id != value))
                {
                    SendPropertyChanging("pay_user_id",_pay_user_id,value);
                    _pay_user_id = value;
                    SendPropertyChanged("pay_user_id");
                }
            }
        }
        System.Nullable<Decimal> _case_amount;
        /// <summary>案件付款金额</summary>
        [Display(Name = "案件付款金额")]
        [Column("case_amount")]
        public virtual System.Nullable<Decimal> case_amount
        {
            get
            {
                return _case_amount;
            }
            set
            {
                if ((_case_amount != value))
                {
                    SendPropertyChanging("case_amount",_case_amount,value);
                    _case_amount = value;
                    SendPropertyChanged("case_amount");
                }
            }
        }
        System.Nullable<Int32> _pay_id;
        /// <summary>付款订单id</summary>
        [Display(Name = "付款订单id")]
        [Column("pay_id")]
        public virtual System.Nullable<Int32> pay_id
        {
            get
            {
                return _pay_id;
            }
            set
            {
                if ((_pay_id != value))
                {
                    SendPropertyChanging("pay_id",_pay_id,value);
                    _pay_id = value;
                    SendPropertyChanged("pay_id");
                }
            }
        }
        DateTime _create_time;
        /// <summary>生成时间</summary>
        [DisallowNull]
        [Display(Name = "生成时间")]
        [Column("create_time")]
        public virtual DateTime create_time
        {
            get
            {
                return _create_time;
            }
            set
            {
                if ((_create_time != value))
                {
                    SendPropertyChanging("create_time",_create_time,value);
                    _create_time = value;
                    SendPropertyChanged("create_time");
                }
            }
        }
        DateTime _valid_time;
        /// <summary>预计可提现时间</summary>
        [DisallowNull]
        [Display(Name = "预计可提现时间")]
        [Column("valid_time")]
        public virtual DateTime valid_time
        {
            get
            {
                return _valid_time;
            }
            set
            {
                if ((_valid_time != value))
                {
                    SendPropertyChanging("valid_time",_valid_time,value);
                    _valid_time = value;
                    SendPropertyChanged("valid_time");
                }
            }
        }
        fa_commission_statusEnum? _status=(fa_commission_statusEnum?)(1);
        /// <summary>状态</summary>
        [Display(Name = "状态")]
        [Column("status")]
        public virtual fa_commission_statusEnum? status
        {
            get
            {
                return _status;
            }
            set
            {
                if ((_status != value))
                {
                    SendPropertyChanging("status",_status,value);
                    _status = value;
                    SendPropertyChanged("status");
                }
            }
        }
        Decimal _amount;
        /// <summary>佣金金额</summary>
        [DisallowNull]
        [Display(Name = "佣金金额")]
        [Column("amount")]
        public virtual Decimal amount
        {
            get
            {
                return _amount;
            }
            set
            {
                if ((_amount != value))
                {
                    SendPropertyChanging("amount",_amount,value);
                    _amount = value;
                    SendPropertyChanged("amount");
                }
            }
        }
        System.Nullable<DateTime> _cancel_time;
        /// <summary>撤销时间</summary>
        [Display(Name = "撤销时间")]
        [Column("cancel_time")]
        public virtual System.Nullable<DateTime> cancel_time
        {
            get
            {
                return _cancel_time;
            }
            set
            {
                if ((_cancel_time != value))
                {
                    SendPropertyChanging("cancel_time",_cancel_time,value);
                    _cancel_time = value;
                    SendPropertyChanged("cancel_time");
                }
            }
        }
        String _user_type;
        /// <summary>user_id是什么类型</summary>
        [MaxLength(50)]
        [Display(Name = "user_id是什么类型")]
        [Column("user_type")]
        public virtual String user_type
        {
            get
            {
                return _user_type;
            }
            set
            {
                if ((_user_type != value))
                {
                    SendPropertyChanging("user_type",_user_type,value);
                    _user_type = value;
                    SendPropertyChanged("user_type");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<fa_commission, bool>> exp)
        {
            base.SetValue<fa_commission>(exp);
        }
    }
    public enum fa_commission_statusEnum:int
    {
        生成 = 1,
        已经累加到余额 = 2,
        已经撤销 = -1
    }
    [TableConfig]
    [Table("fa_user_money_log")]
    [Way.EntityDB.DataItemJsonConverter]
    public class fa_user_money_log :Way.EntityDB.DataItem
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
        Int32 _user_id=0;
        /// <summary>会员ID</summary>
        [DisallowNull]
        [Display(Name = "会员ID")]
        [Column("user_id")]
        public virtual Int32 user_id
        {
            get
            {
                return _user_id;
            }
            set
            {
                if ((_user_id != value))
                {
                    SendPropertyChanging("user_id",_user_id,value);
                    _user_id = value;
                    SendPropertyChanged("user_id");
                }
            }
        }
        String _user_type;
        [MaxLength(255)]
        [DisallowNull]
        [Column("user_type")]
        public virtual String user_type
        {
            get
            {
                return _user_type;
            }
            set
            {
                if ((_user_type != value))
                {
                    SendPropertyChanging("user_type",_user_type,value);
                    _user_type = value;
                    SendPropertyChanged("user_type");
                }
            }
        }
        System.Nullable<Decimal> _money=0.00m;
        /// <summary>变更余额</summary>
        [Display(Name = "变更余额")]
        [Column("money")]
        public virtual System.Nullable<Decimal> money
        {
            get
            {
                return _money;
            }
            set
            {
                if ((_money != value))
                {
                    SendPropertyChanging("money",_money,value);
                    _money = value;
                    SendPropertyChanged("money");
                }
            }
        }
        System.Nullable<Decimal> _before=0.00m;
        /// <summary>变更前余额</summary>
        [Display(Name = "变更前余额")]
        [Column("before")]
        public virtual System.Nullable<Decimal> before
        {
            get
            {
                return _before;
            }
            set
            {
                if ((_before != value))
                {
                    SendPropertyChanging("before",_before,value);
                    _before = value;
                    SendPropertyChanged("before");
                }
            }
        }
        System.Nullable<Decimal> _after=0.00m;
        /// <summary>变更后余额</summary>
        [Display(Name = "变更后余额")]
        [Column("after")]
        public virtual System.Nullable<Decimal> after
        {
            get
            {
                return _after;
            }
            set
            {
                if ((_after != value))
                {
                    SendPropertyChanging("after",_after,value);
                    _after = value;
                    SendPropertyChanged("after");
                }
            }
        }
        String _memo;
        /// <summary>备注</summary>
        [MaxLength(255)]
        [Display(Name = "备注")]
        [Column("memo")]
        public virtual String memo
        {
            get
            {
                return _memo;
            }
            set
            {
                if ((_memo != value))
                {
                    SendPropertyChanging("memo",_memo,value);
                    _memo = value;
                    SendPropertyChanged("memo");
                }
            }
        }
        System.Nullable<Int32> _createtime;
        /// <summary>创建时间</summary>
        [Display(Name = "创建时间")]
        [Column("createtime")]
        public virtual System.Nullable<Int32> createtime
        {
            get
            {
                return _createtime;
            }
            set
            {
                if ((_createtime != value))
                {
                    SendPropertyChanging("createtime",_createtime,value);
                    _createtime = value;
                    SendPropertyChanged("createtime");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<fa_user_money_log, bool>> exp)
        {
            base.SetValue<fa_user_money_log>(exp);
        }
    }
    [TableConfig]
    [Table("fa_setting")]
    [Way.EntityDB.DataItemJsonConverter]
    public class fa_setting :Way.EntityDB.DataItem
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
                    SendPropertyChanging("id",_id,value);
                    _id = value;
                    SendPropertyChanged("id");
                }
            }
        }
        String _key;
        [MaxLength(50)]
        [Column("key")]
        public virtual String key
        {
            get
            {
                return _key;
            }
            set
            {
                if ((_key != value))
                {
                    SendPropertyChanging("key",_key,value);
                    _key = value;
                    SendPropertyChanged("key");
                }
            }
        }
        String _value;
        [MaxLength(300)]
        [Column("value")]
        public virtual String value
        {
            get
            {
                return _value;
            }
            set
            {
                if ((_value != value))
                {
                    SendPropertyChanging("value",_value,value);
                    _value = value;
                    SendPropertyChanged("value");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<fa_setting, bool>> exp)
        {
            base.SetValue<fa_setting>(exp);
        }
    }
    [TableConfig]
    [Table("fa_order")]
    [Way.EntityDB.DataItemJsonConverter]
    public class fa_order :Way.EntityDB.DataItem
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
        System.Nullable<Int32> _agent_id;
        /// <summary>代理商id</summary>
        [Display(Name = "代理商id")]
        [Column("agent_id")]
        public virtual System.Nullable<Int32> agent_id
        {
            get
            {
                return _agent_id;
            }
            set
            {
                if ((_agent_id != value))
                {
                    SendPropertyChanging("agent_id",_agent_id,value);
                    _agent_id = value;
                    SendPropertyChanged("agent_id");
                }
            }
        }
        String _type;
        /// <summary>订单类型:contract=合同文书,business=企业服务,uservip=用户vip,ghostwrite=代写文书,lawyervip=律师vip,buyentrust=委托抢注,userrecharge=用户充值,lawyerrecharge=律师充值</summary>
        [MaxLength(14)]
        [DisallowNull]
        [Display(Name = "订单类型:contract=合同文书,business=企业服务,uservip=用户vip,ghostwrite=代写文书,lawyervip=律师vip,buyentrust=委托抢注,userrecharge=用户充值,lawyerrecharge=律师充值")]
        [Column("type")]
        public virtual String type
        {
            get
            {
                return _type;
            }
            set
            {
                if ((_type != value))
                {
                    SendPropertyChanging("type",_type,value);
                    _type = value;
                    SendPropertyChanged("type");
                }
            }
        }
        String _order_sn;
        /// <summary>订单号</summary>
        [MaxLength(60)]
        [DisallowNull]
        [Display(Name = "订单号")]
        [Column("order_sn")]
        public virtual String order_sn
        {
            get
            {
                return _order_sn;
            }
            set
            {
                if ((_order_sn != value))
                {
                    SendPropertyChanging("order_sn",_order_sn,value);
                    _order_sn = value;
                    SendPropertyChanged("order_sn");
                }
            }
        }
        System.Nullable<Int32> _user_id=0;
        /// <summary>用户</summary>
        [Display(Name = "用户")]
        [Column("user_id")]
        public virtual System.Nullable<Int32> user_id
        {
            get
            {
                return _user_id;
            }
            set
            {
                if ((_user_id != value))
                {
                    SendPropertyChanging("user_id",_user_id,value);
                    _user_id = value;
                    SendPropertyChanged("user_id");
                }
            }
        }
        String _user_type="user";
        /// <summary>用户身份:user=普通用户,lawyer=律师</summary>
        [MaxLength(6)]
        [DisallowNull]
        [Display(Name = "用户身份:user=普通用户,lawyer=律师")]
        [Column("user_type")]
        public virtual String user_type
        {
            get
            {
                return _user_type;
            }
            set
            {
                if ((_user_type != value))
                {
                    SendPropertyChanging("user_type",_user_type,value);
                    _user_type = value;
                    SendPropertyChanged("user_type");
                }
            }
        }
        Decimal _goods_amount=0.00m;
        /// <summary>商品总价</summary>
        [DisallowNull]
        [Display(Name = "商品总价")]
        [Column("goods_amount")]
        public virtual Decimal goods_amount
        {
            get
            {
                return _goods_amount;
            }
            set
            {
                if ((_goods_amount != value))
                {
                    SendPropertyChanging("goods_amount",_goods_amount,value);
                    _goods_amount = value;
                    SendPropertyChanged("goods_amount");
                }
            }
        }
        Int32 _status=0;
        /// <summary>订单状态:-2=交易关闭,-1=已取消,0=未支付,1=已支付,2=已完成,3=待审核退款, 4= 已退款</summary>
        [DisallowNull]
        [Display(Name = "订单状态:-2=交易关闭,-1=已取消,0=未支付,1=已支付,2=已完成,3=待审核退款, 4= 已退款")]
        [Column("status")]
        public virtual Int32 status
        {
            get
            {
                return _status;
            }
            set
            {
                if ((_status != value))
                {
                    SendPropertyChanging("status",_status,value);
                    _status = value;
                    SendPropertyChanged("status");
                }
            }
        }
        String _memo;
        /// <summary>商户备注</summary>
        [MaxLength(255)]
        [Display(Name = "商户备注")]
        [Column("memo")]
        public virtual String memo
        {
            get
            {
                return _memo;
            }
            set
            {
                if ((_memo != value))
                {
                    SendPropertyChanging("memo",_memo,value);
                    _memo = value;
                    SendPropertyChanged("memo");
                }
            }
        }
        Decimal _total_amount=0.00m;
        /// <summary>订单总金额</summary>
        [DisallowNull]
        [Display(Name = "订单总金额")]
        [Column("total_amount")]
        public virtual Decimal total_amount
        {
            get
            {
                return _total_amount;
            }
            set
            {
                if ((_total_amount != value))
                {
                    SendPropertyChanging("total_amount",_total_amount,value);
                    _total_amount = value;
                    SendPropertyChanged("total_amount");
                }
            }
        }
        Decimal _discount_fee=0.00m;
        /// <summary>折扣总金额</summary>
        [DisallowNull]
        [Display(Name = "折扣总金额")]
        [Column("discount_fee")]
        public virtual Decimal discount_fee
        {
            get
            {
                return _discount_fee;
            }
            set
            {
                if ((_discount_fee != value))
                {
                    SendPropertyChanging("discount_fee",_discount_fee,value);
                    _discount_fee = value;
                    SendPropertyChanged("discount_fee");
                }
            }
        }
        Decimal _coupon_fee=0.00m;
        /// <summary>优惠券抵用金额</summary>
        [DisallowNull]
        [Display(Name = "优惠券抵用金额")]
        [Column("coupon_fee")]
        public virtual Decimal coupon_fee
        {
            get
            {
                return _coupon_fee;
            }
            set
            {
                if ((_coupon_fee != value))
                {
                    SendPropertyChanging("coupon_fee",_coupon_fee,value);
                    _coupon_fee = value;
                    SendPropertyChanged("coupon_fee");
                }
            }
        }
        Decimal _total_fee;
        /// <summary>应付金额</summary>
        [DisallowNull]
        [Display(Name = "应付金额")]
        [Column("total_fee")]
        public virtual Decimal total_fee
        {
            get
            {
                return _total_fee;
            }
            set
            {
                if ((_total_fee != value))
                {
                    SendPropertyChanging("total_fee",_total_fee,value);
                    _total_fee = value;
                    SendPropertyChanged("total_fee");
                }
            }
        }
        Decimal _pay_fee=0.00m;
        /// <summary>实际支付金额</summary>
        [DisallowNull]
        [Display(Name = "实际支付金额")]
        [Column("pay_fee")]
        public virtual Decimal pay_fee
        {
            get
            {
                return _pay_fee;
            }
            set
            {
                if ((_pay_fee != value))
                {
                    SendPropertyChanging("pay_fee",_pay_fee,value);
                    _pay_fee = value;
                    SendPropertyChanged("pay_fee");
                }
            }
        }
        String _goods_info;
        /// <summary>商品信息</summary>
        [MaxLength(255)]
        [Display(Name = "商品信息")]
        [Column("goods_info")]
        public virtual String goods_info
        {
            get
            {
                return _goods_info;
            }
            set
            {
                if ((_goods_info != value))
                {
                    SendPropertyChanging("goods_info",_goods_info,value);
                    _goods_info = value;
                    SendPropertyChanged("goods_info");
                }
            }
        }
        System.Nullable<Int32> _goods_item_id;
        /// <summary>商品附加id参数</summary>
        [Display(Name = "商品附加id参数")]
        [Column("goods_item_id")]
        public virtual System.Nullable<Int32> goods_item_id
        {
            get
            {
                return _goods_item_id;
            }
            set
            {
                if ((_goods_item_id != value))
                {
                    SendPropertyChanging("goods_item_id",_goods_item_id,value);
                    _goods_item_id = value;
                    SendPropertyChanged("goods_item_id");
                }
            }
        }
        Decimal _goods_original_amount=0.00m;
        /// <summary>商品原价</summary>
        [DisallowNull]
        [Display(Name = "商品原价")]
        [Column("goods_original_amount")]
        public virtual Decimal goods_original_amount
        {
            get
            {
                return _goods_original_amount;
            }
            set
            {
                if ((_goods_original_amount != value))
                {
                    SendPropertyChanging("goods_original_amount",_goods_original_amount,value);
                    _goods_original_amount = value;
                    SendPropertyChanged("goods_original_amount");
                }
            }
        }
        Int32 _coupons_id=0;
        /// <summary>优惠券 id</summary>
        [DisallowNull]
        [Display(Name = "优惠券 id")]
        [Column("coupons_id")]
        public virtual Int32 coupons_id
        {
            get
            {
                return _coupons_id;
            }
            set
            {
                if ((_coupons_id != value))
                {
                    SendPropertyChanging("coupons_id",_coupons_id,value);
                    _coupons_id = value;
                    SendPropertyChanged("coupons_id");
                }
            }
        }
        String _transaction_id;
        /// <summary>交易单号</summary>
        [MaxLength(60)]
        [Display(Name = "交易单号")]
        [Column("transaction_id")]
        public virtual String transaction_id
        {
            get
            {
                return _transaction_id;
            }
            set
            {
                if ((_transaction_id != value))
                {
                    SendPropertyChanging("transaction_id",_transaction_id,value);
                    _transaction_id = value;
                    SendPropertyChanged("transaction_id");
                }
            }
        }
        String _payment_json;
        /// <summary>交易原始数据</summary>
        [MaxLength(2500)]
        [Display(Name = "交易原始数据")]
        [Column("payment_json")]
        public virtual String payment_json
        {
            get
            {
                return _payment_json;
            }
            set
            {
                if ((_payment_json != value))
                {
                    SendPropertyChanging("payment_json",_payment_json,value);
                    _payment_json = value;
                    SendPropertyChanged("payment_json");
                }
            }
        }
        String _pay_type;
        /// <summary>支付方式:wechat=微信支付,alipay=支付宝,wallet=余额支付</summary>
        [MaxLength(6)]
        [Display(Name = "支付方式:wechat=微信支付,alipay=支付宝,wallet=余额支付")]
        [Column("pay_type")]
        public virtual String pay_type
        {
            get
            {
                return _pay_type;
            }
            set
            {
                if ((_pay_type != value))
                {
                    SendPropertyChanging("pay_type",_pay_type,value);
                    _pay_type = value;
                    SendPropertyChanged("pay_type");
                }
            }
        }
        System.Nullable<Int32> _paytime;
        /// <summary>支付时间</summary>
        [Display(Name = "支付时间")]
        [Column("paytime")]
        public virtual System.Nullable<Int32> paytime
        {
            get
            {
                return _paytime;
            }
            set
            {
                if ((_paytime != value))
                {
                    SendPropertyChanging("paytime",_paytime,value);
                    _paytime = value;
                    SendPropertyChanged("paytime");
                }
            }
        }
        String _ext;
        /// <summary>附加字段</summary>
        [MaxLength(255)]
        [Display(Name = "附加字段")]
        [Column("ext")]
        public virtual String ext
        {
            get
            {
                return _ext;
            }
            set
            {
                if ((_ext != value))
                {
                    SendPropertyChanging("ext",_ext,value);
                    _ext = value;
                    SendPropertyChanged("ext");
                }
            }
        }
        String _platform;
        /// <summary>平台:H5=H5,wxOfficialAccount=微信公众号,wxMiniProgram=微信小程序,App=App</summary>
        [MaxLength(17)]
        [Display(Name = "平台:H5=H5,wxOfficialAccount=微信公众号,wxMiniProgram=微信小程序,App=App")]
        [Column("platform")]
        public virtual String platform
        {
            get
            {
                return _platform;
            }
            set
            {
                if ((_platform != value))
                {
                    SendPropertyChanging("platform",_platform,value);
                    _platform = value;
                    SendPropertyChanged("platform");
                }
            }
        }
        System.Nullable<Int32> _createtime;
        /// <summary>创建时间</summary>
        [Display(Name = "创建时间")]
        [Column("createtime")]
        public virtual System.Nullable<Int32> createtime
        {
            get
            {
                return _createtime;
            }
            set
            {
                if ((_createtime != value))
                {
                    SendPropertyChanging("createtime",_createtime,value);
                    _createtime = value;
                    SendPropertyChanged("createtime");
                }
            }
        }
        System.Nullable<Int32> _updatetime;
        /// <summary>更新时间</summary>
        [Display(Name = "更新时间")]
        [Column("updatetime")]
        public virtual System.Nullable<Int32> updatetime
        {
            get
            {
                return _updatetime;
            }
            set
            {
                if ((_updatetime != value))
                {
                    SendPropertyChanging("updatetime",_updatetime,value);
                    _updatetime = value;
                    SendPropertyChanged("updatetime");
                }
            }
        }
        System.Nullable<Int32> _deletetime;
        /// <summary>删除时间</summary>
        [Display(Name = "删除时间")]
        [Column("deletetime")]
        public virtual System.Nullable<Int32> deletetime
        {
            get
            {
                return _deletetime;
            }
            set
            {
                if ((_deletetime != value))
                {
                    SendPropertyChanging("deletetime",_deletetime,value);
                    _deletetime = value;
                    SendPropertyChanged("deletetime");
                }
            }
        }
        System.Nullable<DateTime> _exam_time;
        /// <summary>退款审核时间</summary>
        [Display(Name = "退款审核时间")]
        [Column("exam_time")]
        public virtual System.Nullable<DateTime> exam_time
        {
            get
            {
                return _exam_time;
            }
            set
            {
                if ((_exam_time != value))
                {
                    SendPropertyChanging("exam_time",_exam_time,value);
                    _exam_time = value;
                    SendPropertyChanged("exam_time");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<fa_order, bool>> exp)
        {
            base.SetValue<fa_order>(exp);
        }
    }
    [TableConfig]
    [Table("fa_invite")]
    [Way.EntityDB.DataItemJsonConverter]
    public class fa_invite :Way.EntityDB.DataItem
    {
        System.Nullable<Int32> _id;
        /// <summary>ID</summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisallowNull]
        [Display(Name = "ID")]
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
        System.Nullable<Int32> _user_id=0;
        /// <summary>会员ID</summary>
        [Display(Name = "会员ID")]
        [Column("user_id")]
        public virtual System.Nullable<Int32> user_id
        {
            get
            {
                return _user_id;
            }
            set
            {
                if ((_user_id != value))
                {
                    SendPropertyChanging("user_id",_user_id,value);
                    _user_id = value;
                    SendPropertyChanged("user_id");
                }
            }
        }
        String _utype;
        /// <summary>会员ID类型</summary>
        [MaxLength(255)]
        [Display(Name = "会员ID类型")]
        [Column("utype")]
        public virtual String utype
        {
            get
            {
                return _utype;
            }
            set
            {
                if ((_utype != value))
                {
                    SendPropertyChanging("utype",_utype,value);
                    _utype = value;
                    SendPropertyChanged("utype");
                }
            }
        }
        System.Nullable<Int32> _invited_user_id=0;
        /// <summary>被邀请人</summary>
        [Display(Name = "被邀请人")]
        [Column("invited_user_id")]
        public virtual System.Nullable<Int32> invited_user_id
        {
            get
            {
                return _invited_user_id;
            }
            set
            {
                if ((_invited_user_id != value))
                {
                    SendPropertyChanging("invited_user_id",_invited_user_id,value);
                    _invited_user_id = value;
                    SendPropertyChanged("invited_user_id");
                }
            }
        }
        String _iutype;
        /// <summary>被邀请人类型</summary>
        [MaxLength(255)]
        [Display(Name = "被邀请人类型")]
        [Column("iutype")]
        public virtual String iutype
        {
            get
            {
                return _iutype;
            }
            set
            {
                if ((_iutype != value))
                {
                    SendPropertyChanging("iutype",_iutype,value);
                    _iutype = value;
                    SendPropertyChanged("iutype");
                }
            }
        }
        String _ip;
        /// <summary>注册IP</summary>
        [MaxLength(50)]
        [Display(Name = "注册IP")]
        [Column("ip")]
        public virtual String ip
        {
            get
            {
                return _ip;
            }
            set
            {
                if ((_ip != value))
                {
                    SendPropertyChanging("ip",_ip,value);
                    _ip = value;
                    SendPropertyChanged("ip");
                }
            }
        }
        System.Nullable<Int32> _createtime=0;
        /// <summary>创建时间</summary>
        [Display(Name = "创建时间")]
        [Column("createtime")]
        public virtual System.Nullable<Int32> createtime
        {
            get
            {
                return _createtime;
            }
            set
            {
                if ((_createtime != value))
                {
                    SendPropertyChanging("createtime",_createtime,value);
                    _createtime = value;
                    SendPropertyChanged("createtime");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<fa_invite, bool>> exp)
        {
            base.SetValue<fa_invite>(exp);
        }
    }
    [TableConfig]
    [Table("fa_coupons")]
    [Way.EntityDB.DataItemJsonConverter]
    public class fa_coupons :Way.EntityDB.DataItem
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
        String _name;
        /// <summary>名称</summary>
        [MaxLength(50)]
        [Display(Name = "名称")]
        [Column("name")]
        public virtual String name
        {
            get
            {
                return _name;
            }
            set
            {
                if ((_name != value))
                {
                    SendPropertyChanging("name",_name,value);
                    _name = value;
                    SendPropertyChanged("name");
                }
            }
        }
        String _type="cash";
        /// <summary>类型:cash=代金券,discount=折扣券</summary>
        [MaxLength(8)]
        [DisallowNull]
        [Display(Name = "类型:cash=代金券,discount=折扣券")]
        [Column("type")]
        public virtual String type
        {
            get
            {
                return _type;
            }
            set
            {
                if ((_type != value))
                {
                    SendPropertyChanging("type",_type,value);
                    _type = value;
                    SendPropertyChanged("type");
                }
            }
        }
        Int32 _isshow=1;
        /// <summary>优惠券类型:0=律师认证券,1=普通优惠券,2=积分兑换券</summary>
        [DisallowNull]
        [Display(Name = "优惠券类型:0=律师认证券,1=普通优惠券,2=积分兑换券")]
        [Column("isshow")]
        public virtual Int32 isshow
        {
            get
            {
                return _isshow;
            }
            set
            {
                if ((_isshow != value))
                {
                    SendPropertyChanging("isshow",_isshow,value);
                    _isshow = value;
                    SendPropertyChanged("isshow");
                }
            }
        }
        String _goods_type="all";
        /// <summary>适用商品类型:all=全部,contract=合同文书,business=企业服务,uservip=用户vip,ghostwrite=代写文书,lawyervip=律师vip,buyentrust=委托抢注</summary>
        [MaxLength(61)]
        [Display(Name = "适用商品类型:all=全部,contract=合同文书,business=企业服务,uservip=用户vip,ghostwrite=代写文书,lawyervip=律师vip,buyentrust=委托抢注")]
        [Column("goods_type")]
        public virtual String goods_type
        {
            get
            {
                return _goods_type;
            }
            set
            {
                if ((_goods_type != value))
                {
                    SendPropertyChanging("goods_type",_goods_type,value);
                    _goods_type = value;
                    SendPropertyChanged("goods_type");
                }
            }
        }
        String _goods_ids;
        /// <summary>适用商品</summary>
        [MaxLength(1200)]
        [Display(Name = "适用商品")]
        [Column("goods_ids")]
        public virtual String goods_ids
        {
            get
            {
                return _goods_ids;
            }
            set
            {
                if ((_goods_ids != value))
                {
                    SendPropertyChanging("goods_ids",_goods_ids,value);
                    _goods_ids = value;
                    SendPropertyChanged("goods_ids");
                }
            }
        }
        Decimal _amount=0.00m;
        /// <summary>券面额</summary>
        [DisallowNull]
        [Display(Name = "券面额")]
        [Column("amount")]
        public virtual Decimal amount
        {
            get
            {
                return _amount;
            }
            set
            {
                if ((_amount != value))
                {
                    SendPropertyChanging("amount",_amount,value);
                    _amount = value;
                    SendPropertyChanged("amount");
                }
            }
        }
        Decimal _enough=0.00m;
        /// <summary>消费门槛</summary>
        [DisallowNull]
        [Display(Name = "消费门槛")]
        [Column("enough")]
        public virtual Decimal enough
        {
            get
            {
                return _enough;
            }
            set
            {
                if ((_enough != value))
                {
                    SendPropertyChanging("enough",_enough,value);
                    _enough = value;
                    SendPropertyChanged("enough");
                }
            }
        }
        Int32 _stock=0;
        /// <summary>库存</summary>
        [DisallowNull]
        [Display(Name = "库存")]
        [Column("stock")]
        public virtual Int32 stock
        {
            get
            {
                return _stock;
            }
            set
            {
                if ((_stock != value))
                {
                    SendPropertyChanging("stock",_stock,value);
                    _stock = value;
                    SendPropertyChanged("stock");
                }
            }
        }
        Int32 _limit=1;
        /// <summary>每人限制 为0时不限制</summary>
        [DisallowNull]
        [Display(Name = "每人限制 为0时不限制")]
        [Column("limit")]
        public virtual Int32 limit
        {
            get
            {
                return _limit;
            }
            set
            {
                if ((_limit != value))
                {
                    SendPropertyChanging("limit",_limit,value);
                    _limit = value;
                    SendPropertyChanged("limit");
                }
            }
        }
        String _gettime;
        /// <summary>领取周期</summary>
        [MaxLength(50)]
        [Display(Name = "领取周期")]
        [Column("gettime")]
        public virtual String gettime
        {
            get
            {
                return _gettime;
            }
            set
            {
                if ((_gettime != value))
                {
                    SendPropertyChanging("gettime",_gettime,value);
                    _gettime = value;
                    SendPropertyChanged("gettime");
                }
            }
        }
        String _usetime;
        /// <summary>有效期</summary>
        [MaxLength(50)]
        [Display(Name = "有效期")]
        [Column("usetime")]
        public virtual String usetime
        {
            get
            {
                return _usetime;
            }
            set
            {
                if ((_usetime != value))
                {
                    SendPropertyChanging("usetime",_usetime,value);
                    _usetime = value;
                    SendPropertyChanged("usetime");
                }
            }
        }
        Int32 _valid_type=1;
        /// <summary>时效:1=绝对时效（取值usertime）,2=相对时效（自领取后xxx天有效）</summary>
        [DisallowNull]
        [Display(Name = "时效:1=绝对时效（取值usertime）,2=相对时效（自领取后xxx天有效）")]
        [Column("valid_type")]
        public virtual Int32 valid_type
        {
            get
            {
                return _valid_type;
            }
            set
            {
                if ((_valid_type != value))
                {
                    SendPropertyChanging("valid_type",_valid_type,value);
                    _valid_type = value;
                    SendPropertyChanged("valid_type");
                }
            }
        }
        System.Nullable<Int32> _valid_day;
        /// <summary>相对时效天数</summary>
        [Display(Name = "相对时效天数")]
        [Column("valid_day")]
        public virtual System.Nullable<Int32> valid_day
        {
            get
            {
                return _valid_day;
            }
            set
            {
                if ((_valid_day != value))
                {
                    SendPropertyChanging("valid_day",_valid_day,value);
                    _valid_day = value;
                    SendPropertyChanged("valid_day");
                }
            }
        }
        Int32 _score=0;
        /// <summary>所需积分</summary>
        [DisallowNull]
        [Display(Name = "所需积分")]
        [Column("score")]
        public virtual Int32 score
        {
            get
            {
                return _score;
            }
            set
            {
                if ((_score != value))
                {
                    SendPropertyChanging("score",_score,value);
                    _score = value;
                    SendPropertyChanged("score");
                }
            }
        }
        String _description;
        /// <summary>描述</summary>
        [MaxLength(255)]
        [Display(Name = "描述")]
        [Column("description")]
        public virtual String description
        {
            get
            {
                return _description;
            }
            set
            {
                if ((_description != value))
                {
                    SendPropertyChanging("description",_description,value);
                    _description = value;
                    SendPropertyChanged("description");
                }
            }
        }
        System.Nullable<Int32> _createtime;
        /// <summary>创建时间</summary>
        [Display(Name = "创建时间")]
        [Column("createtime")]
        public virtual System.Nullable<Int32> createtime
        {
            get
            {
                return _createtime;
            }
            set
            {
                if ((_createtime != value))
                {
                    SendPropertyChanging("createtime",_createtime,value);
                    _createtime = value;
                    SendPropertyChanged("createtime");
                }
            }
        }
        System.Nullable<Int32> _updatetime;
        /// <summary>更新时间</summary>
        [Display(Name = "更新时间")]
        [Column("updatetime")]
        public virtual System.Nullable<Int32> updatetime
        {
            get
            {
                return _updatetime;
            }
            set
            {
                if ((_updatetime != value))
                {
                    SendPropertyChanging("updatetime",_updatetime,value);
                    _updatetime = value;
                    SendPropertyChanged("updatetime");
                }
            }
        }
        System.Nullable<Int32> _deletetime;
        /// <summary>删除时间</summary>
        [Display(Name = "删除时间")]
        [Column("deletetime")]
        public virtual System.Nullable<Int32> deletetime
        {
            get
            {
                return _deletetime;
            }
            set
            {
                if ((_deletetime != value))
                {
                    SendPropertyChanging("deletetime",_deletetime,value);
                    _deletetime = value;
                    SendPropertyChanged("deletetime");
                }
            }
        }
        System.Nullable<Int32> _usetimestart;
        [Column("usetimestart")]
        public virtual System.Nullable<Int32> usetimestart
        {
            get
            {
                return _usetimestart;
            }
            set
            {
                if ((_usetimestart != value))
                {
                    SendPropertyChanging("usetimestart",_usetimestart,value);
                    _usetimestart = value;
                    SendPropertyChanged("usetimestart");
                }
            }
        }
        System.Nullable<Int32> _usetimeend;
        [Column("usetimeend")]
        public virtual System.Nullable<Int32> usetimeend
        {
            get
            {
                return _usetimeend;
            }
            set
            {
                if ((_usetimeend != value))
                {
                    SendPropertyChanging("usetimeend",_usetimeend,value);
                    _usetimeend = value;
                    SendPropertyChanged("usetimeend");
                }
            }
        }
        System.Nullable<Int32> _gettimestart;
        [Column("gettimestart")]
        public virtual System.Nullable<Int32> gettimestart
        {
            get
            {
                return _gettimestart;
            }
            set
            {
                if ((_gettimestart != value))
                {
                    SendPropertyChanging("gettimestart",_gettimestart,value);
                    _gettimestart = value;
                    SendPropertyChanged("gettimestart");
                }
            }
        }
        System.Nullable<Int32> _gettimeend;
        [Column("gettimeend")]
        public virtual System.Nullable<Int32> gettimeend
        {
            get
            {
                return _gettimeend;
            }
            set
            {
                if ((_gettimeend != value))
                {
                    SendPropertyChanging("gettimeend",_gettimeend,value);
                    _gettimeend = value;
                    SendPropertyChanged("gettimeend");
                }
            }
        }
        System.Nullable<Decimal> _discount;
        /// <summary>折扣券折扣</summary>
        [Display(Name = "折扣券折扣")]
        [Column("discount")]
        public virtual System.Nullable<Decimal> discount
        {
            get
            {
                return _discount;
            }
            set
            {
                if ((_discount != value))
                {
                    SendPropertyChanging("discount",_discount,value);
                    _discount = value;
                    SendPropertyChanged("discount");
                }
            }
        }
        System.Nullable<Int32> _discount_type;
        /// <summary>折扣类型:1=满减,2=折扣</summary>
        [Display(Name = "折扣类型:1=满减,2=折扣")]
        [Column("discount_type")]
        public virtual System.Nullable<Int32> discount_type
        {
            get
            {
                return _discount_type;
            }
            set
            {
                if ((_discount_type != value))
                {
                    SendPropertyChanging("discount_type",_discount_type,value);
                    _discount_type = value;
                    SendPropertyChanged("discount_type");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<fa_coupons, bool>> exp)
        {
            base.SetValue<fa_coupons>(exp);
        }
    }
    [TableConfig]
    [Table("fa_vip")]
    [Way.EntityDB.DataItemJsonConverter]
    public class fa_vip :Way.EntityDB.DataItem
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
        String _user_type;
        /// <summary>用户类型</summary>
        [MaxLength(255)]
        [Display(Name = "用户类型")]
        [Column("user_type")]
        public virtual String user_type
        {
            get
            {
                return _user_type;
            }
            set
            {
                if ((_user_type != value))
                {
                    SendPropertyChanging("user_type",_user_type,value);
                    _user_type = value;
                    SendPropertyChanged("user_type");
                }
            }
        }
        String _name;
        /// <summary>名称</summary>
        [MaxLength(255)]
        [Display(Name = "名称")]
        [Column("name")]
        public virtual String name
        {
            get
            {
                return _name;
            }
            set
            {
                if ((_name != value))
                {
                    SendPropertyChanging("name",_name,value);
                    _name = value;
                    SendPropertyChanged("name");
                }
            }
        }
        String _minipic;
        /// <summary>小图</summary>
        [MaxLength(255)]
        [Display(Name = "小图")]
        [Column("minipic")]
        public virtual String minipic
        {
            get
            {
                return _minipic;
            }
            set
            {
                if ((_minipic != value))
                {
                    SendPropertyChanging("minipic",_minipic,value);
                    _minipic = value;
                    SendPropertyChanged("minipic");
                }
            }
        }
        String _pic;
        /// <summary>图片</summary>
        [MaxLength(255)]
        [Display(Name = "图片")]
        [Column("pic")]
        public virtual String pic
        {
            get
            {
                return _pic;
            }
            set
            {
                if ((_pic != value))
                {
                    SendPropertyChanging("pic",_pic,value);
                    _pic = value;
                    SendPropertyChanged("pic");
                }
            }
        }
        System.Nullable<Decimal> _price;
        /// <summary>价格</summary>
        [Display(Name = "价格")]
        [Column("price")]
        public virtual System.Nullable<Decimal> price
        {
            get
            {
                return _price;
            }
            set
            {
                if ((_price != value))
                {
                    SendPropertyChanging("price",_price,value);
                    _price = value;
                    SendPropertyChanged("price");
                }
            }
        }
        System.Nullable<Decimal> _disPrice;
        /// <summary>优惠价</summary>
        [Display(Name = "优惠价")]
        [Column("disprice")]
        public virtual System.Nullable<Decimal> disPrice
        {
            get
            {
                return _disPrice;
            }
            set
            {
                if ((_disPrice != value))
                {
                    SendPropertyChanging("disPrice",_disPrice,value);
                    _disPrice = value;
                    SendPropertyChanged("disPrice");
                }
            }
        }
        String _viptime;
        /// <summary>会员时间</summary>
        [MaxLength(255)]
        [Display(Name = "会员时间")]
        [Column("viptime")]
        public virtual String viptime
        {
            get
            {
                return _viptime;
            }
            set
            {
                if ((_viptime != value))
                {
                    SendPropertyChanging("viptime",_viptime,value);
                    _viptime = value;
                    SendPropertyChanged("viptime");
                }
            }
        }
        String _introduction;
        /// <summary>简介</summary>
        [Display(Name = "简介")]
        [Column("introduction")]
        public virtual String introduction
        {
            get
            {
                return _introduction;
            }
            set
            {
                if ((_introduction != value))
                {
                    SendPropertyChanging("introduction",_introduction,value);
                    _introduction = value;
                    SendPropertyChanged("introduction");
                }
            }
        }
        String _details;
        /// <summary>详情</summary>
        [Display(Name = "详情")]
        [Column("details")]
        public virtual String details
        {
            get
            {
                return _details;
            }
            set
            {
                if ((_details != value))
                {
                    SendPropertyChanging("details",_details,value);
                    _details = value;
                    SendPropertyChanged("details");
                }
            }
        }
        System.Nullable<Int32> _state;
        /// <summary>状态</summary>
        [Display(Name = "状态")]
        [Column("state")]
        public virtual System.Nullable<Int32> state
        {
            get
            {
                return _state;
            }
            set
            {
                if ((_state != value))
                {
                    SendPropertyChanging("state",_state,value);
                    _state = value;
                    SendPropertyChanged("state");
                }
            }
        }
        String _remarks;
        /// <summary>备注</summary>
        [MaxLength(255)]
        [Display(Name = "备注")]
        [Column("remarks")]
        public virtual String remarks
        {
            get
            {
                return _remarks;
            }
            set
            {
                if ((_remarks != value))
                {
                    SendPropertyChanging("remarks",_remarks,value);
                    _remarks = value;
                    SendPropertyChanged("remarks");
                }
            }
        }
        System.Nullable<Int32> _createtime;
        /// <summary>创建时间</summary>
        [Display(Name = "创建时间")]
        [Column("createtime")]
        public virtual System.Nullable<Int32> createtime
        {
            get
            {
                return _createtime;
            }
            set
            {
                if ((_createtime != value))
                {
                    SendPropertyChanging("createtime",_createtime,value);
                    _createtime = value;
                    SendPropertyChanged("createtime");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<fa_vip, bool>> exp)
        {
            base.SetValue<fa_vip>(exp);
        }
    }
    [TableConfig]
    [Table("fa_user_coupons")]
    [Way.EntityDB.DataItemJsonConverter]
    public class fa_user_coupons :Way.EntityDB.DataItem
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
        System.Nullable<Int32> _user_id;
        /// <summary>用户</summary>
        [Display(Name = "用户")]
        [Column("user_id")]
        public virtual System.Nullable<Int32> user_id
        {
            get
            {
                return _user_id;
            }
            set
            {
                if ((_user_id != value))
                {
                    SendPropertyChanging("user_id",_user_id,value);
                    _user_id = value;
                    SendPropertyChanged("user_id");
                }
            }
        }
        String _user_type="user";
        /// <summary>用户类型</summary>
        [MaxLength(20)]
        [DisallowNull]
        [Display(Name = "用户类型")]
        [Column("user_type")]
        public virtual String user_type
        {
            get
            {
                return _user_type;
            }
            set
            {
                if ((_user_type != value))
                {
                    SendPropertyChanging("user_type",_user_type,value);
                    _user_type = value;
                    SendPropertyChanged("user_type");
                }
            }
        }
        System.Nullable<Int32> _coupons_id;
        /// <summary>优惠券</summary>
        [Display(Name = "优惠券")]
        [Column("coupons_id")]
        public virtual System.Nullable<Int32> coupons_id
        {
            get
            {
                return _coupons_id;
            }
            set
            {
                if ((_coupons_id != value))
                {
                    SendPropertyChanging("coupons_id",_coupons_id,value);
                    _coupons_id = value;
                    SendPropertyChanged("coupons_id");
                }
            }
        }
        Int32 _use_order_id=0;
        /// <summary>订单 id</summary>
        [DisallowNull]
        [Display(Name = "订单 id")]
        [Column("use_order_id")]
        public virtual Int32 use_order_id
        {
            get
            {
                return _use_order_id;
            }
            set
            {
                if ((_use_order_id != value))
                {
                    SendPropertyChanging("use_order_id",_use_order_id,value);
                    _use_order_id = value;
                    SendPropertyChanged("use_order_id");
                }
            }
        }
        System.Nullable<Int32> _usetime;
        /// <summary>使用时间</summary>
        [Display(Name = "使用时间")]
        [Column("usetime")]
        public virtual System.Nullable<Int32> usetime
        {
            get
            {
                return _usetime;
            }
            set
            {
                if ((_usetime != value))
                {
                    SendPropertyChanging("usetime",_usetime,value);
                    _usetime = value;
                    SendPropertyChanged("usetime");
                }
            }
        }
        System.Nullable<Int32> _expiretime;
        /// <summary>到期时间</summary>
        [Display(Name = "到期时间")]
        [Column("expiretime")]
        public virtual System.Nullable<Int32> expiretime
        {
            get
            {
                return _expiretime;
            }
            set
            {
                if ((_expiretime != value))
                {
                    SendPropertyChanging("expiretime",_expiretime,value);
                    _expiretime = value;
                    SendPropertyChanged("expiretime");
                }
            }
        }
        System.Nullable<Int32> _createtime;
        /// <summary>领取时间</summary>
        [Display(Name = "领取时间")]
        [Column("createtime")]
        public virtual System.Nullable<Int32> createtime
        {
            get
            {
                return _createtime;
            }
            set
            {
                if ((_createtime != value))
                {
                    SendPropertyChanging("createtime",_createtime,value);
                    _createtime = value;
                    SendPropertyChanged("createtime");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<fa_user_coupons, bool>> exp)
        {
            base.SetValue<fa_user_coupons>(exp);
        }
    }
}

namespace Dfd.Common.DBModels.DB
{
    public class laywer : Way.EntityDB.DBContext
    {
         public laywer(string connection, Way.EntityDB.DatabaseType dbType , bool upgradeDatabase = true): base(connection, dbType , upgradeDatabase)
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
             var db =  sender as Dfd.Common.DBModels.DB.laywer;
            if (db == null) return;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<fa_case_lawyer>().HasKey(m => m.id);
            modelBuilder.Entity<fa_case_pay>().HasKey(m => m.id);
            modelBuilder.Entity<fa_bank_card>().HasKey(m => m.id);
            modelBuilder.Entity<fa_withdraw>().HasKey(m => m.id);
            modelBuilder.Entity<fa_invoice_info>().HasKey(m => m.id);
            modelBuilder.Entity<fa_case_exam>().HasKey(m => m.id);
            modelBuilder.Entity<fa_pay_back>().HasKey(m => m.id);
            modelBuilder.Entity<fa_lawyerinfo>().HasKey(m => m.id);
            modelBuilder.Entity<fa_case_source>().HasKey(m => m.id);
            modelBuilder.Entity<fa_case_class>().HasKey(m => m.id);
            modelBuilder.Entity<fa_user_token>().HasKey(m => m.token);
            modelBuilder.Entity<fa_area>().HasKey(m => m.id);
            modelBuilder.Entity<fa_case_lawyer_power>().HasKey(m => m.id);
            modelBuilder.Entity<fa_user>().HasKey(m => m.id);
            modelBuilder.Entity<fa_commission>().HasKey(m => m.id);
            modelBuilder.Entity<fa_user_money_log>().HasKey(m => m.id);
            modelBuilder.Entity<fa_setting>().HasKey(m => m.id);
            modelBuilder.Entity<fa_order>().HasKey(m => m.id);
            modelBuilder.Entity<fa_invite>().HasKey(m => m.id);
            modelBuilder.Entity<fa_coupons>().HasKey(m => m.id);
            modelBuilder.Entity<fa_vip>().HasKey(m => m.id);
            modelBuilder.Entity<fa_user_coupons>().HasKey(m => m.id);
        }
        System.Linq.IQueryable<fa_case_lawyer> _fa_case_lawyer;
        /// <summary>律师案件关联表</summary>
        public virtual System.Linq.IQueryable<fa_case_lawyer> fa_case_lawyer
        {
            get
            {
                if (_fa_case_lawyer == null)
                {
                    _fa_case_lawyer = this.Set<fa_case_lawyer>();
                }
                return _fa_case_lawyer;
            }
        }
        System.Linq.IQueryable<fa_case_pay> _fa_case_pay;
        /// <summary>案件付款表</summary>
        public virtual System.Linq.IQueryable<fa_case_pay> fa_case_pay
        {
            get
            {
                if (_fa_case_pay == null)
                {
                    _fa_case_pay = this.Set<fa_case_pay>();
                }
                return _fa_case_pay;
            }
        }
        System.Linq.IQueryable<fa_bank_card> _fa_bank_card;
        /// <summary>用户银行卡</summary>
        public virtual System.Linq.IQueryable<fa_bank_card> fa_bank_card
        {
            get
            {
                if (_fa_bank_card == null)
                {
                    _fa_bank_card = this.Set<fa_bank_card>();
                }
                return _fa_bank_card;
            }
        }
        System.Linq.IQueryable<fa_withdraw> _fa_withdraw;
        /// <summary>提现表</summary>
        public virtual System.Linq.IQueryable<fa_withdraw> fa_withdraw
        {
            get
            {
                if (_fa_withdraw == null)
                {
                    _fa_withdraw = this.Set<fa_withdraw>();
                }
                return _fa_withdraw;
            }
        }
        System.Linq.IQueryable<fa_invoice_info> _fa_invoice_info;
        /// <summary>发票信息</summary>
        public virtual System.Linq.IQueryable<fa_invoice_info> fa_invoice_info
        {
            get
            {
                if (_fa_invoice_info == null)
                {
                    _fa_invoice_info = this.Set<fa_invoice_info>();
                }
                return _fa_invoice_info;
            }
        }
        System.Linq.IQueryable<fa_case_exam> _fa_case_exam;
        /// <summary>待客服审核案件</summary>
        public virtual System.Linq.IQueryable<fa_case_exam> fa_case_exam
        {
            get
            {
                if (_fa_case_exam == null)
                {
                    _fa_case_exam = this.Set<fa_case_exam>();
                }
                return _fa_case_exam;
            }
        }
        System.Linq.IQueryable<fa_pay_back> _fa_pay_back;
        /// <summary>退款表</summary>
        public virtual System.Linq.IQueryable<fa_pay_back> fa_pay_back
        {
            get
            {
                if (_fa_pay_back == null)
                {
                    _fa_pay_back = this.Set<fa_pay_back>();
                }
                return _fa_pay_back;
            }
        }
        System.Linq.IQueryable<fa_lawyerinfo> _fa_lawyerinfo;
        /// <summary>律师信息</summary>
        public virtual System.Linq.IQueryable<fa_lawyerinfo> fa_lawyerinfo
        {
            get
            {
                if (_fa_lawyerinfo == null)
                {
                    _fa_lawyerinfo = this.Set<fa_lawyerinfo>();
                }
                return _fa_lawyerinfo;
            }
        }
        System.Linq.IQueryable<fa_case_source> _fa_case_source;
        /// <summary>案件表</summary>
        public virtual System.Linq.IQueryable<fa_case_source> fa_case_source
        {
            get
            {
                if (_fa_case_source == null)
                {
                    _fa_case_source = this.Set<fa_case_source>();
                }
                return _fa_case_source;
            }
        }
        System.Linq.IQueryable<fa_case_class> _fa_case_class;
        /// <summary>案件类别</summary>
        public virtual System.Linq.IQueryable<fa_case_class> fa_case_class
        {
            get
            {
                if (_fa_case_class == null)
                {
                    _fa_case_class = this.Set<fa_case_class>();
                }
                return _fa_case_class;
            }
        }
        System.Linq.IQueryable<fa_user_token> _fa_user_token;
        public virtual System.Linq.IQueryable<fa_user_token> fa_user_token
        {
            get
            {
                if (_fa_user_token == null)
                {
                    _fa_user_token = this.Set<fa_user_token>();
                }
                return _fa_user_token;
            }
        }
        System.Linq.IQueryable<fa_area> _fa_area;
        public virtual System.Linq.IQueryable<fa_area> fa_area
        {
            get
            {
                if (_fa_area == null)
                {
                    _fa_area = this.Set<fa_area>();
                }
                return _fa_area;
            }
        }
        System.Linq.IQueryable<fa_case_lawyer_power> _fa_case_lawyer_power;
        /// <summary>律师可以抢哪些案件</summary>
        public virtual System.Linq.IQueryable<fa_case_lawyer_power> fa_case_lawyer_power
        {
            get
            {
                if (_fa_case_lawyer_power == null)
                {
                    _fa_case_lawyer_power = this.Set<fa_case_lawyer_power>();
                }
                return _fa_case_lawyer_power;
            }
        }
        System.Linq.IQueryable<fa_user> _fa_user;
        public virtual System.Linq.IQueryable<fa_user> fa_user
        {
            get
            {
                if (_fa_user == null)
                {
                    _fa_user = this.Set<fa_user>();
                }
                return _fa_user;
            }
        }
        System.Linq.IQueryable<fa_commission> _fa_commission;
        /// <summary>案件返佣/提现卷</summary>
        public virtual System.Linq.IQueryable<fa_commission> fa_commission
        {
            get
            {
                if (_fa_commission == null)
                {
                    _fa_commission = this.Set<fa_commission>();
                }
                return _fa_commission;
            }
        }
        System.Linq.IQueryable<fa_user_money_log> _fa_user_money_log;
        public virtual System.Linq.IQueryable<fa_user_money_log> fa_user_money_log
        {
            get
            {
                if (_fa_user_money_log == null)
                {
                    _fa_user_money_log = this.Set<fa_user_money_log>();
                }
                return _fa_user_money_log;
            }
        }
        System.Linq.IQueryable<fa_setting> _fa_setting;
        public virtual System.Linq.IQueryable<fa_setting> fa_setting
        {
            get
            {
                if (_fa_setting == null)
                {
                    _fa_setting = this.Set<fa_setting>();
                }
                return _fa_setting;
            }
        }
        System.Linq.IQueryable<fa_order> _fa_order;
        public virtual System.Linq.IQueryable<fa_order> fa_order
        {
            get
            {
                if (_fa_order == null)
                {
                    _fa_order = this.Set<fa_order>();
                }
                return _fa_order;
            }
        }
        System.Linq.IQueryable<fa_invite> _fa_invite;
        public virtual System.Linq.IQueryable<fa_invite> fa_invite
        {
            get
            {
                if (_fa_invite == null)
                {
                    _fa_invite = this.Set<fa_invite>();
                }
                return _fa_invite;
            }
        }
        System.Linq.IQueryable<fa_coupons> _fa_coupons;
        public virtual System.Linq.IQueryable<fa_coupons> fa_coupons
        {
            get
            {
                if (_fa_coupons == null)
                {
                    _fa_coupons = this.Set<fa_coupons>();
                }
                return _fa_coupons;
            }
        }
        System.Linq.IQueryable<fa_vip> _fa_vip;
        public virtual System.Linq.IQueryable<fa_vip> fa_vip
        {
            get
            {
                if (_fa_vip == null)
                {
                    _fa_vip = this.Set<fa_vip>();
                }
                return _fa_vip;
            }
        }
        System.Linq.IQueryable<fa_user_coupons> _fa_user_coupons;
        public virtual System.Linq.IQueryable<fa_user_coupons> fa_user_coupons
        {
            get
            {
                if (_fa_user_coupons == null)
                {
                    _fa_user_coupons = this.Set<fa_user_coupons>();
                }
                return _fa_user_coupons;
            }
        }
        protected override string GetDesignString()
        {
            var result = new StringBuilder();
            result.Append("\r\n");
            result.Append("H4sIAAAAAAAAA+1dXXPTSLr+K6lch4Nlx448VVzswl5Q5+zs1rLn1KnaTLkUWwk62HLWlmEoiipnZgDzERyGwMIQIMl8QM0ySWZg80nIn7Fk52r/wmm1ZEfqlq2orZa7nVwllmRbevz2+z7vZ98Y/as0lZfLo5/97Yb17+dSQR79bPSP1y/9PT86NvqX4jXr5EVNLlj/");
            result.Append("2VcoOXD6f6R8BbxIxm6OdU5o12flo1Oj50uypMnws3+X1ZSiOuq4NltUNVnVHJffmLTuY3L0M/CvkgN/J5Jjk6NZadZ8M3g5Oap/uq9v14yVWmNvU7/1vjW32Fp5OzkKrjI/Fl4yLWWyUlnO5KVr1+USPHdB0qQpcOziBXCFIIIjyn8Vs1fAC3D3k6Pni/lKQS1Pms/Y");
            result.Append("/uK06PpmtZLPO7/EvAi8vlj+XUUrXlSzJbkAHgac0koV2fxISf0cvAUcmJbyZfNIbuqvAB3rzeaV4JB9wP7sP6iVwgV5WlEdx/KyOqNdhm+C78jJ01Ilr0HEjq6CqMFng3hdLP/5P+Er+16KpZxcgk8Vg2cvKOVsSSkoqqQVS447PJ+XymX7Cc1PBj9WB4205+9go9BB");
            result.Append("xYI80xWczjd10LFvcQDgtO/lCB2BEB0xFkPQseQTRQdKJafYxImxERBsmvc2jercyJmR1sGL1sqDxvYvI/rWb3r9qbFZM/9r7D0z3n2Cx9YeGLUFN4ZlTdIqZS4hTJBCiCnBw6+qzd2f9KUN/WXVDY+iXlUAQuDCjJTLlfrA6apUyl6WSsGxiieTocA1TgxXyhMu4x+b");
            result.Append("h//40BUuTTGPEcOVA4au8xHRy1aSGKwJbwP7bF1f+ElfeGB8U7fgQ4ArZ6SZktwPYlPKoBZiKhhYX5iXX/jf80V1WpmBJME6AtmDk9HkbJbhpEeCCN5vUqlLQM5kyDf8CJVAlVChi8MyVZbS7UqlZqXrhDxKjMW55FEpKjxKjCVQ+BfXAfattVV9/ole33LDD7+wP8ZA");
            result.Append("rMaTMQKgwuRU46gWv/PocPWlGyCpUKzYMkCotuWsUpDyweEBwh8PBSBiMxdDWUHz3bvG9l3j6U5PmdIuK6VcZggki5yRoioQ9yk9mTvjzo0PXCIxXChD6BB4/dOtI7Ju/ccPWfeBK00Kl4Dqrebia+DFeLHPLLTmrDNPPwVPbAsFVIFZIuSFFOAfvMNEbAgn0NCLDdPT");
            result.Append("Hf1jHYcJ6GRVlfMcK3ZiX0ZEwzAWUo3dXVSfV8r8KvKA3osDHjQSYyxuAni4jOT5YDRBilEa5QaNj8+Mr5f1+S0UIBg6kDPZYmW2qPKKU8CQVNSecJyqJ4zyGtsT3v8eeBrebnC2WCgo5TJ8wxiZN8xnVmHCKUYhesOYaYPgN/eetzbmONLYLnhC9IGFIcgr+GBD7MUJ");
            result.Append("qDXjNT7gAxBx1kCIj3mxISss4ClCJn3kVIyIoygCGpDj3V3zAYqYXAtY4NhKEWz91tyr67VvW/vv9NpGY/85tgi14pSUl9Ts8VHzMG6hZQsmRy0v5fiQMZ4uSFAlSSK2PN4ata3Dx5utlQf6/ArGk6Yk9QogS6UcKUMS3LQsVIY0pcwQqCj7te/KE+mQJMH7J+DKpXVB");
            result.Append("EyZBQgmkLZgL8803G258oGCqUl+6mzgyIsSOGRrxAYqYLcVRJql/rAIhAlh52jgpmzWJE1zNPONFTJ7iKLsEug5LpYBnmJIHUmNx3EibDzzErCmOckv9zWOw6BB4BrTYQgInIFOK2uyP0zT7WCTVqC80H2541QdcU7TLuZJ0jdTex928kh97L8bo2Ps4lm/jzt67oQnR");
            result.Append("3se9uVDz1z391f2RMyMmKCOOul83Whp8UGYVkh9q5MYfY0l8hkr8ACI2ZwlU3TlT3PrainF3B6lMBY7v7ffcJLv9gCNOlySwIByb/DukFUhcQ5HA+BLT9DskuIhrKBJoUI5DeulrA4kJQgKtMPHyTcwQTIZ5B8UXJGKmkMAah2ov9L1dbuO6vkgRs4MElvLmuAzHFybi");
            result.Append("iEAKi3/fvd/c+6X1YQdxifoqih88iSJOEKSwToK1V4fPb+m1jdaHnzwZZ6l0VZHymSFgnsQ1OVh83XK0Ow6NlU+xDo40Dl621udMKKtVrNay7Yfz7uUwXpKSpBp2waKO9UfNH982DlaMuXUs8qKoV4tKVgZ/p4uk0Zekm2lxFH0RnGIUXvQliZKrTmcMbylzN0IhBmGS");
            result.Append("GLWCUmrce6f/8KGjufT5J439+ZHG9s+N3V0kIaxoeblfPTU44IgpRBJrPIDAdSAznq8dVr+zDo7oq6/06sfm24fWawRBXrEjDtEkMXbhEDoP8RqEBTxu56sfSMQULImFR03xQRuApC/Zbvzxg4eYbCXROGhz8V+t9ZcAoeYyUm44e7moyhz4zz5QEQf2Uli+GKpzr5bz");
            result.Append("bLEwK6nXme8398OKOLiXwlgbDO5ZoVCu43s+iBHH91JoONTCCviJmLbq1BPxvxjJI34pNB7amltsvt/DiJXpZEhZjfkwux9QxNQ0hbJ3Y3HTbOr0UFs5Oa9clUtQbcnlfvI2A9dc5LG/FErlW+tzJl7oKlSGYfmRz1/BpKpdDYH6hJ0gDKc+YUBBijr4kqIafMFydGbu");
            result.Append("d9VYmjczwMvbVu8FFoWBYQD5S6lAGoJJUWwJohuCiTuFKbwQTAql6fx1vbihCTH2MoHxcks2PZI1pkyynq3xgYnYrk1glPzNovGBy1ZXP4zIrRqq747iUE83rCU3Yrz4YMyvWrDxEoHywStgBCpqEzdB1cShfoSVSPIq6zQD21NS9gqpVRMpjn2ia9USdKwahj2HVs0F");
            result.Append("TYhWTfROuqDYOKaR8QoRsUUTsRmQcOnqD1/rL5bdMAE1UbY71Fl1aH1QIrZpIjb6EaLEb9WPD1DE6RQRTafY4gTZJBZX6rg3jHr+PiiRD5RB8ylOlHjl3D5gkY+XwUZEw8ofL5ikUkm5KuU5RypgciVqJilSZZLYdMavqq31rc60HK9ilaNxOaSkMu32fjkileN0SGUa");
            result.Append("G67d8eHuPTPufj9i7K3od5ApYey6bi6UQiRPadTVtdBBaBNYEgMqQhwPBR5i1pTGXBMgNT98p288an2931xEnUOIAMc4EZOmNFZ4f6du4YQS8FwlwLQLFjEipkxp1E9xjglp7B9g4qSUM5Wy3I83R2+QuB9KxFwpPQQ9nj7YBAwGRM2O0j3Y0WVJnQnEjv6Uz3U20GkT");
            result.Append("Hiuq3KnN/Vy+5neJKl/zID1CDB/k1K4Dd0tLQSoBCsOS2sFn7Yh+sz9JlTOACZvkBJs43RhF0aQ5OXr2rL70trl7MDmpFtU8ODpybgSsh0n17NnmT3v2ielp+4zVghB4/fkhSTC/IAslP+eQwi/gneRlDTsa4WJN9dpiKpTFCsOK5WKlZJttr9WKXuNerizD12tDifDg");
            result.Append("y5oS1Ru9o0vCBO+GKef/rSp/hwvFXrIXy+fzlbIml0xqcST18HPM+4LvbDeRfnGkIjpaxfGR7Xcf8zNnzfUX7kdek5WZy/iH0hWbXtNXQxEb1+gQL6FxXXAckWG4r2pw3f3B/EJw6PdS9kpl1vqNc38uFWflkqZAUbzhuDvzRy2ZQRMpf2TvzYjtTeeDe17VCQjfvHmT");
            result.Append("DRXZa4heeCqyE0fvqiE7V/BjXXoNIgoFOjx06AUffhU/EPZqKgwLQnerYBcE3RfxA2CvwkBGTRUbwPUqNwlP77W3D+uq9toX8ANcr+xKeMA5R1l1w85xDT/wUQ+/9O/RHUUW8Aaf+iN9+2vO9hURwx00zrB0TQx9vKAvZ7IjpJG6kxPRRCGGVWNOUPfGhxs+qlPSsTS8");
            result.Append("VTGt19cbez8a91b1xz83dl/0aiCxa9Bni9eId6wHdorXmts0nWEeABFskByHHQBpSoM8ADxYYxt3Ncl+4LDd0TZBPXgSKg/GcvtQXOxhqfao1OVtwfwXaD1wNj4CE/+P9LXvrJWX8JAsdganTo4KaL7Sb/siAvk6WbSPemwrVAnHJvRwXerrJ7ykFWMAKHzYnwkU3kIm");
            result.Append("uHrI4l4YMpJXIVj8BL1kJ2vxU4/Lhrr4sZzh/W+bey+9W2z+T85qGQ46bUJWASdPhKOJkJ/GkgL9KNFE309/lEA/SjQx/WGNUInRBK2HLxUnUg8sD30Bgkg9uDzkBQgi9fqhIS1AEDmpHup4CUIM6wleva3Xn+qP3hpLr91ewoys9RkgoN457dPEQTo/FMCEb+1u9v4Y");
            result.Append("S3eNJzUMKcBYOEeKdHqoIGCbvB9WvwJI6U9u64/nrIAKIlbFYq7M/Jh/H7wCzg5lWYXxFOEUBCxVuVDTFx4YT+809jZbW+v6p2+QED24y5I5VHRWMiWA41AHwT7eJ8qBE6mOV8SbjGF8uHWw2NhHOrFNcS4WCkq5rBDPCgCCzm02nM4EKoAI9hPcreq/vuKv9zRNaRAV");
            result.Append("gAjLiL/80dhd4DAvTmkQFYAIzQJbw7o8qiHNOV08ixFxekwQsM3CoABZSHnthQVFaaAbYR1vboAfZMTtu4KAZhTtjXfWVvX5J16SxalQkU5WAAhh+SnHaIXm4mvgWP37o1l0ZrW1/fvjXTdoSjlzVcr3BVtooxbwznA/3EhnLQDc8G0/Xhu1BW6HwvkhRe60x7F9kKFU");
            result.Append("eSEFJYlzoAL67FH7lfQzj26S7elWui/p5lXGUa/SpZo+rDffrOu1DavpFlNKWnFKyktqXxNzQlRMAfUSwRQ4VgMZ9LOqfQpc76bv4RLDoPYxmByO+XR5I7fj2cXNTvu2SD/1TFlyI+d2nkENTsidn/AePaWH3JqLFsjt2MC0BlvIh6s2joM8ExojTb/agorGYAM8+q18");
            result.Append("QwxeNI18IaWL4r2D6Gw3Tp0Wv/Yrq9GMxTmtswz0o0TTM9ZXnaVDgWAdmMOSb55InpTAQJqDCocg8J1WQwDb1/fuBTyqzmj6kSIrUT9Vs6zvwpGOpn0oLGONDbhnXYqEWPx4iesTZK2ph/HDKOZ3CB1aT6LXXuh7u93Ts30mHQe3u7JAXh0RRwtKGgdrxuKOF0iV2RzX");
            result.Append("IA3P3PJ0NB1xhC1dDuFCS2+MxU1z17x6vXWwgSr8giVFDBMGv8H4QyNfQox6FHtI246EWBRNg2Eayd6bxEezrYW+9Zv+sarf2joHN7Mwln62Xx5rBwusssLXEkRasE9Z3k47LPtFkHos2rXltRd6rgu6rlSs2nBg29CA9WqNMDl3Bi5Ye3fX7V+s9QtOH1a/ax3cOd76");
            result.Append("xZlc+OV1zAofd22qCbSU096as7atL8w332yA339EX3io1zdaD7f0+lPgSRjLO8b8mvl0xv2PxsMfR+zdI8967ZaoKVq+HwejD48/nAZEgv3JmJXOKPYPCFc6sbg7XZ+f9qA1P1kjb0hK4Ju/UXX8B41UpPMUKS/LaLINfbv/CSx3sPbq8Pmt7s1KJg/ioFfpJAUCOOoV");
            result.Append("SKChzMERZKsRqL05Y6fgU7+33Cn4NPdnPDppfPvD4WIVHDsjEFHmkJucCJPep7XGhEF4n0Lh1trKaaHwYPKsQoz55hWHBsRaXfe/N+2sh6kNaGY9RGbwPcGh2NnI2jm9IOSpn3Ms5OYIbDrR6jemnmtbh5B6Q9lDPVj0KgzUGaGS3PdSnSCCic8sZ61NEKp0VmRbYL7r");
            result.Append("xyHFGEuHouFp5kxamee8GV8IONeIZXdciGavs/4jPyiV4m/OkV+QJ2jolWmxiqZ1LNQR0W1hE2PY1ipwRxBL4vRb71tzi55yF7zFzINLDlD+gqm1UNm7KGJJwG6juoKO6WIK4+ipugNiLCYEa+fC2meOKZyDzWwNFWdqtoopgAMaqzAQZsSyRdNo2jdhGkcVas84SLs2");
            result.Append("knFm7iuV5CPrxo8VMbeUpVfcXCtm2I6a+2I3RDWMQjSNx/0vUrRkQt95r9c3jHv7dpwdG77p9sh5zWcLw9PDJAjUS5pMxttVzjonu8oYNkp5cR2Qa339dnMZiTSa1RKz1/rxnmnP6k/HewsWX4PTVXgraF/xWF+fKxckJR/2hxaKU0re41YpryzqVUmWl9OzDh25pOsq");
            result.Append("w6Zxc7zKxHAjBWyrb+r1SFOSegXQgVKuq4y5r+gqYmj3aWv3n429/db6nF7fQoo4chkgCFO2ZWBTyCbEUFW5k8pj8bulDf3BLlKuUJIljtEhn5w8ju2HDNB5WUXQyeVKcrmfpCP1qnk/hMi3ORpH81nNxX+11tHK0stFtR9PedACFHBXIyc87vCW/c0dYGwCwi0wQ2Xc");
            result.Append("epSaXYB3F8S4efu4qEtK72E4LfdgQxTizBcUMI1erzR5/xtOuQkLqlEvmZszqjMQMpLtpZK9O7IZ3l7K5YeEuL1UsrcJuyJfZ9iApf28M2JYUrGesFy17ih6YBLHpIR+yBB0bEWqZOgWTTjViJdyLjvOc6SZo8nHDete7kKc6VSJT6VOGps1ZbWOz2N7BLoafvurECMz");
            result.Append("eSFlOIPNgR7zK4ptP5XnrhnwKZlJvMep51kgyl2l9OhstwCdiO23Xa2aBTZwloVX1l3+UiownnFPUxjOGlbGxQbjmDkMeM+Zshp1FiNOPYsRzjQYTJfa0jv/BAsvl+TpiprLqEWGWbLfyJch6t6PRzO5NaR9GtJY/Nmqrn1wx/jwyXg839hfckubabIkrb+Sh2O1wBjL");
            result.Append("d5q/fNIf7Bzemrfnht29r997q9dum0cIRw8xrEAJUtbRT8IW4hz1y6ZRCmBjZjxbb+xVGzu15q97+qv7HhXNGpRMZtVpyJs0M61OmZ6FyjJyCaaHfDpWKVqgzXcmXYz1XpvD1FiU4KNfLR5Dy6+tcmLTDsDiYas8CJG1cma6VCz8nuW9PX0rO4dolGKCehNbP54b08id");
            result.Append("7sbFIJFOUA/1hlZPmUhhPVRWO8bS2+buAaY2i2pe6asah6LG9CumTA5RLXwimu23wtlDJiFilfGWjNXXrdAX0nohqRkr8MWGnKHjNE7OtjFAO7AvZkzjx8FWT0zjRz0iFYqfmxDdTQloPUc7qcXqwAw/vzbSPXEpSxT1mFM4c7FZxnC8R/QphGJXHBqKj8J0lIPFooyT");
            result.Append("UZHRLj1hpihjnP6cHygc5R6JmaPz3exgGiP6cJsIvbbttV9EDvyUzE8MT6d7S2XQDePHHGhhrYwQJCtz9ZlwztpqYyx+rhd4/eaz6K1nX+QCbn3ItEmkH4cjXp69rQhXizY2Fh/EovWzJZ1b9LQllqqJypKAl/aHQ2E9Com2RdZsbDBRB68vXS8Dqf6Pi6qWGoff5Lra");
            result.Append("FmP8+ktaSVFn8DccyfLx3+N6qK639oX5WObZS7Jmv3FaTmUlQZTPpGJZ+cz4eG7iTDolS2em0wk5KUlTsZicHr35/yU7GFfROgEA");
            return result.ToString();
        }
    }
}

/*<design>
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
<design>*/

