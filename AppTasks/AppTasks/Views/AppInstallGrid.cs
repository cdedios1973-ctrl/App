namespace AppTasks.Views;
using global::App.Models;

public class AppInstallGrid : ContentView
{
    private static Grid grid;

    public AppInstallGrid()
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
        this.AddRow("Task", "Status", "Date");
        Content = grid;
    }

    public void AddRow(MessageEvent mEvent)
    {
        this.AddRow(mEvent.Message, mEvent.Status, mEvent.Time.ToShortDateString());
    }

    public void AddRow(string column1, string column2, string column3)
    {
        grid.RowDefinitions.Add(new RowDefinition());

        int i = grid.RowDefinitions.Count;

        Label label01 = new Label
        {
            Text = column1,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center
        };
        Label label02 = new Label
        {
            Text = column2,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        Label label03 = new Label
        {
            Text = column3,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        grid.Add(label01, 0, i);
        grid.Add(label02, 1, i);
        grid.Add(label03, 2, i);
        //Grid.SetRow(label, i);
        //Grid.SetColumnSpan(label, 2);

        Content = grid;

    }

}