using PolarisLite.Web.Contracts;
using System.Diagnostics;

namespace PolarisLite.Web;

public class CheckBox : ComponentAdapter, IComponentValue
{
    public void Check(bool isChecked = true)
    {
        DefaultCheck(isChecked);
    }

    public void Uncheck()
    {
        DefaultUncheck();
    }

    //public string Value => this.GetValue(WrappedElement);

    //public bool IsDisabled => bool.Parse(GetAttribute("disabled"));

    public virtual bool IsChecked => WrappedElement.Selected;

    protected virtual void DefaultCheck(bool isChecked = true)
    {
        if (isChecked && !WrappedElement.Selected || !isChecked && WrappedElement.Selected)
        {
            //Click(true);
        }
    }

    protected virtual void DefaultUncheck()
    {
        if (WrappedElement.Selected)
        {
            //Click(true);
        }
    }
}