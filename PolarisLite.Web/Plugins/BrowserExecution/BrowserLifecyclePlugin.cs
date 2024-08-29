using PolarisLite.Core;
using PolarisLite.Core.Plugins;
using System.Reflection;

namespace PolarisLite.Web.Plugins;
public class BrowserLifecyclePlugin : Plugin
{
    private readonly DriverFactory _driverFactory;
    private BrowserConfiguration _currentBrowserConfiguration;
    private BrowserConfiguration _previousBrowserConfiguration;
    private GridSettings _currentGridConfiguration;

    public BrowserLifecyclePlugin()
    {
        _driverFactory = new DriverFactory();
    }

    public override void OnBeforeTestInitialize(MethodInfo memberInfo)
    {
        _currentBrowserConfiguration = GetBrowserConfiguration(memberInfo);
        _currentGridConfiguration = GetGridSettingsConfiguration(memberInfo);

        if (_currentGridConfiguration != null)
        {
            _currentGridConfiguration.TestName = memberInfo.Name;
        }

        bool shouldRestartBrowser = ShouldRestartBrowser(_currentBrowserConfiguration);
        if (shouldRestartBrowser)
        {
            RestartBrowser();
        }

        _previousBrowserConfiguration = _currentBrowserConfiguration;
    }

    private void RestartBrowser()
    {
        DriverFactory.Dispose();

        if (_currentBrowserConfiguration.ExecutionType == ExecutionType.Local)
        {
            DriverFactory.Start(_currentBrowserConfiguration);
        }
        else
        {
            DriverFactory.StartGrid(_currentBrowserConfiguration, _currentGridConfiguration);
        }
    }

    private void ShutdownBrowser()
    {
        DriverFactory.Dispose();
    }

    private bool ShouldRestartBrowser(BrowserConfiguration browserConfiguration)
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
        System.Environment.SetEnvironmentVariable("BUILD_NAME", "");
        if (_currentBrowserConfiguration.Lifecycle == Lifecycle.RestartOnFail && testOutcome == TestOutcome.Failed)
        {
            RestartBrowser();
        }

        if (_currentBrowserConfiguration.Lifecycle == Lifecycle.RestartEveryTime || (_currentBrowserConfiguration.Lifecycle == Lifecycle.RestartOnFail && !testOutcome.Equals(TestOutcome.Passed)))
        {
            ShutdownBrowser();
        }
    }

    private BrowserConfiguration GetBrowserConfiguration(MemberInfo testMethod)
    {
        var classBrowser = GetExecutionBrowserClassLevel(testMethod.DeclaringType);
        var methodBrowser = GetExecutionBrowserMethodLevel(testMethod);
        BrowserConfiguration browserConfiguration = methodBrowser != null ? methodBrowser : classBrowser;
        
        return browserConfiguration;
    }

    private GridSettings GetGridSettingsConfiguration(MemberInfo testMethod)
    {
        var classGridSettings = GetLambdaTestClassLevel(testMethod.DeclaringType);
        var methodGridSettings = GetLambdaTestMethodLevel(testMethod);
        GridSettings gridSettings = methodGridSettings != null ? methodGridSettings : classGridSettings;

        return gridSettings;
    }

    private BrowserConfiguration GetExecutionBrowserMethodLevel(MemberInfo testMethod)
    {
        var executionBrowserAttribute = testMethod.GetCustomAttribute<LocalExecutionAttribute>(true);
        return executionBrowserAttribute?.BrowserConfiguration;
    }

    private BrowserConfiguration GetExecutionBrowserClassLevel(Type testClass)
    {
        var executionBrowserAttribute = testClass.GetCustomAttribute<LocalExecutionAttribute>(true);
        return executionBrowserAttribute?.BrowserConfiguration;
    }

    private GridSettings GetLambdaTestMethodLevel(MemberInfo testMethod)
    {
        var gridAttribute = testMethod.GetCustomAttribute<LambdaTestAttribute>(true);
        return gridAttribute?.GridSettings;
    }

    private GridSettings GetLambdaTestClassLevel(Type testClass)
    {
        var gridAttribute = testClass.GetCustomAttribute<LambdaTestAttribute>(true);
        return gridAttribute?.GridSettings;
    }
}