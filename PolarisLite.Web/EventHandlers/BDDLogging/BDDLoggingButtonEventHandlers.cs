using PolarisLite.Web.Controls.EventHandlers;
using PolarisLite.Web.Events;

namespace PolarisLite.Web.Extensions.Controls.Controls.EventHandlers;

public class BDDLoggingButtonEventHandlers : ButtonEventHandlers
{
    protected override void ClickedEventHandler(object sender, ComponentActionEventArgs arg)
    {
        Logger.LogInfo($"Click {arg.Element.FindStrategy.ToString()}");
    }
}
