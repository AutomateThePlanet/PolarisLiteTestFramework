using PolarisLite.Core;
using PolarisLite.Core.Plugins;
using PolarisLite.Web.Troubshoting;
using System.Reflection;
namespace PolarisLite.Web.Plugins;

public class ExceptionAnalysationPlugin : Plugin
{
    public override void OnAfterTestCleanup(TestOutcome testResult, MethodInfo memberInfo, Exception failedTestException)
    {
        ExceptionAnalyser.Analyse(failedTestException);
    }
}