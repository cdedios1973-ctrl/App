namespace AppTasks.Views;

using AppTasks.Controller.Layers;
using global::App.Models;

public class ViewInstall : ContentView
{
    public Grid grid;

    public ViewInstall()
    {
        grid = this.createGrid();
        this.AddRow("Task", "Status", "Date");

        Content = grid;
    }

    private Grid createGrid()
    { 
        return new Grid
        {
            RowDefinitions =
            {
                new RowDefinition{ Height = 40 }
            },
            ColumnDefinitions =
            {
                new ColumnDefinition{ Width = 255 },
                new ColumnDefinition{ Width = 40 },
                new ColumnDefinition{ Width = 65 }
            }
        };
    }

    public void AddRow(MessageEvent mEvent)
    {
        this.AddRow(mEvent.Message, mEvent.Status, mEvent.Time.ToShortDateString());
    }

    public void AddRow(string column1, string column2, string column3)
    {
        grid.RowDefinitions.Add(new RowDefinition());

        int i = grid.RowDefinitions.Count;

        Label label01 = AppObjectDispatcher.CreateLabel("lbl" + column1, column1);
        Label label02 = AppObjectDispatcher.CreateLabel("lbl" + column2, column2);
        Label label03 = AppObjectDispatcher.CreateLabel("lbl" + column3, column3);
        grid.Add(label01, 0, i);
        grid.Add(label02, 1, i);
        grid.Add(label03, 2, i);
        //Grid.SetRow(label, i);
        //Grid.SetColumnSpan(label, 2);

        Content = grid;

    }

}