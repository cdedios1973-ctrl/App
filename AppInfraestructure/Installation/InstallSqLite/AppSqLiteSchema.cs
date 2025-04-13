using System;

using Infraestructure.CORE.Interfaces.Path;

namespace App.Infraestructure.Installation.Database
{
    /// <summary>
    /// Temporary installation class -> SqLite Get Database and Table definitions to create.
    /// </summary>
    internal sealed class AppSqLiteSchema : IAppDatabaseInstallation
    {
        private IAppPathProvider _pathProvider;

        #region Constructor

        internal AppSqLiteSchema(IAppPathProvider pathProvider) 
        {
            if(pathProvider == null) 
                throw new ArgumentNullException("Incorrect path");
        }

        #endregion

        #region SQLite DDBB Schema

        public string GetDatabaseProps()
        {
            string filename = this._pathProvider.GetPath();

            string str = "CREATE DATABASE AppDb ON PRIMARY " +
                "(NAME = AppDb_Data, " +
                "FILENAME = '" + filename + ".mdf', " +
                "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%)" +
                "LOG ON (NAME = AppDb_Log, " +
                "FILENAME = '" + filename + ".ldf', " +
                "SIZE = 1MB, " +
                "MAXSIZE = 5MB, " +
                "FILEGROWTH = 10%)";
            return str;
        }

        #endregion

        #region SqLite DDBB Table schemas

        public List<(string, string)> GetTableSchemas()
        {
            List<(string, string)> sql = new List<(string, string)>()
            {
                ("Users", "CREATE TABLE [dbo].[Users] ( " +
                "[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, " +
                "[Name] VARCHAR(50) NOT NULL, " +
                "[Surname] VARCHAR(100) NULL, " +
                "[Address] VARCHAR(100) NULL, " +
                "[CountryIdentity] VARCHAR(50) NOT NULL, " +
                "CONSTRAINT [uq_CountryIdentity] UNIQUE ([CountryIdentity]), " +
                "CONSTRAINT [uq_Users_FullName] UNIQUE ([Name], [Surname])) "),

                ("Tasks", "CREATE TABLE [dbo].[Tasks] ( " +
                "[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, " +
                "[Name] VARCHAR(100) NOT NULL, " +
                "[Observation] VARCHAR(100) NULL, " +
                "CONSTRAINT [uq_Tasks_Name] UNIQUE ([Name])) "),

                ("Permissions", "CREATE TABLE [dbo].[Permissions] ( " +
                "[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, " +
                "[Parent] VARCHAR(60) NOT NULL, " +
                "[Name] VARCHAR(60) NOT NULL, " +
                "[Observation] VARCHAR(500) NULL, " +
                "CONSTRAINT [uq_Permissions_Name] UNIQUE ([Name])) "),

                ("UsersLogin", "CREATE TABLE [dbo].[UsersLogin] ( " +
                "[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, " +
                "[UserId] BIGINT NOT NULL UNIQUE, " +
                "[UserName] NCHAR(10) NOT NULL, " +
                "[Password] NCHAR(10) NOT NULL, " +
                "CONSTRAINT [FK_UsersLogin_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]), " +
                "CONSTRAINT [uq_UsersLogin_UserName] UNIQUE ([UserName])) "),

                ("UsersPermission", "CREATE TABLE [dbo].[UsersPermission] ( " +
                "[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, " +
                "[UserId] BIGINT NOT NULL, " +
                "[PermissionId] BIGINT NOT NULL, " +
                "[userAccessType] BIGINT NOT NULL, " +
                "CONSTRAINT [uq_UsersPermission_UI_PI] UNIQUE([UserId], [PermissionId]), " +
                "CONSTRAINT FK_UsersPermission_Permissions FOREIGN KEY(PermissionId) " +
                "REFERENCES Permissions(Id), " +
                "CONSTRAINT FK_UsersPermission_Users FOREIGN KEY(UserId) " +
                "REFERENCES Users(Id)) "),

                ("UserTasks", "CREATE TABLE [dbo].[UserTasks] ( " +
                "[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, " +
                "[TaskId] BIGINT NOT NULL, " +
                "[UserId] BIGINT NOT NULL, " +
                "CONSTRAINT [uq_UserTasks_TI_UI] UNIQUE([TaskId], [UserId]), " +
                "CONSTRAINT FK_UserTasks_Tasks FOREIGN KEY(TaskId) " +
                "REFERENCES Tasks(Id), " +
                "CONSTRAINT FK_UserTasks_Users FOREIGN KEY(UserId) " +
                "REFERENCES Users(Id)) ")
            };
            return sql;
        }

        #endregion

    }
}
