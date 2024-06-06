using PolarisLite.Core;
using PolarisLite.Core.Infrastructure.NUnit;
using PolarisLite.Web.Plugins;

namespace PolarisLite.Web.Core.NUnit;
public class WebTest : BaseTest
{
    private static bool _arePluginsAlreadyInitialized;
    public DriverAdapter Driver => new DriverAdapter();

    protected override void Configure()
    {
        if (!_arePluginsAlreadyInitialized)
        {
            PluginExecutionEngine.AddPlugin(new BrowserLifecyclePlugin());
            _arePluginsAlreadyInitialized = true;
        }
    }
}