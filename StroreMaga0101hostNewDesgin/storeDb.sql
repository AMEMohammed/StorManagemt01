USE [master]
GO
/****** Object:  Database [StoreManagemtHost2]    Script Date: 2018-06-26 2:30:47 PM ******/
CREATE DATABASE [StoreManagemtHost2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'StoreManagemtHost2', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.S20012\MSSQL\DATA\StoreManagemtHost2.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'StoreManagemtHost2_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.S20012\MSSQL\DATA\StoreManagemtHost2_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [StoreManagemtHost2] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [StoreManagemtHost2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [StoreManagemtHost2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [StoreManagemtHost2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [StoreManagemtHost2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [StoreManagemtHost2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [StoreManagemtHost2] SET ARITHABORT OFF 
GO
ALTER DATABASE [StoreManagemtHost2] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [StoreManagemtHost2] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [StoreManagemtHost2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [StoreManagemtHost2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [StoreManagemtHost2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [StoreManagemtHost2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [StoreManagemtHost2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [StoreManagemtHost2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [StoreManagemtHost2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [StoreManagemtHost2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [StoreManagemtHost2] SET  DISABLE_BROKER 
GO
ALTER DATABASE [StoreManagemtHost2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [StoreManagemtHost2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [StoreManagemtHost2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [StoreManagemtHost2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [StoreManagemtHost2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [StoreManagemtHost2] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [StoreManagemtHost2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [StoreManagemtHost2] SET RECOVERY FULL 
GO
ALTER DATABASE [StoreManagemtHost2] SET  MULTI_USER 
GO
ALTER DATABASE [StoreManagemtHost2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [StoreManagemtHost2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [StoreManagemtHost2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [StoreManagemtHost2] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [StoreManagemtHost2]
GO
/****** Object:  StoredProcedure [dbo].[GetRequsetSupply]    Script Date: 2018-06-26 2:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetRequsetSupply]
@idSupply int
as
 SELECT  Category.NameCategory as'اسم الصنف' ,TypeQuntity.NameType as 'نوع الصنف',
RequstSupply.DateSupply as 'تاريخ التوريد',RequstSupply.DescSupply as 'وصف التوريد',
RequstSupply.NameSupply as 'اسم المورد',RequstSupply.Price as 'السعر',RequstSupply.Quntity as 'الكمية الموردة'
from Category,TypeQuntity,RequstSupply 
where RequstSupply.IDCategory=Category.IDCategory and RequstSupply.IDType=TypeQuntity.IDType and RequstSupply.IDSupply=@idSupply

GO
/****** Object:  Table [dbo].[Account]    Script Date: 2018-06-26 2:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[IDAccount] [int] IDENTITY(1,1) NOT NULL,
	[IDCategory] [int] NULL,
	[IDType] [int] NULL,
	[Quntity] [int] NULL,
	[Price] [int] NULL,
	[IDCurrency] [int] NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[IDAccount] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AccountDetalis]    Script Date: 2018-06-26 2:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountDetalis](
	[IDDetalis] [int] IDENTITY(1,1) NOT NULL,
	[IDCode] [int] NULL,
	[Mony] [float] NULL,
	[IDSupply] [int] NULL,
	[IDOut] [int] NULL,
	[Detalis] [nvarchar](500) NULL,
	[DateEnter] [datetime] NULL,
	[UserID] [int] NULL,
	[IDCurrncy] [int] NULL,
	[IDSimpleConstraint] [int] NULL,
 CONSTRAINT [PK_AccountDetalis] PRIMARY KEY CLUSTERED 
(
	[IDDetalis] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AccountNm]    Script Date: 2018-06-26 2:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountNm](
	[IDAcountNm] [int] IDENTITY(1,1) NOT NULL,
	[IDCode] [int] NULL,
	[AcountNm] [nvarchar](150) NULL,
	[IDParentAcount] [int] NULL,
	[AcountType] [nvarchar](50) NULL,
	[Active] [bit] NULL,
	[StartEnter] [datetime] NULL,
	[UserID] [int] NULL,
 CONSTRAINT [PK_AccounNm] PRIMARY KEY CLUSTERED 
(
	[IDAcountNm] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AccountTotal]    Script Date: 2018-06-26 2:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountTotal](
	[IDTotal] [int] IDENTITY(1,1) NOT NULL,
	[IDCode] [int] NULL,
	[Balance] [int] NULL,
	[IDCurrncy] [int] NULL,
 CONSTRAINT [PK_AccountTotal] PRIMARY KEY CLUSTERED 
(
	[IDTotal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Category]    Script Date: 2018-06-26 2:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[IDCategory] [int] IDENTITY(1,1) NOT NULL,
	[NameCategory] [nvarchar](150) NULL,
	[IDAccount] [int] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[IDCategory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CheckQuntity]    Script Date: 2018-06-26 2:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CheckQuntity](
	[IDCheck] [int] IDENTITY(1,1) NOT NULL,
	[IDCategory] [int] NULL,
	[IDType] [int] NULL,
	[LessQuntity] [int] NULL,
	[CurrntQuntity] [int] NULL,
 CONSTRAINT [PK_CheckQuntity] PRIMARY KEY CLUSTERED 
(
	[IDCheck] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Creditor]    Script Date: 2018-06-26 2:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Creditor](
	[IdTypeAccount] [int] IDENTITY(1,1) NOT NULL,
	[NameTypeAccount] [nvarchar](150) NULL,
 CONSTRAINT [PK_Creditor] PRIMARY KEY CLUSTERED 
(
	[IdTypeAccount] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Currency]    Script Date: 2018-06-26 2:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currency](
	[IDCurrency] [int] IDENTITY(1,1) NOT NULL,
	[NameCurrency] [nvarchar](50) NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[IDCurrency] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Debit]    Script Date: 2018-06-26 2:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Debit](
	[IdTypeAccount] [int] IDENTITY(1,1) NOT NULL,
	[NameTypeAccount] [nvarchar](350) NULL,
 CONSTRAINT [PK_TypeAccount] PRIMARY KEY CLUSTERED 
(
	[IdTypeAccount] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GroupDetalis]    Script Date: 2018-06-26 2:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupDetalis](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GroupID] [int] NULL,
	[GroupIDItem] [int] NULL,
	[UserID] [int] NULL,
 CONSTRAINT [PK_GroupDetalis] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PlaceSend]    Script Date: 2018-06-26 2:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlaceSend](
	[IDPlace] [int] IDENTITY(1,1) NOT NULL,
	[NamePlace] [nvarchar](150) NULL,
 CONSTRAINT [PK_PlaceSend] PRIMARY KEY CLUSTERED 
(
	[IDPlace] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RequstOut]    Script Date: 2018-06-26 2:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequstOut](
	[IDOut] [int] IDENTITY(1,1) NOT NULL,
	[IDCategory] [int] NULL,
	[IDType] [int] NULL,
	[IDPlace] [int] NULL,
	[Quntity] [int] NULL,
	[NameOut] [nvarchar](150) NULL,
	[DesOut] [nvarchar](150) NULL,
	[DateOut] [datetime] NULL,
	[Chack] [int] NULL,
	[NameSend] [nvarchar](150) NULL,
	[Price] [int] NULL,
	[IDCurrency] [int] NULL,
	[UserId] [int] NULL,
	[Creditor] [int] NULL,
	[Debit] [int] NULL,
 CONSTRAINT [PK_RequstOut] PRIMARY KEY CLUSTERED 
(
	[IDOut] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RequstSupply]    Script Date: 2018-06-26 2:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequstSupply](
	[IDSupply] [int] IDENTITY(1,1) NOT NULL,
	[IDCategory] [int] NULL,
	[IDType] [int] NULL,
	[Quntity] [int] NULL,
	[Price] [int] NULL,
	[NameSupply] [nvarchar](150) NULL,
	[DescSupply] [nvarchar](250) NULL,
	[DateSupply] [datetime] NULL,
	[IDCurrency] [int] NULL,
	[UserId] [int] NULL,
	[chek] [int] NULL,
	[Creditor] [int] NULL,
	[Debit] [int] NULL,
 CONSTRAINT [PK_RequstSupply] PRIMARY KEY CLUSTERED 
(
	[IDSupply] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Setting]    Script Date: 2018-06-26 2:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Setting](
	[SttingID] [int] IDENTITY(1,1) NOT NULL,
	[SttingNM] [nvarchar](50) NULL,
	[SttingValue] [nvarchar](50) NULL,
 CONSTRAINT [PK_Setting] PRIMARY KEY CLUSTERED 
(
	[SttingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SourecGroup]    Script Date: 2018-06-26 2:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SourecGroup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_SourecGroup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblBranches]    Script Date: 2018-06-26 2:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBranches](
	[IDBranch] [int] IDENTITY(1,1) NOT NULL,
	[TheNumber] [int] NULL,
	[BranchName] [nvarchar](180) NULL,
	[AccountID] [int] NULL,
	[Notes] [nvarchar](250) NULL,
	[EnglishName] [nvarchar](180) NULL,
	[phone] [nvarchar](50) NULL,
	[fax] [nvarchar](50) NULL,
	[Address] [nvarchar](150) NULL,
	[UserID] [int] NULL,
	[BranchID] [int] NULL,
	[EnterTime] [datetime] NULL,
 CONSTRAINT [PK_tblBranch] PRIMARY KEY CLUSTERED 
(
	[IDBranch] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblConnectionAccountWithPlace]    Script Date: 2018-06-26 2:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblConnectionAccountWithPlace](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDPalce] [int] NULL,
	[IDAcccountDaan] [int] NULL,
	[IDAccountMadden] [int] NULL,
 CONSTRAINT [PK_tblConnectionAccountWithPlace] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblGroup]    Script Date: 2018-06-26 2:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblGroup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GroupSourceID] [int] NULL,
	[GroupName] [nvarchar](150) NULL,
	[GroupDescription] [nvarchar](150) NULL,
	[UserID] [int] NULL,
	[EnterTime] [datetime] NULL,
 CONSTRAINT [PK_tblGroup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblSimpleConstraint]    Script Date: 2018-06-26 2:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSimpleConstraint](
	[IDSimpleConstraint] [int] IDENTITY(1,1) NOT NULL,
	[IDDaanAccont] [int] NULL,
	[IDMaddenAccount] [int] NULL,
	[Mony] [int] NULL,
	[IDUser] [int] NULL,
	[EnterTime] [datetime] NULL,
	[Note] [nvarchar](150) NULL,
	[IDCurnncy] [int] NULL,
 CONSTRAINT [PK_tblSimpleConstraint] PRIMARY KEY CLUSTERED 
(
	[IDSimpleConstraint] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TypeQuntity]    Script Date: 2018-06-26 2:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeQuntity](
	[IDType] [int] IDENTITY(1,1) NOT NULL,
	[NameType] [nvarchar](150) NULL,
 CONSTRAINT [PK_TypeQuntity] PRIMARY KEY CLUSTERED 
(
	[IDType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UpdateOut]    Script Date: 2018-06-26 2:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UpdateOut](
	[IdUpdate] [int] IDENTITY(1,1) NOT NULL,
	[IdCate] [int] NULL,
	[IdType] [int] NULL,
	[IdPlace] [int] NULL,
	[Quntity] [int] NULL,
	[NameOUt] [nvarchar](150) NULL,
	[DateUpdate] [datetime] NULL,
	[NameSend] [nvarchar](150) NULL,
	[Price] [int] NULL,
	[IdCurrent] [int] NULL,
	[TxtReson] [nvarchar](250) NULL,
	[IDOut] [int] NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_UpdateOut] PRIMARY KEY CLUSTERED 
(
	[IdUpdate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UpdSupply]    Script Date: 2018-06-26 2:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UpdSupply](
	[IDUpt] [int] IDENTITY(1,1) NOT NULL,
	[IDSupply] [int] NULL,
	[IDCategory] [int] NULL,
	[IDType] [int] NULL,
	[Quntity] [int] NULL,
	[Price] [int] NULL,
	[NameSupply] [nvarchar](150) NULL,
	[DateSupply] [datetime] NULL,
	[DescUpd] [nvarchar](250) NULL,
	[dateUpd] [datetime] NULL,
	[IDCurrency] [int] NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_UpdSupply] PRIMARY KEY CLUSTERED 
(
	[IDUpt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 2018-06-26 2:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[IDUSER] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NULL,
	[UserName] [nvarchar](150) NULL,
	[Password] [varchar](50) NULL,
	[AddSupply] [bit] NULL,
	[UpdSupply] [bit] NULL,
	[PrintSupply] [bit] NULL,
	[PrintOut] [bit] NULL,
	[PrintQuntity] [bit] NULL,
	[AddUser] [bit] NULL,
	[Active] [bit] NULL,
	[AddOut] [bit] NULL,
	[UpdOut] [bit] NULL,
	[UserID] [int] NULL,
	[UpdSupp1] [bit] NULL,
	[UpdOut1] [bit] NULL,
	[Cate] [bit] NULL,
	[type1] [bit] NULL,
	[account] [bit] NULL,
	[Monay] [bit] NULL,
	[Place] [bit] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[IDUSER] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[View_1]    Script Date: 2018-06-26 2:30:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_1]
AS
SELECT     dbo.RequstOut.IDOut, dbo.RequstOut.IDCategory, dbo.Category.NameCategory, dbo.TypeQuntity.NameType, dbo.RequstOut.Quntity, dbo.RequstOut.NameOut, dbo.PlaceSend.NamePlace
FROM         dbo.RequstOut INNER JOIN
                      dbo.Category ON dbo.RequstOut.IDCategory = dbo.Category.IDCategory INNER JOIN
                      dbo.TypeQuntity ON dbo.RequstOut.IDType = dbo.TypeQuntity.IDType INNER JOIN
                      dbo.PlaceSend ON dbo.RequstOut.IDType = dbo.PlaceSend.IDPlace

GO
ALTER TABLE [dbo].[tblBranches] ADD  CONSTRAINT [DF_tblBranches_EnterTime]  DEFAULT (getdate()) FOR [EnterTime]
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
         Begin Table = "RequstOut"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 126
               Right = 198
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "Category"
            Begin Extent = 
               Top = 6
               Left = 236
               Bottom = 111
               Right = 397
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TypeQuntity"
            Begin Extent = 
               Top = 6
               Left = 435
               Bottom = 96
               Right = 595
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PlaceSend"
            Begin Extent = 
               Top = 6
               Left = 633
               Bottom = 96
               Right = 793
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_1'
GO
USE [master]
GO
ALTER DATABASE [StoreManagemtHost2] SET  READ_WRITE 
GO
