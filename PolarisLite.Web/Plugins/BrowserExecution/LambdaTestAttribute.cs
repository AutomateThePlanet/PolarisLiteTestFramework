using PolarisLite.Utilities;

namespace PolarisLite.Web.Plugins;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class LambdaTestAttribute : GridAttribute
{
    public LambdaTestAttribute(BrowserType browser = BrowserType.Chrome, int browserVersion = 0, DesktopWindowSize desktopWindowSize = DesktopWindowSize._1920_1080, bool enableAutoHealing = false, int smartWait = 0, bool useTunnel = false)
        : base(browser)
    {
        var buildName = Environment.GetEnvironmentVariable("BUILD_NAME");
        if (string.IsNullOrEmpty(buildName))
        {
            buildName = TimestampBuilder.BuildUniqueText("PO_");
            Environment.SetEnvironmentVariable("BUILD_NAME", buildName);
        }

        string browserVersionString = browserVersion <= 0 ? "latest" : browserVersion.ToString();
        BrowserConfiguration = new BrowserConfiguration(browser, Lifecycle.RestartEveryTime, ExecutionType.LambdaTest, browserVersionString);
        GridSettings = new GridSettings();
        GridSettings.OptionsName = "LT:Options";
        string userName = Environment.GetEnvironmentVariable("LT_USERNAME");
        string accessKey = Environment.GetEnvironmentVariable("LT_ACCESSKEY");
        GridSettings.Url = $"https://{userName}:{accessKey}@hub.lambdatest.com/wd/hub";

        string resolution = WindowsSizeResolver.GetWindowSize(desktopWindowSize).ConvertToString();
        GridSettings.Arguments = new Dictionary<string, object>
        {
            { "resolution", resolution },
            { "platform", "Windows 10" },
            { "visual", "false" },
            { "video", "true" },
            { "seCdp", "true" },
            { "console", "true" },
            { "tunnel", useTunnel },
            { "w3c", "true" },
            { "plugin", "c#-c#" },
            { "build", buildName },
            { "project", "POLARIS_RUN" },
            { "selenium_version", "4.22.0" }
        };

        //GridSettings.Arguments.Add("autoHeal", enableAutoHealing);
        //GridSettings.Arguments.Add("smartWait", enableAutoHealing);
    }
}