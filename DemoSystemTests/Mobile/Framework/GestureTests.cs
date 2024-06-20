using PolarisLite.Mobile;
using PolarisLite.Mobile.Components;
using PolarisLite.Mobile.Core.MSTest;
using PolarisLite.Mobile.Plugins;

namespace DemoSystemTests.Mobile.Framework;

[ExecutionApp(AndroidVersion = "13.0",
    DeviceName = "pixel5-test-device-13-new",
    AppPath = "ApiDemos-debug.apk",
    AppPackage = "io.appium.android.apis",
    AppActivity = ".graphics.TouchRotateActivity",
    Lifecycle = Lifecycle.RestartEveryTime)]
public class GestureTests : AndroidTest
{
    [Test]
    public void SwipeTest()
    {
        App.AppService.StartActivity("io.appium.android.apis", ".graphics.FingerPaint");
        var element = App.Elements.FindById<AndroidComponent>("android:id/content");
        Point point = element.WrappedElement.Coordinates.LocationInDom;
        Size size = element.WrappedElement.Size;

        App.TouchActions.Press(point.X + 5, point.Y + 5)
                   .Wait(200)
                   .MoveTo(point.X + size.Width - 5, point.Y + size.Height - 5)
                   .Release()
                   .Perform();

        App.TouchActions.Swipe(point.X + 5, point.Y + 5, point.X + size.Width - 5, point.Y + size.Height - 5, 200);
    }
}
