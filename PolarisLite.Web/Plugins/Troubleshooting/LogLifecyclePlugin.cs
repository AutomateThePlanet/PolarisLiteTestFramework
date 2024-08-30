using PolarisLite.Core;
using System.Reflection;

namespace PolarisLite.Web.Plugins.Troubleshooting;
public class LogLifecyclePlugin : Plugin
{
    public override void OnBeforeTestInitialize(MethodInfo memberInfo)
    {
        Logger.LogInfo($"Start Test {memberInfo.GetType().Name}.{memberInfo.Name}");
    }
}