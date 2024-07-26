using PolarisLite.Web.Events;

namespace PolarisLite.Web.Controls.EventHandlers;

public class DateEventHandlers : ComponentEventHandlers
{
    public override void SubscribeToAll()
    {
        Date.DateSet += DateSetEventHandler;
    }

    public override void UnsubscribeToAll()
    {
        Date.DateSet -= DateSetEventHandler;
    }

    protected virtual void SettingDateEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void DateSetEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }
}
