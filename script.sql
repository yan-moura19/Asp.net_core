USE [BackEnd.api]
GO
/****** Object:  Table [dbo].[Produtos]    Script Date: 27/01/2022 13:46:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produtos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [nvarchar](40) NOT NULL,
	[preco] [decimal](8, 2) NOT NULL
) ON [PRIMARY]
GO
