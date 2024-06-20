using PolarisLite.Mobile.Components;

namespace PolarisLite.Mobile.Services;

public partial class DriverAdapter : ITouchActionsService
{
    public ITouchActionsService LongPress<TComponent>(TComponent component) 
        where TComponent : AndroidComponent
    {
        _actions.ClickAndHold(component.WrappedElement).Perform();
        return this;
    }

    public ITouchActionsService LongPress(int x, int y)
    {
        _actions.MoveByOffset(x, y).ClickAndHold().Perform();
        return this;
    }

    public ITouchActionsService MoveTo<TComponent>(TComponent component) 
        where TComponent : AndroidComponent
    {
        _actions.MoveToElement(component.WrappedElement).Perform();
        return this;
    }

    public ITouchActionsService MoveTo(int x, int y)
    {
        _actions.MoveByOffset(x, y).Perform();
        return this;
    }

    public void Perform()
    {
        _actions.Perform();
    }

    public ITouchActionsService Press<TComponent>(TComponent component) 
        where TComponent : AndroidComponent
    {
        _actions.Click(component.WrappedElement).Perform();
        return this;
    }

    public ITouchActionsService Press(int x, int y)
    {
        _actions.MoveByOffset(x, y).Click().Perform();
        return this;
    }

    public ITouchActionsService Release()
    {
        _actions.Release().Perform();
        return this;
    }

    public ITouchActionsService Swipe<TComponent>(TComponent firstComponent, TComponent secondComponent, int duration) 
        where TComponent : AndroidComponent
    {
        _actions.ClickAndHold(firstComponent.WrappedElement)
                .MoveToElement(secondComponent.WrappedElement)
                .Release()
                .Perform();
        return this;
    }

    public ITouchActionsService Swipe(int startx, int starty, int endx, int endy, int duration)
    {
        _actions.MoveByOffset(startx, starty)
                .ClickAndHold()
                .MoveByOffset(endx - startx, endy - starty)
                .Release()
                .Perform();
        return this;
    }

    public ITouchActionsService Tap<TComponent>(TComponent component, int count) 
        where TComponent : AndroidComponent
    {
        for (int i = 0; i < count; i++)
        {
            _actions.Click(component.WrappedElement).Perform();
        }
        return this;
    }

    public ITouchActionsService Wait(int waitTimeMilliseconds)
    {
        System.Threading.Thread.Sleep(waitTimeMilliseconds);
        return this;
    }
}
