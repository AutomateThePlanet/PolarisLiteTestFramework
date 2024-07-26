using PolarisLite.Mobile.Plugins.AppExecution;
using WEB = PolarisLite.Web.Services;

namespace PolarisLite.Mobile.Services;

public partial class DriverAdapter
{
    private AndroidDriver _androidDriver;
    private WEB.DriverAdapter _webDriverAdapter;
    private Actions _actions;

    public DriverAdapter()
    {
        _androidDriver = DriverFactory.WrappedAndroidDriver;
        _webDriverAdapter = new WEB.DriverAdapter(_androidDriver);
        Web.Plugins.DriverFactory.WrappedDriver = _androidDriver;
        _actions = new Actions(_androidDriver);
    }
}