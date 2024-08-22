using DemoSystemTests.Integrations.Authentication.Plugins.Sms;
using PolarisLite.Integrations;
using PolarisLite.Integrations.Settings;
using PolarisLite.Web;
using PolarisLite.Web.Core.NUnit;
using PolarisLite.Web.Plugins;
using System.Text.RegularExpressions;

namespace DemoSystemTests.Integrations.Authentication;
[TestFixture]
[LambdaTest(BrowserType.Chrome, useTunnel: true)]
[ListenForSMS]
public class SMSPasswordlessLoginTests : WebTest
{
    private SmsListener _smsListener;

    protected override void TestInitialize()
    {
        App.Navigation.GoToUrl("https://chesstv.local:3000/");
        _smsListener = TwillioService.ListenForSms(IntegrationSettings.TwilioSettings.PhoneNumber);
    }

    [Test]
    public void LoginSuccessfullyUsingSms()
    {
        var smsTab = App.Elements.FindByXPath<Button>("//a[text()='SMS']");
        smsTab.Click();

        var phoneInput = App.Elements.FindById<TextField>("phoneNumber");
        phoneInput.TypeText(IntegrationSettings.TwilioSettings.PhoneNumber);

        var sendSmsButton = App.Elements.FindByXPath<Button>("//button[text()='Send SMS Code']");
        sendSmsButton.Click();

        // Wait and get the latest SMS
        var latestMessage = TwillioService.GetLastMessage(_smsListener);

        var smsCodeInput = App.Elements.FindById<TextField>("smsCode");
        var smsCode = ExtractCode(latestMessage.Body);
        smsCodeInput.TypeText(smsCode);

        var verifyButton = App.Elements.FindByXPath<Button>("//button[text()='Verify SMS Code']");
        verifyButton.Click();

        var userName = App.Elements.FindById<Label>("username");
        Assert.That(userName.Text.Contains("User"), Is.True);

        var logoutButton = App.Elements.FindByXPath<Button>("//a[text()='Logout']");
        logoutButton.Click();
    }

    public static string ExtractCode(string message)
    {
        var pattern = @"code is: (\d+)$";
        var match = Regex.Match(message, pattern);
        return match.Success ? match.Groups[1].Value : null;
    }

    protected override void TestCleanup()
    {
        if (_smsListener != null)
        {
            TwillioService.StopListeningForSms(_smsListener);
        }

        base.TestCleanup();
    }
}