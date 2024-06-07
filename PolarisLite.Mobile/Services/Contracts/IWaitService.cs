using PolarisLite.Mobile.Components;

namespace PolarisLite.Mobile;

public interface IWaitService
{
    void Wait<TWaitStrategy, TComponent>(TComponent element, TWaitStrategy waitStrategy)
      where TWaitStrategy : WaitStrategy
      where TComponent : AndroidComponent;

    void WaitInternal<TBy, TUntil>(TBy by, TUntil until)
        where TBy : FindStrategy
        where TUntil : WaitStrategy;
}
