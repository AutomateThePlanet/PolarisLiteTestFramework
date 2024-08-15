using PolarisLite.API;
using RestSharp;

namespace DemoSystemTests;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public class OrganisationApiClient : IOrganisationApiClient
{
    private readonly ApiClientAdapter _apiClientService;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrganisationApiClient"/> class.
    /// </summary>
    /// <param name="apiClientService">An instance of ApiClientService (optional)</param>
    public OrganisationApiClient(ApiClientAdapter apiClientService = null)
    {
        _apiClientService = apiClientService ?? throw new ArgumentNullException(nameof(apiClientService));
    }

    /// <summary>
    /// Get organisation concurrency. This API fetches the organisation level concurrency.
    /// </summary>
    /// <returns>GetOrgConcurrency</returns>
    public async Task<MeasuredResponse<GetOrgConcurrency>> OrgConcurrencyGetAsync()
    {
        var request = new RestRequest("/org/concurrency", Method.Get);

        var response = await _apiClientService.GetAsync<GetOrgConcurrency>(request);
        return response;
    }
}
