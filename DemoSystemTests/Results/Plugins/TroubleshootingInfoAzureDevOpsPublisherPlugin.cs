using OpenQA.Selenium.Remote;
using PolarisLite.API;
using PolarisLite.Core;
using PolarisLite.Core.Plugins;
using PolarisLite.Web.Integrations;
using PolarisLite.Web.Plugins;
using RestSharp;
using System.IO.Compression;
using System.Reflection;

namespace DemoSystemTests.Results.Plugins;
public class TroubleshootingInfoAzureDevOpsPublisherPlugin : Plugin
{
    public override void OnAfterTestCleanup(TestOutcome result, MethodInfo memberInfo, Exception failedTestException)
    {
        var executionType = DriverFactory.ExecutionType;
        bool isLambdaTestRun = executionType.Equals(ExecutionType.LambdaTest);

        if (isLambdaTestRun)
        {
            var sessionId = DriverFactory.CurrentSessionId;

            var apiClient = new ApiClientAdapter();
            var sessionApiClient = new SessionApiClient();

            var videoResponse = sessionApiClient.GetSessionVideoAsync(sessionId, true).Result;
            var request = new RestRequest(videoResponse.Data.Url, Method.Get);
            var downloadedVideoResponse = apiClient.DownloadAsync(request).Result;

            var videoFileName = $"{memberInfo.DeclaringType.Name}.{memberInfo.Name}.{DateTime.Now:MMMMddyyyyhhmmss}.mp4";
            var videoFilePath = Path.Combine(Path.GetTempPath(), videoFileName);

            // Save the video to a temporary location
            File.WriteAllBytes(videoFilePath, downloadedVideoResponse);

            // Add video attachment to Allure and NUnit TestContext
            TestContext.AddTestAttachment(videoFilePath);

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

                // Upload each screenshot to Blob Storage and add it to Allure and NUnit TestContext
                var screenshotFiles = Directory.GetFiles(extractFolderPath);
                foreach (var screenshotFilePath in screenshotFiles)
                {
                    var screenshotFileName = $"{memberInfo.DeclaringType.Name}.{memberInfo.Name}.{Path.GetFileNameWithoutExtension(screenshotFilePath)}_{DateTime.Now:MMMMddyyyyhhmmss}.png";
                    // Add screenshot attachment to Allure and NUnit TestContext
                    TestContext.AddTestAttachment(screenshotFilePath);
                }

                // Clean up temporary files
                File.Delete(tempZipFilePath);
                Directory.Delete(extractFolderPath, true);
            }

            // Clean up video file
            File.Delete(videoFilePath);
        }
    }
}
