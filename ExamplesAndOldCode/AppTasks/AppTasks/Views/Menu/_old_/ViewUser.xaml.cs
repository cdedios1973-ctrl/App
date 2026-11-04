namespace AppTasks.Views.Menu;

public partial class ViewUser : ContentView
{
    public delegate void OnViewUserButtonClickedDelegate(object? sender, EventArgs e);
    public OnViewUserButtonClickedDelegate OnViewUserButtonClicked { get; set; }

    #region Binds : Binding Properties

    public static readonly BindableProperty UserLoggedProperty = 
        BindableProperty.Create(nameof(UserLogged), typeof(string), typeof(ViewUser), string.Empty);
    
    public static readonly BindableProperty ModuleTitleProperty =
        BindableProperty.Create(nameof(ModuleTitle), typeof(string), typeof(ViewUser), string.Empty); 


    public string UserLogged
    {
        get => (string)GetValue(ViewUser.UserLoggedProperty);
        set => SetValue(ViewUser.UserLoggedProperty, value);
    }

    public string ModuleTitle
    {
        get => (string)GetValue(ViewUser.ModuleTitleProperty);
        set => SetValue(ViewUser.ModuleTitleProperty, value);
    }

    #endregion

    public ViewUser()
	{
		InitializeComponent();
        EventSubscribe();
    }

    #region Events : Subscribe && Unsubscrite

    private void EventSubscribe(bool subscribe = true)
    {
        if (subscribe == true)
        {
            this.Unloaded               += ViewUser_Unloaded;
            this.btnQuit.Clicked        += ViewUserButtonClicked;
            this.btnOptions.Clicked     += ViewUserButtonClicked;
            this.BindingContextChanged  += ViewUser_BindingContextChanged;
        }
        else
        {
            this.Unloaded               -= ViewUser_Unloaded;
            this.btnQuit.Clicked        -= ViewUserButtonClicked;
            this.btnOptions.Clicked     -= ViewUserButtonClicked;
            this.BindingContextChanged -= ViewUser_BindingContextChanged;
        }
    }

    #endregion

    #region Private Methods

    private void ViewUser_Unloaded(object? sender, EventArgs e)
    {
        EventSubscribe(false);
    }

    private void ViewUser_BindingContextChanged(object? sender, EventArgs e)
    {
        this.lblUserName.Text = MainPage.GetUserName;
        this.lblTitle.Text = MainPage.GetTitle;
    }

    private void ViewUserButtonClicked(object? sender, EventArgs e)
    {
        if (OnViewUserButtonClicked != null)
            OnViewUserButtonClicked(sender, e);
    }

    #endregion

}