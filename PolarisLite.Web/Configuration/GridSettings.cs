namespace PolarisLite.Web;

public class GridSettings
{
    public string ProviderName { get; set; }
    public string OptionsName { get; set; }
    public string Url { get; set; }
    public List<Dictionary<string, object>> Arguments { get; set; }
}