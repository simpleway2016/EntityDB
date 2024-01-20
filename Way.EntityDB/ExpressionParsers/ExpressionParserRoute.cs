using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Way.EntityDB.ExpressionParsers
{
    public class ExpressionParserRoute
    {
        static Type[] AllTypes = null;
        internal Func<string, string> FormatObjectNameFunc { get; }
        Dictionary<Type, IExpressionParser> _dict = new Dictionary<Type, IExpressionParser>();
        public ExpressionParserRoute(Func<string, string> formatObjectNameFunc)
        {
            this.FormatObjectNameFunc = formatObjectNameFunc;
            if (AllTypes == null)
            {
                var itype = typeof(IExpressionParser);
                AllTypes = itype.Assembly.GetTypes().Where(m => m.GetInterfaces().Contains(itype)).ToArray();
            }
            foreach (var t in AllTypes)
            {
                var parser = (IExpressionParser)Activator.CreateInstance(t, new object[] { this });
                _dict[parser.ExpressionType] = parser;
            }
        }

        public IExpressionParser GetExpressionParser(Expression expression)
        {
            var type = expression.GetType();
            while(type.Attributes.HasFlag(TypeAttributes.Public) == false && type.BaseType != null)
            {
                type = type.BaseType;
            }
            if (_dict.TryGetValue(type, out IExpressionParser o))
                return o;
            return null;
        }
    }
}
