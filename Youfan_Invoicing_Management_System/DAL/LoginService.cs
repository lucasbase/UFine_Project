using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Youfan_Invoicing_Management_System.Models;

namespace Youfan_Invoicing_Management_System.DAL
{
    public class LoginService
    {
        /// <summary>
        /// 用户登录方法
        /// </summary>
        /// <param name="login_name">学生的登录名</param>
        /// <returns></returns>
        public static emp Loginemp(string login_name)
        {
            //实例化上下文对象
            using (ERPEntities db = new ERPEntities())
            {
                return db.emp.SingleOrDefault(s => s.username == login_name);
            }
        }
        /// <summary>
        /// 判断是否已存在该用户名
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public static emp Checkusername(string username)
        {
            //实例化上下文对象
            using (ERPEntities db = new ERPEntities())
            {
                return db.emp.SingleOrDefault(s => s.username == username);
            }
        }
    }
}