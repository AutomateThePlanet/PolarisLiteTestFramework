namespace PolarisLite.Web;

public interface ICookiesService
{
    void AddCookie(string cookieName, string cookieValue, string path = "/");

    void DeleteAllCookies();

    void DeleteCookie(string cookieName);

    List<OpenQA.Selenium.Cookie> GetAllCookies();

    Cookie GetCookie(string cookieName);
}
