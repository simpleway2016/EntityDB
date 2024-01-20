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

namespace TestDB
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [TableConfig]
    [Table("user")]
    public class User :Way.EntityDB.DataItem
    {
        System.Nullable<Int32> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
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
        String _Name;
        /// <summary>
        /// 姓名
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "姓名")]
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
                    SendPropertyChanging("Name",_Name,value);
                    _Name = value;
                    SendPropertyChanged("Name");
                }
            }
        }
        System.Nullable<DateTime> _Birthday;
        /// <summary>
        /// 生日
        /// </summary>
        [Display(Name = "生日")]
        [Column("birthday")]
        public virtual System.Nullable<DateTime> Birthday
        {
            get
            {
                return _Birthday;
            }
            set
            {
                if ((_Birthday != value))
                {
                    SendPropertyChanging("Birthday",_Birthday,value);
                    _Birthday = value;
                    SendPropertyChanged("Birthday");
                }
            }
        }
        System.Nullable<Int32> _IntColumn1;
        [Column("intcolumn1")]
        public virtual System.Nullable<Int32> IntColumn1
        {
            get
            {
                return _IntColumn1;
            }
            set
            {
                if ((_IntColumn1 != value))
                {
                    SendPropertyChanging("IntColumn1",_IntColumn1,value);
                    _IntColumn1 = value;
                    SendPropertyChanged("IntColumn1");
                }
            }
        }
    }
    [TableConfig( AutoSetPropertyNameOnInsert = "Type" , AutoSetPropertyValueOnInsert=(Log_TypeEnum)0)]
    [Table("log")]
    public class Log :Way.EntityDB.DataItem
    {
        System.Nullable<Int32> _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
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
        System.Nullable<Int32> _UserId;
        /// <summary>
        /// 用户id
        /// </summary>
        [Display(Name = "用户id")]
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
        /// <summary>
        /// 时间
        /// </summary>
        [Display(Name = "时间")]
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
        Log_TypeEnum? _Type;
        [Column("type")]
        public virtual Log_TypeEnum? Type
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
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
    public enum Log_TypeEnum:int
    {
        /// <summary>
        /// 系统日志
        /// 增加日志
        /// </summary>
        SysLog = 1,
        /// <summary>
        /// 用户日志
        /// </summary>
        UserLog = 1<<1,
        /// <summary>
        /// 管理员日志
        /// </summary>
        AdminLog = 1 << 2
    }
    [TableConfig(AutoSetPropertyNameOnInsert = "Type" , AutoSetPropertyValueOnInsert=Log_TypeEnum.SysLog)]
    public class SysLog :Log
    {
        String _SystemPath;
        [MaxLength(50)]
        [Column("systempath")]
        public virtual String SystemPath
        {
            get
            {
                return _SystemPath;
            }
            set
            {
                if ((_SystemPath != value))
                {
                    SendPropertyChanging("SystemPath",_SystemPath,value);
                    _SystemPath = value;
                    SendPropertyChanged("SystemPath");
                }
            }
        }
        String _SysId;
        [MaxLength(50)]
        [Column("sysid")]
        public virtual String SysId
        {
            get
            {
                return _SysId;
            }
            set
            {
                if ((_SysId != value))
                {
                    SendPropertyChanging("SysId",_SysId,value);
                    _SysId = value;
                    SendPropertyChanged("SysId");
                }
            }
        }
    }
    [TableConfig(AutoSetPropertyNameOnInsert = "Type" , AutoSetPropertyValueOnInsert=Log_TypeEnum.UserLog)]
    public class UserLog :Log
    {
        String _PeopleName;
        [MaxLength(50)]
        [Column("peoplename")]
        public virtual String PeopleName
        {
            get
            {
                return _PeopleName;
            }
            set
            {
                if ((_PeopleName != value))
                {
                    SendPropertyChanging("PeopleName",_PeopleName,value);
                    _PeopleName = value;
                    SendPropertyChanged("PeopleName");
                }
            }
        }
    }
    [TableConfig(AutoSetPropertyNameOnInsert = "Type" , AutoSetPropertyValueOnInsert=Log_TypeEnum.AdminLog)]
    public class AdminLog :Log
    {
        String _AdminId;
        [MaxLength(50)]
        [Column("adminid")]
        public virtual String AdminId
        {
            get
            {
                return _AdminId;
            }
            set
            {
                if ((_AdminId != value))
                {
                    SendPropertyChanging("AdminId",_AdminId,value);
                    _AdminId = value;
                    SendPropertyChanged("AdminId");
                }
            }
        }
    }
}

namespace TestDB.DB
{
    public class Test : Way.EntityDB.DBContext
    {
         public Test(string connection, Way.EntityDB.DatabaseType dbType): base(connection, dbType)
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
             var db =  sender as TestDB.DB.Test;
            if (db == null) return;
            if (e.DataItem is User)
            {
                var deletingItem = (User)e.DataItem;
                var items0 = (from m in db.Log where m.UserId == deletingItem.id
                select new Log
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
            modelBuilder.Entity<User>().HasKey(m => m.id);
            modelBuilder.Entity<Log>().HasKey(m => m.id);
            modelBuilder.Entity<Log>().HasDiscriminator<Log_TypeEnum?>("Type")
            .HasValue<Log>((Log_TypeEnum)0)
            .HasValue<SysLog>(Log_TypeEnum.SysLog)
            .HasValue<UserLog>(Log_TypeEnum.UserLog)
            .HasValue<AdminLog>(Log_TypeEnum.AdminLog)
            ;
        }
        System.Linq.IQueryable<User> _User;
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
        System.Linq.IQueryable<Log> _Log;
        public virtual System.Linq.IQueryable<Log> Log
        {
            get
            {
                if (_Log == null)
                {
                    _Log = this.Set<Log>();
                }
                return _Log;
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
        System.Linq.IQueryable<UserLog> _UserLog;
        public virtual System.Linq.IQueryable<UserLog> UserLog
        {
            get
            {
                if (_UserLog == null)
                {
                    _UserLog = this.Set<UserLog>();
                }
                return _UserLog;
            }
        }
        System.Linq.IQueryable<AdminLog> _AdminLog;
        public virtual System.Linq.IQueryable<AdminLog> AdminLog
        {
            get
            {
                if (_AdminLog == null)
                {
                    _AdminLog = this.Set<AdminLog>();
                }
                return _AdminLog;
            }
        }
        protected override string GetDesignString()
        {
            var result = new StringBuilder();
            result.Append("\r\n");
            result.Append("H4sIAAAAAAAAC+1YUW/aMBD+K8jPMBFICCD2wGAP0SqGBpsmNX0wsQPREqdzTBFC/PednQScQikUqDqJJ+y7s/Pdl/vODis0xpOQJqh9v0qHAxxR1Eajv+GI8ifKURn9iBdpgCNolI6yqICA+xcO5zAx1uWNXSwf6daDepxiQdX2XU8EMUNarBczQZnQwlduCsVFbRgG");
            result.Append("BH4Ns+yqBTB20c+EcheBpY8FnuCEOn2wy5DgLvb+wLi6hkkvDucRS2B6n+/TMPR9pAnmTtKdi9hhHqcRIAGX4HMqN8BsMA9DMPg4TKSFTMaQWrpYRpYzqAqAAukkw29qlu0Rc0K5enZVeftB4vEgChgWMc93BjpygDUdYDrYCzFHtMWYPVCD+IS5N8MpVSFlUzFTZqt6");
            result.Append("EHi+8xa5cRTyuo78S8DFjODlGegJ1IwIMgI0+CeCrx0FvlBfDhNp7RhnwM/r4+3I6weQP0hf/3cvZn4wVSWeWlTt6/IimUR0qZqwXKp6JIBiJZZXtF27qrYtnfu7ePpGaVvXlLZ1AWk3nvcwh1y3vqxLyNrWUY9zPV5X0tYlJN0sAFcQzib7K5tHfeoHLDWPlgnUa+lz");
            result.Append("Cah0mXyl2bTTUZYuAXiZqdTplGpnJr6/I0i4Wt4tPW9ACMIeYvm8658jr8A3X35vEkGIk0QHrlrBNjFbqkz3nyWfS+VknZdT4T4ypPFjegP7AIk1Tkgsq/xCZoWLjBLCh3hf9glp5fKFvN73vK0fOG9nmE1POm+/h2Rzsy8csQO62O9gdKEdsDJRTz2VPLMSGlKxY30/");
            result.Append("msyr0rT5ytjhaeP5X4iybkQdR1TjRtRxRNm3DnUMTc0bTZImaFdZ70OSqHSs0yW/9uRRL/95UjfWT/AN3jBVIyxEZ21+N34keMCmuwu2vf74NYWe+yK0B5mW9I6oyBbiOrGblm1WfFzzKqbX8CsT36hXbIsYfqtFfGpStP4HSuhlvXUTAAA=");
            return result.ToString();
        }
    }
}

/*<design>
H4sIAAAAAAAAC72XzW/bNhTA/xWDZ+XDH5JjJT7UUTdo/UCwZGiBqSgYiUq40KRG0m2MwIddhmHAgA4tkJ22dcCwndZDi+7QQ/+ZOcH+iz1KouQkbZI5aX2RyPf43o/vg6IPUIA13saKIP8A0QT5HQdtSPEViXUYIL/poLt4CEK0RZRGDkq2t8YZyQWx4JtagkgR+YjI
/mIUqa8Z2c8kUWp1RJO+wqvZ46TfbLU7rrdqPfV1aYqxDax3wcBBJCPeaERgPUI+PNf9CH5fgGFlXj7D8Z55KjGSMTFvkmQiF93kmupxMDj9fg+PF+140bCbyWCwLrgm+/nATC7GKkKO9a7oMGPkXIKAqD0tMru+lRsw6yewIXpbxHvIXylitpnh2AYuGID4UwgJjHE7
6a643c5CilvxQif20oXttNle6LpJM+31kpR0CJo46I5IRowo5H9ZJMatUjHElIM9G0+TJ8haqD4RLCGQkBQzRRyUYUl4nsXlyQMHbeHtWXNNWBLjTFPBweTxsz+Pvvv7n7fPj755gSpHZvtnHZXbXJ44paWa7LbYOUffQoT89N4MTCEKikGhYEZgfB/mem0HjZHfO+G1
XuKeXeJ5y8WSnnEcBvdDnpD93CmMg0FAGNEECiKlOzMk3imSzwnDJ/xUEw+hGOJq02Z6XbDR0Oh53swEt4omnGGCDI6pRCOqHXt1q8HQpPPGSIuQx5IMIY3I13IESV3H/O6IsSrJth8RBZ1T6KHauGXei5VCQnEYTxCWUAVUxZIOKcda2JKxofVas6Ux/ePp9MkPdVHk
j3fwlUAVYOG14nuEZbyLTTkxwnfytneX30dc2qqQmxcit09W8y9Hh7/XyAMq9W6Cx3NgJ1gTTfMdV9yXpW5dSN2pEEOui4pozgFZ5P5/87Uv5HOvtybdK9Skd/a4yplmjiroreuNnXuViuzOAh8dvv738FWNu1WU1LVXo3uValyp8Yy7eWN5k4+GAUkpN1NLS8cv3xy/
Mf04fXsY8aWl6W8/T7//1Y43xwrOz0a/0XSMsEisFZqkltK1tVLhr+fHT76d/viT1bmRwG5KpcbaWqM1T2ze1Qlmb1VoelVoAFiTYX5x+RCH4DmQnfclEPwxrFQNaL5IFr0LrVVL5mqRK1G781HXX8MNIuBW9sG+Ouege5dDL6u0ZodOK0V5cX78mHcvB25bJ7+RxEaQ
SZERqenM1awFmYDrHCP20sVP3A1TIQnd4Xtk/LDWgkqdmY/z75o9xIFKMAZ/L/KDsTiAHkz+A2LwqKmIDAAA
<design>*/

