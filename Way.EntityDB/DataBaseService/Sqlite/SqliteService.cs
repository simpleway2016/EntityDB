using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;
using Way.EntityDB.Exceptons;
using Way.EntityDB.DataBaseService;

namespace Way.EntityDB
{
    [Attributes.DatabaseTypeAttribute( DatabaseType.Sqlite)]
    class SqliteService:IDatabaseService
    {
        protected DatabaseFacade _database;
        DBContext _dbcontext;

        protected virtual bool SupportEnum => true;

        public virtual void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(this.ConnectionString);
        }

        public System.Data.Common.DbConnection Connection
        {
            get
            {
                return ((Microsoft.EntityFrameworkCore.DbContext)_dbcontext).Database.GetDbConnection();
            }
        }
         public SqliteService()
        {
        }
         public SqliteService(DBContext dbcontext)
         {
             _dbcontext = dbcontext;
             _database = ((Microsoft.EntityFrameworkCore.DbContext)dbcontext).Database;
        }
        //public virtual System.Data.Common.DbConnection CreateConnection(string connectString)
        //{
        //    return new SqliteConnection(connectString);
        //}
        public virtual void AllowIdentityInsert(string tablename, bool allow)
        {

        }

        public virtual void UpdateLock<T>(object pkValue) where T : DataItem
        {
            this.UpdateLock(typeof(T), pkValue);
        }
        public virtual void UpdateLock(Type tableType, object pkValue)
        {
            var tableSchema = SchemaManager.GetSchemaTable(tableType);

            var pkColumn = tableSchema.Columns.FirstOrDefault(m => m.IsKey);
            var columnname = FormatObjectName(pkColumn.Name.ToLower());
            this.ExecSqlString($"update {FormatObjectName(tableSchema.TableName.ToLower())} set {columnname}={columnname} where {columnname}=@p0", pkValue);
        }

        public virtual string FormatObjectName(string name)
        {
            if (name.StartsWith("[") || name.StartsWith("("))
                return name;
            return string.Format("[{0}]", name);
        }
        /// <summary>
        /// GetInsertIDValueSqlString是否放在一个sql语句
        /// </summary>
        /// <returns></returns>
        protected virtual bool GetInsertIDValueSqlStringInOneSql()
        {
            return false;
        }


        /// <summary>
        /// 返回获取自增长字段值的sql语句
        /// </summary>
        /// <param name="pkColumnName"></param>
        /// <returns></returns>
        protected virtual string GetInsertIDValueSqlString(string pkColumnName)
        {
            return "select last_insert_rowid()";
        }
        public virtual System.Data.Common.DbCommand CreateCommand(string sql , params object[] parames)
        {
            var cmd = this.Connection.CreateCommand();
            if (_database.CurrentTransaction != null && cmd.Transaction == null)
            {
                cmd.Transaction = (System.Data.Common.DbTransaction)_database.CurrentTransaction.GetDbTransaction();
            }
            if (sql != null)
            {
                cmd.CommandText = sql;
            }
            if (parames != null && parames.Length > 0)
            {
                for (int i = 0; i < parames.Length; i++)
                {
                    var sqlParameter = cmd.CreateParameter();
                    sqlParameter.ParameterName = "@p" + i;
                    sqlParameter.Value = parames[i];
                    cmd.Parameters.Add(sqlParameter);
                }
            }
            return cmd;
        }
#if NET46
        protected virtual System.Data.Common.DbDataAdapter CreateDataAdapter(string sql)
        {
            return new mySQLiteDataAdapter((SqliteCommand)this.CreateCommand(sql));
        }
#endif
     
        protected virtual void ThrowSqlException(Type tableType, Exception ex)
        {
            if (!(ex is SqliteException))
                throw ex;
            if (((SqliteException)ex).SqliteErrorCode != 19)
                throw ex;
            throw new RepeatException(ex);
        }

        public virtual void Insert(DataItem dataitem, bool insertAllFields)
        {
            bool needToClose = false;
            if (this.Connection.State != System.Data.ConnectionState.Open)
            {
                needToClose = true;
                this.Connection.Open();
            }

            var tableSchema = SchemaManager.GetSchemaTable(dataitem.GetType());
            string pkid = null;
            if (tableSchema != null)
            {
                pkid = tableSchema.KeyColumn.PropertyName;
            }
           
            var fieldValues = dataitem.GetFieldValues(true,insertAllFields);
            if (fieldValues.Count == 0)
                return;

            if (tableSchema != null && tableSchema.AutoSetPropertyNameOnInsert != null)
            {
                if (dataitem.ChangedProperties.Any(m=>m.Key == tableSchema.AutoSetPropertyNameOnInsert) == false)
                {
                    var val = tableSchema.AutoSetPropertyValueOnInsert;
                    if( !SupportEnum && val != null && val.GetType().IsEnum)
                    {
                        val = Convert.ToInt32(val);//PostgreSql不支持枚举
                    }

                    var fieldname = tableSchema.AutoSetPropertyNameOnInsert.ToLower();
                    var fv = fieldValues.FirstOrDefault(m => m.FieldName == fieldname);
                    if (fv != null)
                        fv.Value = val;
                    else
                    {
                        fieldValues.Add(new FieldValue()
                        {
                            FieldName = tableSchema.AutoSetPropertyNameOnInsert.ToLower(),
                            Value = val
                        });
                    }
                    
                }
            }

            StringBuilder str_fields = new StringBuilder();
            StringBuilder str_values = new StringBuilder();
           
            try
            {
              
                using (var command = CreateCommand(null))
                {
                    int parameterIndex = 1;
                    foreach (var field in fieldValues)
                    {

                        if (str_fields.Length > 0)
                            str_fields.Append(',');
                        str_fields.Append(FormatObjectName(field.FieldName));


                        var parameter = command.CreateParameter();
                        parameter.ParameterName = "@p" + (parameterIndex++);
                        parameter.Value = field.Value;
                        if (!SupportEnum && field.Value != null && field.Value.GetType().IsEnum)
                        {
                            parameter.Value = Convert.ToInt32(field.Value);
                        }

                        command.Parameters.Add(parameter);

                        if (str_values.Length > 0)
                            str_values.Append(',');
                        str_values.Append(parameter.ParameterName);
                    }

                    string sql;
                    if( GetInsertIDValueSqlStringInOneSql() )
                    {
                        sql = string.Format("insert into {0} ({1}) values ({2}) {3}", FormatObjectName(dataitem.TableName), str_fields, str_values , this.GetInsertIDValueSqlString(pkid));
                        command.CommandText = sql;
                        object id = command.ExecuteScalar();

                        command.Parameters.Clear();
                        if (id != null && id != DBNull.Value && !string.IsNullOrEmpty(pkid))
                        {
                            dataitem.SetValue(pkid, id);
                        }
                    }
                    else
                    {
                        sql = string.Format("insert into {0} ({1}) values ({2})", FormatObjectName(dataitem.TableName), str_fields, str_values);
                        command.CommandText = sql;
                        command.ExecuteNonQuery();

                        command.Parameters.Clear();
                        sql = this.GetInsertIDValueSqlString(pkid);
                        if (sql != null)
                        {
                            command.CommandText = sql;
                            object id = command.ExecuteScalar();

                            if (id != null && id != DBNull.Value && !string.IsNullOrEmpty(pkid))
                            {
                                dataitem.SetValue(pkid, id);
                            }
                        }
                    }
                   
                }
                dataitem.ChangedProperties.Clear();
            }
            catch (Exception ex)
            {
                this.ThrowSqlException(dataitem.TableType, ex);
            }
            finally
            {
                if (needToClose)
                {
                    this.Connection.Close();
                }
            }

        }

        string getMember(Expression exp)
        {
            while(exp is UnaryExpression)
            {
                UnaryExpression uexp = (UnaryExpression)exp;
                if (uexp.NodeType == ExpressionType.Convert)
                {
                    exp = uexp.Operand;
                }
                else
                    break;
            }
            var left = exp as System.Linq.Expressions.MemberExpression;
            if (left == null)
                throw new ParseUpdateExpressionException("左侧必须是成员表达式");
            return this.FormatObjectName( left.Member.Name.ToLower());
        }
        string getCall(MethodCallExpression exp, System.Data.Common.DbCommand cmd)
        {
            object name = null;
            object value = null;
            System.Data.Common.DbParameter dbparam;
            switch (exp.Method.Name)
            {
                case "Contains":
                    name = getMember(exp.Object);
                    value = getExpressionValue(exp.Arguments[0]);
                    if (value == null)
                        throw new ParseUpdateExpressionException("Contains不能为空值");
                    value = $"%{value}%";

                    break;
                case "StartsWith":
                    name = getMember(exp.Object);
                    value = getExpressionValue(exp.Arguments[0]);
                    if (value == null)
                        throw new ParseUpdateExpressionException("StartsWith不能为空值");
                    value = $"%{value}";

                    break;
                case "EndsWith":
                    name = getMember(exp.Object);
                    value = getExpressionValue(exp.Arguments[0]);
                    if (value == null)
                        throw new ParseUpdateExpressionException("EndsWith不能为空值");
                    value = $"{value}%";
                    break;
            }
            
            if(value != null)
            {
                dbparam = cmd.CreateParameter();
                dbparam.ParameterName = "@w_" + cmd.Parameters.Count;
                cmd.Parameters.Add(dbparam);
                dbparam.Value = value;

                return $"{name} like {dbparam.ParameterName}";
            }

            return "";
        }

        object getExpressionValue(Expression exp)
        {
            if (exp is ConstantExpression constantExpression)
            {
                return constantExpression.Value;
            }
            if (exp is UnaryExpression unaryExpression)
            {
                if(unaryExpression.Operand is ConstantExpression ce)
                    return ce.Value;
            }
            return Expression.Lambda(exp).Compile().DynamicInvoke();
        }
        public virtual string BuildWhereString(Expression exp, System.Data.Common.DbCommand cmd )
        {
            if (exp is MethodCallExpression)
            {
                return getCall(exp as MethodCallExpression, cmd);
            }
            BinaryExpression body = exp as BinaryExpression;
            if (body == null)
            {
                throw new ParseUpdateExpressionException("解析表达式失败");
            }


            if (body.Left == null)
                throw new ParseUpdateExpressionException("解析表达式失败");

            object value;
            object name;
            System.Data.Common.DbParameter dbparam;
            switch (body.NodeType)
            {
                case ExpressionType.Equal:
                    name = getMember(body.Left);
                    dbparam = cmd.CreateParameter();
                    dbparam.ParameterName = "@w_" + cmd.Parameters.Count;
                    dbparam.Value = getExpressionValue(body.Right);
                    if (dbparam.Value != null)
                    {
                        cmd.Parameters.Add(dbparam);
                        return $"{name}={dbparam.ParameterName}";
                    }
                    else
                    {
                        return $"{name} is null";
                    }
                case ExpressionType.NotEqual:
                    name = getMember(body.Left);
                    dbparam = cmd.CreateParameter();
                    dbparam.ParameterName = "@w_" + cmd.Parameters.Count;
                    dbparam.Value = getExpressionValue(body.Right);
                    if (dbparam.Value != null)
                    {
                        cmd.Parameters.Add(dbparam);
                        return $"{name}<>{dbparam.ParameterName}";
                    }
                    else
                    {
                        return $"{name} is not null";
                    }
                case ExpressionType.GreaterThan:
                    name = getMember(body.Left);
                    dbparam = cmd.CreateParameter();
                    dbparam.ParameterName = "@w_" + cmd.Parameters.Count;
                    cmd.Parameters.Add(dbparam);
                    dbparam.Value = getExpressionValue(body.Right);
                    return $"{name}>{dbparam.ParameterName}";
                case ExpressionType.GreaterThanOrEqual:
                    name = getMember(body.Left);
                    dbparam = cmd.CreateParameter();
                    dbparam.ParameterName = "@w_" + cmd.Parameters.Count;
                    cmd.Parameters.Add(dbparam);
                    dbparam.Value = getExpressionValue(body.Right);
                    return $"{name}>={dbparam.ParameterName}";
                case ExpressionType.LessThan:
                    name = getMember(body.Left);
                    dbparam = cmd.CreateParameter();
                    dbparam.ParameterName = "@w_" + cmd.Parameters.Count;
                    cmd.Parameters.Add(dbparam);
                    dbparam.Value = getExpressionValue(body.Right);
                    return $"{name}<{dbparam.ParameterName}";
                case ExpressionType.LessThanOrEqual:
                    name = getMember(body.Left);
                    dbparam = cmd.CreateParameter();
                    dbparam.ParameterName = "@w_" + cmd.Parameters.Count;
                    cmd.Parameters.Add(dbparam);
                    dbparam.Value = getExpressionValue(body.Right);
                    return $"{name}<={dbparam.ParameterName}";
                case ExpressionType.AndAlso:
                    return $"({BuildWhereString(body.Left,cmd)} and {BuildWhereString(body.Right, cmd)})";
                case ExpressionType.OrElse:
                    return $"({BuildWhereString(body.Left, cmd)} or {BuildWhereString(body.Right, cmd)})";
                default:
                    throw new ParseUpdateExpressionException("解析表达式失败");
            }

        }

        void CreateUpdateBody( Expression expression,bool isRight, StringBuilder sb,List<string> findnames, System.Data.Common.DbCommand cmd)
        {
            if (expression is BinaryExpression binaryExpression)
            {
                CreateUpdateBody(binaryExpression.Left,false, sb, findnames, cmd);

                switch (binaryExpression.NodeType)
                {
                    case ExpressionType.Add:
                        sb.Append("+");
                        break;
                    case ExpressionType.Divide:
                        sb.Append("/");
                        break;
                    case ExpressionType.Multiply:
                        sb.Append("*");
                        break;
                    case ExpressionType.Subtract:
                        sb.Append("-");
                        break;
                    case ExpressionType.Equal:
                        sb.Append("=");
                        break;
                    case ExpressionType.And:
                        sb.Append(",");
                        break;
                    case ExpressionType.AndAlso:
                        sb.Append(",");
                        break;
                    case ExpressionType.Or:
                        sb.Append(",");
                        break;
                    case ExpressionType.OrElse:
                        sb.Append(",");
                        break;
                    default:
                        throw new ParseUpdateExpressionException("表达式解析错误");
                }

                CreateUpdateBody(binaryExpression.Right,true, sb,findnames ,cmd);
            }
            else if ( !isRight && expression is MemberExpression memberExpression)
            {
                var membername = memberExpression.Member.Name.ToLower();
                if (findnames.Contains(membername) == false)
                    findnames.Add(membername);
                sb.Append($"{ this.FormatObjectName(membername)}");
            }
            else
            {
                var parmName = $"u_{cmd.Parameters.Count}";
                var sqlparam = cmd.CreateParameter();
                sqlparam.ParameterName = parmName;
                sqlparam.Value = getExpressionValue(expression);
                cmd.Parameters.Add(sqlparam);

                sb.Append($" @{parmName}");
            }
        }


        public virtual int Update<T>(T dataitem, Expression<Func<T, bool>> condition) where T : DataItem
        {


            string pkid = dataitem.KeyName;
            object pkvalue = dataitem.PKValue;
            if (pkvalue == null && condition == null && pkid != null )
            {
                Insert(dataitem,false);
                return 1;
            }

            if (dataitem.ChangedProperties.Count == 0 && dataitem.UpdateExpression == null)
                return 0;

            var changedHistory = dataitem.ChangedProperties[dataitem.KeyName];
            if (changedHistory != null && changedHistory.OriginalValue != null)
                pkvalue = changedHistory.OriginalValue;

            bool needToClose = false;
            if (this.Connection.State != System.Data.ConnectionState.Open)
            {
                needToClose = true;
                this.Connection.Open();
            } 

            try
            {


                StringBuilder str_fields = new StringBuilder();
                int parameterIndex = 1;

                var updateExpression = dataitem.UpdateExpression as Expression<Func<T, bool>>;

                using (var command = CreateCommand(null))
                {

                    var fieldValues = dataitem.GetFieldValues(false,false);
                    if (fieldValues.Count == 0 && updateExpression == null)
                        return 0;

                    List<string> cancelNames = null;
                    if(updateExpression != null)
                    {
                        cancelNames = new List<string>();
                        CreateUpdateBody(updateExpression.Body,false, str_fields, cancelNames, command);
                    }

                    foreach (var fieldValue in fieldValues)
                    {
                        if (cancelNames != null && cancelNames.Contains(fieldValue.FieldName))
                            continue;

                        if (str_fields.Length > 0)
                            str_fields.Append(',');
                        str_fields.Append(FormatObjectName(fieldValue.FieldName));
                        str_fields.Append('=');

                       

                        object value = fieldValue.Value;
                        if (!SupportEnum && value != null && value.GetType().IsEnum)
                        {
                            value = Convert.ToInt32(value);
                        }

                        if (value == DBNull.Value || value == null)
                        {
                            str_fields.Append("null");

                        }
                        else
                        {
                            string parameterName = "@p" + (parameterIndex++);
                            var parameter = command.CreateParameter();
                            parameter.ParameterName = parameterName;
                            parameter.Value = value;
                            command.Parameters.Add(parameter);

                            str_fields.Append(parameterName);

                           

                        }
                    }

                    if (pkvalue != null)
                    {
                        var parameter = command.CreateParameter();
                        parameter.ParameterName = "@pid";
                        parameter.Value = pkvalue;
                        command.Parameters.Add(parameter);

                        if (condition != null)
                        {
                            var where = BuildWhereString(condition.Body, command);
                            command.CommandText = string.Format("update {0} set {1} where " + where, FormatObjectName(dataitem.TableName), str_fields);
                        }
                        else
                        {
                            command.CommandText = string.Format("update {0} set {1} where {2}=@pid", FormatObjectName(dataitem.TableName), str_fields, FormatObjectName(pkid.ToLower()));
                        }
                    }
                    else
                    {
                        if (condition != null)
                        {
                            var where = BuildWhereString(condition.Body, command);
                            command.CommandText = string.Format("update {0} set {1} where " + where, FormatObjectName(dataitem.TableName), str_fields);
                        }
                        else
                        {
                            command.CommandText = string.Format("update {0} set {1}", FormatObjectName(dataitem.TableName), str_fields);
                        }
                    }
                    var ret = command.ExecuteNonQuery();
                   

                    command.Parameters.Clear();

                    return Convert.ToInt32(ret);
                }
            }
            catch (Exception ex)
            {
                ThrowSqlException(dataitem.TableType, ex);
            }
            finally
            {
                if (needToClose)
                {
                    this.Connection.Close();
                }
            }
            return 0;

        }
        public virtual void Delete(DataItem dataitem)
        {
            bool needToClose = false;
            if (this.Connection.State != System.Data.ConnectionState.Open)
            {
                needToClose = true;
                this.Connection.Open();
            }

            try
            {
                using (var command = CreateCommand(null))
                {
                    command.CommandText = string.Format("delete from {0} where {1}=@p0", FormatObjectName(dataitem.TableName), FormatObjectName(dataitem.KeyName.ToLower()));
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@p0";
                    parameter.Value = dataitem.PKValue;
                    command.Parameters.Add(parameter);
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (needToClose)
                {
                    this.Connection.Close();
                }
            }

        }


        public object ExecSqlString(string sql,params object[] parames)
        {
            bool needToClose = false;
            if (this.Connection.State != System.Data.ConnectionState.Open)
            {
                needToClose = true;
                this.Connection.Open();
            } 
            try
            {
                var command = CreateCommand(sql, parames);
                {
                    return command.ExecuteScalar();
                }
            }
            catch(Exception ex)
            {
                throw new Exceptons.SqlExecException(ex.Message ,sql, ex);
            }
            finally
            {
                if (needToClose)
                {
                    this.Connection.Close();
                }
            }
        }


        public WayDataTable SelectTable(string sql, params object[] parames)
        {
            bool needToClose = false;
            if (this.Connection.State != System.Data.ConnectionState.Open)
            {
                needToClose = true;
                this.Connection.Open();
            }
            try
            {
                using (var command = this.CreateCommand(sql,parames))
                {
                    var dtable = new WayDataTable();
                    using (var reader = command.ExecuteReader())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string name = reader.GetName(i);
                            dtable.Columns.Add(new WayDataColumn(name, reader.GetFieldType(i).FullName));
                        }

                        while (reader.Read())
                        {
                            var row = new WayDataRow();
                            dtable.Rows.Add(row);
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string name = reader.GetName(i);
                                row[name] = reader[i];
                                if(reader[i] != null)
                                {
                                    string typename = reader.GetFieldType(i).FullName;
                                    if (typename != dtable.Columns[i].DataType)
                                        dtable.Columns[i].DataType = typename;
                                }
                            }
                        }
                    }
                        return dtable;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (needToClose)
                {
                    this.Connection.Close();
                }
            }
        }
        public WayDataSet SelectDataSet(string sql, params object[] parames)
        {
            bool needToClose = false;
            if (this.Connection.State != System.Data.ConnectionState.Open)
            {
                needToClose = true;
                this.Connection.Open();
            }
            try
            {
                using (var command = this.CreateCommand(sql, parames))
                {
                    var dataset = new WayDataSet();
                    using (var datareader = command.ExecuteReader())
                    {
                        do
                        {
                            if (datareader.HasRows)
                            {
                                var datatable = new WayDataTable();
                                for (int i = 0; i < datareader.FieldCount; i++)
                                {
                                    string name = datareader.GetName(i);
                                    datatable.Columns.Add(new WayDataColumn(name,datareader.GetFieldType(i).FullName));
                                }
                                dataset.Tables.Add(datatable);
                                while (datareader.Read())
                                {
                                    var row = new WayDataRow();
                                    datatable.Rows.Add(row);

                                    for (int i = 0; i < datareader.FieldCount; i++)
                                    {
                                        string name = datareader.GetName(i);
                                        row[name] = datareader[i];
                                    }
                                }
                            }
                        }
                        while (datareader.NextResult());
                    }
                    return dataset;
                }
              
            }
            catch
            {
                throw;
            }
            finally
            {
                if (needToClose)
                {
                    this.Connection.Close();
                }
            }
        }

        public virtual WayDataTable SelectTable(string sql, int skip, int take, params object[] sqlparameters)
        {
            sql = string.Format("select * from ({0}) as t1 limit {1},{2}", sql, skip, take);
            return SelectTable(sql, sqlparameters);
        }

        public void ExecuteReader(Func<System.Data.IDataReader,bool> func , string sql, params object[] parames)
        {
            bool needToClose = false;
            if (this.Connection.State != System.Data.ConnectionState.Open)
            {
                needToClose = true;
                this.Connection.Open();
            }
            try
            {
                using (var command = this.CreateCommand(sql, parames))
                {
                    var dataset = new WayDataSet();
                    using (var datareader = command.ExecuteReader())
                    {

                        while (datareader.Read())
                        {
                            if (func(datareader) == false)
                                break;
                        }
                        
                    }
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                if (needToClose)
                {
                    this.Connection.Close();
                }
            }
        }

        public virtual string ConvertConnectionString(string conStr)
        {
            return conStr;
        }

        public DBContext DBContext
        {
            get { return _dbcontext; }
        }

        public string ConnectionString
        {
            get
            {
                return _dbcontext.ConnectionString;
            }
        }

      
    }

#if NET46
    class mySQLiteDataAdapter : System.Data.Common.DbDataAdapter
    {
        public mySQLiteDataAdapter(SqliteCommand command)
        {
            this.SelectCommand = command;
        }

    }
#endif
}
