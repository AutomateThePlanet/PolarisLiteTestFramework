using Newtonsoft.Json;
using PolarisLite.Web.Integrations;
using PolarisLite.Web.Plugins.Performance.Lighthouse;
using System.Linq.Expressions;

namespace PolarisLite.Web.Plugins;

public static class LighthouseService
{
    public static event EventHandler<LighthouseReportEventArgs> AssertedLighthouseReportEvent;

    public static ThreadLocal<Root> PerformanceReport { get; set; }

    static LighthouseService()
    {
        PerformanceReport = new ThreadLocal<Root>();
    }

    public static void PerformLighthouseAnalysis()
    {
        var jsonResponse = LambdaTestHooks.GenerateLighthouseReport();

        JsonSerializerSettings settings = new()
        {
            NullValueHandling = NullValueHandling.Ignore
        };
        PerformanceReport.Value = JsonConvert.DeserializeObject<Root>(jsonResponse, settings);
    }

    public static void AssertFirstMeaningfulPaintScoreMoreThan(double expected)
    {
        double actualValue = PerformanceReport.Value.Audits.FirstMeaningfulPaint.Score;
        PerformAssertion(actualValue > expected, $"{PerformanceReport.Value.Audits.FirstMeaningfulPaint.Title} should be > {expected} but was {actualValue}");
        AssertedLighthouseReportEvent?.Invoke(null, new LighthouseReportEventArgs(expected.ToString(), actualValue.ToString(), PerformanceReport.Value.Audits.FirstMeaningfulPaint.Title));
    }

    public static void AssertFirstContentfulPaintScoreLessThan(double expected)
    {
        double actualValue = PerformanceReport.Value.Audits.FirstContentfulPaint.Score;
        PerformAssertion(actualValue < expected, $"{PerformanceReport.Value.Audits.FirstContentfulPaint.Title} should be < {expected} but was {actualValue}");
        AssertedLighthouseReportEvent?.Invoke(null, new LighthouseReportEventArgs(expected.ToString(), actualValue.ToString(), PerformanceReport.Value.Audits.FirstContentfulPaint.Title));
    }

    public static void AssertSpeedIndexScoreLessThan(double expected)
    {
        double actualValue = PerformanceReport.Value.Audits.SpeedIndex.Score;
        PerformAssertion(actualValue < expected, $"{PerformanceReport.Value.Audits.SpeedIndex.Title} should be < {expected} but was {actualValue}");
        AssertedLighthouseReportEvent?.Invoke(null, new LighthouseReportEventArgs(expected.ToString(), actualValue.ToString(), PerformanceReport.Value.Audits.SpeedIndex.Title));
    }

    public static void AssertLargestContentfulPaintScoreLessThan(double expected)
    {
        double actualValue = PerformanceReport.Value.Audits.LargestContentfulPaint.Score;
        PerformAssertion(actualValue < expected, $"{PerformanceReport.Value.Audits.LargestContentfulPaint.Title} should be < {expected} but was {actualValue}");
        AssertedLighthouseReportEvent?.Invoke(null, new LighthouseReportEventArgs(expected.ToString(), actualValue.ToString(), PerformanceReport.Value.Audits.LargestContentfulPaint.Title));
    }

    public static void AssertInteractiveScoreLessThan(double expected)
    {
        double actualValue = PerformanceReport.Value.Audits.Interactive.Score;
        PerformAssertion(actualValue < expected, $"{PerformanceReport.Value.Audits.Interactive.Title} should be < {expected} but was {actualValue}");
        AssertedLighthouseReportEvent?.Invoke(null, new LighthouseReportEventArgs(expected.ToString(), actualValue.ToString(), PerformanceReport.Value.Audits.Interactive.Title));
    }

    public static void AssertTotalBlockingTimeLessThan(double expected)
    {
        double actualValue = double.Parse(PerformanceReport.Value.Audits.TotalBlockingTime.DisplayValue);
        PerformAssertion(actualValue < expected, $"{PerformanceReport.Value.Audits.TotalBlockingTime.Title} should be < {expected} but was {actualValue}");
        AssertedLighthouseReportEvent?.Invoke(null, new LighthouseReportEventArgs(expected.ToString(), actualValue.ToString(), PerformanceReport.Value.Audits.TotalBlockingTime.Title));
    }

    public static void AssertCumulativeLayoutShiftScoreLessThan(double expected)
    {
        double actualValue = PerformanceReport.Value.Audits.CumulativeLayoutShift.Score;
        PerformAssertion(actualValue < expected, $"{PerformanceReport.Value.Audits.CumulativeLayoutShift.Title} should be < {expected} but was {actualValue}");
        AssertedLighthouseReportEvent?.Invoke(null, new LighthouseReportEventArgs(expected.ToString(), actualValue.ToString(), PerformanceReport.Value.Audits.CumulativeLayoutShift.Title));
    }

    public static void AssertRedirectScoreAboveThan(double expected)
    {
        double actualValue = PerformanceReport.Value.Audits.Redirects.NumericValue;
        PerformAssertion(actualValue > expected, $"{PerformanceReport.Value.Audits.Redirects.Title} should be > {expected} but was {actualValue}");
        AssertedLighthouseReportEvent?.Invoke(null, new LighthouseReportEventArgs(expected.ToString(), actualValue.ToString(), PerformanceReport.Value.Audits.Redirects.Title));
    }

    public static void AssertJavaExecutionTimeScoreAboveThan(double expected)
    {
        double actualValue = PerformanceReport.Value.Audits.BootupTime.NumericValue;
        PerformAssertion(actualValue > expected, $"{PerformanceReport.Value.Audits.BootupTime.Title} should be > {expected} but was {actualValue}");
        AssertedLighthouseReportEvent?.Invoke(null, new LighthouseReportEventArgs(expected.ToString(), actualValue.ToString(), PerformanceReport.Value.Audits.BootupTime.Title));
    }

    public static void AssertSEOScoreAboveThan(double expected)
    {
        double actualValue = PerformanceReport.Value.Categories.Seo.Score;
        PerformAssertion(actualValue > expected, $"{PerformanceReport.Value.Categories.Seo.Title} should be > {expected} but was {actualValue}");
        AssertedLighthouseReportEvent?.Invoke(null, new LighthouseReportEventArgs(expected.ToString(), actualValue.ToString(), PerformanceReport.Value.Categories.Seo.Title));
    }

    public static void AssertBestPracticesScoreAboveThan(double expected)
    {
        double actualValue = PerformanceReport.Value.Categories.BestPractices.Score;
        PerformAssertion(actualValue > expected, $"{PerformanceReport.Value.Categories.BestPractices.Title} should be > {expected} but was {actualValue}");
        AssertedLighthouseReportEvent?.Invoke(null, new LighthouseReportEventArgs(expected.ToString(), actualValue.ToString(), PerformanceReport.Value.Categories.BestPractices.Title));
    }

    public static void AssertPWAScoreAboveThan(double expected)
    {
        double actualValue = PerformanceReport.Value.Categories.Pwa.Score;
        PerformAssertion(actualValue > expected, $"{PerformanceReport.Value.Categories.Pwa.Title} should be > {expected} but was {actualValue}");
        AssertedLighthouseReportEvent?.Invoke(null, new LighthouseReportEventArgs(expected.ToString(), actualValue.ToString(), PerformanceReport.Value.Categories.Pwa.Title));
    }

    public static void AssertAccessibilityScoreAboveThan(double expected)
    {
        double actualValue = PerformanceReport.Value.Categories.Accessibility.Score;
        PerformAssertion(actualValue > expected, $"{PerformanceReport.Value.Categories.Accessibility.Title} should be > {expected} but was {actualValue}");
        AssertedLighthouseReportEvent?.Invoke(null, new LighthouseReportEventArgs(expected.ToString(), actualValue.ToString(), PerformanceReport.Value.Categories.Accessibility.Title));
    }

    public static void AssertPerformanceScoreAboveThan(double expected)
    {
        double actualValue = PerformanceReport.Value.Categories.Performance.Score;
        PerformAssertion(actualValue > expected, $"{PerformanceReport.Value.Categories.Performance.Title} should be > {expected} but was {actualValue}");
        AssertedLighthouseReportEvent?.Invoke(null, new LighthouseReportEventArgs(expected.ToString(), actualValue.ToString(), PerformanceReport.Value.Categories.Performance.Title));
    }

    public static MetricPreciseValidationBuilder AssertMetric(Expression<Func<Root, object>> expression)
    {
        string metricName = TypePropertiesNameResolver.GetMemberName(expression);
        Func<Root, object> compiledExpression = expression.Compile();
        dynamic actualValue = compiledExpression(PerformanceReport.Value);

        return new MetricPreciseValidationBuilder(actualValue, metricName);
    }

    private static void PerformAssertion(bool condition, string message)
    {
        if (!condition)
        {
            throw new LighthouseAssertFailedException(message);
        }
    }
}
