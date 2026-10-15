CREATE TABLE IF NOT EXISTS [UsersLogin] ( 
    [Id] INTEGER PRIMARY KEY,
    [UserId] INTEGER NOT NULL UNIQUE,
    [UserName] NCHAR(10) NOT NULL,
    [Password] NCHAR(10) NOT NULL,
    CONSTRAINT [FK_UsersLogin_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]),
    CONSTRAINT [uq_UsersLogin_UserName] UNIQUE ([UserName]) 
    );
