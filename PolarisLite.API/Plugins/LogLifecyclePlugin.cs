using PolarisLite.Core;
using PolarisLite.Logging;
using System.Reflection;

namespace PolarisLite.API.Plugins;
public class LogLifecyclePlugin : Plugin
{
    public override void OnBeforeTestInitialize(MethodInfo memberInfo)
    {
        Logger.LogInfo($"Start Test {memberInfo.GetType().Name}.{memberInfo.Name}");
    }
}