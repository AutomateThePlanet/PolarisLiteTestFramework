using PolarisLite.Web.Controls.EventHandlers;
using PolarisLite.Web.Events;
using PolarisLite.Web.Services;

namespace Bellatrix.Web.Extensions.Controls.Controls.EventHandlers;

public class ToastMessagesEmailEventHandlers : EmailEventHandlers
{
    protected override void SettingEmailEventHandler(object sender, ComponentActionEventArgs arg)
    {
        new DriverAdapter().InfoToastMessage($"Type '{arg.ActionValue}' into {arg.Element.FindStrategy.ToString()}");
    }
}