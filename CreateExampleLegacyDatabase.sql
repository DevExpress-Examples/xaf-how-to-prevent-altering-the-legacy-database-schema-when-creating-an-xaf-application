USE [master]
GO
/****** Object:  Database [E1150LegacyDatabase]    Script Date: 30.06.2022 0:02:01 ******/
CREATE DATABASE [E1150LegacyDatabase]
GO
ALTER DATABASE [E1150LegacyDatabase] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [E1150LegacyDatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [E1150LegacyDatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [E1150LegacyDatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [E1150LegacyDatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [E1150LegacyDatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [E1150LegacyDatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [E1150LegacyDatabase] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [E1150LegacyDatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [E1150LegacyDatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [E1150LegacyDatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [E1150LegacyDatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [E1150LegacyDatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [E1150LegacyDatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [E1150LegacyDatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [E1150LegacyDatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [E1150LegacyDatabase] SET  ENABLE_BROKER 
GO
ALTER DATABASE [E1150LegacyDatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [E1150LegacyDatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [E1150LegacyDatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [E1150LegacyDatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [E1150LegacyDatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [E1150LegacyDatabase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [E1150LegacyDatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [E1150LegacyDatabase] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [E1150LegacyDatabase] SET  MULTI_USER 
GO
ALTER DATABASE [E1150LegacyDatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [E1150LegacyDatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [E1150LegacyDatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [E1150LegacyDatabase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [E1150LegacyDatabase] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [E1150LegacyDatabase] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [E1150LegacyDatabase] SET QUERY_STORE = OFF
GO
USE [E1150LegacyDatabase]
GO
/****** Object:  Table [dbo].[Note]    Script Date: 30.06.2022 0:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Note](
	[Oid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Author] [nvarchar](100) NULL,
	[DateTime] [datetime] NULL,
	[Text] [nvarchar](max) NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
 CONSTRAINT [PK_Note] PRIMARY KEY CLUSTERED 
(
	[Oid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Note] ([Oid], [Author], [DateTime], [Text], [OptimisticLockField], [GCRecord]) VALUES (N'6b837db2-1286-4061-8f6c-0042bc6b918b', N'Somebody', CAST(N'2022-06-28T23:57:59.617' AS DateTime), N'is clearly not a short term thinker - the ability to set short and long term business goals is a great asset to the company', 0, NULL)
GO
INSERT [dbo].[Note] ([Oid], [Author], [DateTime], [Text], [OptimisticLockField], [GCRecord]) VALUES (N'62219033-7059-48b4-9038-0d5b214745a0', N'Somebody', CAST(N'2022-06-28T23:57:59.610' AS DateTime), N'is very good at making team members feel included. The inclusion has improved the team''s productivity dramatically', 0, NULL)
GO
INSERT [dbo].[Note] ([Oid], [Author], [DateTime], [Text], [OptimisticLockField], [GCRecord]) VALUES (N'fc856055-51e8-432c-afb4-37e58f7dea07', N'Somebody', CAST(N'2022-06-28T23:57:59.623' AS DateTime), N'to be discussed with the top management...', 0, NULL)
GO
INSERT [dbo].[Note] ([Oid], [Author], [DateTime], [Text], [OptimisticLockField], [GCRecord]) VALUES (N'155c6558-75b0-4c28-81ed-7e2a0079f4b3', N'Somebody', CAST(N'2022-06-28T23:57:59.617' AS DateTime), N'creates an inclusive work environment where everyone feels they are a part of the team', 0, NULL)
GO
INSERT [dbo].[Note] ([Oid], [Author], [DateTime], [Text], [OptimisticLockField], [GCRecord]) VALUES (N'b1ae6c5c-c654-46bf-b6f8-8e7355378e94', N'Somebody', CAST(N'2022-06-28T23:57:59.617' AS DateTime), N'consistently keeps up on new trends in the industry and applies these new practices to every day work', 0, NULL)
GO
INSERT [dbo].[Note] ([Oid], [Author], [DateTime], [Text], [OptimisticLockField], [GCRecord]) VALUES (N'12946b3c-5440-499c-a94d-a4f389f637e3', N'Somebody', CAST(N'2022-06-28T23:57:59.620' AS DateTime), N'seems to want to achieve all of the goals in the last few weeks before annual performance review time, but does not consistently work towards the goals throughout the year', 0, NULL)
GO
INSERT [dbo].[Note] ([Oid], [Author], [DateTime], [Text], [OptimisticLockField], [GCRecord]) VALUES (N'faccd9c2-10ac-4199-b74c-b61e0b29706f', N'Somebody', CAST(N'2022-06-28T23:57:48.003' AS DateTime), N'works with customers until their problems are resolved and often goes an extra step to help upset customers be completely surprised by how far we will go to satisfy customers', 0, NULL)
GO
INSERT [dbo].[Note] ([Oid], [Author], [DateTime], [Text], [OptimisticLockField], [GCRecord]) VALUES (N'fbbac239-1dcc-4dcc-a67b-c0d872e2ba9d', N'Somebody', CAST(N'2022-06-28T23:57:59.620' AS DateTime), N'does not yet delegate effectively and has a tendency to be overloaded with tasks which should be handed off to subordinates', 0, NULL)
GO
INSERT [dbo].[Note] ([Oid], [Author], [DateTime], [Text], [OptimisticLockField], [GCRecord]) VALUES (N'1ec7d99d-e907-46bd-a202-d140a1ff49e7', N'Somebody', CAST(N'2022-06-28T23:57:59.613' AS DateTime), N'actively elicits feedback from customers and works to resolve their problems', 0, NULL)
GO
INSERT [dbo].[Note] ([Oid], [Author], [DateTime], [Text], [OptimisticLockField], [GCRecord]) VALUES (N'c73f2ae9-a72b-48ea-9888-d9f60ebf2eda', N'Somebody', CAST(N'2022-06-28T23:57:59.613' AS DateTime), N'is very good at sharing knowledge and information during a problem to increase the chance it will be resolved quickly', 0, NULL)
GO
/****** Object:  Index [iGCRecord_Note]    Script Date: 30.06.2022 0:02:01 ******/
CREATE NONCLUSTERED INDEX [iGCRecord_Note] ON [dbo].[Note]
(
	[GCRecord] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [E1150LegacyDatabase] SET  READ_WRITE 
GO
