namespace DemoSystemTests.Integrations.Plugins.Auth;
public class AuthenticationConfiguration
{
    public AuthenticationConfiguration()
    {
    }

    public AuthenticationConfiguration(AuthType authType, bool useNewUser, bool useExistingUser, string user)
    {
        AuthType = authType;
        UseNewUser = useNewUser;
        UseExistingUser = useExistingUser;
        User = user;
    }

    public AuthType AuthType { get; set; }
    public bool UseNewUser { get; set; }
    public bool UseExistingUser { get; set; }
    public string User { get; set; }
}
