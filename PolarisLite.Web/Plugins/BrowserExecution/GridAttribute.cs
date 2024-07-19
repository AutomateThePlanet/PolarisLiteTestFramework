using NUnit.Framework;
using PolarisLite.Web.Plugins.BrowserExecution;
using PolarisLite.Web.Settings.FilesImplementation;

namespace PolarisLite.Web.Plugins;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class GridAttribute : LocalExecutionAttribute
{
    public GridAttribute(Browser browser, string browserVersion = "latest")
        : base(browser, Lifecycle.RestartEveryTime)
    {
        BrowserConfiguration = new BrowserConfiguration(browser, Lifecycle.RestartEveryTime);
        GridSettings = new GridSettings();
        GridSettings.OptionsName = "";

        BrowserConfiguration = new BrowserConfiguration(browser, Lifecycle.RestartEveryTime, ExecutionType.Grid, browserVersion);
    }

    public GridSettings GridSettings { get; set; }
}