namespace DemoSystemTests.Scalability.FeatureFlags;
public static class BDDLoggingFeatureFlagEventHandlers
{
    public static void Subscribe()
    {
        FeatureFlagService.FlagToggled += OnFlagToggled;
        FeatureFlagService.FlagRestored += OnFlagRestored;
    }

    public static void Unsubscribe()
    {
        FeatureFlagService.FlagToggled -= OnFlagToggled;
        FeatureFlagService.FlagRestored -= OnFlagRestored;
    }

    private static void OnFlagToggled(object sender, FeatureFlagEventArgs e)
    {
        Logger.LogInfo($"Feature Flag Toggled: {e.Flag} = {e.IsEnabled}");
    }

    private static void OnFlagRestored(object sender, FeatureFlagEventArgs e)
    {
        Logger.LogInfo($"Feature Flag Restored to Default: {e.Flag} = {e.IsEnabled}");
    }
}
