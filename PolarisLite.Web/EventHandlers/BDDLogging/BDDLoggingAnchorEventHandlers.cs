using PolarisLite.Web.Controls.EventHandlers;
using PolarisLite.Web.Events;

namespace Bellatrix.Web.Extensions.Controls.Controls.EventHandlers;

public class BDDLoggingAnchorEventHandlers : AnchorEventHandlers
{
    protected override void ClickedEventHandler(object sender, ComponentActionEventArgs arg)
    {
        Logger.LogInfo($"Click {arg.Element.FindStrategy.ToString()}");
        //ReportPortal.Shared.Context.Current.Log.Info($"BDD Click {arg.Element.FindStrategy.ToString()}");
    }
}