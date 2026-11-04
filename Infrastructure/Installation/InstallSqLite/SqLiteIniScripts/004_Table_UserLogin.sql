CREATE TABLE IF NOT EXISTS [UserLogin] ( 
    [Id] INTEGER PRIMARY KEY,
    [UserId] INTEGER NOT NULL UNIQUE,
    [UserName] NCHAR(10) NOT NULL,
    [Password] NCHAR(10) NOT NULL,
    CONSTRAINT [FK_UserLogin_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]),
    CONSTRAINT [uq_UserLogin_UserName] UNIQUE ([UserName]) 
    );
