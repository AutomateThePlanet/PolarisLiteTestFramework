using OpenQA.Selenium;
using PolarisLite.Core.Infrastructure;
using PolarisLite.Locators;
using PolarisLite.Web.Contracts;

namespace PolarisLite.Web;

public class ComponentAdapter : IComponent
{
    private readonly IWebElement _webElement;

    public ComponentAdapter()
    {
    }

    private Actions Actions => new Actions(WrappedDriver);
    public FindStrategy FindStrategy { get; internal set; }
    public IWebDriver WrappedDriver { get; internal set; }
    public IJavaScriptExecutor JavaScriptExecutor => (IJavaScriptExecutor)WrappedDriver;

    public bool? Enabled => _webElement?.Enabled;

    public bool? Displayed => _webElement?.Displayed;

    public IWebElement WrappedElement { get; set; }

    public void Hover()
    {
        Actions.MoveToElement(_webElement).Perform();
    }

    public string GetAttribute(string attributeName)
    {
        return WrappedElement?.GetAttribute(attributeName);
    }

    public void SetAttribute(string name, string value)
    {
        ((IJavaScriptExecutor)WrappedDriver).ExecuteScript($"arguments[0].setAttribute('{name}', '{value}');", WrappedElement);
    }

    public TComponent FindComponent<TComponent>(FindStrategy findStrategy)
      where TComponent : ComponentAdapter
    {
        var componentFindService = new NativeElementFindService(WrappedElement, WrappedDriver);
        var nativeWebElement = componentFindService.Find(findStrategy);
        var component = InstanceFactory.Create<TComponent>();
        component.FindStrategy = findStrategy;
        component.WrappedDriver = WrappedDriver;
        component.WrappedElement = nativeWebElement;
        return component;
    }

    public List<TComponent> FindComponents<TComponent>(FindStrategy findStrategy)
        where TComponent : ComponentAdapter
    {
        var componentFindService = new NativeElementFindService(WrappedElement, WrappedDriver);
        var nativeWebElements = componentFindService.FindAll(findStrategy);
        var components = new List<TComponent>();
        foreach (var nativeWebElement in nativeWebElements)
        {
            var component = InstanceFactory.Create<TComponent>();
            component.FindStrategy = findStrategy;
            component.WrappedDriver = WrappedDriver;
            component.WrappedElement = nativeWebElement;
            components.Add(component);
        }

        return components;
    }

    protected void Click()
    {
        WrappedElement?.Click();
    }

    protected void TypeText(string text)
    {
        WrappedElement?.Clear();
        WrappedElement?.SendKeys(text);
    }

    protected bool IsDisabled => bool.Parse(GetAttribute("disabled"));
    protected string Href => GetAttribute("href");
    protected string InnerHtml => GetAttribute("innerHTML");
    protected string Text => WrappedElement?.Text;
    protected string Value => GetAttribute("value");
}