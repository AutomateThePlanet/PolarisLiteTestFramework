using PolarisLite.Web.Components;
using PolarisLite.Web.Contracts;

namespace PolarisLite.Web;

public class Anchor : ComponentAdapter, IComponentInnerHtml, IComponentHref, IComponentClick
{
    public new string InnerHtml => base.InnerHtml;

    public new string Href => base.Href;

    public new void Click() => base.Click();
}