namespace App.Logic.IO.Handler
{
    using App.Models;
    using App.Definitions.Enums;
    using App.Definitions.Interfaces;
    using App.Definitions.Filters;
    using App.Config.DDBB;
    using App.DAO;
    using AppLogic.Services.Input;

    //TODO: Apply 'pagination' (users like... permissions like... etc)
    public class AppMenuAction
    {
        private const string MESSAGE_SENDER             = "AppMenuAction";

        private const string MISSING_TABLENAME          = "Must select a table before exporting data.";
        private const string DATASOURCE_CHANGED         = "Data source changed to: ";
        private const string ACTION_ABORT               = "Action aborted !";
        private const string NO_IMPLEMENTED             = "Action not implemented.";

        private DataBaseType    _dbType;
        private DisplaType      _displayType;

        #region Constructor

        public AppMenuAction(DataBaseType dbType, DisplaType displayType)
        {
            this._dbType = dbType;
            this._displayType = displayType;
            DisplayMessage.Displaytype = this._displayType;
        }

        #endregion

        #region Action : Install Database Sources

        public DataBaseType Initialize()
        {
            return new AppInitialize(this._dbType, this._displayType).Initialize();
        }

        #endregion

        #region Action : Apply filters to current user

        private DataFilter? applyFilter(IModel model, long Id)
        {
            DataFilter filter = null;

            bool isAdmin = AppInputService.GetCurrentUser.GetUserId == 1;
            string tableName = model.IGetTableName();

            if (isAdmin == false)
            {
                if (tableName != "Users")
                    filter = new DataFilter(model.IGetFilterByUserId(Id));
                else if (tableName == "Users")
                    filter = new DataFilter(model.IGetFilterById(Id));
            }

            return filter;
        }

        #endregion

        #region Action : Menu input handler (Class handler)

        internal void SetMenuClass(UsersPermission permission, MenuAction action, ref string input)
        {
            IModel model = (IModel)AppClass.GetClassByName(permission.PermissionName);
            string tableName = model.IGetTableName();
            if(tableName == "")
                throw new Exception(MISSING_TABLENAME);

            DDBBParams dDBBParams = new DDBBParams();
            if (action == MenuAction.DDBB)
                dDBBParams = DAOParser.GetStringParser(ref permission, input);

            bool export = (action == MenuAction.Export) ? true : false;

            DataFilter filter = applyFilter(model, AppInputService.GetCurrentUser.GetUserId);

            switch (tableName)
            {
                //case "DAOSchemas": ShowMenu<DAOSchemas>(permission, null); break;
                //case "Export": ShowMenu<Export>(permission, null); break;
                case "Tasks":
                    {
                        List<TasksDTO> tasks = null;
                        if (action == MenuAction.DDBB)
                            tasks = new List<TasksDTO> {
                            new( dDBBParams.dataId, dDBBParams.data[1], dDBBParams.data[2]) };

                        ActionHandler<TasksDTO>(ref permission, filter, export, tasks, dDBBParams.dataAction);
                    }
                    break;
                case "Permissions":
                    {
                        List<Permissions> permissions = null;
                        if (action == MenuAction.DDBB)
                            permissions = new List<Permissions> { new( dDBBParams.dataId,
                                dDBBParams.data[1], dDBBParams.data[2],
                                dDBBParams.data[3]) };

                        ActionHandler<Permissions>(ref permission, filter, export, permissions, dDBBParams.dataAction);
                    }
                    break;
                case "Users":
                    {
                        List<UsersDTO> users = null;
                        if (action == MenuAction.DDBB)
                            users = new List<UsersDTO> { new (dDBBParams.dataId,
                                dDBBParams.data[1], dDBBParams.data[2],
                                dDBBParams.data[3], dDBBParams.data[4]) };

                        ActionHandler<UsersDTO>(ref permission, filter, export, users, dDBBParams.dataAction);
                    }
                    break;
                case "UsersPermission":
                    {
                        List<UsersPermission> up = null;
                        if (action == MenuAction.DDBB)
                            up = new List<UsersPermission> { new( dDBBParams.dataId,
                                int.Parse(dDBBParams.data[1]), int.Parse(dDBBParams.data[2]),
                                (UserAccessType)int.Parse(dDBBParams.data[3])) };

                        ActionHandler<UsersPermission>(ref permission, filter, export, up, dDBBParams.dataAction);
                    }
                    break;
                case "UsersLogin":
                    {
                        List<UsersLoginDTO> ul = null;
                        if (action == MenuAction.DDBB)
                            ul = new List<UsersLoginDTO> { new (dDBBParams.dataId,
                                int.Parse(dDBBParams.data[1]), dDBBParams.data[2],
                                dDBBParams.data[3]) };

                        ActionHandler<UsersLoginDTO>(ref permission, filter, export, ul, dDBBParams.dataAction);
                    }
                    break;
                case "UserTasks":
                    {
                        List<UserTasksDTO> usertasks = null;
                        if (action == MenuAction.DDBB)
                            usertasks = new List<UserTasksDTO> { new( dDBBParams.dataId,
                                int.Parse(dDBBParams.data[1]), int.Parse(dDBBParams.data[2])) };

                        ActionHandler<UserTasksDTO>(ref permission, filter, export, usertasks, dDBBParams.dataAction);
                    }
                    break;
                default:
                    throw new Exception(NO_IMPLEMENTED);
            }
        }

        private void ActionHandler<T>(ref UsersPermission permission, DataFilter filter = null, 
                        bool export = false, List<T>? data = null, DataActions? action = null) where T : IModel
        {
            if (data != null && action != null)
            {
                if (data.Count > 0)
                    DisplayMessage.Show(MESSAGE_SENDER, DDBBAction<T>(data, action.Value));
                    //Display.Show(MESSAGE_SENDER, DDBBAction<T>(data, action.Value));
            }
            else if (export == false)
                ShowMenu<T>(permission, filter);
            else if (export == true)
                ExportXML<T>(ref permission);
        }

        #endregion

        #region DBActions : Insert, Update, Delete Actions

        internal string DDBBAction<T>(List<T> data, DataActions action) where T : IModel
        {
            string msg = "";
            int i = new DAOData<T>(AppInputService.GetCurrentUser).dataActions(data, action);
            msg = (i > 0) ? "Saved [" + i + "] records." : "Error saving data.";
            return msg;
        }

        #endregion

        #region Action : Display menu

        internal void ShowMenu<T>(UsersPermission permission, 
                        DataFilter filter = null, DrawAppMenu? menu = null, bool? vertical = false)
                        where T : IModel
        {
            if (menu.HasValue == true)
                AppOutputHandler.Draw<T>(menu.Value, vertical.Value);
            else if (permission.PermissionName != "")
                AppOutputHandler.Draw<T>(permission, filter);
            else if (menu.HasValue == false) // user at login screen
                AppOutputHandler.Draw<T>(DrawAppMenu.LogIn, vertical.Value);
        }

        #endregion

        #region Action : Export data to XML

        internal void ExportXML<T>(ref UsersPermission permission, DataFilter filter = null) where T : IModel
        {
            string msg = "";
            if (DisplayMessage.Show(MESSAGE_SENDER, "Export : " + permission.PermissionName + " (Y/N)? ", true) == "Y" ? true : false)
                msg = new DAOXml<T>(AppInputService.GetCurrentUser).ExportXML(ref permission, filter);
            else
                msg = ACTION_ABORT;

            DisplayMessage.Show(MESSAGE_SENDER, msg);
        }

        #endregion

        #region Action : Change source type

        internal void ChangeDbType()
        {
            AppOutputHandler.ChangeSource();
            DisplayMessage.Show(MESSAGE_SENDER, DATASOURCE_CHANGED + AppInputService.GetCurrentUser.DDBBType.ToString());
        }

        #endregion
    
    }
}
