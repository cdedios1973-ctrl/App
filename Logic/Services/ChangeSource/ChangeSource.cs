using System;
using App.Definitions.Enums;
using App.Definitions.Interfaces;

namespace App.Logic.Services
{

    // Change Source from user Input => Create Services/Input/ChangeSourceService.cs

    internal static class ChangeSource
    {

        #region Change Source => Return DataBaseType (by User Input Action)

        internal static DataBaseType SetSource(IChangeSourceCommand command)
        {
            DataBaseType _type = DataBaseType.SqLite;
            if (command != null)
            {
                switch (command.GetTargetDatabase())
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
