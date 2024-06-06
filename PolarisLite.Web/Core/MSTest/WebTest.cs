using PolarisLite.Core;
using PolarisLite.Core.Infrastructure.MSTest;
using PolarisLite.Web.Plugins;

namespace PolarisLite.Web.Core.MSTest;
public class WebTest : BaseTest
{
    private static bool _arePluginsAlreadyInitialized;

    public WebTest()
    {
        App = new App();
    }

    public App App { get; set; }

    protected override void Configure()
    {
        if (!_arePluginsAlreadyInitialized)
        {
            PluginExecutionEngine.AddPlugin(new BrowserLifecyclePlugin());
            _arePluginsAlreadyInitialized = true;
        }
    }
}
