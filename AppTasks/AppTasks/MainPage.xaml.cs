using System.Collections.ObjectModel;

using AppTasks.Views;
using AppTasks.Controller;
using App.Models;
using App.Enums;
using App.Logic;
using App.Events;
using App.Config;
using App.Logic.IO.Handler;

namespace AppTasks
{
    public partial class MainPage : ContentPage
    {
        //public delegate void ButtonClicked(object sender, EventArgs e);

        private const string MESSAGE_SENDER     = "MainPage";

        public const string APP_LOGIN          = "Log in";
        public const string APP_LOGOUT         = "Logout";
        public const string APP_EXIT           = "Are you sure to close the Application ?";
        //public const string APP_BTN_INSTALL    = "Install";
        //public const string APP_BTN_REINSTALL  = "Re Install";
        //public const string APP_OVERWRITE      = "Are you sure to Re-install the Application ? all data will be lost!!";
        public const string APP_INSTALL        = "Not available Database to log in, install it.";
        public const string APP_INSTALL_ERROR  = "Unable to initialize Database.";
        public const string APP_LOGIN_ERROR    = "Incorrect Credentials...";

        // Layers navigation.
        public static string _prevLayerMaster = "";
        public static string _prevLayerDetail = "";
        public static string _prevLayerSubMenu = "";

        public ObservableCollection<MessageEvent> MessageEvents { get; set; } = new ObservableCollection<MessageEvent>();
        private AppInitialize init;
        private AppTaskButtonH appBtnHandler;
        private static AppMenuHandler _appMenuHandler;
        private static DataBaseType type = DataBaseType.SqLite;

        private bool _appDbExist = false;

        public MainPage()
        {

            InitializeComponent();

            DisplayMessage.Displaytype = DisplaType.Mobile;
            DisplayMessage.messageHandler.UpdateStatus += MessageHandler_UpdateStatus;

            this.init = new AppInitialize(type, DisplaType.Mobile);
            this.appBtnHandler = new AppTaskButtonH(this, OnButtonClicked);

            this._appDbExist = init.DDBBisInstalled;

            this.btnOptions.IsEnabled = btnLogin.IsEnabled;

            this.ControlStatus();
        
        }

        private void activityIndicator(bool isRunning)
        {
            this.Activity.IsEnabled = isRunning;
            this.Activity.IsRunning = isRunning;
            this.Activity.IsVisible = isRunning;
        }

        #region Action : Install Application Database

        private async Task<bool> InitializeApplication(bool reInstall)
        //private void InitializeApplication(bool reInstall)
        {
            try
            {
                // TODO: fin element. change att.
                this.activityIndicator(true);
                MessageEvents.Clear();

                //if (this._appDbExist == true)
                //    reInstall = await displayMessage(APP_BTN_INSTALL, APP_OVERWRITE);

                if (reInstall == true || this._appDbExist == false)
                {
                    AppInitialize.messageHandler.UpdateStatus += MessageHandler_UpdateStatus;
                    DataBaseType tmp = this.init.Initialize();

                    if (tmp != DataBaseType.SqLite)
                        DisplayMessage.Show(MESSAGE_SENDER, APP_INSTALL_ERROR);
                    //await DisplayAlert("Error", APP_INSTALL_ERROR, "OK");
                }
            }
            catch(Exception ex)
            {
                DisplayMessage.Show(MESSAGE_SENDER, ex.Message);
            }
            finally
            {
                AppInitialize.messageHandler.UpdateStatus -= MessageHandler_UpdateStatus;
                this.activityIndicator(false);
                this._appDbExist = init.DDBBisInstalled;
            }
            return reInstall;
        }

        #endregion

        #region Action : Login

        private void loginAction()
        {
            string msg = "";
            try
            {
                if (this._appDbExist == false)
                    throw new Exception(APP_INSTALL);

                string user = "";
                string pass = "";

                user = AppLayers.GetLayoutEntryValue(this, "layerMaster", "txtUser");
                pass = AppLayers.GetLayoutEntryValue(this, "layerMaster", "txtPass");

                if (user == "" || pass == "")
                    throw new Exception(APP_LOGIN_ERROR);

                if (_appMenuHandler == null)
                    _appMenuHandler = new AppMenuHandler(new UserAccess(DataBaseType.SqLite, DisplaType.Mobile));

                if (_appMenuHandler.DoLogin(user + "," + pass))
                    loginCorrect();
                else
                {
                    _appMenuHandler = null;
                    throw new Exception(APP_LOGIN_ERROR);
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            DisplayMessage.Show(MESSAGE_SENDER, msg);
        }

        private void loginCorrect()
        {
            this.lblUserName.Text = AppMenuHandler.GetCurrentUser.GetUserFullName;
            this.ControlStatus();
        }

        #endregion

        #region Action : Popups messages

        private async Task<bool> displayMessage(string title, string message)
        {
            return await DisplayAlert(title, message, "Ok", "Cancel"); ;
        }

        #endregion

        #region Message Handler from classes

        private void MessageHandler_UpdateStatus(object? sender, EventArgs e)
        {
            UpdateStatusEventArgs use = (UpdateStatusEventArgs)e;

            switch (use.Sender)
            {
                case "AppMenuHandler":
                case "MainPage":
                    this.lblStatus.Text = use.Message + " " + use.Status;
                    break;
                case "AppInitialize":
                    BindingContext = this;
                    this.MessageEvents.Add(new MessageEvent(use.Message, use.Status, DateTime.Now));
                    break;
                default:
                    break;
            }
        }

        #endregion

        private void ControlStatus()
        {
            bool logged = AppMenuHandler.UserAuthenticated;

            if (logged == false)
            {
                this.lblUserName.Text = "";
                AppLayers.AddComponent(this, "layerMaster", new Welcome(), OnButtonClicked);
            }
            else
            {
                AppLayers.RemoveComponent(this, "layerButtons", "layerButtons", OnButtonClicked);
                AppLayers.RemoveComponent(this, "layerSubmenu", "layerSubmenu", OnButtonClicked);
                AppLayers.RemoveComponent(this, "layerMaster", "layerMaster", OnButtonClicked);
                AppLayers.RemoveComponent(this, "layerDetail", "layerDetail", OnButtonClicked);
                
                AppLayers.AddComponent(this, "layerMaster", new AppSearchBar(), OnButtonClicked);
                //this.doUsers();
                // TODO: show task menu.

            }

            this.btnLogin.Text = logged ? APP_LOGOUT : APP_LOGIN;
            this.btnLogin.IsEnabled = true;
            this.btnOptions.IsEnabled = btnLogin.Text == APP_LOGIN ? true : false;
        }

        #region Buttons Handler

        private void doUsers()
        {
            this.layerMaster.HeightRequest = 10;
            AppUsersGrid basic = new AppUsersGrid();
            basic.AddRow("Log as Admin (Test)", "->", "btnLog01", "Login", OnButtonClicked);
            basic.AddRow("Log as User  (Test)", "->", "btnLog02", "Login", OnButtonClicked);
            this.layerDetail.Children.Add(basic);
            basic.BindingContext = this;
        }

        private async Task DoAction(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.StyleId)
            {
                case "btnYes":
                    // TODO : check child sender to do action
                    btn.IsEnabled = false;
                    //this.InitializeApplication(true);
                    bool done = await this.InitializeApplication(true);
                    //bool done = await InitializeApplication(true).ConfigureAwait(false);
                    //this.btnOptions.IsEnabled = true;
                    this.lblStatus.Text = done.ToString();
                    break;
                case "btnNo":
                    this.appBtnHandler.ActionResponseNo();
                    this.btnLogin.IsEnabled = true;
                    this.btnOptions.IsEnabled = true;
                    break;
                case "btnLogin":
                    this.btnOptions.IsEnabled = false;
                    this.appBtnHandler.ActionLogin(ref _appMenuHandler, btn);
                    if (btn.Text != APP_LOGIN)
                        this.ControlStatus();
                    break;
                case "btnLog01":
                case "btnLog02":
                    if (this.appBtnHandler.ActionLoginAs(ref _appMenuHandler, btn.StyleId) == true)
                        this.loginCorrect();
                    break;
                case "btnInstall":
                    this.appBtnHandler.ActionInstall(this._appDbExist);
                    if (this._appDbExist == false)
                        this.InitializeApplication(true);
                    break;
                case "btnQuit":
                    if (await displayMessage("Quit", APP_EXIT) == true)
                        Environment.Exit(0);
                    break;
                case "btnSigIn":
                    btn.IsEnabled = false;
                    loginAction();
                    btn.IsEnabled = true;
                    break;
                case "btnBack":
                    this.appBtnHandler.ActionBack();
                    this.btnLogin.IsEnabled = true;
                    this.btnOptions.IsEnabled = true;
                    break;
                case "btnOptions":
                    this.appBtnHandler.ActionOptions(ref btnLogin);
                    break;
                case "btnUsers":
                    this.appBtnHandler.ActionUsers();
                    this.doUsers();
                    break;
                default: this.lblStatus.Text = btn.Text; break;

            }

        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            try
            {
                DoAction(sender, e).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                DisplayMessage.Show(MESSAGE_SENDER, ex.Message);
            }
        }

        #endregion

    }
}
