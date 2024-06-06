using PolarisLite.Web.Components;

namespace PolarisLite.Web.Services;

public partial class DriverAdapter : ICookiesService
{
    public void AddCookie(string cookieName, string cookieValue, string path = "/") => throw new NotImplementedException();

    public void DeleteAllCookies()
    {
        _webDriver.Manage().Cookies.DeleteAllCookies();
    }

    public void DeleteCookie(string cookieName) => throw new NotImplementedException();
    public List<Cookie> GetAllCookies() => throw new NotImplementedException();
    public string GetCookie(string cookieName) => throw new NotImplementedException();
}
