using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using PolarisLite.Mobile.Plugins.AppExecution;
using WEB = PolarisLite.Web.Services;

namespace PolarisLite.Mobile.Services;

public partial class DriverAdapter
{
    private AndroidDriver _androidDriver;
    private IWebDriver _webDriver;
    private WEB.DriverAdapter _webDriverAdapter;
    private Actions _actions;
    private List<WaitStrategy> _waitStrategies;

    public DriverAdapter()
    {
        _androidDriver = DriverFactory.WrappedAndroidDriver;
        _webDriverAdapter = new WEB.DriverAdapter(_androidDriver);
        //_driverWait = new AndroidDriverWait(_androidDriver, new SystemClock(), TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(2));
        _waitStrategies = new List<WaitStrategy>();
        _waitStrategies.Add(new ToExistWaitStrategy());
        _actions = new Actions(_androidDriver);
    }

    public void EnsureState(WaitStrategy waitStrategy)
    {
        _waitStrategies.Add(waitStrategy);
    }
}
