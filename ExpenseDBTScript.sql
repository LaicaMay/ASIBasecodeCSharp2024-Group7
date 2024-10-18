USE [master]
GO
/****** Object:  Database [ExpenseTrackerDB]    Script Date: 19/10/2024 12:11:47 AM ******/
CREATE DATABASE [ExpenseTrackerDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ExpenseTrackerDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ExpenseTrackerDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ExpenseTrackerDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ExpenseTrackerDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ExpenseTrackerDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ExpenseTrackerDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ExpenseTrackerDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ExpenseTrackerDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ExpenseTrackerDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ExpenseTrackerDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ExpenseTrackerDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ExpenseTrackerDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ExpenseTrackerDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ExpenseTrackerDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ExpenseTrackerDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ExpenseTrackerDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ExpenseTrackerDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ExpenseTrackerDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ExpenseTrackerDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ExpenseTrackerDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ExpenseTrackerDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ExpenseTrackerDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ExpenseTrackerDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ExpenseTrackerDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ExpenseTrackerDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ExpenseTrackerDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ExpenseTrackerDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ExpenseTrackerDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ExpenseTrackerDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ExpenseTrackerDB] SET  MULTI_USER 
GO
ALTER DATABASE [ExpenseTrackerDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ExpenseTrackerDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ExpenseTrackerDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ExpenseTrackerDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ExpenseTrackerDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ExpenseTrackerDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ExpenseTrackerDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [ExpenseTrackerDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ExpenseTrackerDB]
GO
/****** Object:  Table [dbo].[User]    Script Date: 19/10/2024 12:11:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](252) NULL,
	[Password] [nvarchar](252) NULL,
	[ConfirmPassword] [nvarchar](252) NULL,
	[Email] [nvarchar](255) NULL,
	[Code] [nvarchar](255) NULL,
	[Status] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[DateModified] [datetime] NULL,
	[Agree] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserExpense]    Script Date: 19/10/2024 12:11:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserExpense](
	[UserExpenseId] [int] IDENTITY(1,1) NOT NULL,
	[ExpenseId] [int] NULL,
	[UserId] [int] NULL,
	[CategoryId] [int] NULL,
 CONSTRAINT [PK_UserExpense] PRIMARY KEY CLUSTERED 
(
	[UserExpenseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Expense]    Script Date: 19/10/2024 12:11:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expense](
	[ExpenseId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[CategoryId] [int] NULL,
	[ExpenseName] [nvarchar](250) NULL,
	[Amount] [decimal](10, 2) NULL,
	[Description] [nvarchar](252) NULL,
	[Date] [date] NULL,
	[CreatedDate] [datetime] NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_Expense] PRIMARY KEY CLUSTERED 
(
	[ExpenseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 19/10/2024 12:11:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[CategoryName] [nvarchar](252) NULL,
	[CreatedDate] [datetime] NULL,
	[DateModified] [datetime] NULL,
	[SetAmount] [decimal](10, 2) NULL,
	[Description] [nvarchar](252) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_usersExpensesView]    Script Date: 19/10/2024 12:11:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[vw_usersExpensesView]
as
SELECT c.CategoryId, u.UserId, ex.ExpenseId ,c.CategoryName, ex.ExpenseName, ex.Amount, ex.Date, ex.Description
FROM            dbo.UserExpense AS ue INNER JOIN
                         dbo.Category AS c ON ue.CategoryId = c.CategoryId INNER JOIN
                         dbo.[User] AS u ON ue.UserId = u.UserId INNER JOIN
                         dbo.Expense AS ex ON ue.ExpenseId = ex.ExpenseId
GO
/****** Object:  Table [dbo].[Report]    Script Date: 19/10/2024 12:11:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Report](
	[ReportId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[ReportType] [nvarchar](252) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[GeneratedDate] [datetime] NULL,
 CONSTRAINT [PK_Report] PRIMARY KEY CLUSTERED 
(
	[ReportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInformation]    Script Date: 19/10/2024 12:11:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInformation](
	[UserInfoId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[FirstName] [nvarchar](255) NOT NULL,
	[LastName] [nvarchar](255) NULL,
	[Phone] [nvarchar](255) NULL,
	[Street] [nvarchar](255) NULL,
	[City] [nvarchar](255) NULL,
	[ZipCode] [nvarchar](255) NULL,
	[Active] [int] NULL,
 CONSTRAINT [PK_UserInformation] PRIMARY KEY CLUSTERED 
(
	[UserInfoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([CategoryId], [UserId], [CategoryName], [CreatedDate], [DateModified], [SetAmount], [Description]) VALUES (1, 1, N'Grocery', CAST(N'2024-10-25T00:00:00.000' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Category] ([CategoryId], [UserId], [CategoryName], [CreatedDate], [DateModified], [SetAmount], [Description]) VALUES (2, 1, N'School', CAST(N'2024-10-25T00:00:00.000' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Category] ([CategoryId], [UserId], [CategoryName], [CreatedDate], [DateModified], [SetAmount], [Description]) VALUES (3, 2, N'Shopping', CAST(N'2024-10-25T00:00:00.000' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Category] ([CategoryId], [UserId], [CategoryName], [CreatedDate], [DateModified], [SetAmount], [Description]) VALUES (4, 2, N'Fare', CAST(N'2024-10-25T00:00:00.000' AS DateTime), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Expense] ON 

INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified]) VALUES (71, NULL, 2, N'fffffffffffffff', CAST(123333.00 AS Decimal(10, 2)), N'23232', CAST(N'2024-10-21' AS Date), NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified]) VALUES (72, NULL, 2, N'bvbvb', CAST(232323.00 AS Decimal(10, 2)), N'42424', CAST(N'2024-10-07' AS Date), NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified]) VALUES (76, NULL, 4, N'zzzzzzzzzzz', CAST(2323.00 AS Decimal(10, 2)), N'23232', CAST(N'2024-10-08' AS Date), NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified]) VALUES (77, NULL, 3, N'12121', CAST(12121.00 AS Decimal(10, 2)), N'414141', CAST(N'2024-10-01' AS Date), NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified]) VALUES (78, NULL, 3, N'13131', CAST(1212121.00 AS Decimal(10, 2)), N'1212121', CAST(N'2024-10-08' AS Date), NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified]) VALUES (79, NULL, 4, N'1414141', CAST(32323.00 AS Decimal(10, 2)), N'12121', CAST(N'2024-10-21' AS Date), NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified]) VALUES (80, NULL, 4, N'vuivuv', CAST(3232.00 AS Decimal(10, 2)), N'12121', CAST(N'2024-10-22' AS Date), NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified]) VALUES (82, 1, 2, N'vvv', CAST(11111.00 AS Decimal(10, 2)), N'32322', CAST(N'2024-10-22' AS Date), CAST(N'2024-10-18T10:41:02.073' AS DateTime), NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified]) VALUES (89, 2, 4, N'test add', CAST(123.00 AS Decimal(10, 2)), N'testetetadwad', CAST(N'2024-10-08' AS Date), CAST(N'2024-10-18T16:31:41.720' AS DateTime), NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified]) VALUES (90, 2, 3, N'test shopping', CAST(4444.00 AS Decimal(10, 2)), N'1212', CAST(N'2024-09-30' AS Date), CAST(N'2024-10-18T16:32:09.643' AS DateTime), NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified]) VALUES (92, 1, 1, N'gawgawgaw', CAST(1111.00 AS Decimal(10, 2)), N'32323', CAST(N'2024-10-28' AS Date), CAST(N'2024-10-18T19:59:15.233' AS DateTime), NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified]) VALUES (96, 1, 2, N'mmmmmmmmm', CAST(323.00 AS Decimal(10, 2)), N'3333333', CAST(N'2024-10-14' AS Date), CAST(N'2024-10-18T20:03:40.463' AS DateTime), NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified]) VALUES (98, 1, 1, N'vvvvvvv', CAST(1.00 AS Decimal(10, 2)), N'12', CAST(N'2024-10-07' AS Date), CAST(N'2024-10-18T20:27:14.553' AS DateTime), NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified]) VALUES (99, 1, 1, N'cx', CAST(3.00 AS Decimal(10, 2)), N'13', CAST(N'2024-10-07' AS Date), CAST(N'2024-10-18T20:27:25.260' AS DateTime), NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified]) VALUES (100, 1, 1, N'zx', CAST(3.00 AS Decimal(10, 2)), N'1', CAST(N'2024-10-02' AS Date), CAST(N'2024-10-18T20:27:38.537' AS DateTime), NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified]) VALUES (101, 1, 2, N'bvbvbv', CAST(3232.00 AS Decimal(10, 2)), N'232', CAST(N'2024-10-07' AS Date), CAST(N'2024-10-18T20:28:07.390' AS DateTime), NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified]) VALUES (102, 1, 1, N'gggggfgfggf', CAST(3.00 AS Decimal(10, 2)), N'232323', CAST(N'2024-10-15' AS Date), CAST(N'2024-10-18T23:44:03.013' AS DateTime), NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified]) VALUES (103, 1, 1, N'dvvvvvvvvvvv', CAST(222222.00 AS Decimal(10, 2)), N'222222', CAST(N'2024-10-14' AS Date), CAST(N'2024-10-18T23:44:21.340' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Expense] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [Username], [Password], [ConfirmPassword], [Email], [Code], [Status], [CreatedDate], [DateModified], [Agree]) VALUES (1, N'kawkaw', N'123', NULL, N'kaw@gmai..com', N'123', 1, NULL, NULL, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [ConfirmPassword], [Email], [Code], [Status], [CreatedDate], [DateModified], [Agree]) VALUES (2, N'fawfaw', N'123', NULL, N'fawfaw@gmail.com', N'262780', 0, CAST(N'2024-09-05T11:44:03.927' AS DateTime), NULL, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [ConfirmPassword], [Email], [Code], [Status], [CreatedDate], [DateModified], [Agree]) VALUES (3, N'teste123', N'123', NULL, N'tyeat@gmail.com', N'456503', 0, CAST(N'2024-09-05T11:50:19.610' AS DateTime), NULL, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [ConfirmPassword], [Email], [Code], [Status], [CreatedDate], [DateModified], [Agree]) VALUES (4, N'kawkaw2', N'123', N'123', N'fff', N'335002', 0, CAST(N'2024-09-09T13:47:14.347' AS DateTime), NULL, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [ConfirmPassword], [Email], [Code], [Status], [CreatedDate], [DateModified], [Agree]) VALUES (5, N'fsfs', N'123', N'123', N'fssfs', N'120228', 0, CAST(N'2024-09-09T14:02:21.467' AS DateTime), NULL, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [ConfirmPassword], [Email], [Code], [Status], [CreatedDate], [DateModified], [Agree]) VALUES (6, N'fsfsds', N'123', N'123', N'fsfsds', N'820243', 0, CAST(N'2024-09-09T14:03:29.623' AS DateTime), NULL, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [ConfirmPassword], [Email], [Code], [Status], [CreatedDate], [DateModified], [Agree]) VALUES (7, N'gh123', N'123', N'123', N'ghhhh', N'995709', 0, CAST(N'2024-09-09T14:13:44.523' AS DateTime), NULL, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [ConfirmPassword], [Email], [Code], [Status], [CreatedDate], [DateModified], [Agree]) VALUES (8, N'lex123', N'123', N'123', N'wakwak@gmail.com', N'594232', 0, CAST(N'2024-09-09T15:46:12.860' AS DateTime), NULL, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [ConfirmPassword], [Email], [Code], [Status], [CreatedDate], [DateModified], [Agree]) VALUES (9, N'waf123', N'123', N'123', N'waf@gmail.com', N'366592', 0, CAST(N'2024-09-09T15:47:19.497' AS DateTime), NULL, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [ConfirmPassword], [Email], [Code], [Status], [CreatedDate], [DateModified], [Agree]) VALUES (10, N'test121', N'123', N'123', N'test1234@gmail.com', N'198466', 0, CAST(N'2024-09-09T15:55:00.377' AS DateTime), NULL, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [ConfirmPassword], [Email], [Code], [Status], [CreatedDate], [DateModified], [Agree]) VALUES (11, N'kawkaw5000', N'123', N'123', N'testyser123@gmail.com', N'307785', 0, CAST(N'2024-09-11T14:10:23.370' AS DateTime), NULL, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [ConfirmPassword], [Email], [Code], [Status], [CreatedDate], [DateModified], [Agree]) VALUES (12, N'testeste333', N'123', N'123', N'testAccount@gmail.com', N'218634', 0, CAST(N'2024-10-12T20:31:50.490' AS DateTime), NULL, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [ConfirmPassword], [Email], [Code], [Status], [CreatedDate], [DateModified], [Agree]) VALUES (13, N'awkawk', N'123', N'123', N'awkawk@gmail.com', N'244043', 0, CAST(N'2024-10-13T23:20:57.627' AS DateTime), NULL, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [ConfirmPassword], [Email], [Code], [Status], [CreatedDate], [DateModified], [Agree]) VALUES (14, N'sam123', N'123', N'123', N'sampl2@gmail.com', N'382050', 0, CAST(N'2024-10-14T03:06:32.727' AS DateTime), NULL, 1)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserExpense] ON 

INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (15, 1, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (16, 1, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (17, 1, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (18, 1, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (19, 2, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (20, 1, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (21, 2, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (22, 2, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (23, 1, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (24, 2, 2, 4)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (25, 1, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (80, 74, 2, 4)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (81, 75, 2, 4)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (82, 76, 2, 3)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (83, 77, 2, 4)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (84, 78, 2, 3)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (85, 79, 2, 4)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (86, 80, 2, 3)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (87, 81, 2, 3)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (88, 82, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (89, 83, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (90, 84, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (91, 85, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (92, 86, 2, 3)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (93, 87, 2, 4)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (94, 88, 2, 4)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (95, 89, 2, 4)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (96, 90, 2, 3)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (97, 91, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (98, 92, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (99, 93, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (100, 94, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (101, 95, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (102, 96, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (103, 97, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (104, 98, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (105, 99, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (106, 100, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (107, 101, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (108, 102, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (109, 103, 1, 1)
SET IDENTITY_INSERT [dbo].[UserExpense] OFF
GO
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_User]
GO
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD  CONSTRAINT [FK_Expense_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[Expense] CHECK CONSTRAINT [FK_Expense_Category]
GO
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD  CONSTRAINT [FK_Expense_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Expense] CHECK CONSTRAINT [FK_Expense_User]
GO
ALTER TABLE [dbo].[Report]  WITH CHECK ADD  CONSTRAINT [FK_Report_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Report] CHECK CONSTRAINT [FK_Report_User]
GO
ALTER TABLE [dbo].[UserExpense]  WITH CHECK ADD  CONSTRAINT [FK_UserExpense_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserExpense] CHECK CONSTRAINT [FK_UserExpense_User]
GO
ALTER TABLE [dbo].[UserInformation]  WITH CHECK ADD  CONSTRAINT [FK_UserInformation_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserInformation] CHECK CONSTRAINT [FK_UserInformation_User]
GO
/****** Object:  StoredProcedure [dbo].[SearchExpenses]    Script Date: 19/10/2024 12:11:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SearchExpenses]
    @SearchTerm NVARCHAR(100)
AS
BEGIN
    SELECT 
        e.ExpenseId,
        e.UserId,
        e.CategoryId,
        e.ExpenseName,
        e.Amount,
        e.Description,
        e.Date,
        e.CreatedDate,
        e.DateModified,
        c.CategoryName  -- Make sure to include this
    FROM 
        Expense e
    INNER JOIN 
        Category c ON e.CategoryId = c.CategoryId
    WHERE 
        (e.ExpenseName LIKE '%' + @SearchTerm + '%' 
         OR c.CategoryName LIKE '%' + @SearchTerm + '%' 
         OR CONVERT(NVARCHAR, e.Amount) LIKE '%' + @SearchTerm + '%' 
         OR e.Description LIKE '%' + @SearchTerm + '%'
         OR CONVERT(NVARCHAR, e.Date, 120) LIKE '%' + @SearchTerm + '%');  -- Adjust date format if needed
END
GO
USE [master]
GO
ALTER DATABASE [ExpenseTrackerDB] SET  READ_WRITE 
GO
