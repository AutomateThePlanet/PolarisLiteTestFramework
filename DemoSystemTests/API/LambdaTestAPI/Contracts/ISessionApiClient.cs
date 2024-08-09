using PolarisLite.API;

namespace DemoSystemTests;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public interface ISessionApiClient
{
    Task<MeasuredResponse<LogResponse>> SessionAsync(string sessionId);
    Task<MeasuredResponse<SeleniumLogResponse>> SessionSeleniumLogAsync(string sessionId);
    Task<MeasuredResponse<SeleniumLogResponse>> SessionNetworkLogAsync(string sessionId);
    Task<MeasuredResponse<SeleniumLogResponse>> SessionConsoleLogAsync(string sessionId);
    Task<MeasuredResponse<SeleniumHarLogResponse>> SessionHarLogAsync(string sessionId);
    Task<MeasuredResponse<ListsTestsResponse>> SessionsAsync(int? buildId, string username, int? offset, int? limit, string status, string fromdate, string todate, string sort, string tags);
    Task<MeasuredResponse<SessionDeleteSuccess>> DeleteSessionAsync(string sessionId);
    Task<MeasuredResponse<Session>> GetSessionDetailsAsync(string sessionId, string shareExpiryLimit);
    Task<MeasuredResponse<SessionUpdateSuccess>> UpdateSessionAsync(UpdateSessionPayload body, string sessionId);
    Task<MeasuredResponse<ScreenshotResponse>> GetSessionScreenshotsAsync(string sessionId);
    Task<MeasuredResponse<StopSessionResponse>> StopSessionAsync(string sessionId);
    Task<MeasuredResponse<VideoResponse>> GetSessionVideoAsync(string sessionId, bool? videoGeneratedStatus);
    Task<MeasuredResponse<UploadTerminalFileResposeData>> UploadSessionExceptionLogsAsync(UploadExceptionLog body, string sessionId);
    Task<MeasuredResponse<UploadTerminalFileResposeData>> UploadTerminalLogsAsync(byte[] file, string sessionId);
}
