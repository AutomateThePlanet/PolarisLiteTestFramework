using PolarisLite.Web.Plugins.BrowserExecution;

namespace PolarisLite.Web.Plugins;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class LocalExecutionAttribute : Attribute
{
    public LocalExecutionAttribute(Browser browser, Lifecycle lifecycle)
    {
        BrowserConfiguration = new BrowserConfiguration(browser, lifecycle);
    }

    public BrowserConfiguration BrowserConfiguration { get; set; }
}
