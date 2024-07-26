using PolarisLite.Web.Controls.EventHandlers;
using PolarisLite.Web.Events;
using PolarisLite.Web.Services;

namespace Bellatrix.Web.Extensions.Controls.Controls.EventHandlers;

public class ToastMessagesSelectEventHandlers : SelectEventHandlers
{
    protected override void SelectedEventHandler(object sender, ComponentActionEventArgs arg)
    {
        new DriverAdapter().InfoToastMessage($"Select '{arg.ActionValue}' from {arg.Element.FindStrategy.ToString()}");
    }
}
