﻿using Way.EntityDB.Design.Services;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Way.EntityDB.Design.Actions
{
    public class CreateTableAction : Action
    {
        public EJ.DBTable Table
        {
            get;
            set;
        }
        public EJ.DBColumn[] Columns
        {
            get;
            set;
        }
        public IndexInfo[] IDXConfigs
        {
            get;
            set;
        }

         public CreateTableAction()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid">操作人id</param>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <param name="idxConfigs"></param>
        public CreateTableAction(int userid , EJ.DBTable table , EJ.DBColumn[] columns
            , IndexInfo[] idxConfigs)
        {
            this.UserId = userid;
            this.Table = table;
            this.Columns = columns;

            this.IDXConfigs = idxConfigs;
        }
        internal override void BeforeSave()
        {
            foreach (var c in this.Columns)
            {
                c.ChangedProperties.Clear();
            }
        }
        public override void Invoke( EntityDB.IDatabaseService invokingDB)
        {
  
             ITableDesignService service = DBHelper.CreateTableDesignService(invokingDB.DBContext.DatabaseType);
            service.CreateTable(invokingDB, this.Table, this.Columns, IDXConfigs);
        }
        public override string ToString()
        {
            return string.Format("Create Table {0}", this.Table.Name);
        }
    }
}