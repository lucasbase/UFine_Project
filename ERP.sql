USE [master]
GO
/****** Object:  Database [ERP]    Script Date: 2020/9/17 11:39:12 ******/
CREATE DATABASE [ERP]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ERP', FILENAME = N'E:\卓越项目\大三卓越项目\数据库\ERP.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ERP_log', FILENAME = N'E:\卓越项目\大三卓越项目\数据库\ERP_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ERP] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ERP].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ERP] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ERP] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ERP] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ERP] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ERP] SET ARITHABORT OFF 
GO
ALTER DATABASE [ERP] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ERP] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ERP] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ERP] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ERP] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ERP] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ERP] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ERP] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ERP] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ERP] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ERP] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ERP] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ERP] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ERP] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ERP] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ERP] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ERP] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ERP] SET RECOVERY FULL 
GO
ALTER DATABASE [ERP] SET  MULTI_USER 
GO
ALTER DATABASE [ERP] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ERP] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ERP] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ERP] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ERP] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ERP', N'ON'
GO
ALTER DATABASE [ERP] SET QUERY_STORE = OFF
GO
USE [ERP]
GO
/****** Object:  Table [dbo].[dep]    Script Date: 2020/9/17 11:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[dep](
	[dep_id] [int] IDENTITY(1,1) NOT NULL,
	[dep_name] [nvarchar](50) NULL,
	[tel] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[dep_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[emp]    Script Date: 2020/9/17 11:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[emp](
	[emp_id] [int] IDENTITY(1,1) NOT NULL,
	[role_id] [int] NULL,
	[emp_name] [nvarchar](50) NULL,
	[username] [nvarchar](50) NULL,
	[email] [nvarchar](50) NULL,
	[tel] [nvarchar](30) NULL,
	[gender] [bit] NULL,
	[address] [nvarchar](50) NULL,
	[birthday] [date] NULL,
	[password] [nvarchar](32) NULL,
	[IsFrozen] [bit] NULL,
 CONSTRAINT [PK__emp__1299A861E220CD62] PRIMARY KEY CLUSTERED 
(
	[emp_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[menu]    Script Date: 2020/9/17 11:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[menu](
	[menu_id] [int] NOT NULL,
	[parent_menu_id] [int] NULL,
	[menu_name] [nvarchar](20) NULL,
	[url] [nvarchar](255) NULL,
	[icon] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[menu_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_detail]    Script Date: 2020/9/17 11:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_detail](
	[order_detail_id] [int] IDENTITY(1,1) NOT NULL,
	[detail_num] [int] NULL,
	[detail_price] [decimal](18, 0) NULL,
	[product_id] [int] NULL,
	[order_id] [int] NULL,
	[order_type_id] [int] NULL,
 CONSTRAINT [PK__order_de__3C5A4080E2357579] PRIMARY KEY CLUSTERED 
(
	[order_detail_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_model]    Script Date: 2020/9/17 11:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_model](
	[order_id] [int] IDENTITY(1,1) NOT NULL,
	[order_num] [nvarchar](30) NULL,
	[creater] [int] NULL,
	[create_time] [datetime] NULL,
	[checker] [int] NULL,
	[end_time] [datetime] NULL,
	[OutIn_number] [nvarchar](50) NULL,
	[order_type_id] [int] NULL,
	[order_state] [bit] NULL,
	[product_id] [int] NULL,
	[total_num] [int] NULL,
	[total_price] [decimal](18, 2) NULL,
	[supplier_id] [int] NULL,
 CONSTRAINT [PK__order_mo__46596229C1CB282A] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_type]    Script Date: 2020/9/17 11:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_type](
	[order_type_id] [int] IDENTITY(1,1) NOT NULL,
	[order_type_name] [nvarchar](20) NULL,
 CONSTRAINT [PK_order_type] PRIMARY KEY CLUSTERED 
(
	[order_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product]    Script Date: 2020/9/17 11:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product](
	[product_id] [int] IDENTITY(1,1) NOT NULL,
	[product_type_id] [int] NULL,
	[product_name] [nvarchar](30) NULL,
	[producer] [nvarchar](30) NULL,
	[unit] [nvarchar](30) NULL,
	[in_price] [decimal](18, 2) NULL,
	[pro_Inventory] [int] NULL,
	[Shelves] [bit] NULL,
 CONSTRAINT [PK__product__47027DF5913305CE] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product_type]    Script Date: 2020/9/17 11:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_type](
	[product_type_id] [int] IDENTITY(1,1) NOT NULL,
	[supplier_id] [int] NULL,
	[product_type_name] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[product_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[relation_emp_menu]    Script Date: 2020/9/17 11:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[relation_emp_menu](
	[relation_id] [int] IDENTITY(1,1) NOT NULL,
	[emp_id] [int] NULL,
	[menu_id] [int] NULL,
 CONSTRAINT [PK_relation_emp_menu] PRIMARY KEY CLUSTERED 
(
	[relation_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[relation_role_menu]    Script Date: 2020/9/17 11:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[relation_role_menu](
	[relation_id] [int] IDENTITY(1,1) NOT NULL,
	[role_id] [int] NULL,
	[menu_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[relation_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role]    Script Date: 2020/9/17 11:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[role_id] [int] IDENTITY(1,1) NOT NULL,
	[role_name] [nvarchar](30) NULL,
	[dep_id] [int] NULL,
 CONSTRAINT [PK__role__760965CCA39F658A] PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[store]    Script Date: 2020/9/17 11:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[store](
	[store_id] [int] IDENTITY(1,1) NOT NULL,
	[store_name] [nvarchar](30) NULL,
	[address] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[store_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[store_detail]    Script Date: 2020/9/17 11:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[store_detail](
	[detail_id] [int] IDENTITY(1,1) NOT NULL,
	[store_id] [int] NULL,
	[product_id] [int] NULL,
	[num] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[detail_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[store_log]    Script Date: 2020/9/17 11:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[store_log](
	[log_id] [int] IDENTITY(1,1) NOT NULL,
	[emp_id] [int] NULL,
	[order_id] [int] NULL,
	[oper_time] [datetime] NULL,
	[num] [int] NULL,
	[order_type_id] [int] NULL,
 CONSTRAINT [PK__store_lo__9E2397E089732D5C] PRIMARY KEY CLUSTERED 
(
	[log_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[supplier]    Script Date: 2020/9/17 11:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[supplier](
	[supplier_id] [int] IDENTITY(1,1) NOT NULL,
	[supplier_name] [nvarchar](30) NULL,
	[address] [nvarchar](50) NULL,
	[contact] [nvarchar](30) NULL,
	[tel] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[supplier_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[dep] ON 

INSERT [dbo].[dep] ([dep_id], [dep_name], [tel]) VALUES (1, N'系统管理', N'010-8168121')
INSERT [dbo].[dep] ([dep_id], [dep_name], [tel]) VALUES (3, N'采购部', N'010-8168124')
INSERT [dbo].[dep] ([dep_id], [dep_name], [tel]) VALUES (5, N'仓储部', N'010-8168125')
INSERT [dbo].[dep] ([dep_id], [dep_name], [tel]) VALUES (9, N'销售部', N'010-8168125')
SET IDENTITY_INSERT [dbo].[dep] OFF
SET IDENTITY_INSERT [dbo].[emp] ON 

INSERT [dbo].[emp] ([emp_id], [role_id], [emp_name], [username], [email], [tel], [gender], [address], [birthday], [password], [IsFrozen]) VALUES (1, 2, N'张三', N'zhangsan', N'835540436@qq.com', N'15010090634', 1, N'长沙', CAST(N'2016-05-01' AS Date), N'a123456', 1)
INSERT [dbo].[emp] ([emp_id], [role_id], [emp_name], [username], [email], [tel], [gender], [address], [birthday], [password], [IsFrozen]) VALUES (2, 1, N'管理员', N'admina', N'835540436@qq.com', N'15010090634', 1, N'长沙', CAST(N'2016-05-04' AS Date), N'a123456', 1)
INSERT [dbo].[emp] ([emp_id], [role_id], [emp_name], [username], [email], [tel], [gender], [address], [birthday], [password], [IsFrozen]) VALUES (3, 3, N'章子怡', N'zhangzi', N'835540436@qq.com', N'15010090634', 0, N'长沙', CAST(N'2016-04-01' AS Date), N'a123456', 1)
INSERT [dbo].[emp] ([emp_id], [role_id], [emp_name], [username], [email], [tel], [gender], [address], [birthday], [password], [IsFrozen]) VALUES (4, 4, N'李大勇', N'lidayong', N'835540436@qq.com', N'15010090634', 1, N'长沙', CAST(N'2016-05-01' AS Date), N'a123456', 1)
INSERT [dbo].[emp] ([emp_id], [role_id], [emp_name], [username], [email], [tel], [gender], [address], [birthday], [password], [IsFrozen]) VALUES (5, 5, N'张希希', N'zhangxix', N'835540436@qq.com', N'15010090634', 0, N'长沙', CAST(N'2016-05-01' AS Date), N'a123456', 1)
INSERT [dbo].[emp] ([emp_id], [role_id], [emp_name], [username], [email], [tel], [gender], [address], [birthday], [password], [IsFrozen]) VALUES (6, 6, N'李四', N'lisi123', N'80080@qq.com', N'15616519468', 1, N'长沙', NULL, N'a123456', 1)
INSERT [dbo].[emp] ([emp_id], [role_id], [emp_name], [username], [email], [tel], [gender], [address], [birthday], [password], [IsFrozen]) VALUES (7, 6, N'小倩', N'xiaoqian', N'90990@163.com', N'15616519467', 0, N'长沙', NULL, N'a123456', 0)
INSERT [dbo].[emp] ([emp_id], [role_id], [emp_name], [username], [email], [tel], [gender], [address], [birthday], [password], [IsFrozen]) VALUES (1011, 7, N'临时员工', N'lsyg', N'1@163.com', N'15616516496', 1, NULL, NULL, N'MMxbEX7hrlA=', 1)
INSERT [dbo].[emp] ([emp_id], [role_id], [emp_name], [username], [email], [tel], [gender], [address], [birthday], [password], [IsFrozen]) VALUES (1012, 4, N'xc', N'x', N'5@163.com', N'17673467451', 1, NULL, NULL, N'MMxbEX7hrlA=', 1)
INSERT [dbo].[emp] ([emp_id], [role_id], [emp_name], [username], [email], [tel], [gender], [address], [birthday], [password], [IsFrozen]) VALUES (1013, 7, N'李思思', N'lisisi', N'lucas@163.com', N'17673467459', 1, NULL, NULL, N'1X6Rl6UCItLzYa5QlcDctQ==', 1)
INSERT [dbo].[emp] ([emp_id], [role_id], [emp_name], [username], [email], [tel], [gender], [address], [birthday], [password], [IsFrozen]) VALUES (1014, 7, N'张三三', N'张三', N'555@163.com', N'17673468459', 0, NULL, NULL, N'n0ire14ed1JCTknDFO77gA==', 1)
SET IDENTITY_INSERT [dbo].[emp] OFF
INSERT [dbo].[menu] ([menu_id], [parent_menu_id], [menu_name], [url], [icon]) VALUES (1, 0, N'系统管理', NULL, N'&#xe64e;')
INSERT [dbo].[menu] ([menu_id], [parent_menu_id], [menu_name], [url], [icon]) VALUES (2, 0, N'销售管理', NULL, N'&#xe62c;')
INSERT [dbo].[menu] ([menu_id], [parent_menu_id], [menu_name], [url], [icon]) VALUES (3, 0, N'仓储管理', NULL, N'&#xe630;')
INSERT [dbo].[menu] ([menu_id], [parent_menu_id], [menu_name], [url], [icon]) VALUES (4, 0, N'采购管理', NULL, N'&#xe66f;')
INSERT [dbo].[menu] ([menu_id], [parent_menu_id], [menu_name], [url], [icon]) VALUES (5, 0, N'统计报表', NULL, N'&#xe614;')
INSERT [dbo].[menu] ([menu_id], [parent_menu_id], [menu_name], [url], [icon]) VALUES (101, 1, N'部门管理', N'/Dep_Management/Dep_Management_Index', NULL)
INSERT [dbo].[menu] ([menu_id], [parent_menu_id], [menu_name], [url], [icon]) VALUES (103, 1, N'员工管理', N'/Emp_Management/GetAjaxEmp', NULL)
INSERT [dbo].[menu] ([menu_id], [parent_menu_id], [menu_name], [url], [icon]) VALUES (201, 2, N'销售订单', N'/SalesOrders/SalesOrders', NULL)
INSERT [dbo].[menu] ([menu_id], [parent_menu_id], [menu_name], [url], [icon]) VALUES (202, 2, N'库存盘点', N'/GoodsMnag/GoodsEmp', NULL)
INSERT [dbo].[menu] ([menu_id], [parent_menu_id], [menu_name], [url], [icon]) VALUES (203, 2, N'商品管理', N'/GoodsMnag/GoodsManager', NULL)
INSERT [dbo].[menu] ([menu_id], [parent_menu_id], [menu_name], [url], [icon]) VALUES (204, 2, N'补货单管理', N'/ReplenishMent/Replenish', NULL)
INSERT [dbo].[menu] ([menu_id], [parent_menu_id], [menu_name], [url], [icon]) VALUES (301, 3, N'入库管理', N'/WarehouseMent/InWarehouseMent', NULL)
INSERT [dbo].[menu] ([menu_id], [parent_menu_id], [menu_name], [url], [icon]) VALUES (302, 3, N'出库管理', N'/WarehouseMent/OutWarehouseMent', NULL)
INSERT [dbo].[menu] ([menu_id], [parent_menu_id], [menu_name], [url], [icon]) VALUES (303, 3, N'仓库日志', N'/WarehouseMent/StoreLog_View', NULL)
INSERT [dbo].[menu] ([menu_id], [parent_menu_id], [menu_name], [url], [icon]) VALUES (304, 3, N'库存盘点', N'/GoodsMnag/GoodsEmp', NULL)
INSERT [dbo].[menu] ([menu_id], [parent_menu_id], [menu_name], [url], [icon]) VALUES (401, 4, N'采购订单', N'/Purchase_Management/Purchase_Index', NULL)
INSERT [dbo].[menu] ([menu_id], [parent_menu_id], [menu_name], [url], [icon]) VALUES (402, 4, N'采购审批', N'/Purchase_Management/Replenishment_Orders_Index', NULL)
INSERT [dbo].[menu] ([menu_id], [parent_menu_id], [menu_name], [url], [icon]) VALUES (403, 4, N'采购单管理', N'/Purchase_Management/Purchase_Manager_Index', NULL)
INSERT [dbo].[menu] ([menu_id], [parent_menu_id], [menu_name], [url], [icon]) VALUES (404, 4, N'供应商管理', N'/Purchase_Management/Supplier_Index', NULL)
INSERT [dbo].[menu] ([menu_id], [parent_menu_id], [menu_name], [url], [icon]) VALUES (501, 5, N'月度商品统计', N'/Report_Statistics/Sales_Statistics_Index', NULL)
INSERT [dbo].[menu] ([menu_id], [parent_menu_id], [menu_name], [url], [icon]) VALUES (502, 5, N'商品与单价统计', N'/Report_Statistics/Sales_Product_Price_Index', NULL)
INSERT [dbo].[menu] ([menu_id], [parent_menu_id], [menu_name], [url], [icon]) VALUES (503, 5, N'月度采购统计', N'/Purchase_Statistics/Purchase_Statistics_Index', NULL)
SET IDENTITY_INSERT [dbo].[order_model] ON 

INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (1, N'XS20200706042429', 3, CAST(N'2020-02-06T16:24:29.457' AS DateTime), 4, CAST(N'2020-07-06T16:27:36.977' AS DateTime), N'CK20200706042736', 3, 1, 6, 33, CAST(3960.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (2, N'XS20200706042436', 3, CAST(N'2020-02-06T16:24:36.233' AS DateTime), 4, CAST(N'2020-07-06T16:27:36.973' AS DateTime), N'CK20200706042736', 3, 1, 3, 33, CAST(326.70 AS Decimal(18, 2)), NULL)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (3, N'XS20200706042455', 3, CAST(N'2020-03-06T16:24:55.193' AS DateTime), 4, CAST(N'2020-07-06T16:27:36.973' AS DateTime), N'CK20200706042736', 3, 1, 7, 2, CAST(99.80 AS Decimal(18, 2)), NULL)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (4, N'XS20200706042502', 3, CAST(N'2020-03-06T16:25:02.800' AS DateTime), 4, CAST(N'2020-07-06T16:27:36.973' AS DateTime), N'CK20200706042736', 3, 1, 2, 1, CAST(29.90 AS Decimal(18, 2)), NULL)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (5, N'XS20200706042507', 3, CAST(N'2020-04-06T16:25:07.503' AS DateTime), 4, CAST(N'2020-07-06T16:27:34.187' AS DateTime), N'CK20200706042734', 3, 1, 3, 11, CAST(108.90 AS Decimal(18, 2)), NULL)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (6, N'XS20200706042513', 3, CAST(N'2020-04-06T16:25:13.367' AS DateTime), 4, CAST(N'2020-07-06T16:27:34.183' AS DateTime), N'CK20200706042734', 3, 1, 4, 2, CAST(399.80 AS Decimal(18, 2)), NULL)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (7, N'XS20200706042518', 3, CAST(N'2020-05-06T16:25:18.480' AS DateTime), 4, CAST(N'2020-07-06T16:27:34.183' AS DateTime), N'CK20200706042734', 3, 1, 5, 3, CAST(29.70 AS Decimal(18, 2)), NULL)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (8, N'XS20200706042522', 3, CAST(N'2020-05-06T16:25:22.357' AS DateTime), 4, CAST(N'2020-07-06T16:27:30.323' AS DateTime), N'CK20200706042730', 3, 1, 6, 4, CAST(480.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (9, N'XS20200706042551', 3, CAST(N'2020-06-06T16:25:51.583' AS DateTime), 4, CAST(N'2020-07-06T16:27:30.320' AS DateTime), N'CK20200706042730', 3, 1, 1, 36, CAST(356.40 AS Decimal(18, 2)), NULL)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (10, N'XS20200706042557', 3, CAST(N'2020-06-06T16:25:57.337' AS DateTime), 4, CAST(N'2020-07-06T16:27:30.320' AS DateTime), N'CK20200706042730', 3, 1, 2, 36, CAST(1076.40 AS Decimal(18, 2)), NULL)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (11, N'BH20200706042624', 1, CAST(N'2020-02-06T16:26:24.310' AS DateTime), 5, CAST(N'2020-07-06T16:28:37.597' AS DateTime), NULL, 1, 1, 2007, 1, CAST(29.90 AS Decimal(18, 2)), 1)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (12, N'BH20200706042628', 1, CAST(N'2020-02-06T16:26:28.503' AS DateTime), 5, CAST(N'2020-07-06T16:28:35.693' AS DateTime), NULL, 1, 1, 1008, 1, CAST(129.90 AS Decimal(18, 2)), 2)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (13, N'BH20200706042632', 1, CAST(N'2020-03-06T16:26:32.310' AS DateTime), 5, CAST(N'2020-07-06T16:28:34.587' AS DateTime), NULL, 1, 1, 7, 5, CAST(249.50 AS Decimal(18, 2)), 2)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (14, N'BH20200706042635', 1, CAST(N'2020-03-06T16:26:35.120' AS DateTime), 5, CAST(N'2020-07-06T16:28:33.467' AS DateTime), NULL, 1, 1, 6, 2, CAST(240.00 AS Decimal(18, 2)), 4)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (15, N'BH20200706042641', 1, CAST(N'2020-05-06T16:26:41.370' AS DateTime), 5, CAST(N'2020-07-06T16:28:32.067' AS DateTime), NULL, 1, 1, 5, 10, CAST(99.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (16, N'BH20200706042645', 1, CAST(N'2020-05-06T16:26:46.000' AS DateTime), 5, CAST(N'2020-07-06T16:28:30.517' AS DateTime), NULL, 1, 1, 4, 2, CAST(399.80 AS Decimal(18, 2)), 2)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (17, N'BH20200706042649', 1, CAST(N'2020-06-06T16:26:49.897' AS DateTime), 5, CAST(N'2020-07-06T16:28:29.157' AS DateTime), NULL, 1, 1, 3, 25, CAST(247.50 AS Decimal(18, 2)), 1)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (18, N'BH20200706042653', 1, CAST(N'2020-06-06T16:26:53.757' AS DateTime), 5, CAST(N'2020-07-06T16:28:27.380' AS DateTime), NULL, 1, 1, 2, 2, CAST(59.80 AS Decimal(18, 2)), 1)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (19, N'BH20200706042659', 1, CAST(N'2020-07-06T16:26:59.123' AS DateTime), 5, CAST(N'2020-07-06T16:28:25.823' AS DateTime), NULL, 1, 1, 1, 30, CAST(297.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (21, N'CG20200706042909', 6, CAST(N'2020-02-06T16:29:09.047' AS DateTime), 4, CAST(N'2020-07-06T16:29:22.410' AS DateTime), N'RK20200706042922', 2, 1, 2, 2, CAST(59.80 AS Decimal(18, 2)), 1)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (22, N'CG20200706042907', 6, CAST(N'2020-03-06T16:29:07.757' AS DateTime), 4, CAST(N'2020-07-06T16:29:22.410' AS DateTime), N'RK20200706042922', 2, 1, 3, 25, CAST(247.50 AS Decimal(18, 2)), 1)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (23, N'CG20200706042906', 6, CAST(N'2020-03-06T16:29:06.630' AS DateTime), 4, CAST(N'2020-07-06T16:29:22.407' AS DateTime), N'RK20200706042922', 2, 1, 4, 2, CAST(399.80 AS Decimal(18, 2)), 2)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (24, N'CG20200706042905', 6, CAST(N'2020-04-06T16:29:05.390' AS DateTime), 4, CAST(N'2020-07-06T16:29:22.407' AS DateTime), N'RK20200706042922', 2, 1, 5, 10, CAST(99.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (25, N'CG20200706042904', 6, CAST(N'2020-05-06T16:29:04.020' AS DateTime), 4, CAST(N'2020-07-06T16:29:22.403' AS DateTime), N'RK20200706042922', 2, 1, 6, 2, CAST(240.00 AS Decimal(18, 2)), 4)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (26, N'CG20200706042902', 6, CAST(N'2020-06-06T16:29:02.483' AS DateTime), 4, CAST(N'2020-07-06T16:29:19.087' AS DateTime), N'RK20200706042919', 2, 1, 7, 5, CAST(249.50 AS Decimal(18, 2)), 2)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (27, N'CG20200706042901', 6, CAST(N'2020-06-06T16:29:01.147' AS DateTime), 4, CAST(N'2020-07-06T16:29:19.083' AS DateTime), N'RK20200706042919', 2, 1, 1008, 1, CAST(129.90 AS Decimal(18, 2)), 2)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (28, N'CG20200706042858', 6, CAST(N'2020-06-06T16:28:58.690' AS DateTime), 4, CAST(N'2020-07-06T16:29:19.080' AS DateTime), N'RK20200706042919', 2, 1, 2007, 1, CAST(29.90 AS Decimal(18, 2)), 1)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (29, N'XS20200706051251', 3, CAST(N'2020-07-06T17:12:51.657' AS DateTime), 4, CAST(N'2020-07-06T17:12:57.630' AS DateTime), N'CK20200706051257', 3, 1, 3, 20, CAST(198.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (30, N'BH20200706051323', 1, CAST(N'2020-07-06T17:13:23.237' AS DateTime), 5, CAST(N'2020-07-06T17:13:27.887' AS DateTime), NULL, 1, 1, 4, 2, CAST(399.80 AS Decimal(18, 2)), 2)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (31, N'CG20200706051331', 6, CAST(N'2020-07-06T17:13:31.717' AS DateTime), 4, CAST(N'2020-07-06T17:13:38.307' AS DateTime), N'RK20200706051338', 2, 1, 4, 2, CAST(399.80 AS Decimal(18, 2)), 2)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (32, N'XS20200707031835', 1011, CAST(N'2020-07-07T15:18:35.703' AS DateTime), 1011, CAST(N'2020-07-07T15:33:49.643' AS DateTime), N'CK20200707033349', 3, 1, 1008, 2, CAST(259.80 AS Decimal(18, 2)), NULL)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (33, N'BH20200707033721', 2, CAST(N'2020-07-07T15:37:21.617' AS DateTime), 2, CAST(N'2020-07-07T15:37:45.377' AS DateTime), NULL, 1, 1, 7, 2, CAST(99.80 AS Decimal(18, 2)), 2)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (34, N'CG20200707033802', 2, CAST(N'2020-07-07T15:38:02.907' AS DateTime), 2, CAST(N'2020-07-07T15:39:33.583' AS DateTime), N'RK20200707033933', 2, 1, 7, 2, CAST(99.80 AS Decimal(18, 2)), 2)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (35, N'BH20200707033851', 2, CAST(N'2020-07-07T15:38:51.083' AS DateTime), 2, CAST(N'2020-07-07T15:38:56.133' AS DateTime), NULL, 1, 1, 5, 2, CAST(19.80 AS Decimal(18, 2)), 1)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (36, N'CG20200707033918', 2, CAST(N'2020-07-07T15:39:18.653' AS DateTime), 2, CAST(N'2020-07-07T15:39:33.580' AS DateTime), N'RK20200707033933', 2, 1, 5, 2, CAST(19.80 AS Decimal(18, 2)), 1)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (1032, N'XS20200708082558', 2, CAST(N'2020-07-08T08:25:58.397' AS DateTime), 2, CAST(N'2020-07-08T08:27:07.970' AS DateTime), N'CK20200708082707', 3, 1, 1008, 8, CAST(1039.20 AS Decimal(18, 2)), NULL)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (1033, N'XS20200708082643', 2, CAST(N'2020-07-08T08:26:43.413' AS DateTime), 2, CAST(N'2020-07-08T08:27:07.963' AS DateTime), N'CK20200708082707', 3, 1, 1008, 8, CAST(1039.20 AS Decimal(18, 2)), NULL)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (1034, N'XS20200708084653', 2, CAST(N'2020-07-08T08:46:53.447' AS DateTime), 2, CAST(N'2020-07-08T08:51:31.253' AS DateTime), N'CK20200708085131', 3, 1, 2007, 12, CAST(358.80 AS Decimal(18, 2)), NULL)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (1035, N'XS20200708085121', 2, CAST(N'2020-07-08T08:51:21.640' AS DateTime), 2, CAST(N'2020-07-08T08:51:29.430' AS DateTime), N'CK20200708085129', 3, 1, 2007, 12, CAST(358.80 AS Decimal(18, 2)), NULL)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (1036, N'XS20200708085448', 2, CAST(N'2020-07-08T08:54:48.623' AS DateTime), NULL, NULL, NULL, 3, 0, 2007, 11, CAST(328.90 AS Decimal(18, 2)), NULL)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (1037, N'XS20200708085451', 2, CAST(N'2020-07-08T08:54:51.623' AS DateTime), 2, CAST(N'2020-07-08T08:54:58.407' AS DateTime), N'CK20200708085458', 3, 1, 2007, 11, CAST(328.90 AS Decimal(18, 2)), NULL)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (1038, N'XS20200708085717', 2, CAST(N'2020-07-08T08:57:17.003' AS DateTime), 2, CAST(N'2020-07-08T09:00:01.997' AS DateTime), N'CK20200708090001', 3, 1, 2007, 11, CAST(328.90 AS Decimal(18, 2)), NULL)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (1039, N'XS20200708090517', 2, CAST(N'2020-07-08T09:05:17.480' AS DateTime), NULL, NULL, NULL, 3, 0, 5, 10, CAST(99.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (1040, N'XS20200708090519', 2, CAST(N'2020-07-08T09:05:19.233' AS DateTime), 2, CAST(N'2020-07-08T09:05:26.703' AS DateTime), N'CK20200708090526', 3, 1, 5, 10, CAST(99.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (1041, N'XS20200708090656', 2, CAST(N'2020-07-08T09:06:56.780' AS DateTime), NULL, NULL, NULL, 3, 0, 5, 10, CAST(99.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (1042, N'XS20200708021650', 2, CAST(N'2020-07-08T14:16:50.923' AS DateTime), NULL, NULL, NULL, 3, 0, 1, 10, CAST(99.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (1043, N'BH20200708021706', 2, CAST(N'2020-07-08T14:17:06.497' AS DateTime), 2, CAST(N'2020-07-08T14:17:12.377' AS DateTime), NULL, 1, 1, 7, 20, CAST(998.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (1044, N'CG20200708021823', 2, CAST(N'2020-07-08T14:18:23.347' AS DateTime), 2, CAST(N'2020-07-08T14:18:33.260' AS DateTime), N'RK20200708021833', 2, 1, 7, 20, CAST(998.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (1045, N'BH20200708024305', 2, CAST(N'2020-07-08T14:43:05.747' AS DateTime), NULL, NULL, NULL, 1, 0, 2011, 555555555, CAST(30677777747.10 AS Decimal(18, 2)), 1)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (1046, N'BH20200708024415', 2, CAST(N'2020-07-08T14:44:15.790' AS DateTime), 2, CAST(N'2020-07-08T14:44:21.530' AS DateTime), NULL, 1, 1, 2011, 10, CAST(552.20 AS Decimal(18, 2)), 1)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (1047, N'CG20200708024424', 2, CAST(N'2020-07-08T14:44:24.917' AS DateTime), 2, CAST(N'2020-07-08T14:44:29.440' AS DateTime), N'RK20200708024429', 2, 1, 2011, 10, CAST(552.20 AS Decimal(18, 2)), 1)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (1048, N'BH20200708024655', 2, CAST(N'2020-07-08T14:46:55.683' AS DateTime), 2, CAST(N'2020-07-08T14:47:00.380' AS DateTime), NULL, 1, 1, 2011, 20, CAST(1104.40 AS Decimal(18, 2)), 1)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (1049, N'CG20200708024704', 2, CAST(N'2020-07-08T14:47:04.283' AS DateTime), 2, CAST(N'2020-07-08T14:47:07.413' AS DateTime), N'RK20200708024707', 2, 1, 2011, 20, CAST(1104.40 AS Decimal(18, 2)), 1)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (1050, N'BH20200708111928', 2, CAST(N'2020-07-08T23:19:28.910' AS DateTime), NULL, NULL, NULL, 1, 0, 2015, 20, CAST(400.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (1051, N'XS20200708113616', 2, CAST(N'2020-07-08T23:36:16.880' AS DateTime), NULL, NULL, NULL, 3, 0, 1, 20, CAST(198.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (1052, N'BH20200913073919', 2, CAST(N'2020-09-13T19:39:19.247' AS DateTime), 2, CAST(N'2020-09-13T19:43:02.363' AS DateTime), NULL, 1, 1, 2008, 50, CAST(500.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[order_model] ([order_id], [order_num], [creater], [create_time], [checker], [end_time], [OutIn_number], [order_type_id], [order_state], [product_id], [total_num], [total_price], [supplier_id]) VALUES (1053, NULL, NULL, NULL, NULL, NULL, NULL, 2, 0, 2008, 50, CAST(500.00 AS Decimal(18, 2)), 1)
SET IDENTITY_INSERT [dbo].[order_model] OFF
SET IDENTITY_INSERT [dbo].[order_type] ON 

INSERT [dbo].[order_type] ([order_type_id], [order_type_name]) VALUES (1, N'采购申请单')
INSERT [dbo].[order_type] ([order_type_id], [order_type_name]) VALUES (2, N'采购单')
INSERT [dbo].[order_type] ([order_type_id], [order_type_name]) VALUES (3, N'销售单')
SET IDENTITY_INSERT [dbo].[order_type] OFF
SET IDENTITY_INSERT [dbo].[product] ON 

INSERT [dbo].[product] ([product_id], [product_type_id], [product_name], [producer], [unit], [in_price], [pro_Inventory], [Shelves]) VALUES (1, 1, N'2020年新款男士休闲袜', N'晋江总部厂', N'双', CAST(9.90 AS Decimal(18, 2)), 157, 1)
INSERT [dbo].[product] ([product_id], [product_type_id], [product_name], [producer], [unit], [in_price], [pro_Inventory], [Shelves]) VALUES (2, 2, N'2020年新款男士休闲七分短裤', N'晋江总部厂', N'条', CAST(29.90 AS Decimal(18, 2)), 160, 0)
INSERT [dbo].[product] ([product_id], [product_type_id], [product_name], [producer], [unit], [in_price], [pro_Inventory], [Shelves]) VALUES (3, 1, N'2020新款短袜', N'福建晋江', N'双', CAST(9.90 AS Decimal(18, 2)), 128, 1)
INSERT [dbo].[product] ([product_id], [product_type_id], [product_name], [producer], [unit], [in_price], [pro_Inventory], [Shelves]) VALUES (4, 3, N'2020年新款女士休闲上衣', N'晋江总部厂', N'件', CAST(199.90 AS Decimal(18, 2)), 9, 1)
INSERT [dbo].[product] ([product_id], [product_type_id], [product_name], [producer], [unit], [in_price], [pro_Inventory], [Shelves]) VALUES (5, 1, N'2020年新款男士运动袜', N'晋江总部厂', N'双', CAST(9.90 AS Decimal(18, 2)), 19, 0)
INSERT [dbo].[product] ([product_id], [product_type_id], [product_name], [producer], [unit], [in_price], [pro_Inventory], [Shelves]) VALUES (6, 6, N'2020年春季新款男士休闲网鞋', N'晋江总部厂', N'双', CAST(120.00 AS Decimal(18, 2)), 144, 1)
INSERT [dbo].[product] ([product_id], [product_type_id], [product_name], [producer], [unit], [in_price], [pro_Inventory], [Shelves]) VALUES (7, 3, N'2020年潮牌T恤', N'长沙总部厂', N'件', CAST(49.90 AS Decimal(18, 2)), 29, 1)
INSERT [dbo].[product] ([product_id], [product_type_id], [product_name], [producer], [unit], [in_price], [pro_Inventory], [Shelves]) VALUES (1008, 4, N'2020安踏新款拖鞋', N'浙江厂', N'双', CAST(129.90 AS Decimal(18, 2)), 8, 1)
INSERT [dbo].[product] ([product_id], [product_type_id], [product_name], [producer], [unit], [in_price], [pro_Inventory], [Shelves]) VALUES (2007, 2, N'2020新款皮卡丘内裤', N'长沙总部厂', N'条', CAST(29.90 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[product] ([product_id], [product_type_id], [product_name], [producer], [unit], [in_price], [pro_Inventory], [Shelves]) VALUES (2008, 1, N'k', N'jhj ', N'件', CAST(10.00 AS Decimal(18, 2)), 0, 0)
INSERT [dbo].[product] ([product_id], [product_type_id], [product_name], [producer], [unit], [in_price], [pro_Inventory], [Shelves]) VALUES (2009, 1, N'层', N'的', N'件', CAST(10.20 AS Decimal(18, 2)), 0, 0)
INSERT [dbo].[product] ([product_id], [product_type_id], [product_name], [producer], [unit], [in_price], [pro_Inventory], [Shelves]) VALUES (2010, 1, N'层', N'的', N'件', CAST(0.30 AS Decimal(18, 2)), 0, 0)
INSERT [dbo].[product] ([product_id], [product_type_id], [product_name], [producer], [unit], [in_price], [pro_Inventory], [Shelves]) VALUES (2011, 1, N'层', N'的', N'1000', CAST(55.22 AS Decimal(18, 2)), 30, 0)
INSERT [dbo].[product] ([product_id], [product_type_id], [product_name], [producer], [unit], [in_price], [pro_Inventory], [Shelves]) VALUES (2015, 1, N'1', N'1', N'1', CAST(20.00 AS Decimal(18, 2)), 0, 0)
SET IDENTITY_INSERT [dbo].[product] OFF
SET IDENTITY_INSERT [dbo].[product_type] ON 

INSERT [dbo].[product_type] ([product_type_id], [supplier_id], [product_type_name]) VALUES (1, 1, N'袜子')
INSERT [dbo].[product_type] ([product_type_id], [supplier_id], [product_type_name]) VALUES (2, 1, N'短裤')
INSERT [dbo].[product_type] ([product_type_id], [supplier_id], [product_type_name]) VALUES (3, 2, N'上衣')
INSERT [dbo].[product_type] ([product_type_id], [supplier_id], [product_type_name]) VALUES (4, 2, N'鞋（安踏）')
INSERT [dbo].[product_type] ([product_type_id], [supplier_id], [product_type_name]) VALUES (5, 1, N'裤子')
INSERT [dbo].[product_type] ([product_type_id], [supplier_id], [product_type_name]) VALUES (6, 4, N'鞋（鸿星尔克）')
INSERT [dbo].[product_type] ([product_type_id], [supplier_id], [product_type_name]) VALUES (13, 9, N'裙子')
INSERT [dbo].[product_type] ([product_type_id], [supplier_id], [product_type_name]) VALUES (14, 3, N'袜子')
INSERT [dbo].[product_type] ([product_type_id], [supplier_id], [product_type_name]) VALUES (15, 5, N'短裤')
INSERT [dbo].[product_type] ([product_type_id], [supplier_id], [product_type_name]) VALUES (22, 14, N'd')
INSERT [dbo].[product_type] ([product_type_id], [supplier_id], [product_type_name]) VALUES (23, 14, N'du')
INSERT [dbo].[product_type] ([product_type_id], [supplier_id], [product_type_name]) VALUES (24, 14, N'g')
SET IDENTITY_INSERT [dbo].[product_type] OFF
SET IDENTITY_INSERT [dbo].[relation_emp_menu] ON 

INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (1, 2, 1)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (2, 2, 101)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (4, 2, 103)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (5, 2, 2)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (6, 2, 201)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (7, 2, 202)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (8, 2, 203)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (9, 2, 204)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (10, 2, 3)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (11, 2, 301)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (12, 2, 302)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (13, 2, 303)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (14, 2, 304)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (15, 2, 4)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (16, 2, 401)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (17, 2, 402)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (18, 2, 403)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (19, 2, 5)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (20, 2, 501)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (21, 2, 502)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (22, 2, 503)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (23, 1, 2)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (24, 1, 201)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (25, 1, 203)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (26, 1, 204)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (27, 1, 5)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (28, 1, 501)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (29, 1, 502)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (30, 3, 2)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (31, 3, 201)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (32, 3, 202)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (33, 4, 3)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (34, 4, 301)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (35, 4, 302)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (36, 4, 303)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (37, 4, 304)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (38, 5, 4)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (39, 5, 401)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (40, 5, 402)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (41, 5, 403)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (42, 5, 5)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (43, 5, 503)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (44, 6, 4)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (45, 6, 401)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (46, 7, 4)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (47, 7, 401)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (48, 8, 2)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (49, 8, 201)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (50, 8, 203)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (51, 8, 204)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (52, 8, 4)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (53, 8, 401)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (54, 8, 402)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (1033, 1011, 2)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (1034, 1011, 201)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (1035, 1011, 202)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (1036, 1011, 301)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (1037, 1011, 302)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (1038, 1011, 3)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (1044, 1012, 1)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (1045, 1012, 101)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (1046, 1012, 103)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (1047, 1012, 2)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (1048, 1012, 201)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (1049, 1012, 202)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (1050, 1012, 203)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (1051, 1012, 204)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (1052, 2, 404)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (1053, 1014, 4)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (1054, 1014, 401)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (1055, 1014, 402)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (1056, 1014, 403)
INSERT [dbo].[relation_emp_menu] ([relation_id], [emp_id], [menu_id]) VALUES (1057, 1014, 404)
SET IDENTITY_INSERT [dbo].[relation_emp_menu] OFF
SET IDENTITY_INSERT [dbo].[relation_role_menu] ON 

INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (1, 1, 1)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (2, 1, 101)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (4, 1, 103)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (5, 1, 2)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (6, 1, 201)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (7, 1, 202)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (8, 1, 203)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (9, 1, 3)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (10, 1, 301)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (11, 1, 4)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (12, 1, 401)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (13, 1, 402)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (14, 2, 2)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (15, 2, 201)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (16, 2, 203)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (17, 3, 2)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (18, 3, 201)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (19, 3, 202)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (20, 4, 3)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (21, 4, 301)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (22, 5, 4)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (23, 5, 401)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (24, 5, 402)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (25, 6, 4)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (26, 6, 401)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (36, 2, 204)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (1036, 1, 204)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (1037, 1, 403)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (1038, 5, 403)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (1039, 4, 302)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (1040, 4, 303)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (1042, 1, 302)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (1043, 1, 303)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (1044, 1, 304)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (1045, 4, 304)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (1046, 1, 5)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (1047, 1, 501)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (1048, 1, 502)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (1049, 1, 503)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (1050, 2, 5)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (1051, 2, 501)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (1052, 2, 502)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (1053, 5, 5)
INSERT [dbo].[relation_role_menu] ([relation_id], [role_id], [menu_id]) VALUES (1054, 5, 503)
SET IDENTITY_INSERT [dbo].[relation_role_menu] OFF
SET IDENTITY_INSERT [dbo].[role] ON 

INSERT [dbo].[role] ([role_id], [role_name], [dep_id]) VALUES (1, N'系统管理员', 1)
INSERT [dbo].[role] ([role_id], [role_name], [dep_id]) VALUES (2, N'销售经理', 9)
INSERT [dbo].[role] ([role_id], [role_name], [dep_id]) VALUES (3, N'销售员', 9)
INSERT [dbo].[role] ([role_id], [role_name], [dep_id]) VALUES (4, N'仓管员', 5)
INSERT [dbo].[role] ([role_id], [role_name], [dep_id]) VALUES (5, N'采购经理', 3)
INSERT [dbo].[role] ([role_id], [role_name], [dep_id]) VALUES (6, N'采购员', 3)
INSERT [dbo].[role] ([role_id], [role_name], [dep_id]) VALUES (7, N'临时管理', NULL)
SET IDENTITY_INSERT [dbo].[role] OFF
SET IDENTITY_INSERT [dbo].[store] ON 

INSERT [dbo].[store] ([store_id], [store_name], [address]) VALUES (1, N'服装仓库', N'北京亦庄园')
INSERT [dbo].[store] ([store_id], [store_name], [address]) VALUES (2, N'电子仓库', N'北京中关村')
INSERT [dbo].[store] ([store_id], [store_name], [address]) VALUES (3, N'食品仓库', N'北京回龙观')
INSERT [dbo].[store] ([store_id], [store_name], [address]) VALUES (4, N'贵金属仓库', N'北京朝阳区')
INSERT [dbo].[store] ([store_id], [store_name], [address]) VALUES (5, N'服装仓库', N'长春高新区')
SET IDENTITY_INSERT [dbo].[store] OFF
SET IDENTITY_INSERT [dbo].[store_log] ON 

INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (1, 2, 10, CAST(N'2020-07-06T16:27:30.320' AS DateTime), 36, 3)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (2, 2, 9, CAST(N'2020-07-06T16:27:30.320' AS DateTime), 36, 3)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (3, 2, 8, CAST(N'2020-07-06T16:27:30.323' AS DateTime), 4, 3)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (4, 2, 7, CAST(N'2020-07-06T16:27:34.183' AS DateTime), 3, 3)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (5, 2, 6, CAST(N'2020-07-06T16:27:34.183' AS DateTime), 2, 3)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (6, 2, 5, CAST(N'2020-07-06T16:27:34.187' AS DateTime), 11, 3)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (7, 2, 4, CAST(N'2020-07-06T16:27:36.973' AS DateTime), 1, 3)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (8, 2, 3, CAST(N'2020-07-06T16:27:36.973' AS DateTime), 2, 3)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (9, 2, 2, CAST(N'2020-07-06T16:27:36.977' AS DateTime), 33, 3)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (10, 2, 1, CAST(N'2020-07-06T16:27:36.977' AS DateTime), 33, 3)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (11, 2, 28, CAST(N'2020-07-06T16:29:19.083' AS DateTime), 1, 2)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (12, 2, 27, CAST(N'2020-07-06T16:29:19.083' AS DateTime), 1, 2)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (13, 2, 26, CAST(N'2020-07-06T16:29:19.087' AS DateTime), 5, 2)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (14, 2, 25, CAST(N'2020-07-06T16:29:22.407' AS DateTime), 2, 2)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (15, 2, 24, CAST(N'2020-07-06T16:29:22.407' AS DateTime), 10, 2)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (16, 2, 23, CAST(N'2020-07-06T16:29:22.410' AS DateTime), 2, 2)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (17, 2, 22, CAST(N'2020-07-06T16:29:22.410' AS DateTime), 25, 2)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (18, 2, 21, CAST(N'2020-07-06T16:29:22.410' AS DateTime), 2, 2)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (19, 2, 29, CAST(N'2020-07-06T17:12:57.630' AS DateTime), 20, 3)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (20, 2, 31, CAST(N'2020-07-06T17:13:38.307' AS DateTime), 2, 2)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (21, 1011, 32, CAST(N'2020-07-07T15:33:49.647' AS DateTime), 2, 3)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (22, 2, 36, CAST(N'2020-07-07T15:39:33.580' AS DateTime), 2, 2)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (23, 2, 34, CAST(N'2020-07-07T15:39:33.583' AS DateTime), 2, 2)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (1021, 2, 1033, CAST(N'2020-07-08T08:27:07.967' AS DateTime), 8, 3)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (1022, 2, 1032, CAST(N'2020-07-08T08:27:07.970' AS DateTime), 8, 3)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (1023, 2, 1035, CAST(N'2020-07-08T08:51:29.430' AS DateTime), 12, 3)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (1024, 2, 1034, CAST(N'2020-07-08T08:51:31.253' AS DateTime), 12, 3)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (1025, 2, 1037, CAST(N'2020-07-08T08:55:07.317' AS DateTime), 11, 3)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (1026, 2, 1038, CAST(N'2020-07-08T09:00:07.210' AS DateTime), 11, 3)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (1027, 2, 1040, CAST(N'2020-07-08T09:05:26.703' AS DateTime), 10, 3)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (1028, 2, 1044, CAST(N'2020-07-08T14:18:33.263' AS DateTime), 20, 2)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (1029, 2, 1047, CAST(N'2020-07-08T14:44:29.440' AS DateTime), 10, 2)
INSERT [dbo].[store_log] ([log_id], [emp_id], [order_id], [oper_time], [num], [order_type_id]) VALUES (1030, 2, 1049, CAST(N'2020-07-08T14:47:07.413' AS DateTime), 20, 2)
SET IDENTITY_INSERT [dbo].[store_log] OFF
SET IDENTITY_INSERT [dbo].[supplier] ON 

INSERT [dbo].[supplier] ([supplier_id], [supplier_name], [address], [contact], [tel]) VALUES (1, N'福建七匹狼实业股份有限公司', N'江苏', N'李大明', N'400-8868677')
INSERT [dbo].[supplier] ([supplier_id], [supplier_name], [address], [contact], [tel]) VALUES (2, N'安踏体育用品有限公司', N'福建', N'张全德', N'86-595 8592 9999')
INSERT [dbo].[supplier] ([supplier_id], [supplier_name], [address], [contact], [tel]) VALUES (3, N'李宁（中国）体育用品有限公司', N'北京', N'李建', N'010-80800808')
INSERT [dbo].[supplier] ([supplier_id], [supplier_name], [address], [contact], [tel]) VALUES (4, N'鸿星尔克（中国）体育用品有限公司', N'福建', N'张大鹏', N'18244016623')
INSERT [dbo].[supplier] ([supplier_id], [supplier_name], [address], [contact], [tel]) VALUES (5, N'鸿星尔克（中国）体育用品有限公司', N'北京', N'李大刀', N'18244014432')
INSERT [dbo].[supplier] ([supplier_id], [supplier_name], [address], [contact], [tel]) VALUES (9, N'鸿星尔克体育用品有限公司', N'长沙', N'刘海', N'17673467451')
INSERT [dbo].[supplier] ([supplier_id], [supplier_name], [address], [contact], [tel]) VALUES (14, N'sfdg', N'dfvb ', N'fcb', N'17673467415')
SET IDENTITY_INSERT [dbo].[supplier] OFF
ALTER TABLE [dbo].[dep] ADD  DEFAULT (NULL) FOR [dep_name]
GO
ALTER TABLE [dbo].[dep] ADD  DEFAULT (NULL) FOR [tel]
GO
ALTER TABLE [dbo].[emp] ADD  CONSTRAINT [DF__emp__dep_id__5629CD9C]  DEFAULT (NULL) FOR [role_id]
GO
ALTER TABLE [dbo].[emp] ADD  CONSTRAINT [DF__emp__name__571DF1D5]  DEFAULT (NULL) FOR [emp_name]
GO
ALTER TABLE [dbo].[emp] ADD  CONSTRAINT [DF__emp__username__5812160E]  DEFAULT (NULL) FOR [username]
GO
ALTER TABLE [dbo].[emp] ADD  CONSTRAINT [DF__emp__email__59063A47]  DEFAULT (NULL) FOR [email]
GO
ALTER TABLE [dbo].[emp] ADD  CONSTRAINT [DF__emp__tel__59FA5E80]  DEFAULT (NULL) FOR [tel]
GO
ALTER TABLE [dbo].[emp] ADD  CONSTRAINT [DF__emp__gender__5AEE82B9]  DEFAULT (NULL) FOR [gender]
GO
ALTER TABLE [dbo].[emp] ADD  CONSTRAINT [DF__emp__address__5BE2A6F2]  DEFAULT (NULL) FOR [address]
GO
ALTER TABLE [dbo].[emp] ADD  CONSTRAINT [DF__emp__birthday__5CD6CB2B]  DEFAULT (NULL) FOR [birthday]
GO
ALTER TABLE [dbo].[emp] ADD  CONSTRAINT [DF__emp__password__5DCAEF64]  DEFAULT (NULL) FOR [password]
GO
ALTER TABLE [dbo].[emp] ADD  CONSTRAINT [DF_emp_IsFrozen]  DEFAULT ('true') FOR [IsFrozen]
GO
ALTER TABLE [dbo].[menu] ADD  DEFAULT (NULL) FOR [menu_id]
GO
ALTER TABLE [dbo].[menu] ADD  DEFAULT (NULL) FOR [parent_menu_id]
GO
ALTER TABLE [dbo].[menu] ADD  DEFAULT (NULL) FOR [menu_name]
GO
ALTER TABLE [dbo].[menu] ADD  DEFAULT (NULL) FOR [url]
GO
ALTER TABLE [dbo].[order_detail] ADD  CONSTRAINT [DF__order_det__detai__628FA481]  DEFAULT (NULL) FOR [detail_num]
GO
ALTER TABLE [dbo].[order_detail] ADD  CONSTRAINT [DF__order_det__detai__6383C8BA]  DEFAULT (NULL) FOR [detail_price]
GO
ALTER TABLE [dbo].[order_detail] ADD  CONSTRAINT [DF__order_det__produ__6477ECF3]  DEFAULT (NULL) FOR [product_id]
GO
ALTER TABLE [dbo].[order_detail] ADD  CONSTRAINT [DF__order_det__order__656C112C]  DEFAULT (NULL) FOR [order_id]
GO
ALTER TABLE [dbo].[order_model] ADD  CONSTRAINT [DF__order_mod__order__6754599E]  DEFAULT (NULL) FOR [order_num]
GO
ALTER TABLE [dbo].[order_model] ADD  CONSTRAINT [DF__order_mod__creat__68487DD7]  DEFAULT (NULL) FOR [creater]
GO
ALTER TABLE [dbo].[order_model] ADD  CONSTRAINT [DF__order_mod__creat__693CA210]  DEFAULT (NULL) FOR [create_time]
GO
ALTER TABLE [dbo].[order_model] ADD  CONSTRAINT [DF__order_mod__check__6A30C649]  DEFAULT (NULL) FOR [checker]
GO
ALTER TABLE [dbo].[order_model] ADD  CONSTRAINT [DF__order_mod__end_t__6D0D32F4]  DEFAULT (NULL) FOR [end_time]
GO
ALTER TABLE [dbo].[order_model] ADD  CONSTRAINT [DF__order_mod__order__6E01572D]  DEFAULT (NULL) FOR [order_type_id]
GO
ALTER TABLE [dbo].[order_model] ADD  CONSTRAINT [DF__order_mod__order__6EF57B66]  DEFAULT (NULL) FOR [order_state]
GO
ALTER TABLE [dbo].[order_model] ADD  CONSTRAINT [DF__order_mod__total__6FE99F9F]  DEFAULT (NULL) FOR [total_num]
GO
ALTER TABLE [dbo].[order_model] ADD  CONSTRAINT [DF__order_mod__total__70DDC3D8]  DEFAULT (NULL) FOR [total_price]
GO
ALTER TABLE [dbo].[order_model] ADD  CONSTRAINT [DF__order_mod__suppl__71D1E811]  DEFAULT (NULL) FOR [supplier_id]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF__product__product__72C60C4A]  DEFAULT (NULL) FOR [product_type_id]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF__product__name__73BA3083]  DEFAULT (NULL) FOR [product_name]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF__product__produce__75A278F5]  DEFAULT (NULL) FOR [producer]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF__product__unit__76969D2E]  DEFAULT (NULL) FOR [unit]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF__product__in_pric__778AC167]  DEFAULT (NULL) FOR [in_price]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF__product__out_pri__787EE5A0]  DEFAULT ('0') FOR [pro_Inventory]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF_product_Shelves]  DEFAULT ('true') FOR [Shelves]
GO
ALTER TABLE [dbo].[product_type] ADD  DEFAULT (NULL) FOR [supplier_id]
GO
ALTER TABLE [dbo].[product_type] ADD  DEFAULT (NULL) FOR [product_type_name]
GO
ALTER TABLE [dbo].[relation_role_menu] ADD  DEFAULT (NULL) FOR [role_id]
GO
ALTER TABLE [dbo].[relation_role_menu] ADD  DEFAULT (NULL) FOR [menu_id]
GO
ALTER TABLE [dbo].[role] ADD  CONSTRAINT [DF__role__name__02FC7413]  DEFAULT (NULL) FOR [role_name]
GO
ALTER TABLE [dbo].[role] ADD  CONSTRAINT [DF__role__code__03F0984C]  DEFAULT (NULL) FOR [dep_id]
GO
ALTER TABLE [dbo].[store] ADD  DEFAULT (NULL) FOR [store_name]
GO
ALTER TABLE [dbo].[store] ADD  DEFAULT (NULL) FOR [address]
GO
ALTER TABLE [dbo].[store_detail] ADD  DEFAULT (NULL) FOR [store_id]
GO
ALTER TABLE [dbo].[store_detail] ADD  DEFAULT (NULL) FOR [product_id]
GO
ALTER TABLE [dbo].[store_detail] ADD  DEFAULT (NULL) FOR [num]
GO
ALTER TABLE [dbo].[store_log] ADD  CONSTRAINT [DF__store_log__emp_i__0A9D95DB]  DEFAULT (NULL) FOR [emp_id]
GO
ALTER TABLE [dbo].[store_log] ADD  CONSTRAINT [DF__store_log__order__0B91BA14]  DEFAULT (NULL) FOR [order_id]
GO
ALTER TABLE [dbo].[store_log] ADD  CONSTRAINT [DF__store_log__oper___0D7A0286]  DEFAULT (NULL) FOR [oper_time]
GO
ALTER TABLE [dbo].[store_log] ADD  CONSTRAINT [DF__store_log__num__0E6E26BF]  DEFAULT (NULL) FOR [num]
GO
ALTER TABLE [dbo].[store_log] ADD  CONSTRAINT [DF__store_log__type__0F624AF8]  DEFAULT (NULL) FOR [order_type_id]
GO
ALTER TABLE [dbo].[supplier] ADD  DEFAULT (NULL) FOR [supplier_name]
GO
ALTER TABLE [dbo].[supplier] ADD  DEFAULT (NULL) FOR [address]
GO
ALTER TABLE [dbo].[supplier] ADD  DEFAULT (NULL) FOR [contact]
GO
ALTER TABLE [dbo].[supplier] ADD  DEFAULT (NULL) FOR [tel]
GO
ALTER TABLE [dbo].[emp]  WITH CHECK ADD  CONSTRAINT [FK_emp_role] FOREIGN KEY([role_id])
REFERENCES [dbo].[role] ([role_id])
GO
ALTER TABLE [dbo].[emp] CHECK CONSTRAINT [FK_emp_role]
GO
ALTER TABLE [dbo].[order_detail]  WITH CHECK ADD  CONSTRAINT [FK_order_detail_order_type] FOREIGN KEY([order_type_id])
REFERENCES [dbo].[order_type] ([order_type_id])
GO
ALTER TABLE [dbo].[order_detail] CHECK CONSTRAINT [FK_order_detail_order_type]
GO
ALTER TABLE [dbo].[order_detail]  WITH CHECK ADD  CONSTRAINT [FK_order_detail_product] FOREIGN KEY([product_id])
REFERENCES [dbo].[product] ([product_id])
GO
ALTER TABLE [dbo].[order_detail] CHECK CONSTRAINT [FK_order_detail_product]
GO
ALTER TABLE [dbo].[order_model]  WITH CHECK ADD  CONSTRAINT [FK_order_model_emp] FOREIGN KEY([creater])
REFERENCES [dbo].[emp] ([emp_id])
GO
ALTER TABLE [dbo].[order_model] CHECK CONSTRAINT [FK_order_model_emp]
GO
ALTER TABLE [dbo].[order_model]  WITH CHECK ADD  CONSTRAINT [FK_order_model_emp1] FOREIGN KEY([checker])
REFERENCES [dbo].[emp] ([emp_id])
GO
ALTER TABLE [dbo].[order_model] CHECK CONSTRAINT [FK_order_model_emp1]
GO
ALTER TABLE [dbo].[order_model]  WITH CHECK ADD  CONSTRAINT [FK_order_model_order_type] FOREIGN KEY([order_type_id])
REFERENCES [dbo].[order_type] ([order_type_id])
GO
ALTER TABLE [dbo].[order_model] CHECK CONSTRAINT [FK_order_model_order_type]
GO
ALTER TABLE [dbo].[order_model]  WITH CHECK ADD  CONSTRAINT [FK_order_model_product] FOREIGN KEY([product_id])
REFERENCES [dbo].[product] ([product_id])
GO
ALTER TABLE [dbo].[order_model] CHECK CONSTRAINT [FK_order_model_product]
GO
ALTER TABLE [dbo].[order_model]  WITH CHECK ADD  CONSTRAINT [FK_order_model_supplier] FOREIGN KEY([supplier_id])
REFERENCES [dbo].[supplier] ([supplier_id])
GO
ALTER TABLE [dbo].[order_model] CHECK CONSTRAINT [FK_order_model_supplier]
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD  CONSTRAINT [FK_product_product_type] FOREIGN KEY([product_type_id])
REFERENCES [dbo].[product_type] ([product_type_id])
GO
ALTER TABLE [dbo].[product] CHECK CONSTRAINT [FK_product_product_type]
GO
ALTER TABLE [dbo].[product_type]  WITH CHECK ADD  CONSTRAINT [FK_product_type_supplier] FOREIGN KEY([supplier_id])
REFERENCES [dbo].[supplier] ([supplier_id])
GO
ALTER TABLE [dbo].[product_type] CHECK CONSTRAINT [FK_product_type_supplier]
GO
ALTER TABLE [dbo].[relation_role_menu]  WITH CHECK ADD  CONSTRAINT [FK_relation_role_menu_menu] FOREIGN KEY([menu_id])
REFERENCES [dbo].[menu] ([menu_id])
GO
ALTER TABLE [dbo].[relation_role_menu] CHECK CONSTRAINT [FK_relation_role_menu_menu]
GO
ALTER TABLE [dbo].[relation_role_menu]  WITH CHECK ADD  CONSTRAINT [FK_relation_role_menu_role] FOREIGN KEY([role_id])
REFERENCES [dbo].[role] ([role_id])
GO
ALTER TABLE [dbo].[relation_role_menu] CHECK CONSTRAINT [FK_relation_role_menu_role]
GO
ALTER TABLE [dbo].[role]  WITH CHECK ADD  CONSTRAINT [FK_role_dep] FOREIGN KEY([dep_id])
REFERENCES [dbo].[dep] ([dep_id])
GO
ALTER TABLE [dbo].[role] CHECK CONSTRAINT [FK_role_dep]
GO
ALTER TABLE [dbo].[store_detail]  WITH CHECK ADD  CONSTRAINT [FK_store_detail_product] FOREIGN KEY([product_id])
REFERENCES [dbo].[product] ([product_id])
GO
ALTER TABLE [dbo].[store_detail] CHECK CONSTRAINT [FK_store_detail_product]
GO
ALTER TABLE [dbo].[store_detail]  WITH CHECK ADD  CONSTRAINT [FK_store_detail_store] FOREIGN KEY([store_id])
REFERENCES [dbo].[store] ([store_id])
GO
ALTER TABLE [dbo].[store_detail] CHECK CONSTRAINT [FK_store_detail_store]
GO
ALTER TABLE [dbo].[store_log]  WITH CHECK ADD  CONSTRAINT [FK_store_log_emp] FOREIGN KEY([emp_id])
REFERENCES [dbo].[emp] ([emp_id])
GO
ALTER TABLE [dbo].[store_log] CHECK CONSTRAINT [FK_store_log_emp]
GO
ALTER TABLE [dbo].[store_log]  WITH CHECK ADD  CONSTRAINT [FK_store_log_order_model] FOREIGN KEY([order_id])
REFERENCES [dbo].[order_model] ([order_id])
GO
ALTER TABLE [dbo].[store_log] CHECK CONSTRAINT [FK_store_log_order_model]
GO
ALTER TABLE [dbo].[store_log]  WITH CHECK ADD  CONSTRAINT [FK_store_log_order_type] FOREIGN KEY([order_type_id])
REFERENCES [dbo].[order_type] ([order_type_id])
GO
ALTER TABLE [dbo].[store_log] CHECK CONSTRAINT [FK_store_log_order_type]
GO
/****** Object:  StoredProcedure [dbo].[proc_SelAll]    Script Date: 2020/9/17 11:39:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[proc_SelAll]
as
select 
* 
from order_model om,emp e,product p,order_type ot,supplier s 
where om.creater=e.emp_id and
om.checker=e.emp_id and
om.order_type_id=ot.order_type_id and
om.supplier_id=s.supplier_id 
GO
USE [master]
GO
ALTER DATABASE [ERP] SET  READ_WRITE 
GO
