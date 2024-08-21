using PolarisLite.Core;
using PolarisLite.Core.Plugins;
using PolarisLite.Web;
using System.Reflection;

namespace DemoSystemTests.Integrations.Plugins.Auth;
public class AuthenticationPlugin : Plugin
{
    public App App => new App();

    public override void OnBeforeTestInitialize(MethodInfo memberInfo)
    {
        var authenticationConfiguration = GetAuthenticationConfiguration(memberInfo);

        new AuthenticationService(authenticationConfiguration).AuthenticateUser();
    }

    public override void OnBeforeTestCleanup(TestOutcome result, MethodInfo memberInfo)
    {
        App.Cookies.DeleteAllCookies();
    }

    private AuthenticationConfiguration GetAuthenticationConfiguration(MemberInfo testMethod)
    {
        var classBrowser = GetAuthenticationConfigurationClassLevel(testMethod.DeclaringType);
        var methodBrowser = GetAuthenticationConfigurationMethodLevel(testMethod);
        AuthenticationConfiguration authConfiguration = methodBrowser != null ? methodBrowser : classBrowser;

        return authConfiguration;
    }

    private AuthenticationConfiguration GetAuthenticationConfigurationClassLevel(Type testClass)
    {
        var authenticationAttribute = testClass.GetCustomAttribute<AuthenticationAttribute>(true);
        return authenticationAttribute?.AuthenticationConfiguration;
    }

    private AuthenticationConfiguration GetAuthenticationConfigurationMethodLevel(MemberInfo testMethod)
    {
        var authenticationAttribute = testMethod.GetCustomAttribute<AuthenticationAttribute>(true);
        return authenticationAttribute?.AuthenticationConfiguration;
    }
}
