using PolarisLite.Core;
using PolarisLite.Locators;
using PolarisLite.Web;

namespace PolarisLite;

public class ToBeVisibleWaitStrategy : WaitStrategy
{
    public ToBeVisibleWaitStrategy(int? timeoutIntervalInSeconds = null, int? sleepIntervalInSeconds = null)
        : base(timeoutIntervalInSeconds, sleepIntervalInSeconds)
    {
        TimeoutInterval = TimeSpan.FromSeconds(60);
    }

    public override void WaitUntil<TBy>(TBy by)
    {
        WaitUntilInternal(d => ElementIsVisible(WrappedDriver, by));
    }

    private bool ElementIsVisible<TBy>(ISearchContext searchContext, TBy by)
         where TBy : FindStrategy
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
