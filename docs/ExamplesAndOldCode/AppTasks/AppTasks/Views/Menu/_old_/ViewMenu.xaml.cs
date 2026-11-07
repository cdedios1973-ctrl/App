using AppTasks.Controller.Layers;

namespace AppTasks.Views.Menu;

public partial class ViewMenu : ContentView
{
    public delegate void OnViewMenuButtonClickedDelegate(object? sender, EventArgs e);
    public OnViewMenuButtonClickedDelegate OnViewMenuButtonClicked { get; set; }

    //BindingPropertyChangedDelegate<in TPropertyType>(BindableObject bindable, TPropertyType oldValue, TPropertyType newValue);

    #region Binds : Binding Properties

    public static readonly BindableProperty UserLoggedProperty =
    BindableProperty.Create(nameof(UserLogged), 
        typeof(string), 
        typeof(ViewUser), 
        string.Empty,
         propertyChanged: UserLoggedChanged);

    public string UserLogged
    {
        get => (string)GetValue(ViewUser.UserLoggedProperty);
        set => SetValue(ViewUser.UserLoggedProperty, value);
    }

    #endregion

    #region Events : Subscribe && Unsubscrite

    private void EventSubscribe(bool subscribe = true)
    {
        if (subscribe == true)
        {
            this.Unloaded           += ViewMenu_Unloaded;
            this.btnUsers.Clicked       += ViewMenuButtonClicked;
            this.btnLogin.Clicked       += ViewMenuButtonClicked;
            this.btnSource.Clicked      += ViewMenuButtonClicked;
            this.btnInstall.Clicked     += ViewMenuButtonClicked;
            this.BindingContextChanged  += ViewMenu_BindingContextChanged;
        }
        else
        {
            this.Unloaded               -= ViewMenu_Unloaded;
            this.btnUsers.Clicked       -= ViewMenuButtonClicked;
            this.btnLogin.Clicked       -= ViewMenuButtonClicked;
            this.btnSource.Clicked      -= ViewMenuButtonClicked;
            this.btnInstall.Clicked     -= ViewMenuButtonClicked;
            this.BindingContextChanged  -= ViewMenu_BindingContextChanged;
        }
    }

    #endregion 

    public ViewMenu()
	{
		InitializeComponent();
        EventSubscribe();
    }

    #region Private Methods

    static void UserLoggedChanged(BindableObject obj, object oldValue, object newValue)
    {
        var btn = (Button)obj;
        btn.Text = MainPage.UserAuthenticated ? MainPage.APP_LOGOUT : MainPage.APP_LOGIN;
    }

    private void ViewMenu_Unloaded(object? sender, EventArgs e)
    {
        this.EventSubscribe(false);
    }

    private void ViewMenu_BindingContextChanged(object? sender, EventArgs e)
    {
        this.btnLogin.Text = MainPage.UserAuthenticated ? MainPage.APP_LOGOUT : MainPage.APP_LOGIN;
    }

    private void ViewMenuButtonClicked(object? sender, EventArgs e)
    {
        if (OnViewMenuButtonClicked != null)
        {
            AppLayers.HandleMenuButtonStatus(this, sender);
            OnViewMenuButtonClicked(sender, e);
        }
        
    }

    #endregion

}