using PolarisLite.Core;
using PolarisLite.Web.Components;
using PolarisLite.Web.Contracts;
using PolarisLite.Web.Events;

namespace PolarisLite.Web;

public class TextField : WebComponent, IComponentText, IComponentDisabled, IComponentInnerText, IComponentInnerHtml, IComponentValue
{
    public static event EventHandler<ComponentActionEventArgs> TextSet;
    
    //private static readonly ThreadLocal<EventHandler<ComponentActionEventArgs>> _textSet = new ThreadLocal<EventHandler<ComponentActionEventArgs>>();

    //public static event EventHandler<ComponentActionEventArgs> TextSet
    //{
    //    add
    //    {
    //        if (!_textSet.IsValueCreated)
    //        {
    //            _textSet.Value = value;
    //        }
    //    }
    //    remove
    //    {
    //        if (_textSet.IsValueCreated)
    //        {
    //            _textSet.Value -= value;
    //        }
    //    }
    //}

    public virtual bool? IsAutoComplete => GetAttribute("autocomplete") == "on";
    public virtual bool IsReadonly => !string.IsNullOrEmpty(GetAttribute("readonly"));
    public virtual bool IsRequired => !string.IsNullOrEmpty(GetAttribute("required"));
    public virtual string Placeholder => string.IsNullOrEmpty(GetAttribute("placeholder")) ? null : GetAttribute("placeholder");
    public new bool IsDisabled => base.IsDisabled;
    public new string Text => base.Text;
    public new string InnerHtml => base.InnerHtml;
    public new string Value => base.Value;

    public void TypeText(string text, InfoType infoType = InfoType.INFO)
    {
        base.TypeText(text, TextSet, infoType);
    }
}
