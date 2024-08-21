using Allure.Net.Commons;
using OpenQA.Selenium.Remote;
using PolarisLite.API;
using PolarisLite.Core;
using PolarisLite.Core.Plugins;
using PolarisLite.Integrations;
using PolarisLite.Web.Configuration.StaticImplementation;
using PolarisLite.Web.Integrations;
using PolarisLite.Web.Plugins;
using RestSharp;
using RestSharp.Authenticators;
using System.IO.Compression;
using System.Reflection;
using System.Text;

namespace DemoSystemTests.Integrations.Plugins.Blob;
public class TroubleshootingInfoAllurePublisherPlugin : Plugin
{
    public override void OnAfterTestCleanup(TestOutcome result, MethodInfo memberInfo, Exception failedTestException)
    {
        var executionType = DriverFactory.ExecutionType;
        bool isLambdaTestRun = executionType.Equals(ExecutionType.LambdaTest);

        if (isLambdaTestRun)
        {
            var sessionId = ((RemoteWebDriver)DriverFactory.WrappedDriver).SessionId.ToString();

            var apiClient = new ApiClientAdapter();
            var sessionApiClient = new SessionApiClient();

            var videoResponse = sessionApiClient.GetSessionVideoAsync(sessionId, true).Result;
            var request = new RestRequest(videoResponse.Data.Url, Method.Get);
            var downloadedVideoResponse = apiClient.DownloadAsync(request).Result;

            //var blobStorage = new BlobStorageService();
            var fileName = $"{memberInfo.DeclaringType.Name}.{memberInfo.Name}.{DateTime.Now:MMMMddyyyyhhmmss}.mp4";
            //var fileUrl = blobStorage.UploadFile(fileName, downloadedVideoResponse, "videos", "video/mp4");

            AllureApi.AddAttachment(fileName, "video/mp4", downloadedVideoResponse, ".mp4");
            if (result != TestOutcome.Passed)
            {
                LambdaTestHooks.CaptureScreenshot();

                // Download the ZIP file containing screenshots
                var screenshotsResponse = sessionApiClient.GetSessionScreenshotsAsync(sessionId).Result;
                var screenshotsRequest = new RestRequest(screenshotsResponse.Data.Url, Method.Get);
                var downloadedScreenshotsZip = apiClient.DownloadAsync(screenshotsRequest).Result;

                // Save the ZIP file to a temporary location
                var tempZipFilePath = Path.GetTempFileName();
                File.WriteAllBytes(tempZipFilePath, downloadedScreenshotsZip);

                // Extract the screenshots from the ZIP file
                var extractFolderPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                Directory.CreateDirectory(extractFolderPath);
                ZipFile.ExtractToDirectory(tempZipFilePath, extractFolderPath);

                // Upload each screenshot to Blob Storage
                var screenshotFiles = Directory.GetFiles(extractFolderPath);
                foreach (var screenshotFilePath in screenshotFiles)
                {
                    var screenshotFileName = $"{memberInfo.DeclaringType.Name}.{memberInfo.Name}.{Path.GetFileNameWithoutExtension(screenshotFilePath)}_{DateTime.Now:MMMMddyyyyhhmmss}.png";
                    //var screenshotFileUrl = blobStorage.UploadFile(screenshotFileName, screenshotFilePath, "screenshots", "image/png");

                    AllureApi.AddAttachment(screenshotFileName, "image/png", screenshotFilePath);
                    // OR add link to blob storage + LambdaTest
                }

                // Clean up temporary files
                File.Delete(tempZipFilePath);
                Directory.Delete(extractFolderPath, true);
            }
        }
    }
}
