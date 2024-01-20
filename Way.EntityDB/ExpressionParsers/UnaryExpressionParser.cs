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
    internal class UnaryExpressionParser : IExpressionParser
    {
        ExpressionParserRoute _expressionParserRoute;
        Type _ExpressionType = typeof(UnaryExpression);
        public Type ExpressionType => _ExpressionType;

        public UnaryExpressionParser(ExpressionParserRoute expressionParserRoute)
        {
            this._expressionParserRoute = expressionParserRoute;

        }

        public string Parse(Expression expression, DbCommand cmd, List<string> findedMembers, bool isSetter)
        {
            while (expression is UnaryExpression)
            {
                UnaryExpression uexp = (UnaryExpression)expression;
                if (uexp.NodeType == System.Linq.Expressions.ExpressionType.Convert)
                {
                    expression = uexp.Operand;
                }
                else if (uexp.NodeType == System.Linq.Expressions.ExpressionType.Negate)
                {
                    return $"-{Parse(uexp.Operand,cmd,findedMembers, isSetter)}";
                }
                else if (uexp.NodeType == System.Linq.Expressions.ExpressionType.Not)
                {
                    return $"~{Parse(uexp.Operand, cmd, findedMembers, isSetter)}";
                }
                else
                    break;
            }

            var parser = _expressionParserRoute.GetExpressionParser(expression);

            if (parser != null)
            {
               return parser.Parse(expression, cmd, findedMembers, isSetter);
            }
            else
            {
                var value = Expression.Lambda(expression).Compile().DynamicInvoke();
                return ConstantExpressionParser.getValue(cmd, value);
            }
        }
    }
}
