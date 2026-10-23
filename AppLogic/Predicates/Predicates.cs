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

        public static Predicate<Permission> ByPermissionId(long id)
        {
            return delegate (Permission item)
            {
                return item.Id == id;
            };
        }

        public static Predicate<Permission> ByPermissionName(string moduleName)
        {
            return delegate (Permission item)
            {
                return item.Name == moduleName;
            };
        }

    }
}
