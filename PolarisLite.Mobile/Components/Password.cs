using PolarisLite.Mobile.Contracts;

namespace PolarisLite.Mobile.Components;

public class Password : AndroidComponent, IComponentDisabled
{
    public virtual string GetPassword()
    {
        return GetText();
    }

    public virtual void SetPassword(string password)
    {
        base.TypeText(password);
    }

    public new bool IsDisabled => base.IsDisabled();
}