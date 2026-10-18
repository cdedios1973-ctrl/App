using System;

namespace App.Definitions.DTO
{
    [Serializable()]
    internal sealed class UserPermissionsDTO
    {
        private readonly long _id = 0;
        private readonly long _userId = 0;
        private readonly string _userName;
        private readonly long _permissionId = 0;
        private readonly string _permissionName;
        private readonly int _accessType = 0;
        //private readonly UserAccessType _accessType = UserAccessType.None;

        #region Constructors
        internal UserPermissionsDTO(long id, long userId, long permissionId, string userName, string permissionName, int accessType)
        {
            this._id = id;
            this._userId = userId;
            this._permissionId = permissionId;
            this._accessType = accessType;
            this._userName = userName;
            this._permissionName = permissionName;
        }

        #endregion

        #region Properties
        public long Id
        {
            get { return this._id; }
        }
        public long UserId
        {
            get { return this._userId; }
        }
        public long PermissionId
        {
            get { return this._permissionId; }
        }
        public string UserName
        {
            get { return this._userName; }
        }
        public string PermissionName
        {
            get { return this._permissionName; }
        }
        public int userAccessType
        {
            get { return this._accessType; }
        }

        #endregion

    }
}
