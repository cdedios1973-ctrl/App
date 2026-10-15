CREATE TABLE IF NOT EXISTS [UserTasks] ( 
    [Id] INTEGER PRIMARY KEY,
    [TaskId]  INTEGER NOT NULL,
    [UserId]  INTEGER NOT NULL,
    CONSTRAINT [uq_UserTasks_TI_UI] UNIQUE([TaskId], [UserId]), 
    CONSTRAINT FK_UserTasks_Tasks FOREIGN KEY(TaskId) 
    REFERENCES Tasks(Id), 
    CONSTRAINT FK_UserTasks_Users FOREIGN KEY(UserId)
    REFERENCES Users(Id) 
    );