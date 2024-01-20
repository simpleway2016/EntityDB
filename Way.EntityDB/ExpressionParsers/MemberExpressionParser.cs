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
    internal class MemberExpressionParser : IExpressionParser
    {
        ExpressionParserRoute _expressionParserRoute;
        Type _ExpressionType = typeof(MemberExpression);
        public Type ExpressionType => _ExpressionType;

        public MemberExpressionParser(ExpressionParserRoute expressionParserRoute)
        {
            this._expressionParserRoute = expressionParserRoute;

        }

        public string Parse(Expression expression, DbCommand cmd, List<string> findedMembers, bool isSetter)
        {
            var exp = expression as System.Linq.Expressions.MemberExpression;
            if (exp == null)
                throw new ParseUpdateExpressionException("左侧必须是成员表达式");
            if(exp.Expression is ParameterExpression)
            {
                findedMembers?.Add(exp.Member.Name.ToLower());
                return _expressionParserRoute.FormatObjectNameFunc(exp.Member.Name.ToLower());
            }
            else
            {
                var value = Expression.Lambda(expression).Compile().DynamicInvoke();
                return ConstantExpressionParser.getValue(cmd, value);
            }
        }
    }
}
