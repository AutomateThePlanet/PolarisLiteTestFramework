using PolarisLite.Web.Events;

namespace PolarisLite.Web.Controls.EventHandlers;

public class InputFileEventHandlers : ComponentEventHandlers
{
    public override void SubscribeToAll()
    {
        InputFile.Uploaded += UploadedEventHandler;
    }

    public override void UnsubscribeToAll()
    {
        InputFile.Uploaded -= UploadedEventHandler;
    }

    protected virtual void UploadingEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void UploadedEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }
}
