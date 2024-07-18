using PolarisLite.Core.Layout;

namespace PolarisLite.Web.Contracts;
public interface IComponent : ILayoutComponent
{
    public IWebElement WrappedElement { get; internal set; }

    public string GetAttribute(string attributeName);
}
