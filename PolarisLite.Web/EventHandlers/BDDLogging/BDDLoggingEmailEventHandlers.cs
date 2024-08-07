using PolarisLite.Web.Controls.EventHandlers;
using PolarisLite.Web.Events;

namespace PolarisLite.Web.Extensions.Controls.Controls.EventHandlers;

public class BDDLoggingEmailEventHandlers : EmailEventHandlers
{
    protected override void SettingEmailEventHandler(object sender, ComponentActionEventArgs arg)
    {
        Logger.LogInfo($"Type '{arg.ActionValue}' into {arg.Element.FindStrategy.ToString()}");
    }
}