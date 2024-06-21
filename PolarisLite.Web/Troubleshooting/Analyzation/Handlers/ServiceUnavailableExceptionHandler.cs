namespace PolarisLite.Web.Troubshoting;
public class ServiceUnavailableExceptionHandler : HtmlSourceExceptionHandler
{
    public ServiceUnavailableExceptionHandler()
        : base("HTTP Error 503. The service is unavailable.", "IT IS NOT A TEST PROBLEM. THE PAGE DOES NOT EXIST.")
    {
    }
}
