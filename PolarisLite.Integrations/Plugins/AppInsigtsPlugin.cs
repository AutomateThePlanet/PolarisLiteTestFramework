using Microsoft.ApplicationInsights.DataContracts;
using PolarisLite.Core;
using PolarisLite.Core.Plugins;
using PolarisLite.Integrations;
using System.Reflection;
namespace PolarisLite.Web.Plugins.Browser;

public class AppInsigtsPlugin : Plugin
{
    private static readonly Dictionary<string, DateTime> _testsExecutionTimes = new Dictionary<string, DateTime>();

    public override void OnBeforeTestInitialize(MethodInfo memberInfo)
    {
        string testFullName = GetTestFullName(memberInfo);
        DateTime startTime = DateTime.Now;
        if (!_testsExecutionTimes.ContainsKey(testFullName))
        {
            _testsExecutionTimes.Add(testFullName, startTime);
        }
        else
        {
            _testsExecutionTimes[testFullName] = startTime;
        }
    }

    public override void OnAfterTestCleanup(TestOutcome result, MethodInfo memberInfo, Exception failedTestException)
    {
        string testFullName = GetTestFullName(memberInfo);
        TimeSpan totalExecutionTime = default;
        DateTime endTime = DateTime.Now;
        if (_testsExecutionTimes.ContainsKey(testFullName))
        {
            var startTime = _testsExecutionTimes[testFullName];
            totalExecutionTime = endTime - startTime;
            _testsExecutionTimes.Remove(testFullName);
        }

        EventTelemetry eventTelemetry = new EventTelemetry();
      
        eventTelemetry.Name = "Automated Test Result";
        eventTelemetry.Properties["TestName"] = memberInfo.Name;
        eventTelemetry.Properties["TestSuite"] = memberInfo.DeclaringType.FullName;
        eventTelemetry.Properties["TestOutcome"] = result.GetDescription();
        eventTelemetry.Properties["Duration"] = totalExecutionTime.ToString();
        if (result != TestOutcome.Passed && failedTestException != null)
        {
            eventTelemetry.Properties["Exception"] = failedTestException.ToString();
        }
      
        ApplicationInsightsService.TrackEvent(eventTelemetry);
        ApplicationInsightsService.Flush();
    }

    private string GetTestFullName(MethodInfo memberInfo)
    {
        return $"{memberInfo.DeclaringType.FullName}.{memberInfo.Name}";
    }
}