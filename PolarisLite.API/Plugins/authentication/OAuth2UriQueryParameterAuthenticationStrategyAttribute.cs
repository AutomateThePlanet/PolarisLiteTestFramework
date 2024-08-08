using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth2;

namespace PolarisLite.API;

/// <summary>
///     The OAuth 2 authenticator using URI query parameter.
/// </summary>
/// <remarks>
///     Based on http://tools.ietf.org/html/draft-ietf-oauth-v2-10#section-5.1.2.
/// </remarks>
public class OAuth2UriQueryParameterAuthenticationStrategyAttribute : AuthenticationStrategyAttribute
{
    private readonly string _accessToken;

    public OAuth2UriQueryParameterAuthenticationStrategyAttribute(string accessToken) => _accessToken = accessToken;

    public override IAuthenticator GetAuthenticator() => new OAuth2UriQueryParameterAuthenticator(_accessToken);
}