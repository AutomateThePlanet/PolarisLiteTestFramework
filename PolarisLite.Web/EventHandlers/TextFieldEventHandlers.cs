using PolarisLite.Web.Events;

namespace PolarisLite.Web.Controls.EventHandlers;

public class TextFieldEventHandlers : ComponentEventHandlers
{
    public override void SubscribeToAll()
    {
        TextField.TextSet += TextSetEventHandler;
    }

    public override void UnsubscribeToAll()
    {
        TextField.TextSet -= TextSetEventHandler;
    }

    protected virtual void SettingTextEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void TextSetEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }
}
