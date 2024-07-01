using PolarisLite.Core;
using PolarisLite.Locators;
using PolarisLite.Web;

namespace PolarisLite;

public class ToExistWaitStrategy : WaitStrategy
{
    public ToExistWaitStrategy(int? timeoutIntervalInSeconds = null, int? sleepIntervalInSeconds = null)
        : base(timeoutIntervalInSeconds, sleepIntervalInSeconds)
    {
        var webSettings = ConfigurationService.GetSection<WebSettings>();
        TimeoutInterval = TimeSpan.FromSeconds(webSettings.TimeoutSettings.ElementToExistTimeout);
    }

    public override void WaitUntil<TBy>(TBy by)
    {
        WaitUntilInternal(d => ElementExists(WrappedDriver, by));
    }

    private bool ElementExists<TBy>(ISearchContext searchContext, TBy by)
         where TBy : FindStrategy
    {
        try
        {
            var element = FindElement(searchContext, by.Convert());
            return element != null;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }
}
