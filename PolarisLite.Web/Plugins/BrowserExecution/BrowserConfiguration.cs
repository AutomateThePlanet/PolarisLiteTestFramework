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
        ExecutionType = ExecutionType.Regular;
    }

    public BrowserConfiguration(
        string browser,
        string lifecycle,
        string executionType)
    {
        Browser = (Browser)Enum.Parse(typeof(Browser), browser);
        Lifecycle = (Lifecycle)Enum.Parse(typeof(Lifecycle), lifecycle);
        ExecutionType = (ExecutionType)Enum.Parse(typeof(ExecutionType), executionType);
    }

    public Browser Browser { get; set; }
    public Lifecycle Lifecycle { get; set; }
    public ExecutionType ExecutionType { get; set; }
}
