USE [TeamAdmin]
GO

CREATE TABLE [dbo].[ClubMedia](
	[ClubId] [int] NOT NULL,
	[MediaId] [bigint] NOT NULL,
 CONSTRAINT [PK_ClubMedia_1] PRIMARY KEY CLUSTERED 
(
	[ClubId] ASC,
	[MediaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

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

GO

CREATE TABLE [dbo].[Media](
	[MediaId] [bigint] IDENTITY(1,1) NOT NULL,
	[MediaTypeId] [tinyint] NOT NULL,
	[Url] [varchar](500) NOT NULL,
	[Caption] [varchar](200) NULL,
 CONSTRAINT [PK_ClubMedia] PRIMARY KEY CLUSTERED 
(
	[MediaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[MediaTypes](
	[MediaTypeId] [tinyint] NOT NULL,
	[Description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_MediaTypes] PRIMARY KEY CLUSTERED 
(
	[MediaTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[TeamMedia](
	[ClubId] [int] NOT NULL,
	[TeamId] [int] NOT NULL,
	[MediaId] [bigint] NOT NULL,
 CONSTRAINT [PK_TeamMedia] PRIMARY KEY CLUSTERED 
(
	[ClubId] ASC,
	[TeamId] ASC,
	[MediaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

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
GO

ALTER TABLE [dbo].[ClubMedia]  WITH CHECK ADD  CONSTRAINT [FK_ClubMedia_Clubs] FOREIGN KEY([ClubId])
REFERENCES [dbo].[Clubs] ([ClubId])
GO

ALTER TABLE [dbo].[ClubMedia] CHECK CONSTRAINT [FK_ClubMedia_Clubs]
GO

ALTER TABLE [dbo].[ClubMedia]  WITH CHECK ADD  CONSTRAINT [FK_ClubMedia_Media] FOREIGN KEY([MediaId])
REFERENCES [dbo].[Media] ([MediaId])
GO

ALTER TABLE [dbo].[ClubMedia] CHECK CONSTRAINT [FK_ClubMedia_Media]
GO

ALTER TABLE [dbo].[Media]  WITH CHECK ADD  CONSTRAINT [FK_ClubMedia_MediaTypes] FOREIGN KEY([MediaTypeId])
REFERENCES [dbo].[MediaTypes] ([MediaTypeId])
GO

ALTER TABLE [dbo].[Media] CHECK CONSTRAINT [FK_ClubMedia_MediaTypes]
GO

ALTER TABLE [dbo].[TeamMedia]  WITH CHECK ADD  CONSTRAINT [FK_TeamMedia_Media] FOREIGN KEY([MediaId])
REFERENCES [dbo].[Media] ([MediaId])
GO

ALTER TABLE [dbo].[TeamMedia] CHECK CONSTRAINT [FK_TeamMedia_Media]
GO

ALTER TABLE [dbo].[TeamMedia]  WITH CHECK ADD  CONSTRAINT [FK_TeamMedia_Teams] FOREIGN KEY([ClubId], [TeamId])
REFERENCES [dbo].[Teams] ([ClubId], [TeamId])
GO

ALTER TABLE [dbo].[TeamMedia] CHECK CONSTRAINT [FK_TeamMedia_Teams]
GO

ALTER TABLE [dbo].[Teams]  WITH CHECK ADD  CONSTRAINT [FK_Teams_Clubs] FOREIGN KEY([ClubId])
REFERENCES [dbo].[Clubs] ([ClubId])
GO

ALTER TABLE [dbo].[Teams] CHECK CONSTRAINT [FK_Teams_Clubs]
GO
