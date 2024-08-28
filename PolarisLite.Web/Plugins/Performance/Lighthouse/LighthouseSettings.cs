namespace PolarisLite.Web.Plugins;

public class LighthouseSettings
{
    public List<Dictionary<string, string>> Arguments { get; set; }
    public int Timeout { get; set; }
    public bool IsEnabled { get; set; }
}