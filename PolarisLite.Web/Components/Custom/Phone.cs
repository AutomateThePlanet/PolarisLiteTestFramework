using PolarisLite.Web.Components;
using PolarisLite.Web.Contracts;

namespace PolarisLite.Web;

public class Phone : WebComponent, IComponentDisabled, IComponentValue, IComponentPhone, IComponentIsAutoComplete, IComponentIsReadonly, IComponentIsRequired, IComponentMaxLength, IComponentMinLength, IComponentSize, IComponentPlaceholder
{
    public virtual string GetPhone()
    {
        return base.GetAttribute("value");
    }

    public virtual void SetPhone(string phone)
    {
        base.SetAttribute("value", phone);
    }

    public new void Hover()
    {
        base.Hover();
    }

    public virtual bool? IsAutoComplete => GetAttribute("autocomplete") == "on";

    public virtual bool IsReadonly => !string.IsNullOrEmpty(GetAttribute("readonly"));

    public virtual bool IsRequired => !string.IsNullOrEmpty(GetAttribute("required"));

    public virtual string Placeholder => string.IsNullOrEmpty(GetAttribute("placeholder")) ? null : GetAttribute("placeholder");

    public new bool IsDisabled => base.IsDisabled;

    public new string Value => base.Value;

    public virtual int? MaxLength => string.IsNullOrEmpty(GetAttribute("minlength")) ? null : (int?)int.Parse(GetAttribute("minlength"));

    public virtual int? MinLength => string.IsNullOrEmpty(GetAttribute("minlength")) ? null : (int?)int.Parse(GetAttribute("minlength"));

    public virtual int? Size => string.IsNullOrEmpty(GetAttribute("size")) ? null : (int?)int.Parse(GetAttribute("size"));
}