using PolarisLite.Web.Assertions;
using PolarisLite.Web.Events;
using PolarisLite.Web.Services;

namespace PolarisLite.Web.Extensions.Controls.Controls.EventHandlers;

public abstract class ValidateExtensionsEventHandlers
{
    public DriverAdapter DriverAdapter => new DriverAdapter();

    public virtual void SubscribeToAll()
    {
        ValidateComponentExtensions.ValidatedIsCheckedEvent += ValidatedIsCheckedEventHandler;
        ValidateComponentExtensions.ValidatedIsNotCheckedEvent += ValidatedIsNotCheckedEventHandler;
        ValidateComponentExtensions.ValidatedInnerTextIsEvent += ValidatedInnerTextIsEventHandler;
        ValidateComponentExtensions.ValidatedValueIsNullEvent += ValidatedValueIsNullEventHandler;
        ValidateComponentExtensions.ValidatedValueIsEvent += ValidatedValueIsEventHandler;
        ValidateComponentExtensions.ValidatedIsVisibleEvent += ValidatedIsVisibleEventHandler;
        ValidateComponentExtensions.ValidatedIsNotVisibleEvent += ValidatedIsNotVisibleEventHandler;
    }

    protected virtual void ValidatedIsVisibleEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedIsNotVisibleEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedValueIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedValueIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedInnerTextIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedIsCheckedEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedIsNotCheckedEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }
}
