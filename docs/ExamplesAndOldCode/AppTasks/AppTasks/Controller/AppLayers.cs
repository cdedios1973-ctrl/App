using System;
using AppTasks.Views.Menu;
using Microsoft.Maui.Handlers;

namespace AppTasks.Controller.Layers
{
    public static class AppLayers
    {

        #region XAML Page (2.0) : Main menu button status

        public static void HandleMenuButtonStatus(ContentView cView, object sender)
        {
            Button btn = (Button)sender;
            if (btn != null)
            {
                btn.IsEnabled = !btn.IsEnabled;
                List <IVisualTreeElement> lItems = (List<IVisualTreeElement>)cView.GetVisualTreeDescendants();
                foreach (IVisualTreeElement item in lItems)
                    if (item is Button)
                    {
                        Button b = (Button)item;
                        if (b.StyleId != btn.StyleId)
                            b.IsEnabled = !btn.IsEnabled;
                    }
            }
        }


        #endregion

        #region XAML Page (2.0) : Get Objects (Horizontal / Vertical / Grid)

        public static VerticalStackLayout GetVerticalLayoutByName(ContentPage page, string layoutName)
        {
            return (VerticalStackLayout)page.FindByName(layoutName);
        }
        
        public static HorizontalStackLayout GetHorizonalLayoutByName(ContentPage page, string layoutName)
        {
            return (HorizontalStackLayout)page.FindByName(layoutName);
        }

        public static ListView GetListViewByName(ContentView cView, string layout, string iName)
        {
            VerticalStackLayout vLayout = (VerticalStackLayout)cView.FindByName(layout);
            if (vLayout != null)
            {
                List<IVisualTreeElement> lItems = (List<IVisualTreeElement>)vLayout.GetVisualTreeDescendants();
                foreach (IVisualTreeElement item in lItems)
                {
                    if (item is ListView)
                    {
                        ListView obj = (ListView)item;
                        if (obj.StyleId == iName)
                            return obj;
                    }
                }
            }
            return null;
        }

        #endregion


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
                if (layout.Children.Count > 0 && addChildren == false)
                {
                    ManageLayoutEvents(layout, true, ev);
                    layout.Children.Clear();
                }
                // add the new item.
                //AddContentViewToPage(page, vlDestination, view, view.HeightRequest, addChildren);

                //view.HeightRequest = view.HeightRequest;
                layout.BatchBegin();
                layout.Children.Add(view);
                layout.IsEnabled = true;
                layout.BatchCommit();

                ManageLayoutEvents(view, false, ev);
            }
        }

        public static void AddComponentB(ContentPage page, string stack, ContentView view, EventHandler ev)
        {
            //search layer to add items.
            StackLayout layout = (StackLayout)page.FindByName(stack);
            if (layout != null)
            {
                layout.HeightRequest = view.HeightRequest;

                // delete prevoius handlers
                if (layout.Children.Count > 0)
                {
                    ManageLayoutEvents(view, true, ev);
                    layout.Children.Clear();
                }
                // add the new item.
                //AddContentViewToPage(page, vlDestination, view, view.HeightRequest, addChildren);

                //view.HeightRequest = view.HeightRequest;
                layout.BatchBegin();
                layout.Children.Add(view);
                layout.IsEnabled = true;
                layout.BatchCommit();

                ManageLayoutEvents(view, false, ev);
            }
        }

        #endregion

        #region XAML Page : Enable / Disable component

        public static void DisableComponent(ContentPage page, string vlSource, bool enable = false)
        {
            VerticalStackLayout layout = (VerticalStackLayout)page.FindByName(vlSource);
            if (layout != null)
            {
                if (layout.Children.Count > 0)
                    layout.IsEnabled = enable;
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

        public static void ManageLayoutEvents(ContentView view, bool removeHandler, EventHandler eh)
        {
            List<IVisualTreeElement> listButton = (List<IVisualTreeElement>)view.GetVisualTreeDescendants();
            foreach (IVisualTreeElement button in listButton)
            {
                if (button is Button)
                {
                    Button bt = (Button)button;
                    if (removeHandler == false && eh != null)
                        bt.Clicked += eh; //OnButtonClicked;
                    else
                        bt.Clicked -= eh; //OnButtonClicked;
                }
            }
        }

        public static void ManageLayoutEvents(VerticalStackLayout layout, bool removeHandler, EventHandler eh)
        {
            List<IVisualTreeElement> listButton = (List<IVisualTreeElement>)layout.GetVisualTreeDescendants();
            foreach (IVisualTreeElement button in listButton)
            {
                if (button is Button)
                {
                    Button bt = (Button)button;
                    if (removeHandler == false && eh != null)
                        bt.Clicked += eh; //OnButtonClicked;
                    else
                        bt.Clicked -= eh; //OnButtonClicked;
                }
            }
        }


        #endregion

        #region XAML Page : Get/Set values from 'Entry' fields

        public static string GetEntryValue(VerticalStackLayout vLayout, string eName)
        {
            List<IVisualTreeElement> lTextbox = (List<IVisualTreeElement>)vLayout.GetVisualTreeDescendants();
            foreach (IVisualTreeElement textbox in lTextbox)
            {
                if (textbox is Entry)
                {
                    Entry txt = (Entry)textbox;
                    if (txt.StyleId == eName)
                        return txt.Text;
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

        #region XAML Page : Set values from 'Label'

        public static void SetLayoutLabelValue(ContentPage page, string layoutName, string eName, string value)
        {
            VerticalStackLayout layout = (VerticalStackLayout)page.FindByName(layoutName);
            if (layout != null)
            {
                List<IVisualTreeElement> lLabel = (List<IVisualTreeElement>)layout.GetVisualTreeDescendants();
                foreach (IVisualTreeElement label in lLabel)
                {
                    if (label is Label)
                    {
                        Label lbl = (Label)label;
                        if (lbl.StyleId == eName)
                            lbl.Text = value;
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

    }
}
