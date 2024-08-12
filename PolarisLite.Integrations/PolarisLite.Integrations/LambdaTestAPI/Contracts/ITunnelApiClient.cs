using PolarisLite.API;

namespace PolarisLite.Integrations;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public interface ITunnelApiClient
{
    /// <summary>
    /// Fetch running tunnels of your account. To fetch lists of all tunnels running in an account.
    /// </summary>
    /// <returns>GetTunnelsResponse</returns>
    Task<MeasuredResponse<GetTunnelsResponse>> TunnelsGetAsync();

    /// <summary>
    /// Stop a running tunnel. To stop a running tunnel in your account.
    /// </summary>
    /// <param name="tunnelId">Your tunnel ID.</param>
    /// <returns>TunnelsDeleteResponse</returns>
    Task<MeasuredResponse<TunnelsDeleteResponse>> TunnelsTunnelIdDeleteAsync(int? tunnelId);
}
