using PolarisLite.API;
using RestSharp;

namespace PolarisLite.Integrations.LambdaTestAPI;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public class ResolutionApiClient : IResolutionApiClient
{
    private readonly ApiClientAdapter _ApiClientAdapter;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResolutionApiClient"/> class.
    /// </summary>
    /// <param name="ApiClientAdapter">An instance of ApiClientAdapter (optional)</param>
    public ResolutionApiClient(ApiClientAdapter ApiClientAdapter = null)
    {
        _ApiClientAdapter = ApiClientAdapter ?? throw new ArgumentNullException(nameof(ApiClientAdapter));
    }

    /// <summary>
    /// Get Resolutions of Platforms. This API fetches available supported Platforms Resolution.
    /// </summary>
    /// <returns>GetResolutions</returns>
    public async Task<MeasuredResponse<GetResolutions>> ResolutionsGetAsync()
    {
        var request = new RestRequest("/resolutions", Method.Get);

        var response = await _ApiClientAdapter.GetAsync<GetResolutions>(request);
        return response;
    }
}
