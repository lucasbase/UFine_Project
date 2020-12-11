using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Youfan_Invoicing_Management_System.DAL;
using Youfan_Invoicing_Management_System.ERP_Verification;

namespace Youfan_Invoicing_Management_System.BLL
{
    public class LoginManager
    {
        /// <summary>
        /// 用户登录方法
        /// </summary>
        /// <param name="login_name">用户的登录名</param>
        /// <returns></returns>
        public static bool CheckLogin(string login_name, string login_pwd)
        {
            var student = LoginService.Loginemp(login_name);
            if (student == null)
            {
                return false;
            }
            FormsAuthentication.SetAuthCookie(login_name, false);
            return DESEncrypt.DecryptDES(student.password) == login_pwd;
            //return DESEncrypt.DecryptDES(student.StuLoginPwd) == login_pwd;//获取数据库的密文密码与明文密码进行对比
        }
    }
}