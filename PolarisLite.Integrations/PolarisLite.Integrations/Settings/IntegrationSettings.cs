namespace PolarisLite.Integrations.Settings;

public class IntegrationSettings
{
    static IntegrationSettings()
    {
        BlobStorageSettings = new BlobStorageSettings();
        TwilioSettings = new TwilioSettings();
    }

    public static BlobStorageSettings BlobStorageSettings { get; set; }
    public static TwilioSettings TwilioSettings { get; set; }
}
