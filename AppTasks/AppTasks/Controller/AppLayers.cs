using Microsoft.Maui.Handlers;
using System;
using System.Collections.Generic;

namespace AppTasks.Controller
{
    public static class AppLayers
    {

        #region XAML Page : Add content to page

        public static void AddComponent(ContentPage page, string vlDestination, ContentView view,
                        EventHandler ev, bool addChildren = false)
        {
            //search layer to add items.
            VerticalStackLayout layout = (VerticalStackLayout)page.FindByName(vlDestination);
            if (layout != null)
            {
                layout.HeightRequest = view.HeightRequest;

                // delete prevoius handlers
                if (layout.Children.Count > 0)
                    ManageLayoutEvents(layout, true, ev);

                // add new item.
                AddContentViewToPage(page, vlDestination, view, view.HeightRequest, addChildren);
                ManageLayoutEvents(layout, false, ev);
            }
        }

        private static void AddContentViewToPage(ContentPage page, string xName, ContentView item, double heightRequest = 50, bool addChildren = false)
        {
            VerticalStackLayout layout = (VerticalStackLayout)page.FindByName(xName);
            if (layout != null)
            {
                if (addChildren == false)
                    layout.Children.Clear();

                item.HeightRequest = heightRequest;

                layout.BatchBegin();
                layout.Children.Add(item);
                layout.IsEnabled = true;
                layout.BatchCommit();
            }
        }

        #endregion

        #region XAML Page : Remove content from page

        public static void RemoveComponent(ContentPage page, string vlSource, string xName, EventHandler eh)
        {
            VerticalStackLayout layout = (VerticalStackLayout)page.FindByName(vlSource);
            if (layout != null)
            {
                if (layout.Children.Count > 0)
                    ManageLayoutEvents(layout, true, eh);

                layout.Children.Clear();
            }
        }

        #endregion

        #region XAML Page : Manage buttons event to added or removed XAML

        public static void ManageLayoutEvents(VerticalStackLayout layout, bool hasElements, EventHandler eh)
        {
            List<IVisualTreeElement> listButton = (List<IVisualTreeElement>)layout.GetVisualTreeDescendants();
            foreach (IVisualTreeElement button in listButton)
            {
                if (button is Button)
                {
                    Button bt = (Button)button;
                    if (hasElements == false && eh != null)
                        bt.Clicked += eh; //OnButtonClicked;
                    else
                        bt.Clicked -= eh; //OnButtonClicked;
                }
            }
        }

        #endregion

        #region XAML Page : Get/Set values from 'Entry' fields

        public static string GetLayoutEntryValue(ContentPage page, string layoutName, string eName)
        {
            VerticalStackLayout layout = (VerticalStackLayout)page.FindByName(layoutName);
            if (layout != null)
            {
                List<IVisualTreeElement> lTextbox = (List<IVisualTreeElement>)layout.GetVisualTreeDescendants();
                foreach (IVisualTreeElement textbox in lTextbox)
                {
                    if (textbox is Entry)
                    {
                        Entry txt = (Entry)textbox;
                        if (txt.StyleId == eName)
                            return txt.Text;
                    }
                }
            }
            return "";
        }

        public static void SetLayoutEntryValue(ContentPage page, string layoutName, string eName, string value)
        {
            VerticalStackLayout layout = (VerticalStackLayout)page.FindByName(layoutName);
            if (layout != null)
            {
                List<IVisualTreeElement> lTextbox = (List<IVisualTreeElement>)layout.GetVisualTreeDescendants();
                foreach (IVisualTreeElement textbox in lTextbox)
                {
                    if (textbox is Entry)
                    {
                        Entry txt = (Entry)textbox;
                        if (txt.StyleId == eName)
                            txt.Text = value;
                    }
                }
            }
        }


        #endregion

        #region XAML Page : Return ActivityIndicator

        public static ActivityIndicator? GetLayoutActivity(ContentPage page, string layoutName)
        {
            VerticalStackLayout layout = (VerticalStackLayout)page.FindByName(layoutName);
            if (layout != null)
            {
                List<IVisualTreeElement> lact = (List<IVisualTreeElement>)layout.GetVisualTreeDescendants();
                foreach (IVisualTreeElement act in lact)
                {
                    if (act is ActivityIndicator)
                        return (ActivityIndicator)act;
                }
            }
            return null;
            //return null;
        }

        #endregion

        #region XAML Page : Search if vertical layout has clildrens

        public static bool SearchVLayoutChildrenByName(ContentPage page, string xName)
        {
            VerticalStackLayout layout = (VerticalStackLayout)page.FindByName(xName);
            return (layout.Children.Count > 0) ? true : false;
        }

        #endregion

        #region Deprecated

        /*
        public static void RemoveElementFromPage(ContentPage page, string xName)
        {
            VerticalStackLayout layout = (VerticalStackLayout)page.FindByName(xName);
            if (layout != null)
                layout.Children.Clear();
        }
        */

        #endregion

    }
}
