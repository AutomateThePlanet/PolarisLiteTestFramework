using PolarisLite.Mobile.Contracts;

namespace PolarisLite.Mobile.Components;

public class TextField : AndroidComponent, IComponentDisabled, IComponentText
{
    public void SetText(string value)
    {
        base.TypeText(value);
    }

    public string Text => base.GetText();

    public new bool IsDisabled => base.IsDisabled();
}