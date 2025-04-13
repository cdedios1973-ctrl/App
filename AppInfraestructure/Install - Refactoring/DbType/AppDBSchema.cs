using App.Definitions.Enums;

namespace App.Config.DDBB
{
    internal sealed class AppDBSchema
    {
        internal AppDBSchema() { }

        #region DB : SQL Server DB Schema

        internal string GetDatabaseProps(string fileName)
        {
            string str = "CREATE DATABASE AppDb ON PRIMARY " +
                "(NAME = AppDb_Data, " +
                "FILENAME = '" + fileName + ".mdf', " +
                "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%)" +
                "LOG ON (NAME = AppDb_Log, " +
                "FILENAME = '" + fileName + ".ldf', " +
                "SIZE = 1MB, " +
                "MAXSIZE = 5MB, " +
                "FILEGROWTH = 10%)";
            return str;
        }

        #endregion

        #region DB : SQL Server / SqLite Table schemas

        internal List<(string, string)> GetTableSchemas(DataBaseType dbType)
        {
            bool _sqlite = (dbType == DataBaseType.SqLite) ? true : false;

            string _typeDb = (_sqlite == false ? "[dbo]." : "");
            string _typePk = (_sqlite == false ? "BIGINT NOT NULL PRIMARY KEY IDENTITY, " : "INTEGER PRIMARY KEY, ");
            string _typeInt = (_sqlite == false ? "BIGINT NOT NULL, " : "INTEGER NOT NULL, ");

            List<(string, string)> sql = new List<(string, string)>()
            {
                ("Users", "CREATE TABLE " + _typeDb + "[Users] ( " +
                "[Id] " + _typePk +
                "[Name] VARCHAR(50) NOT NULL, " +
                "[Surname] VARCHAR(100) NULL, " +
                "[Address] VARCHAR(100) NULL, " +
                "[CountryIdentity] VARCHAR(50) NOT NULL, " +
                "CONSTRAINT [uq_CountryIdentity] UNIQUE ([CountryIdentity]), " +
                "CONSTRAINT [uq_Users_FullName] UNIQUE ([Name], [Surname])) "),

                ("Tasks", "CREATE TABLE " + _typeDb + "[Tasks] ( " +
                "[Id] " + _typePk +
                "[Name] VARCHAR(100) NOT NULL, " +
                "[Observation] VARCHAR(100) NULL, " +
                "CONSTRAINT [uq_Tasks_Name] UNIQUE ([Name])) "),

                ("Permissions", "CREATE TABLE " + _typeDb + "[Permissions] ( " +
                "[Id] " + _typePk +
                "[Parent] VARCHAR(60) NOT NULL, " +
                "[Name] VARCHAR(60) NOT NULL, " +
                "[Observation] VARCHAR(500) NULL, " +
                "CONSTRAINT [uq_Permissions_Name] UNIQUE ([Name])) "),

                ("UsersLogin", "CREATE TABLE " + _typeDb + "[UsersLogin] ( " +
                "[Id] " + _typePk +
                "[UserId] BIGINT NOT NULL UNIQUE, " +
                "[UserName] NCHAR(10) NOT NULL, " +
                "[Password] NCHAR(10) NOT NULL, " +
                "CONSTRAINT [FK_UsersLogin_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]), " +
                "CONSTRAINT [uq_UsersLogin_UserName] UNIQUE ([UserName])) "),

                ("UsersPermission", "CREATE TABLE " + _typeDb +  "[UsersPermission] ( " +
                "[Id] " + _typePk +
                "[UserId] " + _typeInt +
                "[PermissionId] " + _typeInt +
                "[userAccessType] " + _typeInt +
                "CONSTRAINT [uq_UsersPermission_UI_PI] UNIQUE([UserId], [PermissionId]), " +
                "CONSTRAINT FK_UsersPermission_Permissions FOREIGN KEY(PermissionId) " +
                "REFERENCES Permissions(Id), " +
                "CONSTRAINT FK_UsersPermission_Users FOREIGN KEY(UserId) " +
                "REFERENCES Users(Id)) "),

                ("UserTasks", "CREATE TABLE " + _typeDb + "[UserTasks] ( " +
                "[Id] " + _typePk +
                "[TaskId] " + _typeInt +
                "[UserId] " + _typeInt +
                "CONSTRAINT [uq_UserTasks_TI_UI] UNIQUE([TaskId], [UserId]), " +
                "CONSTRAINT FK_UserTasks_Tasks FOREIGN KEY(TaskId) " +
                "REFERENCES Tasks(Id), " +
                "CONSTRAINT FK_UserTasks_Users FOREIGN KEY(UserId) " +
                "REFERENCES Users(Id)) ")
            };
            return sql;
        }

        #endregion

        #region DB : SQL Server / SqLite Table data

        internal List<(string, string)> GetTableData()
        {
            List<(string, string)> data = new List<(string, string)>()
            {
                ("Users", "INSERT INTO Users(Name, Surname, Address, CountryIdentity) VALUES('admin', 'surname admin', 'address admin', 'ES-123456')"),
                ("Users", "INSERT INTO Users(Name, Surname, Address, CountryIdentity) VALUES('user1', 'surname 001', 'address 001', 'FR-123456')"),
                ("Permissions", "INSERT INTO Permissions(Parent, Name, Observation) VALUES('Maintenance', 'Users', 'Table to storage Users personal data.')"),
                ("Permissions", "INSERT INTO Permissions(Parent, Name, Observation) VALUES('Maintenance', 'UsersLogin', 'Relational table used to vinculate Users with log in data.')"),
                ("Permissions", "INSERT INTO Permissions(Parent, Name, Observation) VALUES('Maintenance', 'Permissions', 'Table to storage Permissions data.')"),
                ("Permissions", "INSERT INTO Permissions(Parent, Name, Observation) VALUES('Maintenance', 'UsersPermission', 'Relational table used to vinculate users with modules.')"),
                ("Permissions", "INSERT INTO Permissions(Parent, Name, Observation) VALUES('Maintenance', 'Tasks', 'Table to storge Tasks.')"),
                ("Permissions", "INSERT INTO Permissions(Parent, Name, Observation) VALUES('Maintenance', 'UserTasks', 'Relational table used to vinculate Users with Task.')"),
                ("Permissions", "INSERT INTO Permissions(Parent, Name, Observation) VALUES('Config', 'DAOSchemas', 'Class to get Table columns info.')"),
                ("Permissions", "INSERT INTO Permissions(Parent, Name, Observation) VALUES('Config', 'Export', 'Export Table data from database to XML.')"),
                ("Tasks", "INSERT INTO Tasks(Name, Observation) VALUES('First task', 'Task notes....1')"),
                ("Tasks", "INSERT INTO Tasks(Name, Observation) VALUES('Second task', 'Task notes....2')"),
                ("Users", "INSERT INTO UsersLogin(UserId, UserName, Password) VALUES(1, 'admin', '123')"),
                ("UsersLogin", "INSERT INTO UsersLogin(UserId, UserName, Password) VALUES(2, 'user 2', '123')"),
                ("UsersPermission", "INSERT INTO UsersPermission(UserId, PermissionId, userAccessType) VALUES(1, 1, 3)"),
                ("UsersPermission", "INSERT INTO UsersPermission(UserId, PermissionId, userAccessType) VALUES(1, 2, 3)"),
                ("UsersPermission", "INSERT INTO UsersPermission(UserId, PermissionId, userAccessType) VALUES(1, 3, 3)"),
                ("UsersPermission", "INSERT INTO UsersPermission(UserId, PermissionId, userAccessType) VALUES(1, 4, 3)"),
                ("UsersPermission", "INSERT INTO UsersPermission(UserId, PermissionId, userAccessType) VALUES(1, 5, 3)"),
                ("UsersPermission", "INSERT INTO UsersPermission(UserId, PermissionId, userAccessType) VALUES(1, 6, 3)"),
                ("UsersPermission", "INSERT INTO UsersPermission(UserId, PermissionId, userAccessType) VALUES(1, 7, 3)"),
                ("UsersPermission", "INSERT INTO UsersPermission(UserId, PermissionId, userAccessType) VALUES(2, 1, 1)"),
                ("UsersPermission", "INSERT INTO UsersPermission(UserId, PermissionId, userAccessType) VALUES(2, 2, 1)"),
                ("UsersPermission", "INSERT INTO UsersPermission(UserId, PermissionId, userAccessType) VALUES(2, 4, 1)"),
                ("UserTasks", "INSERT INTO UserTasks(TaskId, UserId) VALUES(1, 1)"),
                ("UserTasks", "INSERT INTO UserTasks(TaskId, UserId) VALUES(2, 1)"),
                ("UserTasks", "INSERT INTO UserTasks(TaskId, UserId) VALUES(1, 2)")
            };
            return data;
        }

        #endregion

    }
}
