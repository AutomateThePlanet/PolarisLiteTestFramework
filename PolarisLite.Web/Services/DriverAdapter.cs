using PolarisLite.Web.Plugins.BrowserExecution;

namespace PolarisLite.Web.Services;

public partial class DriverAdapter
{
    private IWebDriver _webDriver;
    private WebDriverWait _webDriverWait;

    public DriverAdapter()
    {
        _webDriver = DriverFactory.WrappedDriver;
        _webDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));

        if (_webDriver is IDevTools)
        {
            DevToolsSession = ((IDevTools)_webDriver).GetDevToolsSession();
            DevToolsSessionDomains = DevToolsSession.GetVersionSpecificDomains<DevToolsSessionDomains>();
            RequestsHistory = new List<NetworkRequestSentEventArgs>();
            ResponsesHistory = new List<NetworkResponseReceivedEventArgs>();
        }
    }

    public DriverAdapter(IWebDriver driver)
    {
        _webDriver = driver;
        _webDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
    }

    public IWebDriver WrappedDriver => _webDriver;
}
