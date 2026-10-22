CREATE TABLE IF NOT EXISTS [Permission] ( 
    [Id] INTEGER PRIMARY KEY,
    [Parent] VARCHAR(60) NOT NULL, 
    [Name] VARCHAR(60) NOT NULL, 
    [Observation] VARCHAR(500) NULL,
    CONSTRAINT [uq_Permission_Name] UNIQUE ([Name]) 
    );