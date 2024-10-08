﻿using PolarisLite.Web.Components;

namespace PolarisLite.Web;
public static class WebComponentExtensions
{
    public static TComponent ToExist<TComponent>(this TComponent component) 
        where TComponent : WebComponent
    {
        var waitStrategy = new ToExistWaitStrategy();
        component.EnsureState(waitStrategy);
        return component;
    }

    public static TComponent ToBeVisible<TComponent>(this TComponent component) 
        where TComponent : WebComponent
    {
        var waitStrategy = new ToBeVisibleWaitStrategy();
        component.EnsureState(waitStrategy);
        return component;
    }

    public static TComponent ToBeClickable<TComponent>(this TComponent component) 
        where TComponent : WebComponent
    {
        var waitStrategy = new ToBeClickableWaitStrategy();
        component.EnsureState(waitStrategy);
        return component;
    }

    public static TComponent ToExist<TComponent>(this TComponent component, int timeoutInterval, int sleepInterval) 
        where TComponent : WebComponent
    {
        var waitStrategy = new ToExistWaitStrategy(timeoutInterval, sleepInterval);
        component.EnsureState(waitStrategy);
        return component;
    }

    public static TComponent ToBeVisible<TComponent>(this TComponent component, int timeoutInterval, int sleepInterval) 
        where TComponent : WebComponent
    {
        var waitStrategy = new ToBeVisibleWaitStrategy(timeoutInterval, sleepInterval);
        component.EnsureState(waitStrategy);
        return component;
    }

    public static TComponent ToBeClickable<TComponent>(this TComponent component, int timeoutInterval, int sleepInterval)
        where TComponent : WebComponent
    {
        var waitStrategy = new ToBeClickableWaitStrategy(timeoutInterval, sleepInterval);
        component.EnsureState(waitStrategy);
        return component;
    }
}

