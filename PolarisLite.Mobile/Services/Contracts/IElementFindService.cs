using PolarisLite.Mobile.Components;

namespace PolarisLite.Mobile;

public interface IElementFindService
{
    TComponent FindById<TComponent>(string id) where TComponent : AndroidComponent, new();
    TComponent FindByIdContaining<TComponent>(string id) where TComponent : AndroidComponent, new();
    TComponent FindByXPath<TComponent>(string xpath) where TComponent : AndroidComponent, new();
    TComponent FindByDescriptionContaining<TComponent>(string description) where TComponent : AndroidComponent, new();
    TComponent FindByTextContaining<TComponent>(string text) where TComponent : AndroidComponent, new();
    TComponent FindByClass<TComponent>(string className) where TComponent : AndroidComponent, new();
    TComponent FindByAndroidUIAutomator<TComponent>(string uiAutomatorExpression) where TComponent : AndroidComponent, new();

    List<TComponent> FindComponentsById<TComponent>(string id) where TComponent : AndroidComponent, new();
    List<TComponent> FindComponentsByXPath<TComponent>(string xpath) where TComponent : AndroidComponent, new();
    List<TComponent> FindComponentsByDescriptionContaining<TComponent>(string description) where TComponent : AndroidComponent, new();
    List<TComponent> FindComponentsByTextContaining<TComponent>(string description) where TComponent : AndroidComponent, new();
    List<TComponent> FindComponentsByAndroidUIAutomator<TComponent>(string uiAutomatorExpression) where TComponent : AndroidComponent, new();

    TComponent FindComponent<TComponent>(FindStrategy findStrategy) where TComponent : AndroidComponent, new();
    List<TComponent> FindComponents<TComponent>(FindStrategy findStrategy) where TComponent : AndroidComponent, new();

    // Placeholder methods for future implementation or other find strategies
    TComponent FindByTag<TComponent>(string tag) where TComponent : AndroidComponent, new();
    List<TComponent> FindAllById<TComponent>(string id) where TComponent : AndroidComponent, new();
    List<TComponent> FindAllByXPath<TComponent>(string xpath) where TComponent : AndroidComponent, new();
    List<TComponent> FindAllByTag<TComponent>(string tag) where TComponent : AndroidComponent, new();
    List<TComponent> FindAllByClass<TComponent>(string cssClass) where TComponent : AndroidComponent, new();
    List<TComponent> FindAll<TComponent>(FindStrategy findStrategy) where TComponent : AndroidComponent, new();
}
