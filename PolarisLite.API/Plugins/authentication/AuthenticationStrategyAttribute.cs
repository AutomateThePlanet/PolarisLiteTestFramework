using RestSharp.Authenticators;

namespace PolarisLite.API;

[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public abstract class AuthenticationStrategyAttribute : Attribute
{
    public abstract IAuthenticator GetAuthenticator();
}