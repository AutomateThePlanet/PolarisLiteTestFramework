namespace PolarisLite.Mobile.Settings.FilesImplementation;

public sealed class AndroidSettings
{
    public List<GridSettings> GridSettings { get; set; }
    public TimeoutSettings TimeoutSettings { get; set; }

    public string ExecutionType { get; set; }
    public string DefaultLifeCycle { get; set; }
    public string DefaultBrowser { get; set; }

    public string DefaultDeviceName { get; set; }
    public string DefaultAndroidVersion { get; set; }
    public string DefaultAppPackage { get; set; }
    public string DefaultAppActivity { get; set; }
    public string DefaultAppPath { get; set; }

    public bool ScreenshotsOnFailEnabled { get; set; }
    public string ScreenshotsSaveLocation { get; set; }

    public bool VideosOnFailEnabled { get; set; }
    public string VideosSaveLocation { get; set; }
}