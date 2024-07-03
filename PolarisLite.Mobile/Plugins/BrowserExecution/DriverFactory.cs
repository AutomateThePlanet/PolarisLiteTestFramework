using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace PolarisLite.Mobile.Plugins.AppExecution;
public class DriverFactory : IDisposable
{
    //private static readonly TimeSpan IMPLICIT_TIMEOUT = TimeSpan.FromSeconds(30);
    //private static readonly ThreadLocal<bool> Disposed = new ThreadLocal<bool>(() => false);
    //private static readonly ThreadLocal<AppConfiguration> _appConfiguration = new ThreadLocal<AppConfiguration>();
    //private static readonly ThreadLocal<Dictionary<string, string>> CustomDriverOptions = new ThreadLocal<Dictionary<string, string>>(() => new Dictionary<string, string>());
    //private static readonly ThreadLocal<AndroidDriver> WrappedAndroidDriver = new ThreadLocal<AndroidDriver>();

    public static bool Disposed { get; set; }

    public static AppConfiguration AppConfiguration { get; set; }

    public static Dictionary<string, string> CustomDriverOptions { get; set; } = new Dictionary<string, string>();

    public static AndroidDriver WrappedAndroidDriver { get; set; }

    public AndroidDriver StartApp(AppConfiguration configuration)
    {
        AppConfiguration = configuration;
        Disposed = false;

        var executionType = configuration.ExecutionType;

        if (executionType.ToString().ToLower().Equals("regular"))
        {
            WrappedAndroidDriver = InitializeDriverRegularMode();
        }
        else
        {
            throw new NotSupportedException("Currently the grid mode is not supported.");
        }

        return WrappedAndroidDriver;
    }

    private AndroidDriver InitializeDriverRegularMode()
    {
        var gridUrl = AppConfiguration.Url;
        var caps = new AppiumOptions();
        caps.PlatformName = "Android";
        caps.AutomationName = "UiAutomator2";
        caps.PlatformVersion = AppConfiguration.AndroidVersion;
        caps.DeviceName = AppConfiguration.DeviceName;

        if (AppConfiguration.IsMobileWebExecution)
        {
            caps.BrowserName = AppConfiguration.DefaultBrowser;
        }
        else
        {
            string testAppPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", AppConfiguration.AppPath);
            caps.App = testAppPath;
            caps.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppPackage, AppConfiguration.AppPackage);
            caps.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppActivity, AppConfiguration.AppActivity);
        }

        AddCustomDriverOptions(caps);
        var driver = new AndroidDriver(new Uri(gridUrl), caps);
        WrappedAndroidDriver = driver;
        return driver;
    }

    private void AddCustomDriverOptions(AppiumOptions caps)
    {
        foreach (var optionKey in CustomDriverOptions.Keys)
        {
            caps.AddAdditionalAppiumOption(optionKey, CustomDriverOptions[optionKey]);
        }
    }

    public void Dispose()
    {
        if (Disposed)
        {
            return;
        }

        if (WrappedAndroidDriver != null)
        {
            WrappedAndroidDriver.Quit();
            CustomDriverOptions.Clear();
        }

        Disposed = true;
    }
}
