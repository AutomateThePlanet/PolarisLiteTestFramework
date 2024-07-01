using PolarisLite.Core;
using PolarisLite.Core.Plugins;
using PolarisLite.Web.Plugins.BrowserExecution;
using System.Reflection;

namespace PolarisLite.Web.Plugins;
public class BrowserLifecyclePlugin : Plugin
{
    private readonly DriverFactory _driverFactory;
    private BrowserConfiguration _currentBrowserConfiguration;
    private BrowserConfiguration _previousBrowserConfiguration;
    private GridConfiguration _currentGridConfiguration;

    public BrowserLifecyclePlugin()
    {
        _driverFactory = new DriverFactory();
    }

    public override void OnBeforeTestInitialize(MethodInfo memberInfo)
    {
        _currentBrowserConfiguration = GetBrowserConfiguration(memberInfo);
        _currentGridConfiguration = GetGridSettingsConfiguration(memberInfo);
        bool shouldRestartBrowser = ShouldRestartBrowser(_currentBrowserConfiguration);
        if (shouldRestartBrowser)
        {
            RestartBrowser();
        }

        _previousBrowserConfiguration = _currentBrowserConfiguration;
    }

    private void RestartBrowser()
    {
        try
        {
            _driverFactory.Dispose();

            if (_currentBrowserConfiguration.ExecutionType == ExecutionType.Regular)
            {
                _driverFactory.Start(_currentBrowserConfiguration);
            }
            else
            {
                _driverFactory.StartGrid(_currentBrowserConfiguration, _currentGridConfiguration);
            }
        } catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private void ShutdownBrowser()
    {
        _driverFactory.Dispose();
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

    private GridConfiguration GetGridSettingsConfiguration(MemberInfo testMethod)
    {
        var classGridSettings = GetLambdaTestClassLevel(testMethod.DeclaringType);
        var methodGridSettings = GetLambdaTestMethodLevel(testMethod);
        GridConfiguration gridSettings = methodGridSettings != null ? methodGridSettings : classGridSettings;

        return gridSettings;
    }

    private BrowserConfiguration GetExecutionBrowserMethodLevel(MemberInfo testMethod)
    {
        var executionBrowserAttribute = testMethod.GetCustomAttribute<BrowserAttribute>(true);
        return executionBrowserAttribute?.BrowserConfiguration;
    }

    private BrowserConfiguration GetExecutionBrowserClassLevel(Type testClass)
    {
        var executionBrowserAttribute = testClass.GetCustomAttribute<BrowserAttribute>(true);
        return executionBrowserAttribute?.BrowserConfiguration;
    }

    private GridConfiguration GetLambdaTestMethodLevel(MemberInfo testMethod)
    {
        var gridAttribute = testMethod.GetCustomAttribute<LambdaTestAttribute>(true);
        return gridAttribute?.GridSettings;
    }

    private GridConfiguration GetLambdaTestClassLevel(Type testClass)
    {
        var gridAttribute = testClass.GetCustomAttribute<LambdaTestAttribute>(true);
        return gridAttribute?.GridSettings;
    }
}