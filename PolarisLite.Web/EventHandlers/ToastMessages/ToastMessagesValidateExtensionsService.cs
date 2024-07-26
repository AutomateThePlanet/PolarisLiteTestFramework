using PolarisLite.Web.Events;
using PolarisLite.Web.Services;

namespace PolarisLite.Web.Extensions.Controls.Controls.EventHandlers;

public class ToastMessagesValidateExtensionsService : ValidateExtensionsEventHandlers
{
    protected override void ValidatedIsVisibleEventHandler(object sender, ComponentActionEventArgs arg)
    {
        new DriverAdapter().InfoToastMessage($"Validate {arg.Element.FindStrategy.ToString()} is visible");
    }

    protected override void ValidatedIsNotVisibleEventHandler(object sender, ComponentActionEventArgs arg)
    {
        new DriverAdapter().InfoToastMessage($"Validate {arg.Element.FindStrategy.ToString()} is NOT visible");
    }

    protected override void ValidatedValueIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
        new DriverAdapter().InfoToastMessage($"Validate {arg.Element.FindStrategy.ToString()} value is NULL");
    }

    protected override void ValidatedValueIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
        new DriverAdapter().InfoToastMessage($"Validate {arg.Element.FindStrategy.ToString()} value is '{arg.ActionValue}'");
    }

    protected override void ValidatedInnerTextIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
        new DriverAdapter().InfoToastMessage($"Validate {arg.Element.FindStrategy.ToString()} inner text is '{arg.ActionValue}'");
    }

    protected override void ValidatedIsCheckedEventHandler(object sender, ComponentActionEventArgs arg)
    {
        new DriverAdapter().InfoToastMessage($"Validate {arg.Element.FindStrategy.ToString()} is checked");
    }

    protected override void ValidatedIsNotCheckedEventHandler(object sender, ComponentActionEventArgs arg)
    {
        new DriverAdapter().InfoToastMessage($"Validate {arg.Element.FindStrategy.ToString()} is NOT checked");
    }
}