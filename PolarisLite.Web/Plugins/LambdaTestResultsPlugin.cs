using OpenQA.Selenium.Support.Extensions;
using PolarisLite.Core;
using PolarisLite.Core.Plugins;
using PolarisLite.Web.Services;
using System.Reflection;
namespace PolarisLite.Web.Plugins.Browser;

public class LambdaTestResultsPlugin : Plugin
{
    public override void OnAfterTestCleanup(TestOutcome testResult, MethodInfo memberInfo, Exception failedTestException)
    {
        var driver = new DriverAdapter();
        bool isLambdaTestRun = DriverFactory.ExecutionType.Equals(ExecutionType.LambdaTest);

        if (isLambdaTestRun && testResult == TestOutcome.Passed)
        {
            driver.Execute("lambda-status=passed");
        }
        else if (isLambdaTestRun)
        {
            // pass the real exception to LambdaTest
            var exceptionCapture = new List<string>
                {
                    failedTestException.ToString()
                };
            driver.Execute("lambda-exceptions", exceptionCapture);
            driver.Execute("lambda-status=failed");
        }
    }
}