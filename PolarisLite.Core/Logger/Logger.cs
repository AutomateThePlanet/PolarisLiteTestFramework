using PolarisLite.Logging.StaticSettings;
using Serilog;

namespace PolarisLite.Logging;

public static class Logger
{
    private static readonly ILogger s_logger;

    static Logger()
    {
        var loggerConfiguration = new LoggerConfiguration();
        if (LoggingSettings.IsEnabled)
        {
            if (LoggingSettings.IsConsoleLoggingEnabled)
            {
                loggerConfiguration.WriteTo.Console(outputTemplate: LoggingSettings.OutputTemplate);
            }

            if (LoggingSettings.IsDebugLoggingEnabled)
            {
                loggerConfiguration.WriteTo.Debug(outputTemplate: LoggingSettings.OutputTemplate);
            }

            if (LoggingSettings.IsFileLoggingEnabled)
            {
                loggerConfiguration.WriteTo.File("unicorn-log.txt", rollingInterval: RollingInterval.Day, outputTemplate: LoggingSettings.OutputTemplate);
            }
        }

        s_logger = loggerConfiguration.CreateLogger();
    }

    public static void LogInfo(string message, params object[] args)
    {
        s_logger.Information(message, args);
    }

    public static void LogError(string message, params object[] args)
    {
        s_logger.Error(message, args);
    }

    public static void LogWarning(string message, params object[] args)
    {
        s_logger.Warning(message, args);
    }
}
