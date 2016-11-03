/*
Create a database called [TeamAdmin], preferably in SQL2014
Then run this script
*/


USE [TeamAdmin]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Teams_Clubs]') AND parent_object_id = OBJECT_ID(N'[dbo].[Teams]'))
ALTER TABLE [dbo].[Teams] DROP CONSTRAINT [FK_Teams_Clubs]
GO
/****** Object:  Table [dbo].[Teams]    Script Date: 01/11/2016 9:33:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Teams]') AND type in (N'U'))
DROP TABLE [dbo].[Teams]
GO
/****** Object:  Table [dbo].[Clubs]    Script Date: 01/11/2016 9:33:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Clubs]') AND type in (N'U'))
DROP TABLE [dbo].[Clubs]
GO

USE [TeamAdmin]
GO
/****** Object:  Table [dbo].[Clubs]    Script Date: 01/11/2016 9:33:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Clubs]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Clubs](
	[ClubId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Address] [varchar](50) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[Province] [varchar](50) NULL,
	[Country] [varchar](50) NOT NULL,
	[PostalCode] [varchar](6) NOT NULL,
	[Deleted] [bit] NULL,
 CONSTRAINT [PK_Clubs] PRIMARY KEY CLUSTERED 
(
	[ClubId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Teams]    Script Date: 01/11/2016 9:33:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Teams]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Teams](
	[ClubId] [int] NOT NULL,
	[TeamId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Teams] PRIMARY KEY CLUSTERED 
(
	[TeamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Teams_Clubs]') AND parent_object_id = OBJECT_ID(N'[dbo].[Teams]'))
ALTER TABLE [dbo].[Teams]  WITH CHECK ADD  CONSTRAINT [FK_Teams_Clubs] FOREIGN KEY([ClubId])
REFERENCES [dbo].[Clubs] ([ClubId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Teams_Clubs]') AND parent_object_id = OBJECT_ID(N'[dbo].[Teams]'))
ALTER TABLE [dbo].[Teams] CHECK CONSTRAINT [FK_Teams_Clubs]
GO
