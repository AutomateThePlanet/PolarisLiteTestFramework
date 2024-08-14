namespace PolarisLite.Integrations.Settings;

public class IntegrationSettings
{
    static IntegrationSettings()
    {
        BlobStorageSettings = new BlobStorageSettings();
        TwilioSettings = new TwilioSettings();
        MailslurpSettings = new MailslurpSettings();
    }

    public static BlobStorageSettings BlobStorageSettings { get; set; }
    public static TwilioSettings TwilioSettings { get; set; }
    public static MailslurpSettings MailslurpSettings { get; set; }
}
