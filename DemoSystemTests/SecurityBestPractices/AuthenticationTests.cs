using PolarisLite.Web.Core.NUnit;
using PolarisLite.Web.Plugins;
using PolarisLite.Web;
using PolarisLite.Web.Assertions;
using DemoSystemTests.Integrations.Authentication.Factories;
using DemoSystemTests.Integrations.Authentication.Services;

namespace DemoSystemTests.SecurityBestPractices;
[TestFixture]
//[LocalExecution]
[LambdaTest(useTunnel:true)]
public class AuthenticationTests : WebTest
{
    [Test]
    public void LoginSuccessfully_UsingEmail()
    {
        // NOTE: Need to map localhost to chesstv.local in order CAPTCHA to work properly
        // Navigate to the login page
        App.Navigation.GoToUrl("http://chesstv.local:3000/");

        // Find and click the Login tab
        var loginTab = App.Elements.FindByXPath<Anchor>("//a[text()='Login']");
        loginTab.Click();

        // Input email and password
        var emailInput = App.Elements.FindById<Email>("usernameOrEmail");
        emailInput.SetEmail("john@example.com");

        var passwordInput = App.Elements.FindById<Password>("password");
        passwordInput.SetPassword("password123");

        // Check the Remember Me checkbox
        var rememberMeCheckbox = App.Elements.FindById<CheckBox>("rememberMe");
        rememberMeCheckbox.Check();

        // Bypass the CAPTCHA
        BypassCaptcha();

        // Click the Login button
        var loginButton = App.Elements.FindByXPath<Button>("//button[text()='Login']");
        loginButton.Click();

        // Validate the username is displayed correctly after login
        var userName = App.Elements.FindById<Label>("username");
        userName.ValidateInnerTextIs("johnDoe");

        // Click the Logout button
        var logoutButton = App.Elements.FindByXPath<Anchor>("//a[text()='Logout']");
        logoutButton.Click();
    }

    [Test]
    public void LoginSuccessfully_UsingEmailAndBypass2FA()
    {
        // NOTE: Need to map localhost to chesstv.local in order CAPTCHA to work properly
        // Navigate to the login page
        App.Navigation.GoToUrl("http://chesstv.local:3000/");

        // Find and click the Login tab
        var loginTab = App.Elements.FindByXPath<Anchor>("//a[text()='Login']");
        loginTab.Click();

        // Assuming TestUserFactory returns a test user with 2FA enabled
        var testUser = TestUserFactory.CreateDefault2FAWithRealEmailAsync().Result;

        // Input email and password
        var emailInput = App.Elements.FindById<Email>("usernameOrEmail");
        emailInput.SetEmail(testUser.Email);

        var passwordInput = App.Elements.FindById<Password>("password");
        passwordInput.SetPassword(testUser.Password);

        // Check the Remember Me checkbox
        var rememberMeCheckbox = App.Elements.FindById<CheckBox>("rememberMe");
        rememberMeCheckbox.Check();

        // Bypass the CAPTCHA
        BypassCaptcha();

        // Click the Login button
        var loginButton = App.Elements.FindByXPath<Button>("//button[text()='Login']");
        loginButton.Click();

        // Bypass 2FA by generating a 2FA token
        var twoFaCode = AuthBypassService.Generate2FATokenAsync(testUser.Id).Result;
        var twoFACodeInput = App.Elements.FindById<TextField>("twoFaToken");
        twoFACodeInput.TypeText(twoFaCode);

        // Click the Login button again to complete the 2FA process
        loginButton.Click();

        // Validate the username is displayed correctly after login
        var userName = App.Elements.FindById<Label>("username");
        userName.ValidateInnerTextIs(testUser.Username);

        // Click the Logout button
        var logoutButton = App.Elements.FindByXPath<Anchor>("//a[text()='Logout']");
        logoutButton.Click();
    }

    private void BypassCaptcha()
    {
        var captchaByPass = App.Elements.FindByXPath<Div>("//input[@name='captcha-bypass']");
        captchaByPass.SetAttribute("value", "10685832-cd90-4e91-9224-2ef69ce88f53");
        //App.JavaScript.Execute("arguments[0].setAttribute('value', arguments[1]);", captchaByPass, "10685832-cd90-4e91-9224-2ef69ce88f53");
    }
}
