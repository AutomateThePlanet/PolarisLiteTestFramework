namespace PolarisLite.Integrations;

/// <summary>
/// Represents the response data for the UploadUserFiles API.
/// </summary>
public class UploadUserFilesResponseData
{
    /// <summary>
    /// Gets or sets the status of the upload operation.
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the message returned from the API.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the list of uploaded file names.
    /// </summary>
    public List<string> UploadedFiles { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the files were uploaded.
    /// </summary>
    public DateTime UploadedAt { get; set; }

    /// <summary>
    /// Gets or sets any error message related to the upload operation.
    /// </summary>
    public string ErrorMessage { get; set; }

    public UploadUserFilesResponseData()
    {
        UploadedFiles = new List<string>();
    }
}
