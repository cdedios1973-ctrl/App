CREATE TABLE [dbo].[Tasks]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(100) NOT NULL,
    [Observation] VARCHAR(100) NULL,

    CONSTRAINT [uq_Name] UNIQUE ([Name])
)
