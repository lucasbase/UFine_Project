using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Youfan_Invoicing_Management_System.Models;

namespace Youfan_Invoicing_Management_System.Controllers
{
    [Authorize]
    public class GoodsMnagController : Controller
    {
        // GET: GoodsMnag
        /// <summary>
        /// 显示视图页面
        /// </summary>
        /// <returns></returns>
        public ActionResult GoodsManager()
        {
            return View();
        }

        /// <summary>
        /// 获取供应商信息
        /// </summary>
        [HttpPost]
        public JsonResult GetSupplierlist()
        {
            using (ERPEntities db = new ERPEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;//关闭EF的默认加载
                var Supplierlist = db.supplier.ToList();
                //将数据Json化并传到前台视图
                return Json(new { data = Supplierlist }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 通过供应商ID
        /// </summary>
        /// <param name="supplier_id">供应商ID</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetProductTypelist(int supplier_id)
        {
            using (ERPEntities db = new ERPEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;//关闭EF的默认加载
                var ProductType = db.product_type.Where(p => p.supplier_id == supplier_id).ToList();//通过部门ID 查找到对应的角色
                List<SelectListItem> item = new List<SelectListItem>();
                foreach (var Product_Type in ProductType)
                {
                    item.Add(new SelectListItem { Text = Product_Type.product_type_name, Value = Product_Type.product_type_id.ToString() });
                }
                //将数据Json化并传到前台视图
                return Json(new { data = item }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 页面初始化加载的时候分页显示数据
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
                //查询商品表数据
                var pro = db.product.ToList();
                //使用分页功能
                var ProPaging = pro
                    .OrderByDescending(p => p.product_id)
                    .Skip(limit * (page - 1))
                    .Take(limit);
                var result = new
                {
                    code = 0,
                    msg = "",
                    count = pro.Count(),
                    data = ProPaging
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 通过商品名称或厂商来模糊查询并分页显示数据
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="limit">每页显示的条目数</param>
        /// <param name="KeyWords">商品名称或厂商</param>
        /// <returns></returns>
        public ActionResult BlurSelPro(int page, int limit, string KeyWords)
        {
            using (ERPEntities db = new ERPEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                //查询商品表数据
                var pro = db.product.ToList()
                    .Where(p => p
                    .product_name.Contains(KeyWords) || p
                    .producer.Contains(KeyWords));
                //使用分页功能
                var ProPaging = pro
                    .OrderByDescending(p => p.product_id)
                    .Skip(limit * (page - 1))
                    .Take(limit);
                var result = new
                {
                    code = 0,
                    msg = "",
                    count = pro.Count(),
                    data = ProPaging
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 显示页面视图
        /// </summary>
        /// <returns></returns>
        public ActionResult GoodsEmp()
        {
            return View();
        }


        /// <summary>
        /// 添加商品信息的页面显示
        /// </summary>
        /// <returns></returns>
        public ActionResult AddProduct()
        {
            using (ERPEntities db = new ERPEntities())
            {
                ViewBag.product_type = new SelectList(db.product_type.ToList(), "product_type_id", "product_type_name");
                return View();
            }
        }

        /// <summary>
        /// 添加商品信息
        /// </summary>
        /// <param name="product">页面传过来的商品对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ADD(product product)
        {
            using (ERPEntities db = new ERPEntities())
            {
                product addpro = new product
                {
                    product_type_id = product.product_type_id,
                    product_name = product.product_name,
                    producer = product.producer,
                    unit = product.unit,
                    in_price = product.in_price,
                    pro_Inventory = 0,
                    Shelves = false

                };
                db.product.Add(addpro);
                if (db.SaveChanges() > 0)
                {
                    return Json(new
                    {
                        Success = true,
                        Message = "添加成功！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "添加失败！"
                    });
                }
            }
        }

        /// <summary>
        /// 批量删除商品信息
        /// </summary>
        /// <param name="Product_ID">页面中选中的商品ID集合</param>
        /// <returns></returns>
        public ActionResult ProDels(List<int> Product_ID)
        {
            using (ERPEntities db = new ERPEntities())
            {
                var ProDels = db.product.Where(p => Product_ID.Contains(p.product_id));
                db.product.RemoveRange(ProDels);
                if (db.SaveChanges() > 0)
                {
                    return Json(new
                    {
                        Success = true,
                        Message = "删除成功！"
                    });
                }

                return Json(new
                {
                    Success = false,
                    Message = "删除失败！"
                });
            }
        }

        /// <summary>
        /// 商品下架
        /// </summary>
        /// <param name="product_id">商品ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Off_shelf(int? product_id)
        {
            using (ERPEntities db = new ERPEntities())
            {
                product pro_shelves = db.product.FirstOrDefault(p => p.product_id == product_id);
                pro_shelves.Shelves = false;
                if (db.SaveChanges() > 0)
                {
                    return Json(new
                    {
                        Success = true,
                        Message = "下架成功！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "下架失败！"
                    });
                }
            }
        }

        /// <summary>
        /// 商品上架
        /// </summary>
        /// <param name="product_id">商品ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Shelves(int? product_id)
        {
            using (ERPEntities db = new ERPEntities())
            {
                product pro_shelves = db.product.FirstOrDefault(p => p.product_id == product_id);
                pro_shelves.Shelves = true;
                if (db.SaveChanges() > 0)
                {
                    return Json(new
                    {
                        Success = true,
                        Message = "上架成功！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "上架失败！"
                    });
                }
            }
        }

        /// <summary>
        /// 单行删除商品信息
        /// </summary>
        /// <param name="product_id">商品ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DelPro(int? product_id)
        {
            using (ERPEntities db = new ERPEntities())
            {
                var delete_pro = db.product.FirstOrDefault(p => p.product_id == product_id);
                db.product.Remove(delete_pro);
                if (db.SaveChanges() > 0)
                {
                    return Json(new
                    {
                        Success = true,
                        Message = "删除成功！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "删除失败！"
                    });
                }
            }
        }

        /// <summary>
        /// 申请补货视图显示
        /// </summary>
        /// <param name="product_id">补货商品ID</param>
        /// <returns></returns>
        public ActionResult InsBhOrder(int? product_id)
        {
            using (ERPEntities db = new ERPEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;
                var pid = db.product.Include("product_type").Where(p => product_id == p.product_id).FirstOrDefault();
                ViewBag.ProName = pid.product_name;
                ViewBag.ProID = pid.product_id;
                var SupplierID = pid.product_type.supplier_id;
                var supplierName = db.supplier.Where(s => s.supplier_id == SupplierID).FirstOrDefault();
                ViewBag.SupplierName = supplierName.supplier_name;
                return View();
            }
        }

        /// <summary>
        /// 申请补货
        /// </summary>
        /// <param name="order_model">补货商品的对象</param>
        /// <param name="product_id">补货商品ID</param>
        /// <returns></returns>
        public ActionResult InsertReplenish(order_model order_model, int? product_id, string SupplierName)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //接收登录时的用户名
                var username = Session["username"].ToString();
                //查询当前用户id
                var EmpAll = db.emp.Where(e => e.username == username).FirstOrDefault();
                var EmpID = EmpAll.emp_id;
                //查询product表ID
                var p_id = db.product.Where(p => product_id == p.product_id).FirstOrDefault();
                //根据ID查询出商品单价
                var d_price = p_id.in_price;
                //计算出总价格
                var t_price = d_price * order_model.total_num;
                //查询当前补货商品对应的供应商ID
                var supplier_Info = db.supplier.Where(s => s.supplier_name == SupplierName).FirstOrDefault();
                var supplier_ID = supplier_Info.supplier_id;
                //将数据添加到order_model表
                order_model Addmodel = new order_model()
                {
                    //自动添加单号（ddh=订单号）
                    order_num = "BH" + DateTime.Now.ToString("yyyyMMddhhmmss"),
                    //添加创建人ID
                    creater = EmpID,
                    //添加创建时间
                    create_time = DateTime.Now,
                    //添加商品ID
                    product_id = product_id,
                    //添加商品数量
                    total_num = order_model.total_num,
                    //添加商品总价
                    total_price = t_price,
                    //添加供应商ID
                    supplier_id = supplier_ID,
                    //添加订单类型
                    order_type_id = 1,
                    //添加订单状态
                    order_state = false
                };
                db.order_model.Add(Addmodel);
                if (db.SaveChanges() > 0)
                {
                    return Json(new
                    {
                        Success = true,
                        Message = "申请成功！！！"
                    });
                }
                return Json(new
                {
                    Success = false,
                    Message = "申请失败！！！"
                });
            }
        }
    }
}