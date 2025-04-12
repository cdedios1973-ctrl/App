namespace App.Logic
{
    using System;
    using App.Models;
    using App.Enums;
    using App.Predicate;
    using App.DAO;

    // Class: Verifiy user permission over the application. Filled when DAOLogin
    public sealed class UserAccess
    {
        private const string PERMISSION_NOT_GRANTED = "Current user has no access to this module.";

        private static Users _user;
        private static List<Permissions> _permissions;
        private static List<UsersPermission> _userPermissions = new List<UsersPermission>();
        private static string _userFullName;
        private static DataBaseType _dbType;
        private static DisplaType _display;

        #region Constructors

        public UserAccess(DataBaseType DataBaseType, DisplaType display) 
        {
            _dbType = DataBaseType;
            _display = display;
            DataFilter filter = new DataFilter();
            if (_permissions == null)
            {
                UserAccess tmpuser = this;
                _permissions = new DAOData<Permissions>(tmpuser, ref filter).GetData;
            }
        }

        public UserAccess(ref Users user, ref List<UsersPermission> userPermissions)
        {
            if(_user == null)
                _user = user;

            if (_userPermissions.Count == 0)
                _userPermissions = userPermissions;

        }

        #endregion

        #region Properties

        public bool IsUserAuthenticated
        {
            get { return (_user != null && _userPermissions != null) ? true : false; }
            private set { }
        }

        public Users GetUser
        {
            get { return _user; }
        }

        public long GetUserId
        {
            get { return _user.Id; }
        }

        public List<Permissions> GetPermissions
        {
            get { return _permissions; }
        }

        public List<UsersPermission> GetUserPermissions
        { 
            get { return _userPermissions; }
        }

        public DisplaType Display
        {
            get { return _display; }
            set { _display = value; }
        }

        public DataBaseType DDBBType
        { 
            get { return _dbType; }
            set { _dbType = value; }
        }

        public string GetUserFullName
        {
            get
            {
                if (_userFullName == null && _user != null)
                    _userFullName = "{" + _user.Id + "|" +
                            _user.Surname + ", " + _user.Name + "}";

                return _userFullName;
            }
        }

        public bool Logout
        {
            get 
            {
                _user = null;
                _permissions = null;
                _userFullName = null;
                _userPermissions = new List<UsersPermission>();
                return true;
            }
        }

        #endregion

        #region Function to return what sort of access has to the input module

        public UserAccessType checkUserPermission(string moduleName)
        {
            // Temporal permissions needed to login (no permissions at start).
            UserAccessType ua = UserAccessType.None;
            try
            {
                if (IsUserAuthenticated == false)
                {
                    if (moduleName == "UsersLogin" ||
                        moduleName == "Users" ||
                        moduleName == "UsersPermission" ||
                        moduleName == "Permissions")
                    {
                        ua = UserAccessType.Read;
                    }
                }
                else 
                {
                    Permissions per = _permissions.Find(Predicates.ByPermissionName(moduleName));
                    if (per != null)
                    {
                        try
                        { ua = (UserAccessType)(int)_userPermissions.Find(Predicates.ByUserPermissionId(per.Id)).userAccessType; }
                        catch 
                        { ua = UserAccessType.None; }
                    }
                }
            }
            catch
            {
                throw new Exception(moduleName + PERMISSION_NOT_GRANTED);
            }
            return ua;
        }

        public UserAccessType checkUserPermissionById(long id)
        {
            UserAccessType ua = UserAccessType.None;
            try
            {
                if (_user.Id > 0)
                    ua = (UserAccessType)(int)_userPermissions.Find(Predicates.ByUserPermissionId(id)).userAccessType;
                else
                    throw new Exception();
            }
            catch
            {
                throw new Exception(PERMISSION_NOT_GRANTED);
            }
            return ua;
        }

        #endregion

    }
}