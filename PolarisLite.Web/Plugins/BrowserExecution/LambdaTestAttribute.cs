using PolarisLite.Web.Plugins.BrowserExecution;
using System.Runtime.InteropServices;

namespace PolarisLite.Web.Plugins;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class LambdaTestAttribute : GridAttribute
{
    public LambdaTestAttribute(Browser browser = Browser.Chrome, string browserVersion = "latest")
        : base(browser)
    {
        BrowserConfiguration = new BrowserConfiguration(browser, Lifecycle.RestartEveryTime, ExecutionType.Grid, browserVersion);
        GridSettings = new GridConfiguration();
        GridSettings.OptionsName = "LT:Options";
        string userName = Environment.GetEnvironmentVariable("LT_USERNAME", EnvironmentVariableTarget.Machine);
        string accessKey = Environment.GetEnvironmentVariable("LT_ACCESSKEY", EnvironmentVariableTarget.Machine);
        GridSettings.Url = $"https://{userName}:{accessKey}@hub.lambdatest.com/wd/hub";
        GridSettings.Arguments = new Dictionary<string, object>
        {
            { "resolution", "1920x1080" },
            { "platform", "Windows 10" },
            { "visual", "true" },
            { "video", "true" },
            { "build", "1.2" },
            { "project", "POLARIS_RUN" },
            { "selenium_version", "4.21.0" }
        };
    }
}