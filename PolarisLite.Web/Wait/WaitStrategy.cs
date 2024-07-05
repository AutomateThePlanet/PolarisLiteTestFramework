using PolarisLite.Core;
using PolarisLite.Locators;
using PolarisLite.Web;
using PolarisLite.Web.Core;

namespace PolarisLite;

public abstract class WaitStrategy
{
    protected WaitStrategy(int? timeoutIntervalInSeconds = null, int? sleepIntervalInSeconds = null)
    {
        TimeoutInterval = TimeSpan.FromSeconds(timeoutIntervalInSeconds ?? 60);
        SleepInterval = TimeSpan.FromSeconds(sleepIntervalInSeconds ?? 2);
        WrappedDriver = DriverFactory.WrappedDriver;
    }

    protected TimeSpan TimeoutInterval { get; set; }

    protected TimeSpan SleepInterval { get; set; }

    protected IWebDriver WrappedDriver { get; }

    public abstract void WaitUntil<TFindStrategy>(TFindStrategy findStrategy)
     where TFindStrategy : FindStrategy;

    protected void WaitUntilInternal(Func<ISearchContext, bool> waitCondition)
    {
        var wait = new WebDriverWait(new SystemClock(), WrappedDriver, TimeoutInterval, SleepInterval);
        wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException), typeof(InvalidOperationException));
        wait.Until(waitCondition);
    }

    protected IWebElement FindElement(ISearchContext searchContext, By by)
    {
        var element = searchContext.FindElement(by);
        return element;
    }
}
