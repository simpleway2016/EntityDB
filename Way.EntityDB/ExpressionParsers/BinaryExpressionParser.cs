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
    internal class BinaryExpressionParser : IExpressionParser
    {
        ExpressionParserRoute _expressionParserRoute;
        Type _ExpressionType = typeof(BinaryExpression);
        public Type ExpressionType => _ExpressionType;

        public BinaryExpressionParser(ExpressionParserRoute expressionParserRoute)
        {
            this._expressionParserRoute = expressionParserRoute;

        }

        public string Parse(Expression expression, DbCommand cmd, List<string> findedMembers, bool isSetter)
        {
            var body = (BinaryExpression)expression;
            if (body == null)
                throw new ParseUpdateExpressionException("expression 不是 BinaryExpression");

            var leftParser = _expressionParserRoute.GetExpressionParser(body.Left);
            if (leftParser == null)
                throw new ParseUpdateExpressionException($"{body}左边表达式找不到解析器");

            var rightParser = _expressionParserRoute.GetExpressionParser(body.Right);
            if (rightParser == null)
                throw new ParseUpdateExpressionException($"{body}右边表达式找不到解析器");

            var leftRet = leftParser.Parse(body.Left,cmd,findedMembers, isSetter);
            var rightRet = rightParser.Parse(body.Right, cmd,findedMembers, isSetter);

            switch (body.NodeType)
            {
                case System.Linq.Expressions.ExpressionType.Equal:
                    if(rightRet == "null")
                        return isSetter ? $"{leftRet}=null" : $"{leftRet} is null";
                    return $"{leftRet}={rightRet}";
                case System.Linq.Expressions.ExpressionType.NotEqual:
                    if (rightRet == "null")
                        return $"{leftRet} is not null";
                    return $"{leftRet}<>{rightRet}";
                case System.Linq.Expressions.ExpressionType.GreaterThan:
                    return $"{leftRet}>{rightRet}";
                case System.Linq.Expressions.ExpressionType.GreaterThanOrEqual:
                    return $"{leftRet}>={rightRet}";
                case System.Linq.Expressions.ExpressionType.LessThan:
                    return $"{leftRet}<{rightRet}";
                case System.Linq.Expressions.ExpressionType.LessThanOrEqual:
                    return $"{leftRet}<={rightRet}";
                case System.Linq.Expressions.ExpressionType.And:
                    return $"{leftRet} & {rightRet}";
                case System.Linq.Expressions.ExpressionType.AndAlso:
                    if (findedMembers == null)//表示用在where上面
                        return $"({leftRet} and {rightRet})";
                    else
                        return $"{leftRet} and {rightRet}";
                case System.Linq.Expressions.ExpressionType.Or:
                     return $"{leftRet} | {rightRet}";
                case System.Linq.Expressions.ExpressionType.OrElse:
                    if(findedMembers == null)
                        return $"({leftRet} or {rightRet})";
                    else
                        return $"{leftRet} or {rightRet}";
                case System.Linq.Expressions.ExpressionType.Add:
                    return $"{leftRet}+{rightRet}";
                case System.Linq.Expressions.ExpressionType.Divide:
                    return $"{leftRet}/{rightRet}";
                case System.Linq.Expressions.ExpressionType.Multiply:
                    return $"{leftRet}*{rightRet}";
                case System.Linq.Expressions.ExpressionType.Subtract:
                    return $"{leftRet}-{rightRet}";
                default:
                    throw new ParseUpdateExpressionException("解析表达式失败");
            }
        }
    }
}
