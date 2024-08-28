using PolarisLite.Web.Plugins;
using System.Collections.Concurrent;

namespace PolarisLite.Web.Services;

public partial class DriverAdapter
{
    private IWebDriver _webDriver;
    
    public DriverAdapter()
    {
        _webDriver = DriverFactory.WrappedDriver;
        if (_webDriver != null)
        {
            if (_webDriver is IDevTools)
            {
                DevToolsSession = ((IDevTools)_webDriver).GetDevToolsSession();
                DevToolsSessionDomains = DevToolsSession.GetVersionSpecificDomains<DevToolsSessionDomains>();
                RequestsHistory = new ConcurrentBag<NetworkRequestSentEventArgs>();
                ResponsesHistory = new ConcurrentBag<NetworkResponseReceivedEventArgs>();
            }
        }
    }

    public DriverAdapter(IWebDriver driver)
    {
        _webDriver = driver;
    }

    public IWebDriver WrappedDriver => _webDriver;
}