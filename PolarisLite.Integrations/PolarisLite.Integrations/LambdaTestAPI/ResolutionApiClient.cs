using PolarisLite.API;
using RestSharp;

namespace PolarisLite.Integrations.LambdaTestAPI;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public class ResolutionApiClient : IResolutionApiClient
{
    private readonly ApiClientService _apiClientService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResolutionApiClient"/> class.
    /// </summary>
    /// <param name="apiClientService">An instance of ApiClientService (optional)</param>
    public ResolutionApiClient(ApiClientService apiClientService = null)
    {
        _apiClientService = apiClientService ?? throw new ArgumentNullException(nameof(apiClientService));
    }

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
