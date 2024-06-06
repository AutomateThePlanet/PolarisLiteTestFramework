using PolarisLite.Locators;
using PolarisLite.Web.Components;

namespace PolarisLite.Web;

public interface IElementFindService
{
    TComponent FindById<TComponent>(string id)
        where TComponent : Component;
    TComponent FindByXPath<TComponent>(string xpath)
        where TComponent : Component;
    TComponent FindByTag<TComponent>(string tag)
        where TComponent : Component;
    TComponent FindByClass<TComponent>(string cssClass)
        where TComponent : Component;
    TComponent FindByCss<TComponent>(string css)
        where TComponent : Component;
    TComponent FindByLinkText<TComponent>(string linkText)
        where TComponent : Component;
    List<TComponent> FindAllById<TComponent>(string id)
        where TComponent : Component;
    List<TComponent> FindAllByXPath<TComponent>(string xpath)
        where TComponent : Component;
    List<TComponent> FindAllByTag<TComponent>(string tag)
        where TComponent : Component;
    List<TComponent> FindAllByClass<TComponent>(string cssClass)
        where TComponent : Component;
    List<TComponent> FindAllByCss<TComponent>(string css)
        where TComponent : Component;
    List<TComponent> FindAllByLinkText<TComponent>(string linkText)
        where TComponent : Component;

    List<TComponent> FindAll<TComponent>(FindStrategy findStrategy)
        where TComponent : Component;
    TComponent Create<TComponent>(FindStrategy findStrategy)
        where TComponent : Component;
}
