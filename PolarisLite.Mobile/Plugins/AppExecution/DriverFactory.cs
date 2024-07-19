using AngleSharp.Html.Parser;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using PolarisLite.Core;
using PolarisLite.Mobile.Settings.FilesImplementation;
using PolarisLite.Secrets;

namespace PolarisLite.Mobile.Plugins.AppExecution.Factory;
public static class DriverFactory
{
    private static readonly TimeSpan IMPLICIT_TIMEOUT = TimeSpan.FromSeconds(30);
    private static readonly ThreadLocal<bool> _disposed = new ThreadLocal<bool>(() => false);
    private static readonly ThreadLocal<AppConfiguration> _appConfiguration = new ThreadLocal<AppConfiguration>();
    private static readonly ThreadLocal<Dictionary<string, string>> _customDriverOptions = new ThreadLocal<Dictionary<string, string>>(() => new Dictionary<string, string>());
    private static readonly ThreadLocal<AndroidDriver> _wrappedAndroidDriver = new ThreadLocal<AndroidDriver>();

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

    public static AndroidDriver StartApp(AppConfiguration configuration)
    {
        AppConfiguration = configuration;
        Disposed = false;

        //var androidSettings = ConfigurationService.Get<AndroidSettings>();
        var executionType = configuration.ExecutionType;

        if (executionType.ToString().ToLower().Equals("regular"))
        {
            WrappedAndroidDriver = InitializeDriverRegularMode();
        }
        else
        {
            var testName = AppConfiguration.TestName;
            WrappedAndroidDriver = InitializeDriverGridMode(testName);
        }

        return WrappedAndroidDriver;
    }

    private static AndroidDriver InitializeDriverGridMode(string testName)
    {
        var androidSettings = ConfigurationService.GetSection<AndroidSettings>();
        var gridSettings = androidSettings.GridSettings.First(x => x.ProviderName == androidSettings.ExecutionType);
        
        var gridUrl = ConstructGridUrl(gridSettings.Url);

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

        AddGridOptionsConfig(options, gridSettings);
        appiumOptions.AddAdditionalAppiumOption(gridSettings.OptionsName, options);

        var driver = new AndroidDriver(new Uri(gridUrl), appiumOptions);

        return driver;
    }

    private static AndroidDriver InitializeDriverRegularMode()
    {
        var androidSettings = ConfigurationService.GetSection<AndroidSettings>();
        var gridSettings = androidSettings.GridSettings.First(x => x.ProviderName == "regular");
        var gridUrl = gridSettings.Url;
        var caps = new AppiumOptions();
        caps.PlatformName = "Android";
        caps.AutomationName = "UiAutomator2";
        caps.PlatformVersion = AppConfiguration.AndroidVersion;
        //caps.DeviceName = gridSettings.Arguments["deviceName"];

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
        foreach (var optionKey in _customDriverOptions.Value.Keys)
        {
            caps.AddAdditionalAppiumOption(optionKey, _customDriverOptions.Value[optionKey]);
        }
    }

    private static void AddGridOptionsConfig(Dictionary<string, object> options, GridSettings gridSettings)
    {
        var arguments = gridSettings?.Arguments;
        foreach (var entry in arguments)
        {
            // handle mask command for LambdaTest
            if (entry.Key.Equals("maskCommands", StringComparison.OrdinalIgnoreCase))
            {
                if (entry.Value is JArray maskCommandsArray)
                {
                    var maskCommands = maskCommandsArray.ToObject<string[]>();
                    options.Add(entry.Key, maskCommands);
                }
            }
            else if (entry.Value is string value && value.StartsWith("{env_"))
            {
                var envValue = SecretsResolver.GetSecret(value);
                options.Add(entry.Key, envValue);
            }
            else
            {
                options.Add(entry.Key, entry.Value);
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

    public static void Dispose()
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
