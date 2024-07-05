namespace PolarisLite.Web.Core;
public class BrowserConfiguration
{
    public BrowserConfiguration()
    {
        Browser = Browser.Chrome;
        BrowserVersion = "latest";
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
    Browser browser,
    Lifecycle lifecycle,
    ExecutionType executionType,
    string browserVersion = "latest")
    {
        Browser = browser;
        Lifecycle = lifecycle;
        ExecutionType = executionType;
        BrowserVersion = browserVersion;
    }

    public BrowserConfiguration(
        string browser,
        string lifecycle,
        string executionType,
        string browserVersion = "latest")
    {
        Browser = (Browser)Enum.Parse(typeof(Browser), browser);
        Lifecycle = (Lifecycle)Enum.Parse(typeof(Lifecycle), lifecycle);
        ExecutionType = (ExecutionType)Enum.Parse(typeof(ExecutionType), executionType);
        BrowserVersion = browserVersion;
    }

    public Browser Browser { get; set; }
    public string BrowserVersion { get; set; }
    public Lifecycle Lifecycle { get; set; }
    public ExecutionType ExecutionType { get; set; }
}
