using PolarisLite.API;
using RestSharp;

namespace DemoSystemTests;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public class PlatformsApiClient : LambdaTestApiClient, IPlatformsApiClient
{
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
