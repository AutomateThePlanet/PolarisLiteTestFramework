using PolarisLite.API;

namespace PolarisLite.Integrations;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public interface ISessionLogsApiClient
{
    /// <summary>
    /// Console/browser log of a test session. Fetches console/browser log that contains console errors thrown by the application during a test session in plain JSON text.
    /// </summary>
    /// <param name="sessionId">Session ID</param>
    /// <returns>LogNewResponse</returns>
    Task<MeasuredResponse<LogNewResponse>> SessionBrowserLogsV2Async(string sessionId);

    /// <summary>
    /// Command logs of a test session. Fetches all executed commands of a test session in a ZIP link.
    /// </summary>
    /// <param name="sessionId">Session ID</param>
    /// <returns>LogNewResponse</returns>
    Task<MeasuredResponse<LogNewResponse>> SessionCommandLogsV2Async(string sessionId);

    /// <summary>
    /// Full HAR log of a test session. Fetches full network HAR log containing all requested HARs of a test session along with request and response content.
    /// </summary>
    /// <param name="sessionId">Session ID</param>
    /// <returns>LogNewResponse</returns>
    Task<MeasuredResponse<LogNewResponse>> SessionNetworkFullHarLogsV2Async(string sessionId);

    /// <summary>
    /// Network HAR log of a test session. Fetches network HAR log containing all requested HARs of a test session in plain JSON text.
    /// </summary>
    /// <param name="sessionId">Session ID</param>
    /// <returns>LogNewResponse</returns>
    Task<MeasuredResponse<LogNewResponse>> SessionNetworkHarLogsV2Async(string sessionId);

    /// <summary>
    /// Network log of a test session. Fetches network log containing all requested URLs of a test session in plain JSON text.
    /// </summary>
    /// <param name="sessionId">Session ID</param>
    /// <returns>LogNewResponse</returns>
    Task<MeasuredResponse<LogNewResponse>> SessionNetworkLogsV2Async(string sessionId);

    /// <summary>
    /// Selenium/Appium log of a test session. Fetches Selenium/Appium log containing grid requests and responses of a test session in plain text.
    /// </summary>
    /// <param name="sessionId">Session ID</param>
    /// <returns>LogNewResponse</returns>
    Task<MeasuredResponse<LogNewResponse>> SessionRawLogsV2Async(string sessionId);
}
