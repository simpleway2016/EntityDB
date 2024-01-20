﻿using System.Linq;
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

    private static object Private(this object obj, string privateField) => obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);
    private static T Private<T>(this object obj, string privateField) => (T)obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);
}