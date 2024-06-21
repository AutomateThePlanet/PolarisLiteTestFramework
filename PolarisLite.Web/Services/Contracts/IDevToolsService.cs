namespace PolarisLite.Web.Services.Contracts;
public interface IDevToolsService
{
    DevToolsSessionDomains DevToolsSessionDomains { get; set; }
    DevToolsSession DevToolsSession { get; set; }
    List<NetworkRequestSentEventArgs> RequestsHistory { get; set; }
    List<NetworkResponseReceivedEventArgs> ResponsesHistory { get; set; }

    void StartNetworkTrafficMonitoring();
    void ClearNetworkTrafficHistory();
    List<string> GetSpecificRequestUrls(string requestName);
    void OverrideScreenResolution(long screenHeight, long screenWidth, bool mobile, double deviceScaleFactor);
    long GetResponseContentLengthByPartialUrl(string partialUrl);
    string GetResponseContentTypeByPartialUrl(string partialUrl);
    void AssertResponse404ErrorCodeReceivedByPartialUrl(string partialUrl);
    void AssertNoErrorCodes();
    void AssertRequestMade(string url);
    void AssertRequestNotMade(string url);
    int CountRequestsMadeByFileFormat(string fileFormat);
    void DisableCache();
    Task<DocumentSnapshot[]> CaptureSnapshot();
    void AddExtraHttpHeader(string header);
    void OverrideUserAgent(string userAgent);
    void OverrideGeolocationSettings(double latitude, double longitude, int accuracy);
    void OverrideDeviceMetrics(long width, long height, bool mobile, double deviceScaleFactor);
    void IgnoreCertificateError();
    Task BlockUrls(string pattern);
    Task EmulateNetworkConditionOffline();
    Task EmulateNetworkConditions(ConnectionType connectionType, int downloadThroughput, double latency, int uploadThroughput);
    Task ListenConsoleLogs(EventHandler<MessageAddedEventArgs> messageAddedHandler);
    Task ListenJavaScriptConsoleLogs(EventHandler<JavaScriptConsoleApiCalledEventArgs> javaScriptConsoleApiCalled);
    Task ListenJavaScriptExceptionsThrown(EventHandler<JavaScriptExceptionThrownEventArgs> javaScriptExceptionThrown);
    Task AddInitializationScript(string name, string script);
    Task TurnOnPerformanceMetrics();
    Task<Metric[]> GetPerformanceMetrics();
}
