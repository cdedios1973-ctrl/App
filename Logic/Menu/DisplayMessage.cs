namespace App.Logic
{
    using System;
    using App.Enums;
    using App.Events;

    public static class DisplayMessage
    {
        private const string DISPLAY_NOT_IMPLEMENTED = "Display method not implemented.";

        public static AppMessageHandler messageHandler = new AppMessageHandler();
        private static DisplaType _display;

        #region Properties

        public static DisplaType Displaytype
        { 
            set { _display = value; }
        }

        #endregion

        #region Method Show => display messages, can return string values.

        public static string Show(string sender, string message, bool confirmation = false)
        {
            string result = "";
            //messageHandler.UpdateStatus += MessageHandler_UpdateStatus;
            switch (_display)
            {
                case DisplaType.Console:
                    {
                        if (message.Length > 0)
                            Console.WriteLine(message);
                        if (confirmation == true)
                            if (Console.ReadKey().Key == ConsoleKey.Y)
                                result = "Y";
                        break;
                    }
                case DisplaType.Mobile:
                    {
                        messageHandler.updateStatus(sender, message, "");
                        break;
                    }
                default:
                    {
                        messageHandler.updateStatus(sender, DISPLAY_NOT_IMPLEMENTED, "");
                        break;
                    }
            }
            //messageHandler.UpdateStatus -= MessageHandler_UpdateStatus;
            return result;
        }

        #endregion

    }
}
