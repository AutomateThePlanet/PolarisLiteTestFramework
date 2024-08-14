using DemoSystemTests.Authentication.Factories;
using DemoSystemTests.Authentication.Models;
using DemoSystemTests.Authentication.Plugins.Auth;
using DemoSystemTests.Authentication.Services;
using PolarisLite.API;
using PolarisLite.Integrations;
using PolarisLite.Web;
using PolarisLite.Web.Core.NUnit;
using PolarisLite.Web.Plugins;
using System.Text.RegularExpressions;

namespace DemoSystemTests.Authentication;

[TestFixture]
[LambdaTest(BrowserType.Chrome, enableAutoHealing: true, smartWait: 30)]
//[Authentication(AuthType.EMAIL_PASSWORD_2FA, useNewUser: true)]
public class AuthenticationPluginTests : WebTest
{
    protected override void TestInitialize()
    {
        ApiSettings.BaseUrl = "https://chesstv.local:3000/";
        TestUserFactory.ApiClientService = ApiApp.ApiClient;
        AuthBypassService.ApiClientService = ApiApp.ApiClient;

        App.Navigation.GoToUrl("https://chesstv.local:3000/");
    }

    [Test]
    public async Task ProfileUpdatedSuccessfully_When_NewUserUpdatesProfile()
    {
        var testUser = await TestUserFactory.CreateDefaultAsync();
        var loginTab = App.Elements.FindByXPath<Button>("//a[text()='Login']");
        loginTab.Click();

        var emailInput = App.Elements.FindById<TextField>("usernameOrEmail");
        emailInput.TypeText(testUser.Username);

        var passwordInput = App.Elements.FindById<Password>("password");
        passwordInput.SetPassword(testUser.Password);

        var rememberMeCheckbox = App.Elements.FindById<CheckBox>("rememberMe");
        rememberMeCheckbox.Check(true);

        //ByPassCaptcha();

        var loginButton = App.Elements.FindByXPath<Button>("//button[text()='Login']");
        loginButton.Click();

        var userName = App.Elements.FindById<Label>("username");
        Assert.That(userName.Text, Is.EqualTo(testUser.Username));

        var userNameEditInput = App.Elements.FindById<TextField>("editUsername");
        userNameEditInput.TypeText("newUserName");

        var updateProfileButton = App.Elements.FindByXPath<Button>("//button[text()='Update Profile']");
        updateProfileButton.Click();

        var logoutButton = App.Elements.FindByXPath<Button>("//a[text()='Logout']");
        logoutButton.Click();

        emailInput = App.Elements.FindById<TextField>("usernameOrEmail");
        emailInput.TypeText("newUserName");

        passwordInput = App.Elements.FindById<Password>("password");
        passwordInput.SetPassword(testUser.Password);

        rememberMeCheckbox = App.Elements.FindById<CheckBox>("rememberMe");
        rememberMeCheckbox.Check(true);

        //ByPassCaptcha();

        loginButton = App.Elements.FindByXPath<Button>("//button[text()='Login']");
        loginButton.Click();

        userNameEditInput = App.Elements.FindById<TextField>("editUsername");
        Assert.That(userNameEditInput.GetAttribute("value"), Is.EqualTo("newUserName"));
    }
}
