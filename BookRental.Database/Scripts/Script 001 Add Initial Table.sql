CREATE TABLE [dbo].[com_Book](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](MAX) NOT NULL,
	[CreatedBy] [nvarchar](56) NOT NULL,
	[CreatedDateTime] [datetime2](7) NOT NULL,
	[LastModifiedBy] [nvarchar](56) NOT NULL,
	[LastModifiedDateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_com_Book] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[com_Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Address] [nvarchar](MAX) NOT NULL,
	[CreatedBy] [nvarchar](56) NOT NULL,
	[CreatedDateTime] [datetime2](7) NOT NULL,
	[LastModifiedBy] [nvarchar](56) NOT NULL,
	[LastModifiedDateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_com_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[com_Rental](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[BookId] [int] NOT NULL,
	[RentDate] [datetime2](7) NOT NULL,
	[ReturnDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](56) NOT NULL,
	[CreatedDateTime] [datetime2](7) NOT NULL,
	[LastModifiedBy] [nvarchar](56) NOT NULL,
	[LastModifiedDateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_com_Rental] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[com_Rental]  WITH CHECK ADD  CONSTRAINT [FK_com_Book_to_com_Rental] FOREIGN KEY([BookId])
REFERENCES [dbo].[com_Book] ([Id])
GO

ALTER TABLE [dbo].[com_Rental] CHECK CONSTRAINT [FK_com_Book_to_com_Rental]
GO

ALTER TABLE [dbo].[com_Rental]  WITH CHECK ADD  CONSTRAINT [FK_com_Customer_to_com_Rental] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[com_Customer] ([Id])
GO

ALTER TABLE [dbo].[com_Rental] CHECK CONSTRAINT [FK_com_Customer_to_com_Rental]
GO
