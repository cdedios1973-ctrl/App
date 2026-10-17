using System;

namespace App.Definitions.Entity.Database
{

    // Deprecated class

    /*

    internal sealed class AppSqLiteTableData
    {
        /// <summary>
        /// Temporary installation class -> SqLite Get Tables init data.
        /// </summary>
        internal AppSqLiteTableData() 
        { }

        #region SqLite DDBB Table data

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

    */

}
