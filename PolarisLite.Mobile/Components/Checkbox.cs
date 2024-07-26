using PolarisLite.Mobile.Contracts;

namespace PolarisLite.Mobile.Components;

public class CheckBox : AndroidComponent, IComponentDisabled, IComponentChecked, IComponentText
{
    public virtual void Check(bool isChecked = true)
    {
        bool isElementChecked = GetIsChecked();
        if (isChecked && !isElementChecked || !isChecked && isElementChecked)
        {
            Click();
        }
    }

    public virtual void Uncheck()
    {
        bool isChecked = GetIsChecked();
        if (isChecked)
        {
            Click();
        }
    }

    public string Text => base.GetText();

    public new bool IsDisabled => base.IsDisabled();

    public virtual bool IsChecked => GetIsChecked();
}