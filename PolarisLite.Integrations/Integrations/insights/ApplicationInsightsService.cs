using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using PolarisLite.Integrations.Settings;

namespace PolarisLite.Integrations;

public static class ApplicationInsightsService
{
    static ApplicationInsightsService()
    {
        var config = new TelemetryConfiguration
        {
            ConnectionString = IntegrationSettings.AppInsightsSettings.ConnectionString
        };

        TelemetryClient = new TelemetryClient(config);

        // Set additional context information
        TelemetryClient.Context.Component.Version = typeof(ApplicationInsightsService).Assembly.GetName().Version.ToString();
        TelemetryClient.Context.GlobalProperties.Add("Client OS", System.Runtime.InteropServices.RuntimeInformation.OSDescription);
        TelemetryClient.Context.Device.OperatingSystem = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
    }

    private static TelemetryClient TelemetryClient { get; }

    public static void TrackException(ExceptionTelemetry exceptionTelemetry)
    {
        if (IntegrationSettings.AppInsightsSettings.IsEnabled)
        {
            TelemetryClient.TrackException(exceptionTelemetry);
        }
    }

    public static void TrackEvent(EventTelemetry eventTelemetry)
    {
        if (IntegrationSettings.AppInsightsSettings.IsEnabled)
        {
            TelemetryClient.TrackEvent(eventTelemetry);
        }
    }

    public static void TrackMetric(MetricTelemetry metricTelemetry)
    {
        if (IntegrationSettings.AppInsightsSettings.IsEnabled)
        {
            TelemetryClient.TrackMetric(metricTelemetry);
        }
    }

    public static void Flush()
    {
        if (IntegrationSettings.AppInsightsSettings.IsEnabled)
        {
            TelemetryClient.Flush();

            // Wait for the flush to complete
            System.Threading.Thread.Sleep(5000);
        }
    }
}
