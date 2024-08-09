using System.Reflection;
using PolarisLite.Core;
using RestSharp.Authenticators;

namespace PolarisLite.API;

public class ApiAuthenticationWorkflowPlugin : Plugin
{
    public override void OnBeforeTestClassInitialize(Type type)
    {
        var authenticator = GetAuthenticatorByType(type);
        if (authenticator != null)
        {
            ApiClientService.Authenticator = authenticator;
        }
    }

    private IAuthenticator GetAuthenticatorByType(Type currentType)
    {
        if (currentType == null)
        {
            throw new ArgumentNullException();
        }

        var authenticationClassAttribute = currentType.GetCustomAttribute<AuthenticationStrategyAttribute>(true);
        return authenticationClassAttribute?.GetAuthenticator();
    }
}