using PolarisLite.Web.Plugins;

namespace PolarisLite.Mobile.Settings.StaticImplementation;
public class MobileConfigurationLoader
{
    public static void LoadConfiguration()
    {
        var environment = System.Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
        switch (environment)
        {
            case "Development":
                LoadDevelopmentConfiguration();
                break;
            case "Staging":
                LoadStagingConfiguration();
                break;
            case "QA":
                LoadQAConfiguration();
                break;
            default:
                throw new InvalidOperationException($"Unknown environment: {environment}");
        }
    }

    private static void LoadQAConfiguration()
    {
        AndroidSettings.ExecutionType = Mobile.Plugins.ExecutionType.Local;
        AndroidSettings.DefaultLifeCycle = Lifecycle.RestartEveryTime;
        AndroidSettings.DefaultBrowser = BrowserType.Chrome;
        AndroidSettings.DefaultDeviceName = "Pixel 6";
        AndroidSettings.DefaultAndroidVersion = "13.0";
        AndroidSettings.DefaultAppPackage = "io.appium.android.apis";
        AndroidSettings.DefaultAppActivity = ".ApiDemos";
        AndroidSettings.DefaultAppPath = "path/to/your/app.apk";
        AndroidSettings.TimeoutSettings = new TimeoutSettings
        {
            PageLoadTimeout = 30,
            ScriptTimeout = 30,
            ValidationsTimeout = 30,
            WaitForAjaxTimeout = 30,
            SleepInterval = 10,
            ElementToBeVisibleTimeout = 30,
            ElementToExistTimeout = 30,
            ElementToNotExistTimeout = 10,
            ElementToBeClickableTimeout = 30,
            ElementNotToBeVisibleTimeout = 10,
            ElementToHaveContentTimeout = 30
        };
        AndroidSettings.GridSettings.Arguments = new Dictionary<string, object>
        {
            { "isRealMobile", "true" },
            { "build", "1.3" },
            { "video", "true" },
            { "visual", "true" },
            { "w3c", "true" },
            { "autoGrantPermissions", "true" },
            { "project", "POLARIS_ANDROID_RUN" },
            { "appiumVersion", "1.22.0" }
        };
    }

    private static void LoadStagingConfiguration()
    {
        AndroidSettings.ExecutionType = Mobile.Plugins.ExecutionType.LambdaTest;
        AndroidSettings.DefaultLifeCycle = Lifecycle.RestartEveryTime;
        AndroidSettings.DefaultBrowser = BrowserType.Firefox;
        AndroidSettings.DefaultDeviceName = "Pixel 5";
        AndroidSettings.DefaultAndroidVersion = "12.0";
        AndroidSettings.DefaultAppPackage = "io.appium.android.apis";
        AndroidSettings.DefaultAppActivity = ".ApiDemos";
        AndroidSettings.DefaultAppPath = "path/to/your/app.apk";
        AndroidSettings.TimeoutSettings = new TimeoutSettings
        {
            PageLoadTimeout = 60,
            ScriptTimeout = 60,
            ValidationsTimeout = 60,
            WaitForAjaxTimeout = 60,
            SleepInterval = 15,
            ElementToBeVisibleTimeout = 60,
            ElementToExistTimeout = 60,
            ElementToNotExistTimeout = 15,
            ElementToBeClickableTimeout = 60,
            ElementNotToBeVisibleTimeout = 15,
            ElementToHaveContentTimeout = 60
        };
        AndroidSettings.GridSettings.Arguments = new Dictionary<string, object>
        {
            { "isRealMobile", "true" },
            { "build", "1.3" },
            { "video", "true" },
            { "visual", "true" },
            { "w3c", "true" },
            { "autoGrantPermissions", "true" },
            { "project", "POLARIS_ANDROID_RUN" },
            { "appiumVersion", "1.22.0" }
        };
    }

    private static void LoadDevelopmentConfiguration()
    {
        AndroidSettings.ExecutionType = Mobile.Plugins.ExecutionType.Local;
        AndroidSettings.DefaultLifeCycle = Lifecycle.RestartEveryTime;
        AndroidSettings.DefaultBrowser = BrowserType.Edge;
        AndroidSettings.DefaultDeviceName = "Pixel 4";
        AndroidSettings.DefaultAndroidVersion = "11.0";
        AndroidSettings.DefaultAppPackage = "io.appium.android.apis";
        AndroidSettings.DefaultAppActivity = ".ApiDemos";
        AndroidSettings.DefaultAppPath = "path/to/your/app.apk";
        AndroidSettings.TimeoutSettings = new TimeoutSettings
        {
            PageLoadTimeout = 45,
            ScriptTimeout = 45,
            ValidationsTimeout = 45,
            SleepInterval = 12,
            ElementToBeVisibleTimeout = 45,
            ElementToExistTimeout = 45,
            ElementToNotExistTimeout = 12,
            ElementToBeClickableTimeout = 45,
            ElementNotToBeVisibleTimeout = 12,
            ElementToHaveContentTimeout = 45
        };
        AndroidSettings.GridSettings.Arguments = new Dictionary<string, object>
        {
            { "isRealMobile", "true" },
            { "build", "1.3" },
            { "video", "true" },
            { "visual", "true" },
            { "w3c", "true" },
            { "autoGrantPermissions", "true" },
            { "project", "POLARIS_ANDROID_RUN" },
            { "appiumVersion", "1.22.0" }
        };
    }
}

