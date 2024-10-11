namespace DemoSystemTests.Scalability.FeatureFlags;
public class FeatureFlagEventArgs : EventArgs
{
    public FeatureFlags Flag { get; }
    public bool IsEnabled { get; }

    public FeatureFlagEventArgs(FeatureFlags flag, bool isEnabled)
    {
        Flag = flag;
        IsEnabled = isEnabled;
    }
}
