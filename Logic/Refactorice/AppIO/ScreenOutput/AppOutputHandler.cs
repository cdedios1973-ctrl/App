namespace App.Logic.IO
{
    using App.Models;
    using App.Enums;
    using App.Interfaces;
    using AppLogic.Services.Input;

    public static class AppOutputHandler
    {
        private const string DISPLAY_NOT_IMPLEMENTED = "Output display not implemented for :";
        private const string DISPLAY_WRONG_OUTPUT = "Output display doesn't macht, config error.";
        private const string DISPLAY_WRONG_PARAMATERS = "Wrong parameters";
        private const string NOT_LOGGED_USER = "User not logged in";
        private const string PERMISSION_NOT_GRANTED = "Current user has no access to this module.";

        public struct Display
        {
            public string UserName;
            public string Title;
            public string ColumnsName;
            public string DataRows;
            public string DDBBActions;
            public string MenuActions;
        }

        public static Display _dsp = new Display
        {
            UserName    = "",
            Title       = "",
            ColumnsName = "",
            DataRows    = "",
            DDBBActions = "* Data actions : " + string.Join(" | ", GetSubmenuOptions.ToList()),
            MenuActions = "* Options :      " + string.Join(" | ", GetMenuOptions.ToList())
        };

        private static List<string> _menuOptions;
        private static List<string> _subMenuOptions;

        #region Properties

        private static List<string> GetMenuOptions
        { 
            get {
                if (_menuOptions == null)
                {
                    _menuOptions = new List<string>();
                    List<AppCommand> lst = new AppCommand().GetCommandList;
                    foreach (AppCommand cmd in lst)
                        _menuOptions.Add(cmd.Name);
                }
                int i = _menuOptions.FindIndex(x => x.Contains("(S)ource"));
                _menuOptions[i] = "(S)ource {" + AppInputService.GetCurrentUser.DDBBType.ToString() + "}";

                return _menuOptions; } 
        }

        private static List<string> GetSubmenuOptions
        {
            get
            {
                if (_subMenuOptions == null)
                {
                    _subMenuOptions = new List<string>();
                    List<AppCommand> lst = new AppCommand().GetDDBBCommandList;
                    foreach (AppCommand cmd in lst)
                        _subMenuOptions.Add(cmd.Name);
                }
                return _subMenuOptions; }
        }

        public static void ChangeSource()
        {
            DataBaseType type = AppInputService.GetCurrentUser.DDBBType;
            AppInputService.DDBBType = ((int)type < 4) ? (DataBaseType)((int)type + 1) : DataBaseType.SqlServer;
            _dsp.MenuActions = "* Options :      " + string.Join(" | ", GetMenuOptions.ToList());
        }

        #endregion

        #region Method: Display Selection

        public static void Draw<T>(UsersPermission permission, DataFilter dataFilter = null) where T : IModel
        {
            try
            {
                if (AppInputService.GetCurrentUser.IsUserAuthenticated == false)
                    throw new Exception(NOT_LOGGED_USER);

                if (AppInputService.GetCurrentUser.checkUserPermissionById(permission.PermissionId) < 0)
                    throw new Exception(PERMISSION_NOT_GRANTED);

                DataFilter filter = new DataFilter();
                if (dataFilter != null)
                    filter = dataFilter;

                ExecDisplay<T>(filter);

            }
            catch (Exception ex)
            {
                throw new Exception("OutputScreen: " + ex.Message);
            }
        }

        public static void Draw<T>(DrawAppMenu menu, bool vertical) where T : IModel
        {
            ExecDisplay<T>(null, vertical, menu);
        }

        private static void ExecDisplay<T>(DataFilter filter = null, bool vertical = true, 
                            DrawAppMenu menu = DrawAppMenu.None) where T : IModel
        {
            try
            {
                switch (AppInputService.GetCurrentUser.Display)
                {
                    case DisplaType.Console:
                        if (filter != null)
                            ConsoleOutput.DrawConsole<T>(ref filter);
                        else if (menu != DrawAppMenu.None)
                            ConsoleOutput.DrawConsole<T>(menu, vertical);
                        else
                            throw new Exception(DISPLAY_WRONG_PARAMATERS + DisplaType.Console.ToString());
                        break;
                    case DisplaType.Desktop: throw new Exception(DISPLAY_NOT_IMPLEMENTED + DisplaType.Desktop.ToString());
                    case DisplaType.Mobile: throw new Exception(DISPLAY_NOT_IMPLEMENTED + DisplaType.Mobile.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

    }
}
