using App.Definitions.DTO;
using App.Definitions.Entity;
using System;

namespace App.Definitions.Predicates
{

    public static class UsersPermissionFilters
    {
        public static Predicate<UsersPermission> ByUserPermissionId(long permissionId)
        {
            return delegate (UsersPermission item)
            {
                return item.PermissionId == permissionId;
            };
        }
    }

    public static class PermissionsFilters
    {
        public static Predicate<Permission> WithId(long id)
        {
            return delegate (Permission item)
            {
                return item.Id == id;
            };
        }

        public static Predicate<Permission> WithName(string moduleName)
        {
            return delegate (Permission item)
            {
                return item.Name == moduleName;
            };
        }
    }

    public static class PathInfoFilters
    {
        public static Predicate<PathInfo> WithKey(string key)
        {
            return delegate (PathInfo item)
            {
                return item.Key == key;
            };
        }
    }
}
