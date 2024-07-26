using PolarisLite.Web.Components;
using PolarisLite.Web.Contracts;
using PolarisLite.Web.Events;

namespace PolarisLite.Web;

public class CheckBox : WebComponent, IComponentValue, IComponentDisabled, IComponentChecked
{
    public static event EventHandler<ComponentActionEventArgs> Checked;
    public static event EventHandler<ComponentActionEventArgs> Unchecked;

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

    protected virtual void DefaultCheck(bool isChecked = true, EventHandler<ComponentActionEventArgs> check = null)
    {
        if (isChecked && !WrappedElement.Selected || !isChecked && WrappedElement.Selected)
        {
            WrappedElement.Click();
            check?.Invoke(this, new ComponentActionEventArgs(this));
        }
    }

    protected virtual void DefaultUncheck(EventHandler<ComponentActionEventArgs> uncheck = null)
    {
        if (WrappedElement.Selected)
        {
            WrappedElement.Click();
            uncheck?.Invoke(this, new ComponentActionEventArgs(this));
        }
    }
}