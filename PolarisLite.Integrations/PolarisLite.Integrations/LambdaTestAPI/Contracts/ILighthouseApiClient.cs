using PolarisLite.API;

namespace PolarisLite.Integrations;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public interface ILighthouseApiClient
{
    /// <summary>
    /// To fetch the Lighthouse performance report data. To fetch URL to download the generated Lighthouse performance report JSON data.
    /// </summary>
    /// <param name="sessionId">SESSION ID</param>
    /// <returns>LighthouseReportResponse</returns>
    Task<MeasuredResponse<LighthouseReportResponse>> LighthouseReportSessionIdGetAsync(string sessionId);
}
