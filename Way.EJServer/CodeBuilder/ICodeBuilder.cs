using System;
using System.Collections.Generic;
using System.Linq;

namespace Way.EJServer
{
    interface ICodeBuilder
    {
        void BuilderDB(EJDB db, EJ.Databases database, NamespaceCode namespaceCode, List<EJ.DBTable> tables);
        void BuildTable(EJDB db, NamespaceCode namespaceCode, EJ.DBTable table,List<string> foreignKeys);
        void BuildSimpleTable(EJDB db, NamespaceCode namespaceCode, EJ.DBTable table);
        void BuildVerySimpleTable(EJDB db, NamespaceCode namespaceCode, EJ.DBTable table);
    }
}