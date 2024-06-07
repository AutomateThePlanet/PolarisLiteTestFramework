using OpenQA.Selenium.Appium;

namespace PolarisLite.Mobile.Contracts;
public interface IComponent
{
    public AppiumElement WrappedElement { get; internal set; }

    public string GetAttribute(string attributeName);
}
