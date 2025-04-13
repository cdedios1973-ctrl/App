using System;
using System.Data;

using App.Definitions.Filters;

// TODO : Need to read about microsoft security token
// TODO : View send 'dto' with data filter, Logic transform where query (dto from view -> logic -> entityWhere)
// TODO : Can create generic at this point for more clear code. This version trying not to use generics.

// create summary - > 

namespace App.Infraestructure.CORE.Interfaces.DAOCommands
{
    public interface IUserCommandsDAO
    {
        DataTable Select(DataFilter filter); //, ITokenCredential securityToken); // Permission needed >= 1
    }
}