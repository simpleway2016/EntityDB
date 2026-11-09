# EntityDbClient

用 Avalonia **12.1.4** 重写的 EJClient，目标运行时 .NET 10。官方 DataGrid 独立控件使用其已发布的 **12.1.2**（依赖 Avalonia >= 12.1.0）。
不依赖旧 WPF/WinForms 客户端或 Way.Lib，直接兼容 EJServer HTTP remoting 和二进制文件协议。
旧 `EJClient/` 源码保留用于参考；解决方案中的客户端入口改为 `EntityDbClient`。

## 运行

```powershell
dotnet run --project EntityDbClient/EntityDbClient.csproj
```

或发布：

```powershell
dotnet publish EntityDbClient/EntityDbClient.csproj -c Release -r win-x64 --self-contained true
```

每个连接占一个独立 Tab，内部含项目/数据库树及编辑页面。可同时连接不同服务器，或以不同用户连接相同服务器。
通过顶部“连接 → 新建连接…”（Ctrl+N）打开独立连接窗口。输入地址、用户名和密码后点击连接；最近连接会自动保存，选择记录可恢复地址、用户名和密码。
连接失败不会保存输入的密码。连接窗口内的“移除记录”移除保存记录。关闭 Tab 会取消其网络请求并释放连接。

Windows 连接记录存于 `%LOCALAPPDATA%\EntityDbClient\connections.json`，密码由当前用户 DPAPI 加密，不写明文。
其他平台使用当前用户应用数据目录和权限为 0600 的 AES-GCM 密钥文件。
HTTPS 既支持系统受信任证书，也兼容旧 EJClient 内置的 EJServer 证书，不关闭全部证书验证。

## 功能

- 项目、数据库和表的创建、修改、删除；数据库配置及连接类型。
- 字段、主键、自增、空值、默认值、枚举、jsonb、索引、导航属性设计；字段顺序调整；DTO 生成。
- SQL 查询、分页、单元格编辑、行增删、保存。
- 模型/简单模型/最简模型代码生成，旧格式数据备份导入导出，C# 设计模型还原，其他数据库结构导入。
- 数据库设计模块与可拖动的表卡片布局。
- 用户、密码、权限、变更历史。高级操作页提供完整的服务器操作入口（含问题记录等旧功能）。
- 不展示级联删除功能；编辑旧表时保留其已有配置。
- Agent API：连接管理、表结构读写、全部业务 RPC、文件操作、OpenAPI 和 Python SDK。

展开树节点即加载下一层，支持保留刷新前的展开状态；双击表或模块打开编辑。模块卡片左侧显示中文注释，右侧依次显示字段名、可空类型及长度，完整备注可通过悬停查看。卡片可拖动；“保存布局”只提交已移动表的位置，不重新加载项目树、表结构或模块，未修改时不发送请求。
修改前读取完整表定义；编辑页检测经过本客户端发生的字段/索引/导航属性并发修改。
Agent 写入会刷新对应 Tab 的目录；已打开的编辑页使用“重新加载”同步，避免覆盖未保存内容。
对象操作集中在右侧工具栏的常用命令、更多菜单和树节点右键菜单；高级权限及问题记录通过顶部“工具 → 高级操作”访问。接口模块已从客户端和 Agent 能力目录中移除。

## Agent 接口

参见 [Agent/AGENT_API.md](Agent/AGENT_API.md)。应用启动后发布 `%LOCALAPPDATA%\EntityDbClient\agent.json`，
Agent 可使用随附的 `Agent/agent_client.py`，也可直接按 OpenAPI 调用本机 HTTP API，不需要截图或 computer use。

## 验证

```powershell
dotnet run --project EntityDbClient.Tests/EntityDbClient.Tests.csproj
```

独立控制台测试不依赖生产服务器，启动两个模拟 EJServer 并通过实际 HTTP 测试 Agent API、
会话隔离、会话过期恢复、凭据保存重连、结构读写和旧文件格式；还运行 Avalonia Headless 布局检查。
真实数据库的 DDL 和各数据库驱动仍需使用测试 EJServer 环境进行验收。

网络请求期间在对应服务器 Tab 中显示加载进度和操作说明，并暂时阻止重复操作；连接窗口也有独立的连接进度提示。
对象树、模块画布、表卡片、字段列表、数据和用户列表提供统一右键菜单。模块表卡片的移除操作仅位于右键菜单。
搜索功能已移除。

字段编辑采用原客户端的字段列表及分类属性面板，保留 YES/NO、类型默认长度、枚举和继承设置联动。索引行通过下拉框依次选择唯一性、聚集类型及多个字段，字段重命名时同步更新索引。

关闭页面或断开连接时，仅在存在未保存修改的情况下提示。未修改、修改后恢复原值、保存成功的模块/表结构/数据页面可直接关闭；是否有修改由本地状态比较判断，不额外请求服务器。
