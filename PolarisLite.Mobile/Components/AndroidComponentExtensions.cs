using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarisLite.Mobile.Components;
public static class AndroidComponentExtensions
{
    public static TComponent ToExist<TComponent>(this TComponent component) where TComponent : AndroidComponent
    {
        var waitStrategy = new ToExistWaitStrategy();
        component.EnsureState(waitStrategy);
        return component;
    }

    public static TComponent ToNotExist<TComponent>(this TComponent component) where TComponent : AndroidComponent
    {
        var waitStrategy = new NotExistWaitStrategy();
        component.EnsureState(waitStrategy);
        return component;
    }

    public static TComponent ToBeVisible<TComponent>(this TComponent component) where TComponent : AndroidComponent
    {
        var waitStrategy = new ToBeVisibleWaitStrategy();
        component.EnsureState(waitStrategy);
        return component;
    }

    public static TComponent ToNotBeVisible<TComponent>(this TComponent component) where TComponent : AndroidComponent
    {
        var waitStrategy = new NotBeVisibleWaitStrategy();
        component.EnsureState(waitStrategy);
        return component;
    }

    public static TComponent ToBeClickable<TComponent>(this TComponent component) where TComponent : AndroidComponent
    {
        var waitStrategy = new ToBeClickableWaitStrategy();
        component.EnsureState(waitStrategy);
        return component;
    }

    public static TComponent ToHaveContent<TComponent>(this TComponent component) where TComponent : AndroidComponent
    {
        var waitStrategy = new ToHaveContentWaitStrategy();
        component.EnsureState(waitStrategy);
        return component;
    }

    public static TComponent ToExist<TComponent>(this TComponent component, int timeoutInterval, int sleepInterval) where TComponent : AndroidComponent
    {
        var waitStrategy = new ToExistWaitStrategy(timeoutInterval, sleepInterval);
        component.EnsureState(waitStrategy);
        return component;
    }

    public static TComponent ToNotExist<TComponent>(this TComponent component, int timeoutInterval, int sleepInterval) 
        where TComponent : AndroidComponent
    {
        var waitStrategy = new NotExistWaitStrategy(timeoutInterval, sleepInterval);
        component.EnsureState(waitStrategy);
        return component;
    }

    public static TComponent ToBeVisible<TComponent>(this TComponent component, int timeoutInterval, int sleepInterval) where TComponent : AndroidComponent
    {
        var waitStrategy = new ToBeVisibleWaitStrategy(timeoutInterval, sleepInterval);
        component.EnsureState(waitStrategy);
        return component;
    }

    public static TComponent ToBeClickable<TComponent>(this TComponent component, int timeoutInterval, int sleepInterval) where TComponent : AndroidComponent
    {
        var waitStrategy = new ToBeClickableWaitStrategy(timeoutInterval, sleepInterval);
        component.EnsureState(waitStrategy);
        return component;
    }

    public static TComponent ToHaveContent<TComponent>(this TComponent component, int timeoutInterval, int sleepInterval) where TComponent : AndroidComponent
    {
        var waitStrategy = new ToHaveContentWaitStrategy(timeoutInterval, sleepInterval);
        component.EnsureState(waitStrategy);
        return component;
    }
}

