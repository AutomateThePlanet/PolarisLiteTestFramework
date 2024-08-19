namespace PolarisLite.Web.Plugins;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class LambdaTestAttribute : GridAttribute
{
    public LambdaTestAttribute(BrowserType browser = BrowserType.Chrome, int browserVersion = 0, DesktopWindowSize desktopWindowSize = DesktopWindowSize._1920_1080, bool enableAutoHealing = false, int smartWait = 0)
        : base(browser)
    {
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
            //{ "tunnel", "true" },
            { "w3c", "true" },
            { "plugin", "c#-c#" },
            { "build", "2.2" },
            { "project", "POLARIS_RUN" },
            { "selenium_version", "4.22.0" }
        };

        //GridSettings.Arguments.Add("autoHeal", enableAutoHealing);
        //GridSettings.Arguments.Add("smartWait", enableAutoHealing);
    }
}