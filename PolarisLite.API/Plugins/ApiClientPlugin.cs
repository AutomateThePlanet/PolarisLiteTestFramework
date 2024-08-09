namespace PolarisLite.API;

public class ApiClientPlugin
{
    public virtual void Enable()
    {
        ApiClientPluginExecutionEngine.OnClientInitializedEvent += OnClientInitialized;
        ApiClientPluginExecutionEngine.OnRequestTimeoutEvent += OnRequestTimeout;
        ApiClientPluginExecutionEngine.OnMakingRequestEvent += OnMakingRequest;
        ApiClientPluginExecutionEngine.OnRequestMadeEvent += OnRequestMade;
        ApiClientPluginExecutionEngine.OnRequestFailedEvent += OnRequestFailed;
    }

    public virtual void Disable()
    {
        ApiClientPluginExecutionEngine.OnClientInitializedEvent -= OnClientInitialized;
        ApiClientPluginExecutionEngine.OnRequestTimeoutEvent -= OnRequestTimeout;
        ApiClientPluginExecutionEngine.OnMakingRequestEvent -= OnMakingRequest;
        ApiClientPluginExecutionEngine.OnRequestMadeEvent -= OnRequestMade;
        ApiClientPluginExecutionEngine.OnRequestFailedEvent -= OnRequestFailed;
    }

    protected virtual void OnClientInitialized(object sender, ClientEventArgs client)
    {
    }

    protected virtual void OnRequestTimeout(object sender, ClientEventArgs client)
    {
    }

    protected virtual void OnMakingRequest(object sender, RequestEventArgs client)
    {
    }

    protected virtual void OnRequestMade(object sender, ResponseEventArgs client)
    {
    }

    protected virtual void OnRequestFailed(object sender, ResponseEventArgs client)
    {
    }
}
