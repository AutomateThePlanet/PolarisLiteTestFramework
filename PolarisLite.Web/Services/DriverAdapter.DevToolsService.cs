using NUnit.Framework;
using PolarisLite.Web.Services.Contracts;
using System.Collections.Concurrent;

namespace PolarisLite.Web.Services;

public partial class DriverAdapter : IDevToolsService
{
    public DevToolsSessionDomains DevToolsSessionDomains { get; set; }
    public DevToolsSession DevToolsSession { get; set; }
    public ConcurrentBag<NetworkRequestSentEventArgs> RequestsHistory { get; set; }
    public ConcurrentBag<NetworkResponseReceivedEventArgs> ResponsesHistory { get; set; }

    public async Task ListenConsoleLogs(EventHandler<Console.MessageAddedEventArgs> messageAddedHandler)
    {
        DevToolsSessionDomains.Console.MessageAdded += messageAddedHandler;
        await DevToolsSessionDomains.Console.Enable();
    }

    public async Task ListenJavaScriptConsoleLogs(EventHandler<JavaScriptConsoleApiCalledEventArgs> javaScriptConsoleApiCalled)
    {
        IJavaScriptEngine monitor = new JavaScriptEngine(_webDriver);
        monitor.JavaScriptConsoleApiCalled += javaScriptConsoleApiCalled;
        await monitor.StartEventMonitoring();
    }

    public async Task ListenJavaScriptExceptionsThrown(EventHandler<JavaScriptExceptionThrownEventArgs> javaScriptExceptionThrown)
    {
        IJavaScriptEngine monitor = new JavaScriptEngine(_webDriver);
        monitor.JavaScriptExceptionThrown += javaScriptExceptionThrown;
        await monitor.StartEventMonitoring();
    }

    public async Task AddInitializationScript(string name, string script)
    {
        IJavaScriptEngine monitor = new JavaScriptEngine(_webDriver);
        await monitor.AddInitializationScript(name, script);
    }

    public void StartNetworkTrafficMonitoring()
    {
        RequestsHistory = new();
        ResponsesHistory = new();

        INetwork networkInterceptor = WrappedDriver.Manage().Network;
        networkInterceptor.NetworkResponseReceived += (o, s) =>
        {
            ResponsesHistory.Add(s);
        };
        networkInterceptor.NetworkRequestSent += (o, s) =>
        {
            RequestsHistory.Add(s);
        };

        networkInterceptor.StartMonitoring();
    }

    public void ClearNetworkTrafficHistory()
    {
        RequestsHistory?.Clear();
        ResponsesHistory?.Clear();
    }

    public List<string> GetSpecificRequestUrls(string requestName)
    {
        HashSet<NetworkRequestSentEventArgs> requests = new HashSet<NetworkRequestSentEventArgs>(RequestsHistory);
        return requests.ToList()
            .FindAll(r => r.RequestUrl.ToString().Contains(requestName))
            .Select(fr => fr.RequestUrl).ToList();
    }

    public long GetResponseContentLengthByPartialUrl(string partialUrl)
    {
        var contentLength = ResponsesHistory.ToList().Find(r => r.ResponseUrl.Contains(partialUrl)).ResponseHeaders["content-length"].ToString();

        return long.Parse(contentLength);
    }

    public string GetResponseContentTypeByPartialUrl(string partialUrl)
    {
        var contentType = ResponsesHistory.ToList().Find(r => r.ResponseUrl.Contains(partialUrl)).ResponseHeaders["content-type"].ToString();

        return contentType;
    }

    public void AssertResponse404ErrorCodeRecievedByPartialUrl(string partialUrl)
    {
        var responseStatusCode = ResponsesHistory.ToList().Find(r => r.ResponseUrl.Contains(partialUrl)).ResponseStatusCode;

        Assert.That(responseStatusCode, Is.EqualTo(404), "404 Error code not detected on the page.");
    }

    public void AssertNoErrorCodes()
    {
        bool hasErrorCode = ResponsesHistory.Any(r => r.ResponseStatusCode > 400 && r.ResponseStatusCode < 599);

        Assert.That(hasErrorCode, Is.False, "Error codes detected on the page.");
    }

    public void AssertRequestMade(string url)
    {
        bool isRequestMade = ResponsesHistory.Any(r => r.ResponseUrl.Contains(url));

        Assert.That(isRequestMade, Is.True, $"Request {url} was not made.");
    }

    public void AssertRequestNotMade(string url)
    {
        bool areRequestsMade = ResponsesHistory.Any(r => r.ResponseUrl.Contains(url));

        Assert.That(areRequestsMade, Is.False, $"Request {url} was made.");
    }

    public int CountRequestsMadeByFileFormat(string fileFormat)
    {
        var responsesList = ResponsesHistory.ToList().FindAll(r => r.ResponseUrl.EndsWith(fileFormat)).ToList();

        var numberOfResponses = responsesList.Count;

        return numberOfResponses;
    }

    public void OverrideGeolocationSettings(double latitude, double longitude, int accuracy)
    {
        var settings = new Emulation.SetGeolocationOverrideCommandSettings();
        settings.Latitude = latitude;
        settings.Longitude = longitude;
        settings.Accuracy = accuracy;

        DevToolsSession.SendCommand(settings);
    }

    public void OverrideTimezoneSettings(string timezoneId)
    {
        var settings = new Emulation.SetTimezoneOverrideCommandSettings();
        settings.TimezoneId = timezoneId;

        DevToolsSession.SendCommand(settings);
    }

    public void OverrideLocaleSettings(string locale)
    {
        var settings = new Emulation.SetLocaleOverrideCommandSettings();
        settings.Locale = locale;

        DevToolsSession.SendCommand(settings);
    }

    public void OverrideDeviceMetrics(long width, long height, bool mobile, double deviceScaleFactor)
    {
        var settings = new Emulation.SetDeviceMetricsOverrideCommandSettings();
        settings.Width = width;
        settings.Height = height;
        settings.Mobile = mobile;
        settings.DeviceScaleFactor = deviceScaleFactor;

        DevToolsSession.SendCommand(settings);
    }

    public void IgnoreCertificateError()
    {
        var settings = new Security.SetIgnoreCertificateErrorsCommandSettings();
        settings.Ignore = true;

        DevToolsSession.SendCommand(settings);
    }

    public async Task BlockUrls(string pattern)
    {
        await DevToolsSessionDomains.Network.Enable(new Network.EnableCommandSettings());
        await DevToolsSessionDomains.Network.SetBlockedURLs(new Network.SetBlockedURLsCommandSettings()
        {
            Urls = new string[] { "*://*/*.css" }
        });
    }

    public async Task EmulateNetworkConditionOffline()
    {
        await DevToolsSessionDomains.Network.Enable(new Network.EnableCommandSettings()
        {
            MaxTotalBufferSize = 100000000
        });

        await DevToolsSessionDomains.Network.EmulateNetworkConditions(new Network.EmulateNetworkConditionsCommandSettings()
        {
            Offline = true,
        });
    }

    public async Task EmulateNetworkConditions(Network.ConnectionType connectionType, double downloadThroughput, double latency, double uploadThroughput)
    {
        await DevToolsSessionDomains.Network.Enable(new Network.EnableCommandSettings()
        {
            MaxTotalBufferSize = 100000000
        });

        await DevToolsSessionDomains.Network.EmulateNetworkConditions(new Network.EmulateNetworkConditionsCommandSettings()
        {
            ConnectionType = connectionType,
            DownloadThroughput = downloadThroughput,
            Latency = latency,
            UploadThroughput = uploadThroughput,
        });
    }

    public async Task TurnOnPerformanceMetrics()
    {
        var enableCommand = new Performance.EnableCommandSettings();
        await DevToolsSession.SendCommand(enableCommand);
    }

    public async Task<Performance.Metric[]> GetPerformanceMetrics()
    {
        var metricsResponse = await DevToolsSession.SendCommand<Performance.GetMetricsCommandSettings, Performance.GetMetricsCommandResponse>(new Performance.GetMetricsCommandSettings());
        return metricsResponse.Metrics;
    }
}
