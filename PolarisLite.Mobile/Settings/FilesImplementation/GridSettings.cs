namespace PolarisLite.Mobile;

public class GridSettings
{
    public string ProviderName { get; set; }
    public string OptionsName { get; set; }
    public string Url { get; set; }
    public Dictionary<string, object> Arguments { get; set; } = new Dictionary<string, object>();
    public List<string> Tags { get; set; } = new List<string>();
}