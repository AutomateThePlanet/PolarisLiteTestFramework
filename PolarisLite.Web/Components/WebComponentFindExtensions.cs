using PolarisLite.Locators;
using PolarisLite.Web.Components;

namespace PolarisLite.Web;
public static class WebComponentExtensions
{
    public static TComponent FindByLabel<TComponent>(this TComponent component, string label)
      where TComponent : WebComponent, new()
    {
        return component.Find<TComponent>(new LabelFindStrategy(label));
    }

    public static List<TComponent> FindAllByLabel<TComponent>(this TComponent component, string label)
         where TComponent : WebComponent, new()
    {
        return component.FindAll<TComponent>(new LabelFindStrategy(label));
    }
}

