using System;

namespace App.Models.Events
{
    public sealed class MessageEvent
    {
        private string _message = "";
        private string _status = "";
        private DateTime _time = DateTime.Now;

        private MessageEvent() { }

        public MessageEvent(string message, string status, DateTime time)
        {
            this._message = message;
            this._status = status;
            this._time = time;
        }

        public string Message 
        {
            get { return this._message; }
            set { this._message = value; } 
        }
        public string Status 
        { 
            get { return this._status; }
            set { this._status = value; }
        }
        public DateTime Time 
        {
            get { return this._time; }
            set { this._time = value; }
        }

    }
}
