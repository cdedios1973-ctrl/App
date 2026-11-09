CREATE TABLE IF NOT EXISTS [User] ( 
    [Id] INTEGER PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL, 
    [Surname] VARCHAR(100) NOT NULL, 
    [Address] VARCHAR(100) NULL,
    [Active] INTEGER NOT NULL DEFAULT 1 CONSTRAINT [chk_User_Active] CHECK ([Active] IN (0, 1)),
    [CountryIdentity] VARCHAR(50) NOT NULL, 
    CONSTRAINT [uq_CountryIdentity] UNIQUE ([CountryIdentity]),
    CONSTRAINT [uq_Users_FullName] UNIQUE ([Name], [Surname]) 
);
