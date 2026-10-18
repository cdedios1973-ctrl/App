using System;

namespace App.Definitions.Entity
{
    public sealed class UserTasks
    {
        private readonly long _id = 0;
        private readonly long _userId = 0;
        private readonly long _taskId = 0;

        #region Constructors

        public UserTasks(long id, long userId, long taskId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid User Id");
            }
            if (taskId <= 0)
            {
                throw new ArgumentException("Invalid Task Id");
            }

            this._id = id;
            this._userId = userId;
            this._taskId = taskId;
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
        public long TaskId
        {
            get { return this._taskId; }
            private set { }
        }

        #endregion

    }
}