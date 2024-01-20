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
    internal class ConstantExpressionParser : IExpressionParser
    {
        ExpressionParserRoute _expressionParserRoute;
        Type _ExpressionType = typeof(ConstantExpression);
        public Type ExpressionType => _ExpressionType;

        public ConstantExpressionParser(ExpressionParserRoute expressionParserRoute)
        {
            this._expressionParserRoute = expressionParserRoute;

        }

        internal static string getValue(DbCommand cmd,object value)
        {
            if (value is string || value is Array)
            {
                var dbparam = cmd.CreateParameter();
                dbparam.ParameterName = "@w_" + cmd.Parameters.Count;
                dbparam.Value = value;
                cmd.Parameters.Add(dbparam);
                return dbparam.ParameterName;
            }
            else if (value is DateTime || value is DateTime?)
            {
                var dbparam = cmd.CreateParameter();
                dbparam.ParameterName = "@w_" + cmd.Parameters.Count;
                dbparam.Value = value;
                cmd.Parameters.Add(dbparam);
                return dbparam.ParameterName;
            }
            else if (value is bool || value is bool?)
            {
                var dbparam = cmd.CreateParameter();
                dbparam.ParameterName = "@w_" + cmd.Parameters.Count;
                dbparam.Value = value;
                dbparam.DbType = System.Data.DbType.Boolean;
                cmd.Parameters.Add(dbparam);
                return dbparam.ParameterName;
            }
            else if (value != null && value.GetType().IsEnum)
            {
                return Convert.ChangeType(value , value.GetType().GetEnumUnderlyingType()).ToString();
            }
            else if (value == null)
            {
                return "null";
            }
            else
                return value.ToString();
        }

        public string Parse(Expression expression, DbCommand cmd, List<string> findedMembers, bool isSetter)
        {
            var exp = expression as System.Linq.Expressions.ConstantExpression;
            return getValue(cmd , exp.Value);
        }
    }
}
