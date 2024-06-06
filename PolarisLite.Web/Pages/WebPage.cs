using PolarisLite.Web.Services;

namespace PolarisLite.Web;
public abstract class WebPage
{
    protected readonly DriverAdapter Driver;

    public WebPage(DriverAdapter driver)
    {
        Driver = driver;
    }
}
