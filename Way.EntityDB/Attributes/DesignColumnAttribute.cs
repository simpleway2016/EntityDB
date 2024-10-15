using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Way.EntityDB.Attributes
{
    public class DesignColumnAttribute : Attribute
    {
        public string TypeName
        {
            get;
            set;
        }
    }
}
