USE [master]
GO
/****** Object:  Database [ExpenseTrackerDB]    Script Date: 24/10/2024 12:44:12 AM ******/
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
/****** Object:  Table [dbo].[UserExpense]    Script Date: 24/10/2024 12:44:12 AM ******/
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
/****** Object:  Table [dbo].[Expense]    Script Date: 24/10/2024 12:44:12 AM ******/
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
	[SetDay] [bit] NULL,
	[DaysOfWeek] [nvarchar](250) NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
 CONSTRAINT [PK_Expense] PRIMARY KEY CLUSTERED 
(
	[ExpenseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 24/10/2024 12:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[CategoryName] [nvarchar](252) NULL,
	[Description] [nvarchar](252) NULL,
	[CreatedDate] [datetime] NULL,
	[DateModified] [datetime] NULL,
	[SetAmount] [decimal](10, 2) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 24/10/2024 12:44:12 AM ******/
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
/****** Object:  View [dbo].[vw_usersExpensesView]    Script Date: 24/10/2024 12:44:12 AM ******/
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
/****** Object:  Table [dbo].[Balance]    Script Date: 24/10/2024 12:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Balance](
	[BalanceId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[TotalBalance] [decimal](10, 2) NULL,
	[RemainingBalance] [decimal](10, 2) NULL,
	[TodayExpense] [decimal](10, 2) NULL,
	[MonthId] [int] NULL,
	[YearId] [int] NULL,
	[isActive] [bit] NULL,
	[UpdatedBalance] [decimal](10, 2) NULL,
 CONSTRAINT [PK_Balance] PRIMARY KEY CLUSTERED 
(
	[BalanceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Month]    Script Date: 24/10/2024 12:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Month](
	[MonthId] [int] IDENTITY(1,1) NOT NULL,
	[MonthName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Month] PRIMARY KEY CLUSTERED 
(
	[MonthId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Report]    Script Date: 24/10/2024 12:44:12 AM ******/
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
/****** Object:  Table [dbo].[UserInformation]    Script Date: 24/10/2024 12:44:12 AM ******/
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
/****** Object:  Table [dbo].[Year]    Script Date: 24/10/2024 12:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Year](
	[YearId] [int] IDENTITY(1,1) NOT NULL,
	[YearCount] [nvarchar](50) NULL,
 CONSTRAINT [PK_Year] PRIMARY KEY CLUSTERED 
(
	[YearId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Balance] ON 

INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (1, 1, CAST(11.00 AS Decimal(10, 2)), CAST(11.00 AS Decimal(10, 2)), NULL, 1, 1, NULL, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (19, 2, CAST(121.00 AS Decimal(10, 2)), NULL, NULL, 1, 1, NULL, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (20, 2, CAST(666.00 AS Decimal(10, 2)), NULL, NULL, 1, 3, 0, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (21, 2, CAST(77777.00 AS Decimal(10, 2)), NULL, NULL, 2, 1, 0, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (22, 2, CAST(23232.00 AS Decimal(10, 2)), NULL, NULL, 6, 5, 0, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (23, 2, CAST(12121.00 AS Decimal(10, 2)), NULL, NULL, 7, 3, 0, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (24, 2, CAST(12121.00 AS Decimal(10, 2)), NULL, NULL, 7, 3, 0, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (25, 2, CAST(323.00 AS Decimal(10, 2)), NULL, NULL, 1, 2, 0, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (26, 2, CAST(32313131.00 AS Decimal(10, 2)), NULL, NULL, 3, 3, 0, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (27, 2, CAST(998.00 AS Decimal(10, 2)), NULL, NULL, 4, 2, 0, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (28, 2, CAST(9985.00 AS Decimal(10, 2)), NULL, NULL, 2, 5, 0, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (29, 1, CAST(1877.00 AS Decimal(10, 2)), CAST(1877.00 AS Decimal(10, 2)), NULL, 10, 1, 0, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (30, 1, NULL, NULL, NULL, 2, 3, 0, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (31, 1, NULL, NULL, NULL, 1, 3, 0, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (32, 1, CAST(2000.00 AS Decimal(10, 2)), CAST(-4442444.00 AS Decimal(10, 2)), NULL, 4, 3, 0, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (33, 1, CAST(3000.00 AS Decimal(10, 2)), CAST(624.00 AS Decimal(10, 2)), NULL, 5, 4, 0, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (34, 2, CAST(5000.00 AS Decimal(10, 2)), NULL, NULL, 10, 1, 0, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (35, 2, CAST(11111.00 AS Decimal(10, 2)), NULL, NULL, 1, 2, 0, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (36, 2, CAST(1111.00 AS Decimal(10, 2)), NULL, NULL, 1, 1, 0, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (37, 2, CAST(5000.00 AS Decimal(10, 2)), NULL, NULL, 1, 1, 0, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (38, 2, CAST(11111.00 AS Decimal(10, 2)), NULL, NULL, 2, 1, 0, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (39, 2, CAST(5555.00 AS Decimal(10, 2)), NULL, NULL, 1, 1, 0, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (40, 1, CAST(422.00 AS Decimal(10, 2)), NULL, NULL, 1, 1, 0, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (41, 2, CAST(5555.00 AS Decimal(10, 2)), NULL, NULL, 1, 3, 0, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (42, 2, CAST(1111111.00 AS Decimal(10, 2)), NULL, NULL, 1, 1, 0, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (43, 2, CAST(27755.00 AS Decimal(10, 2)), CAST(16755.00 AS Decimal(10, 2)), NULL, 1, 1, 0, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (44, 1, CAST(5222.00 AS Decimal(10, 2)), CAST(757.00 AS Decimal(10, 2)), NULL, 1, 1, 0, NULL)
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (45, 2, CAST(5000.00 AS Decimal(10, 2)), CAST(1000.00 AS Decimal(10, 2)), NULL, 1, 1, 0, CAST(1000.00 AS Decimal(10, 2)))
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (46, 1, CAST(778.00 AS Decimal(10, 2)), CAST(278.00 AS Decimal(10, 2)), NULL, 1, 1, 0, CAST(278.00 AS Decimal(10, 2)))
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (47, 1, CAST(5000.00 AS Decimal(10, 2)), CAST(3000.00 AS Decimal(10, 2)), NULL, 1, 1, 1, CAST(3000.00 AS Decimal(10, 2)))
INSERT [dbo].[Balance] ([BalanceId], [UserId], [TotalBalance], [RemainingBalance], [TodayExpense], [MonthId], [YearId], [isActive], [UpdatedBalance]) VALUES (48, 2, CAST(3000.00 AS Decimal(10, 2)), CAST(2756.00 AS Decimal(10, 2)), NULL, 2, 2, 1, CAST(2756.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Balance] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([CategoryId], [UserId], [CategoryName], [Description], [CreatedDate], [DateModified], [SetAmount]) VALUES (1, 1, N'Grocery', NULL, CAST(N'2024-10-25T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Category] ([CategoryId], [UserId], [CategoryName], [Description], [CreatedDate], [DateModified], [SetAmount]) VALUES (2, 1, N'School', NULL, CAST(N'2024-10-25T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Category] ([CategoryId], [UserId], [CategoryName], [Description], [CreatedDate], [DateModified], [SetAmount]) VALUES (3, 2, N'Shopping', NULL, CAST(N'2024-10-25T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Category] ([CategoryId], [UserId], [CategoryName], [Description], [CreatedDate], [DateModified], [SetAmount]) VALUES (4, 2, N'Fare', NULL, CAST(N'2024-10-25T00:00:00.000' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Expense] ON 

INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (71, NULL, 2, N'fffffffffffffff', CAST(123333.00 AS Decimal(10, 2)), N'23232', CAST(N'2024-10-21' AS Date), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (72, NULL, 2, N'bvbvb', CAST(232323.00 AS Decimal(10, 2)), N'42424', CAST(N'2024-10-07' AS Date), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (76, NULL, 4, N'zzzzzzzzzzz', CAST(2323.00 AS Decimal(10, 2)), N'23232', CAST(N'2024-10-08' AS Date), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (77, NULL, 3, N'12121', CAST(12121.00 AS Decimal(10, 2)), N'414141', CAST(N'2024-10-01' AS Date), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (78, NULL, 3, N'13131', CAST(1212121.00 AS Decimal(10, 2)), N'1212121', CAST(N'2024-10-08' AS Date), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (79, NULL, 4, N'1414141', CAST(32323.00 AS Decimal(10, 2)), N'12121', CAST(N'2024-10-21' AS Date), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (80, NULL, 4, N'vuivuv', CAST(3232.00 AS Decimal(10, 2)), N'12121', CAST(N'2024-10-22' AS Date), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1158, 1, 1, N'jaja', CAST(3290.00 AS Decimal(10, 2)), N'2323', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T20:03:06.073' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1159, 1, 1, N'keke', CAST(232.00 AS Decimal(10, 2)), N'232', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T20:18:20.967' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1160, 1, 1, N'323232', CAST(32323.00 AS Decimal(10, 2)), N'323232', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T20:18:42.227' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1161, 1, 1, N'1313', CAST(1313.00 AS Decimal(10, 2)), N'3232', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T20:18:52.730' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1162, 1, 2, N'32323', CAST(100.00 AS Decimal(10, 2)), N'232', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T20:21:43.860' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1163, 1, 1, N'1212', CAST(2121.00 AS Decimal(10, 2)), N'2121', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T20:28:37.890' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1164, 1, 1, N'2323', CAST(200.00 AS Decimal(10, 2)), N'232', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T20:43:25.057' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1165, 1, 1, N'3232', CAST(100.00 AS Decimal(10, 2)), N'232', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T20:44:24.573' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1166, 1, 1, N'21', CAST(200.00 AS Decimal(10, 2)), N'222', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T20:46:35.493' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1167, 1, 1, N'2323', CAST(111.00 AS Decimal(10, 2)), N'232', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T20:46:48.367' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1168, 1, 2, N'323232', CAST(400.00 AS Decimal(10, 2)), N'23232', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T20:47:03.277' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1169, 1, 1, N'2323', CAST(500.00 AS Decimal(10, 2)), N'23232', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T20:47:19.347' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1170, 1, 1, N'4444', CAST(4444444.00 AS Decimal(10, 2)), N'232', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T20:48:05.043' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1171, 1, 1, N'232', CAST(500.00 AS Decimal(10, 2)), N'2323232', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T20:48:32.627' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1172, 1, 1, N'3232', CAST(100.00 AS Decimal(10, 2)), N'23232', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T20:48:50.490' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1173, 1, 2, N'10', CAST(200.00 AS Decimal(10, 2)), N'232', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T20:54:40.150' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1174, 1, 2, N'232', CAST(500.00 AS Decimal(10, 2)), N'232', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T20:54:54.177' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1175, 1, 1, N'3232', CAST(324.00 AS Decimal(10, 2)), N'23232', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T20:55:14.827' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1179, 1, 1, N'232', CAST(222.00 AS Decimal(10, 2)), N'232', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T21:14:33.233' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1180, 1, 1, N'323', CAST(122.00 AS Decimal(10, 2)), N'232', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T21:22:38.527' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1184, 2, 3, N'33', CAST(333.00 AS Decimal(10, 2)), N'3232', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T21:59:46.323' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1185, 2, 3, N'111', CAST(1212.00 AS Decimal(10, 2)), N'23232', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T22:01:59.517' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1186, 2, 3, N'111', CAST(1212.00 AS Decimal(10, 2)), N'23232', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T22:02:02.137' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1187, 2, 3, N'111', CAST(1212.00 AS Decimal(10, 2)), N'23232', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T22:02:07.027' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1188, 2, 4, N'13131', CAST(111.00 AS Decimal(10, 2)), N'232', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T22:05:54.290' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1189, 2, 3, N'12121', CAST(1212.00 AS Decimal(10, 2)), N'1212', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T22:13:56.897' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1190, 1, 1, N'1212', CAST(121.00 AS Decimal(10, 2)), N'1212', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T22:15:42.743' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1191, 1, 2, N'121', CAST(444.00 AS Decimal(10, 2)), N'4141', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T22:15:55.043' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1192, 1, 1, N'121', CAST(1111.00 AS Decimal(10, 2)), N'12121', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T22:18:08.710' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1193, 1, 1, N'121', CAST(1111.00 AS Decimal(10, 2)), N'12121', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T22:18:10.923' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1194, 1, 1, N'111', CAST(232.00 AS Decimal(10, 2)), N'23232', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T22:23:23.417' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1195, 2, 3, N'232', CAST(111.00 AS Decimal(10, 2)), N'232', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T22:23:56.403' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1196, 2, 3, N'121', CAST(121.00 AS Decimal(10, 2)), N'121', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T22:25:47.113' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1197, 2, 3, N'121', CAST(121.00 AS Decimal(10, 2)), N'121', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T22:26:19.463' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1198, 1, 1, N'222', CAST(131.00 AS Decimal(10, 2)), N'232', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T22:26:57.603' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1199, 1, 1, N'121', CAST(121.00 AS Decimal(10, 2)), N'121', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T22:41:27.620' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1200, 1, 1, N'11', CAST(111.00 AS Decimal(10, 2)), N'2322', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T23:06:59.463' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1201, 2, 3, N'121', CAST(121.00 AS Decimal(10, 2)), N'121', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T23:07:25.233' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1202, 2, 4, N'111', CAST(1111.00 AS Decimal(10, 2)), N'12121', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T23:13:41.883' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1203, 2, 3, N'121', CAST(121.00 AS Decimal(10, 2)), N'121', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T23:34:03.777' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1204, 1, 1, N'111', CAST(111.00 AS Decimal(10, 2)), N'121', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23T23:34:50.690' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1205, 2, 3, N'444', CAST(3232.00 AS Decimal(10, 2)), N'23232', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-24T00:06:32.263' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1206, 2, 3, N'5555', CAST(12121.00 AS Decimal(10, 2)), N'23232', NULL, CAST(N'2024-10-24T00:06:51.843' AS DateTime), NULL, 1, N'Sunday,Wednesday,Friday,Saturday', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23' AS Date))
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1207, 2, 3, N'zzzzzzzzzzz', CAST(444.00 AS Decimal(10, 2)), N'23232', NULL, CAST(N'2024-10-24T00:07:33.873' AS DateTime), NULL, 0, N'', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-23' AS Date))
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1208, 2, 3, N'1313', CAST(13131.00 AS Decimal(10, 2)), N'13131', NULL, CAST(N'2024-10-24T00:09:31.850' AS DateTime), NULL, 1, N'Monday,Wednesday,Thursday', CAST(N'2024-10-08' AS Date), CAST(N'2024-11-01' AS Date))
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1209, 2, 3, N'1111', CAST(23.00 AS Decimal(10, 2)), N'232', CAST(N'2024-10-09' AS Date), CAST(N'2024-10-24T00:13:38.637' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1210, 2, 3, N'555', CAST(5555.00 AS Decimal(10, 2)), N'23232', CAST(N'2024-10-09' AS Date), CAST(N'2024-10-24T00:13:58.233' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1211, 1, 1, N'12121', CAST(333.00 AS Decimal(10, 2)), N'121', CAST(N'2024-10-08' AS Date), CAST(N'2024-10-24T00:14:19.533' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1212, 1, 1, N'42', CAST(555.00 AS Decimal(10, 2)), N'23232', CAST(N'2024-10-09' AS Date), CAST(N'2024-10-24T00:18:48.167' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1213, 1, 1, N'232', CAST(444.00 AS Decimal(10, 2)), N'23223', CAST(N'2024-10-15' AS Date), CAST(N'2024-10-24T00:18:59.490' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1214, 1, 1, N'232', CAST(2000.00 AS Decimal(10, 2)), N'2322', CAST(N'2024-10-21' AS Date), CAST(N'2024-10-24T00:19:16.810' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1215, 1, 1, N'111121', CAST(466.00 AS Decimal(10, 2)), N'232', NULL, CAST(N'2024-10-24T00:22:47.850' AS DateTime), NULL, 1, N'Monday,Tuesday,Wednesday', CAST(N'2024-10-08' AS Date), CAST(N'2024-10-29' AS Date))
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1216, 1, 1, N'444', CAST(1000.00 AS Decimal(10, 2)), N'2323', NULL, CAST(N'2024-10-24T00:23:09.980' AS DateTime), NULL, 1, N'Monday,Tuesday,Thursday', CAST(N'2024-10-24' AS Date), CAST(N'2024-10-31' AS Date))
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1217, 2, 3, N'1111', CAST(1000.00 AS Decimal(10, 2)), N'12121', CAST(N'2024-10-23' AS Date), CAST(N'2024-10-24T00:23:32.530' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1218, 2, 3, N'444', CAST(10000.00 AS Decimal(10, 2)), N'232', CAST(N'2024-10-15' AS Date), CAST(N'2024-10-24T00:23:53.657' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1219, 2, 3, N'1000', CAST(1000.00 AS Decimal(10, 2)), N'2323', NULL, CAST(N'2024-10-24T00:24:45.647' AS DateTime), NULL, 1, N'Monday,Thursday,Saturday', CAST(N'2024-10-08' AS Date), CAST(N'2024-10-07' AS Date))
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1220, 2, 3, N'111', CAST(111.00 AS Decimal(10, 2)), N'111', CAST(N'2024-10-09' AS Date), CAST(N'2024-10-24T00:26:22.920' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1221, 1, 1, N'111', CAST(500.00 AS Decimal(10, 2)), N'232', CAST(N'2024-10-07' AS Date), CAST(N'2024-10-24T00:27:02.800' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1222, 1, 1, N'111111', CAST(233.00 AS Decimal(10, 2)), N'23232', CAST(N'2024-10-09' AS Date), CAST(N'2024-10-24T00:29:33.117' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1223, 1, 1, N'2323', CAST(222.00 AS Decimal(10, 2)), N'2323', CAST(N'2024-10-15' AS Date), CAST(N'2024-10-24T00:32:57.757' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1224, 1, 1, N'222', CAST(500.00 AS Decimal(10, 2)), N'23232', CAST(N'2024-10-01' AS Date), CAST(N'2024-10-24T00:36:09.337' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1225, 1, 1, N'444', CAST(2000.00 AS Decimal(10, 2)), N'23232', CAST(N'2024-10-07' AS Date), CAST(N'2024-10-24T00:36:47.570' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1226, 2, 3, N'4000', CAST(4000.00 AS Decimal(10, 2)), N'232', CAST(N'2024-10-15' AS Date), CAST(N'2024-10-24T00:37:19.323' AS DateTime), NULL, 0, N'', NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [UserId], [CategoryId], [ExpenseName], [Amount], [Description], [Date], [CreatedDate], [DateModified], [SetDay], [DaysOfWeek], [StartDate], [EndDate]) VALUES (1227, 2, 3, N'244', CAST(244.00 AS Decimal(10, 2)), N'244', CAST(N'2024-10-15' AS Date), CAST(N'2024-10-24T00:37:40.770' AS DateTime), NULL, 0, N'', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Expense] OFF
GO
SET IDENTITY_INSERT [dbo].[Month] ON 

INSERT [dbo].[Month] ([MonthId], [MonthName]) VALUES (1, N'January')
INSERT [dbo].[Month] ([MonthId], [MonthName]) VALUES (2, N'February')
INSERT [dbo].[Month] ([MonthId], [MonthName]) VALUES (3, N'March')
INSERT [dbo].[Month] ([MonthId], [MonthName]) VALUES (4, N'April')
INSERT [dbo].[Month] ([MonthId], [MonthName]) VALUES (5, N'May')
INSERT [dbo].[Month] ([MonthId], [MonthName]) VALUES (6, N'June')
INSERT [dbo].[Month] ([MonthId], [MonthName]) VALUES (7, N'July')
INSERT [dbo].[Month] ([MonthId], [MonthName]) VALUES (8, N'August')
INSERT [dbo].[Month] ([MonthId], [MonthName]) VALUES (9, N'September')
INSERT [dbo].[Month] ([MonthId], [MonthName]) VALUES (10, N'Octoboer')
INSERT [dbo].[Month] ([MonthId], [MonthName]) VALUES (11, N'November')
INSERT [dbo].[Month] ([MonthId], [MonthName]) VALUES (12, N'December')
SET IDENTITY_INSERT [dbo].[Month] OFF
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
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (110, 104, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (111, 105, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (112, 106, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (113, 107, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (114, 108, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (115, 109, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (116, 110, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (117, 111, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (118, 112, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (119, 113, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (120, 114, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (121, 115, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (122, 116, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (123, 117, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (124, 118, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (125, 119, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (126, 120, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (127, 124, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (128, 125, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (129, 126, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (130, 127, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (131, 128, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (132, 129, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (133, 130, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (134, 131, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (135, 132, 2, 3)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1123, 1125, 2, 3)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1124, 1126, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1125, 1127, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1126, 1128, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1127, 1129, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1128, 1130, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1129, 1132, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1130, 1131, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1131, 1133, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1132, 1134, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1133, 1135, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1134, 1136, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1135, 1137, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1136, 1138, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1137, 1139, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1138, 1140, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1139, 1141, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1140, 1142, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1141, 1143, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1142, 1144, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1143, 1145, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1144, 1146, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1145, 1147, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1146, 1148, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1147, 1149, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1148, 1150, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1149, 1151, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1150, 1152, 2, 4)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1151, 1153, 2, 3)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1152, 1154, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1153, 1155, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1154, 1156, 1, 1)
GO
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1155, 1157, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1156, 1158, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1157, 1159, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1158, 1160, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1159, 1161, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1160, 1162, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1161, 1163, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1162, 1164, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1163, 1165, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1164, 1166, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1165, 1167, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1166, 1168, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1167, 1169, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1168, 1170, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1169, 1171, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1170, 1172, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1171, 1173, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1172, 1174, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1173, 1175, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1174, 1176, 2, 3)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1175, 1177, 2, 3)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1176, 1178, 2, 3)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1177, 1179, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1178, 1180, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1179, 1181, 2, 3)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1180, 1182, 2, 3)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1181, 1183, 2, 3)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1182, 1184, 2, 3)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1183, 1185, 2, 3)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1184, 1186, 2, 3)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1185, 1187, 2, 3)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1186, 1188, 2, 4)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1187, 1189, 2, 3)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1188, 1190, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1189, 1191, 1, 2)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1190, 1192, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1191, 1193, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1192, 1194, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1193, 1195, 2, 3)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1194, 1196, 2, 3)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1195, 1197, 2, 3)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1196, 1198, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1197, 1199, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1198, 1200, 1, 1)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1199, 1201, 2, 3)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1200, 1202, 2, 4)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1201, 1203, 2, 3)
INSERT [dbo].[UserExpense] ([UserExpenseId], [ExpenseId], [UserId], [CategoryId]) VALUES (1202, 1204, 1, 1)
SET IDENTITY_INSERT [dbo].[UserExpense] OFF
GO
SET IDENTITY_INSERT [dbo].[Year] ON 

INSERT [dbo].[Year] ([YearId], [YearCount]) VALUES (1, N'2024')
INSERT [dbo].[Year] ([YearId], [YearCount]) VALUES (2, N'2025')
INSERT [dbo].[Year] ([YearId], [YearCount]) VALUES (3, N'2026')
INSERT [dbo].[Year] ([YearId], [YearCount]) VALUES (4, N'2027')
INSERT [dbo].[Year] ([YearId], [YearCount]) VALUES (5, N'2028')
INSERT [dbo].[Year] ([YearId], [YearCount]) VALUES (6, N'2029')
INSERT [dbo].[Year] ([YearId], [YearCount]) VALUES (7, N'2030')
INSERT [dbo].[Year] ([YearId], [YearCount]) VALUES (8, N'2031')
INSERT [dbo].[Year] ([YearId], [YearCount]) VALUES (9, N'2032')
INSERT [dbo].[Year] ([YearId], [YearCount]) VALUES (10, N'2033')
INSERT [dbo].[Year] ([YearId], [YearCount]) VALUES (11, N'2034')
INSERT [dbo].[Year] ([YearId], [YearCount]) VALUES (12, N'2035')
SET IDENTITY_INSERT [dbo].[Year] OFF
GO
ALTER TABLE [dbo].[Balance]  WITH CHECK ADD  CONSTRAINT [FK_Balance_Month] FOREIGN KEY([MonthId])
REFERENCES [dbo].[Month] ([MonthId])
GO
ALTER TABLE [dbo].[Balance] CHECK CONSTRAINT [FK_Balance_Month]
GO
ALTER TABLE [dbo].[Balance]  WITH CHECK ADD  CONSTRAINT [FK_Balance_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Balance] CHECK CONSTRAINT [FK_Balance_User]
GO
ALTER TABLE [dbo].[Balance]  WITH CHECK ADD  CONSTRAINT [FK_Balance_Year] FOREIGN KEY([YearId])
REFERENCES [dbo].[Year] ([YearId])
GO
ALTER TABLE [dbo].[Balance] CHECK CONSTRAINT [FK_Balance_Year]
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
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_User]
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
/****** Object:  StoredProcedure [dbo].[SearchExpenses]    Script Date: 24/10/2024 12:44:12 AM ******/
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
