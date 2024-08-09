namespace DemoSystemTests.Mobile.NoFramework;

[TestFixture]
public class WorkingWithMobileWebTests
{
    private static AndroidDriver _driver;
    private static AppiumLocalService _appiumLocalService;

    [SetUp]
    public void SetUp()
    {
        var args = new OptionCollector()
        .AddArguments(new KeyValuePair<string, string>("--webdriver.chrome.driver", "yourDownloadLocation\\chromedriver.exe"));

        _appiumLocalService = new AppiumServiceBuilder()
            .WithArguments(args)
            .UsingAnyFreePort()
            .Build();
        _appiumLocalService.Start();

        var appiumOptions = new AppiumOptions();
        appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.DeviceName, "pixel5-test-device-13-new");
        appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.PlatformName, "Android");
        appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.PlatformVersion, "13.0");
        appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.BrowserName, "Chrome");

        _driver = new AndroidDriver(_appiumLocalService, appiumOptions);
    }

    [TearDown]
    public void TearDown()
    {
        _driver?.Close();
        _appiumLocalService?.Dispose();
    }

    [Test]
    public void GoToWebSite()
    {
        _driver.Navigate().GoToUrl("http://demos.bellatrix.solutions/");
        Console.WriteLine(_driver.PageSource);
        Assert.That(_driver.PageSource.Contains("Shop"), Is.True);
    }
}
