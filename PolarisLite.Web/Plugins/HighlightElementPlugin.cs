using OpenQA.Selenium.Support.Extensions;

namespace PolarisLite.Web.Plugins;
public class HighlightElementPlugin : WebComponentPlugin
{
    public override void OnComponentFound(IWebElement webElement)
    {
        HighlightElement(webElement);
    }

    private void HighlightElement(IWebElement webElement)
    {
        try
        {
            // Get original styles
            //var originalElementBackground = element.WrappedElement.GetCssValue("background");
            //var originalElementColor = element.WrappedElement.GetCssValue("color");
            //var originalElementOutline = element.WrappedElement.GetCssValue("outline");

            //// JavaScript to modify and then revert the element's style
            //var script = @"
            //    let defaultBG = arguments[0].style.backgroundColor;
            //    let defaultOutline = arguments[0].style.outline;
            //    arguments[0].style.backgroundColor = '#FDFF47';
            //    arguments[0].style.outline = '#f00 solid 2px';

            //    setTimeout(function()
            //    {
            //        arguments[0].style.backgroundColor = defaultBG;
            //        arguments[0].style.outline = defaultOutline;
            //    }, 500);";

            // can be moved to constructor injection, instead of interface we can use base class
            var driver = DriverFactory.WrappedDriver;
            //driver.ExecuteJavaScript(script, element.WrappedElement);
            driver.ExecuteJavaScript("highlight(arguments[0])", webElement);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
