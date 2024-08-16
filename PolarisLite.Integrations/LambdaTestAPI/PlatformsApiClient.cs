using PolarisLite.API;
using RestSharp;

namespace PolarisLite.Integrations.LambdaTestAPI;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public class PlatformsApiClient : IPlatformsApiClient
{
    private readonly ApiClientAdapter _ApiClientAdapter;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformsApiClient"/> class.
    /// </summary>
    /// <param name="ApiClientAdapter">An instance of ApiClientAdapter (optional)</param>
    public PlatformsApiClient(ApiClientAdapter ApiClientAdapter = null)
    {
        _ApiClientAdapter = ApiClientAdapter ?? throw new ArgumentNullException(nameof(ApiClientAdapter));
    }

    /// <summary>
    /// Fetch platforms. Fetch platforms along with browsers and versions supported.
    /// </summary>
    /// <returns>GetPlatformResponse</returns>
    public async Task<MeasuredResponse<GetPlatformResponse>> PlatformsGetAsync()
    {
        var request = new RestRequest("/platforms", Method.Get);

        var response = await _ApiClientAdapter.GetAsync<GetPlatformResponse>(request);
        return response;
    }
}
