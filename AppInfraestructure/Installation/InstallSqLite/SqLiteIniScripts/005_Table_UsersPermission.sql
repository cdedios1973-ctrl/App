CREATE TABLE IF NOT EXISTS [UsersPermission] ( 
    [Id] INTEGER PRIMARY KEY,
    [UserId] INTEGER NOT NULL,
    [PermissionId] INTEGER NOT NULL,
    [userAccessType] INTEGER NOT NULL,
    CONSTRAINT [uq_UsersPermission_UI_PI] UNIQUE([UserId], [PermissionId]), 
    CONSTRAINT FK_UsersPermission_Permissions FOREIGN KEY(PermissionId) 
    REFERENCES Permissions(Id), 
    CONSTRAINT FK_UsersPermission_Users FOREIGN KEY(UserId)
    REFERENCES Users(Id) 
    );