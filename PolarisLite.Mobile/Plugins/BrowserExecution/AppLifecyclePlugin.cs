using PolarisLite.Core;
using PolarisLite.Core.Plugins;
using PolarisLite.Mobile.Plugins.AppExecution;
using System;
using System.Reflection;

namespace PolarisLite.Mobile.Plugins;
public class AppLifecyclePlugin : Plugin
{
    private readonly DriverFactory _driverFactory;
    private AppConfiguration _currentAppConfiguration;
    private AppConfiguration _previousBrowserConfiguration;

    public AppLifecyclePlugin()
    {
        _driverFactory = new DriverFactory();
    }

    public override void OnBeforeTestInitialize(MethodInfo memberInfo)
    {
        _currentAppConfiguration = GetAppConfiguration(memberInfo);
        bool shouldRestartBrowser = ShouldRestartBrowser(_currentAppConfiguration);
        if (shouldRestartBrowser)
        {
            RestartApp();
        }

        _previousBrowserConfiguration = _currentAppConfiguration;
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

    private bool ShouldRestartBrowser(AppConfiguration browserConfiguration)
    {
        if (_previousBrowserConfiguration == null)
        {
            return true;
        }

        bool shouldRestartBrowser = 
            browserConfiguration.Lifecycle == Lifecycle.RestartEveryTime
            || browserConfiguration.Lifecycle == Lifecycle.NotSet;
        return shouldRestartBrowser;
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
        var classApp = GetExecutionAppClassLevel(testMethod.DeclaringType);
        var methodApp = GetExecutionAppMethodLevel(testMethod);
        var appAttribute = methodApp != null ? methodApp : classApp;

        AppConfiguration appConfiguration = default;
        if (appAttribute == null)
        {
            var androidSettings = ConfigurationService.GetSection<AndroidSettings>();
            appConfiguration = new AppConfiguration(androidSettings);
        }
        else
        {
            appConfiguration = AppConfiguration.FromAttribute(appAttribute);
        }

        return appConfiguration;
    }

    private ExecutionAppAttribute GetExecutionAppMethodLevel(MemberInfo testMethod)
    {
        var executionBrowserAttribute = testMethod.GetCustomAttribute<ExecutionAppAttribute>(true);
        return executionBrowserAttribute;
    }

    private ExecutionAppAttribute GetExecutionAppClassLevel(Type testClass)
    {
        var executionBrowserAttribute = testClass.GetCustomAttribute<ExecutionAppAttribute>(true);
        return executionBrowserAttribute;
    }
}