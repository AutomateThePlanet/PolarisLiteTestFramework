using PolarisLite.Mobile.Components;

namespace PolarisLite.Mobile.Services;

public partial class DriverAdapter : IElementFindService
{
    public TComponent FindById<TComponent>(string id) where TComponent : AndroidComponent, new()
    {
        return FindComponent<TComponent>(new IdFindStrategy(id));
    }

    public TComponent FindByIdContaining<TComponent>(string id) where TComponent : AndroidComponent, new()
    {
        return FindComponent<TComponent>(new IdContainingFindStrategy(id));
    }

    public TComponent FindByXPath<TComponent>(string xpath) where TComponent : AndroidComponent, new()
    {
        return FindComponent<TComponent>(new XPathFindStrategy(xpath));
    }

    public TComponent FindByDescriptionContaining<TComponent>(string description) where TComponent : AndroidComponent, new()
    {
        return FindComponent<TComponent>(new DescriptionContainingFindStrategy(description));
    }

    public TComponent FindByAndroidUIAutomator<TComponent>(string uiAutomatorExpression) where TComponent : AndroidComponent, new()
    {
        return FindComponent<TComponent>(new AndroidUIAutomatorFindStrategy(uiAutomatorExpression));
    }

    public List<TComponent> FindComponentsById<TComponent>(string id) where TComponent : AndroidComponent, new()
    {
        return FindComponents<TComponent>(new IdFindStrategy(id));
    }

    public List<TComponent> FindComponentsByXPath<TComponent>(string xpath) where TComponent : AndroidComponent, new()
    {
        return FindComponents<TComponent>(new XPathFindStrategy(xpath));
    }

    public List<TComponent> FindComponentsByDescriptionContaining<TComponent>(string description) where TComponent : AndroidComponent, new()
    {
        return FindComponents<TComponent>(new DescriptionContainingFindStrategy(description));
    }

    public List<TComponent> FindComponentsByAndroidUIAutomator<TComponent>(string uiAutomatorExpression) where TComponent : AndroidComponent, new()
    {
        return FindComponents<TComponent>(new AndroidUIAutomatorFindStrategy(uiAutomatorExpression));
    }

    public TComponent FindComponent<TComponent>(FindStrategy findStrategy)
        where TComponent : AndroidComponent, new()
    {
        var component = new TComponent();
        component.FindStrategy = findStrategy;
        component.WrappedDriver = _androidDriver;
        return component;
    }

    public List<TComponent> FindComponents<TComponent>(FindStrategy findStrategy) where TComponent : AndroidComponent, new()
    {
        IEnumerable<AppiumElement> nativeWebElements = findStrategy.FindAllElements(_androidDriver);
        var components = new List<TComponent>();
        foreach (var nativeWebElement in nativeWebElements)
        {
            var component = new TComponent();
            component.FindStrategy = findStrategy;
            component.WrappedDriver = _androidDriver;
            component.WrappedElement = nativeWebElement;
            components.Add(component);
        }

        return components;
    }

    // Placeholder methods that throw NotImplementedException
    public TComponent FindByTag<TComponent>(string tag) where TComponent : AndroidComponent, new() => throw new NotImplementedException();
    public TComponent FindByLinkText<TComponent>(string linkText) where TComponent : AndroidComponent, new() => throw new NotImplementedException();

    public List<TComponent> FindAllById<TComponent>(string id) where TComponent : AndroidComponent, new()
    {
        return FindComponents<TComponent>(new IdFindStrategy(id));
    }

    public List<TComponent> FindAllByXPath<TComponent>(string xpath) where TComponent : AndroidComponent, new()
    {
        return FindComponents<TComponent>(new XPathFindStrategy(xpath));
    }

    public TComponent FindByClass<TComponent>(string className) where TComponent : AndroidComponent, new() => throw new NotImplementedException();
    public List<TComponent> FindAllByTag<TComponent>(string tag) where TComponent : AndroidComponent, new() => throw new NotImplementedException();
    public List<TComponent> FindAllByClass<TComponent>(string cssClass) where TComponent : AndroidComponent, new() => throw new NotImplementedException();
    public List<TComponent> FindAll<TComponent>(FindStrategy findStrategy) where TComponent : AndroidComponent, new() => throw new NotImplementedException();
    public TComponent FindByTextContaining<TComponent>(string text) 
        where TComponent : AndroidComponent, new()
    {
        return FindComponent<TComponent>(new TextContainingFindStrategy(text));
    }

    public List<TComponent> FindComponentsByTextContaining<TComponent>(string text)
        where TComponent : AndroidComponent, new()
    {
        return FindComponents<TComponent>(new TextContainingFindStrategy(text));
    }
}
