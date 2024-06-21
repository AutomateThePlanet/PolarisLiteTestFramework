using PolarisLite.Locators;
using PolarisLite.Web.Components;

namespace PolarisLite.Web;

public interface IElementFindService
{
    TComponent FindById<TComponent>(string id)
        where TComponent : WebComponent;
    TComponent FindByXPath<TComponent>(string xpath)
        where TComponent : WebComponent;
    TComponent FindByTag<TComponent>(string tag)
        where TComponent : WebComponent;
    TComponent FindByClass<TComponent>(string cssClass)
        where TComponent : WebComponent;
    TComponent FindByCss<TComponent>(string css)
        where TComponent : WebComponent;
    TComponent FindByLinkText<TComponent>(string linkText)
        where TComponent : WebComponent;
    List<TComponent> FindAllById<TComponent>(string id)
        where TComponent : WebComponent;
    List<TComponent> FindAllByXPath<TComponent>(string xpath)
        where TComponent : WebComponent;
    List<TComponent> FindAllByTag<TComponent>(string tag)
        where TComponent : WebComponent;
    List<TComponent> FindAllByClass<TComponent>(string cssClass)
        where TComponent : WebComponent;
    List<TComponent> FindAllByCss<TComponent>(string css)
        where TComponent : WebComponent;
    List<TComponent> FindAllByLinkText<TComponent>(string linkText)
        where TComponent : WebComponent;

    List<TComponent> FindAll<TComponent>(FindStrategy findStrategy)
        where TComponent : WebComponent;
    TComponent Create<TComponent>(FindStrategy findStrategy)
        where TComponent : WebComponent;
}
