using System;

namespace ERP.Domain.Events;

public class AppMessageHandler
{
    public delegate void UpdateStatusEventHandler(object sender, UpdateStatusEventArgs e);
    public event EventHandler? UpdateStatus;

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
        EventHandler? handler = UpdateStatus;
        handler?.Invoke(this, e);
    }
}

public class UpdateStatusEventArgs : EventArgs
{
    public string Sender { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Status { get ; set; } = string.Empty;
    public bool Confirmation { get; set; } = false;
    public DateTime TimeReached { get; set; }

}
