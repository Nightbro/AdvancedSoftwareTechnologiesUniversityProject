USE [master]
GO
/****** Object:  Database [RestourantManager]    Script Date: 5/5/2021 8:47:00 PM ******/
CREATE DATABASE [RestourantManager]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RestourantManager', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\RestourantManager.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RestourantManager_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\RestourantManager_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [RestourantManager] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RestourantManager].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RestourantManager] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RestourantManager] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RestourantManager] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RestourantManager] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RestourantManager] SET ARITHABORT OFF 
GO
ALTER DATABASE [RestourantManager] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RestourantManager] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RestourantManager] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RestourantManager] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RestourantManager] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RestourantManager] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RestourantManager] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RestourantManager] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RestourantManager] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RestourantManager] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RestourantManager] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RestourantManager] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RestourantManager] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RestourantManager] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RestourantManager] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RestourantManager] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RestourantManager] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RestourantManager] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [RestourantManager] SET  MULTI_USER 
GO
ALTER DATABASE [RestourantManager] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RestourantManager] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RestourantManager] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RestourantManager] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RestourantManager] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RestourantManager] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [RestourantManager] SET QUERY_STORE = OFF
GO
USE [RestourantManager]
GO
/****** Object:  User [RMAdmin]    Script Date: 5/5/2021 8:47:00 PM ******/
CREATE USER [RMAdmin] FOR LOGIN [RMAdmin] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [RMAdmin]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 5/5/2021 8:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](30) NULL,
	[IsAdmin] [binary](1) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 5/5/2021 8:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](30) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [varbinary](128) NULL,
	[RoleID] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_UserRole] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_UserRole]
GO
USE [master]
GO
ALTER DATABASE [RestourantManager] SET  READ_WRITE 
GO




UPDATE [dbo].[User]
SET [Password]=CONVERT(varbinary,'Test1234')
WHERE ID = 5


ALTER TABLE [User]
ADD CONSTRAINT FK_UserRole FOREIGN KEY (RoleID) REFERENCES [Role](ID)

ALTER TABLE [User]
ALTER COLUMN [Email] varchar(50) NOT NULL;

CREATE TABLE [dbo].[Claim](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](30) NULL,
	[Description] [varchar](100) NULL,
 CONSTRAINT [PK_Claim] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[RoleClaim](
	[RoleID] [int] NOT NULL,
	[ClaimID] [int] NOT NULL)



ALTER TABLE [RoleClaim]
ADD CONSTRAINT FK_RoleRoleClaim FOREIGN KEY (RoleID) REFERENCES [Role](ID)


CREATE TABLE dbo.UserImages

(
	  [UserID] [int],

      [ImageName] [varchar](40) NOT NULL,

      [OriginalFormat] [nvarchar](5) NOT NULL, 

      [ImageFile] [varbinary](max) NOT NULL

 )    
  ALTER TABLE [UserImages]
ADD CONSTRAINT FK_UserUserImages FOREIGN KEY (UserID) REFERENCES [User](ID)


INSERT INTO [dbo].[UserImages]

(
		UserID
       ,ImageName

      ,OriginalFormat

      ,ImageFile

)

SELECT
		5,
      'mmatic'

      ,'jpg'

      ,ImageFile

FROM OPENROWSET(BULK N'D:\Repositories\mmatic.jpg', SINGLE_BLOB) AS ImageSource(ImageFile);