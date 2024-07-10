using PolarisLite.Web.Services;

namespace PolarisLite.Web;

public interface IDriverAdapter : INavigationService, ICookiesService, IElementFindService, IJavaScriptService, IInteractionsService
{
}