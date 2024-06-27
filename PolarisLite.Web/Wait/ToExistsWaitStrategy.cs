using PolarisLite.Core;
using PolarisLite.Web;

namespace PolarisLite;

public class ToExistsWaitStrategy : WaitStrategy
{
    public ToExistsWaitStrategy(int? timeoutIntervalInSeconds = null, int? sleepIntervalInSeconds = null)
        : base(timeoutIntervalInSeconds, sleepIntervalInSeconds)
    {
        var webSettings = ConfigurationService.GetSection<WebSettings>();
        TimeoutInterval = TimeSpan.FromSeconds(webSettings.TimeoutSettings.ElementToExistTimeout);
    }

    public override void WaitUntil(ISearchContext searchContext, IWebDriver driver, By by)
    {
        WaitUntil(ElementExists(searchContext, by), driver);
    }

    private Func<ISearchContext, bool> ElementExists(ISearchContext searchContext, By by)
    {
        return _ =>
        {
            try
            {
                var element = FindElement(searchContext, by);
                return element != null;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        };
    }
}
