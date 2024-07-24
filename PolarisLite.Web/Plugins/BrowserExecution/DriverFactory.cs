using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using WebDriverManager.DriverConfigs.Impl;
using System.Drawing;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chromium;

namespace PolarisLite.Web.Plugins.BrowserExecution;
public class DriverFactory
{
    public static bool Disposed { get; set; } = true;

    public static BrowserConfiguration BrowserConfiguration { get; set; } = new BrowserConfiguration();

    public static Dictionary<string, string> CustomDriverOptions { get; set; } = new Dictionary<string, string>();

    public static ExecutionType ExecutionType { get; set; }
    public static IWebDriver WrappedDriver { get; set; }

    public static void Start(BrowserConfiguration browserConfiguration)
    {
        var options = InitializeOptions(browserConfiguration.Browser, browserConfiguration.BrowserVersion);
        var mobileEmulationOptions = InitializeMobileEmulationOptions(browserConfiguration);

        ExecutionType = browserConfiguration.ExecutionType;
        switch (browserConfiguration.Browser)
        {
            case Browser.Chrome:
                new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                ChromeOptions chromeOptions = options as ChromeOptions;

                if (browserConfiguration.MobileEmulation)
                {
                    chromeOptions.EnableMobileEmulation(mobileEmulationOptions);
                    chromeOptions.EnableMobileEmulation(browserConfiguration.DeviceName);
                }

                WrappedDriver = new ChromeDriver(chromeOptions);
                break;
            case Browser.Edge:
                new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                EdgeOptions edgeOptions = options as EdgeOptions;

                if (browserConfiguration.MobileEmulation)
                {
                    edgeOptions.EnableMobileEmulation(mobileEmulationOptions);
                    edgeOptions.EnableMobileEmulation(browserConfiguration.DeviceName);
                }

                WrappedDriver = new EdgeDriver(edgeOptions);
                break;
            case Browser.Firefox:
                new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                WrappedDriver = new FirefoxDriver(options as FirefoxOptions);
                break;
            case Browser.Safari:
                WrappedDriver = new SafariDriver(options as SafariOptions);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(browserConfiguration.Browser), browserConfiguration.Browser, null);
        }

        // change window size
        if (browserConfiguration.Size != default)
        {
            WrappedDriver.Manage().Window.Size = browserConfiguration.Size;
        }
        else
        {
            WrappedDriver.Manage().Window.Maximize();
        }

        WrappedDriver.Manage().Window.Maximize();


        Disposed = false;
    }

    private static ChromiumMobileEmulationDeviceSettings InitializeMobileEmulationOptions(BrowserConfiguration browserConfiguration)
    {
        ChromiumMobileEmulationDeviceSettings deviceOptions = default;
        if (browserConfiguration.MobileEmulation)
        {
            deviceOptions = new ChromiumMobileEmulationDeviceSettings();
            deviceOptions.UserAgent = browserConfiguration.UserAgent;
            deviceOptions.Width = browserConfiguration.Size.Width;
            deviceOptions.Height = browserConfiguration.Size.Height;
            deviceOptions.EnableTouchEvents = true;
            deviceOptions.PixelRatio = browserConfiguration.PixelRation;
        }

        return deviceOptions;
    }

    public static void StartGrid(BrowserConfiguration browserConfiguration, GridConfiguration gridSettings)
    {
        var options = InitializeOptions(browserConfiguration.Browser, browserConfiguration.BrowserVersion);
        AddGridOptions(options, gridSettings);

        WrappedDriver = new RemoteWebDriver(new Uri(gridSettings.Url), options);
        WrappedDriver.Manage().Window.Maximize();
        Disposed = false;
    }

    private static DriverOptions InitializeOptions(Browser browserType, string browserVersion)
    {
        DriverOptions options = default;
        switch (browserType)
        {
            case Browser.Chrome:
                options = new ChromeOptions();
                break;
            case Browser.Firefox:
                options = new FirefoxOptions();
                break;

            case Browser.Edge:
                options = new EdgeOptions();
                break;

            case Browser.Safari:
                options = new SafariOptions();
                break;
        }

        options.BrowserVersion = browserVersion;
        return options;
    }

    private static void AddGridOptions<TOptions>(TOptions options, GridConfiguration gridSettings) 
        where TOptions : DriverOptions
    {
        Dictionary<string, object> args = new();
        foreach (var entry in gridSettings?.Arguments)
        {
            args.Add(entry.Key, entry.Value);
        }

        options.AddAdditionalOption(gridSettings.OptionsName, args);
    }

    private static void ChangeWindowSize(Size windowSize, IWebDriver wrappedWebDriver)
    {
        if (windowSize != default)
        {
            wrappedWebDriver.Manage().Window.Size = windowSize;
        }
        else
        {
            wrappedWebDriver.Manage().Window.Maximize();
        }
    }

    public static void Dispose()
    {
        if (!Disposed)
        {
            if (WrappedDriver is IDevTools)
            {
                var devToolsSession = (IDevTools)WrappedDriver;
                devToolsSession.CloseDevToolsSession();
            }

            WrappedDriver.Quit();
            WrappedDriver.Dispose();
            WrappedDriver = null;
            Disposed = true;
        }
    }
}
