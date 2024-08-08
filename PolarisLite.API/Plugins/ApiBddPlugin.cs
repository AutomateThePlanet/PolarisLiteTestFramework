using PolarisLite.Logging;

namespace PolarisLite.API;

public class ApiBddPlugin : ApiClientPlugin
{
    public override void Enable()
    {
        if (ApiSettings.EnableBDDLogging)
        {
            base.Enable();
        }
    }

    public override void Disable()
    {
        if (ApiSettings.EnableBDDLogging)
        {
            base.Disable();
        }
    }

    protected override void OnRequestTimeout(object sender, ClientEventArgs client)
    {
        Logger.LogInfo("Request was not executed in the specified timeout.");
    }

    protected override void OnMakingRequest(object sender, RequestEventArgs requestEventArgs)
    {
        var sb = new StringBuilder();
        sb.Append($"Making {requestEventArgs.Request.Method} request against resource {requestEventArgs.RequestResource}");
        if (requestEventArgs.Request.Parameters != null && requestEventArgs.Request.Parameters.Count > 0)
        {
            sb.Append(" with parameters ");
            foreach (var param in requestEventArgs.Request.Parameters)
            {
                sb.Append($"{param.Name}={param.Value} ");
            }
        }

        Logger.LogInfo(sb.ToString().TrimEnd());
    }

    protected override void OnRequestMade(object sender, ResponseEventArgs responseEventArgs)
    {
        Logger.LogInfo($"Response of request {responseEventArgs.Response.Request.Method} against resource {responseEventArgs.RequestUri} - {responseEventArgs.Response.ResponseStatus}");
    }

    protected override void OnRequestFailed(object sender, ResponseEventArgs responseEventArgs)
    {
        Logger.LogInfo($"Request Failed {responseEventArgs.Response.Request.Method} on URL {responseEventArgs.RequestUri} - {responseEventArgs.Response.ResponseStatus} {responseEventArgs.Response.ErrorMessage}");
    }
}