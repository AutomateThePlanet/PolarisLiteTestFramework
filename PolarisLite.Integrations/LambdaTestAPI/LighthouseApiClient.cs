using PolarisLite.API;
using RestSharp;

namespace PolarisLite.Integrations.LambdaTestAPI;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public class LighthouseApiClient : LambdaTestApiClient, ILighthouseApiClient
{
    /// <summary>
    /// To fetch the Lighthouse performance report data. To fetch URL to download the generated Lighthouse performance report JSON data.
    /// </summary>
    /// <param name="sessionId">SESSION ID</param>
    /// <returns>LighthouseReportResponse</returns>
    public async Task<MeasuredResponse<LighthouseReportResponse>> LighthouseReportSessionIdGetAsync(string sessionId)
    {
        if (string.IsNullOrEmpty(sessionId))
        {
            throw new ApiException(400, "Missing required parameter 'sessionId' when calling LighthouseReportSessionIdGet");
        }

        var request = new RestRequest($"/lighthouse/report/{sessionId}", Method.Get);

        var response = await _ApiClientAdapter.GetAsync<LighthouseReportResponse>(request);
        return response;
    }
}