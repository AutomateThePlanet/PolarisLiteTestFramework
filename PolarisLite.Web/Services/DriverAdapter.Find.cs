using PolarisLite.Core.Infrastructure;
using PolarisLite.Locators;
using PolarisLite.Web.Components;
using PolarisLite.Web.Plugins.BrowserExecution;

namespace PolarisLite.Web.Services;

public partial class DriverAdapter : IElementFindService
{
    public TComponent FindById<TComponent>(string id)
     where TComponent : ComponentAdapter
    {
        return FindComponent<TComponent>(new IdFindStrategy(id));
    }

    public TComponent FindByIdContaining<TComponent>(string id)
       where TComponent : ComponentAdapter
    {
        return FindComponent<TComponent>(new IdContainingFindStrategy(id));
    }

    public TComponent FindByXPath<TComponent>(string xpath)
      where TComponent : ComponentAdapter
    {
        return FindComponent<TComponent>(new XPathFindStrategy(xpath));
    }

    public TComponent FindByCss<TComponent>(string css)
        where TComponent : ComponentAdapter
    {
        return FindComponent<TComponent>(new CssFindStrategy(css));
    }

    public TComponent FindByClass<TComponent>(string value)
       where TComponent : ComponentAdapter
    {
        return FindComponent<TComponent>(new ClassFindStrategy(value));
    }

    public List<TComponent> FindComponentsById<TComponent>(string id)
      where TComponent : ComponentAdapter
    {
        return FindComponents<TComponent>(new IdFindStrategy(id));
    }

    public List<TComponent> FindComponentsByXPath<TComponent>(string xpath)
     where TComponent : ComponentAdapter
    {
        return FindComponents<TComponent>(new XPathFindStrategy(xpath));
    }

    public List<TComponent> FindComponentsByCss<TComponent>(string css)
    where TComponent : ComponentAdapter
    {
        return FindComponents<TComponent>(new CssFindStrategy(css));
    }

    public TComponent FindComponent<TComponent>(FindStrategy findStrategy)
        where TComponent : ComponentAdapter
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
        where TComponent : ComponentAdapter
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
}
