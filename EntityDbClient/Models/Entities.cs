// Wire property names match EJServer; no dependency on the legacy WPF client.
using System;
namespace EntityDbClient.Models;
public enum Databases_dbTypeEnum { SqlServer=1, Sqlite=2, MySql=3, PostgreSql=4 }
public enum User_RoleEnum { 开发人员=1, 客户端测试人员=2, 数据库设计师=5, 管理员=13, 项目经理=17 }
public sealed class Project
{
    public Int32? id { get; set; }
    public String Name { get; set; } = "";
}
public sealed class Databases
{
    public Int32? id { get; set; }
    public Int32? ProjectID { get; set; }
    public String Name { get; set; } = "";
    public Databases_dbTypeEnum? dbType { get; set; }
    public String conStr { get; set; } = "";
    public String dllPath { get; set; } = "";
    public Int32? iLock { get; set; } = 0;
    public String NameSpace { get; set; } = "";
    public String Guid { get; set; } = "";
}
public sealed class User
{
    public Int32? id { get; set; }
    public User_RoleEnum? Role { get; set; }
    public String Name { get; set; } = "";
    public String Password { get; set; } = "";
}
public sealed class DBTable
{
    public Int32? id { get; set; }
    public String caption { get; set; } = "";
    public String Name { get; set; } = "";
    public Int32? DatabaseID { get; set; }
    public Int32? iLock { get; set; } = 0;
}
public sealed class DBColumn
{
    public Int32? id { get; set; }
    public String caption { get; set; } = "";
    public String Name { get; set; } = "";
    public Boolean? IsAutoIncrement { get; set; } = false;
    public Boolean? CanNull { get; set; } = true;
    public String dbType { get; set; } = "varchar";
    public String Type { get; set; } = "";
    public String EnumDefine { get; set; } = "";
    public String length { get; set; } = "50";
    public String defaultValue { get; set; } = "";
    public Int32? TableID { get; set; }
    public Boolean? IsPKID { get; set; } = false;
    public Int32? orderid { get; set; }
    public Boolean? IsDiscriminator { get; set; } = false;
    public String ClassName { get; set; } = "";
    public String ClassFullName { get; set; } = "";
}
public sealed class DBModule
{
    public Int32? id { get; set; }
    public String Name { get; set; } = "";
    public Int32? DatabaseID { get; set; }
    public Boolean? IsFolder { get; set; }
    public Int32? parentID { get; set; }
}
public sealed class TableInModule
{
    public Int32? id { get; set; }
    public Int32? TableID { get; set; }
    public Int32? ModuleID { get; set; }
    public Int32? x { get; set; }
    public Int32? y { get; set; }
    public String flag { get; set; } = "";
    public String flag2 { get; set; } = "";
}
public sealed class ClassProperty
{
    public Int32? id { get; set; }
    public Int32? tableid { get; set; }
    public String name { get; set; } = "";
    public String desc { get; set; } = "";
    public Int32? foreignkey_tableid { get; set; }
    public Int32? foreignkey_columnid { get; set; }
    public Boolean? iscollection { get; set; }
}
