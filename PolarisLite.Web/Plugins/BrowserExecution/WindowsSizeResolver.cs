using System.Drawing;

namespace PolarisLite.Web.Plugins;

public static class WindowsSizeResolver
{
    public static Size GetWindowSize(string resolution)
    {
        var parts = resolution.Split('x');
        Size result = new Size(int.Parse(parts[0]), int.Parse(parts[1]));

        return result;
    }

    public static Size GetWindowSize(DesktopWindowSize windowSize)
    {
        Size result = default;

        switch (windowSize)
        {
            case DesktopWindowSize._1366_768:
                result = new Size(1366, 768);
                break;
            case DesktopWindowSize._1920_1080:
                result = new Size(1920, 1080);
                break;
            case DesktopWindowSize._1440_900:
                result = new Size(1440, 900);
                break;
            case DesktopWindowSize._1600_900:
                result = new Size(1600, 900);
                break;
            case DesktopWindowSize._1280_800:
                result = new Size(1280, 800);
                break;
            case DesktopWindowSize._1280_1024:
                result = new Size(1280, 1024);
                break;
        }

        return result;
    }

    public static Size GetWindowSize(MobileWindowSize windowSize)
    {
        Size result = default;
        switch (windowSize)
        {
            case MobileWindowSize._360_640:
                result = new Size(360, 640);
                break;
            case MobileWindowSize._375_667:
                result = new Size(375, 667);
                break;
            case MobileWindowSize._720_1280:
                result = new Size(720, 1280);
                break;
            case MobileWindowSize._320_568:
                result = new Size(320, 568);
                break;
            case MobileWindowSize._414_736:
                result = new Size(414, 736);
                break;
            case MobileWindowSize._320_534:
                result = new Size(320, 534);
                break;
            case MobileWindowSize._412_915:
                result = new Size(412, 915);
                break;
        }

        return result;
    }

    public static Size GetWindowSize(TabletWindowSize windowSize)
    {
        Size result = default;
        switch (windowSize)
        {
            case TabletWindowSize._768_1024:
                result = new Size(768, 1024);
                break;
            case TabletWindowSize._1280_800:
                result = new Size(1280, 800);
                break;
            case TabletWindowSize._600_1024:
                result = new Size(600, 1024);
                break;
            case TabletWindowSize._601_962:
                result = new Size(601, 962);
                break;
            case TabletWindowSize._800_1280:
                result = new Size(800, 1280);
                break;
            case TabletWindowSize._1024_600:
                result = new Size(1024, 600);
                break;
        }

        return result;
    }

    public static string ConvertToString(this Size size) => $"{size.Width}x{size.Height}";

    public static string ConvertToStringWithColorDepth(this Size size, int colorDepth = 24) => $"{size.Width}x{size.Height}x{colorDepth}";
}