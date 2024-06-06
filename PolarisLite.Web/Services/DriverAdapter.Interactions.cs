﻿using PolarisLite.Web.Components;

namespace PolarisLite.Web.Services;

public partial class DriverAdapter : IInteractionsService
{
    public IInteractionsService ClickAndHold<TComponent>(TComponent element) where TComponent : ComponentAdapter => throw new NotImplementedException();
    public IInteractionsService DoubleClick<TComponent>(TComponent element) where TComponent : ComponentAdapter => throw new NotImplementedException();
    public IInteractionsService DragAndDrop<TComponent>(TComponent sourceComponent, TComponent destinationElement) where TComponent : ComponentAdapter => throw new NotImplementedException();
    public IInteractionsService MoveToElement<TComponent>(TComponent component) where TComponent : ComponentAdapter => throw new NotImplementedException();
    public void Perform() => throw new NotImplementedException();
    public IInteractionsService Release() => throw new NotImplementedException();
}
