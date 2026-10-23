using System;

namespace App.Definitions.DTO
{
    public sealed class PathInfo
    {
        private readonly string _key;
        private readonly string _fileName;
        private readonly string _path;
        private readonly string _type;

        #region Constructors

        public PathInfo(string key, string fileName, string path, string type)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (fileName == null) 
                throw new ArgumentNullException(nameof(fileName));
            if(path == null) 
                throw new ArgumentNullException(nameof(path));
            if(type == null)
                throw new ArgumentNullException(nameof(type));

            this._key       = key;
            this._fileName  = fileName;
            this._path      = path;
            this._type      = type;
        }

        #endregion

        #region Properties
        public string Key
        {
            get { return this._key; }
        }
        public string FileName 
        { 
            get { return this._fileName; } 
        }
        public string Path 
        { 
            get { return this._path; } 
        }
        public string Type 
        { 
            get { return this._type; } 
        }

        #endregion

    }
}
