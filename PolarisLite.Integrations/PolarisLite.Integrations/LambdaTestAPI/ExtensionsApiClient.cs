using PolarisLite.API;
using RestSharp;

namespace PolarisLite.Integrations.LambdaTestAPI;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public class ExtensionsApiClient : IExtensionsClientApi
{
    private readonly ApiClientService _apiClientService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExtensionsApiClient"/> class.
    /// </summary>
    /// <param name="apiClientService"> an instance of ApiClientService (optional)</param>
    /// <returns></returns>
    public ExtensionsApiClient(ApiClientService apiClientService = null)
    {
        _apiClientService = apiClientService ?? throw new ArgumentNullException(nameof(apiClientService));
    }

    /// <summary>
    /// Delete extension from our lambda storage This API deletes extension from lambda storage
    /// </summary>
    /// <param name="body">To delete a extension</param>
    /// <returns>DeleteExtensionResponse</returns>
    public async Task<MeasuredResponse<DeleteExtensionResponse>> FilesExtensionsDeleteDeleteAsync(DeleteExtensionPayload body)
    {
        if (body == null)
        {
            throw new ApiException(400, "Missing required parameter 'body' when calling FilesExtensionsDeleteDelete");
        }

        var request = new RestRequest("/files/extensions/delete", Method.Delete)
        {
            RequestFormat = DataFormat.Json
        };
        request.AddJsonBody(body);

        var response = await _apiClientService.DeleteAsync<DeleteExtensionResponse>(request);
        return response;
    }

    /// <summary>
    /// Fetch all extensions uploaded by the user This API fetches all the extensions which are uploaded to our lambda storage.
    /// </summary>
    /// <returns>ListExtensionResponse</returns>
    public async Task<MeasuredResponse<ListExtensionResponse>> ListExtensionAsync()
    {
        var request = new RestRequest("/files/extensions", Method.Get);

        var response = await _apiClientService.GetAsync<ListExtensionResponse>(request);
        return response;
    }

    /// <summary>
    /// Upload extensions in zip format to our lambda storage 
    /// </summary>
    /// <param name="extensions"></param>
    /// <returns>UploadExtensionResponseData</returns>
    public async Task<MeasuredResponse<UploadExtensionResponseData>> UploadExtensionsAsync(byte[] extensions)
    {
        if (extensions == null)
        {
            throw new ApiException(400, "Missing required parameter 'extensions' when calling UploadExtensions");
        }

        var request = new RestRequest("/files/extensions", Method.Post);
        request.AddFile("extensions", extensions, "extensions.zip");

        var response = await _apiClientService.PostAsync<UploadExtensionResponseData>(request);
        return response;
    }
}