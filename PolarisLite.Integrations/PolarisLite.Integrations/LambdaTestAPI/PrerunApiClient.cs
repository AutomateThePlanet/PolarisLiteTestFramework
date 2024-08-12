using PolarisLite.API;
using RestSharp;

namespace PolarisLite.Integrations.LambdaTestAPI;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public class PrerunApiClient : IPrerunApiClient
{
    private readonly ApiClientService _apiClientService;

    /// <summary>
    /// Initializes a new instance of the <see cref="PrerunApiClient"/> class.
    /// </summary>
    /// <param name="apiClientService">An instance of ApiClientService (optional)</param>
    public PrerunApiClient(ApiClientService apiClientService = null)
    {
        _apiClientService = apiClientService ?? throw new ArgumentNullException(nameof(apiClientService));
    }

    /// <summary>
    /// Delete pre run from our lambda storage. This API deletes a pre run executable script from our lambda storage. Since pre run executable name should be unique, this API is useful if you want to re-upload your updated pre run script with the name same as the previous one.
    /// </summary>
    /// <param name="body">To delete a pre run executable</param>
    /// <returns>DeletePrerunResponse</returns>
    public async Task<MeasuredResponse<DeletePrerunResponse>> FilesDeleteDeleteAsync(DeletePrerunPayload body)
    {
        if (body == null)
        {
            throw new ApiException(400, "Missing required parameter 'body' when calling FilesDeleteDelete");
        }

        var request = new RestRequest("/files/delete", Method.Delete)
        {
            RequestFormat = DataFormat.Json
        };
        request.AddJsonBody(body);

        var response = await _apiClientService.DeleteAsync<DeletePrerunResponse>(request);
        return response;
    }

    /// <summary>
    /// Download pre run executable file.
    /// </summary>
    /// <param name="body">To download a pre run executable</param>
    /// <returns>byte[]</returns>
    public async Task<MeasuredResponse<byte[]>> FilesDownloadPutAsync(DownloadPrerunPayload body)
    {
        if (body == null)
        {
            throw new ApiException(400, "Missing required parameter 'body' when calling FilesDownloadPut");
        }

        var request = new RestRequest("/files/download", Method.Put)
        {
            RequestFormat = DataFormat.Json
        };
        request.AddJsonBody(body);

        var response = await _apiClientService.PutAsync<byte[]>(request);
        return response;
    }

    /// <summary>
    /// Check if the file is approved by Lambdatest. Once the pre run executable is successfully uploaded, LambdaTest will check the script and approve it after successful verification. This API will tell if the file is approved or not.
    /// </summary>
    /// <param name="body">To check if the file is approved by Lambdatest</param>
    /// <returns>ValidatePrerunResponse</returns>
    public async Task<MeasuredResponse<ValidatePrerunResponse>> FilesValidatePostAsync(ValidatePrerunPayload body)
    {
        if (body == null)
        {
            throw new ApiException(400, "Missing required parameter 'body' when calling FilesValidatePost");
        }

        var request = new RestRequest("/files/validate", Method.Post)
        {
            RequestFormat = DataFormat.Json
        };
        request.AddJsonBody(body);

        var response = await _apiClientService.PostAsync<ValidatePrerunResponse>(request);
        return response;
    }

    /// <summary>
    /// Fetch all pre run files uploaded by the user. This API fetches all the pre run executable which are uploaded to our lambda storage.
    /// </summary>
    /// <returns>ListPrerunFileResponse</returns>
    public async Task<MeasuredResponse<ListPrerunFileResponse>> ListFilesAsync()
    {
        var request = new RestRequest("/files", Method.Get);

        var response = await _apiClientService.GetAsync<ListPrerunFileResponse>(request);
        return response;
    }

    /// <summary>
    /// Upload pre run executable file to our lambda storage. In order to use pre run feature you first need to upload your relevant script files to our lambda storage. For every pre run action you need to upload 2 scripts (Pre run file and Post run file). Pre run file will be executed before starting the test and post run file will be executed after the test is completed. If you perform any changes in the test machine like changing host file, changing windows registry key, installing certificates, then your post run file should undo those changes.
    /// </summary>
    /// <param name="preRunFile"></param>
    /// <param name="name"></param>
    /// <param name="postRunFile"></param>
    /// <returns>CreatePrerunResponse</returns>
    public async Task<MeasuredResponse<CreatePrerunResponse>> UploadPrerunAsync(byte[] preRunFile, string name, byte[] postRunFile)
    {
        if (preRunFile == null)
        {
            throw new ApiException(400, "Missing required parameter 'preRunFile' when calling UploadPrerun");
        }
        if (string.IsNullOrEmpty(name))
        {
            throw new ApiException(400, "Missing required parameter 'name' when calling UploadPrerun");
        }
        if (postRunFile == null)
        {
            throw new ApiException(400, "Missing required parameter 'postRunFile' when calling UploadPrerun");
        }

        var request = new RestRequest("/files", Method.Post);
        request.AddFile("pre_run_file", preRunFile, "preRunFile.zip");
        request.AddParameter("name", name);
        request.AddFile("post_run_file", postRunFile, "postRunFile.zip");

        var response = await _apiClientService.PostAsync<CreatePrerunResponse>(request);
        return response;
    }
}