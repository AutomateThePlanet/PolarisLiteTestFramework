using PolarisLite.Web.Events;

namespace PolarisLite.Web.Controls.EventHandlers;

public class AnchorEventHandlers : ComponentEventHandlers
{
    public override void SubscribeToAll()
    {
        Anchor.Clicked += ClickedEventHandler;
    }

    public override void UnsubscribeToAll()
    {
        Anchor.Clicked -= ClickedEventHandler;
    }

    protected virtual void ClickedEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }
}
