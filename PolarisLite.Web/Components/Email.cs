using PolarisLite.Web.Components;
using PolarisLite.Web.Contracts;
using PolarisLite.Web.Events;

namespace PolarisLite.Web;

public class Email : WebComponent, IComponentDisabled, IComponentValue
{
    public static event EventHandler<ComponentActionEventArgs> EmailSet;
    public virtual string GetEmail()
    {
        return GetAttribute("value");
    }

    public virtual void SetEmail(string email)
    {
        WrappedElement?.Clear();
        WrappedElement?.SendKeys(email);
        EmailSet?.Invoke(this, new ComponentActionEventArgs(this));
    }

    public virtual bool? IsAutoComplete => GetAttribute("autocomplete") == "on";

    public virtual bool IsReadonly => !string.IsNullOrEmpty(GetAttribute("readonly"));

    public virtual bool IsRequired => !string.IsNullOrEmpty(GetAttribute("required"));

    public virtual string Placeholder => string.IsNullOrEmpty(GetAttribute("placeholder")) ? null : GetAttribute("placeholder");

    public new bool IsDisabled => base.IsDisabled;

    public new string Value => base.Value;
}