using PolarisLite.Core;
using PolarisLite.Core.Infrastructure.NUnit;
using PolarisLite.Mobile.Plugins;
using PolarisLite.Mobile.Services;

namespace PolarisLite.Mobile.Core.NUnit;
public class AndroidTest : BaseTest
{
    private static bool _arePluginsAlreadyInitialized;
    //public WebTest()
    //{
    //    App = new App();
    //}

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