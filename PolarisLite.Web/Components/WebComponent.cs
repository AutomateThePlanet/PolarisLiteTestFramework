using PolarisLite.Core;
using PolarisLite.Core.Infrastructure;
using PolarisLite.Locators;
using PolarisLite.Web.Contracts;
using PolarisLite.Web.Plugins;
using PolarisLite.Web.Services;

namespace PolarisLite.Web.Components;

public class WebComponent : IComponent, IComponentVisible
{
    private IWebElement _wrappedWebElement;
    private readonly List<WaitStrategy> waitStrategies;
<<<<<<< Updated upstream
    private static readonly List<WebComponentPlugin> _plugins = new List<WebComponentPlugin>();
=======
    private static readonly List<WebComponentPlugin> _plugins = new();
>>>>>>> Stashed changes

    public WebComponent()
    {
        waitStrategies = new List<WaitStrategy>();
        Interactions = new DriverAdapter();
        JavaScriptService = new DriverAdapter();
    }

    public FindStrategy FindStrategy { get; internal set; }
    public IWebDriver WrappedDriver { get; internal set; }
    public IJavaScriptService JavaScriptService { get; private set; }
    protected IInteractionsService Interactions { get; private set; }


    public IWebElement ParentWrappedElement { get; set; }

    public IWebElement WrappedElement
    {
        get
        {
            if (_wrappedWebElement == null)
            {
                _wrappedWebElement = FindElement(FindStrategy);
                WebComponentPluginExecutionEngine.OnComponentFound(this);
            }

            _plugins.ToList().ForEach(plugin => plugin.OnComponentFound(this));

            return _wrappedWebElement;
        }
        set => _wrappedWebElement = value;
    }

    public static void AddPlugin(WebComponentPlugin plugin)
    {
        _plugins.Add(plugin);
    }

    private IWebElement FindElement(FindStrategy findStrategy)
    {
        if (!waitStrategies.Any())
        {
            waitStrategies.Add(new ToExistWaitStrategy());
        }

        foreach (var waitStrategy in waitStrategies)
        {
            waitStrategy.WaitUntil(FindStrategy);
        }

        _wrappedWebElement = FindNativeElements(findStrategy).FirstOrDefault();

        return _wrappedWebElement;
    }
    private List<IWebElement> FindNativeElements(FindStrategy findStrategy)
    {
        if (ParentWrappedElement != null)
        {
            var elements = ParentWrappedElement.FindElements(findStrategy.Convert()).ToList();
            return elements;
        }
        else
        {
            var elements = WrappedDriver.FindElements(findStrategy.Convert()).ToList();
            return elements;
        }
    }

    public void EnsureState(WaitStrategy waitStrategy)
    {
        waitStrategies.Add(waitStrategy);
    }

    public bool? Enabled => WrappedElement?.Enabled;

    public bool? Displayed => WrappedElement?.Displayed;

    public void Hover()
    {
        Interactions.MoveToElement(this).Perform();
    }

    public string GetAttribute(string attributeName)
    {
        return WrappedElement?.GetAttribute(attributeName);
    }

    public void SetAttribute(string name, string value)
    {
        ((IJavaScriptExecutor)WrappedDriver).ExecuteScript($"arguments[0].setAttribute('{name}', '{value}');", WrappedElement);
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












    public TComponent FindById<TComponent>(string id)
    where TComponent : WebComponent
    {
        return Find<TComponent>(new IdFindStrategy(id));
    }

    public TComponent FindByIdContaining<TComponent>(string id)
       where TComponent : WebComponent
    {
        return Find<TComponent>(new IdContainingFindStrategy(id));
    }

    public TComponent FindByXPath<TComponent>(string xpath)
      where TComponent : WebComponent
    {
        return Find<TComponent>(new XPathFindStrategy(xpath));
    }

    public TComponent FindByCss<TComponent>(string css)
        where TComponent : WebComponent
    {
        return Find<TComponent>(new CssFindStrategy(css));
    }

    public TComponent FindByClass<TComponent>(string value)
       where TComponent : WebComponent
    {
        return Find<TComponent>(new ClassFindStrategy(value));
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

    public TComponent FindByTag<TComponent>(string tag) where TComponent : WebComponent => throw new NotImplementedException();
    public TComponent FindByLinkText<TComponent>(string linkText) where TComponent : WebComponent => throw new NotImplementedException();
    public List<TComponent> FindAllById<TComponent>(string id) where TComponent : WebComponent => throw new NotImplementedException();
    public List<TComponent> FindAllByXPath<TComponent>(string xpath) where TComponent : WebComponent => throw new NotImplementedException();
    public List<TComponent> FindAllByTag<TComponent>(string tag) where TComponent : WebComponent => throw new NotImplementedException();
    public List<TComponent> FindAllByClass<TComponent>(string cssClass) where TComponent : WebComponent => throw new NotImplementedException();
    public List<TComponent> FindAllByCss<TComponent>(string css) where TComponent : WebComponent => throw new NotImplementedException();
    public List<TComponent> FindAllByLinkText<TComponent>(string linkText) where TComponent : WebComponent => throw new NotImplementedException();
    public List<TComponent> FindAll<TComponent>(FindStrategy findStrategy) where TComponent : WebComponent => throw new NotImplementedException();
    public TComponent Create<TComponent>(FindStrategy findStrategy) where TComponent : WebComponent => throw new NotImplementedException();

    public TComponent Find<TComponent>(FindStrategy findStrategy)
      where TComponent : WebComponent
    {
        var component = InstanceFactory.Create<TComponent>();
        component.FindStrategy = findStrategy;
        component.WrappedDriver = WrappedDriver;
        component.WrappedElement = FindElement(findStrategy);
        return component;
    }

    public List<TComponent> FindComponents<TComponent>(FindStrategy findStrategy)
        where TComponent : WebComponent
    {
        var nativeWebElements = FindNativeElements(findStrategy);
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


    private List<IWebElement> FindElements(FindStrategy findStrategy)
    {
        if (!waitStrategies.Any())
        {
            waitStrategies.Add(new ToExistWaitStrategy());
        }

        foreach (var waitStrategy in waitStrategies)
        {
            waitStrategy.WaitUntil(FindStrategy);
        }

        return FindNativeElements(findStrategy);
    }


}