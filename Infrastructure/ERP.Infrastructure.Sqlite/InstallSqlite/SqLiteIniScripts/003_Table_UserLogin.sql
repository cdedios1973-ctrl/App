CREATE TABLE IF NOT EXISTS [UserLogin] ( 
    [Id] INTEGER PRIMARY KEY,
    [UserId] INTEGER NOT NULL UNIQUE,
    [UserName] VARCHAR(50) NOT NULL,
    [Password] VARCHAR(100) NOT NULL,
    [MachineName] VARCHAR(150) NOT NULL,
    CONSTRAINT [FK_UserLogin_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]),
    CONSTRAINT [uq_UserLogin_UserName] UNIQUE ([UserName]),
    CONSTRAINT [uq_UserLogin_MachineName] UNIQUE ([MachineName])
);

-- NOTA DE DISEÑO: Se restringe MachineName como UNIQUE de forma intencionada.
-- Es una medida restrictiva para entornos de Intranet/Aplicación local: 
-- el empleado SOLO puede loguear y ejecutar el ThreadPrincipal desde su máquina asignada.
-- [Password] VARCHAR(100): Espacio ideal para hashes tipo SHA-256 o BCrypt
