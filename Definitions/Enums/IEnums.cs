using System;

namespace App.Definitions.Enums
{

    //public enum DataActions { Insert, Update, Delete, Select };
    //public enum DatabaseConnection { Local, Remote };
    //public enum SqlServerMaster { Yes, No }
    //public enum DatabaseTransaction { Yes, No };
    public enum SQLClausule { WHERE, HAVING }; // TODO : Removing...
    public enum SQLSyntax { AND, OR }; // TODO : Removing...
    public enum DataBaseType { SqlServer, SqLite };
    public enum DisplaType { Console, Mobile, Desktop };
    public enum DrawAppMenu { LogIn, Commands, DDBB, Users, Help, All, None };
    public enum UserAccessType : long { None = 0, Read = 1, Write = 2, All = 3 };
    public enum MenuAction { Display, DDBB, Export };
    public struct ModuleData
    {
        public long id { get; set; }
        public string name { get; set; }
    };
    public struct ColumnsConfig
    {
        public string Header;
        public Dictionary<string, (int Value1, int Value2)> Column;
    }
    public struct Menu
    {
        public string MenuId { get; set; }
        public string MenuName { get; set; }
    };

    /*
    public struct DDBBParams
    {
        public UsersPermission usersPermission;
        public DataFilter filter;
        public DataActions dataAction;
        public long dataId;
        public string[] data;
    };
    */
}
