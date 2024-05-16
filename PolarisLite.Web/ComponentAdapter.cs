using OpenQA.Selenium;
using PolarisLite.Core.Infrastructure;
using PolarisLite.Locators;

namespace PolarisLite.Web;

public class ComponentAdapter
{
    private readonly IWebElement _webElement;

    private Actions Actions => new Actions(WrappedDriver);
    public FindStrategy FindStrategy { get; internal set; }
    public IWebDriver WrappedDriver { get; internal set; }

    public string Text => _webElement?.Text;

    public bool? Enabled => _webElement?.Enabled;

    public bool? Displayed => _webElement?.Displayed;

    public IWebElement WrappedElement { get; internal set; }

    public void Click(bool waitToBeClickable = false)
    {
        if (waitToBeClickable)
        {
           WaitToBeClickable(FindStrategy);
        }

        _webElement?.Click();
    }

    public string GetAttribute(string attributeName)
    {
        return _webElement?.GetAttribute(attributeName);
    }

    public void TypeText(string text)
    {
        _webElement?.Clear();
        _webElement?.SendKeys(text);
    }

    public void Hover()
    {
        Actions.MoveToElement(_webElement).Perform();
    }

    private void WaitToBeClickable(FindStrategy findStrategy)
    {
        Wait.To.BeClickable().WaitUntil(WrappedElement, WrappedDriver, findStrategy.Convert());
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

    //public TComponent FindComponent<TComponent>(By locator)
    //   where TComponent : ComponentAdapter
    //{
    //    var nativeWebElement = WrappedDriver.FindElement(locator);
    //    var component = InstanceFactory.Create<TComponent>();
    //    component.FindStrategy = locator;
    //    component.WrappedDriver = WrappedDriver;
    //    component.WrappedElement = nativeWebElement;
    //    return component;
    //}

    //public List<TComponent> FindComponents<TComponent>(By locator)
    //    where TComponent : ComponentAdapter
    //{
    //    var nativeWebElements = WrappedDriver.FindElements(locator);
    //    var components = new List<TComponent>();
    //    foreach (var nativeWebElement in nativeWebElements)
    //    {
    //        var component = InstanceFactory.Create<TComponent>();
    //        component.FindStrategy = locator;
    //        component.WrappedDriver = WrappedDriver;
    //        component.WrappedElement = nativeWebElement;
    //        components.Add(component);
    //    }

    //    return components;
    //}
}
