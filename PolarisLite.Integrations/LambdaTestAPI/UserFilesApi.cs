using PolarisLite.API;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace PolarisLite.Integrations.LambdaTestAPI;
/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public class UserFilesApi : LambdaTestApiClient, IUserFilesApiClient
{
    /// <summary>
    /// Fetch all user files uploaded by the user This API fetches all the user files which are uploaded to our lambda storage.
    /// </summary>
    /// <returns>Task<MeasuredResponse<ListUserFileResponse>></returns>
    public async Task<MeasuredResponse<ListUserFileResponse>> ListUserFilesAsync()
    {
        var request = new RestRequest("/user-files", Method.Get);
        var response = await _apiClientService.GetAsync<ListUserFileResponse>(request);
        return response;
    }

    /// <summary>
    /// Upload files to our lambda storage You can upload multiple files to our lambda storage. A maximum of 150 files can be uploaded per organization. We have a limit of 20 MB files size per API. So if your total file sizes reach the limit, please upload your files in multiple API calls.
    /// </summary>
    /// <param name="files"></param>
    /// <returns>Task<MeasuredResponse<UploadUserFilesResponseData>></returns>
    public async Task<MeasuredResponse<UploadUserFilesResponseData>> UploadUserFilesAsync(byte[] files)
    {
        if (files == null)
        {
            throw new ApiException(400, "Missing required parameter 'files' when calling UploadUserFiles");
        }

        var request = new RestRequest("/user-files", Method.Post);
        request.AddFile("files", files, "userfiles.zip");

        var response = await _apiClientService.PostAsync<UploadUserFilesResponseData>(request);
        return response;
    }

    /// <summary>
    /// Delete user files from our lambda storage This API deletes user file from lambda storage.
    /// </summary>
    /// <param name="body">To delete a user file</param>
    /// <returns>Task<MeasuredResponse<DeleteUserFileResponse>></returns>
    public async Task<MeasuredResponse<DeleteUserFileResponse>> UserFilesDeleteDeleteAsync(DeleteUserFilePayload body)
    {
        if (body == null)
        {
            throw new ApiException(400, "Missing required parameter 'body' when calling UserFilesDeleteDelete");
        }

        var request = new RestRequest("/user-files/delete", Method.Delete)
        {
            RequestFormat = DataFormat.Json
        };
        request.AddJsonBody(body);

        var response = await _apiClientService.DeleteAsync<DeleteUserFileResponse>(request);
        return response;
    }

    /// <summary>
    /// Download user file from lambda storage. 
    /// </summary>
    /// <param name="body">To download a user file</param>
    /// <returns>Task<MeasuredResponse<byte[]>></returns>
    public async Task<MeasuredResponse<byte[]>> UserFilesDownloadPutAsync(DownloadUserFilePayload body)
    {
        if (body == null)
        {
            throw new ApiException(400, "Missing required parameter 'body' when calling UserFilesDownloadPut");
        }

        var request = new RestRequest("/user-files/download", Method.Put)
        {
            RequestFormat = DataFormat.Json
        };
        request.AddJsonBody(body);

        var response = await _apiClientService.PutAsync<byte[]>(request);
        return response;
    }
}