using PolarisLite.API;

namespace PolarisLite.Integrations;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public interface IExtensionsClientApi
{
    /// <summary>
    /// Delete extension from our lambda storage This API deletes extension from lambda storage
    /// </summary>
    /// <param name="body">To delete a extension</param>
    /// <returns>DeleteExtensionResponse</returns>
    Task<MeasuredResponse<DeleteExtensionResponse>> FilesExtensionsDeleteDeleteAsync(DeleteExtensionPayload body);

    /// <summary>
    /// Fetch all extensions uploaded by the user This API fetches all the extensions which are uploaded to our lambda storage.
    /// </summary>
    /// <returns>ListExtensionResponse</returns>
    Task<MeasuredResponse<ListExtensionResponse>> ListExtensionAsync();

    /// <summary>
    /// Upload extensions in zip format to our lambda storage 
    /// </summary>
    /// <param name="extensions"></param>
    /// <returns>UploadExtensionResponseData</returns>
    Task<MeasuredResponse<UploadExtensionResponseData>> UploadExtensionsAsync(byte[] extensions);
}
