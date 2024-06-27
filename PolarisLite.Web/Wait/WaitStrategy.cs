using PolarisLite.Core;
using PolarisLite.Web;

namespace PolarisLite;

public abstract class WaitStrategy
{
    protected WaitStrategy(int? timeoutIntervalInSeconds = null, int? sleepIntervalInSeconds = null)
    {
        var webSettings = ConfigurationService.GetSection<WebSettings>();
        TimeoutInterval = TimeSpan.FromSeconds(timeoutIntervalInSeconds ?? webSettings.TimeoutSettings.ElementToExistTimeout);
        SleepInterval = TimeSpan.FromSeconds(sleepIntervalInSeconds ?? webSettings.TimeoutSettings.SleepInterval);
    }

    protected TimeSpan TimeoutInterval { get; set; }

    protected TimeSpan SleepInterval { get; set; }

    public abstract void WaitUntil(ISearchContext searchContext, IWebDriver driver, By by);

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
