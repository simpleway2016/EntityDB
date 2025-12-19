using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Way.EntityDB.DataBaseService;

namespace Way.EntityDB
{
    [Attributes.DatabaseTypeAttribute(DatabaseType.PostgreSql)]
    class PostgreSQLService :SqliteService
    {
        protected override bool SupportEnum => false;
        public PostgreSQLService()
        {
        }
        public PostgreSQLService(DBContext dbcontext):base(dbcontext)
         {

        }

        public override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(this.ConnectionString);
        }
      

        public override void AllowIdentityInsert(string tablename, bool allow)
        {
           
        }
        public override WayDataTable SelectTable(string sql, int skip, int take, params object[] sqlparameters)
        {
            sql = $"select * from ({sql}) as t1 limit {take} offset {skip}";
            return SelectTable(sql, sqlparameters);
        }

        public override void UpdateLock(Type tableType, object pkValue)
        {
            var tableSchema = SchemaManager.GetSchemaTable(tableType);
            var pkColumn = tableSchema.Columns.FirstOrDefault(m => m.IsKey);
            var columnname = FormatObjectName(pkColumn.Name.ToLower());
            this.ExecSqlString($"select {columnname} from {FormatObjectName(tableSchema.TableName.ToLower())} where {columnname}=@p0 for update", pkValue);
        }

        public override string FormatObjectName(string name)
        {
            return $"\"{name}\"";
        }

        public override string FormatTableName(string name)
        {
            if (!string.IsNullOrWhiteSpace(DBContext.Schema))
            {
                return $"\"{DBContext.Schema}\".\"{name}\"";
            }
            else
            {
                return $"\"{name}\"";
            }
        }

        protected override bool GetInsertIDValueSqlStringInOneSql()
        {
            return true;
        }
        protected override string GetInsertIDValueSqlString(string pkColumnName)
        {
            if (pkColumnName.IsNullOrEmpty())
                return null;
            return $"RETURNING {pkColumnName}";
        }

        protected override void ThrowSqlException(Type tableType, Exception ex)
        {
            if (!(ex is PostgresException))
                throw ex;
            PostgresException nerror = ex as PostgresException;
            if(nerror.SqlState != "23505")
                throw ex;
            throw new RepeatException(ex);
        }

        public override string ConvertConnectionString(string conStr)
        {
            Npgsql.NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder(conStr);
            if(builder.Database != null)
            {
                builder.Database = builder.Database.ToLower();
            }
           
            return builder.ToString();
        }

        public override void ConvertDesignTypeToDataTypeName(DbParameter dbParameter, object value, string designType)
        {
            if (value == null)
                return;

            switch (designType)
            {
                case "jsonb":
                    dbParameter.Value = System.Text.Json.JsonSerializer.Serialize(value, DefaultJsonSerializerOptions);
                    ((Npgsql.NpgsqlParameter)dbParameter).DataTypeName = "jsonb";
                    break;
                case "datetimezone":
                    ((Npgsql.NpgsqlParameter)dbParameter).DataTypeName = "timestamp with time zone";
                    break;
                default:
                    break;
            }
        }
    }
}
