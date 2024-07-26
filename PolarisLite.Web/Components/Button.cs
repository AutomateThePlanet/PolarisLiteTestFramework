using PolarisLite.Web.Components;
using PolarisLite.Web.Contracts;
using PolarisLite.Web.Events;

namespace PolarisLite.Web;

public class Button : WebComponent, IComponentClick, IComponentDisabled, IComponentValue
{
    public static event EventHandler<ComponentActionEventArgs> Clicked;
    public new bool IsDisabled => base.IsDisabled;

    public new string Value => base.Value;

    public void Click() => base.Click(Clicked);
}