namespace AppTasks.Controls
{
    public partial class UserView : ContentPage
    {
        public delegate void OnUserViewButtonClickedDelegate(object? sender, EventArgs e);
        public OnUserViewButtonClickedDelegate OnUserViewButtonClicked { get; set; }

        public UserView()
        {
            InitializeComponent();
        }

        #region Events : Subscribe && Unsubscrite

        private void ViewMenu_Unloaded(object? sender, EventArgs e)
        {
            this.EventSubscribe(false);
        }

        private void EventSubscribe(bool subscribe = true)
        {
            /*
            if (subscribe == true)
                this.Unloaded += ViewUser_Unloaded;
            else
                this.Unloaded -= ViewUser_Unloaded;
            */
        }

        #endregion

        private void UserViewButtonClicked(object? sender, EventArgs e)
        {
            if (OnUserViewButtonClicked != null)
                OnUserViewButtonClicked(sender, e);
        }
    }
}
