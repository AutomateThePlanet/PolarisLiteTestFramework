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

    public BrowserLifecyclePlugin()
    {
        _driverFactory = new DriverFactory();
    }

    public override void OnBeforeTestInitialize(MethodInfo memberInfo)
    {
        _currentBrowserConfiguration = GetBrowserConfiguration(memberInfo);
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
                _driverFactory.Start(_currentBrowserConfiguration.Browser);
            }
            else
            {
                _driverFactory.StartGrid();
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

        var webSettings = ConfigurationService.GetSection<WebSettings>();
        if (browserConfiguration == null)
        {
            browserConfiguration = new BrowserConfiguration(webSettings.DefaultBrowser, webSettings.DefaultLifeCycle, webSettings.ExecutionType);
        }
        
        return browserConfiguration;
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
}