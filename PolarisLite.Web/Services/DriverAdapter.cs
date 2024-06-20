using PolarisLite.Core.Infrastructure;
using PolarisLite.Locators;
using PolarisLite.Web.Components;
using PolarisLite.Web.Plugins.BrowserExecution;

namespace PolarisLite.Web.Services;

public partial class DriverAdapter
{
    private IWebDriver _webDriver;
    private WebDriverWait _webDriverWait;
    private NativeElementFindService _nativeElementFindService;
    private List<WaitStrategy> _waitStrategies;

    public DriverAdapter()
    {
        _webDriver = DriverFactory.WrappedDriver;
        _webDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
        _nativeElementFindService = new NativeElementFindService(_webDriver, _webDriver);
        _waitStrategies = new List<WaitStrategy>();
        _waitStrategies.Add(new ToExistsWaitStrategy());
    }

    public DriverAdapter(IWebDriver driver)
    {
        _webDriver = driver;
        _webDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
        _nativeElementFindService = new NativeElementFindService(_webDriver, _webDriver);
        _waitStrategies = new List<WaitStrategy>();
        _waitStrategies.Add(new ToExistsWaitStrategy());
    }

    public void EnsureState(WaitStrategy waitStrategy)
    {
        _waitStrategies.Add(waitStrategy);
    }
}
