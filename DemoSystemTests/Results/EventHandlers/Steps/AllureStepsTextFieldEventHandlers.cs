using Allure.Net.Commons;
using PolarisLite.Web.Controls.EventHandlers;
using PolarisLite.Web.Events;

namespace Allure.EventHandlers;

public class AllureStepsTextFieldEventHandlers : TextFieldEventHandlers
{
    protected override void SettingTextEventHandler(object sender, ComponentActionEventArgs arg)
    {
        AllureApi.Step($"Type '{arg.ActionValue}' into {arg.Element.FindStrategy.ToString()}");
    }
}
