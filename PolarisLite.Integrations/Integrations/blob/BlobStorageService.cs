using Microsoft.WindowsAzure.Storage;
using PolarisLite.Core.Settings.StaticSettings;
using PolarisLite.Integrations.Settings;
using PolarisLite.Logging;

namespace PolarisLite.Integrations;

public class BlobStorageService
{
    private string _connectionString;

    public BlobStorageService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public BlobStorageService()
    {
        _connectionString = IntegrationSettings.BlobStorageSettings.ConnectionString;
    }

    public void DownloadFile(string fileName, string downloadFilePath, string blobContainerName)
    {
        var storageAccount = CloudStorageAccount.Parse(_connectionString);
        var blobClient = storageAccount.CreateCloudBlobClient();
        var container = blobClient.GetContainerReference(blobContainerName);
        var pageBlob = container.GetBlobReference(fileName);

        try
        {
            pageBlob.DownloadToFileAsync(downloadFilePath, FileMode.CreateNew).Wait();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.ToString());
        }
    }

    public string GetFileUrl(string fileName, string blobContainerName)
    {
        var storageAccount = CloudStorageAccount.Parse(_connectionString);
        var blobClient = storageAccount.CreateCloudBlobClient();
        var container = blobClient.GetContainerReference(blobContainerName);
        var pageBlob = container.GetBlobReference(fileName);

        return pageBlob.Uri.AbsoluteUri;
    }

    public bool CheckIfFileExists(string fileName, string blobContainerName)
    {
        var storageAccount = CloudStorageAccount.Parse(_connectionString);
        var blobClient = storageAccount.CreateCloudBlobClient();
        var container = blobClient.GetContainerReference(blobContainerName);
        var pageBlob = container.GetBlobReference(fileName);

        return pageBlob.ExistsAsync().Result;
    }

    public string UploadFile(string fileName, string filePath, string blobContainerName, string contentType)
    {
        var storageAccount = CloudStorageAccount.Parse(_connectionString);
        var blobClient = storageAccount.CreateCloudBlobClient();
        var container = blobClient.GetContainerReference(blobContainerName);
        var pageBlob = container.GetBlockBlobReference(fileName);
        pageBlob.Properties.ContentType = contentType;
        pageBlob.UploadFromFileAsync(filePath).Wait();

        return pageBlob.Uri.AbsoluteUri;
    }

    public string UploadFile(string fileName, byte[] fileContent, string blobContainerName, string contentType)
    {
        var storageAccount = CloudStorageAccount.Parse(_connectionString);
        var blobClient = storageAccount.CreateCloudBlobClient();
        var container = blobClient.GetContainerReference(blobContainerName);
        var pageBlob = container.GetBlockBlobReference(fileName);
        pageBlob.Properties.ContentType = contentType;
        pageBlob.UploadFromByteArrayAsync(fileContent, 0, fileContent.Length - 1).Wait();

        return pageBlob.Uri.AbsoluteUri;
    }
}
