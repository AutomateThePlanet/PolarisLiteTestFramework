using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using PolarisLite.Mobile.Plugins.AppExecution;

namespace PolarisLite.Mobile.Services;

public partial class DriverAdapter
{
    private AndroidDriver _androidDriver;
    //private AndroidDriverWait _driverWait;
    //private IMultiAction _wrappedMultiAction;
    private List<WaitStrategy> _waitStrategies;

    public DriverAdapter()
    {
        _androidDriver = DriverFactory.WrappedAndroidDriver;
        //_driverWait = new AndroidDriverWait(_androidDriver, new SystemClock(), TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(2));
        _waitStrategies = new List<WaitStrategy>();
        _waitStrategies.Add(new ToExistWaitStrategy());
        //_wrappedMultiAction = new MultiAction(_androidDriver);
    }

    public void EnsureState(WaitStrategy waitStrategy)
    {
        _waitStrategies.Add(waitStrategy);
    }
}
