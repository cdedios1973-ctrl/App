using System;
using System.IO;

using App.Infraestructure.CORE.Interfaces.Path;

namespace App.Infraestructure.CORE
{

    /// <summary>
    /// Return 'base' directory of the Application configuration file
    /// This class has its own pourpose, look at this directory.
    /// Open to modification, If i need to search elsewhere i can make new class and use this interface.
    /// </summary>

    internal sealed class DefaultAppConfigPath : IAppPathProvider
    {
        private readonly string _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private readonly string _assemblyName = AppDomain.CurrentDomain.FriendlyName;
        private readonly string _extension;

        #region Constructors

        internal DefaultAppConfigPath(string extension = ".config")
        {
            if (extension == "")
            {
                throw new ArgumentNullException("Extension cannot be empty or null" + nameof(extension));
            }
            this._extension = extension;
        }

        #endregion

        #region Interface Implementation

        public string GetPath()
        {
            string fullpath = Path.Combine(_baseDirectory, $"{_assemblyName}" + this._extension);

            if (!File.Exists(fullpath))
            {
                throw new FileNotFoundException(fullpath);
            }
            return fullpath;
        }

        #endregion

    }
}
