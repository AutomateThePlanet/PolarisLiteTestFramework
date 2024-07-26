using PolarisLite.Web.Services.Contracts;

namespace PolarisLite.Web;

public interface IDriverAdapter : IDevToolsService, INavigationService, IBrowserService, ICookiesService, IElementFindService, IJavaScriptService, IInteractionsService
{
}