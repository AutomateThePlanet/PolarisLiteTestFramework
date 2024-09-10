using PolarisLite.Secrets;

namespace PolarisLite.Core.Settings.StaticSettings;

public class GlobalSettings
{
    static GlobalSettings()
    {
        LoggingSettings = new LoggingSettings();
        KeyVaultSettings = new KeyVaultSettings();
        AwsSecretsSettings = new AwsSecretsSettings();
    }

    public static LoggingSettings LoggingSettings { get; set; }
    public static KeyVaultSettings KeyVaultSettings { get; set; }
    public static AwsSecretsSettings AwsSecretsSettings { get; set; }
    // add later report portal + others
}
