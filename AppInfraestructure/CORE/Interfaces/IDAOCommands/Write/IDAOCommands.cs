using System;

// TODO : Can create generic at this point for more clear code. This version trying to avoid to use generics.

namespace App.Infraestructure.CORE.Interfaces.DAOCommands
{
    public interface IDAOCommands
    {
        int Insert(); // Permission needed >= 2
        int Update(); // Permission needed >= 2
        int Delete(); // Permission needed >= 2
    }

}