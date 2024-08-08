namespace PolarisLite.API;

public class RequestEventArgs
{
    public RequestEventArgs(RestRequest request, string requestUri)
    {
        Request = request;
        RequestResource = requestUri;
    }

    public RestRequest Request { get; }
    public string RequestResource { get; }
}