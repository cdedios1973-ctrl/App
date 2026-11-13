CREATE TABLE [dbo].[UsersPermission]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [PermissionId] INT NOT NULL,
    [userAccessType] INT NOT NULL,

    CONSTRAINT [uq_UsersPermission] UNIQUE ([UserId], [PermissionId]),
        
    CONSTRAINT FK_UsersPermission_Permissions FOREIGN KEY (PermissionId)
    REFERENCES Permissions (Id),

    CONSTRAINT FK_UsersPermission_Users FOREIGN KEY (UserId)
    REFERENCES Users (Id)
)
