using PolarisLite.Core;
using PolarisLite.Core.Plugins;
using PolarisLite.Mobile.Plugins.AppExecution;
using System;
using System.Reflection;

namespace PolarisLite.Mobile.Plugins;
public class AppLifecyclePlugin : Plugin
{
    private AppConfiguration _currentAppConfiguration;
    private AppConfiguration _previousBrowserConfiguration;

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
        DriverFactory.Dispose();

        DriverFactory.StartApp(_currentAppConfiguration);
    }

    private void ShutdownApp()
    {
        DriverFactory.Dispose();
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

    private LocalExecutionAttribute GetExecutionAppMethodLevel(MemberInfo testMethod)
    {
        var executionBrowserAttribute = testMethod.GetCustomAttribute<LocalExecutionAttribute>(true);
        return executionBrowserAttribute;
    }

    private LocalExecutionAttribute GetExecutionAppClassLevel(Type testClass)
    {
        var executionBrowserAttribute = testClass.GetCustomAttribute<LocalExecutionAttribute>(true);
        return executionBrowserAttribute;
    }
}