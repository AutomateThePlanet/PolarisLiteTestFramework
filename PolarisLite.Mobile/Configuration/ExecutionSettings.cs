namespace PolarisLite.Mobile;

public class ExecutionSettings
{
    public string ExecutionType { get; set; }
    public string DefaultBrowser { get; set; }
    public string DefaultLifeCycle { get; set; }
    public string Url { get; set; }
    public string OptionsName { get; set; }
    public List<Dictionary<string, string>> Arguments { get; set; }
}