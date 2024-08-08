using RestSharp.Authenticators;

namespace PolarisLite.API;

public class HttpBasicAuthenticationStrategyAttribute : AuthenticationStrategyAttribute
{
    private readonly string _userName;
    private readonly string _password;

    public HttpBasicAuthenticationStrategyAttribute(string username, string password)
    {
        _userName = username;
        _password = password;
    }

    public override IAuthenticator GetAuthenticator() => new HttpBasicAuthenticator(_userName, _password);
}