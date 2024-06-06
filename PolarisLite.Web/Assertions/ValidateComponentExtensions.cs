using PolarisLite.Web.Contracts;
using PolarisLite.Web.Plugins.BrowserExecution;

namespace PolarisLite.Web.Assertions;
public static class ValidateComponentExtensions
{
    public static void ValidateIsVisible<T>(this T control, int? timeout = null, int? sleepInterval = null)
       where T : IComponentVisible
    {
        WaitUntil(() => control.IsVisible.Equals(true), $"The control should be visible but was NOT.", timeout, sleepInterval);
    }

    public static void ValidateIsNotVisible<T>(this T control, int? timeout = null, int? sleepInterval = null)
        where T : IComponentVisible, IComponent
    {
        WaitUntil(() => !control.IsVisible.Equals(true), $"The control should be NOT visible but it was.", timeout, sleepInterval);
    }

    public static void ValidateValueIsNull<T>(this T control, int? timeout = null, int? sleepInterval = null)
       where T : IComponentValue, IComponent
    {
        WaitUntil(() => control.Value == null, $"The control's value should be null but was '{control.Value}'.", timeout, sleepInterval);
    }

    public static void ValidateValueIs<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
        where T : IComponentValue, IComponent
    {
        WaitUntil(() => control.Value.Equals(value), $"The control's value should be '{value}' but was '{control.Value}'.", timeout, sleepInterval);
    }

    public static void ValidateValueContains<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
     where T : IComponentValue, IComponent
    {
        WaitUntil(() => control.Value.Contains(value), $"The control's value should contain '{value}' but was '{control.Value}'.", timeout, sleepInterval);
    }

    public static void ValidateIsChecked<T>(this T control, int? timeout = null, int? sleepInterval = null)
       where T : IComponentChecked, IComponent
    {
        WaitUntil(() => control.IsChecked.Equals(true), "The control should be checked but was NOT.", timeout, sleepInterval);
    }

    public static void ValidateIsNotChecked<T>(this T control, int? timeout = null, int? sleepInterval = null)
        where T : IComponentChecked, IComponent
    {
        WaitUntil(() => control.IsChecked.Equals(false), "The control should be not checked but it WAS.", timeout, sleepInterval);
    }

    public static void ValidateInnerTextIs<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
        where T : IComponentInnerText, IComponent
    {
        WaitUntil(() => control.Text.Equals(value), $"The control's inner text should be '{value}' but was '{control.Text}'.", timeout, sleepInterval);
    }

    public static void ValidateInnerTextIsNot<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
    where T : IComponentInnerText, IComponent
    {
        WaitUntil(() => !control.Text.Equals(value), $"The control's inner text should not be '{value}' but was '{control.Text}'.", timeout, sleepInterval);
    }

    public static void ValidateInnerTextContains<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
        where T : IComponentInnerText, IComponent
    {
        WaitUntil(() => control.Text.Contains(value), $"The control's inner text should contain '{value}' but was '{control.Text}'.", timeout, sleepInterval);
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
