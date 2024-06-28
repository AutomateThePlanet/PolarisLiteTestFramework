using PolarisLite.Mobile.Components;

namespace PolarisLite.Mobile.Services;

public partial class DriverAdapter : IWaitService
{
    public void Wait<TWaitStrategy, TComponent>(TComponent element, TWaitStrategy waitStrategy)
         where TWaitStrategy : WaitStrategy
         where TComponent : AndroidComponent
    {
        try
        {
            WaitInternal(element.FindStrategy, waitStrategy);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void WaitInternal<TBy, TUntil>(TBy by, TUntil until)
        where TBy : FindStrategy
        where TUntil : WaitStrategy
    {
        until?.WaitUntil(by);
    }
}
