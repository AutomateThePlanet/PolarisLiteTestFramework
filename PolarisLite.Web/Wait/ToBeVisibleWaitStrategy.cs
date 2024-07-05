using PolarisLite.Core;
using PolarisLite.Locators;
using PolarisLite.Web;

namespace PolarisLite;

public class ToBeVisibleWaitStrategy : WaitStrategy
{
    public ToBeVisibleWaitStrategy(int? timeoutIntervalInSeconds = null, int? sleepIntervalInSeconds = null)
        : base(timeoutIntervalInSeconds, sleepIntervalInSeconds)
    {
        var webSettings = ConfigurationService.GetSection<WebSettings>();
        TimeoutInterval = TimeSpan.FromSeconds(webSettings.TimeoutSettings.ElementToBeVisibleTimeout);
    }

    public override void WaitUntil<TFindStrategy>(TFindStrategy by)
    {
        WaitUntilInternal(d => ElementIsVisible(WrappedDriver, by));
    }

    private bool ElementIsVisible<TFindStrategy>(ISearchContext searchContext, TFindStrategy by)
         where TFindStrategy : FindStrategy
    {
        try
        {
            var element = FindElement(searchContext, by.Convert());
            return element != null && element.Displayed;
        }
        catch (StaleElementReferenceException)
        {
            return false;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }
}
