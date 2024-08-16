using OpenQA.Selenium.Support.Extensions;

namespace PolarisLite.Web.Plugins;
public class ScrollIntoViewPlugin : WebComponentPlugin
{
    public override void OnComponentFound(IWebElement webElement)
    {
        ScrollIntoView(webElement);
    }

    private void ScrollIntoView(IWebElement webElement)
    {
        try
        {
            var driver = DriverFactory.WrappedDriver;
            var actions = new Actions(driver);

            // Scroll to the element using the Actions API
            actions.MoveToElement(webElement);
            actions.Perform();
        }
        catch { }
      
    }
}
