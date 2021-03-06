USE [Cinema]
GO
/****** Object:  Table [dbo].[Auth]    Script Date: 18.08.2020 14:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Auth](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](150) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Auth] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movies]    Script Date: 18.08.2020 14:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SeanceId] [int] NOT NULL,
	[SalonId] [int] NOT NULL,
	[Name] [nvarchar](150) NULL,
	[TypeId] [int] NOT NULL,
	[Director] [nvarchar](150) NULL,
	[Banner] [nvarchar](250) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Movies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MoviesType]    Script Date: 18.08.2020 14:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MoviesType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_MoviesType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Profile]    Script Date: 18.08.2020 14:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](150) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Profile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProfileDetail]    Script Date: 18.08.2020 14:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfileDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AuthId] [int] NOT NULL,
	[ProfileId] [int] NOT NULL,
 CONSTRAINT [PK_ProfileDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Salon]    Script Date: 18.08.2020 14:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Salon](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Salon] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Seance]    Script Date: 18.08.2020 14:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Seance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NULL,
	[Date] [datetime] NULL,
	[Time] [nvarchar](50) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Seans] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 18.08.2020 14:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Surname] [nvarchar](150) NOT NULL,
	[Mail] [nvarchar](50) NULL,
	[ProfileId] [int] NOT NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserToken]    Script Date: 18.08.2020 14:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserToken](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Token] [nvarchar](150) NOT NULL,
	[ValidBeginDate] [datetime] NOT NULL,
	[ValidEndDate] [datetime] NOT NULL,
	[IsValid] [bit] NOT NULL,
	[LogoutDateTime] [datetime] NULL,
	[ProfileId] [int] NOT NULL,
 CONSTRAINT [PK_UserToken] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Auth] ON 

INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (1, N'AUTH_LIST', N'Auth List', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (2, N'AUTH_ADD', N'Auth Add', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (3, N'AUTH_EDIT', N'Auth Edit', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (4, N'AUTH_DELETE', N'Auth Delete', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (5, N'MOVIES_LIST', N'Movies List', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (6, N'MOVIES_ADD', N'Movies Add', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (7, N'MOVIES_EDIT', N'Movies Edit', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (8, N'MOVIES_DELETE', N'Movies Delete', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (9, N'MOVIESTYPE_LIST', N'Movies Type List', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (10, N'MOVIESTYPE_ADD', N'Movies Type Add', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (11, N'MOVIESTYPE_EDIT', N'Movies Type Edit', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (12, N'MOVIESTYPE_DELETE', N'Movies Type Delete', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (13, N'PROFILE_LIST', N'Profile List', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (14, N'PROFILE_ADD', N'Profile Add', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (15, N'PROFILE_EDIT', N'Profile Edit', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (16, N'PROFILE_DELETE', N'Profile Delete', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (17, N'PROFILEDETAIL_BATCHEDIT', N'Profile Detail Batch Edit', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (18, N'SALON_LIST', N'Salon List', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (19, N'SALON_ADD', N'Salon Add', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (20, N'SALON_EDIT', N'Salon Edit', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (21, N'SALON_DELETE', N'Salon Delete', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (22, N'SEANCE_LIST', N'Seance List', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (23, N'SEANCE_ADD', N'Seance Add', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (24, N'SEANCE_EDIT', N'Seance Edit', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (25, N'SEANCE_DELETE', N'Seance Delete', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (26, N'USER_LIST', N'User List', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (27, N'USER_ADD', N'User Add', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (28, N'USER_EDIT', N'User Edit', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (29, N'USER_DELETE', N'User Delete', 0)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted]) VALUES (30, N'yhd--', N'asd---', 1)
SET IDENTITY_INSERT [dbo].[Auth] OFF
GO
SET IDENTITY_INSERT [dbo].[Movies] ON 

INSERT [dbo].[Movies] ([Id], [SeanceId], [SalonId], [Name], [TypeId], [Director], [Banner], [IsDeleted]) VALUES (1, 1, 2, N'movie1', 9, N'movie1director', N'/Uploads/MoviesBanner/92293dc8-238d-4ff7-9440-b4caef8da418.jpg', 0)
INSERT [dbo].[Movies] ([Id], [SeanceId], [SalonId], [Name], [TypeId], [Director], [Banner], [IsDeleted]) VALUES (2, 2, 2, N'movie2', 1, N'movie2director', N'/Uploads/MoviesBanner/92293dc8-238d-4ff7-9440-b4caef8da418.jpg', 0)
INSERT [dbo].[Movies] ([Id], [SeanceId], [SalonId], [Name], [TypeId], [Director], [Banner], [IsDeleted]) VALUES (3, 3, 3, N'movie3', 3, N'movie3director', N'/Uploads/MoviesBanner/92293dc8-238d-4ff7-9440-b4caef8da418.jpg', 1)
INSERT [dbo].[Movies] ([Id], [SeanceId], [SalonId], [Name], [TypeId], [Director], [Banner], [IsDeleted]) VALUES (4, 4, 4, N'movie4', 3, N'movie4director', N'/Uploads/MoviesBanner/92293dc8-238d-4ff7-9440-b4caef8da418.jpg', 0)
INSERT [dbo].[Movies] ([Id], [SeanceId], [SalonId], [Name], [TypeId], [Director], [Banner], [IsDeleted]) VALUES (5, 11, 2, N'ökk', 1, N'jnjı', N'/Uploads/MoviesBanner/92293dc8-238d-4ff7-9440-b4caef8da418.jpg', 0)
INSERT [dbo].[Movies] ([Id], [SeanceId], [SalonId], [Name], [TypeId], [Director], [Banner], [IsDeleted]) VALUES (6, 11, 10, N'jk', 1, N'movie1director', N'/Uploads/MoviesBanner/92293dc8-238d-4ff7-9440-b4caef8da418.jpg', 0)
INSERT [dbo].[Movies] ([Id], [SeanceId], [SalonId], [Name], [TypeId], [Director], [Banner], [IsDeleted]) VALUES (7, 12, 10, N'deneme', 4, N'denemeeee--', N'/Uploads/MoviesBanner/92293dc8-238d-4ff7-9440-b4caef8da418.jpg', 1)
INSERT [dbo].[Movies] ([Id], [SeanceId], [SalonId], [Name], [TypeId], [Director], [Banner], [IsDeleted]) VALUES (8, 11, 10, N'Movie Name', 9, N'Director Name', N'/Uploads/MoviesBanner/92293dc8-238d-4ff7-9440-b4caef8da418.jpg', 0)
SET IDENTITY_INSERT [dbo].[Movies] OFF
GO
SET IDENTITY_INSERT [dbo].[MoviesType] ON 

INSERT [dbo].[MoviesType] ([Id], [Name], [IsDeleted]) VALUES (1, N'Aksiyon', 0)
INSERT [dbo].[MoviesType] ([Id], [Name], [IsDeleted]) VALUES (2, N'Macera', 0)
INSERT [dbo].[MoviesType] ([Id], [Name], [IsDeleted]) VALUES (3, N'Animasyon', 0)
INSERT [dbo].[MoviesType] ([Id], [Name], [IsDeleted]) VALUES (4, N'Çocuk', 0)
INSERT [dbo].[MoviesType] ([Id], [Name], [IsDeleted]) VALUES (5, N'Komedi', 0)
INSERT [dbo].[MoviesType] ([Id], [Name], [IsDeleted]) VALUES (6, N'Suç', 0)
INSERT [dbo].[MoviesType] ([Id], [Name], [IsDeleted]) VALUES (7, N'Drama', 0)
INSERT [dbo].[MoviesType] ([Id], [Name], [IsDeleted]) VALUES (8, N'Epik', 0)
INSERT [dbo].[MoviesType] ([Id], [Name], [IsDeleted]) VALUES (9, N'Aile', 0)
INSERT [dbo].[MoviesType] ([Id], [Name], [IsDeleted]) VALUES (10, N'Korku', 0)
INSERT [dbo].[MoviesType] ([Id], [Name], [IsDeleted]) VALUES (11, N'Gizem/Gerilim', 0)
INSERT [dbo].[MoviesType] ([Id], [Name], [IsDeleted]) VALUES (14, N'dwq---', 1)
SET IDENTITY_INSERT [dbo].[MoviesType] OFF
GO
SET IDENTITY_INSERT [dbo].[Profile] ON 

INSERT [dbo].[Profile] ([Id], [Code], [Name], [IsDeleted]) VALUES (1, N'ADMIN', N'Admin', 0)
INSERT [dbo].[Profile] ([Id], [Code], [Name], [IsDeleted]) VALUES (2, N'USER', N'User', 0)
INSERT [dbo].[Profile] ([Id], [Code], [Name], [IsDeleted]) VALUES (3, N'dcs--', N'dfg--', 1)
SET IDENTITY_INSERT [dbo].[Profile] OFF
GO
SET IDENTITY_INSERT [dbo].[ProfileDetail] ON 

INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (12, 17, 1)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (15, 1, 1)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (16, 2, 1)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (17, 3, 1)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (18, 4, 1)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (19, 9, 1)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (20, 10, 1)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (21, 11, 1)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (22, 12, 1)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (23, 13, 1)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (24, 14, 1)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (25, 15, 1)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (26, 16, 1)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (27, 26, 1)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (28, 27, 1)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (29, 28, 1)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (30, 29, 1)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (31, 5, 2)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (32, 6, 2)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (33, 7, 2)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (34, 8, 2)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (35, 18, 2)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (36, 19, 2)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (37, 20, 2)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (38, 21, 2)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (39, 22, 2)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (40, 23, 2)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (41, 24, 2)
INSERT [dbo].[ProfileDetail] ([Id], [AuthId], [ProfileId]) VALUES (42, 25, 2)
SET IDENTITY_INSERT [dbo].[ProfileDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[Salon] ON 

INSERT [dbo].[Salon] ([Id], [Name], [IsDeleted]) VALUES (1, N'salon1', 0)
INSERT [dbo].[Salon] ([Id], [Name], [IsDeleted]) VALUES (2, N'salon2', 0)
INSERT [dbo].[Salon] ([Id], [Name], [IsDeleted]) VALUES (3, N'salon3', 0)
INSERT [dbo].[Salon] ([Id], [Name], [IsDeleted]) VALUES (4, N'salon4', 0)
INSERT [dbo].[Salon] ([Id], [Name], [IsDeleted]) VALUES (5, N'salon5', 0)
INSERT [dbo].[Salon] ([Id], [Name], [IsDeleted]) VALUES (6, N'salon6', 0)
INSERT [dbo].[Salon] ([Id], [Name], [IsDeleted]) VALUES (7, N'salon7', 0)
INSERT [dbo].[Salon] ([Id], [Name], [IsDeleted]) VALUES (8, N'salon8', 0)
INSERT [dbo].[Salon] ([Id], [Name], [IsDeleted]) VALUES (9, N'salon9', 0)
INSERT [dbo].[Salon] ([Id], [Name], [IsDeleted]) VALUES (10, N'salon10', 0)
INSERT [dbo].[Salon] ([Id], [Name], [IsDeleted]) VALUES (11, N'salon11', 0)
INSERT [dbo].[Salon] ([Id], [Name], [IsDeleted]) VALUES (12, N'jhgvfc', 1)
INSERT [dbo].[Salon] ([Id], [Name], [IsDeleted]) VALUES (13, N'sa---', 1)
SET IDENTITY_INSERT [dbo].[Salon] OFF
GO
SET IDENTITY_INSERT [dbo].[Seance] ON 

INSERT [dbo].[Seance] ([Id], [Name], [Date], [Time], [IsDeleted]) VALUES (1, N'seance1', CAST(N'2020-01-01T00:00:00.000' AS DateTime), N'01:01', 0)
INSERT [dbo].[Seance] ([Id], [Name], [Date], [Time], [IsDeleted]) VALUES (2, N'seance2', CAST(N'2020-02-02T00:00:00.000' AS DateTime), N'02:02', 0)
INSERT [dbo].[Seance] ([Id], [Name], [Date], [Time], [IsDeleted]) VALUES (3, N'seance3', CAST(N'2020-03-03T00:00:00.000' AS DateTime), N'03:03', 0)
INSERT [dbo].[Seance] ([Id], [Name], [Date], [Time], [IsDeleted]) VALUES (4, N'seance4', CAST(N'2020-04-04T00:00:00.000' AS DateTime), N'04:04', 0)
INSERT [dbo].[Seance] ([Id], [Name], [Date], [Time], [IsDeleted]) VALUES (5, N'seance5', CAST(N'2020-05-05T00:00:00.000' AS DateTime), N'05:05', 0)
INSERT [dbo].[Seance] ([Id], [Name], [Date], [Time], [IsDeleted]) VALUES (6, N'seance6', CAST(N'2020-06-06T00:00:00.000' AS DateTime), N'06:06', 0)
INSERT [dbo].[Seance] ([Id], [Name], [Date], [Time], [IsDeleted]) VALUES (7, N'seance7', CAST(N'2020-07-07T00:00:00.000' AS DateTime), N'07:07', 0)
INSERT [dbo].[Seance] ([Id], [Name], [Date], [Time], [IsDeleted]) VALUES (8, N'seance8', CAST(N'2020-08-08T00:00:00.000' AS DateTime), N'08:08', 0)
INSERT [dbo].[Seance] ([Id], [Name], [Date], [Time], [IsDeleted]) VALUES (9, N'seance9', CAST(N'2020-09-09T00:00:00.000' AS DateTime), N'09:09', 0)
INSERT [dbo].[Seance] ([Id], [Name], [Date], [Time], [IsDeleted]) VALUES (10, N'seance 10 ', CAST(N'2020-10-10T00:00:00.000' AS DateTime), N'10:10', 0)
INSERT [dbo].[Seance] ([Id], [Name], [Date], [Time], [IsDeleted]) VALUES (11, N'seance 11', CAST(N'2020-11-11T00:00:00.000' AS DateTime), N'11:11', 0)
INSERT [dbo].[Seance] ([Id], [Name], [Date], [Time], [IsDeleted]) VALUES (12, N'seance 12', CAST(N'2020-12-12T00:00:00.000' AS DateTime), N'12:12', 0)
INSERT [dbo].[Seance] ([Id], [Name], [Date], [Time], [IsDeleted]) VALUES (14, N'dsadasd12', CAST(N'2019-03-12T00:00:00.000' AS DateTime), N'12:12', 1)
INSERT [dbo].[Seance] ([Id], [Name], [Date], [Time], [IsDeleted]) VALUES (15, N'se----', CAST(N'2012-11-11T00:00:00.000' AS DateTime), N'11:11', 1)
SET IDENTITY_INSERT [dbo].[Seance] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [UserName], [Password], [Name], [Surname], [Mail], [ProfileId], [IsDeleted]) VALUES (1, N'admin', N'123456', N'Talha', N'Erdoğan (Admin)', N'talha_erd_91@hotmail.com', 1, 0)
INSERT [dbo].[User] ([Id], [UserName], [Password], [Name], [Surname], [Mail], [ProfileId], [IsDeleted]) VALUES (2, N'user', N'123456', N'Talha', N'Erdoğan (User)', N'talha_erd_91@hotmail.com', 2, 0)
INSERT [dbo].[User] ([Id], [UserName], [Password], [Name], [Surname], [Mail], [ProfileId], [IsDeleted]) VALUES (3, N'denemede---', N'1', N'denemede----', N'denemede (user)', N'denemede', 2, 1)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserToken] ON 

INSERT [dbo].[UserToken] ([Id], [Username], [Token], [ValidBeginDate], [ValidEndDate], [IsValid], [LogoutDateTime], [ProfileId]) VALUES (1, N'admin', N'0d7bb29e-af81-4a3d-a8b0-f76fbdfca614', CAST(N'2020-02-24T11:29:53.587' AS DateTime), CAST(N'2020-02-23T17:29:53.587' AS DateTime), 1, NULL, 1)
INSERT [dbo].[UserToken] ([Id], [Username], [Token], [ValidBeginDate], [ValidEndDate], [IsValid], [LogoutDateTime], [ProfileId]) VALUES (2, N'admin', N'd6c9b89e-7f9a-44e3-89f2-6f3cd330a864', CAST(N'2020-02-24T16:35:40.157' AS DateTime), CAST(N'2020-02-24T22:35:40.157' AS DateTime), 0, CAST(N'2020-02-24T16:36:23.227' AS DateTime), 1)
INSERT [dbo].[UserToken] ([Id], [Username], [Token], [ValidBeginDate], [ValidEndDate], [IsValid], [LogoutDateTime], [ProfileId]) VALUES (3, N'user', N'f20f3da4-27b1-4574-a36c-87af8c48ca47', CAST(N'2020-02-24T16:36:26.050' AS DateTime), CAST(N'2020-02-24T22:36:26.050' AS DateTime), 1, NULL, 2)
INSERT [dbo].[UserToken] ([Id], [Username], [Token], [ValidBeginDate], [ValidEndDate], [IsValid], [LogoutDateTime], [ProfileId]) VALUES (4, N'user', N'0dd62356-c83b-4f22-b3c6-646d31059320', CAST(N'2020-02-24T16:49:37.670' AS DateTime), CAST(N'2020-02-24T22:49:37.670' AS DateTime), 1, NULL, 2)
INSERT [dbo].[UserToken] ([Id], [Username], [Token], [ValidBeginDate], [ValidEndDate], [IsValid], [LogoutDateTime], [ProfileId]) VALUES (5, N'user', N'aa6b23c7-7fb7-4c9c-8fd1-a3abf77d3dfa', CAST(N'2020-02-24T16:50:19.117' AS DateTime), CAST(N'2020-02-24T22:50:19.117' AS DateTime), 1, NULL, 2)
INSERT [dbo].[UserToken] ([Id], [Username], [Token], [ValidBeginDate], [ValidEndDate], [IsValid], [LogoutDateTime], [ProfileId]) VALUES (6, N'user', N'e25fb1b4-b16f-4c71-adf4-15eb7161781f', CAST(N'2020-02-24T16:51:31.910' AS DateTime), CAST(N'2020-02-24T22:51:31.910' AS DateTime), 1, NULL, 2)
INSERT [dbo].[UserToken] ([Id], [Username], [Token], [ValidBeginDate], [ValidEndDate], [IsValid], [LogoutDateTime], [ProfileId]) VALUES (7, N'user', N'0d94bd08-7666-4335-9b38-a5e4ceb251d4', CAST(N'2020-02-24T19:37:33.237' AS DateTime), CAST(N'2020-02-25T01:37:33.237' AS DateTime), 1, NULL, 2)
INSERT [dbo].[UserToken] ([Id], [Username], [Token], [ValidBeginDate], [ValidEndDate], [IsValid], [LogoutDateTime], [ProfileId]) VALUES (8, N'user', N'3872d74a-612c-4b6b-9512-3d2031bab6ea', CAST(N'2020-02-24T19:43:54.800' AS DateTime), CAST(N'2020-02-25T01:43:54.800' AS DateTime), 1, NULL, 2)
INSERT [dbo].[UserToken] ([Id], [Username], [Token], [ValidBeginDate], [ValidEndDate], [IsValid], [LogoutDateTime], [ProfileId]) VALUES (9, N'user', N'2304e605-728d-4485-9b82-7fb49892fa1a', CAST(N'2020-02-24T19:45:13.817' AS DateTime), CAST(N'2020-02-25T01:45:13.817' AS DateTime), 1, NULL, 2)
INSERT [dbo].[UserToken] ([Id], [Username], [Token], [ValidBeginDate], [ValidEndDate], [IsValid], [LogoutDateTime], [ProfileId]) VALUES (10, N'user', N'0dcc8489-1f59-4499-90c0-a186fbac862c', CAST(N'2020-02-24T21:50:51.307' AS DateTime), CAST(N'2020-02-25T03:50:51.307' AS DateTime), 1, NULL, 2)
INSERT [dbo].[UserToken] ([Id], [Username], [Token], [ValidBeginDate], [ValidEndDate], [IsValid], [LogoutDateTime], [ProfileId]) VALUES (11, N'user', N'b22ab48f-e3e8-41a5-a270-1c52a45e24f2', CAST(N'2020-02-24T22:02:50.233' AS DateTime), CAST(N'2020-02-25T04:02:50.233' AS DateTime), 1, NULL, 2)
INSERT [dbo].[UserToken] ([Id], [Username], [Token], [ValidBeginDate], [ValidEndDate], [IsValid], [LogoutDateTime], [ProfileId]) VALUES (12, N'user', N'37b07644-6737-4820-b253-280e7880a5ec', CAST(N'2020-02-24T22:05:27.433' AS DateTime), CAST(N'2020-02-25T04:05:27.433' AS DateTime), 1, NULL, 2)
INSERT [dbo].[UserToken] ([Id], [Username], [Token], [ValidBeginDate], [ValidEndDate], [IsValid], [LogoutDateTime], [ProfileId]) VALUES (13, N'user', N'6f70880b-3295-47b8-b3b1-626dd49f730f', CAST(N'2020-02-24T22:07:56.137' AS DateTime), CAST(N'2020-02-25T04:07:56.137' AS DateTime), 1, NULL, 2)
INSERT [dbo].[UserToken] ([Id], [Username], [Token], [ValidBeginDate], [ValidEndDate], [IsValid], [LogoutDateTime], [ProfileId]) VALUES (14, N'user', N'480fa163-ee48-450e-ae18-9972f5a8aed1', CAST(N'2020-02-24T22:18:13.587' AS DateTime), CAST(N'2020-02-25T04:18:13.587' AS DateTime), 1, NULL, 2)
INSERT [dbo].[UserToken] ([Id], [Username], [Token], [ValidBeginDate], [ValidEndDate], [IsValid], [LogoutDateTime], [ProfileId]) VALUES (15, N'user', N'b8cba8ac-e060-49bd-b5ac-9ac1fad34cf7', CAST(N'2020-02-24T22:22:40.583' AS DateTime), CAST(N'2020-02-25T04:22:40.583' AS DateTime), 1, NULL, 2)
INSERT [dbo].[UserToken] ([Id], [Username], [Token], [ValidBeginDate], [ValidEndDate], [IsValid], [LogoutDateTime], [ProfileId]) VALUES (16, N'user', N'5860f5ac-5bca-41c5-ae45-27cdd05ba79a', CAST(N'2020-02-24T22:27:41.763' AS DateTime), CAST(N'2020-02-25T04:27:41.763' AS DateTime), 1, NULL, 2)
INSERT [dbo].[UserToken] ([Id], [Username], [Token], [ValidBeginDate], [ValidEndDate], [IsValid], [LogoutDateTime], [ProfileId]) VALUES (17, N'user', N'b2be2e32-ad30-4117-a44b-baeb5f83f082', CAST(N'2020-03-20T13:51:29.203' AS DateTime), CAST(N'2020-03-20T19:51:29.203' AS DateTime), 1, NULL, 2)
INSERT [dbo].[UserToken] ([Id], [Username], [Token], [ValidBeginDate], [ValidEndDate], [IsValid], [LogoutDateTime], [ProfileId]) VALUES (10017, N'user', N'bcb0e074-c8bb-4a8f-9b48-f832c7514d8e', CAST(N'2020-08-18T14:03:40.423' AS DateTime), CAST(N'2020-08-18T20:03:40.423' AS DateTime), 0, CAST(N'2020-08-18T14:04:00.080' AS DateTime), 2)
INSERT [dbo].[UserToken] ([Id], [Username], [Token], [ValidBeginDate], [ValidEndDate], [IsValid], [LogoutDateTime], [ProfileId]) VALUES (10018, N'admin', N'60e1b21c-aa68-481b-af54-c7d55f8b2e1e', CAST(N'2020-08-18T14:04:06.537' AS DateTime), CAST(N'2020-08-18T20:04:06.537' AS DateTime), 0, CAST(N'2020-08-18T14:04:27.127' AS DateTime), 1)
INSERT [dbo].[UserToken] ([Id], [Username], [Token], [ValidBeginDate], [ValidEndDate], [IsValid], [LogoutDateTime], [ProfileId]) VALUES (10019, N'admin', N'6183af25-935c-4928-8b8a-861c7ebff845', CAST(N'2020-08-18T14:06:06.380' AS DateTime), CAST(N'2020-08-18T20:06:06.380' AS DateTime), 0, CAST(N'2020-08-18T14:06:32.493' AS DateTime), 1)
INSERT [dbo].[UserToken] ([Id], [Username], [Token], [ValidBeginDate], [ValidEndDate], [IsValid], [LogoutDateTime], [ProfileId]) VALUES (10020, N'user', N'837edb70-2995-4883-9d61-2ce81fac1fa0', CAST(N'2020-08-18T14:06:35.553' AS DateTime), CAST(N'2020-08-18T20:06:35.553' AS DateTime), 1, NULL, 2)
SET IDENTITY_INSERT [dbo].[UserToken] OFF
GO
