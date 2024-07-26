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

    public TPage Create<TPage>()
       where TPage : WebPage, new()
    {
        TPage pageInstance = new TPage();
        return pageInstance;
    }

    public TPage GoTo<TPage>()
       where TPage : NavigatablePage, new()
    {
        TPage pageInstance = new TPage();
        pageInstance.Open();
        return pageInstance;
    }
}