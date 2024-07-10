using PolarisLite.Core;
using PolarisLite.Core.Infrastructure.NUnit;
using PolarisLite.Web.Plugins;

namespace PolarisLite.Web.Core.NUnit;
public class WebTest : BaseTest
{
    private static bool _arePluginsAlreadyInitialized;

    public App App => new App();

    protected override void Configure()
    {
        if (!_arePluginsAlreadyInitialized)
        {
            PluginExecutionEngine.AddPlugin(new BrowserLifecyclePlugin());
            WebComponentPluginExecutionEngine.AddPlugin(new HighlightElementPlugin());
            WebComponentPluginExecutionEngine.AddPlugin(new ScrollIntoViewPlugin());
            _arePluginsAlreadyInitialized = true;
        }
    }
}