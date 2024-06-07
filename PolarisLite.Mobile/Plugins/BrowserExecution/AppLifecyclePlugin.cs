using PolarisLite.Core;
using PolarisLite.Core.Plugins;
using PolarisLite.Mobile.Plugins.AppExecution;
using System;
using System.Reflection;

namespace PolarisLite.Mobile.Plugins;
public class AppLifecyclePlugin : Plugin
{
    // TODO: refactor DriverAdapter later to move logic to initialize the driver
    // especially when we have LambdaTest logic and more complex stuff there.
    //private DriverAdapter _driverAdapter;

    private readonly DriverFactory _driverFactory;
    private AppConfiguration _currentAppConfiguration;
    private AppConfiguration _previousBrowserConfiguration;

    public AppLifecyclePlugin()
    {
        _driverFactory = new DriverFactory();
    }

    public override void OnBeforeTestInitialize(MethodInfo memberInfo)
    {
        _currentAppConfiguration = GetBrowserConfiguration(memberInfo);
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

    private AppConfiguration GetBrowserConfiguration(MemberInfo testMethod)
    {
        var classBrowser = GetExecutionBrowserClassLevel(testMethod.DeclaringType);
        var methodBrowser = GetExecutionBrowserMethodLevel(testMethod);
        AppConfiguration browserConfiguration = methodBrowser != null ? methodBrowser : classBrowser;
        return browserConfiguration;
    }

    private AppConfiguration GetExecutionBrowserMethodLevel(MemberInfo testMethod)
    {
        var executionBrowserAttribute = testMethod.GetCustomAttribute<ExecutionAppAttribute>(true);
        return AppConfiguration.FromAttribute(executionBrowserAttribute);
    }

    private AppConfiguration GetExecutionBrowserClassLevel(Type testClass)
    {
        var executionBrowserAttribute = testClass.GetCustomAttribute<ExecutionAppAttribute>(true);
        return AppConfiguration.FromAttribute(executionBrowserAttribute);
    }
}