using PolarisLite.Locators;
using PolarisLite.Web.Components;

namespace PolarisLite.Web.Services;

public static class DriverAdapterExtensions
{
    public static TComponent FindByLabel<TComponent>(this IElementFindService driverAdapter, string label)
         where TComponent : WebComponent, new()
    {
        return driverAdapter.Find<TComponent>(new LabelFindStrategy(label));
    }

    public static List<TComponent> FindAllByLabel<TComponent>(this IElementFindService driverAdapter, string label)
         where TComponent : WebComponent, new()
    {
        return driverAdapter.FindAll<TComponent>(new LabelFindStrategy(label));
    }
}
