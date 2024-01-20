using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Way.EntityDB.Attributes
{
    public class TableConfigAttribute : Attribute
    {
        public string AutoSetPropertyNameOnInsert
        {
            get;
            set;
        }
        public object AutoSetPropertyValueOnInsert
        {
            get;
            set;
        }
    }
}
