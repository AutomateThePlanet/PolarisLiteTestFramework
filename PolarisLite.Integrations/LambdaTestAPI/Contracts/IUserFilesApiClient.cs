using PolarisLite.API;

namespace PolarisLite.Integrations;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public interface IUserFilesApiClient
{
    /// <summary>
    /// Fetch all user files uploaded by the user This API fetches all the user files which are uploaded to our lambda storage.
    /// </summary>
    /// <returns>Task<MeasuredResponse<ListUserFileResponse>></returns>
    Task<MeasuredResponse<ListUserFileResponse>> ListUserFilesAsync();

    /// <summary>
    /// Upload files to our lambda storage You can upload multiple files to our lambda storage. A maximum of 150 files can be uploaded per organization. We have a limit of 20 MB files size per API. So if your total file sizes reach the limit, please upload your files in multiple API calls.
    /// </summary>
    /// <param name="files"></param>
    /// <returns>Task<MeasuredResponse<UploadUserFilesResponseData>></returns>
    Task<MeasuredResponse<UploadUserFilesResponseData>> UploadUserFilesAsync(byte[] files);

    /// <summary>
    /// Delete user files from our lambda storage This API deletes user file from lambda storage.
    /// </summary>
    /// <param name="body">To delete a user file</param>
    /// <returns>Task<MeasuredResponse<DeleteUserFileResponse>></returns>
    Task<MeasuredResponse<DeleteUserFileResponse>> UserFilesDeleteDeleteAsync(DeleteUserFilePayload body);

    /// <summary>
    /// Download user file from lambda storage. 
    /// </summary>
    /// <param name="body">To download a user file</param>
    /// <returns>Task<MeasuredResponse<byte[]>></returns>
    Task<MeasuredResponse<byte[]>> UserFilesDownloadPutAsync(DownloadUserFilePayload body);
}
