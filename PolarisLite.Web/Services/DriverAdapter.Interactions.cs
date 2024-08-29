using PolarisLite.Web.Components;

namespace PolarisLite.Web.Services;

public partial class DriverAdapter : IInteractionsService
{
    public IInteractionsService Click<TComponent>(TComponent element) 
        where TComponent : WebComponent
    {
        _actions.Click(element.WrappedElement);
        return this;
    }

    public IInteractionsService ClickAndHold<TComponent>(TComponent element) 
        where TComponent : WebComponent
    {
        _actions.ClickAndHold(element.WrappedElement);
        return this;
    }

    public IInteractionsService DoubleClick<TComponent>(TComponent element) 
        where TComponent : WebComponent
    {
        _actions.DoubleClick(element.WrappedElement);
        return this;
    }

    public IInteractionsService DragAndDrop<TComponent>(TComponent sourceComponent, TComponent destinationElement) 
        where TComponent : WebComponent
    {
        _actions.DragAndDrop(sourceComponent.WrappedElement, destinationElement.WrappedElement);
        return this;
    }

    public IInteractionsService MoveToElement<TComponent>(TComponent component) 
        where TComponent : WebComponent
    {
        _actions.MoveToElement(component.WrappedElement);
        return this;
    }

    public void Perform()
    {
       _actions.Build().Perform();
    }

    public IInteractionsService Release()
    {
        _actions.Release();
        return this;
    }

    public IInteractionsService SendKeys(string keysToSend)
    {
        _actions.SendKeys(keysToSend);
        return this;
    }
}
