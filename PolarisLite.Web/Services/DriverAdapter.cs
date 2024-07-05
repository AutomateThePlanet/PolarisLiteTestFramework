using PolarisLite.Web.Core;

namespace PolarisLite.Web.Services;

public partial class DriverAdapter
{
    private WebDriverWait _webDriverWait;
    private Actions _actions;
    private List<WaitStrategy> _waitStrategies;

    public DriverAdapter()
    {
        WrappedDriver = DriverFactory.WrappedDriver;
        _webDriverWait = new WebDriverWait(WrappedDriver, TimeSpan.FromSeconds(30));
        _actions = new Actions(WrappedDriver);
        _waitStrategies = new List<WaitStrategy>();
        _waitStrategies.Add(new ToExistWaitStrategy());
    }

    public IWebDriver WrappedDriver { get; private set; }
}
