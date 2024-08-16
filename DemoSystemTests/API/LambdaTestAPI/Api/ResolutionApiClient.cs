using PolarisLite.API;
using RestSharp;

namespace DemoSystemTests;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public class ResolutionApiClient : LambdaTestApiClient, IResolutionApiClient
{
    /// <summary>
    /// Get Resolutions of Platforms. This API fetches available supported Platforms Resolution.
    /// </summary>
    /// <returns>GetResolutions</returns>
    public async Task<MeasuredResponse<GetResolutions>> ResolutionsGetAsync()
    {
        var request = new RestRequest("/resolutions", Method.Get);

        var response = await _apiClientService.GetAsync<GetResolutions>(request);
        return response;
    }
}
