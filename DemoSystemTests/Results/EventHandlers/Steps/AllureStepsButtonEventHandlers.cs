using Allure.Net.Commons;
using PolarisLite.Web.Controls.EventHandlers;
using PolarisLite.Web.Events;

namespace Allure.EventHandlers;

public class AllureStepsButtonEventHandlers : ButtonEventHandlers
{
    protected override void ClickedEventHandler(object sender, ComponentActionEventArgs arg)
    {
        AllureApi.Step($"Click {arg.Element.FindStrategy.ToString()}");
    }
}
