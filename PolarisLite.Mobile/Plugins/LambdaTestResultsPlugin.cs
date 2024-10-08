﻿using OpenQA.Selenium.Support.Extensions;
using PolarisLite.Core;
using PolarisLite.Core.Plugins;
using PolarisLite.Mobile.Plugins;
using PolarisLite.Mobile.Plugins.AppExecution;
using System.Reflection;

namespace Bellatrix.Mobile.Plugins;

public class LambdaTestResultsPlugin : Plugin
{
    public override void OnAfterTestCleanup(TestOutcome testResult, MethodInfo memberInfo, Exception failedTestException)
    {
        var driver = DriverFactory.WrappedAndroidDriver;

        bool isLambdaTestRun = DriverFactory.AppConfiguration.ExecutionType.Equals(ExecutionType.LambdaTest);

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