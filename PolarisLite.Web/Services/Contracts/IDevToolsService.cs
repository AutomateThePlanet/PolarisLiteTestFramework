using System.Collections.Concurrent;

namespace PolarisLite.Web.Services.Contracts;
public interface IDevToolsService
{
    DevToolsSessionDomains DevToolsSessionDomains { get; set; }
    DevToolsSession DevToolsSession { get; set; }

    ConcurrentBag<NetworkRequestSentEventArgs> RequestsHistory { get; set; }
    ConcurrentBag<NetworkResponseReceivedEventArgs> ResponsesHistory { get; set; }

    Task ListenConsoleLogs(EventHandler<Console.MessageAddedEventArgs> messageAddedHandler);
    Task ListenJavaScriptConsoleLogs(EventHandler<JavaScriptConsoleApiCalledEventArgs> javaScriptConsoleApiCalled);
    Task ListenJavaScriptExceptionsThrown(EventHandler<JavaScriptExceptionThrownEventArgs> javaScriptExceptionThrown);
    Task AddInitializationScript(string name, string script);
    void StartNetworkTrafficMonitoring();
    void ClearNetworkTrafficHistory();
    List<string> GetSpecificRequestUrls(string requestName);
    long GetResponseContentLengthByPartialUrl(string partialUrl);
    string GetResponseContentTypeByPartialUrl(string partialUrl);
    void AssertResponse404ErrorCodeRecievedByPartialUrl(string partialUrl);
    void AssertNoErrorCodes();
    void AssertRequestMade(string url);
    void AssertRequestNotMade(string url);
    int CountRequestsMadeByFileFormat(string fileFormat);
    void OverrideGeolocationSettings(double latitude, double longitude, int accuracy);
    void OverrideTimezoneSettings(string timezoneId);
    void OverrideLocaleSettings(string locale);
    void OverrideDeviceMetrics(long width, long height, bool mobile, double deviceScaleFactor);
    void IgnoreCertificateError();
    Task BlockUrls(string pattern);
    Task EmulateNetworkConditionOffline();
    Task EmulateNetworkConditions(Network.ConnectionType connectionType, double downloadThroughput, double latency, double uploadThroughput);
    Task TurnOnPerformanceMetrics();
    Task<Performance.Metric[]> GetPerformanceMetrics();
}
