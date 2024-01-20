using System;
using System.Collections.Generic;
using System.Linq;

namespace Way.EJServer
{
    public class EJDB_Check : EJDB
    {
        

        public override IQueryable<EJ.Project> Project
        {
            get
            {
                if (LoginUser.Role == EJ.User_RoleEnum.管理员)
                {
                    return base.Project;
                }
                else
                {
                    return from m in base.Project
                           join p in ProjectPower on m.id equals p.ProjectID
                           where p.UserID == LoginUser.id
                           select m;
                }
            }
        }
        public override IQueryable<EJ.Databases> Databases
        {
            get
            {
                if (LoginUser.Role.GetValueOrDefault().HasFlag(EJ.User_RoleEnum.管理员))
                {
                    return base.Databases;
                }
                else
                {
                    return from m in base.Databases
                           join p in DBPower on m.id equals p.DatabaseID
                           where p.UserID == LoginUser.id
                           select m;
                }
            }
        }
        public IQueryable<EJ.Bug> MyBugList
        {
            get
            {
                if (LoginUser.Role.GetValueOrDefault().HasFlag(EJ.User_RoleEnum.数据库设计师))
                {
                    var query = from m in this.Bug
                                select m;
                    return query;
                }
                else
                {
                    var query = from m in this.Bug
                                where m.HandlerID == LoginUser.id || m.SubmitUserID == LoginUser.id
                                select m;
                    return query;
                }
            }
        }
    }

   
}