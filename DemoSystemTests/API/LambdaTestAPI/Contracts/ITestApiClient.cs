using PolarisLite.API;

namespace DemoSystemTests;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public interface ITestApiClient
{
    /// <summary>
    /// Fetch recorded video of a test id. To fetch video of a recorded test.
    /// </summary>
    /// <param name="testId">Test ID</param>
    /// <param name="videoGeneratedStatus">Video generated status</param>
    /// <returns>VideoResponse</returns>
    Task<MeasuredResponse<VideoResponse>> TestTestIdVideoGetAsync(string testId, bool? videoGeneratedStatus);

    /// <summary>
    /// Upload assertion logs to our lambda storage. You can upload assertion logs or other logs for a test Id. 
    /// The logs uploaded can then be viewed in the automation dashboard page under EXCEPTION sections. 
    /// You can only upload a list of strings.
    /// </summary>
    /// <param name="body">To upload exception log for a given test Id</param>
    /// <param name="testId">Test ID</param>
    /// <returns>UploadTerminalFileResposeData</returns>
    Task<MeasuredResponse<UploadTerminalFileResposeData>> UploadTestExceptionLogsAsync(UploadExceptionLog body, string testId);
}
