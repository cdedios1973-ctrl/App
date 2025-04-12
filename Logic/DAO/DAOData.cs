namespace App.DAO
{
    using System;
    using System.Linq;
    using App.Enums;
    using App.Models;
    using App.Interfaces;
    using App.Logic;
    using App.DAO.Source;

    //TODO: Create different source types (Database, SOAP, File, XML, etc)
    //TODO: insertData() Method -> Modify DBConnector to insert data list (with transaction), Actually: one by one
    //TODO: Create update method.

    public class DAOData<T> where T : IModel
    {

        private static UserAccess _userAccess;
        private DataFilter _filter = new DataFilter();
        private IModel _dataObject;

        #region Constructors

        private DAOData() { }

        public DAOData(UserAccess access)
        {
            _userAccess = access;
            this._dataObject = (T)Activator.CreateInstance(typeof(T));
        }

        public DAOData(UserAccess userAccess, ref DataFilter filter) : this(userAccess)
        {
            this._filter = filter;
        }

        #endregion

        #region Properties

        private UserAccessType CheckUserPermission 
        { 
            get { return _userAccess.checkUserPermission(this._dataObject.IGetModule().name); } 
        }

        #endregion

        #region Command Handler : Select, Insert, Update, Delete

        public int dataActions(List<T> data, DataActions action)
        {
            int i = 0;
            switch (action)
            {
                case DataActions.Insert:
                    i = this.insertData(data);
                    break;
                case DataActions.Update:
                    i = this.updateData(ref data);
                    break;
                case DataActions.Delete:
                    i = this.deleteData(data[0]);
                    break;
            }
            return i;
        }

        #endregion

        #region Command => Select : Return List of elements
        public List<T> GetData
        {
            get 
            {
                List<T> data = new List<T>();
                try
                {
                    if (CheckUserPermission >= UserAccessType.Read)
                    {
                        switch (_userAccess.DDBBType)
                        {
                            case DataBaseType.SqLite:
                            case DataBaseType.SqlServer:
                                data = new DAODB<T>(ref _userAccess, (T)this._dataObject).getData(this._filter);
                                break;
                            case DataBaseType.XML:
                                data = new DAOXml<T>(_userAccess).ReadTableDataXML();
                                break;
                        }
                    }
                    else
                    { throw new Exception("User has no permission to read data."); }
                }
                finally { }
                return data;
            }
        }

        #endregion

        //TODO: insertData() Method -> Modify DBConnector to insert data list (with transaction), Actually: one by one
        #region Command => Insert : Return number of inserted records
        private int insertData(List<T> data)
        {
            int i = 0;
            if (CheckUserPermission >= UserAccessType.Write)
            {
                switch (_userAccess.DDBBType)
                {
                    case DataBaseType.SqLite:
                    case DataBaseType.SqlServer:
                        if (data.Count == 1)
                            i = new DAODB<T>(ref _userAccess, data[0]).Insert;
                        else
                        {
                            int j = 0;
                            //Modify DBConnector to insert data list (with transaction)
                            foreach (T obj in data)
                            {
                                j = new DAODB<T>(ref _userAccess, obj).Insert;
                                i = i + j;
                            }
                        }
                        break;
                    case DataBaseType.XML:
                        throw new Exception("Method not implemented.");
                        //break;
                }
            }
            else
            { throw new Exception("User has no permission to insert data."); } 

            return i;
        }

        #endregion

        #region TODO: Command => Update : 

        private int updateData(ref List<T> data)
        {
            throw new Exception("Update Method not implemented");
            return 0;
        }

        #endregion

        #region Command => Delete : Return number of deleted records
        private int deleteData(T data)
        {
            int i = 0;
            if (CheckUserPermission >= UserAccessType.Write)
            {
                switch (_userAccess.DDBBType)
                {
                    case DataBaseType.SqLite:
                    case DataBaseType.SqlServer:
                        i = new DAODB<T>(ref _userAccess, data).DeleteById;
                        break;
                    case DataBaseType.XML:
                        throw new Exception("Method not implemented.");
                        //break;
                }
            }
            else
            { throw new Exception("User has no permission to delete data."); }
            return i;
        }

        #endregion
   
    }
}
