using PolarisLite.Mobile.Components;

namespace PolarisLite.Mobile;

public interface ITouchActionsService
{
    ITouchActionsService Tap<TComponent>(TComponent component, int count) where TComponent : AndroidComponent;
    ITouchActionsService Press<TComponent>(TComponent component) where TComponent : AndroidComponent;
    ITouchActionsService Press(int x, int y);
    ITouchActionsService LongPress<TComponent>(TComponent component) where TComponent : AndroidComponent;
    ITouchActionsService LongPress(int x, int y);
    ITouchActionsService Wait(int waitTimeMilliseconds);
    ITouchActionsService MoveTo<TComponent>(TComponent component) where TComponent : AndroidComponent;
    ITouchActionsService MoveTo(int x, int y);
    ITouchActionsService Release();
    ITouchActionsService Swipe<TComponent>(TComponent firstComponent, TComponent secondComponent, int duration) where TComponent : AndroidComponent;
    ITouchActionsService Swipe(int startx, int starty, int endx, int endy, int duration);
    void Perform();
}
