using System;

using App.Definitions.Entity;




namespace App.Logic.Predicate
{

    internal static class Predicates
    {
        public static Predicate<UsersPermission> ByUserPermissionId(long permissionId)
        {
            return delegate (UsersPermission item)
            { 
                return item.PermissionId == permissionId; 
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

    }
}
