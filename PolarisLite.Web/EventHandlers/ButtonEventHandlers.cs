using PolarisLite.Web.Events;

namespace PolarisLite.Web.Controls.EventHandlers;

public class ButtonEventHandlers : ComponentEventHandlers
{
    public override void SubscribeToAll()
    {
        Button.Clicked += ClickedEventHandler;
    }

    public override void UnsubscribeToAll()
    {
        Button.Clicked -= ClickedEventHandler;
    }

    protected virtual void ClickedEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }
}
