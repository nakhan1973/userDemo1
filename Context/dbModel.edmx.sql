
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/22/2022 04:56:14
-- Generated from EDMX file: D:\mygithub\userDemo1\Context\dbModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [dbtest];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_RolesPermissions_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RolesPermissions] DROP CONSTRAINT [FK_RolesPermissions_Roles];
GO
IF OBJECT_ID(N'[dbo].[FK_RolesUsers_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RolesUsers] DROP CONSTRAINT [FK_RolesUsers_Roles];
GO
IF OBJECT_ID(N'[dbo].[FK_RolesUsers_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RolesUsers] DROP CONSTRAINT [FK_RolesUsers_Users];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[RolesPermissions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RolesPermissions];
GO
IF OBJECT_ID(N'[dbo].[RolesUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RolesUsers];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [FirstName] varchar(50)  NOT NULL,
    [LastName] varchar(50)  NOT NULL,
    [Phone] varchar(50)  NULL,
    [Email] varchar(100)  NOT NULL,
    [Password] varchar(50)  NOT NULL,
    [Status] bit  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [RoleId] int IDENTITY(1,1) NOT NULL,
    [RoleName] varchar(50)  NOT NULL,
    [Active] bit  NOT NULL
);
GO

-- Creating table 'RolesPermissions'
CREATE TABLE [dbo].[RolesPermissions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ModuleName] varchar(50)  NOT NULL,
    [ViewPermission] bit  NOT NULL,
    [AddPermission] bit  NOT NULL,
    [EditPermission] bit  NOT NULL,
    [DeletePermission] bit  NOT NULL,
    [RoleId] int  NOT NULL
);
GO

-- Creating table 'RolesUsers'
CREATE TABLE [dbo].[RolesUsers] (
    [Roles_RoleId] int  NOT NULL,
    [Users_UserId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [RoleId] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([RoleId] ASC);
GO

-- Creating primary key on [Id] in table 'RolesPermissions'
ALTER TABLE [dbo].[RolesPermissions]
ADD CONSTRAINT [PK_RolesPermissions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Roles_RoleId], [Users_UserId] in table 'RolesUsers'
ALTER TABLE [dbo].[RolesUsers]
ADD CONSTRAINT [PK_RolesUsers]
    PRIMARY KEY CLUSTERED ([Roles_RoleId], [Users_UserId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Roles_RoleId] in table 'RolesUsers'
ALTER TABLE [dbo].[RolesUsers]
ADD CONSTRAINT [FK_RolesUsers_Role]
    FOREIGN KEY ([Roles_RoleId])
    REFERENCES [dbo].[Roles]
        ([RoleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_UserId] in table 'RolesUsers'
ALTER TABLE [dbo].[RolesUsers]
ADD CONSTRAINT [FK_RolesUsers_User]
    FOREIGN KEY ([Users_UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RolesUsers_User'
CREATE INDEX [IX_FK_RolesUsers_User]
ON [dbo].[RolesUsers]
    ([Users_UserId]);
GO

-- Creating foreign key on [RoleId] in table 'RolesPermissions'
ALTER TABLE [dbo].[RolesPermissions]
ADD CONSTRAINT [FK_RolesPermissions_Roles]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Roles]
        ([RoleId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RolesPermissions_Roles'
CREATE INDEX [IX_FK_RolesPermissions_Roles]
ON [dbo].[RolesPermissions]
    ([RoleId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------