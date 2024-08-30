using OpenQA.Selenium.Support.Extensions;
using PolarisLite.Core;
using PolarisLite.Core.Plugins;
using PolarisLite.Web.Configuration.StaticImplementation;
using PolarisLite.Web.Integrations;
using PolarisLite.Web.Services;
using System.Reflection;
namespace PolarisLite.Web.Plugins.Browser;

public class PerformanceTestingPluginV2 : Plugin
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

        if (geoLocationAttribute != null || WebSettings.SetGeoLocation)
        {
            DriverFactory.CustomGridOptions.Add("geoLocation", geoLocationAttribute?.Location ?? WebSettings.GeoLocation);
        }

        if (timeZoneAttribute != null || WebSettings.SetTimeZone)
        {
            DriverFactory.CustomGridOptions.Add("timezone", timeZoneAttribute?.TimeZoneName ?? WebSettings.TimeZoneName);
        }

        if (localeAttribute != null || WebSettings.SetLocale)
        {
            DriverFactory.CustomGridOptions.Add("language", localeAttribute?.Locale ?? WebSettings.Locale);
        }

        if (networkEmulationAttribute != null || WebSettings.EnableNetworkThrottling)
        {
            // or use just hooks as bellow
            DriverFactory.CustomGridOptions.Add("enableNetworkThrottling", true);
            DriverFactory.CustomGridOptions.Add("networkThrottling", networkEmulationAttribute?.ConnectionTypeName ?? WebSettings.NetworkThrottlingSettings.ConnectionTypeName);
        }

        if (captureNetworkTrafficAttribute != null || WebSettings.CaptureNetworkTraffic)
        {
            DriverFactory.CustomGridOptions.Add("network", true);
        }

        // if use hook to generate lighthouse report turn off
        if (captureLighthousePerformanceMetricsAttribute != null || WebSettings.CaptureLighthousePerformanceMetrics)
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
            if ((networkEmulationAttribute != null 
                && networkEmulationAttribute?.DownloadThroughput != null
                && networkEmulationAttribute?.UploadThroughput != null
                && networkEmulationAttribute?.Latency != null)
                || WebSettings.EnableNetworkThrottling)
            {
                LambdaTestHooks.ThrottleNetwork(networkEmulationAttribute?.ConnectionTypeName ?? WebSettings.NetworkThrottlingSettings.ConnectionTypeName);

                // Custom network throttling using executeScript
                Dictionary<string, object> throttleParams = new();
                throttleParams.Add("download", networkEmulationAttribute?.DownloadThroughput ?? WebSettings.NetworkThrottlingSettings.DownloadThroughput); // Maximum download speed in kbps
                throttleParams.Add("upload", networkEmulationAttribute?.UploadThroughput ?? WebSettings.NetworkThrottlingSettings.UploadThroughput);   // Maximum upload speed in kbps
                throttleParams.Add("latency", networkEmulationAttribute?.Latency ?? WebSettings.NetworkThrottlingSettings.Latency);   // Latency in ms

                // Use executeScript with the provided payload
                driver.ExecuteJavaScript("lambda-throttle-network", throttleParams);
            }
        }
        else
        {
            if (geoLocationAttribute != null || WebSettings.SetGeoLocation)
            {
                driverAdapter.OverrideGeolocationSettings((double)geoLocationAttribute?.Latitude, (double)geoLocationAttribute.Longitude, geoLocationAttribute.Accuracy);
            }

            if (timeZoneAttribute != null || WebSettings.SetTimeZone)
            {
                driverAdapter.OverrideTimezoneSettings(timeZoneAttribute?.TimeZoneId ?? WebSettings.TimeZoneId);
            }

            if (localeAttribute != null || WebSettings.SetLocale)
            {
                driverAdapter.OverrideLocaleSettings(localeAttribute?.Locale ?? WebSettings.Locale);
            }

            if (networkEmulationAttribute != null || WebSettings.EnableNetworkThrottling)
            {
                driverAdapter.EmulateNetworkConditions(networkEmulationAttribute.ConnectionType, networkEmulationAttribute.DownloadThroughput, networkEmulationAttribute.UploadThroughput, networkEmulationAttribute.Latency).Wait();
            }

            if (captureNetworkTrafficAttribute != null || WebSettings.CaptureNetworkTraffic)
            {
                driverAdapter.StartNetworkTrafficMonitoring();
            }

            // if use hook to generate lighthouse report turn off
            if (captureLighthousePerformanceMetricsAttribute != null || WebSettings.CaptureLighthousePerformanceMetrics)
            {
                // TODO: add custom service
            }
        }

        if (captureNativePerformanceMetricsAttribute != null || WebSettings.CaptureNativePerformanceMetrics)
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