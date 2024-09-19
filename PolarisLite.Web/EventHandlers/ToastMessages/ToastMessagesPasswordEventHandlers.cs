using PolarisLite.Web.Controls.EventHandlers;
using PolarisLite.Web.Events;
using PolarisLite.Web.Services;

namespace Bellatrix.Web.Extensions.Controls.Controls.EventHandlers;

public class ToastMessagesPasswordEventHandlers : PasswordEventHandlers
{
    protected override void PasswordSetEventHandler(object sender, ComponentActionEventArgs arg)
    {
        new DriverAdapter().InfoToastMessage($"Type \"{arg.ActionValue}\" into {arg.Element.FindStrategy.ToString()}");
    }
}