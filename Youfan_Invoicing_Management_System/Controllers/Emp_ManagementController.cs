using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Youfan_Invoicing_Management_System.BLL;
using Youfan_Invoicing_Management_System.ERP_Verification;
using Youfan_Invoicing_Management_System.Models;

namespace Youfan_Invoicing_Management_System.Controllers
{
    [Authorize]
    public class Emp_ManagementController : Controller
    {
        /// <summary>
        /// 批量导出需要导出的列表
        /// </summary>
        /// <returns></returns>
        public FileResult ExportData()
        {
            using (ERPEntities db = new ERPEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;//关闭EF的默认加载
                //数据显示
                var checkList = db.emp.Include("role").Include("dep")
                                    .Select(e => new
                                    {
                                        Emp_ID = e.emp_id,
                                        Emp_Name = e.emp_name,
                                        Role_Name = e.role.role_name,
                                        Emp_tel = e.tel,
                                        Emp_email = e.email,
                                        Emp_Sex = (bool)e.gender ? "男" : "女",
                                        Dep_Name = e.role.dep.dep_name
                                    }).ToList();
                //创建一个新的excel文件
                HSSFWorkbook book = new HSSFWorkbook();
                //创建一个工作区
                ISheet sheet = book.CreateSheet("sheet1");
                //创建一行 也就是在sheet1这个工作区创建一行 在NPOI中只有先创建才能后使用
                IRow row = sheet.CreateRow(0);
                for (int i = 0; i < 7; i++)
                {
                    //设置单元格的宽度
                    sheet.SetColumnWidth(i, 16 * 156);
                }
                sheet.SetColumnWidth(0, 16 * 160);
                sheet.SetColumnWidth(1, 35 * 160);
                sheet.SetColumnWidth(2, 16 * 160);
                sheet.SetColumnWidth(3, 40 * 160);
                sheet.SetColumnWidth(4, 50 * 160);
                sheet.SetColumnWidth(5, 50 * 160);
                sheet.SetColumnWidth(6, 50 * 160);
                //定义一个样式，迎来设置样式属性
                ICellStyle setborder = book.CreateCellStyle();

                //设置单元格上下左右边框线 但是不包括最外面的一层
                setborder.BorderLeft = BorderStyle.Thin;
                setborder.BorderRight = BorderStyle.Thin;
                setborder.BorderBottom = BorderStyle.Thin;
                setborder.BorderTop = BorderStyle.Thin;

                //文字水平和垂直对齐方式
                setborder.VerticalAlignment = VerticalAlignment.Center;//垂直居中
                setborder.Alignment = HorizontalAlignment.Center;//水平居中
                setborder.WrapText = true;//自动换行

                //再定义一个样式，用来设置最上面标题行的样式
                ICellStyle setborderdeth = book.CreateCellStyle();

                //设置单元格上下左右边框线 但是不包括最外面的一层
                setborderdeth.BorderLeft = BorderStyle.Thin;
                setborderdeth.BorderRight = BorderStyle.Thin;
                setborderdeth.BorderBottom = BorderStyle.Thin;
                setborderdeth.BorderTop = BorderStyle.Thin;

                //定义一个字体样式
                IFont font = book.CreateFont();
                //将字体设为红色
                font.Color = IndexedColors.Red.Index;
                //font.FontHeightInPoints = 17;
                //将定义的font样式给到setborderdeth样式中
                setborderdeth.SetFont(font);

                //文字水平和垂直对齐方式
                setborderdeth.VerticalAlignment = VerticalAlignment.Center;//垂直居中
                setborderdeth.Alignment = HorizontalAlignment.Center;//水平居中
                setborderdeth.WrapText = true;  //自动换行

                //设置第一行单元格的高度为25
                row.HeightInPoints = 25;
                //设置单元格的值
                row.CreateCell(0).SetCellValue("员工编号");
                //将style属性给到这个单元格
                row.GetCell(0).CellStyle = setborderdeth;
                row.CreateCell(1).SetCellValue("员工姓名");
                row.GetCell(1).CellStyle = setborderdeth;
                row.CreateCell(2).SetCellValue("性别");
                row.GetCell(2).CellStyle = setborderdeth;
                row.CreateCell(3).SetCellValue("部门名称");
                row.GetCell(3).CellStyle = setborderdeth;
                row.CreateCell(4).SetCellValue("员工角色名");
                row.GetCell(4).CellStyle = setborderdeth;
                row.CreateCell(5).SetCellValue("员工联系电话");
                row.GetCell(5).CellStyle = setborderdeth;
                row.CreateCell(6).SetCellValue("员工邮箱");
                row.GetCell(6).CellStyle = setborderdeth;
                //循环的导出到excel的每一行
                for (int i = 0; i < checkList.Count; i++)
                {
                    //每循环一次，就新增一行  索引从0开始 所以第一次循环CreateRow(1) 前面已经创建了标题行为0
                    IRow row1 = sheet.CreateRow(i + 1);
                    row1.HeightInPoints = 21;
                    //给新加的这一行创建第一个单元格，并且给这第一个单元格设置值 以此类推...
                    row1.CreateCell(0).SetCellValue(Convert.ToString(checkList[i].Emp_ID));
                    //先获取这一行的第一个单元格，再给其设置样式属性 以此类推...
                    row1.GetCell(0).CellStyle = setborder;
                    row1.CreateCell(1).SetCellValue(Convert.ToString(checkList[i].Emp_Name));
                    row1.GetCell(1).CellStyle = setborder;
                    row1.CreateCell(2).SetCellValue(Convert.ToString(checkList[i].Emp_Sex));
                    row1.GetCell(2).CellStyle = setborder;
                    row1.CreateCell(3).SetCellValue(Convert.ToString(checkList[i].Dep_Name));
                    row1.GetCell(3).CellStyle = setborder;
                    row1.CreateCell(4).SetCellValue(Convert.ToString(checkList[i].Role_Name));
                    row1.GetCell(4).CellStyle = setborder;
                    row1.CreateCell(5).SetCellValue(Convert.ToString(checkList[i].Emp_tel));
                    row1.GetCell(5).CellStyle = setborder;
                    row1.CreateCell(6).SetCellValue(Convert.ToString(checkList[i].Emp_email));
                    row1.GetCell(6).CellStyle = setborder;
                }
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                book.Write(ms);
                ms.Seek(0, SeekOrigin.Begin);
                DateTime dttime = DateTime.Now;
                string datetime = dttime.ToString("yyyy-MM-dd");
                string filename = "员工信息" + datetime + ".xls";
                return File(ms, "application/vns.ms-excel", filename);
            }
        }

        // GET: Emp_Management
        public ActionResult GetAjaxEmp()
        {
            return View();
        }
        /// <summary>
        /// 页面初始加载就分页显示数据
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="limit">每页显示的条数</param>
        /// <returns></returns>
        public ActionResult GetAjaxUserList(int page, int limit)
        {
            using (ERPEntities db = new ERPEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;//关闭EF的默认加载
                //数据显示
                var pageQuery = db.emp.Include("role").Include("dep").Select(e => new { Emp_ID = e.emp_id, Emp_Name = e.emp_name, Role_Name = e.role.role_name, TimeInfo = e.birthday, Emp_tel = e.tel, Emp_role_id = e.role_id, Dep_Name = e.role.dep.dep_name, Emp_email = e.email, Emp_address = e.address, Emp_Sex = e.gender, IsFrozen = e.IsFrozen }).ToList();
                //将显示的数据分页显示
                var PageInfo = pageQuery.OrderBy(e => e.Emp_ID).Skip(limit * (page - 1)).Take(limit).ToList();
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
        /// 模糊查询
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="limit">每页显示的条数</param>
        /// <param name="KeyWords">关键字</param>
        /// <returns></returns>
        public ActionResult GetAjaxEmpList(int page, int limit, string KeyWords/*,string date*/)
        {
            using (ERPEntities db = new ERPEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;//关闭EF的默认加载
                //模糊查询的数据显示
                var info = db.emp.Include("role").Where(e => e.emp_name.Contains(KeyWords) || e.role.role_name.Contains(KeyWords)).Select(e => new { Emp_ID = e.emp_id, Emp_Name = e.emp_name, Role_Name = e.role.role_name, TimeInfo = e.birthday, Emp_tel = e.tel, Emp_role_id = e.role_id, Emp_email = e.email, Emp_Sex = e.gender, Emp_address = e.address, IsFrozen = e.IsFrozen }).ToList();
                //将模糊查询的数据进行分页显示
                var PageInfo = info.OrderBy(e => e.Emp_ID).Skip(limit * (page - 1)).Take(limit).ToList();
                var result = new
                {
                    code = 0,
                    msg = "",
                    count = info.Count,
                    data = PageInfo
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 创建生成添加员工信息的视图
        /// </summary>
        /// <returns></returns>
        public ActionResult EmpAdd()
        {
            ViewBag.Role = new SelectList(RoleManager.GetAll(), "role_id", "role_name");
            return View();
        }
        /// <summary>
        /// 添加员工方法
        /// </summary>
        /// <param name="EmpInfo">需要添加的员工信息对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EmpAdd(emp EmpInfo)
        {
            using (ERPEntities db = new ERPEntities())
            {
                var EmpInfos = db.emp.FirstOrDefault(e => e.username == EmpInfo.username);
                //判断是否存在该用户名
                if (EmpInfos != null)
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "该用户名已存在！！！"
                    });
                }
                else
                {
                    //添加员工信息
                    emp AddEmpInfo = new emp()
                    {
                        //role_id = Convert.ToInt32(EmpInfo.role_id),
                        emp_name = EmpInfo.emp_name,
                        username = EmpInfo.username,
                        email = EmpInfo.email,
                        tel = EmpInfo.tel,
                        gender = EmpInfo.gender,
                        role_id = EmpInfo.role_id,
                        IsFrozen = true,
                        password = DESEncrypt.EncryptDES(EmpInfo.password)
                    };
                    db.emp.Add(AddEmpInfo);
                    //判断数据库受影响行数的行数是否大于0
                    if (db.SaveChanges() > 0)
                    {
                        return Json(new
                        {
                            code = 0,
                            Success = true,
                            Message = "员工信息添加成功！！！"
                        });
                    }
                    return Json(new
                    {
                        Success = false,
                        Message = "员工信息添加失败！！！"
                    });
                }
            }
        }
        /// <summary>
        /// 通过员工ID查找到指定的员工信息
        /// </summary>
        /// <param name="emp_id">员工ID</param>
        /// <returns></returns>
        public ActionResult EmpEidt(int? emp_id)
        {
            using (ERPEntities db = new ERPEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;//关闭EF贪婪加载
                //通过传过来的员工ID查询信息
                var editEmpInfo = db.emp.FirstOrDefault(e => e.emp_id == emp_id);
                ViewBag.emp_id = editEmpInfo.emp_id;//员工ID
                ViewBag.emp_name = editEmpInfo.emp_name;//员工姓名
                ViewBag.username = editEmpInfo.username;//员工登录名
                ViewBag.tel = editEmpInfo.tel;//员工电话
                ViewBag.eamil = editEmpInfo.email;//员工邮箱地址
                ViewBag.Role = new SelectList(RoleManager.GetAll(), "role_id", "role_name");
                ViewBag.Role_ID = editEmpInfo.role_id;//角色ID
                ViewBag.EmpInfo = Newtonsoft.Json.JsonConvert.SerializeObject(editEmpInfo);
            }
            return View();
        }
        /// <summary>
        /// 修改员工权限的操作方法并显示修改权限视图
        /// </summary>
        /// <param name="emp_id">员工ID</param>
        /// <returns></returns>
        public ActionResult EmpSubAuthority(int? emp_id)
        {
            using (ERPEntities db = new ERPEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;//关闭EF贪婪加载
                //通过传过来的员工ID查询信息
                var editEmpInfo = db.emp.FirstOrDefault(e => e.emp_id == emp_id);
                ViewBag.emp_id = editEmpInfo.emp_id;//员工ID
            }
            return View();
        }

        /// <summary>
        /// 对用户的权限进行修改
        /// </summary>
        /// <param name="MenuIDList">树形菜单节点ID</param>
        /// <param name="emp_id">员工ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EmpSubAuthority(List<int?> MenuIDList, int? emp_id)
        {
            using (ERPEntities db = new ERPEntities())
            {
                //通过员工ID找到该员工对应的菜单信息
                var MenuInfo = db.relation_emp_menu.Where(rem => rem.emp_id == emp_id).Select(s => s.relation_id).ToList();
                //判断该用户是否拥有菜单，如果没有直接添加所选的菜单，如果有菜单，就先删除所拥有的菜单，再次对该用户添加所选中的菜单
                if (MenuInfo.Count > 0)
                {
                    //先批量删除该用户对应的所有菜单，再循环添加菜单权限
                    var DelMenuList = db.relation_emp_menu.Where(rem => MenuInfo.Contains(rem.relation_id));
                    db.relation_emp_menu.RemoveRange(DelMenuList);
                    if (db.SaveChanges() > 0)
                    {
                        //循环添加员工对菜单表的数据
                        foreach (var item in MenuIDList)
                        {
                            relation_emp_menu emp_Menu = new relation_emp_menu
                            {
                                emp_id = emp_id,
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
                                Message = "用户权限修改成功成功！！！"
                            });
                        }
                        return Json(new
                        {
                            Success = false,
                            Message = "用户权限修改失败！！！"
                        });
                    }
                    return Json(new
                    {
                        Success = false,
                        Message = "用户权限修改失败！！！"
                    });
                }
                else
                {
                    //循环添加员工对菜单表的数据
                    foreach (var item in MenuIDList)
                    {
                        relation_emp_menu emp_Menu = new relation_emp_menu
                        {
                            emp_id = emp_id,
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
                            Message = "用户权限修改成功成功！！！"
                        });
                    }
                    return Json(new
                    {
                        Success = false,
                        Message = "用户权限修改失败！！！"
                    });
                }

            }
        }

        /// <summary>
        /// 通过员工ID查找该用户对应的菜单ID
        /// </summary>
        /// <param name="emp_id">员工ID</param>
        /// <returns></returns>
        public JsonResult GetEmpMenuList(int? emp_id)
        {
            using (ERPEntities db = new ERPEntities())
            {
                var checkArr = "";
                var EmpMenulist = (from e in db.emp
                                   join rem in db.relation_emp_menu on e.emp_id equals rem.emp_id
                                   join m in db.menu on rem.menu_id equals m.menu_id
                                   where rem.emp_id == emp_id
                                   select new
                                   {
                                       title = m.menu_name,
                                       id = m.menu_id,
                                       parentId = m.parent_menu_id,
                                       //checked:"1"为复选框选中状态
                                       checkArr = "{\"type\": \"0\", \"checked\": \"1\"}"
                                   }).ToList();
                var list = new
                {
                    code = 0,
                    message = "操作成功",
                    data = EmpMenulist
                };
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 创建生成修改员工信息的视图
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EmpEidtlInfo(emp EmpInfo)
        {
            using (ERPEntities db = new ERPEntities())
            {
                emp edit = db.emp.FirstOrDefault(e => e.emp_id == EmpInfo.emp_id);
                edit.emp_name = EmpInfo.emp_name;
                edit.username = EmpInfo.username;
                edit.tel = EmpInfo.tel;
                edit.email = EmpInfo.email;
                edit.role_id = EmpInfo.role_id;
                //db.emp.Add(EmpInfo);
                if (db.SaveChanges() > 0)
                {
                    return Json(new
                    {
                        code = 0,
                        Success = true,
                        Message = "员工信息修改成功！！！"
                    });
                }
                return Json(new
                {
                    Success = false,
                    Message = "员工信息修改失败！！！"
                });
            }
        }
        /// <summary>
        /// 批量删除的操作方法
        /// </summary>
        /// <param name="Emp_IDs">页面数据表格选中的员工ID集合</param>
        /// <returns></returns>
        public ActionResult BatchDelEmps(List<int> Emp_IDs)
        {
            using (ERPEntities db = new ERPEntities())
            {
                var delEmpsQuery = db.emp.Where(u => Emp_IDs.Contains(u.emp_id));
                db.emp.RemoveRange(delEmpsQuery);
                if (db.SaveChanges() > 0)
                {
                    return Json(new
                    {
                        code = 0,
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
        /// 单行删除员工信息
        /// </summary>
        /// <param name="Emp_ID">员工ID</param>
        /// <returns></returns>
        public ActionResult DelEmp(int? Emp_ID)
        {
            using (ERPEntities db = new ERPEntities())
            {
                emp empinfo = db.emp.FirstOrDefault(u => u.emp_id == Emp_ID);
                db.emp.Remove(empinfo);
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
        /// <summary>
        /// 冻结账户
        /// </summary>
        /// <param name="Emp_ID">员工ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Frozen(int? Emp_ID)
        {
            using (ERPEntities db = new ERPEntities())
            {
                emp Empinfo = db.emp.FirstOrDefault(u => u.emp_id == Emp_ID);
                Empinfo.IsFrozen = false;
                if (db.SaveChanges() > 0)
                {
                    return Json(new
                    {
                        Success = true,
                        Message = "账户冻结成功！！！"
                    });
                }
                return Json(new
                {
                    Success = false,
                    Message = "账户冻结失败！！！"
                });
            }
        }
        /// <summary>
        /// 解冻账户
        /// </summary>
        /// <param name="Emp_ID">员工ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Thaw(int? Emp_ID)
        {
            using (ERPEntities db = new ERPEntities())
            {
                emp Empinfo = db.emp.FirstOrDefault(u => u.emp_id == Emp_ID);
                Empinfo.IsFrozen = true;
                if (db.SaveChanges() > 0)
                {
                    return Json(new
                    {
                        Success = true,
                        Message = "账户解冻成功！！！"
                    });
                }

                return Json(new
                {
                    Success = false,
                    Message = "账户解冻失败！！！"
                });
            }
        }
    }
}