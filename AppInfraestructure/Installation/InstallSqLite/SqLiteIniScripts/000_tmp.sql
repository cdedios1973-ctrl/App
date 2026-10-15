"Users" "CREATE TABLE IF NOT EXISTS [dbo].[Users] 
    ( [Id] BIGINT NOT NULL PRIMARY KEY IDENTITY,
    [Name] VARCHAR(50) NOT NULL, 
    [Surname] VARCHAR(100) NULL, 
    [Address] VARCHAR(100) NULL, 
    [CountryIdentity] VARCHAR(50) NOT NULL, 
    CONSTRAINT [uq_CountryIdentity] UNIQUE ([CountryIdentity]),
    CONSTRAINT [uq_Users_FullName] UNIQUE ([Name], [Surname]) );"

"Tasks" "CREATE TABLE [dbo].[Tasks] ( [Id] BIGINT NOT NULL PRIMARY KEY IDENTITY,
    [Name] VARCHAR(100) NOT NULL,
    [Observation] VARCHAR(100) NULL,
    CONSTRAINT [uq_Tasks_Name] UNIQUE ([Name])) );"

"Permissions" "CREATE TABLE [dbo].[Permissions] ( [Id] BIGINT NOT NULL PRIMARY KEY IDENTITY,
    [Parent] VARCHAR(60) NOT NULL, 
    [Name] VARCHAR(60) NOT NULL, 
    [Observation] VARCHAR(500) NULL,
    CONSTRAINT [uq_Permissions_Name] UNIQUE ([Name]) ) ";

                "UsersLogin", "CREATE TABLE " + _typeDb + "[UsersLogin] ( " +
                "[Id] " + _typePk +
                "[UserId] BIGINT NOT NULL UNIQUE, " +
                "[UserName] NCHAR(10) NOT NULL, " +
                "[Password] NCHAR(10) NOT NULL, " +
                "CONSTRAINT [FK_UsersLogin_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]), " +
                "CONSTRAINT [uq_UsersLogin_UserName] UNIQUE ([UserName])) ";

                ("UsersPermission", "CREATE TABLE " + _typeDb +  "[UsersPermission] ( " +
                "[Id] " + _typePk +
                "[UserId] " + _typeInt +
                "[PermissionId] " + _typeInt +
                "[userAccessType] " + _typeInt +
                "CONSTRAINT [uq_UsersPermission_UI_PI] UNIQUE([UserId], [PermissionId]), " +
                "CONSTRAINT FK_UsersPermission_Permissions FOREIGN KEY(PermissionId) " +
                "REFERENCES Permissions(Id), " +
                "CONSTRAINT FK_UsersPermission_Users FOREIGN KEY(UserId) " +
                "REFERENCES Users(Id)) "),

                ("UserTasks", "CREATE TABLE " + _typeDb + "[UserTasks] ( " +
                "[Id] " + _typePk +
                "[TaskId] " + _typeInt +
                "[UserId] " + _typeInt +
                "CONSTRAINT [uq_UserTasks_TI_UI] UNIQUE([TaskId], [UserId]), " +
                "CONSTRAINT FK_UserTasks_Tasks FOREIGN KEY(TaskId) " +
                "REFERENCES Tasks(Id), " +
                "CONSTRAINT FK_UserTasks_Users FOREIGN KEY(UserId) " +
                "REFERENCES Users(Id)) ")