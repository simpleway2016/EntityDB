using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Way.EntityDB
{
    public interface IDatabaseService
    {
        DBContext DBContext
        {
            get;
        }
        System.Data.Common.DbConnection Connection
        {
            get;
        }
        String ConnectionString
        {
            get;
        }
        string ConvertConnectionString(string conStr);
        void OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder);
        //System.Data.Common.DbConnection CreateConnection(string connectString);
        System.Data.Common.DbCommand CreateCommand(string sql, params object[] parames);

        void Insert(DataItem dataitem,bool insertAllFields);
        Task InsertAsync(DataItem dataitem, bool insertAllFields);
        int Update<T>(T dataitem,Expression<Func<T,bool>> condition) where T :DataItem;
        int Delete<T>(Expression<Func<T, bool>> condition) where T : DataItem;
        Task<int> DeleteAsync<T>(Expression<Func<T, bool>> condition) where T : DataItem;
        Task<int> UpdateAsync<T>(T dataitem, Expression<Func<T, bool>> condition) where T : DataItem;

        void Delete(DataItem dataitem);
        Task DeleteAsync(DataItem dataitem);
        /// <summary>
        /// 允许自增长字段手动设置值
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="allow">是否允许</param>
        void AllowIdentityInsert(string tablename ,bool allow);

        /// <summary>
        /// 开启更新锁
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pkValue"></param>
        void UpdateLock<T>(object pkValue) where T : DataItem;
        /// <summary>
        /// 开启更新锁
        /// </summary>
        /// <param name="tableType"></param>
        /// <param name="pkValue"></param>
        void UpdateLock(Type tableType, object pkValue);

        /// <summary>
        /// 开启更新锁
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pkValue"></param>
        Task UpdateLockAsync<T>(object pkValue) where T : DataItem;
        /// <summary>
        /// 开启更新锁
        /// </summary>
        /// <param name="tableType"></param>
        /// <param name="pkValue"></param>
        Task UpdateLockAsync(Type tableType, object pkValue);

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlparameters">sql参数，分别对应@p0、@p1等</param>
        /// <returns></returns>
        object ExecSqlString(string sql,params object[] sqlparameters);
        Task<object> ExecSqlStringAsync(string sql, params object[] sqlparameters);
        int ExecuteNonQuery(string sql, params object[] sqlparameters);

        Task<int> ExecuteNonQueryAsync(string sql, params object[] sqlparameters);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="func">datareader读取数据期间的委托函数，如果返回false，表示不继续从数据库读取数据</param>
        /// <param name="sql"></param>
        /// <param name="sqlparameters"></param>
        void ExecuteReader(Func<System.Data.IDataReader,bool> func, string sql, params object[] sqlparameters);
        /// <summary>
        /// 通过sql语句，读取DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlparameters"></param>
        /// <returns></returns>
        WayDataTable SelectTable(string sql, params object[] sqlparameters);

        /// <summary>
        /// 通过sql语句，读取DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlparameters"></param>
        /// <returns></returns>
        WayDataSet SelectDataSet(string sql, params object[] sqlparameters);

         /// <summary>
        /// 通过sql语句，读取DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="skip">跳过几天记录</param>
        /// <param name="take">读取几条记录</param>
        /// <param name="sqlparameters">sql参数，分别对应@p0、@p1等</param>
        /// <returns></returns>
        WayDataTable SelectTable(string sql,int skip,int take, params object[] sqlparameters);

        /// <summary>
        /// 格式化文字，如tableName，可能需要变为[tableName]
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string FormatObjectName(string name);
       
    }
}
