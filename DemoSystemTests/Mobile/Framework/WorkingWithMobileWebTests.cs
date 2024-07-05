using PolarisLite.Mobile;
using PolarisLite.Mobile.Core.NUnit;
using PolarisLite.Mobile.Plugins;
using PolarisLite.Web;

namespace DemoSystemTests.Mobile.Framework;

[LocalExecution(AndroidVersion = "13.0",
    DeviceName = "pixel5-test-device-13-new",
    IsMobileWebTest = true,
    BrowserName = "Chrome",
    Lifecycle = Lifecycle.RestartEveryTime)]
public class WorkingWithMobileWebTests : AndroidTest
{
    [Test]
    public void GoToWebSite()
    {
        App.Web.Navigation.GoToUrl("https://ecommerce-playground.lambdatest.io/");
        
        var searchInput = App.Web.Elements.FindByXPath<TextField>("//input[@name='search']");
        searchInput.TypeText("iphone");
    }
}
