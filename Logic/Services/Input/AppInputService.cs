using System;
using App.Definitions.Enums;
using App.Logic.IO;
using App.Logic.IO.Handler;
using App.Logic.Models.POCO;
using App.Logic.Security;

namespace App.Logic.Services.Input
{

    public class AppInputService
    {
        private const string MESSAGE_SENDER = "AppMenuHandler";
        private static UserAccess       _userAccess;
        private static List<AppCommand> _commandList;
        private static List<AppCommand> _commandDDBBList;
        private static AppMenuAction    _appMenuAction;
        private static UsersPermission  _previousPermission = new UsersPermission();

        #region Constructor

        public AppInputService(UserAccess userAccess)
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
                {
                    permission = InputString.CheckInputPermission(ref _userAccess, ref input);
                }
                    
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
                    case "I": DataBaseType type = _appMenuAction.Initialize(); break;
                    case "Q": Environment.Exit(0); break;
                    case "L": Logout(); break;
                    case "M":
                        if(_userAccess.IsUserAuthenticated == false)
                            _appMenuAction.ShowMenu<UsersPermission>(permission);
                        else
                            _appMenuAction.SetMenuClass(permission, action, ref input);
                        break;
                    case "S": _appMenuAction.ChangeDbType(); break;
                    case "U": _appMenuAction.ShowMenu<UsersPermission>(permission, null, DrawAppMenu.Users, true); break;
                    case "?": _appMenuAction.ShowMenu<UsersPermission>(permission, null, DrawAppMenu.Help, true); break;
                    //case "00": this.DoLogin("admin,123"); break;
                    //case "01": this.DoLogin("user 2,123"); break;
                    //case "00": _userAccess = new DAOLogin("admin,123", _userAccess.DDBBType, _userAccess.Display).GetValidateUser; break;
                    //case "01": _userAccess = new DAOLogin("user 2,123", _userAccess.DDBBType, _userAccess.Display).GetValidateUser; break;
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

        #region Action : Logout

        private void Logout()
        {
            if (_userAccess.Logout == true)
            {
                if (_userAccess.Display == DisplaType.Console)
                    Input("M");
                else
                    DisplayMessage.Show(MESSAGE_SENDER, "User logout.");
            }

        }

        #endregion
    
    }
}
