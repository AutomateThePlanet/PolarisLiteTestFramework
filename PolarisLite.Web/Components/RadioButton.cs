using System.Diagnostics;
using PolarisLite.Web.Contracts;

namespace PolarisLite.Web;

public class RadioButton : ComponentAdapter, IComponentDisabled, IComponentValue, IComponentClick
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsChecked => WrappedElement.Selected;
}