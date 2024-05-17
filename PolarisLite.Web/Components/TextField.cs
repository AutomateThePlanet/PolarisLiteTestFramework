using System.Diagnostics;
using PolarisLite.Web.Contracts;

namespace PolarisLite.Web;

public class TextField : ComponentAdapter, IComponentText, IComponentDisabled, IComponentInnerText, IComponentInnerHtml, IComponentValue
{
    // size, minLength, maxLengt
    // TODO: move to interfaces
    public virtual bool? IsAutoComplete => GetAttribute("autocomplete") == "on";

    public virtual bool IsReadonly => !string.IsNullOrEmpty(GetAttribute("readonly"));

    public virtual bool IsRequired => !string.IsNullOrEmpty(GetAttribute("required"));

    public virtual string Placeholder => string.IsNullOrEmpty(GetAttribute("placeholder")) ? null : GetAttribute("placeholder");
}