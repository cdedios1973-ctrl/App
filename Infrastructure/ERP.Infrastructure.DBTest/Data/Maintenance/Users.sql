CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Surname] VARCHAR(100) NULL, 
    [Address] VARCHAR(100) NULL,
    [CountryIdentity] VARCHAR(50) NOT NULL, 

    CONSTRAINT [uq_Users] UNIQUE ([CountryIdentity]),
    CONSTRAINT [uq_Users_FullName] UNIQUE ([Name], [Surname])
)


