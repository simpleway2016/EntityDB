using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System;
using Way.EntityDB;
using System.Text;
using Microsoft.EntityFrameworkCore;

public static class WayEntityDBExtensions
{
    public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
    {
        //var enumerator = query.Provider.Execute<IEnumerable<TEntity>>(query.Expression).GetEnumerator();
        //var relationalCommandCache = enumerator.Private("_relationalCommandCache");
        //var selectExpression = relationalCommandCache.Private<SelectExpression>("_selectExpression");
        //var factory = relationalCommandCache.Private<IQuerySqlGeneratorFactory>("_querySqlGeneratorFactory");

        //var sqlGenerator = factory.Create();
        //var command = sqlGenerator.GetCommand(selectExpression);

        //string sql = command.CommandText;
        //return sql;
        return query.ToQueryString();
    }
    public static string ToSql<TEntity>(this Expression<Func<TEntity, bool>> exp , DBContext dbcontext) where TEntity : DataItem
    {
        using (var cmd = dbcontext.Database.CreateCommand("select 1"))
        {
            var sql = dbcontext.Database.BuildWhereString(exp.Body, cmd);
            if (sql.StartsWith("("))
                sql = sql.Substring(1, sql.Length - 2);
            StringBuilder buffer = new StringBuilder(sql);
            if(cmd.Parameters.Count > 0)
            {
                buffer.Append("\r\n");
            }
            foreach( System.Data.Common.DbParameter parameter in cmd.Parameters )
            {
                buffer.Append("\r\n");
                buffer.Append($"{parameter.ParameterName}:{parameter.Value}");
            }
            return buffer.ToString();
        }
    }
    private static object Private(this object obj, string privateField) => obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);
    private static T Private<T>(this object obj, string privateField) => (T)obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);
}