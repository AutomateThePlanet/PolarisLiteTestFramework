using OpenQA.Selenium.Appium.Enums;

namespace PolarisLite.Mobile.Plugins.AppExecution;
public class DriverFactory
{
    public static bool Disposed { get; set; }

    public static AppConfiguration AppConfiguration { get; set; }

    public static Dictionary<string, string> CustomDriverOptions { get; set; } = new Dictionary<string, string>();

    public static AndroidDriver WrappedAndroidDriver { get; set; }

    public static AndroidDriver StartApp(AppConfiguration configuration)
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
            WrappedAndroidDriver = InitializeDriverGridMode();
        }

        return WrappedAndroidDriver;
    }

    private static AndroidDriver InitializeDriverGridMode()
    {
        var gridUrl = AppConfiguration.GridSettings.Url;

        var appiumOptions = new AppiumOptions();
        var options = new Dictionary<string, object>
        {
            { "platformName", "android" },
            { "platformVersion", AppConfiguration.AndroidVersion },
            { "deviceName", AppConfiguration.DeviceName },
            { "automationName", "UiAutomator2"}
        };

        if (AppConfiguration.IsMobileWebExecution)
        {
            appiumOptions.BrowserName = AppConfiguration.DefaultBrowser;
        }
        else
        {
            if (AppConfiguration.AppPath.Contains("://"))
            {
                options.Add("app", AppConfiguration.AppPath);
            }
            else
            {
                string testAppPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", AppConfiguration.AppPath);
                appiumOptions.App = testAppPath;
            }

            appiumOptions.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppPackage, AppConfiguration.AppPackage);
            appiumOptions.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppActivity, AppConfiguration.AppActivity);
        }

        appiumOptions.AddAdditionalAppiumOption("name", Guid.NewGuid().ToString());

        AddGridOptionsConfig(options, AppConfiguration.GridSettings);
        appiumOptions.AddAdditionalAppiumOption(AppConfiguration.GridSettings.OptionsName, options);

        WrappedAndroidDriver = new AndroidDriver(new Uri(gridUrl), appiumOptions);

        return WrappedAndroidDriver;
    }

    private static AndroidDriver InitializeDriverRegularMode()
    {
        var gridUrl = AppConfiguration.GridSettings.OptionsName;
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

    private static void AddCustomDriverOptions(AppiumOptions caps)
    {
        foreach (var optionKey in CustomDriverOptions.Keys)
        {
            caps.AddAdditionalAppiumOption(optionKey, CustomDriverOptions[optionKey]);
        }
    }

    private static void AddGridOptionsConfig(Dictionary<string, object> options, GridSettings gridSettings)
    {
        foreach (var entry in gridSettings?.Arguments)
        {
            options.Add(entry.Key, entry.Value);
        }
    }

    public static void Dispose()
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
