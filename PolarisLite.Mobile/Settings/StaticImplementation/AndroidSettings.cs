using PolarisLite.Mobile.Plugins;

namespace PolarisLite.Mobile.Settings.StaticImplementation;

public class AndroidSettings
{
    public static ExecutionType ExecutionType { get; set; } = ExecutionType.LambdaTest;
    public static Lifecycle DefaultLifeCycle { get; set; } = Lifecycle.RestartEveryTime;
    public static BrowserType DefaultBrowser { get; set; } = BrowserType.Chrome;

    public static string DefaultDeviceName { get; set; } = "Pixel 6";
    public static string DefaultAndroidVersion { get; set; } = "13.0";
    public static string DefaultAppPackage { get; set; } = "io.appium.android.apis";
    public static string DefaultAppActivity { get; set; } = ".ApiDemos";
    public static string DefaultAppPath { get; set; } = "";

    public static GridSettings GridSettings { get; set; } = new GridSettings();
    public static TimeoutSettings TimeoutSettings { get; set; } = new TimeoutSettings();
}