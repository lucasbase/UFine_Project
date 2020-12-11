using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Youfan_Invoicing_Management_System.Models;
using Youfan_Invoicing_Management_System.DAL;

namespace Youfan_Invoicing_Management_System.BLL
{
    public class RoleManager
    {
        /// <summary>
        /// 获取所有的角色信息
        /// </summary>
        /// <returns></returns>
        public static List<role> GetAll()
        {
            return RoleService.SelectAll();
        }
    }
}