using PolarisLite.Locators;
using PolarisLite.Web.Components;

namespace PolarisLite.Web;

public interface IElementFindService
{
    TComponent FindById<TComponent>(string id)
        where TComponent : WebComponent, new();
    TComponent FindByXPath<TComponent>(string xpath)
        where TComponent : WebComponent, new();
    TComponent FindByTag<TComponent>(string tag)
        where TComponent : WebComponent, new();
    TComponent FindByClass<TComponent>(string cssClass)
        where TComponent : WebComponent, new();
    TComponent FindByCss<TComponent>(string css)
        where TComponent : WebComponent, new();
    TComponent FindByLinkText<TComponent>(string linkText)
        where TComponent : WebComponent, new();
    List<TComponent> FindAllById<TComponent>(string id)
        where TComponent : WebComponent, new();
    List<TComponent> FindAllByXPath<TComponent>(string xpath)
        where TComponent : WebComponent, new();
    List<TComponent> FindAllByTag<TComponent>(string tag)
        where TComponent : WebComponent, new();
    List<TComponent> FindAllByClass<TComponent>(string cssClass)
        where TComponent : WebComponent, new();
    List<TComponent> FindAllByCss<TComponent>(string css)
        where TComponent : WebComponent, new();
    List<TComponent> FindAllByLinkText<TComponent>(string linkText)
        where TComponent : WebComponent, new();

    List<TComponent> FindAll<TComponent>(FindStrategy findStrategy)
        where TComponent : WebComponent, new();
}
