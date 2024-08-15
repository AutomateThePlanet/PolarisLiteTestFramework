using PolarisLite.API;
using RestSharp;

namespace DemoSystemTests;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public class PlatformsApiClient : IPlatformsApiClient
{
    private readonly ApiClientAdapter _apiClientService;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformsApiClient"/> class.
    /// </summary>
    /// <param name="apiClientService">An instance of ApiClientService (optional)</param>
    public PlatformsApiClient(ApiClientAdapter apiClientService = null)
    {
        _apiClientService = apiClientService ?? throw new ArgumentNullException(nameof(apiClientService));
    }

    /// <summary>
    /// Fetch platforms. Fetch platforms along with browsers and versions supported.
    /// </summary>
    /// <returns>GetPlatformResponse</returns>
    public async Task<MeasuredResponse<GetPlatformResponse>> PlatformsGetAsync()
    {
        var request = new RestRequest("/platforms", Method.Get);

        var response = await _apiClientService.GetAsync<GetPlatformResponse>(request);
        return response;
    }
}
