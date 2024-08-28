namespace PolarisLite.Web.Plugins;

public class LighthouseReportEventArgs
{
    public LighthouseReportEventArgs(dynamic expectedValue, dynamic actualValue, string metricName)
    {
        ExpectedValue = expectedValue;
        ActualValue = actualValue;
        Metric = metricName;
    }

    public LighthouseReportEventArgs(string message)
    {
        Message = message;
    }

    public dynamic ExpectedValue { get; }
    public dynamic ActualValue { get; }
    public string Metric { get; }
    public string Message { get; }
}
