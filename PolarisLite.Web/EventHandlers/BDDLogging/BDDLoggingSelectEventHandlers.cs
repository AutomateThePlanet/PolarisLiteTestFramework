using PolarisLite.Web.Controls.EventHandlers;
using PolarisLite.Web.Events;

namespace PolarisLite.Web.Extensions.Controls.Controls.EventHandlers;

public class BDDLoggingSelectEventHandlers : SelectEventHandlers
{
    protected override void SelectedEventHandler(object sender, ComponentActionEventArgs arg)
    {
        Logger.LogInfo($"Select '{arg.ActionValue}' from {arg.Element.FindStrategy.ToString()}");
    }
}
