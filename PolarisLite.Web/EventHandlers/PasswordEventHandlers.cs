using PolarisLite.Web.Events;

namespace PolarisLite.Web.Controls.EventHandlers;

public class PasswordEventHandlers : ComponentEventHandlers
{
    public override void SubscribeToAll()
    {
        Password.PasswordSet += PasswordSetEventHandler;
    }

    public override void UnsubscribeToAll()
    {
        Password.PasswordSet -= PasswordSetEventHandler;
    }

    protected virtual void SettingPasswordEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void PasswordSetEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }
}
