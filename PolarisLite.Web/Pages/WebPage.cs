using PolarisLite.Web.Services;

namespace PolarisLite.Web;
public abstract class WebPage
{
    protected readonly DriverAdapter Driver;

    public WebPage(DriverAdapter driver)
    {
        Driver = driver;
        App = new App();
    }

    public App App { get; set; }
}
