namespace PolarisLite.Core.Settings.StaticSettings;

public class GlobalSettings
{
    static GlobalSettings()
    {
        LoggingSettings = new LoggingSettings();
    }

    public static LoggingSettings LoggingSettings { get; set; }

    // add later report portal + others
}
