using PolarisLite.Core.Infrastructure;
using PolarisLite.Locators;
using PolarisLite.Web.Components;

namespace PolarisLite.Web.Services;

public partial class DriverAdapter : IElementFindService
{
    public TComponent FindById<TComponent>(string id)
        where TComponent : WebComponent, new()
    {
        return FindComponent<TComponent>(new IdFindStrategy(id));
    }

    public TComponent FindByIdContaining<TComponent>(string id)
       where TComponent : WebComponent, new()
    {
        return FindComponent<TComponent>(new IdContainingFindStrategy(id));
    }

    public TComponent FindByXPath<TComponent>(string xpath)
      where TComponent : WebComponent, new()
    {
        return FindComponent<TComponent>(new XPathFindStrategy(xpath));
    }

    public TComponent FindByCss<TComponent>(string css)
        where TComponent : WebComponent, new()
    {
        return FindComponent<TComponent>(new CssFindStrategy(css));
    }

    public TComponent FindByClass<TComponent>(string value)
       where TComponent : WebComponent, new()
    {
        return FindComponent<TComponent>(new ClassFindStrategy(value));
    }

    public List<TComponent> FindComponentsById<TComponent>(string id)
      where TComponent : WebComponent, new()
    {
        return FindComponents<TComponent>(new IdFindStrategy(id));
    }

    public List<TComponent> FindComponentsByXPath<TComponent>(string xpath)
     where TComponent : WebComponent, new()
    {
        return FindComponents<TComponent>(new XPathFindStrategy(xpath));
    }

    public List<TComponent> FindComponentsByCss<TComponent>(string css)
    where TComponent : WebComponent, new()
    {
        return FindComponents<TComponent>(new CssFindStrategy(css));
    }

    public TComponent FindComponent<TComponent>(FindStrategy findStrategy)
        where TComponent : WebComponent, new()
    {
        var component = new TComponent();
        component.FindStrategy = findStrategy;
        component.WrappedDriver = WrappedDriver;
        return component;
    }

    public List<TComponent> FindComponents<TComponent>(FindStrategy findStrategy)
        where TComponent : WebComponent, new()
    {
        IEnumerable<IWebElement> nativeWebElements =
            WrappedDriver.FindElements(findStrategy.Convert());
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

    public TComponent FindByTag<TComponent>(string tag) where TComponent : WebComponent, new() => throw new NotImplementedException();
    public TComponent FindByLinkText<TComponent>(string linkText) where TComponent : WebComponent, new() => throw new NotImplementedException();
    public List<TComponent> FindAllById<TComponent>(string id) 
        where TComponent : WebComponent, new()
    {
        return FindComponents<TComponent>(new IdFindStrategy(id));
    }
    public List<TComponent> FindAllByXPath<TComponent>(string xpath) 
        where TComponent : WebComponent, new()
    {
        return FindComponents<TComponent>(new XPathFindStrategy(xpath));
    }
    public List<TComponent> FindAllByTag<TComponent>(string tag) where TComponent : WebComponent, new() => throw new NotImplementedException();
    public List<TComponent> FindAllByClass<TComponent>(string cssClass) where TComponent : WebComponent, new() => throw new NotImplementedException();
    public List<TComponent> FindAllByCss<TComponent>(string css) where TComponent : WebComponent, new() => throw new NotImplementedException();
    public List<TComponent> FindAllByLinkText<TComponent>(string linkText) where TComponent : WebComponent, new() => throw new NotImplementedException();
    public List<TComponent> FindAll<TComponent>(FindStrategy findStrategy) where TComponent : WebComponent, new() => throw new NotImplementedException();
    public TComponent Find<TComponent>(FindStrategy findStrategy) where TComponent : WebComponent, new() => throw new NotImplementedException();
}
