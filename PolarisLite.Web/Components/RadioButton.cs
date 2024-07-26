using System.Diagnostics;
using PolarisLite.Web.Components;
using PolarisLite.Web.Contracts;
using PolarisLite.Web.Events;

namespace PolarisLite.Web;

public class RadioButton : WebComponent, IComponentDisabled, IComponentValue, IComponentClick
{
    public static event EventHandler<ComponentActionEventArgs> Clicked;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsChecked => WrappedElement.Selected;

    public new bool IsDisabled => base.IsDisabled;

    public new string Value => base.Value;

    public void Click() => base.Click(Clicked);
}