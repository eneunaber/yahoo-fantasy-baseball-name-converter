USE [fantasy.baseball]
GO

/****** Object:  Table [dbo].[YahooDataScrubbed]    Script Date: 03/31/2015 22:12:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[YahooDataScrubbed](
	[Name] [nvarchar](50) NULL,
	[PlayerId] [nvarchar](50) NULL,
	[ScrubbedName] [nvarchar](50) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


