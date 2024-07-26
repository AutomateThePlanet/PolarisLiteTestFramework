using PolarisLite.Web.Components;

namespace PolarisLite.Web.Plugins;
public static class WebComponentPluginExecutionEngine
{
    private static readonly HashSet<WebComponentPlugin> Plugins = new();

    public static void AddPlugin(WebComponentPlugin plugin)
    {
        Plugins.Add(plugin);
    }

    public static void RemovePlugin(WebComponentPlugin plugin)
    {
        Plugins.Remove(plugin);
    }

    public static void OnComponentFound(IWebElement webElement)
    {
        foreach (var plugin in Plugins)
        {
            plugin?.OnComponentFound(webElement);
        }
    }
}