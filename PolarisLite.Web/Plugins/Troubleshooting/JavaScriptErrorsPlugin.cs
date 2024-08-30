using NUnit.Framework;
using PolarisLite.Core;
using PolarisLite.Core.Plugins;
using System.Collections.ObjectModel;
using System.Reflection;
namespace PolarisLite.Web.Plugins.Troubleshooting;

public class JavaScriptErrorsPlugin : Plugin
{
    public override void OnAfterTestCleanup(TestOutcome result, MethodInfo memberInfo, Exception failedTestException)
    {
        // TODO: refactor DriverFactory to collect logs
        // chromeOptions.SetLoggingPreference(LogType.Browser, LogLevel.All);
        var driver = DriverFactory.WrappedDriver;
        //bool shouldCheckForJsErrors = ConfigurationService.GetSection<WebSettings>().ShouldCheckForJavaScriptErrors;
        //if (driver == null || !shouldCheckForJsErrors)
        //{
        //    return;
        //}

        var errorStrings = new List<string>
         {
             "SyntaxError",
             "EvalError",
             "ReferenceError",
             "RangeError",
             "TypeError",
             "URIError",
         };
        ReadOnlyCollection<OpenQA.Selenium.LogEntry> browserLogs = driver?.Manage()?.Logs?.GetLog(LogType.Browser);
        var jsErrors = browserLogs?.Where(x => errorStrings.Any(e => !string.IsNullOrEmpty(x.Message) && x.Message.Contains(e)));
        if (jsErrors != null && jsErrors.Any())
        {
            Assert.Fail($"JavaScript error(s): {Environment.NewLine} {jsErrors.Aggregate(string.Empty, (s, entry) => s + entry.Message)}{Environment.NewLine}");
        }
    }
}