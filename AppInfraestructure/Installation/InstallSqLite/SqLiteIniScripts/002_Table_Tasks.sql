CREATE TABLE IF NOT EXISTS [Tasks] ( 
    [Id] INTEGER PRIMARY KEY,
    [Name] VARCHAR(100) NOT NULL,
    [Observation] VARCHAR(100) NULL,
    CONSTRAINT [uq_Tasks_Name] UNIQUE ([Name]) 
    );
