namespace PolarisLite.Web.Services;

public partial class DriverAdapter : ICookiesService
{
    public void AddCookie(string cookieName, string cookieValue, string path = "/")
    {
        _webDriver.Manage().Cookies.AddCookie(new Cookie(cookieName, cookieValue, path));
    }

    public void DeleteAllCookies()
    {
        _webDriver.Manage().Cookies.DeleteAllCookies();
    }

    public void DeleteCookie(string cookieName)
    {
        _webDriver.Manage().Cookies.DeleteCookie(GetAllCookies().First(c => c.Name.Equals(cookieName)));
    }

    public List<OpenQA.Selenium.Cookie> GetAllCookies()
    {
       return _webDriver.Manage().Cookies.AllCookies.ToList();
    }

    public Cookie GetCookie(string cookieName)
    {
        return _webDriver.Manage().Cookies.GetCookieNamed(cookieName);
    }
}
