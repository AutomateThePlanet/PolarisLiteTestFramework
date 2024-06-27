using PolarisLite.Core;
using PolarisLite.Web;

namespace PolarisLite;

public class ToBeClickableWaitStrategy : WaitStrategy
{
    public ToBeClickableWaitStrategy(int? timeoutIntervalInSeconds = null, int? sleepIntervalInSeconds = null)
        : base(timeoutIntervalInSeconds, sleepIntervalInSeconds)
    {
        var webSettings = ConfigurationService.GetSection<WebSettings>();
        TimeoutInterval = TimeSpan.FromSeconds(webSettings.TimeoutSettings.ElementToBeClickableTimeout);
    }

    public override void WaitUntil(ISearchContext searchContext, IWebDriver driver, By by)
    {
        WaitUntil(ElementIsClickable(searchContext, by), driver);
    }

    private Func<ISearchContext, bool> ElementIsClickable(ISearchContext searchContext, By by)
    {
        return _ =>
        {
            var element = FindElement(searchContext, by);
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
        };
    }
}
