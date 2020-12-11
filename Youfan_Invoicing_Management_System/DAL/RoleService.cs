using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Youfan_Invoicing_Management_System.Models;

namespace Youfan_Invoicing_Management_System.DAL
{
    public class RoleService
    {
        /// <summary>
        /// 获取所有的角色信息
        /// </summary>
        /// <returns></returns>
        public static List<role> SelectAll()
        {
            using (ERPEntities db =  new ERPEntities())
            {
                return db.role.Where(r=>r.role_name!="系统管理员").ToList();
            }
        }
    }
}