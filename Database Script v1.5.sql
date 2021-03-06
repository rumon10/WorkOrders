USE [WorkOrders]
GO
/****** Object:  Table [dbo].[NumberSquenceTable]    Script Date: 12/30/2015 13:25:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NumberSquenceTable]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[NumberSquenceTable](
	[TypeId] [varchar](20) NOT NULL,
	[NextNumber] [numeric](18, 0) NOT NULL,
	[LastEditdate] [datetime] NULL,
	[LastEdit] [varchar](20) NULL,
 CONSTRAINT [PK_NumberSquenceTable] PRIMARY KEY CLUSTERED 
(
	[TypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[NumberSquenceTable] ([TypeId], [NextNumber], [LastEditdate], [LastEdit]) VALUES (N'WK', CAST(1543 AS Numeric(18, 0)), CAST(0x0000A57E00D8529C AS DateTime), N'')
/****** Object:  Table [dbo].[WorkOrder]    Script Date: 12/30/2015 13:25:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkOrder]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkOrder](
	[WorkOrderId] [bigint] NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[TechId] [int] NOT NULL,
	[TotalUsedTime] [decimal](18, 2) NOT NULL,
	[TotalCharge] [money] NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastEditDate] [datetime] NOT NULL,
 CONSTRAINT [PK_WorkOrders] PRIMARY KEY CLUSTERED 
(
	[WorkOrderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[WorkOrder] ([WorkOrderId], [OrderDate], [CustomerId], [TechId], [TotalUsedTime], [TotalCharge], [CreateDateTime], [LastEditDate]) VALUES (1222, CAST(0x0000A56900000000 AS DateTime), 1, 2, CAST(13.50 AS Decimal(18, 2)), 0.0000, CAST(0x0000A56600C29831 AS DateTime), CAST(0x0000A56700DE698A AS DateTime))
INSERT [dbo].[WorkOrder] ([WorkOrderId], [OrderDate], [CustomerId], [TechId], [TotalUsedTime], [TotalCharge], [CreateDateTime], [LastEditDate]) VALUES (1224, CAST(0x0000A57000000000 AS DateTime), 1, 2, CAST(12.50 AS Decimal(18, 2)), 0.0000, CAST(0x0000A56600C34359 AS DateTime), CAST(0x0000A5670097428D AS DateTime))
INSERT [dbo].[WorkOrder] ([WorkOrderId], [OrderDate], [CustomerId], [TechId], [TotalUsedTime], [TotalCharge], [CreateDateTime], [LastEditDate]) VALUES (1226, CAST(0x0000A56100000000 AS DateTime), 1, 2, CAST(0.00 AS Decimal(18, 2)), 0.0000, CAST(0x0000A56600C453DD AS DateTime), CAST(0x0000A56600C453DD AS DateTime))
INSERT [dbo].[WorkOrder] ([WorkOrderId], [OrderDate], [CustomerId], [TechId], [TotalUsedTime], [TotalCharge], [CreateDateTime], [LastEditDate]) VALUES (1227, CAST(0x0000A56800000000 AS DateTime), 1, 2, CAST(0.00 AS Decimal(18, 2)), 0.0000, CAST(0x0000A56600C5428B AS DateTime), CAST(0x0000A56600C5428B AS DateTime))
INSERT [dbo].[WorkOrder] ([WorkOrderId], [OrderDate], [CustomerId], [TechId], [TotalUsedTime], [TotalCharge], [CreateDateTime], [LastEditDate]) VALUES (1228, CAST(0x0000A56F00000000 AS DateTime), 1, 2, CAST(12.00 AS Decimal(18, 2)), 0.0000, CAST(0x0000A56600C62CD4 AS DateTime), CAST(0x0000A56600C62CD4 AS DateTime))
INSERT [dbo].[WorkOrder] ([WorkOrderId], [OrderDate], [CustomerId], [TechId], [TotalUsedTime], [TotalCharge], [CreateDateTime], [LastEditDate]) VALUES (1229, CAST(0x0000A56100000000 AS DateTime), 1, 2, CAST(13.00 AS Decimal(18, 2)), 0.0000, CAST(0x0000A56600C66E8F AS DateTime), CAST(0x0000A56600C66E8F AS DateTime))
INSERT [dbo].[WorkOrder] ([WorkOrderId], [OrderDate], [CustomerId], [TechId], [TotalUsedTime], [TotalCharge], [CreateDateTime], [LastEditDate]) VALUES (1253, CAST(0x0000A56700000000 AS DateTime), 1, 2, CAST(12.25 AS Decimal(18, 2)), 0.0000, CAST(0x0000A56600FA38B2 AS DateTime), CAST(0x0000A56600FA38B2 AS DateTime))
INSERT [dbo].[WorkOrder] ([WorkOrderId], [OrderDate], [CustomerId], [TechId], [TotalUsedTime], [TotalCharge], [CreateDateTime], [LastEditDate]) VALUES (1274, CAST(0x0000A56700000000 AS DateTime), 1, 2, CAST(19.25 AS Decimal(18, 2)), 0.0000, CAST(0x0000A566012BF863 AS DateTime), CAST(0x0000A566012BF863 AS DateTime))
INSERT [dbo].[WorkOrder] ([WorkOrderId], [OrderDate], [CustomerId], [TechId], [TotalUsedTime], [TotalCharge], [CreateDateTime], [LastEditDate]) VALUES (1275, CAST(0x0000A56100000000 AS DateTime), 1, 2, CAST(1.25 AS Decimal(18, 2)), 0.0000, CAST(0x0000A566012D28A5 AS DateTime), CAST(0x0000A566012D28A5 AS DateTime))
INSERT [dbo].[WorkOrder] ([WorkOrderId], [OrderDate], [CustomerId], [TechId], [TotalUsedTime], [TotalCharge], [CreateDateTime], [LastEditDate]) VALUES (1276, CAST(0x0000A56800000000 AS DateTime), 1, 2, CAST(20.25 AS Decimal(18, 2)), 0.0000, CAST(0x0000A566012E6F61 AS DateTime), CAST(0x0000A56700918F2A AS DateTime))
INSERT [dbo].[WorkOrder] ([WorkOrderId], [OrderDate], [CustomerId], [TechId], [TotalUsedTime], [TotalCharge], [CreateDateTime], [LastEditDate]) VALUES (1299, CAST(0x0000A56900000000 AS DateTime), 1, 2, CAST(12.50 AS Decimal(18, 2)), 0.0000, CAST(0x0000A56700D17B11 AS DateTime), CAST(0x0000A56700D17B11 AS DateTime))
INSERT [dbo].[WorkOrder] ([WorkOrderId], [OrderDate], [CustomerId], [TechId], [TotalUsedTime], [TotalCharge], [CreateDateTime], [LastEditDate]) VALUES (1347, CAST(0x0000A56800000000 AS DateTime), 1, 2, CAST(3.50 AS Decimal(18, 2)), 0.0000, CAST(0x0000A56700E6BB51 AS DateTime), CAST(0x0000A56700E6BB51 AS DateTime))
INSERT [dbo].[WorkOrder] ([WorkOrderId], [OrderDate], [CustomerId], [TechId], [TotalUsedTime], [TotalCharge], [CreateDateTime], [LastEditDate]) VALUES (1348, CAST(0x0000A56A00000000 AS DateTime), 2, 2, CAST(14.75 AS Decimal(18, 2)), 0.0000, CAST(0x0000A56700E73A68 AS DateTime), CAST(0x0000A56700E73A68 AS DateTime))
INSERT [dbo].[WorkOrder] ([WorkOrderId], [OrderDate], [CustomerId], [TechId], [TotalUsedTime], [TotalCharge], [CreateDateTime], [LastEditDate]) VALUES (1385, CAST(0x0000A56200000000 AS DateTime), 1, 2, CAST(20.00 AS Decimal(18, 2)), 0.0000, CAST(0x0000A5690118CA82 AS DateTime), CAST(0x0000A5690118CA82 AS DateTime))
/****** Object:  Table [dbo].[WorkOrderLine]    Script Date: 12/30/2015 13:25:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkOrderLine]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkOrderLine](
	[TransId] [bigint] IDENTITY(1,1) NOT NULL,
	[WorkOrderId] [bigint] NOT NULL,
	[WorkType] [varchar](50) NOT NULL,
	[Description] [varchar](500) NOT NULL,
	[UsedTime] [decimal](18, 2) NOT NULL,
	[LastEditDate] [datetime] NOT NULL,
 CONSTRAINT [PK_WorkOrderLine] PRIMARY KEY CLUSTERED 
(
	[TransId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[WorkOrderLine] ON
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (5, 0, N'Desktop', N'Desktop Service', CAST(12.25 AS Decimal(18, 2)), CAST(0x0000A56600C29E43 AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (6, 0, N'Desktop', N'Desktop', CAST(10.00 AS Decimal(18, 2)), CAST(0x0000A56600C34736 AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (8, 0, N'Desktop', N'Desktop', CAST(1.00 AS Decimal(18, 2)), CAST(0x0000A56600C45B10 AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (9, 0, N'Desktop', N'Desktop', CAST(1.00 AS Decimal(18, 2)), CAST(0x0000A56600C548BA AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (10, 0, N'Desktop', N'Desktop Service', CAST(12.00 AS Decimal(18, 2)), CAST(0x0000A56600C62CD9 AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (11, 0, N'Desktop', N'Desktop', CAST(1.00 AS Decimal(18, 2)), CAST(0x0000A56600C66E8F AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (12, 0, N'Server', N'Bsss', CAST(12.00 AS Decimal(18, 2)), CAST(0x0000A56600C66E8F AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (13, 0, N'Desktop', N'Desktop', CAST(12.25 AS Decimal(18, 2)), CAST(0x0000A56600FA38C5 AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (14, 0, N'Desktop', N'Desktop Service', CAST(12.00 AS Decimal(18, 2)), CAST(0x0000A566012BF87A AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (15, 0, N'Network', N'Network Service', CAST(5.00 AS Decimal(18, 2)), CAST(0x0000A566012BF884 AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (16, 0, N'Travel', N'Tranvel Service', CAST(2.25 AS Decimal(18, 2)), CAST(0x0000A566012BF884 AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (17, 0, N'Desktop', N'Fes', CAST(1.25 AS Decimal(18, 2)), CAST(0x0000A566012D28A5 AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (35, 1276, N'Desktop', N'Desktop Service', CAST(1.25 AS Decimal(18, 2)), CAST(0x0000A56700918F2F AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (36, 1276, N'Travel', N'Travel', CAST(5.25 AS Decimal(18, 2)), CAST(0x0000A56700918F2F AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (37, 1276, N'Network', N'Network Service LKtf', CAST(12.50 AS Decimal(18, 2)), CAST(0x0000A56700918F33 AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (38, 1276, N'Server', N'Service', CAST(1.25 AS Decimal(18, 2)), CAST(0x0000A56700918F33 AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (39, 1224, N'Desktop', N'Desktop Service', CAST(12.50 AS Decimal(18, 2)), CAST(0x0000A56700974291 AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (41, 1299, N'Desktop', N'Desktop', CAST(12.50 AS Decimal(18, 2)), CAST(0x0000A56700D17B28 AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (42, 1222, N'Network', N'Network Service Information', CAST(13.50 AS Decimal(18, 2)), CAST(0x0000A56700DE698F AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (43, 1347, N'Desktop', N'Desktop', CAST(1.00 AS Decimal(18, 2)), CAST(0x0000A56700E6BBD0 AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (44, 1347, N'Network', N'Network', CAST(2.50 AS Decimal(18, 2)), CAST(0x0000A56700E6BBE7 AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (45, 1348, N'Desktop', N'Desktop Service', CAST(1.25 AS Decimal(18, 2)), CAST(0x0000A56700E73A7B AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (46, 1348, N'Network', N'Network - LAN', CAST(5.50 AS Decimal(18, 2)), CAST(0x0000A56700E73A80 AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (47, 1348, N'Server', N'Server admin', CAST(1.25 AS Decimal(18, 2)), CAST(0x0000A56700E73A80 AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (48, 1348, N'Software', N'Software Installtion', CAST(4.75 AS Decimal(18, 2)), CAST(0x0000A56700E73A80 AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (49, 1348, N'Travel', N'Travel for Company Service & reach', CAST(2.00 AS Decimal(18, 2)), CAST(0x0000A56700E73A85 AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (50, 1385, N'Desktop', N'Desktop Service', CAST(10.00 AS Decimal(18, 2)), CAST(0x0000A5690118CB1B AS DateTime))
INSERT [dbo].[WorkOrderLine] ([TransId], [WorkOrderId], [WorkType], [Description], [UsedTime], [LastEditDate]) VALUES (51, 1385, N'Network', N'Network', CAST(10.00 AS Decimal(18, 2)), CAST(0x0000A5690118CB25 AS DateTime))
SET IDENTITY_INSERT [dbo].[WorkOrderLine] OFF
/****** Object:  Table [dbo].[CustTable]    Script Date: 12/30/2015 13:25:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustTable]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CustTable](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerName] [varchar](100) NOT NULL,
	[ContactNo] [varchar](20) NOT NULL,
	[EmailAddress] [varchar](50) NOT NULL,
	[Address] [varchar](200) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](10) NULL,
	[ZipCode] [varchar](10) NULL,
	[Status] [bit] NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[CustTable] ON
INSERT [dbo].[CustTable] ([CustomerId], [CustomerName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (1, N'Rumon Prosad', N'01722830244', N'rumon10@gmail.com', N'New State Road', N'Dhaka', N'Dhaka', N'', 0)
INSERT [dbo].[CustTable] ([CustomerId], [CustomerName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (2, N'Sonali Saha', N'01722830244', N'rumon10@gmail.com', N'', N'', N'', N'', 1)
INSERT [dbo].[CustTable] ([CustomerId], [CustomerName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (3, N'Sonali Saha', N'01722830244', N'rumon10@gmail.com', N'', N'', N'', N'', 1)
INSERT [dbo].[CustTable] ([CustomerId], [CustomerName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (4, N'Sonali Saha', N'01722830244', N'rumon10@gmail.com', N'', N'', N'', N'', 1)
INSERT [dbo].[CustTable] ([CustomerId], [CustomerName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (5, N'Sonali Saha', N'01722830244', N'rumon10@gmail.com', N'', N'', N'', N'', 1)
INSERT [dbo].[CustTable] ([CustomerId], [CustomerName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (6, N'Sonali Saha', N'01722830244', N'rumon10@gmail.com', N'', N'', N'', N'', 1)
INSERT [dbo].[CustTable] ([CustomerId], [CustomerName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (7, N'Sonali Saha', N'01722830244', N'rumon10@gmail.com', N'', N'', N'', N'', 1)
INSERT [dbo].[CustTable] ([CustomerId], [CustomerName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (8, N'Sonali Saha', N'01722830244', N'rumon10@gmail.com', N'', N'', N'', N'', 1)
INSERT [dbo].[CustTable] ([CustomerId], [CustomerName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (9, N'Sonali Saha', N'01722830244', N'rumon10@gmail.com', N'', N'', N'', N'', 1)
INSERT [dbo].[CustTable] ([CustomerId], [CustomerName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (10, N'Sonali Saha', N'01722830244', N'rumon10@gmail.com', N'', N'', N'', N'', 1)
INSERT [dbo].[CustTable] ([CustomerId], [CustomerName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (11, N'Sonali Saha', N'01722830244', N'rumon10@gmail.com', N'', N'', N'', N'', 1)
INSERT [dbo].[CustTable] ([CustomerId], [CustomerName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (12, N'Sonali Saha', N'01722830244', N'rumon10@gmail.com', N'', N'', N'', N'', 1)
INSERT [dbo].[CustTable] ([CustomerId], [CustomerName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (13, N'Sonali Saha', N'01722830244', N'rumon10@gmail.com', N'', N'', N'', N'', 1)
INSERT [dbo].[CustTable] ([CustomerId], [CustomerName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (14, N'Sonali Saha', N'01722830244', N'rumon10@gmail.com', N'', N'', N'', N'', 1)
INSERT [dbo].[CustTable] ([CustomerId], [CustomerName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (15, N'Sonali Saha', N'01722830244', N'rumon10@gmail.com', N'', N'', N'', N'', 1)
INSERT [dbo].[CustTable] ([CustomerId], [CustomerName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (16, N'Sonali Saha', N'01722830244', N'rumon10@gmail.com', N'', N'', N'', N'', 1)
INSERT [dbo].[CustTable] ([CustomerId], [CustomerName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (17, N'Sonali Saha', N'01722830244', N'rumon10@gmail.com', N'', N'', N'', N'', 1)
INSERT [dbo].[CustTable] ([CustomerId], [CustomerName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (18, N'Sonali Saha', N'01722830244', N'rumon10@gmail.com', N'', N'', N'', N'', 1)
INSERT [dbo].[CustTable] ([CustomerId], [CustomerName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (19, N'Sonali Saha', N'01722830244', N'rumon10@gmail.com', N'', N'', N'', N'', 1)
INSERT [dbo].[CustTable] ([CustomerId], [CustomerName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (20, N'Sonali Saha', N'01722830244', N'rumon10@gmail.com', N'', N'', N'', N'', 1)
INSERT [dbo].[CustTable] ([CustomerId], [CustomerName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (21, N'Sonali Saha', N'01722830244', N'rumon10@gmail.com', N'', N'', N'', N'', 1)
INSERT [dbo].[CustTable] ([CustomerId], [CustomerName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (22, N'Sonali Saha', N'01722830244', N'rumon10@gmail.com', N'', N'', N'', N'', 1)
INSERT [dbo].[CustTable] ([CustomerId], [CustomerName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (23, N'Sonali Saha', N'01722830244', N'rumon10@gmail.com', N'', N'', N'', N'', 1)
INSERT [dbo].[CustTable] ([CustomerId], [CustomerName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (24, N'Sonali Saha Test', N'01722830244', N'rumon10@gmail.com', N'', N'', N'', N'', 1)
SET IDENTITY_INSERT [dbo].[CustTable] OFF
/****** Object:  Table [dbo].[TechTable]    Script Date: 12/30/2015 13:25:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TechTable]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TechTable](
	[TechId] [int] IDENTITY(1,1) NOT NULL,
	[TechName] [varchar](100) NOT NULL,
	[ContactNo] [varchar](20) NULL,
	[EmailAddress] [varchar](50) NULL,
	[Address] [varchar](200) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](10) NULL,
	[ZipCode] [varchar](10) NULL,
	[Status] [bit] NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[TechTable] ON
INSERT [dbo].[TechTable] ([TechId], [TechName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (1, N'Tester', N'01722830244', NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[TechTable] ([TechId], [TechName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (2, N'ABC Tesd', NULL, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[TechTable] ([TechId], [TechName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (3, N'CSSS', NULL, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[TechTable] ([TechId], [TechName], [ContactNo], [EmailAddress], [Address], [City], [State], [ZipCode], [Status]) VALUES (4, N'DDS', NULL, NULL, NULL, NULL, NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[TechTable] OFF
/****** Object:  Table [dbo].[Users]    Script Date: 12/30/2015 13:25:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](20) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[FullName] [varchar](100) NOT NULL,
	[Email] [varchar](100) NULL,
	[IsEnabled] [bit] NOT NULL,
	[LastEdit] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON
INSERT [dbo].[Users] ([UserId], [UserName], [Password], [FullName], [Email], [IsEnabled], [LastEdit]) VALUES (1, N'admin', N'admin', N'Administrator', N'admin@yahoo.com', 0, CAST(0x0000A57E00D84E1F AS DateTime))
INSERT [dbo].[Users] ([UserId], [UserName], [Password], [FullName], [Email], [IsEnabled], [LastEdit]) VALUES (2, N'rumon', N'admin123', N'Rumon Kumar Prosad', N'rumon10@gmail.com', 1, CAST(0x0000A57D00FB2162 AS DateTime))
SET IDENTITY_INSERT [dbo].[Users] OFF
/****** Object:  StoredProcedure [dbo].[GetCustomers]    Script Date: 12/30/2015 13:25:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCustomers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[GetCustomers]
    @PageIndex INT ,
    @SortColumnName VARCHAR(50) ,
    @SortOrderBy VARCHAR(4) ,
    @NumberOfRows INT ,
    @TotalRecords INT OUTPUT
AS 
    BEGIN
    
        SET NOCOUNT ON 
        SELECT  @TotalRecords = ( SELECT COUNT(1) FROM  [CustTable] )
        
        DECLARE @StartRow INT
        SET @StartRow = ( @PageIndex * @NumberOfRows ) + 1 ;
        
        
        WITH    CTE
                  AS ( SELECT   ROW_NUMBER() OVER ( ORDER BY CASE
                                                              WHEN @SortColumnName = ''CustomerId''
                                                              AND @SortOrderBy = ''asc''
                                                              THEN CustomerId
                                                             END ASC, CASE
                                                              WHEN @SortColumnName = ''CustomerId''
                                                              AND @SortOrderBy = ''desc''
                                                              THEN CustomerId
                                                              END DESC, CASE
                                                              WHEN @SortColumnName = ''CustomerName''
                                                              AND @SortOrderBy = ''asc''
                                                              THEN CustomerName
                                                              END ASC, CASE
                                                              WHEN @SortColumnName = ''CustomerName''
                                                              AND @SortOrderBy = ''desc''
                                                              THEN CustomerName
                                                              END DESC, CASE
                                                              WHEN @SortColumnName = ''ContactNo''
                                                              AND @SortOrderBy = ''asc''
                                                              THEN ContactNo
                                                              END ASC, CASE
                                                              WHEN @SortColumnName = ''ContactNo''
                                                              AND @SortOrderBy = ''desc''
                                                              THEN ContactNo
                                                              END DESC , CASE
                                                              WHEN @SortColumnName = ''EmailAddress''
                                                              AND @SortOrderBy = ''asc''
                                                              THEN EmailAddress
                                                              END ASC, CASE
                                                              WHEN @SortColumnName = ''EmailAddress''
                                                              AND @SortOrderBy = ''desc''
                                                              THEN EmailAddress
                                                              END DESC , CASE
                                                              WHEN @SortColumnName = ''Address''
                                                              AND @SortOrderBy = ''asc''
                                                              THEN [Address]
                                                              END ASC, CASE
                                                              WHEN @SortColumnName = ''Address''
                                                              AND @SortOrderBy = ''desc''
                                                              THEN [Address]
                                                              END DESC, CASE
                                                              WHEN @SortColumnName = ''City''
                                                              AND @SortOrderBy = ''asc''
                                                              THEN City
                                                              END ASC, CASE
                                                              WHEN @SortColumnName = ''City''
                                                              AND @SortOrderBy = ''desc''
                                                              THEN City
                                                              WHEN @SortColumnName = ''State''
                                                              AND @SortOrderBy = ''asc''
                                                              THEN [State]
                                                              END ASC, CASE
                                                              WHEN @SortColumnName = ''State''
                                                              AND @SortOrderBy = ''desc''
                                                              THEN [State]
                                                              WHEN @SortColumnName = ''ZipCode''
                                                              AND @SortOrderBy = ''asc''
                                                              THEN ZipCode
                                                              END ASC, CASE
                                                              WHEN @SortColumnName = ''ZipCode''
                                                              AND @SortOrderBy = ''desc''
                                                              THEN ZipCode
                                                              END DESC ) AS RN ,
                                CustomerId ,
                                CustomerName ,
                                ContactNo ,
                                EmailAddress ,
                                [Address],
                                City,
                                [State],
                                ZipCode,
                                [Status]
                       FROM     CustTable
                     )
            SELECT  CustomerId ,
                    CustomerName ,
                    ContactNo,
                    EmailAddress,
                    [Address],
                    City,
                    [State],
                    ZipCode,
                   [Status]
            FROM    CTE
            WHERE   RN BETWEEN @StartRow - @NumberOfRows
                       AND     @StartRow - 1
       
        SET NOCOUNT OFF


    END' 
END
GO
/****** Object:  Default [DF_CustTable_Status]    Script Date: 12/30/2015 13:25:15 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CustTable_Status]') AND parent_object_id = OBJECT_ID(N'[dbo].[CustTable]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CustTable_Status]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CustTable] ADD  CONSTRAINT [DF_CustTable_Status]  DEFAULT ((0)) FOR [Status]
END


End
GO
/****** Object:  Default [DF_NumberSquenceTable_NextNumber]    Script Date: 12/30/2015 13:25:15 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_NumberSquenceTable_NextNumber]') AND parent_object_id = OBJECT_ID(N'[dbo].[NumberSquenceTable]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_NumberSquenceTable_NextNumber]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[NumberSquenceTable] ADD  CONSTRAINT [DF_NumberSquenceTable_NextNumber]  DEFAULT ((250000)) FOR [NextNumber]
END


End
GO
/****** Object:  Default [DF_TechTable_Status]    Script Date: 12/30/2015 13:25:15 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_TechTable_Status]') AND parent_object_id = OBJECT_ID(N'[dbo].[TechTable]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TechTable_Status]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TechTable] ADD  CONSTRAINT [DF_TechTable_Status]  DEFAULT ((0)) FOR [Status]
END


End
GO
/****** Object:  Default [DF_Users_IsEnabled]    Script Date: 12/30/2015 13:25:15 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Users_IsEnabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Users_IsEnabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsEnabled]  DEFAULT ((0)) FOR [IsEnabled]
END


End
GO
/****** Object:  Default [DF_WorkOrders_TotalUsedTime]    Script Date: 12/30/2015 13:25:15 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_WorkOrders_TotalUsedTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkOrder]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WorkOrders_TotalUsedTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WorkOrder] ADD  CONSTRAINT [DF_WorkOrders_TotalUsedTime]  DEFAULT ((0.00)) FOR [TotalUsedTime]
END


End
GO
/****** Object:  Default [DF_WorkOrders_TotalCharge]    Script Date: 12/30/2015 13:25:15 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_WorkOrders_TotalCharge]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkOrder]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WorkOrders_TotalCharge]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WorkOrder] ADD  CONSTRAINT [DF_WorkOrders_TotalCharge]  DEFAULT ((0.00)) FOR [TotalCharge]
END


End
GO
/****** Object:  Default [DF_WorkOrderLine_UsedTime]    Script Date: 12/30/2015 13:25:15 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_WorkOrderLine_UsedTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkOrderLine]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WorkOrderLine_UsedTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WorkOrderLine] ADD  CONSTRAINT [DF_WorkOrderLine_UsedTime]  DEFAULT ((0.00)) FOR [UsedTime]
END


End
GO
