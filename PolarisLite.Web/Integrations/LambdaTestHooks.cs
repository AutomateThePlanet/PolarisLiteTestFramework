using PolarisLite.Web.Services;

namespace PolarisLite.Web.Integrations;
public static class LambdaTestHooks
{
    private static readonly DriverAdapter _driverAdapter;

    static LambdaTestHooks()
    {
        _driverAdapter = new DriverAdapter();
    }

    // Set the status of the test
    public static void SetLambdaStatus(LambdaStatus status)
    {
        _driverAdapter.Execute($"lambda-status={status.GetDescription()}");
    }

    // Check if a file exists on the test machine
    public static void CheckFileExists(string fileName)
    {
        _driverAdapter.Execute($"lambda-file-exists={fileName}");
    }

    // Retrieve file metadata
    public static void GetFileStats(string fileName)
    {
        _driverAdapter.Execute($"lambda-file-stats={fileName}");
    }

    // Download file content using base64 encoding
    public static void GetFileContent(string fileName)
    {
        _driverAdapter.Execute($"lambda-file-content={fileName}");
    }

    // List files in the download directory
    public static void ListFiles(string matchString)
    {
        _driverAdapter.Execute($"lambda-file-list={matchString}");
    }

    // Change the test name
    public static void UpdateTestName(string testName)
    {
        _driverAdapter.Execute($"lambda-name={testName}");
    }

    // Update the build name
    public static void UpdateBuildName(string buildName)
    {
        _driverAdapter.Execute($"lambda-build={buildName}");
    }

    // Mark test as passed/failed with reason
    public static void SetActionStatus(string status, string reason)
    {
        var action = new { status, reason };
        _driverAdapter.Execute("lambda-action", action);
    }

    // Perform keyboard events
    public static void PerformKeyboardEvent(string eventCommand)
    {
        _driverAdapter.Execute($"lambda-perform-keyboard-events:{eventCommand}");
    }

    // Abort test execution for live interaction
    public static void Breakpoint()
    {
        _driverAdapter.Execute("lambda-breakpoint=true");
    }

    // Capture an asynchronous screenshot
    public static void CaptureScreenshot()
    {
        _driverAdapter.Execute("lambda-screenshot=true");
    }

    // Delete files in the download directory
    public static void DeleteFiles(params string[] files)
    {
        var filesList = string.Join(",", files);
        _driverAdapter.Execute($"lambda-files-delete={filesList}");
    }

    // Throttle network speed during test execution
    public static void ThrottleNetwork(string profile)
    {
        _driverAdapter.Execute("lambda-throttle-network", profile);
    }

    // Fetch the IPs of the domain
    public static void PingDomain(string domain)
    {
        _driverAdapter.Execute($"lambda-ping={domain}");
    }

    // Upload captured exceptions
    public static void UploadExceptions(params string[] exceptions)
    {
        _driverAdapter.Execute("lambda-exceptions", exceptions);
    }

    // Print clipboard data
    public static void GetClipboard()
    {
        _driverAdapter.Execute("lambda-get-clipboard");
    }

    // Set clipboard data
    public static void SetClipboard(string data)
    {
        _driverAdapter.Execute($"lambda-set-clipboard={data}");
    }

    // Clear clipboard data
    public static void ClearClipboard()
    {
        _driverAdapter.Execute("lambda-clear-clipboard");
    }

    // Fetch the IPs from the outbound domain
    public static void UnboundPing(string domain)
    {
        _driverAdapter.Execute($"lambda-unbound-ping={domain}");
    }

    // Fetch network log entries
    public static void FetchNetworkLog(string range = "all")
    {
        _driverAdapter.Execute($"lambda:network={range}");
    }

    // Update test name during test execution
    public static void UpdateNameDuringExecution(string testName)
    {
        _driverAdapter.Execute($"lambdaUpdateName={testName}");
    }

    public static string GenerateLighthouseReport()
    {
        return (string)_driverAdapter.Execute($"lambdatest_executor: {{\"action\": \"generateLighthouseReport\", \"arguments\": {{\"url\": \"{_driverAdapter.Url}\"}}");
    }
}