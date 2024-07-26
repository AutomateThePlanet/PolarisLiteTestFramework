using PolarisLite.Web.Events;

namespace PolarisLite.Web.Controls.EventHandlers;

public class CheckboxEventHandlers : ComponentEventHandlers
{
    public override void SubscribeToAll()
    {
        CheckBox.Checked += CheckedEventHandler;
        CheckBox.Unchecked += UncheckedEventHandler;
    }

    public override void UnsubscribeToAll()
    {
        CheckBox.Checked -= CheckedEventHandler;
        CheckBox.Unchecked -= UncheckedEventHandler;
    }

    protected virtual void UncheckedEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void CheckedEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }
}
