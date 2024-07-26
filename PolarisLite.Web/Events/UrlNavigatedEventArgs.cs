namespace PolarisLite.Web.Events;
public class UrlNavigatedEventArgs
{
    public UrlNavigatedEventArgs(string url) => Url = url;

    public string Url { get; }
}
