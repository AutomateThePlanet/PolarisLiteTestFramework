using PolarisLite.Core;
using PolarisLite.Core.Plugins;
using System.Reflection;

namespace Polaris.Plugins.Common.ExecutionTime;

public class ExecutionTimeUnderPlugin : Plugin
{
    private static readonly Dictionary<string, DateTime> _testsExecutionTimes = new Dictionary<string, DateTime>();

    public override void OnBeforeTestInitialize(MethodInfo memberInfo)
    {
        TimeSpan executionTimeout = GetExecutionTimeout(memberInfo);
        string testFullName = GetTestFullName(memberInfo);
        if (executionTimeout != TimeSpan.MaxValue)
        {
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
    }

    public override void OnAfterTestCleanup(TestOutcome result, MethodInfo memberInfo, Exception failedTestException)
    {
        TimeSpan executionTimeout = GetExecutionTimeout(memberInfo);
        string testFullName = GetTestFullName(memberInfo);
        if (executionTimeout != TimeSpan.MaxValue)
        {
            DateTime endTime = DateTime.Now;
            if (_testsExecutionTimes.ContainsKey(testFullName))
            {
                var startTime = _testsExecutionTimes[testFullName];
                var totalExecutionTime = endTime - startTime;
                _testsExecutionTimes.Remove(testFullName);
                if (totalExecutionTime > executionTimeout)
                {
                    throw new ExecutionTimeoutException($"The test {testFullName} was executed for {totalExecutionTime}. The specified limit was {executionTimeout}.");
                }
            }
        }
    }

    private string GetTestFullName(MethodInfo memberInfo)
    {
        return $"{memberInfo.DeclaringType.FullName}.{memberInfo.Name}";
    }

    private TimeSpan GetExecutionTimeout(MemberInfo memberInfo)
    {
        TimeSpan methodTimeout = GetTimeoutByMethodInfo(memberInfo);
        TimeSpan classTimeout = GetTimeoutInfoByType(memberInfo.DeclaringType);

        if (methodTimeout != TimeSpan.MaxValue)
        {
            return methodTimeout;
        }

        if (classTimeout != TimeSpan.MaxValue)
        {
            return classTimeout;
        }

        return TimeSpan.MaxValue;
    }

    private TimeSpan GetTimeoutInfoByType(Type currentType)
    {
        if (currentType == null)
        {
            throw new ArgumentNullException();
        }

        var executionTimeUnderAttribute = currentType.GetCustomAttribute<ExecutionTimeUnderAttribute>(true);
        if (executionTimeUnderAttribute != null)
        {
            return executionTimeUnderAttribute.Timeout;
        }

        return TimeSpan.MaxValue;
    }

    private TimeSpan GetTimeoutByMethodInfo(MemberInfo memberInfo)
    {
        if (memberInfo == null)
        {
            throw new ArgumentNullException();
        }

        var executionTimeUnderAttribute = memberInfo.GetCustomAttribute<ExecutionTimeUnderAttribute>(true);
        if (executionTimeUnderAttribute != null)
        {
            return executionTimeUnderAttribute.Timeout;
        }

        return TimeSpan.MaxValue;
    }
}