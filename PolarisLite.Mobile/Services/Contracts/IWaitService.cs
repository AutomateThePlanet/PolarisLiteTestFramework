﻿using PolarisLite.Mobile.Components;

namespace PolarisLite.Mobile;

public interface IWaitService
{
    void Wait<TWaitStrategy, TComponent>(TComponent element, TWaitStrategy waitStrategy)
      where TWaitStrategy : WaitStrategy
      where TComponent : AndroidComponent;
}