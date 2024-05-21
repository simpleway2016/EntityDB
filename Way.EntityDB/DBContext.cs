using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Way.EntityDB.DataBaseService;

namespace Way.EntityDB
{
    public enum DatabaseType:int 
    {


        /// <summary>
        /// 
        /// </summary>
        SqlServer = 1,

        /// <summary>
        /// 
        /// </summary>
        Sqlite = 2,

        /// <summary>
        /// 
        /// </summary>
        MySql = 3,
        /// <summary>
        /// 
        /// </summary>
        PostgreSql = 4,
    }
    public class DatabaseModifyEventArg
    {
        public object DataItem
        {
            get;
            internal set;
        }
    }
    public class DatabaseUpdateArg : DatabaseModifyEventArg
    {
        public object Condition
        {
            get;
            internal set;
        }
    }
   
    public class DBContext : Microsoft.EntityFrameworkCore.DbContext
    {

        #region 事件
        public delegate void DatabaseEventHandler(object sender, DatabaseModifyEventArg e);
        public delegate void DatabaseUpdateEventHandler(object sender, DatabaseUpdateArg e);
        public static event DatabaseEventHandler BeforeDelete;
        public static event DatabaseEventHandler BeforeInsert;
        public static event DatabaseUpdateEventHandler BeforeUpdate;
        public static event DatabaseEventHandler AfterDelete;
        public static event DatabaseEventHandler AfterInsert;
        public static event DatabaseUpdateEventHandler AfterUpdate;

        public event EventHandler BeforeCommitTransaction;
        public event EventHandler AfterCommitTransaction;
        public event EventHandler BeforeRollbackTransaction;
        public event EventHandler AfterRollbackTransaction;
        #endregion

        #region 静态变量

        static List<IActionCapture> TypeCaptures = new List<IActionCapture>();

        static Dictionary<EntityDB.DatabaseType, Type> _DatabaseServiceTypes;
        static Dictionary<EntityDB.DatabaseType, Type> DatabaseServiceTypes
        {
            get
            {
                //在DBContext 静态构造函数中调用一下，进行初始化，防止多线程同时运行这里来，造成冲突
                if (_DatabaseServiceTypes == null)
                {
                    var compareType = typeof(IDatabaseService);
                    _DatabaseServiceTypes = new Dictionary<DatabaseType, Type>();
                    var types = typeof(DBContext).GetTypeInfo().Assembly.GetTypes();
                    foreach (var type in types)
                    {
                        if (type.GetInterfaces().Any(m => m == compareType))
                        {
                            var att = type.GetTypeInfo().GetCustomAttribute(typeof(EntityDB.Attributes.DatabaseTypeAttribute)) as EntityDB.Attributes.DatabaseTypeAttribute;
                            if (att != null)
                            {
                                _DatabaseServiceTypes[att.DBType] = type;
                            }
                        }
                    }
                }
                return _DatabaseServiceTypes;
            }
        }

        #endregion

        #region 属性
        public DatabaseType DatabaseType { get; set; }
        public string ConnectionString { get; internal set; }

        IDatabaseService _databaseService;
        static object GlobalLockObj = new object();
        static Dictionary<string,bool> upgradedDatabase = new Dictionary<string, bool>();
        public new IDatabaseService Database
        {
            get
            {
                return _databaseService;
            }
        }

        /// <summary>
        /// 当进行insert、update、delete等操作时，自动启动事务
        /// </summary>
        public bool AutoBeginTransaction { get; set; }

        #endregion

        bool _disposed = false;
        static DBContext()
        {
            //这两句，防止pg数据库出现以下错误：
            //Cannot write DateTime with Kind=Local to PostgreSQL type 'timestamp with time zone', only UTC is supported. Note that it's not possible to mix DateTimes with different Kinds in an array/range. See the Npgsql.EnableLegacyTimestampBehavior AppContext switch to enable legacy behavior.
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

            //在DBContext 静态构造函数中调用一下，进行初始化，防止多线程同时运行这里来，造成冲突
            var t = DatabaseServiceTypes.Count;

            //防止有些dll版本不对，无法加载
            //Way.EntityDB.PlatformHelper.setAssemblyResolve();

            
            BeforeDelete += Database_BeforeDelete;
            BeforeInsert += Database_BeforeInsert;
            BeforeUpdate += Database_BeforeUpdate;

            AfterDelete += Database_AfterDelete;
            AfterInsert += Database_AfterInsert;
            AfterUpdate += Database_AfterUpdate;
        }

        /// <summary>
        /// 注册触发器
        /// </summary>
        /// <param name="actionCapture"></param>
        public static void RegisterActionCapture(IActionCapture actionCapture)
        {
            TypeCaptures.Add(actionCapture);
        }
        /// <summary>
        /// 取消触发器
        /// </summary>
        /// <param name="actionCapture"></param>
        public static void UnRegisterActionCapture(IActionCapture actionCapture)
        {
            TypeCaptures.Remove(actionCapture);
        }

        #region 关联事件
        static void Database_AfterUpdate(object sender, DatabaseUpdateArg e)
        {
            if (e.DataItem != null)
            {
                Type dataitemType = e.DataItem.GetType();
                foreach (var capture in TypeCaptures)
                {
                    if (dataitemType.FullName == capture.DataItemType.FullName)
                    {
                        capture.AfterUpdate(  sender, e);
                    }
                }
            }
        }

        static void Database_AfterInsert(object sender, DatabaseModifyEventArg e)
        {
            if (e.DataItem != null)
            {
                Type dataitemType = e.DataItem.GetType();
                foreach (var capture in TypeCaptures)
                {
                    if (dataitemType.FullName == capture.DataItemType.FullName)
                    {
                        capture.AfterInsert(sender, e);
                    }
                }
            }
        }

        static void Database_AfterDelete(object sender, DatabaseModifyEventArg e)
        {
            if (e.DataItem != null)
            {
                Type dataitemType = e.DataItem.GetType();
                foreach (var capture in TypeCaptures)
                {
                    if (dataitemType.FullName == capture.DataItemType.FullName)
                    {
                        capture.AfterDelete( sender, e);
                    }
                }
            }
        }

        static void Database_BeforeUpdate(object sender, DatabaseUpdateArg e)
        {
            if (e.DataItem != null)
            {
                Type dataitemType = e.DataItem.GetType();
                foreach (var capture in TypeCaptures)
                {
                    if (dataitemType.FullName == capture.DataItemType.FullName)
                    {
                        capture.BeforeUpdate(sender, e);
                    }
                }
            }
        }

        static void Database_BeforeInsert(object sender, DatabaseModifyEventArg e)
        {
            if (e.DataItem != null)
            {
                Type dataitemType = e.DataItem.GetType();
                foreach (var capture in TypeCaptures)
                {
                    if (dataitemType.FullName == capture.DataItemType.FullName)
                    {
                        capture.BeforeInsert(sender, e);
                    }
                }
            }
        }

        static void Database_BeforeDelete(object sender, DatabaseModifyEventArg e)
        {

            if (e.DataItem != null)
            {
                Type dataitemType = e.DataItem.GetType();
                foreach (var capture in TypeCaptures)
                {
                    if (dataitemType.FullName == capture.DataItemType.FullName)
                    {
                        capture.BeforeDelete(sender, e);
                    }
                }
            }
        }
        #endregion

#region 动态query
#if NET46
        public static object GetQueryByString(object linqQuery, string stringQuery)
        {
            Type dataType = linqQuery.GetType().GetGenericArguments()[0];
            Type dynamicQueryableType = typeof(DynamicQueryable);
            var methods = dynamicQueryableType.GetMethods(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            foreach (System.Reflection.MethodInfo method in methods)
            {
                if (method.Name != "Where" || method.IsGenericMethod == false)
                    continue;
                System.Reflection.MethodInfo mmm = method.MakeGenericMethod(dataType);
                return mmm.Invoke(null, new object[] { linqQuery, stringQuery, null });

            }
            return linqQuery;
        }
#endif

        public static bool InvokeAny(object linqQuery, string propertyName, object value)
        {
            Type dataType = linqQuery.GetType().GetGenericArguments()[0];
            ParameterExpression param = System.Linq.Expressions.Expression.Parameter(dataType, "n");
            System.Reflection.PropertyInfo pinfo;
            System.Linq.Expressions.Expression left, right;

            left = GetPropertyExpression(param, dataType, propertyName, out pinfo);
            if (pinfo.PropertyType.GetTypeInfo().IsGenericType)
            {
                Type ptype = pinfo.PropertyType.GetGenericArguments()[0];
                left = System.Linq.Expressions.Expression.Convert(left, ptype);
                //等式右边的值
                right = System.Linq.Expressions.Expression.Constant(Convert.ChangeType(value, ptype));
            }
            else
            {
                //等式右边的值
                right = System.Linq.Expressions.Expression.Constant(Convert.ChangeType(value, pinfo.PropertyType));
            }

            System.Linq.Expressions.Expression expression = System.Linq.Expressions.Expression.Equal(left, right);
            expression = System.Linq.Expressions.Expression.Lambda(expression, param);

            Type queryableType = typeof(System.Linq.Queryable);
            var methods = queryableType.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            foreach (System.Reflection.MethodInfo method in methods)
            {
                if (method.Name == "Any" && method.IsGenericMethod)
                {
                    System.Reflection.MethodInfo mmm = method.MakeGenericMethod(dataType);
                    return (bool)mmm.Invoke(null, new object[] { linqQuery, expression });
                }
            }
            return false;
        }

        public static object InvokeWhereEquals(object linqQuery, string propertyName, object value)
        {
            Type dataType = linqQuery.GetType().GetGenericArguments()[0];
            ParameterExpression param = System.Linq.Expressions.Expression.Parameter(dataType, "n");
            System.Reflection.PropertyInfo pinfo;
            System.Linq.Expressions.Expression left, right;

            left = GetPropertyExpression(param, dataType, propertyName, out pinfo);
            if (pinfo.PropertyType.GetTypeInfo().IsGenericType)
            {
                Type ptype = pinfo.PropertyType.GetGenericArguments()[0];
                left = System.Linq.Expressions.Expression.Convert(left, ptype);
                //等式右边的值
                right = System.Linq.Expressions.Expression.Constant(Convert.ChangeType(value, ptype));
            }
            else
            {
                //等式右边的值
                right = System.Linq.Expressions.Expression.Constant(Convert.ChangeType(value, pinfo.PropertyType));
            }

            System.Linq.Expressions.Expression expression = System.Linq.Expressions.Expression.Equal(left, right);
            expression = System.Linq.Expressions.Expression.Lambda(expression, param);

            Type queryableType = typeof(System.Linq.Queryable);
            var methods = queryableType.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            foreach (System.Reflection.MethodInfo method in methods)
            {
                if (method.Name == "Where" && method.IsGenericMethod)
                {
                    System.Reflection.MethodInfo mmm = method.MakeGenericMethod(dataType);
                    return mmm.Invoke(null, new object[] { linqQuery, expression });
                }
            }
            return null;
        }

        public static object InvokeWhereWithMethod(object linqQuery,MethodInfo fieldMethod, string propertyName, object value)
        {
            Type dataType = linqQuery.GetType().GetGenericArguments()[0];
            ParameterExpression param = System.Linq.Expressions.Expression.Parameter(dataType, "n");
            System.Reflection.PropertyInfo pinfo;
            System.Linq.Expressions.Expression left, right;

            left = GetPropertyExpression(param, dataType, propertyName, out pinfo);

            if (pinfo.PropertyType.GetTypeInfo().IsGenericType)
            {
                Type ptype = pinfo.PropertyType.GetGenericArguments()[0];
                left = System.Linq.Expressions.Expression.Convert(left, ptype);
                //等式右边的值
                right = System.Linq.Expressions.Expression.Constant(Convert.ChangeType(value, ptype));
            }
            else
            {
                //等式右边的值
                right = System.Linq.Expressions.Expression.Constant(Convert.ChangeType(value, pinfo.PropertyType));
            }

            System.Linq.Expressions.Expression expression = System.Linq.Expressions.Expression.Call(left,fieldMethod, right);
            expression = System.Linq.Expressions.Expression.Lambda(expression, param);

            Type queryableType = typeof(System.Linq.Queryable);
            var methods = queryableType.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            foreach (System.Reflection.MethodInfo method in methods)
            {
                if (method.Name == "Where" && method.IsGenericMethod)
                {
                    System.Reflection.MethodInfo mmm = method.MakeGenericMethod(dataType);
                    return mmm.Invoke(null, new object[] { linqQuery, expression });
                }
            }
            return null;
        }
        internal static object InvokeToList(object linqQuery)
        {
            Type dataType = linqQuery.GetType().GetGenericArguments()[0];
            Type queryType = typeof(System.Linq.Enumerable);
            var methods = queryType.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Where(m => m.Name == "ToList");
            foreach (System.Reflection.MethodInfo method in methods)
            {
                System.Reflection.MethodInfo mmm = method.MakeGenericMethod(dataType);
                if (mmm != null)
                {
                    return mmm.Invoke(null, new object[] { linqQuery });
                }
            }
            return null;
        }

        static MethodInfo ToArrayMethod;
        public static object InvokeToArray(object linqQuery)
        {
            Type dataType = linqQuery.GetType().GetGenericArguments()[0];
            if (ToArrayMethod == null)
            {
                Type queryType = typeof(System.Linq.Enumerable);
                var methods = queryType.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Where(m => m.Name == "ToArray");
                foreach (System.Reflection.MethodInfo method in methods)
                {
                    if (method.IsGenericMethod)
                    {
                        ToArrayMethod = method;
                    }
                }
            }
            if (ToArrayMethod == null)
                throw new Exception("找不到泛型ToArray方法");

            System.Reflection.MethodInfo mmm = ToArrayMethod.MakeGenericMethod(dataType);
            if (mmm != null)
            {
                return mmm.Invoke(null, new object[] { linqQuery });
            }
            else
                throw new Exception(ToArrayMethod.Name + ".MakeGenericMethod失败，参数类型：" + dataType.FullName);
        }

        static MethodInfo ToArrayAsyncMethod;
        public static async Task<object> InvokeToArrayAsync(object linqQuery)
        {
            Type dataType = linqQuery.GetType().GetGenericArguments()[0];
            if (ToArrayAsyncMethod == null)
            {
                Type queryType = typeof(EntityFrameworkQueryableExtensions);
                var methods = queryType.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Where(m => m.Name == "ToArrayAsync");
                foreach (System.Reflection.MethodInfo method in methods)
                {
                    if (method.IsGenericMethod)
                    {
                        ToArrayAsyncMethod = method;
                    }
                }
            }
            if (ToArrayAsyncMethod == null)
                throw new Exception("找不到泛型ToArrayAsync方法");

            System.Reflection.MethodInfo mmm = ToArrayAsyncMethod.MakeGenericMethod(dataType);
            if (mmm != null)
            {
                var t = mmm.Invoke(null, new object[] { linqQuery, CancellationToken.None });
                return await (dynamic)t;
            }
            else
                throw new Exception(ToArrayMethod.Name + ".MakeGenericMethod失败，参数类型：" + dataType.FullName);
        }

        static MethodInfo CountAsyncMethod;
        public static async Task<object> InvokeCountAsync(object linqQuery)
        {
            Type dataType = linqQuery.GetType().GetGenericArguments()[0];
            if (CountAsyncMethod == null)
            {
                Type queryType = typeof(EntityFrameworkQueryableExtensions);
                var methods = queryType.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Where(m => m.Name == "CountAsync");
                foreach (System.Reflection.MethodInfo method in methods)
                {
                    if (method.IsGenericMethod && method.GetParameters().Length == 2)
                    {
                        CountAsyncMethod = method;
                    }
                }
            }
            if (CountAsyncMethod == null)
                throw new Exception("找不到泛型CountAsync方法");

            System.Reflection.MethodInfo mmm = CountAsyncMethod.MakeGenericMethod(dataType);
            if (mmm != null)
            {
                var t = mmm.Invoke(null, new object[] { linqQuery, CancellationToken.None });
                return await (dynamic)t;
            }
            else
                throw new Exception(ToArrayMethod.Name + ".MakeGenericMethod失败，参数类型：" + dataType.FullName);
        }

        static MethodInfo TakeMethod;
        public static object InvokeTake(object linqQuery, int takeSize)
        {
            Type dataType = linqQuery.GetType().GetGenericArguments()[0];
            if (TakeMethod == null)
            {
                Type queryType = typeof(System.Linq.Queryable);
                var methods = queryType.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Where(m => m.Name == "Take");
                foreach (System.Reflection.MethodInfo method in methods)
                {
                    if (method.IsGenericMethod)
                    {
                        TakeMethod = method;
                        break;
                    }
                    
                }
            }
            if (TakeMethod == null)
                throw new Exception("找不到泛型Take方法");

            System.Reflection.MethodInfo mmm = TakeMethod.MakeGenericMethod(dataType);
            if (mmm != null)
            {
                return mmm.Invoke(null, new object[] { linqQuery, takeSize });
            }
            else
                throw new Exception(TakeMethod.Name + ".MakeGenericMethod失败，参数类型：" + dataType.FullName);
        }

        static MethodInfo SkipMethod;
        public static object InvokeSkip(object linqQuery, int skip)
        {
            Type dataType = linqQuery.GetType().GetGenericArguments()[0];
            if (SkipMethod == null)
            {
                Type queryType = typeof(System.Linq.Queryable);
                var methods = queryType.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Where(m => m.Name == "Skip");
                foreach (System.Reflection.MethodInfo method in methods)
                {
                    if (method.IsGenericMethod)
                    {
                        SkipMethod = method;
                        break;
                    }

                }
            }
            if (SkipMethod == null)
                throw new Exception("找不到泛型Skip方法");

            System.Reflection.MethodInfo mmm = SkipMethod.MakeGenericMethod(dataType);
            if (mmm != null)
            {
                return mmm.Invoke(null, new object[] { linqQuery, skip });
            }
            else
                throw new Exception(SkipMethod.Name + ".MakeGenericMethod失败，参数类型：" + dataType.FullName);
        }
        /// <summary>
        /// 获取属性对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyName">属性名，可以是a.b的形式</param>
        /// <returns></returns>
        public static object GetPropertyValue(object data, string propertyName)
        {
            if (data == null)
                return null;
            string[] dataFieldArr = propertyName.Split('.');
            Type currentObjType = data.GetType();
            PropertyInfo propertyInfo = null;
            object result = data;
            for (int i = 0; i < dataFieldArr.Length; i++)
            {
                propertyInfo = currentObjType.GetProperty(dataFieldArr[i], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo == null)
                    throw new Exception("属性" + dataFieldArr[i] + "无效");

                result = propertyInfo.GetValue(result);
                if (result == null)
                    return null;
                if (i < dataFieldArr.Length - 1)
                {
                    currentObjType = propertyInfo.PropertyType;
                }
            }
            return result;
        }
        public static System.Linq.Expressions.Expression GetPropertyExpression(ParameterExpression param, Type dataType, string propertyName, out PropertyInfo propertyInfo)
        {
            System.Linq.Expressions.Expression left = null;
            string[] dataFieldArr = propertyName.Split('.');
            System.Linq.Expressions.Expression lastObjectExpress = param;
            Type currentObjType = dataType;
            propertyInfo = null;
            for (int i = 0; i < dataFieldArr.Length; i++)
            {
                propertyInfo = currentObjType.GetProperty(dataFieldArr[i], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo == null)
                    throw new Exception("属性" + dataFieldArr[i] + "无效");
                left = System.Linq.Expressions.Expression.Property(lastObjectExpress, propertyInfo);
                if (i < dataFieldArr.Length - 1)
                {
                    currentObjType = propertyInfo.PropertyType;
                    lastObjectExpress = left;
                }
            }
            return left;
        }

        public static object InvokeSelect(object linqQuery, string propertyName)
        {
            Type dataType = linqQuery.GetType().GetGenericArguments()[0];
            ParameterExpression param = System.Linq.Expressions.Expression.Parameter(dataType, "n");
            PropertyInfo pinfo;
            System.Linq.Expressions.Expression left = GetPropertyExpression(param, dataType, propertyName, out pinfo);


            System.Linq.Expressions.Expression expression = System.Linq.Expressions.Expression.Lambda(left, param);

            Type myType = typeof(System.Linq.Queryable);
            var methods = myType.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            foreach (System.Reflection.MethodInfo method in methods)
            {
                if (method.Name != "Select" || method.IsGenericMethod == false)
                    continue;
                System.Reflection.MethodInfo mmm = method.MakeGenericMethod(dataType, pinfo.PropertyType);
                if (mmm.GetParameters().Length != 2)
                    continue;

                return mmm.Invoke(null, new object[] { linqQuery, expression });

            }
            return null;
        }
        public static object InvokeSum(object linqQuery)
        {
            Type dataType = linqQuery.GetType().GetGenericArguments()[0];
            Type myType = typeof(System.Linq.Queryable);
            var methods = myType.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            foreach (System.Reflection.MethodInfo method in methods)
            {
                if (method.Name != "Sum" || method.IsGenericMethod || method.ReturnType != dataType)
                    continue;
                if (method.GetParameters().Length != 1)
                    continue;

                return method.Invoke(null, new object[] { linqQuery });

            }
            return null;
        }
        public static object InvokeFirstOrDefault(object linqQuery)
        {
            Type dataType = linqQuery.GetType().GetGenericArguments()[0];
            Type myType = typeof(System.Linq.Queryable);
            var methods = myType.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            foreach (System.Reflection.MethodInfo method in methods)
            {
                if (method.Name != "FirstOrDefault" || method.IsGenericMethod == false)
                    continue;
                System.Reflection.MethodInfo mmm = method.MakeGenericMethod(dataType);

                var ps = mmm.GetParameters();
                if (ps.Length != 1)
                    continue;
                return mmm.Invoke(null, new object[] { linqQuery });

            }
            return null;
        }

        public static async Task<object> InvokeFirstOrDefaultAsync(object linqQuery)
        {
            Type dataType = linqQuery.GetType().GetGenericArguments()[0];
            Type myType = typeof(EntityFrameworkQueryableExtensions);
            var methods = myType.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            foreach (System.Reflection.MethodInfo method in methods)
            {
                if (method.Name != "FirstOrDefaultAsync" || method.IsGenericMethod == false)
                    continue;
                System.Reflection.MethodInfo mmm = method.MakeGenericMethod(dataType);

                var ps = mmm.GetParameters();
                return await (dynamic)mmm.Invoke(null, new object[] { linqQuery , CancellationToken.None });

            }
            return null;
        }

        public static object GetQueryForOrderBy(object linqQuery, string stringOrder)
        {
            Type myType = typeof(System.Linq.Queryable);
            Type dataType = linqQuery.GetType().GetGenericArguments()[0];
            ParameterExpression param = System.Linq.Expressions.Expression.Parameter(dataType, "n");
            var methods = myType.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            bool isThenBy = false;
            string[] orders = stringOrder.Split(',');
            foreach (string order in orders)
            {
                if (order.Trim().Length == 0)
                    continue;
                bool desc = order.Trim().ToLower().Contains(" desc");
                string methodName;
                if (isThenBy == false)
                {
                    isThenBy = true;
                    methodName = desc ? "OrderByDescending" : "OrderBy";
                }
                else
                {
                    methodName = desc ? "ThenByDescending" : "ThenBy";
                }
                string itemProperty = order.Trim().Split(' ')[0];
                if (itemProperty.StartsWith("[") && itemProperty.EndsWith("]"))
                    itemProperty = itemProperty.Substring(1, itemProperty.Length - 2);

                System.Reflection.PropertyInfo pinfo;
                System.Linq.Expressions.Expression left = GetPropertyExpression(param, dataType, itemProperty, out pinfo);
                System.Linq.Expressions.Expression expression = System.Linq.Expressions.Expression.Lambda(left, param);


                foreach (System.Reflection.MethodInfo method in methods)
                {
                    if (method.Name != methodName || method.IsGenericMethod == false)
                        continue;
                    System.Reflection.MethodInfo mmm = method.MakeGenericMethod(dataType, pinfo.PropertyType);
                    if (mmm.GetParameters().Length != 2)
                        continue;

                    linqQuery = mmm.Invoke(null, new object[] { linqQuery, expression });
                    break;
                }
            }
            return linqQuery;
        }
        public static object GetQueryForThenBy(object linqQuery, string stringOrder)
        {
            Type myType = typeof(System.Linq.Queryable);
            Type dataType = linqQuery.GetType().GetGenericArguments()[0];
            ParameterExpression param = System.Linq.Expressions.Expression.Parameter(dataType, "n");
            var methods = myType.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);

            string[] orders = stringOrder.Split(',');
            foreach (string order in orders)
            {
                if (order.Trim().Length == 0)
                    continue;
                bool desc = order.Trim().ToLower().Contains(" desc");
                string methodName = desc ? "ThenByDescending" : "ThenBy";
                string itemProperty = order.Trim().Split(' ')[0];
                if (itemProperty.StartsWith("[") && itemProperty.EndsWith("]"))
                    itemProperty = itemProperty.Substring(1, itemProperty.Length - 2);

                System.Reflection.PropertyInfo pinfo;
                System.Linq.Expressions.Expression left = GetPropertyExpression(param, dataType, itemProperty, out pinfo);
                System.Linq.Expressions.Expression expression = System.Linq.Expressions.Expression.Lambda(left, param);


                foreach (System.Reflection.MethodInfo method in methods)
                {
                    if (method.Name != methodName || method.IsGenericMethod == false)
                        continue;
                    System.Reflection.MethodInfo mmm = method.MakeGenericMethod(dataType, pinfo.PropertyType);
                    if (mmm.GetParameters().Length != 2)
                        continue;

                    linqQuery = mmm.Invoke(null, new object[] { linqQuery, expression });
                    break;
                }
            }
            return linqQuery;
        }
        #endregion

        #region SaveChanges
        [Obsolete("DBContext不采用缓存机制，不支持SaveChanges方法")]
        public override int SaveChanges()
        {
            throw new Exception($"此方法已禁用，请使用 Update Delete 等方法");
        }
        [Obsolete("DBContext不采用缓存机制，不支持SaveChanges方法")]
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            throw new Exception($"此方法已禁用，请使用 Update Delete 等方法");
        }
        [Obsolete("DBContext不采用缓存机制，不支持SaveChanges方法")]
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new Exception($"此方法已禁用，请使用 Update Delete 等方法");
        }
        [Obsolete("DBContext不采用缓存机制，不支持SaveChanges方法")]
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new Exception($"此方法已禁用，请使用 Update Delete 等方法");
        }
        #endregion

        //public static void Init(System.Reflection.Assembly mainAssembly)
        //{
        //    Helper.Init(mainAssembly);
        //}

        //static System.Data.Common.DbConnection CreateConnection(string connectionString, DatabaseType dbType)
        //{
        //    Type type = DatabaseServiceTypes[dbType];
        //    IDatabaseService service = (IDatabaseService)Activator.CreateInstance(type);
        //    return service.CreateConnection(connectionString);
        //}

        public static IDatabaseService CreateDatabaseService(string connectionString, DatabaseType dbType)
        {
            DBContext context = new DBContext(connectionString, dbType);
            return context.Database;
        }

        ~DBContext()
        {
            this.Dispose();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conStr">连接字符串</param>
        /// <param name="dbType">数据库类型</param>
        public DBContext(string conStr, DatabaseType dbType = DatabaseType.SqlServer):this(conStr,dbType,true)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conStr">连接字符串</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="upgradeDatabase">是否自动更新数据库结构到最新</param>
        public DBContext(string conStr, DatabaseType dbType , bool upgradeDatabase)
        {
            this.DatabaseType = dbType;
           

            Type type = DatabaseServiceTypes[this.DatabaseType];
            _databaseService = (IDatabaseService)Activator.CreateInstance(type, new object[] { this });

            this.ConnectionString = _databaseService.ConvertConnectionString(conStr);

            var thisType = this.GetType() ;
            var dictKey = thisType + "," + this.ConnectionString;
            if (upgradeDatabase && thisType != typeof(EntityDB.DBContext))
            {
                if (upgradedDatabase.ContainsKey(dictKey) == false || !upgradedDatabase[dictKey])
                {
                    lock (GlobalLockObj)
                    {
                        if (upgradedDatabase.ContainsKey(dictKey) == false || !upgradedDatabase[dictKey])
                        {
                            try
                            {
                                this.CreateIfNotExist();
                                Way.EntityDB.Design.DBUpgrade.Upgrade(this, GetDesignString());
                            }
                            catch
                            {
                                throw;
                            }
                            finally
                            {
                                upgradedDatabase[dictKey] = true;
                            }
                        }
                    }
                }
            }

            this.ChangeTracker.AutoDetectChangesEnabled = false;
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        /// <summary>
        /// 获取数据库结构修改的所有action
        /// </summary>
        /// <returns></returns>
        public List<Design.Actions.Action> GetDesignActions()
        {
            return Way.EntityDB.Design.DBUpgrade.GetDatabaseActions(GetDesignString());
        }

        static Dictionary<Type,bool> CreatedIfNotExist = new Dictionary<Type, bool>();
        /// <summary>
        /// 如果数据库不存在，创建数据库，此方法在构造函数中自动调用，如果不想创建数据库，请重写此方法
        /// 此方法只在进程第一次调用有效
        /// </summary>
        protected virtual void CreateIfNotExist()
        {
            Type thisType = this.GetType();
            
            if (CreatedIfNotExist.ContainsKey(thisType) == false || !CreatedIfNotExist[thisType])
            {
                lock (GlobalLockObj)
                {
                    try
                    {
                        if (CreatedIfNotExist.ContainsKey(thisType) == false || !CreatedIfNotExist[thisType])
                        {
                            CreatedIfNotExist[thisType] = true;
                            Design.Services.IDatabaseDesignService dbservice = Way.EntityDB.Design.DBHelper.CreateDatabaseDesignService((Way.EntityDB.DatabaseType)(int)this.DatabaseType);
                            dbservice.Create(new EJ.Databases()
                            {
                                conStr = this.ConnectionString
                            });
                        }
                    }
                    catch(Exception ex)
                    {
                        CreatedIfNotExist[thisType] = true;
                    }
                    
                }
            }
        }
        protected virtual string GetDesignString()
        {
            return null;
        }
        protected override void OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                this.Database.OnConfiguring(optionsBuilder);
            }
            // optionsBuilder.UseLazyLoadingProxies()  启用懒加载
        }


        #region 数据更新、添加、删除操作

        public new void Update(DataItem entity)
        {
            this.Update(entity, null);
        }

        public new void Update<T>(T dataitem) where T : DataItem
        {
            this.Update(dataitem, null);
        }

        public Task UpdateAsync(DataItem entity)
        {
            return this.UpdateAsync(entity, null);
        }

        public Task UpdateAsync<T>(T dataitem) where T : DataItem
        {
            return this.UpdateAsync(dataitem, null);
        }

        /// <summary>
        /// 更新对象数据到数据库
        /// </summary>
        /// <param name="dataitem"></param>
        /// <param name="condition">指定更新条件，如：m=&gt;m.age &gt; 16 &amp;&amp; m.id == dataitem.id，默认使用主键匹配</param>
        public virtual int Update<T>(T dataitem, Expression<Func<T, bool>> condition) where T : DataItem
        {
            if (AutoBeginTransaction && this.CurrentTransaction == null)
                this.BeginTransaction();

            if (BeforeUpdate != null)
            {
                BeforeUpdate(this, new DatabaseUpdateArg()
                {
                    DataItem = dataitem,
                    Condition = condition
                });
            }

            bool needCloseConnection = false;
            if (this.Database.Connection.State != System.Data.ConnectionState.Open)
            {
                needCloseConnection = true;
                this.Database.Connection.Open();
            }

            try
            {
                var ret = this.Database.Update(dataitem, condition);
                if (AfterUpdate != null)
                {
                    AfterUpdate(this, new DatabaseUpdateArg()
                    {
                        DataItem = dataitem,
                        Condition = condition
                    });
                }
                dataitem.UpdateExpression = null;
                dataitem.ChangedProperties.Clear();
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (needCloseConnection)
                    this.Database.Connection.Close();
            }
        }

        public virtual int Delete<T>(Expression<Func<T, bool>> condition) where T : DataItem
        {
            if (AutoBeginTransaction && this.CurrentTransaction == null)
                this.BeginTransaction();

            if (BeforeDelete != null)
            {
                BeforeDelete(this, new DatabaseUpdateArg()
                {
                    Condition = condition
                });
            }

            bool needCloseConnection = false;
            if (this.Database.Connection.State != System.Data.ConnectionState.Open)
            {
                needCloseConnection = true;
                this.Database.Connection.Open();
            }

            try
            {
                var ret = this.Database.Delete<T>(condition);
                if (AfterDelete != null)
                {
                    AfterDelete(this, new DatabaseUpdateArg()
                    {
                        Condition = condition
                    });
                }
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (needCloseConnection)
                    this.Database.Connection.Close();
            }
        }

        public virtual async Task<int> DeleteAsync<T>(Expression<Func<T, bool>> condition) where T : DataItem
        {
            if (AutoBeginTransaction && this.CurrentTransaction == null)
               await this.BeginTransactionAsync();

            if (BeforeDelete != null)
            {
                BeforeDelete(this, new DatabaseUpdateArg()
                {
                    Condition = condition
                });
            }

            bool needCloseConnection = false;
            if (this.Database.Connection.State != System.Data.ConnectionState.Open)
            {
                needCloseConnection = true;
                await this.Database.Connection.OpenAsync();
            }

            try
            {
                var ret = await this.Database.DeleteAsync<T>(condition);
                if (AfterDelete != null)
                {
                    AfterDelete(this, new DatabaseUpdateArg()
                    {
                        Condition = condition
                    });
                }
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (needCloseConnection)
                   await this.Database.Connection.CloseAsync();
            }
        }

        /// <summary>
        /// 更新对象数据到数据库
        /// </summary>
        /// <param name="dataitem"></param>
        /// <param name="condition">指定更新条件，如：m=&gt;m.age &gt; 16 &amp;&amp; m.id == dataitem.id，默认使用主键匹配</param>
        public virtual async Task<int> UpdateAsync<T>(T dataitem, Expression<Func<T, bool>> condition) where T : DataItem
        {
            if (AutoBeginTransaction && this.CurrentTransaction == null)
               await this.BeginTransactionAsync();

            if (BeforeUpdate != null)
            {
                BeforeUpdate(this, new DatabaseUpdateArg()
                {
                    DataItem = dataitem,
                    Condition = condition
                });
            }

            bool needCloseConnection = false;
            if (this.Database.Connection.State != System.Data.ConnectionState.Open)
            {
                needCloseConnection = true;
               await this.Database.Connection.OpenAsync();
            }

            try
            {
                var ret =await this.Database.UpdateAsync(dataitem, condition);
                if (AfterUpdate != null)
                {
                    AfterUpdate(this, new DatabaseUpdateArg()
                    {
                        DataItem = dataitem,
                        Condition = condition
                    });
                }
                dataitem.UpdateExpression = null;
                dataitem.ChangedProperties.Clear();
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (needCloseConnection)
                  await this.Database.Connection.CloseAsync();
            }
        }
        /// <summary>
        /// 添加对象数据到数据库
        /// </summary>
        /// <param name="dataitem"></param>
        public virtual void Insert(DataItem dataitem)
        {
            this.Insert(dataitem, false);
        }

        /// <summary>
        /// 添加对象数据到数据库
        /// </summary>
        /// <param name="dataitem"></param>
        /// <returns></returns>
        public virtual Task InsertAsync(DataItem dataitem)
        {
            return this.InsertAsync(dataitem, false);
        }

        /// <summary>
        /// 添加对象数据到数据库
        /// </summary>
        /// <param name="dataitem"></param>
        /// <param name="insertAllFields">是否连自增长字段，也放到insert语句里</param>
        public virtual void Insert(DataItem dataitem,bool insertAllFields)
        {
            if (AutoBeginTransaction && this.CurrentTransaction == null)
                this.BeginTransaction();

            if (BeforeInsert != null)
            {
                BeforeInsert(this, new DatabaseModifyEventArg()
                {
                    DataItem = dataitem,
                });
            }

            bool needCloseConnection = false;
            if (this.Database.Connection.State != System.Data.ConnectionState.Open)
            {
                needCloseConnection = true;
                this.Database.Connection.Open();
            }

            try
            {
                this.Database.Insert(dataitem, insertAllFields);
                if (AfterInsert != null)
                {
                    AfterInsert(this, new DatabaseModifyEventArg()
                    {
                        DataItem = dataitem,
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (needCloseConnection)
                    this.Database.Connection.Close();
            }
        }

        /// <summary>
        /// 添加对象数据到数据库
        /// </summary>
        /// <param name="dataitem"></param>
        /// <param name="insertAllFields">是否连自增长字段，也放到insert语句里</param>
        public virtual async Task InsertAsync(DataItem dataitem, bool insertAllFields)
        {
            if (AutoBeginTransaction && this.CurrentTransaction == null)
               await this.BeginTransactionAsync();

            if (BeforeInsert != null)
            {
                BeforeInsert(this, new DatabaseModifyEventArg()
                {
                    DataItem = dataitem,
                });
            }

            bool needCloseConnection = false;
            if (this.Database.Connection.State != System.Data.ConnectionState.Open)
            {
                needCloseConnection = true;
                await this.Database.Connection.OpenAsync();
            }

            try
            {
                await this.Database.InsertAsync(dataitem, insertAllFields);
                if (AfterInsert != null)
                {
                    AfterInsert(this, new DatabaseModifyEventArg()
                    {
                        DataItem = dataitem,
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (needCloseConnection)
                    await this.Database.Connection.CloseAsync();
            }
        }

        /// <summary>
        /// 在数据库删除此对象数据
        /// </summary>
        /// <param name="dataitem"></param>
        public virtual void Delete(DataItem dataitem)
        {
            if (AutoBeginTransaction && this.CurrentTransaction == null)
                this.BeginTransaction();

            if (BeforeDelete != null)
            {
                BeforeDelete(this, new DatabaseModifyEventArg()
                {
                    DataItem = dataitem,
                });
            }

            bool needCloseConnection = false;
            if (this.Database.Connection.State != System.Data.ConnectionState.Open)
            {
                needCloseConnection = true;
                this.Database.Connection.Open();
            }

            try
            {
                this.Database.Delete(dataitem);
                if (AfterDelete != null)
                {
                    AfterDelete(this, new DatabaseModifyEventArg()
                    {
                        DataItem = dataitem,
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (needCloseConnection)
                    this.Database.Connection.Close();
            }
           
        }

        /// <summary>
        /// 在数据库删除此对象数据
        /// </summary>
        /// <param name="dataitem"></param>
        public virtual async Task DeleteAsync(DataItem dataitem)
        {
            if (AutoBeginTransaction && this.CurrentTransaction == null)
               await this.BeginTransactionAsync();

            if (BeforeDelete != null)
            {
                BeforeDelete(this, new DatabaseModifyEventArg()
                {
                    DataItem = dataitem,
                });
            }

            bool needCloseConnection = false;
            if (this.Database.Connection.State != System.Data.ConnectionState.Open)
            {
                needCloseConnection = true;
                await this.Database.Connection.OpenAsync();
            }

            try
            {
                await this.Database.DeleteAsync(dataitem);
                if (AfterDelete != null)
                {
                    AfterDelete(this, new DatabaseModifyEventArg()
                    {
                        DataItem = dataitem,
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (needCloseConnection)
                   await this.Database.Connection.CloseAsync();
            }

        }

        /// <summary>
        /// 开启更新锁
        /// </summary>
        /// <param name="items"></param>
        public void UpdateLock(System.Linq.IQueryable items)
        {
            if (this.Database.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("没有开启事务");

           Type dataType = items.GetType().GetGenericArguments()[0];

            var tableSchema = SchemaManager.GetSchemaTable(dataType);

            if (tableSchema.KeyColumn == null)
                throw new Exception(dataType.Name + "没有定义主键");

            int pagesize = 100;
                var query = InvokeSelect(items, tableSchema.KeyColumn.PropertyName);
                int skip = 0;
                while (true)
                {
                    var skipQuery = InvokeSkip(query, skip);
                    var data1 = InvokeTake(skipQuery, pagesize);
                    var dataitems = (System.Array)InvokeToArray(data1);

                    foreach (var idvalue in dataitems)
                    {
                        this.Database.UpdateLock(dataType, idvalue);
                    }

                    if (dataitems.Length < pagesize)
                        break;

                    skip += pagesize;
                }

            
        }

        /// <summary>
        /// 开启更新锁
        /// </summary>
        /// <param name="items"></param>
        public async Task UpdateLockAsync(System.Linq.IQueryable items)
        {
            if (this.Database.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("没有开启事务");

            Type dataType = items.GetType().GetGenericArguments()[0];

            var tableSchema = SchemaManager.GetSchemaTable(dataType);

            if (tableSchema.KeyColumn == null)
                throw new Exception(dataType.Name + "没有定义主键");

            int pagesize = 100;
            var query = InvokeSelect(items, tableSchema.KeyColumn.PropertyName);
            int skip = 0;
            while (true)
            {
                var skipQuery = InvokeSkip(query, skip);
                var data1 = InvokeTake(skipQuery, pagesize);
                var dataitems = (System.Array)InvokeToArray(data1);

                foreach (var idvalue in dataitems)
                {
                    await this.Database.UpdateLockAsync(dataType, idvalue);
                }

                if (dataitems.Length < pagesize)
                    break;

                skip += pagesize;
            }


        }


        /// <summary>
        /// 开启更新锁，并返回锁住后的数据集
        /// </summary>
        /// <param name="dataQuery"></param>
        public T[] UpdateLockToArray<T>(System.Linq.IQueryable<T> dataQuery) where T:DataItem
        {
            if (this.Database.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("没有开启事务");

            var dset = this.Set<T>();
            Type dataType = dataQuery.GetType().GetGenericArguments()[0];

            var tableSchema = SchemaManager.GetSchemaTable(dataType);

            if (tableSchema.KeyColumn == null)
                throw new Exception(dataType.Name + "没有定义主键");

            List<T> result = new List<T>();
            int pagesize = 100;
            object query = InvokeSelect(dataQuery, tableSchema.KeyColumn.PropertyName);
            int skip = 0;
            while (true)
            {
                var skipQuery = InvokeSkip(query, skip);
                var data1 = InvokeTake(skipQuery, pagesize);
                var dataitems = (System.Array)InvokeToArray(data1);

                foreach (var idvalue in dataitems)
                {
                    this.Database.UpdateLock(dataType, idvalue);
                    var data = InvokeFirstOrDefault(InvokeWhereEquals(dset, tableSchema.KeyColumn.PropertyName, idvalue));
                    if(data != null)
                    {
                        result.Add((T)data);
                    }
                }

                if (dataitems.Length < pagesize)
                    break;

                skip += pagesize;
            }

            return result.ToArray();

        }

        /// <summary>
        /// 开启更新锁，并返回锁住后的数据集
        /// </summary>
        /// <param name="dataQuery"></param>
        public async Task<T[]> UpdateLockToArrayAsync<T>(System.Linq.IQueryable<T> dataQuery) where T : DataItem
        {
            if (this.Database.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("没有开启事务");

            var dset = this.Set<T>();
            Type dataType = dataQuery.GetType().GetGenericArguments()[0];

            var tableSchema = SchemaManager.GetSchemaTable(dataType);

            if (tableSchema.KeyColumn == null)
                throw new Exception(dataType.Name + "没有定义主键");

            List<T> result = new List<T>();
            int pagesize = 100;
            object query = InvokeSelect(dataQuery, tableSchema.KeyColumn.PropertyName);
            int skip = 0;
            while (true)
            {
                var skipQuery = InvokeSkip(query, skip);
                var data1 = InvokeTake(skipQuery, pagesize);
                var dataitems = (System.Array)await InvokeToArrayAsync(data1);

                foreach (var idvalue in dataitems)
                {
                    await this.Database.UpdateLockAsync(dataType, idvalue);
                    var data = await InvokeFirstOrDefaultAsync(InvokeWhereEquals(dset, tableSchema.KeyColumn.PropertyName, idvalue));
                    if (data != null)
                    {
                        result.Add((T)data);
                    }
                }

                if (dataitems.Length < pagesize)
                    break;

                skip += pagesize;
            }

            return result.ToArray();

        }

        /// <summary>
        /// 锁住一条数据，并从数据库重新读取这条数据返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataitem"></param>
        public T UpdateLockDataItem<T>(T dataitem) where T : DataItem
        {
            if (this.Database.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("没有开启事务");

            Type dataType = typeof(T);

            var tableSchema = SchemaManager.GetSchemaTable(dataType);

            if (tableSchema.KeyColumn == null)
                throw new Exception(dataType.Name + "没有定义主键");


            var idvalue = dataitem.PKValue;

            if (idvalue == null)
                return default(T);

            this.Database.UpdateLock(dataType, idvalue);

            object query = this.Set<T>();
            query = InvokeWhereEquals(query, tableSchema.KeyColumn.PropertyName, idvalue);
            return  (T)InvokeFirstOrDefault(query);
        }

        /// <summary>
        /// 锁住一条数据，并从数据库重新读取这条数据返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataitem"></param>
        public async Task<T> UpdateLockDataItemAsync<T>(T dataitem) where T : DataItem
        {
            if (this.Database.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("没有开启事务");

            Type dataType = typeof(T);

            var tableSchema = SchemaManager.GetSchemaTable(dataType);

            if (tableSchema.KeyColumn == null)
                throw new Exception(dataType.Name + "没有定义主键");


            var idvalue = dataitem.PKValue;

            if (idvalue == null)
                return default(T);

            await this.Database.UpdateLockAsync(dataType, idvalue);

            object query = this.Set<T>();
            query = InvokeWhereEquals(query, tableSchema.KeyColumn.PropertyName, idvalue);
            return (T)(await InvokeFirstOrDefaultAsync(query));
        }

        /// <summary>
        /// 对第一条记录开启更新锁，并返回这条记录的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public T UpdateLockFirstOrDefault<T>(System.Linq.IQueryable<T> items) where T : class
        {
            if (this.Database.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("没有开启事务");

            Type dataType = typeof(T);

            var tableSchema = SchemaManager.GetSchemaTable(dataType);

            if (tableSchema.KeyColumn == null)
                throw new Exception(dataType.Name + "没有定义主键");

            var query = InvokeSelect(items, tableSchema.KeyColumn.PropertyName);
            var idvalue = InvokeFirstOrDefault(query);
            if (idvalue == null)
                return default(T);

            this.Database.UpdateLock(dataType, idvalue);

            query = this.Set<T>();
            query = InvokeWhereEquals(query, tableSchema.KeyColumn.PropertyName, idvalue);
            return (T)InvokeFirstOrDefault(query);
        }

        /// <summary>
        /// 对第一条记录开启更新锁，并返回这条记录的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<T> UpdateLockFirstOrDefaultAsync<T>(System.Linq.IQueryable<T> items) where T : class
        {
            if (this.Database.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("没有开启事务");

            Type dataType = typeof(T);

            var tableSchema = SchemaManager.GetSchemaTable(dataType);

            if (tableSchema.KeyColumn == null)
                throw new Exception(dataType.Name + "没有定义主键");

            var query = InvokeSelect(items, tableSchema.KeyColumn.PropertyName);
            var idvalue = await InvokeFirstOrDefaultAsync(query);
            if (idvalue == null)
                return default(T);

            await this.Database.UpdateLockAsync(dataType, idvalue);

            query = this.Set<T>();
            query = InvokeWhereEquals(query, tableSchema.KeyColumn.PropertyName, idvalue);
            return (T)(await InvokeFirstOrDefaultAsync(query));
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="items"></param>
        public void Delete(System.Linq.IQueryable items)
        {
            if (AutoBeginTransaction && this.CurrentTransaction == null)
                this.BeginTransaction();

            Type dataType = items.GetType().GetGenericArguments()[0];

            var tableSchema = SchemaManager.GetSchemaTable(dataType);

            if (tableSchema.KeyColumn == null)
                throw new Exception(dataType.Name + "没有定义主键");


            bool needCloseConnection = false;
            if (this.Database.Connection.State != System.Data.ConnectionState.Open)
            {
                needCloseConnection = true;
                this.Database.Connection.Open();
            }

            int pagesize = 100;
            try
            {
                var query = InvokeSelect(items, tableSchema.KeyColumn.PropertyName);
                while (true)
                {
                    var data1 = InvokeTake(query, pagesize);
                    var dataitems = (System.Array)InvokeToArray(data1);
                   
                    foreach (var idvalue in dataitems)
                    {
                        var deldataItem = (DataItem)Activator.CreateInstance(dataType);
                        deldataItem.SetValue(tableSchema.KeyColumn.PropertyName, idvalue);
                        deldataItem.ChangedProperties.Clear();
                        Delete(deldataItem);
                    }

                    if (dataitems.Length < pagesize)
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (needCloseConnection)
                    this.Database.Connection.Close();
            }

        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="items"></param>
        public async Task DeleteAsync(System.Linq.IQueryable items)
        {
            if (AutoBeginTransaction && this.CurrentTransaction == null)
               await this.BeginTransactionAsync();

            Type dataType = items.GetType().GetGenericArguments()[0];

            var tableSchema = SchemaManager.GetSchemaTable(dataType);

            if (tableSchema.KeyColumn == null)
                throw new Exception(dataType.Name + "没有定义主键");


            bool needCloseConnection = false;
            if (this.Database.Connection.State != System.Data.ConnectionState.Open)
            {
                needCloseConnection = true;
               await this.Database.Connection.OpenAsync();
            }

            int pagesize = 100;
            try
            {
                var query = InvokeSelect(items, tableSchema.KeyColumn.PropertyName);
                while (true)
                {
                    var data1 = InvokeTake(query, pagesize);
                    var dataitems = (System.Array)await InvokeToArrayAsync(data1);

                    foreach (var idvalue in dataitems)
                    {
                        var deldataItem = (DataItem)Activator.CreateInstance(dataType);
                        deldataItem.SetValue(tableSchema.KeyColumn.PropertyName, idvalue);
                        deldataItem.ChangedProperties.Clear();
                        await DeleteAsync(deldataItem);
                    }

                    if (dataitems.Length < pagesize)
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (needCloseConnection)
                   await this.Database.Connection.CloseAsync();
            }

        }
        #endregion

        /// <summary>
        /// 当前事务对象
        /// </summary>
        public Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction CurrentTransaction => _disposed ? null : ((Microsoft.EntityFrameworkCore.DbContext)this).Database.CurrentTransaction;

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="IsolationLevel"></param>
        public virtual Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction BeginTransaction(System.Data.IsolationLevel IsolationLevel = System.Data.IsolationLevel.ReadUncommitted)
        {
            if (this.CurrentTransaction != null)
                return this.CurrentTransaction;

            return ((Microsoft.EntityFrameworkCore.DbContext)this).Database.BeginTransaction(IsolationLevel);
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="IsolationLevel"></param>
        public virtual Task<Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction> BeginTransactionAsync(System.Data.IsolationLevel IsolationLevel = System.Data.IsolationLevel.ReadUncommitted)
        {
            if (this.CurrentTransaction != null)
                return Task.FromResult(this.CurrentTransaction);

            return ((Microsoft.EntityFrameworkCore.DbContext)this).Database.BeginTransactionAsync(IsolationLevel);
        }


        public virtual void CommitTransaction()
        {
            if (((Microsoft.EntityFrameworkCore.DbContext)this).Database.CurrentTransaction != null)
            {
                if (this.BeforeCommitTransaction != null)
                {
                    this.BeforeCommitTransaction(this, null);
                }
                ((Microsoft.EntityFrameworkCore.DbContext)this).Database.CommitTransaction();
                if (this.AfterCommitTransaction != null)
                {
                    this.AfterCommitTransaction(this, null);
                }
            }
        }

        public virtual async Task CommitTransactionAsync()
        {
            if (((Microsoft.EntityFrameworkCore.DbContext)this).Database.CurrentTransaction != null)
            {
                if (this.BeforeCommitTransaction != null)
                {
                    this.BeforeCommitTransaction(this, null);
                }
                await ((Microsoft.EntityFrameworkCore.DbContext)this).Database.CommitTransactionAsync();
                if (this.AfterCommitTransaction != null)
                {
                    this.AfterCommitTransaction(this, null);
                }
            }
        }

        public virtual void RollbackTransaction()
        {
            if (((Microsoft.EntityFrameworkCore.DbContext)this).Database.CurrentTransaction != null)
            {
                if (this.BeforeRollbackTransaction != null)
                {
                    this.BeforeRollbackTransaction(this, null);
                }
                ((Microsoft.EntityFrameworkCore.DbContext)this).Database.RollbackTransaction();
                if (this.AfterRollbackTransaction != null)
                {
                    this.AfterRollbackTransaction(this, null);
                }
            }
        }

        public virtual async Task RollbackTransactionAsync()
        {
            if (((Microsoft.EntityFrameworkCore.DbContext)this).Database.CurrentTransaction != null)
            {
                if (this.BeforeRollbackTransaction != null)
                {
                    this.BeforeRollbackTransaction(this, null);
                }
                await ((Microsoft.EntityFrameworkCore.DbContext)this).Database.RollbackTransactionAsync();
                if (this.AfterRollbackTransaction != null)
                {
                    this.AfterRollbackTransaction(this, null);
                }
            }
        }

        /// <summary>
        /// DBContext是否已经释放
        /// </summary>
        public bool IsDisposed
        {
            get
            {
                return _disposed;
            }
        }

        public override void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                try
                {
                    this.RollbackTransaction();
                }
                catch
                {
                }
                try
                {
                    base.Dispose();
                }
                catch
                {
                }
            }
        }
    }
}