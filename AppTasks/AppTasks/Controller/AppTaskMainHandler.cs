using System;

using AppTasks.Views;
using AppTasks.Resources;

using App.Enums;
using App.Logic;
using App.Logic.IO.Handler;

namespace AppTasks.Controller
{
    public class AppTaskMainHandler
    {
        private ContentPage _page;
        private EventHandler _eventHandler;

        public AppTaskMainHandler(ContentPage page, EventHandler eventHandler)
        {
            this._page = page;
            this._eventHandler = eventHandler;
        }

        #region Status : Layer user actions

        public void Status(bool logged, bool dbExist)
        {
            AppLayers.RemoveComponent(_page, "layerButtons", "layerButtons", _eventHandler);
            AppLayers.RemoveComponent(_page, "layerSubmenu", "layerSubmenu", _eventHandler);
            AppLayers.RemoveComponent(_page, "layerDetail", "layerDetail", _eventHandler);

            if (dbExist == false)
                this.ActionInstall(ref dbExist);
            else if (logged == false)
                AppLayers.AddComponent(_page, "layerMaster", new Welcome(), _eventHandler);
            else if(logged == true)
                AppLayers.AddComponent(_page, "layerMaster", new AppSearchBar(), _eventHandler);
        }

        #endregion

        #region Action : Show Login menu && Action : Logout 

        public void ActionLogin(ref AppMenuHandler appMenu)
        {
            AppLayers.RemoveComponent(_page, "layerButtons", "layerButtons", _eventHandler);
            AppLayers.RemoveComponent(_page, "layerDetail", "layerDetail", _eventHandler);
            AppLayers.AddComponent(_page, "layerMaster", new AppLogin(), _eventHandler);
        }

        #endregion

        #region Action : Login

        public bool ActionLoginAs(ref AppMenuHandler appMenu, string input)
        {
            if (appMenu == null)
                appMenu = new AppMenuHandler(new UserAccess(DataBaseType.SqLite, DisplaType.Mobile));

            if (InputString.CheckInput(ref input) == true)
                return appMenu.DoLogin(input);
            else
                return false;
        }

        #endregion

        #region Action : Install Database

        public void ActionInstall(ref bool dbExist, Label? lbl = null)
        {
            if (dbExist == true)
            {
                AppLayers.AddComponent(_page, "layerMaster", new AppConfirmation(), _eventHandler);
                if(lbl != null)
                    lbl.Text = "Atention! Database already exist.";
            }
            else
                AppLayers.RemoveComponent(_page, "layerMaster", "layerMaster", _eventHandler);
            
            AppLayers.RemoveComponent(_page, "layerButtons", "layerButtons", _eventHandler);
            AppLayers.AddComponent(_page, "layerDetail", new AppInstallGrid(), _eventHandler);
        }

        #endregion

        #region Action : Show Options menu

        public void ActionOptions()
        {
            bool hasItem = AppLayers.SearchVLayoutChildrenByName(_page, "layerSubmenu");
            if (hasItem == false)
                AppLayers.AddComponent(_page, "layerSubmenu", new AppMenu(), _eventHandler);
            else
                AppLayers.RemoveComponent(_page, "layerSubmenu", "layerSubmenu", _eventHandler);
        }

        #endregion

    }
}
