using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Way.EntityDB
{
    public partial class DBContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            createILike(modelBuilder);
            createLike(modelBuilder);

            createJsonArrayExist(modelBuilder);
        }

        void createJsonArrayExist(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDbFunction(typeof(DBContext).GetMethod(nameof(DBContext.JsonArrayExist)))
                .HasTranslation(args =>
                {
                    switch (this.DatabaseType)
                    {
                        case DatabaseType.PostgreSql:
                            var querySqlGeneratorFactory = this.GetService<IQuerySqlGeneratorFactory>();
                            var querySqlGenerator = querySqlGeneratorFactory.Create();

                            var memberExp = args[0] as SqlUnaryExpression;
                            if(memberExp == null)
                            {
                                throw new InvalidCastException($"JsonArrayExist的第一个参数必须使用.ToString()");
                            }
                            var command = querySqlGenerator.GetCommand(memberExp.Operand);

                            var left = command.CommandText;
                            var right = args[1].Print();
                            return new SqlConstantExpression(Expression.Constant($"({left} ? {right})"), new IntTypeMapping("int", DbType.Int32));

                        default:
                            throw new NotSupportedException($"{this.DatabaseType}不支持JsonArrayExist");

                    }
                });
        }

        void createILike(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDbFunction(typeof(DBContext).GetMethod(nameof(DBContext.ILike)))
                .HasTranslation(args =>
                {
                    switch (this.DatabaseType)
                    {
                        case DatabaseType.PostgreSql:
                            var querySqlGeneratorFactory = this.GetService<IQuerySqlGeneratorFactory>();
                            var querySqlGenerator = querySqlGeneratorFactory.Create();

                            var command = querySqlGenerator.GetCommand(args[0]);

                            var left = command.CommandText;
                            var right = args[1].Print();
                            return new SqlConstantExpression(Expression.Constant($"({left} ilike {right})"), new IntTypeMapping("int", DbType.Int32));

                        default:
                            return new LikeExpression(args[0], args[1], null, null);

                    }
                });
        }

        void createLike(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDbFunction(typeof(DBContext).GetMethod(nameof(DBContext.Like)))
                .HasTranslation(args =>
                {
                    return new LikeExpression(args[0], args[1], null, null);
                });
        }

        public bool ILike(string member, string pattern) => throw new NotSupportedException();
        public bool Like(string member, string pattern) => throw new NotSupportedException();


        /// <summary>
        /// 判断jonsb的字符串数组里是否包含指定字符串，
        /// </summary>
        /// <param name="member">数据库字段，应采用.ToString()转换一下</param>
        /// <param name="item">指定查找的字符串</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public bool JsonArrayExist(string member, string item) => throw new NotSupportedException();
    }
}
