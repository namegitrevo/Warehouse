USE [master]
GO
/****** Object:  Database [Warehouse]    Script Date: 17.04.2023 7:17:30 ******/
CREATE DATABASE [Warehouse]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Warehouse', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Warehouse.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Warehouse_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Warehouse_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Warehouse] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Warehouse].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Warehouse] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Warehouse] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Warehouse] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Warehouse] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Warehouse] SET ARITHABORT OFF 
GO
ALTER DATABASE [Warehouse] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Warehouse] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Warehouse] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Warehouse] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Warehouse] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Warehouse] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Warehouse] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Warehouse] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Warehouse] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Warehouse] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Warehouse] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Warehouse] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Warehouse] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Warehouse] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Warehouse] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Warehouse] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Warehouse] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Warehouse] SET RECOVERY FULL 
GO
ALTER DATABASE [Warehouse] SET  MULTI_USER 
GO
ALTER DATABASE [Warehouse] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Warehouse] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Warehouse] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Warehouse] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Warehouse] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Warehouse] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Warehouse', N'ON'
GO
ALTER DATABASE [Warehouse] SET QUERY_STORE = OFF
GO
USE [Warehouse]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 17.04.2023 7:17:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contractor]    Script Date: 17.04.2023 7:17:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contractor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Contractor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConversionOfMeasureUnits]    Script Date: 17.04.2023 7:17:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConversionOfMeasureUnits](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MaterialAssetId] [int] NOT NULL,
	[AtMeasureUnitId] [int] NOT NULL,
	[TransfarmationRatio] [decimal](3, 3) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ConversionOfMeasureUnits] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentItems]    Script Date: 17.04.2023 7:17:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentId] [int] NOT NULL,
	[MaterialAssetsId] [int] NOT NULL,
	[Amount] [decimal](9, 3) NOT NULL,
	[PriceForUnit] [money] NOT NULL,
 CONSTRAINT [PK_DocumentItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Documents]    Script Date: 17.04.2023 7:17:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Documents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentTypeId] [int] NOT NULL,
	[WerehouseId] [int] NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentTypes]    Script Date: 17.04.2023 7:17:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_DocumentTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 17.04.2023 7:17:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Patronymic] [nvarchar](50) NULL,
	[Login] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaterialAssets]    Script Date: 17.04.2023 7:17:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialAssets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[ItemNumber] [varchar](50) NOT NULL,
	[MeasureUnitId] [int] NOT NULL,
 CONSTRAINT [PK_MaterialAssets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeasureUnits]    Script Date: 17.04.2023 7:17:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeasureUnits](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_MeasureUnits] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Priority]    Script Date: 17.04.2023 7:17:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Priority](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Priority] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Receipt]    Script Date: 17.04.2023 7:17:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Receipt](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](50) NOT NULL,
	[Date] [date] NOT NULL,
	[СontractorId] [int] NOT NULL,
	[DocumentId] [int] NOT NULL,
 CONSTRAINT [PK_Receipt] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Request]    Script Date: 17.04.2023 7:17:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Request](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StatusId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Deadline] [datetime] NOT NULL,
	[ThemeId] [int] NOT NULL,
	[Customer] [varchar](50) NOT NULL,
	[ExecutorId] [int] NOT NULL,
	[PriorityId] [int] NOT NULL,
	[СompanyId] [int] NOT NULL,
	[Creator] [varchar](50) NOT NULL,
	[Address] [varchar](500) NOT NULL,
	[Content] [varchar](500) NOT NULL,
	[DocumentId] [int] NULL,
 CONSTRAINT [PK_Request] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reserve]    Script Date: 17.04.2023 7:17:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reserve](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MaterialAssetsId] [int] NOT NULL,
	[MinAmount] [int] NOT NULL,
	[MaxAmount] [int] NOT NULL,
 CONSTRAINT [PK_ROFs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 17.04.2023 7:17:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 17.04.2023 7:17:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Storages]    Script Date: 17.04.2023 7:17:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Storages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Note] [varchar](500) NOT NULL,
 CONSTRAINT [PK_Warehouses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplies]    Script Date: 17.04.2023 7:17:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MaterialAssetsId] [int] NOT NULL,
	[WarehousesId] [int] NOT NULL,
	[Amount] [decimal](9, 3) NOT NULL,
	[PriceForUnit] [money] NOT NULL,
 CONSTRAINT [PK_Remains] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Themes]    Script Date: 17.04.2023 7:17:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Themes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Note] [varchar](500) NULL,
 CONSTRAINT [PK_Themes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Company] ON 

INSERT [dbo].[Company] ([Id], [Name]) VALUES (1, N'Правительство РХ')
INSERT [dbo].[Company] ([Id], [Name]) VALUES (2, N'Частный предприниматель')
SET IDENTITY_INSERT [dbo].[Company] OFF
GO
SET IDENTITY_INSERT [dbo].[Contractor] ON 

INSERT [dbo].[Contractor] ([Id], [Name], [Address]) VALUES (1, N'Dns', N'Ул. пушкника 130')
INSERT [dbo].[Contractor] ([Id], [Name], [Address]) VALUES (2, N'Ситилинк', N'Ул. ленина 3')
SET IDENTITY_INSERT [dbo].[Contractor] OFF
GO
SET IDENTITY_INSERT [dbo].[DocumentItems] ON 

INSERT [dbo].[DocumentItems] ([Id], [DocumentId], [MaterialAssetsId], [Amount], [PriceForUnit]) VALUES (2, 11, 3, CAST(1.000 AS Decimal(9, 3)), 4000.0000)
INSERT [dbo].[DocumentItems] ([Id], [DocumentId], [MaterialAssetsId], [Amount], [PriceForUnit]) VALUES (3, 12, 15, CAST(1.000 AS Decimal(9, 3)), 12.0000)
INSERT [dbo].[DocumentItems] ([Id], [DocumentId], [MaterialAssetsId], [Amount], [PriceForUnit]) VALUES (4, 13, 3, CAST(1.000 AS Decimal(9, 3)), 4000.0000)
INSERT [dbo].[DocumentItems] ([Id], [DocumentId], [MaterialAssetsId], [Amount], [PriceForUnit]) VALUES (5, 14, 13, CAST(1.000 AS Decimal(9, 3)), 1244124.0000)
INSERT [dbo].[DocumentItems] ([Id], [DocumentId], [MaterialAssetsId], [Amount], [PriceForUnit]) VALUES (6, 1, 2, CAST(1.000 AS Decimal(9, 3)), 4000.0000)
INSERT [dbo].[DocumentItems] ([Id], [DocumentId], [MaterialAssetsId], [Amount], [PriceForUnit]) VALUES (7, 1, 3, CAST(4.000 AS Decimal(9, 3)), 4000.0000)
INSERT [dbo].[DocumentItems] ([Id], [DocumentId], [MaterialAssetsId], [Amount], [PriceForUnit]) VALUES (8, 1, 4, CAST(3.000 AS Decimal(9, 3)), 200.0000)
INSERT [dbo].[DocumentItems] ([Id], [DocumentId], [MaterialAssetsId], [Amount], [PriceForUnit]) VALUES (9, 8, 5, CAST(1.000 AS Decimal(9, 3)), 333.0000)
INSERT [dbo].[DocumentItems] ([Id], [DocumentId], [MaterialAssetsId], [Amount], [PriceForUnit]) VALUES (10, 8, 12, CAST(1.000 AS Decimal(9, 3)), 444.0000)
INSERT [dbo].[DocumentItems] ([Id], [DocumentId], [MaterialAssetsId], [Amount], [PriceForUnit]) VALUES (11, 8, 13, CAST(1.000 AS Decimal(9, 3)), 212.0000)
INSERT [dbo].[DocumentItems] ([Id], [DocumentId], [MaterialAssetsId], [Amount], [PriceForUnit]) VALUES (13, 8, 14, CAST(222.000 AS Decimal(9, 3)), 1.0000)
INSERT [dbo].[DocumentItems] ([Id], [DocumentId], [MaterialAssetsId], [Amount], [PriceForUnit]) VALUES (14, 8, 2, CAST(3.000 AS Decimal(9, 3)), 141.0000)
INSERT [dbo].[DocumentItems] ([Id], [DocumentId], [MaterialAssetsId], [Amount], [PriceForUnit]) VALUES (15, 8, 3, CAST(4.000 AS Decimal(9, 3)), 124.0000)
INSERT [dbo].[DocumentItems] ([Id], [DocumentId], [MaterialAssetsId], [Amount], [PriceForUnit]) VALUES (18, 8, 10, CAST(10.000 AS Decimal(9, 3)), 987.0000)
INSERT [dbo].[DocumentItems] ([Id], [DocumentId], [MaterialAssetsId], [Amount], [PriceForUnit]) VALUES (30, 1, 15, CAST(5.000 AS Decimal(9, 3)), 23.0000)
INSERT [dbo].[DocumentItems] ([Id], [DocumentId], [MaterialAssetsId], [Amount], [PriceForUnit]) VALUES (31, 8, 15, CAST(14.000 AS Decimal(9, 3)), 21.0000)
INSERT [dbo].[DocumentItems] ([Id], [DocumentId], [MaterialAssetsId], [Amount], [PriceForUnit]) VALUES (33, 1, 14, CAST(4.000 AS Decimal(9, 3)), 1.0000)
INSERT [dbo].[DocumentItems] ([Id], [DocumentId], [MaterialAssetsId], [Amount], [PriceForUnit]) VALUES (43, 1, 10, CAST(4.000 AS Decimal(9, 3)), 1.0000)
INSERT [dbo].[DocumentItems] ([Id], [DocumentId], [MaterialAssetsId], [Amount], [PriceForUnit]) VALUES (45, 1, 13, CAST(5.000 AS Decimal(9, 3)), 1.0000)
SET IDENTITY_INSERT [dbo].[DocumentItems] OFF
GO
SET IDENTITY_INSERT [dbo].[Documents] ON 

INSERT [dbo].[Documents] ([Id], [DocumentTypeId], [WerehouseId], [Name], [Date]) VALUES (1, 1, 1, N'Поступление МЗ 0000-000150 от 16.01.2023', CAST(N'2023-01-16T00:00:00.000' AS DateTime))
INSERT [dbo].[Documents] ([Id], [DocumentTypeId], [WerehouseId], [Name], [Date]) VALUES (2, 2, 1, N'Списание МЗ 000001 от 14.02.2023', CAST(N'2023-02-14T00:00:00.000' AS DateTime))
INSERT [dbo].[Documents] ([Id], [DocumentTypeId], [WerehouseId], [Name], [Date]) VALUES (8, 1, 1, N'Поступление МЗ 0000-000151 от 25.01.2023', CAST(N'2023-01-25T00:00:00.000' AS DateTime))
INSERT [dbo].[Documents] ([Id], [DocumentTypeId], [WerehouseId], [Name], [Date]) VALUES (9, 2, 1, N'Списание МЗ 000002 от 23.02.2023', CAST(N'2023-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[Documents] ([Id], [DocumentTypeId], [WerehouseId], [Name], [Date]) VALUES (11, 2, 1, N'499444', CAST(N'2023-04-06T00:00:00.000' AS DateTime))
INSERT [dbo].[Documents] ([Id], [DocumentTypeId], [WerehouseId], [Name], [Date]) VALUES (12, 2, 1, N'124124124124', CAST(N'2023-04-06T00:00:00.000' AS DateTime))
INSERT [dbo].[Documents] ([Id], [DocumentTypeId], [WerehouseId], [Name], [Date]) VALUES (13, 2, 1, N'4343412341234', CAST(N'2023-04-08T00:00:00.000' AS DateTime))
INSERT [dbo].[Documents] ([Id], [DocumentTypeId], [WerehouseId], [Name], [Date]) VALUES (14, 2, 1, N'123123123', CAST(N'2023-04-09T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Documents] OFF
GO
SET IDENTITY_INSERT [dbo].[DocumentTypes] ON 

INSERT [dbo].[DocumentTypes] ([Id], [Name]) VALUES (1, N'Приход')
INSERT [dbo].[DocumentTypes] ([Id], [Name]) VALUES (2, N'Расход')
SET IDENTITY_INSERT [dbo].[DocumentTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([Id], [Surname], [Name], [Patronymic], [Login], [Password], [RoleId]) VALUES (1, N'Иванов', N'Иван', N'Иванович', N'1111', N'1111', 1)
INSERT [dbo].[Employee] ([Id], [Surname], [Name], [Patronymic], [Login], [Password], [RoleId]) VALUES (5, N'Андреева', N'Полина', N'Максимовна', N'2222', N'2222', 2)
INSERT [dbo].[Employee] ([Id], [Surname], [Name], [Patronymic], [Login], [Password], [RoleId]) VALUES (25, N'345634563456', N'34564536345634', N'56345643563456', N'34563456', N'3456345643', 2)
INSERT [dbo].[Employee] ([Id], [Surname], [Name], [Patronymic], [Login], [Password], [RoleId]) VALUES (26, N'23414', N'124124124', N'124124124', N'124124124', N'124124124124', 1)
INSERT [dbo].[Employee] ([Id], [Surname], [Name], [Patronymic], [Login], [Password], [RoleId]) VALUES (27, N'иванов', N'алекс', N'иванович1', N'44444', N'44444', 2)
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
SET IDENTITY_INSERT [dbo].[MaterialAssets] ON 

INSERT [dbo].[MaterialAssets] ([Id], [Name], [ItemNumber], [MeasureUnitId]) VALUES (2, N'Акамулятор Delta', N'111111', 1)
INSERT [dbo].[MaterialAssets] ([Id], [Name], [ItemNumber], [MeasureUnitId]) VALUES (3, N'БП Xilence', N'111112', 1)
INSERT [dbo].[MaterialAssets] ([Id], [Name], [ItemNumber], [MeasureUnitId]) VALUES (4, N'Корпус AeroCool', N'111113', 1)
INSERT [dbo].[MaterialAssets] ([Id], [Name], [ItemNumber], [MeasureUnitId]) VALUES (5, N'Компьютерная мышь Aceline', N'111114', 1)
INSERT [dbo].[MaterialAssets] ([Id], [Name], [ItemNumber], [MeasureUnitId]) VALUES (10, N'5422345234523432', N'1231231231235235', 1)
INSERT [dbo].[MaterialAssets] ([Id], [Name], [ItemNumber], [MeasureUnitId]) VALUES (12, N'kkeke', N'1124124', 1)
INSERT [dbo].[MaterialAssets] ([Id], [Name], [ItemNumber], [MeasureUnitId]) VALUES (13, N'12421412', N'4412441', 1)
INSERT [dbo].[MaterialAssets] ([Id], [Name], [ItemNumber], [MeasureUnitId]) VALUES (14, N'товар такой то ', N'14414', 1)
INSERT [dbo].[MaterialAssets] ([Id], [Name], [ItemNumber], [MeasureUnitId]) VALUES (15, N'1111', N'1111', 1)
SET IDENTITY_INSERT [dbo].[MaterialAssets] OFF
GO
SET IDENTITY_INSERT [dbo].[MeasureUnits] ON 

INSERT [dbo].[MeasureUnits] ([Id], [Name]) VALUES (1, N'шт.')
SET IDENTITY_INSERT [dbo].[MeasureUnits] OFF
GO
SET IDENTITY_INSERT [dbo].[Priority] ON 

INSERT [dbo].[Priority] ([Id], [Name]) VALUES (1, N'Высокий')
INSERT [dbo].[Priority] ([Id], [Name]) VALUES (2, N'Средний')
INSERT [dbo].[Priority] ([Id], [Name]) VALUES (3, N'Низкий')
SET IDENTITY_INSERT [dbo].[Priority] OFF
GO
SET IDENTITY_INSERT [dbo].[Receipt] ON 

INSERT [dbo].[Receipt] ([Id], [Number], [Date], [СontractorId], [DocumentId]) VALUES (1, N'0000-000150', CAST(N'2023-01-16' AS Date), 1, 1)
INSERT [dbo].[Receipt] ([Id], [Number], [Date], [СontractorId], [DocumentId]) VALUES (2, N'0000-000151', CAST(N'2023-01-25' AS Date), 2, 8)
SET IDENTITY_INSERT [dbo].[Receipt] OFF
GO
SET IDENTITY_INSERT [dbo].[Request] ON 

INSERT [dbo].[Request] ([Id], [StatusId], [Date], [Deadline], [ThemeId], [Customer], [ExecutorId], [PriorityId], [СompanyId], [Creator], [Address], [Content], [DocumentId]) VALUES (1, 2, CAST(N'2023-02-13T00:00:00.000' AS DateTime), CAST(N'2023-02-14T00:00:00.000' AS DateTime), 4, N'Горшкова Арина Гордеевна', 5, 1, 1, N'Иванов Иван Иванович', N'Ленина 67', N'Поменять мышь в каб 105', 2)
INSERT [dbo].[Request] ([Id], [StatusId], [Date], [Deadline], [ThemeId], [Customer], [ExecutorId], [PriorityId], [СompanyId], [Creator], [Address], [Content], [DocumentId]) VALUES (4, 2, CAST(N'2023-02-22T00:00:00.000' AS DateTime), CAST(N'2023-02-23T00:00:00.000' AS DateTime), 2, N'Горшкова Арина Гордеевна', 5, 1, 1, N'Иванов Иван Иванович', N'Ленина 67', N'Поменять картридж в каб 105', 9)
INSERT [dbo].[Request] ([Id], [StatusId], [Date], [Deadline], [ThemeId], [Customer], [ExecutorId], [PriorityId], [СompanyId], [Creator], [Address], [Content], [DocumentId]) VALUES (6, 1, CAST(N'2023-04-05T00:00:00.000' AS DateTime), CAST(N'2023-04-06T00:00:00.000' AS DateTime), 4, N'Юнгейм Светлана Игоревна', 1, 1, 1, N'Иванов Иван Иванович', N'г. Абакан', N'Замена БП', 11)
INSERT [dbo].[Request] ([Id], [StatusId], [Date], [Deadline], [ThemeId], [Customer], [ExecutorId], [PriorityId], [СompanyId], [Creator], [Address], [Content], [DocumentId]) VALUES (8, 2, CAST(N'2023-04-08T00:00:00.000' AS DateTime), CAST(N'2023-04-03T00:00:00.000' AS DateTime), 2, N'14121431234', 1, 3, 2, N'123412341', N'1234123412341234', N'123412341234', 13)
INSERT [dbo].[Request] ([Id], [StatusId], [Date], [Deadline], [ThemeId], [Customer], [ExecutorId], [PriorityId], [СompanyId], [Creator], [Address], [Content], [DocumentId]) VALUES (9, 1, CAST(N'2023-04-05T00:00:00.000' AS DateTime), CAST(N'2023-04-09T00:00:00.000' AS DateTime), 1, N'13123123', 1, 2, 1, N'123123123', N'123123123', N'123123123', 14)
SET IDENTITY_INSERT [dbo].[Request] OFF
GO
SET IDENTITY_INSERT [dbo].[Reserve] ON 

INSERT [dbo].[Reserve] ([Id], [MaterialAssetsId], [MinAmount], [MaxAmount]) VALUES (1, 5, 1, 10)
INSERT [dbo].[Reserve] ([Id], [MaterialAssetsId], [MinAmount], [MaxAmount]) VALUES (2, 2, 1, 5)
SET IDENTITY_INSERT [dbo].[Reserve] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [Name]) VALUES (1, N'Администратор')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (2, N'Пользователь')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Status] ON 

INSERT [dbo].[Status] ([Id], [Name]) VALUES (1, N'Открыта')
INSERT [dbo].[Status] ([Id], [Name]) VALUES (2, N'Закрыта')
SET IDENTITY_INSERT [dbo].[Status] OFF
GO
SET IDENTITY_INSERT [dbo].[Storages] ON 

INSERT [dbo].[Storages] ([Id], [Name], [Note]) VALUES (1, N'Склад 1', N'склад')
INSERT [dbo].[Storages] ([Id], [Name], [Note]) VALUES (3, N'Склад 2', N'склад')
SET IDENTITY_INSERT [dbo].[Storages] OFF
GO
SET IDENTITY_INSERT [dbo].[Supplies] ON 

INSERT [dbo].[Supplies] ([Id], [MaterialAssetsId], [WarehousesId], [Amount], [PriceForUnit]) VALUES (1, 5, 1, CAST(3.000 AS Decimal(9, 3)), 120.0000)
INSERT [dbo].[Supplies] ([Id], [MaterialAssetsId], [WarehousesId], [Amount], [PriceForUnit]) VALUES (3, 3, 1, CAST(3.000 AS Decimal(9, 3)), 4000.0000)
INSERT [dbo].[Supplies] ([Id], [MaterialAssetsId], [WarehousesId], [Amount], [PriceForUnit]) VALUES (5, 4, 1, CAST(1.000 AS Decimal(9, 3)), 3000.0000)
INSERT [dbo].[Supplies] ([Id], [MaterialAssetsId], [WarehousesId], [Amount], [PriceForUnit]) VALUES (7, 13, 3, CAST(123.000 AS Decimal(9, 3)), 1244124.0000)
INSERT [dbo].[Supplies] ([Id], [MaterialAssetsId], [WarehousesId], [Amount], [PriceForUnit]) VALUES (8, 14, 1, CAST(3.000 AS Decimal(9, 3)), 3.0000)
INSERT [dbo].[Supplies] ([Id], [MaterialAssetsId], [WarehousesId], [Amount], [PriceForUnit]) VALUES (9, 15, 1, CAST(1110.000 AS Decimal(9, 3)), 12.0000)
SET IDENTITY_INSERT [dbo].[Supplies] OFF
GO
SET IDENTITY_INSERT [dbo].[Themes] ON 

INSERT [dbo].[Themes] ([Id], [Name], [Note]) VALUES (1, N'Вода', N'Вода')
INSERT [dbo].[Themes] ([Id], [Name], [Note]) VALUES (2, N'Заправка картриджа', NULL)
INSERT [dbo].[Themes] ([Id], [Name], [Note]) VALUES (3, N'Проблема с сетью', NULL)
INSERT [dbo].[Themes] ([Id], [Name], [Note]) VALUES (4, N'Замена оборудования', NULL)
SET IDENTITY_INSERT [dbo].[Themes] OFF
GO
ALTER TABLE [dbo].[DocumentItems]  WITH CHECK ADD  CONSTRAINT [FK_DocumentItems_Documents1] FOREIGN KEY([DocumentId])
REFERENCES [dbo].[Documents] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DocumentItems] CHECK CONSTRAINT [FK_DocumentItems_Documents1]
GO
ALTER TABLE [dbo].[DocumentItems]  WITH CHECK ADD  CONSTRAINT [FK_DocumentItems_MaterialAssets1] FOREIGN KEY([MaterialAssetsId])
REFERENCES [dbo].[MaterialAssets] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DocumentItems] CHECK CONSTRAINT [FK_DocumentItems_MaterialAssets1]
GO
ALTER TABLE [dbo].[Documents]  WITH CHECK ADD  CONSTRAINT [FK_Documents_DocumentTypes1] FOREIGN KEY([DocumentTypeId])
REFERENCES [dbo].[DocumentTypes] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Documents] CHECK CONSTRAINT [FK_Documents_DocumentTypes1]
GO
ALTER TABLE [dbo].[Documents]  WITH CHECK ADD  CONSTRAINT [FK_Documents_Warehouses1] FOREIGN KEY([WerehouseId])
REFERENCES [dbo].[Storages] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Documents] CHECK CONSTRAINT [FK_Documents_Warehouses1]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Role]
GO
ALTER TABLE [dbo].[MaterialAssets]  WITH CHECK ADD  CONSTRAINT [FK_MaterialAssets_MeasureUnits1] FOREIGN KEY([MeasureUnitId])
REFERENCES [dbo].[MeasureUnits] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MaterialAssets] CHECK CONSTRAINT [FK_MaterialAssets_MeasureUnits1]
GO
ALTER TABLE [dbo].[Receipt]  WITH CHECK ADD  CONSTRAINT [FK_Receipt_Contractor] FOREIGN KEY([СontractorId])
REFERENCES [dbo].[Contractor] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Receipt] CHECK CONSTRAINT [FK_Receipt_Contractor]
GO
ALTER TABLE [dbo].[Receipt]  WITH CHECK ADD  CONSTRAINT [FK_Receipt_Documents] FOREIGN KEY([DocumentId])
REFERENCES [dbo].[Documents] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Receipt] CHECK CONSTRAINT [FK_Receipt_Documents]
GO
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_Documents] FOREIGN KEY([DocumentId])
REFERENCES [dbo].[Documents] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_Documents]
GO
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_Employee] FOREIGN KEY([ExecutorId])
REFERENCES [dbo].[Employee] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_Employee]
GO
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_Priority] FOREIGN KEY([СompanyId])
REFERENCES [dbo].[Company] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_Priority]
GO
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_Priority1] FOREIGN KEY([PriorityId])
REFERENCES [dbo].[Priority] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_Priority1]
GO
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_Status]
GO
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_Themes] FOREIGN KEY([ThemeId])
REFERENCES [dbo].[Themes] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_Themes]
GO
ALTER TABLE [dbo].[Reserve]  WITH CHECK ADD  CONSTRAINT [FK_Reserve_MaterialAssets1] FOREIGN KEY([MaterialAssetsId])
REFERENCES [dbo].[MaterialAssets] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reserve] CHECK CONSTRAINT [FK_Reserve_MaterialAssets1]
GO
ALTER TABLE [dbo].[Supplies]  WITH CHECK ADD  CONSTRAINT [FK_Remains_MaterialAssets] FOREIGN KEY([MaterialAssetsId])
REFERENCES [dbo].[MaterialAssets] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Supplies] CHECK CONSTRAINT [FK_Remains_MaterialAssets]
GO
ALTER TABLE [dbo].[Supplies]  WITH CHECK ADD  CONSTRAINT [FK_Remains_Warehouses] FOREIGN KEY([WarehousesId])
REFERENCES [dbo].[Storages] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Supplies] CHECK CONSTRAINT [FK_Remains_Warehouses]
GO
USE [master]
GO
ALTER DATABASE [Warehouse] SET  READ_WRITE 
GO
