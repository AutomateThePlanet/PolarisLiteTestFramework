using PolarisLite.Web.Controls.EventHandlers;
using PolarisLite.Web.Events;

namespace PolarisLite.Web.Extensions.Controls.Controls.EventHandlers;

public class BDDLoggingTextFieldEventHandlers : TextFieldEventHandlers
{
    protected override void TextSetEventHandler(object sender, ComponentActionEventArgs arg)
    {
        Logger.LogInfo($"Type '{arg.ActionValue}' into {arg.Element.FindStrategy.ToString()}");
    }
}
