using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Youfan_Invoicing_Management_System.Models;

namespace Youfan_Invoicing_Management_System.Controllers
{
    [Authorize]
    public class Role_ManagementController : Controller
    {
        // GET: Role_Management
        public ActionResult Role_Index()
        {
            return View();
        }
        /// <summary>
        /// 页面初始化数据分页显示
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="limit">条目数</param>
        /// <returns></returns>
        public ActionResult Role_Index_Info(int page, int limit)
        {
            using (ERPEntities db = new ERPEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;//关闭EF的默认加载
                //数据显示
                var pageQuery = db.role.Select(r => new {Role_ID = r.role_id, Dep_ID = r.dep_id, Dep_Name = r.dep.dep_name, Role_Name = r.role_name }).ToList();
                //将显示的数据分页显示
                var PageInfo = pageQuery.OrderBy(e => e.Role_ID).Skip(limit * (page - 1)).Take(limit).ToList();
                var result = new
                {
                    code = 0,
                    msg = "",
                    count = pageQuery.Count,//获取总条数
                    data = PageInfo
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}