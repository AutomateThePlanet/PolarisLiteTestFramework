using PolarisLite.Web.Controls.EventHandlers;
using PolarisLite.Web.Events;

namespace PolarisLite.Web.Extensions.Controls.Controls.EventHandlers;

public class BDDLoggingInputFileEventHandlers : InputFileEventHandlers
{
    protected override void UploadedEventHandler(object sender, ComponentActionEventArgs arg)
    {
        Logger.LogInfo($"I upload '{arg.ActionValue}' for {arg.Element.FindStrategy.ToString()}");
    }
}