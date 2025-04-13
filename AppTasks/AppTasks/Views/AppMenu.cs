namespace AppTasks.Views;

public class AppMenu : ContentView
{
	public AppMenu()
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
				new Button 
                { 
                    StyleId = "btnInstall",
                    Text = "Install",
                    //SemanticProperties.Hint="Install the application"
                    Background = Colors.DarkBlue,
                    HorizontalOptions = LayoutOptions.Center, 
					VerticalOptions = LayoutOptions.Center,
                    MaximumHeightRequest= 35,
                    WidthRequest= 65
                },
                new Button
                {
                    StyleId = "btnSource",
                    Text = "Source",
                    //SemanticProperties.Hint="Change the application Database Source"
                    Background = Colors.DarkBlue,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    MaximumHeightRequest= 35,
                    WidthRequest= 65
                },
                new Button
                {
                    StyleId = "btnUsers",
                    Text = "Users",
                    //SemanticProperties.Hint="Demo users list"
                    Background = Colors.DarkBlue,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    MaximumHeightRequest= 35,
                    WidthRequest= 65
                },
                new Button
                {
                    StyleId = "btnLogin",
                    Text = str,
                    //SemanticProperties.Hint="Log into the application."
                    Background = Colors.DarkCyan,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    MaximumHeightRequest= 35,
                    WidthRequest= 65
                }
            }
        };
	}
}