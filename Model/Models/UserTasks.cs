namespace App.Models
{
    using App.Enums;
    using App.Interfaces;

    [Serializable()]
    public sealed class UserTasks : IModel
    {
        private long _id = 0;
        private long _userId = 0;
        private long _taskId = 0;
        private string _taskName = "";
        private string _taskObservation = "";

        #region Constructors

        public UserTasks() { }

        public UserTasks(long id, long userId, long taskId)
        {
            this._id = id;
            this._userId = userId;
            this._taskId = taskId;
        }

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
        public long TaskId
        {
            get { return this._taskId; }
            set { this._taskId = value; }
        }

        [DAOAttributes(DAOJoin = "TaskName")]
        public string TaskName
        {
            get { return this._taskName; }
            set { this._taskName = value; }
        }

        [DAOAttributes(DAOJoin = "TaskObservation")]
        public string TaskObservation
        {
            get { return this._taskObservation; }
            set { this._taskObservation = value; }
        }

        #endregion

        #region Interface implementation

        public string IGetTableName()
        {
            return "UserTasks";
        }
        public long IGetId()
        {
            return this._id; ;
        }
        public string IGetSQLJoin()
        {
            return "SELECT UserTasks.*, Tasks.Name as TaskName, Tasks.Observation as TaskObservation " +
                "FROM UserTasks INNER JOIN Tasks ON UserTasks.TaskId=Tasks.ID ";
        }
        public ModuleData IGetModule()
        {
            return new ModuleData { id = this._id, name = "UserTasks" };
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