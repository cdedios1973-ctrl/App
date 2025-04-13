using AppTasks.Controller.Layers;

namespace AppTasks.Views;

public class ViewMenu_old : ContentView
{
	public ViewMenu_old()
	{
        string str = MainPage.UserAuthenticated == false ? MainPage.APP_LOGIN : MainPage.APP_LOGOUT;

		Content = new HorizontalStackLayout
		{
			StyleId = "layerBtnSubMenu",
			Spacing = 3,
			HeightRequest = 40,
			VerticalOptions = LayoutOptions.Center,

            Children = 
			{
                AppObjectDispatcher.CreateButton("btnInstall", "Install", true),
                AppObjectDispatcher.CreateButton("btnSource", "Source", true),
                AppObjectDispatcher.CreateButton("btnUsers", "Users", true),
                AppObjectDispatcher.CreateButton("btnLogin", str, true)
            }
        };
	}
}