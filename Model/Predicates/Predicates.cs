namespace App.Predicate
{
    using System;
    //using App.Interfaces;
    using App.Models;
    
    /*
    public static class PredicatesT<T> where T : IModel
    {
        public static Predicate<T> ByUserPermissionId(long permissionId, Type fieldName)
        {
            return delegate (T item)
            {
                // TODO: search the property and look for it.
                return item.PermissionId == permissionId;
            };
        }


    }
    */

    public static class Predicates
    {
        public static Predicate<UsersPermission> ByUserPermissionId(long permissionId)
        {
            return delegate (UsersPermission item)
            { 
                return item.PermissionId == permissionId; 
            };
        }

        public static Predicate<UsersPermission> ByUserPermissionName(string moduleName)
        {
            return delegate (UsersPermission item)
            {
                return item.IGetTableName() == moduleName;
            };
        }

        public static Predicate<Permissions> ByPermissionId(long id)
        {
            return delegate (Permissions item)
            {
                return item.Id == id;
            };
        }
        public static Predicate<Permissions> ByPermissionName(string moduleName)
        {
            return delegate (Permissions item)
            {
                return item.Name == moduleName;
            };
        }


        /*
          
        internal static Predicate<UserPermission> ByModuleId(int moduleId)
        {
            return delegate(UserPermission item)
            { return item.Id == moduleId; };
        }      
         
        public static Predicate<Table> ByTableName(String name)
        {
            return delegate(Table item)
            { return item.Name == name; };
        }

        public static class PredicatesT<T> where T : IDbClass
        {
            public static Predicate<T> ByName(String name)
            {
                return delegate(T item)
                { return item.GetName().StartsWith(name, StringComparison.OrdinalIgnoreCase); };
            }

            public static Predicate<T> ById(long id)
            {
                return delegate(T item)
                { return item.GetId() == id; };
            }
        }
        */
    }
}
