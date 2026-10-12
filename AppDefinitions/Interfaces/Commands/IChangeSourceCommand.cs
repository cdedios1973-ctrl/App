using System;

using App.Definitions.Enums;

namespace App.Definitions.Interfaces.Commands
{
    public interface IChangeSourceCommand
    {
        public DataBaseType GetDatabaseType();

    }
}
