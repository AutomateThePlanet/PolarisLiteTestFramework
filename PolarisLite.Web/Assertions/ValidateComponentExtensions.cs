using PolarisLite.Web.Contracts;
using PolarisLite.Web.Core;

namespace PolarisLite.Web.Assertions;
public static partial class ValidateComponentExtensions
{
    public static void ValidateIsVisible<T>(this T component, int? timeout = null, int? sleepInterval = null)
       where T : IComponentVisible
    {
        Validate(() => component.IsVisible.Equals(true), $"The component should be visible but was NOT.", timeout, sleepInterval);
    }

    public static void ValidateIsNotVisible<T>(this T component, int? timeout = null, int? sleepInterval = null)
        where T : IComponentVisible, IComponent
    {
        Validate(() => !component.IsVisible.Equals(true), $"The component should be NOT visible but it was.", timeout, sleepInterval);
    }

    public static void ValidateValueIsNull<T>(this T component, int? timeout = null, int? sleepInterval = null)
       where T : IComponentValue, IComponent
    {
        Validate(() => component.Value == null, $"The component's value should be null but was '{component.Value}'.", timeout, sleepInterval);
    }

    public static void ValidateValueIs<T>(this T component, string value, int? timeout = null, int? sleepInterval = null)
        where T : IComponentValue, IComponent
    {
        Validate(() => component.Value.Equals(value), $"The component's value should be '{value}' but was '{component.Value}'.", timeout, sleepInterval);
    }

    public static void ValidateValueContains<T>(this T component, string value, int? timeout = null, int? sleepInterval = null)
     where T : IComponentValue, IComponent
    {
        Validate(() => component.Value.Contains(value), $"The component's value should contain '{value}' but was '{component.Value}'.", timeout, sleepInterval);
    }

    public static void ValidateIsChecked<T>(this T component, int? timeout = null, int? sleepInterval = null)
       where T : IComponentChecked, IComponent
    {
        Validate(() => component.IsChecked.Equals(true), "The component should be checked but was NOT.", timeout, sleepInterval);
    }

    public static void ValidateIsNotChecked<T>(this T component, int? timeout = null, int? sleepInterval = null)
        where T : IComponentChecked, IComponent
    {
        Validate(() => component.IsChecked.Equals(false), "The component should be not checked but it WAS.", timeout, sleepInterval);
    }

    public static void ValidateInnerTextIs<T>(this T component, string value, int? timeout = null, int? sleepInterval = null)
        where T : IComponentInnerText, IComponent
    {
        Validate(() => component.Text.Equals(value), $"The component's inner text should be '{value}' but was '{component.Text}'.", timeout, sleepInterval);
    }

    public static void ValidateInnerTextIsNot<T>(this T component, string value, int? timeout = null, int? sleepInterval = null)
    where T : IComponentInnerText, IComponent
    {
        Validate(() => !component.Text.Equals(value), $"The component's inner text should not be '{value}' but was '{component.Text}'.", timeout, sleepInterval);
    }

    public static void ValidateInnerTextContains<T>(this T component, string value, int? timeout = null, int? sleepInterval = null)
        where T : IComponentInnerText, IComponent
    {
        Validate(() => component.Text.Contains(value), $"The component's inner text should contain '{value}' but was '{component.Text}'.", timeout, sleepInterval);
    }

    private static void Validate(Func<bool> waitCondition, string exceptionMessage, int? timeoutInSeconds, int? sleepIntervalInSeconds)
    {
        var wrappedWebDriver = DriverFactory.WrappedDriver;
        var webDriverWait = new WebDriverWait(wrappedWebDriver, TimeSpan.FromSeconds(30));
        webDriverWait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

        try
        {
            webDriverWait.Until(d => waitCondition);
        }
        catch (WebDriverTimeoutException)
        {
            var elementPropertyValidateException = new ComponentPropertyValidateException(exceptionMessage, wrappedWebDriver.Url);
            throw elementPropertyValidateException;
        }
    }
}
