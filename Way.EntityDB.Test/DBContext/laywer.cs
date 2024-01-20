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
        [MaxLength(30)]
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
    [TableConfig]
    [Table("fa_case_bid")]
    [Way.EntityDB.DataItemJsonConverter]
    public class fa_case_bid :Way.EntityDB.DataItem
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
        System.Nullable<Int32> _case_id;
        /// <summary>案源</summary>
        [Display(Name = "案源")]
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
        System.Nullable<Int32> _user_id;
        /// <summary>律师</summary>
        [Display(Name = "律师")]
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
        String _order_id;
        /// <summary>订单号ID</summary>
        [MaxLength(255)]
        [Display(Name = "订单号ID")]
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
        System.Nullable<Int32> _status;
        /// <summary>状态</summary>
        [Display(Name = "状态")]
        [Column("status")]
        public virtual System.Nullable<Int32> status
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
        System.Nullable<Decimal> _money;
        /// <summary>金额</summary>
        [Display(Name = "金额")]
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
        String _memo;
        /// <summary>注释</summary>
        [MaxLength(255)]
        [Display(Name = "注释")]
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
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<fa_case_bid, bool>> exp)
        {
            base.SetValue<fa_case_bid>(exp);
        }
    }
    [TableConfig]
    [Table("fa_sms")]
    [Way.EntityDB.DataItemJsonConverter]
    public class fa_sms :Way.EntityDB.DataItem
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
       
        String _mobile;
        /// <summary>手机号</summary>
        [MaxLength(20)]
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
        String _code;
        /// <summary>验证码</summary>
        [MaxLength(10)]
        [Display(Name = "验证码")]
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
        Int32 _times=0;
        /// <summary>验证次数</summary>
        [DisallowNull]
        [Display(Name = "验证次数")]
        [Column("times")]
        public virtual Int32 times
        {
            get
            {
                return _times;
            }
            set
            {
                if ((_times != value))
                {
                    SendPropertyChanging("times",_times,value);
                    _times = value;
                    SendPropertyChanged("times");
                }
            }
        }
        String _ip;
        /// <summary>IP</summary>
        [MaxLength(30)]
        [Display(Name = "IP")]
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
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<fa_sms, bool>> exp)
        {
            base.SetValue<fa_sms>(exp);
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
            modelBuilder.Entity<fa_case_bid>().HasKey(m => m.id);
            modelBuilder.Entity<fa_sms>().HasKey(m => m.id);
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
        System.Linq.IQueryable<fa_case_bid> _fa_case_bid;
        public virtual System.Linq.IQueryable<fa_case_bid> fa_case_bid
        {
            get
            {
                if (_fa_case_bid == null)
                {
                    _fa_case_bid = this.Set<fa_case_bid>();
                }
                return _fa_case_bid;
            }
        }
        System.Linq.IQueryable<fa_sms> _fa_sms;
        public virtual System.Linq.IQueryable<fa_sms> fa_sms
        {
            get
            {
                if (_fa_sms == null)
                {
                    _fa_sms = this.Set<fa_sms>();
                }
                return _fa_sms;
            }
        }
        protected override string GetDesignString()
        {
            var result = new StringBuilder();
            result.Append("\r\n");
            result.Append("H4sIAAAAAAAAA+1dW1PbyJ7/KhTPZGPZ2NhTxcM5yT6kdnf21Obs1lYtU5SwBWhjyxxfIKlUqszMEEwSApk4mVzIBSaTsLMZYCY53Alfxi2bp/kKp3XBSN2yZbXVcrfhCSy1Zemnf//vl9v9fxXH0lK+/6v/uW38+7WYkfq/6v+3W9f/lu4f6P+P7Ixx8lpByhj/mSvk");
            result.Append("FDz9X2K6CD9EQ3cGGicKt6aks1P9V3KSWJD0a/8pWZCzSr9lbTKrFCSlYFl+e8S4j5H+r+C/cgr+HYoOjPQnxSnty/DjSD/4ch/sltXVcvVgG8x9qs9W6qvrI/1wlXZZfcm4OJoU89JoWpy5JeX0c1fFgjgGj127ClcIcXhE/tds8gb8AO9+pP9KNl3MKPkR7RlPfzgR");
            result.Append("t/2yUkynrT+iLYKfr+X/VCxkrynJnJSBDwNPFXJFSbukqHwNvwIPjIvpvHYkNfZXiI7xZW0lPGQeMK/9z0oxc1UalxXLsbSkTBQm9S/p30hJ42IxXdARO1ulo6Y/m47Xtfxf/kX/ZN5LNpeScvpThfSzV+V8MidnZEUsZHOWO7ySFvN58wm1K8OX1UAj4fgeTBQaqBiQ");
            result.Append("jzYFp/FLDXTMW+wCOKf3coaOQIhOPBRC0DHoE0VHp0pOsQkTYyMg2NTubaul2b5LffXjl/XVB9XdX/vAzu9g6am6Xdb+qx48Uz9+0Y9tPFDLy3YM8wWxUMxzCWGEFEKMCZ58W6rtvwcrW+BVyQ6PrEzLECG4cFRMpXId4DQt5pKTYs47VuFo1Be4BonhijnCpf64ffLj");
            result.Append("56ZwFWTtGDFcKSjoGpcInraixGANOQvYZ5tg+T1YfqB+v2TAhwCXHxUnclIniI3J3dqIMW9gfaMtv/rfV7LKuDyhKwnGEV17sGo0KVPLsKpHQhx+X1OlrkM6k3R9w02hEqgqVOjmMESVwXSbqlJT4i1CPSoeCnOpR8Wo6FHxUASFv7IJsa9vrIHFJ2Bpxw6//oOdaQzE");
            result.Append("bDwaIgDKT51qEOXi849O1l7ZARIz2aJJA4RsW0rKGTHtHR5I/GFfACIWcyFUK6h9/FjdXVCf7rWkqcKknEuN9gBlkWukKAvEbUpHzZ1x48YFrjgxXKiG0FDgwZe5M2Xd+I8fZd0FrgQpXALKt2qVN9CKcdI+k7o0Z13zdGPwxLJQQBmYQUJOSEH9g3eYiAXhEOp6MWF6");
            result.Append("ugcOl3CYIE9WFCnNMWMntmXiqBvGQKq6v4/y82KeX0bu0XqxwIN6YtTKNoSHS0+eC0ZDpBglUN2gevhM/e4tWNxBAdJdB9JoMlucyiq84uTRJRW0JRymagmjeo1pCR/9BC0NZzM4mc1k5Hxe/8IAmTXMZ1RhyEpGPlrDmGjTwa8dPK9vzXLEsW3w+GgDCz0QV3DBhtiK");
            result.Append("E1Bpxqt/wAUg4qiBEB5w0oYMt4AjCWnqI6dkROxFEVCHHO/mmgtQxMq1gDmOjRDBzu+1gyVQ/qF+9BGUt6pHz7FNWMiOiWlRSbaPmoNw8y1aMNJvWCntQ8Z4uCBCVUmKY9tjXS3vnDzerq8+AIurmJ40Jio3oLKUS5FqSIJdLfNVQxqTJwhYlPnZdefF6ShJgvMr4Mqk");
            result.Append("tUHjp4KEKpAmYS4v1j5s2fHRCVMRO+LdxJ4RIdSma8QFKGJtKYxqkuCwBIkIYuUo48RkUlOc9N3MM17EylMY1S4hr8NCKfAZxqSu5Fi062lzgYdYawqjuiX48BhuOgSeLm02n8DxqCkFLfYHaYp9zJOqLi3XHm455QfMyIXJVE6cIZX3YbteyY+8j4foyPswFm/jTt7b");
            result.Append("ofFR3oeddaHabwfg9f2+S30aKH2WvF87WgX9QZllSG6okQt/TEvi01XiBhCxOIug7M4a4gYbq+rCHpKZCg3fu5+4CXa7AUccLolgTjg29W+fdiBxDkUE05eYVr99gos4hyKCOuU4VC9dZSCxghBBM0ycbBPNBTPKvIHiChKxphDBCofKL8HBPrd+XVekiLWDCBby5jgN");
            result.Append("xxUmYo9ADPN/L9yvHfxa/7yHmEQdJcV3X4kiDhDEsEqCjdcnz+dAeav++b2jxpnLTctierQHNE/inBzMv24Y2g2DxoinGAf7qsev6puzGpSlEpZreWqH827lMJ6SEqXqdsG8jkuPaj+vV49X1dlNzPMiK9NZOSnBv+NZUu9L1K5pceR9Eaxk5J/3JYoqV43KGN5C5naE");
            result.Append("fHTCRDHVSqdS9d5H8O5zg3OBxSfVo8W+6u4v1f19JCAsF9JSp3yqe8ARqxBRrPBAB64Bmfp846T0wjjYB9Zeg9Jhbf2h8RlBkFfsiF00UUy7sBCdA3l1QwK2W/nqBhKxChbF3KMa+aAFQOJNtgt/3OAhVraiqB+0Vvl7ffMVRKj2Fkk3nJrMKhIH9rMLVMSOvRgWL9bZ");
            result.Append("uVPJeTKbmRKVW8zXm7thRezci2Fam+7cM1yhXPv3XBAj9u/FUHeogRW0EzFu1cgn4n8zknv8Yqg/tD5bqX06wBQrzcgQkwXm3exuQBGrpjFUe1cr21pRpwPbSklpeVrK6WxLyncSt+k65yL3/cVQVb6+Oavhhe5CuRe2H3n/FYyqTrMhUJuw4YTh1Cb0SEhBO19iVJ0v");
            result.Append("WIxOi/2uqSuLWgT47a5Re4F5YXQ3gHRTzJC6YGIUS4LoumDCVmLyzwUTQ9V0/qpe7ND46HsZwvRygzYdgjUaTbIerXGBiViuDWEq+YeK+pnLUlc3jMilGsrvzvxQT7eMLdenvvysLq4ZsPHigXLBy6MHKmgRN0RVxKF2hBFIckrr1BzbY2LyBqlUi1Ns+0RXqkXoSDUM");
            result.Append("ew6lmg0aH6Va3DnogmJj6UbGK0TEEi2O9YDUty54+Aa8fGuHCbKJvFmhzqpB64ISsUyLY60fdZT4zfpxAYo4nBJHwykmOenaJOZXapg3jFr+LiiRN5RB4ylWlHjVuV3AIm8vg7WI1jN/nGASczl5WkxzjpTH4ErQmmScqiaJdWf8tlTf3Gl0y3FKVjlrl0OqVCbs1i9H");
            result.Append("SuUgHaUygTXXbthw956pCz/1qQerYB7pEsau6WZDyUflKYGaugY6iNoEt0SXkhAHfYGHWGtKYKYJpJp3L8DWo/p3R7UKahzqCHCME7HSlMAS7+eXDJxQBTxV9NDtgkWMiFWmBGqnWNuEVI+OMXKS86PFvNSJNUevkbgbSsS6UqIHajxdsPHoDAhaO0q00I4mRWXCk3b0");
            result.Append("7+lUY4DOqcJjeJUbublfSzNuSxRpxkHpEUJ4I6fTPHA7tWTEHFRhWGI7eK+duFvvT1LmDGHCOjnpRZx2jIIo0hzpv3wZrKzX9o9HRpSskoZH+4b74H4YUS5frr0/ME+Mj5tnjBIEz/vPDUmC/gVJnfJTFir8Rr+TtFTAjga4WWOtRkz5sll1t2I+W8yZYttpt6Jr7NuV");
            result.Append("ZfhaDZTwD76kRlGt0Ttb4id4tzU6/09F/pu+Ucwtey1/JV3MF6ScplqcUb1+He2+9G+eFpF+c8YiGlzFcsnTb7d5zSlt//l7yRlJnpjEL0qXbFp1X/WFbGytQ5yIxragHZJhuK6qe9X93uxCeOjPYvJGccp4x6m/5LJTUq4g66R423J32kvNaU4TMX0m7zWP7R3rgzuu");
            result.Append("ajiE79y5wwaLbNVEzz8W2fCjN+WQjRX8SJdWjYh8gQ53HTrBh6/iB8JWRYV+QWgvFWyCoH0RPwC2SgxkVFSxAVyrdBP/+N7p+LCmbO90AT/AtYqu+AectZVVM+wsa/iBj7r7pXOL7syzgBf4LD0Cu99xNlck7m+jcYapa6jn/QUdGZMNIg3UnBwKxgvRqxxziLo13tvw");
            result.Append("Ue2SjoXhjYxpsLRZPfhZvbcGHv9S3X/ZqoDEzEGfys4QT6yHcorXnNsEnWYeEBGskRyHFQAJSo08IDxYYRt3Oclu4LBd0TZE3Xniqx6MxfZ1cjGbpZqtUt/uCtq/kOvBs+E+PfD/CGy8MHZexIGy2GmcOtIvoPFKt/FFBPR1vtQ+6r4tXykc69DDdaqvG/GSZoxBoPBm");
            result.Append("fxpQeAmZYKshCzthyEhchWDzE9SSna/NT90v6+vmx2KG93+oHbxyLrH5XylZGOWg0sZnFnD+SDgYD/mFL8nTSwnG+37xUjy9lGB8+r3qoYoH47TuvVBcnLpjuecTEOLUncs9noAQp54/1KMJCHFOsocaVoIQwmqC1+6Cpafg0bq68sZuJUxIhQ4dBNQrp12KOEj7h0KY");
            result.Append("8NHuWu2PurKgPiljSEGNhXOkSLuHCgI25P2k9C1ECjy5Cx7PGg4VhKyy2VSe+Tb/Lnh57B3KMgvjycMpCFiocrkMlh+oT+erB9v1nU3w5XvERQ/vMqc1FZ0SNQrg2NVBMMf7XBlwcartFfEiY90/XD+uVI+QSmyNnLOZjJzPy8S9AiChcxsNp9OBCiKCvYKFEvjtNX+1");
            result.Append("pwlKjaggRFhE/NXP6v4yh3FxSo2oIERoFNho1uWQDan16eKZjIjDY4KADQvTCchAymkWlk5KXR2E1V7fADfIiMt3BQGNKJqDdzbWwOITJ8rilKhIOytAhLD4lKW1Qq3yBhpWfxxqSWdGWdsfhwt20OT86LSY7gg231ot4JXhbriR9lqAuOFjP96o5WVum8K5IUVutIex");
            result.Append("Ocg6VTkhpVMS50B5tNmDtivpRx7tSrajWWlf0syqDKNWpY01fd6sfdgE5S2j6BZjSoXsmJgWlY465vjImDzyJYIucKw6MuhHVTskuNZF371Fhl7lozc6HHCp8kZux7GKm53y7Tj90DNlyg1ct3N0anCi3LkR79lTOtCttmkh3Q50jWuwhby/bKMd5JngGAn62RZUOAYb");
            result.Append("4NEv5eth8IIp5PMpXBRu7URnu3DqIvm1U1oNpi3ORZ6lp5cSTM1YR3mWFgaCVWD2Srx5KHpeHAMJDjIcvMB3kQ0BZV/H0wt4ZJ3B1CMFlqJ+wWZZn8KRCKZ8yC9hjTW4Z52KhFC4vcD1OZLW1N34fiTzW4gOzScB5ZfgYL95eLbDoGP3pisL5NkRYTShpHq8oVb2nEAq");
            result.Append("TqW4Bql3+pYngqmIIyzpshAXmnqjVra1qXlLS/XjLZThZwwqYlhhcGuM3zP0JYSoe7F7tOxICAVRNOinkGw9JD6YsRZg53dwWAJzO8P6MAt15RfzY1sTLLDMCldJEGjCPmV6u6iw7BRB6r5o28hrJ/RsC5ruVCzbsGtjaOB+NVqYDF/SN6w53XX3V2P/wtMnpRf14/n2");
            result.Append("9i+uyfmfXscs8XFXphpBUznN0ZzlXbC8WPuwBd9/H1h+CJa26g93wNJTaEmob/fUxQ3t6dT7h+rDn/vM6ZGXnaYlFuRCuhMDowOL358CRIL5ZMxSZxDzA/ylTszvTtfmp91ozY3WyAuSIvjwN6qGf7eRCrSfIuVtGUy0oWPzP4LFDjZenzyfa16spOlBHNQqnSdHAEe1");
            result.Append("AhHUldk9BdkoBDodzthI+AT33jYSPrX5jGcn1R/enVRK8NglgUhl9rnIiTDofZFrTOiEd0kUrm+sXiQKdyfOKoSYL16xcECs1PXoJ03OOohaj2LWgWS6XxPsi5wNrJzTCUKe6jkHfC6OwLoTrX2v8blT6eBTbSh7qHvzXvmBOiOqJPe1VOdIwcR7lrNWJqizdFZoW2C+");
            result.Append("6sdCxZiWrpOGo5jT1Mo058X4gse+Riyb40Iws8469/ygqhR/fY7cnDxeXa9Mk1UwpWO+tog+JbZ4CButok8EMSgOzH2qz1Yc6c57iZmDLtlF+vPG1nzV3uNxLAjYrFWX1zZdTGEcvKpugRjzCem5c37NmWMKZ289W33FmZqsYgpgj8LKD4QZkWzBFJp2rDANogy1pR/k");
            result.Append("NDeScc3clSrJW9YNtuUxN5ilk9+8kB1l22vuil0P5TAKwRQed75J0ZQJsPcJLG2p945MPzvWfNNukfMazxZ6p4ZJEKinNGkab1M6a5xsSmNYK+XKJlSuwebd2lvE06hlS0zNdGI90+7Vnwi3Jiy+Gqcr+q2gdcUDHV1Xyohy2u+LZrJjctrhVinvLOpZSYaV0zIPHVnS");
            result.Append("dJdh3bg53mVxfz0FbLNv6vlIY6JyA6oDuVRTGrOvaEpiaPVpff//qwdH9c1ZsLSDJHGkRiEhjJmSgU0iG4r7ysqtqjzmv1vZAg/2kXSFnCRyjA555+RBbB4yROdVCUEnlcpJ+U6CjtSz5t0QIh9zNIjGs2qVv9c30czSyazSiaXcbQLyONXICo/dvWX+cgMYUwHhFpie");
            result.Append("Em4tUs2u6nfnRbg527ioSUrvYThN92CDFMLMJxQwjV6rMHnnA6fsCgvKUa9rwxmVCR0ykvFS0dYV2QyPl7LZIT6Ol4q2FmE3pFsMC7CEm3VGDEss1BKWaeOOggcm0qZK6IYMQcVWoEyGbtKElY04Mee85TxHnDmYeFyvznIXwkyHSlwydRJYrymjdHwRmxFoK/jtLEOM");
            result.Append("TOT5FOH01gd6wC0p9vSpHKdm6E/JTOA9TD3OoqPclErPzjZz0MWxedulkpZgo/eycIq6SzfFDOMR9wSF5qx+RVxMMNqMYej3PJpXgo5ihKlHMfzpBoPxUpN6F59g7uWcNF5UUqNKlmEt2a3lSw9V74eD6dzq05yGBOZ/NrJrH8yrn7+ojxerRyt2atNElljoLOWhrRIY");
            result.Append("9e187dcv4MHeydyi2Tds4T64tw7Kd7UjhK2HGGagBCHr4DthC2GO6mUTqApgYqY+26welKp75dpvB+D1fYeM5oJOmcyyU5+HNDPNTpnuhcoychGmm3xadimaoM13JD0ear03e6mwKMJHvVo4hKZfG+nEmhzQk4eN9CCE1vKj47ls5s8sz/Z0zezsoVaKEepFbJ1Ybkwj");
            result.Append("dzGNi0FFOkLd1etbPmUkhtVQGeUYK+u1/WOMbWaVtNxRNg5FjumWTBntoVz4SDDjt/yZIROJY5nxBo0tbRquL6T0QlRGDccXG3SGttM4P2NjIHdgn8yYxo+DUU9M40fdI+WLnRuJ24sS0HyO06AWqw0z3OzaQGfiUqYo6j4nf/pis4zhYAvvkw/Jrjg0FB+FaS8Hi0kZ");
            result.Append("5yMj4zT1hJmkjEH6fX504si3CMycnW8mBxOYoq+PiQDlXad5ESn4KpnvGJ5ItKZKrwPjByxoYaWMOkhG5OorYdgYtTEQHm4FXqfxLHr72RU5j6MPmRaJ9P1wxNuztRThatOGBsLd2LRusqRxi46yxGA17EgS+nmotEg1HEI5ZmOCkck0Q8NGkkt9450WZCzvDkA2+nzj");
            result.Append("pPSisRLy09qHTVC+C+YeqYtr8Ajq5cxPZmfaJm+nLsXZ4pg5CImC+8mNtoMjbUOe8ZXcOkjfcXrOqd83haLbpN+arE83OTuUTd9XS42yBUwF+XFbfVKGOnDt4BXY3DM+6hNInoLSoRbW01Kx/zhc0Oj55a51SX3+l5O1u9rC5Yc3b94E7/5PXVnQTyEzS6aNlvRe1Gfm");
            result.Append("mL3H2VjBcfsBy8tF2Zb1hWmv58mW04tJiZ3UBvr7Wry+lQsZ3JJTBTCZ6oJTsSSYfWZTbUvmbnMhHyHvLgviTReiHjckK2Fz4S5CDOUu9Y01sPjEVO+1m8uJycIwWC6D5Qfq0/nq3vuBsWIeUlI+P1w9nK3uvlBXFsG91QGN8UzLU8O1yrpa3oH/DUxMZvOFmZxckIar");
            result.Append("Bz+Bu8/N7xuBYW2tYTxoa8eKtyAKuWIe/taHirrwTL23pn5a16+ak7Qc2wnJvDSYm4N8zrxK45xxKeOcfVt1ysV8zvHFN5ZbGUlgoh2yWW9+Ix6L9gapx2MvdipvOzXC307VmAvfOxV+NG9Z36tnP3u6Y7WGONprhJ+v34K3mfmna0ohNqj/km21uYvx9dcLOVmZwL9w");
            result.Append("tpXb/47toZre2jfaY2lnr0sF84vjUiwpCnHpUiyUlC4NDqaGLiViknhpPBGRoqI4FgpJif47/wAGXvrjCVEBAA==");
            return result.ToString();
        }
    }
}

/*<design>
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
<design>*/

