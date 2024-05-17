using PolarisLite.Web.Contracts;

namespace PolarisLite.Web;

public class Password : ComponentAdapter, IComponentDisabled, IComponentValue
{
    public virtual string GetPassword()
    {
        return GetAttribute("value");
    }

    public virtual void SetPassword(string password)
    {
        WrappedElement?.Clear();
        WrappedElement?.SendKeys(password);
    }

    public virtual bool? IsAutoComplete => GetAttribute("autocomplete") == "on";

    public virtual bool IsReadonly => !string.IsNullOrEmpty(GetAttribute("readonly"));

    public virtual bool IsRequired => !string.IsNullOrEmpty(GetAttribute("required"));

    public virtual string Placeholder => string.IsNullOrEmpty(GetAttribute("placeholder")) ? null : GetAttribute("placeholder");
}