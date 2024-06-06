using System.Diagnostics;
using PolarisLite.Web.Components;
using PolarisLite.Web.Contracts;

namespace PolarisLite.Web;

public class RadioButton : ComponentAdapter, IComponentDisabled, IComponentValue, IComponentClick
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsChecked => WrappedElement.Selected;

    public new bool IsDisabled => base.IsDisabled;

    public new string Value => base.Value;

    public new void Click() => base.Click();
}