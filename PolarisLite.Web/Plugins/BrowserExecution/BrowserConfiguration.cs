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

    public Browser Browser { get; set; }
    public Lifecycle Lifecycle { get; set; }
    public ExecutionType ExecutionType { get; set; }
}
