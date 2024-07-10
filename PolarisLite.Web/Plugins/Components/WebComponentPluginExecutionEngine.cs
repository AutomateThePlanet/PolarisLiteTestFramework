using PolarisLite.Core.Plugins;
using PolarisLite.Web;
using PolarisLite.Web.Components;
using System.Reflection;

namespace PolarisLite.Core;
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

    public static void OnComponentFound(WebComponent component)
    {
        foreach (var plugin in Plugins)
        {
            plugin?.OnComponentFound(component);
        }
    }

    public static void OnComponentsFound(List<WebComponent> components)
    {
        Plugins.ToList().ForEach(plugin => plugin?.OnComponentsFound(components));
    }
}