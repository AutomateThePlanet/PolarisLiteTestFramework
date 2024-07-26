using PolarisLite.Core.Layout;
using PolarisLite.Locators;

namespace PolarisLite.Web.Contracts;
public interface IComponent : ILayoutComponent
{
    public IWebElement WrappedElement { get; set; }
    public FindStrategy FindStrategy { get; set; }

    public string GetAttribute(string attributeName);
}
