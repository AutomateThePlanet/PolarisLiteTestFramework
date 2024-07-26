using PolarisLite.Web.Events;

namespace PolarisLite.Web.Controls.EventHandlers;

public class EmailEventHandlers : ComponentEventHandlers
{
    public override void SubscribeToAll()
    {
        Email.EmailSet += EmailSetEventHandler;
    }

    public override void UnsubscribeToAll()
    {
        Email.EmailSet -= EmailSetEventHandler;
    }

    protected virtual void SettingEmailEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void EmailSetEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }
}
