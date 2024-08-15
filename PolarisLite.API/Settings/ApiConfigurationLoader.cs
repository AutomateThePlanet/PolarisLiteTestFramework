using PolarisLite.API;
using PolarisLite.Core.Settings.StaticSettings;

namespace PolarisLite.Api.Configuration;
public class ApiConfigurationLoader
{
    public static void LoadConfiguration()
    {
        var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
        switch (environment)
        {
            case "Development":
                LoadDevelopmentConfiguration();
                break;
            case "Staging":
                LoadStagingConfiguration();
                break;
            case "QA":
                LoadQAConfiguration();
                break;
            default:
                throw new InvalidOperationException($"Unknown environment: {environment}");
        }
    }

    private static void LoadQAConfiguration()
    {
        ApiSettings.PauseBetweenFailures = 2;
        ApiSettings.ClientTimeoutSeconds = 30;
        ApiSettings.BaseUrl = "http://localhost:60715/";
        ApiSettings.TimeUnit = TimeUnit.Seconds;
        ApiSettings.MaxRetryAttempts = 2;
        ApiSettings.EnableBDDLogging = true;

        GlobalSettings.LoggingSettings.IsEnabled = true;
        GlobalSettings.LoggingSettings.IsConsoleLoggingEnabled = true;
        GlobalSettings.LoggingSettings.IsFileLoggingEnabled = false;
        GlobalSettings.LoggingSettings.IsDebugLoggingEnabled = false;
    }

    private static void LoadStagingConfiguration()
    {
        ApiSettings.PauseBetweenFailures = 3;
        ApiSettings.ClientTimeoutSeconds = 60;
        ApiSettings.BaseUrl = "http://localhost:60715/";
        ApiSettings.TimeUnit = TimeUnit.Seconds;
        ApiSettings.MaxRetryAttempts = 3;
        ApiSettings.EnableBDDLogging = true;
        ApiSettings.EnableToastMessages = false;

        GlobalSettings.LoggingSettings.IsEnabled = true;
        GlobalSettings.LoggingSettings.IsConsoleLoggingEnabled = true;
        GlobalSettings.LoggingSettings.IsFileLoggingEnabled = false;
        GlobalSettings.LoggingSettings.IsDebugLoggingEnabled = false;

        GlobalSettings.LoggingSettings.IsEnabled = true;
        GlobalSettings.LoggingSettings.IsConsoleLoggingEnabled = true;
        GlobalSettings.LoggingSettings.IsFileLoggingEnabled = false;
        GlobalSettings.LoggingSettings.IsDebugLoggingEnabled = false;
    }

    private static void LoadDevelopmentConfiguration()
    {
        ApiSettings.PauseBetweenFailures = 3;
        ApiSettings.ClientTimeoutSeconds = 15;
        ApiSettings.BaseUrl = "http://localhost:3001/api/";
        ApiSettings.TimeUnit = TimeUnit.Seconds;
        ApiSettings.MaxRetryAttempts = 3;
        ApiSettings.EnableBDDLogging = true;
        ApiSettings.EnableToastMessages = true;

        GlobalSettings.LoggingSettings.IsEnabled = true;
        GlobalSettings.LoggingSettings.IsConsoleLoggingEnabled = true;
        GlobalSettings.LoggingSettings.IsFileLoggingEnabled = false;
        GlobalSettings.LoggingSettings.IsDebugLoggingEnabled = false;
    }
}
