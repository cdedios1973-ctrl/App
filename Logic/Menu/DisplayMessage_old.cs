namespace App.Logic
{
    using System;
    using System.Reflection;

    using App.Enums;
    using App.Events;
    using App.Models;

    public sealed class DisplayMessage_
    {
        private const string DISPLAY_NOT_IMPLEMENTED = "Display method not implemented.";

        public static AppMessageHandler messageHandler = new AppMessageHandler();

        private struct ObjectToSend
        {
            public Type type;
            public string objId;
        }

        private DisplaType      _display;
        private object          _objDestination;
        private ObjectToSend    _objResult;

        #region Constructors

        public DisplayMessage_(DisplaType display)
        {
            this._display = display;
        }

        public DisplayMessage_(DisplaType display, object? objDestination = null) : this(display)
        {
            if (objDestination != null)
            {
                this._objDestination = objDestination;
                GetObjTextProperty();
            }
        }

        #endregion

        #region Properties

        private void GetObjTextProperty()
        {
            Type type = _objDestination.GetType(); //type of the object
            PropertyInfo pi = _objDestination.GetType().GetProperty("StyleId");
            var result = pi.GetValue(_objDestination); // name of the object.
                
            this._objResult.objId = (string)result;
            this._objResult.type = type;
        }

        #endregion

        #region Method Show => display messages, can return string values.

        public string Show(string sender, string message, bool confirmation = false)
        {
            string result = "";
            
            switch (this._display)
            {
                case DisplaType.Console:
                    if (message.Length > 0)
                        Console.WriteLine(message);
                    if (confirmation == true)
                        if (Console.ReadKey().Key == ConsoleKey.Y)
                            result = "Y";
                    break;
                case DisplaType.Mobile:
                    messageHandler.UpdateStatus += MessageHandler_UpdateStatus;
                    messageHandler.updateStatus(sender, message, "");
                    messageHandler.UpdateStatus -= MessageHandler_UpdateStatus;
                    break;
                default: throw new Exception(DISPLAY_NOT_IMPLEMENTED);
            }
            
            return result;
        }

        private void MessageHandler_UpdateStatus(object? sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        #endregion

    }
}
