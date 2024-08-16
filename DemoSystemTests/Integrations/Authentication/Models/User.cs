namespace DemoSystemTests.Integrations.Authentication.Models;
public class User
{
    public string FirstName { get; set; }
    public string UserName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    public string Password { get; set; }
    public string PasswordConfirm { get; set; }
    public bool? ShouldSubscribe { get; set; }
    public bool? AgreedPrivacyPolicy { get; set; }
}
