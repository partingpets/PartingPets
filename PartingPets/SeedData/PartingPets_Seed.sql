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
    [ID] [int] NOT NULL IDENTITY(1,1),
    [FirstName] [nvarchar](255) NOT NULL,
    [LastName] [nvarchar](255) NOT NULL,
    [Street] [nvarchar](255) NOT NULL,
    [City] [nvarchar](255) NULL,
    [State] [nvarchar](255) NULL,
    [ZipCode] [nvarchar](255) NOT NULL,
    [Email] [nvarchar](255) NOT NULL,
    [IsPartner] [bit] NOT NULL,
    [PartnerID] [int],
CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
    [ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Create a new table called '[Invoice]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Invoice]', 'U') IS NOT NULL
DROP TABLE [dbo].[Invoice]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Invoice](
    [ID] [int] NOT NULL IDENTITY(1,1),
    [UserID] [int] NOT NULL,
    [BillingStreet] [nvarchar](255) NOT NULL,
    [BillingZip] [int] NOT NULL,
    [BillingState] [nvarchar](255) NOT NULL,
    [BillingCity] [nvarchar](255) NOT NULL,
    [Total] [numeric](10, 2) NOT NULL,
CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
    [ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Create a new table called '[InvoiceLine]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[InvoiceLine]', 'U') IS NOT NULL
DROP TABLE [dbo].[InvoiceLine]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[InvoiceLine](
    [ID] [int] NOT NULL IDENTITY(1,1),
    [InvoiceID] [int] NOT NULL,
    [ProductID] [int] NOT NULL,
    [Quantity] [int] NOT NULL,
    [UnitPrice] [numeric](10, 2) NOT NULL,
    [ExtendedPrice] [numeric](10, 2) NOT NULL,
CONSTRAINT [PK_InvoiceLine] PRIMARY KEY CLUSTERED 
(
    [ID] ASC
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
    [ID] [int] NOT NULL IDENTITY(1,1),
    [Name] [nvarchar](255) NOT NULL,
    [Description] [nvarchar](255) NOT NULL,
    [Street] [nvarchar](255) NOT NULL,
    [City] [nvarchar](255) NOT NULL,
    [State] [nvarchar](255) NOT NULL,
    [Zipcode] [int] NOT NULL,
CONSTRAINT [PK_Partners] PRIMARY KEY CLUSTERED 
(
    [ID] ASC
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
    [ID] [int] NOT NULL IDENTITY(1,1),
    [Name] [nvarchar](255) NOT NULL,
    [UserId] [int] NOT NULL,
    [Breed] [nvarchar](255) NOT NULL,
    [DateOfBirth] [datetime] NOT NULL,
    [DateOfDeath] [datetime] NOT NULL,
    [BurialStreet] [nvarchar](255) NOT NULL,
    [BurialCity] [nvarchar](255) NULL,
    [BurialState] [nvarchar](255) NULL,
    [BurialZipCode] [nvarchar](255) NOT NULL,
    [BurialPlot] [nvarchar](255) NOT NULL,
CONSTRAINT [PK_Pets] PRIMARY KEY CLUSTERED 
(
    [ID] ASC
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
    [ID] [int] NOT NULL IDENTITY(1,1),
    [Name] [nvarchar](255) NOT NULL,
    [Type] [nvarchar](255) NOT NULL,
CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED 
(
    [ID] ASC
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
    [ID] [int] NOT NULL IDENTITY(1,1),
    [Name] [nvarchar](255) NOT NULL,
    [CategoryId] [int] NOT NULL,
    [UnitPrice] [numeric](10, 2) NOT NULL,
    [PartnerID] [int],
    [Description] [nvarchar](255) NOT NULL,
    [IsOnSale] [bit] NOT NULL,
CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
    [ID] ASC
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
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_UserID]
GO
ALTER TABLE [dbo].[InvoiceLine]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceLine_InvoiceID] FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[Invoice] ([ID])
GO
ALTER TABLE [dbo].[InvoiceLine] CHECK CONSTRAINT [FK_InvoiceLine_InvoiceID]
GO
ALTER TABLE [dbo].[InvoiceLine]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceLine_ProductID] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ID])
GO
ALTER TABLE [dbo].[InvoiceLine] CHECK CONSTRAINT [FK_InvoiceLine_ProductID]
GO
ALTER TABLE [dbo].[Pets]  WITH CHECK ADD  CONSTRAINT [FK_Pets_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Pets] CHECK CONSTRAINT [FK_Pets_UserId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[ProductCategory] ([ID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_CategoryId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_PartnerID] FOREIGN KEY([PartnerID])
REFERENCES [dbo].[Partners] ([ID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_PartnerID]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Partners] FOREIGN KEY([PartnerID])
REFERENCES [dbo].[Partners] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Partners]
GO
USE [master]
GO
ALTER DATABASE [PartingPets] SET  READ_WRITE 
GO

USE PartingPets
    INSERT INTO [dbo].[Partners] ([Name], [Description], [Street], [City], [State], [Zipcode]) VALUES ('Skinte','Vivamus in felis eu sapien cursus vestibulum. Proin eu mi.','05 Scofield Lane','Oklahoma City','Oklahoma','73104')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [Street], [City], [State], [Zipcode]) VALUES ('Voomm','Morbi vel lectus in quam fringilla rhoncus. Mauris enim leo, rhoncus sed, vestibulum sit amet, cursus id, turpis.','60122 Ludington Circle','Minneapolis','Minnesota','55402')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [Street], [City], [State], [Zipcode]) VALUES ('Jabberstorm','Morbi vel lectus in quam fringilla rhoncus. Mauris enim leo, rhoncus sed, vestibulum sit amet, cursus id, turpis.','841 Graceland Center','New Orleans','Louisiana','70142')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [Street], [City], [State], [Zipcode]) VALUES ('Vimbo','Phasellus sit amet erat. Nulla tempus.','28563 Vermont Trail','Dayton','Ohio','45403')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [Street], [City], [State], [Zipcode]) VALUES ('Quamba','Curabitur convallis.','85362 Golden Leaf Hill','Washington','District of Columbia','20566')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [Street], [City], [State], [Zipcode]) VALUES ('Browsetype','Morbi quis tortor id nulla ultrices aliquet. Maecenas leo odio, condimentum id, luctus nec, molestie sed, justo.','965 Melby Avenue','Boise','Idaho','83716')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [Street], [City], [State], [Zipcode]) VALUES ('Wikivu','Maecenas rhoncus aliquam lacus. Morbi quis tortor id nulla ultrices aliquet.','2184 Becker Plaza','Tampa','Florida','33610')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [Street], [City], [State], [Zipcode]) VALUES ('Oyoba','Nullam varius.','98922 Bartelt Junction','Decatur','Georgia','30089')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [Street], [City], [State], [Zipcode]) VALUES ('Photobug','Praesent blandit lacinia erat.','80802 Holy Cross Terrace','Saint Paul','Minnesota','55114')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [Street], [City], [State], [Zipcode]) VALUES ('Buzzbean','Mauris enim leo, rhoncus sed, vestibulum sit amet, cursus id, turpis.','92 Homewood Point','Dallas','Texas','75260')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [Street], [City], [State], [Zipcode]) VALUES ('Bubblemix','Mauris sit amet eros. Suspendisse accumsan tortor quis turpis.','39692 Meadow Vale Parkway','Oakland','California','94605')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [Street], [City], [State], [Zipcode]) VALUES ('Tazz','Maecenas ut massa quis augue luctus tincidunt. Nulla mollis molestie lorem.','45647 Anniversary Crossing','Santa Barbara','California','93150')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [Street], [City], [State], [Zipcode]) VALUES ('Thoughtstorm','Integer ac neque.','62497 Porter Trail','Washington','District of Columbia','20520')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [Street], [City], [State], [Zipcode]) VALUES ('Photolist','Nam ultrices, libero non mattis pulvinar, nulla pede ullamcorper augue, a suscipit nulla elit ac nulla. Sed vel enim sit amet nunc viverra dapibus.','5 Londonderry Pass','Torrance','California','90510')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [Street], [City], [State], [Zipcode]) VALUES ('Mudo','Cras in purus eu magna vulputate luctus.','833 Vidon Road','Pueblo','Colorado','81005')
    INSERT INTO [dbo].[Partners] ([Name], [Description], [Street], [City], [State], [Zipcode]) VALUES ('Avavee','Etiam vel augue. Vestibulum rutrum rutrum neque.','34941 Crescent Oaks Street','Hattiesburg','Mississippi','39404')

USE PartingPets
    INSERT INTO [dbo].[User] ([FirstName], [LastName], [Street], [City], [State], [ZipCode], [Email], [IsPartner], [PartnerID]) VALUES ('Webster','Frami','4979 Collier Walks','New Martineton','Maine','66888','Crystal_Nader@maida.com',0, null)
    INSERT INTO [dbo].[User] ([FirstName], [LastName], [Street], [City], [State], [ZipCode], [Email], [IsPartner], [PartnerID]) VALUES ('Irwin','Goodwin','956 Mann Coves North','Laurence','Iowa','40989','Golda_Bins@buster.info',1, 4)
    INSERT INTO [dbo].[User] ([FirstName], [LastName], [Street], [City], [State], [ZipCode], [Email], [IsPartner], [PartnerID]) VALUES ('Jaunita','Rippin','45573 Marley Junctions','Ortizland','Nevada','68240','Murphy@ariel.us',0, null)
    INSERT INTO [dbo].[User] ([FirstName], [LastName], [Street], [City], [State], [ZipCode], [Email], [IsPartner], [PartnerID]) VALUES ('Lori','Stark','89462 Susanna Station North','Alysson','Massachusetts','16759','Kara@laverne.org',0, null)
    INSERT INTO [dbo].[User] ([FirstName], [LastName], [Street], [City], [State], [ZipCode], [Email], [IsPartner], [PartnerID]) VALUES ('Shanna','Sawayn','01649 Karlee Union','New Noah','New York','96813','Morgan@rosemary.me',0, null)
    INSERT INTO [dbo].[User] ([FirstName], [LastName], [Street], [City], [State], [ZipCode], [Email], [IsPartner], [PartnerID]) VALUES ('Lavinia','Schmitt','420 Ratke Estates','Turnerfort','Wisconsin','94996','Darrin@judson.tv',1, 8)
    INSERT INTO [dbo].[User] ([FirstName], [LastName], [Street], [City], [State], [ZipCode], [Email], [IsPartner], [PartnerID]) VALUES ('Willis','Mohr','7984 Rath Brook','Wolffurt','Kansas','58656-2132','Chadrick.Stokes@toney.name',0, null)
    INSERT INTO [dbo].[User] ([FirstName], [LastName], [Street], [City], [State], [ZipCode], [Email], [IsPartner], [PartnerID]) VALUES ('Freeda','Torp','0809 Feil Pine West','Santos','Idaho','31470','Isabel@miracle.us',0, null)
    INSERT INTO [dbo].[User] ([FirstName], [LastName], [Street], [City], [State], [ZipCode], [Email], [IsPartner], [PartnerID]) VALUES ('Dejah','Veum','754 John Greens West','Celine','Massachusetts','69584-1413','Christ@elsie.info',0, null)
    INSERT INTO [dbo].[User] ([FirstName], [LastName], [Street], [City], [State], [ZipCode], [Email], [IsPartner], [PartnerID]) VALUES ('Mikel','Carter','763 Leannon Cove','Blandamouth','Iowa','43249-7406','Jessika@davion.info',1, 12)

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
    INSERT INTO [dbo].[Pets] ([UserId], [Name], [Breed], [DateOfBirth], [DateOfDeath], [BurialStreet], [BurialCity], [BurialState], [BurialZipCode], [BurialPlot]) VALUES (5,'Hawai''i Silversword','Collared lizard','2008-07-25 10:50:40','2004-11-07 10:29:37','378 Sycamore Hill','Charlotte','North Carolina','28272','SWM-40')

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
    INSERT INTO [dbo].[Products] ([Name], [CategoryId], [UnitPrice], [PartnerID], [Description], [IsOnSale]) VALUES ('Modern Casket', 1, 100.00, 3, 'The perfect size casket for any cat or small dog', 0)
    INSERT INTO [dbo].[Products] ([Name], [CategoryId], [UnitPrice], [PartnerID], [Description], [IsOnSale]) VALUES ('Kosher Cremation', 6, 276.99, 6, 'The most civilised and affordable cremation', 1)
    INSERT INTO [dbo].[Products] ([Name], [CategoryId], [UnitPrice], [PartnerID], [Description], [IsOnSale]) VALUES ('New Casket Smell Candle', 9, 9.99, null, 'Never forget the smell of the final resting place of your pet', 0)
    INSERT INTO [dbo].[Products] ([Name], [CategoryId], [UnitPrice], [PartnerID], [Description], [IsOnSale]) VALUES ('Silver Burial Service', 7, 129.99, 11, 'Burial service that includes your pets favorite toys', 0)
    INSERT INTO [dbo].[Products] ([Name], [CategoryId], [UnitPrice], [PartnerID], [Description], [IsOnSale]) VALUES ('Gold inlaid Urn', 2, 29.99, null, 'A glorious urn with 8 karat gold inlaid', 0)
    INSERT INTO [dbo].[Products] ([Name], [CategoryId], [UnitPrice], [PartnerID], [Description], [IsOnSale]) VALUES ('Reclaimed Wood 5x7 Frame', 5, 19.99, null, 'Stunning 5x7 reclaimed wooden frame for lasting memories', 0)
    INSERT INTO [dbo].[Products] ([Name], [CategoryId], [UnitPrice], [PartnerID], [Description], [IsOnSale]) VALUES ('Locket of Memories', 4, 14.99, null, 'Two sided locket to hold photos of you and your pet', 0)
    INSERT INTO [dbo].[Products] ([Name], [CategoryId], [UnitPrice], [PartnerID], [Description], [IsOnSale]) VALUES ('Photo Tee', 3, 29.99, null, '100% Egyption Cotton Tee with a picture of your pet', 0)
    INSERT INTO [dbo].[Products] ([Name], [CategoryId], [UnitPrice], [PartnerID], [Description], [IsOnSale]) VALUES ('2 Gun Funeral Salute', 8, 59.99, 7, 'Send your pet off right with a gun salute', 1)
    INSERT INTO [dbo].[Products] ([Name], [CategoryId], [UnitPrice], [PartnerID], [Description], [IsOnSale]) VALUES ('Gold Burial Service', 7, 179.99, 11, 'Burial service that includes Silver service plus a gun salute', 0)
   
USE PartingPets
    INSERT INTO [dbo].[Invoice] ([UserID], [BillingStreet], [BillingCity], [BillingState], [BillingZip], [Total]) VALUES (1, '4979 Collier Walks','New Martineton','Maine','66888', 119.98)
    INSERT INTO [dbo].[Invoice] ([UserID], [BillingStreet], [BillingCity], [BillingState], [BillingZip], [Total]) VALUES (3, '45573 Marley Junctions','Ortizland','Nevada','68240', 306.98)
    

USE PartingPets
    INSERT INTO [dbo].[InvoiceLine] ([InvoiceID], [ProductID], [Quantity], [UnitPrice], [ExtendedPrice]) VALUES (1, 1, 1, 100.00, 100.00)
    INSERT INTO [dbo].[InvoiceLine] ([InvoiceID], [ProductID], [Quantity], [UnitPrice], [ExtendedPrice]) VALUES (1,3, 2, 9.99, 19.98)
    INSERT INTO [dbo].[InvoiceLine] ([InvoiceID], [ProductID], [Quantity], [UnitPrice], [ExtendedPrice]) VALUES (2, 5, 1, 29.99, 29.99)
    INSERT INTO [dbo].[InvoiceLine] ([InvoiceID], [ProductID], [Quantity], [UnitPrice], [ExtendedPrice]) VALUES (2, 2, 1, 276.99, 276.99)