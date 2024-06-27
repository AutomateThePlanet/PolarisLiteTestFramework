using PolarisLite.Core;
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

    public override void WaitUntil(ISearchContext searchContext, IWebDriver driver, By by)
    {
        WaitUntil(ElementIsVisible(searchContext, by), driver);
    }

    private Func<ISearchContext, bool> ElementIsVisible(ISearchContext searchContext, By by)
    {
        return _ =>
        {
            try
            {
                var element = FindElement(searchContext, by);
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
        };
    }
}
