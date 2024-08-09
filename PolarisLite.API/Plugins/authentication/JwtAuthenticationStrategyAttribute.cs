using RestSharp.Authenticators;

namespace PolarisLite.API;

/// <summary>
///     JSON WEB TOKEN (JWT) Authenticator class.
///     <remarks>https://tools.ietf.org/html/draft-ietf-oauth-json-web-token</remarks>
/// </summary>
public class JwtAuthenticationStrategyAttribute : AuthenticationStrategyAttribute
{
    private readonly string _accessToken;

    public JwtAuthenticationStrategyAttribute(string accessToken) => _accessToken = accessToken;

    public override IAuthenticator GetAuthenticator() => new JwtAuthenticator(_accessToken);
}