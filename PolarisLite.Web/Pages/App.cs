using PolarisLite.Web.Services;

namespace PolarisLite.Web;
public class App
{
    private readonly DriverAdapter _driver;

    public App(DriverAdapter driver)
    {
        _driver = driver;
        // add or register plugins
        // TODO: add app to base tests?
        // TODO: add webTest derive from BaseTest
    }
}
