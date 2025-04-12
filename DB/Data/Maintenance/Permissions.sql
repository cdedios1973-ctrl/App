CREATE TABLE [dbo].[Permissions]
(
	[Id] INT NOT NULL IDENTITY PRIMARY KEY, 
    [Parent] VARCHAR(60) NOT NULL, 
    [Name] VARCHAR(60) NOT NULL, 
    [Observation] VARCHAR(500) NULL
    
    CONSTRAINT [uq_Permissions_Name] UNIQUE ([Name])
)
