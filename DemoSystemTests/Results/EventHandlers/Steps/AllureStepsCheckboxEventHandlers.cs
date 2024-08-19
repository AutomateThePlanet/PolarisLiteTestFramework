using Allure.Net.Commons;
using PolarisLite.Web.Controls.EventHandlers;
using PolarisLite.Web.Events;

namespace Allure.EventHandlers;

public class AllureStepsCheckboxEventHandlers : CheckboxEventHandlers
{
    protected override void CheckedEventHandler(object sender, ComponentActionEventArgs arg)
    {
        AllureApi.Step($"Check {arg.Element.FindStrategy.ToString()}");
    }

    protected override void UncheckedEventHandler(object sender, ComponentActionEventArgs arg)
    {
        AllureApi.Step($"Uncheck {arg.Element.FindStrategy.ToString()}");
    }
}
