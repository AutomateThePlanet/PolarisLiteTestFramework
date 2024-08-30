using PolarisLite.Web.Plugins;
using PolarisLite.Web.Settings.StaticImplementation;
using System.Drawing;

namespace PolarisLite.Web.Configuration.StaticImplementation;
public class WebSettings
{
    public WebSettings()
    {
        NetworkThrottlingSettings = new NetworkThrottlingSettings();
    }
    public static ExecutionType ExecutionType { get; set; } = ExecutionType.LambdaTest;
    public static Lifecycle DefaultLifeCycle { get; set; } = Lifecycle.RestartEveryTime;
    public static BrowserType DefaultBrowser { get; set; } = BrowserType.Chrome;
    public static string BrowserVersion { get; set; } = "latest";

    public static Size Size { get; set; } = WindowsSizeResolver.GetWindowSize(DesktopWindowSize._1280_800);
    public static double PixelRation { get; set; } = 1;
    public static string DeviceName { get; set; } = MobileDevices.GalaxyS20Ultra;
    public static string UserAgent { get; set; } = MobileUserAgents.GalaxyS20Ultra;
    public static bool MobileEmulation { get; set; } = false;
    public static TimeoutSettings TimeoutSettings { get; set; } = new TimeoutSettings();
    public static GridSettings GridSettings { get; set; } = new GridSettings();
    //public List<GridSettings> GridSettings { get; set; }

    public static bool EnableBDDLogging { get; set; } = true;
    public static bool EnableHighlight { get; set; } = true;
    public static bool EnableScrollIntoView { get; set; } = true;
    public static bool EnableToastMessages { get; set; } = true;
    public static bool ScreenshotsOnFailure { get; set; } = true;
    public static string ScreenshotsSaveLocation { get; set; } = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "Polaris", "ScreenshotsOnFailure");
    public static bool EnableExceptionAnalysis { get; set; } = true;


    public static bool SetGeoLocation { get; set; }
    public static string GeoLocation { get; set; }
    public static double? Latitude { get; set; }
    public static double? Longitude { get; set; }
    public static int Accuracy { get; set; } = 1;
    public static bool SetTimeZone { get; set; }
    public static string TimeZoneName { get; set; }
    public static string TimeZoneId { get; set; }
    public static bool SetLocale { get; set; }
    public static string Locale { get; set; }
    public static bool EnableNetworkThrottling { get; set; }
    public static bool CaptureNetworkTraffic { get; set; }
    public static NetworkThrottlingSettings NetworkThrottlingSettings { get; set; }
    public static bool CaptureNativePerformanceMetrics { get; set; }

    public static bool CaptureLighthousePerformanceMetrics { get; set; }
}
