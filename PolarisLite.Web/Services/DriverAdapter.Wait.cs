using PolarisLite.Locators;
using PolarisLite.Web.Components;

namespace PolarisLite.Web.Services;

public partial class DriverAdapter : IWaitService
{
    public void Wait<TWaitStrategy, TComponent>(TComponent element, TWaitStrategy waitStrategy)
       where TWaitStrategy : WaitStrategy
       where TComponent : WebComponent
    {
        waitStrategy.WaitUntil(element.FindStrategy);
    }
}
