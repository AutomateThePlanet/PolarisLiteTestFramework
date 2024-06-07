using PolarisLite.Mobile.Contracts;

namespace PolarisLite.Mobile.Components;

public class RadioButton : AndroidComponent, IComponentDisabled, IComponentChecked, IComponentText
{
    public new bool IsDisabled => base.IsDisabled();
    public virtual bool IsChecked => GetIsChecked();

    public string Text => base.GetText();

    public new void Click()
    {
        base.Click();
    }
}