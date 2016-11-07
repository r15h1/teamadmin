USE [TeamAdminTests]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Teams_Clubs]') AND parent_object_id = OBJECT_ID(N'[dbo].[Teams]'))
ALTER TABLE [dbo].[Teams] DROP CONSTRAINT [FK_Teams_Clubs]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClubMedia_MediaTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[ClubMedia]'))
ALTER TABLE [dbo].[ClubMedia] DROP CONSTRAINT [FK_ClubMedia_MediaTypes]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClubMedia_Clubs]') AND parent_object_id = OBJECT_ID(N'[dbo].[ClubMedia]'))
ALTER TABLE [dbo].[ClubMedia] DROP CONSTRAINT [FK_ClubMedia_Clubs]
GO
/****** Object:  Table [dbo].[Teams]    Script Date: 07/11/2016 3:40:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Teams]') AND type in (N'U'))
DROP TABLE [dbo].[Teams]
GO
/****** Object:  Table [dbo].[MediaTypes]    Script Date: 07/11/2016 3:40:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MediaTypes]') AND type in (N'U'))
DROP TABLE [dbo].[MediaTypes]
GO
/****** Object:  Table [dbo].[Clubs]    Script Date: 07/11/2016 3:40:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Clubs]') AND type in (N'U'))
DROP TABLE [dbo].[Clubs]
GO
/****** Object:  Table [dbo].[ClubMedia]    Script Date: 07/11/2016 3:40:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClubMedia]') AND type in (N'U'))
DROP TABLE [dbo].[ClubMedia]
GO
/****** Object:  Table [dbo].[ClubMedia]    Script Date: 07/11/2016 3:40:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClubMedia]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ClubMedia](
	[ClubId] [int] NOT NULL,
	[MediaId] [bigint] IDENTITY(1,1) NOT NULL,
	[MediaTypeId] [tinyint] NOT NULL,
	[Url] [varchar](500) NOT NULL,
	[Caption] [varchar](200) NULL,
	[Position] [int] NOT NULL,
 CONSTRAINT [PK_ClubMedia] PRIMARY KEY CLUSTERED 
(
	[MediaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ClubMedia] UNIQUE NONCLUSTERED 
(
	[ClubId] ASC,
	[Position] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Clubs]    Script Date: 07/11/2016 3:40:05 PM ******/
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
	[Street] [varchar](50) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[Province] [varchar](50) NULL,
	[Country] [varchar](50) NOT NULL,
	[PostalCode] [varchar](6) NULL,
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
/****** Object:  Table [dbo].[MediaTypes]    Script Date: 07/11/2016 3:40:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MediaTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MediaTypes](
	[MediaTypeId] [tinyint] NOT NULL,
	[Description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_MediaTypes] PRIMARY KEY CLUSTERED 
(
	[MediaTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Teams]    Script Date: 07/11/2016 3:40:05 PM ******/
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
	[Deleted] [bit] NULL,
 CONSTRAINT [PK_Teams] PRIMARY KEY CLUSTERED 
(
	[TeamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Teams] UNIQUE NONCLUSTERED 
(
	[ClubId] ASC,
	[TeamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClubMedia_Clubs]') AND parent_object_id = OBJECT_ID(N'[dbo].[ClubMedia]'))
ALTER TABLE [dbo].[ClubMedia]  WITH CHECK ADD  CONSTRAINT [FK_ClubMedia_Clubs] FOREIGN KEY([ClubId])
REFERENCES [dbo].[Clubs] ([ClubId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClubMedia_Clubs]') AND parent_object_id = OBJECT_ID(N'[dbo].[ClubMedia]'))
ALTER TABLE [dbo].[ClubMedia] CHECK CONSTRAINT [FK_ClubMedia_Clubs]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClubMedia_MediaTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[ClubMedia]'))
ALTER TABLE [dbo].[ClubMedia]  WITH CHECK ADD  CONSTRAINT [FK_ClubMedia_MediaTypes] FOREIGN KEY([MediaTypeId])
REFERENCES [dbo].[MediaTypes] ([MediaTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClubMedia_MediaTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[ClubMedia]'))
ALTER TABLE [dbo].[ClubMedia] CHECK CONSTRAINT [FK_ClubMedia_MediaTypes]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Teams_Clubs]') AND parent_object_id = OBJECT_ID(N'[dbo].[Teams]'))
ALTER TABLE [dbo].[Teams]  WITH CHECK ADD  CONSTRAINT [FK_Teams_Clubs] FOREIGN KEY([ClubId])
REFERENCES [dbo].[Clubs] ([ClubId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Teams_Clubs]') AND parent_object_id = OBJECT_ID(N'[dbo].[Teams]'))
ALTER TABLE [dbo].[Teams] CHECK CONSTRAINT [FK_Teams_Clubs]
GO

INSERT [dbo].[MediaTypes] ([MediaTypeId], [Description]) VALUES (1, N'Image')
INSERT [dbo].[MediaTypes] ([MediaTypeId], [Description]) VALUES (2, N'Video')
INSERT [dbo].[MediaTypes] ([MediaTypeId], [Description]) VALUES (3, N'Logo')
GO