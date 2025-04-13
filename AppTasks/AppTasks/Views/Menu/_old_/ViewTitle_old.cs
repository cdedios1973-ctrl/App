using AppTasks.Controller.Layers;

namespace AppTasks.Views;

public class ViewTitle_old : ContentView
{
	public ViewTitle_old(string module)
	{
        /*
		Content = new HorizontalStackLayout
		{
			StyleId = "layerBtnSubMenu",
			Spacing = 3,
			HeightRequest = 40,
			VerticalOptions = LayoutOptions.Center,

			Children =
			{
				AppObjectDispatcher.CreateLabel("lblTitle", "Module : "),
				AppObjectDispatcher.CreateLabel("lblModule", module),
				AppObjectDispatcher.CreateButton("btnNo", "Back")
			}
		};
        */
        Grid grid = this.createGrid();

        grid.Add(AppObjectDispatcher.CreateLabel("lblTitle", "Module : "), 0, 0);
        grid.Add(AppObjectDispatcher.CreateLabel("lblModule", module), 1, 0);
        grid.Add(AppObjectDispatcher.CreateButton("btnBack", "Back"), 2, 0);

        Content = grid;

    }

    private Grid createGrid()
    {
        return new Grid
        {
            RowDefinitions =
            {
                new RowDefinition{ Height = 38 }
            },
            ColumnDefinitions =
            {
                new ColumnDefinition{ Width = 60 },
                new ColumnDefinition{ Width = 160 },
                new ColumnDefinition()
            }
        };
    }

}