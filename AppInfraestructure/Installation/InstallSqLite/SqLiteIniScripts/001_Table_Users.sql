CREATE TABLE IF NOT EXISTS [Users] ( 
    [Id] INTEGER PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL, 
    [Surname] VARCHAR(100) NULL, 
    [Address] VARCHAR(100) NULL, 
    [CountryIdentity] VARCHAR(50) NOT NULL, 
    CONSTRAINT [uq_CountryIdentity] UNIQUE ([CountryIdentity]),
    CONSTRAINT [uq_Users_FullName] UNIQUE ([Name], [Surname]) 
    );
