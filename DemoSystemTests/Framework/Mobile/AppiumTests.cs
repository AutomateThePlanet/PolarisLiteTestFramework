using PolarisLite.Mobile;
using PolarisLite.Mobile.Components;
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

    [Test]
    public void TestPageLayout()
    {
        App.AppService.StartActivity("com.example.android.apis", ".view.Controls1");

        var button = App.Elements.FindByIdContaining<Button>("button");
        var secondButton = App.Elements.FindByIdContaining<Button>("button_disabled");
        var checkBox = App.Elements.FindByIdContaining<CheckBox>("check1");
        var secondCheckBox = App.Elements.FindByIdContaining<CheckBox>("check2");
        var mainElement = App.Elements.FindById<AndroidComponent>("android:id/content");

        button.AssertAboveOf(checkBox);

        button.AssertAboveOf(checkBox, 105);

        button.AssertAboveOfGreaterThan(checkBox, 100);
        button.AssertAboveOfGreaterThanOrEqual(checkBox, 105);
        button.AssertAboveOfLessThan(checkBox, 110);
        button.AssertAboveOfLessThanOrEqual(checkBox, 105);

        button.AssertAboveOfApproximate(checkBox, 104, percent: 10);

        button.AssertAboveOfBetween(checkBox, 100, 120);

        LayoutAssert.AssertAlignedHorizontallyAll(button, secondButton);

        LayoutAssert.AssertAlignedHorizontallyTop(button, secondButton);
        LayoutAssert.AssertAlignedHorizontallyCentered(button, secondButton, secondButton);
        LayoutAssert.AssertAlignedHorizontallyBottom(button, secondButton, secondButton);

        LayoutAssert.AssertAlignedVerticallyAll(secondCheckBox, checkBox);

        LayoutAssert.AssertAlignedVerticallyLeft(secondCheckBox, checkBox);
        LayoutAssert.AssertAlignedVerticallyCentered(secondCheckBox, checkBox);
        LayoutAssert.AssertAlignedVerticallyRight(secondCheckBox, checkBox);

        button.AssertHeightLessThan(100);
        button.AssertWidthBetween(50, 80);
    }
}