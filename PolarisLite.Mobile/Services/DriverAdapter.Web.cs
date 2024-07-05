using PolarisLite.Web;

namespace PolarisLite.Mobile.Services;

public partial class DriverAdapter : IWebService
{
    public Web.IElementFindService Elements => _webDriverAdapter;
    public IBrowserService Browser => _webDriverAdapter;
    public INavigationService Navigation => _webDriverAdapter;
    public ICookiesService Cookies => _webDriverAdapter;
    public IInteractionsService Interactions => _webDriverAdapter;
    public IJavaScriptService JavaScript => _webDriverAdapter;
}
