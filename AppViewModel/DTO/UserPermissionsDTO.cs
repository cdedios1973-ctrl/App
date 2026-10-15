using System;

// TODO : DONE : change UserAccessType to Int or smallint, check permission only need numbers

namespace App.View.Models
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
            private set { }
        }
        public long UserId
        {
            get { return this._userId; }
            private set { }
        }
        public long PermissionId
        {
            get { return this._permissionId; }
            private set { }
        }
        public string UserName
        {
            get { return this._userName; }
            private set { }
        }
        public string PermissionName
        {
            get { return this._permissionName; }
            private set { }
        }
        public int userAccessType
        {
            get { return this._accessType; }
            private set { }
        }

        #endregion

    }
}
