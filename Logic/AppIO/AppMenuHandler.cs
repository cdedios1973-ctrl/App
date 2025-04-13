namespace App.Logic.IO.Handler
{
    using System;
    using App.Models;
    using App.Logic.IO;
    using App.Enums;
    using App.DAO;

    public class AppMenuHandler
    {
        private const string MESSAGE_SENDER = "AppMenuHandler";

        private const string INVALID_OPTION = "Invalid option.";
        private const string LOGGED_AS      = "Logged as User: {0} ";
        private const string LOGGED_FAIL    = "User not found.";
        private const string SELECT_OPTION  = "Select option: ";

        private static UserAccess       _userAccess;
        private static List<AppCommand> _commandList;
        private static List<AppCommand> _commandDDBBList;
        private static AppMenuAction    _appMenuAction;
        private static UsersPermission  _previousPermission = new UsersPermission();

        #region Constructor

        public AppMenuHandler(UserAccess userAccess)
        {
            _userAccess = userAccess;

            if (_commandList == null || _commandDDBBList == null)
            {
                _appMenuAction = new AppMenuAction(_userAccess.DDBBType, _userAccess.Display);
                AppCommand cmd = new AppCommand();
                _commandList = cmd.GetCommandList;
                _commandDDBBList = cmd.GetDDBBCommandList;
            }
        }

        #endregion

        #region Properties

        public static UserAccess GetCurrentUser
        {
            get { return _userAccess; }
        }

        public static DataBaseType DDBBType
        {
            get { return _userAccess.DDBBType; }
            set { _userAccess.DDBBType = value; }
        }

        public static DisplaType Displaytype
        {
            get { return _userAccess.Display; }
        }

        public static bool UserAuthenticated
        { 
            get 
            {
                if (_userAccess != null)
                    return _userAccess.IsUserAuthenticated;
                else
                    return false;
            }
        }

        #endregion

        #region Menu Input Handler

        public void Input(string input)
        {
            bool test = true;
            UsersPermission permission = new UsersPermission();
            try
            {
                if (_userAccess.IsUserAuthenticated)
                    permission = InputString.CheckInputPermission(ref _userAccess, ref input);
            }
            catch { }
            try
            {
                if (InputString.CheckInput(ref input) == true)
                    executeCommand(input, permission);
                else
                    throw new Exception("Incorrect syntax / command.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        #endregion

        #region Menu Handler

        private void executeCommand(string input, UsersPermission permission)
        {
            try
            {
                MenuAction action = input.Length > 5 ? MenuAction.DDBB : MenuAction.Display;

                if (action == MenuAction.DDBB)
                    permission = _previousPermission;

                switch (input.ToUpper())
                {
                    case "E": _appMenuAction.SetMenuClass(_previousPermission, MenuAction.Export, ref input); break;
                    case "I": _appMenuAction.Initialize(); break;
                    case "Q": Environment.Exit(0); break;
                    case "L": this.Logout(); break;
                    case "M":
                        if(_userAccess.IsUserAuthenticated == false)
                            _appMenuAction.ShowMenu<UsersPermission>(permission);
                        else
                            _appMenuAction.SetMenuClass(permission, action, ref input);
                        break;
                    case "S": _appMenuAction.ChangeDbType(); break;
                    case "U": _appMenuAction.ShowMenu<UsersPermission>(permission, null, DrawAppMenu.Users, true); break;
                    case "?": _appMenuAction.ShowMenu<UsersPermission>(permission, null, DrawAppMenu.Help, true); break;
                    case "00": this.DoLogin("admin,123"); break;
                    case "01": this.DoLogin("user 2,123"); break;
                    default:
                        Console.WriteLine("Option selected: {" + permission.PermissionId + " " + permission.PermissionName + "}");
                        _appMenuAction.SetMenuClass(permission, action, ref input);
                        break;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage.Show(MESSAGE_SENDER, ex.Message);
            }
            _previousPermission = permission;
        }

        #endregion

        #region Login Action

        //public UserAccess DoLogin(string input)
        public bool DoLogin(string input)
        {
            _userAccess = new DAOLogin(input, _userAccess.DDBBType, _userAccess.Display).GetValidateUser;
            if (_userAccess.GetUser == null)
            {
                DisplayMessage.Show(MESSAGE_SENDER, LOGGED_FAIL);
            }
            return _userAccess.IsUserAuthenticated;
        }

        #endregion

        #region Logout Action

        private void Logout()
        {
            if (_userAccess.Logout == true)
            {
                if (_userAccess.Display == DisplaType.Console)
                    this.Input("M");
                else
                    DisplayMessage.Show(MESSAGE_SENDER, "User logout.");
            }

        }

        #endregion
    
    }
}
