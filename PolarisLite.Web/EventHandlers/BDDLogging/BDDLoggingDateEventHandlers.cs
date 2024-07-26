using PolarisLite.Web.Controls.EventHandlers;
using PolarisLite.Web.Events;

namespace Bellatrix.Web.Extensions.Controls.Controls.EventHandlers;

public class BDDLoggingDateEventHandlers : DateEventHandlers
{
    protected override void SettingDateEventHandler(object sender, ComponentActionEventArgs arg) => Logger.LogInfo($"Set '{arg.ActionValue}' into {arg.Element.FindStrategy.ToString()}");
}
