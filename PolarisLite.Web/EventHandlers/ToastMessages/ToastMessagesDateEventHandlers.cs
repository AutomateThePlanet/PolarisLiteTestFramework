using PolarisLite.Web.Controls.EventHandlers;
using PolarisLite.Web.Events;
using PolarisLite.Web.Services;

namespace Bellatrix.Web.Extensions.Controls.Controls.EventHandlers;

public class ToastMessagesDateEventHandlers : DateEventHandlers
{
    protected override void SettingDateEventHandler(object sender, ComponentActionEventArgs arg)
    {
        new DriverAdapter().InfoToastMessage($"Set '{arg.ActionValue}' into {arg.Element.FindStrategy.ToString()}");
    }
}
