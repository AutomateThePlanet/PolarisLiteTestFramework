using PolarisLite.Core.Infrastructure;
using PolarisLite.Locators;
using PolarisLite.Web.Components;
using PolarisLite.Web.Plugins.BrowserExecution;

namespace PolarisLite.Web.Services;

public partial class DriverAdapter : IElementFindService
{
    public TComponent FindById<TComponent>(string id)
     where TComponent : WebComponent
    {
        return FindComponent<TComponent>(new IdFindStrategy(id));
    }

    public TComponent FindByIdContaining<TComponent>(string id)
       where TComponent : WebComponent
    {
        return FindComponent<TComponent>(new IdContainingFindStrategy(id));
    }

    public TComponent FindByXPath<TComponent>(string xpath)
      where TComponent : WebComponent
    {
        return FindComponent<TComponent>(new XPathFindStrategy(xpath));
    }

    public TComponent FindByCss<TComponent>(string css)
        where TComponent : WebComponent
    {
        return FindComponent<TComponent>(new CssFindStrategy(css));
    }

    public TComponent FindByClass<TComponent>(string value)
       where TComponent : WebComponent
    {
        return FindComponent<TComponent>(new ClassFindStrategy(value));
    }

    public List<TComponent> FindComponentsById<TComponent>(string id)
      where TComponent : WebComponent
    {
        return FindComponents<TComponent>(new IdFindStrategy(id));
    }

    public List<TComponent> FindComponentsByXPath<TComponent>(string xpath)
     where TComponent : WebComponent
    {
        return FindComponents<TComponent>(new XPathFindStrategy(xpath));
    }

    public List<TComponent> FindComponentsByCss<TComponent>(string css)
    where TComponent : WebComponent
    {
        return FindComponents<TComponent>(new CssFindStrategy(css));
    }

    public TComponent FindComponent<TComponent>(FindStrategy findStrategy)
        where TComponent : WebComponent
    {
        foreach (var waitStrategy in _waitStrategies)
        {
            waitStrategy.WaitUntil(_webDriver, _webDriver, findStrategy.Convert());
        }

        IWebElement nativeWebElement =
            _nativeElementFindService.Find(findStrategy);
        var component = InstanceFactory.Create<TComponent>();
        component.FindStrategy = findStrategy;
        component.WrappedDriver = _webDriver;
        component.WrappedElement = nativeWebElement;
        return component;
    }

    public List<TComponent> FindComponents<TComponent>(FindStrategy findStrategy)
        where TComponent : WebComponent
    {
        foreach (var waitStrategy in _waitStrategies)
        {
            waitStrategy.WaitUntil(_webDriver, _webDriver, findStrategy.Convert());
        }

        IEnumerable<IWebElement> nativeWebElements =
            _nativeElementFindService.FindAll(findStrategy);
        var components = new List<TComponent>();
        foreach (var nativeWebElement in nativeWebElements)
        {
            var component = InstanceFactory.Create<TComponent>();
            component.FindStrategy = findStrategy;
            component.WrappedDriver = _webDriver;
            component.WrappedElement = nativeWebElement;
            components.Add(component);
        }

        return components;
    }

    public TComponent FindByTag<TComponent>(string tag) where TComponent : WebComponent => throw new NotImplementedException();
    public TComponent FindByLinkText<TComponent>(string linkText) where TComponent : WebComponent => throw new NotImplementedException();
    public List<TComponent> FindAllById<TComponent>(string id) 
        where TComponent : WebComponent 
    {
        return FindComponents<TComponent>(new IdFindStrategy(id));
    }
    public List<TComponent> FindAllByXPath<TComponent>(string xpath) 
        where TComponent : WebComponent
    {
        return FindComponents<TComponent>(new XPathFindStrategy(xpath));
    }
    public List<TComponent> FindAllByTag<TComponent>(string tag) where TComponent : WebComponent => throw new NotImplementedException();
    public List<TComponent> FindAllByClass<TComponent>(string cssClass) where TComponent : WebComponent => throw new NotImplementedException();
    public List<TComponent> FindAllByCss<TComponent>(string css) where TComponent : WebComponent => throw new NotImplementedException();
    public List<TComponent> FindAllByLinkText<TComponent>(string linkText) where TComponent : WebComponent => throw new NotImplementedException();
    public List<TComponent> FindAll<TComponent>(FindStrategy findStrategy) where TComponent : WebComponent => throw new NotImplementedException();
    public TComponent Create<TComponent>(FindStrategy findStrategy) where TComponent : WebComponent => throw new NotImplementedException();
}
