using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using Youfan_Invoicing_Management_System.Json;
using Youfan_Invoicing_Management_System.Models;

namespace Youfan_Invoicing_Management_System.Controllers
{
    [Authorize]
    public class Purchase_ManagementController : Controller
    {
        // GET: Procurement_Management
        #region 采购员模块
        /// <summary>
        /// 页面显示
        /// </summary>
        /// <returns></returns>
        public ActionResult Purchase_Index()
        {
            return View();
        }
        /// <summary>
        /// 采购员页面初始化数据分页加载
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="limit">每页显示的条目数</param>
        /// <returns></returns>
        public ActionResult Procurement(int page, int limit)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //关闭EF的默认加载方式
                db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;
                //获取到登录的用户名
                //var userName = Session["username"].ToString();
                //查询当前用户信息并获取到员工的ID
                //var EmpAll = db.emp.Where(e => e.username == userName).FirstOrDefault();
                //var EmpID = EmpAll.emp_id;
                //获取到采购单的类型ID
                var order_type = db.order_type.Where(ot => ot.order_type_name == "采购单").SingleOrDefault();
                var Order_Type_ID = order_type.order_type_id;
                var purchases = db.order_model.Include("emp").Include("product").Include("supplier")
                                .Where(o => o.order_type_id == Order_Type_ID/* && o.emp.emp_id == EmpID*/)
                                .Select(o => new
                                {
                                    Order_ID = o.order_id,
                                    Order_Num = o.order_num,
                                    Create_Time = o.create_time,
                                    Creater = o.emp.emp_name,
                                    Product_Name = o.product.product_name,
                                    Order_Type_Name = o.order_type.order_type_name,
                                    Order_State = o.order_state,
                                    Total_Price = o.total_price,
                                    Supplier_Name = o.supplier.supplier_name,
                                    OutIn_Number = o.OutIn_number,
                                    Total_Num = o.total_num,
                                })
                                .ToList();
                var PurchasesModel = purchases.OrderByDescending(o => o.Order_ID).Skip(limit * (page - 1)).Take(limit);
                var result = new
                {
                    code = 0,
                    msg = "",
                    count = purchases.Count(),
                    data = PurchasesModel
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 采购员通过商品名称进行模糊查询并分页显示数据
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="limit">每页显示的条目数</param>
        /// <param name="KeyWords">商品名称</param>
        /// <returns></returns>
        public ActionResult Search_Purchase(int page, int limit, string KeyWords, string dateInfo)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //关闭EF的默认加载方式
                db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;
                if (dateInfo == "")
                {
                    //获取到采购单的类型ID
                    var order_type = db.order_type.Where(ot => ot.order_type_name == "采购单").SingleOrDefault();
                    var Order_Type_ID = order_type.order_type_id;
                    var purchases = db.order_model.Include("emp").Include("product").Include("supplier")
                                    .Where(o =>
                                    o.order_type_id == Order_Type_ID &&
                                    (o.order_num.Contains(KeyWords) ||
                                    o.product.product_name.Contains(KeyWords) ||
                                    o.emp.emp_name.Contains(KeyWords))
                                    )
                                    .Select(o => new
                                    {
                                        Order_ID = o.order_id,
                                        Order_Num = o.order_num,
                                        Create_Time = o.create_time,
                                        Creater = o.emp.emp_name,
                                        Product_Name = o.product.product_name,
                                        Order_Type_Name = o.order_type.order_type_name,
                                        Order_State = o.order_state,
                                        Total_Price = o.total_price,
                                        Supplier_Name = o.supplier.supplier_name,
                                        OutIn_Number = o.OutIn_number,
                                        Total_Num = o.total_num,
                                    })
                                    .ToList();
                    //分页查询的方法语法
                    var PurchasesModel = purchases.OrderByDescending(o => o.Order_ID).Skip(limit * (page - 1)).Take(limit);
                    var result = new
                    {
                        code = 0,
                        msg = "",
                        count = purchases.Count(),
                        data = PurchasesModel
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //获取到采购单的类型ID
                    var order_type = db.order_type.Where(ot => ot.order_type_name == "采购单").SingleOrDefault();
                    var Order_Type_ID = order_type.order_type_id;
                    ////声明变量存储时间
                    var startTime = DateTime.Parse(dateInfo);
                    var endTime = DateTime.Parse(dateInfo).AddDays(1);
                    var purchases = db.order_model.Include("emp").Include("product").Include("supplier")
                                    .Where(o =>
                                    o.order_type_id == Order_Type_ID &&
                                    ((o.order_num.Contains(KeyWords) ||
                                    o.product.product_name.Contains(KeyWords) ||
                                    o.emp.emp_name.Contains(KeyWords) ||
                            o.order_num.Contains(KeyWords)) && (o.create_time >= startTime && o.create_time < endTime))
                                    )
                                    .Select(o => new
                                    {
                                        Order_ID = o.order_id,
                                        Order_Num = o.order_num,
                                        Create_Time = o.create_time,
                                        Creater = o.emp.emp_name,
                                        Product_Name = o.product.product_name,
                                        Order_Type_Name = o.order_type.order_type_name,
                                        Order_State = o.order_state,
                                        Total_Price = o.total_price,
                                        Supplier_Name = o.supplier.supplier_name,
                                        OutIn_Number = o.OutIn_number,
                                        Total_Num = o.total_num,
                                    })
                                    .ToList();
                    //分页查询的方法语法
                    var PurchasesModel = purchases.OrderByDescending(o => o.Order_ID).Skip(limit * (page - 1)).Take(limit);
                    var result = new
                    {
                        code = 0,
                        msg = "",
                        count = purchases.Count(),
                        data = PurchasesModel
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
        }

        /// <summary>
        /// 采购员采购单详情页面
        /// </summary>
        /// <param name="order_id">采购单ID</param>
        /// <returns></returns>
        public ActionResult Purchase_Detail(int? order_id)
        {
            string checkerName = "";
            using (ERPEntities db = new ERPEntities())
            {
                //获取到登录的用户名
                //var userName = Session["username"].ToString();
                //查询当前用户信息并获取到员工的ID
                //var EmpAll = db.emp.Where(e => e.username == userName).FirstOrDefault();
                //var EmpID = EmpAll.emp_id;
                //通过审核人的ID获取到订单审核人的姓名
                var checkerInfo = db.order_model.Include("emp").Where(o => o.order_id == order_id).FirstOrDefault();
                //判断审核人是否为空
                if (checkerInfo.checker == null)
                {
                    checkerName = "";
                }
                else
                {
                    checkerName = checkerInfo.emp.emp_name;
                }
                //获取到采购单的类型ID
                var order_type = db.order_type.Where(ot => ot.order_type_name == "采购单").SingleOrDefault();
                var Order_Type_ID = order_type.order_type_id;
                var purchases = db.order_model.Include("emp").Include("product").Include("supplier")
                                .Where(o => o.order_type_id == Order_Type_ID && o.order_id == order_id)
                                .Select(o => new
                                {
                                    Order_ID = o.order_id,
                                    Order_Num = o.order_num,
                                    Create_Time = o.create_time,
                                    Creater = o.emp.emp_name,
                                    Product_Name = o.product.product_name,
                                    Order_Type_Name = o.order_type.order_type_name,
                                    Order_State = o.order_state,
                                    Total_Price = o.total_price,
                                    Supplier_Name = o.supplier.supplier_name,
                                    OutIn_Number = o.OutIn_number,
                                    Total_Num = o.total_num,
                                    Checker = checkerName,
                                    Contact = o.supplier.contact,
                                    Supplier_ID = o.supplier.supplier_id,
                                    Product_ID = o.product_id,
                                    Unit_Price = o.product.in_price,
                                    Unit = o.product.unit
                                })
                                .SingleOrDefault();
                ViewBag.Order_ID = purchases.Order_Num;//订单ID
                ViewBag.OutIn_Number = purchases.OutIn_Number;//出入库订单号
                ViewBag.Order_Stat = purchases.Order_State;
                ViewBag.Order_State = ViewBag.Order_Stat ? "审核通过" : "等待审核";//订单状态
                ViewBag.Creater = purchases.Creater;//订单创建人
                ViewBag.Create_Time = purchases.Create_Time;//订单创建时间
                ViewBag.Checker = purchases.Checker;//订单审核人
                ViewBag.Supplier_ID = purchases.Supplier_ID;//供应商ID
                ViewBag.Supplier_Name = purchases.Supplier_Name;//供应商名称
                ViewBag.Contact = purchases.Contact;//供应商联系人
                ViewBag.Product_Name = purchases.Product_Name;//商品名称
                ViewBag.Product_ID = purchases.Product_ID;//商品ID
                ViewBag.Unit_Price = purchases.Unit_Price;//商品单价
                ViewBag.Total_Price = purchases.Total_Price;//总价格
                ViewBag.Total_Num = purchases.Total_Num;//出入库数
                ViewBag.Unit = purchases.Unit;//商品规格
            }
            return View();
        }

        /// <summary>
        /// 显示采购员进行采购的视图页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Make_A_Purchase(int? order_id)
        {
            string checkerName = "";
            using (ERPEntities db = new ERPEntities())
            {
                //获取到登录的用户名
                //var userName = Session["username"].ToString();
                //查询当前用户信息并获取到员工的ID
                //var EmpAll = db.emp.Where(e => e.username == userName).FirstOrDefault();
                //var EmpID = EmpAll.emp_id;
                //通过审核人的ID获取到订单审核人的姓名
                var checkerInfo = db.order_model.Include("emp").Where(o => o.order_id == order_id).FirstOrDefault();
                //判断审核人是否为空
                if (checkerInfo.checker == null)
                {
                    checkerName = "";
                }
                else
                {
                    checkerName = checkerInfo.emp.emp_name;
                }
                //获取到采购单的类型ID
                var order_type = db.order_type.Where(ot => ot.order_type_name == "采购单").SingleOrDefault();
                var Order_Type_ID = order_type.order_type_id;
                var purchases = db.order_model.Include("emp").Include("product").Include("supplier")
                                .Where(o => o.order_type_id == Order_Type_ID && o.order_id == order_id)
                                .Select(o => new
                                {
                                    Order_ID = o.order_id,
                                    Order_Num = o.order_num,
                                    Create_Time = o.create_time,
                                    Creater = o.emp.emp_name,
                                    Product_Name = o.product.product_name,
                                    Order_Type_Name = o.order_type.order_type_name,
                                    Order_State = o.order_state,
                                    Total_Price = o.total_price,
                                    Supplier_Name = o.supplier.supplier_name,
                                    OutIn_Number = o.OutIn_number,
                                    Total_Num = o.total_num,
                                    Checker = checkerName,
                                    Contact = o.supplier.contact,
                                    Supplier_ID = o.supplier.supplier_id,
                                    Product_ID = o.product_id,
                                    Unit_Price = o.product.in_price,
                                    Unit = o.product.unit
                                })
                                .SingleOrDefault();
                ViewBag.Order_ID = purchases.Order_Num;//订单ID
                ViewBag.OutIn_Number = purchases.OutIn_Number;//出入库订单号
                ViewBag.Order_Stat = purchases.Order_State;
                ViewBag.Order_State = ViewBag.Order_Stat ? "审核通过" : "等待审核";//订单状态
                ViewBag.Creater = purchases.Creater;//订单创建人
                ViewBag.Create_Time = purchases.Create_Time;//订单创建时间
                ViewBag.Checker = purchases.Checker;//订单审核人
                ViewBag.Supplier_ID = purchases.Supplier_ID;//供应商ID
                ViewBag.Supplier_Name = purchases.Supplier_Name;//供应商名称
                ViewBag.Contact = purchases.Contact;//供应商联系人
                ViewBag.Product_Name = purchases.Product_Name;//商品名称
                ViewBag.Product_ID = purchases.Product_ID;//商品ID
                ViewBag.Unit_Price = purchases.Unit_Price;//商品单价
                ViewBag.Total_Price = purchases.Total_Price;//总价格
                ViewBag.Total_Num = purchases.Total_Num;//出入库数
                ViewBag.Unit = purchases.Unit;//商品规格
            }
            return View();
        }

        public ActionResult Make_Purchase(int? Order_ID)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //通过采购员的员工用户名查询到该员工的员工ID
                var username = Session["username"].ToString();
                var empInfo = db.emp.Where(e => e.username == username).FirstOrDefault();
                var createrID = empInfo.emp_id;
                //通过页面中的订单ID找到该条数据，并将数据更新到order_model表
                order_model UpdateOrderModel = db.order_model.FirstOrDefault(od => od.order_id == Order_ID);
                //自动生成采购单单号
                UpdateOrderModel.order_num = "CG" + DateTime.Now.ToString("yyyyMMddhhmmss");
                //添加创建人ID
                UpdateOrderModel.creater = createrID;
                //添加创建时间
                UpdateOrderModel.create_time = DateTime.Now;
                if (db.SaveChanges() > 0)
                {
                    return Json(new
                    {
                        Success = true,
                        Message = "商品采购成功！！！"
                    });
                }
                return Json(new
                {
                    Success = false,
                    Message = "商品采购失败！！！"
                });
            }
        }


        #endregion

        #region 采购经理模块
        /// <summary>
        /// 采购经理管理模块的视图页面显示
        /// </summary>
        /// <returns></returns>
        public ActionResult Purchase_Manager_Index()
        {
            return View();
        }

        /// <summary>
        /// 采购经理的采购单管理页面初始化数据分页加载
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="limit">每页显示的条目数</param>
        /// <returns></returns>
        public ActionResult Purchase_Orders_Management(int page, int limit)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //关闭EF的默认加载方式
                db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;
                var order_type = db.order_type.Where(ot => ot.order_type_name == "采购单").SingleOrDefault();
                var Order_Type_ID = order_type.order_type_id;
                var purchases = db.order_model.Include("emp").Include("product").Include("supplier")
                                .Where(o => o.order_type_id == Order_Type_ID)
                                .Select(o => new
                                {
                                    Order_ID = o.order_id,
                                    Order_Num = o.order_num,
                                    Create_Time = o.create_time,
                                    Creater = o.emp.emp_name,
                                    Product_Name = o.product.product_name,
                                    Order_Type_Name = o.order_type.order_type_name,
                                    Order_State = o.order_state,
                                    Total_Price = o.total_price,
                                    Supplier_Name = o.supplier.supplier_name,
                                    OutIn_Number = o.OutIn_number,
                                    Total_Num = o.total_num,
                                })
                                .ToList();
                //分页代码
                var PurchasesModel = purchases.OrderByDescending(o => o.Order_ID).Skip(limit * (page - 1)).Take(limit);
                var result = new
                {
                    code = 0,
                    msg = "",
                    count = purchases.Count(),
                    data = PurchasesModel
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 采购经理采购单详情页面
        /// </summary>
        /// <param name="order_id">采购单ID</param>
        /// <returns></returns>
        public ActionResult Purchase_Orders_Details(int? order_id)
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
                    checkerName = checkerInfo.emp.emp_name;
                }
                //获取到采购单的类型ID
                var order_type = db.order_type.Where(ot => ot.order_type_name == "采购单").SingleOrDefault();
                var Order_Type_ID = order_type.order_type_id;
                var purchases = db.order_model.Include("emp").Include("product").Include("supplier")
                                .Where(o => o.order_type_id == Order_Type_ID && o.order_id == order_id)
                                .Select(o => new
                                {
                                    Order_ID = o.order_id,
                                    Order_Num = o.order_num,
                                    Create_Time = o.create_time,
                                    Creater = o.emp.emp_name,
                                    Product_Name = o.product.product_name,
                                    Order_Type_Name = o.order_type.order_type_name,
                                    Order_State = o.order_state,
                                    Total_Price = o.total_price,
                                    Supplier_Name = o.supplier.supplier_name,
                                    OutIn_Number = o.OutIn_number,
                                    Total_Num = o.total_num,
                                    Checker = checkerName,
                                    Contact = o.supplier.contact,
                                    Supplier_ID = o.supplier.supplier_id,
                                    Product_ID = o.product_id,
                                    Unit_Price = o.product.in_price,
                                    Unit = o.product.unit
                                })
                                .SingleOrDefault();
                ViewBag.Order_ID = purchases.Order_Num;//订单ID
                ViewBag.OutIn_Number = purchases.OutIn_Number;//出入库订单号
                ViewBag.Order_Stat = purchases.Order_State;
                ViewBag.Order_State = ViewBag.Order_Stat ? "已审核" : "未审核";//订单状态
                ViewBag.Creater = purchases.Creater;//订单创建人
                ViewBag.Create_Time = purchases.Create_Time;//订单创建时间
                ViewBag.Checker = purchases.Checker;//订单审核人
                ViewBag.Supplier_ID = purchases.Supplier_ID;//供应商ID
                ViewBag.Supplier_Name = purchases.Supplier_Name;//供应商名称
                ViewBag.Contact = purchases.Contact;//供应商联系人
                ViewBag.Product_Name = purchases.Product_Name;//商品名称
                ViewBag.Product_ID = purchases.Product_ID;//商品ID
                ViewBag.Unit_Price = purchases.Unit_Price;//商品单价
                ViewBag.Total_Price = purchases.Total_Price;//总价格
                ViewBag.Total_Num = purchases.Total_Num;//出入库数
                ViewBag.Unit = purchases.Unit;//商品规格
            }
            return View();
        }

        /// <summary>
        /// 采购经理的通过采购单号进行模糊查询并分页显示数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="KeyWords">采购单号</param>
        /// <returns></returns>
        public ActionResult Seach_KeyWords_Manag(int page, int limit, string KeyWords, string dateInfo)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //关闭EF的默认加载方式
                db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;
                if (dateInfo == "")
                {
                    //获取到采购单的类型ID
                    var order_type = db.order_type.Where(ot => ot.order_type_name == "采购单").SingleOrDefault();
                    var Order_Type_ID = order_type.order_type_id;
                    var purchases = db.order_model.Include("emp").Include("product").Include("supplier")
                                    .Where(o =>
                                    o.order_type_id == Order_Type_ID &&
                                    (o.order_num.Contains(KeyWords) ||
                                    o.product.product_name.Contains(KeyWords) ||
                                    o.emp.emp_name.Contains(KeyWords))
                                    )
                                    .Select(o => new
                                    {
                                        Order_ID = o.order_id,
                                        Order_Num = o.order_num,
                                        Create_Time = o.create_time,
                                        Creater = o.emp.emp_name,
                                        Product_Name = o.product.product_name,
                                        Order_Type_Name = o.order_type.order_type_name,
                                        Order_State = o.order_state,
                                        Total_Price = o.total_price,
                                        Supplier_Name = o.supplier.supplier_name,
                                        OutIn_Number = o.OutIn_number,
                                        Total_Num = o.total_num,
                                    })
                                    .ToList();
                    //分页查询的方法语法
                    var PurchasesModel = purchases.OrderByDescending(o => o.Order_ID).Skip(limit * (page - 1)).Take(limit);
                    var result = new
                    {
                        code = 0,
                        msg = "",
                        count = purchases.Count(),
                        data = PurchasesModel
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    //获取到采购单的类型ID
                    var order_type = db.order_type.Where(ot => ot.order_type_name == "采购单").SingleOrDefault();
                    var Order_Type_ID = order_type.order_type_id;
                    ////声明变量存储时间
                    var startTime = DateTime.Parse(dateInfo);
                    var endTime = DateTime.Parse(dateInfo).AddDays(1);
                    var purchases = db.order_model.Include("emp").Include("product").Include("supplier")
                            .Where(o =>
                            o.order_type_id == Order_Type_ID &&
                            (((o.order_num.Contains(KeyWords) ||
                            o.product.product_name.Contains(KeyWords) ||
                            o.emp.emp_name.Contains(KeyWords)) ||
                            o.order_num.Contains(KeyWords)) && (o.create_time >= startTime && o.create_time < endTime))
                            )
                            .Select(o => new
                            {
                                Order_ID = o.order_id,
                                Order_Num = o.order_num,
                                Create_Time = o.create_time,
                                Creater = o.emp.emp_name,
                                Product_Name = o.product.product_name,
                                Order_Type_Name = o.order_type.order_type_name,
                                Order_State = o.order_state,
                                Total_Price = o.total_price,
                                Supplier_Name = o.supplier.supplier_name,
                                OutIn_Number = o.OutIn_number,
                                Total_Num = o.total_num,
                            })
                            .ToList();
                    //分页查询的方法语法
                    var PurchasesModel = purchases.OrderByDescending(o => o.Order_ID).Skip(limit * (page - 1)).Take(limit);
                    var result = new
                    {
                        code = 0,
                        msg = "",
                        count = purchases.Count(),
                        data = PurchasesModel
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
        }

        /// <summary>
        /// 采购单批量删除的操作方法
        /// </summary>
        /// <param name="Purchase_Orders">页面数据表格选中的采购单ID集合</param>
        /// <returns></returns>
        public ActionResult BatchDel_Purchase_Orders(List<int> Purchase_Orders)
        {
            using (ERPEntities db = new ERPEntities())
            {
                var delPurchase_OrdersQuery = db.order_model.Where(d => Purchase_Orders.Contains(d.order_id));
                db.order_model.RemoveRange(delPurchase_OrdersQuery);
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

        /// <summary>
        /// 采购单单行删除数据
        /// </summary>
        /// <param name="Order_ID">页面数据表格选中的采购单ID</param>
        /// <returns></returns>
        public ActionResult Del_Purchase_Orders(int? Order_ID)
        {
            try
            {
                using (ERPEntities db = new ERPEntities())
                {
                    var Purchase_Orders_Del = db.order_model.FirstOrDefault(u => u.order_id == Order_ID);
                    db.order_model.Remove(Purchase_Orders_Del);
                    if (db.SaveChanges() > 0)
                    {
                        return Json(new
                        {
                            Success = true,
                            Message = "删除成功"
                        });
                    }

                    return Json(new
                    {
                        Success = false,
                        Message = "删除失败"
                    });
                }

            }
            catch (Exception)
            {
                return Json(new
                {
                    Success = false,
                    Message = "删除失败"
                });
            }
        }

        /// <summary>
        /// 采购经理的补货单管理视图界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Replenishment_Orders_Index()
        {
            return View();
        }

        /// <summary>
        /// 采购经理补货单管理视图页面分页显示
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="limit">每页显示的条目数</param>
        /// <returns></returns>
        public ActionResult Replenishment_Orders_Management(int page, int limit)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //关闭EF的默认加载方式
                db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;
                var order_type = db.order_type.Where(ot => ot.order_type_name == "采购申请单").SingleOrDefault();
                var Order_Type_ID = order_type.order_type_id;
                var purchases = db.order_model.Include("emp").Include("product").Include("supplier")
                                .Where(o => o.order_type_id == Order_Type_ID)
                                .Select(o => new
                                {
                                    Order_ID = o.order_id,
                                    Order_Num = o.order_num,
                                    Create_Time = o.create_time,
                                    Creater = o.emp.emp_name,
                                    Product_Name = o.product.product_name,
                                    Order_Type_Name = o.order_type.order_type_name,
                                    Order_State = o.order_state,
                                    Total_Price = o.total_price,
                                    Unit = o.product.unit,
                                    Supplier_Name = o.supplier.supplier_name,
                                    OutIn_Number = o.OutIn_number,
                                    Total_Num = o.total_num,
                                })
                                .ToList();
                var PurchasesModel = purchases.OrderByDescending(o => o.Order_ID).Skip(limit * (page - 1)).Take(limit);
                var result = new
                {
                    code = 0,
                    msg = "",
                    count = purchases.Count(),
                    data = PurchasesModel
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 采购经理采购申请单（补货单）的详情页面
        /// </summary>
        /// <param name="order_id">采购申请单（补货单）ID</param>
        /// <returns></returns>
        public ActionResult Replenishment_Orders_Details(int? order_id)
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
                //获取到采购单的类型ID
                var order_type = db.order_type.Where(ot => ot.order_type_name == "采购申请单").SingleOrDefault();
                var Order_Type_ID = order_type.order_type_id;
                //通过订单类型以及订单号查询信息
                var purchases = db.order_model.Include("emp").Include("product").Include("supplier")
                                .Where(o =>
                                o.order_type_id == Order_Type_ID &&
                                o.order_id == order_id
                                )
                                .Select(o => new
                                {
                                    //查询的字段投影
                                    Order_ID = o.order_id,
                                    Order_Num = o.order_num,
                                    Create_Time = o.create_time,
                                    Creater = o.emp.emp_name,
                                    Product_Name = o.product.product_name,
                                    Order_Type_Name = o.order_type.order_type_name,
                                    Order_State = o.order_state,
                                    Total_Price = o.total_price,
                                    Supplier_Name = o.supplier.supplier_name,
                                    OutIn_Number = o.OutIn_number,
                                    Total_Num = o.total_num,
                                    Checker = checkerName,
                                    Contact = o.supplier.contact,
                                    Supplier_ID = o.supplier.supplier_id,
                                    Product_ID = o.product_id,
                                    Unit_Price = o.product.in_price,
                                    Unit = o.product.unit
                                }).FirstOrDefault();
                ViewBag.Order_Num = purchases.Order_Num;//订单ID
                ViewBag.OutIn_Number = purchases.OutIn_Number;//出入库订单号
                ViewBag.Order_Stat = purchases.Order_State;
                ViewBag.Order_State = ViewBag.Order_Stat ? "审核通过" : "等待审核";//订单状态
                ViewBag.Creater = purchases.Creater;//订单创建人
                ViewBag.Create_Time = purchases.Create_Time;//订单创建时间
                ViewBag.Checker = purchases.Checker;//订单审核人
                ViewBag.Supplier_ID = purchases.Supplier_ID;//供应商ID
                ViewBag.Supplier_Name = purchases.Supplier_Name;//供应商名称
                ViewBag.Contact = purchases.Contact;//供应商联系人
                ViewBag.Product_Name = purchases.Product_Name;//商品名称
                ViewBag.Product_ID = purchases.Product_ID;//商品ID
                ViewBag.Unit_Price = purchases.Unit_Price;//商品单价
                ViewBag.Total_Price = purchases.Total_Price;//总价格
                ViewBag.Total_Num = purchases.Total_Num;//出入库数
                ViewBag.Unit = purchases.Unit;//商品规格
            }
            return View();
        }

        /// <summary>
        /// 采购经理采购申请单的模糊查询并分页显示
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="KeyWords"></param>
        /// <returns></returns>
        public ActionResult Search_Replenishment(int page, int limit, string KeyWords, string dateInfo)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //关闭EF的默认加载方式
                db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;
                if (dateInfo == "")
                {
                    var order_type = db.order_type.Where(ot => ot.order_type_name == "采购申请单").SingleOrDefault();
                    var Order_Type_ID = order_type.order_type_id;
                    var purchases = db.order_model.Include("emp").Include("product").Include("supplier")
                                    .Where(o => o.order_type_id == Order_Type_ID &&
                                (o.order_num.Contains(KeyWords) ||
                                o.product.product_name.Contains(KeyWords) ||
                                o.emp.emp_name.Contains(KeyWords)))
                                    .Select(o => new
                                    {
                                        Order_ID = o.order_id,
                                        Order_Num = o.order_num,
                                        Create_Time = o.create_time,
                                        Creater = o.emp.emp_name,
                                        Product_Name = o.product.product_name,
                                        Order_Type_Name = o.order_type.order_type_name,
                                        Order_State = o.order_state,
                                        Total_Price = o.total_price,
                                        Supplier_Name = o.supplier.supplier_name,
                                        OutIn_Number = o.OutIn_number,
                                        Total_Num = o.total_num,
                                    })
                                    .ToList();
                    var PurchasesModel = purchases.OrderByDescending(o => o.Order_ID).Skip(limit * (page - 1)).Take(limit);
                    var result = new
                    {
                        code = 0,
                        msg = "",
                        count = purchases.Count(),
                        data = PurchasesModel
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var order_type = db.order_type.Where(ot => ot.order_type_name == "采购申请单").SingleOrDefault();
                    var Order_Type_ID = order_type.order_type_id;
                    ////声明变量存储时间
                    var startTime = DateTime.Parse(dateInfo);
                    var endTime = DateTime.Parse(dateInfo).AddDays(1);
                    var purchases = db.order_model.Include("emp").Include("product").Include("supplier")
                                .Where(o =>
                                o.order_type_id == Order_Type_ID &&
                                (((o.order_num.Contains(KeyWords) ||
                                o.product.product_name.Contains(KeyWords) ||
                                o.emp.emp_name.Contains(KeyWords)) ||
                                o.order_num.Contains(KeyWords)) && (o.create_time >= startTime && o.create_time < endTime))
                                )
                                .Select(o => new
                                {
                                    Order_ID = o.order_id,
                                    Order_Num = o.order_num,
                                    Create_Time = o.create_time,
                                    Creater = o.emp.emp_name,
                                    Product_Name = o.product.product_name,
                                    Order_Type_Name = o.order_type.order_type_name,
                                    Order_State = o.order_state,
                                    Total_Price = o.total_price,
                                    Supplier_Name = o.supplier.supplier_name,
                                    OutIn_Number = o.OutIn_number,
                                    Total_Num = o.total_num,
                                })
                                .ToList();
                    var PurchasesModel = purchases.OrderByDescending(o => o.Order_ID).Skip(limit * (page - 1)).Take(limit);
                    var result = new
                    {
                        code = 0,
                        msg = "",
                        count = purchases.Count(),
                        data = PurchasesModel
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
        }

        /// <summary>
        /// 采购经理采购申请单（补货单）的订单审核视图页面
        /// </summary>
        /// <param name="Order_ID">申请单（补货单）的订单ID</param>
        /// <returns></returns>
        public ActionResult Replenishment_Audit(int? Order_ID)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //获取采购经理的员工用户名并查询到员工ID
                var username = Session["username"].ToString();
                var empInfo = db.emp.Where(e => e.username == username).FirstOrDefault();
                var checkerID = empInfo.emp_id;
                //通过补货单ID查询到该条数据
                order_model order_modelInfo = db.order_model.FirstOrDefault(o => o.order_id == Order_ID);
                //将审核人的员工ID更新到订单表中的审核人ID中，并将订单状态修改为审核通过：true
                order_modelInfo.order_state = true;
                order_modelInfo.checker = checkerID;
                order_modelInfo.end_time = DateTime.Now;
                //获取采购单类型ID
                var order_Type_Info = db.order_type.Where(oti => oti.order_type_name == "采购单").FirstOrDefault();
                var order_Type_ID = order_Type_Info.order_type_id;
                //添加一条新的采购单信息
                order_model order_Model = new order_model
                {
                    product_id = order_modelInfo.product_id,
                    total_num = order_modelInfo.total_num,
                    total_price = order_modelInfo.total_price,
                    supplier_id = order_modelInfo.supplier_id,
                    order_state = false,
                    order_type_id = order_Type_ID
                };
                db.order_model.Add(order_Model);
                //判断是否保存成功
                if (db.SaveChanges() > 0)
                {
                    return Json(new
                    {
                        Success = true,
                        Message = "补货单审核成功！！！"
                    });
                }
                return Json(new
                {
                    Success = false,
                    Message = "补货单审核失败！！！"
                });
            }
        }
        #endregion


        #region 供应商管理

        public ActionResult SupAdd()
        {
            return View();
        }

        /// <summary>
        /// 添加商品类型信息
        /// </summary>
        /// <param name="productnames">商品类型集合</param>
        /// <returns></returns>
        public ActionResult Add_product_type(List<String> productnames, string supplier_name)
        {
            using (ERPEntities db = new ERPEntities())
            {
                var supplierinfo = db.supplier.FirstOrDefault(s => s.supplier_name == supplier_name);
                //循环添加商品类型表的数据
                foreach (var item in productnames)
                {
                    product_type Product_Type = new product_type
                    {
                        supplier_id = supplierinfo.supplier_id,
                        product_type_name = item
                    };
                    db.product_type.Add(Product_Type);
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
        /// 添加供应商信息
        /// </summary>
        /// <param name="Supplier">供应商对象</param>
        /// <returns></returns>
        public ActionResult Add_Supplier_Info(supplier Supplier)
        {
            using (ERPEntities db = new ERPEntities())
            {
                var SupplierInfos = db.supplier.FirstOrDefault(s => s.address == Supplier.address && s.supplier_name == Supplier.supplier_name);
                //判断是否该供应商信息已存在
                if (SupplierInfos != null)
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "该供应商信息已存在！！！"
                    });
                }
                else
                {
                    //添加供应商信息
                    supplier AddsupplierInfo = new supplier()
                    {
                        supplier_name = Supplier.supplier_name,
                        address = Supplier.address,
                        contact = Supplier.contact,
                        tel = Supplier.tel
                    };
                    db.supplier.Add(AddsupplierInfo);
                    //判断数据库受影响行数的行数是否大于0
                    if (db.SaveChanges() > 0)
                    {
                        return Json(new
                        {
                            code = 0,
                            Success = true,
                            Message = "供应商信息添加成功！！！"
                        });
                    }
                    return Json(new
                    {
                        Success = false,
                        Message = "供应商信息添加失败！！！"
                    });
                }
            }
        }
        /// <summary>
        /// 供应商管理显示
        /// </summary>
        /// <returns></returns>
        public ActionResult Supplier_Index()
        {
            return View();
        }
        /// <summary>
        /// 显示供应商管理获取数据
        /// </summary>
        /// <returns></returns>
        public ActionResult Get_Supplier_List()
        {
            using (ERPEntities db = new ERPEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var supplier_product = db.supplier.OrderByDescending(s=>s.supplier_id).ToList();

                var result = new
                {
                    msg = "",
                    code = 0,
                    data = supplier_product,
                    tip = "操作成功！"
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Get_Supplier_Type_List()
        {
            using (ERPEntities db = new ERPEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var supplier_product = (from s in db.supplier
                                        join pt in db.product_type on s.supplier_id equals pt.supplier_id
                                        select new
                                        {
                                            product_type_name = pt.product_type_name,
                                            supplier_name = s.supplier_name,
                                            contact = s.contact,
                                        }).ToList();
                var result = new
                {
                    msg = "",
                    code = 0,
                    data = supplier_product,
                    tip = "操作成功！"
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }



        #endregion
    }
}