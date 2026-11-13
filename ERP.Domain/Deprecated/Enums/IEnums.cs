// Deprecated

//using System;

//namespace App.Definitions.Enums;

// TODO: Falta sustituir token Json de seguridad de microsoft
//public enum UserAccessType : long { None = 0, Read = 1, Write = 2, All = 3 }; 

// DONE : Eliminar, Solo una base de datos (cambiar a archivo de configuracion json)
//public enum DataBaseType { SqlServer, SqLite }; 

// DONE : Eliminar, Llamar a los servicios pertinentes por DTO / Entidad
//public enum DataActions { Insert, Update, Delete, Select }; 

// DONE : Eliminar, Enfocarse en bases de datos local
//public enum DatabaseConnection { Local, Remote };

// DONE : Eliminar, SqlServer siempre tiene dos archivos .mdf / .ldf
//public enum SqlServerMaster { Yes, No } 

// DONE : Todo transaccional, sin opcion de cambio.
//public enum DatabaseTransaction { Yes, No }; 

// DONE : Eliminar, Complejo y dificil de mantener
//public enum SQLClausule { WHERE, HAVING };

// DONE : Eliminar, Complejo y dificil de mantener
//public enum SQLSyntax { AND, OR }; 

// DONE : Eliminar, Se puede usar JSON controller -> Aproximacion a Sercicios.
//public enum DisplaType { Console, Mobile, Desktop }; 

// DONE : Eliminar, Estas acciones son eventos enviados desde la vista a traves del controlador. (Interfaces)
//public enum DrawAppMenu { LogIn, Commands, DDBB, Users, Help, All, None }; 

// DONE : Eliminar, Estas acciones son eventos enviados desde la vista a traves del controlador. (Interfaces)
//public enum MenuAction { Display, DDBB, Export }; 

// Deprecated -> Si lo necesitara crearia un servicio.
/*
public struct ColumnsConfig
{
    public string Header;
    public Dictionary<string, (int Value1, int Value2)> Column;
}
*/

// Deprecated -> Es una lista de dtos
/*
public struct ModuleData
{
    public long id { get; set; }
    public string name { get; set; }
};
*/

// Deprecated -> Seria un elemento de una lista (List<Menu>)
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
    public UsersPermission usersPermission; -> Convertir a Servicio (ya no es necesario) 
    public DataFilter filter;               -> Complejo de aplicar y mantener.
    public DataActions dataAction;          -> Implementar por el flujo del DTO
    public long dataId;                     -> Delegar a servicio
    public string[] data;                   -> No voy a usar "listener" con comandos.
};
*/
