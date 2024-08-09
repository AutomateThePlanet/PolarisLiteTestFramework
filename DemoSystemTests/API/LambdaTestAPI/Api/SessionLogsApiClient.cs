using PolarisLite.API;
using RestSharp;

namespace DemoSystemTests;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public class SessionLogsApiClient : ISessionLogsApiClient
{
    private readonly ApiClientService _apiClientService;

    /// <summary>
    /// Initializes a new instance of the <see cref="SessionLogsApiClient"/> class.
    /// </summary>
    /// <param name="apiClientService">An instance of ApiClientService (optional)</param>
    public SessionLogsApiClient(ApiClientService apiClientService = null)
    {
        _apiClientService = apiClientService ?? throw new ArgumentNullException(nameof(apiClientService));
    }

    /// <summary>
    /// Console/browser log of a test session. Fetches console/browser log that contains console errors thrown by the application during a test session in plain JSON text.
    /// </summary>
    /// <param name="sessionId">Session ID</param>
    /// <returns>LogNewResponse</returns>
    public async Task<MeasuredResponse<LogNewResponse>> SessionBrowserLogsV2Async(string sessionId)
    {
        if (string.IsNullOrEmpty(sessionId))
        {
            throw new ApiException(400, "Missing required parameter 'sessionId' when calling SessionBrowserLogsV2");
        }

        var request = new RestRequest($"/sessions/{sessionId}/v2/log/console", Method.Get);

        var response = await _apiClientService.GetAsync<LogNewResponse>(request);
        return response;
    }

    /// <summary>
    /// Command logs of a test session. Fetches all executed commands of a test session in a ZIP link.
    /// </summary>
    /// <param name="sessionId">Session ID</param>
    /// <returns>LogNewResponse</returns>
    public async Task<MeasuredResponse<LogNewResponse>> SessionCommandLogsV2Async(string sessionId)
    {
        if (string.IsNullOrEmpty(sessionId))
        {
            throw new ApiException(400, "Missing required parameter 'sessionId' when calling SessionCommandLogsV2");
        }

        var request = new RestRequest($"/sessions/{sessionId}/v2/log/command", Method.Get);

        var response = await _apiClientService.GetAsync<LogNewResponse>(request);
        return response;
    }

    /// <summary>
    /// Full HAR log of a test session. Fetches full network HAR log containing all requested HARs of a test session along with request and response content.
    /// </summary>
    /// <param name="sessionId">Session ID</param>
    /// <returns>LogNewResponse</returns>
    public async Task<MeasuredResponse<LogNewResponse>> SessionNetworkFullHarLogsV2Async(string sessionId)
    {
        if (string.IsNullOrEmpty(sessionId))
        {
            throw new ApiException(400, "Missing required parameter 'sessionId' when calling SessionNetworkFullHarLogsV2");
        }

        var request = new RestRequest($"/sessions/{sessionId}/v2/log/full-har", Method.Get);

        var response = await _apiClientService.GetAsync<LogNewResponse>(request);
        return response;
    }

    /// <summary>
    /// Network HAR log of a test session. Fetches network HAR log containing all requested HARs of a test session in plain JSON text.
    /// </summary>
    /// <param name="sessionId">Session ID</param>
    /// <returns>LogNewResponse</returns>
    public async Task<MeasuredResponse<LogNewResponse>> SessionNetworkHarLogsV2Async(string sessionId)
    {
        if (string.IsNullOrEmpty(sessionId))
        {
            throw new ApiException(400, "Missing required parameter 'sessionId' when calling SessionNetworkHarLogsV2");
        }

        var request = new RestRequest($"/sessions/{sessionId}/v2/log/network.har", Method.Get);

        var response = await _apiClientService.GetAsync<LogNewResponse>(request);
        return response;
    }

    /// <summary>
    /// Network log of a test session. Fetches network log containing all requested URLs of a test session in plain JSON text.
    /// </summary>
    /// <param name="sessionId">Session ID</param>
    /// <returns>LogNewResponse</returns>
    public async Task<MeasuredResponse<LogNewResponse>> SessionNetworkLogsV2Async(string sessionId)
    {
        if (string.IsNullOrEmpty(sessionId))
        {
            throw new ApiException(400, "Missing required parameter 'sessionId' when calling SessionNetworkLogsV2");
        }

        var request = new RestRequest($"/sessions/{sessionId}/v2/log/network", Method.Get);

        var response = await _apiClientService.GetAsync<LogNewResponse>(request);
        return response;
    }

    /// <summary>
    /// Selenium/Appium log of a test session. Fetches Selenium/Appium log containing grid requests and responses of a test session in plain text.
    /// </summary>
    /// <param name="sessionId">Session ID</param>
    /// <returns>LogNewResponse</returns>
    public async Task<MeasuredResponse<LogNewResponse>> SessionRawLogsV2Async(string sessionId)
    {
        if (string.IsNullOrEmpty(sessionId))
        {
            throw new ApiException(400, "Missing required parameter 'sessionId' when calling SessionRawLogsV2");
        }

        var request = new RestRequest($"/sessions/{sessionId}/v2/log/selenium", Method.Get);

        var response = await _apiClientService.GetAsync<LogNewResponse>(request);
        return response;
    }
}
