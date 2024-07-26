namespace PolarisLite.Web;

public class GridSettings
{
    public string ProviderName { get; set; } = "lambda test";
    public string OptionsName { get; set; } = "LT:Options";
    public string Url { get; set; } = "https://{userName}:{accessKey}@hub.lambdatest.com/wd/hub";
    public Dictionary<string, object> Arguments { get; set; } = new Dictionary<string, object>
        {
            { "resolution", "1280x800" },
            { "platform", "Windows 10" },
            { "visual", "true" },
            { "video", "true" },
            { "build", "1.2" },
            { "project", "POLARIS_RUN" },
            { "selenium_version", "4.21.0" }
        };
}