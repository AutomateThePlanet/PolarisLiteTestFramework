using PolarisLite.Secrets;
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth2;

namespace PolarisLite.API;

/// <summary>
///     The OAuth 2 authenticator using the authorization request header field.
/// </summary>
/// <remarks>
///     Based on http://tools.ietf.org/html/draft-ietf-oauth-v2-10#section-5.1.1.
/// </remarks>
public class OAuth2AuthorizationRequestHeaderAuthenticationStrategyAttribute2 : AuthenticationStrategyAttribute
{
    private readonly string _accessToken;

    public OAuth2AuthorizationRequestHeaderAuthenticationStrategyAttribute2(string _accessTokenEnvName)
    {
        // instead of using API settings since there will be multiple tokens and APIs
        _accessToken = SecretsResolver.GetSecret(_accessTokenEnvName);
    }

    public override IAuthenticator GetAuthenticator() => new OAuth2AuthorizationRequestHeaderAuthenticator(_accessToken);
}