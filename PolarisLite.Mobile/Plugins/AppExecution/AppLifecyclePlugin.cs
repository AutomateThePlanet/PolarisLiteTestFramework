using PolarisLite.Core;
using PolarisLite.Core.Plugins;
using PolarisLite.Mobile.Plugins.AppExecution;
using PolarisLite.Web;
using System;
using System.Reflection;

namespace PolarisLite.Mobile.Plugins;
public class AppLifecyclePlugin : Plugin
{
    private readonly DriverFactory _driverFactory;
    private AppConfiguration _currentAppConfiguration;
    private AppConfiguration _previousAppConfiguration;

    public AppLifecyclePlugin()
    {
        _driverFactory = new DriverFactory();
    }

    public override void OnBeforeTestInitialize(MethodInfo memberInfo)
    {
        _currentAppConfiguration = GetAppConfiguration(memberInfo);
        
        bool shouldRestartApp = ShouldRestartApp(_currentAppConfiguration);
        if (shouldRestartApp)
        {
            RestartApp();
        }

        _previousAppConfiguration = _currentAppConfiguration;
    }

    private void RestartApp()
    {
        _driverFactory.Dispose();
        
        _driverFactory.StartApp(_currentAppConfiguration);
    }

    private void ShutdownApp()
    {
        _driverFactory.Dispose();
    }

    private bool ShouldRestartApp(AppConfiguration AppConfiguration)
    {
        if (_previousAppConfiguration == null)
        {
            return true;
        }

        bool shouldRestartApp = 
            AppConfiguration.Lifecycle == Lifecycle.RestartEveryTime
            || AppConfiguration.Lifecycle == Lifecycle.NotSet;
        return shouldRestartApp;
    }

    public override void OnAfterTestCleanup(TestOutcome testOutcome, MethodInfo memberInfo, Exception failedTestException)
    {
        if (_currentAppConfiguration.Lifecycle == Lifecycle.RestartOnFail && testOutcome == TestOutcome.Failed)
        {
            RestartApp();
        }

        if (_currentAppConfiguration.Lifecycle == Lifecycle.RestartEveryTime || (_currentAppConfiguration.Lifecycle == Lifecycle.RestartOnFail && !testOutcome.Equals(TestOutcome.Passed)))
        {
            ShutdownApp();
        }
    }

    private AppConfiguration GetAppConfiguration(MemberInfo testMethod)
    {
        var lambdaClassAttribute = GetLambdaTestClassLevel(testMethod.DeclaringType);
        var lambdaMethodAttribute = GetLambdaTestMethodLevel(testMethod);
        var lambdaAppAttribute = lambdaMethodAttribute != null ? lambdaMethodAttribute : lambdaClassAttribute;

        AppConfiguration appConfiguration = default;
        if (lambdaAppAttribute != null)
        {
            appConfiguration = AppConfiguration.FromAttribute(lambdaAppAttribute);
            return appConfiguration;
        }
        else
        {
            var classApp = GetExecutionAppClassLevel(testMethod.DeclaringType);
            var methodApp = GetExecutionAppMethodLevel(testMethod);
            var appAttribute = methodApp != null ? methodApp : classApp;

            appConfiguration = AppConfiguration.FromAttribute(appAttribute);
            return appConfiguration;
        }
    }

    private ExecutionAppAttribute GetExecutionAppMethodLevel(MemberInfo testMethod)
    {
        var executionAppAttribute = testMethod.GetCustomAttribute<ExecutionAppAttribute>(true);
        return executionAppAttribute;
    }

    private ExecutionAppAttribute GetExecutionAppClassLevel(Type testClass)
    {
        var executionAppAttribute = testClass.GetCustomAttribute<ExecutionAppAttribute>(true);
        return executionAppAttribute;
    }

    private LambdaTestAttribute GetLambdaTestMethodLevel(MemberInfo testMethod)
    {
        var gridAttribute = testMethod.GetCustomAttribute<LambdaTestAttribute>(true);
        return gridAttribute;
    }

    private LambdaTestAttribute GetLambdaTestClassLevel(Type testClass)
    {
        var gridAttribute = testClass.GetCustomAttribute<LambdaTestAttribute>(true);
        return gridAttribute;
    }
}