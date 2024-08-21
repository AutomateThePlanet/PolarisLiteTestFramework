using PolarisLite.Web.Controls.EventHandlers;
using PolarisLite.Web.Events;

namespace Bellatrix.Web.Extensions.Controls.Controls.EventHandlers;

public class BDDLoggingTextFieldEventHandlers : TextFieldEventHandlers
{
    protected override void TextSetEventHandler(object sender, ComponentActionEventArgs arg)
    {
        Logger.LogInfo($"Type '{arg.ActionValue}' into {arg.Element.FindStrategy.ToString()}");
        //ReportPortal.Shared.Context.Current.Log.Info($"BDD Type '{arg.ActionValue}' into {arg.Element.FindStrategy.ToString()}");
    }
}
