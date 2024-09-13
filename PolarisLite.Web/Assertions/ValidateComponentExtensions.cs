using PolarisLite.Web.Contracts;
using PolarisLite.Web.Events;
using PolarisLite.Web.Plugins;

namespace PolarisLite.Web.Assertions;
public static class ValidateComponentExtensions
{
    public static event EventHandler<ComponentActionEventArgs> ValidatedIsVisibleEvent;
    public static event EventHandler<ComponentActionEventArgs> ValidatedIsNotVisibleEvent;
    public static event EventHandler<ComponentActionEventArgs> ValidatedValueIsNullEvent;
    public static event EventHandler<ComponentActionEventArgs> ValidatedValueIsEvent;
    public static event EventHandler<ComponentActionEventArgs> ValidatedValueContainsEvent;
    public static event EventHandler<ComponentActionEventArgs> ValidatedIsCheckedEvent;
    public static event EventHandler<ComponentActionEventArgs> ValidatedIsNotCheckedEvent;
    public static event EventHandler<ComponentActionEventArgs> ValidatedInnerTextIsEvent;
    public static event EventHandler<ComponentActionEventArgs> ValidatedInnerTextIsNotEvent;
    public static event EventHandler<ComponentActionEventArgs> ValidatedInnerTextContainsEvent;

    public static void ValidateIsVisible<T>(this T component, int? timeout = null, int? sleepInterval = null)
       where T : IComponentVisible
    {
        WaitUntil(() => component.IsVisible.Equals(true), $"The component should be visible but was NOT.", timeout, sleepInterval);
        ValidatedIsVisibleEvent?.Invoke(component, new ComponentActionEventArgs(component));
    }

    public static void ValidateIsNotVisible<T>(this T component, int? timeout = null, int? sleepInterval = null)
        where T : IComponentVisible, IComponent
    {
        WaitUntil(() => !component.IsVisible.Equals(true), $"The component should be NOT visible but it was.", timeout, sleepInterval);
        ValidatedIsNotVisibleEvent?.Invoke(component, new ComponentActionEventArgs(component));
    }

    public static void ValidateValueIsNull<T>(this T component, int? timeout = null, int? sleepInterval = null)
       where T : IComponentValue, IComponent
    {
        WaitUntil(() => component.Value == null, $"The component's value should be null but was '{component.Value}'.", timeout, sleepInterval);
        ValidatedValueIsNullEvent?.Invoke(component, new ComponentActionEventArgs(component));
    }

    public static void ValidateValueIs<T>(this T component, string value, InfoType infoType = InfoType.INFO, int? timeout = null, int? sleepInterval = null)
        where T : IComponentValue, IComponent
    {
        WaitUntil(() => component.Value.Equals(value), $"The component's value should be '{value}' but was '{component.Value}'.", timeout, sleepInterval);
        ValidatedValueIsEvent?.Invoke(component, new ComponentActionEventArgs(component, value, infoType));
    }

    public static void ValidateValueContains<T>(this T component, string value, InfoType infoType = InfoType.INFO, int? timeout = null, int? sleepInterval = null)
     where T : IComponentValue, IComponent
    {
        WaitUntil(() => component.Value.Contains(value), $"The component's value should contain '{value}' but was '{component.Value}'.", timeout, sleepInterval);
        ValidatedValueContainsEvent?.Invoke(component, new ComponentActionEventArgs(component, value, infoType));
    }

    public static void ValidateIsChecked<T>(this T component, int? timeout = null, int? sleepInterval = null)
       where T : IComponentChecked, IComponent
    {
        WaitUntil(() => component.IsChecked.Equals(true), "The component should be checked but was NOT.", timeout, sleepInterval);
        ValidatedIsCheckedEvent?.Invoke(component, new ComponentActionEventArgs(component));
    }

    public static void ValidateIsNotChecked<T>(this T component, int? timeout = null, int? sleepInterval = null)
        where T : IComponentChecked, IComponent
    {
        WaitUntil(() => component.IsChecked.Equals(false), "The component should be not checked but it WAS.", timeout, sleepInterval);
        ValidatedIsNotCheckedEvent?.Invoke(component, new ComponentActionEventArgs(component));
    }

    public static void ValidateInnerTextIs<T>(this T component, string value, InfoType infoType = InfoType.INFO, int? timeout = null, int? sleepInterval = null)
        where T : IComponentInnerText, IComponent
    {
        WaitUntil(() => component.Text.Equals(value), $"The component's inner text should be '{value}' but was '{component.Text}'.", timeout, sleepInterval);
        ValidatedInnerTextIsEvent?.Invoke(component, new ComponentActionEventArgs(component, value, infoType));
    }

    public static void ValidateInnerTextIsNot<T>(this T component, string value, InfoType infoType = InfoType.INFO, int? timeout = null, int? sleepInterval = null)
    where T : IComponentInnerText, IComponent
    {
        WaitUntil(() => !component.Text.Equals(value), $"The component's inner text should not be '{value}' but was '{component.Text}'.", timeout, sleepInterval);
        ValidatedInnerTextIsNotEvent?.Invoke(component, new ComponentActionEventArgs(component, value, infoType));
    }

    public static void ValidateInnerTextContains<T>(this T component, string value, InfoType infoType = InfoType.INFO, int? timeout = null, int? sleepInterval = null)
        where T : IComponentInnerText, IComponent
    {
        WaitUntil(() => component.Text.Contains(value), $"The component's inner text should contain '{value}' but was '{component.Text}'.", timeout, sleepInterval);
        ValidatedInnerTextContainsEvent?.Invoke(component, new ComponentActionEventArgs(component, value, infoType));
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
