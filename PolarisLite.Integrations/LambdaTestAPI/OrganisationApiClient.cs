using PolarisLite.API;
using RestSharp;

namespace PolarisLite.Integrations.LambdaTestAPI;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public class OrganisationApiClient : IOrganisationApiClient
{
    private readonly ApiClientAdapter _ApiClientAdapter;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrganisationApiClient"/> class.
    /// </summary>
    /// <param name="ApiClientAdapter">An instance of ApiClientAdapter (optional)</param>
    public OrganisationApiClient(ApiClientAdapter ApiClientAdapter = null)
    {
        _ApiClientAdapter = ApiClientAdapter ?? throw new ArgumentNullException(nameof(ApiClientAdapter));
    }

    /// <summary>
    /// Get organisation concurrency. This API fetches the organisation level concurrency.
    /// </summary>
    /// <returns>GetOrgConcurrency</returns>
    public async Task<MeasuredResponse<GetOrgConcurrency>> OrgConcurrencyGetAsync()
    {
        var request = new RestRequest("/org/concurrency", Method.Get);

        var response = await _ApiClientAdapter.GetAsync<GetOrgConcurrency>(request);
        return response;
    }
}
