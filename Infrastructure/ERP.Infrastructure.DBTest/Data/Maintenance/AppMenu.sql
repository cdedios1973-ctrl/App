CREATE TABLE [dbo].[AppMenu]
(
	[Id] INT NOT NULL IDENTITY PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL,
	[Observation] VARCHAR(500) NULL

	CONSTRAINT [uq_AppMenu_Name] UNIQUE ([Name])
)
