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

        _appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
        _appiumLocalService.Start();
        var appiumOptions = new AppiumOptions();
        appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.DeviceName, "pixel5-test-device-13-new");
        appiumOptions.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppPackage, "io.appium.android.apis");
        appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.PlatformName, "Android");
        appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.PlatformVersion, "13.0");
        appiumOptions.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppActivity, ".ApiDemos");
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
    public void PerformActionsButtons()
    {
        By byScrollLocator = new ByAndroidUIAutomator("new UiSelector().text('Views');");
        var viewsButton = _driver.FindElement(byScrollLocator);
        viewsButton.Click();

        var controlsViewButton = _driver.FindElement(By.XPath("//*[@text='Controls']"));
        controlsViewButton.Click();

        var lightThemeButton = _driver.FindElement(By.XPath("//*[@text='1. Light Theme']"));
        lightThemeButton.Click();
        var saveButton = _driver.FindElement(By.XPath("//*[@text='Save']"));

        Assert.IsTrue(saveButton.Enabled);
    }
}