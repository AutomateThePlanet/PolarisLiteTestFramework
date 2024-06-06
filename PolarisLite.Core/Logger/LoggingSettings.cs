namespace PolarisLite.Logging;

public class LoggingSettings
{
    public bool IsEnabled { get; set; }
    public bool IsConsoleLoggingEnabled { get; set; }
    public bool IsDebugLoggingEnabled { get; set; }
    public bool IsFileLoggingEnabled { get; set; }
    public string OutputTemplate { get; set; }
}
