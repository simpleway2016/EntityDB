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

namespace EJ
{
    /// <summary>项目</summary>
    [TableConfig]
    [Table("project")]
    [Way.EntityDB.DataItemJsonConverter]
    public class Project :Way.EntityDB.DataItem
    {
        System.Nullable<Int32> _id;
        /// <summary>id</summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "id")]
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
        string? _Name;
        /// <summary>Name</summary>
        [MaxLength(0)]
        [Display(Name = "Name")]
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
                    SendPropertyChanging("Name",_Name,value);
                    _Name = value;
                    SendPropertyChanged("Name");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<Project, bool>> exp)
        {
            base.SetValue<Project>(exp);
        }
    }
    /// <summary>数据库</summary>
    [TableConfig]
    [Table("databases")]
    [Way.EntityDB.DataItemJsonConverter]
    public class Databases :Way.EntityDB.DataItem
    {
        System.Nullable<Int32> _id;
        /// <summary>id</summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "id")]
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
        System.Nullable<Int32> _ProjectID;
        /// <summary>项目ID</summary>
        [Display(Name = "项目ID")]
        [Column("projectid")]
        public virtual System.Nullable<Int32> ProjectID
        {
            get
            {
                return _ProjectID;
            }
            set
            {
                if ((_ProjectID != value))
                {
                    SendPropertyChanging("ProjectID",_ProjectID,value);
                    _ProjectID = value;
                    SendPropertyChanged("ProjectID");
                }
            }
        }
        string? _Name;
        /// <summary>Name</summary>
        [MaxLength(0)]
        [Display(Name = "Name")]
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
                    SendPropertyChanging("Name",_Name,value);
                    _Name = value;
                    SendPropertyChanged("Name");
                }
            }
        }
        Databases_dbTypeEnum? _dbType=(Databases_dbTypeEnum?)(1);
        /// <summary>数据库类型</summary>
        [Display(Name = "数据库类型")]
        [Column("dbtype")]
        public virtual Databases_dbTypeEnum? dbType
        {
            get
            {
                return _dbType;
            }
            set
            {
                if ((_dbType != value))
                {
                    SendPropertyChanging("dbType",_dbType,value);
                    _dbType = value;
                    SendPropertyChanged("dbType");
                }
            }
        }
        string? _conStr;
        /// <summary>连接字符串</summary>
        [MaxLength(200)]
        [Display(Name = "连接字符串")]
        [Column("constr")]
        public virtual string? conStr
        {
            get
            {
                return _conStr;
            }
            set
            {
                if ((_conStr != value))
                {
                    SendPropertyChanging("conStr",_conStr,value);
                    _conStr = value;
                    SendPropertyChanged("conStr");
                }
            }
        }
        string? _dllPath;
        /// <summary>dll生成文件夹</summary>
        [MaxLength(100)]
        [Display(Name = "dll生成文件夹")]
        [Column("dllpath")]
        public virtual string? dllPath
        {
            get
            {
                return _dllPath;
            }
            set
            {
                if ((_dllPath != value))
                {
                    SendPropertyChanging("dllPath",_dllPath,value);
                    _dllPath = value;
                    SendPropertyChanged("dllPath");
                }
            }
        }
        System.Nullable<Int32> _iLock=0;
        /// <summary>iLock</summary>
        [Display(Name = "iLock")]
        [Column("ilock")]
        public virtual System.Nullable<Int32> iLock
        {
            get
            {
                return _iLock;
            }
            set
            {
                if ((_iLock != value))
                {
                    SendPropertyChanging("iLock",_iLock,value);
                    _iLock = value;
                    SendPropertyChanged("iLock");
                }
            }
        }
        string? _NameSpace;
        /// <summary>NameSpace</summary>
        [MaxLength(0)]
        [Display(Name = "NameSpace")]
        [Column("namespace")]
        public virtual string? NameSpace
        {
            get
            {
                return _NameSpace;
            }
            set
            {
                if ((_NameSpace != value))
                {
                    SendPropertyChanging("NameSpace",_NameSpace,value);
                    _NameSpace = value;
                    SendPropertyChanged("NameSpace");
                }
            }
        }
        string? _Guid;
        /// <summary>唯一标示ID</summary>
        [MaxLength(50)]
        [Display(Name = "唯一标示ID")]
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
                    SendPropertyChanging("Guid",_Guid,value);
                    _Guid = value;
                    SendPropertyChanged("Guid");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<Databases, bool>> exp)
        {
            base.SetValue<Databases>(exp);
        }
    }
    public enum Databases_dbTypeEnum:int
    {
        SqlServer = 1,
        Sqlite = 2,
        MySql=3,
        PostgreSql=4
    }
    /// <summary>系统用户</summary>
    [TableConfig]
    [Table("user")]
    [Way.EntityDB.DataItemJsonConverter]
    public class User :Way.EntityDB.DataItem
    {
        System.Nullable<Int32> _id;
        /// <summary>id</summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "id")]
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
        User_RoleEnum? _Role;
        /// <summary>角色</summary>
        [Display(Name = "角色")]
        [Column("role")]
        public virtual User_RoleEnum? Role
        {
            get
            {
                return _Role;
            }
            set
            {
                if ((_Role != value))
                {
                    SendPropertyChanging("Role",_Role,value);
                    _Role = value;
                    SendPropertyChanged("Role");
                }
            }
        }
        string? _Name;
        /// <summary>Name</summary>
        [MaxLength(0)]
        [Display(Name = "Name")]
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
                    SendPropertyChanging("Name",_Name,value);
                    _Name = value;
                    SendPropertyChanged("Name");
                }
            }
        }
        string? _Password;
        /// <summary>Password</summary>
        [MaxLength(0)]
        [Display(Name = "Password")]
        [Column("password")]
        public virtual string? Password
        {
            get
            {
                return _Password;
            }
            set
            {
                if ((_Password != value))
                {
                    SendPropertyChanging("Password",_Password,value);
                    _Password = value;
                    SendPropertyChanged("Password");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<User, bool>> exp)
        {
            base.SetValue<User>(exp);
        }
    }
    public enum User_RoleEnum:int
    {
        开发人员 = 1,
        客户端测试人员 = 1<<1,
        数据库设计师 = 1<<2 | 开发人员,
        管理员 = 数据库设计师 | 1<<3,
        项目经理 = 1<<4 | 开发人员,
    }
    /// <summary>数据库权限</summary>
    [TableConfig]
    [Table("dbpower")]
    [Way.EntityDB.DataItemJsonConverter]
    public class DBPower :Way.EntityDB.DataItem
    {
        System.Nullable<Int32> _id;
        /// <summary>id</summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "id")]
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
        System.Nullable<Int32> _UserID;
        /// <summary>用户</summary>
        [Display(Name = "用户")]
        [Column("userid")]
        public virtual System.Nullable<Int32> UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                if ((_UserID != value))
                {
                    SendPropertyChanging("UserID",_UserID,value);
                    _UserID = value;
                    SendPropertyChanged("UserID");
                }
            }
        }
        System.Nullable<Int32> _DatabaseID;
        /// <summary>数据库ID</summary>
        [Display(Name = "数据库ID")]
        [Column("databaseid")]
        public virtual System.Nullable<Int32> DatabaseID
        {
            get
            {
                return _DatabaseID;
            }
            set
            {
                if ((_DatabaseID != value))
                {
                    SendPropertyChanging("DatabaseID",_DatabaseID,value);
                    _DatabaseID = value;
                    SendPropertyChanged("DatabaseID");
                }
            }
        }
        DBPower_RoleEnum _Role=(DBPower_RoleEnum)(0);
        /// <summary>权限角色</summary>
        [DisallowNull]
        [Display(Name = "权限角色")]
        [Column("role")]
        public virtual DBPower_RoleEnum Role
        {
            get
            {
                return _Role;
            }
            set
            {
                if ((_Role != value))
                {
                    SendPropertyChanging("Role",_Role,value);
                    _Role = value;
                    SendPropertyChanged("Role");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<DBPower, bool>> exp)
        {
            base.SetValue<DBPower>(exp);
        }
    }
    public enum DBPower_RoleEnum:int
    {
        None = 0,
        /// <summary>查看</summary>
        View = 1<<0,
        /// <summary>修改表结构</summary>
        Modify = 1<<1 | View,
        /// <summary>项目创建人</summary>
        Owner = 1<<2|Modify
    }
    /// <summary>错误报告</summary>
    [TableConfig]
    [Table("bug")]
    [Way.EntityDB.DataItemJsonConverter]
    public class Bug :Way.EntityDB.DataItem
    {
        System.Nullable<Int32> _id;
        /// <summary>id</summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "id")]
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
        string? _Title;
        /// <summary>标题</summary>
        [MaxLength(0)]
        [Display(Name = "标题")]
        [Column("title")]
        public virtual string? Title
        {
            get
            {
                return _Title;
            }
            set
            {
                if ((_Title != value))
                {
                    SendPropertyChanging("Title",_Title,value);
                    _Title = value;
                    SendPropertyChanged("Title");
                }
            }
        }
        System.Nullable<Int32> _SubmitUserID;
        /// <summary>提交者ID</summary>
        [Display(Name = "提交者ID")]
        [Column("submituserid")]
        public virtual System.Nullable<Int32> SubmitUserID
        {
            get
            {
                return _SubmitUserID;
            }
            set
            {
                if ((_SubmitUserID != value))
                {
                    SendPropertyChanging("SubmitUserID",_SubmitUserID,value);
                    _SubmitUserID = value;
                    SendPropertyChanged("SubmitUserID");
                }
            }
        }
        System.Nullable<DateTime> _SubmitTime;
        /// <summary>提交时间</summary>
        [Display(Name = "提交时间")]
        [Column("submittime")]
        public virtual System.Nullable<DateTime> SubmitTime
        {
            get
            {
                return _SubmitTime;
            }
            set
            {
                if ((_SubmitTime != value))
                {
                    SendPropertyChanging("SubmitTime",_SubmitTime,value);
                    _SubmitTime = value;
                    SendPropertyChanged("SubmitTime");
                }
            }
        }
        System.Nullable<Int32> _HandlerID;
        /// <summary>处理者ID</summary>
        [Display(Name = "处理者ID")]
        [Column("handlerid")]
        public virtual System.Nullable<Int32> HandlerID
        {
            get
            {
                return _HandlerID;
            }
            set
            {
                if ((_HandlerID != value))
                {
                    SendPropertyChanging("HandlerID",_HandlerID,value);
                    _HandlerID = value;
                    SendPropertyChanged("HandlerID");
                }
            }
        }
        System.Nullable<DateTime> _LastDate;
        /// <summary>最后反馈时间</summary>
        [Display(Name = "最后反馈时间")]
        [Column("lastdate")]
        public virtual System.Nullable<DateTime> LastDate
        {
            get
            {
                return _LastDate;
            }
            set
            {
                if ((_LastDate != value))
                {
                    SendPropertyChanging("LastDate",_LastDate,value);
                    _LastDate = value;
                    SendPropertyChanged("LastDate");
                }
            }
        }
        System.Nullable<DateTime> _FinishTime;
        /// <summary>处理完毕时间</summary>
        [Display(Name = "处理完毕时间")]
        [Column("finishtime")]
        public virtual System.Nullable<DateTime> FinishTime
        {
            get
            {
                return _FinishTime;
            }
            set
            {
                if ((_FinishTime != value))
                {
                    SendPropertyChanging("FinishTime",_FinishTime,value);
                    _FinishTime = value;
                    SendPropertyChanged("FinishTime");
                }
            }
        }
        Bug_StatusEnum? _Status;
        /// <summary>当前状态</summary>
        [Display(Name = "当前状态")]
        [Column("status")]
        public virtual Bug_StatusEnum? Status
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
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<Bug, bool>> exp)
        {
            base.SetValue<Bug>(exp);
        }
    }
    public enum Bug_StatusEnum:int
    {
        提交给开发人员 = 0,
        反馈给提交者 = 1,
        处理完毕 = 2
    }
    /// <summary>数据表</summary>
    [TableConfig]
    [Table("dbtable")]
    [Way.EntityDB.DataItemJsonConverter]
    public class DBTable :Way.EntityDB.DataItem
    {
        System.Nullable<Int32> _id;
        /// <summary>id</summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "id")]
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
        string? _caption;
        /// <summary>caption</summary>
        [MaxLength(0)]
        [Display(Name = "caption")]
        [Column("caption")]
        public virtual string? caption
        {
            get
            {
                return _caption;
            }
            set
            {
                if ((_caption != value))
                {
                    SendPropertyChanging("caption",_caption,value);
                    _caption = value;
                    SendPropertyChanged("caption");
                }
            }
        }
        string? _Name;
        /// <summary>Name</summary>
        [MaxLength(0)]
        [Display(Name = "Name")]
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
                    SendPropertyChanging("Name",_Name,value);
                    _Name = value;
                    SendPropertyChanged("Name");
                }
            }
        }
        System.Nullable<Int32> _DatabaseID;
        /// <summary>DatabaseID</summary>
        [Display(Name = "DatabaseID")]
        [Column("databaseid")]
        public virtual System.Nullable<Int32> DatabaseID
        {
            get
            {
                return _DatabaseID;
            }
            set
            {
                if ((_DatabaseID != value))
                {
                    SendPropertyChanging("DatabaseID",_DatabaseID,value);
                    _DatabaseID = value;
                    SendPropertyChanged("DatabaseID");
                }
            }
        }
        System.Nullable<Int32> _iLock=0;
        /// <summary>iLock</summary>
        [Display(Name = "iLock")]
        [Column("ilock")]
        public virtual System.Nullable<Int32> iLock
        {
            get
            {
                return _iLock;
            }
            set
            {
                if ((_iLock != value))
                {
                    SendPropertyChanging("iLock",_iLock,value);
                    _iLock = value;
                    SendPropertyChanged("iLock");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<DBTable, bool>> exp)
        {
            base.SetValue<DBTable>(exp);
        }
    }
    /// <summary>字段</summary>
    [TableConfig]
    [Table("dbcolumn")]
    [Way.EntityDB.DataItemJsonConverter]
    public class DBColumn :Way.EntityDB.DataItem
    {
        System.Nullable<Int32> _id;
        /// <summary>id</summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "id")]
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
        string? _caption;
        /// <summary>caption</summary>
        [MaxLength(200)]
        [Display(Name = "caption")]
        [Column("caption")]
        public virtual string? caption
        {
            get
            {
                return _caption;
            }
            set
            {
                if ((_caption != value))
                {
                    SendPropertyChanging("caption",_caption,value);
                    _caption = value;
                    SendPropertyChanged("caption");
                }
            }
        }
        string? _Name;
        /// <summary>Name</summary>
        [MaxLength(0)]
        [Display(Name = "Name")]
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
                    SendPropertyChanging("Name",_Name,value);
                    _Name = value;
                    SendPropertyChanged("Name");
                }
            }
        }
        System.Nullable<Boolean> _IsAutoIncrement=false;
        /// <summary>自增长</summary>
        [Display(Name = "自增长")]
        [Column("isautoincrement")]
        public virtual System.Nullable<Boolean> IsAutoIncrement
        {
            get
            {
                return _IsAutoIncrement;
            }
            set
            {
                if ((_IsAutoIncrement != value))
                {
                    SendPropertyChanging("IsAutoIncrement",_IsAutoIncrement,value);
                    _IsAutoIncrement = value;
                    SendPropertyChanged("IsAutoIncrement");
                }
            }
        }
        System.Nullable<Boolean> _CanNull=true;
        /// <summary>可以为空</summary>
        [Display(Name = "可以为空")]
        [Column("cannull")]
        public virtual System.Nullable<Boolean> CanNull
        {
            get
            {
                return _CanNull;
            }
            set
            {
                if ((_CanNull != value))
                {
                    SendPropertyChanging("CanNull",_CanNull,value);
                    _CanNull = value;
                    SendPropertyChanged("CanNull");
                }
            }
        }
        string? _dbType;
        /// <summary>数据库字段类型</summary>
        [MaxLength(0)]
        [Display(Name = "数据库字段类型")]
        [Column("dbtype")]
        public virtual string? dbType
        {
            get
            {
                return _dbType;
            }
            set
            {
                if ((_dbType != value))
                {
                    SendPropertyChanging("dbType",_dbType,value);
                    _dbType = value;
                    SendPropertyChanged("dbType");
                }
            }
        }
        string? _Type;
        /// <summary>c#类型</summary>
        [MaxLength(0)]
        [Display(Name = "c#类型")]
        [Column("type")]
        public virtual string? Type
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
        string? _EnumDefine;
        /// <summary>Enum定义</summary>
        [MaxLength(300)]
        [Display(Name = "Enum定义")]
        [Column("enumdefine")]
        public virtual string? EnumDefine
        {
            get
            {
                return _EnumDefine;
            }
            set
            {
                if ((_EnumDefine != value))
                {
                    SendPropertyChanging("EnumDefine",_EnumDefine,value);
                    _EnumDefine = value;
                    SendPropertyChanged("EnumDefine");
                }
            }
        }
        string? _length;
        /// <summary>长度</summary>
        [MaxLength(0)]
        [Display(Name = "长度")]
        [Column("length")]
        public virtual string? length
        {
            get
            {
                return _length;
            }
            set
            {
                if ((_length != value))
                {
                    SendPropertyChanging("length",_length,value);
                    _length = value;
                    SendPropertyChanged("length");
                }
            }
        }
        string? _defaultValue;
        /// <summary>默认值</summary>
        [MaxLength(200)]
        [Display(Name = "默认值")]
        [Column("defaultvalue")]
        public virtual string? defaultValue
        {
            get
            {
                return _defaultValue;
            }
            set
            {
                if ((_defaultValue != value))
                {
                    SendPropertyChanging("defaultValue",_defaultValue,value);
                    _defaultValue = value;
                    SendPropertyChanged("defaultValue");
                }
            }
        }
        System.Nullable<Int32> _TableID;
        /// <summary>TableID</summary>
        [Display(Name = "TableID")]
        [Column("tableid")]
        public virtual System.Nullable<Int32> TableID
        {
            get
            {
                return _TableID;
            }
            set
            {
                if ((_TableID != value))
                {
                    SendPropertyChanging("TableID",_TableID,value);
                    _TableID = value;
                    SendPropertyChanged("TableID");
                }
            }
        }
        System.Nullable<Boolean> _IsPKID=false;
        /// <summary>是否是主键</summary>
        [Display(Name = "是否是主键")]
        [Column("ispkid")]
        public virtual System.Nullable<Boolean> IsPKID
        {
            get
            {
                return _IsPKID;
            }
            set
            {
                if ((_IsPKID != value))
                {
                    SendPropertyChanging("IsPKID",_IsPKID,value);
                    _IsPKID = value;
                    SendPropertyChanged("IsPKID");
                }
            }
        }
        System.Nullable<Int32> _orderid=0;
        /// <summary>orderid</summary>
        [Display(Name = "orderid")]
        [Column("orderid")]
        public virtual System.Nullable<Int32> orderid
        {
            get
            {
                return _orderid;
            }
            set
            {
                if ((_orderid != value))
                {
                    SendPropertyChanging("orderid",_orderid,value);
                    _orderid = value;
                    SendPropertyChanged("orderid");
                }
            }
        }
        System.Nullable<Boolean> _IsDiscriminator=false;
        /// <summary>是否是Discriminator字段</summary>
        [Display(Name = "是否是Discriminator字段")]
        [Column("isdiscriminator")]
        public virtual System.Nullable<Boolean> IsDiscriminator
        {
            get
            {
                return _IsDiscriminator;
            }
            set
            {
                if ((_IsDiscriminator != value))
                {
                    SendPropertyChanging("IsDiscriminator",_IsDiscriminator,value);
                    _IsDiscriminator = value;
                    SendPropertyChanged("IsDiscriminator");
                }
            }
        }
        string? _ClassName;
        /// <summary>派生类的类名</summary>
        [MaxLength(120)]
        [Display(Name = "派生类的类名")]
        [Column("classname")]
        public virtual string? ClassName
        {
            get
            {
                return _ClassName;
            }
            set
            {
                if ((_ClassName != value))
                {
                    SendPropertyChanging("ClassName",_ClassName,value);
                    _ClassName = value;
                    SendPropertyChanged("ClassName");
                }
            }
        }
        string? _ClassFullName;
        /// <summary>字段类型全称</summary>
        [MaxLength(50)]
        [Display(Name = "字段类型全称")]
        [Column("classfullname")]
        public virtual string? ClassFullName
        {
            get
            {
                return _ClassFullName;
            }
            set
            {
                if ((_ClassFullName != value))
                {
                    SendPropertyChanging("ClassFullName",_ClassFullName,value);
                    _ClassFullName = value;
                    SendPropertyChanged("ClassFullName");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<DBColumn, bool>> exp)
        {
            base.SetValue<DBColumn>(exp);
        }
    }
    /// <summary>项目权限</summary>
    [TableConfig]
    [Table("projectpower")]
    [Way.EntityDB.DataItemJsonConverter]
    public class ProjectPower :Way.EntityDB.DataItem
    {
        System.Nullable<Int32> _id;
        /// <summary>id</summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "id")]
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
        System.Nullable<Int32> _ProjectID;
        /// <summary>ProjectID</summary>
        [Display(Name = "ProjectID")]
        [Column("projectid")]
        public virtual System.Nullable<Int32> ProjectID
        {
            get
            {
                return _ProjectID;
            }
            set
            {
                if ((_ProjectID != value))
                {
                    SendPropertyChanging("ProjectID",_ProjectID,value);
                    _ProjectID = value;
                    SendPropertyChanged("ProjectID");
                }
            }
        }
        System.Nullable<Int32> _UserID;
        /// <summary>UserID</summary>
        [Display(Name = "UserID")]
        [Column("userid")]
        public virtual System.Nullable<Int32> UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                if ((_UserID != value))
                {
                    SendPropertyChanging("UserID",_UserID,value);
                    _UserID = value;
                    SendPropertyChanged("UserID");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<ProjectPower, bool>> exp)
        {
            base.SetValue<ProjectPower>(exp);
        }
    }
    /// <summary>数据模块</summary>
    [TableConfig]
    [Table("dbmodule")]
    [Way.EntityDB.DataItemJsonConverter]
    public class DBModule :Way.EntityDB.DataItem
    {
        System.Nullable<Int32> _id;
        /// <summary>id</summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "id")]
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
        string? _Name;
        /// <summary>Name</summary>
        [MaxLength(0)]
        [Display(Name = "Name")]
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
                    SendPropertyChanging("Name",_Name,value);
                    _Name = value;
                    SendPropertyChanged("Name");
                }
            }
        }
        System.Nullable<Int32> _DatabaseID;
        /// <summary>DatabaseID</summary>
        [Display(Name = "DatabaseID")]
        [Column("databaseid")]
        public virtual System.Nullable<Int32> DatabaseID
        {
            get
            {
                return _DatabaseID;
            }
            set
            {
                if ((_DatabaseID != value))
                {
                    SendPropertyChanging("DatabaseID",_DatabaseID,value);
                    _DatabaseID = value;
                    SendPropertyChanged("DatabaseID");
                }
            }
        }
        System.Nullable<Boolean> _IsFolder=false;
        /// <summary>IsFolder</summary>
        [Display(Name = "IsFolder")]
        [Column("isfolder")]
        public virtual System.Nullable<Boolean> IsFolder
        {
            get
            {
                return _IsFolder;
            }
            set
            {
                if ((_IsFolder != value))
                {
                    SendPropertyChanging("IsFolder",_IsFolder,value);
                    _IsFolder = value;
                    SendPropertyChanged("IsFolder");
                }
            }
        }
        System.Nullable<Int32> _parentID;
        /// <summary>parentID</summary>
        [Display(Name = "parentID")]
        [Column("parentid")]
        public virtual System.Nullable<Int32> parentID
        {
            get
            {
                return _parentID;
            }
            set
            {
                if ((_parentID != value))
                {
                    SendPropertyChanging("parentID",_parentID,value);
                    _parentID = value;
                    SendPropertyChanged("parentID");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<DBModule, bool>> exp)
        {
            base.SetValue<DBModule>(exp);
        }
    }
    /// <summary>级联删除</summary>
    [TableConfig]
    [Table("dbdeleteconfig")]
    [Way.EntityDB.DataItemJsonConverter]
    public class DBDeleteConfig :Way.EntityDB.DataItem
    {
        System.Nullable<Int32> _id;
        /// <summary>id</summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "id")]
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
        System.Nullable<Int32> _TableID;
        /// <summary>TableID</summary>
        [Display(Name = "TableID")]
        [Column("tableid")]
        public virtual System.Nullable<Int32> TableID
        {
            get
            {
                return _TableID;
            }
            set
            {
                if ((_TableID != value))
                {
                    SendPropertyChanging("TableID",_TableID,value);
                    _TableID = value;
                    SendPropertyChanged("TableID");
                }
            }
        }
        System.Nullable<Int32> _RelaTableID;
        /// <summary>关联表ID</summary>
        [Display(Name = "关联表ID")]
        [Column("relatableid")]
        public virtual System.Nullable<Int32> RelaTableID
        {
            get
            {
                return _RelaTableID;
            }
            set
            {
                if ((_RelaTableID != value))
                {
                    SendPropertyChanging("RelaTableID",_RelaTableID,value);
                    _RelaTableID = value;
                    SendPropertyChanged("RelaTableID");
                }
            }
        }
        string? _RelaTable_Desc;
        /// <summary>RelaTable_Desc</summary>
        [MaxLength(0)]
        [Display(Name = "RelaTable_Desc")]
        [Column("relatable_desc")]
        public virtual string? RelaTable_Desc
        {
            get
            {
                return _RelaTable_Desc;
            }
            set
            {
                if ((_RelaTable_Desc != value))
                {
                    SendPropertyChanging("RelaTable_Desc",_RelaTable_Desc,value);
                    _RelaTable_Desc = value;
                    SendPropertyChanged("RelaTable_Desc");
                }
            }
        }
        System.Nullable<Int32> _RelaColumID;
        /// <summary>关联字段的ID</summary>
        [Display(Name = "关联字段的ID")]
        [Column("relacolumid")]
        public virtual System.Nullable<Int32> RelaColumID
        {
            get
            {
                return _RelaColumID;
            }
            set
            {
                if ((_RelaColumID != value))
                {
                    SendPropertyChanging("RelaColumID",_RelaColumID,value);
                    _RelaColumID = value;
                    SendPropertyChanged("RelaColumID");
                }
            }
        }
        string? _RelaColumn_Desc;
        /// <summary>RelaColumn_Desc</summary>
        [MaxLength(0)]
        [Display(Name = "RelaColumn_Desc")]
        [Column("relacolumn_desc")]
        public virtual string? RelaColumn_Desc
        {
            get
            {
                return _RelaColumn_Desc;
            }
            set
            {
                if ((_RelaColumn_Desc != value))
                {
                    SendPropertyChanging("RelaColumn_Desc",_RelaColumn_Desc,value);
                    _RelaColumn_Desc = value;
                    SendPropertyChanged("RelaColumn_Desc");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<DBDeleteConfig, bool>> exp)
        {
            base.SetValue<DBDeleteConfig>(exp);
        }
    }
    /// <summary>TableInModule</summary>
    [TableConfig]
    [Table("tableinmodule")]
    [Way.EntityDB.DataItemJsonConverter]
    public class TableInModule :Way.EntityDB.DataItem
    {
        System.Nullable<Int32> _id;
        /// <summary>id</summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "id")]
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
        System.Nullable<Int32> _TableID;
        /// <summary>TableID</summary>
        [Display(Name = "TableID")]
        [Column("tableid")]
        public virtual System.Nullable<Int32> TableID
        {
            get
            {
                return _TableID;
            }
            set
            {
                if ((_TableID != value))
                {
                    SendPropertyChanging("TableID",_TableID,value);
                    _TableID = value;
                    SendPropertyChanged("TableID");
                }
            }
        }
        System.Nullable<Int32> _ModuleID;
        /// <summary>ModuleID</summary>
        [Display(Name = "ModuleID")]
        [Column("moduleid")]
        public virtual System.Nullable<Int32> ModuleID
        {
            get
            {
                return _ModuleID;
            }
            set
            {
                if ((_ModuleID != value))
                {
                    SendPropertyChanging("ModuleID",_ModuleID,value);
                    _ModuleID = value;
                    SendPropertyChanged("ModuleID");
                }
            }
        }
        System.Nullable<Int32> _x;
        /// <summary>x</summary>
        [Display(Name = "x")]
        [Column("x")]
        public virtual System.Nullable<Int32> x
        {
            get
            {
                return _x;
            }
            set
            {
                if ((_x != value))
                {
                    SendPropertyChanging("x",_x,value);
                    _x = value;
                    SendPropertyChanged("x");
                }
            }
        }
        System.Nullable<Int32> _y;
        /// <summary>y</summary>
        [Display(Name = "y")]
        [Column("y")]
        public virtual System.Nullable<Int32> y
        {
            get
            {
                return _y;
            }
            set
            {
                if ((_y != value))
                {
                    SendPropertyChanging("y",_y,value);
                    _y = value;
                    SendPropertyChanged("y");
                }
            }
        }
        string? _flag;
        /// <summary>临时变量</summary>
        [MaxLength(0)]
        [Display(Name = "临时变量")]
        [Column("flag")]
        public virtual string? flag
        {
            get
            {
                return _flag;
            }
            set
            {
                if ((_flag != value))
                {
                    SendPropertyChanging("flag",_flag,value);
                    _flag = value;
                    SendPropertyChanged("flag");
                }
            }
        }
        string? _flag2;
        /// <summary>flag2</summary>
        [MaxLength(0)]
        [Display(Name = "flag2")]
        [Column("flag2")]
        public virtual string? flag2
        {
            get
            {
                return _flag2;
            }
            set
            {
                if ((_flag2 != value))
                {
                    SendPropertyChanging("flag2",_flag2,value);
                    _flag2 = value;
                    SendPropertyChanged("flag2");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<TableInModule, bool>> exp)
        {
            base.SetValue<TableInModule>(exp);
        }
    }
    /// <summary>唯一值索引</summary>
    [TableConfig]
    [Table("idxindex")]
    [Way.EntityDB.DataItemJsonConverter]
    public class IDXIndex :Way.EntityDB.DataItem
    {
        System.Nullable<Int32> _id;
        /// <summary>id</summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "id")]
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
        System.Nullable<Int32> _TableID;
        /// <summary>TableID</summary>
        [Display(Name = "TableID")]
        [Column("tableid")]
        public virtual System.Nullable<Int32> TableID
        {
            get
            {
                return _TableID;
            }
            set
            {
                if ((_TableID != value))
                {
                    SendPropertyChanging("TableID",_TableID,value);
                    _TableID = value;
                    SendPropertyChanged("TableID");
                }
            }
        }
        string? _Keys;
        /// <summary>Keys</summary>
        [MaxLength(100)]
        [Display(Name = "Keys")]
        [Column("keys")]
        public virtual string? Keys
        {
            get
            {
                return _Keys;
            }
            set
            {
                if ((_Keys != value))
                {
                    SendPropertyChanging("Keys",_Keys,value);
                    _Keys = value;
                    SendPropertyChanged("Keys");
                }
            }
        }
        System.Nullable<Boolean> _IsUnique=true;
        /// <summary>是否唯一索引</summary>
        [Display(Name = "是否唯一索引")]
        [Column("isunique")]
        public virtual System.Nullable<Boolean> IsUnique
        {
            get
            {
                return _IsUnique;
            }
            set
            {
                if ((_IsUnique != value))
                {
                    SendPropertyChanging("IsUnique",_IsUnique,value);
                    _IsUnique = value;
                    SendPropertyChanged("IsUnique");
                }
            }
        }
        System.Nullable<Boolean> _IsClustered=false;
        /// <summary>是否聚焦</summary>
        [Display(Name = "是否聚焦")]
        [Column("isclustered")]
        public virtual System.Nullable<Boolean> IsClustered
        {
            get
            {
                return _IsClustered;
            }
            set
            {
                if ((_IsClustered != value))
                {
                    SendPropertyChanging("IsClustered",_IsClustered,value);
                    _IsClustered = value;
                    SendPropertyChanged("IsClustered");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<IDXIndex, bool>> exp)
        {
            base.SetValue<IDXIndex>(exp);
        }
    }
    /// <summary>Bug处理历史记录</summary>
    [TableConfig]
    [Table("bughandlehistory")]
    [Way.EntityDB.DataItemJsonConverter]
    public class BugHandleHistory :Way.EntityDB.DataItem
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
        System.Nullable<Int32> _BugID;
        [Column("bugid")]
        public virtual System.Nullable<Int32> BugID
        {
            get
            {
                return _BugID;
            }
            set
            {
                if ((_BugID != value))
                {
                    SendPropertyChanging("BugID",_BugID,value);
                    _BugID = value;
                    SendPropertyChanged("BugID");
                }
            }
        }
        System.Nullable<Int32> _UserID;
        /// <summary>发标者ID</summary>
        [Display(Name = "发标者ID")]
        [Column("userid")]
        public virtual System.Nullable<Int32> UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                if ((_UserID != value))
                {
                    SendPropertyChanging("UserID",_UserID,value);
                    _UserID = value;
                    SendPropertyChanged("UserID");
                }
            }
        }
        Byte[] _content;
        /// <summary>内容</summary>
        [Display(Name = "内容")]
        [Column("content")]
        public virtual Byte[] content
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
        System.Nullable<DateTime> _SendTime;
        /// <summary>发表时间</summary>
        [Display(Name = "发表时间")]
        [Column("sendtime")]
        public virtual System.Nullable<DateTime> SendTime
        {
            get
            {
                return _SendTime;
            }
            set
            {
                if ((_SendTime != value))
                {
                    SendPropertyChanging("SendTime",_SendTime,value);
                    _SendTime = value;
                    SendPropertyChanged("SendTime");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<BugHandleHistory, bool>> exp)
        {
            base.SetValue<BugHandleHistory>(exp);
        }
    }
    /// <summary>Bug附带截图</summary>
    [TableConfig]
    [Table("bugimages")]
    [Way.EntityDB.DataItemJsonConverter]
    public class BugImages :Way.EntityDB.DataItem
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
        System.Nullable<Int32> _BugID;
        [Column("bugid")]
        public virtual System.Nullable<Int32> BugID
        {
            get
            {
                return _BugID;
            }
            set
            {
                if ((_BugID != value))
                {
                    SendPropertyChanging("BugID",_BugID,value);
                    _BugID = value;
                    SendPropertyChanged("BugID");
                }
            }
        }
        Byte[] _content;
        [Column("content")]
        public virtual Byte[] content
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
        System.Nullable<Int32> _orderID;
        /// <summary>排序</summary>
        [Display(Name = "排序")]
        [Column("orderid")]
        public virtual System.Nullable<Int32> orderID
        {
            get
            {
                return _orderID;
            }
            set
            {
                if ((_orderID != value))
                {
                    SendPropertyChanging("orderID",_orderID,value);
                    _orderID = value;
                    SendPropertyChanged("orderID");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<BugImages, bool>> exp)
        {
            base.SetValue<BugImages>(exp);
        }
    }
    /// <summary>引入的dll</summary>
    [TableConfig]
    [Table("dllimport")]
    [Way.EntityDB.DataItemJsonConverter]
    public class DLLImport :Way.EntityDB.DataItem
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
        string? _path;
        /// <summary>dll文件路径</summary>
        [MaxLength(200)]
        [Display(Name = "dll文件路径")]
        [Column("path")]
        public virtual string? path
        {
            get
            {
                return _path;
            }
            set
            {
                if ((_path != value))
                {
                    SendPropertyChanging("path",_path,value);
                    _path = value;
                    SendPropertyChanged("path");
                }
            }
        }
        System.Nullable<Int32> _ProjectID;
        [Column("projectid")]
        public virtual System.Nullable<Int32> ProjectID
        {
            get
            {
                return _ProjectID;
            }
            set
            {
                if ((_ProjectID != value))
                {
                    SendPropertyChanging("ProjectID",_ProjectID,value);
                    _ProjectID = value;
                    SendPropertyChanged("ProjectID");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<DLLImport, bool>> exp)
        {
            base.SetValue<DLLImport>(exp);
        }
    }
    /// <summary>接口设计的目录结构</summary>
    [TableConfig]
    [Table("interfacemodule")]
    [Way.EntityDB.DataItemJsonConverter]
    public class InterfaceModule :Way.EntityDB.DataItem
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
        System.Nullable<Int32> _ProjectID;
        [Column("projectid")]
        public virtual System.Nullable<Int32> ProjectID
        {
            get
            {
                return _ProjectID;
            }
            set
            {
                if ((_ProjectID != value))
                {
                    SendPropertyChanging("ProjectID",_ProjectID,value);
                    _ProjectID = value;
                    SendPropertyChanged("ProjectID");
                }
            }
        }
        string? _Name;
        [MaxLength(50)]
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
                    SendPropertyChanging("Name",_Name,value);
                    _Name = value;
                    SendPropertyChanged("Name");
                }
            }
        }
        System.Nullable<Int32> _ParentID=0;
        [Column("parentid")]
        public virtual System.Nullable<Int32> ParentID
        {
            get
            {
                return _ParentID;
            }
            set
            {
                if ((_ParentID != value))
                {
                    SendPropertyChanging("ParentID",_ParentID,value);
                    _ParentID = value;
                    SendPropertyChanged("ParentID");
                }
            }
        }
        System.Nullable<Boolean> _IsFolder=false;
        [Column("isfolder")]
        public virtual System.Nullable<Boolean> IsFolder
        {
            get
            {
                return _IsFolder;
            }
            set
            {
                if ((_IsFolder != value))
                {
                    SendPropertyChanging("IsFolder",_IsFolder,value);
                    _IsFolder = value;
                    SendPropertyChanged("IsFolder");
                }
            }
        }
        System.Nullable<Int32> _LockUserId;
        /// <summary>已经被某人锁定</summary>
        [Display(Name = "已经被某人锁定")]
        [Column("lockuserid")]
        public virtual System.Nullable<Int32> LockUserId
        {
            get
            {
                return _LockUserId;
            }
            set
            {
                if ((_LockUserId != value))
                {
                    SendPropertyChanging("LockUserId",_LockUserId,value);
                    _LockUserId = value;
                    SendPropertyChanged("LockUserId");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<InterfaceModule, bool>> exp)
        {
            base.SetValue<InterfaceModule>(exp);
        }
    }
    [TableConfig]
    [Table("interfaceinmodule")]
    [Way.EntityDB.DataItemJsonConverter]
    public class InterfaceInModule :Way.EntityDB.DataItem
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
        System.Nullable<Int32> _ModuleID;
        [Column("moduleid")]
        public virtual System.Nullable<Int32> ModuleID
        {
            get
            {
                return _ModuleID;
            }
            set
            {
                if ((_ModuleID != value))
                {
                    SendPropertyChanging("ModuleID",_ModuleID,value);
                    _ModuleID = value;
                    SendPropertyChanged("ModuleID");
                }
            }
        }
        System.Nullable<Int32> _x;
        [Column("x")]
        public virtual System.Nullable<Int32> x
        {
            get
            {
                return _x;
            }
            set
            {
                if ((_x != value))
                {
                    SendPropertyChanging("x",_x,value);
                    _x = value;
                    SendPropertyChanged("x");
                }
            }
        }
        System.Nullable<Int32> _y;
        [Column("y")]
        public virtual System.Nullable<Int32> y
        {
            get
            {
                return _y;
            }
            set
            {
                if ((_y != value))
                {
                    SendPropertyChanging("y",_y,value);
                    _y = value;
                    SendPropertyChanged("y");
                }
            }
        }
        string? _Type;
        [MaxLength(100)]
        [Column("type")]
        public virtual string? Type
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
        string? _JsonData;
        [Column("jsondata")]
        public virtual string? JsonData
        {
            get
            {
                return _JsonData;
            }
            set
            {
                if ((_JsonData != value))
                {
                    SendPropertyChanging("JsonData",_JsonData,value);
                    _JsonData = value;
                    SendPropertyChanged("JsonData");
                }
            }
        }
        System.Nullable<Int32> _width;
        [Column("width")]
        public virtual System.Nullable<Int32> width
        {
            get
            {
                return _width;
            }
            set
            {
                if ((_width != value))
                {
                    SendPropertyChanging("width",_width,value);
                    _width = value;
                    SendPropertyChanged("width");
                }
            }
        }
        System.Nullable<Int32> _height;
        [Column("height")]
        public virtual System.Nullable<Int32> height
        {
            get
            {
                return _height;
            }
            set
            {
                if ((_height != value))
                {
                    SendPropertyChanging("height",_height,value);
                    _height = value;
                    SendPropertyChanged("height");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<InterfaceInModule, bool>> exp)
        {
            base.SetValue<InterfaceInModule>(exp);
        }
    }
    /// <summary>InterfaceModule权限设定表</summary>
    [TableConfig]
    [Table("interfacemodulepower")]
    [Way.EntityDB.DataItemJsonConverter]
    public class InterfaceModulePower :Way.EntityDB.DataItem
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
        System.Nullable<Int32> _UserID;
        [Column("userid")]
        public virtual System.Nullable<Int32> UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                if ((_UserID != value))
                {
                    SendPropertyChanging("UserID",_UserID,value);
                    _UserID = value;
                    SendPropertyChanged("UserID");
                }
            }
        }
        System.Nullable<Int32> _ModuleID;
        [Column("moduleid")]
        public virtual System.Nullable<Int32> ModuleID
        {
            get
            {
                return _ModuleID;
            }
            set
            {
                if ((_ModuleID != value))
                {
                    SendPropertyChanging("ModuleID",_ModuleID,value);
                    _ModuleID = value;
                    SendPropertyChanged("ModuleID");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<InterfaceModulePower, bool>> exp)
        {
            base.SetValue<InterfaceModulePower>(exp);
        }
    }
    [TableConfig]
    [Table("classproperty")]
    [Way.EntityDB.DataItemJsonConverter]
    public class classproperty :Way.EntityDB.DataItem
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
        System.Nullable<Int32> _tableid;
        [Column("tableid")]
        public virtual System.Nullable<Int32> tableid
        {
            get
            {
                return _tableid;
            }
            set
            {
                if ((_tableid != value))
                {
                    SendPropertyChanging("tableid",_tableid,value);
                    _tableid = value;
                    SendPropertyChanged("tableid");
                }
            }
        }
        string? _name;
        [MaxLength(50)]
        [Column("name")]
        public virtual string? name
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
        System.Nullable<Int32> _foreignkey_tableid;
        /// <summary>属性的类型</summary>
        [Display(Name = "属性的类型")]
        [Column("foreignkey_tableid")]
        public virtual System.Nullable<Int32> foreignkey_tableid
        {
            get
            {
                return _foreignkey_tableid;
            }
            set
            {
                if ((_foreignkey_tableid != value))
                {
                    SendPropertyChanging("foreignkey_tableid",_foreignkey_tableid,value);
                    _foreignkey_tableid = value;
                    SendPropertyChanged("foreignkey_tableid");
                }
            }
        }
        System.Nullable<Int32> _foreignkey_columnid;
        [Column("foreignkey_columnid")]
        public virtual System.Nullable<Int32> foreignkey_columnid
        {
            get
            {
                return _foreignkey_columnid;
            }
            set
            {
                if ((_foreignkey_columnid != value))
                {
                    SendPropertyChanging("foreignkey_columnid",_foreignkey_columnid,value);
                    _foreignkey_columnid = value;
                    SendPropertyChanged("foreignkey_columnid");
                }
            }
        }
        System.Nullable<Boolean> _iscollection=false;
        [Column("iscollection")]
        public virtual System.Nullable<Boolean> iscollection
        {
            get
            {
                return _iscollection;
            }
            set
            {
                if ((_iscollection != value))
                {
                    SendPropertyChanging("iscollection",_iscollection,value);
                    _iscollection = value;
                    SendPropertyChanged("iscollection");
                }
            }
        }
        string? _desc;
        [MaxLength(100)]
        [Column("desc")]
        public virtual string? desc
        {
            get
            {
                return _desc;
            }
            set
            {
                if ((_desc != value))
                {
                    SendPropertyChanging("desc",_desc,value);
                    _desc = value;
                    SendPropertyChanged("desc");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<classproperty, bool>> exp)
        {
            base.SetValue<classproperty>(exp);
        }
    }
    [TableConfig]
    [Table("designhistory")]
    [Way.EntityDB.DataItemJsonConverter]
    public class DesignHistory :Way.EntityDB.DataItem
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
        System.Nullable<Int32> _DatabaseId;
        [Column("databaseid")]
        public virtual System.Nullable<Int32> DatabaseId
        {
            get
            {
                return _DatabaseId;
            }
            set
            {
                if ((_DatabaseId != value))
                {
                    SendPropertyChanging("DatabaseId",_DatabaseId,value);
                    _DatabaseId = value;
                    SendPropertyChanged("DatabaseId");
                }
            }
        }
        System.Nullable<Int32> _ActionId;
        [Column("actionid")]
        public virtual System.Nullable<Int32> ActionId
        {
            get
            {
                return _ActionId;
            }
            set
            {
                if ((_ActionId != value))
                {
                    SendPropertyChanging("ActionId",_ActionId,value);
                    _ActionId = value;
                    SendPropertyChanged("ActionId");
                }
            }
        }
        string? _Type;
        [MaxLength(100)]
        [Column("type")]
        public virtual string? Type
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
        string? _Content;
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
                    SendPropertyChanging("Content",_Content,value);
                    _Content = value;
                    SendPropertyChanged("Content");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<DesignHistory, bool>> exp)
        {
            base.SetValue<DesignHistory>(exp);
        }
    }
    [TableConfig]
    [Table("syslog")]
    [Way.EntityDB.DataItemJsonConverter]
    public class SysLog :Way.EntityDB.DataItem
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
        System.Nullable<Int32> _DatabaseId;
        [Column("databaseid")]
        public virtual System.Nullable<Int32> DatabaseId
        {
            get
            {
                return _DatabaseId;
            }
            set
            {
                if ((_DatabaseId != value))
                {
                    SendPropertyChanging("DatabaseId",_DatabaseId,value);
                    _DatabaseId = value;
                    SendPropertyChanged("DatabaseId");
                }
            }
        }
        System.Nullable<Int32> _UserId;
        [Column("userid")]
        public virtual System.Nullable<Int32> UserId
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
        string? _Type;
        [MaxLength(100)]
        [Column("type")]
        public virtual string? Type
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
        string? _Content;
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
                    SendPropertyChanging("Content",_Content,value);
                    _Content = value;
                    SendPropertyChanged("Content");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<SysLog, bool>> exp)
        {
            base.SetValue<SysLog>(exp);
        }
    }
}

namespace EJ.DB
{
    public class easyjob : Way.EntityDB.DBContext
    {
         public easyjob(string connection, Way.EntityDB.DatabaseType dbType , bool upgradeDatabase = true): base(connection, dbType , upgradeDatabase)
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
             var db =  sender as easyjob;
            if (db == null) return;
            if (e.DataItem is Project)
            {
                var deletingItem = (Project)e.DataItem;
                var items0 = (from m in db.Databases where m.ProjectID == deletingItem.id
                select new Databases
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
                var items1 = (from m in db.DLLImport where m.ProjectID == deletingItem.id
                select new DLLImport
                {
                    id = m.id
                }
                );
                while(true)
                {
                    var data2del = items1.Take(100).ToList();
                    if(data2del.Count() ==0)
                        break;
                    foreach (var t in data2del)
                    {
                        t.ChangedProperties.Clear();
                        db.Delete(t);
                    }
                }
                var items2 = (from m in db.InterfaceModule where m.ProjectID == deletingItem.id
                select new InterfaceModule
                {
                    id = m.id
                }
                );
                while(true)
                {
                    var data2del = items2.Take(100).ToList();
                    if(data2del.Count() ==0)
                        break;
                    foreach (var t in data2del)
                    {
                        t.ChangedProperties.Clear();
                        db.Delete(t);
                    }
                }
                var items3 = (from m in db.ProjectPower where m.ProjectID == deletingItem.id
                select new ProjectPower
                {
                    id = m.id
                }
                );
                while(true)
                {
                    var data2del = items3.Take(100).ToList();
                    if(data2del.Count() ==0)
                        break;
                    foreach (var t in data2del)
                    {
                        t.ChangedProperties.Clear();
                        db.Delete(t);
                    }
                }
            }
            if (e.DataItem is Databases)
            {
                var deletingItem = (Databases)e.DataItem;
                var items0 = (from m in db.DBPower where m.DatabaseID == deletingItem.id
                select new DBPower
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
                var items1 = (from m in db.DBTable where m.DatabaseID == deletingItem.id
                select new DBTable
                {
                    id = m.id
                }
                );
                while(true)
                {
                    var data2del = items1.Take(100).ToList();
                    if(data2del.Count() ==0)
                        break;
                    foreach (var t in data2del)
                    {
                        t.ChangedProperties.Clear();
                        db.Delete(t);
                    }
                }
                var items2 = (from m in db.DBModule where m.DatabaseID == deletingItem.id
                select new DBModule
                {
                    id = m.id
                }
                );
                while(true)
                {
                    var data2del = items2.Take(100).ToList();
                    if(data2del.Count() ==0)
                        break;
                    foreach (var t in data2del)
                    {
                        t.ChangedProperties.Clear();
                        db.Delete(t);
                    }
                }
            }
            if (e.DataItem is User)
            {
                var deletingItem = (User)e.DataItem;
                var items0 = (from m in db.InterfaceModulePower where m.UserID == deletingItem.id
                select new InterfaceModulePower
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
                var items1 = (from m in db.DBPower where m.UserID == deletingItem.id
                select new DBPower
                {
                    id = m.id
                }
                );
                while(true)
                {
                    var data2del = items1.Take(100).ToList();
                    if(data2del.Count() ==0)
                        break;
                    foreach (var t in data2del)
                    {
                        t.ChangedProperties.Clear();
                        db.Delete(t);
                    }
                }
                var items2 = (from m in db.ProjectPower where m.UserID == deletingItem.id
                select new ProjectPower
                {
                    id = m.id
                }
                );
                while(true)
                {
                    var data2del = items2.Take(100).ToList();
                    if(data2del.Count() ==0)
                        break;
                    foreach (var t in data2del)
                    {
                        t.ChangedProperties.Clear();
                        db.Delete(t);
                    }
                }
            }
            if (e.DataItem is Bug)
            {
                var deletingItem = (Bug)e.DataItem;
                var items0 = (from m in db.BugHandleHistory where m.BugID == deletingItem.id
                select new BugHandleHistory
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
                var items1 = (from m in db.BugImages where m.BugID == deletingItem.id
                select new BugImages
                {
                    id = m.id
                }
                );
                while(true)
                {
                    var data2del = items1.Take(100).ToList();
                    if(data2del.Count() ==0)
                        break;
                    foreach (var t in data2del)
                    {
                        t.ChangedProperties.Clear();
                        db.Delete(t);
                    }
                }
            }
            if (e.DataItem is DBTable)
            {
                var deletingItem = (DBTable)e.DataItem;
                var items0 = (from m in db.IDXIndex where m.TableID == deletingItem.id
                select new IDXIndex
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
                var items1 = (from m in db.DBDeleteConfig where m.TableID == deletingItem.id
                select new DBDeleteConfig
                {
                    id = m.id
                }
                );
                while(true)
                {
                    var data2del = items1.Take(100).ToList();
                    if(data2del.Count() ==0)
                        break;
                    foreach (var t in data2del)
                    {
                        t.ChangedProperties.Clear();
                        db.Delete(t);
                    }
                }
                var items2 = (from m in db.DBDeleteConfig where m.RelaTableID == deletingItem.id
                select new DBDeleteConfig
                {
                    id = m.id
                }
                );
                while(true)
                {
                    var data2del = items2.Take(100).ToList();
                    if(data2del.Count() ==0)
                        break;
                    foreach (var t in data2del)
                    {
                        t.ChangedProperties.Clear();
                        db.Delete(t);
                    }
                }
                var items3 = (from m in db.DBColumn where m.TableID == deletingItem.id
                select new DBColumn
                {
                    id = m.id
                }
                );
                while(true)
                {
                    var data2del = items3.Take(100).ToList();
                    if(data2del.Count() ==0)
                        break;
                    foreach (var t in data2del)
                    {
                        t.ChangedProperties.Clear();
                        db.Delete(t);
                    }
                }
                var items4 = (from m in db.TableInModule where m.TableID == deletingItem.id
                select new TableInModule
                {
                    id = m.id
                }
                );
                while(true)
                {
                    var data2del = items4.Take(100).ToList();
                    if(data2del.Count() ==0)
                        break;
                    foreach (var t in data2del)
                    {
                        t.ChangedProperties.Clear();
                        db.Delete(t);
                    }
                }
                var items5 = (from m in db.classproperty where m.tableid == deletingItem.id
                select new classproperty
                {
                    id = m.id
                }
                );
                while(true)
                {
                    var data2del = items5.Take(100).ToList();
                    if(data2del.Count() ==0)
                        break;
                    foreach (var t in data2del)
                    {
                        t.ChangedProperties.Clear();
                        db.Delete(t);
                    }
                }
            }
            if (e.DataItem is DBColumn)
            {
                var deletingItem = (DBColumn)e.DataItem;
                var items0 = (from m in db.DBDeleteConfig where m.RelaColumID == deletingItem.id
                select new DBDeleteConfig
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
            if (e.DataItem is InterfaceModule)
            {
                var deletingItem = (InterfaceModule)e.DataItem;
                var items0 = (from m in db.InterfaceInModule where m.ModuleID == deletingItem.id
                select new InterfaceInModule
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
                var items1 = (from m in db.InterfaceModulePower where m.ModuleID == deletingItem.id
                select new InterfaceModulePower
                {
                    id = m.id
                }
                );
                while(true)
                {
                    var data2del = items1.Take(100).ToList();
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
            modelBuilder.Entity<Project>().HasKey(m => m.id);
            modelBuilder.Entity<Databases>().HasKey(m => m.id);
            modelBuilder.Entity<User>().HasKey(m => m.id);
            modelBuilder.Entity<DBPower>().HasKey(m => m.id);
            modelBuilder.Entity<Bug>().HasKey(m => m.id);
            modelBuilder.Entity<DBTable>().HasKey(m => m.id);
            modelBuilder.Entity<DBColumn>().HasKey(m => m.id);
            modelBuilder.Entity<ProjectPower>().HasKey(m => m.id);
            modelBuilder.Entity<DBModule>().HasKey(m => m.id);
            modelBuilder.Entity<DBDeleteConfig>().HasKey(m => m.id);
            modelBuilder.Entity<TableInModule>().HasKey(m => m.id);
            modelBuilder.Entity<IDXIndex>().HasKey(m => m.id);
            modelBuilder.Entity<BugHandleHistory>().HasKey(m => m.id);
            modelBuilder.Entity<BugImages>().HasKey(m => m.id);
            modelBuilder.Entity<DLLImport>().HasKey(m => m.id);
            modelBuilder.Entity<InterfaceModule>().HasKey(m => m.id);
            modelBuilder.Entity<InterfaceInModule>().HasKey(m => m.id);
            modelBuilder.Entity<InterfaceModulePower>().HasKey(m => m.id);
            modelBuilder.Entity<classproperty>().HasKey(m => m.id);
            modelBuilder.Entity<DesignHistory>().HasKey(m => m.id);
            modelBuilder.Entity<SysLog>().HasKey(m => m.id);
        }
        System.Linq.IQueryable<Project> _Project;
        /// <summary>项目</summary>
        public virtual System.Linq.IQueryable<Project> Project
        {
            get
            {
                if (_Project == null)
                {
                    _Project = this.Set<Project>();
                }
                return _Project;
            }
        }
        System.Linq.IQueryable<Databases> _Databases;
        /// <summary>数据库</summary>
        public virtual System.Linq.IQueryable<Databases> Databases
        {
            get
            {
                if (_Databases == null)
                {
                    _Databases = this.Set<Databases>();
                }
                return _Databases;
            }
        }
        System.Linq.IQueryable<User> _User;
        /// <summary>系统用户</summary>
        public virtual System.Linq.IQueryable<User> User
        {
            get
            {
                if (_User == null)
                {
                    _User = this.Set<User>();
                }
                return _User;
            }
        }
        System.Linq.IQueryable<DBPower> _DBPower;
        /// <summary>数据库权限</summary>
        public virtual System.Linq.IQueryable<DBPower> DBPower
        {
            get
            {
                if (_DBPower == null)
                {
                    _DBPower = this.Set<DBPower>();
                }
                return _DBPower;
            }
        }
        System.Linq.IQueryable<Bug> _Bug;
        /// <summary>错误报告</summary>
        public virtual System.Linq.IQueryable<Bug> Bug
        {
            get
            {
                if (_Bug == null)
                {
                    _Bug = this.Set<Bug>();
                }
                return _Bug;
            }
        }
        System.Linq.IQueryable<DBTable> _DBTable;
        /// <summary>数据表</summary>
        public virtual System.Linq.IQueryable<DBTable> DBTable
        {
            get
            {
                if (_DBTable == null)
                {
                    _DBTable = this.Set<DBTable>();
                }
                return _DBTable;
            }
        }
        System.Linq.IQueryable<DBColumn> _DBColumn;
        /// <summary>字段</summary>
        public virtual System.Linq.IQueryable<DBColumn> DBColumn
        {
            get
            {
                if (_DBColumn == null)
                {
                    _DBColumn = this.Set<DBColumn>();
                }
                return _DBColumn;
            }
        }
        System.Linq.IQueryable<ProjectPower> _ProjectPower;
        /// <summary>项目权限</summary>
        public virtual System.Linq.IQueryable<ProjectPower> ProjectPower
        {
            get
            {
                if (_ProjectPower == null)
                {
                    _ProjectPower = this.Set<ProjectPower>();
                }
                return _ProjectPower;
            }
        }
        System.Linq.IQueryable<DBModule> _DBModule;
        /// <summary>数据模块</summary>
        public virtual System.Linq.IQueryable<DBModule> DBModule
        {
            get
            {
                if (_DBModule == null)
                {
                    _DBModule = this.Set<DBModule>();
                }
                return _DBModule;
            }
        }
        System.Linq.IQueryable<DBDeleteConfig> _DBDeleteConfig;
        /// <summary>级联删除</summary>
        public virtual System.Linq.IQueryable<DBDeleteConfig> DBDeleteConfig
        {
            get
            {
                if (_DBDeleteConfig == null)
                {
                    _DBDeleteConfig = this.Set<DBDeleteConfig>();
                }
                return _DBDeleteConfig;
            }
        }
        System.Linq.IQueryable<TableInModule> _TableInModule;
        /// <summary>TableInModule</summary>
        public virtual System.Linq.IQueryable<TableInModule> TableInModule
        {
            get
            {
                if (_TableInModule == null)
                {
                    _TableInModule = this.Set<TableInModule>();
                }
                return _TableInModule;
            }
        }
        System.Linq.IQueryable<IDXIndex> _IDXIndex;
        /// <summary>唯一值索引</summary>
        public virtual System.Linq.IQueryable<IDXIndex> IDXIndex
        {
            get
            {
                if (_IDXIndex == null)
                {
                    _IDXIndex = this.Set<IDXIndex>();
                }
                return _IDXIndex;
            }
        }
        System.Linq.IQueryable<BugHandleHistory> _BugHandleHistory;
        /// <summary>Bug处理历史记录</summary>
        public virtual System.Linq.IQueryable<BugHandleHistory> BugHandleHistory
        {
            get
            {
                if (_BugHandleHistory == null)
                {
                    _BugHandleHistory = this.Set<BugHandleHistory>();
                }
                return _BugHandleHistory;
            }
        }
        System.Linq.IQueryable<BugImages> _BugImages;
        /// <summary>Bug附带截图</summary>
        public virtual System.Linq.IQueryable<BugImages> BugImages
        {
            get
            {
                if (_BugImages == null)
                {
                    _BugImages = this.Set<BugImages>();
                }
                return _BugImages;
            }
        }
        System.Linq.IQueryable<DLLImport> _DLLImport;
        /// <summary>引入的dll</summary>
        public virtual System.Linq.IQueryable<DLLImport> DLLImport
        {
            get
            {
                if (_DLLImport == null)
                {
                    _DLLImport = this.Set<DLLImport>();
                }
                return _DLLImport;
            }
        }
        System.Linq.IQueryable<InterfaceModule> _InterfaceModule;
        /// <summary>接口设计的目录结构</summary>
        public virtual System.Linq.IQueryable<InterfaceModule> InterfaceModule
        {
            get
            {
                if (_InterfaceModule == null)
                {
                    _InterfaceModule = this.Set<InterfaceModule>();
                }
                return _InterfaceModule;
            }
        }
        System.Linq.IQueryable<InterfaceInModule> _InterfaceInModule;
        public virtual System.Linq.IQueryable<InterfaceInModule> InterfaceInModule
        {
            get
            {
                if (_InterfaceInModule == null)
                {
                    _InterfaceInModule = this.Set<InterfaceInModule>();
                }
                return _InterfaceInModule;
            }
        }
        System.Linq.IQueryable<InterfaceModulePower> _InterfaceModulePower;
        /// <summary>InterfaceModule权限设定表</summary>
        public virtual System.Linq.IQueryable<InterfaceModulePower> InterfaceModulePower
        {
            get
            {
                if (_InterfaceModulePower == null)
                {
                    _InterfaceModulePower = this.Set<InterfaceModulePower>();
                }
                return _InterfaceModulePower;
            }
        }
        System.Linq.IQueryable<classproperty> _classproperty;
        public virtual System.Linq.IQueryable<classproperty> classproperty
        {
            get
            {
                if (_classproperty == null)
                {
                    _classproperty = this.Set<classproperty>();
                }
                return _classproperty;
            }
        }
        System.Linq.IQueryable<DesignHistory> _DesignHistory;
        public virtual System.Linq.IQueryable<DesignHistory> DesignHistory
        {
            get
            {
                if (_DesignHistory == null)
                {
                    _DesignHistory = this.Set<DesignHistory>();
                }
                return _DesignHistory;
            }
        }
        System.Linq.IQueryable<SysLog> _SysLog;
        public virtual System.Linq.IQueryable<SysLog> SysLog
        {
            get
            {
                if (_SysLog == null)
                {
                    _SysLog = this.Set<SysLog>();
                }
                return _SysLog;
            }
        }
        protected override string GetDesignString()
        {
            var result = new StringBuilder();
            result.Append("\r\n");
            result.Append("H4sIAAAAAAAACu1dW3MTRxb+Ky7tq9lopJFGosgDtpeNN4RQmKRSFadSgzW2tZFHjmYU4wKqzC5gAwsyYC+3hFswsGQjE0LASAT+jGYk/Yv0dOvS3TPSjC49age9gKfnov66T3/ndPc5p08EjsrHUooW2PvlCfTnIXlBCewNTH2bSupKYDRwJL2E7k7qygL6q/ZIMgFu");
            result.Append("fy6nsuBCiMROjTbu6MuLSvNeYDyjyLoCv75/Rk+m1QD27Exa1RVVxx4/MY1qMh3YC/5MJsD/4dHpwIy8aL0LrqYD1fuvy7fz0wFQbH0Glh3OpP+pzOiwcELW5WOypkxOgFshUJA8mJ75BvwdPAUuxtOp7IKqgcsv6z8gBKlfsErxr9euJ7X9WT09qc5klAVQa3BLz2QV");
            result.Append("65uyeiibSjULEseOglZA71oPgqK/qdmFCWU2qaJiWJZQZuVsSofYm6UQP6y8hXxSO/wxvKh9Op1JKBlYbQCngUCgEKCq4xgaJXYUs3JKc4PxnZyZmZcz/YFS/8EmFuHUV9YDE1+Mp9XZ5BzsHlQCgeIyk6h1Ly6BYhi8b0nrlA6EDTWNm8zGWcqsSPWHufnMvJQ3CtfI");
            result.Append("TqmLqtal4IY4FVzRs+A6D27rPYfhXSvuToL7hscuvSEMkMj7SHTBEsawRFpJcfmXonHnIgmrXsV+dxBQRlNK5jslM/LhCKC5aRVpJ3AFpH9a/WQZXH8YdoYudIZdxLBHKeyVd3fMy1vGz9fL/39U2nlOYgecMKVnGHZqSlHn9Hl4HQoG+9HPEQyrRGFNpFLljbvm2rr5");
            result.Append("39VS8aXx8DXV1anUYdmqjh94hf7gjWJ4YzRtIp4lmLNR5APbBDuDImFQ4g50M7UozzhwTrOYS+KJ4ahoWMbGdmlnxby3Wn5YoJXD37Mt1VynsDCxi3TYKf5bMNEgSwuG5v7yr8Vy8W5544m59ops/880JdOd/RLi1fCOeLVfQrThXXl8tXKe0g5H0lbL9ptKjDcrRu5K");
            result.Append("qVAwrtyoq0Yj/wB0T/mnbfO3i5XtzebNffvg/YYCr+TfVvL3jZ01dDM0cnIE/571bDl/v7x+Dr3v8N5J6z1gvU2ryGQrF3PgcfQ50fa5jhvdaYA1Wz3Eu5EV8WwwhmgL+LCsaUvgUcoCxku5xBT2nwAFlgRIG4CNMWD+8O/qzXVqIjd2OL3UNQ2KnNJg1DMN2rRFCz0x");
            result.Append("sBlc1Du52Dreob+bvd1fTs89rWwXAYkCxTitlt7lzY3XFqX2AyLOOVIr2aZtK1yWeey3AZBOiCXp0B1T3bhZ2d42L2wZVy6QPTOWneuSbWKcso3kmW3o2QGYF1Qf3CBRHE3qPVldvWpLyTPhhGkr2MytlwoPKytn6dE4lT22kNQHyqOSZ5IJC46wzOsvq9dfOOE6muzJ");
            result.Append("ZAPjW9Hrn2AADVsYC9P2p/HwDDB+HXrsI1lNpPjtLmzBK0zboeb3K8b6ZSN3qfpozanTDsqaDsiH5y7D17hsiz6oz4z8f8ztTSd8B5JqUpvvp1Biqwud4cDXrkS6n4zfrxnnL5UvvDRXTlPDCii1rNZ/yUMDuVy8SU1BodGCBAbcbLBYY3KKNbi1ettbm0j+a/4wS80f");
            result.Append("czTJKvef0BON2ovdqP4wrxONmFfVH6YnGo2/cRh44WC0f8y79o/yvpYR867yafOVmj7wM62Iedf1vG8WuEAR/edJkSVP2nYGfr5u5n+jSRKRX5csGeeUJeNeWVIM8sGSXexaxj0Tp8i9z0vcM3GK9ISisvrUeHCnuvmOBGTD0TW2Y8keKCfumT3t1mpuu1TcKu0Uyv8r");
            result.Append("kNgatfUHk9AZJmymJLZ07kFcxMQ5grEsYjMl0WZi/cUJEd94sBmTSBtY1s8Z+Vul1+dJRHg1/KDEcH8oEfMGEG3Lh5vvjMIjEma9Brx2HeYHINK2V7V4o5J/aKy8oUYX8Yu7SJ3FMay01dH4CDHqsEL/rWY35YyZIBHbouaNbWP9Efi3tFOsblDuu/WPcqnOBMzoiNBG");
            result.Append("R+MxHA5eOIAZgRuekP9TggjLKYHNf7uxduK0dQff7mG3NsKr0y1sBk/zgwhtlWFbCnxs15JY2s4DIiL/xOkGZwAjMsp0RNI0iXyEHHfSkWt3LwMywuuAFDwPSNpGJR3eufGDJxG1H5YS9xwjcDcoJaaDMuyoJs0n940frtMLaJ+kE9lutxmiAq8D0nNAVZR7D0MSS9uh");
            result.Append("GA3vlmV5N1DYklmUVvuT2oF0CjxIzyyw0gHMLdwQYWtlUVqPLcoZUEu6k4hS/rpoAPsNTKNPbQFd5cLjyukNY+1e9eZDmjUnlJSiKwh5l9wZ5ZU7Pcf0RaVdYJC7RnM04dgcR87+CgQAzC5pSEeUlMw3LJw/6fWmRu2/nlC0mRbImvcGpPW8Ry9KQcd+q63R3zrj1Htw");
            result.Append("QHLbe9gehESbWI3aqy26j77JZ/9F/NceTOPAbSG0qClUzLymGBG/1bH2kLhdm/IcUSWFd4H28B6qJNHmA+peGg9Ryh8gTG9ItEQfJ5Ec5xQCrhloG2uZhLDMKQSc/2kbq7TzAjixGrkb1dUciWY2Jc8NkvEjnneeJdrSsqoesqMJcQwn6rsCk5iGAdvyIKBIbLAFW37x");
            result.Append("wHizSc22J76YVBPK8S61F6+OV7ARPGmvWHAXaC/vsXAx2sz8WFnWSCyNEm4SQbjhw7O12FaR0DY1EnJHCdc+U5Pf9uRmUF9Pol3fXV2U3IDhqVtsi0kIWOX0rfIZyiFkUhtPZTVdySgJP1AFO0Tl/4KSxDSu2BYrO5adq4VGXD5n5J5X8s+M3ympA4+gSKKPkpqezix3");
            result.Append("m+dM6o5N653iQEDNfoS4PGYrC1Pg+sKFrUNJyKq1ZTzB5t1n5K6Y91YdYrv6tnnVfcWJxFO0XBnnzhp5KnNPTUJ7qfSCPKf0WG2cqGzueaC9Lc8NpzBBRU34E4/lBmAAnMQ07NgW8w7GZPXmGWPnkbn21Lj91sZGk5YUdJu1Di4CMaMhz7aaAG15P2nIu+ElQK3o97Dt");
            result.Append("wHCy5QwzL181CtTUE7464Eb1P0OAxDRO0Ja7AZioxtktsKCdqDnPN/ejDh6cXFhMZ/Rux2mc5Tj1HO4v2NJEAaQoM13l1bbx9gyJerF/mekcHH5bAWg/mmGaKCZePG0Ghmuovu8Dg2lgmC2vhZWuMfcjylgFhgfwPwN2dbl4zbxDScwk+HpmVp5Rell6F2BSKmajxXOE");
            result.Append("rAATSvkta97jXQWY7YiFF0275H1uFcRVGzT+mg3YP3eLjqfGbrXG7WiYk4KF20vfa41nWbVF1xqvnpeLucqDn8y766VCobpx2sjfIoerNRDh3CsxUJn2f6tSYupFD7Pq2Dmxtw1JAZpnzFjRc0SsALUhi+22NgLkPZZVgMLW1z207utFcGG83xtj3deLYLsgo1hEh/Vm");
            result.Append("7zUkmI0w+f6hpVVr8PRQS1053msL4vlwYL6fRv2WkomerObeexfPcQPzlDTqNq8k5+b1gVbO/2QzEtP4DFvKMcoERoEawHQGyteWgoZ6tofIDQGmc2GlHSBIj6cQRH1e2CXr1l43hP1XXW7V83/uGGO6yw3XzpurbSlZ0xYz6UUlo3e51QKWCxhKNqyut1SJQUINARQp");
            result.Append("0BNMZYesnJNo+y477fbz5mV1riPZ+TSVgI+3EZdDypLbI2l9XskMBadzwVGVJcdmI6a/qj+rCm41xlPuBgl7azadAVaN+o2y/DUPYxIzrUNBsUVFZ2CzD7immIkdCpLWgwYqmAJrXb3lNOp62cOt5mi9YAbyTQITYas0AeNG6FJfOTLElCPxtIU2dsRvtuLFYepCvzwc");
            result.Append("h6kL+Y6RfN9SF5IKn2sObbfxO7Qzd88EBavd0KwcmpW7wKzcRSTZzglgSJJDkmQ19yZ2mX+5Y648tpxCYGbLUnGr+q8fSztPofppFu8UjNVzpL00nLi/pxP3XcSwEbZTeeLMavtknrjdajo/PLl6eHL18OTq4cnVjiscw5OrCf0zPLl6eHI1+5Ord419A7bWmboCEK4g");
            result.Append("E4oGdr96iroMBVm6QMLqenQFIPxIGjgYL7Z1cPBqkHCCRP034OoRs8j4IHwO3WqIb93CNMONGo73HDTmweXQrXq+R0qCBWWm9EAMoqll7WC623NYYf5ZZrzg/TRVOBfznRe8R0+FyHBxH1z/3SqHswIZzOpPeLJb/QhOiAyEtbwfPBoiQ139YS3Xg0N9Zy3W/hfYiWgO");
            result.Append("DhjY3RYOVvaA3/qBHhNgAS+TXEiqMjCSnI5jm9SIRwaxxucWioGihLm1eMO8C4ct9vLF2/LGXbCMXltMX79EisS45RDJJvZPCHXa+SLfnc92w8x2/oJNAGxPOAuBEIc+z4QUoDCCx1cr55+T/X8k3cHh7O0MHlSgggftTiiH0qpSP5b4gw/Mu1vl7y+CPz9PKkvWmcT7");
            result.Append("9tXulN7lzY3XlftPasHB01bIW3J2GT0kjJwcsV7pjniQTd5OV486EGSzEZrjBILkW1IjfEsq120XZUzxbZoNv9lybAt/5rGNnkWH1Bhrt41ioVQogLJPl1Qlgx4NnUQvdkkDcMtzMDTQ6EQpbst253QgT0MWeptete0+I/e0sl2sdyDqJKudydatvevmEenauKHOG9fX");
            result.Append("sS/9eXjzfbEG2DMGA8PBV6FucyoHOiaj42W45iqGdYXxFDsQcb4nXihXif04dOS/ZJx9Un78zGHidQAIRB8nX22GXa3MvjtFyX/tOe9ztshgzObR5vojWKJkIHHwDFXO3CNbTfqpYyRrfdj4UKJ/R010JGEt8gTbRCwEz8Vql5KgIxkbtQ0uvkQPXNZ+CQof+hsXQWvl");
            result.Append("32pocA32NYCU/hVE+kdF+EvE0zWxtD8/pWeS6pz9haZsen+HANWyal9ZsKy7U4pee1E6II6LY9Hgnkh0bGyPOBYL7dkfDwt74uC87TExKEX2S9HAqT8Al7IT4PykAAA=");
            return result.ToString();
        }
    }
}

/*<design>
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
<design>*/

