using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using DemoSystemTests.Results;
using PolarisLite.Web;
using PolarisLite.Web.Core.NUnit;
using PolarisLite.Web.Plugins;

namespace DemoSystemTests.Web.Results;

[TestFixture]
[LambdaTest]
//[LocalExecution(BrowserType.Chrome, Lifecycle.RestartEveryTime)]
[AllureNUnit]
[AllureParentSuite("Advanced Custom Components")]
[AllureEpic("Web Components")]
[AllureFeature("Custom Components")]
[AllureSeverity(SeverityLevel.minor)]
[AllureOwner("Anton Angelov")]
public class CustomControlsTests : ResultsWebTest
{
    [Test]
    [Category(Categories.CUSTOM_CONTROLS)]
    [Category(Categories.CI)]
    [AllureSuite("Kendo DatePicker")]
    public void SetDateKendoDatePickerCustomControl()
    {
        App.Navigation.GoToUrl("http://demos.telerik.com/kendo-ui/datepicker/index");
        var datePicket = App.Elements.FindById<KendoDatePicker>("datepicker");

        datePicket.SetDate(DateTime.Now);
    }

    [Test]
    [Category(Categories.CUSTOM_CONTROLS)]
    [Category(Categories.CI)]
    [AllureSuite("Gauge Needle")]
    public void SetValueGaugeNeedleCustomControl()
    {
        App.Navigation.GoToUrl("http://www.igniteui.com/radial-gauge/gauge-needle");
        var gaugeNeedle = App.Elements.FindById<GaugeNeedle>("radialgauge");
        gaugeNeedle.SetValue(44);
    }

    [Test]
    [Category(Categories.CUSTOM_CONTROLS)]
    [Category(Categories.CI)]
    [AllureSuite("Full Calendar")]
    public void TestMethodsFullCalendarCustomControl()
    {
        App.Navigation.GoToUrl("https://fullcalendar.io/docs/v3/month-view-demo");

        var fullCalendar = App.Elements.FindById<FullCalendar>("calendar");
        fullCalendar.ClickNextButton();
        fullCalendar.ClickPreviousButton();
        fullCalendar.GoToDate(new DateTime(2012, 11, 28));
        fullCalendar.GoToToday();
    }

    [Test]
    [Category(Categories.CUSTOM_CONTROLS)]
    [Category(Categories.CI)]
    [AllureSuite("Kendo Color Picker")]
    public void SetColorKendoColorPickerCustomControl()
    {
        App.Navigation.GoToUrl("http://demos.telerik.com/kendo-ui/colorpicker/index");

        var kendoColorPicker = App.Elements.FindById<KendoColorPicker>("picker");
        kendoColorPicker.SetColor("ccc");
    }
}
