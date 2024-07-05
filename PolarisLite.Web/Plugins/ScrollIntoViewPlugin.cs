using PolarisLite.Web.Components;
using PolarisLite.Web.Services;

namespace PolarisLite.Web.Plugins;
public class ScrollIntoViewPlugin : WebComponentPlugin
{
    public override void OnComponentFound(WebComponent component)
    {
        ScrollIntoView(component);
    }

    public override void OnComponentsFound(List<WebComponent> components)
    {
        if (components.Any())
        {
            ScrollIntoView(components.Last());
        }
    }

    private void ScrollIntoView(WebComponent element)
    {
        var driver = new DriverAdapter();
        driver.Execute("arguments[0].scrollIntoView(true);", element.WrappedElement);
    }
}
