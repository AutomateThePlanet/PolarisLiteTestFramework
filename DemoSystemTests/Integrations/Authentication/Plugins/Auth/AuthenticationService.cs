using DemoSystemTests.Integrations.Authentication.Factories;
using DemoSystemTests.Integrations.Authentication.Models;
using DemoSystemTests.Integrations.Authentication.Services;
using PolarisLite.Integrations;
using PolarisLite.Web;
using System.Text.RegularExpressions;

namespace DemoSystemTests.Integrations.Authentication.Plugins.Auth;

public class AuthenticationService
{
    private readonly AuthenticationConfiguration _config;

    public AuthenticationService(AuthenticationConfiguration config)
    {
        _config = config;
    }

    public App App => new App();
    public static TestUser TestUser { get; private set; }

    public void AuthenticateUser()
    {
        App.Navigation.GoToUrl("https://chesstv.local:3000/");
        switch (_config.AuthType)
        {
            case AuthType.PASSWORDLESS_EMAIL_NO_2FA:
                AuthenticatePasswordlessEmail();
                break;
            case AuthType.PASSWORDLESS_SMS_NO_2FA:
                AuthenticatePasswordlessSms();
                break;
            case AuthType.EMAIL_PASSWORD_NO_2FA:
                AuthenticateEmailPasswordNo2FA();
                break;
            case AuthType.EMAIL_PASSWORD_2FA:
                AuthenticateEmailPassword2FA();
                break;
            case AuthType.SSO_GOOGLE:
            case AuthType.SSO_FACEBOOK:
                throw new NotImplementedException($"{_config.AuthType} authentication is not implemented yet.");
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void AuthenticatePasswordlessEmail()
    {
        TestUser = GetUser();
        var inbox = TestUser.UserInbox ?? MailslurpService.CreateInbox();
        var email = inbox.EmailAddress;

        App.Navigation.GoToUrl("https://localhost:3000/");
        var emailTab = App.Elements.FindByXPath<Button>("//a[text()='Email']");
        emailTab.Click();

        var emailInput = App.Elements.FindById<TextField>("email");
        emailInput.TypeText(email);

        var sendLoginCode = App.Elements.FindByXPath<Button>("//button[text()='Send Login Code']");
        sendLoginCode.Click();

        var receivedEmail = MailslurpService.WaitForLatestEmail(inbox, DateTime.UtcNow.AddMinutes(-5));
        var emailCode = ExtractCode(receivedEmail.Body);

        var emailCodeInput = App.Elements.FindById<TextField>("code");
        emailCodeInput.TypeText(emailCode);

        var verifyButton = App.Elements.FindByXPath<Button>("//button[text()='Verify Code']");
        verifyButton.Click();

        VerifyUserLoggedIn(TestUser.Username);
    }

    private void AuthenticatePasswordlessSms()
    {
        TestUser = GetUser();

        var smsListener = TwillioService.ListenForSms();

        App.Navigation.GoToUrl("https://localhost:3000/");
        var smsTab = App.Elements.FindByXPath<Button>("//a[text()='SMS']");
        smsTab.Click();

        var phoneInput = App.Elements.FindById<TextField>("phoneNumber");
        phoneInput.TypeText(_config.User);

        var sendSmsButton = App.Elements.FindByXPath<Button>("//button[text()='Send SMS Code']");
        sendSmsButton.Click();

        var latestMessage = TwillioService.GetLastMessage(smsListener);
        var smsCode = ExtractCode(latestMessage.Body);

        var smsCodeInput = App.Elements.FindById<TextField>("smsCode");
        smsCodeInput.TypeText(smsCode);

        var verifyButton = App.Elements.FindByXPath<Button>("//button[text()='Verify SMS Code']");
        verifyButton.Click();

        VerifyUserLoggedIn(TestUser.Username);
    }

    private void AuthenticateEmailPassword2FA()
    {
        TestUser = GetUserWith2FA();
        var loginTab = App.Elements.FindByXPath<Button>("//a[text()='Login']");
        loginTab.Click();

        var emailInput = App.Elements.FindById<TextField>("usernameOrEmail");
        emailInput.TypeText(TestUser.Email);

        var passwordInput = App.Elements.FindById<Password>("password");
        passwordInput.SetPassword(TestUser.Password);

        var rememberMeCheckbox = App.Elements.FindById<CheckBox>("rememberMe");
        rememberMeCheckbox.Check(true);

        ByPassCaptcha();

        var loginButton = App.Elements.FindByXPath<Button>("//button[text()='Login']");
        loginButton.Click();

        var twoFaCode = AuthBypassService.Generate2FATokenAsync(TestUser.Id).Result;
        var twoFACodeInput = App.Elements.FindById<TextField>("twoFaToken");
        twoFACodeInput.TypeText(twoFaCode);

        loginButton = App.Elements.FindByXPath<Button>("//button[text()='Login']");
        loginButton.Click();

        VerifyUserLoggedIn(TestUser.Username);
    }

    private void AuthenticateEmailPasswordNo2FA()
    {
        TestUser = GetUser();
        var loginTab = App.Elements.FindByXPath<Button>("//a[text()='Login']");
        loginTab.Click();

        var emailInput = App.Elements.FindById<TextField>("usernameOrEmail");
        emailInput.TypeText(_config.User);
       
        var passwordInput = App.Elements.FindById<Password>("password");
        passwordInput.SetPassword("password123");

        ByPassCaptcha();
        var loginButton = App.Elements.FindByXPath<Button>("//button[text()='Login']");
        loginButton.Click();

        VerifyUserLoggedIn(_config.User);
    }

    private void ByPassCaptcha()
    {
        var captchaByPass = App.Elements.FindByXPath<TextField>("//input[@name='captcha-bypass']");
        App.JavaScript.Execute("arguments[0].setAttribute('value', arguments[1]);", captchaByPass, "10685832-cd90-4e91-9224-2ef69ce88f53");
    }

    private TestUser GetUser()
    {
        if (_config.UseNewUser)
        {
            return  TestUserFactory.CreateDefaultWithRealEmailAsync().Result;
        }

        if (_config.UseExistingUser)
        {
            throw new NotImplementedException("Using an existing user is not implemented yet.");
        }

        throw new ArgumentException("Invalid configuration for user creation.");
    }

    private TestUser GetUserWith2FA()
    {
        if (_config.UseNewUser)
        {
            return TestUserFactory.CreateDefault2FAWithRealEmailAsync().Result;
        }

        if (_config.UseExistingUser)
        {
            throw new NotImplementedException("Using an existing user with 2FA is not implemented yet.");
        }

        throw new ArgumentException("Invalid configuration for user creation.");
    }

    private void VerifyUserLoggedIn(string expectedUsername)
    {
        var userName = App.Elements.FindById<Label>("username");
        if (!userName.Text.Contains(expectedUsername))
        {
            throw new Exception("User login failed.");
        }
    }

    private string ExtractCode(string message)
    {
        var pattern = @"code is: (\d+)$";
        var match = Regex.Match(message, pattern);
        return match.Success ? match.Groups[1].Value : null;
    }
}
