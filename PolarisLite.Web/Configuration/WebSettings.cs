namespace PolarisLite.Web;

public sealed class WebSettings
{
    public List<GridSettings> GridSettings { get; set; }
    public TimeoutSettings TimeoutSettings { get; set; }

    public string ExecutionType { get; set; }
    public string DefaultLifeCycle { get; set; }
    public string DefaultBrowser { get; set; }
    public string BrowserVersion { get; set; } = "latest";
    public bool ShouldHighlightElements { get; set; }
    public bool ShouldCaptureHttpTraffic { get; set; }
    public bool ToastNotificationBddLogging { get; set; }
    public long NotificationToastTimeout { get; set; }

    public bool ScreenshotsOnFailEnabled { get; set; }
    public string ScreenshotsSaveLocation { get; set; }

    public bool VideosOnFailEnabled { get; set; }
    public string VideosSaveLocation { get; set; }
}