using OpenQA.Selenium.Support.Extensions;
using PolarisLite.Web.Components;

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
            var script = "arguments[0].scrollIntoView(true);";
            driver.ExecuteJavaScript(script, webElement);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
