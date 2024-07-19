using PolarisLite.Web.Plugins.BrowserExecution;
using PolarisLite.Web.Plugins;

namespace PolarisLite.Web.Configuration.StaticImplementation;
public class ConfigurationLoader
{
    public static void LoadConfiguration()
    {
        var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
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
        WebSettings.ExecutionType = ExecutionType.Local;
        WebSettings.DefaultLifeCycle = Lifecycle.RestartEveryTime;
        WebSettings.DefaultBrowser = Browser.Chrome;
        WebSettings.BrowserVersion = "latest";
        WebSettings.Size = WindowsSizeResolver.GetWindowSize(DesktopWindowSize._1280_800);
        WebSettings.PixelRation = 1;
        WebSettings.DeviceName = MobileDevices.GalaxyS20Ultra;
        WebSettings.UserAgent = MobileUserAgents.GalaxyS20Ultra;
        WebSettings.MobileEmulation = false;
        WebSettings.TimeoutSettings = new TimeoutSettings
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
        WebSettings.GridSettings = null;
    }

    private static void LoadStagingConfiguration()
    {
        WebSettings.ExecutionType = ExecutionType.LambdaTest;
        WebSettings.DefaultLifeCycle = Lifecycle.RestartEveryTime;
        WebSettings.DefaultBrowser = Browser.Firefox;
        WebSettings.BrowserVersion = "latest";
        WebSettings.Size = WindowsSizeResolver.GetWindowSize(DesktopWindowSize._1920_1080);
        WebSettings.PixelRation = 1.25;
        WebSettings.DeviceName = MobileDevices.IPhone8;
        WebSettings.UserAgent = MobileUserAgents.IPhone8;
        WebSettings.MobileEmulation = false;
        WebSettings.TimeoutSettings = new TimeoutSettings
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
        WebSettings.GridSettings = new GridSettings();
        var resolution = WindowsSizeResolver.ConvertToString(WindowsSizeResolver.GetWindowSize(DesktopWindowSize._1920_1080));
        WebSettings.GridSettings.Arguments = new Dictionary<string, object>
        {
            { "resolution", resolution },
            { "platform", "Windows 11" },
            { "visual", "true" },
            { "video", "true" },
            { "build", "1.2" },
            { "project", "POLARIS_RUN" },
            { "selenium_version", "4.21.0" }
        };
    }

    private static void LoadDevelopmentConfiguration()
    {
        WebSettings.ExecutionType = ExecutionType.Local;
        WebSettings.DefaultLifeCycle = Lifecycle.RestartEveryTime;
        WebSettings.DefaultBrowser = Browser.Edge;
        WebSettings.BrowserVersion = "latest";
        WebSettings.Size = WindowsSizeResolver.GetWindowSize(DesktopWindowSize._1440_900);
        WebSettings.PixelRation = 1.5;
        WebSettings.DeviceName = MobileDevices.Pixel2;
        WebSettings.UserAgent = MobileUserAgents.Pixel2;
        WebSettings.MobileEmulation = true;
        WebSettings.TimeoutSettings = new TimeoutSettings
        {
            PageLoadTimeout = 45,
            ScriptTimeout = 45,
            ValidationsTimeout = 45,
            WaitForAjaxTimeout = 45,
            SleepInterval = 12,
            ElementToBeVisibleTimeout = 45,
            ElementToExistTimeout = 45,
            ElementToNotExistTimeout = 12,
            ElementToBeClickableTimeout = 45,
            ElementNotToBeVisibleTimeout = 12,
            ElementToHaveContentTimeout = 45
        };
        WebSettings.GridSettings = new GridSettings();
        var resolution = WindowsSizeResolver.ConvertToString(WindowsSizeResolver.GetWindowSize(DesktopWindowSize._1440_900));
        WebSettings.GridSettings.Arguments = new Dictionary<string, object>
        {
            { "resolution", resolution },
            { "platform", "Windows XP" },
            { "visual", "true" },
            { "video", "true" },
            { "build", "1.2" },
            { "project", "POLARIS_RUN" },
            { "selenium_version", "4.21.0" }
        };
    }
}
