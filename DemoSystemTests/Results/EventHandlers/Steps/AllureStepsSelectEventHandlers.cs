using Allure.Net.Commons;
using PolarisLite.Web.Controls.EventHandlers;
using PolarisLite.Web.Events;

namespace Allure.EventHandlers;

public class AllureStepsSelectEventHandlers : SelectEventHandlers
{
    protected override void SelectedEventHandler(object sender, ComponentActionEventArgs arg)
    {
        AllureApi.Step($"Select '{arg.ActionValue}' from {arg.Element.FindStrategy.ToString()}");
    }
}
