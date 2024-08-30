using PolarisLite.Web.Core.NUnit;
using PolarisLite.Web;
using PolarisLite.Web.Assertions;
using PolarisLite.Web.Plugins;
using PolarisLite.Web.Plugins.Performance;
using static PolarisLite.Web.Plugins.Performance.PerformanceService;
using static PolarisLite.Web.Plugins.Performance.PerformanceMetrics;
using Polaris.Plugins.Common.ExecutionTime;

namespace DemoSystemTests.Web.Performance;

[TestFixture]
[LambdaTest]
[Geolocation(GeoLocations.Finland)]
//[Geolocation(63.246778, 25.920916, 1)]
[TimeZone(TimeZones.Bratislava)]
[Locale(LanguageCodes.Finnish)]
[NetworkEmulation(NetworkTypes.Regular4G)]
[CaptureNetworkTraffic]
[CaptureNativePerformanceMetrics]
//[CaptureLighthousePerformanceMetrics]
public class CapturePerformanceMetricsTests : WebTest
{
    [Test]
    [ExecutionTimeUnder(2, PolarisLite.TimeUnit.Minutes)]
    public void VerifyToDoListCreatedSuccessfully()
    {

            App.Navigation.GoToUrl("https://todomvc.com/");
            OpenTechnologyApp("KnockoutJS");

            for (int i = 0; i <= 20; i++)
            {
                string randomString = GenerateRandomString();
                AddNewToDoItem(randomString);
                GetItemCheckbox(randomString).Check();
            }

        var metricList = App.DevTools.GetPerformanceMetrics().Result;
        foreach (var metric in metricList)
        {
            Console.WriteLine($"{metric.Name} = {metric.Value}");
        }

        Console.WriteLine("###############################");

        PerformanceService.AssertNativePerformanceMetricLessThan(JSHeapTotalSize, 33056000);
        PerformanceService.AssertNativePerformanceMetricLessThan(JSHeapUsedSize, 16027844);
        PerformanceService.AssertNativePerformanceMetricLessThan(LayoutDuration, 0.062);
        PerformanceService.AssertNativePerformanceMetricLessThan(RecalcStyleDuration, 0.054);
        PerformanceService.AssertNativePerformanceMetricLessThan(DevToolsCommandDuration, 0.578);
        PerformanceService.AssertNativePerformanceMetricLessThan(ScriptDuration, 0.545);
        PerformanceService.AssertNativePerformanceMetricLessThan(V8CompileDuration, 0.0501);
        PerformanceService.AssertNativePerformanceMetricLessThan(TaskDuration, 0.980);
        PerformanceService.AssertNativePerformanceMetricLessThan(TaskOtherDuration, 0.579);
        PerformanceService.AssertNativePerformanceMetricLessThan(ThreadTime, 0.929);
        PerformanceService.AssertNativePerformanceMetricLessThan(ProcessTime, 4.137);
        PerformanceService.AssertNativePerformanceMetricLessThan(FirstMeaningfulPaint, 97572.780);
        PerformanceService.AssertNativePerformanceMetricLessThan(DomContentLoaded, 97572.616);
        PerformanceService.AssertNativePerformanceMetricLessThan(NavigationStart, 97572.204);


        PerformanceService.PerformLighthouseAnalysis();

        PerformanceService.AssertMetric(m => m.Audits.FirstContentfulPaint.Score < 4.5);
        PerformanceService.AssertMetric(m => m.Audits.FirstMeaningfulPaint.Score < 4.2);
        PerformanceService.AssertMetric(m => m.Audits.LargestContentfulPaint.Score < 3.78);
        PerformanceService.AssertMetric(m => m.Categories.Performance.Score > 70);
        PerformanceService.AssertMetric(m => m.Categories.Accessibility.Score > 50);
        PerformanceService.AssertMetric(m => m.Categories.BestPractices.Score > 90);
        PerformanceService.AssertMetric(m => m.Categories.Seo.Score > 70);
    }

    private string GenerateRandomString()
    {
        var random = new Random();
        const string chars = "abcdefghijklmnopqrstuvwxyz";
        return new string(Enumerable.Repeat(chars, 10)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    private void AssertLeftItems(int expectedCount)
    {
        var resultSpan = App.Elements.FindByXPath<PolarisLite.Web.Label>("//footer/*/span | //footer/span");
        var expectedText = expectedCount == 1 ? $"{expectedCount} item left" : $"{expectedCount} items left";

        resultSpan.ValidateInnerTextContains(expectedText);
    }

    private CheckBox GetItemCheckbox(string todoItem)
    {
        var xpathLocator = $"//label[text()='{todoItem}']/preceding-sibling::input";
        return App.Elements.FindByXPath<CheckBox>(xpathLocator);
    }

    private void OpenTechnologyApp(string technologyName)
    {
        var technologyLink = App.Elements.FindByLinkText<Anchor>(technologyName);
        technologyLink.Click();
    }

    private void AddNewToDoItem(string todoItem)
    {
        var todoInput = App.Elements.FindByXPath<TextField>("//input[@placeholder='What needs to be done?']");
        var todoInputButton = App.Elements.FindByXPath<Button>("//input[@placeholder='What needs to be done?']");
        todoInput.TypeText(todoItem);

        App.Interactions.Click(todoInput).SendKeys(Keys.Enter).Perform();
    }
}