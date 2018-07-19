CREATE DATABASE [SMSService]
GO

USE [SMSService]
GO
/****** Object:  Schema [msg]    Script Date: 7/18/2018 11:18:15 PM ******/
CREATE SCHEMA [msg]
GO
/****** Object:  Schema [pbl]    Script Date: 7/18/2018 11:18:16 PM ******/
CREATE SCHEMA [pbl]
GO

/****** File Groups *****/
ALTER DATABASE SMSService
ADD FILEGROUP fgCLOB
GO

ALTER DATABASE SMSService
ADD FILEGROUP fgData
GO

ALTER DATABASE SMSService
ADD FILEGROUP fgIndexes
GO

/*****mama ********/
ALTER DATABASE SMSService
ADD FILE (
	NAME = SmsService_CLOB
	, FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\SmsService_CLOB.ndf'
	, SIZE = 6MB
	, MAXSIZE = Unlimited 
	, FILEGROWTH = 1
)
TO FILEGROUP fgCLOB
GO

ALTER DATABASE SMSService
ADD FILE (
	NAME = SmsService_Data
	, FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\SmsService_Data.ndf'
	, SIZE = 6MB
	, MAXSIZE = Unlimited 
	, FILEGROWTH = 1
)
TO FILEGROUP fgData
GO

ALTER DATABASE SMSService
ADD FILE (
	NAME = SmsService_Indexes
	, FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\SmsService_Indexes.ndf'
	, SIZE = 6MB
	, MAXSIZE = Unlimited 
	, FILEGROWTH = 1
)
TO FILEGROUP fgIndexes	
GO
/****** Object:  Table [msg].[Message]    Script Date: 7/18/2018 11:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [msg].[Message](
	[ID] [uniqueidentifier] NOT NULL,
	[SourceAccountID] [uniqueidentifier] NOT NULL,
	[ExternalMessageID] [bigint] NOT NULL,
	[Priority] [tinyint] NOT NULL,
	[SendType] [tinyint] NOT NULL,
	[EncodingType] [tinyint] NOT NULL,
	[Status] [smallint] NOT NULL,
	[SendDate] [datetime] NOT NULL,
	[DeliveryDate] [datetime] NULL,
	[UDH] [varchar](50) NULL,
	[Content] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Outgoing] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [fgData]
) ON [fgData] TEXTIMAGE_ON [fgCLOB]
GO
/****** Object:  Table [msg].[MessageReceiver]    Script Date: 7/18/2018 11:18:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [msg].[MessageReceiver](
	[ID] [uniqueidentifier] NOT NULL,
	[MessageID] [uniqueidentifier] NULL,
	[Number] [nvarchar](50) NULL,
	[IsQueue] [bit] NULL,
	[QueueDate] [datetime] NULL,
	[IsSent] [bit] NULL,
	[SendDate] [datetime] NULL,
 CONSTRAINT [PK_CellNumber] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [fgData]
) ON [fgData]
GO
/****** Object:  Table [msg].[SendTry]    Script Date: 7/18/2018 11:18:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [msg].[SendTry](
	[ID] [uniqueidentifier] NOT NULL,
	[ReceiverMessageID] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Succeed] [bit] NOT NULL,
	[Message] [nvarchar](500) NULL,
 CONSTRAINT [PK_SendTry] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [fgData]
) ON [fgData]
GO
/****** Object:  Table [pbl].[Account]    Script Date: 7/18/2018 11:18:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [pbl].[Account](
	[ID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](256) NOT NULL,
	[Domain] [varchar](1000) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Password] [varchar](256) NOT NULL,
	[Number] [varchar](50) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Type] [tinyint] NULL,
	[AlertCreditAmount] [decimal](8, 2) NULL,
	[CreditAlertCount] [int] NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [fgData]
) ON [fgData]
GO
/****** Object:  Table [pbl].[AccountAdminNumber]    Script Date: 7/18/2018 11:18:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [pbl].[AccountAdminNumber](
	[ID] [uniqueidentifier] NOT NULL,
	[AccountID] [uniqueidentifier] NULL,
	[Number] [nvarchar](20) NULL,
 CONSTRAINT [PK_AccountAdminNumber] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [fgData]
) ON [fgData]
GO
/****** Object:  Table [pbl].[Config]    Script Date: 7/18/2018 11:18:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [pbl].[Config](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [varchar](256) NOT NULL,
	[Value] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_Config] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [fgData],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [fgData]
) ON [fgData]
GO
ALTER TABLE [msg].[Message] ADD  CONSTRAINT [DF__Outgoing__Status__286302EC]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [msg].[Message]  WITH CHECK ADD  CONSTRAINT [FK_Outgoing_Account] FOREIGN KEY([SourceAccountID])
REFERENCES [pbl].[Account] ([ID])
GO
ALTER TABLE [msg].[Message] CHECK CONSTRAINT [FK_Outgoing_Account]
GO
ALTER TABLE [msg].[MessageReceiver]  WITH CHECK ADD  CONSTRAINT [FK_ReceiverMessage_Message] FOREIGN KEY([MessageID])
REFERENCES [msg].[Message] ([ID])
GO
ALTER TABLE [msg].[MessageReceiver] CHECK CONSTRAINT [FK_ReceiverMessage_Message]
GO
ALTER TABLE [msg].[SendTry]  WITH CHECK ADD  CONSTRAINT [FK_SendTry_ReceiverMessage] FOREIGN KEY([ReceiverMessageID])
REFERENCES [msg].[MessageReceiver] ([ID])
GO
ALTER TABLE [msg].[SendTry] CHECK CONSTRAINT [FK_SendTry_ReceiverMessage]
GO
ALTER TABLE [pbl].[AccountAdminNumber]  WITH CHECK ADD  CONSTRAINT [FK_AccountAdminNumber_Account] FOREIGN KEY([AccountID])
REFERENCES [pbl].[Account] ([ID])
GO
ALTER TABLE [pbl].[AccountAdminNumber] CHECK CONSTRAINT [FK_AccountAdminNumber_Account]
GO
