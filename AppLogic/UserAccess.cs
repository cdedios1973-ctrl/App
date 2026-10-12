using System;

using App.Definitions.Enums;
using App.Definitions.Interfaces;
using App.Logic.Entity;
using App.Logic.Predicate;

namespace App.Logic.Security
{
    // TODO : move this class to CORE (Infra)
    // TODO : need a valid token from Servicelogin to fill this class... need to implement inteface
    // TODO : pass this class by ref to LoginService (if ok fill)
    // Class: Verifiy user permission over the application. Filled when ServiceLogin
    public sealed class UserAccess : ITokenCredential, ISecurityPermissions
    {
        private const string PERMISSION_NOT_GRANTED = "Current user has no access to this module.";

        // User related : User and UserPermissions Info
        private User _user;
        private List<UsersPermission> _userPermissions = new List<UsersPermission>();

        // Module Permissions related: List of module permissions.
        private List<Permissions> _permissions;

        #region Constructors

        internal UserAccess() 
        { 
            // TODO : Need to read from permissions table
            // call PermissionsService() to fill list, and only once, if any permise is revoke, user must logout -> isssue: user will have permissions extra time.. mistake..
            // looking for alternatives....
        }

        internal UserAccess(ITokenCredential token, User user, List<UsersPermission> userPermissions)
        {
            if (!token.TokenExists())
                throw new ArgumentException("Invalid User");
            
            this._user = user;
            this._userPermissions = userPermissions;
        }

        #endregion

        #region Properties

        public bool IsUserAuthenticated
        {
            get { return (this._user.Id > 0 && _userPermissions.Count > 0) ? true : false; }
        }

        public long UserId
        {
            get { return this._user.Id; }
        }

        #endregion

        #region Function to return what sort of access has to the input module

        public UserAccessType CheckUserPermission(string moduleName)
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
                    Permissions per = this._permissions.Find(Predicates.ByPermissionName(moduleName));
                    if (per != null)
                    {
                        try
                        { 
                            ua = (UserAccessType)(int)_userPermissions.Find(Predicates.ByUserPermissionId(per.Id)).userAccessType; 
                        }
                        catch 
                        { 
                            ua = UserAccessType.None; 
                        }
                    }
                }
            }
            catch
            {
                throw new Exception(moduleName + PERMISSION_NOT_GRANTED);
            }
            return ua;
        }

        public UserAccessType CheckUserPermissionById(long id)
        {
            UserAccessType ua = UserAccessType.None;
            try
            {
                if (this.UserId > 0)
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

        public bool TokenExists()
        {
            return (this._user.Id > 0 && _userPermissions.Count > 0) ? true : false;
        }

        #endregion

    }
}