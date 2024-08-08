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

    protected override void Configure()
    {
        if (!_arePluginsAlreadyInitialized)
        {
            PluginExecutionEngine.AddPlugin(new LogLifecyclePlugin());
            PluginExecutionEngine.AddPlugin(new ExecutionTimeUnderPlugin());
            new ApiBddPlugin().Enable();
            //ExecutionTimePlugin.Add();
            //APIPluginsConfiguration.AddAssertExtensionsBddLogging();
            //APIPluginsConfiguration.AddRetryFailedRequests();

            _arePluginsAlreadyInitialized = true;
        }
    }
}
