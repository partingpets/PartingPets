--DROP DATABASE [ IF EXISTS ] { database_name | database_snapshot_name } [ ,...n ] [;]
IF EXISTS (
    SELECT [name]
        FROM sys.databases
        WHERE [name] = N'PartingPets'
)
    BEGIN
        -- Delete Database Backup and Restore History from MSDB System Database
        EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'PartingPets'
        -- GO

        -- Close Connections
        USE [master]
        -- GO
        ALTER DATABASE [PartingPets] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
        -- GO

        -- Drop Database in SQL Server 
        DROP DATABASE [PartingPets]
        -- GO
    END


-- Create a new database called 'PartingPets'
-- Connect to the 'master' database to run this snippet
USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT [name]
        FROM sys.databases
        WHERE [name] = N'PartingPets'
)
CREATE DATABASE PartingPets
GO

USE PartingPets
GO

-- Create a new table called '[User]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[User]', 'U') IS NOT NULL
DROP TABLE [dbo].[User]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[User](
    [Id] [int] NOT NULL IDENTITY(1,1),
	[FireBaseUid] [nvarchar](255) NOT NULL,
    [FirstName] [nvarchar](255) NOT NULL,
    [LastName] [nvarchar](255) NOT NULL,
    [Street1] [nvarchar](255) NOT NULL,
	[Street2] [nvarchar](255),
    [City] [nvarchar](255) NULL,
    [State] [nvarchar](255) NULL,
    [ZipCode] [nvarchar](255) NOT NULL,
    [Email] [nvarchar](255) NOT NULL,
    [IsPartner] [bit] NOT NULL,
    [PartnerID] [int],
    [IsAdmin] [bit] NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    [DateDeleted] [datetime],
    [IsDeleted] [bit] NOT NULL,
CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Create a new table called '[PaymentType]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[PaymentType]', 'U') IS NOT NULL
DROP TABLE [dbo].[PaymentType]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[PaymentType](
    [Id] [int] NOT NULL IDENTITY(1,1),
    [UserId] [int] NOT NULL,
    [Name] [nvarchar](255) NOT NULL,
    [AccountNumber] [nvarchar](50) NOT NULL,
    [Type] [nvarchar](50) NOT NULL,
    [CVV] [int] NOT NULL,
    [ExpDate] [datetime] NOT NULL,
    [IsDeleted] [bit] NOT NULL
CONSTRAINT [PK_PaymentType] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Create a new table called '[Orders]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Orders]', 'U') IS NOT NULL
DROP TABLE [dbo].[Orders]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Orders](
    [Id] [int] NOT NULL IDENTITY(1000,1),
    [UserID] [int] NOT NULL,
    [PaymentTypeId] [int] NOT NULL,
    [PurchaseDate] [datetime] NOT NULL,
    [Total] [numeric](10, 2) NOT NULL,
CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Create a new table called '[OrdersLine]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[OrdersLine]', 'U') IS NOT NULL
DROP TABLE [dbo].[OrdersLine]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[OrdersLine](
    [Id] [int] NOT NULL IDENTITY(1,1),
    [OrdersId] [int] NOT NULL,
    [ProductId] [int] NOT NULL,
    [Quantity] [int] NOT NULL,
    [UnitPrice] [numeric](10, 2) NOT NULL,
CONSTRAINT [PK_OrdersLine] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Create a new table called '[ShoppingCart]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[ShoppingCart]', 'U') IS NOT NULL
DROP TABLE [dbo].[ShoppingCart]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[ShoppingCart](
    [Id] [int] NOT NULL IDENTITY(1,1),
    [UserID] [int] NOT NULL,
    [ProductID] [int] NOT NULL,
    [Quantity] [int] NOT NULL,
CONSTRAINT [PK_ShoppingCart] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Create a new table called '[Partners]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Partners]', 'U') IS NOT NULL
DROP TABLE [dbo].[Partners]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Partners](
    [Id] [int] NOT NULL IDENTITY(1,1),
    [Name] [nvarchar](255) NOT NULL,
    [Description] [nvarchar](255) NOT NULL,
    [RegistrationCode] [nvarchar](25),
    [Street] [nvarchar](255) NOT NULL,
    [City] [nvarchar](255) NOT NULL,
    [State] [nvarchar](255) NOT NULL,
    [Zipcode] [nvarchar](255) NOT NULL,
    [IsDeleted] [bit],
    [DateDeleted] [datetime],
CONSTRAINT [PK_Partners] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Create a new table called '[Pets]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Pets]', 'U') IS NOT NULL
DROP TABLE [dbo].[Pets]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Pets](
    [Id] [int] NOT NULL IDENTITY(1,1),
    [Name] [nvarchar](255) NOT NULL,
    [UserId] [int] NOT NULL,
    [Breed] [nvarchar](255) NOT NULL,
    [DateOfBirth] [datetime] NOT NULL,
    [DateOfDeath] [datetime] NULL,
    [BurialStreet] [nvarchar](255) NULL,
    [BurialCity] [nvarchar](255) NULL,
    [BurialState] [nvarchar](255) NULL,
    [BurialZipCode] [nvarchar](255) NULL,
    [BurialPlot] [nvarchar](255) NULL,
CONSTRAINT [PK_Pets] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Create a new table called '[ProductCategory]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[ProductCategory]', 'U') IS NOT NULL
DROP TABLE [dbo].[ProductCategory]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[ProductCategory](
    [Id] [int] NOT NULL IDENTITY(1,1),
    [Name] [nvarchar](255) NOT NULL,
    [Type] [nvarchar](255) NOT NULL,
CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Create a new table called '[Products]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Products]', 'U') IS NOT NULL
DROP TABLE [dbo].[Products]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Products](
    [Id] [int] NOT NULL IDENTITY(1,1),
    [Name] [nvarchar](255) NOT NULL,
	[ImageUrl] [nvarchar](255) NOT NULL,
    [CategoryId] [int] NOT NULL,
    [UnitPrice] [numeric](10, 2) NOT NULL,
    [PartnerID] [int],
    [Description] [nvarchar](255) NOT NULL,
    [IsOnSale] [bit] NOT NULL,
    [IsDeleted] [bit],
CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING ON
GO
/****** Object:  Index [idx_User_FirstName]    Script Date: 5/20/2019 9:15:57 PM ******/
CREATE NONCLUSTERED INDEX [idx_User_FirstName] ON [dbo].[User]
(
    [FirstName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[PaymentType]  WITH CHECK ADD  CONSTRAINT [FK_PaymentType_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_PaymentTypeId] FOREIGN KEY([PaymentTypeId])
REFERENCES [dbo].[PaymentType] ([Id])
GO

ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_UserID]
GO

ALTER TABLE [dbo].[OrdersLine]  WITH CHECK ADD  CONSTRAINT [FK_OrdersLine_OrdersID] FOREIGN KEY([OrdersID])
REFERENCES [dbo].[Orders] ([Id])
GO

ALTER TABLE [dbo].[OrdersLine] CHECK CONSTRAINT [FK_OrdersLine_OrdersID]
GO

ALTER TABLE [dbo].[OrdersLine]  WITH CHECK ADD  CONSTRAINT [FK_OrdersLine_ProductID] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([Id])
GO

ALTER TABLE [dbo].[OrdersLine] CHECK CONSTRAINT [FK_OrdersLine_ProductID]
GO

ALTER TABLE [dbo].[Pets]  WITH CHECK ADD  CONSTRAINT [FK_Pets_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[Pets] CHECK CONSTRAINT [FK_Pets_UserId]
GO

ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[ProductCategory] ([Id])
GO

ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_CategoryId]
GO

ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_PartnerID] FOREIGN KEY([PartnerID])
REFERENCES [dbo].[Partners] ([Id])
GO

ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_PartnerID]
GO

ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Partners] FOREIGN KEY([PartnerID])
REFERENCES [dbo].[Partners] ([Id])
GO

ALTER TABLE [dbo].[ShoppingCart]  WITH CHECK ADD  CONSTRAINT [FK_ShoppingCart_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[ShoppingCart]  WITH CHECK ADD  CONSTRAINT [FK_ShoppingCart_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO

ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Partners]
GO

USE [master]
GO

ALTER DATABASE [PartingPets] SET  READ_WRITE 
GO

USE PartingPets
    INSERT INTO [dbo].[Partners] ([Name], [Description], [RegistrationCode], [Street], [City], [State], [Zipcode]) VALUES ('Skinte','Vivamus in felis eu sapien cursus vestibulum. Proin eu mi.', 'yvkd34x5', '05 Scofield Lane','Oklahoma City','Oklahoma','73104')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [RegistrationCode], [Street], [City], [State], [Zipcode]) VALUES ('Voomm','Morbi vel lectus in quam fringilla rhoncus. Mauris enim leo, rhoncus sed, vestibulum sit amet, cursus id, turpis.', 'eemqrqwd', '60122 Ludington Circle','Minneapolis','Minnesota','55402')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [RegistrationCode], [Street], [City], [State], [Zipcode]) VALUES ('Jabberstorm','Morbi vel lectus in quam fringilla rhoncus. Mauris enim leo, rhoncus sed, vestibulum sit amet, cursus id, turpis.', '2p72x598', '841 Graceland Center','New Orleans','Louisiana','70142')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [RegistrationCode], [Street], [City], [State], [Zipcode]) VALUES ('Vimbo','Phasellus sit amet erat. Nulla tempus.', 'xrx5ay92', '28563 Vermont Trail','Dayton','Ohio','45403')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [RegistrationCode], [Street], [City], [State], [Zipcode]) VALUES ('Quamba','Curabitur convallis.', 'gd5v34rb', '85362 Golden Leaf Hill','Washington','District of Columbia','20566')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [RegistrationCode], [Street], [City], [State], [Zipcode]) VALUES ('Browsetype','Morbi quis tortor id nulla ultrices aliquet. Maecenas leo odio, condimentum id, luctus nec, molestie sed, justo.', '6vy875n7' ,'965 Melby Avenue','Boise','Idaho','83716')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [RegistrationCode], [Street], [City], [State], [Zipcode]) VALUES ('Wikivu','Maecenas rhoncus aliquam lacus. Morbi quis tortor id nulla ultrices aliquet.', 'y4y9a488', '2184 Becker Plaza','Tampa','Florida','33610')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [RegistrationCode], [Street], [City], [State], [Zipcode]) VALUES ('Oyoba','Nullam varius.', '4pkwywy6', '98922 Bartelt Junction','Decatur','Georgia','30089')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [RegistrationCode], [Street], [City], [State], [Zipcode]) VALUES ('Photobug','Praesent blandit lacinia erat.', 'ypak8dwq', '80802 Holy Cross Terrace','Saint Paul','Minnesota','55114')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [RegistrationCode], [Street], [City], [State], [Zipcode]) VALUES ('Buzzbean','Mauris enim leo, rhoncus sed, vestibulum sit amet, cursus id, turpis.', 'pdwvwwy6', '92 Homewood Point','Dallas','Texas','75260')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [RegistrationCode], [Street], [City], [State], [Zipcode]) VALUES ('Bubblemix','Mauris sit amet eros. Suspendisse accumsan tortor quis turpis.', 'wdyqzgv2', '39692 Meadow Vale Parkway','Oakland','California','94605')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [RegistrationCode], [Street], [City], [State], [Zipcode]) VALUES ('Tazz','Maecenas ut massa quis augue luctus tincidunt. Nulla mollis molestie lorem.', 'kjr6v8qm', '45647 Anniversary Crossing','Santa Barbara','California','93150')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [RegistrationCode], [Street], [City], [State], [Zipcode]) VALUES ('Thoughtstorm','Integer ac neque.', '96g3d3rw', '62497 Porter Trail','Washington','District of Columbia','20520')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [RegistrationCode], [Street], [City], [State], [Zipcode]) VALUES ('Photolist','Nam ultrices, libero non mattis pulvinar, nulla pede ullamcorper augue, a suscipit nulla elit ac nulla. Sed vel enim sit amet nunc viverra dapibus.', 'yzm6vymg', '5 Londonderry Pass','Torrance','California','90510')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [RegistrationCode], [Street], [City], [State], [Zipcode]) VALUES ('Mudo','Cras in purus eu magna vulputate luctus.', '6q9gxj57', '833 Vidon Road','Pueblo','Colorado','81005')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [RegistrationCode], [Street], [City], [State], [Zipcode]) VALUES ('Avavee','Etiam vel augue. Vestibulum rutrum rutrum neque.', 'jke7p92j', '34941 Crescent Oaks Street','Hattiesburg','Mississippi','39404')

USE PartingPets
    INSERT INTO [dbo].[User] ([FireBaseUid], [FirstName], [LastName], [Street1], [Street2], [City], [State], [ZipCode], [Email], [IsPartner], [PartnerID], [IsAdmin], [DateCreated], [DateDeleted], [IsDeleted]) VALUES ('QUZNyHcYTDomtpT9Kjmg4PUY3iEr', 'Webster','Frami','4979 Collier Walks',null,'New Martineton','Maine','66888','Crystal_Nader@maida.com',0, null, 0, '2019-04-05 11:25:23', null, 0)
    INSERT INTO [dbo].[User] ([FireBaseUid], [FirstName], [LastName], [Street1], [Street2], [City], [State], [ZipCode], [Email], [IsPartner], [PartnerID], [IsAdmin], [DateCreated], [DateDeleted], [IsDeleted]) VALUES ('BxZQxeTU8myVsZr4zwGPJWDYykwe', 'Irwin','Goodwin','956 Mann Coves North',null,'Laurence','Iowa','40989','Golda_Bins@buster.info', 1, 4, 0, '2018-11-23 14:16:21', null, 0)
    INSERT INTO [dbo].[User] ([FireBaseUid], [FirstName], [LastName], [Street1], [Street2], [City], [State], [ZipCode], [Email], [IsPartner], [PartnerID], [IsAdmin], [DateCreated], [DateDeleted], [IsDeleted]) VALUES ('sUZAGRK74MAvPmKzi7WfuMg7W72V', 'Jaunita','Rippin','45573 Marley Junctions',null,'Ortizland','Nevada','68240','Murphy@ariel.us', 0, null, 0, '2018-07-10 11:01:39', null, 0)
    INSERT INTO [dbo].[User] ([FireBaseUid], [FirstName], [LastName], [Street1], [Street2], [City], [State], [ZipCode], [Email], [IsPartner], [PartnerID], [IsAdmin], [DateCreated], [DateDeleted], [IsDeleted]) VALUES ('8edLWaKVeshvvTXTMKBibzY9z36F', 'Lori','Stark','89462 Susanna Station North',null,'Alysson','Massachusetts','16759','Kara@laverne.org', 0, null, 0, '2018-08-24 19:49:16', null, 0)
    INSERT INTO [dbo].[User] ([FireBaseUid], [FirstName], [LastName], [Street1], [Street2], [City], [State], [ZipCode], [Email], [IsPartner], [PartnerID], [IsAdmin], [DateCreated], [DateDeleted], [IsDeleted]) VALUES ('T4N8RcxhzLTc7rAdamsHHiPVXsiq', 'Shanna','Sawayn','01649 Karlee Union',null,'New Noah','New York','96813','Morgan@rosemary.me', 0, null, 0, '2018-08-09 19:40:41', null, 0)
    INSERT INTO [dbo].[User] ([FireBaseUid], [FirstName], [LastName], [Street1], [Street2], [City], [State], [ZipCode], [Email], [IsPartner], [PartnerID], [IsAdmin], [DateCreated], [DateDeleted], [IsDeleted]) VALUES ('9wLntz32B2U9LRFMGhQudw9VRZfn', 'Lavinia','Schmitt','420 Ratke Estates',null,'Turnerfort','Wisconsin','94996','Darrin@judson.tv', 1, 8, 0, '2019-05-16 12:48:19', null, 0)
    INSERT INTO [dbo].[User] ([FireBaseUid], [FirstName], [LastName], [Street1], [Street2], [City], [State], [ZipCode], [Email], [IsPartner], [PartnerID], [IsAdmin], [DateCreated], [DateDeleted], [IsDeleted]) VALUES ('DQH8vTqJW24637i3AmZ8xvh6dD8X', 'Willis','Mohr','7984 Rath Brook',null,'Wolffurt','Kansas','58656-2132','Chadrick.Stokes@toney.name', 0, null, 0, '2019-05-10 16:23:24', null, 0)
    INSERT INTO [dbo].[User] ([FireBaseUid], [FirstName], [LastName], [Street1], [Street2], [City], [State], [ZipCode], [Email], [IsPartner], [PartnerID], [IsAdmin], [DateCreated], [DateDeleted], [IsDeleted]) VALUES ('eZ2CWnThXXu7ZqbEjxNyQWZCoZhs', 'Freeda','Torp','0809 Feil Pine West',null,'Santos','Idaho','31470','Isabel@miracle.us', 0, null, 0, '2018-06-22 16:24:50', null, 0)
    INSERT INTO [dbo].[User] ([FireBaseUid], [FirstName], [LastName], [Street1], [Street2], [City], [State], [ZipCode], [Email], [IsPartner], [PartnerID], [IsAdmin], [DateCreated], [DateDeleted], [IsDeleted]) VALUES ('DPRCN67VANPGxhHkG6HUnnXjk4G4', 'Dejah','Veum','754 John Greens West',null,'Celine','Massachusetts','69584-1413','Christ@elsie.info', 0, null, 0, '2018-06-01 20:22:09', null, 0)
    INSERT INTO [dbo].[User] ([FireBaseUid], [FirstName], [LastName], [Street1], [Street2], [City], [State], [ZipCode], [Email], [IsPartner], [PartnerID], [IsAdmin], [DateCreated], [DateDeleted], [IsDeleted]) VALUES ('Qx4oZpGEsuWE7uPsHHETTYUyuprB', 'Mikel','Carter','763 Leannon Cove',null,'Blandamouth','Iowa','43249-7406','Jessika@davion.info', 1, 12, 0, '2018-07-13 07:08:48', '2018-12-15 03:32:50', 1)
    INSERT INTO [dbo].[User] ([FireBaseUid], [FirstName], [LastName], [Street1], [Street2], [City], [State], [ZipCode], [Email], [IsPartner], [PartnerID], [IsAdmin], [DateCreated], [DateDeleted], [IsDeleted]) VALUES ('9emWpmdvbNWnnHavCMKYP7kugx83', 'Marco','Crank','123 United Way',null,'Hollywood','California','90210','marco@home.com', 1, 5, 1, '2019-06-01 07:08:48', null, 0)
    INSERT INTO [dbo].[User] ([FireBaseUid], [FirstName], [LastName], [Street1], [Street2], [City], [State], [ZipCode], [Email], [IsPartner], [PartnerID], [IsAdmin], [DateCreated], [DateDeleted], [IsDeleted]) VALUES ('yY66USNBdQaZml8uxQ9K8v7eiVm1', 'Colin','White','123 United Way',null,'Hollywood','California','90210','colin@home.com', 1, 6, 1, '2019-06-01 07:08:48', null, 0)
    INSERT INTO [dbo].[User] ([FireBaseUid], [FirstName], [LastName], [Street1], [Street2], [City], [State], [ZipCode], [Email], [IsPartner], [PartnerID], [IsAdmin], [DateCreated], [DateDeleted], [IsDeleted]) VALUES ('t8pgNG5PsKhsnXpe9p6L4cAP9yH2', 'Jonathan','Mohan','123 United Way',null,'Hollywood','California','90210','jonathan@home.com', 1, 3, 1, '2019-06-01 07:08:48', null, 0)
    INSERT INTO [dbo].[User] ([FireBaseUid], [FirstName], [LastName], [Street1], [Street2], [City], [State], [ZipCode], [Email], [IsPartner], [PartnerID], [IsAdmin], [DateCreated], [DateDeleted], [IsDeleted]) VALUES ('rOWk75eq3wOhGvRQDGjLGX0iZYj2', 'Timothy','Harley','123 United Way',null,'Hollywood','California','90210','timothy@home.com', 1, 2, 1, '2019-06-01 07:08:48', null, 0)


USE PartingPets
    INSERT INTO [dbo].[Pets] ([UserId], [Name], [Breed], [DateOfBirth], [DateOfDeath], [BurialStreet], [BurialCity], [BurialState], [BurialZipCode], [BurialPlot]) VALUES (1,'Spotted Beebalm','Pie, rufous tree','2007-06-26 04:32:05','2017-07-11 04:24:56','2404 Sommers Point','Stockton','California','95205','XND-16')
    INSERT INTO [dbo].[Pets] ([UserId], [Name], [Breed], [DateOfBirth], [DateOfDeath], [BurialStreet], [BurialCity], [BurialState], [BurialZipCode], [BurialPlot]) VALUES (2,'Nelson''s Brachythecium Moss','Turtle, eastern box','2014-06-30 05:35:17','2014-05-06 19:58:31','47 Hanson Way','Sacramento','California','94297','QRB-77')
    INSERT INTO [dbo].[Pets] ([UserId], [Name], [Breed], [DateOfBirth], [DateOfDeath], [BurialStreet], [BurialCity], [BurialState], [BurialZipCode], [BurialPlot]) VALUES (3,'Limber Honeysuckle','Orca','2006-11-21 05:47:59','2008-11-02 06:40:41','5 Clarendon Junction','South Lake Tahoe','California','96154','ANJ-26')
    INSERT INTO [dbo].[Pets] ([UserId], [Name], [Breed], [DateOfBirth], [DateOfDeath], [BurialStreet], [BurialCity], [BurialState], [BurialZipCode], [BurialPlot]) VALUES (4,'Glorybower','Agama lizard (unidentified)','2015-07-19 19:36:49','2011-10-09 13:51:48','38 Forest Dale Center','San Francisco','California','94137','HYT-23')
    INSERT INTO [dbo].[Pets] ([UserId], [Name], [Breed], [DateOfBirth], [DateOfDeath], [BurialStreet], [BurialCity], [BurialState], [BurialZipCode], [BurialPlot]) VALUES (5,'White False Gilyflower','Whale, long-finned pilot','2005-07-24 10:33:17','2008-06-10 16:31:04','47865 Merrick Circle','Baltimore','Maryland','21216','PCV-69')
    INSERT INTO [dbo].[Pets] ([UserId], [Name], [Breed], [DateOfBirth], [DateOfDeath], [BurialStreet], [BurialCity], [BurialState], [BurialZipCode], [BurialPlot]) VALUES (6,'Velvetbells','Magistrate black colobus','2013-08-08 13:49:36','2000-08-16 13:46:29','0 Golf View Place','Humble','Texas','77346','CKB-04')
    INSERT INTO [dbo].[Pets] ([UserId], [Name], [Breed], [DateOfBirth], [DateOfDeath], [BurialStreet], [BurialCity], [BurialState], [BurialZipCode], [BurialPlot]) VALUES (7,'Northern Bluebells','Grey-footed squirrel','2017-03-11 13:20:44','2015-03-22 14:42:19','79 Novick Hill','Washington','District of Columbia','20591','QOA-43')
    INSERT INTO [dbo].[Pets] ([UserId], [Name], [Breed], [DateOfBirth], [DateOfDeath], [BurialStreet], [BurialCity], [BurialState], [BurialZipCode], [BurialPlot]) VALUES (8,'Clusterspike False Indigo','Snow goose','2008-05-03 17:29:05','2006-04-25 09:59:54','9 Elmside Point','Austin','Texas','78715','VGI-66')
    INSERT INTO [dbo].[Pets] ([UserId], [Name], [Breed], [DateOfBirth], [DateOfDeath], [BurialStreet], [BurialCity], [BurialState], [BurialZipCode], [BurialPlot]) VALUES (9,'Thicket Tribisee','Racer, american','2013-06-09 16:39:58','2016-01-21 22:18:32','13825 Grayhawk Place','Norwalk','Connecticut','06859','DDI-67')
    INSERT INTO [dbo].[Pets] ([UserId], [Name], [Breed], [DateOfBirth], [DateOfDeath], [BurialStreet], [BurialCity], [BurialState], [BurialZipCode], [BurialPlot]) VALUES (10,'Lindenleaf Rosemallow','Bengal vulture','2006-03-24 02:35:04','2015-01-05 07:02:17','76 Ramsey Lane','Milwaukee','Wisconsin','53234','GRA-26')
    INSERT INTO [dbo].[Pets] ([UserId], [Name], [Breed], [DateOfBirth], [DateOfDeath], [BurialStreet], [BurialCity], [BurialState], [BurialZipCode], [BurialPlot]) VALUES (3,'Front Range Twinpod','White-bellied sea eagle','2013-04-22 20:42:24','2014-03-04 21:45:49','07 Reindahl Avenue','Dallas','Texas','75251','YUI-58')
    INSERT INTO [dbo].[Pets] ([UserId], [Name], [Breed], [DateOfBirth], [DateOfDeath], [BurialStreet], [BurialCity], [BurialState], [BurialZipCode], [BurialPlot]) VALUES (5,'Hawai''i Silversword','Collared lizard','2009-07-25 10:50:40','2004-11-07 10:29:37','378 Sycamore Hill','Charlotte','North Carolina','28272','SWM-40')
	INSERT INTO [dbo].[Pets] ([UserId], [Name], [Breed], [DateOfBirth], [DateOfDeath], [BurialStreet], [BurialCity], [BurialState], [BurialZipCode], [BurialPlot]) VALUES (11,'Volt','Snail','2008-07-25 10:50:40','2010-11-07 10:29:37','378 Sycamore Hill','Toronto','Ontario','99272','SWM-41')
    INSERT INTO [dbo].[Pets] ([UserId], [Name], [Breed], [DateOfBirth], [DateOfDeath], [BurialStreet], [BurialCity], [BurialState], [BurialZipCode], [BurialPlot]) VALUES (11,'Wayne','Skunk','2008-07-25 10:50:40','2009-11-07 10:29:37','378 Sycamore Hill','Toronto','Ontario','99272','SWM-42')
    INSERT INTO [dbo].[Pets] ([UserId], [Name], [Breed], [DateOfBirth], [DateOfDeath], [BurialStreet], [BurialCity], [BurialState], [BurialZipCode], [BurialPlot]) VALUES (12,'Deku','Tree Frog','2008-07-25 10:50:40','2011-11-07 10:29:37','378 Sycamore Hill','Toronto','Ontario','99272','SWM-43')
    INSERT INTO [dbo].[Pets] ([UserId], [Name], [Breed], [DateOfBirth], [DateOfDeath], [BurialStreet], [BurialCity], [BurialState], [BurialZipCode], [BurialPlot]) VALUES (12,'Voodoo','Walrus','2008-07-25 10:50:40','2017-11-07 10:29:37','378 Sycamore Hill','Toronto','Ontario','99272','SWM-44')
    INSERT INTO [dbo].[Pets] ([UserId], [Name], [Breed], [DateOfBirth], [DateOfDeath], [BurialStreet], [BurialCity], [BurialState], [BurialZipCode], [BurialPlot]) VALUES (13,'Grover','Squirrel','2008-07-25 10:50:40','2010-11-07 10:29:37','378 Sycamore Hill','Toronto','Ontario','99272','SWM-45')
    INSERT INTO [dbo].[Pets] ([UserId], [Name], [Breed], [DateOfBirth], [DateOfDeath], [BurialStreet], [BurialCity], [BurialState], [BurialZipCode], [BurialPlot]) VALUES (13,'Tusk','Donkey','2008-07-25 10:50:40','2011-11-07 10:29:37','378 Sycamore Hill','Toronto','Ontario','99272','SWM-46')
    INSERT INTO [dbo].[Pets] ([UserId], [Name], [Breed], [DateOfBirth], [DateOfDeath], [BurialStreet], [BurialCity], [BurialState], [BurialZipCode], [BurialPlot]) VALUES (14,'Chief','Grizzly Bear','2008-07-25 10:50:40','2012-11-07 10:29:37','378 Sycamore Hill','Toronto','Ontario','99272','SWM-47')
    INSERT INTO [dbo].[Pets] ([UserId], [Name], [Breed], [DateOfBirth], [DateOfDeath], [BurialStreet], [BurialCity], [BurialState], [BurialZipCode], [BurialPlot]) VALUES (14,'Artemis','Barn Owl','2008-07-25 10:50:40','2014-11-07 10:29:37','378 Sycamore Hill','Toronto','Ontario','99272','SWM-48')


USE PartingPets
    INSERT INTO [dbo].[PaymentType] ([UserId], [Name], [AccountNumber], [Type], [CVV], [ExpDate], [IsDeleted]) VALUES (11, 'Marcos Amex', '3111111111111117', 'American Express', 123, '2020-07-25 10:50:40', 0)
    INSERT INTO [dbo].[PaymentType] ([UserId], [Name], [AccountNumber], [Type], [CVV], [ExpDate], [IsDeleted]) VALUES (12, 'Colins Visa', '4111111111111111', 'Visa', 123, '2020-07-25 10:50:40', 0)
    INSERT INTO [dbo].[PaymentType] ([UserId], [Name], [AccountNumber], [Type], [CVV], [ExpDate], [IsDeleted]) VALUES (13, 'Jonathans MasterCard', '5111111111111118', 'MasterCard', 123, '2020-07-25 10:50:40', 0)
    INSERT INTO [dbo].[PaymentType] ([UserId], [Name], [AccountNumber], [Type], [CVV], [ExpDate], [IsDeleted]) VALUES (14, 'Tims Discover', '6111111111111116', 'Discover', 123, '2020-07-25 10:50:40', 0)

USE PartingPets
    INSERT INTO [dbo].[ProductCategory] ([Name], [Type]) VALUES ('Casket','Product')
    INSERT INTO [dbo].[ProductCategory] ([Name], [Type]) VALUES ('Urn','Product')
    INSERT INTO [dbo].[ProductCategory] ([Name], [Type]) VALUES ('Clothing','Product')
    INSERT INTO [dbo].[ProductCategory] ([Name], [Type]) VALUES ('Trinket','Product')
    INSERT INTO [dbo].[ProductCategory] ([Name], [Type]) VALUES ('Picture Frame','Product')
    INSERT INTO [dbo].[ProductCategory] ([Name], [Type]) VALUES ('Cremation','Service')
    INSERT INTO [dbo].[ProductCategory] ([Name], [Type]) VALUES ('Burial','Service')
    INSERT INTO [dbo].[ProductCategory] ([Name], [Type]) VALUES ('Gun Salute','Service')
    INSERT INTO [dbo].[ProductCategory] ([Name], [Type]) VALUES ('Candle','Product')

USE PartingPets
    INSERT INTO [dbo].[Products] ([Name], [ImageUrl], [CategoryId], [UnitPrice], [PartnerID], [Description], [IsOnSale], [IsDeleted]) VALUES ('Modern Casket', 'http://intuitiveconsumer.com/blog/wp-content/uploads/2015/05/new-product.png', 1, 100.00, 3, 'The perfect size casket for any cat or small dog', 0, 1)
    INSERT INTO [dbo].[Products] ([Name], [ImageUrl], [CategoryId], [UnitPrice], [PartnerID], [Description], [IsOnSale], [IsDeleted]) VALUES ('Kosher Cremation', 'http://intuitiveconsumer.com/blog/wp-content/uploads/2015/05/new-product.png', 6, 276.99, 6, 'The most civilised and affordable cremation', 0, 0)
    INSERT INTO [dbo].[Products] ([Name], [ImageUrl], [CategoryId], [UnitPrice], [PartnerID], [Description], [IsOnSale], [IsDeleted]) VALUES ('New Casket Smell Candle', 'http://intuitiveconsumer.com/blog/wp-content/uploads/2015/05/new-product.png', 9, 9.99, null, 'Never forget the smell of the final resting place of your pet', 0, 0)
    INSERT INTO [dbo].[Products] ([Name], [ImageUrl], [CategoryId], [UnitPrice], [PartnerID], [Description], [IsOnSale], [IsDeleted]) VALUES ('Silver Burial Service', 'http://intuitiveconsumer.com/blog/wp-content/uploads/2015/05/new-product.png', 7, 129.99, 11, 'Burial service that includes your pets favorite toys', 0, 0)
    INSERT INTO [dbo].[Products] ([Name], [ImageUrl], [CategoryId], [UnitPrice], [PartnerID], [Description], [IsOnSale], [IsDeleted]) VALUES ('Gold inlaid Urn', 'http://intuitiveconsumer.com/blog/wp-content/uploads/2015/05/new-product.png', 2, 29.99, null, 'A glorious urn with 8 karat gold inlaid', 0, 0)
    INSERT INTO [dbo].[Products] ([Name], [ImageUrl], [CategoryId], [UnitPrice], [PartnerID], [Description], [IsOnSale], [IsDeleted]) VALUES ('Locket of Memories', 'http://intuitiveconsumer.com/blog/wp-content/uploads/2015/05/new-product.png', 4, 14.99, null, 'Two sided locket to hold photos of you and your pet', 0, 0)
    INSERT INTO [dbo].[Products] ([Name], [ImageUrl], [CategoryId], [UnitPrice], [PartnerID], [Description], [IsOnSale], [IsDeleted]) VALUES ('Photo Tee', 'http://intuitiveconsumer.com/blog/wp-content/uploads/2015/05/new-product.png', 3, 29.99, null, '100% Egyption Cotton Tee with a picture of your pet', 0, 0)
    INSERT INTO [dbo].[Products] ([Name], [ImageUrl], [CategoryId], [UnitPrice], [PartnerID], [Description], [IsOnSale], [IsDeleted]) VALUES ('2 Gun Funeral Salute', 'http://intuitiveconsumer.com/blog/wp-content/uploads/2015/05/new-product.png', 8, 59.99, 7, 'Send your pet off right with a gun salute', 1, 0)
    INSERT INTO [dbo].[Products] ([Name], [ImageUrl], [CategoryId], [UnitPrice], [PartnerID], [Description], [IsOnSale], [IsDeleted]) VALUES ('Gold Burial Service', 'http://intuitiveconsumer.com/blog/wp-content/uploads/2015/05/new-product.png', 7, 179.99, 11, 'Burial service that includes Silver service plus a gun salute', 0, 0)
    INSERT INTO [dbo].[Products] ([Name], [ImageUrl], [CategoryId], [UnitPrice], [PartnerID], [Description], [IsOnSale], [IsDeleted]) VALUES ('Reclaimed Wood 5x7 Frame', 'http://intuitiveconsumer.com/blog/wp-content/uploads/2015/05/new-product.png', 5, 19.99, null, 'Stunning 5x7 reclaimed wooden frame for lasting memories', 0, 0)
   
USE PartingPets
    INSERT INTO [dbo].[Orders] ([UserID], [PaymentTypeId], [PurchaseDate], [Total]) VALUES (11, 1, '2018-09-05 13:07:44', 119.98)
    INSERT INTO [dbo].[Orders] ([UserID], [PaymentTypeId], [PurchaseDate], [Total]) VALUES (12, 2, '2019-04-05 11:25:23', 306.98)
    INSERT INTO [dbo].[Orders] ([UserID], [PaymentTypeId], [PurchaseDate], [Total]) VALUES (13, 3, '2019-04-05 11:25:23', 306.98)
    INSERT INTO [dbo].[Orders] ([UserID], [PaymentTypeId], [PurchaseDate], [Total]) VALUES (14, 4, '2019-04-05 11:25:23', 306.98)
    INSERT INTO [dbo].[Orders] ([UserID], [PaymentTypeId], [PurchaseDate], [Total]) VALUES (11, 1, '2018-09-05 13:07:44', 119.98)
    INSERT INTO [dbo].[Orders] ([UserID], [PaymentTypeId], [PurchaseDate], [Total]) VALUES (12, 2, '2019-04-05 11:25:23', 306.98)
    INSERT INTO [dbo].[Orders] ([UserID], [PaymentTypeId], [PurchaseDate], [Total]) VALUES (13, 3, '2019-04-05 11:25:23', 306.98)
    INSERT INTO [dbo].[Orders] ([UserID], [PaymentTypeId], [PurchaseDate], [Total]) VALUES (14, 4, '2019-04-05 11:25:23', 306.98)
    

USE PartingPets
    INSERT INTO [dbo].[OrdersLine] ([OrdersID], [ProductID], [Quantity], [UnitPrice]) VALUES (1000, 1, 1, 100.00)
    INSERT INTO [dbo].[OrdersLine] ([OrdersID], [ProductID], [Quantity], [UnitPrice]) VALUES (1000, 2, 2, 276.99)
    INSERT INTO [dbo].[OrdersLine] ([OrdersID], [ProductID], [Quantity], [UnitPrice]) VALUES (1001, 3, 1, 9.99)
    INSERT INTO [dbo].[OrdersLine] ([OrdersID], [ProductID], [Quantity], [UnitPrice]) VALUES (1001, 4, 2, 129.99)
    INSERT INTO [dbo].[OrdersLine] ([OrdersID], [ProductID], [Quantity], [UnitPrice]) VALUES (1002, 5, 1, 29.99)
    INSERT INTO [dbo].[OrdersLine] ([OrdersID], [ProductID], [Quantity], [UnitPrice]) VALUES (1002, 6, 2, 14.99)
    INSERT INTO [dbo].[OrdersLine] ([OrdersID], [ProductID], [Quantity], [UnitPrice]) VALUES (1003, 7, 1, 29.99)
    INSERT INTO [dbo].[OrdersLine] ([OrdersID], [ProductID], [Quantity], [UnitPrice]) VALUES (1003, 8, 2, 59.99)
    INSERT INTO [dbo].[OrdersLine] ([OrdersID], [ProductID], [Quantity], [UnitPrice]) VALUES (1004, 9, 1, 179.99)
    INSERT INTO [dbo].[OrdersLine] ([OrdersID], [ProductID], [Quantity], [UnitPrice]) VALUES (1004, 10, 2, 19.99)
    INSERT INTO [dbo].[OrdersLine] ([OrdersID], [ProductID], [Quantity], [UnitPrice]) VALUES (1005, 9, 1, 179.99)
    INSERT INTO [dbo].[OrdersLine] ([OrdersID], [ProductID], [Quantity], [UnitPrice]) VALUES (1005, 10, 2, 19.99)
    INSERT INTO [dbo].[OrdersLine] ([OrdersID], [ProductID], [Quantity], [UnitPrice]) VALUES (1006, 9, 1, 179.99)
    INSERT INTO [dbo].[OrdersLine] ([OrdersID], [ProductID], [Quantity], [UnitPrice]) VALUES (1006, 10, 2, 19.99)
    INSERT INTO [dbo].[OrdersLine] ([OrdersID], [ProductID], [Quantity], [UnitPrice]) VALUES (1007, 9, 1, 179.99)
    INSERT INTO [dbo].[OrdersLine] ([OrdersID], [ProductID], [Quantity], [UnitPrice]) VALUES (1007, 10, 2, 19.99)

USE PartingPets
    -- Marco Shopping Cart
    INSERT INTO [dbo].[ShoppingCart] ([UserID], [ProductID], [Quantity]) VALUES (11, 3, 5)
    INSERT INTO [dbo].[ShoppingCart] ([UserID], [ProductID], [Quantity]) VALUES (11, 1, 2)
    INSERT INTO [dbo].[ShoppingCart] ([UserID], [ProductID], [Quantity]) VALUES (11, 8, 1)
    -- Colin Shopping Cart
    INSERT INTO [dbo].[ShoppingCart] ([UserID], [ProductID], [Quantity]) VALUES (12, 3, 5)
    INSERT INTO [dbo].[ShoppingCart] ([UserID], [ProductID], [Quantity]) VALUES (12, 1, 2)
    INSERT INTO [dbo].[ShoppingCart] ([UserID], [ProductID], [Quantity]) VALUES (12, 8, 1)
    -- Jonathan Shopping Cart
    INSERT INTO [dbo].[ShoppingCart] ([UserID], [ProductID], [Quantity]) VALUES (13, 3, 5)
    INSERT INTO [dbo].[ShoppingCart] ([UserID], [ProductID], [Quantity]) VALUES (13, 1, 2)
    INSERT INTO [dbo].[ShoppingCart] ([UserID], [ProductID], [Quantity]) VALUES (13, 8, 1)
    -- Tim Shopping Cart
    INSERT INTO [dbo].[ShoppingCart] ([UserID], [ProductID], [Quantity]) VALUES (14, 3, 5)
    INSERT INTO [dbo].[ShoppingCart] ([UserID], [ProductID], [Quantity]) VALUES (14, 1, 2)
    INSERT INTO [dbo].[ShoppingCart] ([UserID], [ProductID], [Quantity]) VALUES (14, 8, 1)

USE master