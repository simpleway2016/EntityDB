using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Way.EJServer.Exceptions
{
    public class NoPowerException : Exception
    {
        public NoPowerException(string msg) : base(msg)
        {

        }
    }
}
