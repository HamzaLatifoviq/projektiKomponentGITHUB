USE [komponentDatabase]
GO
/****** Object:  Table [dbo].[Klienti]    Script Date: 20-May-25 8:16:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Klienti](
	[klient_id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Emri] [nvarchar](50) NOT NULL,
	[Mbiemri] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Klienti] PRIMARY KEY CLUSTERED 
(
	[klient_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Klienti] ON 

INSERT [dbo].[Klienti] ([klient_id], [Username], [Password], [Email], [Emri], [Mbiemri]) VALUES (1, N'username1', N'password1', N'email1@gmail.com', N'Emri1', N'Mbiemri1')
SET IDENTITY_INSERT [dbo].[Klienti] OFF
GO
