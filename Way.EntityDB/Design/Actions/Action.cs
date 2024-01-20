using System;
using System.Collections.Generic;
using System.Linq;


namespace Way.EntityDB.Design.Actions
{
    class ActionIdObject
    {
        public int ActionId = 0;
    }
    public abstract class Action
    {
        public int ID { get; set; }
        /// <summary>
        /// 操作人id
        /// </summary>
        public int UserId { get; set; }
        public abstract void Invoke( EntityDB.IDatabaseService invokingDB);
        internal abstract void BeforeSave();

        static System.Collections.Concurrent.ConcurrentDictionary<int, ActionIdObject> IDDict = new System.Collections.Concurrent.ConcurrentDictionary<int, ActionIdObject>();
        public object Save( EJ.DB.easyjob db , int databaseid)
        {
            BeforeSave();

            var action = new EJ.DesignHistory();
            action.Type = this.GetType().Name;
            action.DatabaseId = databaseid;
            action.Content = this.ToJsonString();

            var actionObject = IDDict.GetOrAdd(databaseid, new ActionIdObject());
            lock(actionObject)
            {
                if(actionObject.ActionId == 0)
                {
                    actionObject.ActionId = db.DesignHistory.Where(m => m.DatabaseId == databaseid).Max(m => m.ActionId).GetValueOrDefault();
                }
            }
            action.ActionId = System.Threading.Interlocked.Increment(ref actionObject.ActionId);

            db.Insert(action);
            return action.ActionId;
        }

        public void AddLog(EJ.DB.easyjob db, int userid, int databaseid)
        {
            var log = new EJ.SysLog();
            log.DatabaseId = databaseid;
            log.Content = this.ToJsonString();
            log.UserId = userid;
            log.Type = this.GetType().Name;
            log.Time = DateTime.Now;
            db.Insert(log);
        }
    }

}