using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Way.EntityDB.ExpressionParsers
{
    public interface IExpressionParser
    {
        Type ExpressionType { get; }
        string Parse(Expression expression, System.Data.Common.DbCommand cmd,List<string> findedMembers,bool isSetter);
    }
}
