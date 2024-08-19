using Allure.Net.Commons;
using PolarisLite.Web.Controls.EventHandlers;
using PolarisLite.Web.Events;

namespace Allure.EventHandlers;

public class AllureStepsInputFileEventHandlers : InputFileEventHandlers
{
    protected override void UploadedEventHandler(object sender, ComponentActionEventArgs arg)
    {
        AllureApi.Step($"I upload '{arg.ActionValue}' for {arg.Element.FindStrategy.ToString()}");
    }
}