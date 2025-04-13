using System.Data;
using App.Models;
using App.DAO;
using App.Logic;
using AppTasks.Controller.Layers;

namespace AppTasks.Views;

public class ViewUsersPermission : ContentView
{
    public ViewUsersPermission(UserAccess user) //, EventHandler ev)
    {
        if (user.IsUserAuthenticated == false)
            throw new Exception(MainPage.APP_LOGIN_ERROR);

        VerticalStackLayout layout = new VerticalStackLayout
        {
            StyleId = "layoutUsersPer",
            Spacing = 6
        };

        layout.Children.Add(AddHeader(ref user));

        ScrollView sview = new ScrollView
        {
            StyleId = "svUsersPer",
            MaximumHeightRequest = 260,
            Content = AddRows(user.GetUserPermissions) //, ev)
        };

        layout.Children.Add(sview);

        Content = layout;
    }

    #region Grid : Create grid

    private Grid CreateGrid()
    {
        return new Grid
        {
            RowDefinitions =
            {
                new RowDefinition{ Height = 40 }
            },
            ColumnDefinitions =
            {
                new ColumnDefinition{ Width = 30 }, // Id
                new ColumnDefinition{ Width = 30 }, // UserId
                new ColumnDefinition{ Width = 80 }, // UserName
                new ColumnDefinition{ Width = 30 }, // PermissionId
                new ColumnDefinition{ Width = 110 },// PermissionName
                new ColumnDefinition{ Width = 50 }, // userAccessType
                new ColumnDefinition{ Width = 40 }  // Action
            }
        };
    }

    #endregion

    #region Grid : Create header

    private Grid AddHeader(ref UserAccess user)
    {
        Grid grid = CreateGrid();

        DataTable dt = new DAOSchemas<UsersPermission>(ref user).GetColums;
        
        grid.RowDefinitions.Add(new RowDefinition());

        int i = 0;
        foreach (DataColumn col in dt.Columns)
        {
            grid.Add(AppObjectDispatcher.CreateLabel("lbl" + col.ColumnName, col.ColumnName), i, 0);
            i++;
        }
        grid.Add(AppObjectDispatcher.CreateLabel("lblClick", "Click"), i++, 0);

        return grid;
    }

    #endregion

    #region Grid : Rows management

    private Grid AddRows(List<UsersPermission> permissions)//, EventHandler ev)
    {
        Grid grid = CreateGrid();
        foreach (UsersPermission per in permissions)
        {
            AddRow(ref grid, per);//, ev);
        }

        return grid;
    }

    private void AddRow(ref Grid grid, UsersPermission data)//, EventHandler? e = null)
    {
        grid.RowDefinitions.Add(new RowDefinition());

        int i = grid.RowDefinitions.Count;

        grid.Add(AppObjectDispatcher.CreateLabel("lblup00", data.Id.ToString()), 0, i);
        grid.Add(AppObjectDispatcher.CreateLabel("lblup01", data.UserId.ToString()), 1, i);
        grid.Add(AppObjectDispatcher.CreateLabel("lblup02", data.UserName), 2, i);
        grid.Add(AppObjectDispatcher.CreateLabel("lblup03", data.PermissionId.ToString()), 3, i);
        grid.Add(AppObjectDispatcher.CreateLabel("lblup04", data.PermissionName), 4, i);
        grid.Add(AppObjectDispatcher.CreateLabel("lblup05", data.userAccessType.ToString()), 5, i);

        Button btn01 = new Button
        {
            StyleId = data.PermissionId.ToString(),
            Text = "+",
            Background = Colors.OliveDrab,
            MaximumHeightRequest = 32,
            MaximumWidthRequest = 40,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        //btn01.Clicked += e;
        grid.Add(btn01, 6, i);

        Content = grid;
    }

    #endregion

}