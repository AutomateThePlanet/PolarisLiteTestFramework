namespace DemoSystemTests.Scalability.AppSettings;
public static class BDDLoggingSettingsEventHandlers
{
    public static void Subscribe()
    {
        EventEnabledMockSettingsService.SettingRetrieved += OnSettingRetrieved;
        EventEnabledMockSettingsService.SettingUpdated += OnSettingUpdated;
        EventEnabledMockSettingsService.SettingRestored += OnSettingRestored;
    }

    public static void Unsubscribe()
    {
        EventEnabledMockSettingsService.SettingRetrieved -= OnSettingRetrieved;
        EventEnabledMockSettingsService.SettingUpdated -= OnSettingUpdated;
        EventEnabledMockSettingsService.SettingRestored -= OnSettingRestored;
    }

    private static void OnSettingRetrieved(object sender, SettingsEventArgs e)
    {
        Logger.LogInfo($"Setting Retrieved: {e.Setting} = {e.Value}");
    }

    private static void OnSettingUpdated(object sender, SettingsEventArgs e)
    {
        Logger.LogInfo($"Setting Updated: {e.Setting} = {e.Value}");
    }

    private static void OnSettingRestored(object sender, SettingsEventArgs e)
    {
        Logger.LogInfo($"Setting Restored to Default: {e.Setting} = {e.Value}");
    }
}
