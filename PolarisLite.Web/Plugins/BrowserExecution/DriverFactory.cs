using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using WebDriverManager.DriverConfigs.Impl;

namespace PolarisLite.Web.Plugins.BrowserExecution;
public class DriverFactory : IDisposable
{
    private static readonly ThreadLocal<bool> _disposed = new ThreadLocal<bool>(() => true);
    private static readonly ThreadLocal<BrowserConfiguration> _browserConfiguration = new ThreadLocal<BrowserConfiguration>();
    private static readonly ThreadLocal<Dictionary<string, string>> _customDriverOptions = new ThreadLocal<Dictionary<string, string>>();
    private static readonly ThreadLocal<IWebDriver> _wrappedDriver = new ThreadLocal<IWebDriver>();

    public static bool Disposed
    {
        get { return _disposed.Value; }
        set { _disposed.Value = value; }
    }

    public static BrowserConfiguration BrowserConfiguration
    {
        get { return _browserConfiguration.Value; }
        set { _browserConfiguration.Value = value; }
    }

    public static Dictionary<string, string> CustomDriverOptions
    {
        get { return _customDriverOptions.Value; }
        set { _customDriverOptions.Value = value; }
    }

    public static IWebDriver WrappedDriver
    {
        get { return _wrappedDriver.Value; }
        set { _wrappedDriver.Value = value; }
    }

    // TODO: add methods for LambdaTest + ConfigurationService
    public void Start(Browser browser)
    {
        switch (browser)
        {
            case Browser.Chrome:
                new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                WrappedDriver = new ChromeDriver();
                break;
            case Browser.Firefox:
                new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                WrappedDriver = new FirefoxDriver();
                break;
            case Browser.Edge:
                new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                WrappedDriver = new EdgeDriver();
                break;
            case Browser.Safari:
                WrappedDriver = new SafariDriver();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(browser), browser, null);
        }

        WrappedDriver.Manage().Window.Maximize();
        Disposed = false;
        //ServiceLocator.Instance.RegisterInstance((IWebDriver)_wrappedDriver);

        //// resolve in DriverAdapter?
        //ServiceLocator.Instance.RegisterInstance(new WebDriverWait(WrappedDriver, TimeSpan.FromSeconds(30)));
        //ServiceLocator.Instance.RegisterInstance(new NativeElementFindService(WrappedDriver, WrappedDriver));
        ////
        ////_webDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
        ////_nativeElementFindService = new NativeElementFindService(_webDriver, _webDriver);
    }


    public void Dispose()
    {
        if (!Disposed)
        {
            WrappedDriver.Quit();
            GC.SuppressFinalize(this);
            Disposed = true;
        }
    }
}
