namespace App.DAO
{
    using System;
    using System.Xml.Serialization;
    using App.Enums;
    using App.Interfaces;
    using App.Logic;
    using App.Models;

    // TODO: Reading XML as main database -> need to open 'Users' for login.

    public sealed class DAOXml<T> where T : IModel
    {
        private const string MISSING_TABLENAME  = "DAOXml: Must select a table before exporting data. ";
        private const string WRONG_SOURCE       = "DAOXml: Can't export data if source isn't a database. ";
        private const string XML_READ_ERROR     = "DAOXml: Unexpected error while reading Xml file. ";
        private const string XML_WRITE_ERROR    = "DAOXml: User not allowed to write data (or nothing to write).";
        private const string XML_FOLDER         = "DAOXml: Unable to find Xml's folder.";
        private const string USER_NOT_LOGGED    = "DAOXml: User not logged in the application.";
        private const string USER_PERMISSION    = "DAOXml: This user has no permission to this module.";

        private string          _FILE = "";
        private string          _FOLDER = "";
        private const string    PATH = @"Database\XML";
        private UserAccess      _userAccess;
        private UserAccessType  _userAccessType = UserAccessType.None;

        #region Constructors

        public DAOXml(UserAccess user)
        {
            //if (user.IsUserAuthenticated == false)
            //    throw new Exception(USER_NOT_LOGGED);

            IModel obj = (T)Activator.CreateInstance(typeof(T));
            this._userAccessType = user.checkUserPermission(obj.IGetTableName());

            this._userAccess = user;
            if (this._userAccessType < UserAccessType.Read)
                throw new Exception(USER_PERMISSION);

            this._FOLDER = Path.Combine(AppContext.BaseDirectory, PATH);
            this._FILE = Path.Combine(this._FOLDER, obj.IGetTableName() + ".xml");
            if (this.CreateDirectory == false)
                throw new Exception(XML_FOLDER);
        }

        #endregion

        #region Properties

        private bool DeleteFile
        {
            get
            {
                try
                {
                    if (File.Exists(this._FILE))
                        File.Delete(this._FILE);
                }
                catch 
                { throw new Exception("DAOStream: File '" + this._FILE + "' couldn't be deleted."); }

                return true;
            }
        }

        private bool CreateDirectory
        {
            get
            {
                if (Directory.Exists(this._FOLDER) == false)
                    Directory.CreateDirectory(this._FOLDER);
                return true;
            }
        }

        #endregion

        // TODO : Apply filters...
        #region Method ReadTableDataXML => Return Deserialized Data List from XML File

        public List<T> ReadTableDataXML()
        {
            List<T> deserializedList = new List<T>();
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                using (FileStream stream = File.OpenRead(this._FILE))
                {
                    deserializedList = (List<T>)serializer.Deserialize(stream);
                    stream.Close();
                }
            }
            catch (Exception ex)
            { 
                throw new Exception(XML_READ_ERROR + ex.Message); 
            }
            return deserializedList;
        }

        #endregion

        #region Method ExportToXML => Write / Overwrite Serialized Data List to XML File

        public string ExportXML(ref UsersPermission permission, DataFilter filter = null)
        {
            if (_userAccess.IsUserAuthenticated == false)
                throw new Exception(USER_NOT_LOGGED);

            if (permission.PermissionName == "")
                throw new Exception(MISSING_TABLENAME);

            if ((_userAccess.DDBBType != DataBaseType.SqlServer && _userAccess.DDBBType != DataBaseType.SqLite))
                throw new Exception(WRONG_SOURCE);

            DataFilter flt = (filter == null) ? new DataFilter() : flt = filter;

            List<T> list = new DAOData<T>(_userAccess, ref flt).GetData;

            string msg = ExportToXML(list) ? "Success" : "Error";
            msg += " exporting data from " + permission.PermissionName;

            return msg;
        }

        private bool ExportToXML(List<T> data)
        {
            bool ok = true;
            try
            {
                if (this._userAccessType >= UserAccessType.Write && data.Count > 0)
                {
                    if (this.DeleteFile)
                    {
                        FileInfo file = new FileInfo(this._FILE);
                        StreamWriter sw = file.AppendText();

                        var writer = new XmlSerializer(typeof(List<T>));
                        writer.Serialize(sw, data);
                        sw.Close();
                    }
                }
                else
                    throw new Exception(XML_WRITE_ERROR);
            }
            catch (Exception ex)
            { 
                throw new Exception(ex.Message); 
            }
            return ok;
        }

        #endregion

    }
}