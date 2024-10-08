﻿namespace DemoSystemTests.Integrations.Plugins.Auth;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class AuthenticationAttribute : Attribute
{
    public AuthenticationAttribute(AuthType authType = AuthType.EMAIL_PASSWORD_2FA, bool useNewUser = true, bool useExistingUser = false, string user = null)
    {
        AuthenticationConfiguration = new AuthenticationConfiguration();
        AuthenticationConfiguration.AuthType = authType;
        AuthenticationConfiguration.UseNewUser = useNewUser;
        AuthenticationConfiguration.UseExistingUser = useExistingUser;
        AuthenticationConfiguration.User = user;
    }

    public AuthenticationConfiguration AuthenticationConfiguration { get; set; }
}
