using PolarisLite.Core;
using PolarisLite.Core.Infrastructure.MSTest;
using PolarisLite.Mobile.Plugins;

namespace PolarisLite.Mobile.Core.MSTest;
public class AndroidTest : BaseTest
{
    private static bool _arePluginsAlreadyInitialized;

    public AndroidTest()
    {
        App = new App();
    }

    public App App { get; set; }

    protected override void Configure()
    {
        if (!_arePluginsAlreadyInitialized)
        {
            PluginExecutionEngine.AddPlugin(new AppLifecyclePlugin());
            _arePluginsAlreadyInitialized = true;
        }
    }
}
