using PolarisLite.Web.Controls.EventHandlers;
using PolarisLite.Web.Events;

namespace Bellatrix.Web.Extensions.Controls.Controls.EventHandlers;

public class BDDLoggingTextFieldEventHandlers : TextFieldEventHandlers
{
    protected override void SettingTextEventHandler(object sender, ComponentActionEventArgs arg)
    {
        Logger.LogInfo($"Type '{arg.ActionValue}' into {arg.Element.FindStrategy.ToString()}");
    }
}
