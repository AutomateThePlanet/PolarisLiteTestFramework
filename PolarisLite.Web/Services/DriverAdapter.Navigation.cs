namespace PolarisLite.Web.Services;

public partial class DriverAdapter : INavigationService
{
    public string Url => WrappedDriver.Url;

    public void GoToUrl(string url)
    {
        WrappedDriver.Navigate().GoToUrl(url);
    }
}
