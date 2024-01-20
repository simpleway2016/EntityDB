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

namespace TestDB
{
    [TableConfig]
    [Table("userinfo")]
    [Way.EntityDB.DataItemJsonConverter]
    public class UserInfo :Way.EntityDB.DataItem
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
                    SendPropertyChanging("Name",_Name,value);
                    _Name = value;
                    SendPropertyChanged("Name");
                }
            }
        }
        Way.EntityDB.Test.ExtObj _Ext;
        [Column("ext",TypeName = "jsonb")]
        public virtual Way.EntityDB.Test.ExtObj Ext
        {
            get
            {
                return _Ext;
            }
            set
            {
                    SendPropertyChanging("Ext",_Ext,value);
                    _Ext = value;
                    SendPropertyChanged("Ext");
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<UserInfo, bool>> exp)
        {
            base.SetValue<UserInfo>(exp);
        }
    }
}

namespace TestDB.DB
{
    public class TestPG : Way.EntityDB.DBContext
    {
         public TestPG(string connection, Way.EntityDB.DatabaseType dbType , bool upgradeDatabase = true): base(connection, dbType , upgradeDatabase)
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
             var db =  sender as TestDB.DB.TestPG;
            if (db == null) return;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>().HasKey(m => m.id);
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
        protected override string GetDesignString()
        {
            var result = new StringBuilder();
            result.Append("\r\n");
            result.Append("H4sIAAAAAAAACu2W3W/aMBDA/xc/w5QSPlreOuhDtImh0U2Tmj44ySV4dWxmO2MV4n/f2TEQRAdjk7rPJ3yfvvvlfGJFbmnCQZPh3ao+TmgJZEimUptCwewTJy3yVi5rj8hAWZ+8G8vQ/J7yCoWLdWurN48L2FnISAE14PJfp4ZJQRq+qRQGhGm4r+K6lpgM8cgy/O20");
            result.Append("YpLShY1FSVSco8ImQCkm7zSoSOQyJqgdU0MTqiEaoy1EBXst0wc8B2sURpJXpdAo3m1yh0dzWxeUI31dGRmJVEGJ1aLJqApsQiomGIKKnHJtNVlyi+274IQVzDqj1ut8+htRlWPImWjoOIjCzHdyBjmtuHFUdlpHxvVmmUR6+soJvhqpMlCuq8BZx0ynipVMUCNVo8YR");
            result.Append("p1r7Hm1i/B6eRvcojfrwJI9t6i0QX1ODx2eq0jlV5wKJSS9wMWch2RS0Y3Lxg0x6R5ncfDE/geSjliJ5jgk5xNE5D8e9dR9/GEmRs8K9oFrjh809Qkd53XzemX+OzVURYi67VWYG14J7mCd2S+fIbplTUZy1W97wbLvqDtfHBJZHrAKWjQ1i+0/d/dkTe+Xk1HT+hbFB");
            result.Append("1UuaPlSL+kNlUyUXoAwDywpReVf7XZTdlpRv6vIva712k5cBB7PH+RfOY/hXzuP/cTw5jp3fcx67f9I8Pjs9FP1FxPKrz02K9i+jXaMozx41gn4RCdPvuov2vD3bQ/+ZUUwUhwE7wN8fs9fTN0u7t21Z6wyMD+wEKSRXwaBNr2je7gZh0L4Ms26bdgd5P7jMe4OkT9Zf");
            result.Append("AXzJ5oL0CwAA");
            return result.ToString();
        }
    }
}

/*<design>
H4sIAAAAAAAACrWUW2+bMBTHv0p1ngkihDQtUR6W0Fbs0kVqd5GWPRgwqVtjI9t0jaJ89x3jQLqulapqgwefi/kfnx8HtpAQQzKiKcRbYAXEIw+WSt7S3KQJxEMPLkmFSbim2iwvwIMiu97UGIk8yKW4MgqTV1TdUzWLJn4YDf0w8KPxdCmVmY2jUTj9oqlKi1kttVkr
qqdLovUvqYrZmulpV3/mCkxtBc6XxNyg7nalVuLoaIVFVxDjuohXeFlBbY33JL+za0L1nZG1NZ2Mn+sVeN3TmlU1p04Bb4zusAz7KPO7tg/b4lVN8q7PZI7pi8bigDDIaXYaTAbklJSDKBgFg5NREQ1INCmPg5NyPMmOYefBJ1k0nGqIf/Qc9+QqwgTqdY1arJhM9bnk
BUV6JeGaelATRUULPdj99OCaZI/lQqRNasOkgFg0nPfqLVxRyr8r7Ps7qKXi6SHDLpO0tkvvH3/Atz8JPNjgGqJfcrLuals7dI5VT5PvqSjoQ6uMfjJPKKeGLqQo2boPLiRvKvEHoud7wqQF9K4xMhW5ohWCgdioBjEtiLjEvT22bhwhY2uG27Cj1neCZ6KpEloy0Uc4
FWs7W84raEkabr4S3vQ7HhNJ9fKDNV1tnFmq7MkRS6oTpnPFKiaIkYfXuOA43a4PJ9cGztE8BHeeAxC9BKBdnkHQ1egYuGP1CO6Jym+IejUDGAfwegb74j2E4b+BMH4JwtmDeQODWy1F9p+m4CmBNvU2AvCNbPwzYZjZJHPf/nR87Pdzdgv2g8rt5lrJmirD3Fe1+w0C
kolwqwUAAA==
<design>*/

