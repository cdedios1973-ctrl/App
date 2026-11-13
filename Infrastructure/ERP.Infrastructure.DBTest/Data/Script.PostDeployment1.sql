/*
Plantilla de script posterior a la implementación							
--------------------------------------------------------------------------------------
 Este archivo contiene instrucciones de SQL que se anexarán al script de compilación.		
 Use la sintaxis de SQLCMD para incluir un archivo en el script posterior a la implementación.			
 Ejemplo:      :r .\miArchivo.sql								
 Use la sintaxis de SQLCMD para hacer referencia a una variable en el script posterior a la implementación.		
 Ejemplo:      :setvar TableName miTabla							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


INSERT INTO Users (Name, Surname, Address, CountryIdentity) VALUES ('admin', 'surname admin', 'address admin', 'ES-123456')
INSERT INTO Users (Name, Surname, Address, CountryIdentity) VALUES ('user1', 'surname 001', 'address 001', 'FR-123456')
INSERT INTO Permissions (Parent, Name, Observation) VALUES ('Maintenance', 'Users', 'Table to storage Users personal data.')
INSERT INTO Permissions (Parent, Name, Observation) VALUES ('Maintenance', 'UsersLogin', 'Relational table used to vinculate Users with log in data.')
INSERT INTO Permissions (Parent, Name, Observation) VALUES ('Maintenance', 'Permissions', 'Table to storage Permissions data.')
INSERT INTO Permissions (Parent, Name, Observation) VALUES ('Maintenance', 'UserPermissions', 'Relational table used to vinculate users with modules.')
INSERT INTO Permissions (Parent, Name, Observation) VALUES ('Maintenance', 'Tasks', 'Table to storge Tasks.')
INSERT INTO Permissions (Parent, Name, Observation) VALUES ('Maintenance', 'UserTasks', 'Relational table used to vinculate Users with Task.')
INSERT INTO Permissions (Parent, Name, Observation) VALUES ('Config', 'DAOSchemas', 'Class to get Table columns info.')
INSERT INTO Permissions (Parent, Name, Observation) VALUES ('Config', 'Export', 'Export Table data from database to XML.')
INSERT INTO Tasks (Name, Observation) VALUES ('First task', 'Task notes....1')
INSERT INTO Tasks (Name, Observation) VALUES ('Second task', 'Task notes....2')
INSERT INTO UsersLogin (UserId, UserName, Password) VALUES (1, 'admin', '123')
INSERT INTO UsersLogin (UserId, UserName, Password) VALUES (2, 'user 2', '123')
INSERT INTO UsersPermission (UserId, PermissionId, userAccessType) VALUES (1, 1, 3)
INSERT INTO UsersPermission (UserId, PermissionId, userAccessType) VALUES (1, 2, 3)
INSERT INTO UsersPermission (UserId, PermissionId, userAccessType) VALUES (1, 3, 3)
INSERT INTO UsersPermission (UserId, PermissionId, userAccessType) VALUES (1, 4, 3)
INSERT INTO UsersPermission (UserId, PermissionId, userAccessType) VALUES (1, 5, 3)
INSERT INTO UsersPermission (UserId, PermissionId, userAccessType) VALUES (1, 6, 3)
INSERT INTO UsersPermission (UserId, PermissionId, userAccessType) VALUES (1, 7, 3)
INSERT INTO UsersPermission (UserId, PermissionId, userAccessType) VALUES (2, 1, 2)
INSERT INTO UsersPermission (UserId, PermissionId, userAccessType) VALUES (2, 1, 2)
INSERT INTO UserTasks (TaskId, UserId) VALUES (1, 1)
INSERT INTO UserTasks (TaskId, UserId) VALUES (2, 1)
INSERT INTO UserTasks (TaskId, UserId) VALUES (1, 2)

