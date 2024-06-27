using OpenQA.Selenium.Support.Extensions;
using PolarisLite.Core;
using PolarisLite.Core.Plugins;
using PolarisLite.Web;
using PolarisLite.Web.Plugins.BrowserExecution;
using System.Reflection;
namespace Bellatrix.Web.Plugins.Browser;

public class LambdaTestResultsPlugin : Plugin
{
    public override void OnAfterTestCleanup(TestOutcome testResult, MethodInfo memberInfo, Exception failedTestException)
    {
        var driver = DriverFactory.WrappedDriver;

        var executionType = ConfigurationService.GetSection<WebSettings>().ExecutionType;
        bool isLambdaTestRun = executionType.Equals("lambda test", StringComparison.OrdinalIgnoreCase);

        try
        {
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
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}