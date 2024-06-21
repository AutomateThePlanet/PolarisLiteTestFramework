using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using PolarisLite.Core;
using PolarisLite.Secrets;

namespace PolarisLite.Mobile.Plugins.AppExecution;
public class DriverFactory : IDisposable
{
    private static readonly TimeSpan IMPLICIT_TIMEOUT = TimeSpan.FromSeconds(30);
    private static readonly ThreadLocal<bool> _disposed = new ThreadLocal<bool>(() => false);
    private static readonly ThreadLocal<AppConfiguration> _appConfiguration = new ThreadLocal<AppConfiguration>();
    private static readonly ThreadLocal<Dictionary<string, string>> _customDriverOptions = new ThreadLocal<Dictionary<string, string>>(() => new Dictionary<string, string>());
    private static readonly ThreadLocal<AndroidDriver> _wrappedAndroidDriver = new ThreadLocal<AndroidDriver>();
    //private static bool isBuildNameSet = false;

    public static bool Disposed
    {
        get { return _disposed.Value; }
        set { _disposed.Value = value; }
    }

    public static AppConfiguration AppConfiguration
    {
        get { return _appConfiguration.Value; }
        set { _appConfiguration.Value = value; }
    }

    public static Dictionary<string, string> CustomDriverOptions
    {
        get { return _customDriverOptions.Value; }
        set { _customDriverOptions.Value = value; }
    }

    public static AndroidDriver WrappedAndroidDriver
    {
        get { return _wrappedAndroidDriver.Value; }
        set { _wrappedAndroidDriver.Value = value; }
    }

    public AndroidDriver StartApp(AppConfiguration configuration)
    {
        AppConfiguration = configuration;
        Disposed = false;

        //var androidSettings = ConfigurationService.Get<AndroidSettings>();
        var executionType = configuration.ExecutionType;

        AndroidDriver driver;
        if (executionType.Equals("regular"))
        {
            driver = InitializeDriverRegularMode();
        }
        else
        {
            var testName = AppConfiguration.TestName;
            //var gridSettings = androidSettings.GridSettings.FirstOrDefault(g => g.ProviderName.Equals(executionType, StringComparison.OrdinalIgnoreCase));

            driver = InitializeDriverGridMode(testName);
        }

        driver.Manage().Timeouts().ImplicitWait = IMPLICIT_TIMEOUT;
        _wrappedAndroidDriver.Value = driver;
        WrappedAndroidDriver = driver;
        return driver;
    }

    private AndroidDriver InitializeDriverGridMode(string testName)
    {
        var androidSettings = ConfigurationService.GetSection<AndroidSettings>();
        var gridSettings = androidSettings.GridSettings.First(x => x.ProviderName == androidSettings.ExecutionType);
        
        var gridUrl = ConstructGridUrl(gridSettings.Url);

        var appiumOptions = new AppiumOptions();
        var options = new Dictionary<string, object>
        {
            { MobileCapabilityType.PlatformName, "Android" },
            { MobileCapabilityType.PlatformVersion, AppConfiguration.AndroidVersion },
            { MobileCapabilityType.DeviceName, AppConfiguration.DeviceName }
        };

        if (AppConfiguration.IsMobileWebExecution)
        {
            options[MobileCapabilityType.BrowserName] = AppConfiguration.DefaultBrowser;
        }
        else
        {
            options[MobileCapabilityType.App] = AppConfiguration.AppPath.Replace("\\", "/");
            options[AndroidMobileCapabilityType.AppPackage] = AppConfiguration.AppPackage;
            options[AndroidMobileCapabilityType.AppActivity] = AppConfiguration.AppActivity;
        }

        options["name"] = testName;
        appiumOptions.AddAdditionalAppiumOption(gridSettings.OptionsName, gridSettings);

        AndroidDriver driver = null;
        try
        {
            driver = new AndroidDriver(new Uri(gridUrl), appiumOptions);
            WrappedAndroidDriver = driver;
        }
        catch (Exception e)
        {
            //DebugInformation.PrintStackTrace(e);
        }

        return driver;
    }

    private AndroidDriver InitializeDriverRegularMode()
    {
        var androidSettings = ConfigurationService.GetSection<AndroidSettings>();
        var gridSettings = androidSettings.GridSettings.First(x => x.ProviderName == "regular");
        var gridUrl = ConstructGridUrl(gridSettings.Url);
        var caps = new AppiumOptions();
        caps.AddAdditionalAppiumOption(MobileCapabilityType.PlatformName, "Android");
        caps.AddAdditionalAppiumOption(MobileCapabilityType.PlatformVersion, AppConfiguration.AndroidVersion);
        caps.AddAdditionalAppiumOption(MobileCapabilityType.DeviceName, AppConfiguration.DeviceName);

        if (AppConfiguration.IsMobileWebExecution)
        {
            caps.AddAdditionalAppiumOption(MobileCapabilityType.BrowserName, AppConfiguration.DefaultBrowser);
        }
        else
        {
            caps.AddAdditionalAppiumOption(MobileCapabilityType.App, AppConfiguration.AppPath);
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
        foreach (var optionKey in _customDriverOptions.Value.Keys)
        {
            caps.AddAdditionalAppiumOption(optionKey, _customDriverOptions.Value[optionKey]);
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

    public void Dispose()
    {
        if (_disposed.Value)
        {
            return;
        }

        if (_wrappedAndroidDriver.Value != null)
        {
            _wrappedAndroidDriver.Value.Quit();
            _customDriverOptions.Value.Clear();
        }

        _disposed.Value = true;
    }
}
