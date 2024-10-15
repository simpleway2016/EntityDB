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

namespace TestTimeZone.DBModels
{
    [TableConfig]
    [Table("userinfo")]
    [Way.EntityDB.DataItemJsonConverter]
    public partial class UserInfo :Way.EntityDB.DataItem
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
        DateTime _CreateTime;
        [DisallowNull]
        [Column("createtime",TypeName = "datetimezone")]
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
                    SendPropertyChanging("CreateTime",_CreateTime,value);
                    _CreateTime = value;
                    SendPropertyChanged("CreateTime");
                }
            }
        }
        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param>指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
         public virtual void SetValue(System.Linq.Expressions.Expression<Func<UserInfo, bool>> exp)
        {
            base.SetValue<UserInfo>(exp);
        }
    }
}

namespace TestTimeZone.DBModels.DB
{
    public class Test : Way.EntityDB.DBContext
    {
         public Test(string connection, Way.EntityDB.DatabaseType dbType , bool upgradeDatabase = true): base(connection, dbType , upgradeDatabase)
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
             var db =  sender as Test;
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
            result.Append("H4sIAAAAAAAAA61UUW/aMBD+L36GCVBKB28VbFK0iVWjnSY1PBzOBaw5dms7Qx3iv+/sGBJGx4bEU3zf3X25+86+LXuApUTLxk/b+jiDEtmY3WvrVgbnL5J12Fe9qSNSh2V9imEiJ/c3kBUZ/V3ngLvXZ2w8bGIQHAb+O+6EVqwVy7VyqFwrfJvVtWRsTEeR07ef9DoZ");
            result.Append("4/Dss8lWlZQEeAqyMvZo0aSq0BkjdAoOlmAxnfrMW0LEZ81/kNHbkTHRsiqVzXwje/rhzfuz/D6K7NTeVU6nihssqWZyOVOh5wQ1oxQCCpDWI/nygUQIyUuxEj6Y0IhF+g+qKqdYCNXCJKqVWzd2jgVU0gVtGjToU/cXlEnt/adgxnq0ydGE1mrvVFhuRCkUOG1aVU4k");
            result.Append("WBu7jNQB+kjnFkzjapQanVWqPryp1eGnB7FitS2tfoLhazCXipWxm17IuVCufUmNXv1r6zU8f3Pj2xAXqPbGFcuJwxHHL63wcu2uo9zgKsotPMv0+0SrQqzCI62ReJXDQw/C7tpLJI9P/mgh3RKZX15zR+qEx/+PFTY4s8LWoFYXrbAvMj9s1NMdNcPNGa/CTWtLeQF4");
            result.Append("+H/+B5qjRHeCHsu39VN5VOIljHU/jtROZGUdGszbQwpEvqaQenw7F821bU3qGnMhM7YQJtNUsZ+P3+j+RpM9f6Wqy3epcsMk/OkoOk7tNH7ujFCr04RmdP+fc9TUX0tb+La8d45u/7PBaIl9zrsw6hfdJBlAF4YcuhyS4QgSzoFztvsN0VhuCpUHAAA=");
            return result.ToString();
        }
    }
}

/*<design>
H4sIAAAAAAAAA62UUW/aMBDHv0rl54BIGmgJ4mEk7RR1q5Cg27TRB+NcqDfHzmynK6v47jsnJG1Rq3XqyEN8Z+f/P//s454k1NI1NUCie8IzEvknHplr9R2YTRMMPXJJC5wlSzCWeCRbL7clxqFHmJILq3FqAfoW9NQPTvoDfPzJXGk79YfhcTC5MqDTbFoqYzcazGRO
jfmldDbdcDNpzacWxSdOXYg5tTeoeb+SR0crtFuRCN9xtMIfUxkYNziTltttMnPjz3Tbb+O+q9Ilk1mspIW7OnDJJS/gq5LQZ2ZFvEbc8KIU0BjgI3dYAf+g2A8SBc2+FyVl7eY7hWT2EesQBle/rxwzwoLxGnzGenTs570wDGiPjhjtMRqOxjRkjDJGdh7B7yoBhkTf
GtjhaYe3oFyiYEukZo9HkZpzJTJAyjkVBjxSUg2yPprB7tojS7p+LOiHAzwXWlquJIlkJURnUB+EzNUzJvs9Pwim8rBS3w/buWRv0yxxodvGHTIbjjyyxdnBsUdyQTdtBW4cNIGzSJMvqczg7knd/uhQ/wK2uIDEGqgFB584HFeS/6ygw5GaWFTGgoZsn3MOySwBARbw
DuR842zqZKxEVchHrqPh6Uu4cN6pv6usSiXTUCB1ElldoWlM5SWu7Ypoe4Ks+YZL1yVN3AieyapIIOeyywiQG3fJmyiDnFbCfqKi6lY8JZGa+YULGndsHtCu/Hom4YZpXnBJrXq4JbHANmt20gjWiXMcPiR3Xkdh/BKF+vUMh9amBdFU1nG4pZrdUP1qEGQ4IP8CYm/f
kfD/F4nRi+1zcA3/wuPwYmT4rcVvf+P/x+upvIlJ8EYm2DHMpUutStCW1916vfsDDbleTzIGAAA=
<design>*/

