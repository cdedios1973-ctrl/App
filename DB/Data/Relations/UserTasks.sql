CREATE TABLE [dbo].[UserTasks]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [TaskId] INT NOT NULL,
    [UserId] INT NOT NULL,

    CONSTRAINT [uq_UserTasks] UNIQUE ([TaskId], [UserId]),
    
    CONSTRAINT FK_UserTasks_Tasks FOREIGN KEY (TaskId)
    REFERENCES Tasks (Id),

    CONSTRAINT FK_UserTasks_Users FOREIGN KEY (UserId)
    REFERENCES Users (Id)
)
