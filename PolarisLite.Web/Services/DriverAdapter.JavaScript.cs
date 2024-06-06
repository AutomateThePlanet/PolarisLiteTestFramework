using PolarisLite.Web.Components;

namespace PolarisLite.Web.Services;

public partial class DriverAdapter : IJavaScriptService
{
    public object Execute<TComponent>(string script, TComponent element, params object[] args) 
        where TComponent : ComponentAdapter
    {
        return ((IJavaScriptExecutor)_webDriver).ExecuteScript(script, element.WrappedElement, args);
    }

    public object Execute(string script, params object[] args)
    {
        return ((IJavaScriptExecutor)_webDriver).ExecuteScript(script, args);
    }
}
