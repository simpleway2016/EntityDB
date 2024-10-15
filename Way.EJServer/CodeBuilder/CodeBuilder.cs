using Microsoft.CSharp;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Way.EJServer
{
    /*
    这里修改了EntityFramework src\EntityFramework\ModelConfiguration\Mappers\TypeMapper.cs
     * var baseType = _mappingContext.Model.GetEntityType(type.BaseType().Name);
                baseType = null;
     * 这里把baseType设为了null
    */

    class CodeBuilder : ICodeBuilder
    {

        public void BuilderDB(EJDB db, EJ.Databases databaseObj, NamespaceCode namespaceCode, List<EJ.DBTable> tables)
        {
            CodeItem classCode = new CodeItem($"public class {databaseObj.Name} : Way.EntityDB.DBContext");
            namespaceCode.AddItem(classCode);



            StringBuilder result = new StringBuilder();

            //构造函数
            CodeItem initFunc = new CodeItem($" public {databaseObj.Name}(string connection, Way.EntityDB.DatabaseType dbType , bool upgradeDatabase = true): base(connection, dbType , upgradeDatabase)");
            classCode.AddItem(initFunc);
            initFunc.AddString("if (!setEvented)");
            initFunc.AddString("{");
            initFunc.AddString("    lock (lockObj)");
            initFunc.AddString("    {");
            initFunc.AddString("        if (!setEvented)");
            initFunc.AddString("        {");
            initFunc.AddString("            setEvented = true;");
            initFunc.AddString("            Way.EntityDB.DBContext.BeforeDelete += Database_BeforeDelete;");
            initFunc.AddString("        }");
            initFunc.AddString("    }");
            initFunc.AddString("}");


            classCode.AddString("static object lockObj = new object();");
            classCode.AddString("static bool setEvented = false;");

            //级联删除
            CodeItem beforeDeleteFunc = new CodeItem("static void Database_BeforeDelete(object sender, Way.EntityDB.DatabaseModifyEventArg e)");
            classCode.AddItem(beforeDeleteFunc);
            beforeDeleteFunc.AddString($" var db =  sender as " + databaseObj.Name + @";");
            beforeDeleteFunc.AddString($"if (db == null) return;");

            StringBuilder _Database_deleteCodes = new StringBuilder();
            foreach (var t in tables)
            {

                var delConfigs = db.DBDeleteConfig.Where(m => m.TableID == t.id).ToList();

                if (delConfigs.Count > 0)
                {
                    CodeItem codeitem = new CodeItem($"if (e.DataItem is {t.Name})");
                    beforeDeleteFunc.AddItem(codeitem);
                    codeitem.AddString($"var deletingItem = ({t.Name})e.DataItem;");

                    StringBuilder codestrs = new StringBuilder();
                    for (int i = 0; i < delConfigs.Count; i++)
                    {
                        var configitem = delConfigs[i];
                        var delDBTable = db.DBTable.FirstOrDefault(m => m.id == configitem.RelaTableID);
                        var relaColumn = db.DBColumn.FirstOrDefault(m => m.id == configitem.RelaColumID);
                        var rela_pkcolumn = db.DBColumn.FirstOrDefault(m => m.TableID == configitem.RelaTableID && m.IsPKID == true);
                        if (rela_pkcolumn == null)
                            throw new Exception("关联表" + delDBTable.Name + "没有定义主键");

                        codeitem.AddString($"var items{i} = (from m in db.{delDBTable.Name} where m.{relaColumn.Name} == deletingItem.id");
                        codeitem.AddItem(new CodeItem($"select new {delDBTable.Name}")
                            .AddString($"{rela_pkcolumn.Name} = m.{rela_pkcolumn.Name}")
                            );
                        codeitem.AddString(");");

                        codeitem.AddItem(new CodeItem("while(true)")
                            .AddString($"var data2del = items{i}.Take(100).ToList();")
                            .AddString("if(data2del.Count() ==0)")
                            .AddString("    break;")
                            .AddString("foreach (var t in data2del)")
                            .AddString("{")
                            .AddString("    t.ChangedProperties.Clear();")
                            .AddString("    db.Delete(t);")
                            .AddString("}")
                            );

                    }
                    beforeDeleteFunc.AddString("");
                }
            }


            CodeItem modelbuildFunc = new CodeItem("protected override void OnModelCreating(ModelBuilder modelBuilder)");
            classCode.AddItem(modelbuildFunc);

            foreach (var t in tables)
            {
                var pkcolumn = db.DBColumn.FirstOrDefault(m => m.TableID == t.id && m.IsPKID == true);
                if (pkcolumn == null)
                    throw new Exception(string.Format("表{0}缺少主键", t.Name));
                modelbuildFunc.AddString($"modelBuilder.Entity<{t.Name}>().HasKey(m => m.{pkcolumn.Name});");

                var discriminatorColumn = db.DBColumn.FirstOrDefault(m => m.TableID == t.id && m.IsDiscriminator == true);
                if (discriminatorColumn != null)
                {
                    var classNames = ParseNames(discriminatorColumn.EnumDefine).ToArray();
                    modelbuildFunc.AddString($"modelBuilder.Entity<{t.Name}>().HasDiscriminator<{t.Name}_{discriminatorColumn.Name}Enum>(\"{discriminatorColumn.Name}\")");

                    modelbuildFunc.AddString($".HasValue<{t.Name}>(({t.Name}_{discriminatorColumn.Name}Enum)0)");

                    foreach (var classnameitem in classNames)
                    {
                        modelbuildFunc.AddString($".HasValue<{classnameitem.Name}>({t.Name}_{discriminatorColumn.Name}Enum.{classnameitem.Name})");
                    }
                    modelbuildFunc.AddString(";");
                }

            }


            foreach (var t in tables)
            {
                classCode.AddString($"System.Linq.IQueryable<{t.Name}> _{t.Name};");
                PropertyCodeItem proCodeItem = new PropertyCodeItem(t.Name);
                proCodeItem.Comment = t.caption;
                classCode.AddItem(proCodeItem);
                proCodeItem.Modification = "public virtual";
                proCodeItem.PropertyType = $"System.Linq.IQueryable<{t.Name}>";
                proCodeItem.ItemForSet = null;
                proCodeItem.ItemForGet.AddString($"if (_{t.Name} == null)");
                proCodeItem.ItemForGet.AddString("{");
                proCodeItem.ItemForGet.AddString($"    _{t.Name} = this.Set<{t.Name}>();");
                proCodeItem.ItemForGet.AddString("}");
                proCodeItem.ItemForGet.AddString($"return _{t.Name};");

                var discriminatorColumn = db.DBColumn.FirstOrDefault(m => m.TableID == t.id && m.IsDiscriminator == true);
                if (discriminatorColumn != null)
                {
                    var classNames = ParseNames(discriminatorColumn.EnumDefine).ToArray();
                    foreach (var g in classNames)
                    {
                        classCode.AddString($"System.Linq.IQueryable<{g.Name}> _{g.Name};");
                        proCodeItem = new PropertyCodeItem(g.Name);
                        classCode.AddItem(proCodeItem);
                        proCodeItem.Modification = "public virtual";
                        proCodeItem.PropertyType = $"System.Linq.IQueryable<{g.Name}>";
                        proCodeItem.ItemForSet = null;
                        proCodeItem.ItemForGet.AddString($"if (_{g.Name} == null)");
                        proCodeItem.ItemForGet.AddString("{");
                        proCodeItem.ItemForGet.AddString($"    _{g.Name} = this.Set<{g.Name}>();");
                        proCodeItem.ItemForGet.AddString("}");
                        proCodeItem.ItemForGet.AddString($"return _{g.Name};");
                    }
                }
            }
            result.Append("\r\n");



            string content = GetDesignData(db, databaseObj);

            var getDesignStringFunc = new CodeItem("protected override string GetDesignString()");
            classCode.AddItem(getDesignStringFunc);
            getDesignStringFunc.AddString("var result = new StringBuilder();");
            getDesignStringFunc.AddString($"result.Append(\"\\r\\n\");");

            for (int i = 0; i < content.Length; i += 200)
            {
                int len = Math.Min(content.Length - i, 200);
                getDesignStringFunc.AddString("result.Append(\"" + content.Substring(i, len) + "\");");
            }
            getDesignStringFunc.AddString("return result.ToString();");


            //记录数据库设计数据
            StringBuilder codeend = new StringBuilder();
            codeend.AppendLine("/*<design>");
            outputDesigns(codeend, db, databaseObj);
            codeend.AppendLine("<design>*/");

            namespaceCode.CodeEnd = codeend.ToString();

        }

        internal static string GetDesignData(EJDB db, EJ.Databases databaseObj)
        {
            var dt = db.Database.SelectDataSet("select actionid as [id],type,content,databaseid from designhistory where databaseid=" + databaseObj.id + " order by [actionid]");
            dt.Tables[0].TableName = databaseObj.dbType.ToString();
            dt.DataSetName = databaseObj.Guid;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            return Convert.ToBase64String(GZip(System.Text.Encoding.UTF8.GetBytes(json)));
        }

        void outputDesigns(StringBuilder result, EJDB db, EJ.Databases databaseObj)
        {
            DesignData data = new DesignData();
            data.Database = databaseObj;
            data.Modules = db.DBModule.Where(m => m.DatabaseID == databaseObj.id).ToArray();
            data.Tables = db.DBTable.Where(m => m.DatabaseID == databaseObj.id).ToArray();
            var tableids = data.Tables.Select(m => m.id.Value).ToArray();
            data.TableInModules = db.TableInModule.Where(m => tableids.Contains(m.TableID.Value)).ToArray();
            data.IDXIndexes = db.IDXIndex.Where(m => tableids.Contains(m.TableID.Value)).ToArray();
            data.DBDeleteConfigs = db.DBDeleteConfig.Where(m => tableids.Contains(m.TableID.Value)).ToArray();
            data.DBColumns = db.DBColumn.Where(m => tableids.Contains(m.TableID.Value)).OrderBy(m => m.orderid).ToArray();
            data.classproperties = db.classproperty.Where(m => tableids.Contains(m.tableid.Value)).ToArray();
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            string content = Convert.ToBase64String(GZip(System.Text.Encoding.UTF8.GetBytes(json)));

            for (int i = 0; i < content.Length; i += 200)
            {
                int len = Math.Min(content.Length - i, 200);
                result.AppendLine(content.Substring(i, len));
            }
        }

        static byte[] GZip(byte[] byteArray)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                System.IO.Compression.GZipStream sw = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Compress);
                //Compress
                sw.Write(byteArray, 0, byteArray.Length);
                //Close, DO NOT FLUSH cause bytes will go missing...
                sw.Close();
                //Transform byte[] zip data to string
                return ms.ToArray();
            }
        }

        internal static byte[] UnGzip(byte[] zippedData)
        {
            MemoryStream ms = new MemoryStream(zippedData);
            GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Decompress);
            MemoryStream outBuffer = new MemoryStream();
            byte[] block = new byte[1024];
            while (true)
            {
                int bytesRead = compressedzipStream.Read(block, 0, block.Length);
                if (bytesRead <= 0)
                    break;
                else
                    outBuffer.Write(block, 0, bytesRead);
            }
            compressedzipStream.Close();
            return outBuffer.ToArray();
        }

        public void BuildTable(EJDB db, bool columnToLower, NamespaceCode namespaceCode, EJ.DBTable table, List<string> foreignKeys)
        {
            var columns = db.DBColumn.Where(m => m.TableID == table.id).OrderBy(m => m.orderid).ToList();
            BuildTable(db, columnToLower, namespaceCode, table, columns, foreignKeys);
        }
        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        public static string GetLinqTypeString(string type, bool canNull)
        {
            switch (type)
            {

                case "bigint":
                    if (canNull)
                        return "System.Nullable<Int64>";
                    else
                        return "Int64";
                case "binary":
                    return "Byte[]";
                case "bit":
                    if (canNull)
                        return "System.Nullable<Boolean>";
                    else
                        return "Boolean";
                case "char":
                    if (canNull)
                        return "string?";
                    else
                        return "string";
                case "datetime":
                case "datetimezone":
                    if (canNull)
                        return "System.Nullable<DateTime>";
                    else
                        return "DateTime";
                case "date":
                    if (canNull)
                        return "System.Nullable<DateTime>";
                    else
                        return "DateTime";
                case "time":
                    if (canNull)
                        return "System.Nullable<TimeSpan>";
                    else
                        return "TimeSpan";


                case "decimal":
                    if (canNull)
                        return "System.Nullable<Decimal>";
                    else
                        return "Decimal";


                case "float":
                    if (canNull)
                        return "System.Nullable<float>";
                    else
                        return "float";


                case "double":
                    if (canNull)
                        return "System.Nullable<double>";
                    else
                        return "double";

                case "image":
                    if (canNull)
                        return "Byte[]?";
                    else
                        return "Byte[]";
                case "int":
                    if (canNull)
                        return "System.Nullable<Int32>";
                    else
                        return "Int32";

                case "money":
                    if (canNull)
                        return "System.Nullable<Decimal>";
                    else
                        return "Decimal";
                case "nchar":
                    if (canNull)
                        return "string?";
                    else
                        return "string";
                case "ntext":
                    if (canNull)
                        return "string?";
                    else
                        return "string";
                case "numeric":
                    if (canNull)
                        return "System.Nullable<Decimal>";
                    else
                        return "Decimal";
                case "nvarchar":
                    if (canNull)
                        return "string?";
                    else
                        return "string";
                case "real":
                    if (canNull)
                        return "System.Nullable<float>";
                    else
                        return "float";
                case "smalldatetime":
                    if (canNull)
                        return "System.Nullable<DateTime>";
                    else
                        return "DateTime";
                case "smallint":
                    if (canNull)
                        return "System.Nullable<Int16>";
                    else
                        return "Int16";

                case "smallmoney":
                    if (canNull)
                        return "System.Nullable<Decimal>";
                    else
                        return "Decimal";

                case "text":
                    if (canNull)
                        return "string?";
                    else
                        return "string";
                case "timestamp":
                    if (canNull)
                        return "Byte[]?";
                    else
                        return "Byte[]";
                case "varbinary":
                    if (canNull)
                        return "Byte[]?";
                    else
                        return "Byte[]";
                case "varchar":
                    if (canNull)
                        return "string?";
                    else
                        return "string";
                case "longtext":
                    if (canNull)
                        return "string?";
                    else
                        return "string";
                case "jsonb":
                    if (canNull)
                        return "object?";
                    else
                        return "object";
                default:
                    return type;

            }
        }

        class TempAssembly
        {
            public string ClassFullName;
            public Type TableType;
        }
        class ClassName
        {
            public string Name;
            public string BaseName;
            public string Comment;
        }
        static List<ClassName> ParseNames(string value)
        {
            List<ClassName> names = new List<ClassName>();
            string[] lines = value.Split('\n');
            StringBuilder comment = new StringBuilder();
            foreach (var line in lines)
            {
                if (line.Trim().StartsWith("//"))
                {
                    if (comment.Length > 0)
                        comment.Append("\r\n");
                    try
                    {
                        comment.Append(line.Trim().Substring(2));

                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    var match = Regex.Match(line, @"(?<name>\w+)[ ]?=(?<value>[ 0-9\w\<\>\(\)\|\+\-]+)");
                    if (match.Length > 0)
                    {
                        names.Add(new ClassName() { Name = match.Groups["name"].Value, Comment = comment.ToString() });
                        comment = new StringBuilder();
                    }
                }
            }

            var ms = Regex.Matches(value, @"(?<name>\w+)[ ]?=(?<value>[ 0-9\w\<\>\(\)\|\+\-]+)");
            foreach (Match m in ms)
            {
                var name = m.Groups["name"].Value;
                var currentClass = names.FirstOrDefault(m => m.Name == name);
                var arr = Regex.Split(m.Groups["value"].Value, @"\||\+|\-");
                foreach (var item in arr)
                {
                    var itemstr = item.Trim();
                    if (names.Any(m => m.Name == itemstr))
                    {
                        currentClass.BaseName = itemstr;
                        break;
                    }
                }
            }
            return names;
        }
        
        static void BuildTable(EJDB db, bool columnToLower, NamespaceCode namespaceCode, EJ.DBTable table, List<EJ.DBColumn> columns, List<string> foreignKeys)
        {
            var pkcolumn = columns.FirstOrDefault(m => m.IsPKID == true);
            CodeItem classCode = new CodeItem($"public partial class {table.Name} :Way.EntityDB.DataItem");
            namespaceCode.AddItem(classCode);
            classCode.Comment = table.caption;

            Dictionary<string, CodeItem> otherClassCode = new Dictionary<string, CodeItem>();
            var discriminatorColumn = columns.FirstOrDefault(m => m.IsDiscriminator == true && !string.IsNullOrEmpty(m.EnumDefine?.Trim()));

            ClassName[] classNames = null;
            if (discriminatorColumn != null)
            {
                classNames = ParseNames(discriminatorColumn.EnumDefine).ToArray();
                classCode.Attributes.Add(@"[TableConfig( AutoSetPropertyNameOnInsert = """ + discriminatorColumn.Name + @""" , AutoSetPropertyValueOnInsert=(" + table.Name + "_" + discriminatorColumn.Name + @"Enum)0)]");

                foreach (var classnameitem in classNames)
                {
                    var myClsCodeItem = new CodeItem($"public class {classnameitem.Name} :{((classnameitem.BaseName == null) ? table.Name : classnameitem.BaseName)}");
                    namespaceCode.AddItem(myClsCodeItem);
                    myClsCodeItem.Comment = classnameitem.Comment;
                    myClsCodeItem.Attributes.Add(@"[TableConfig(AutoSetPropertyNameOnInsert = """ + discriminatorColumn.Name + @""" , AutoSetPropertyValueOnInsert=" + table.Name + "_" + discriminatorColumn.Name + @"Enum." + classnameitem.Name + @")]");
                    otherClassCode[classnameitem.Name] = myClsCodeItem;
                    myClsCodeItem.AddString("");
                }
            }
            else
            {
                classCode.Attributes.Add("[TableConfig]");
            }

            classCode.Attributes.Add($"[Table(\"{table.Name.ToLower()}\")]");
            classCode.Attributes.Add("[Way.EntityDB.DataItemJsonConverter]");


            CodeItem curClassCodeItem = null;

            foreach (var column in columns)
            {
                if (discriminatorColumn != null && classNames != null && !string.IsNullOrEmpty(column.ClassName?.Trim()) && columns.Any(m => m.IsDiscriminator == true) &&
                    otherClassCode.ContainsKey(column.ClassName))
                {
                    curClassCodeItem = otherClassCode[column.ClassName];
                }
                else
                {
                    curClassCodeItem = classCode;
                }

                PropertyCodeItem columnCodeItem = new PropertyCodeItem(column.Name);
                columnCodeItem.Modification = "public virtual";

                string caption = column.caption == null ? "" : column.caption;
                if (caption.Contains(","))
                {
                    caption = caption.Substring(0, caption.IndexOf(","));
                }
                else if (caption.Contains("，"))
                {
                    caption = caption.Substring(0, caption.IndexOf("，"));
                }

                string dataType = column.ClassFullName;
                if (string.IsNullOrWhiteSpace(dataType))
                {
                    dataType = GetLinqTypeString(column.dbType, column.CanNull.GetValueOrDefault() || column.IsAutoIncrement == true);
                }
                if (column.dbType == "jsonb" && column.CanNull == true)
                {
                    if (dataType.EndsWith("?") == false)
                        dataType += "?";
                }

                string eqString = "";
                if (!string.IsNullOrEmpty(column.EnumDefine) && column.dbType == "int")
                {
                    if (column.EnumDefine.Trim().StartsWith("$"))
                    {
                        var target = column.EnumDefine.Trim().Substring(1).Split('.');
                        if (column.CanNull == true)
                            dataType = target[0] + "_" + target[1] + "Enum?";
                        else
                            dataType = target[0] + "_" + target[1] + "Enum";
                    }
                    else
                    {
                        string[] enumitems = column.EnumDefine.Replace("\r", "").Split('\n');

                        CodeItem codeEnum = new CodeItem($"public enum {table.Name}_{column.Name}Enum:int");
                        namespaceCode.AddItem(codeEnum);

                        CodeItem codeEnumField = new CodeItem();
                        for (int i = 0; i < enumitems.Length; i++)
                        {
                            var code = enumitems[i].Trim();
                            if (code.Length == 0)
                                continue;
                            if (code.StartsWith("//"))
                            {
                                if (code.Length > 2)
                                {
                                    if (codeEnumField.Comment.Length > 0)
                                        codeEnumField.Comment += "\r\n";
                                    codeEnumField.Comment += code.Substring(2);
                                }

                            }
                            else
                            {
                                codeEnumField.Body = code;
                                codeEnum.AddItem(codeEnumField);
                                codeEnumField = new CodeItem();
                            }

                        }
                        if (column.CanNull == true)
                            dataType = table.Name + "_" + column.Name + "Enum?";
                        else
                            dataType = table.Name + "_" + column.Name + "Enum";
                    }
                }


                if (column.IsDiscriminator == true && !string.IsNullOrEmpty(column.EnumDefine?.Trim()))
                {
                    if (dataType.EndsWith("?"))
                        dataType = dataType.Substring(0, dataType.Length - 1);
                }

                if (!string.IsNullOrEmpty(column.defaultValue))
                {
                    if (column.defaultValue.Trim().Length > 0)
                    {
                        eqString = column.defaultValue.Trim();
                        if (dataType == "string" || dataType == "string?")
                        {
                            if (eqString.StartsWith("'") && eqString.EndsWith("'") && eqString.Length > 1)
                                eqString = eqString.Substring(1, eqString.Length - 2);
                            eqString = "\"" + eqString + "\"";
                        }
                        else if (dataType == "System.Nullable<Decimal>" || dataType == "Decimal")
                        {
                            eqString = eqString + "m";
                        }
                        else if (dataType == "System.Nullable<float>" || dataType == "float")
                        {
                            eqString = eqString + "f";
                        }
                        else if (dataType == "System.Nullable<Boolean>" || dataType == "Boolean")
                        {
                            if (eqString == "1")
                                eqString = "true";
                            else if (eqString == "0")
                                eqString = "false";
                        }
                        else if (!string.IsNullOrEmpty(column.EnumDefine) && column.dbType == "int")
                        {
                            eqString = "(" + dataType + ")(" + eqString + ")";
                        }
                        eqString = "=" + eqString;
                    }
                }

                if (column.dbType.Contains("char"))
                {
                    try
                    {
                        columnCodeItem.Attributes.Add($"[MaxLength({Convert.ToInt32(column.length)})]");
                    }
                    catch
                    {
                    }
                }

                if (column.IsPKID == true)
                {
                    columnCodeItem.Attributes.Add("[Key]");
                }
                if (column.IsAutoIncrement == true)
                {
                    columnCodeItem.Attributes.Add("[DatabaseGenerated(DatabaseGeneratedOption.Identity)]");
                }
                if (column.CanNull == false)
                {
                    columnCodeItem.Attributes.Add("[DisallowNull]");
                    //columnCodeItem.Attributes.Add("[Required]");
                }
                if (!string.IsNullOrEmpty(column.caption))
                {
                    columnCodeItem.Attributes.Add($"[Display(Name = \"{column.caption.Replace("\r", "").Replace("\n", " ").Replace("\"", "\\\"")}\")]");
                }

                columnCodeItem.Attributes.Add($"[DesignColumn(TypeName = \"{column.dbType}\")]");
                if (columnToLower)
                {
                    if (column.dbType == "jsonb")
                    {
                        columnCodeItem.Attributes.Add($"[Column(\"{column.Name.ToLower()}\",TypeName = \"jsonb\")]");
                    }
                    else
                    {
                        columnCodeItem.Attributes.Add($"[Column(\"{column.Name.ToLower()}\")]");
                    }
                }
                else
                {
                    if (column.dbType == "jsonb")
                    {
                        columnCodeItem.Attributes.Add($"[Column(\"{column.Name}\",TypeName = \"jsonb\")]");
                    }
                    else
                    {
                        columnCodeItem.Attributes.Add($"[Column(\"{column.Name}\")]");
                    }
                }
                columnCodeItem.PropertyType = dataType;
                columnCodeItem.Comment = column.caption;
                curClassCodeItem.AddString($"{dataType} _{column.Name + eqString};");
                curClassCodeItem.AddItem(columnCodeItem);

                columnCodeItem.ItemForGet.AddString($"return _{column.Name};");
                if (column.dbType != "jsonb")
                {
                    columnCodeItem.ItemForSet.AddString($"if ((_{column.Name} != value))");
                    columnCodeItem.ItemForSet.AddString("{");
                }
                columnCodeItem.ItemForSet.AddString($"    SendPropertyChanging(\"{column.Name.Trim()}\",_{column.Name.Trim()},value);");
                columnCodeItem.ItemForSet.AddString($"    _{column.Name} = value;");
                columnCodeItem.ItemForSet.AddString($"    SendPropertyChanged(\"{column.Name.Trim()}\");");
                if (column.dbType != "jsonb")
                {
                    columnCodeItem.ItemForSet.AddString("}");
                }

            }

            var classProperties = db.classproperty.Where(m => m.tableid == table.id).ToArray();
            foreach (var pro in classProperties)
            {
                try
                {
                    var foreign_table = db.DBTable.FirstOrDefault(m => m.id == pro.foreignkey_tableid);
                    var column = db.DBColumn.FirstOrDefault(m => m.id == pro.foreignkey_columnid);


                    if (pro.iscollection == false)
                    {

                        if (column != null && column.TableID == table.id)
                        {
                            var procodeitem = new CodeItem($"public virtual {foreign_table.Name} {pro.name} {{ get; set; }}");
                            if (!string.IsNullOrWhiteSpace(pro.desc))
                            {
                                procodeitem.Comment = pro.desc;
                            }
                            classCode.AddItem(procodeitem);

                            if (foreignKeys.Contains($"{table.Name}->{column.Name}") == false)
                            {
                                foreignKeys.Add($"{table.Name}->{column.Name}");
                                procodeitem.Attributes.Add(@"[ForeignKey(""" + column.Name + @""")]");
                            }

                        }
                        else
                        {
                            //与其他表一对一
                            var procodeitem = new CodeItem($"public virtual {foreign_table.Name} {pro.name} {{ get; set; }}");
                            if (!string.IsNullOrWhiteSpace(pro.desc))
                            {
                                procodeitem.Comment = pro.desc;
                            }
                            classCode.AddItem(procodeitem);

                        }
                    }
                    else
                    {
                        if (column != null)
                        {

                            //与其他表多对一
                            var procodeitem = new CodeItem($"public virtual ICollection<{foreign_table.Name}> {pro.name} {{ get; set; }}");
                            if (!string.IsNullOrWhiteSpace(pro.desc))
                            {
                                procodeitem.Comment = pro.desc;
                            }
                            classCode.AddItem(procodeitem);

                            if (foreignKeys.Contains($"{foreign_table.Name}->{column.Name}") == false)
                            {
                                foreignKeys.Add($"{foreign_table.Name}->{column.Name}");
                                procodeitem.Attributes.Add(@"[ForeignKey(""" + column.Name + @""")]");
                            }

                        }
                    }
                }
                catch
                {
                }

            }

            CodeItem setValueFunc = new CodeItem($" public virtual void SetValue(System.Linq.Expressions.Expression<Func<{table.Name}, bool>> exp)");
            setValueFunc.Comment = "<?xml version=\"1.0\"?><root><summary>把字段的更新，设置为一个指定的表达式值</summary><param name=\"exp\">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + \"aa\"，相当于sql语句的 age=age+1,name=name + 'aa'</param></root>";
            classCode.AddItem(setValueFunc);
            setValueFunc.AddString($"base.SetValue<{table.Name}>(exp);");
        }
        public static string GetTypeString(string type)
        {
            switch (type)
            {
                case "bigint":
                    return "Int64";
                case "binary":
                    return "Byte[]";
                case "bit":
                    return "Boolean";
                case "char":
                    return "string";
                case "datetime":
                    return "DateTime";
                case "date":
                    return "DateTime";
                case "time":
                    return "DateTime";
                case "decimal":
                    return "Decimal";
                case "float":
                    return "Double";
                case "image":
                    return "Byte[]";
                case "int":
                    return "Int32";
                case "money":
                    return "Decimal";
                case "nchar":
                    return "String";
                case "ntext":
                    return "String";
                case "numeric":
                    return "Decimal";
                case "nvarchar":
                    return "String";
                case "real":
                    return "Single";
                case "smalldatetime":
                    return "DateTime";
                case "smallint":
                    return "Int16";
                case "smallmoney":
                    return "Decimal";
                case "sql_variant":
                    return "Object";
                case "text":
                    return "String";
                case "timestamp":
                    return "Byte[]";
                case "tinyint":
                    return "Byte";
                case "uniqueidentifier":
                    return "Guid";
                case "varbinary":
                    return "Byte[]";
                case "varchar":
                    return "String";

            }
            return "";
        }

        /// <summary>
        /// 生成一般的model类
        /// </summary>
        /// <param name="db"></param>
        /// <param name="nameSpace"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public void BuildSimpleTable(EJDB db, NamespaceCode namespaceCode, EJ.DBTable table)
        {
            var columns = db.DBColumn.Where(m => m.TableID == table.id).OrderBy(m => m.orderid).ToList();
            BuildSimpleTable(db, namespaceCode, table, columns);
        }

        static void BuildSimpleTable(EJDB db, NamespaceCode namespaceCode, EJ.DBTable table, List<EJ.DBColumn> columns)
        {
            var pkcolumn = columns.FirstOrDefault(m => m.IsPKID == true);


            CodeItem classCode = new CodeItem($"public partial class {table.Name} :Way.Lib.DataModel");
            namespaceCode.AddItem(classCode);
            classCode.Comment = table.caption;

            //构造函数
            classCode.AddItem(new CodeItem($"public {table.Name}()").AddString(""));

            foreach (var column in columns)
            {
                FieldCodeItem fieldCodeItem = new FieldCodeItem($"_{column.Name}");
                classCode.AddItem(fieldCodeItem);
                PropertyCodeItem proCodeItem = new PropertyCodeItem(column.Name);
                classCode.AddItem(proCodeItem);
                proCodeItem.Comment = column.caption;

                string dataType = column.ClassFullName;
                if (string.IsNullOrWhiteSpace(dataType))
                {
                    dataType = GetLinqTypeString(column.dbType, column.CanNull.GetValueOrDefault() || column.IsAutoIncrement == true);
                }
                string eqString = "";
                if (!string.IsNullOrEmpty(column.EnumDefine) && column.dbType == "int")
                {
                    if (column.EnumDefine.Trim().StartsWith("$"))
                    {
                        var target = column.EnumDefine.Trim().Substring(1).Split('.');
                        if (column.CanNull == true)
                            dataType = target[0] + "_" + target[1] + "Enum?";
                        else
                            dataType = target[0] + "_" + target[1] + "Enum";
                    }
                    else
                    {
                        string[] enumitems = column.EnumDefine.Replace("\r", "").Split('\n');

                        CodeItem codeEnum = new CodeItem($"public enum {table.Name}_{column.Name}Enum:int");
                        namespaceCode.AddItem(codeEnum);

                        CodeItem codeEnumField = new CodeItem();
                        for (int i = 0; i < enumitems.Length; i++)
                        {
                            var code = enumitems[i].Trim();
                            if (code.Length == 0)
                                continue;
                            if (code.StartsWith("//"))
                            {
                                if (code.Length > 2)
                                {
                                    if (codeEnumField.Comment.Length > 0)
                                        codeEnumField.Comment += "\r\n";
                                    codeEnumField.Comment += code.Substring(2);
                                }

                            }
                            else
                            {
                                codeEnumField.Body = code;
                                codeEnum.AddItem(codeEnumField);
                                codeEnumField = new CodeItem();
                            }

                        }
                        if (column.CanNull == true)
                            dataType = table.Name + "_" + column.Name + "Enum?";
                        else
                            dataType = table.Name + "_" + column.Name + "Enum";
                    }
                }


                if (!string.IsNullOrEmpty(column.defaultValue))
                {
                    if (column.defaultValue.Trim().Length > 0)
                    {
                        eqString = column.defaultValue.Trim();
                        if (dataType == "string" || dataType == "string?")
                        {
                            if (eqString.StartsWith("'") && eqString.EndsWith("'") && eqString.Length > 1)
                                eqString = eqString.Substring(1, eqString.Length - 2);
                            eqString = "\"" + eqString + "\"";
                        }
                        else if (dataType == "System.Nullable<Decimal>" || dataType == "Decimal")
                        {
                            eqString = eqString + "m";
                        }
                        else if (dataType == "System.Nullable<float>" || dataType == "float")
                        {
                            eqString = eqString + "f";
                        }
                        else if (dataType == "System.Nullable<Boolean>" || dataType == "Boolean")
                        {
                            if (eqString == "1")
                                eqString = "true";
                            else if (eqString == "0")
                                eqString = "false";
                        }
                        else if (!string.IsNullOrEmpty(column.EnumDefine) && column.dbType == "int")
                        {
                            eqString = "(" + dataType + ")(" + eqString + ")";
                        }
                        eqString = "=" + eqString;
                    }
                }

                fieldCodeItem.FieldType = dataType;
                proCodeItem.PropertyType = dataType;
                proCodeItem.Modification = "public virtual";
                proCodeItem.ItemForGet.AddString($"return _{column.Name};");
                proCodeItem.ItemForSet.AddItem(new CodeItem($"if ((_{column.Name} != value))")
                    .AddString($"var original = _{column.Name};")
                    .AddString($"_{column.Name} = value;")
                    .AddString($"OnPropertyChanged(\"{column.Name.Trim()}\",original,value);")
                    );

            }


            var classProperties = db.classproperty.Where(m => m.tableid == table.id).ToArray();
            foreach (var pro in classProperties)
            {
                try
                {
                    var foreign_table = db.DBTable.FirstOrDefault(m => m.id == pro.foreignkey_tableid);
                    if (pro.iscollection == false)
                    {

                        var column = db.DBColumn.FirstOrDefault(m => m.id == pro.foreignkey_columnid);
                        classCode.AddItem(new PropertyCodeItem(pro.name)
                        {
                            PropertyType = foreign_table.Name,
                            Modification = "public virtual",
                            Comment = pro.desc
                        });
                    }
                    else
                    {
                        //与其他表多对一
                        classCode.AddItem(new PropertyCodeItem(pro.name)
                        {
                            PropertyType = $"ICollection<{foreign_table.Name}>",
                            Modification = "public virtual",
                            Comment = pro.desc
                        });
                    }
                }
                catch
                {
                }

            }
        }


        public void BuildVerySimpleTable(EJDB db, NamespaceCode namespaceCode, EJ.DBTable table)
        {
            var columns = db.DBColumn.Where(m => m.TableID == table.id).OrderBy(m => m.orderid).ToList();
            BuildVerySimpleTable(db, namespaceCode, table, columns);
        }

        static void BuildVerySimpleTable(EJDB db, NamespaceCode namespaceCode, EJ.DBTable table, List<EJ.DBColumn> columns)
        {
            var pkcolumn = columns.FirstOrDefault(m => m.IsPKID == true);


            CodeItem classCode = new CodeItem($"public partial class {table.Name}");
            namespaceCode.AddItem(classCode);
            classCode.Comment = table.caption;


            foreach (var column in columns)
            {
                FieldCodeItem fieldCodeItem = new FieldCodeItem($"_{column.Name}");
                classCode.AddItem(fieldCodeItem);
                PropertyCodeItem proCodeItem = new PropertyCodeItem(column.Name);
                classCode.AddItem(proCodeItem);
                proCodeItem.Comment = column.caption;

                string dataType = column.ClassFullName;
                if (string.IsNullOrWhiteSpace(dataType))
                {
                    dataType = GetLinqTypeString(column.dbType, column.CanNull.GetValueOrDefault() || column.IsAutoIncrement == true);
                }
                string eqString = "";
                if (!string.IsNullOrEmpty(column.EnumDefine) && column.dbType == "int")
                {
                    if (column.EnumDefine.Trim().StartsWith("$"))
                    {
                        var target = column.EnumDefine.Trim().Substring(1).Split('.');
                        if (column.CanNull == true)
                            dataType = target[0] + "_" + target[1] + "Enum?";
                        else
                            dataType = target[0] + "_" + target[1] + "Enum";
                    }
                    else
                    {
                        string[] enumitems = column.EnumDefine.Replace("\r", "").Split('\n');

                        CodeItem codeEnum = new CodeItem($"public enum {table.Name}_{column.Name}Enum:int");
                        namespaceCode.AddItem(codeEnum);

                        CodeItem codeEnumField = new CodeItem();
                        for (int i = 0; i < enumitems.Length; i++)
                        {
                            var code = enumitems[i].Trim();
                            if (code.Length == 0)
                                continue;
                            if (code.StartsWith("//"))
                            {
                                if (code.Length > 2)
                                {
                                    if (codeEnumField.Comment.Length > 0)
                                        codeEnumField.Comment += "\r\n";
                                    codeEnumField.Comment += code.Substring(2);
                                }

                            }
                            else
                            {
                                codeEnumField.Body = code;
                                codeEnum.AddItem(codeEnumField);
                                codeEnumField = new CodeItem();
                            }

                        }
                        if (column.CanNull == true)
                            dataType = table.Name + "_" + column.Name + "Enum?";
                        else
                            dataType = table.Name + "_" + column.Name + "Enum";
                    }
                }


                if (!string.IsNullOrEmpty(column.defaultValue))
                {
                    if (column.defaultValue.Trim().Length > 0)
                    {
                        eqString = column.defaultValue.Trim();
                        if (dataType == "string" || dataType == "string?")
                        {
                            if (eqString.StartsWith("'") && eqString.EndsWith("'") && eqString.Length > 1)
                                eqString = eqString.Substring(1, eqString.Length - 2);
                            eqString = "\"" + eqString + "\"";
                        }
                        else if (dataType == "System.Nullable<Decimal>" || dataType == "Decimal")
                        {
                            eqString = eqString + "m";
                        }
                        else if (dataType == "System.Nullable<float>" || dataType == "float")
                        {
                            eqString = eqString + "f";
                        }
                        else if (dataType == "System.Nullable<Boolean>" || dataType == "Boolean")
                        {
                            if (eqString == "1")
                                eqString = "true";
                            else if (eqString == "0")
                                eqString = "false";
                        }
                        else if (!string.IsNullOrEmpty(column.EnumDefine) && column.dbType == "int")
                        {
                            eqString = "(" + dataType + ")(" + eqString + ")";
                        }
                        eqString = "=" + eqString;
                    }
                }

                fieldCodeItem.FieldType = dataType;
                proCodeItem.PropertyType = dataType;
                proCodeItem.Modification = "public virtual";
                proCodeItem.ItemForGet.AddString($"return _{column.Name};");
                proCodeItem.ItemForSet.AddItem(new CodeItem($"if ((_{column.Name} != value))")
                    .AddString($"_{column.Name} = value;")
                    );

            }


            var classProperties = db.classproperty.Where(m => m.tableid == table.id).ToArray();
            foreach (var pro in classProperties)
            {
                try
                {
                    var foreign_table = db.DBTable.FirstOrDefault(m => m.id == pro.foreignkey_tableid);
                    if (pro.iscollection == false)
                    {

                        var column = db.DBColumn.FirstOrDefault(m => m.id == pro.foreignkey_columnid);
                        classCode.AddItem(new PropertyCodeItem(pro.name)
                        {
                            PropertyType = foreign_table.Name,
                            Modification = "public virtual",
                            Comment = pro.desc
                        });
                    }
                    else
                    {
                        //与其他表多对一
                        classCode.AddItem(new PropertyCodeItem(pro.name)
                        {
                            PropertyType = $"ICollection<{foreign_table.Name}>",
                            Modification = "public virtual",
                            Comment = pro.desc
                        });
                    }
                }
                catch
                {
                }

            }
        }


        public string BuildDtoTable(EJDB db, EJ.DBTable table)
        {
            var columns = db.DBColumn.Where(m => m.TableID == table.id).OrderBy(m => m.orderid).ToList();
            NamespaceCode namespaceCode = new NamespaceCode(null);
            var oldname = table.Name;
            table.Name += "Dto";
            BuildDtoTable(db, namespaceCode, table, columns);
            table.Name = oldname;
            return namespaceCode.Build();
        }

        static void BuildDtoTable(EJDB db, NamespaceCode namespaceCode, EJ.DBTable table, List<EJ.DBColumn> columns)
        {
            var pkcolumn = columns.FirstOrDefault(m => m.IsPKID == true);


            CodeItem classCode = new CodeItem($"public class {table.Name}");
            namespaceCode.AddItem(classCode);
            classCode.Comment = table.caption;


            foreach (var column in columns)
            {

                PropertyCodeItem proCodeItem = new PropertyCodeItem(column.Name);
                classCode.AddItem(proCodeItem);
                proCodeItem.Comment = column.caption;

                string dataType = column.ClassFullName;
                if (string.IsNullOrWhiteSpace(dataType))
                {
                    dataType = GetLinqTypeString(column.dbType, column.CanNull.GetValueOrDefault() || column.IsAutoIncrement == true);
                }
                string eqString = "";
                if (!string.IsNullOrEmpty(column.EnumDefine) && column.dbType == "int")
                {
                    if (column.EnumDefine.Trim().StartsWith("$"))
                    {
                        var target = column.EnumDefine.Trim().Substring(1).Split('.');
                        var t_name = target[0];
                        var c_name = target[1];

                        try
                        {
                            var other_t = db.DBTable.FirstOrDefault(m => m.DatabaseID == table.DatabaseID && m.Name == t_name);
                            var other_c = db.DBColumn.FirstOrDefault(m => m.TableID == other_t.id && m.Name == c_name);
                            column.EnumDefine = other_c.EnumDefine;
                        }
                        catch (Exception)
                        {
                            column.EnumDefine = null;
                            if (column.CanNull == true)
                                dataType = "object?";
                            else
                                dataType = "object";
                        }
                    }

                    if (column.EnumDefine != null)
                    {
                        string[] enumitems = column.EnumDefine.Replace("\r", "").Split('\n');

                        CodeItem codeEnum = new CodeItem($"public enum {table.Name}_{column.Name}Enum:int");
                        namespaceCode.AddItem(codeEnum);

                        CodeItem codeEnumField = new CodeItem();
                        for (int i = 0; i < enumitems.Length; i++)
                        {
                            var code = enumitems[i].Trim();
                            if (code.Length == 0)
                                continue;
                            if (code.StartsWith("//"))
                            {
                                if (code.Length > 2)
                                {
                                    if (codeEnumField.Comment.Length > 0)
                                        codeEnumField.Comment += "\r\n";
                                    codeEnumField.Comment += code.Substring(2);
                                }

                            }
                            else
                            {
                                codeEnumField.Body = code;
                                codeEnum.AddItem(codeEnumField);
                                codeEnumField = new CodeItem();
                            }

                        }
                        if (column.CanNull == true)
                            dataType = table.Name + "_" + column.Name + "Enum?";
                        else
                            dataType = table.Name + "_" + column.Name + "Enum";
                    }
                }


                if (!string.IsNullOrEmpty(column.defaultValue))
                {
                    if (column.defaultValue.Trim().Length > 0)
                    {
                        eqString = column.defaultValue.Trim();
                        if (dataType == "string" || dataType == "string?")
                        {
                            if (eqString.StartsWith("'") && eqString.EndsWith("'") && eqString.Length > 1)
                                eqString = eqString.Substring(1, eqString.Length - 2);
                            eqString = "\"" + eqString + "\"";
                        }
                        else if (dataType == "System.Nullable<Decimal>" || dataType == "Decimal")
                        {
                            eqString = eqString + "m";
                        }
                        else if (dataType == "System.Nullable<float>" || dataType == "float")
                        {
                            eqString = eqString + "f";
                        }
                        else if (dataType == "System.Nullable<Boolean>" || dataType == "Boolean")
                        {
                            if (eqString == "1")
                                eqString = "true";
                            else if (eqString == "0")
                                eqString = "false";
                        }
                        else if (!string.IsNullOrEmpty(column.EnumDefine) && column.dbType == "int")
                        {
                            eqString = "(" + dataType + ")(" + eqString + ")";
                        }
                        eqString = "=" + eqString;
                    }
                }


                proCodeItem.PropertyType = dataType;
                proCodeItem.Modification = "public virtual";

            }


            var classProperties = db.classproperty.Where(m => m.tableid == table.id).ToArray();
            foreach (var pro in classProperties)
            {
                try
                {
                    var foreign_table = db.DBTable.FirstOrDefault(m => m.id == pro.foreignkey_tableid);
                    if (pro.iscollection == false)
                    {

                        var column = db.DBColumn.FirstOrDefault(m => m.id == pro.foreignkey_columnid);
                        classCode.AddItem(new PropertyCodeItem(pro.name)
                        {
                            PropertyType = foreign_table.Name,
                            Modification = "public virtual",
                            Comment = pro.desc
                        });
                    }
                    else
                    {
                        //与其他表多对一
                        classCode.AddItem(new PropertyCodeItem(pro.name)
                        {
                            PropertyType = $"ICollection<{foreign_table.Name}>",
                            Modification = "public virtual",
                            Comment = pro.desc
                        });
                    }
                }
                catch
                {
                }

            }
        }

    }

    public class DesignData
    {
        public EJ.Databases Database;
        public EJ.DBModule[] Modules;
        public EJ.DBTable[] Tables;
        public EJ.TableInModule[] TableInModules;
        public EJ.IDXIndex[] IDXIndexes;
        public EJ.DBDeleteConfig[] DBDeleteConfigs;
        public EJ.DBColumn[] DBColumns;
        public EJ.classproperty[] classproperties;
    }
}