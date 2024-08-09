namespace DemoSystemTests.Mobile.NoFramework;

[TestFixture]
public class AppiumTests
{
    private static AndroidDriver _driver;
    private static AppiumLocalService _appiumLocalService;

    [SetUp]
    public void SetUp()
    {
        string testAppPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "ApiDemos-debug.apk");

        //_appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
        //_appiumLocalService.Start();
        var appiumOptions = new AppiumOptions();
        appiumOptions.DeviceName = "pixel5-test-device-13-3";
        appiumOptions.PlatformName = "Android";
        appiumOptions.PlatformVersion = "13.0";
        appiumOptions.AutomationName = "UiAutomator2";
        appiumOptions.App = testAppPath;
        appiumOptions.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppPackage, "io.appium.android.apis");
        appiumOptions.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppActivity, ".ApiDemos");

        _driver = new AndroidDriver(new Uri("http://127.0.0.1:4722/wd/hub/"), appiumOptions);
    }

    [TearDown]
    public void TearDown()
    {
        _driver?.TerminateApp(_driver.CurrentPackage);
        _driver.Dispose();
        //_appiumLocalService.Dispose();
    }

    [Test]
    public void PerformActionsButtons()
    {
        By byScrollLocator = new ByAndroidUIAutomator("new UiSelector().text(\"Views\")");
        var viewsButton = _driver.FindElement(byScrollLocator);
        viewsButton.Click();

        var controlsViewButton = _driver.FindElement(By.XPath("//*[@text=\"Controls\"]"));
        controlsViewButton.Click();

        var lightThemeButton = _driver.FindElement(By.XPath("//*[@text=\"1. Light Theme\"]"));
        lightThemeButton.Click();
        var saveButton = _driver.FindElement(By.XPath("//*[@text=\"Save\"]"));

        Assert.That(saveButton.Enabled, Is.True);
    }
}