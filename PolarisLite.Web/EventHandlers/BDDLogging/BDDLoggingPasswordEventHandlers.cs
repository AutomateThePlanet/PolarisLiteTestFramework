using PolarisLite.Web.Controls.EventHandlers;
using PolarisLite.Web.Events;

namespace Bellatrix.Web.Extensions.Controls.Controls.EventHandlers;

public class BDDLoggingPasswordEventHandlers : PasswordEventHandlers
{
    protected override void PasswordSetEventHandler(object sender, ComponentActionEventArgs arg)
    {
        Logger.LogInfo($"Type '{arg.ActionValue}' into {arg.Element.FindStrategy.ToString()}");
    }
}