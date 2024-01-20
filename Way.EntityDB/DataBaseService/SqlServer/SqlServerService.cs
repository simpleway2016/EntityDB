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
using System.Linq;
namespace Way.EntityDB
{
    [Attributes.DatabaseTypeAttribute(DatabaseType.SqlServer)]
    class SqlServerService : SqliteService
    {

         public SqlServerService()
        {
        }
         public SqlServerService(DBContext dbcontext):base(dbcontext)
         {
           
        }
        public override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(this.ConnectionString.Contains("Encrypt=", StringComparison.OrdinalIgnoreCase) == false)
            {
                //加上这个配置，否则会报System.Security.Authentication.AuthenticationException: The remote certificate was rejected by the provided RemoteCertificateValidationCallback.
                this.ConnectionString += ";Encrypt=False";
            }
            optionsBuilder.UseSqlServer(this.ConnectionString);
        }
      

        public override void AllowIdentityInsert(string tablename, bool allow)
        {
            this.ExecSqlString($"SET IDENTITY_INSERT [{tablename}] {(allow ? "on":"off")}");
        }

        protected int GetRowCount()
        {
            //
            return Convert.ToInt32( this.ExecSqlString("select @@ROWCOUNT"));
        }
        public override WayDataTable SelectTable(string sql, int skip, int take, params object[] sqlparameters)
        {
            sql = string.Format("SELECT * FROM  ({0}) as t1 ORDER BY 1   OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY", sql, skip, take);
            return SelectTable(sql, sqlparameters);
        }


        public override void UpdateLock(Type tableType, object pkValue)
        {
            var tableSchema = SchemaManager.GetSchemaTable(tableType);

            var pkColumn = tableSchema.Columns.FirstOrDefault(m => m.IsKey);
            var columnname = FormatObjectName(pkColumn.Name.ToLower());
            this.ExecSqlString($"select {columnname} from {FormatObjectName(tableSchema.TableName.ToLower())} WITH (UPDLOCK) where {columnname}=@p0", pkValue);
        }

        public override string FormatObjectName(string name)
        {
            if (name.StartsWith("[") || name.StartsWith("("))
                return name;
            return string.Format("[{0}]", name);
        }
        protected override string GetInsertIDValueSqlString(string pkColumnName)
        {
            return "select Scope_Identity()";
        }
        protected override bool GetInsertIDValueSqlStringInOneSql()
        {
            return true;
        }

        protected override void ThrowSqlException(Type tableType, Exception ex)
        {
            if (!(ex is Microsoft.Data.SqlClient.SqlException))
                throw ex;
            if (((Microsoft.Data.SqlClient.SqlException)ex).Number != 2601)
                throw ex;

            throw new RepeatException( ex);
        }



    }
}
