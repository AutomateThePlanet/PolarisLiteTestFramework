using PolarisLite.Mobile.Components;
using PolarisLite.Mobile.Contracts;

namespace PolarisLite.Mobile;

public class Button : AndroidComponent, IComponentDisabled, IComponentText
{
    public new void Click()
    {
        base.Click();
    }

    public string Text => base.GetText();

    public new void TypeText(string text)
    {
        base.TypeText(text);
    }

    public new bool IsDisabled => base.IsDisabled();
}