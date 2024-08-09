using PolarisLite.API;

namespace DemoSystemTests;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public interface IOrganisationApiClient
{
    /// <summary>
    /// Get organisation concurrency. This API fetches the organisation level concurrency.
    /// </summary>
    /// <returns>GetOrgConcurrency</returns>
    Task<MeasuredResponse<GetOrgConcurrency>> OrgConcurrencyGetAsync();
}
