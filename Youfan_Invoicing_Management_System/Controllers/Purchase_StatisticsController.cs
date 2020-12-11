using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Youfan_Invoicing_Management_System.Models;

namespace Youfan_Invoicing_Management_System.Controllers
{
    public class Purchase_StatisticsController : Controller
    {
        // GET: Purchase_Statistics

        /// <summary>
        /// 通过某年某月分组查询采购商品（入库数量）、月平均采购金额、月采购金额形成柱状图视图显示页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Purchase_Statistics_Index()
        {
            return View();
        }
        /// <summary>
        /// 通过某年某月分组查询销售商品销售（）出库数量、月平均销售金额、月销售金额形成柱状图
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Purchase_Data()
        {
            using (ERPEntities db = new ERPEntities())
            {
                var list = db.order_model
                              .Where(o => o.order_type_id == 2)
                              .GroupBy(time => new
                              {
                                  time.create_time.Value.Year,
                                  time.create_time.Value.Month
                              }) 
                              .Select(o => new
                              {
                                  Dates = o.Key.Year + "年-" + o.Key.Month + "月",
                                  Sum = o.Sum(s => s.total_num),
                                  Nums = o.Sum(s => s.total_price),
                                  avg = o.Average(s => s.total_price)
                              })
                              .OrderBy(o => new { o.Dates,o.Sum }).ToList();
                return Json(new { data = list }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}