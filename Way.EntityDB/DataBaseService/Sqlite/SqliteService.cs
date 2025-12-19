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
using Way.EntityDB.ExpressionParsers;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;
using System.Data.Common;

namespace Way.EntityDB
{
    [Attributes.DatabaseTypeAttribute(DatabaseType.Sqlite)]
    class SqliteService : IDatabaseService
    {
        protected DatabaseFacade _database;
        DBContext _dbcontext;
        ExpressionParserRoute _expressionParserRoute;
        protected virtual bool SupportEnum => true;
        internal static System.Text.Json.JsonSerializerOptions DefaultJsonSerializerOptions = new System.Text.Json.JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
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
            _expressionParserRoute = new ExpressionParserRoute(name => this.FormatObjectName(name));
        }
        public SqliteService(DBContext dbcontext)
        {
            _dbcontext = dbcontext;
            _database = ((Microsoft.EntityFrameworkCore.DbContext)dbcontext).Database;
            _expressionParserRoute = new ExpressionParserRoute(name => this.FormatObjectName(name));
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
            this.ExecSqlString($"update {FormatTableName(tableSchema.TableName.ToLower())} set {columnname}={columnname} where {columnname}=@p0", pkValue);
        }

        public virtual Task UpdateLockAsync<T>(object pkValue) where T : DataItem
        {
            return this.UpdateLockAsync(typeof(T), pkValue);
        }
        public virtual Task UpdateLockAsync(Type tableType, object pkValue)
        {
            var tableSchema = SchemaManager.GetSchemaTable(tableType);

            var pkColumn = tableSchema.Columns.FirstOrDefault(m => m.IsKey);
            var columnname = FormatObjectName(pkColumn.Name.ToLower());
            return this.ExecSqlStringAsync($"update {FormatTableName(tableSchema.TableName.ToLower())} set {columnname}={columnname} where {columnname}=@p0", pkValue);
        }

        public virtual string FormatObjectName(string name)
        {
            if (name.StartsWith("[") || name.StartsWith("("))
                return name;
            return string.Format("[{0}]", name);
        }

        public virtual string FormatTableName(string name)
        {
            if (!string.IsNullOrWhiteSpace(DBContext.Schema))
            {
                if (name.StartsWith("[") || name.StartsWith("("))
                    return $"[{DBContext.Schema}].{name}";
                else
                    return $"[{DBContext.Schema}].[{name}]";
            }
            else
            {
                if (name.StartsWith("[") || name.StartsWith("("))
                    return name;
                return string.Format("[{0}]", name);
            }
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
        public virtual System.Data.Common.DbCommand CreateCommand(string sql, params object[] parames)
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

            var fieldValues = dataitem.GetFieldValues(true, insertAllFields);
            if (fieldValues.Count == 0)
                return;

            if (tableSchema != null && tableSchema.AutoSetPropertyNameOnInsert != null)
            {
                if (dataitem.ChangedProperties.Any(m => m.Key == tableSchema.AutoSetPropertyNameOnInsert) == false)
                {
                    var val = tableSchema.AutoSetPropertyValueOnInsert;
                    if (!SupportEnum && val != null && val.GetType().IsEnum)
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

                        SchemaColumn schemaColumn = tableSchema?.Columns.FirstOrDefault(m => m.Name == field.FieldName);
                        if (!SupportEnum && field.Value != null && schemaColumn != null && schemaColumn.PropertyInfo.PropertyType.IsEnum)
                        {
                            parameter.Value = Convert.ToInt32(field.Value);
                        }
                        else if (!SupportEnum && field.Value != null && field.Value.GetType().IsEnum)
                        {
                            parameter.Value = Convert.ToInt32(field.Value);
                        }

                        ConvertDesignTypeToDataTypeName(parameter, field.Value, schemaColumn?.TypeName);


                        command.Parameters.Add(parameter);

                        if (str_values.Length > 0)
                            str_values.Append(',');
                        str_values.Append(parameter.ParameterName);
                    }

                    string sql;
                    if (GetInsertIDValueSqlStringInOneSql() && pkid != null && tableSchema.KeyColumn.IsDatabaseGenerated)
                    {
                        sql = string.Format("insert into {0} ({1}) values ({2}) {3}", FormatTableName(dataitem.TableName), str_fields, str_values, this.GetInsertIDValueSqlString(pkid));
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
                        sql = string.Format("insert into {0} ({1}) values ({2})", FormatTableName(dataitem.TableName), str_fields, str_values);
                        command.CommandText = sql;
                        command.ExecuteNonQuery();

                        command.Parameters.Clear();
                        if (tableSchema.KeyColumn.IsDatabaseGenerated)
                        {
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

        public virtual async Task InsertAsync(DataItem dataitem, bool insertAllFields)
        {
            bool needToClose = false;
            if (this.Connection.State != System.Data.ConnectionState.Open)
            {
                needToClose = true;
                await this.Connection.OpenAsync();
            }

            var tableSchema = SchemaManager.GetSchemaTable(dataitem.GetType());
            string pkid = null;
            if (tableSchema != null)
            {
                pkid = tableSchema.KeyColumn.PropertyName;
            }

            var fieldValues = dataitem.GetFieldValues(true, insertAllFields);
            if (fieldValues.Count == 0)
                return;

            if (tableSchema != null && tableSchema.AutoSetPropertyNameOnInsert != null)
            {
                if (dataitem.ChangedProperties.Any(m => m.Key == tableSchema.AutoSetPropertyNameOnInsert) == false)
                {
                    var val = tableSchema.AutoSetPropertyValueOnInsert;
                    if (!SupportEnum && val != null && val.GetType().IsEnum)
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

                        SchemaColumn schemaColumn = tableSchema?.Columns.FirstOrDefault(m => m.Name == field.FieldName);
                        if (!SupportEnum && field.Value != null && schemaColumn != null && schemaColumn.PropertyInfo.PropertyType.IsEnum)
                        {
                            parameter.Value = Convert.ToInt32(field.Value);
                        }
                        else if (!SupportEnum && field.Value != null && field.Value.GetType().IsEnum)
                        {
                            parameter.Value = Convert.ToInt32(field.Value);
                        }

                        ConvertDesignTypeToDataTypeName(parameter, field.Value, schemaColumn?.TypeName);

                        command.Parameters.Add(parameter);

                        if (str_values.Length > 0)
                            str_values.Append(',');
                        str_values.Append(parameter.ParameterName);
                    }

                    string sql;
                    if (GetInsertIDValueSqlStringInOneSql() && pkid != null && tableSchema.KeyColumn.IsDatabaseGenerated)
                    {
                        sql = string.Format("insert into {0} ({1}) values ({2}) {3}", FormatTableName(dataitem.TableName), str_fields, str_values, this.GetInsertIDValueSqlString(pkid));
                        command.CommandText = sql;
                        object id = await command.ExecuteScalarAsync();

                        command.Parameters.Clear();
                        if (id != null && id != DBNull.Value && !string.IsNullOrEmpty(pkid))
                        {
                            dataitem.SetValue(pkid, id);
                        }
                    }
                    else
                    {
                        sql = string.Format("insert into {0} ({1}) values ({2})", FormatTableName(dataitem.TableName), str_fields, str_values);
                        command.CommandText = sql;
                        await command.ExecuteNonQueryAsync();

                        command.Parameters.Clear();
                        if (tableSchema.KeyColumn.IsDatabaseGenerated)
                        {
                            sql = this.GetInsertIDValueSqlString(pkid);
                            if (sql != null)
                            {
                                command.CommandText = sql;
                                object id = await command.ExecuteScalarAsync();

                                if (id != null && id != DBNull.Value && !string.IsNullOrEmpty(pkid))
                                {
                                    dataitem.SetValue(pkid, id);
                                }
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
                    await this.Connection.CloseAsync();
                }
            }

        }

        public virtual int Update<T>(T dataitem, Expression<Func<T, bool>> condition) where T : DataItem
        {


            string pkid = dataitem.KeyName;
            object pkvalue = dataitem.PKValue;
            if (pkvalue == null && condition == null && pkid != null)
            {
                Insert(dataitem, false);
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
                List<string> addedMembers = null;
                using (var command = CreateCommand(null))
                {

                    var fieldValues = dataitem.GetFieldValues(false, false);
                    if (fieldValues.Count == 0 && updateExpression == null)
                        return 0;


                    if (updateExpression != null)
                    {
                        addedMembers = new List<string>();
                        var parser = _expressionParserRoute.GetExpressionParser(updateExpression.Body);
                        if (parser == null)
                            throw new ParseUpdateExpressionException($"无法解析{updateExpression.Body}");
                        var setString = parser.Parse(updateExpression.Body, command, addedMembers, true);
                        if (setString != null)
                        {
                            setString = setString.Replace(" and ", ",").Replace(" or ", ",");
                            str_fields.Append(setString);
                        }
                    }

                    var tableSchema = SchemaManager.GetSchemaTable(dataitem.GetType());
                    foreach (var fieldValue in fieldValues)
                    {
                        if (addedMembers != null && addedMembers.Contains(fieldValue.FieldName))
                            continue;

                        if (str_fields.Length > 0)
                            str_fields.Append(',');
                        str_fields.Append(FormatObjectName(fieldValue.FieldName));
                        str_fields.Append('=');



                        object value = fieldValue.Value;
                        var parameter = command.CreateParameter();

                        SchemaColumn schemaColumn = tableSchema?.Columns.FirstOrDefault(m => m.Name == fieldValue.FieldName);
                        if (!SupportEnum && value != null && schemaColumn != null && schemaColumn.PropertyInfo.PropertyType.IsEnum)
                        {
                            value = Convert.ToInt32(value);
                        }
                        else if (!SupportEnum && value != null && value.GetType().IsEnum)
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
                            parameter.ParameterName = parameterName;
                            parameter.Value = value;

                            ConvertDesignTypeToDataTypeName(parameter, value,schemaColumn?.TypeName);

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
                            var parser = _expressionParserRoute.GetExpressionParser(condition.Body);
                            if (parser == null)
                                throw new ParseUpdateExpressionException($"无法解析{condition.Body}");
                            var where = parser.Parse(condition.Body, command, null, false);

                            command.CommandText = string.Format("update {0} set {1} where " + where, FormatTableName(dataitem.TableName), str_fields);
                        }
                        else
                        {
                            command.CommandText = string.Format("update {0} set {1} where {2}=@pid", FormatTableName(dataitem.TableName), str_fields, FormatObjectName(pkid.ToLower()));
                        }
                    }
                    else
                    {
                        if (condition != null)
                        {
                            var parser = _expressionParserRoute.GetExpressionParser(condition.Body);
                            if (parser == null)
                                throw new ParseUpdateExpressionException($"无法解析{condition.Body}");
                            var where = parser.Parse(condition.Body, command, null, false);

                            command.CommandText = string.Format("update {0} set {1} where " + where, FormatTableName(dataitem.TableName), str_fields);
                        }
                        else
                        {
                            command.CommandText = string.Format("update {0} set {1}", FormatTableName(dataitem.TableName), str_fields);
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

        public virtual int Delete<T>(Expression<Func<T, bool>> condition) where T : DataItem
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

                    var tableSchema = SchemaManager.GetSchemaTable(typeof(T));

                    if (condition == null)
                    {
                        command.CommandText = string.Format("delete from {0}", FormatTableName(tableSchema.TableName));
                    }
                    else
                    {
                        var parser = _expressionParserRoute.GetExpressionParser(condition.Body);
                        if (parser == null)
                            throw new ParseUpdateExpressionException($"无法解析{condition.Body}");
                        var where = parser.Parse(condition.Body, command, null, false);

                        command.CommandText = string.Format("delete from {0} where {1}", FormatTableName(tableSchema.TableName), where);
                    }
                    var ret = command.ExecuteNonQuery();


                    command.Parameters.Clear();

                    return Convert.ToInt32(ret);
                }
            }
            catch (Exception ex)
            {
                ThrowSqlException(typeof(T), ex);
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

        public virtual async Task<int> DeleteAsync<T>(Expression<Func<T, bool>> condition) where T : DataItem
        {
            bool needToClose = false;
            if (this.Connection.State != System.Data.ConnectionState.Open)
            {
                needToClose = true;
                await this.Connection.OpenAsync();
            }

            try
            {
                using (var command = CreateCommand(null))
                {

                    var tableSchema = SchemaManager.GetSchemaTable(typeof(T));

                    if (condition == null)
                    {
                        command.CommandText = string.Format("delete from {0}", FormatTableName(tableSchema.TableName));
                    }
                    else
                    {
                        var parser = _expressionParserRoute.GetExpressionParser(condition.Body);
                        if (parser == null)
                            throw new ParseUpdateExpressionException($"无法解析{condition.Body}");
                        var where = parser.Parse(condition.Body, command, null, false);

                        command.CommandText = string.Format("delete from {0} where {1}", FormatTableName(tableSchema.TableName), where);
                    }
                    var ret = await command.ExecuteNonQueryAsync();


                    command.Parameters.Clear();

                    return Convert.ToInt32(ret);
                }
            }
            catch (Exception ex)
            {
                ThrowSqlException(typeof(T), ex);
            }
            finally
            {
                if (needToClose)
                {
                    await this.Connection.CloseAsync();
                }
            }
            return 0;

        }

        public virtual async Task<int> UpdateAsync<T>(T dataitem, Expression<Func<T, bool>> condition) where T : DataItem
        {


            string pkid = dataitem.KeyName;
            object pkvalue = dataitem.PKValue;
            if (pkvalue == null && condition == null && pkid != null)
            {
                await InsertAsync(dataitem, false);
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
                await this.Connection.OpenAsync();
            }

            try
            {


                StringBuilder str_fields = new StringBuilder();
                int parameterIndex = 1;

                var updateExpression = dataitem.UpdateExpression as Expression<Func<T, bool>>;
                List<string> addedMembers = null;
                using (var command = CreateCommand(null))
                {

                    var fieldValues = dataitem.GetFieldValues(false, false);
                    if (fieldValues.Count == 0 && updateExpression == null)
                        return 0;


                    if (updateExpression != null)
                    {
                        addedMembers = new List<string>();
                        var parser = _expressionParserRoute.GetExpressionParser(updateExpression.Body);
                        if (parser == null)
                            throw new ParseUpdateExpressionException($"无法解析{updateExpression.Body}");
                        var setString = parser.Parse(updateExpression.Body, command, addedMembers, true);
                        if (setString != null)
                        {
                            setString = setString.Replace(" and ", ",").Replace(" or ", ",");
                            str_fields.Append(setString);
                        }
                    }

                    var tableSchema = SchemaManager.GetSchemaTable(dataitem.GetType());
                    foreach (var fieldValue in fieldValues)
                    {
                        if (addedMembers != null && addedMembers.Contains(fieldValue.FieldName))
                            continue;

                        if (str_fields.Length > 0)
                            str_fields.Append(',');
                        str_fields.Append(FormatObjectName(fieldValue.FieldName));
                        str_fields.Append('=');



                        object value = fieldValue.Value;
                        var parameter = command.CreateParameter();

                        SchemaColumn schemaColumn = tableSchema?.Columns.FirstOrDefault(m => m.Name == fieldValue.FieldName);
                        if (!SupportEnum && value != null && schemaColumn != null && schemaColumn.PropertyInfo.PropertyType.IsEnum)
                        {
                            value = Convert.ToInt32(value);
                        }
                        else if (!SupportEnum && value != null && value.GetType().IsEnum)
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
                            parameter.ParameterName = parameterName;
                            parameter.Value = value;

                            ConvertDesignTypeToDataTypeName(parameter, value, schemaColumn?.TypeName);

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
                            var parser = _expressionParserRoute.GetExpressionParser(condition.Body);
                            if (parser == null)
                                throw new ParseUpdateExpressionException($"无法解析{condition.Body}");
                            var where = parser.Parse(condition.Body, command, null, false);

                            command.CommandText = string.Format("update {0} set {1} where " + where, FormatTableName(dataitem.TableName), str_fields);
                        }
                        else
                        {
                            command.CommandText = string.Format("update {0} set {1} where {2}=@pid", FormatTableName(dataitem.TableName), str_fields, FormatObjectName(pkid.ToLower()));
                        }
                    }
                    else
                    {
                        if (condition != null)
                        {
                            var parser = _expressionParserRoute.GetExpressionParser(condition.Body);
                            if (parser == null)
                                throw new ParseUpdateExpressionException($"无法解析{condition.Body}");
                            var where = parser.Parse(condition.Body, command, null, false);

                            command.CommandText = string.Format("update {0} set {1} where " + where, FormatTableName(dataitem.TableName), str_fields);
                        }
                        else
                        {
                            command.CommandText = string.Format("update {0} set {1}", FormatTableName(dataitem.TableName), str_fields);
                        }
                    }
                    var ret = await command.ExecuteNonQueryAsync();


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
                    await this.Connection.CloseAsync();
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
                    command.CommandText = string.Format("delete from {0} where {1}=@p0", FormatTableName(dataitem.TableName), FormatObjectName(dataitem.KeyName.ToLower()));
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

        public virtual async Task DeleteAsync(DataItem dataitem)
        {
            bool needToClose = false;
            if (this.Connection.State != System.Data.ConnectionState.Open)
            {
                needToClose = true;
                await this.Connection.OpenAsync();
            }

            try
            {
                using (var command = CreateCommand(null))
                {
                    command.CommandText = string.Format("delete from {0} where {1}=@p0", FormatTableName(dataitem.TableName), FormatObjectName(dataitem.KeyName.ToLower()));
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@p0";
                    parameter.Value = dataitem.PKValue;
                    command.Parameters.Add(parameter);
                    await command.ExecuteNonQueryAsync();
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
                    await this.Connection.CloseAsync();
                }
            }

        }

        public object ExecSqlString(string sql, params object[] parames)
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
            catch (Exception ex)
            {
                throw new Exceptons.SqlExecException(ex.Message, sql, ex);
            }
            finally
            {
                if (needToClose)
                {
                    this.Connection.Close();
                }
            }
        }

        public async Task<object> ExecSqlStringAsync(string sql, params object[] parames)
        {
            bool needToClose = false;
            if (this.Connection.State != System.Data.ConnectionState.Open)
            {
                needToClose = true;
                await this.Connection.OpenAsync();
            }
            try
            {
                var command = CreateCommand(sql, parames);
                {
                    return await command.ExecuteScalarAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exceptons.SqlExecException(ex.Message, sql, ex);
            }
            finally
            {
                if (needToClose)
                {
                    await this.Connection.CloseAsync();
                }
            }
        }
        public int ExecuteNonQuery(string sql, params object[] parames)
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
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exceptons.SqlExecException(ex.Message, sql, ex);
            }
            finally
            {
                if (needToClose)
                {
                    this.Connection.Close();
                }
            }
        }

        public async Task<int> ExecuteNonQueryAsync(string sql, params object[] parames)
        {
            bool needToClose = false;
            if (this.Connection.State != System.Data.ConnectionState.Open)
            {
                needToClose = true;
                await this.Connection.OpenAsync();
            }
            try
            {
                var command = CreateCommand(sql, parames);
                {
                    return await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exceptons.SqlExecException(ex.Message, sql, ex);
            }
            finally
            {
                if (needToClose)
                {
                    await this.Connection.CloseAsync();
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
                using (var command = this.CreateCommand(sql, parames))
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
                                if (reader[i] != null)
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
                                    datatable.Columns.Add(new WayDataColumn(name, datareader.GetFieldType(i).FullName));
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

        public void ExecuteReader(Func<System.Data.IDataReader, bool> func, string sql, params object[] parames)
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

        public virtual void ConvertDesignTypeToDataTypeName(DbParameter dbParameter,  object value,string designType)
        {

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
            set
            {
                _dbcontext.ConnectionString = value;
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
