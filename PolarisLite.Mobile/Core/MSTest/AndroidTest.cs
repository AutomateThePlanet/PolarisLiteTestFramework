using PolarisLite.Core;
using PolarisLite.Core.Infrastructure.MSTest;
using PolarisLite.Mobile.Plugins;

namespace PolarisLite.Mobile.Core.MSTest;
public class AndroidTest : BaseTest
{
    private static bool _arePluginsAlreadyInitialized;

    public App App => new App();

    protected override void Configure()
    {
        if (!_arePluginsAlreadyInitialized)
        {
            PluginExecutionEngine.AddPlugin(new AppLifecyclePlugin());
            _arePluginsAlreadyInitialized = true;
        }
    }
}
