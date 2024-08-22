using PolarisLite.API;
using RestSharp;

namespace PolarisLite.Integrations.LambdaTestAPI;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public class TestApiClient : LambdaTestApiClient, ITestApiClient
{
    /// <summary>
    /// Fetch recorded video of a test id. To fetch video of a recorded test.
    /// </summary>
    /// <param name="testId">Test ID</param>
    /// <param name="videoGeneratedStatus">Video generated status</param>
    /// <returns>VideoResponse</returns>
    public async Task<MeasuredResponse<VideoResponse>> TestTestIdVideoGetAsync(string testId, bool? videoGeneratedStatus)
    {
        if (string.IsNullOrEmpty(testId))
        {
            throw new ApiException(400, "Missing required parameter 'testId' when calling TestTestIdVideoGet");
        }

        var request = new RestRequest("/test/{test_id}/video", Method.Get);
        request.AddUrlSegment("test_id", testId);

        if (videoGeneratedStatus.HasValue)
        {
            request.AddQueryParameter("video_generated_status", videoGeneratedStatus.ToString());
        }

        var response = await _ApiClientAdapter.GetAsync<VideoResponse>(request);
        return response;
    }

    /// <summary>
    /// Upload assertion logs to our lambda storage. You can upload assertion logs or other logs for a test Id. 
    /// The logs uploaded can then be viewed in the automation dashboard page under EXCEPTION sections. 
    /// You can only upload a list of strings.
    /// </summary>
    /// <param name="body">To upload exception log for a given test Id</param>
    /// <param name="testId">Test ID</param>
    /// <returns>UploadTerminalFileResposeData</returns>
    public async Task<MeasuredResponse<UploadTerminalFileResposeData>> UploadTestExceptionLogsAsync(UploadExceptionLog body, string testId)
    {
        if (body == null)
        {
            throw new ApiException(400, "Missing required parameter 'body' when calling UploadTestExceptionLogs");
        }

        if (string.IsNullOrEmpty(testId))
        {
            throw new ApiException(400, "Missing required parameter 'testId' when calling UploadTestExceptionLogs");
        }

        var request = new RestRequest("/tests/{test_id}/exceptions", Method.Post);
        request.AddUrlSegment("test_id", testId);
        request.AddJsonBody(body);

        var response = await _ApiClientAdapter.PostAsync<UploadTerminalFileResposeData>(request);
        return response;
    }
}
