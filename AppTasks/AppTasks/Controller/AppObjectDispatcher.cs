using System;
using Microsoft.Maui.Handlers;

namespace AppTasks.Controller.Layers
{
    public static class AppObjectDispatcher
    {

        #region XAML Page : Return Button

        public static Button CreateButton(string Id, string text)
        {
            return new Button
            {
                StyleId = Id,
                Text = text,
                Background = Colors.OliveDrab,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
            };
        }

        public static Button CreateButton(string Id, string text, bool isDarkBlue)
        {
            //SemanticProperties.Hint="Install the application"
            //SemanticProperties.Hint="Change the application Database Source"
            //SemanticProperties.Hint="Demo users list"
            //SemanticProperties.Hint="Log into the application."
            return new Button
            {
                StyleId = Id,
                Text = text,
                Background = Colors.DarkBlue,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                MaximumHeightRequest = 35,
                WidthRequest = 65
            };
        }


        #endregion

        #region XAML Page : Return Label

        public static Label CreateLabel(string Id, string text)
        {
            return new Label
            {
                StyleId = Id,
                Text = text,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center
            };
        }

        #endregion

        #region XAML Page : Return Label

        public static BoxView CreateBoxView(string? Id)
        {
            return new BoxView
            {
                StyleId = "box" + Id,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Colors.LightSkyBlue
            };
        }

        #endregion


    }
}
