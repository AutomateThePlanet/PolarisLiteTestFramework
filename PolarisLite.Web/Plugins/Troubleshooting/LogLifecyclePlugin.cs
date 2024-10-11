using PolarisLite.Core;
using System.Reflection;

namespace PolarisLite.Web.Plugins.Troubleshooting;
public class LogLifecyclePlugin : Plugin
{
    public override void OnBeforeTestInitialize(MethodInfo memberInfo)
    {
        //Logger.CurrentTestFullName.Value = memberInfo.ReflectedType.FullName.ToString();

        Logger.LogInfo($"Start Test {memberInfo.ReflectedType.FullName}.{memberInfo.Name}");
    }
}