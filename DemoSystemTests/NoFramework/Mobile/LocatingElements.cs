namespace DemoSystemTests.Mobile.NoFramework;

[TestFixture]
public class LocatingElements
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
        appiumOptions.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppPackage, "com.example.android.apis");
        appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.PlatformName, "Android");
        appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.PlatformVersion, "13.0");
        appiumOptions.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppActivity, ".view.ControlsMaterialDark");
        appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.App, testAppPath);

        _driver = new AndroidDriver(_appiumLocalService, appiumOptions);
    }

    [TearDown]
    public void TearDown()
    {
        _driver?.Close();
        _appiumLocalService?.Dispose();
    }

    [Test]
    public void LocatingElementsTest()
    {
        AppiumElement button = _driver.FindElement(By.Id("button"));
        button.Click();

        AppiumElement checkBox = _driver.FindElement(By.ClassName("android.widget.CheckBox"));
        checkBox.Click();

        AppiumElement secondButton = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().textContains(\"BUTTO\")"));
        secondButton.Click();

        AppiumElement thirdButton = _driver.FindElement(By.XPath("//*[@resource-id='com.example.android.apis:id/button']"));
        thirdButton.Click();
    }

    [Test]
    public void LocatingElementInsideAnotherElementTest()
    {
        var mainElement = _driver.FindElement(By.Id("decor_content_parent"));

        var button = mainElement.FindElement(By.ClassName("button"));
        button.Click();

        var checkBox = mainElement.FindElement(By.ClassName("android.widget.CheckBox"));
        checkBox.Click();

        var thirdButton = mainElement.FindElement(By.XPath("//*[@resource-id='com.example.android.apis:id/button']"));
        thirdButton.Click();
    }
}
