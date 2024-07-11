using PolarisLite.Core;
using PolarisLite.Core.Plugins;
using PolarisLite.Web.Core;
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

    private void RestartBrowser()
    {
        try
        {
            DriverFactory.Dispose();

            if (_currentBrowserConfiguration.ExecutionType == ExecutionType.Local)
            {
                DriverFactory.Start(_currentBrowserConfiguration);
            }
            else
            {
                // TODO: To be implemented
            }
        } catch (Exception ex)
        {
            Console.WriteLine(ex);
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

    private BrowserConfiguration GetBrowserConfiguration(MemberInfo testMethod)
    {
        var classBrowser = GetLocalExecutionAttributeClassLevel(testMethod.DeclaringType);
        var methodBrowser = GetLocalExecutionAttributeMethodLevel(testMethod);
        BrowserConfiguration browserConfiguration = methodBrowser != null ? methodBrowser : classBrowser;
        
        return browserConfiguration;
    }

    private BrowserConfiguration GetLocalExecutionAttributeMethodLevel(MemberInfo testMethod)
    {
        var LocalExecutionAttributeAttribute = testMethod.GetCustomAttribute<LocalExecutionAttribute>(true);
        return LocalExecutionAttributeAttribute?.BrowserConfiguration;
    }

    private BrowserConfiguration GetLocalExecutionAttributeClassLevel(Type testClass)
    {
        var LocalExecutionAttributeAttribute = testClass.GetCustomAttribute<LocalExecutionAttribute>(true);
        return LocalExecutionAttributeAttribute?.BrowserConfiguration;
    }
}