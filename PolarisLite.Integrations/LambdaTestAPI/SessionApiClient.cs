using PolarisLite.API;
using RestSharp;

namespace PolarisLite.Integrations.LambdaTestAPI;
/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public class SessionApiClient : LambdaTestApiClient, ISessionApiClient
{
    public async Task<MeasuredResponse<LogResponse>> SessionAsync(string sessionId)
    {
        ValidateSessionId(sessionId);
        var request = new RestRequest($"/sessions/{sessionId}/log/command", Method.Get);
        return await _apiClientService.GetAsync<LogResponse>(request);
    }

    public async Task<MeasuredResponse<SeleniumLogResponse>> SessionSeleniumLogAsync(string sessionId)
    {
        ValidateSessionId(sessionId);
        var request = new RestRequest($"/sessions/{sessionId}/log/selenium", Method.Get);
        return await _apiClientService.GetAsync<SeleniumLogResponse>(request);
    }

    public async Task<MeasuredResponse<SeleniumLogResponse>> SessionNetworkLogAsync(string sessionId)
    {
        ValidateSessionId(sessionId);
        var request = new RestRequest($"/sessions/{sessionId}/log/network", Method.Get);
        return await _apiClientService.GetAsync<SeleniumLogResponse>(request);
    }

    public async Task<MeasuredResponse<SeleniumLogResponse>> SessionConsoleLogAsync(string sessionId)
    {
        ValidateSessionId(sessionId);
        var request = new RestRequest($"/sessions/{sessionId}/log/console", Method.Get);
        return await _apiClientService.GetAsync<SeleniumLogResponse>(request);
    }

    public async Task<MeasuredResponse<SeleniumHarLogResponse>> SessionHarLogAsync(string sessionId)
    {
        ValidateSessionId(sessionId);
        var request = new RestRequest($"/sessions/{sessionId}/log/network.har", Method.Get);
        return await _apiClientService.GetAsync<SeleniumHarLogResponse>(request);
    }

    public async Task<MeasuredResponse<ListsTestsResponse>> SessionsAsync(int? buildId, string username, int? offset, int? limit, string status, string fromdate, string todate, string sort, string tags)
    {
        var request = new RestRequest("/sessions", Method.Get);

        if (buildId.HasValue)
        {
            request.AddQueryParameter("build_id", buildId.Value.ToString());
        }

        if (!string.IsNullOrEmpty(username))
        {
            request.AddQueryParameter("username", username);
        }

        if (offset.HasValue)
        {
            request.AddQueryParameter("offset", offset.Value.ToString());
        }

        if (limit.HasValue)
        {
            request.AddQueryParameter("limit", limit.Value.ToString());
        }

        if (!string.IsNullOrEmpty(status))
        {
            request.AddQueryParameter("status", status);
        }

        if (!string.IsNullOrEmpty(fromdate))
        {
            request.AddQueryParameter("fromdate", fromdate);
        }

        if (!string.IsNullOrEmpty(todate))
        {
            request.AddQueryParameter("todate", todate);
        }

        if (!string.IsNullOrEmpty(sort))
        {
            request.AddQueryParameter("sort", sort);
        }

        if (!string.IsNullOrEmpty(tags))
        {
            request.AddQueryParameter("tags", tags);
        }

        return await _apiClientService.GetAsync<ListsTestsResponse>(request);
    }

    public async Task<MeasuredResponse<SessionDeleteSuccess>> DeleteSessionAsync(string sessionId)
    {
        ValidateSessionId(sessionId);
        var request = new RestRequest($"/sessions/{sessionId}", Method.Delete);
        return await _apiClientService.DeleteAsync<SessionDeleteSuccess>(request);
    }

    public async Task<MeasuredResponse<Session>> GetSessionDetailsAsync(string sessionId, string shareExpiryLimit)
    {
        ValidateSessionId(sessionId);
        var request = new RestRequest($"/sessions/{sessionId}", Method.Get);

        if (!string.IsNullOrEmpty(shareExpiryLimit))
        {
            request.AddQueryParameter("shareExpiryLimit", shareExpiryLimit);
        }

        return await _apiClientService.GetAsync<Session>(request);
    }

    public async Task<MeasuredResponse<SessionUpdateSuccess>> UpdateSessionAsync(UpdateSessionPayload body, string sessionId)
    {
        ValidateSessionId(sessionId);
        ValidateRequestBody(body);
        var request = new RestRequest($"/sessions/{sessionId}", Method.Patch);
        request.AddJsonBody(body);
        return await _apiClientService.PatchAsync<SessionUpdateSuccess>(request);
    }

    public async Task<MeasuredResponse<ScreenshotResponse>> GetSessionScreenshotsAsync(string sessionId)
    {
        ValidateSessionId(sessionId);
        var request = new RestRequest($"/sessions/{sessionId}/screenshots", Method.Get);
        return await _apiClientService.GetAsync<ScreenshotResponse>(request);
    }

    public async Task<MeasuredResponse<StopSessionResponse>> StopSessionAsync(string sessionId)
    {
        ValidateSessionId(sessionId);
        var request = new RestRequest($"/sessions/{sessionId}/stop", Method.Put);
        return await _apiClientService.PutAsync<StopSessionResponse>(request);
    }

    public async Task<MeasuredResponse<VideoResponse>> GetSessionVideoAsync(string sessionId, bool? videoGeneratedStatus)
    {
        ValidateSessionId(sessionId);
        var request = new RestRequest($"/sessions/{sessionId}/video", Method.Get);

        if (videoGeneratedStatus.HasValue)
        {
            request.AddQueryParameter("video_generated_status", videoGeneratedStatus.Value.ToString());
        }

        return await _apiClientService.GetAsync<VideoResponse>(request);
    }

    public async Task<MeasuredResponse<UploadTerminalFileResposeData>> UploadSessionExceptionLogsAsync(UploadExceptionLog body, string sessionId)
    {
        ValidateSessionId(sessionId);
        ValidateRequestBody(body);
        var request = new RestRequest($"/sessions/{sessionId}/exceptions", Method.Post);
        request.AddJsonBody(body);
        return await _apiClientService.PostAsync<UploadTerminalFileResposeData>(request);
    }

    public async Task<MeasuredResponse<UploadTerminalFileResposeData>> UploadTerminalLogsAsync(byte[] file, string sessionId)
    {
        ValidateSessionId(sessionId);
        ValidateFile(file);
        var request = new RestRequest($"/sessions/{sessionId}/terminal-logs", Method.Post);
        request.AddFile("file", file, "terminal-log.txt");
        return await _apiClientService.PostAsync<UploadTerminalFileResposeData>(request);
    }

    private void ValidateSessionId(string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId))
        {
            throw new ApiException(400, "Missing required parameter 'sessionId'");
        }
    }

    private void ValidateRequestBody(object body)
    {
        if (body == null)
        {
            throw new ApiException(400, "Missing required parameter 'body'");
        }
    }

    private void ValidateFile(byte[] file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ApiException(400, "Missing or empty required parameter 'file'");
        }
    }
}
