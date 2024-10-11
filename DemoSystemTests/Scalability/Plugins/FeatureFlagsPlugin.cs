using DemoSystemTests.Scalability.FeatureFlags;
using PolarisLite.Core;
using PolarisLite.Core.Plugins;
using PolarisLite.Web.Plugins;
using System.Reflection;

namespace DemoSystemTests;
public class FeatureTogglePlugin : Plugin
{
    public override void OnBeforeTestInitialize(MethodInfo memberInfo)
    {
        var featureToggles = GetFeatureToggleAttribute(memberInfo.DeclaringType);

        foreach (var currentToggle in featureToggles)
        {
            FeatureFlagService.SetFeatureFlag(currentToggle.Item1, currentToggle.Item2);
            DriverFactory.Tags.Add($"featureFlag:{currentToggle.Item1}={currentToggle.Item2}");
        }
    }

    public override void OnAfterTestCleanup(TestOutcome result, MethodInfo memberInfo, Exception failedTestException)
    {
        var settings = GetFeatureToggleAttribute(memberInfo.DeclaringType);

        foreach (var currentSetting in settings)
        {
            FeatureFlagService.RestoreDefaultFeatureFlag(currentSetting.Item1);
        }
    }

    private List<(FeatureFlags, bool)> GetFeatureToggleAttribute(Type testClass)
    {
        var featureToggleAttributes = testClass.GetCustomAttributes<FeatureFlagAttribute>(true);
        return featureToggleAttributes
            .Select(featureToggleAttribute => (featureToggleAttribute.Flag, featureToggleAttribute.IsEnabled))
            .ToList();
    }
}