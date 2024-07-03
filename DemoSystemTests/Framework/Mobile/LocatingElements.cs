using PolarisLite.Mobile;
using PolarisLite.Mobile.Components;
using PolarisLite.Mobile.Core.NUnit;
using PolarisLite.Mobile.Plugins;

namespace DemoSystemTests.Mobile.Framework;

[ExecutionApp(AndroidVersion = "13.0",
    DeviceName = "pixel5-test-device-13-new",
    AppPath = "ApiDemos-debug.apk",
    AppPackage = "com.example.android.apis",
    AppActivity = ".view.ControlsMaterialDark",
    Lifecycle = Lifecycle.RestartEveryTime)]
public class LocatingElements : AndroidTest
{
    [Test]
    public void LocatingElementsTest()
    {
        Button button = App.Elements.FindById<Button>("button");
        button.Click();

        CheckBox checkBox = App.Elements.FindByClass<CheckBox>("android.widget.CheckBox");
        checkBox.Check();

        var secondButton = App.Elements.FindByTextContaining<Button>("BUTTO");
        secondButton.Click();

        var thirdButton = App.Elements.FindByXPath<Button>("//*[@resource-id='com.example.android.apis:id/button']");
        thirdButton.Click();
    }

    [Test]
    public void LocatingElementInsideAnotherElementTest()
    {
        var mainElement = App.Elements.FindById<AndroidComponent>("decor_content_parent");

        var button = mainElement.FindByClass<Button>("button");
        button.Click();

        var checkBox = mainElement.FindByClass<CheckBox>("android.widget.CheckBox");
        checkBox.Check();

        var thirdButton = mainElement.FindByXPath<Button>("//*[@resource-id='com.example.android.apis:id/button']");
        thirdButton.Click();
    }
}
