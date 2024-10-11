using PolarisLite.Core.Plugins;
using System.Reflection;

namespace PolarisLite.Core;
public class PluginExecutionEngine
{
    // private static readonly HashSet<Plugin> Plugins = new HashSet<Plugin>();

    private static readonly ThreadLocal<HashSet<Plugin>> ThreadLocalPlugins =
        new ThreadLocal<HashSet<Plugin>>(() => new HashSet<Plugin>());

    public static void AddPlugin(Plugin plugin)
    {
        ThreadLocalPlugins.Value.Add(plugin);
    }

    public static void RemovePlugin(Plugin plugin)
    {
        ThreadLocalPlugins.Value.Remove(plugin);
    }

    public static void OnBeforeTestClassInitialize(Type type)
    {
        foreach (var plugin in ThreadLocalPlugins.Value)
        {
            plugin?.OnBeforeTestClassInitialize(type);
        }
    }

    public static void OnAfterTestClassInitialize(Type type)
    {
        foreach (var plugin in ThreadLocalPlugins.Value)
        {
            plugin?.OnAfterTestClassInitialize(type);
        }
    }

    public static void OnBeforeTestInitialize(MethodInfo memberInfo)
    {
        foreach (var plugin in ThreadLocalPlugins.Value)
        {
            plugin?.OnBeforeTestInitialize(memberInfo);
        }
    }

    public static void OnAfterTestInitialize(MethodInfo memberInfo)
    {
        foreach (var plugin in ThreadLocalPlugins.Value)
        {
            plugin?.OnAfterTestInitialize(memberInfo);
        }
    }

    public static void OnBeforeTestCleanup(TestOutcome result, MethodInfo memberInfo)
    {
        foreach (var plugin in ThreadLocalPlugins.Value)
        {
            plugin?.OnBeforeTestCleanup(result, memberInfo);
        }
    }

    public static void OnAfterTestCleanup(TestOutcome result, MethodInfo memberInfo, Exception failedTestException)
    {
        foreach (var plugin in ThreadLocalPlugins.Value)
        {
            plugin?.OnAfterTestCleanup(result, memberInfo, failedTestException);
        }
    }

    public static void OnBeforeTestClassCleanup(Type type)
    {
        foreach (var plugin in ThreadLocalPlugins.Value)
        {
            plugin?.OnBeforeTestClassCleanup(type);
        }
    }

    public static void OnAfterTestClassCleanup(Type type)
    {
        foreach (var plugin in ThreadLocalPlugins.Value)
        {
            plugin?.OnAfterTestClassCleanup(type);
        }
    }
}
