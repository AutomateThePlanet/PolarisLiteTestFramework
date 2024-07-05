using PolarisLite.Web.Plugins.BrowserExecution;

namespace PolarisLite.Web.Plugins;
public class BrowserConfiguration
{
    public BrowserConfiguration()
    {
    }

    public BrowserConfiguration(
        Browser browser,
        Lifecycle lifecycle)
    {
        Browser = browser;
        Lifecycle = lifecycle;
        ExecutionType = ExecutionType.Local;
    }

    public BrowserConfiguration(
        string browser,
        string lifecycle,
        string executionType)
    {
        Browser = (Browser)Enum.Parse(typeof(Browser), browser.RemoveSpacesAndCapitalize());
        Lifecycle = (Lifecycle)Enum.Parse(typeof(Lifecycle), lifecycle.RemoveSpacesAndCapitalize());
        ExecutionType = (ExecutionType)Enum.Parse(typeof(ExecutionType), executionType.RemoveSpacesAndCapitalize());
    }

    public Browser Browser { get; set; }
    public Lifecycle Lifecycle { get; set; }
    public ExecutionType ExecutionType { get; set; }
}
