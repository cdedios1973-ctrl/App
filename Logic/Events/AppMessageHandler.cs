namespace App.Events
{
    using System;

    public class AppMessageHandler
    {
        public delegate void UpdateStatusEventHandler(object sender, UpdateStatusEventArgs e);
        public event EventHandler UpdateStatus;

        public void updateStatus(string sender, string message, string status, bool confirmation = false)
        {
            var data = new UpdateStatusEventArgs();

            data.Sender = sender;
            data.Message = message;
            data.Status = status;
            data.TimeReached = DateTime.Now;
            data.Confirmation = confirmation;

            OnUpdateStatus(data);
        }

        protected virtual void OnUpdateStatus(UpdateStatusEventArgs e)
        {
            EventHandler handler = UpdateStatus;
            handler?.Invoke(this, e);
        }
    }

    public class UpdateStatusEventArgs : EventArgs
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public string Status { get ; set; }
        public bool Confirmation { get; set; }
        public DateTime TimeReached { get; set; }

    }

}
