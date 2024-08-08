using Polaris.Plugins.Common.ExecutionTime;
using PolarisLite.API;
using PolarisLite.API.Plugins;
using PolarisLite.Core;
using PolarisLite.Core.Infrastructure.NUnit;

namespace Polaris.API.NUnit;

public abstract class APITest : BaseTest
{
    private static bool _arePluginsAlreadyInitialized;

    public App App => new App();

    protected override void ClassCleanup()
    {
        App.ApiClient.Dispose();
    }

    protected override void Configure()
    {
        if (!_arePluginsAlreadyInitialized)
        {
            PluginExecutionEngine.AddPlugin(new LogLifecyclePlugin());
            PluginExecutionEngine.AddPlugin(new ExecutionTimeUnderPlugin());
            PluginExecutionEngine.AddPlugin(new RetryFailedRequestsWorkflowPlugin());
            PluginExecutionEngine.AddPlugin(new RetryFailedRequestsWorkflowPlugin());

            App.AddApiClientExecutionPlugin<ApiBddPlugin>();
            App.AddAssertionsEventHandler<BDDLoggingAssertExtensions>();
            //ExecutionTimePlugin.Add();
            //APIPluginsConfiguration.AddAssertExtensionsBddLogging();
            //APIPluginsConfiguration.AddRetryFailedRequests();

            _arePluginsAlreadyInitialized = true;
        }
    }
}
