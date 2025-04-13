using System;
using App.Logic.IO.Handler;
using AppTasks.Views;
using AppTasks.Views.Menu;

namespace AppTasks.Controller.Layers
{
    public abstract class LayerMaster
    {
        internal const string APP_ERROR_FIND_OBJECT = "Couldn't find Object {0}";


        internal ContentPage _page;
        internal EventHandler _event;

        #region Constructors

        protected internal LayerMaster() { }

        protected internal LayerMaster(ContentPage page) 
        {
            this._page = page;
        }

        protected internal LayerMaster(ContentPage page, EventHandler? eventHandler) : this(page)
        {
            this._event = eventHandler;
        }

        #endregion

        #region Action : Show menu

        protected internal virtual void Show()
        {
            throw new Exception("Not Implemented");
        }

        #endregion

        #region Action : Destroy

        protected internal virtual void Destroy()
        {
            AppLayers.RemoveComponent(_page, "layerMaster", "layerMaster", _event);
            //AppLayers.RemoveComponent(_page, "layerDetail", "layerDetail", _event);
            AppLayers.RemoveComponent(_page, "layerActions", "layerActions", _event);
        }

        #endregion

        #region Action : Show welcome

        protected internal virtual void Action()
        {
            AppLayers.AddComponent(_page, "layerMaster", new Welcome(), _event);
            //AppLayers.RemoveComponent(_page, "layerDetail", "layerDetail", _event);
            AppLayers.RemoveComponent(_page, "layerActions", "layerActions", _event);
        }

        #endregion

        #region Action : Back

        protected internal virtual void ActionBack()
        {
            /*
            this.Destroy();

            if (MainPage.UserAuthenticated == false)
                AppLayers.AddComponent(_page, "layerMaster", new Welcome(), _event);
            else
            {
                // TODO : create LayerUserPermission
                ViewUsersPermission list = new ViewUsersPermission(AppMenuHandler.GetCurrentUser); //, _event);
                AppLayers.AddComponent(_page, "layerMaster", new AppSearchBar(), _event);
                AppLayers.AddComponent(_page, "layerDetail", list, _event);
            }
            */
        }

        #endregion

    }
}
