using PolarisLite.Mobile.Components;

namespace PolarisLite.Mobile;

public interface IWebService
{
    object Execute(string script, params object[] args);
    object Execute<TComponent>(string script, TComponent element, params object[] args)
        where TComponent : AndroidComponent;
}
