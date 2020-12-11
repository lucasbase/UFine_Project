using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Web;
using System.Web.Mvc;
using Youfan_Invoicing_Management_System.Models;

namespace Youfan_Invoicing_Management_System.Controllers
{
    [Authorize]
    public class WarehouseMentController : Controller
    {
        // GET: WarehouseMent
        public ActionResult Index()
        {
            return View();
        }

        #region 出库模块  版本：1.0

        /// <summary>
        /// 出库管理视图
        /// </summary>
        /// <returns></returns>
        public ActionResult OutWarehouse()
        {
            return View();
        }

        /// <summary>
        /// 出库管理表格数据源
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public ActionResult DataOutWarehouse(int page, int limit)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //关闭延迟加载
                db.Configuration.LazyLoadingEnabled = false;

                //多表查询数据，投影
                var model = db.order_model.Include("emp")
                    .Include("product")
                    .Include("order_type")
                    .Include("supplier")
                    .Where(o => o.order_type_id == 3)
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
                        o.product.unit,
                        o.product.product_type.supplier_id
                    })
                    .ToList();

                //实现分页
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

        /// <summary>
        /// 出库管理模糊查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="KeyWords">模糊查询关键字：订单号/创建人/商品名称</param>
        /// <returns></returns>
        public ActionResult OutBlurSelModel(int page, int limit, string KeyWords)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //关闭延迟加载
                db.Configuration.LazyLoadingEnabled = false;

                //多表查询数据，投影
                var model = db.order_model.Include("emp")
                    .Include("product")
                    .Include("order_type")
                    .Include("supplier")
                    .Where(o => o.order_type_id == 3)
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
                        o.product.unit,
                        o.product.product_type.supplier_id
                    })
                    .ToList();

                //实现模糊查询以及分页
                var BlurModel = model
                    .Where(o => o
                    .order_num.Contains(KeyWords) || o
                    .creater.Contains(KeyWords) || o
                    .product_name.Contains(KeyWords))
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

        /// <summary>
        /// 确认出库/详情  页面
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        public ActionResult OutOrDetils_View(int? order_id)
        {
            string checkerName = "";
            using (ERPEntities db = new ERPEntities())
            {
                //通过审核人的ID获取到订单审核人的姓名
                var checkerInfo = db.order_model.Include("emp").Where(o => o.order_id == order_id).FirstOrDefault();
                //判断审核人是否为空
                if (checkerInfo.checker == null)
                {
                    checkerName = "";
                }
                else
                {
                    //如果不为空则通过审核人的ID查询到员工表中对应的审核人姓名
                    var checker = db.emp.Where(e => e.emp_id == checkerInfo.checker).FirstOrDefault();
                    checkerName = checker.emp_name;
                }

                //多表查询数据，投影
                var model = db.order_model.Include("emp")
                    .Include("product")
                    .Include("order_type")
                    .Include("supplier")
                    .Where(o => o.order_type_id == 3 && o.order_id == order_id)//条件:销售单，订单ID
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
                        order_state = o.order_state,
                        o.total_num,
                        o.total_price,
                        o.supplier.supplier_name,
                        o.order_type_id,
                        o.product.product_name,
                        o.product.in_price,
                        o.product.unit,
                        o.product.product_type.supplier_id
                    })
                    .FirstOrDefault();

                ViewBag.order_num = model.order_num;
                ViewBag.creater = model.creater;
                ViewBag.create_time = model.create_time;
                ViewBag.Out_num = model.OutIn_number;
                ViewBag.checker = checkerName;
                ViewBag.end_time = model.end_time;
                ViewBag.state = (bool)model.order_state ? "已出库" : "等待出库";
                ViewBag.product_name = model.product_name;
                ViewBag.in_price = model.in_price;
                ViewBag.total_price = model.total_price;
                ViewBag.total_num = model.total_num;
                ViewBag.unit = model.unit;

            }
            return View();
        }

        /// <summary>
        /// 点击按钮后，确认出库，对数据库进行一系列操作
        /// </summary>
        /// <param name="order_model"></param>
        /// <param name="order_id">视图传递的订单ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SureOut_Btn(order_model order_model, int? order_id)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //获取确认出库员工的用户名并查询到员工ID
                var username = Session["username"].ToString();
                var empInfo = db.emp.Where(e => e.username == username).FirstOrDefault();
                var checkerID = empInfo.emp_id;

                //修改订单表数据，添加审核人，结束时间，出库单号（CK）,修改状态为已审核
                order_model model = db.order_model.FirstOrDefault(o => order_id == o.order_id);
                model.checker = checkerID;
                model.end_time = DateTime.Now;
                model.OutIn_number = "CK" + DateTime.Now.ToString("yyyyMMddhhmmss");
                model.order_state = true;

                //修改商品表库存，出库：减
                var pro_id = model.product_id;
                product product = db.product.FirstOrDefault(p => pro_id == p.product_id);

                if (product.pro_Inventory < model.total_num)
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "库存不足，出库失败"
                    });
                }
                product.pro_Inventory -= model.total_num;

                //为仓库日志表添加数据
                store_log store_Log = new store_log
                {
                    emp_id = checkerID,//员工ID
                    order_id = order_id,//订单ID
                    oper_time = DateTime.Now,//订单时间
                    num = model.total_num,//订单数量
                    order_type_id = model.order_type_id//商品类型
                };
                //添加
                db.store_log.Add(store_Log);
                //保存
                
                if (db.SaveChanges() > 0)
                {
                    return Json(new
                    {
                        Success = true,
                        Message = "出库成功"
                    });
                }
                return Json(new
                {
                    Success = false,
                    Message = "出库失败"
                });
            }
        }

        #endregion


        #region 入库模块 版本：1.0

        /// <summary>
        /// 入库管理视图
        /// </summary>
        /// <returns></returns>
        public ActionResult InWarehouse()
        {
            return View();
        }

        /// <summary>
        /// 入库管理表格数据源
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public ActionResult DataInWarehouse(int page, int limit)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //关闭延迟加载
                db.Configuration.LazyLoadingEnabled = false;

                //多表查询数据，投影
                var model = db.order_model.Include("emp")
                    .Include("product")
                    .Include("order_type")
                    .Include("supplier")
                    .Where(o => o.order_type_id == 2)
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
                        o.product.unit,
                        o.product.product_type.supplier_id
                    })
                    .ToList();

                //实现分页
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

        /// <summary>
        /// 入库管理模糊查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="KeyWords">模糊查询关键字：订单号/创建人/商品名称</param>
        /// <returns></returns>
        public ActionResult InBlurSelModel(int page, int limit, string KeyWords)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //关闭延迟加载
                db.Configuration.LazyLoadingEnabled = false;

                //多表查询数据，投影
                var model = db.order_model.Include("emp")
                    .Include("product")
                    .Include("order_type")
                    .Include("supplier")
                    .Where(o => o.order_type_id == 2)
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
                        o.product.unit,
                        o.product.product_type.supplier_id
                    })
                    .ToList();

                //实现模糊查询以及分页
                var BlurModel = model
                    .Where(o => o
                    .order_num.Contains(KeyWords) || o
                    .creater.Contains(KeyWords) || o
                    .product_name.Contains(KeyWords))
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


        /// <summary>
        /// 确认入库/详情  页面
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        public ActionResult InOrDetils_View(int? order_id)
        {
            string checkerName = "";
            using (ERPEntities db = new ERPEntities())
            {
                //通过审核人的ID获取到订单审核人的姓名
                var checkerInfo = db.order_model.Include("emp").Where(o => o.order_id == order_id).FirstOrDefault();
                //判断审核人是否为空
                if (checkerInfo.checker == null)
                {
                    checkerName = "";
                }
                else
                {
                    //如果不为空则通过审核人的ID查询到员工表中对应的审核人姓名
                    var checker = db.emp.Where(e => e.emp_id == checkerInfo.checker).FirstOrDefault();
                    checkerName = checker.emp_name;
                }

                //多表查询数据，投影
                var model = db.order_model.Include("emp")
                    .Include("product")
                    .Include("order_type")
                    .Include("supplier")
                    .Where(o => o.order_type_id == 2 && o.order_id == order_id)//条件:采购单，订单ID
                    .Select(o => new
                    {
                        o.order_id,
                        order_num = o.order_num,
                        creater = o.emp.emp_name,
                        create_time = o.create_time,
                        checker = o.checker,
                        end_time = o.end_time,
                        o.OutIn_number,
                        o.order_type.order_type_name,
                        order_state = o.order_state,
                        o.total_num,
                        o.total_price,
                        o.supplier.supplier_name,
                        o.order_type_id,
                        o.product.product_name,
                        o.product.in_price,
                        o.product.unit,
                        o.product.product_type.supplier_id
                    })
                    .FirstOrDefault();

                ViewBag.order_num = model.order_num;
                ViewBag.creater = model.creater;
                ViewBag.create_time = model.create_time;
                ViewBag.Out_num = model.OutIn_number;
                ViewBag.checker = checkerName;
                ViewBag.end_time = model.end_time;
                ViewBag.state = (bool)model.order_state ? "已入库" : "等待入库";
                ViewBag.product_name = model.product_name;
                ViewBag.in_price = model.in_price;
                ViewBag.total_price = model.total_price;
                ViewBag.total_num = model.total_num;
                ViewBag.unit = model.unit;

            }
            return View();
        }


        /// <summary>
        /// 点击按钮后，确认入库，对数据库进行一系列操作
        /// </summary>
        /// <param name="order_model"></param>
        /// <param name="order_id">视图传递的订单ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SureIn_Btn(order_model order_model, int? order_id)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //获取确认入库员工的用户名并查询到员工ID
                var username = Session["username"].ToString();
                var empInfo = db.emp.Where(e => e.username == username).FirstOrDefault();
                var checkerID = empInfo.emp_id;

                //修改订单表数据，添加审核人，结束时间，入库单号（CK）,修改状态为已审核
                order_model model = db.order_model.FirstOrDefault(o => order_id == o.order_id);
                model.checker = checkerID;
                model.end_time = DateTime.Now;
                model.OutIn_number = "RK" + DateTime.Now.ToString("yyyyMMddhhmmss");
                model.order_state = true;

                //修改商品表库存，入库：加
                var pro_id = model.product_id;
                product product = db.product.FirstOrDefault(p => pro_id == p.product_id);
                product.pro_Inventory += model.total_num;

                //为仓库日志表添加数据
                store_log store_Log = new store_log
                {
                    emp_id = checkerID,//员工ID
                    order_id = order_id,//订单ID
                    oper_time = DateTime.Now,//订单时间
                    num = model.total_num,//订单数量
                    order_type_id = model.order_type_id//商品类型
                };
                //添加
                db.store_log.Add(store_Log);
                //保存
                if (db.SaveChanges() > 0)
                {
                    return Json(new
                    {
                        Success = true,
                        Message = "入库成功"
                    });
                }
                return Json(new
                {
                    Success = false,
                    Message = "入库失败"
                });
            }
        }
        #endregion


        #region 仓库日志 版本：2.0

        /// <summary>
        /// 仓库日志视图
        /// </summary>
        /// <returns></returns>
        public ActionResult StoreLog_View()
        {
            return View();
        }

        /// <summary>
        /// 仓库日志的数据源
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public ActionResult StoreLog_Data(int page, int limit)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //关闭延迟加载
                db.Configuration.LazyLoadingEnabled = false;
                //多表查询，投影出日志所需要的数据
                var log = db.store_log
                    .Include("emp")
                    .Include("order_model")
                    .Include("order_type")
                    .Select(s => new
                    {
                        log_id = s.log_id,
                        emp_name = s.emp.emp_name,
                        order_num = s.order_model.OutIn_number,
                        oper_time = s.oper_time,
                        num = s.num,
                        order_type = s.order_type.order_type_id
                    }).ToList();
                //分页功能
                var logPaging = log
                    .OrderByDescending(s => s.log_id)
                    .Skip(limit * (page - 1))
                    .Take(limit);

                var result = new
                {
                    code = 0,
                    msg = "",
                    count = log.Count(),
                    data = logPaging
                };
                //返回json格式数据
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult BlurSel_StoreLog(int page, int limit, string KeyWords)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //关闭延迟加载
                db.Configuration.LazyLoadingEnabled = false;
                //多表查询，投影出日志所需要的数据
                var log = db.store_log
                    .Include("emp")
                    .Include("order_model")
                    .Include("order_type")
                    .Select(s => new
                    {
                        log_id = s.log_id,
                        emp_name = s.emp.emp_name,
                        order_num = s.order_model.OutIn_number,
                        oper_time = s.oper_time,
                        num = s.num,
                        order_type = s.order_type.order_type_id,
                        type_name = s.order_type.order_type_name
                    })
                    .Where(l =>
                    l.emp_name.Contains(KeyWords) ||
                    l.order_num.Contains(KeyWords) ||
                    l.type_name.Contains(KeyWords)
                    ).ToList();
                //分页功能
                var logPaging = log
                    .OrderByDescending(s => s.log_id)
                    .Skip(limit * (page - 1))
                    .Take(limit);

                var result = new
                {
                    code = 0,
                    msg = "",
                    count = log.Count(),
                    data = logPaging
                };
                //返回json格式数据
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region 出库管理（待出库/已出库） 版本：2.0

        public ActionResult OutWarehouseMent()
        {
            return View();
        }
        /// <summary>
        /// 页面加载并分页显示视图
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public ActionResult WaitOutWarehouseMent(int page, int limit)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //关闭延迟加载
                db.Configuration.LazyLoadingEnabled = false;
                //多表查询数据，投影
                var model = db.order_model.Include("emp")
                    .Include("product")
                    .Include("order_type")
                    .Include("supplier")
                    .Where(o => o.order_type_id == 3 && o.order_state == false)
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
                        o.product.unit,
                        o.product.product_type.supplier_id
                    })
                    .ToList();

                //实现分页
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
        /// <summary>
        /// 退销售单进行批量出库
        /// </summary>
        /// <param name="OrderID">订单ID集合</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BatchOut_btn(List<int> OrderID)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //获取确认出库员工的用户名并查询到员工ID
                var username = Session["username"].ToString();
                var empInfo = db.emp.Where(e => e.username == username).FirstOrDefault();
                var checkerID = empInfo.emp_id;

                //修改订单表数据，添加审核人，结束时间，出库单号（CK）,修改状态为已审核
                var outin_num = "CK" + DateTime.Now.ToString("yyyyMMddhhmmss");

                foreach (var item in OrderID)
                {
                    order_model model = db.order_model.FirstOrDefault(o => item == o.order_id);
                    model.checker = checkerID;
                    model.end_time = DateTime.Now;
                    model.OutIn_number = outin_num;
                    model.order_state = true;
                    var name=model.product.product_name;

                    //修改商品表库存，出库：减
                    var pro_id = model.product_id;
                    product product = db.product.FirstOrDefault(p => pro_id == p.product_id);

                    if (product.pro_Inventory < model.total_num)
                    {
                        return Json(new
                        {
                            Success = false,
                            Message = name+"库存不足，出库失败"
                        });
                    }

                    product.pro_Inventory -= model.total_num;

                    //为仓库日志表添加数据
                    store_log store_Log = new store_log
                    {
                        emp_id = checkerID,//员工ID
                        order_id = item,//订单ID
                        oper_time = DateTime.Now,//订单时间
                        num = model.total_num,//订单数量
                        order_type_id = model.order_type_id//商品类型
                    };
                    //添加
                    db.store_log.Add(store_Log);
                }
                //保存
                if (db.SaveChanges() > 0)
                {
                    return Json(new
                    {
                        Success = true,
                        Message = "批量出库成功"
                    });
                }
                return Json(new
                {
                    Success = false,
                    Message = "批量出库失败"
                });
            }
        }
        /// <summary>
        /// 等待出库模块通过商品名称、订单号、员工姓名进行模糊查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="KeyWords"></param>
        /// <param name="dateInfo"></param>
        /// <returns></returns>
        public ActionResult WaitOutBlurSelModel(int page, int limit, string KeyWords, string dateInfo)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //关闭延迟加载
                db.Configuration.LazyLoadingEnabled = false;
                if (dateInfo == "")
                {
                    //多表查询数据，投影
                    var model = db.order_model.Include("emp")
                        .Include("product")
                        .Include("order_type")
                        .Include("supplier")
                        .Where(o => o.order_type_id == 3 && o.order_state == false && (o.product.product_name.Contains(KeyWords)|| o.order_num.Contains(KeyWords)||o.emp.emp_name.Contains(KeyWords)))
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
                            o.product.unit,
                            o.product.product_type.supplier_id
                        })
                        .ToList();

                    //实现模糊查询以及分页
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
                    //多表查询数据，投影
                    var model = db.order_model.Include("emp")
                        .Include("product")
                        .Include("order_type")
                        .Include("supplier")
                        .Where(o => 
                        o.order_type_id == 3 && 
                        o.order_state == false &&
                        ((o.create_time >= startTime && o.create_time < endTime)&& (o.product.product_name.Contains(KeyWords) ||
                          o.order_num.Contains(KeyWords) ||
                          o.emp.emp_name.Contains(KeyWords)))
                        )
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
                            o.product.unit,
                            o.product.product_type.supplier_id
                        })
                        .ToList();

                    //实现模糊查询以及分页
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

        /// <summary>
        /// 已出库模块的数据分页显示视图
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public ActionResult HasOutWarehouseMent(int page, int limit)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //关闭延迟加载
                db.Configuration.LazyLoadingEnabled = false;

                //多表查询数据，投影
                var model = db.order_model.Include("emp")
                    .Include("product")
                    .Include("order_type")
                    .Where(o => o.order_type_id == 3 && o.order_state == true)
                    .GroupBy(o => new { o.OutIn_number, o.order_state })
                    .Select(o => new
                    {
                        OutIn_number = o.Key.OutIn_number,
                        order_state = o.Key.order_state,
                        end_time = o.Min(w => w.end_time),
                        total_price = o.Sum(q => q.total_price),
                        total_num = o.Sum(q => q.total_num)
                    })
                    .ToList();

                //实现分页
                var BlurModel = model
                    .OrderByDescending(o => o.end_time)
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

        /// <summary>
        /// 已出库模块通过
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="KeyWord"></param>
        /// <param name="dateInfos"></param>
        /// <returns></returns>
        public ActionResult HasOutBlurSelModel(int page, int limit, string KeyWord, string dateInfos)
        {
            try
            {
                using (ERPEntities db = new ERPEntities())
                {
                    //关闭延迟加载
                    db.Configuration.LazyLoadingEnabled = false;
                    if (dateInfos == "")
                    {
                        //多表查询数据，投影
                        var model = db.order_model.Include("emp")
                            .Include("product")
                            .Include("order_type")
                            .Where(o => o.order_type_id == 3 && o.order_state == true)
                            .GroupBy(o => new { o.OutIn_number, o.order_state })
                            .Select(o => new
                            {
                                OutIn_number = o.Key.OutIn_number,
                                order_state = o.Key.order_state,
                                end_time = o.Min(w => w.end_time),
                                total_price = o.Sum(q => q.total_price),
                                total_num = o.Sum(q => q.total_num)
                            })
                            .ToList();

                        //实现分页
                        var BlurModel = model
                            .OrderByDescending(o => o.end_time)
                            .Where(o =>
                            o.OutIn_number.Contains(KeyWord))
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
                        var startTime = DateTime.Parse(dateInfos);
                        var endTime = DateTime.Parse(dateInfos).AddDays(1);
                        //多表查询数据，投影
                        var model = db.order_model.Include("emp")
                            .Include("product")
                            .Include("order_type")
                            .Where(o => (o.order_type_id == 3 && o.order_state == true))
                            .GroupBy(o => new { o.OutIn_number, o.order_state })
                            .Select(o => new
                            {
                                OutIn_number = o.Key.OutIn_number,
                                order_state = o.Key.order_state,
                                end_time = o.Min(w => w.end_time),
                                total_price = o.Sum(q => q.total_price),
                                total_num = o.Sum(q => q.total_num)
                            })
                            .ToList();

                        //实现分页
                        var BlurModel = model
                            .OrderByDescending(o => o.end_time)
                            .Where(o => o.OutIn_number.Contains(KeyWord) && (o.end_time >= startTime && o.end_time < endTime))
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
            catch (Exception)
            {
                return Json(new
                {
                    Success = false,
                    Message = "查询失败，出入库单号为空！！！"
                });
            }
        }
        /// <summary>
        /// 出库详情界面
        /// </summary>
        /// <param name="OutIn_number"></param>
        /// <returns></returns>
        public ActionResult OutDetilsView(string OutIn_number)
        {
            string checkerName = "";
            using (ERPEntities db = new ERPEntities())
            {
                //通过审核人的ID获取到订单审核人的姓名
                var checkerInfo = db.order_model.Include("emp").Where(o => o.OutIn_number == OutIn_number).FirstOrDefault();
                //判断审核人是否为空
                if (checkerInfo.checker == null)
                {
                    checkerName = "";
                }
                else
                {
                    //如果不为空则通过审核人的ID查询到员工表中对应的审核人姓名
                    var checker = db.emp.Where(e => e.emp_id == checkerInfo.checker).FirstOrDefault();
                    checkerName = checker.emp_name;
                }

                //多表查询数据，投影
                var model = db.order_model.Include("emp")
                    .Include("product")
                    .Include("order_type")
                    .Include("supplier")
                    .Where(o => o.order_type_id == 3 && o.OutIn_number == OutIn_number)//条件:销售单，订单ID
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
                        order_state = o.order_state,
                        o.total_num,
                        o.total_price,
                        o.supplier.supplier_name,
                        o.order_type_id,
                        o.product.product_name,
                        o.product.in_price,
                        o.product.unit,
                        o.product.product_type.supplier_id
                    })
                    .FirstOrDefault();

                ViewBag.Out_num = model.OutIn_number;
                ViewBag.checker = checkerName;
                ViewBag.end_time = model.end_time;
                ViewBag.state = (bool)model.order_state ? "已出库" : "等待出库";

                var Model = db.order_model.Include("emp")
                    .Include("product")
                    .Where(o => o.order_type_id == 3 && o.OutIn_number == OutIn_number)
                    .ToList();
                ViewBag.total_num = Model.Sum(o => o.total_num);
                ViewBag.total_price = Model.Sum(o => o.total_price);
                return View(Model);
            }
        }

        #endregion


        #region 入库管理（待入库/已入库）  版本：2.0

        public ActionResult InWarehouseMent()
        {
            return View();
        }
        public ActionResult WaitInWarehouseMent(int page, int limit)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //关闭延迟加载
                db.Configuration.LazyLoadingEnabled = false;

                //多表查询数据，投影
                var model = db.order_model.Include("emp")
                    .Include("product")
                    .Include("order_type")
                    .Include("supplier")
                    .Where(o => o.order_type_id == 2 && o.order_state == false)
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
                        o.product.unit,
                        o.product.product_type.supplier_id
                    })
                    .ToList();

                //实现分页
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

        [HttpPost]
        public ActionResult BatchIn_btn(List<int> OrderID)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //获取确认出库员工的用户名并查询到员工ID
                var username = Session["username"].ToString();
                var empInfo = db.emp.Where(e => e.username == username).FirstOrDefault();
                var checkerID = empInfo.emp_id;

                //修改订单表数据，添加审核人，结束时间，出库单号（CK）,修改状态为已审核
                var outin_num = "RK" + DateTime.Now.ToString("yyyyMMddhhmmss");

                foreach (var item in OrderID)
                {
                    order_model model = db.order_model.FirstOrDefault(o => item == o.order_id);
                    model.checker = checkerID;
                    model.end_time = DateTime.Now;
                    model.OutIn_number = outin_num;
                    model.order_state = true;

                    //修改商品表库存，出库：减
                    var pro_id = model.product_id;
                    product product = db.product.FirstOrDefault(p => pro_id == p.product_id);
                    product.pro_Inventory += model.total_num;

                    //为仓库日志表添加数据
                    store_log store_Log = new store_log
                    {
                        emp_id = checkerID,//员工ID
                        order_id = item,//订单ID
                        oper_time = DateTime.Now,//订单时间
                        num = model.total_num,//订单数量
                        order_type_id = model.order_type_id//订单类型
                    };
                    //添加
                    db.store_log.Add(store_Log);
                }

                //保存
                if (db.SaveChanges() > 0)
                {
                    return Json(new
                    {
                        Success = true,
                        Message = "批量入库成功"
                    });
                }
                return Json(new
                {
                    Success = false,
                    Message = "批量入库失败"
                });
            }
        }

        public ActionResult WaitInBlurSelModel(int page, int limit, string KeyWords,string dateInfo)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //关闭延迟加载
                db.Configuration.LazyLoadingEnabled = false;
                if (dateInfo=="")
                {
                    //多表查询数据，投影
                    var model = db.order_model.Include("emp")
                        .Include("product")
                        .Include("order_type")
                        .Where(o => o.order_type_id == 2 && o.order_state == false && (o.product.product_name.Contains(KeyWords) || o.order_num.Contains(KeyWords) || o.emp.emp_name.Contains(KeyWords)))
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
                            o.product.unit,
                            o.product.product_type.supplier_id
                        })
                        .ToList();

                    //实现模糊查询以及分页
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
                    //多表查询数据，投影
                    var model = db.order_model.Include("emp")
                        .Include("product")
                        .Include("order_type")
                        .Where(o =>
                        o.order_type_id == 2 &&
                        o.order_state == false &&
                        ((o.create_time >= startTime && o.create_time < endTime) && (o.product.product_name.Contains(KeyWords) ||
                          o.order_num.Contains(KeyWords) ||
                          o.emp.emp_name.Contains(KeyWords)))
                        )
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
                            o.product.unit,
                            o.product.product_type.supplier_id
                        })
                        .ToList();

                    //实现模糊查询以及分页
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


        public ActionResult HasInWarehouseMent(int page, int limit)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //关闭延迟加载
                db.Configuration.LazyLoadingEnabled = false;

                //多表查询数据，投影
                var model = db.order_model.Include("emp")
                    .Include("product")
                    .Include("order_type")
                    .Where(o => o.order_type_id == 2 && o.order_state == true)
                    .GroupBy(o => new { o.OutIn_number, o.order_state })
                    .Select(o => new
                    {
                        OutIn_number = o.Key.OutIn_number,
                        order_state = o.Key.order_state,
                        end_time = o.Min(w => w.end_time),
                        total_price = o.Sum(q => q.total_price),
                        total_num = o.Sum(q => q.total_num)
                    })
                    .ToList();

                //实现分页
                var BlurModel = model
                    .OrderByDescending(o => o.end_time)
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


        public ActionResult HasInBlurSelModel(int page, int limit, string KeyWords,string dateInfo)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //关闭延迟加载
                db.Configuration.LazyLoadingEnabled = false;
                if (dateInfo=="")
                {
                    //多表查询数据，投影
                    var model = db.order_model.Include("emp")
                        .Include("product")
                        .Include("order_type")
                        .Where(o => o.order_type_id == 2 && o.order_state == true)
                        .GroupBy(o => new { o.OutIn_number, o.order_state })
                        .Select(o => new
                        {
                            OutIn_number = o.Key.OutIn_number,
                            order_state = o.Key.order_state,
                            end_time = o.Min(w => w.end_time),
                            total_price = o.Sum(q => q.total_price),
                            total_num = o.Sum(q => q.total_num)
                        })
                        .ToList();

                    //实现分页
                    var BlurModel = model
                        .OrderByDescending(o => o.end_time)
                        .Where(o => o
                        .OutIn_number.Contains(KeyWords))
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
                    //多表查询数据，投影
                    var model = db.order_model.Include("emp")
                        .Include("product")
                        .Include("order_type")
                        .Where(o => o.order_type_id == 2 && o.order_state == true && ((o.end_time >= startTime && o.end_time < endTime)&& o.OutIn_number.Contains(KeyWords)))
                        .GroupBy(o => new { o.OutIn_number, o.order_state })
                        .Select(o => new
                        {
                            OutIn_number = o.Key.OutIn_number,
                            order_state = o.Key.order_state,
                            end_time = o.Min(w => w.end_time),
                            total_price = o.Sum(q => q.total_price),
                            total_num = o.Sum(q => q.total_num)
                        })
                        .ToList();

                    //实现分页
                    var BlurModel = model
                        .OrderByDescending(o => o.end_time)
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

        public ActionResult InDetilsView(string OutIn_number, string total_num)
        {
            string checkerName = "";
            using (ERPEntities db = new ERPEntities())
            {
                //通过审核人的ID获取到订单审核人的姓名
                var checkerInfo = db.order_model.Include("emp").Where(o => o.OutIn_number == OutIn_number).FirstOrDefault();
                //判断审核人是否为空
                if (checkerInfo.checker == null)
                {
                    checkerName = "";
                }
                else
                {
                    //如果不为空则通过审核人的ID查询到员工表中对应的审核人姓名
                    var checker = db.emp.Where(e => e.emp_id == checkerInfo.checker).FirstOrDefault();
                    checkerName = checker.emp_name;
                }

                //多表查询数据，投影
                var model = db.order_model.Include("emp")
                    .Include("product")
                    .Include("order_type")
                    .Include("supplier")
                    .Where(o => o.order_type_id == 2 && o.OutIn_number == OutIn_number)//条件:销售单，订单ID
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
                        order_state = o.order_state,
                        o.total_num,
                        o.total_price,
                        o.supplier.supplier_name,
                        o.order_type_id,
                        o.product.product_name,
                        o.product.in_price,
                        o.product.unit,
                        o.product.product_type.supplier_id
                    })
                    .FirstOrDefault();

                ViewBag.In_num = model.OutIn_number;
                ViewBag.checker = checkerName;
                ViewBag.end_time = model.end_time;
                ViewBag.state = (bool)model.order_state ? "已入库" : "等待入库";

                var Model = db.order_model.Include("emp")
                    .Include("product")
                    .Where(o => o.order_type_id == 2 && o.OutIn_number == OutIn_number)
                    .ToList();
                ViewBag.total_num = Model.Sum(o => o.total_num);
                ViewBag.total_price = Model.Sum(o => o.total_price);
                return View(Model);
            }
        }
        #endregion

    }
}