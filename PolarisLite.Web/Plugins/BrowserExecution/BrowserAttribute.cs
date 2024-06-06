namespace PolarisLite.Web.Plugins;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class BrowserAttribute : Attribute
{
    public BrowserAttribute(Browser browser, Lifecycle lifecycle)
    {
        BrowserConfiguration = new BrowserConfiguration(browser, lifecycle);
    }

    public BrowserConfiguration BrowserConfiguration { get; set; }
}
