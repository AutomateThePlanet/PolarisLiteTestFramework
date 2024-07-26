using PolarisLite.Core.Infrastructure;
using PolarisLite.Mobile.Contracts;
using PolarisLite.Mobile.Plugins.AppExecution;
using PolarisLite.Mobile.Services;
using PolarisLite.Mobile.Services.Contracts;
using System.Drawing;

namespace PolarisLite.Mobile.Components;

public class AndroidComponent : IComponent, IComponentVisible
{
    private AppiumElement _wrappedElement;
    public AppiumElement ParentWrappedElement { get; set; }
    public int ElementIndex { get; set; }
    public FindStrategy FindStrategy { get; set; }
    public AndroidDriver WrappedDriver { get; set; }
    protected IAppService AppService { get; private set; }
    protected IElementFindService ComponentCreateService { get; private set; }
    //protected IWaitService ComponentWaitService { get; private set; }
    private readonly List<WaitStrategy> waitStrategies;

    public AndroidComponent()
    {
        waitStrategies = new List<WaitStrategy>();
        AppService = new DriverAdapter();
        ComponentCreateService = new DriverAdapter();
        WrappedDriver = DriverFactory.WrappedAndroidDriver;
    }

    public AppiumElement WrappedElement
    {
        get
        {
            if (_wrappedElement == null)
            {
                return FindElement(FindStrategy);
            }
            else
            {
                return _wrappedElement;
            }
        }
        set => _wrappedElement = value;
    }

    public Point Location => WrappedElement.Location;

    public Size Size => WrappedElement.Size;

    public bool IsVisible => WrappedElement.Displayed;

    internal void Click()
    {
        WrappedElement.Click();
    }

    internal bool GetIsCheckedValue()
    {
        string value = WrappedElement.GetAttribute("value");
        if (value == "1")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    internal bool GetIsChecked()
    {
        return bool.Parse(WrappedElement.GetAttribute("checked"));
    }

    internal string GetText()
    {
        return WrappedElement.Text.Replace("\r\n", string.Empty);
    }

    internal string GetValueAttribute()
    {
        return WrappedElement.GetAttribute("value");
    }

    internal bool IsDisabled()
    {
        return !WrappedElement.Enabled;
    }

    internal void SetValue(string value)
    {
        WrappedElement.Clear();
        WrappedElement.SendKeys(value);
    }

    internal void TypeText(string value)
    {
        WrappedElement.Clear();
        WrappedElement.SendKeys(value);

        try
        {
            WrappedDriver.HideKeyboard();
        }
        catch
        {
            // ignore
        }
    }

    public string GetComponentName()
    {
        return $"{GetType().Name} ({FindStrategy})";
    }

    public void WaitToBe()
    {
        FindElement(FindStrategy);
    }

    public void Hover()
    {
        var action = new Actions(WrappedDriver);
        action.MoveToElement(FindElement(FindStrategy)).Perform();
    }

    public Point GetLocation()
    {
        return FindElement(FindStrategy).Location;
    }

    public Size GetSize()
    {
        return FindElement(FindStrategy).Size;
    }

    public string GetAttribute(string name)
    {
        return FindElement(FindStrategy).GetAttribute(name);
    }

    public void EnsureState(WaitStrategy waitStrategy)
    {
        waitStrategies.Add(waitStrategy);
    }

    public AppiumElement FindElement(FindStrategy findStrategy)
    {
        if (!waitStrategies.Any())
        {
            waitStrategies.Add(new ToExistWaitStrategy());
        }

        foreach (var waitStrategy in waitStrategies)
        {
            waitStrategy.WaitUntil(FindStrategy);
        }

        _wrappedElement = FindNativeElement(findStrategy);

        return _wrappedElement;
    }

    public TComponent FindById<TComponent>(string id) where TComponent : AndroidComponent
    {
        return FindComponent<TComponent>(new IdFindStrategy(id));
    }

    public TComponent FindByIdContaining<TComponent>(string id) where TComponent : AndroidComponent
    {
        return FindComponent<TComponent>(new IdContainingFindStrategy(id));
    }

    public TComponent FindByXPath<TComponent>(string xpath) where TComponent : AndroidComponent
    {
        return FindComponent<TComponent>(new XPathFindStrategy(xpath));
    }

    public TComponent FindByDescriptionContaining<TComponent>(string description) where TComponent : AndroidComponent
    {
        return FindComponent<TComponent>(new DescriptionContainingFindStrategy(description));
    }

    public TComponent FindByAndroidUIAutomator<TComponent>(string uiAutomatorExpression) where TComponent : AndroidComponent
    {
        return FindComponent<TComponent>(new AndroidUIAutomatorFindStrategy(uiAutomatorExpression));
    }

    public List<TComponent> FindComponentsById<TComponent>(string id) where TComponent : AndroidComponent
    {
        return FindComponents<TComponent>(new IdFindStrategy(id));
    }

    public List<TComponent> FindComponentsByXPath<TComponent>(string xpath) where TComponent : AndroidComponent
    {
        return FindComponents<TComponent>(new XPathFindStrategy(xpath));
    }

    public List<TComponent> FindComponentsByDescriptionContaining<TComponent>(string description) where TComponent : AndroidComponent
    {
        return FindComponents<TComponent>(new DescriptionContainingFindStrategy(description));
    }

    public List<TComponent> FindComponentsByAndroidUIAutomator<TComponent>(string uiAutomatorExpression) where TComponent : AndroidComponent
    {
        return FindComponents<TComponent>(new AndroidUIAutomatorFindStrategy(uiAutomatorExpression));
    }

    public TComponent FindComponent<TComponent>(FindStrategy findStrategy)
       where TComponent : AndroidComponent
    {
        AppiumElement nativeElement = findStrategy.FindElement(WrappedElement);
        var component = InstanceFactory.Create<TComponent>();
        component.FindStrategy = findStrategy;
        component.WrappedDriver = WrappedDriver;
        component.WrappedElement = nativeElement;
        return component;
    }

    public List<TComponent> FindComponents<TComponent>(FindStrategy findStrategy) where TComponent : AndroidComponent
    {
        IEnumerable<AppiumElement> nativeElements = findStrategy.FindAllElements(WrappedElement);
        var components = new List<TComponent>();
        foreach (var nativeElement in nativeElements)
        {
            var component = InstanceFactory.Create<TComponent>();
            component.FindStrategy = findStrategy;
            component.WrappedDriver = WrappedDriver;
            component.WrappedElement = nativeElement;
            components.Add(component);
        }

        return components;
    }

    // Placeholder methods that throw NotImplementedException
    public TComponent FindByTag<TComponent>(string tag) where TComponent : AndroidComponent => throw new NotImplementedException();
    public TComponent FindByLinkText<TComponent>(string linkText) where TComponent : AndroidComponent => throw new NotImplementedException();

    public List<TComponent> FindAllById<TComponent>(string id) where TComponent : AndroidComponent
    {
        return FindComponents<TComponent>(new IdFindStrategy(id));
    }

    public List<TComponent> FindAllByXPath<TComponent>(string xpath) where TComponent : AndroidComponent
    {
        return FindComponents<TComponent>(new XPathFindStrategy(xpath));
    }

    public TComponent FindByClass<TComponent>(string className) where TComponent : AndroidComponent => throw new NotImplementedException();
    public List<TComponent> FindAllByTag<TComponent>(string tag) where TComponent : AndroidComponent => throw new NotImplementedException();
    public List<TComponent> FindAllByClass<TComponent>(string cssClass) where TComponent : AndroidComponent => throw new NotImplementedException();
    public List<TComponent> FindAll<TComponent>(FindStrategy findStrategy) where TComponent : AndroidComponent => throw new NotImplementedException();

    private AppiumElement FindNativeElement(FindStrategy findStrategy)
    {
        if (ParentWrappedElement == null)
        {
            return findStrategy.FindAllElements(WrappedDriver).ElementAt(ElementIndex);
        }
        else
        {
            return findStrategy.FindElement(ParentWrappedElement);
        }
    }
}