using PolarisLite.Web.Plugins.BrowserExecution;

namespace PolarisLite.Web.Plugins;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class LocalExecutionAttribute : Attribute
{
    public LocalExecutionAttribute(Browser browser, Lifecycle lifecycle)
    {
        BrowserConfiguration = new BrowserConfiguration(browser, lifecycle);
    }

    public LocalExecutionAttribute(Browser browser, Lifecycle lifecycle, string browserVersion, DesktopWindowSize size)
    {
        BrowserConfiguration = new BrowserConfiguration(browser, lifecycle);
        BrowserConfiguration.BrowserVersion = browserVersion;
        BrowserConfiguration.Size = WindowsSizeResolver.GetWindowSize(size);
    }

    // Supported only for Chromium
    public LocalExecutionAttribute(Browser browser, Lifecycle lifecycle, bool mobileEmulation, string deviceName, MobileWindowSize size, double pixelRation, string userAgent)
    {
        BrowserConfiguration = new BrowserConfiguration(browser, lifecycle);
        BrowserConfiguration.MobileEmulation = mobileEmulation;
        BrowserConfiguration.Size = WindowsSizeResolver.GetWindowSize(size);
        BrowserConfiguration.DeviceName = deviceName;
        BrowserConfiguration.PixelRation = pixelRation;
        BrowserConfiguration.UserAgent = userAgent;
    }

    public LocalExecutionAttribute(Browser browser, Lifecycle lifecycle, bool mobileEmulation, string deviceName, TabletWindowSize size, double pixelRation, string userAgent)
    {
        BrowserConfiguration = new BrowserConfiguration(browser, lifecycle);
        BrowserConfiguration.MobileEmulation = mobileEmulation;
        BrowserConfiguration.Size = WindowsSizeResolver.GetWindowSize(size);
        BrowserConfiguration.DeviceName = deviceName;
        BrowserConfiguration.PixelRation = pixelRation;
        BrowserConfiguration.UserAgent = userAgent;
    }

    public LocalExecutionAttribute(Browser browser, Lifecycle lifecycle, bool mobileEmulation, string deviceName, DesktopWindowSize size, double pixelRation, string userAgent)
    {
        BrowserConfiguration = new BrowserConfiguration(browser, lifecycle);
        BrowserConfiguration.MobileEmulation = mobileEmulation;
        BrowserConfiguration.Size = WindowsSizeResolver.GetWindowSize(size);
        BrowserConfiguration.DeviceName = deviceName;
        BrowserConfiguration.PixelRation = pixelRation;
        BrowserConfiguration.UserAgent = userAgent;
    }

    public BrowserConfiguration BrowserConfiguration { get; set; }
}
