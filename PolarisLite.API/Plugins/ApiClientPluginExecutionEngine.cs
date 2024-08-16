namespace PolarisLite.API;

public static class ApiClientPluginExecutionEngine
{
    public static event EventHandler<ClientEventArgs> OnClientInitializedEvent;
    public static event EventHandler<ClientEventArgs> OnRequestTimeoutEvent;
    public static event EventHandler<RequestEventArgs> OnMakingRequestEvent;
    public static event EventHandler<ResponseEventArgs> OnRequestMadeEvent;
    public static event EventHandler<ResponseEventArgs> OnRequestFailedEvent;

    public static void OnClientInitialized(RestClient client)
    {
        OnClientInitializedEvent?.Invoke(client, new ClientEventArgs(client));
    }

    public static void OnRequestTimeout(RestClient client)
    {
        OnRequestTimeoutEvent?.Invoke(client, new ClientEventArgs(client));
    }

    public static void OnMakingRequest(RestRequest request, string requestUri)
    {
        OnMakingRequestEvent?.Invoke(request, new RequestEventArgs(request, requestUri));
    }

    public static void OnRequestMade(RestResponse response, string requestUri)
    {
        OnRequestMadeEvent?.Invoke(response, new ResponseEventArgs(response, requestUri));
    }

    public static void OnRequestFailed(RestResponse response, string requestUri)
    {
        OnRequestFailedEvent?.Invoke(response, new ResponseEventArgs(response, requestUri));
    }
}
