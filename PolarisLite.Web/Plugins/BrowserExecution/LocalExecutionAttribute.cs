namespace PolarisLite.Web.Plugins;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class LocalExecutionAttribute : Attribute
{
    public LocalExecutionAttribute(BrowserType browser = BrowserType.Chrome, Lifecycle lifecycle = Lifecycle.RestartEveryTime)
    {
        BrowserConfiguration = new BrowserConfiguration(browser, lifecycle);
    }

    public LocalExecutionAttribute(BrowserType browser, Lifecycle lifecycle, string browserVersion = "latest", DesktopWindowSize size = DesktopWindowSize._1920_1080)
    {
        BrowserConfiguration = new BrowserConfiguration(browser, lifecycle);
        BrowserConfiguration.BrowserVersion = browserVersion;
        BrowserConfiguration.Size = WindowsSizeResolver.GetWindowSize(size);
    }

    // Supported only for Chromium
    public LocalExecutionAttribute(BrowserType browser, Lifecycle lifecycle, bool mobileEmulation, string deviceName, MobileWindowSize size, double pixelRation, string userAgent)
    {
        BrowserConfiguration = new BrowserConfiguration(browser, lifecycle);
        BrowserConfiguration.MobileEmulation = mobileEmulation;
        BrowserConfiguration.Size = WindowsSizeResolver.GetWindowSize(size);
        BrowserConfiguration.DeviceName = deviceName;
        BrowserConfiguration.PixelRation = pixelRation;
        BrowserConfiguration.UserAgent = userAgent;
    }

    public LocalExecutionAttribute(BrowserType browser, Lifecycle lifecycle, bool mobileEmulation, string deviceName, TabletWindowSize size, double pixelRation, string userAgent)
    {
        BrowserConfiguration = new BrowserConfiguration(browser, lifecycle);
        BrowserConfiguration.MobileEmulation = mobileEmulation;
        BrowserConfiguration.Size = WindowsSizeResolver.GetWindowSize(size);
        BrowserConfiguration.DeviceName = deviceName;
        BrowserConfiguration.PixelRation = pixelRation;
        BrowserConfiguration.UserAgent = userAgent;
    }

    public LocalExecutionAttribute(BrowserType browser, Lifecycle lifecycle, bool mobileEmulation, string deviceName, DesktopWindowSize size, double pixelRation, string userAgent)
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
