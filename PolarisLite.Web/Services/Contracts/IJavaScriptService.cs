using PolarisLite.Web.Components;

namespace PolarisLite.Web;

public interface IJavaScriptService
{
    object Execute(string script, params object[] args);
    object Execute<TComponent>(string script, TComponent element, params object[] args)
        where TComponent : Component;
}
