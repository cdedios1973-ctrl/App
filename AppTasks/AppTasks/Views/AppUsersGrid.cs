namespace AppTasks.Views;

public class AppUsersGrid : ContentView
{
    private static Grid grid;

    public AppUsersGrid()
    {
        grid = new Grid
        {
            RowDefinitions =
            {
                new RowDefinition{ Height = 40 }
            },
            ColumnDefinitions =
            {
                new ColumnDefinition{ Width = 260 },
                new ColumnDefinition{ Width = 40 },
                new ColumnDefinition{ Width = 60 }
            }
        };

        this.AddRow("Users", "");

        Content = grid;
    }

    public void AddRow(string column1, string column2, 
                string? btnName = null, string? btnText = null, EventHandler? e = null)
    {
        grid.RowDefinitions.Add(new RowDefinition());

        int i = grid.RowDefinitions.Count;

        Label label01 = new Label
        {
            Text = column1,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center
        };
        //Grid.SetRow(label, i);
        //Grid.SetColumnSpan(label, 2);

        Label label02 = new Label
        {
            Text = column2,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        grid.Add(label01, 0, i);
        grid.Add(label02, 1, i);

        if (btnName != null && btnText != null)
        {
            Button btn01 = new Button
            {
                StyleId = btnName,
                Text = btnText,
                Background = Colors.OliveDrab,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
            };
            btn01.Clicked += e;
            grid.Add(btn01, 2, i);
        }
        else
        {
            Label label03 = new Label
            {
                Text = "Action",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            grid.Add(label03, 2, i);
        }

        Content = grid;

    }

}