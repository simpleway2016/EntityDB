using Pomelo.Data.MySql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Way.EntityDB.DataBaseService;

namespace Way.EntityDB
{
    [Attributes.DatabaseTypeAttribute(DatabaseType.MySql)]
    class MySqlService:SqliteService
    { 

         public MySqlService()
        {
        }
         public MySqlService(DBContext dbcontext):base(dbcontext)
         {
            
        }
        public override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(this.ConnectionString, Microsoft.EntityFrameworkCore.ServerVersion.AutoDetect(this.ConnectionString));
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
            if (name.StartsWith("`") || name.StartsWith("("))
                return name;
            return string.Format("`{0}`", name);
        }


        public virtual string FormatTableName(string name)
        {
            if (!string.IsNullOrWhiteSpace(DBContext.Schema))
            {
                return $"`{DBContext.Schema}`.`{name}`";
            }
            else
            {
                return $"`{name}`";
            }
        }
        protected override string GetInsertIDValueSqlString(string pkColumnName)
        {
            return "SELECT LAST_INSERT_ID()";
        }
      

        public override void AllowIdentityInsert(string tablename, bool allow)
        {
            
        }
        public override WayDataTable SelectTable(string sql, int skip, int take, params object[] sqlparameters)
        {
            sql = string.Format("select * from ({0}) as t1 limit {1},{2}", sql, skip, take);
            return SelectTable(sql, sqlparameters);
        }

      
        protected override void ThrowSqlException(Type tableType, Exception ex)
        {
            if (!(ex is Pomelo.Data.MySql.MySqlException))
                throw ex;
            if (((Pomelo.Data.MySql.MySqlException)ex).Number != 1062)
                throw ex;
            throw new RepeatException(ex);
        }


        public override string ConvertConnectionString(string conStr)
        {
            Pomelo.Data.MySql.MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder(conStr);
            if (builder.Database != null)
            {
                builder.Database = builder.Database.ToLower();
            }
            return builder.ToString();
        }

        public override void ConvertDesignTypeToDataTypeName(DbParameter dbParameter, object value, string designType)
        {
            switch (designType)
            {
                case "datetimezone":
                    ((Pomelo.Data.MySql.MySqlParameter)dbParameter).MySqlDbType = MySqlDbType.Timestamp;
                    break;
                default:
                    break;
            }
        }
    }
}
