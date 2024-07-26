using PolarisLite.Web.Components;
using PolarisLite.Web.Contracts;
using PolarisLite.Web.Events;

namespace PolarisLite.Web;

public class Password : WebComponent, IComponentDisabled, IComponentValue
{
    public static event EventHandler<ComponentActionEventArgs> PasswordSet;
    public virtual string GetPassword()
    {
        return GetAttribute("value");
    }

    public virtual void SetPassword(string password)
    {
        WrappedElement?.Clear();
        WrappedElement?.SendKeys(password);

        PasswordSet?.Invoke(this, new ComponentActionEventArgs(this));
    }

    public virtual bool? IsAutoComplete => GetAttribute("autocomplete") == "on";

    public virtual bool IsReadonly => !string.IsNullOrEmpty(GetAttribute("readonly"));

    public virtual bool IsRequired => !string.IsNullOrEmpty(GetAttribute("required"));

    public virtual string Placeholder => string.IsNullOrEmpty(GetAttribute("placeholder")) ? null : GetAttribute("placeholder");

    public new bool IsDisabled => base.IsDisabled;

    public new string Value => base.Value;
}