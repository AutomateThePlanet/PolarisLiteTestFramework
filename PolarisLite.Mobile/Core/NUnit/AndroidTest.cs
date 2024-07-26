using Bellatrix.Mobile.Plugins;
using PolarisLite.Core;
using PolarisLite.Core.Infrastructure.NUnit;
using PolarisLite.Mobile.Plugins;

namespace PolarisLite.Mobile.Core.NUnit;
public class AndroidTest : BaseTest
{
    private static bool _arePluginsAlreadyInitialized;
    public App App => new App();

    protected override void Configure()
    {
        if (!_arePluginsAlreadyInitialized)
        {
            PluginExecutionEngine.AddPlugin(new LambdaTestResultsPlugin());
            PluginExecutionEngine.AddPlugin(new AppLifecyclePlugin());
            _arePluginsAlreadyInitialized = true;
        }
    }
}