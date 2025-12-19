using System;
using System.Collections.Generic;
using System.Linq;


namespace Way.EntityDB.Design.Services
{
    public interface IDatabaseDesignService
    {
        string GetEasyJobTableFullName(EntityDB.IDatabaseService db);
        void Drop(EJ.Databases database);
        void Create(EJ.Databases database);
        void Create(EJ.Databases database,string schema);
        void CreateEasyJobTable(EntityDB.IDatabaseService db);
        void ChangeName(EJ.Databases database, string newName,string newConnectString);
        /// <summary>
        /// 获取数据库的真实字段描述
        /// </summary>
        /// <param name="db"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        List<EJ.DBColumn> GetCurrentColumns(IDatabaseService db, string tablename);
        /// <summary>
        /// 获取数据库的真实索引描述
        /// </summary>
        /// <param name="db"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        List<Design.IndexInfo> GetCurrentIndexes(IDatabaseService db, string tablename);
        /// <summary>
        /// 获取数据库的数据表列表
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        List<TableInfo> GetCurrentTableNames(IDatabaseService db);
        string GetObjectFormat();
    }

    public class TableInfo
    {
        public string Name { get; set; }
        public string Comment { get; set; }
    }
}