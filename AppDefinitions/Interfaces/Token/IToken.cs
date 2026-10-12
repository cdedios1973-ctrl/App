using System;

using App.Definitions.Enums;

namespace App.Definitions.Interfaces
{
    public interface ISecurityPermissions
    {
        public UserAccessType CheckUserPermissionById(long id);
        public UserAccessType CheckUserPermission(string moduleName);

    }

    public interface ITokenCredential
    {
        bool TokenExists();

    }
}
