namespace DemoSystemTests.Mobile.NoFramework;

[TestFixture]
public class HybridAppTests
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

        string testAppPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "ApiDemos-debug.apk");

        var appiumOptions = new AppiumOptions();
        appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.DeviceName, "pixel5-test-device-13-new");
        appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.PlatformName, "Android");
        appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.PlatformVersion, "13.0");
        appiumOptions.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppPackage, "io.appium.android.apis");
        appiumOptions.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppActivity, ".view.WebView1");
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
    public void TestWebViewHybridApp()
    {
        var contexts = _driver.Contexts;
        foreach (var context in contexts)
        {
            if (context.Contains("WEBVIEW"))
            {
                _driver.Context = context;
                break;
            }
        }

        var textInput = _driver.FindElement(By.Id("i_am_a_textbox"));
        textInput.Clear();
        textInput.SendKeys("Automating The Planet");

        Assert.That(textInput.Text, Is.EqualTo("Automating The Planet"));

        var link = _driver.FindElement(By.Id("i am a link"));
        link.Click();

        var body = _driver.FindElement(By.TagName("body"));
        Assert.IsTrue(body.Text.Contains("I am some other page content"));
    }

    private void SwitchToWebView(Func<bool> filterConditionToSwitchWebView)
    {
        var switchedToRightWebView = false;
        var webDriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
        webDriverWait.Until(d =>
        {
            var contexts = ((IContextAware)_driver).Contexts;
            foreach (var c in contexts)
            {
                ((IContextAware)_driver).Context = c;
                if (filterConditionToSwitchWebView())
                {
                    switchedToRightWebView = true;
                }
            }

            if (switchedToRightWebView)
            {
                return true;
            }

            _driver.SwitchTo().DefaultContent();
            return false;
        });
    }

    private List<string> GetWebViews()
    {
        var contexts = ((IContextAware)_driver).Contexts;
        return contexts.ToList();
    }

    private void SwitchToDefault()
    {
        var contexts = ((IContextAware)_driver).Contexts;
        var firstContext = contexts.First();
        ((IContextAware)_driver).Context = firstContext;
    }

    private void SwitchToWebView()
    {
        var contexts = ((IContextAware)_driver).Contexts;
        var lastContext = contexts.Last();
        ((IContextAware)_driver).Context = lastContext;
    }

    private void SwitchToFirstWebView()
    {
        var contexts = ((IContextAware)_driver).Contexts;
        var firstContext = contexts.First();
        ((IContextAware)_driver).Context = firstContext;
    }

    private void SwitchToWebViewUrlContains(string url)
    {
        SwitchToWebView(() => _driver.Url != null && _driver.Url.Contains(url));
    }

    private void SwitchToWebViewTitleContains(string title)
    {
        SwitchToWebView(() => _driver.Title != null && _driver.Title.Contains(title));
    }
}
