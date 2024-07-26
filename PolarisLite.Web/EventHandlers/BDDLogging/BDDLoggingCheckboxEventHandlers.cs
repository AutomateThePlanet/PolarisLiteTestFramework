using PolarisLite.Web.Controls.EventHandlers;
using PolarisLite.Web.Events;

namespace Bellatrix.Web.Extensions.Controls.Controls.EventHandlers;

public class BDDLoggingCheckboxEventHandlers : CheckboxEventHandlers
{
    protected override void CheckedEventHandler(object sender, ComponentActionEventArgs arg)
    {
        Logger.LogInfo($"Check {arg.Element.FindStrategy.ToString()}");
    }

    protected override void UncheckedEventHandler(object sender, ComponentActionEventArgs arg)
    {
        Logger.LogInfo($"Uncheck {arg.Element.FindStrategy.ToString()}");
    }
}
