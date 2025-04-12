CREATE TABLE [dbo].[UsersLogin]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [UserId] INT NOT NULL UNIQUE,
    [UserName] NCHAR(10) NOT NULL, 
    [Password] NCHAR(10) NOT NULL, 

    CONSTRAINT [FK_UsersLogin_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]),
    CONSTRAINT [uq_UserName] UNIQUE ([UserName]) 
)
