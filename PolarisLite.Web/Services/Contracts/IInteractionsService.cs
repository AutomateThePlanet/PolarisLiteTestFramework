using PolarisLite.Web.Components;

namespace PolarisLite.Web;

public interface IInteractionsService
{
    IInteractionsService Release();
    IInteractionsService MoveToElement<TComponent>(TComponent component)
        where TComponent : ComponentAdapter;
    IInteractionsService DragAndDrop<TComponent>(TComponent sourceComponent, TComponent destinationElement)
        where TComponent : ComponentAdapter;
    IInteractionsService DoubleClick<TComponent>(TComponent element)
        where TComponent : ComponentAdapter;
    IInteractionsService ClickAndHold<TComponent>(TComponent element)
        where TComponent : ComponentAdapter;
    void Perform();
}
