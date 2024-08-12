using PolarisLite.API;

namespace PolarisLite.Integrations;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public interface IPlatformsApiClient
{
    /// <summary>
    /// Fetch platforms. Fetch platforms along with browsers and versions supported.
    /// </summary>
    /// <returns>GetPlatformResponse</returns>
    Task<MeasuredResponse<GetPlatformResponse>> PlatformsGetAsync();
}
