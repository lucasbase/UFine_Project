using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Youfan_Invoicing_Management_System.Ajax;
using Youfan_Invoicing_Management_System.BLL;
using Youfan_Invoicing_Management_System.ERP_Verification;
using Youfan_Invoicing_Management_System.Json;
using Youfan_Invoicing_Management_System.Models;
using static Youfan_Invoicing_Management_System.Ajax.AjaxResult;

namespace Youfan_Invoicing_Management_System.Controllers
{
    public class LoginController : Controller
    {
        private ERPEntities db = new ERPEntities();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAuthCode()
        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.CreateValidateCode(5);
            Session["ERP_session_verifycode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }

        /// <summary>
        /// 检查登录信息以及验证码是否正确
        /// </summary>
        /// <param name="StuLoginName">用户名</param>
        /// <param name="StuLoginPwd">用户登录密码</param>
        /// <param name="txt_code">验证码</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckCode(string username, string password, string txt_code)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("LoginIndex", "Login");
                }
                if (Session["ERP_session_verifycode"].ToString() != txt_code)
                {
                    //错误消息
                    throw new Exception("验证码错误，请重新输入！！！");
                }
                //此处验证用户名、密码
                if (!LoginManager.CheckLogin(username, password))
                {
                    //错误消息
                    throw new Exception("账号或密码不正确，请重新输入！！！");
                }
                else
                {
                    //判断该账号是否被冻结
                    var userinfo = db.emp.SingleOrDefault(u => u.username == username);
                    if (!(userinfo.IsFrozen == true))
                    {
                        //错误消息
                        throw new Exception("该账号已被冻结！请联系管理员！！！");
                    }
                    else
                    {
                        Session["emp_name"] = userinfo.emp_name;
                        Session["username"] = username;
                        //HttpContext.Session.Timeout = 2;
                        //验证成功
                        return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功！！！" }.ToJson());

                    }
                }

            }
            catch (Exception ex)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }
        }


        /// <summary>
        /// 退出登录功能操作方法
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginOut()
        {
            //清楚所有当前会话中Session
            Session.Abandon();
            //跳转登录页
            return RedirectToAction("Login");
        }
    }
}