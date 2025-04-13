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

        private const string MESSAGE_SENDER    = "MainPage";

        public const string APP_LOGIN          = "Login";
        public const string APP_LOGOUT         = "Logout";
        public const string APP_EXIT           = "Are you sure to close the Application ?";
        public const string APP_INSTALL        = "Not available Database to log in, install it.";
        public const string APP_INSTALL_ERROR  = "Unable to initialize Database.";
        public const string APP_LOGIN_ERROR    = "Incorrect Credentials...";
        public const string APP_NO_USER        = "No user.";
        //public const string APP_BTN_INSTALL    = "Install";
        //public const string APP_BTN_REINSTALL  = "Re Install";
        //public const string APP_OVERWRITE      = "Are you sure to Re-install the Application ? all data will be lost!!";

        //private static string _currentMenu = "";

        //public ObservableCollection<MessageEvent> MessageEvents { get; set; } = new ObservableCollection<MessageEvent>();
        
        private AppTaskMainHandler _appHandler;
        private static AppMenuHandler _appMenuHandler;

        private AppInitialize init;
        private AppInstallGrid installGrid;

        private static DataBaseType type = DataBaseType.SqLite;
        private bool _appDbExist = false;

        public MainPage()
        {
            InitializeComponent();

            DisplayMessage.Displaytype = DisplaType.Mobile;
            DisplayMessage.messageHandler.UpdateStatus += MessageHandler_UpdateStatus;

            this.init = new AppInitialize(type, DisplaType.Mobile);
            this._appHandler = new AppTaskMainHandler(this, OnButtonClicked);
            this._appDbExist = init.DDBBisInstalled;

            this.PageStatus();
        }

        private void PageStatus()
        {
            bool logged = UserAuthenticated;
            if (logged == false)
                this.lblUserName.Text = APP_NO_USER;

            this._appHandler.Status(logged, this._appDbExist);
            // TODO: if logged => show task menu.
        }

        public static bool UserAuthenticated
        { 
            get { return AppMenuHandler.UserAuthenticated; }
        }

        private void activityIndicator(bool isRunning)
        {
            this.Activity.IsEnabled = isRunning;
            this.Activity.IsRunning = isRunning;
            this.Activity.IsVisible = isRunning;
        }

        #region Action : Install Application Database

        private bool InitializeApplication(bool reInstall)
        {
            try
            {
                if (reInstall == true || this._appDbExist == false)
                {
                    // TODO: error creating activity
                    this.activityIndicator(true);
                    this.scrollDetail.HeightRequest = 250;
                    this.installGrid = new AppInstallGrid();
                    this.layerDetail.Children.Add(installGrid);

                    AppInitialize.messageHandler.UpdateStatus += MessageHandler_UpdateStatus;
                    DataBaseType tmp = this.init.Initialize();
                    if (tmp != DataBaseType.SqLite)
                        DisplayMessage.Show(MESSAGE_SENDER, APP_INSTALL_ERROR);
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
                this.installGrid = null;
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

                string input = "";
                input = AppLayers.GetLayoutEntryValue(this, "layerMaster", "txtUser") + ",";
                input += AppLayers.GetLayoutEntryValue(this, "layerMaster", "txtPass");

                if (this._appHandler.ActionLoginAs(ref _appMenuHandler, input))
                {
                    this.lblUserName.Text = AppMenuHandler.GetCurrentUser.GetUserFullName;
                    this.PageStatus();
                }
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
                    this.RefreshMessage(new MessageEvent(use.Message, use.Status, DateTime.Now));
                    break;
                default:
                    break;
            }
        }

        private void RefreshMessage(MessageEvent msg)
        {
            if (this.installGrid == null)
                throw new Exception("Install Grid instance failed.");

            installGrid.AddRow(msg);
            //this.layerDetail.Add(installGrid);
            installGrid.BindingContext = this;

        }

        #endregion

        #region Buttons Handler

        private async Task DoAction(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            bool logged = UserAuthenticated;

            switch (btn.StyleId)
            {
                #if DEBUG == true
                // START: Test Users (DELETE WHEN RELEASE)
                case "btnUsers":
                    this._appHandler.ActionLogin(ref _appMenuHandler);
                    AppUsersGrid basic = new AppUsersGrid();
                    basic.AddRow("Log as Admin (Test)", " -> ", "btnLog01", "admin,123", OnButtonClicked);
                    basic.AddRow("Log as User  (Test)", " -> ", "btnLog02", "user 2,123", OnButtonClicked);
                    this.layerButtons.Children.Add(basic);
                    basic.BindingContext = this;
                    break;
                case "btnLog01":
                case "btnLog02":
                    string[] str = btn.Text.Split(",");
                    AppLayers.SetLayoutEntryValue(this, "layerMaster", "txtUser", str[0]);
                    AppLayers.SetLayoutEntryValue(this, "layerMaster", "txtPass", str[1]);
                    loginAction();
                    break;
                // END : Test Users
                #endif
                case "btnYes":
                    // TODO : check child sender to do action
                    btn.IsEnabled = false;
                    //bool done = await this.InitializeApplication(true).ConfigureAwait(false);
                    bool done = InitializeApplication(true);
                    this.lblStatus.Text = done.ToString();
                    break;
                case "btnNo":
                case "btnBack":
                    this.PageStatus();
                    break;
                case "btnLogin":
                    this._appHandler.ActionLogin(ref _appMenuHandler);
                    if (btn.Text != APP_LOGIN)
                    {
                        _appMenuHandler.Input("L");
                        this.PageStatus();
                    }
                    break;
                case "btnSigIn":
                    btn.IsEnabled = false;
                    loginAction();
                    btn.IsEnabled = true;
                    break;
                case "btnInstall":
                    this._appHandler.ActionInstall(ref this._appDbExist, lblStatus);
                    if (this._appDbExist == false)
                        this.InitializeApplication(true);
                    break;
                case "btnQuit":
                    if (await displayMessage("Quit", APP_EXIT) == true)
                        Environment.Exit(0);
                    break;
                case "btnOptions":
                    this._appHandler.ActionOptions();
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
