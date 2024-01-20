using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Way.EntityDB.Exceptons
{
    public class ParseUpdateExpressionException : Exception
    {
        public ParseUpdateExpressionException( string msg):base(msg )
        {

        }
    }
}
