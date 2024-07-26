
using PolarisLite.Web.Components;
using PolarisLite.Web.Contracts;
using PolarisLite.Web.Events;
using System.Web;

namespace PolarisLite.Web;

public class Image : WebComponent, IComponentClick
{
    public static event EventHandler<ComponentActionEventArgs> Clicked;
    public virtual string Src => HttpUtility.HtmlDecode(HttpUtility.UrlDecode(GetAttribute("src")));
    public virtual int? Height => string.IsNullOrEmpty(GetAttribute("height")) ? null : int.Parse(GetAttribute("height"));

    public virtual int? Width => string.IsNullOrEmpty(GetAttribute("width")) ? null : int.Parse(GetAttribute("width"));

    public void Click() => base.Click(Clicked);
}