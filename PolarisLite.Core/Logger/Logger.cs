using PolarisLite.Core.Settings.StaticSettings;
using ReportPortal.Serilog;
using Serilog;

namespace PolarisLite.Logging;

public static class Logger
{
    private static readonly ILogger s_logger;

    static Logger()
    {
        var loggerConfiguration = new LoggerConfiguration();
        if (GlobalSettings.LoggingSettings.IsEnabled)
        {
            if (GlobalSettings.LoggingSettings.IsConsoleLoggingEnabled)
            {
                loggerConfiguration.WriteTo.Console(outputTemplate: GlobalSettings.LoggingSettings.OutputTemplate);
            }

            if (GlobalSettings.LoggingSettings.IsDebugLoggingEnabled)
            {
                loggerConfiguration.WriteTo.Debug(outputTemplate: GlobalSettings.LoggingSettings.OutputTemplate);
            }

            if (GlobalSettings.LoggingSettings.IsFileLoggingEnabled)
            {
                loggerConfiguration.WriteTo.File("unicorn-log.txt", rollingInterval: RollingInterval.Day, outputTemplate: GlobalSettings.LoggingSettings.OutputTemplate);
            }

            if (GlobalSettings.LoggingSettings.IsReportPortalLoggingEnabled)
            {
                loggerConfiguration.WriteTo.ReportPortal();
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
