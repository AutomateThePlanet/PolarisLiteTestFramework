using PolarisLite.API;

namespace DemoSystemTests;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public interface IPrerunApiClient
{
    /// <summary>
    /// Delete pre run from our lambda storage. This API deletes a pre run executable script from our lambda storage. Since pre run executable name should be unique, this API is useful if you want to re-upload your updated pre run script with the name same as the previous one.
    /// </summary>
    /// <param name="body">To delete a pre run executable</param>
    /// <returns>DeletePrerunResponse</returns>
    Task<MeasuredResponse<DeletePrerunResponse>> FilesDeleteDeleteAsync(DeletePrerunPayload body);

    /// <summary>
    /// Download pre run executable file.
    /// </summary>
    /// <param name="body">To download a pre run executable</param>
    /// <returns>byte[]</returns>
    Task<MeasuredResponse<byte[]>> FilesDownloadPutAsync(DownloadPrerunPayload body);

    /// <summary>
    /// Check if the file is approved by Lambdatest. Once the pre run executable is successfully uploaded, LambdaTest will check the script and approve it after successful verification. This API will tell if the file is approved or not.
    /// </summary>
    /// <param name="body">To check if the file is approved by Lambdatest</param>
    /// <returns>ValidatePrerunResponse</returns>
    Task<MeasuredResponse<ValidatePrerunResponse>> FilesValidatePostAsync(ValidatePrerunPayload body);

    /// <summary>
    /// Fetch all pre run files uploaded by the user. This API fetches all the pre run executable which are uploaded to our lambda storage.
    /// </summary>
    /// <returns>ListPrerunFileResponse</returns>
    Task<MeasuredResponse<ListPrerunFileResponse>> ListFilesAsync();

    /// <summary>
    /// Upload pre run executable file to our lambda storage. In order to use pre run feature you first need to upload your relevant script files to our lambda storage. For every pre run action you need to upload 2 scripts (Pre run file and Post run file). Pre run file will be executed before starting the test and post run file will be executed after the test is completed. If you perform any changes in the test machine like changing host file, changing windows registry key, installing certificates, then your post run file should undo those changes.
    /// </summary>
    /// <param name="preRunFile"></param>
    /// <param name="name"></param>
    /// <param name="postRunFile"></param>
    /// <returns>CreatePrerunResponse</returns>
    Task<MeasuredResponse<CreatePrerunResponse>> UploadPrerunAsync(byte[] preRunFile, string name, byte[] postRunFile);
}
