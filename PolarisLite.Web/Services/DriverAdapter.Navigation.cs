namespace PolarisLite.Web.Services;

public partial class DriverAdapter : INavigationService
{
    public string Url => _webDriver.Url;

    public void GoToUrl(string url)
    {
        _webDriver.Navigate().GoToUrl(url);
    }
}
