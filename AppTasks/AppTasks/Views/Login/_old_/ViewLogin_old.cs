using System.Data;

namespace AppTasks.Views;

public class ViewLogin_old : ContentView
{
    public ViewLogin_old()
    {
        VerticalStackLayout layout = new VerticalStackLayout
        {
            StyleId = "layoutLogin",
            Spacing = 5
        };

        Grid grid = this.CreateGrid();
        grid.Add(createEntry("txtUser", "Enter username"), 0, 0);
        grid.Add(createEntry("txtPass", "Enter password", true), 0, 1);
        layout.Children.Add(grid);

        Content = layout;
    }

    public ViewLogin_old(string detail)
    {
        VerticalStackLayout layout = new VerticalStackLayout
        {
            StyleId = "layoutLoginB",
            Spacing = 5
        };

        Grid grid = this.CreateButtonGrid();
        grid.Add(createButton("btnBack", "Back"), 1, 0);
        grid.Add(createButton("btnSigIn", "Sign up"), 2, 0);
        layout.Children.Add(grid);

        Content = layout;
    }

    #region Object : Grid data and buttons

    private Grid CreateGrid()
    {
        return new Grid
        {
            StyleId = "gridLogin",

            RowDefinitions =
            {
                new RowDefinition{ Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition{ Height = new GridLength(1, GridUnitType.Star) },
            },
            ColumnDefinitions =
            {
                new ColumnDefinition()
            }
        };
    }

    private Grid CreateButtonGrid()
    {
        return new Grid
        {
            StyleId = "gridLogin",

            RowDefinitions =
            {
                new RowDefinition{ Height = new GridLength(1, GridUnitType.Star) },
            },
            ColumnDefinitions =
            {
                new ColumnDefinition(),
                new ColumnDefinition(),
                new ColumnDefinition(),
                new ColumnDefinition()
            }
        };
    }

    #endregion

    #region Object : Create entry, normal and password

    private Entry createEntry(string styleId, string placeHolder)
    {
        return new Entry
        {
            StyleId = styleId,
            Placeholder = placeHolder,
            FontSize = 18,
            WidthRequest = 200,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
    }

    private Entry createEntry(string styleId, string placeHolder, bool isPassword)
    {
        return new Entry
        {
            StyleId = styleId,
            Placeholder = placeHolder,
            FontSize = 18,
            WidthRequest = 200,
            IsPassword = true,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
    }

    #endregion

    #region Object : Create Accept and cancel

    private Button createButton(string styleId, string text)
    {
        return new Button
        {
            StyleId = styleId,
            Text = text,
            FontSize = 14,
            //SemanticProperties.Hint = "Log In into the application.",
            //BackgroundColor = Colors.Crimson,
            WidthRequest = 90,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
    }

    #endregion

}