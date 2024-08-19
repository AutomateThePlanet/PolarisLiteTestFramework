using Allure.Net.Commons;
using PolarisLite.Web.Controls.EventHandlers;
using PolarisLite.Web.Events;

namespace Allure.EventHandlers;

public class AllureStepsDateEventHandlers : DateEventHandlers
{
    protected override void SettingDateEventHandler(object sender, ComponentActionEventArgs arg)
    {
        AllureApi.Step($"Set '{arg.ActionValue}' into {arg.Element.FindStrategy.ToString()}");
    }
}
