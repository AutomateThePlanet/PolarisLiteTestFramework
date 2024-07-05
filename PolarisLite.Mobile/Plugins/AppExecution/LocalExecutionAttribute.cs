namespace PolarisLite.Mobile.Plugins;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public sealed class LocalExecutionAttribute : Attribute
{
    public Lifecycle Lifecycle { get; set; } = Lifecycle.RestartEveryTime;
    public ExecutionType ExecutionType { get; set; } = ExecutionType.Local;
    public string AndroidVersion { get; set; } = string.Empty;
    public bool IsMobileWebTest { get; set; } = false;
    public string DeviceName { get; set; } = string.Empty;
    public string BrowserName { get; set; } = "Chrome";
    public string AppPath { get; set; } = string.Empty;
    public string AppPackage { get; set; } = string.Empty;
    public string AppActivity { get; set; } = string.Empty;
}
