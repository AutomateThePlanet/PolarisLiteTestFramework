using PolarisLite.Core;
using PolarisLite.Core.Settings.StaticSettings;
using PolarisLite.Secrets;
using PolarisLite.Utilities;
using PolarisLite.Web.Configuration.StaticImplementation;

namespace PolarisLite.Web.Plugins;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class LambdaTestAttribute : GridAttribute
{
    public LambdaTestAttribute(BrowserType browser = BrowserType.Chrome, int browserVersion = 0, DesktopWindowSize desktopWindowSize = DesktopWindowSize._1920_1080, bool enableAutoHealing = false, int smartWait = 0, bool useTunnel = false)
        : base(browser)
    {
        if (string.IsNullOrEmpty(BuildName))
        {
            BuildName = Environment.GetEnvironmentVariable("BUILD_NAME", EnvironmentVariableTarget.Process);
            if (string.IsNullOrEmpty(BuildName))
            {
                BuildName = TimestampBuilder.BuildUniqueText("PO_");
                Environment.SetEnvironmentVariable("BUILD_NAME", BuildName, EnvironmentVariableTarget.Process);
            }
        }

        string browserVersionString = browserVersion <= 0 ? "latest" : browserVersion.ToString();
        BrowserConfiguration = new BrowserConfiguration(browser, Lifecycle.RestartEveryTime, ExecutionType.LambdaTest, browserVersionString);
        GridSettings = new GridSettings();
        GridSettings.OptionsName = "LT:Options";
        GridSettings.BuildName = BuildName;

        string userName = Environment.GetEnvironmentVariable("LT_USERNAME");
        string accessKey = Environment.GetEnvironmentVariable("LT_ACCESSKEY");

        //string userName = SecretsResolver.GetSecret("LT_USERNAME");
        //string accessKey = SecretsResolver.GetSecret("LT_ACCESSKEY");

        //var webSettings = ConfigurationService.GetSection<Settings.FilesImplementation.WebSettings>();
        //string userName = SecretsResolver.GetSecret(() => webSettings.GridSettings.Arguments["username"].ToString());
        //string accessKey = SecretsResolver.GetSecret(() => webSettings.GridSettings.Arguments["accessKey"].ToString());

        // webSettings.GridSettings.Arguments["accessKey"].ToString().GetSecretValue();
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
            { "build", BuildName },
            { "project", "POLARIS_RUN" },
            { "selenium_version", "4.22.0" },
            { "idleTimeout", "300" }
        };

        if (GlobalSettings.LoggingSettings.ShouldMaskSensitiveInfo)
        {
            var maskCommands = new string[] { "setValues", "setCookies", "getCookies" };
            GridSettings.Arguments.Add("lambdaMaskCommands", maskCommands);
        }

        //GridSettings.Arguments.Add("autoHeal", enableAutoHealing);
        //GridSettings.Arguments.Add("smartWait", enableAutoHealing);
    }

    public static string BuildName { get; set; }
}