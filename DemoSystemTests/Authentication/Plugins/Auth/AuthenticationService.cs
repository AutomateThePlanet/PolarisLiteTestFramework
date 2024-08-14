using DemoSystemTests.Authentication.Factories;
using DemoSystemTests.Authentication.Models;
using DemoSystemTests.Authentication.Services;
using PolarisLite.Integrations;
using PolarisLite.Web;
using System.Text.RegularExpressions;

namespace DemoSystemTests.Authentication.Plugins.Auth;

public class AuthenticationService
{
    private readonly AuthenticationConfiguration _config;

    public AuthenticationService(AuthenticationConfiguration config)
    {
        _config = config;
    }

    public App App => new App();

    public async Task AuthenticateUser()
    {
        switch (_config.AuthType)
        {
            case AuthType.PASSWORDLESS_EMAIL_NO_2FA:
                await AuthenticatePasswordlessEmailAsync();
                break;
            case AuthType.PASSWORDLESS_SMS_NO_2FA:
                await AuthenticatePasswordlessSmsAsync();
                break;
            case AuthType.EMAIL_PASSWORD_NO_2FA:
                AuthenticateEmailPasswordNo2FA();
                break;
            case AuthType.EMAIL_PASSWORD_2FA:
                await AuthenticateEmailPassword2FAAsync();
                break;
            case AuthType.SSO_GOOGLE:
            case AuthType.SSO_FACEBOOK:
                throw new NotImplementedException($"{_config.AuthType} authentication is not implemented yet.");
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private async Task AuthenticatePasswordlessEmailAsync()
    {
        var testUser = await GetUserAsync();
        var inbox = testUser.UserInbox ?? MailslurpService.CreateInbox();
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

        VerifyUserLoggedIn(testUser.Username);
    }

    private async Task AuthenticatePasswordlessSmsAsync()
    {
        var testUser = await GetUserAsync();

        var smsListener = TwillioService.ListenForSms(_config.User);

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

        VerifyUserLoggedIn(testUser.Username);
    }

    private async Task AuthenticateEmailPassword2FAAsync()
    {
        var testUser = await GetUserWith2FAAsync();
        var loginTab = App.Elements.FindByXPath<Button>("//a[text()='Login']");
        loginTab.Click();

        var emailInput = App.Elements.FindById<TextField>("usernameOrEmail");
        emailInput.TypeText(testUser.Email);

        var passwordInput = App.Elements.FindById<Password>("password");
        passwordInput.SetPassword(testUser.Password);

        var rememberMeCheckbox = App.Elements.FindById<CheckBox>("rememberMe");
        rememberMeCheckbox.Check(true);

        var loginButton = App.Elements.FindByXPath<Button>("//button[text()='Login']");
        loginButton.Click();

        var twoFaCode = await AuthBypassService.Generate2FATokenAsync(testUser.Id);
        var twoFACodeInput = App.Elements.FindById<TextField>("twoFaToken");
        twoFACodeInput.TypeText(twoFaCode);

        loginButton = App.Elements.FindByXPath<Button>("//button[text()='Login']");
        loginButton.Click();

        VerifyUserLoggedIn(testUser.Username);
    }

    private void AuthenticateEmailPasswordNo2FA()
    {
        App.Navigation.GoToUrl("https://localhost:3000/");
        var loginTab = App.Elements.FindByXPath<Button>("//a[text()='Login']");
        loginTab.Click();

        var emailInput = App.Elements.FindById<TextField>("usernameOrEmail");
        emailInput.TypeText(_config.User);

        var passwordInput = App.Elements.FindById<Password>("password");
        passwordInput.SetPassword("password123");

        var loginButton = App.Elements.FindByXPath<Button>("//button[text()='Login']");
        loginButton.Click();

        VerifyUserLoggedIn(_config.User);
    }

    private async Task<TestUser> GetUserAsync()
    {
        if (_config.UseNewUser)
        {
            return await TestUserFactory.CreateDefaultWithRealEmailAsync();
        }

        if (_config.UseExistingUser)
        {
            throw new NotImplementedException("Using an existing user is not implemented yet.");
        }

        throw new ArgumentException("Invalid configuration for user creation.");
    }

    private async Task<TestUser> GetUserWith2FAAsync()
    {
        if (_config.UseNewUser)
        {
            return await TestUserFactory.CreateDefault2FAWithRealEmailAsync();
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
