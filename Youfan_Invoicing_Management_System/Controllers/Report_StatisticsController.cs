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
    public class Report_StatisticsController : Controller
    {
        // GET: Report_Statistics
        /// <summary>
        /// 通过某年某月分组查询销售商品销售（）出库数量、月平均销售金额、月销售金额形成柱状图视图显示页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Sales_Statistics_Index()
        {
            return View();
        }
        /// <summary>
        /// 通过某年某月分组查询销售商品销售（）出库数量、月平均销售金额、月销售金额形成柱状图
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Sales_Data()
        {
            using (ERPEntities db = new ERPEntities())
            {
                var list = db.order_model
                              .Where(o => o.order_type_id == 3)
                              .GroupBy(time => new
                              {
                                  time.create_time.Value.Year,
                                  time.create_time.Value.Month
                              })
                              .Select(o => new
                              {
                                  Dates = o.Key.Year + "年-" + o.Key.Month + "月",
                                  Sum= o.Sum(s => s.total_num),
                                  Nums = o.Sum(s => s.total_price),
                                  avg = o.Average(s => s.total_price)
                              })
                              .OrderBy(o => new { o.Dates, o.Sum }).ToList();
                return Json(new { data = list},JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 通过商品名称、商品单价、商品销售（出库）数量形成南丁格尔玫瑰图视图显示
        /// </summary>
        /// <returns></returns>
        public ActionResult Sales_Product_Price_Index() 
        {
            return View();
        }

        /// <summary>
        /// 通过商品名称、商品单价、商品销售（出库）数量形成南丁格尔玫瑰图
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Sales_Product_Price()
        {
            using (ERPEntities db = new ERPEntities())
            {
                var list = db.order_model.Include("product")
                              .Where(o => o.order_type_id == 3)
                              .GroupBy(x =>new { x.product.product_name,x.product.in_price })
                              .Select(o => new
                              {
                                  product_name = o.Key.product_name,
                                  Price = o.Key.in_price,
                                  Nums = o.Sum(s => s.total_num)
                              })
                              .ToList();
                return Json(new { data = list }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}