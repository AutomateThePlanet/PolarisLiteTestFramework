using PolarisLite.Mobile.Components;

namespace PolarisLite.Mobile.Services;

public partial class DriverAdapter : ITouchActionsService
{
    // TODO: Implement when components are ready
    public ITouchActionsService LongPress<TComponent>(TComponent component) where TComponent : AndroidComponent => throw new NotImplementedException();
    public ITouchActionsService LongPress(int x, int y) => throw new NotImplementedException();
    public ITouchActionsService MoveTo<TComponent>(TComponent component) where TComponent : AndroidComponent => throw new NotImplementedException();
    public ITouchActionsService MoveTo(int x, int y) => throw new NotImplementedException();
    public void Perform() => throw new NotImplementedException();
    public ITouchActionsService Press<TComponent>(TComponent component) where TComponent : AndroidComponent => throw new NotImplementedException();
    public ITouchActionsService Press(int x, int y) => throw new NotImplementedException();
    public ITouchActionsService Release() => throw new NotImplementedException();
    public ITouchActionsService Swipe<TComponent>(TComponent firstComponent, TComponent secondComponent, int duration) where TComponent : AndroidComponent => throw new NotImplementedException();
    public ITouchActionsService Swipe(int startx, int starty, int endx, int endy, int duration) => throw new NotImplementedException();
    public ITouchActionsService Tap<TComponent>(TComponent component, int count) where TComponent : AndroidComponent => throw new NotImplementedException();
    public ITouchActionsService Wait(int waitTimeMilliseconds) => throw new NotImplementedException();
}
