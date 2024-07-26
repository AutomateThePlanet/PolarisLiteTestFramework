namespace PolarisLite.Web.Plugins;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class GridAttribute : LocalExecutionAttribute
{
    public GridAttribute(BrowserType browser, string browserVersion = "latest")
        : base(browser, Lifecycle.RestartEveryTime)
    {
        BrowserConfiguration = new BrowserConfiguration(browser, Lifecycle.RestartEveryTime);
        GridSettings = new GridSettings();
        GridSettings.OptionsName = "";

        BrowserConfiguration = new BrowserConfiguration(browser, Lifecycle.RestartEveryTime, ExecutionType.Grid, browserVersion);
    }

    public GridSettings GridSettings { get; set; }
}
