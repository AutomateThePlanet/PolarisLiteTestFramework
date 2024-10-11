using PolarisLite.Core.Plugins;
using PolarisLite.Logging;
using System.Reflection;

namespace PolarisLite.Core;

public class LoggerFlushPlugin : Plugin
{
    public override void OnBeforeTestCleanup(TestOutcome testResult, MethodInfo memberInfo)
    {
       //Logger.FlushLogs();
    }
}
