using DemoSystemTests.Scalability.FeatureFlags;

namespace DemoSystemTests;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
public class FeatureFlagAttribute : Attribute
{
    public FeatureFlagAttribute(FeatureFlags featureFlag, bool isEnabled, bool setTags = true)
    {
        Flag = featureFlag;
        IsEnabled = isEnabled;
        SetTags = setTags;
    }

    public FeatureFlags Flag { get; set; }
    public bool IsEnabled { get; set; }
    public bool SetTags { get; set; }
}
