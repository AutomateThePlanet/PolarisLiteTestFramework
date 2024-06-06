using PolarisLite.Web.Components;

namespace PolarisLite.Web;

public interface IInteractionsService
{
    IInteractionsService Release();
    IInteractionsService MoveToElement<TComponent>(TComponent component)
        where TComponent : Component;
    IInteractionsService DragAndDrop<TComponent>(TComponent sourceComponent, TComponent destinationElement)
        where TComponent : Component;
    IInteractionsService DoubleClick<TComponent>(TComponent element)
        where TComponent : Component;
    IInteractionsService ClickAndHold<TComponent>(TComponent element)
        where TComponent : Component;
    void Perform();
}
