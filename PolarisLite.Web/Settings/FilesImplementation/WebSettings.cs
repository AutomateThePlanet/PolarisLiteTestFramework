namespace PolarisLite.Web.Settings.FilesImplementation;

public class WebSettings
{
    public GridSettings GridSettings { get; set; }
    public TimeoutSettings TimeoutSettings { get; set; }

    public string ExecutionType { get; set; }
    public string DefaultLifeCycle { get; set; }
    public string DefaultBrowser { get; set; }
    public string BrowserVersion { get; set; } = "latest";
}