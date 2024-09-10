namespace PolarisLite.API;

public static class ApiSettings
{
    public static string BaseUrl { get; set; }
    public static int ClientTimeoutSeconds { get; set; }
    public static int MaxRetryAttempts { get; set; }
    public static int PauseBetweenFailures { get; set; }
    public static TimeUnit TimeUnit { get; set; }
    public static bool EnableBDDLogging { get; set; }
    public static bool EnableToastMessages { get; set; }
    public static string OAuth2Token { get; set; }
}