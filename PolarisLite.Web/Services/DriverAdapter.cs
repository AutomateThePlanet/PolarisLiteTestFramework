using PolarisLite.Web.Plugins.BrowserExecution;

namespace PolarisLite.Web.Services;

public partial class DriverAdapter
{
    private IWebDriver _webDriver;
    private WebDriverWait _webDriverWait;
    private List<WaitStrategy> _waitStrategies;

    public DriverAdapter()
    {
        _webDriver = DriverFactory.WrappedDriver;
        _webDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
        _waitStrategies = new List<WaitStrategy>();
        _waitStrategies.Add(new ToExistWaitStrategy());
    }

    public IWebDriver WrappedDriver => _webDriver;

    public DriverAdapter(IWebDriver driver)
    {
        _webDriver = driver;
        _webDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
        _waitStrategies = new List<WaitStrategy>();
        _waitStrategies.Add(new ToExistWaitStrategy());
    }

    public void EnsureState(WaitStrategy waitStrategy)
    {
        _waitStrategies.Add(waitStrategy);
    }
}
