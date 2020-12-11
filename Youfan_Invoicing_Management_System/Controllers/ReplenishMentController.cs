using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Youfan_Invoicing_Management_System.Models;

namespace Youfan_Invoicing_Management_System.Controllers
{
    [Authorize]
    public class ReplenishMentController : Controller
    {
        // GET: ReplenishMent
        public ActionResult Index(int page, int limit)
        {
            using (ERPEntities db = new ERPEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var model = db.order_model.Include("emp")
                    .Include("product")
                    .Include("order_type")
                    .Include("supplier")
                    .Where(o => o.order_type_id == 1)
                    .Select(o => new
                    {
                        o.order_id,
                        o.order_num,
                        creater = o.emp.emp_name,
                        create_time = o.create_time,
                        checker = o.checker,
                        end_time = o.end_time,
                        o.OutIn_number,
                        o.order_type.order_type_name,
                        o.order_state,
                        o.total_num,
                        o.total_price,
                        o.supplier.supplier_name,
                        o.order_type_id,
                        o.product.product_name,
                        o.product.unit
                    })
                    .ToList();

                var BlurModel = model
                    .OrderByDescending(o => o.order_id)
                    .Skip(limit * (page - 1))
                    .Take(limit);

                var result = new
                {
                    code = 0,
                    msg = "",
                    count = model.Count(),
                    data = BlurModel
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Replenish()
        {
            return View();
        }


        public ActionResult BlurSelRep(int page, int limit, string KeyWords, string dateInfo)
        {
            using (ERPEntities db = new ERPEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                if (dateInfo == "")
                {
                    var model = db.order_model.Include("emp")
                   .Include("product")
                   .Include("order_type")
                   .Include("supplier")
                   .Where(o => o.order_type_id == 1)
                   .Select(o => new
                   {
                       o.order_id,
                       o.order_num,
                       creater = o.emp.emp_name,
                       create_time = o.create_time,
                       checker = o.checker,
                       end_time = o.end_time,
                       o.OutIn_number,
                       o.order_type.order_type_name,
                       o.order_state,
                       o.total_num,
                       o.total_price,
                       o.supplier.supplier_name,
                       o.order_type_id,
                       o.product.product_name,
                       o.product.unit
                   })
                   .Where(o =>
                   o.order_num.Contains(KeyWords) || o
                   .creater.Contains(KeyWords) || o
                   .product_name.Contains(KeyWords))
                   .ToList();
                    var BlurModel = model
                        .OrderByDescending(o => o.order_id)
                        .Skip(limit * (page - 1))
                        .Take(limit);
                    var result = new
                    {
                        code = 0,
                        msg = "",
                        count = model.Count(),
                        data = BlurModel
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ////声明变量存储时间
                    var startTime = DateTime.Parse(dateInfo);
                    var endTime = DateTime.Parse(dateInfo).AddDays(1);
                    var model = db.order_model.Include("emp")
                .Include("product")
                .Include("order_type")
                .Include("supplier")
                .Where(o => 
                o.order_type_id == 1 &&
                ((o.order_num.Contains(KeyWords)||
                  o.emp.emp_name.Contains(KeyWords)||
                  o.product.product_name.Contains(KeyWords))&&
                  (o.create_time >= startTime && o.create_time < endTime)))
                .Select(o => new
                {
                    o.order_id,
                    o.order_num,
                    creater = o.emp.emp_name,
                    create_time = o.create_time,
                    checker = o.checker,
                    end_time = o.end_time,
                    o.OutIn_number,
                    o.order_type.order_type_name,
                    o.order_state,
                    o.total_num,
                    o.total_price,
                    o.supplier.supplier_name,
                    o.order_type_id,
                    o.product.product_name,
                    o.product.unit
                })
                .ToList();
                    var BlurModel = model
                        .OrderByDescending(o => o.order_id)
                        .Skip(limit * (page - 1))
                        .Take(limit);
                    var result = new
                    {
                        code = 0,
                        msg = "",
                        count = model.Count(),
                        data = BlurModel
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
        }


        public ActionResult Replenish_Modify(int? order_id)
        {
            using (ERPEntities db = new ERPEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var model = db.order_model.Include("product").Where(o => order_id == o.order_id).FirstOrDefault();
                ViewBag.pro_name = model.product.product_name;
                ViewBag.pro_inprice = model.product.in_price;
                ViewBag.pro_num = model.total_num;
                ViewBag.pro_unit = model.product.unit;
                ViewBag.order_id = order_id;
                ViewBag.state = model.order_state;
                return View();
            }
        }

        [HttpPost]
        public ActionResult ChangeReplenish(order_model order_model, int? order_id, decimal? in_price)
        {
            using (ERPEntities db = new ERPEntities())
            {
                var price = in_price;
                order_model model = db.order_model.FirstOrDefault(o => order_id == o.order_id);
                model.total_num = order_model.total_num;
                model.total_price = (model.total_num) * price;
                if (db.SaveChanges() > 0)
                {
                    return Json(new
                    {
                        Success = true,
                        Message = "修改成功"
                    });
                }
                return Json(new
                {
                    Success = false,
                    Message = "修改失败"
                });
            }
        }


        [HttpPost]
        public ActionResult CancelReplenish(int? order_id)
        {
            using (ERPEntities db = new ERPEntities())
            {
                var DelRep = db.order_model.FirstOrDefault(o => o.order_id == order_id);
                db.order_model.Remove(DelRep);
                if (db.SaveChanges() > 0)
                {
                    return Json(new
                    {
                        Success = true,
                        Message = "取消申请成功！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "取消申请失败！"
                    });
                }
            }
        }
    }
}