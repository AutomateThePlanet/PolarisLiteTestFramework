using PolarisLite.Core.Infrastructure;
using PolarisLite.Locators;
using PolarisLite.Web.Contracts;
using PolarisLite.Web.Services;

namespace PolarisLite.Web.Components;

public class Component : IComponent, IComponentVisible
{
    private readonly IWebElement _webElement;

    public Component()
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

    public TComponent FindById<TComponent>(string id)
        where TComponent : Component
    {
        return FindComponent<TComponent>(new IdFindStrategy(id));
    }

    public TComponent FindByIdContaining<TComponent>(string id)
       where TComponent : Component
    {
        return FindComponent<TComponent>(new IdContainingFindStrategy(id));
    }

    public TComponent FindByXPath<TComponent>(string xpath)
      where TComponent : Component
    {
        return FindComponent<TComponent>(new XPathFindStrategy(xpath));
    }

    public TComponent FindByCss<TComponent>(string css)
        where TComponent : Component
    {
        return FindComponent<TComponent>(new CssFindStrategy(css));
    }

    public TComponent FindByClass<TComponent>(string value)
       where TComponent : Component
    {
        return FindComponent<TComponent>(new ClassFindStrategy(value));
    }

    public List<TComponent> FindComponentsById<TComponent>(string id)
      where TComponent : Component
    {
        return FindComponents<TComponent>(new IdFindStrategy(id));
    }

    public List<TComponent> FindComponentsByXPath<TComponent>(string xpath)
     where TComponent : Component
    {
        return FindComponents<TComponent>(new XPathFindStrategy(xpath));
    }

    public List<TComponent> FindComponentsByCss<TComponent>(string css)
    where TComponent : Component
    {
        return FindComponents<TComponent>(new CssFindStrategy(css));
    }

    public TComponent FindByTag<TComponent>(string tag) where TComponent : Component => throw new NotImplementedException();
    public TComponent FindByLinkText<TComponent>(string linkText) where TComponent : Component => throw new NotImplementedException();
    public List<TComponent> FindAllById<TComponent>(string id) where TComponent : Component => throw new NotImplementedException();
    public List<TComponent> FindAllByXPath<TComponent>(string xpath) where TComponent : Component => throw new NotImplementedException();
    public List<TComponent> FindAllByTag<TComponent>(string tag) where TComponent : Component => throw new NotImplementedException();
    public List<TComponent> FindAllByClass<TComponent>(string cssClass) where TComponent : Component => throw new NotImplementedException();
    public List<TComponent> FindAllByCss<TComponent>(string css) where TComponent : Component => throw new NotImplementedException();
    public List<TComponent> FindAllByLinkText<TComponent>(string linkText) where TComponent : Component => throw new NotImplementedException();
    public List<TComponent> FindAll<TComponent>(FindStrategy findStrategy) where TComponent : Component => throw new NotImplementedException();
    public TComponent Create<TComponent>(FindStrategy findStrategy) where TComponent : Component => throw new NotImplementedException();

    public TComponent FindComponent<TComponent>(FindStrategy findStrategy)
      where TComponent : Component
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
        where TComponent : Component
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

    public bool IsVisible => WrappedElement.Displayed;
    protected bool IsDisabled => bool.Parse(GetAttribute("disabled"));
    protected string Href => GetAttribute("href");
    protected string InnerHtml => GetAttribute("innerHTML");
    protected string Text => WrappedElement?.Text;
    protected string Value => GetAttribute("value");
}