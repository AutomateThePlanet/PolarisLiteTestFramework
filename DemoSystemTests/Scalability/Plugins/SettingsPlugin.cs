using PolarisLite.Core;
using PolarisLite.Core.Plugins;
using PolarisLite.Web.Plugins;
using System.Reflection;
namespace DemoSystemTests;
public class SettingsPlugin : Plugin
{
    public override void OnBeforeTestInitialize(MethodInfo memberInfo)
    {
        var settings = GetSettingsAttribute(memberInfo.DeclaringType);
        var settingsService = new MockSettingsService();

        foreach (var currentSetting in settings)
        {
            settingsService.SetSetting(currentSetting.Item1, currentSetting.Item2);
            DriverFactory.Tags.Add($"settings:{currentSetting.Item1}={currentSetting.Item2}");
        }
    }

    public override void OnAfterTestCleanup(TestOutcome result, MethodInfo memberInfo, Exception failedTestException)
    {
        var settings = GetSettingsAttribute(memberInfo.DeclaringType);
        var settingsService = new MockSettingsService();

        foreach (var currentSetting in settings)
        {
            settingsService.RestoreDefault(currentSetting.Item1);
        }
    }

    private List<(Settings, object)> GetSettingsAttribute(Type testClass)
    {
        var settingsAttributes = testClass.GetCustomAttributes<SettingsAttribute>(true);
        return settingsAttributes
            .Select(settingsAttribute => (settingsAttribute.Settings, settingsAttribute.Value))
            .ToList();
    }
}