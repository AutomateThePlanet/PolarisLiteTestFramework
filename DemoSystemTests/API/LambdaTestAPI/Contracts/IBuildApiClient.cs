using PolarisLite.API;

namespace DemoSystemTests;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public interface IBuildApiClient
{
    /// <summary>
    /// Update Build Name or Status. To change build name or status.
    /// </summary>
    /// <param name="body">You can update either name or status or both of a build in single request.</param>
    /// <param name="buildId">Build ID that needs to be updated.</param>
    /// <returns>EditBuildResponse</returns>
    Task<MeasuredResponse<EditBuildResponse>> BuildIdAsync(EditBuild body, string buildId);

    /// <summary>
    /// Stop tests by BuildID. To stop tests by BuildID.
    /// </summary>
    /// <param name="build">Build ID for which to stop tests</param>
    /// <returns>StopBuildResponse</returns>
    Task<MeasuredResponse<StopBuildResponse>> BuildStopPutAsync(string build);

    /// <summary>
    /// Fetch all builds of an account. Fetch all builds of an account. You can limit the number of records and apply filters on status, build date range, and sort by users, start date, and end date in ascending and descending order. You can apply sort on multiple columns.
    /// </summary>
    /// <param name="offset">It defines the number of lists on the basis of limit parameter. e.g. offset=10</param>
    /// <param name="limit">To fetch a specified number of records. e.g. limit=10</param>
    /// <param name="status">To fetch the list of builds with specific statuses. You can pass multiple comma-separated statuses e.g. running, queued, completed, timeout, and error.</param>
    /// <param name="fromdate">To fetch the list of builds that executed from the specified Start Date. The Date format must be YYYY-MM-DD. e.g. "2018-03-15".</param>
    /// <param name="todate">To fetch the list of builds that executed till the specified End Date. If both fromdate and todate value provided then it works as range filter. The Date format must be YYYY-MM-DD. e.g. "2018-03-15".</param>
    /// <param name="sort">To sort the list in ascending or descending order using multiple keys. e.g. "asc.user_id,desc.org_id"</param>
    /// <returns>ListBuildResponse</returns>
    Task<MeasuredResponse<ListBuildResponse>> BuildsAsync(int? offset, int? limit, string status, string fromdate, string todate, string sort);

    /// <summary>
    /// Fetch specified build details. To fetch build details of the build ID specified by the user.
    /// </summary>
    /// <param name="buildId">Build ID for which details are to be fetched.</param>
    /// <param name="shareExpiryLimit">Days after which share link will expire (3, 7, 10, 30)</param>
    /// <returns>SingleBuildResponse</returns>
    Task<MeasuredResponse<SingleBuildResponse>> SinglebuildAsync(int? buildId, string shareExpiryLimit);

    /// <summary>
    /// Delete Build. To delete a specified Build from the dashboard.
    /// </summary>
    /// <param name="buildId">Build ID that needs to be deleted</param>
    /// <returns>DeleteBuildResponse</returns>
    Task<MeasuredResponse<DeleteBuildResponse>> StatusIndAsync(int? buildId);
}
