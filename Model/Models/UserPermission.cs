namespace App.Models
{
    using System;
    using App.Enums;
    using App.Interfaces;

    [Serializable()]
    public sealed class UsersPermission : IModel
    {
        private long _id = 0;
        private long _userId = 0;
        private string _userName = "";
        private long _permissionId = 0;
        private string _permissionName = "";
        private UserAccessType _accessType = UserAccessType.None;

        #region Constructors

        public UsersPermission() { }

        public UsersPermission(long id, long userId, long permissionId, UserAccessType accessType) //: this ()
        {
            this._id = id;
            this._userId = userId;
            this._permissionId = permissionId;
            this._accessType = accessType;
        }

        public UsersPermission(UsersPermission permission) : this(permission.Id, permission.UserId,
                permission.PermissionId, permission.userAccessType)
        { }
        /*
        public static UsersPermission Create(IDataRecord record)
        {
            return new UsersPermission()
            {
                Id              = (int)record["id"],
                UserId          = (int)record["userid"],
                PermissionId    = (int)record["permissionid"],
                userAccessType  = (UserAccessType)record["userAccessType"]
            };
        }
        */
        #endregion

        #region Properties

        public long Id
        {
            get { return this._id; }
            set { this._id = value; }
        }
        public long UserId
        {
            get { return this._userId; }
            set { this._userId = value; }
        }
        [DAOAttributes(DAOJoin = "UserName")]
        public string UserName
        {
            get { return this._userName; }
            set { this._userName = value; }
        }
        public long PermissionId
        {
            get { return this._permissionId; }
            set { this._permissionId = value; }
        }
        [DAOAttributes(DAOJoin = "PermissionName")]
        public string PermissionName
        {
            get { return this._permissionName; }
            set { this._permissionName = value; }
        }
        public UserAccessType userAccessType
        {
            get { return this._accessType; }
            set { this._accessType = value; }
        }

        private int userAccessTypeConversion(UserAccessType uat)
        {
            return (int)uat;
        }

        #endregion

        #region Interface implementation

        public string IGetTableName()
        {
            return "UsersPermission";
        }

        public long IGetId()
        {
            return this._id; ;
        }

        public string IGetSQLJoin()
        {
            return "SELECT UsersPermission.*, Permissions.Name as PermissionName, Users.Name as UserName " +
                "FROM UsersPermission INNER JOIN Permissions " +
                "ON UsersPermission.PermissionId=Permissions.ID " +
                "LEFT JOIN Users "+
                "ON UsersPermission.UserId = Users.ID ";
        }

        public ModuleData IGetModule()
        {
            return new ModuleData { id = this._id, name = "UsersPermission" };
        }

        public DFilter IGetFilterById(long Id)
        {
            return new DFilter("WHERE", "Id", "=", Id.ToString());
        }

        public DFilter? IGetFilterByUserId(long Id)
        {
            return new DFilter("WHERE", "UserId", "=", Id.ToString());
        }

        #endregion

    }
}
