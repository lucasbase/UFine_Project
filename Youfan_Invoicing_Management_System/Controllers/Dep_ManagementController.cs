using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Youfan_Invoicing_Management_System.BLL;
using Youfan_Invoicing_Management_System.ERP_Verification;
using Youfan_Invoicing_Management_System.Models;

namespace Youfan_Invoicing_Management_System.Controllers
{
    [Authorize]
    public class Dep_ManagementController : Controller
    {
        // GET: Dep_Management
        /// <summary>
        /// 显示界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Dep_Management_Index()
        {
            return View();
        }
        /// <summary>
        /// 页面初始化数据分页显示
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="limit">每页显示的条目数</param>
        /// <returns></returns>
        public ActionResult Dep_ManagementIndex(int page, int limit)
        {
            using (ERPEntities db = new ERPEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;//关闭EF的默认加载
                //数据显示
                var pageQuery = db.dep.Select(e => new { Dep_ID = e.dep_id, Dep_Name = e.dep_name, Dep_Tel = e.tel }).ToList();
                //将显示的数据分页显示
                var PageInfo = pageQuery.OrderBy(e => e.Dep_ID).Skip(limit * (page - 1)).Take(limit).ToList();
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
        /// <summary>
        /// 通过部门名称进行模糊查询并分页查询出来的显示数据
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="limit">每页显示的条数</param>
        /// <param name="KeyWords">关键字</param>
        /// <returns></returns>
        public ActionResult GetAjaxDepList(int page, int limit, string KeyWords)
        {
            using (ERPEntities db = new ERPEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;//关闭EF的默认加载
                //数据显示
                var pageQuery = db.dep.Where(d => d.dep_name.Contains(KeyWords)).Select(d => new { Dep_ID = d.dep_id, Dep_Name = d.dep_name, Dep_Tel = d.tel }).ToList();
                //将显示的数据分页显示
                var PageInfo = pageQuery.OrderBy(e => e.Dep_ID).Skip(limit * (page - 1)).Take(limit).ToList();
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

        /// <summary>
        /// 生成部门添加的视图界面
        /// </summary>
        /// <returns></returns>
        public ActionResult DepAdd()
        {
            return View();
        }
        /// <summary>
        /// 添加部门信息
        /// </summary>
        /// <param name="DepInfo">视图页面传过来的部门对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DepAdd(dep DepInfo)
        {
            using (ERPEntities db = new ERPEntities())
            {
                dep AddDepInfo = new dep()
                {
                    dep_name = DepInfo.dep_name,
                    tel = DepInfo.tel,
                };
                db.dep.Add(AddDepInfo);
                if (db.SaveChanges() > 0)
                {
                    return Json(new
                    {
                        code = 0,
                        Success = true,
                        Message = "部门信息添加成功！！！"
                    });
                }
                return Json(new
                {
                    Success = false,
                    Message = "部门信息添加失败！！！"
                });
            }
        }
        /// <summary>
        /// 通过部门ID查找到指定的部门信息
        /// </summary>
        /// <param name="dep_id">部门ID</param>
        /// <returns></returns>
        public ActionResult DepEidt(int? dep_id)
        {
            using (ERPEntities db = new ERPEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var editDepInfo = db.dep.FirstOrDefault(d => d.dep_id == dep_id);
                ViewBag.dep_id = editDepInfo.dep_id;
                ViewBag.dep_name = editDepInfo.dep_name;
                ViewBag.tel = editDepInfo.tel;
            }
            return View();
        }

        /// <summary>
        /// 创建生成修改员工信息的视图
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DepEidtlInfo(dep DepInfo)
        {
            using (ERPEntities db = new ERPEntities())
            {
                dep editDep = db.dep.FirstOrDefault(d => d.dep_id == DepInfo.dep_id);
                editDep.dep_name = DepInfo.dep_name;
                editDep.tel = DepInfo.tel;
                //db.emp.Add(EmpInfo);
                if (db.SaveChanges() > 0)
                {
                    return Json(new
                    {
                        code = 0,
                        Success = true,
                        Message = "部门信息修改成功！！！"
                    });
                }
                return Json(new
                {
                    Success = false,
                    Message = "部门信息修改失败！！！"
                });
            }
        }
        /// <summary>
        /// 单行删除部门信息
        /// </summary>
        /// <param name="Dep_ID">部门ID</param>
        /// <returns></returns>
        public ActionResult DelDep(int? Dep_ID)
        {
            try
            {
                using (ERPEntities db = new ERPEntities())
                {
                    dep Depinfo = db.dep.FirstOrDefault(d => d.dep_id == Dep_ID);
                    db.dep.Remove(Depinfo);
                    if (db.SaveChanges() > 0)
                    {
                        return Json(new
                        {
                            Success = true,
                            Message = "删除信息成功！！！"
                        });
                    }
                    return Json(new
                    {
                        Success = false,
                        Message = "删除信息失败！！！"
                    });
                }

            }
            catch (Exception)
            {
                return Json(new
                {
                    Success = false,
                    Message = "删除信息失败！！！该部门有下级人员！！！"
                });
            }
        }
        /// <summary>
        /// 批量删除的操作方法
        /// </summary>
        /// <param name="Emp_IDs">页面数据表格选中的部门ID集合</param>
        /// <returns></returns>
        public ActionResult BatchDelDeps(List<int> Dep_IDs)
        {
            try
            {
                using (ERPEntities db = new ERPEntities())
                {
                    var delDepsQuery = db.dep.Where(d => Dep_IDs.Contains(d.dep_id));
                    db.dep.RemoveRange(delDepsQuery);
                    if (db.SaveChanges() > 0)
                    {
                        return Json(new
                        {
                            Success = true,
                            Message = "批量删除成功！！！"
                        });
                    }

                    return Json(new
                    {
                        Success = false,
                        Message = "批量删除失败！！！"
                    });
                }
            }
            catch (Exception)
            {
                return Json(new
                {
                    Success = false,
                    Message = "删除信息失败！！！所选部门中有下级人员！！！"
                });
            }
        }
    }
}