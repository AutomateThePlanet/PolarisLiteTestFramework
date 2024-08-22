using DemoSystemTests.Integrations.Authentication.Factories;
using DemoSystemTests.Integrations.Authentication.Services;
using mailslurp.Api;
using mailslurp.Model;
using PolarisLite.API;
using PolarisLite.Integrations;
using PolarisLite.Web;
using PolarisLite.Web.Core.NUnit;
using PolarisLite.Web.Plugins;
using System.Text.RegularExpressions;

namespace DemoSystemTests.Integrations.Authentication;
[TestFixture]
[LambdaTest(BrowserType.Chrome, useTunnel: true)]
public class EmailPasswordlessLoginTests : WebTest
{
    protected override void TestInitialize()
    {
        ApiSettings.BaseUrl = "https://chesstv.local:3000/";
        TestUserFactory.ApiClient = ApiApp.ApiClient;
        AuthBypassService.ApiClientService = ApiApp.ApiClient;
        App.Navigation.GoToUrl(ApiSettings.BaseUrl);
    }

    [Test]
    public void LoginSuccessfullyUsingEmail()
    {
        var emailTab = App.Elements.FindByXPath<Button>("//a[text()='Email']");
        emailTab.Click();

        var inbox = MailslurpService.CreateInbox();

        var emailInput = App.Elements.FindById<TextField>("email");
        emailInput.TypeText(inbox.EmailAddress);

        var sendLoginCode = App.Elements.FindByXPath<Button>("//button[text()='Send Login Code']");
        sendLoginCode.Click();

        var receivedEmail = MailslurpService.WaitForLatestEmail(inbox);

        var emailCodeInput = App.Elements.FindById<TextField>("code");
        var emailCode = ExtractCode(receivedEmail.Body);
        emailCodeInput.TypeText(emailCode);

        var verifyButton = App.Elements.FindByXPath<Button>("//button[text()='Verify Code']");
        verifyButton.Click();

        var userName = App.Elements.FindById<Label>("username");
        Assert.That(userName.Text.Contains("User"), Is.True);

        var logoutButton = App.Elements.FindByXPath<Button>("//a[text()='Logout']");
        logoutButton.Click();
    }

    private string ExtractCode(string message)
    {
        var pattern = @"code is: (\d+)\s*$";
        var match = Regex.Match(message, pattern);
        return match.Success ? match.Groups[1].Value : null;
    }
}