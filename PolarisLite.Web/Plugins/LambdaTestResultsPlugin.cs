using OpenQA.Selenium.Support.Extensions;
using PolarisLite.Core;
using PolarisLite.Core.Plugins;
using PolarisLite.Web.Plugins;
using PolarisLite.Web.Plugins.BrowserExecution;
using System.Reflection;
namespace Bellatrix.Web.Plugins.Browser;

public class LambdaTestResultsPlugin : Plugin
{
    public override void OnAfterTestCleanup(TestOutcome testResult, MethodInfo memberInfo, Exception failedTestException)
    {
        var driver = DriverFactory.WrappedDriver;
        bool isLambdaTestRun = DriverFactory.ExecutionType.Equals(ExecutionType.LambdaTest);

        if (isLambdaTestRun && testResult == TestOutcome.Passed)
        {
            driver.ExecuteJavaScript("lambda-status=passed");
        }
        else if (isLambdaTestRun)
        {
            // pass the real exception to LambdaTest
            var exceptionCapture = new List<string>
                {
                    failedTestException.ToString()
                };
            driver.ExecuteJavaScript("lambda-exceptions", exceptionCapture);
            driver.ExecuteJavaScript("lambda-status=failed");
        }
    }
}