using NUnit.Framework;
using PolarisLite.Web.Plugins.BrowserExecution;

namespace PolarisLite.Web.Plugins;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class GridAttribute : LocalExecutionAttribute
{
    public GridAttribute(Browser browser, string browserVersion = "latest")
        : base(browser, Lifecycle.RestartEveryTime)
    {
        BrowserConfiguration = new BrowserConfiguration(browser, Lifecycle.RestartEveryTime);
        GridSettings = new GridConfiguration();
        GridSettings.OptionsName = "";

        BrowserConfiguration = new BrowserConfiguration(browser, Lifecycle.RestartEveryTime, ExecutionType.Grid, browserVersion);
    }

    public GridConfiguration GridSettings { get; set; }
}
