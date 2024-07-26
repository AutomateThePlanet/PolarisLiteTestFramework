using PolarisLite.Core;
using PolarisLite.Core.Plugins;
using System.Reflection;

namespace Bellatrix.Core.Plugins;
public abstract class ScreenshotPlugin : Plugin
{
    private readonly bool _isEnabled;

    protected ScreenshotPlugin(bool isEnabled)
    {
        _isEnabled = isEnabled;
    }

    protected abstract void TakeScreenshot(string screenshotSaveDir, string filename);
    protected abstract string GetOutputFolder();
    protected abstract string GetUniqueFileName(string testName);

    public override void OnBeforeTestCleanup(TestOutcome testResult, MethodInfo memberInfo)
    {
        if (!_isEnabled || testResult == TestOutcome.Passed)
        {
            return;
        }

        var screenshotSaveDir = GetOutputFolder();
        var screenshotFileName = GetUniqueFileName(memberInfo.Name);
        TakeScreenshot(screenshotSaveDir, screenshotFileName);
    }
}
