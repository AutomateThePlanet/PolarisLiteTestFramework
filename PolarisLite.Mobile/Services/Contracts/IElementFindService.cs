using PolarisLite.Mobile.Components;

namespace PolarisLite.Mobile;

public interface IElementFindService
{
    TComponent FindById<TComponent>(string id) where TComponent : AndroidComponent;
    TComponent FindByIdContaining<TComponent>(string id) where TComponent : AndroidComponent;
    TComponent FindByXPath<TComponent>(string xpath) where TComponent : AndroidComponent;
    TComponent FindByDescriptionContaining<TComponent>(string description) where TComponent : AndroidComponent;
    TComponent FindByTextContaining<TComponent>(string text) where TComponent : AndroidComponent;
    TComponent FindByClass<TComponent>(string className) where TComponent : AndroidComponent;
    TComponent FindByAndroidUIAutomator<TComponent>(string uiAutomatorExpression) where TComponent : AndroidComponent;

    List<TComponent> FindComponentsById<TComponent>(string id) where TComponent : AndroidComponent;
    List<TComponent> FindComponentsByXPath<TComponent>(string xpath) where TComponent : AndroidComponent;
    List<TComponent> FindComponentsByDescriptionContaining<TComponent>(string description) where TComponent : AndroidComponent;
    List<TComponent> FindComponentsByTextContaining<TComponent>(string description) where TComponent : AndroidComponent;
    List<TComponent> FindComponentsByAndroidUIAutomator<TComponent>(string uiAutomatorExpression) where TComponent : AndroidComponent;

    TComponent FindComponent<TComponent>(FindStrategy findStrategy) where TComponent : AndroidComponent;
    List<TComponent> FindComponents<TComponent>(FindStrategy findStrategy) where TComponent : AndroidComponent;

    // Placeholder methods for future implementation or other find strategies
    TComponent FindByTag<TComponent>(string tag) where TComponent : AndroidComponent;
    List<TComponent> FindAllById<TComponent>(string id) where TComponent : AndroidComponent;
    List<TComponent> FindAllByXPath<TComponent>(string xpath) where TComponent : AndroidComponent;
    List<TComponent> FindAllByTag<TComponent>(string tag) where TComponent : AndroidComponent;
    List<TComponent> FindAllByClass<TComponent>(string cssClass) where TComponent : AndroidComponent;
    List<TComponent> FindAll<TComponent>(FindStrategy findStrategy) where TComponent : AndroidComponent;
}
