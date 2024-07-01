using PolarisLite.Locators;
using PolarisLite.Web.Components;

namespace PolarisLite.Web;
public interface IWaitService
{
    void Wait<TWaitStrategy, TComponent>(TComponent element, TWaitStrategy waitStrategy)
      where TWaitStrategy : WaitStrategy
      where TComponent : WebComponent;
}

