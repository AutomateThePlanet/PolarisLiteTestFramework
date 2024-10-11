using PolarisLite.Core.Settings.StaticSettings;
using ReportPortal.Serilog;
using Serilog;

namespace PolarisLite.Logging;

public static class ThreadSafeLogger
{
    // ThreadLocal to ensure each test has its own log buffer
    private static readonly ThreadLocal<Dictionary<string, List<string>>> s_logBuffer = new ThreadLocal<Dictionary<string, List<string>>>(() => new Dictionary<string, List<string>>());

    // ThreadLocal to ensure each thread has its own logger configuration
    private static readonly ThreadLocal<ILogger> s_logger = new ThreadLocal<ILogger>(InitializeLogger);

    public static ThreadLocal<string> CurrentTestFullName { get; set; } = new ThreadLocal<string>();
    private static ILogger InitializeLogger()
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

        return loggerConfiguration.CreateLogger();
    }

    // Method to accumulate logs in the buffer per test/thread
    private static void AccumulateLog(string logLevel, string message)
    {
        if (!s_logBuffer.Value.ContainsKey(CurrentTestFullName.Value))
        {
            s_logBuffer.Value.Add(CurrentTestFullName.Value, new List<string>());
        }

        var threadId = Environment.CurrentManagedThreadId;
        string formattedMessage = $"{threadId} [{DateTime.Now:HH:mm:ss}] [{logLevel}] {message}";
        s_logBuffer.Value[CurrentTestFullName.Value].Add(formattedMessage);
    }

    public static void LogInfo(string message, params object[] args)
    {
        AccumulateLog("INFO", string.Format(message, args));
    }

    public static void LogError(string message, params object[] args)
    {
        AccumulateLog("ERROR", string.Format(message, args));
    }

    public static void LogWarning(string message, params object[] args)
    {
        AccumulateLog("WARNING", string.Format(message, args));
    }

    // Lock object to ensure thread-safe execution of FlushLogs
    private static readonly object _flushLock = new object();

    // Method to print the accumulated logs at the end of the test
    public static void FlushLogs()
    {
        lock (_flushLock) // Ensure that only one thread can execute FlushLogs at a time
        {
            // Check if there's anything to flush for the current test
            if (s_logBuffer.Value.ContainsKey(CurrentTestFullName.Value))
            {
                // Flush the logs to the configured sinks
                foreach (var log in s_logBuffer.Value[CurrentTestFullName.Value])
                {
                    s_logger.Value.Information(log); // This could be switched to appropriate log levels
                }

                // Clear the log buffer after flushing
                s_logBuffer.Value[CurrentTestFullName.Value].Clear();
            }
        }
    }
}
