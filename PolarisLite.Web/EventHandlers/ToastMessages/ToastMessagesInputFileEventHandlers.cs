using PolarisLite.Web.Controls.EventHandlers;
using PolarisLite.Web.Events;
using PolarisLite.Web.Services;

namespace Bellatrix.Web.Extensions.Controls.Controls.EventHandlers;

public class ToastMessagesInputFileEventHandlers : InputFileEventHandlers
{
    protected override void UploadedEventHandler(object sender, ComponentActionEventArgs arg)
    {
        new DriverAdapter().InfoToastMessage($"I upload '{arg.ActionValue}' for {arg.Element.FindStrategy.ToString()}");
    }
}