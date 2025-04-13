using System;

namespace AppConsole.Interfaces
{
    internal interface IConsole
    {
        internal void Write(string text);
        internal string Read();

    }
}
