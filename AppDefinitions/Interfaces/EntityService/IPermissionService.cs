using System;

using App.Definitions.DTO;

namespace App.Definitions.Interfaces.EntityService
{
    internal interface IPermissionService
    {
        bool AddPermission(PermissionDTO user);

    }
}
