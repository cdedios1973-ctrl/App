//using System.Collections.ObjectModel;
using System;

using AppTasks.Controller.Layers;
using App.Enums;
using App.Models;
using App.Logic;
using App.Events;
using App.Config.DDBB;
using App.Logic.IO.Handler;
using System.Windows.Input;

namespace AppTasks
{
    public partial class MainPage : ContentPage
    {
        //public delegate void ButtonClicked(object? sender, EventArgs e);

        internal const string MESSAGE_SENDER    = "MainPage";
        internal const string APP_LOGIN         = "Login";
        internal const string APP_LOGOUT        = "Logout";
        internal const string APP_EXIT          = "Are you sure to close the Application ?";
        internal const string APP_INSTALL       = "Not available Database to log in, install it.";
        internal const string APP_INSTALL_ERROR = "Unable to initialize Database.";
        internal const string APP_LOGIN_ERROR   = "Incorrect Credentials...";
        internal const string APP_NO_USER       = "No user.";

        //public const string APP_BTN_INSTALL    = "Install";
        //public const string APP_BTN_REINSTALL  = "Re Install";
        //public const string APP_OVERWRITE      = "Are you sure to Re-install the Application ? all data will be lost!!";
        //public ObservableCollection<MessageEvent> MessageEvents { get; set; } = new ObservableCollection<MessageEvent>();
        //private static UserAccess   _user;

        private static DataBaseType type = DataBaseType.SqLite;
        private static bool _appDbExist = false;

        private static string _currentTitle = "Main screen";

        private static LayerMenu _layerMenu;

        //TODO: change AppInitialize to Singleton pattern.
        private AppInitialize _appInit;
        //TODO: change LayerInstall to Singleton pattern.
        private static LayerInstall _lInstall;
        //TODO: change AppMenuHandler to Singleton pattern.
        internal static AppMenuHandler _appMenuHandler;

        //private static LayerTestUsers _lTestUser;
        
        public ICommand NavigateCommand { get; private set; }

        public MainPage()
        {
            InitializeComponent();
            this.Start();
            //this.scrollDetail.MaximumHeightRequest = 360;

        }

        #region Action : Initialize App vars.

        private void Start()
        {
            DisplayMessage.Displaytype = DisplaType.Mobile;
            DisplayMessage.messageHandler.UpdateStatus += MessageHandler_UpdateStatus;

            _appDbExist = new AppInitialize(type, DisplaType.Mobile).DDBBisInstalled;

            _layerMenu = LayerMenu.GetInstance(this);
            //this.CurrentUser.OnUserViewButtonClicked += OnButtonClicked;
            //this.CurrentUser.OnViewUserButtonClicked += OnButtonClicked;
            //this.CurrentMenu.OnMenuViewButtonClicked += OnButtonClicked;

            this.mainMenu.OnMenuViewButtonClicked += OnButtonClicked;

            //this.mainMenu.OnMenuViewButtonClicked += OnButtonClicked;

            _layerMenu.ActionBack();


            NavigateCommand = new Command<Type>(
            async (Type pageType) =>
            {
                Page page = (Page)Activator.CreateInstance(pageType);
                await Navigation.PushAsync(page);
            });

            BindingContext = this;


        }

        #endregion

        #region Properties

        public static bool UserAuthenticated
        { 
            get { return AppMenuHandler.UserAuthenticated; }
        }
        public static bool AppDbExist
        {
            get { return _appDbExist; }
            set { _appDbExist = value; }
        }
        public bool IsBusy
        { 
            get { return this.Activity.IsRunning; }
            set { this.Activity.IsRunning = value; }
        }

        public static string GetUserName
        {
            get { return AppMenuHandler.GetCurrentUser.GetUserFullName; }
        }

        public static string GetTitle
        {
            get { return _currentTitle; }
        }

        #endregion

        #region Action : Popups messages

        private async Task<bool> displayMessage(string title, string message)
        {
            return await DisplayAlert(title, message, "Ok", "Cancel"); ;
        }

        #endregion

        #region Action : Message Handler from classes

        private void MessageHandler_UpdateStatus(object? sender, EventArgs e)
        {
            UpdateStatusEventArgs use = (UpdateStatusEventArgs)e;

            switch (use.Sender)
            {
                case "AppMenuHandler":
                case "MainPage":
                    //this.lblStatus.Text = use.Message + " " + use.Status;
                    break;
                case "AppInitialize":
                    //this.lblStatus.Text = use.Message + " " + use.Status;
                    if (_lInstall == null)
                        throw new Exception("Install manager instance failed.");
                    _lInstall.Action(new MessageEvent(use.Message, use.Status, DateTime.Now));
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Action : Buttons Handler

        private async Task DoAction(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            // TODO : disable current button, foreach enable others.

            //switch (btn.StyleId)
            switch (btn.CommandParameter)
            {
#if DEBUG == true // START: Test Users (DELETE WHEN RELEASE)
                case "btnUsers":
                    new LayerTestUsers(this, OnButtonClicked).Show("a");
                    break;
                case "btnLog01":
                case "btnLog02":
                    if (new LayerTestUsers(this, OnButtonClicked).Action(ref btn, ref _appMenuHandler) == true)
                        _layerMenu.Show();
                    break;
#endif // END : Test Users
                case "btnLogin":
                    if (btn.Text == APP_LOGIN)
                        new LayerLogin(this, OnButtonClicked).Show();
                    else
                        _layerMenu.Show(false);
                    break;
                case "btnSigIn":
                    btn.IsEnabled = false;
                    if (new LayerLogin(this).Action(ref _appMenuHandler) == true)
                        _layerMenu.Show();
                    else
                        //lblStatus.Text = APP_LOGIN_ERROR;
                        btn.IsEnabled = true;
                    break;
                case "btnYes":
                    // TODO : check child sender to do action
                    this.scrollDetail.MaximumHeightRequest = 360;
                    bool install = _lInstall.Action(ref this._appInit, MessageHandler_UpdateStatus, true);
                    if (install == true)
                        _lInstall = null;
                    break;
                case "btnNo":
                case "btnBack":
                    _layerMenu.ActionBack();
                    break;
                case "btnInstall":
                    if (_lInstall != null)
                        _lInstall = null;
                    _lInstall = new LayerInstall(this, OnButtonClicked);
                    _lInstall.Show();
                    break;
                case "btnQuit":
                    if (await displayMessage("Quit", APP_EXIT) == true)
                        Environment.Exit(0);
                    break;
                case "btnOptions":
                    //this.CurrentMenu.IsVisible = !this.CurrentMenu.IsVisible;
                    //_layerMenu.Show();
                    break;

                default:  break; //this.lblStatus.Text = "Action: " + btn.Text;

            }
        }

        public void OnButtonClicked(object? sender, EventArgs e)
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

        public void ViewUserButtonClick(object sender, EventArgs e)
        {
            string asd = e.ToString();
        }

        #endregion

        }
}
