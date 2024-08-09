using PolarisLite.Web;
using PolarisLite.Web.Core.NUnit;
using PolarisLite.Web.Plugins;

namespace DemoSystemTests.Web;

[TestFixture]
[LocalExecution(BrowserType.Chrome, Lifecycle.RestartEveryTime)]
public class CustomControlsTests : WebTest
{
    [Test]
    public void SetDateKendoDatePickerCustomControl()
    {
        App.Navigation.GoToUrl("http://demos.telerik.com/kendo-ui/datepicker/index");
        var datePicket = App.Elements.FindById<KendoDatePicker>("datepicker");

        datePicket.SetDate(DateTime.Now);
    }

    [Test]
    public void SetValueGaugeNeedleCustomControl()
    {
        App.Navigation.GoToUrl("http://www.igniteui.com/radial-gauge/gauge-needle");
        var gaugeNeedle = App.Elements.FindById<GaugeNeedle>("radialgauge");
        gaugeNeedle.SetValue(44);
    }

    [Test]
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
    public void SetColorKendoColorPickerCustomControl()
    {
        App.Navigation.GoToUrl("http://demos.telerik.com/kendo-ui/colorpicker/index");

        var kendoColorPicker = App.Elements.FindById<KendoColorPicker>("picker");
        kendoColorPicker.SetColor("ccc");
    }
}
