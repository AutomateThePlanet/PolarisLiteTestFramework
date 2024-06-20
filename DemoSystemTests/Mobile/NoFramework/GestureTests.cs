namespace DemoSystemTests.Mobile.NoFramework;

[TestFixture]
public class GestureTests
{
    private static AndroidDriver _driver;
    private static AppiumLocalService _appiumLocalService;
    [SetUp]
    public void SetUp()
    {
        _appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
        _appiumLocalService.Start();

        string testAppPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "ApiDemos-debug.apk");

        var appiumOptions = new AppiumOptions();
        appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.DeviceName, "pixel5-test-device-13-new");
        appiumOptions.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppPackage, "io.appium.android.apis");
        appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.PlatformName, "Android");
        appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.PlatformVersion, "13.0");
        appiumOptions.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppActivity, ".graphics.TouchRotateActivity");
        appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.App, testAppPath);

        _driver = new AndroidDriver(_appiumLocalService, appiumOptions);
    }

    [TearDown]
    public void TearDown()
    {
        _driver?.Close();
        _appiumLocalService.Dispose();
    }

    [Test]
    public void SwipeTest()
    {
        _driver.StartActivity("io.appium.android.apis", ".graphics.FingerPaint");
        var element = _driver.FindElement(By.Id("android:id/content"));
        Point point = element.Coordinates.LocationInDom;
        Size size = element.Size;

        var touchAction = new TouchAction(_driver);
        touchAction.Press(point.X + 5, point.Y + 5)
                   .Wait(200)
                   .MoveTo(point.X + size.Width - 5, point.Y + size.Height - 5)
                   .Release()
                   .Perform();
    }
}
