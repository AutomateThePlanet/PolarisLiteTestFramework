using PolarisLite.Web.Components;
using PolarisLite.Web.Contracts;
using PolarisLite.Web.Events;

namespace PolarisLite.Web;

public class Anchor : WebComponent, IComponentInnerHtml, IComponentHref, IComponentClick
{
    public static event EventHandler<ComponentActionEventArgs> Clicked;
    public new string InnerHtml => base.InnerHtml;

    public new string Href => base.Href;

    public void Click() => base.Click(Clicked);
}