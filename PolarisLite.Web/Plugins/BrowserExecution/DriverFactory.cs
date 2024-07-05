using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using WebDriverManager.DriverConfigs.Impl;

namespace PolarisLite.Web.Core;
public class DriverFactory
{
    public static bool Disposed { get; set; } = true;
    public static IWebDriver WrappedDriver { get; set; }

    public static void Start(BrowserConfiguration browserConfiguration)
    {
        DriverOptions options = default;
        switch (browserConfiguration.Browser)
        {
            case Browser.Chrome:
                new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                options = new ChromeOptions();
                options.BrowserVersion = browserConfiguration.BrowserVersion;
                WrappedDriver = new ChromeDriver(options as ChromeOptions);
                break;
            case Browser.Firefox:
                new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                options = new FirefoxOptions();
                options.BrowserVersion = browserConfiguration.BrowserVersion;
                WrappedDriver = new FirefoxDriver(options as FirefoxOptions);
                break;
            case Browser.Edge:
                new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                options = new EdgeOptions();
                options.BrowserVersion = browserConfiguration.BrowserVersion;
                WrappedDriver = new EdgeDriver(options as EdgeOptions);
                break;
            case Browser.Safari:
                options = new SafariOptions();
                options.BrowserVersion = browserConfiguration.BrowserVersion;
                WrappedDriver = new SafariDriver(options as SafariOptions);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(browserConfiguration.Browser), browserConfiguration.Browser, null);
        }

        WrappedDriver.Manage().Window.Maximize();
        Disposed = false;
    }

    public static void Dispose()
    {
        if (!Disposed)
        {
            WrappedDriver.Quit();
            WrappedDriver.Dispose();
            WrappedDriver = null;
            Disposed = true;
        }
    }
}
