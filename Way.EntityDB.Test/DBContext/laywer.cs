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
        已完成=40,
        已取消=-1
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
        System.Nullable<Int64> _invite_coupon_id;
        /// <summary>优惠卷id</summary>
        [Display(Name = "优惠卷id")]
        [Column("invite_coupon_id")]
        public virtual System.Nullable<Int64> invite_coupon_id
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
    /// <summary>邀请优惠卷</summary>
    [TableConfig]
    [Table("fa_invite_coupon")]
    [Way.EntityDB.DataItemJsonConverter]
    public class fa_invite_coupon :Way.EntityDB.DataItem
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
        fa_invite_coupon_typeEnum? _type;
        /// <summary>类型</summary>
        [Display(Name = "类型")]
        [Column("type")]
        public virtual fa_invite_coupon_typeEnum? type
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
        System.Nullable<Decimal> _rate;
        /// <summary>折扣</summary>
        [Display(Name = "折扣")]
        [Column("rate")]
        public virtual System.Nullable<Decimal> rate
        {
            get
            {
                return _rate;
            }
            set
            {
                if ((_rate != value))
                {
                    SendPropertyChanging("rate",_rate,value);
                    _rate = value;
                    SendPropertyChanged("rate");
                }
            }
        }
        System.Nullable<Decimal> _full;
        /// <summary>满多少能用</summary>
        [Display(Name = "满多少能用")]
        [Column("full")]
        public virtual System.Nullable<Decimal> full
        {
            get
            {
                return _full;
            }
            set
            {
                if ((_full != value))
                {
                    SendPropertyChanging("full",_full,value);
                    _full = value;
                    SendPropertyChanged("full");
                }
            }
        }
        System.Nullable<Decimal> _reduce;
        /// <summary>减多少</summary>
        [Display(Name = "减多少")]
        [Column("reduce")]
        public virtual System.Nullable<Decimal> reduce
        {
            get
            {
                return _reduce;
            }
            set
            {
                if ((_reduce != value))
                {
                    SendPropertyChanging("reduce",_reduce,value);
                    _reduce = value;
                    SendPropertyChanged("reduce");
                }
            }
        }
        System.Nullable<Boolean> _is_used;
        /// <summary>是否已经使用</summary>
        [Display(Name = "是否已经使用")]
        [Column("is_used")]
        public virtual System.Nullable<Boolean> is_used
        {
            get
            {
                return _is_used;
            }
            set
            {
                if ((_is_used != value))
                {
                    SendPropertyChanging("is_used",_is_used,value);
                    _is_used = value;
                    SendPropertyChanged("is_used");
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
        /// <summary>使用有效期</summary>
        [MaxLength(50)]
        [Display(Name = "使用有效期")]
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
        String _goods_type;
        /// <summary>适用商品类型</summary>
        [MaxLength(50)]
        [Display(Name = "适用商品类型")]
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
        String _title;
        /// <summary>
        /// 优惠券名称
        /// 后台获取时根据type拼接  满减/折扣
        /// </summary>
        [MaxLength(100)]
        [Display(Name = "优惠券名称  后台获取时根据type拼接  满减/折扣")]
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
        System.Nullable<DateTime> _createtime;
        /// <summary>创建时间</summary>
        [Display(Name = "创建时间")]
        [Column("createtime")]
        public virtual System.Nullable<DateTime> createtime
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
        System.Nullable<DateTime> _updatetime;
        /// <summary>修改时间</summary>
        [Display(Name = "修改时间")]
        [Column("updatetime")]
        public virtual System.Nullable<DateTime> updatetime
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
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<fa_invite_coupon, bool>> exp)
        {
            base.SetValue<fa_invite_coupon>(exp);
        }
    }
    public enum fa_invite_coupon_typeEnum:int
    {
        折扣=1,
        满减=2
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
            modelBuilder.Entity<fa_invite_coupon>().HasKey(m => m.id);
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
        System.Linq.IQueryable<fa_invite_coupon> _fa_invite_coupon;
        /// <summary>邀请优惠卷</summary>
        public virtual System.Linq.IQueryable<fa_invite_coupon> fa_invite_coupon
        {
            get
            {
                if (_fa_invite_coupon == null)
                {
                    _fa_invite_coupon = this.Set<fa_invite_coupon>();
                }
                return _fa_invite_coupon;
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
        protected override string GetDesignString()
        {
            var result = new StringBuilder();
            result.Append("\r\n");
            result.Append("H4sIAAAAAAAAA+1dW3PTWLb+Kyk/h4Nlx47cVf0wA/NAzZw+U4eZU6dq0uVSbCXoYMtpWyZNUVQ53Q2YS3BoDA1NgCR9gepDJ+mGyZWQP2PJztP8hdnaUhxpb9myti7e2+QpsSTb0ue11/rWdV+L/U2YLoiV2Cf/uGb8+5lQFGOfxP7z6sUvCrHx2H+X5o2TFxSxaPxn");
            result.Append("XiHlwen/EQpV8CIVvz7ePaFcnRNPTsXOlUVBEeFn/yGnSCU5Zrk2V5IVUVYsl1+bMu5jKvYJ+FfKg7+TqfGpWE6Y098MXk7F1A931Z26tlpv7W+pN952Fpqd1ddTMXCV/rHwkhkhmxMqYrYgzF8Vy/DceUERpsGxC+fBFRwPjkh/KeUugxfg7qdi50qFalGuTOnPePzF");
            result.Append("Gd72zXK1ULB+iX4ReH2h8oeqUrog58piETwMOKWUq6L+kYL8GXgLODAjFCr6kfz03wA6xpv1K8Eh84D52X+Sq8Xz4owkW44VRHlWuQTfBN+RF2eEakGBiJ1cBVGDzwbxulD565/hK/NeSuW8WIZPFYdnz0uVXFkqSrKglMqWOzxXECoV8wn1TwY/VheNjOPvYKLQRcWA");
            result.Append("PNsTnO43ddExb3EI4Bzfywk6HCE6fDyOoGPIJ4oOlEpGsUkQY8Mh2LTvbGm1hbEzY53DZ53Ve62dX8fU7d/VxmNtq67/19p/or35AI+t39PqS3YMK4qgVCtMQpgkhRBTgkdf1dp7P6vLm+rzmh0eSb4iAYTAhVkhny/7wOmKUM5dEsresUqkUoHANUEMV9oRLu27raPv");
            result.Append("3vWES5H0Y8Rw5YGh635E9LKVIgZr0tnAPtlQl35Wl+5p3zQM+BDgKllhtiz6QWxaGtZCTHsD63P98vP/e64kz0izkCQYRyB7sDKavMkyrPSI48H7dSp1EciZCPmGG6HiQiVU6OIwTJWhdHtSqTnhKiGP4uMJJnlUOhQexceTKPzNDYB9Z31NXXykNrbt8MMv9McYiNV4");
            result.Append("Kk4AVJCcagLV4rceHK09twMkFEtVUwYI1baYk4pCwTs8QPgTgQBEbObiKCtov3nT2rmtPd7tK1PKJamcz46AZJEzUlQF4j6lI3On3LlxgYsnhgtlCF0Cr364cULWjf/YIesucGVI4eJQvdVuvgRejBP7zEFrTjvzdFPwxLaQQxWYIUJOSAH+wTpMxIZwEg29mDA93lXf");
            result.Append("N3CYgE6WZbHAsGIn9mV4NAxjINXa20P1ebXCriL36L1Y4EEjMVpzC8DDZCTPBaNJUowyKDdovX+ifb2iLm6jAMHQgZjNlapzJZlVnDyGpKL2hBOhesIorzE94YMfgKfh7AbnSsWiVKnAN4yTecNsZhUmrWIUoDeMmTYIfnv/aWdzgSGNbYMnQB+YG4G8ggs2xF4ch1oz");
            result.Append("VuMDLgARZw24xLgTGzLCAo4ipNNHRsWIOIrCoQE51t01F6CIyTWHBY6NFMH27+39hlr/tnPwRq1vtg6eYotQKU0LBUHODY6ag3ELLFswFTO8lMEhozxdkAyVJPHY8nit1bePHm51Vu+pi6sYT5oW5MuALJXzpAyJs9OyQBnStDRLoKLM164rjw+HJHHOPwFTLq0NmiAJ");
            result.Append("EkogTcFcWmy/2rTjAwVTFnzpbuLICBcfMDTiAhQxW0qgTFJ9XwNCBLBytHFCLqcTJ7iaWcaLmDwlUHYJdB2WSgHPMC0OpcZi0EibCzzErCmBckv11UOw6BB4hrTYAgLHI1OK2uxPhGn2sUiq1lhq3990qg+Yl5RL+bIwT2rvE3ZeyY695+Ph2PsElm9jzt7boQnQ3iec");
            result.Append("uVD7t331xd2xM2M6KGOWul87Wgp8UGoVkhtq5MYfY0lshkrcACI2Z0lU3VlT3Or6qnZ7F6lMBY7vzbfMJLvdgCNOlySxIByd/DugFUhcQ5HE+BLV9DsguIhrKJJoUI5BeulqA4kJQhKtMHHyTfQQTJZ6B8UVJGKmkMQah+rP1P09ZuO6rkgRs4MklvJmuAzHFSbiiEAa");
            result.Append("i3/fvtve/7XzbhdxiXwVxQ+fRBEnCNJYJ8H6i6OnN9T6Zufdz46Ms1y+IgmF7AgwT+KaHCy+bjjaXYfGyKcYB8dah887Gws6lLUaVmt57Iez7uVQXpKSCjXsgkUdGw/aP71uHa5qCxtY5EWSr5SknAj+zpRIoy8pO9NiKPrCWcUouOhLCiVX3c4Y1lLmdoQCDMKkMGoF");
            result.Append("pVS780b98V1Xc6mLj1oHi2OtnV9ae3tIQlhSCqJfPTU84IgpRAprPIDAdSHTnq4f1b43Do6pay/U2vv26/vGawRBVrEjDtGkMHZhEToH8RqGBRy089UNJGIKlsLCo7r4oA1Awpd0N/64wUNMtlJoHLTd/Gdn4zlAqL2ClBvOXSrJIgP+swtUxIG9NJYvhurcqeU8VyrO");
            result.Append("CfJV6vvN3bAiDu6lMdYGg3tGKJTp+J4LYsTxvTQaDjWwAn4ipq269UTsL0byiF8ajYd2Fprtt/sYsdKdDCGnUB9mdwOKmJqmUfauNbf0pk4HtZUXC9IVsQzVlljxk7cZuuYij/2lUSrf2VjQ8UJXoTQKy498/gomVcfVEKhP2A3CMOoTehSkqIMv6VCDL1iOTs/9rmnL");
            result.Append("i3oGeGXH6L3AojAwDCB+KRRJQzDpEFuCwg3BJKzCFFwIJo3SdPa6XuzQBBh7mcR4uSGbDskaXSZpz9a4wERs1yYxSv6qqb1jstXVDSNyq4bqu5M41ONNY8mNac/eaYtrBmysRKBc8PIYgYraxE2GauJQP8JIJDmVdeqB7Wkhd5nUqvEhjn0K16olw7FqGPYMWjUbNAFa");
            result.Append("Nd456YJiY5lGxipExBaNx2ZAwqWr3n+pPluxwwTURMXsUKfVoXVBidim8djoR4gSu1U/LkARp1N4NJ1iihNkk1hcqeveUOr5u6BEPlAGzadYUWKVc7uART5eBhsRDSt/nGASymXpilBgHCmPyZWomSQfKpPEpjN+VetsbHen5TgVq5yMyyEllRm798sQqZwIh1RmsOHa");
            result.Append("XR/uzhPt9g9j2v6qeguZEkav62ZDKUDylEFdXQMdhDaBJTGkIsSJQOAhZk0ZzDUBUvPj9+rmg87XB+0m6hxCBBjGiZg0ZbDC+1sNAyeUgOerHqZd0IgRMWXKoH6KdUxI6+AQEyepkq1WRD/eXHiDxN1QIuZKmRHo8XTBxmMwIGp2lOnDji4J8qwndvRfhXx3A51jwmNE");
            result.Append("lbu1uZ+J826XyOK8A+nh4vggp+M6cLu0FIUyoDA0qR181g7vNvuTVDkDmLBJTrCJ045RFE2aU7GzZ9Xl1+29w6kpuSQXwNGxT8fAepiSz55t/7xvnpiZMc8YLQie158bkgTzC3JQ8vMWKfwc3klBVLCjES7WdL8tpgJZrDCsWClVy6bZdlqt6DX25UozfP02lAgOvpwu");
            result.Append("Uf3RO7kkSPCu6XL+d1n6Ai4Uc8leqJwrVCuKWNapxYnUw8/R7wu+87iJ9PMTFdHVKpaPPH73gJ85p6+/YD9yXpRmL+EfGq7Y9Ju+GojY2EaHOAmN7YJBRIbivqrhdfd78wvBoT8KucvVOeM3zv+1XJoTy4oERfGa5e70H7WsB02Ewom91yO2160P7nhVNyB8/fp1OlRk");
            result.Append("vyF6wanIbhy9p4bsXsGOdek3iCgQ6PDQoRN8+FXsQNivqTAoCO2tgj0QtF/EDoD9CgMpNVV0ANev3CQ4vXe8fVhPtXd8ATvA9cuuBAecdZRVL+ws17ADX+jhF/8e3UlkAW/waTxQd75mbF8RPthB4xRL1+TIxwt8OZNdIY3UnZyMJgoxqhpzMnRvfLThC3VKOpaGNyqm");
            result.Append("1cZGa/8n7c6a+vCX1t6zfg0kZg36XGmeeMd6YKdYrbnNhDPMAyCCDZJjsAMgE9IgDwAP1tjGXE2yGzh0d7RNhh48CZQHY7l9KC7msFRzVOrKDqf/C7QeOJsYg4n/B+r698bKSzpIFj2DU6diHJqvdNu+iEC+Pi7aF3psK1AJxyb0MF3q6ya8pBVjACh82J8OFN5Cxtl6");
            result.Append("yBJOGFKSVyFY/AS9ZB/X4g89Lhvo4sdyhne/be8/d26x+T8xp2QZ6LQJWAV8fCIcTYT8NJbk6UeJJvp++qN4+lGiiemPaoSKjyZoPXqpOD70wPLIFyDwoQeXR7wAgQ+9fmhECxB4RqqHul4CF8d6gtduqo3H6oPX2vJLu5cwKyo+AwShd067NHGQzg8FMOFbu+u9P9ry");
            result.Append("be1RHUMKMBbGkSKdHspx2CbvR7WvAFLqo5vqwwUjoIKIVamUr1A/5t8FL4+zQ2lWYSxFODkOS1Uu1dWle9rjW639rc72hvrhGyRED+6yrA8VnRN0CWA41EGwj/dH5cDxoY5XxJuMYXy4c9hsHSCd2Lo4l4pFqVKRiGcFAEFnNhsezgQqgAj2E9yuqb+9YK/3NBPSICoA");
            result.Append("EZYRf/6TtrfEYF48pEFUACI0C2wM63KohtTndLEsRsTpMY7DNguDAmQg5bQXFhSloW6ENdjcADfIiNt3OQ7NKJob76yvqYuPnCSLUaEinawAEMLyU5bRCu3mS+BY/eu9XnRmtLX96/1tO2hSJXtFKPiCLbBRC3hnuBtupLMWAG74th8vtfoSs0Ph3JAid9oT2D7IUKqc");
            result.Append("kIKSxDhQHn32qP3K8DOPdpLt6FbaL+nlVSZQr9Kmmt5ttF9tqPVNo+kWU0pKaVooCLKviTkBKiaPeolgChytgYzws6o+Ba5/0/doiaFX++hNDsdduryR23Hs4qanfZsPP/UcsuRGzu0cgxqMkDs34T15Sge51RctkNvxoWkNupAPVm0MgjwVGiMTfrVFKBqDDvDCb+Ub");
            result.Append("YfCiaeQLKF2U6B9Ep7tx6rT41a+sRjMW57TO0tOPEk3PmK86S4sCwTowRyXfPJn6WAIDGQYqHLzAd1oNAWyf790LWFSd0fQjRVaifqpmad+FIxNN+1BQxhobcE+7FHHxxGCJ64/IWocexg+imN8idGg9iVp/pu7v9U7P+kw6Dm93ZY68OiKBFpS0Dte15q4TSNW5PNMg");
            result.Append("jc7c8kw0HXGELV0W4UJLb7Tmlr5rXqPROdxEFX7RkCKKCYPbYPyRkS8uHnoUe0Tbjrh4FE2DQRrJ/pvER7Othbr9u/q+pt7Y/hRuZqEt/2K+HGgHC6yywtUSRFqwH7K8nXZY+kUw9Fi0bctrJ/RsF/RcqVi14dC2oQHr1Rhh8ukZuGDN3V13fjXWLzh9VPu+c3hrsPWL");
            result.Append("M7ngy+uoFT7m2lSTaCmnuTVnfUddWmy/2gS//5i6dF9tbHbub6uNx8CT0FZ2tcV1/em0u++1+z+NmbtHnnXaLVGRlIIfB8OHxx9MAyLB/mTUSmcU+wcEK51Y3D1cnz/sQWtuskbekJTEN38L1fEfNlKRzlMMeVlGk23w7f4nsdzB+oujpzd6NyvpPIiBXqWPKRDAUK9A");
            result.Append("Eg1lDo8gG41Ax5szdgs+1Tsr3YJPfX/Gk5Patz8eNWvg2BmOiDIH3OREmPQ+rTUmDMK7FAp31ldPC4WHk2fl4tQ3r1g0INbqevCDbmcdTK1HM+sgMsPvCQ7EzkbWzukEIUv9nOMBN0dg04nWvtH13LF1CKg3lD7UvUWvgkCdEirJfC/VR0Qw8ZnltLUJQpVOi2xz1Hf9");
            result.Append("WKQYY+lQNBzNnE4rC4w343Me5xrR7I5z0ex15j/yg1Ip9uYcuQV5vIZeqRaraFrHAh0RfSxsfBzbWgXuCGJInHrjbWeh6Sh33lvMHLjkEOXPm1oLlL3zPJYE7DWqy+uYLqowjp6qWyDGYkKwdi6ofeaowtnbzNZAcQ7NVlEFsEdjFQTClFi2aBpNfROmCVSh9o2DHNdG");
            result.Append("Us7MXaWSfGTdxEARc0NZOsXNlVKW7qi5K3YjVMPIRdN47H+RoiUT6u5btbGp3Tkw4+zY8E27R85qPpsbnR4mjgu9pElnvD3lrHuyp4xho5SbG4Bcqxs32ytIpFGvlpib9+M9hz2rP5PoL1hsDU6X4a2gfcXjvj5XLApSIegPLZampYLDrYa8skKvSjK8nL516MglPVcZ");
            result.Append("No2b4VXGBxspoFt9h16PNC3IlwEdKOd7ypj9ip4ihnafdvb+v7V/0NlYUBvbSBFHPgsEYdq0DHQK2SQfqCq3Unksfre8qd7bQ8oVyqLAMDrkk5MnsP2QATrPawg6+XxZrPhJOoZeNe+GEPk2RxNoPqvd/GdnA60svVSS/XjKwxYgj7saWeGxh7fMb+4CYxIQZoEZKePW");
            result.Append("p9TsPLw7L8bN2cdFXdLwHobRcg86RCFBfUEB1ej1S5P733DKTlhQjXpR35xRnoWQkWwvlerfkU3x9lI2PyTA7aVS/U3YZfEqxQYs4+adEcOSjveF5YpxR9EDkxyQErohQ9CxFamSCbdowqpGnJRzxXKeIc0cTT5uVPdy5xJUp0pcKnUy2Kwpo3V8Edsj0Nbw669CjMzk");
            result.Append("BZTh9DYHetytKPb4qRx3zYBPSU3iPRF6ngWi3FNKT872CtDx2H7btZpeYANnWThl3cUvhSLlGfdMCMNZg8q4mGAMmMOA95ytyFFnMRKhZzGCmQaD6VJTehcfYeHlsjhTlfNZuUQxS3Yb+TJC3fuJaCa3BrRPQwaLPxvVtfduae8+aA8XWwfLdmnTTZag+Ct5GKgFRlu5");
            result.Append("1f71g3pv9+jGojk37PZd9c5rtX5TP0I4eohiBUqQso5+EjaXYKhfNoNSABMz7clGa7/W2q23f9tXX9x1qGhWoGRSq04D3qSZanVK9SxUmpFLUj3k07JK0QJttjPpfLz/2hylxqIkG/1qiThafm2UE+t2ABYPG+VBiKxVsjPlUvGPNO/t6VrZOUKjFJOhN7H58dyoRu50");
            result.Append("Ny4KiXQy9FBvYPWUyTTWQ2W0Yyy/bu8dYmqzJBckX9U4IWpMt2LKFDu18OCl+eFQxk7k+ljS9Oy0DiF4ffEqWAfF/7ggK+kJ+E22q03pw6+/qADZmMXfcCKCg7/H9lA9b+1z/bH0sxdFxXzjjJjOCRwvnknHc+KZiYn85JlMWhTOzGSSYkoQpuNxMRO7/m8VxffCligB");
            result.Append("AA==");
            return result.ToString();
        }
    }
}

/*<design>
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
<design>*/

