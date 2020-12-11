using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Youfan_Invoicing_Management_System.Json;
using Youfan_Invoicing_Management_System.Models;

namespace Youfan_Invoicing_Management_System.Controllers
{
    [Authorize]
    public class SalesOrdersController : Controller
    {
        // GET: SalesOrders
        /// <summary>
        /// 页面显示
        /// </summary>
        /// <returns></returns>
        public ActionResult SalesOrders()
        {
            return View();
        }
        /// <summary>
        /// 页面初始化并分页显示加载
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="limit">每页显示的条目数</param>
        /// <returns></returns>
        public ActionResult Index(int page, int limit)
        {
            using (ERPEntities db = new ERPEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;
                //获取到登录的用户名
                var userName = Session["username"].ToString();
                //查询当前用户信息并获取到员工的ID
                var EmpAll = db.emp.Where(e => e.username == userName).FirstOrDefault();
                var EmpID = EmpAll.emp_id;
                //通过该员工的角色ID找到对应的角色名称
                var RoleInfo = db.role.Where(r => r.role_id == EmpAll.role_id).FirstOrDefault();
                var EmpRoleName = RoleInfo.role_name;
                //获取到采购单的类型ID
                var order_type = db.order_type.Where(ot => ot.order_type_name == "销售单").SingleOrDefault();
                var Order_Type_ID = order_type.order_type_id;
                if (EmpRoleName == "系统管理员")
                {
                    var model = db.order_model
                .Include("emp")
                .Include("product")
                .Include("order_type")
                .Include("supplier")
                .Where(o => o.order_type_id == Order_Type_ID/*&&o.emp.emp_id==EmpID*/)
                .Select(o => new
                {
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
                    o.order_id,
                    o.product.product_name
                }).ToList();
                    var BlurModel = model
                        .OrderByDescending(o => o.order_id)
                        .Skip(limit * (page - 1))
                        .Take(limit);//分页的方法语法代码
                    var result = new
                    {
                        code = 0,
                        msg = "",
                        count = model.Count(),
                        data = BlurModel
                    };
                    //数据Json化
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else if (EmpRoleName == "销售经理")
                {
                    var model = db.order_model
                .Include("emp")
                .Include("product")
                .Include("order_type")
                .Include("supplier")
                .Where(o => o.order_type_id == Order_Type_ID/*&&o.emp.emp_id==EmpID*/)
                .Select(o => new
                {
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
                    o.order_id,
                    o.product.product_name
                }).ToList();
                    var BlurModel = model
                        .OrderByDescending(o => o.order_id)
                        .Skip(limit * (page - 1))
                        .Take(limit);//分页的方法语法代码
                    var result = new
                    {
                        code = 0,
                        msg = "",
                        count = model.Count(),
                        data = BlurModel
                    };
                    //数据Json化
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var model = db.order_model
                   .Include("emp")
                   .Include("product")
                   .Include("order_type")
                   .Include("supplier")
                   .Where(o => o.order_type_id == Order_Type_ID && o.emp.emp_id == EmpID)
                   .Select(o => new
                   {
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
                       o.order_id,
                       o.product.product_name
                   }).ToList();
                    var BlurModel = model
                        .OrderByDescending(o => o.order_id)
                        .Skip(limit * (page - 1))
                        .Take(limit);//分页的方法语法代码
                    var result = new
                    {
                        code = 0,
                        msg = "",
                        count = model.Count(),
                        data = BlurModel
                    };
                    //数据Json化
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
        }

        /// <summary>
        /// 添加销售单的显示视图
        /// </summary>
        /// <returns></returns>
        public ActionResult AddOrder()
        {
            using (ERPEntities db = new ERPEntities())
            {
                ViewBag.product_name = new SelectList(db.product.Where(p => p.pro_Inventory >= 10 && p.Shelves == true).ToList(), "product_id", "product_name");
                return View();
            }
        }
        /// <summary>
        /// 添加新的销售单
        /// </summary>
        /// <param name="order_model">页面传过来的对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ADD(order_model order_model)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //接收登录时的用户名
                var username = Session["username"].ToString();
                //查询当前用户id
                var EmpAll = db.emp.Where(e => e.username == username).FirstOrDefault();
                var EmpID = EmpAll.emp_id;
                //获取到销售单的类型ID
                var order_type = db.order_type.Where(ot => ot.order_type_name == "销售单").SingleOrDefault();
                var Order_Type_ID = order_type.order_type_id;
                //自动生成的订单号
                //var order_num = "ddh" + DateTime.Now.ToString("yyyyMMddhhmmss");
                //查询product表ID
                var p_id = db.product.Where(p => p.product_id == order_model.product_id).FirstOrDefault();
                //根据ID查询出商品单价
                var d_price = p_id.in_price;
                //根据ID查询出商品库存
                var p_pro_num = p_id.pro_Inventory;
                //计算出总价格
                var t_price = d_price * order_model.total_num;
                //将数据添加到order_model表
                order_model Addmodel = new order_model()
                {
                    //自动添加单号（ddh=订单号）
                    order_num = "XS" + DateTime.Now.ToString("yyyyMMddhhmmss"),
                    //添加创建人ID
                    creater = EmpID,
                    //添加创建时间
                    create_time = DateTime.Now,
                    //添加商品ID
                    product_id = order_model.product_id,
                    //添加商品数量
                    total_num = order_model.total_num,
                    //添加商品总价
                    total_price = t_price,
                    //添加销售单订单类型ID
                    order_type_id = Order_Type_ID,
                    //添加销售单状态
                    order_state = false
                };
                db.order_model.Add(Addmodel);
                //如果库存数量小于想要销售的数量，则无法销售
                if (p_pro_num < order_model.total_num)
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "库存不足，无法销售！！！"
                    });
                }
                if (db.SaveChanges() > 0)
                {
                    return Json(new
                    {
                        Success = true,
                        Message = "添加成功！！！"
                    });
                }
                return Json(new
                {
                    Success = false,
                    Message = "添加失败！！！"
                });
            }
        }

        /// <summary>
        /// 批量删除销售订单
        /// </summary>
        /// <param name="OrderID">销售订单ID集合</param>
        /// <returns></returns>
        public ActionResult ModelDels(List<int> OrderID)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //批量删除
                var OrderDelQeury = db.order_model.Where(o => OrderID.Contains(o.order_id));
                db.order_model.RemoveRange(OrderDelQeury);
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

        /// <summary>
        /// 单行删除销售单
        /// </summary>
        /// <param name="order_id">销售订单ID</param>
        /// <returns></returns>
        public ActionResult DelUser(int? order_id)
        {
            using (ERPEntities db = new ERPEntities())
            {
                var OrderDel = db.order_model.FirstOrDefault(u => u.order_id == order_id);
                db.order_model.Remove(OrderDel);
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

        /// <summary>
        /// 通过销售订单号或商品名称或创建人模糊查询销售订单信息并分页显示
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="limit">每页显示的条目数</param>
        /// <param name="KeyWords">关键字：销售订单号或商品名称或创建人</param>
        /// <returns></returns>
        public ActionResult BlurSelModel(int page, int limit, string KeyWords, string dateInfo)
        {
            using (ERPEntities db = new ERPEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;//关闭EF默认的加载方式
                //获取到登录的用户名
                var userName = Session["username"].ToString();
                //查询当前用户信息并获取到员工的ID
                var EmpAll = db.emp.Where(e => e.username == userName).FirstOrDefault();
                var EmpID = EmpAll.emp_id;
                //通过该员工的角色ID找到对应的角色名称
                var RoleInfo = db.role.Where(r => r.role_id == EmpAll.role_id).FirstOrDefault();
                var EmpRoleName = RoleInfo.role_name;
                if (EmpRoleName == "系统管理员")
                {
                    if (dateInfo == "")
                    {
                        //获取到采购单的类型ID
                        var order_type = db.order_type.Where(ot => ot.order_type_name == "销售单").SingleOrDefault();
                        var Order_Type_ID = order_type.order_type_id;
                        var model = db.order_model.Include("emp").Include("product").Include("order_type").Include("supplier")
                            .Where(o => o.order_type_id == Order_Type_ID/*&&o.emp.emp_id==EmpID*/)
                            .Select(o => new
                            {
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
                                o.order_id,
                                o.product.product_name
                            })
                            .Where(o => o
                            .order_num.Contains(KeyWords) || o
                            .creater.Contains(KeyWords) || o
                            .product_name.Contains(KeyWords))
                            .ToList();
                        var BlurModel = model.OrderByDescending(o => o.order_id).Skip(limit * (page - 1)).Take(limit);
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
                        //获取到采购单的类型ID
                        var order_type = db.order_type.Where(ot => ot.order_type_name == "销售单").SingleOrDefault();
                        var Order_Type_ID = order_type.order_type_id;
                        var model = db.order_model.Include("emp").Include("product").Include("order_type").Include("supplier")
                            .Where(o => o.order_type_id == Order_Type_ID &&
                                ((o.create_time >= startTime && o.create_time < endTime) && (o.order_num.Contains(KeyWords) || o.product.product_name.Contains(KeyWords) || o.emp.emp_name.Contains(KeyWords))))
                            .Select(o => new
                            {
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
                                o.order_id,
                                o.product.product_name
                            })
                            .ToList();
                        var BlurModel = model.OrderByDescending(o => o.order_id).Skip(limit * (page - 1)).Take(limit);
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
                else if (EmpRoleName == "销售经理")
                {
                    if (dateInfo == "")
                    {
                        //获取到采购单的类型ID
                        var order_type = db.order_type.Where(ot => ot.order_type_name == "销售单").SingleOrDefault();
                        var Order_Type_ID = order_type.order_type_id;
                        var model = db.order_model.Include("emp").Include("product").Include("order_type").Include("supplier")
                            .Where(o => o.order_type_id == Order_Type_ID/*&&o.emp.emp_id==EmpID*/)
                            .Select(o => new
                            {
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
                                o.order_id,
                                o.product.product_name
                            })
                            .Where(o => o
                            .order_num.Contains(KeyWords) || o
                            .creater.Contains(KeyWords) || o
                            .product_name.Contains(KeyWords))
                            .ToList();
                        var BlurModel = model.OrderByDescending(o => o.order_id).Skip(limit * (page - 1)).Take(limit);
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
                        //获取到采购单的类型ID
                        var order_type = db.order_type.Where(ot => ot.order_type_name == "销售单").SingleOrDefault();
                        var Order_Type_ID = order_type.order_type_id;
                        var model = db.order_model.Include("emp").Include("product").Include("order_type").Include("supplier")
                            .Where(o => o.order_type_id == Order_Type_ID &&
                                ((o.create_time >= startTime && o.create_time < endTime) && (o.order_num.Contains(KeyWords) || o.product.product_name.Contains(KeyWords) || o.emp.emp_name.Contains(KeyWords))))
                            .Select(o => new
                            {
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
                                o.order_id,
                                o.product.product_name
                            })
                            .ToList();
                        var BlurModel = model.OrderByDescending(o => o.order_id).Skip(limit * (page - 1)).Take(limit);
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
                else
                {
                    if (dateInfo == "")
                    {
                        //获取到采购单的类型ID
                        var order_type = db.order_type.Where(ot => ot.order_type_name == "销售单").SingleOrDefault();
                        var Order_Type_ID = order_type.order_type_id;
                        var model = db.order_model.Include("emp").Include("product").Include("order_type").Include("supplier")
                            .Where(o => o.order_type_id == Order_Type_ID && o.emp.emp_id == EmpID)
                            .Select(o => new
                            {
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
                                o.order_id,
                                o.product.product_name
                            })
                            .Where(o => o
                            .order_num.Contains(KeyWords) || o
                            .creater.Contains(KeyWords) || o
                            .product_name.Contains(KeyWords))
                            .ToList();
                        var BlurModel = model.OrderByDescending(o => o.order_id).Skip(limit * (page - 1)).Take(limit);
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
                        //获取到采购单的类型ID
                        var order_type = db.order_type.Where(ot => ot.order_type_name == "销售单").SingleOrDefault();
                        var Order_Type_ID = order_type.order_type_id;
                        var model = db.order_model.Include("emp").Include("product").Include("order_type").Include("supplier")
                            .Where(o => o.order_type_id == Order_Type_ID && o.emp.emp_id == EmpID &&
                                ((o.create_time >= startTime && o.create_time < endTime) && (o.order_num.Contains(KeyWords) || o.product.product_name.Contains(KeyWords) || o.emp.emp_name.Contains(KeyWords))))
                            .Select(o => new
                            {
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
                                o.order_id,
                                o.product.product_name
                            })
                            .ToList();
                        var BlurModel = model.OrderByDescending(o => o.order_id).Skip(limit * (page - 1)).Take(limit);
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
        }

        /// <summary>
        /// 销售单详情页面
        /// </summary>
        /// <param name="order_id">ajax传递的订单ID</param>
        /// <returns></returns>
        public ActionResult SalesOrder_Detail(int? order_id)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //获取到登录的用户名
                //var userName = Session["username"].ToString();
                //查询当前用户信息并获取到员工的ID
                //var EmpAll = db.emp.Where(e => e.username == userName).FirstOrDefault();
                //var EmpID = EmpAll.emp_id;
                //通过审核人的ID获取到订单审核人的姓名
                var checkerName = "";
                var checkerInfo = db.order_model.Include("emp").Where(o => o.order_id == order_id).SingleOrDefault();
                if (checkerInfo.checker == null)
                {
                    checkerName = "";
                }
                else
                {
                    var checker = db.emp.Where(e => e.emp_id == checkerInfo.checker).FirstOrDefault();
                    checkerName = checker.emp_name;
                }
                //获取到采购单的类型ID
                var order_type = db.order_type.Where(ot => ot.order_type_name == "销售单").SingleOrDefault();
                var Order_Type_ID = order_type.order_type_id;
                var aa = db.order_model.Include("emp")
                    .Include("product")
                    .Include("order_type")
                    .Include("supplier")

                    .Select(o => new
                    {
                        o.order_id,
                        Order_Num = o.order_num,
                        creater = o.emp.emp_name,
                        create_time = o.create_time,
                        checker = o.checker,
                        end_time = o.end_time,
                        OutIn_Number = o.OutIn_number,
                        o.order_type.order_type_name,
                        Order_State = o.order_state,
                        Total_Num = o.total_num,
                        Total_Price = o.total_price,
                        o.supplier.supplier_name,
                        o.order_type_id,
                        Product_Name = o.product.product_name,
                        Product_ID = o.product.product_id,
                        In_Price = o.product.in_price,
                        Unit = o.product.unit
                    })
                    .ToList();
                var purchases = aa.Where(o => o.order_type_id == Order_Type_ID && o.order_id == order_id).SingleOrDefault();

                ViewBag.Order_Num = purchases.Order_Num;
                ViewBag.OutIn_Number = purchases.OutIn_Number;
                ViewBag.Order_Stat = purchases.Order_State;
                ViewBag.Order_State = ViewBag.Order_Stat ? "已审核" : "未审核";
                ViewBag.Creater = purchases.creater;
                ViewBag.Create_Time = purchases.create_time;
                ViewBag.Checker = checkerName;
                //ViewBag.Supplier_ID = purchases.Supplier_ID;
                //ViewBag.Supplier_Name = purchases.Supplier_Name;
                //ViewBag.Contact = purchases.Contact;
                ViewBag.Product_Name = purchases.Product_Name;
                ViewBag.Product_ID = purchases.Product_ID;
                ViewBag.In_Price = purchases.In_Price;
                ViewBag.Total_Price = purchases.Total_Price;
                ViewBag.Total_Num = purchases.Total_Num;
                ViewBag.Unit = purchases.Unit;
            }
            return View();
        }
    }
}