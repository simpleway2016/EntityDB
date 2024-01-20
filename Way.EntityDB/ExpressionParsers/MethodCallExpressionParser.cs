using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Way.EntityDB.Exceptons;

namespace Way.EntityDB.ExpressionParsers
{
    internal class MethodCallExpressionParser : IExpressionParser
    {
        ExpressionParserRoute _expressionParserRoute;
        Type _ExpressionType = typeof(MethodCallExpression);
        public Type ExpressionType => _ExpressionType;

        public MethodCallExpressionParser(ExpressionParserRoute expressionParserRoute)
        {
            this._expressionParserRoute = expressionParserRoute;

        }


        public string Parse(Expression expression, DbCommand cmd, List<string> findedMembers, bool isSetter)
        {
            var exp = (MethodCallExpression)expression;
            try
            {
                var value = Expression.Lambda(expression).Compile().DynamicInvoke();
                //如果能计算出数值
                var dbparam = cmd.CreateParameter();
                dbparam.ParameterName = "@w_" + cmd.Parameters.Count;
                cmd.Parameters.Add(dbparam);
                dbparam.Value = value;
                return dbparam.ParameterName;
            }
            catch (Exception)
            {

            }
            var parser = _expressionParserRoute.GetExpressionParser(exp.Object);
            if (parser == null)
                throw new ParseUpdateExpressionException($"无法解析{exp.Object}");

            var leftRet = parser.Parse(exp.Object, cmd, findedMembers, isSetter);
            string rightRet = null;
            switch (exp.Method.Name)
            {
                case "Contains":
                    rightRet = $"%{Expression.Lambda(exp.Arguments[0]).Compile().DynamicInvoke()}%";
                    break;
                case "StartsWith":
                    rightRet = $"{Expression.Lambda(exp.Arguments[0]).Compile().DynamicInvoke()}%";
                    break;
                case "EndsWith":
                    rightRet = $"%{Expression.Lambda(exp.Arguments[0]).Compile().DynamicInvoke()}";
                    break;
                case "GetValueOrDefault":
                    return leftRet;
            }
            if (rightRet != null)
            {
                var dbparam = cmd.CreateParameter();
                dbparam.ParameterName = "@w_" + cmd.Parameters.Count;
                cmd.Parameters.Add(dbparam);
                dbparam.Value = rightRet;

                return $"{leftRet} like {dbparam.ParameterName}";
            }

            throw new ParseUpdateExpressionException($"无法解析{expression}");
        }
    }
}
