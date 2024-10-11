using PolarisLite.Web.Contracts;
using PolarisLite.Web.Events;
using PolarisLite.Web.Plugins;

namespace PolarisLite.Web.Assertions;
public static class ValidateComponentExtensions
{
    // Thread-local event handlers for each validation event
    private static readonly ThreadLocal<EventHandler<ComponentActionEventArgs>> _validatedIsVisibleEvent = new ThreadLocal<EventHandler<ComponentActionEventArgs>>();
    private static readonly ThreadLocal<EventHandler<ComponentActionEventArgs>> _validatedIsNotVisibleEvent = new ThreadLocal<EventHandler<ComponentActionEventArgs>>();
    private static readonly ThreadLocal<EventHandler<ComponentActionEventArgs>> _validatedValueIsNullEvent = new ThreadLocal<EventHandler<ComponentActionEventArgs>>();
    private static readonly ThreadLocal<EventHandler<ComponentActionEventArgs>> _validatedValueIsEvent = new ThreadLocal<EventHandler<ComponentActionEventArgs>>();
    private static readonly ThreadLocal<EventHandler<ComponentActionEventArgs>> _validatedValueContainsEvent = new ThreadLocal<EventHandler<ComponentActionEventArgs>>();
    private static readonly ThreadLocal<EventHandler<ComponentActionEventArgs>> _validatedIsCheckedEvent = new ThreadLocal<EventHandler<ComponentActionEventArgs>>();
    private static readonly ThreadLocal<EventHandler<ComponentActionEventArgs>> _validatedIsNotCheckedEvent = new ThreadLocal<EventHandler<ComponentActionEventArgs>>();
    private static readonly ThreadLocal<EventHandler<ComponentActionEventArgs>> _validatedInnerTextIsEvent = new ThreadLocal<EventHandler<ComponentActionEventArgs>>();
    private static readonly ThreadLocal<EventHandler<ComponentActionEventArgs>> _validatedInnerTextIsNotEvent = new ThreadLocal<EventHandler<ComponentActionEventArgs>>();
    private static readonly ThreadLocal<EventHandler<ComponentActionEventArgs>> _validatedInnerTextContainsEvent = new ThreadLocal<EventHandler<ComponentActionEventArgs>>();

    public static event EventHandler<ComponentActionEventArgs> ValidatedIsVisibleEvent
    {
        add
        {
            if (!_validatedIsVisibleEvent.IsValueCreated)
            {
                _validatedIsVisibleEvent.Value = value;
            }
        }
        remove
        {
            if (_validatedIsVisibleEvent.IsValueCreated)
            {
                _validatedIsVisibleEvent.Value -= value;
            }
        }
    }

    public static event EventHandler<ComponentActionEventArgs> ValidatedIsNotVisibleEvent
    {
        add
        {
            if (!_validatedIsNotVisibleEvent.IsValueCreated)
            {
                _validatedIsNotVisibleEvent.Value = value;
            }
        }
        remove
        {
            if (_validatedIsNotVisibleEvent.IsValueCreated)
            {
                _validatedIsNotVisibleEvent.Value -= value;
            }
        }
    }

    public static event EventHandler<ComponentActionEventArgs> ValidatedValueIsNullEvent
    {
        add
        {
            if (!_validatedValueIsNullEvent.IsValueCreated)
            {
                _validatedValueIsNullEvent.Value = value;
            }
        }
        remove
        {
            if (_validatedValueIsNullEvent.IsValueCreated)
            {
                _validatedValueIsNullEvent.Value -= value;
            }
        }
    }

    public static event EventHandler<ComponentActionEventArgs> ValidatedValueIsEvent
    {
        add
        {
            if (!_validatedValueIsEvent.IsValueCreated)
            {
                _validatedValueIsEvent.Value = value;
            }
        }
        remove
        {
            if (_validatedValueIsEvent.IsValueCreated)
            {
                _validatedValueIsEvent.Value -= value;
            }
        }
    }

    public static event EventHandler<ComponentActionEventArgs> ValidatedValueContainsEvent
    {
        add
        {
            if (!_validatedValueContainsEvent.IsValueCreated)
            {
                _validatedValueContainsEvent.Value = value;
            }
        }
        remove
        {
            if (_validatedValueContainsEvent.IsValueCreated)
            {
                _validatedValueContainsEvent.Value -= value;
            }
        }
    }

    public static event EventHandler<ComponentActionEventArgs> ValidatedIsCheckedEvent
    {
        add
        {
            if (!_validatedIsCheckedEvent.IsValueCreated)
            {
                _validatedIsCheckedEvent.Value = value;
            }
        }
        remove
        {
            if (_validatedIsCheckedEvent.IsValueCreated)
            {
                _validatedIsCheckedEvent.Value -= value;
            }
        }
    }

    public static event EventHandler<ComponentActionEventArgs> ValidatedIsNotCheckedEvent
    {
        add
        {
            if (!_validatedIsNotCheckedEvent.IsValueCreated)
            {
                _validatedIsNotCheckedEvent.Value = value;
            }
        }
        remove
        {
            if (_validatedIsNotCheckedEvent.IsValueCreated)
            {
                _validatedIsNotCheckedEvent.Value -= value;
            }
        }
    }

    public static event EventHandler<ComponentActionEventArgs> ValidatedInnerTextIsEvent
    {
        add
        {
            if (!_validatedInnerTextIsEvent.IsValueCreated)
            {
                _validatedInnerTextIsEvent.Value = value;
            }
        }
        remove
        {
            if (_validatedInnerTextIsEvent.IsValueCreated)
            {
                _validatedInnerTextIsEvent.Value -= value;
            }
        }
    }

    public static event EventHandler<ComponentActionEventArgs> ValidatedInnerTextIsNotEvent
    {
        add
        {
            if (!_validatedInnerTextIsNotEvent.IsValueCreated)
            {
                _validatedInnerTextIsNotEvent.Value = value;
            }
        }
        remove
        {
            if (_validatedInnerTextIsNotEvent.IsValueCreated)
            {
                _validatedInnerTextIsNotEvent.Value -= value;
            }
        }
    }

    public static event EventHandler<ComponentActionEventArgs> ValidatedInnerTextContainsEvent
    {
        add
        {
            if (!_validatedInnerTextContainsEvent.IsValueCreated)
            {
                _validatedInnerTextContainsEvent.Value = value;
            }
        }
        remove
        {
            if (_validatedInnerTextContainsEvent.IsValueCreated)
            {
                _validatedInnerTextContainsEvent.Value -= value;
            }
        }
    }

    public static void ValidateIsVisible<T>(this T component, int? timeout = null, int? sleepInterval = null)
        where T : IComponentVisible
    {
        WaitUntil(() => component.IsVisible.Equals(true), $"The component should be visible but was NOT.", timeout, sleepInterval);
        _validatedIsVisibleEvent.Value?.Invoke(component, new ComponentActionEventArgs(component));
    }

    public static void ValidateIsNotVisible<T>(this T component, int? timeout = null, int? sleepInterval = null)
        where T : IComponentVisible, IComponent
    {
        WaitUntil(() => !component.IsVisible.Equals(true), $"The component should be NOT visible but it was.", timeout, sleepInterval);
        _validatedIsNotVisibleEvent.Value?.Invoke(component, new ComponentActionEventArgs(component));
    }

    public static void ValidateValueIsNull<T>(this T component, int? timeout = null, int? sleepInterval = null)
        where T : IComponentValue, IComponent
    {
        WaitUntil(() => component.Value == null, $"The component's value should be null but was '{component.Value}'.", timeout, sleepInterval);
        _validatedValueIsNullEvent.Value?.Invoke(component, new ComponentActionEventArgs(component));
    }

    public static void ValidateValueIs<T>(this T component, string value, InfoType infoType = InfoType.INFO, int? timeout = null, int? sleepInterval = null)
        where T : IComponentValue, IComponent
    {
        WaitUntil(() => component.Value.Equals(value), $"The component's value should be '{value}' but was '{component.Value}'.", timeout, sleepInterval);
        _validatedValueIsEvent.Value?.Invoke(component, new ComponentActionEventArgs(component, value, infoType));
    }

    public static void ValidateValueContains<T>(this T component, string value, InfoType infoType = InfoType.INFO, int? timeout = null, int? sleepInterval = null)
        where T : IComponentValue, IComponent
    {
        WaitUntil(() => component.Value.Contains(value), $"The component's value should contain '{value}' but was '{component.Value}'.", timeout, sleepInterval);
        _validatedValueContainsEvent.Value?.Invoke(component, new ComponentActionEventArgs(component, value, infoType));
    }

    public static void ValidateIsChecked<T>(this T component, int? timeout = null, int? sleepInterval = null)
        where T : IComponentChecked, IComponent
    {
        WaitUntil(() => component.IsChecked.Equals(true), "The component should be checked but was NOT.", timeout, sleepInterval);
        _validatedIsCheckedEvent.Value?.Invoke(component, new ComponentActionEventArgs(component));
    }

    public static void ValidateIsNotChecked<T>(this T component, int? timeout = null, int? sleepInterval = null)
        where T : IComponentChecked, IComponent
    {
        WaitUntil(() => component.IsChecked.Equals(false), "The component should be not checked but it WAS.", timeout, sleepInterval);
        _validatedIsNotCheckedEvent.Value?.Invoke(component, new ComponentActionEventArgs(component));
    }

    public static void ValidateInnerTextIs<T>(this T component, string value, InfoType infoType = InfoType.INFO, int? timeout = null, int? sleepInterval = null)
        where T : IComponentInnerText, IComponent
    {
        WaitUntil(() => component.Text.Equals(value), $"The component's inner text should be '{value}' but was '{component.Text}'.", timeout, sleepInterval);
        _validatedInnerTextIsEvent.Value?.Invoke(component, new ComponentActionEventArgs(component, value, infoType));
    }

    public static void ValidateInnerTextIsNot<T>(this T component, string value, InfoType infoType = InfoType.INFO, int? timeout = null, int? sleepInterval = null)
        where T : IComponentInnerText, IComponent
    {
        WaitUntil(() => !component.Text.Equals(value), $"The component's inner text should not be '{value}' but was '{component.Text}'.", timeout, sleepInterval);
        _validatedInnerTextIsNotEvent.Value?.Invoke(component, new ComponentActionEventArgs(component, value, infoType));
    }

    public static void ValidateInnerTextContains<T>(this T component, string value, InfoType infoType = InfoType.INFO, int? timeout = null, int? sleepInterval = null)
        where T : IComponentInnerText, IComponent
    {
        WaitUntil(() => component.Text.Contains(value), $"The component's inner text should contain '{value}' but was '{component.Text}'.", timeout, sleepInterval);
        _validatedInnerTextContainsEvent.Value?.Invoke(component, new ComponentActionEventArgs(component, value, infoType));
    }

    private static void WaitUntil(Func<bool> waitCondition, string exceptionMessage, int? timeoutInSeconds, int? sleepIntervalInSeconds)
    {
        var wrappedWebDriver = DriverFactory.WrappedDriver;
        var webDriverWait = new WebDriverWait(wrappedWebDriver, TimeSpan.FromSeconds(30));
        webDriverWait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
        bool LocalCondition(IWebDriver s)
        {
            try
            {
                return waitCondition();
            }
            catch (Exception)
            {
                return false;
            }
        }

        try
        {
            webDriverWait.Until(LocalCondition);
        }
        catch (WebDriverTimeoutException)
        {
            var elementPropertyValidateException = new ComponentPropertyValidateException(exceptionMessage, wrappedWebDriver.Url);
            throw elementPropertyValidateException;
        }
    }
}

