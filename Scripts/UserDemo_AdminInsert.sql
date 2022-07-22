USE [dbtest]
GO

INSERT INTO [dbo].[Users] ([FirstName],[LastName],[Phone],[Email],[Password],[Status])
     VALUES ('Super','Administrator','','admin@userdemo.com','Test@123',1)
GO

INSERT INTO [dbo].[Roles]([RoleName],[Active]) VALUES ('Administrator',1)
GO

INSERT INTO [dbo].[RolesUsers] ([Roles_RoleId],[Users_UserId]) VALUES (1,1)
GO

INSERT INTO [dbo].[RolesPermissions] VALUES ('Users',1,1,1,1,1)
INSERT INTO [dbo].[RolesPermissions] VALUES ('Roles',1,1,1,1,1)
INSERT INTO [dbo].[RolesPermissions] VALUES ('Permissions',1,1,1,1,1)
INSERT INTO [dbo].[RolesPermissions] VALUES ('Operations',1,1,1,1,1)
INSERT INTO [dbo].[RolesPermissions] VALUES ('Customers',1,1,1,1,1)

GO



