using System;
using App.Definitions.Enums;
using App.Definitions.Interfaces.Commands;

namespace App.Logic.Services
{

    internal sealed class ChangeSource
    {

        #region Change Source => Return DataBaseType (by User Input Action)

        internal DataBaseType SetSource(IChangeSourceCommand command)
        {
            DataBaseType _type = DataBaseType.SqLite;
            if (command != null)
            {
                switch (command.GetDatabaseType())
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        type = DataBaseType.SqlServer;
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        type = DataBaseType.SqLite;
                        break;
                    //case ConsoleKey.D3:
                    //case ConsoleKey.NumPad3:
                    //    type = DataBaseType.XML;
                    //    break;
                }
            }
            return _type;
        }

        #endregion

    }
}
