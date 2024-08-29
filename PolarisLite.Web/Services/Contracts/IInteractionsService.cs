using PolarisLite.Web.Components;

namespace PolarisLite.Web;

public interface IInteractionsService
{
    IInteractionsService Release();
    IInteractionsService MoveToElement<TComponent>(TComponent component)
        where TComponent : WebComponent;
    IInteractionsService DragAndDrop<TComponent>(TComponent sourceComponent, TComponent destinationElement)
        where TComponent : WebComponent;
    IInteractionsService DoubleClick<TComponent>(TComponent element)
        where TComponent : WebComponent;
    IInteractionsService ClickAndHold<TComponent>(TComponent element)
        where TComponent : WebComponent;

    IInteractionsService Click<TComponent>(TComponent element)
    where TComponent : WebComponent;

    IInteractionsService SendKeys(string keysToSend);

    void Perform();
}
