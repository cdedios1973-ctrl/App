using System;

namespace App.Definitions.Enums
{

    public enum UserAccessType : long { None = 0, Read = 1, Write = 2, All = 3 }; // Lo puedo mantener de momento, pondre el token Json de seguridad de microsoft

    // DONE : Rmove, only 1 database (could change to json config file)
    //public enum DataBaseType { SqlServer, SqLite }; 

    // DONE : Remove, orquestator logic service
    //public enum DataActions { Insert, Update, Delete, Select }; 

    // DONE : Remove, this app use local database focus
    //public enum DatabaseConnection { Local, Remote };

    // DONE : Remove, SqlServer always ha 2 files .df / .ldf
    //public enum SqlServerMaster { Yes, No } 

    // DONE : actually all is transactional, no chance to change.
    //public enum DatabaseTransaction { Yes, No }; 

    // DONE : Remove, Complex and hard to maintain
    //public enum SQLClausule { WHERE, HAVING };

    // DONE : Remove, Complex and hard to maintain
    //public enum SQLSyntax { AND, OR }; 

    // DONE : Remove, can use JSON controller -> service approach.
    //public enum DisplaType { Console, Mobile, Desktop }; 

    // DONE : This actions are events from views trought controller. (Interfaces)
    //public enum DrawAppMenu { LogIn, Commands, DDBB, Users, Help, All, None }; 

    // DONE : Remove, This actions are events from views trought controller. (Interfaces)
    //public enum MenuAction { Display, DDBB, Export }; 

    // Deprecated -> If i really need this create service.
    /*
    public struct ColumnsConfig
    {
        public string Header;
        public Dictionary<string, (int Value1, int Value2)> Column;
    }
    */

    // Deprecated -> Its a List DTO
    /*
    public struct ModuleData
    {
        public long id { get; set; }
        public string name { get; set; }
    };
    */

    // Deprecated -> Its a element from a List<T>
    /*
    public struct Menu
    {
        public string MenuId { get; set; }
        public string MenuName { get; set; }
    };
    */

    // Deprecated : struct DDBBParams
    /*
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
