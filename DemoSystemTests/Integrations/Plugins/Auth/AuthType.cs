namespace DemoSystemTests.Integrations.Plugins.Auth;
public enum AuthType
{
    PASSWORDLESS_EMAIL_NO_2FA,
    PASSWORDLESS_SMS_NO_2FA,
    EMAIL_PASSWORD_NO_2FA,
    EMAIL_PASSWORD_2FA,
    SSO_GOOGLE,
    SSO_FACEBOOK,
}
