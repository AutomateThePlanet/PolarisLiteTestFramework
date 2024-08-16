using mailslurp.Api;
using mailslurp.Model;
using PolarisLite.Core.Utilities;
using PolarisLite.Integrations.Settings;

namespace PolarisLite.Integrations;

public static class MailslurpService
{
    private const long TIMEOUT = 60000L;
    private static InboxControllerApi _inboxControllerApi;
    private static WaitForControllerApi _waitForControllerApi;

    static MailslurpService()
    {
        var config = new mailslurp.Client.Configuration();
        config.ApiKey.Add("x-api-key", IntegrationSettings.MailslurpSettings.ApiKey);
        _inboxControllerApi = new InboxControllerApi(config);
        _waitForControllerApi = new WaitForControllerApi(config);
    }

    public static InboxDto CreateInbox()
    {
        var inbox = _inboxControllerApi.CreateInboxWithDefaults();

        return inbox;
    }

    public static InboxDto CreateInbox(string name)
    {
        var inbox = _inboxControllerApi.CreateInbox();

        return inbox;
    }

    public static mailslurp.Model.Email WaitForLatestEmail(InboxDto inbox, DateTime? since = null)
    {
        if (since == null)
        {
            since = DateTime.UtcNow.AddMinutes(-5);
        }

        mailslurp.Model.Email receivedEmail = _waitForControllerApi.WaitForLatestEmail(inbox.Id, TIMEOUT, false, null, since, null, 10000L);

        return receivedEmail;
    }

    public static void SendEmail(InboxDto inbox, string toEmail, string subject, string templateName)
    {
        var emailBody = EmbeddedResourcesService.FromFile(templateName);
        // send HTML body email
        var sendEmailOptions = new SendEmailOptions();
        sendEmailOptions.To = new List<string> { toEmail };
        sendEmailOptions.Subject = subject;
        sendEmailOptions.Body = emailBody;

        _inboxControllerApi.SendEmail(inbox.Id, sendEmailOptions);
    }
}
