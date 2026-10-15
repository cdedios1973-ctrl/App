using System;

namespace App.View.Models
{
    [Serializable()]
    internal sealed class UserTasksDTO
    {
        private readonly long _id = 0;
        private readonly long _userId = 0;
        private readonly long _taskId = 0;
        private readonly string _taskName;
        private readonly string _taskObservation;

        #region Constructors
        public UserTasksDTO(long id, long userId, long taskId, string taskName, string taskObservation)
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
            this._taskName = taskName;
            this._taskObservation = taskObservation;
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
        public string TaskName
        {
            get { return this._taskName; }
            private set { }
        }
        public string TaskObservation
        {
            get { return this._taskObservation; }
            private set { }
        }

        #endregion

    }
}