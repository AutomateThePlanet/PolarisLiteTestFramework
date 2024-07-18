using OpenQA.Selenium.Appium;
using PolarisLite.Core.Layout;

namespace PolarisLite.Mobile.Contracts;
public interface IComponent : ILayoutComponent
{
    public AppiumElement WrappedElement { get; internal set; }

    public string GetAttribute(string attributeName);
}
