using Allure.Net.Commons;
using PolarisLite.Web.Controls.EventHandlers;
using PolarisLite.Web.Events;

namespace Allure.EventHandlers;

public class AllureStepsEmailEventHandlers : EmailEventHandlers
{
    protected override void SettingEmailEventHandler(object sender, ComponentActionEventArgs arg)
    {
        AllureApi.Step($"Type '{arg.ActionValue}' into {arg.Element.FindStrategy.ToString()}");
    }
}