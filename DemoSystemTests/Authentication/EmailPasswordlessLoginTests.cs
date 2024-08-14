using DemoSystemTests.Authentication.Factories;
using DemoSystemTests.Authentication.Services;
using mailslurp.Api;
using mailslurp.Model;
using PolarisLite.API;
using PolarisLite.Integrations;
using PolarisLite.Web;
using PolarisLite.Web.Core.NUnit;
using PolarisLite.Web.Plugins;
using System.Text.RegularExpressions;

namespace DemoSystemTests.Authentication;
[TestFixture]
[LambdaTest(BrowserType.Chrome, enableAutoHealing: true, smartWait: 30)]
public class EmailPasswordlessLoginTests : WebTest
{
    private static InboxControllerApi _inboxControllerApi;
    private static string API_KEY = Environment.GetEnvironmentVariable("MAILSLURP_KEY");
    private static readonly long TIMEOUT = 30000L;

    protected override void TestInitialize()
    {
        ApiSettings.BaseUrl = "https://localhost:3000/";
        TestUserFactory.ApiClientService = ApiApp.ApiClient;
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

    [Test]
    public async Task InteractWithEmailBody()
    {
        var inbox = await _inboxControllerApi.CreateInboxAsync();

        var email = inbox.EmailAddress;
        await SendEmail(inbox, email);

        var waitForControllerApi = new WaitForControllerApi();
        var receivedEmail = await waitForControllerApi.WaitForLatestEmailAsync(inbox.Id, TIMEOUT, false, since: DateTime.Now);

        LoadEmailBody(receivedEmail.Body);

        var myAccountLink = App.Elements.FindByXPath<Button>("//a[contains(text(), 'My Account')]");
        myAccountLink.Click();
    }

    private async Task SendEmail(InboxDto inbox, string toEmail)
    {
        var emailBody = File.ReadAllText("path-to-your-sample-email.html");
        var sendEmailOptions = new SendEmailOptions
        {
            To = new List<string> { toEmail },
            Subject = "HTML BODY email Interaction",
            Body = emailBody
        };
        await _inboxControllerApi.SendEmailAsync(inbox.Id, sendEmailOptions);
    }

    private void LoadEmailBody(string htmlBody)
    {
        htmlBody = htmlBody.Replace("\n", "").Replace("\\/", "/").Replace("\\\"", "\"");
        var filePath = WriteStringToTempFile(htmlBody);
        App.Navigation.GoToUrl(new Uri(filePath).ToString());
    }

    private string WriteStringToTempFile(string fileContent)
    {
        var tempFile = Path.GetTempFileName() + ".html";
        File.WriteAllText(tempFile, fileContent);
        return tempFile;
    }

    private string ExtractCode(string message)
    {
        var pattern = @"code is: (\d+)\s*$";
        var match = Regex.Match(message, pattern);
        return match.Success ? match.Groups[1].Value : null;
    }
}