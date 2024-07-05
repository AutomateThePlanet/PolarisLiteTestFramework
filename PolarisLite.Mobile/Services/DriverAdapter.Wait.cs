﻿using PolarisLite.Mobile.Components;

namespace PolarisLite.Mobile.Services;

public partial class DriverAdapter : IWaitService
{
    public void Wait<TWaitStrategy, TComponent>(TComponent element, TWaitStrategy waitStrategy)
         where TWaitStrategy : WaitStrategy
         where TComponent : AndroidComponent
    {
        waitStrategy.WaitUntil(element.FindStrategy);
    }
}