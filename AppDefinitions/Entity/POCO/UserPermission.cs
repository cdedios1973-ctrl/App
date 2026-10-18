using System;
using App.Definitions.Enums;

namespace App.Definitions.Entity
{
    public sealed class UsersPermission
    {
        private readonly long _id = 0;
        private readonly long _userId = 0;
        private readonly long _permissionId = 0;
        private readonly UserAccessType _accessType = UserAccessType.None;

        #region Constructors

        public UsersPermission(long id, long userId, long permissionId, UserAccessType accessType)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid user Id");
            }
            if (permissionId <= 0)
            {
                throw new ArgumentException("Invalid Permission Id");
            }

            this._id = id;
            this._userId = userId;
            this._permissionId = permissionId;
            this._accessType = accessType;
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

        public UserAccessType userAccessType
        {
            get { return this._accessType; }
            private set { }
        }

        #endregion

    }
}
