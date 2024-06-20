using PolarisLite.Mobile.Plugins.AppExecution;
using PolarisLite.Mobile.Services;
using PolarisLite.Mobile.Services.Contracts;

namespace PolarisLite.Mobile;
public class App
{
    private readonly DriverAdapter _driver;

    public App()
    {
        _driver = new DriverAdapter();
    }

    public IElementFindService Elements => _driver;
    public IDeviceService Device => _driver;
    public IKeyboardService Keyboard => _driver;
    public IFileSystemService FileSystem => _driver;
    public IAppService AppService => _driver;
    public ITouchActionsService TouchActions => _driver;
    public IWebService Web => _driver;

    public void AddBrowserOptions(string option, string value)
    {
        DriverFactory.CustomDriverOptions.Add(option, value);
    }

    public TPage Create<TPage>()
        where TPage : AndroidView, new()
    {
        TPage pageInstance = new TPage();
        return pageInstance;
    }
}
