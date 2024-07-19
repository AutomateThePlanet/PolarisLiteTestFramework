using PolarisLite.Web.Plugins;
using PolarisLite.Web.Plugins.BrowserExecution;
using System.Drawing;

namespace PolarisLite.Web.Configuration.StaticImplementation;
public class WebSettings
{
    public static ExecutionType ExecutionType { get; set; } = ExecutionType.LambdaTest;
    public static Lifecycle DefaultLifeCycle { get; set; } = Lifecycle.RestartEveryTime;
    public static Browser DefaultBrowser { get; set; } = Browser.Chrome;
    public static string BrowserVersion { get; set; } = "latest";

    public static Size Size { get; set; } = WindowsSizeResolver.GetWindowSize(DesktopWindowSize._1280_800);
    public static double PixelRation { get; set; } = 1;
    public static string DeviceName { get; set; } = MobileDevices.GalaxyS20Ultra;
    public static string UserAgent { get; set; } = MobileUserAgents.GalaxyS20Ultra;
    public static bool MobileEmulation { get; set; } = false;
    public static TimeoutSettings TimeoutSettings { get; set; } = new TimeoutSettings();
    public static GridSettings GridSettings { get; set; } = new GridSettings();
    //public List<GridConfiguration> GridSettings { get; set; }
}
