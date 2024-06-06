using PolarisLite.Core;
using PolarisLite.Core.Infrastructure.NUnit;
using PolarisLite.Web.Plugins;
using PolarisLite.Web.Services;

namespace PolarisLite.Web.Core.NUnit;
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