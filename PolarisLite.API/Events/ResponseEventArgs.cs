using RestSharp;

namespace PolarisLite.API;

public class ResponseEventArgs
{
    public ResponseEventArgs(RestResponse response, string requestUri)
    {
        Response = response;
        RequestUri = requestUri;
    }

    public RestResponse Response { get; }
    public string RequestUri { get; }
}