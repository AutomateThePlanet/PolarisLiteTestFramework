using PolarisLite.Web.Components;
using PolarisLite.Web.Contracts;

namespace PolarisLite.Web;

public class CheckBox : ComponentAdapter, IComponentValue, IComponentDisabled, IComponentChecked
{
    public void Check(bool isChecked = true)
    {
        DefaultCheck(isChecked);
    }

    public void Uncheck()
    {
        DefaultUncheck();
    }

    public virtual bool IsChecked => WrappedElement.Selected;

    public new string Value => base.Value;

    public new bool IsDisabled => base.IsDisabled;

    protected virtual void DefaultCheck(bool isChecked = true)
    {
        if (isChecked && !WrappedElement.Selected || !isChecked && WrappedElement.Selected)
        {
            WrappedElement.Click();
        }
    }

    protected virtual void DefaultUncheck()
    {
        if (WrappedElement.Selected)
        {
            WrappedElement.Click();
        }
    }
}