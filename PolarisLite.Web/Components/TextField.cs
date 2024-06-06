using PolarisLite.Web.Components;
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

    public new bool IsDisabled => base.IsDisabled;

    public new string Text => base.Text;

    public new string InnerHtml => base.InnerHtml;

    public new string Value => base.Value;

    public new void TypeText(string text) => base.TypeText(text);
}