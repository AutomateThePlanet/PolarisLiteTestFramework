using PolarisLite.Web.Events;

namespace PolarisLite.Web.Controls.EventHandlers;

public class SelectEventHandlers : ComponentEventHandlers
{
    public override void SubscribeToAll()
    {
        Select.Selected += SelectedEventHandler;
    }

    public override void UnsubscribeToAll()
    {
        Select.Selected -= SelectedEventHandler;
    }

    protected virtual void SelectedEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }
}
