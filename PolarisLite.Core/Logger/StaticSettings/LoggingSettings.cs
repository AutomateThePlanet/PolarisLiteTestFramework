namespace PolarisLite.Logging.StaticSettings;

public class LoggingSettings
{
    public static bool IsEnabled { get; set; } = true;
    public static bool IsConsoleLoggingEnabled { get; set; } = true;
    public static bool IsDebugLoggingEnabled { get; set; } = true;
    public static bool IsFileLoggingEnabled { get; set; } = false;
    public static string OutputTemplate { get; set; } = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";
}
