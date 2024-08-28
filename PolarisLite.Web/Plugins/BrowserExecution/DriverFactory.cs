using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using WebDriverManager.DriverConfigs.Impl;
using System.Drawing;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chromium;

namespace PolarisLite.Web.Plugins;
public class DriverFactory
{
    public static bool Disposed { get; set; } = true;

    public static BrowserConfiguration BrowserConfiguration { get; set; } = new BrowserConfiguration();

    public static Dictionary<string, string> CustomDriverOptions { get; set; } = new Dictionary<string, string>();
    public static Dictionary<string, object> CustomGridOptions { get; set; } = new Dictionary<string, object>();

    public static ExecutionType ExecutionType { get; set; }
    public static GridSettings GridSettings { get; set; }
    public static IWebDriver WrappedDriver { get; set; }
    public static string CurrentSessionId { get; set; }

    public static void Start(BrowserConfiguration browserConfiguration)
    {
        var options = InitializeOptions(browserConfiguration.Browser, browserConfiguration.BrowserVersion);
        var mobileEmulationOptions = InitializeMobileEmulationOptions(browserConfiguration);

        ExecutionType = browserConfiguration.ExecutionType;
        switch (browserConfiguration.Browser)
        {
            case BrowserType.Chrome:
                new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                ChromeOptions chromeOptions = options as ChromeOptions;

                if (browserConfiguration.MobileEmulation)
                {
                    chromeOptions.EnableMobileEmulation(mobileEmulationOptions);
                    chromeOptions.EnableMobileEmulation(browserConfiguration.DeviceName);
                }

                WrappedDriver = new ChromeDriver(chromeOptions);
                break;
            case BrowserType.Edge:
                new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                EdgeOptions edgeOptions = options as EdgeOptions;

                if (browserConfiguration.MobileEmulation)
                {
                    edgeOptions.EnableMobileEmulation(mobileEmulationOptions);
                    edgeOptions.EnableMobileEmulation(browserConfiguration.DeviceName);
                }

                WrappedDriver = new EdgeDriver(edgeOptions);
                break;
            case BrowserType.Firefox:
                new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                WrappedDriver = new FirefoxDriver(options as FirefoxOptions);
                break;
            case BrowserType.Safari:
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

    public static void StartGrid(BrowserConfiguration browserConfiguration, GridSettings gridSettings)
    {
        var options = InitializeOptions(browserConfiguration.Browser, browserConfiguration.BrowserVersion);
        AddGridOptions(options, gridSettings);
        GridSettings = gridSettings;
        BrowserConfiguration = browserConfiguration;
        ExecutionType = browserConfiguration.ExecutionType;
        WrappedDriver = new RemoteWebDriver(new Uri(gridSettings.Url), options);
        CurrentSessionId = ((RemoteWebDriver)WrappedDriver).SessionId.ToString();
        WrappedDriver.Manage().Window.Maximize();
        Disposed = false;
    }

    private static DriverOptions InitializeOptions(BrowserType browserType, string browserVersion)
    {
        DriverOptions options = default;
        switch (browserType)
        {
            case BrowserType.Chrome:
                options = new ChromeOptions();
                options.AcceptInsecureCertificates = true;
                break;
            case BrowserType.Firefox:
                options = new FirefoxOptions();
                break;

            case BrowserType.Edge:
                options = new EdgeOptions();
                options.AcceptInsecureCertificates = true;
                break;

            case BrowserType.Safari:
                options = new SafariOptions();
                break;
        }

        options.BrowserVersion = browserVersion;
        return options;
    }

    private static void AddGridOptions<TOptions>(TOptions options, GridSettings gridSettings) 
        where TOptions : DriverOptions
    {
        Dictionary<string, object> args = new();
        foreach (var entry in gridSettings?.Arguments)
        {
            args.Add(entry.Key, entry.Value);
        }

        foreach (var entry in CustomGridOptions)
        {
            args.Add(entry.Key, entry.Value);
        }

        args.Add("name", gridSettings.TestName);
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
