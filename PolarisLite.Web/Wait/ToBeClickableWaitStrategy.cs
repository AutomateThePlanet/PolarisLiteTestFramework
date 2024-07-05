using PolarisLite.Core;
using PolarisLite.Locators;
using PolarisLite.Web;

namespace PolarisLite;

public class ToBeClickableWaitStrategy : WaitStrategy
{
    public ToBeClickableWaitStrategy(int? timeoutIntervalInSeconds = null, int? sleepIntervalInSeconds = null)
        : base(timeoutIntervalInSeconds, sleepIntervalInSeconds)
    {
        TimeoutInterval = TimeSpan.FromSeconds(60);
    }

    public override void WaitUntil<TBy>(TBy by)
    {
        WaitUntilInternal(d => ElementIsClickable(WrappedDriver, by));
    }

    private bool ElementIsClickable<TFindStrategy>(ISearchContext searchContext, TFindStrategy by)
      where TFindStrategy : FindStrategy
    {
        var element = base.FindElement(searchContext, by.Convert());
        try
        {
            return element != null && element.Enabled;
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
