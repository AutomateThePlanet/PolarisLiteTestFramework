using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using System.Net.Sockets;
using System.Net;
using WebDriverManager.DriverConfigs.Impl;
using System.Drawing;
using OpenQA.Selenium.Remote;
using PolarisLite.Core;
using System.Text;
using PolarisLite.Secrets;
using OpenQA.Selenium.IE;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Chromium;

namespace PolarisLite.Web.Plugins.BrowserExecution;
public class DriverFactory
{
    private static readonly ThreadLocal<bool> _disposed = new ThreadLocal<bool>(() => true);
    private static readonly ThreadLocal<BrowserConfiguration> _browserConfiguration = new ThreadLocal<BrowserConfiguration>();
    private static readonly ThreadLocal<Dictionary<string, string>> _customDriverOptions = new ThreadLocal<Dictionary<string, string>>();
    private static readonly ThreadLocal<IWebDriver> _wrappedDriver = new ThreadLocal<IWebDriver>();
    private static readonly ThreadLocal<ExecutionType> _executionType = new ThreadLocal<ExecutionType>();

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

    public static ExecutionType ExecutionType
    {
        get { return _executionType.Value; }
        set { _executionType.Value = value; }
    }

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

        WrappedDriver.Manage().Window.Maximize();


        Disposed = false;
    }

    public static void Start(Browser browser)
    {
        var webSettings = ConfigurationService.GetSection<WebSettings>();
        var options = InitializeOptionsFromConfig(webSettings);
        var gridSettings = webSettings.GridSettings.FirstOrDefault(x => x.ProviderName.ToLower() == webSettings.ExecutionType);

        AddOptionsConfig(options, gridSettings);
        if (browser == Browser.NotSet)
        {
            browser = (Browser)Enum.Parse(typeof(Browser), webSettings.DefaultBrowser.RemoveSpacesAndCapitalize());
        }

        switch (browser)
        {
            case Browser.Chrome:
                new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                WrappedDriver = new ChromeDriver(options as ChromeOptions);
                break;
            case Browser.Firefox:
                new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                WrappedDriver = new FirefoxDriver(options as FirefoxOptions);
                break;
            case Browser.Edge:
                new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                WrappedDriver = new EdgeDriver(options as EdgeOptions);
                break;
            case Browser.Safari:
                WrappedDriver = new SafariDriver(options as SafariOptions);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(browser), browser, null);
        }

        WrappedDriver.Manage().Window.Maximize();
        Disposed = false;
    }

    public static void StartGrid(BrowserConfiguration browserConfiguration, GridConfiguration gridSettings)
    {
        var options = InitializeOptions(browserConfiguration.Browser, browserConfiguration.BrowserVersion);
        AddGridOptions(options, gridSettings);

        WrappedDriver = new RemoteWebDriver(new Uri(gridSettings.Url), options);
        WrappedDriver.Manage().Window.Maximize();
        Disposed = false;
    }

    public static void StartGrid()
    {
        var webSettings = ConfigurationService.GetSection<WebSettings>();
        //var options = new ChromeOptions();
        var options = InitializeOptionsFromConfig(webSettings);
        var gridSettings = webSettings.GridSettings.First(x => x.ProviderName == webSettings.ExecutionType);
        AddGridOptionsConfig(options, gridSettings);
        //options.AddAdditionalOption(gridSettings.OptionsName, args);
        var gridUrl = ConstructGridUrl(gridSettings.Url);

        WrappedDriver = new RemoteWebDriver(new Uri(gridUrl), options);
        WrappedDriver.Manage().Window.Maximize();
        Disposed = false;
    }

    private static ChromiumMobileEmulationDeviceSettings InitializeMobileEmulationOptions(BrowserConfiguration browserConfiguration)
    {
        ChromiumMobileEmulationDeviceSettings deviceOptions = default;
        if (browserConfiguration.MobileEmulation)
        {
            deviceOptions = new ChromiumMobileEmulationDeviceSettings();
            deviceOptions.UserAgent = "Mozilla/5.0 (Linux; Android 4.2.1; en-us; Nexus 5 Build/JOP40D) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.166 Mobile Safari/535.19";
            deviceOptions.Width = browserConfiguration.Size.Width;
            deviceOptions.Height = browserConfiguration.Size.Height;
            deviceOptions.EnableTouchEvents = true;
            deviceOptions.PixelRatio = browserConfiguration.PixelRation;
        }

        return deviceOptions;
    }

    private static DriverOptions InitializeOptionsFromConfig(WebSettings webSettings)
    {
        Browser browserType = (Browser)Enum.Parse(typeof(Browser), webSettings.DefaultBrowser.RemoveSpacesAndCapitalize());
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

        options.BrowserVersion = webSettings.BrowserVersion;
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

    private static void AddGridOptionsConfig<TOptions>(TOptions options, GridConfiguration gridSettings) 
        where TOptions : DriverOptions
    {
        if(gridSettings == null)
        {
            return;
        }

        //foreach (var entry in gridSettings?.Arguments)
        //{
        //    foreach (var c in entry)
        //    {
        //        if (c.Value is string value && value.StartsWith("{env_"))
        //        {
        //            var envValue = SecretsResolver.GetSecret(value);
        //            options.AddAdditionalOption(c.Key, envValue);
        //        }
        //        else
        //        {
        //            options.AddAdditionalOption(c.Key, c.Value);
        //        }
        //    }
        //}
        Dictionary<string, object> args = new();
        foreach (var entry in gridSettings?.Arguments)
        {
            // handle mask command for LambdaTest
            if (entry.Key.Equals("maskCommands", StringComparison.OrdinalIgnoreCase))
            {
                if (entry.Value is JArray maskCommandsArray)
                {
                    var maskCommands = maskCommandsArray.ToObject<string[]>();
                    args.Add(entry.Key, maskCommands);
                }
            }
            else if (entry.Value is string value && value.StartsWith("{env_"))
            {
                var envValue = SecretsResolver.GetSecret(value);
                args.Add(entry.Key, envValue);
            }
            else
            {
                args.Add(entry.Key, entry.Value);
            }
        }

        options.AddAdditionalOption(gridSettings.OptionsName, args);
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

    private static void AddOptionsConfig<TOptions>(TOptions options, GridConfiguration gridSettings)
        where TOptions : DriverOptions
    {
        if (gridSettings == null)
        {
            return;
        }

        foreach (var entry in gridSettings?.Arguments)
        {
            if (entry.Value is string value && value.StartsWith("{env_"))
            {
                var envValue = SecretsResolver.GetSecret(value);
                options.AddAdditionalOption(entry.Key, envValue);
            }
            else
            {
                options.AddAdditionalOption(entry.Key, entry.Value);
            }
        }
    }

    private static string ConstructGridUrl(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            return url;
        }

        var resolvedUrl = url;
        var startIndex = 0;
        while ((startIndex = resolvedUrl.IndexOf("{env_", startIndex)) != -1)
        {
            var endIndex = resolvedUrl.IndexOf("}", startIndex);
            if (endIndex == -1)
            {
                throw new ArgumentException($"Invalid placeholder in URL: {resolvedUrl.Substring(startIndex)}");
            }

            var placeholder = resolvedUrl.Substring(startIndex, endIndex - startIndex + 1);
            var envVariable = placeholder.Substring(1, placeholder.Length - 2); // Removing { and }
            var envValue = SecretsResolver.GetSecret(envVariable);

            resolvedUrl = resolvedUrl.Replace(placeholder, envValue);
        }

        return resolvedUrl;
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

    private static int GetFreeTcpPort()
    {
        Thread.Sleep(100);
        var tcpListener = new TcpListener(IPAddress.Loopback, 0);
        tcpListener.Start();
        int port = ((IPEndPoint)tcpListener.LocalEndpoint).Port;
        tcpListener.Stop();
        return port;
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
