
using PolarisLite.Web.Components;
using PolarisLite.Web.Contracts;
using System.Web;

namespace PolarisLite.Web;

public class Image : Component, IComponentClick
{
    public virtual string Src => HttpUtility.HtmlDecode(HttpUtility.UrlDecode(GetAttribute("src")));
    public virtual int? Height => string.IsNullOrEmpty(GetAttribute("height")) ? null : int.Parse(GetAttribute("height"));

    public virtual int? Width => string.IsNullOrEmpty(GetAttribute("width")) ? null : int.Parse(GetAttribute("width"));

    public new void Click() => base.Click();
}