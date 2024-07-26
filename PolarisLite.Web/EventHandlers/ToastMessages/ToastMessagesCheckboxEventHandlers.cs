using PolarisLite.Web.Controls.EventHandlers;
using PolarisLite.Web.Events;
using PolarisLite.Web.Services;

namespace Bellatrix.Web.Extensions.Controls.Controls.EventHandlers;

public class ToastMessagesCheckboxEventHandlers : CheckboxEventHandlers
{
    protected override void CheckedEventHandler(object sender, ComponentActionEventArgs arg)
    {
        new DriverAdapter().InfoToastMessage($"Check {arg.Element.FindStrategy.ToString()}");
    }

    protected override void UncheckedEventHandler(object sender, ComponentActionEventArgs arg)
    {
        new DriverAdapter().InfoToastMessage($"Uncheck {arg.Element.FindStrategy.ToString()}");
    }
}
