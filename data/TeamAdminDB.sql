
USE [TeamAdmin]
GO

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
	[MediaTypeId] ASC,
	[Position] ASC
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
CREATE TABLE [dbo].[ClubTeamEvents](
	[ClubTeamEventId] [bigint] IDENTITY(1,1) NOT NULL,
	[ClubId] [int] NOT NULL,
	[TeamId] [int] NULL,
	[EventId] [bigint] NOT NULL,
 CONSTRAINT [PK_ClubTeamEvents] PRIMARY KEY CLUSTERED 
(
	[ClubTeamEventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ClubTeamEvents] UNIQUE NONCLUSTERED 
(
	[ClubId] ASC,
	[TeamId] ASC,
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Events](
	[EventId] [bigint] IDENTITY(1,1) NOT NULL,
	[EventTypeId] [tinyint] NOT NULL,
	[Title] [varchar](250) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[StartDate] [datetime2](0) NOT NULL,
	[EndDate] [datetime2](0) NOT NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE TABLE [dbo].[EventTypes](
	[EventTypeId] [tinyint] NOT NULL,
	[EventTypeName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_EventTypes] PRIMARY KEY CLUSTERED 
(
	[EventTypeId] ASC
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
	[TeamId] [int] NOT NULL,
	[MediaId] [bigint] IDENTITY(1,1) NOT NULL,
	[MediaTypeId] [tinyint] NOT NULL,
	[Url] [varchar](500) NOT NULL,
	[Caption] [varchar](200) NULL,
	[Position] [int] NULL,
 CONSTRAINT [PK_TeamMedia] PRIMARY KEY CLUSTERED 
(
	[MediaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_TeamMedia] UNIQUE NONCLUSTERED 
(
	[TeamId] ASC,
	[MediaTypeId] ASC,
	[Position] ASC
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
ALTER TABLE [dbo].[ClubMedia]  WITH CHECK ADD  CONSTRAINT [FK_ClubMedia_MediaTypes] FOREIGN KEY([MediaTypeId])
REFERENCES [dbo].[MediaTypes] ([MediaTypeId])
GO
ALTER TABLE [dbo].[ClubMedia] CHECK CONSTRAINT [FK_ClubMedia_MediaTypes]
GO
ALTER TABLE [dbo].[ClubTeamEvents]  WITH CHECK ADD  CONSTRAINT [FK_ClubTeamEvents_Events] FOREIGN KEY([EventId])
REFERENCES [dbo].[Events] ([EventId])
GO
ALTER TABLE [dbo].[ClubTeamEvents] CHECK CONSTRAINT [FK_ClubTeamEvents_Events]
GO
ALTER TABLE [dbo].[ClubTeamEvents]  WITH CHECK ADD  CONSTRAINT [FK_ClubTeamEvents_Teams] FOREIGN KEY([ClubId], [TeamId])
REFERENCES [dbo].[Teams] ([ClubId], [TeamId])
GO
ALTER TABLE [dbo].[ClubTeamEvents] CHECK CONSTRAINT [FK_ClubTeamEvents_Teams]
GO
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_EventTypes] FOREIGN KEY([EventTypeId])
REFERENCES [dbo].[EventTypes] ([EventTypeId])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_EventTypes]
GO
ALTER TABLE [dbo].[TeamMedia]  WITH CHECK ADD  CONSTRAINT [FK_TeamMedia_MediaTypes] FOREIGN KEY([MediaTypeId])
REFERENCES [dbo].[MediaTypes] ([MediaTypeId])
GO
ALTER TABLE [dbo].[TeamMedia] CHECK CONSTRAINT [FK_TeamMedia_MediaTypes]
GO
ALTER TABLE [dbo].[TeamMedia]  WITH CHECK ADD  CONSTRAINT [FK_TeamMedia_Teams] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Teams] ([TeamId])
GO
ALTER TABLE [dbo].[TeamMedia] CHECK CONSTRAINT [FK_TeamMedia_Teams]
GO
ALTER TABLE [dbo].[Teams]  WITH CHECK ADD  CONSTRAINT [FK_Teams_Clubs] FOREIGN KEY([ClubId])
REFERENCES [dbo].[Clubs] ([ClubId])
GO
ALTER TABLE [dbo].[Teams] CHECK CONSTRAINT [FK_Teams_Clubs]
GO


INSERT [dbo].[EventTypes] ([EventTypeId], [EventTypeName]) VALUES (1, N'Game')
GO
INSERT [dbo].[EventTypes] ([EventTypeId], [EventTypeName]) VALUES (2, N'Training')
GO
INSERT [dbo].[EventTypes] ([EventTypeId], [EventTypeName]) VALUES (3, N'Meeting')
GO
INSERT [dbo].[EventTypes] ([EventTypeId], [EventTypeName]) VALUES (4, N'Celebration')
GO
INSERT [dbo].[EventTypes] ([EventTypeId], [EventTypeName]) VALUES (5, N'Other')
GO
INSERT [dbo].[MediaTypes] ([MediaTypeId], [Description]) VALUES (1, N'Image')
GO
INSERT [dbo].[MediaTypes] ([MediaTypeId], [Description]) VALUES (2, N'Video')
GO
INSERT [dbo].[MediaTypes] ([MediaTypeId], [Description]) VALUES (3, N'Logo')
GO
