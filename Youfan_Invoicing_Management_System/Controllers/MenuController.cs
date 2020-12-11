using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Youfan_Invoicing_Management_System.Models;

namespace Youfan_Invoicing_Management_System.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        /// <summary>
        /// 通过用户添加权限
        /// </summary>
        /// <param name="MenuIDList">树形菜单节点ID</param>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddMenuListTree(List<int> MenuIDList, string userName)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //通过用户名查找用户信息
                var EmpInfo = db.emp.Where(e => e.username == userName).FirstOrDefault();
                //循环添加员工对菜单表的数据
                foreach (var item in MenuIDList)
                {
                    relation_emp_menu emp_Menu = new relation_emp_menu
                    {
                        emp_id = EmpInfo.emp_id,
                        menu_id = item,
                    };
                    db.relation_emp_menu.Add(emp_Menu);
                }
                if (db.SaveChanges() > 0)
                {
                    return Json(new
                    {
                        code = 0,
                        Success = true,
                        Message = "信息添加成功！！！"
                    });
                }
                return Json(new
                {
                    Success = false,
                    Message = "信息添加失败！！！"
                });
            }
        }
        /// <summary>
        /// 获取全部的菜单
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMenuListTree()
        {
            using (ERPEntities db = new ERPEntities())
            {
                var checkArr = "";
                var Menulist = (from m in db.menu
                                select new
                                {
                                    title = m.menu_name,
                                    id = m.menu_id,
                                    parentId = m.parent_menu_id,
                                    checkArr = "{\"type\": \"0\", \"checked\": \"0\"}"//开启复选框
                                }).ToList();
                var list = new
                {
                    code = 0,
                    message = "操作成功",
                    data = Menulist
                };
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 通过用户名来动态生成菜单
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetMenuList(string username)
        {
            using (ERPEntities db = new ERPEntities())
            {
                var Emp_Info = db.emp.Where(e => e.username == username).SingleOrDefault();
                var Emp_ID = Emp_Info.emp_id;
                var Menulist = (from e in db.emp
                                join rem in db.relation_emp_menu on e.emp_id equals rem.emp_id
                                join m in db.menu on rem.menu_id equals m.menu_id
                                where e.emp_id == Emp_ID
                                select new
                                {
                                    username = e.username,
                                    name = m.menu_name,
                                    menu_id = m.menu_id,
                                    parent_menu_id = m.parent_menu_id,
                                    url = m.url,
                                    icon = m.icon
                                }).ToList();
                return Json(Menulist, JsonRequestBehavior.AllowGet);

            }
        }
    }
}