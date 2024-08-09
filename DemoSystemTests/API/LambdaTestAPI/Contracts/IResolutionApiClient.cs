using PolarisLite.API;

namespace DemoSystemTests;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public interface IResolutionApiClient
{
    /// <summary>
    /// Get Resolutions of Platforms. This API fetches available supported Platforms Resolution.
    /// </summary>
    /// <returns>GetResolutions</returns>
    Task<MeasuredResponse<GetResolutions>> ResolutionsGetAsync();
}
