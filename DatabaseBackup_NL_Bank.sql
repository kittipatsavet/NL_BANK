USE [master]
GO
/****** Object:  Database [NL_Bank]    Script Date: 25/1/2566 18:28:59 ******/
CREATE DATABASE [NL_Bank]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NL_Bank', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\NL_Bank.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NL_Bank_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\NL_Bank_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [NL_Bank] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NL_Bank].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NL_Bank] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NL_Bank] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NL_Bank] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NL_Bank] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NL_Bank] SET ARITHABORT OFF 
GO
ALTER DATABASE [NL_Bank] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NL_Bank] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NL_Bank] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NL_Bank] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NL_Bank] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NL_Bank] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NL_Bank] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NL_Bank] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NL_Bank] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NL_Bank] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NL_Bank] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NL_Bank] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NL_Bank] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NL_Bank] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NL_Bank] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NL_Bank] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NL_Bank] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NL_Bank] SET RECOVERY FULL 
GO
ALTER DATABASE [NL_Bank] SET  MULTI_USER 
GO
ALTER DATABASE [NL_Bank] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NL_Bank] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NL_Bank] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NL_Bank] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NL_Bank] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NL_Bank] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'NL_Bank', N'ON'
GO
ALTER DATABASE [NL_Bank] SET QUERY_STORE = ON
GO
ALTER DATABASE [NL_Bank] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [NL_Bank]
GO
/****** Object:  Table [dbo].[ACCTransactions]    Script Date: 25/1/2566 18:28:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACCTransactions](
	[CTransID] [int] IDENTITY(1,1) NOT NULL,
	[CTransType] [nvarchar](50) NOT NULL,
	[CAmount] [numeric](18, 3) NULL,
	[CAccountNo] [nvarchar](25) NULL,
	[CDateCreated] [datetime] NULL,
 CONSTRAINT [PK_ACCTransactions] PRIMARY KEY CLUSTERED 
(
	[CTransID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[V_ACCTransactions]    Script Date: 25/1/2566 18:28:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_ACCTransactions]
AS
SELECT        CTransID, CTransType, CAmount, CAccountNo, CDateCreated
FROM            dbo.ACCTransactions
GO
/****** Object:  Table [dbo].[BankAccountMaster]    Script Date: 25/1/2566 18:28:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankAccountMaster](
	[AccountID] [int] IDENTITY(1,1) NOT NULL,
	[IBANAccountNo] [nvarchar](25) NOT NULL,
	[Customer_FirstName] [nvarchar](50) NULL,
	[Customer_LastName] [nvarchar](50) NULL,
	[CAddress1] [nvarchar](50) NULL,
	[CAddress2] [nvarchar](50) NULL,
	[CCity] [nvarchar](50) NULL,
	[CZipcode] [nvarchar](50) NULL,
	[CCountry] [nvarchar](50) NULL,
	[CPhone] [nvarchar](50) NULL,
	[Cemail] [nvarchar](50) NULL,
	[Balance] [decimal](18, 3) NULL,
	[AccDateCreated] [datetime] NULL,
 CONSTRAINT [PK_BankAccountMaster] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[V_BankAccountMaster]    Script Date: 25/1/2566 18:28:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_BankAccountMaster]
AS
SELECT        AccountID, IBANAccountNo, Customer_FirstName, Customer_LastName, CAddress1, CAddress2, CCity, CZipcode, CCountry, Cemail, CPhone, Balance, AccDateCreated
FROM            dbo.BankAccountMaster
GO
/****** Object:  Table [dbo].[BANK_INCOME]    Script Date: 25/1/2566 18:28:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BANK_INCOME](
	[BANKID] [int] IDENTITY(1,1) NOT NULL,
	[BANKFEE_ACCOUNT] [nvarchar](10) NULL,
	[BALANCE_CHARGES] [decimal](18, 2) NULL,
 CONSTRAINT [PK_BANK_INCOME] PRIMARY KEY CLUSTERED 
(
	[BANKID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[V_Bank_Income]    Script Date: 25/1/2566 18:28:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_Bank_Income]
AS
SELECT        BANKID, BANKFEE_ACCOUNT, BALANCE_CHARGES
FROM            dbo.BANK_INCOME
GO
/****** Object:  Table [dbo].[User]    Script Date: 25/1/2566 18:28:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[USERID] [int] IDENTITY(1,1) NOT NULL,
	[USER_FNAME] [nvarchar](50) NULL,
	[USER_LNAME] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NULL,
	[Pwd] [nvarchar](50) NULL,
	[Role] [nvarchar](20) NULL,
	[IBANAccountNo] [nvarchar](25) NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[USERID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[V_User]    Script Date: 25/1/2566 18:28:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_User]
AS
SELECT        USER_FNAME, USER_LNAME, UserName, Pwd, Role, IBANAccountNo
FROM            dbo.[User]
GO
SET IDENTITY_INSERT [dbo].[BankAccountMaster] ON 

INSERT [dbo].[BankAccountMaster] ([AccountID], [IBANAccountNo], [Customer_FirstName], [Customer_LastName], [CAddress1], [CAddress2], [CCity], [CZipcode], [CCountry], [CPhone], [Cemail], [Balance], [AccDateCreated]) VALUES (3, N'NL86ABNA7830770891', N'CUSTOMER1', N'TEST', N'1234 TEST', N'1234 TEST', N'BANGKOK', N'10100', N'THAILAND', N'12345678', N'test2@gmail.com', CAST(9000.000 AS Decimal(18, 3)), CAST(N'2023-01-24T00:00:00.000' AS DateTime))
INSERT [dbo].[BankAccountMaster] ([AccountID], [IBANAccountNo], [Customer_FirstName], [Customer_LastName], [CAddress1], [CAddress2], [CCity], [CZipcode], [CCountry], [CPhone], [Cemail], [Balance], [AccDateCreated]) VALUES (5, N'NL86ABNA7830770891', N'Customer3', N'test3', N'55555', N'55555', N'Bangkok', N'10600', N'THAILAND', N'02444-4444', N'test4@gmail.com', CAST(4086.000 AS Decimal(18, 3)), CAST(N'2023-01-24T00:00:00.000' AS DateTime))
INSERT [dbo].[BankAccountMaster] ([AccountID], [IBANAccountNo], [Customer_FirstName], [Customer_LastName], [CAddress1], [CAddress2], [CCity], [CZipcode], [CCountry], [CPhone], [Cemail], [Balance], [AccDateCreated]) VALUES (6, N'NL67RABO1782020381', N'CUST5', N'TEST5', N'8888888', N'8888888', N'BANGKOK', N'10200', N'THAILAND', N'02-222-2222', N'test5@gmail.com', CAST(1096.000 AS Decimal(18, 3)), CAST(N'2023-01-25T00:00:00.000' AS DateTime))
INSERT [dbo].[BankAccountMaster] ([AccountID], [IBANAccountNo], [Customer_FirstName], [Customer_LastName], [CAddress1], [CAddress2], [CCity], [CZipcode], [CCountry], [CPhone], [Cemail], [Balance], [AccDateCreated]) VALUES (1006, N'NL25INGB3121637177', N'SOMCHAI', N'TEST', N'333 NEW ROAD, BANGRAK,', N'333 NEW ROAD, BANGRAK,', N'BANGKOK', N'10500', N'THAILAND', N'0233333333', N'somchai@gmail.com', CAST(999.000 AS Decimal(18, 3)), CAST(N'2023-01-25T13:20:46.413' AS DateTime))
INSERT [dbo].[BankAccountMaster] ([AccountID], [IBANAccountNo], [Customer_FirstName], [Customer_LastName], [CAddress1], [CAddress2], [CCity], [CZipcode], [CCountry], [CPhone], [Cemail], [Balance], [AccDateCreated]) VALUES (1007, N'NL59ABNA1388268736', N'SOMSAK', N'TEST', N'111 CONDO ONE, SUKUMVIT ROAD,', N'111 CONDO ONE, SUKUMVIT ROAD,', N'BANGKOK', N'10110', N'THAILAND', N'02-2222222', N'somsak@gmail.com', CAST(999.000 AS Decimal(18, 3)), CAST(N'2023-01-25T14:10:14.990' AS DateTime))
INSERT [dbo].[BankAccountMaster] ([AccountID], [IBANAccountNo], [Customer_FirstName], [Customer_LastName], [CAddress1], [CAddress2], [CCity], [CZipcode], [CCountry], [CPhone], [Cemail], [Balance], [AccDateCreated]) VALUES (1008, N'NL26RABO4863744099', N'KITCHAI', N'TEST', N'444 D CONDO , BANSAO THONG', N'444 D CONDO , BANSAO THONG', N'SAMUTPRAKARN', N'10560', N'THAILAND', N'02-666-6666', N'kitchai@gmail.com', CAST(2997.000 AS Decimal(18, 3)), CAST(N'2023-01-25T14:26:38.170' AS DateTime))
INSERT [dbo].[BankAccountMaster] ([AccountID], [IBANAccountNo], [Customer_FirstName], [Customer_LastName], [CAddress1], [CAddress2], [CCity], [CZipcode], [CCountry], [CPhone], [Cemail], [Balance], [AccDateCreated]) VALUES (1009, N'NL67RABO1782020381', N'PATANAPONG', N'TEST', N'2 TWO CONDO, BANGNA', N'2 TWO CONDO, BANGNA', N'BANGKOK', N'10260', N'THAILAND', N'02-555-5555', N'panapong@gmail.com', CAST(2900.000 AS Decimal(18, 3)), CAST(N'2023-01-25T14:39:59.150' AS DateTime))
INSERT [dbo].[BankAccountMaster] ([AccountID], [IBANAccountNo], [Customer_FirstName], [Customer_LastName], [CAddress1], [CAddress2], [CCity], [CZipcode], [CCountry], [CPhone], [Cemail], [Balance], [AccDateCreated]) VALUES (1010, N'NL30INGB7467539436', N'PICHIT', N'TEST', N'66   SIXTY SIX CONDO', N'66   SIXTY SIX CONDO', N'BANGKOK', N'10500', N'THAILAND', N'02-666-6666', N'pichit@gmail.com', CAST(6993.000 AS Decimal(18, 3)), CAST(N'2023-01-25T14:50:26.147' AS DateTime))
SET IDENTITY_INSERT [dbo].[BankAccountMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([USERID], [USER_FNAME], [USER_LNAME], [UserName], [Pwd], [Role], [IBANAccountNo]) VALUES (1, N'KITTIPAT', N'SAVET', N'KITTIPAT', N'1234', N'ADMIN', N' ')
INSERT [dbo].[User] ([USERID], [USER_FNAME], [USER_LNAME], [UserName], [Pwd], [Role], [IBANAccountNo]) VALUES (2, N'CUSTOMER1', N'TEST', N'CUST1', N'1234', N'CUSTOMER', N'NL19INGB7827071589')
INSERT [dbo].[User] ([USERID], [USER_FNAME], [USER_LNAME], [UserName], [Pwd], [Role], [IBANAccountNo]) VALUES (3, N'Customer3', N'TEST', N'CUST3', N'1234', N'CUSTOMER', N'NL86ABNA7830770891')
INSERT [dbo].[User] ([USERID], [USER_FNAME], [USER_LNAME], [UserName], [Pwd], [Role], [IBANAccountNo]) VALUES (4, N'CUST5', N'TEST', N'CUST5', N'1234', N'CUSTOMER', N'NL67RABO1782020381')
INSERT [dbo].[User] ([USERID], [USER_FNAME], [USER_LNAME], [UserName], [Pwd], [Role], [IBANAccountNo]) VALUES (5, N'ADMIN', N'TEST', N'ADMIN', N'1234', N'ADMIN', N' ')
INSERT [dbo].[User] ([USERID], [USER_FNAME], [USER_LNAME], [UserName], [Pwd], [Role], [IBANAccountNo]) VALUES (6, N'SOMCHAI', N'TEST', N'somchai', N'1234', N'CUSTOMER', N'NL25INGB3121637177')
INSERT [dbo].[User] ([USERID], [USER_FNAME], [USER_LNAME], [UserName], [Pwd], [Role], [IBANAccountNo]) VALUES (7, N'SOMSAK', N'TEST', N'SOMSAK', N'1234', N'CUSTOMER', N'NL59ABNA1388268736')
INSERT [dbo].[User] ([USERID], [USER_FNAME], [USER_LNAME], [UserName], [Pwd], [Role], [IBANAccountNo]) VALUES (8, N'PATANAPONG', N'TEST', N'PATANAPONG', N'1234', N'CUSTOMER', N'NL09RABO5994508810')
INSERT [dbo].[User] ([USERID], [USER_FNAME], [USER_LNAME], [UserName], [Pwd], [Role], [IBANAccountNo]) VALUES (9, N'PICHIT', N'TEST', N'PICHIT', N'1234', N'CUSTOMER', N'NL30INGB7467539436')
INSERT [dbo].[User] ([USERID], [USER_FNAME], [USER_LNAME], [UserName], [Pwd], [Role], [IBANAccountNo]) VALUES (10, N'KITCHAI', N'TEST', N'KITCHAI', N'1234', N'CUSTOMER', N'NL26RABO4863744099')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ACCTransactions"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 1
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_ACCTransactions'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_ACCTransactions'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "BANK_INCOME"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 119
               Right = 237
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_Bank_Income'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_Bank_Income'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "BankAccountMaster"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 238
            End
            DisplayFlags = 280
            TopColumn = 9
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_BankAccountMaster'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_BankAccountMaster'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "User"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 215
            End
            DisplayFlags = 280
            TopColumn = 3
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_User'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_User'
GO
USE [master]
GO
ALTER DATABASE [NL_Bank] SET  READ_WRITE 
GO
