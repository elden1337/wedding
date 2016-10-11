
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/07/2016 00:23:33
-- Generated from EDMX file: C:\Users\magnus.r\Documents\Source\wedding\Wedding.Data\Wedding.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [boda_live];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__car__user_id__503BEA1C]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[car] DROP CONSTRAINT [FK__car__user_id__503BEA1C];
GO
IF OBJECT_ID(N'[dbo].[FK__guest_boo__guest__339FAB6E]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[guest_booking] DROP CONSTRAINT [FK__guest_boo__guest__339FAB6E];
GO
IF OBJECT_ID(N'[dbo].[FK__x_car_boo__car_i__6EF57B66]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[car_booking] DROP CONSTRAINT [FK__x_car_boo__car_i__6EF57B66];
GO
IF OBJECT_ID(N'[dbo].[FK__x_car_boo__guest__6E01572D]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[car_booking] DROP CONSTRAINT [FK__x_car_boo__guest__6E01572D];
GO
IF OBJECT_ID(N'[dbo].[FK__x_food__guest_id__6B24EA82]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[food] DROP CONSTRAINT [FK__x_food__guest_id__6B24EA82];
GO
IF OBJECT_ID(N'[dbo].[FK__x_playlis__guest__656C112C]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[playlists] DROP CONSTRAINT [FK__x_playlis__guest__656C112C];
GO
IF OBJECT_ID(N'[dbo].[FK__x_toastme__guest__628FA481]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[toastmessage] DROP CONSTRAINT [FK__x_toastme__guest__628FA481];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[__MigrationHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[__MigrationHistory];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[car]', 'U') IS NOT NULL
    DROP TABLE [dbo].[car];
GO
IF OBJECT_ID(N'[dbo].[car_booking]', 'U') IS NOT NULL
    DROP TABLE [dbo].[car_booking];
GO
IF OBJECT_ID(N'[dbo].[food]', 'U') IS NOT NULL
    DROP TABLE [dbo].[food];
GO
IF OBJECT_ID(N'[dbo].[guest]', 'U') IS NOT NULL
    DROP TABLE [dbo].[guest];
GO
IF OBJECT_ID(N'[dbo].[guest_booking]', 'U') IS NOT NULL
    DROP TABLE [dbo].[guest_booking];
GO
IF OBJECT_ID(N'[dbo].[playlists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[playlists];
GO
IF OBJECT_ID(N'[dbo].[toastmessage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[toastmessage];
GO
IF OBJECT_ID(N'[dbo].[translation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[translation];
GO
IF OBJECT_ID(N'[dbo].[UserTokens]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserTokens];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'car'
CREATE TABLE [dbo].[car] (
    [id] int IDENTITY(1,1) NOT NULL,
    [free_seats_out] int  NULL,
    [arriving_day] int  NULL,
    [home_address] varchar(255)  NULL,
    [home_lat] varchar(128)  NULL,
    [home_lon] varchar(128)  NULL,
    [user_id] nvarchar(128)  NULL,
    [departure_time] int  NULL
);
GO

-- Creating table 'car_booking'
CREATE TABLE [dbo].[car_booking] (
    [id] int IDENTITY(1,1) NOT NULL,
    [guest_id] int  NULL,
    [car_id] int  NULL,
    [route] int  NULL,
    [added] datetime  NULL
);
GO

-- Creating table 'food'
CREATE TABLE [dbo].[food] (
    [id] int IDENTITY(1,1) NOT NULL,
    [guest_id] int  NULL,
    [food_comment] varchar(255)  NULL,
    [lactose] bit  NULL,
    [vegan] bit  NULL,
    [vegetarian] bit  NULL,
    [pescetarian] bit  NULL,
    [added] datetime  NULL,
    [gluten] bit  NULL,
    [nut] bit  NULL,
    [bringscake] bit  NULL,
    [nonalco] bit  NULL
);
GO

-- Creating table 'guest'
CREATE TABLE [dbo].[guest] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] varchar(255)  NOT NULL,
    [email] varchar(255)  NULL,
    [phonenumber] varchar(255)  NULL,
    [login_token] varchar(255)  NULL,
    [coming] bit  NULL,
    [updated] datetime  NULL
);
GO

-- Creating table 'guest_booking'
CREATE TABLE [dbo].[guest_booking] (
    [id] int IDENTITY(1,1) NOT NULL,
    [guest_id] int  NULL,
    [fri_sat] bit  NULL,
    [sat_sun] bit  NULL,
    [booking_comment] varchar(1024)  NULL
);
GO

-- Creating table 'playlists'
CREATE TABLE [dbo].[playlists] (
    [id] int IDENTITY(1,1) NOT NULL,
    [song] varchar(255)  NULL,
    [added] binary(8)  NOT NULL,
    [guest_id] int  NULL,
    [message] varchar(255)  NULL
);
GO

-- Creating table 'toastmessage'
CREATE TABLE [dbo].[toastmessage] (
    [id] int IDENTITY(1,1) NOT NULL,
    [thread_id] int  NOT NULL,
    [added] binary(8)  NOT NULL,
    [guest_id] int  NULL,
    [message] nvarchar(max)  NULL
);
GO

-- Creating table 'translation'
CREATE TABLE [dbo].[translation] (
    [id] int IDENTITY(1,1) NOT NULL,
    [text] varchar(255)  NULL
);
GO

-- Creating table 'UserTokens'
CREATE TABLE [dbo].[UserTokens] (
    [UserId] nvarchar(128)  NOT NULL,
    [Token] varchar(255)  NOT NULL
);
GO


-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId], [ContextKey] ASC);
GO


-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [id] in table 'car'
ALTER TABLE [dbo].[car]
ADD CONSTRAINT [PK_car]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'car_booking'
ALTER TABLE [dbo].[car_booking]
ADD CONSTRAINT [PK_car_booking]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'food'
ALTER TABLE [dbo].[food]
ADD CONSTRAINT [PK_food]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'guest'
ALTER TABLE [dbo].[guest]
ADD CONSTRAINT [PK_guest]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'guest_booking'
ALTER TABLE [dbo].[guest_booking]
ADD CONSTRAINT [PK_guest_booking]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'playlists'
ALTER TABLE [dbo].[playlists]
ADD CONSTRAINT [PK_playlists]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'toastmessage'
ALTER TABLE [dbo].[toastmessage]
ADD CONSTRAINT [PK_toastmessage]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'translation'
ALTER TABLE [dbo].[translation]
ADD CONSTRAINT [PK_translation]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [UserId], [Token] in table 'UserTokens'
ALTER TABLE [dbo].[UserTokens]
ADD CONSTRAINT [PK_UserTokens]
    PRIMARY KEY CLUSTERED ([UserId], [Token] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [user_id] in table 'car'
ALTER TABLE [dbo].[car]
ADD CONSTRAINT [FK__car__user_id__503BEA1C]
    FOREIGN KEY ([user_id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__car__user_id__503BEA1C'
CREATE INDEX [IX_FK__car__user_id__503BEA1C]
ON [dbo].[car]
    ([user_id]);
GO

-- Creating foreign key on [car_id] in table 'car_booking'
ALTER TABLE [dbo].[car_booking]
ADD CONSTRAINT [FK__x_car_boo__car_i__6EF57B66]
    FOREIGN KEY ([car_id])
    REFERENCES [dbo].[car]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__x_car_boo__car_i__6EF57B66'
CREATE INDEX [IX_FK__x_car_boo__car_i__6EF57B66]
ON [dbo].[car_booking]
    ([car_id]);
GO

-- Creating foreign key on [guest_id] in table 'car_booking'
ALTER TABLE [dbo].[car_booking]
ADD CONSTRAINT [FK__x_car_boo__guest__6E01572D]
    FOREIGN KEY ([guest_id])
    REFERENCES [dbo].[guest]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__x_car_boo__guest__6E01572D'
CREATE INDEX [IX_FK__x_car_boo__guest__6E01572D]
ON [dbo].[car_booking]
    ([guest_id]);
GO

-- Creating foreign key on [guest_id] in table 'food'
ALTER TABLE [dbo].[food]
ADD CONSTRAINT [FK__x_food__guest_id__6B24EA82]
    FOREIGN KEY ([guest_id])
    REFERENCES [dbo].[guest]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__x_food__guest_id__6B24EA82'
CREATE INDEX [IX_FK__x_food__guest_id__6B24EA82]
ON [dbo].[food]
    ([guest_id]);
GO

-- Creating foreign key on [guest_id] in table 'guest_booking'
ALTER TABLE [dbo].[guest_booking]
ADD CONSTRAINT [FK__guest_boo__guest__339FAB6E]
    FOREIGN KEY ([guest_id])
    REFERENCES [dbo].[guest]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__guest_boo__guest__339FAB6E'
CREATE INDEX [IX_FK__guest_boo__guest__339FAB6E]
ON [dbo].[guest_booking]
    ([guest_id]);
GO

-- Creating foreign key on [guest_id] in table 'playlists'
ALTER TABLE [dbo].[playlists]
ADD CONSTRAINT [FK__x_playlis__guest__656C112C]
    FOREIGN KEY ([guest_id])
    REFERENCES [dbo].[guest]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__x_playlis__guest__656C112C'
CREATE INDEX [IX_FK__x_playlis__guest__656C112C]
ON [dbo].[playlists]
    ([guest_id]);
GO

-- Creating foreign key on [guest_id] in table 'toastmessage'
ALTER TABLE [dbo].[toastmessage]
ADD CONSTRAINT [FK__x_toastme__guest__628FA481]
    FOREIGN KEY ([guest_id])
    REFERENCES [dbo].[guest]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__x_toastme__guest__628FA481'
CREATE INDEX [IX_FK__x_toastme__guest__628FA481]
ON [dbo].[toastmessage]
    ([guest_id]);
GO


-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------