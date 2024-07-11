using PolarisLite.Core.Plugins;
using System.Reflection;

namespace PolarisLite.Core;
public static class PluginExecutionEngine
{
    private static readonly List<Plugin> Plugins = new List<Plugin>();

    public static void AddPlugin(Plugin plugin)
    {
        Plugins.Add(plugin);
    }

    public static void RemovePlugin(Plugin plugin)
    {
        Plugins.Remove(plugin);
    }

    public static void OnBeforeTestClassInitialize(Type type)
    {
        foreach (var plugin in Plugins)
        {
            plugin?.OnBeforeTestClassInitialize(type);
        }
    }

    public static void OnAfterTestClassInitialize(Type type)
    {
        foreach (var plugin in Plugins)
        {
            plugin?.OnAfterTestClassInitialize(type);
        }
    }

    public static void OnBeforeTestInitialize(MethodInfo memberInfo)
    {
        foreach (var plugin in Plugins)
        {
            plugin?.OnBeforeTestInitialize(memberInfo);
        }
    }

    public static void OnAfterTestInitialize(MethodInfo memberInfo)
    {
        foreach (var plugin in Plugins)
        {
            plugin?.OnAfterTestInitialize(memberInfo);
        }
    }

    public static void OnBeforeTestCleanup(TestOutcome result, MethodInfo memberInfo)
    {
        foreach (var plugin in Plugins)
        {
            plugin?.OnBeforeTestCleanup(result, memberInfo);
        }
    }

    public static void OnAfterTestCleanup(TestOutcome result, MethodInfo memberInfo, Exception failedTestException)
    {
        foreach (var plugin in Plugins)
        {
            plugin?.OnAfterTestCleanup(result, memberInfo, failedTestException);
        }
    }

    public static void OnBeforeTestClassCleanup(Type type)
    {
        foreach (var plugin in Plugins)
        {
            plugin?.OnBeforeTestClassCleanup(type);
        }
    }

    public static void OnAfterTestClassCleanup(Type type)
    {
        foreach (var plugin in Plugins)
        {
            plugin?.OnAfterTestClassCleanup(type);
        }
    }
}