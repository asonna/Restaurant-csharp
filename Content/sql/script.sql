USE [best_restaurants]
GO
/****** Object:  Table [dbo].[cuisines]    Script Date: 12/7/2016 4:57:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cuisines](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[restaurants]    Script Date: 12/7/2016 4:57:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[restaurants](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[description] [varchar](255) NULL,
	[cuisine_id] [int] NULL
) ON [PRIMARY]

GO
