using PolarisLite.Core;
using PolarisLite.Locators;
using PolarisLite.Web;
using PolarisLite.Web.Plugins.BrowserExecution;

namespace PolarisLite;

public abstract class WaitStrategy
{
    protected WaitStrategy(int? timeoutIntervalInSeconds = null, int? sleepIntervalInSeconds = null)
    {
        var webSettings = ConfigurationService.GetSection<WebSettings>();
        TimeoutInterval = TimeSpan.FromSeconds(timeoutIntervalInSeconds ?? webSettings.TimeoutSettings.ElementToExistTimeout);
        SleepInterval = TimeSpan.FromSeconds(sleepIntervalInSeconds ?? webSettings.TimeoutSettings.SleepInterval);
        WrappedDriver = DriverFactory.WrappedDriver;
    }

    protected TimeSpan TimeoutInterval { get; set; }

    protected TimeSpan SleepInterval { get; set; }

    protected IWebDriver WrappedDriver { get; }

    public abstract void WaitUntil<TBy>(TBy by)
     where TBy : FindStrategy;

    protected void WaitUntilInternal(Func<ISearchContext, bool> waitCondition)
    {
        var wait = new WebDriverWait(new SystemClock(), WrappedDriver, TimeoutInterval, SleepInterval);
        wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException), typeof(InvalidOperationException));
        wait.Until(waitCondition);
    }

    protected void WaitUntil(Func<ISearchContext, bool> waitCondition, IWebDriver driver)
    {
        var webDriverWait = new WebDriverWait(new SystemClock(), driver, TimeoutInterval, SleepInterval);
        webDriverWait.Until(waitCondition);
    }

    protected void WaitUntil(Func<ISearchContext, IWebElement> waitCondition, IWebDriver driver)
    {
        var webDriverWait = new WebDriverWait(new SystemClock(), driver, TimeoutInterval, SleepInterval);
        webDriverWait.Until(waitCondition);
    }

    protected IWebElement FindElement(ISearchContext searchContext, By by)
    {
        var element = searchContext.FindElement(by);
        return element;
    }
}
