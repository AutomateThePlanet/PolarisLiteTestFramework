namespace PolarisLite.Web;

public interface INavigationService
{
    void GoToUrl(string url);
    string Url { get; }
}
