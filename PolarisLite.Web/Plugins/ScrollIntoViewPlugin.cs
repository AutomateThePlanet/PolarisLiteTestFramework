using OpenQA.Selenium.Support.Extensions;
using PolarisLite.Web.Components;
using PolarisLite.Web.Plugins.BrowserExecution;

namespace PolarisLite.Web.Plugins;
public class ScrollIntoViewPlugin
    //: DriverAdapterPlugin
{
    public void OnComponentFound(WebComponent component)
    {
        ScrollIntoView(component);
    }

    public void OnComponentsFound(List<WebComponent> components)
    {
        if (components.Any())
        {
            ScrollIntoView(components.Last());
        }
    }

    private void ScrollIntoView(WebComponent element)
    {
        var driver = DriverFactory.WrappedDriver;
        var script = "arguments[0].scrollIntoView(true);";
        driver.ExecuteJavaScript(script, element.WrappedElement);
    }
}
