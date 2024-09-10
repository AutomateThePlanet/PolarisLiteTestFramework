namespace PolarisLite.Integrations.Settings;

public class IntegrationSettings
{
    static IntegrationSettings()
    {
        BlobStorageSettings = new BlobStorageSettings();
        TwilioSettings = new TwilioSettings();
        MailslurpSettings = new MailslurpSettings();
        AppInsightsSettings = new AppInsightsSettings();
    }

    public static bool ReportPortalEnabled { get; set; } = true;
    public static string AzureDevOpsBuildUrl { get; set; }
    public static string TwoFASecret { get; set; }
    public static BlobStorageSettings BlobStorageSettings { get; set; }
    public static TwilioSettings TwilioSettings { get; set; }
    public static MailslurpSettings MailslurpSettings { get; set; }
    public static AppInsightsSettings AppInsightsSettings { get; set; }
}
