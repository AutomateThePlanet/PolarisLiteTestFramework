using PolarisLite.Mobile;
using PolarisLite.Mobile.Core.NUnit;
using PolarisLite.Mobile.Plugins;

namespace DemoSystemTests.Mobile.Framework;

//[ExecutionApp(AndroidVersion = "13.0",
//    DeviceName = "pixel5-test-device-13-3",
//    AppPath = "ApiDemos-debug.apk",
//    AppPackage = "io.appium.android.apis",
//    AppActivity = ".ApiDemos",
//    Lifecycle = Lifecycle.RestartEveryTime)]
[LambdaTest(AndroidVersion = "13.0",
    DeviceName = "Pixel 6",
    AppPath = "lt://APP10160522261719924644677200",
    AppPackage = "io.appium.android.apis",
    AppActivity = ".ApiDemos",
    Lifecycle = Lifecycle.RestartEveryTime)]
public class AppiumTests : AndroidTest
{
    [Test]
    public void PerformActionsButtons()
    {
        var viewsButton = App.Elements.FindByAndroidUIAutomator<Button>("new UiSelector().text(\"Views\");");
        viewsButton.Click();

        var controlsViewButton = App.Elements.FindByXPath<Button>("//*[@text=\"Controls\"]");
        controlsViewButton.Click();

        var lightThemeButton = App.Elements.FindByXPath<Button>("//*[@text=\"1. Light Theme\"]");
        lightThemeButton.Click();
        var saveButton = App.Elements.FindByXPath<Button>("//*[@text=\"Save\"]");

        //saveButton.ValidateIsNotDisabled();
    }
}