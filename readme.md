使用教程：https://www.cnblogs.com/IWings/p/9304874.html  

本组件，在数据查询方面，除了禁用了数据跟踪，其他与EF Core原版一致，所以，如果发现查询方面的问题，请在EF Core源码处提问，https://github.com/aspnet/EntityFrameworkCore  


# 启动服务器

在Way.EJServer\bin\Debug\netcoreapp2.0文件夹里面，创一个run.bat批处理文件，内容如下：

```code
dotnet Way.EJServer.dll 6060
```

6060是本机任意一个没有使用的端口号，表示以6060为端口，创建一个服务器工作空间

# 运行客户端

使用 Avalonia 12.1.4 客户端 EntityDbClient：

```powershell
dotnet run --project EntityDbClient/EntityDbClient.csproj
```

支持多服务器独立 Tab、记住服务器和加密保存用户名密码，以及供 AI Agent 直接操作的本机 HTTP API。
详见 [客户端说明](EntityDbClient/README.md) 和 [Agent 接口说明](EntityDbClient/Agent/AGENT_API.md)。

连接到服务器6060工作空间：
server url:https://localhost:6060  
user name: sa  
password:  1  

# 支持表达式更新数据

```code
var dataObj = db.Account.First();
dataObj.SetValue<Account>(m => m.Money == m.Money - 100 && m.Name == "abc");
db.Update(dataObj, m=>m.id == dataObj.id && m.Money >= 100);
```
以上语句，等于执行sql语句：update Account set Money=Money-100,Name='abc' where id=dataObj.id and Money>=100

# 目前支持的数据库

SqlServer  
Sqlite  
MySql  
PostgreSql  

由于MySQL表结构修改，不支持事务，所以不推荐使用MySQL
