using DemoSystemTests.Integrations.Authentication.Factories;
using DemoSystemTests.Integrations.Authentication.Models;
using DemoSystemTests.Integrations.Authentication.Services;
using PolarisLite.API;
using PolarisLite.Integrations;
using PolarisLite.Web;
using PolarisLite.Web.Core.NUnit;
using PolarisLite.Web.Plugins;
using System.Text.RegularExpressions;

namespace DemoSystemTests.Integrations.Authentication;

[TestFixture]
//[LambdaTest(BrowserType.Chrome)]
[LocalExecution(BrowserType.Chrome, Lifecycle.RestartEveryTime)]
public class AuthenticationTests : WebTest
{
    protected override void TestInitialize()
    {
        ApiSettings.BaseUrl = "https://chesstv.local:3000/";
        TestUserFactory.ApiClient = ApiApp.ApiClient;
        AuthBypassService.ApiClientService = ApiApp.ApiClient;

        App.Navigation.GoToUrl("https://chesstv.local:3000/");
    }

    protected override void TestCleanup() 
    {
        App.Cookies.DeleteAllCookies();
    }

    [Test]
    public void LoginSuccessfullyUsingEmail()
    {
        var loginTab = App.Elements.FindByXPath<Button>("//a[text()='Login']");
        loginTab.Click();

        var emailInput = App.Elements.FindById<TextField>("usernameOrEmail");
        emailInput.TypeText("john@example.com");

        var passwordInput = App.Elements.FindById<Password>("password");
        passwordInput.SetPassword("password123");

        var rememberMeCheckbox = App.Elements.FindById<CheckBox>("rememberMe");
        rememberMeCheckbox.Check(true);

        ByPassCaptcha();

        var loginButton = App.Elements.FindByXPath<Button>("//button[text()='Login']");
        loginButton.Click();

        var userName = App.Elements.FindById<Label>("username");
        Assert.That(userName.Text, Is.EqualTo("johnDoe"));
    }

    [Test]
    public void ProfileUpdatedSuccessfully_When_NewUserUpdatesProfile()
    {
        var testUser = TestUserFactory.CreateDefaultAsync().Result;
        var loginTab = App.Elements.FindByXPath<Button>("//a[text()='Login']");
        loginTab.Click();

        var emailInput = App.Elements.FindById<TextField>("usernameOrEmail");
        emailInput.TypeText(testUser.Username);

        var passwordInput = App.Elements.FindById<Password>("password");
        passwordInput.SetPassword(testUser.Password);

        var rememberMeCheckbox = App.Elements.FindById<CheckBox>("rememberMe");
        rememberMeCheckbox.Check(true);

        ByPassCaptcha();

        var loginButton = App.Elements.FindByXPath<Button>("//button[text()='Login']");
        loginButton.Click();

        var userName = App.Elements.FindById<Label>("username");
        Assert.That(userName.Text, Is.EqualTo(testUser.Username));

        var userNameEditInput = App.Elements.FindById<TextField>("editUsername");
        userNameEditInput.TypeText(testUser.Username + 's');

        var updateProfileButton = App.Elements.FindByXPath<Button>("//button[text()='Update Profile']");
        updateProfileButton.Click();

        //App.Navigation.GoToUrl("https://chesstv.local:3000/");
        var logoutButton = App.Elements.FindByXPath<Button>("//a[text()='Logout']");
        logoutButton.Click();

        emailInput = App.Elements.FindById<TextField>("usernameOrEmail");
        emailInput.TypeText(testUser.Username + 's');

        passwordInput = App.Elements.FindById<Password>("password");
        passwordInput.SetPassword(testUser.Password);

        rememberMeCheckbox = App.Elements.FindById<CheckBox>("rememberMe");
        rememberMeCheckbox.Check(true);

        ByPassCaptcha();

        loginButton = App.Elements.FindByXPath<Button>("//button[text()='Login']");
        loginButton.Click();

        userNameEditInput = App.Elements.FindById<TextField>("editUsername");
        Assert.That(userNameEditInput.GetAttribute("value"), Is.EqualTo(testUser.Username + 's'));
    }

    [Test]
    public void PasswordSuccessfullyReset_When_RequestReset()
    {
        var testUser = TestUserFactory.CreateDefaultWithRealEmailAsync(UserStatus.PENDING).Result;
        var activateTab = App.Elements.FindByXPath<Button>("//a[text()='Request Reset']");
        activateTab.Click();

        var resetEmailInput = App.Elements.FindById<TextField>("resetEmail");
        resetEmailInput.TypeText(testUser.Email);

        var requestPasswordResetButton = App.Elements.FindByXPath<Button>("//button[text()='Request Password Reset']");
        requestPasswordResetButton.Click();

        Thread.Sleep(10000);
        var receivedEmail = MailslurpService.WaitForLatestEmail(testUser.UserInbox);
        var activationUrl = ExtractActivationUrl(receivedEmail.Body);
        App.Navigation.GoToUrl(activationUrl);

        var resetPasswordTab = App.Elements.FindById<Button>("reset-tab");
        resetPasswordTab.Click();

        var newPasswordInput = App.Elements.FindById<Password>("newPassword");
        newPasswordInput.SetPassword("password123");

        var resetPasswordButton = App.Elements.FindByXPath<Button>("//button[text()='Reset Password']");
        resetPasswordButton.Click();

        // login with the new password
        var loginTab = App.Elements.FindByXPath<Button>("//a[text()='Login']");
        loginTab.Click();

        var emailInput = App.Elements.FindById<TextField>("usernameOrEmail");
        emailInput.TypeText(testUser.Username);

        var passwordInput = App.Elements.FindById<Password>("password");
        passwordInput.SetPassword("password123");

        var rememberMeCheckbox = App.Elements.FindById<CheckBox>("rememberMe");
        rememberMeCheckbox.Check(true);

        ByPassCaptcha();

        var loginButton = App.Elements.FindByXPath<Button>("//button[text()='Login']");
        loginButton.Click();

        var userName = App.Elements.FindById<Label>("username");
        Assert.That(userName.Text, Is.EqualTo(testUser.Username));
    }

    [Test]
    public void FasterLoginWithCookie()
    {
        var testUser = TestUserFactory.CreateDefaultWithRealEmailAsync(UserStatus.PENDING).Result;
        var authCookieValue = AuthBypassService.GenerateAuthCookieAsync(testUser.Username, testUser.Password, testUser.Id.ToString()).Result;

        App.Cookies.AddCookie("auth", authCookieValue);
        App.Cookies.AddCookie("userId", testUser.Id.ToString());

        App.Navigation.GoToUrl("https://chesstv.local:3000/profile");

        var userName = App.Elements.FindById<Label>("username");
        Assert.That(userName.Text, Is.EqualTo(testUser.Username));
    }

    [Test]
    public void AccountSuccessfullyActivated_When_FillAllRequiredRegistrationFields()
    {
        var testUser = TestUserFactory.CreateTestUserDto();
        var registerTab = App.Elements.FindByXPath<Button>("//a[text()='Register']");
        registerTab.Click();

        var userNameInput = App.Elements.FindById<TextField>("registerUsername");
        userNameInput.TypeText(testUser.Username);

        var registerEmailInput = App.Elements.FindById<PolarisLite.Web.Email>("registerEmail");

        registerEmailInput.SetEmail(testUser.Email);

        var registerPhoneInput = App.Elements.FindById<TextField>("registerPhone");
        registerPhoneInput.TypeText(testUser.Phone);

        var registerPasswordInput = App.Elements.FindById<Password>("registerPassword");
        registerPasswordInput.SetPassword(testUser.Password);

        var confirmPasswordInput = App.Elements.FindById<Password>("confirmPassword");
        confirmPasswordInput.SetPassword(testUser.Password);

        var registerButton = App.Elements.FindByXPath<Button>("//button[text()='Register']");
        registerButton.Click();

        var activateTab = App.Elements.FindByXPath<Button>("//a[text()='Activate']");
        activateTab.Click();

        Thread.Sleep(10000);
        var currentTime = DateTime.Now.AddSeconds(-10);
        var receivedEmail = MailslurpService.WaitForLatestEmail(testUser.UserInbox, currentTime);
        var code = ExtractActivationCode(receivedEmail.Body);

        var activationCodeInput = App.Elements.FindById<TextField>("activationCode");
        activationCodeInput.TypeText(code);

        var activateButton = App.Elements.FindByXPath<Button>("//button[text()='Activate']");
        activateButton.Click();

        // Try to login
        var loginTab = App.Elements.FindByXPath<Button>("//a[text()='Login']");
        loginTab.Click();

        var emailInput = App.Elements.FindById<TextField>("usernameOrEmail");
        emailInput.TypeText(testUser.Username);

        var passwordInput = App.Elements.FindById<Password>("password");
        passwordInput.SetPassword(testUser.Password);

        var rememberMeCheckbox = App.Elements.FindById<CheckBox>("rememberMe");
        rememberMeCheckbox.Check(true);

        ByPassCaptcha();

        var loginButton = App.Elements.FindByXPath<Button>("//button[text()='Login']");
        loginButton.Click();

        var userName = App.Elements.FindById<Label>("username");
        Assert.That(userName.Text, Is.EqualTo(testUser.Username));
    }


    private string ExtractActivationCode(string message)
    {
        var pattern = @"Your activation code is: ([a-zA-Z0-9]+)\s*$";
        var match = Regex.Match(message, pattern);
        return match.Success ? match.Groups[1].Value : null;
    }

    private string ExtractActivationUrl(string message)
    {
        var pattern = @"http[s]?://[^\s""]+";
        var match = Regex.Match(message, pattern);
        return match.Success ? match.Value : null;
    }

    private void ByPassCaptcha()
    {
        var captchaByPass = App.Elements.FindByXPath<TextField>("//input[@name='captcha-bypass']");
        App.JavaScript.Execute("arguments[0].setAttribute('value', arguments[1]);", captchaByPass, "10685832-cd90-4e91-9224-2ef69ce88f53");
    }

}
