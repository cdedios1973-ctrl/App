using System;

namespace App.Definitions.Enums
{

    //public enum DataActions { Insert, Update, Delete, Select }; // removed -> orquestator logic service
    //public enum DatabaseConnection { Local, Remote }; // removed, this app use local database focus
    //public enum SqlServerMaster { Yes, No } // removed, SqlServer always ha 2 files .df / .ldf
    //public enum DatabaseTransaction { Yes, No }; // actually all is transactional, no chance to change.
    //public enum SQLClausule { WHERE, HAVING }; // TODO : Removed, Complex and hard to maintain
    //public enum SQLSyntax { AND, OR }; // TODO : Removed, Complex and hard to maintain
    public enum DataBaseType { SqlServer, SqLite }; // TODO : Actually writing small service to get connection string from app.config (could change to json)
    public enum DisplaType { Console, Mobile, Desktop }; // TODO : Remove, can use JSON controller->service approach.
    public enum DrawAppMenu { LogIn, Commands, DDBB, Users, Help, All, None }; // TODO : This actions are events from views trought controller. (Interfaces)
    public enum UserAccessType : long { None = 0, Read = 1, Write = 2, All = 3 }; // Keep.
    public enum MenuAction { Display, DDBB, Export }; // TODO : Remove, This actions are events from views trought controller. (Interfaces)
    
    public struct ModuleData
    {
        public long id { get; set; }
        public string name { get; set; }
    };

    // TODO : Deprecated -> If i really need this create service.
    public struct ColumnsConfig
    {
        public string Header;
        public Dictionary<string, (int Value1, int Value2)> Column;
    }
    // TODO : Deprecated -> Its a element from a List<T>
    public struct Menu
    {
        public string MenuId { get; set; }
        public string MenuName { get; set; }
    };

    /*
    // Deprecated : public struct DDBBParams
    public struct DDBBParams
    {
        public UsersPermission usersPermission; -> convert to small service 
        public DataFilter filter;               -> complex to apply and maintain
        public DataActions dataAction;          -> implemented by the DTO flow
        public long dataId;                     -> delegate to service orquestator
        public string[] data;                   -> Not going to use "listener" with commands (maybe)
    };
    */
}
