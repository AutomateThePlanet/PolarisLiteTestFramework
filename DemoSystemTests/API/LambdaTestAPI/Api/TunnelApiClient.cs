using PolarisLite.API;
using RestSharp;

namespace DemoSystemTests;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public class TunnelApiClient : ITunnelApiClient
{
    private readonly ApiClientService _apiClientService;

    /// <summary>
    /// Initializes a new instance of the <see cref="TunnelApiClient"/> class.
    /// </summary>
    /// <param name="apiClientService">An instance of ApiClientService</param>
    public TunnelApiClient(ApiClientService apiClientService)
    {
        _apiClientService = apiClientService ?? throw new ArgumentNullException(nameof(apiClientService));
    }

    /// <summary>
    /// Fetch running tunnels of your account. To fetch lists of all tunnels running in an account.
    /// </summary>
    /// <returns>GetTunnelsResponse</returns>
    public async Task<MeasuredResponse<GetTunnelsResponse>> TunnelsGetAsync()
    {
        var request = new RestRequest("/tunnels", Method.Get);

        var response = await _apiClientService.GetAsync<GetTunnelsResponse>(request);
        return response;
    }

    /// <summary>
    /// Stop a running tunnel. To stop a running tunnel in your account.
    /// </summary>
    /// <param name="tunnelId">Your tunnel ID.</param>
    /// <returns>TunnelsDeleteResponse</returns>
    public async Task<MeasuredResponse<TunnelsDeleteResponse>> TunnelsTunnelIdDeleteAsync(int? tunnelId)
    {
        if (tunnelId == null)
        {
            throw new ApiException(400, "Missing required parameter 'tunnelId' when calling TunnelsTunnelIdDelete");
        }

        var request = new RestRequest("/tunnels/{tunnel_id}", Method.Delete);
        request.AddUrlSegment("tunnel_id", tunnelId.ToString());

        var response = await _apiClientService.DeleteAsync<TunnelsDeleteResponse>(request);
        return response;
    }
}
