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

    public void Start(Browser browser)
    {
        var webSettings = ConfigurationService.GetSection<WebSettings>();
        var options = InitializeOptionsFromConfig(webSettings);
        var gridSettings = webSettings.GridSettings.FirstOrDefault(x => x.ProviderName.ToLower() == webSettings.ExecutionType);
        AddGridOptions(options, gridSettings);
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

    public void StartGrid()
    {
        var webSettings = ConfigurationService.GetSection<WebSettings>();
        var options = InitializeOptionsFromConfig(webSettings);
        var gridSettings = webSettings.GridSettings.First(x => x.ProviderName == webSettings.ExecutionType);
        options.AddAdditionalOption(gridSettings.OptionsName, gridSettings);
        var gridUrl = ConstructGridUrl(gridSettings.Url);

        WrappedDriver = new RemoteWebDriver(new Uri(gridUrl), options);
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

    private static void AddGridOptions<TOptions>(TOptions options, GridSettings gridSettings) 
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

        foreach (var entry in gridSettings?.Arguments)
        {
            foreach (var c in entry)
            {
                // handle mask command for LambdaTest
                if (c.Key.Equals("maskCommands", StringComparison.OrdinalIgnoreCase))
                {
                    if (c.Value is JArray maskCommandsArray)
                    {
                        var maskCommands = maskCommandsArray.ToObject<string[]>();
                        options.AddAdditionalOption(c.Key, maskCommands);
                    }
                }
                else if (c.Value is string value && value.StartsWith("{env_"))
                {
                    var envValue = SecretsResolver.GetSecret(value);
                    options.AddAdditionalOption(c.Key, envValue);
                }
                else
                {
                    options.AddAdditionalOption(c.Key, c.Value);
                }
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
