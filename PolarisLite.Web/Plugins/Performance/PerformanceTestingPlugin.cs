using OpenQA.Selenium.Support.Extensions;
using PolarisLite.Core;
using PolarisLite.Core.Plugins;
using PolarisLite.Web.Integrations;
using PolarisLite.Web.Services;
using System.Reflection;
namespace PolarisLite.Web.Plugins.Browser;

public class PerformanceTestingPlugin : Plugin
{
    public override void OnBeforeTestInitialize(MethodInfo memberInfo) 
    {
        var networkEmulationAttribute = GetNetworkEmulationAttribute(memberInfo.DeclaringType);
        var geoLocationAttribute = GetGeoLocationAttribute(memberInfo.DeclaringType);
        var timeZoneAttribute = GetTimeZoneAttribute(memberInfo.DeclaringType);
        var localeAttribute = GetLocaleAttribute(memberInfo.DeclaringType);
        var captureNetworkTrafficAttribute = GetCaptureNetworkTrafficAttribute(memberInfo.DeclaringType);
        var captureLighthousePerformanceMetricsAttribute = GetCaptureLighthousePerformanceMetricsAttribute(memberInfo.DeclaringType);
        var captureNativePerformanceMetricsAttribute = GetCaptureNativePerformanceMetricsAttribute(memberInfo.DeclaringType);

        if (geoLocationAttribute != null)
        {
            DriverFactory.CustomGridOptions.Add("geoLocation", geoLocationAttribute.Location);
        }

        if (timeZoneAttribute != null)
        {
            DriverFactory.CustomGridOptions.Add("timezone", timeZoneAttribute.TimeZoneName);
        }

        if (localeAttribute != null)
        {
            DriverFactory.CustomGridOptions.Add("language", localeAttribute.Locale);
        }

        if (networkEmulationAttribute != null)
        {
            // or use just hooks as bellow
            DriverFactory.CustomGridOptions.Add("enableNetworkThrottling", true);
            DriverFactory.CustomGridOptions.Add("networkThrottling", networkEmulationAttribute.ConnectionTypeName);
        }

        if (captureNetworkTrafficAttribute != null)
        {
            DriverFactory.CustomGridOptions.Add("network", true);
        }

        // if use hook to generate lighthouse report turn off
        if (captureLighthousePerformanceMetricsAttribute != null)
        {
            //DriverFactory.CustomGridOptions.Add("performance", false);
        }
    }

    public override void OnAfterTestInitialize(MethodInfo memberInfo)
    {
        var driverAdapter = new DriverAdapter();
        var driver = DriverFactory.WrappedDriver;
        bool isLambdaTestRun = DriverFactory.ExecutionType.Equals(ExecutionType.LambdaTest);

        var networkEmulationAttribute = GetNetworkEmulationAttribute(memberInfo.DeclaringType);
        var geoLocationAttribute = GetGeoLocationAttribute(memberInfo.DeclaringType);
        var timeZoneAttribute = GetTimeZoneAttribute(memberInfo.DeclaringType);
        var localeAttribute = GetLocaleAttribute(memberInfo.DeclaringType);
        var captureNetworkTrafficAttribute = GetCaptureNetworkTrafficAttribute(memberInfo.DeclaringType);
        var captureLighthousePerformanceMetricsAttribute = GetCaptureLighthousePerformanceMetricsAttribute(memberInfo.DeclaringType);
        var captureNativePerformanceMetricsAttribute = GetCaptureNativePerformanceMetricsAttribute(memberInfo.DeclaringType);

        if (isLambdaTestRun)
        {
            if (networkEmulationAttribute != null 
                && networkEmulationAttribute?.DownloadThroughput != null
                && networkEmulationAttribute?.UploadThroughput != null
                && networkEmulationAttribute?.Latency != null)
            {
                LambdaTestHooks.ThrottleNetwork(networkEmulationAttribute.ConnectionTypeName);

                // Custom network throttling using executeScript
                Dictionary<string, object> throttleParams = new();
                throttleParams.Add("download", networkEmulationAttribute.DownloadThroughput); // Maximum download speed in kbps
                throttleParams.Add("upload", networkEmulationAttribute.UploadThroughput);   // Maximum upload speed in kbps
                throttleParams.Add("latency", networkEmulationAttribute.Latency);   // Latency in ms

                // Use executeScript with the provided payload
                driver.ExecuteJavaScript("lambda-throttle-network", throttleParams);
            }
        }
        else
        {
            if (geoLocationAttribute != null)
            {
                driverAdapter.OverrideGeolocationSettings((double)geoLocationAttribute.Latitude, (double)geoLocationAttribute.Longitude, geoLocationAttribute.Accuracy);
            }

            if (timeZoneAttribute != null)
            {
                driverAdapter.OverrideTimezoneSettings(timeZoneAttribute.TimeZoneId);
            }

            if (localeAttribute != null)
            {
                driverAdapter.OverrideLocaleSettings(localeAttribute.Locale);
            }

            if (networkEmulationAttribute != null)
            {
                driverAdapter.EmulateNetworkConditions(networkEmulationAttribute.ConnectionType, networkEmulationAttribute.DownloadThroughput, networkEmulationAttribute.UploadThroughput, networkEmulationAttribute.Latency).Wait();
            }

            if (captureNetworkTrafficAttribute != null)
            {
                driverAdapter.StartNetworkTrafficMonitoring();
            }

            // if use hook to generate lighthouse report turn off
            if (captureLighthousePerformanceMetricsAttribute != null)
            {
                // TODO: add custom service
            }
        }

        if (captureNativePerformanceMetricsAttribute != null)
        {
            driverAdapter.TurnOnPerformanceMetrics().Wait();
        }
    }

    public override void OnAfterTestCleanup(TestOutcome testResult, MethodInfo memberInfo, Exception failedTestException)
    {
        var driverAdapter = new DriverAdapter();
        var driver = DriverFactory.WrappedDriver;
        bool isLambdaTestRun = DriverFactory.ExecutionType.Equals(ExecutionType.LambdaTest);

        if (!isLambdaTestRun)
        {
            driverAdapter.ClearNetworkTrafficHistory();
        }
    }

    private GeolocationAttribute GetGeoLocationAttribute(Type testClass)
    {
        var attribute = testClass.GetCustomAttribute<GeolocationAttribute>(true);
        return attribute;
    }

    private TimeZoneAttribute GetTimeZoneAttribute(Type testClass)
    {
        var attribute = testClass.GetCustomAttribute<TimeZoneAttribute>(true);
        return attribute;
    }

    private LocaleAttribute GetLocaleAttribute(Type testClass)
    {
        var attribute = testClass.GetCustomAttribute<LocaleAttribute>(true);
        return attribute;
    }

    private CaptureNetworkTrafficAttribute GetCaptureNetworkTrafficAttribute(Type testClass)
    {
        var attribute = testClass.GetCustomAttribute<CaptureNetworkTrafficAttribute>(true);
        return attribute;
    }

    private CaptureLighthousePerformanceMetricsAttribute GetCaptureLighthousePerformanceMetricsAttribute(Type testClass)
    {
        var attribute = testClass.GetCustomAttribute<CaptureLighthousePerformanceMetricsAttribute>(true);
        return attribute;
    }

    private CaptureNativePerformanceMetricsAttribute GetCaptureNativePerformanceMetricsAttribute(Type testClass)
    {
        var attribute = testClass.GetCustomAttribute<CaptureNativePerformanceMetricsAttribute>(true);
        return attribute;
    }

    private NetworkEmulationAttribute GetNetworkEmulationAttribute(Type testClass)
    {
        var attribute = testClass.GetCustomAttribute<NetworkEmulationAttribute>(true);
        return attribute;
    }
}