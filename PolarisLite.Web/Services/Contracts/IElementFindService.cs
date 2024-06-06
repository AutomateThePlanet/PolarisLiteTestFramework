using PolarisLite.Locators;
using PolarisLite.Web.Components;

namespace PolarisLite.Web;

public interface IElementFindService
{
    TComponent FindById<TComponent>(string id)
        where TComponent : ComponentAdapter;
    TComponent FindByXPath<TComponent>(string xpath)
        where TComponent : ComponentAdapter;
    TComponent FindByTag<TComponent>(string tag)
        where TComponent : ComponentAdapter;
    TComponent FindByClass<TComponent>(string cssClass)
        where TComponent : ComponentAdapter;
    TComponent FindByCss<TComponent>(string css)
        where TComponent : ComponentAdapter;
    TComponent FindByLinkText<TComponent>(string linkText)
        where TComponent : ComponentAdapter;
    List<TComponent> FindAllById<TComponent>(string id)
        where TComponent : ComponentAdapter;
    List<TComponent> FindAllByXPath<TComponent>(string xpath)
        where TComponent : ComponentAdapter;
    List<TComponent> FindAllByTag<TComponent>(string tag)
        where TComponent : ComponentAdapter;
    List<TComponent> FindAllByClass<TComponent>(string cssClass)
        where TComponent : ComponentAdapter;
    List<TComponent> FindAllByCss<TComponent>(string css)
        where TComponent : ComponentAdapter;
    List<TComponent> FindAllByLinkText<TComponent>(string linkText)
        where TComponent : ComponentAdapter;

    List<TComponent> FindAll<TComponent>(FindStrategy findStrategy)
        where TComponent : ComponentAdapter;
    TComponent Create<TComponent>(FindStrategy findStrategy)
        where TComponent : ComponentAdapter;
}
